import { BinaryWriter } from '@bufbuild/protobuf/wire';
import { DecodedWebcastPushFrame, WebSocketParams } from '@/types/client';
import { createBaseWebcastPushFrame, deserializeWebSocketMessage } from '@/lib/utilities';
import Config from '@/lib/config';
import TypedEventEmitter from 'typed-emitter';
import CookieJar from '@/lib/web/lib/cookie-jar';
import { HeartbeatMessage, WebcastImEnterRoomMessage } from '@/types';
import { ClientOptions, WebSocket } from 'ws';

const textEncoder = new TextEncoder();

type EventMap = {
    close: () => void;
    messageDecodingFailed: (error: Error) => void;
    protoMessageFetchResult: (response: any) => void;
    webSocketData: (data: Buffer) => void;
    imEnteredRoom: (decodedContainer: DecodedWebcastPushFrame) => void;
};

type TypedWebSocket = new (...args: any[]) => WebSocket & TypedEventEmitter<EventMap>;

export default class TikTokWsClient extends (WebSocket as TypedWebSocket) {
    protected pingInterval: NodeJS.Timeout | null;

    constructor(
        wsUrl: string,
        cookieJar: CookieJar,
        protected readonly webSocketParams: WebSocketParams,
        webSocketHeaders: Record<string, string>,
        webSocketOptions: ClientOptions,
        protected webSocketPingIntervalMs: number = 10000
    ) {
        const wsHeaders = { Cookie: cookieJar.getCookieString(), ...(webSocketHeaders || {}) };
        const wsUrlWithParams = `${wsUrl}?${new URLSearchParams(webSocketParams)}${Config.DEFAULT_WS_CLIENT_PARAMS_APPEND_PARAMETER}`;

        super(
            wsUrlWithParams,
            {
                headers: wsHeaders,
                host: `https://${Config.TIKTOK_HOST_WEB}`,
                ...webSocketOptions,
                autoPong: false
            }
        );

        this.pingInterval = null;
        this.on('open', this.onOpen.bind(this));
        this.on('message', this.onMessage.bind(this));
        this.on('close', this.onDisconnect.bind(this));
    }

    protected onOpen() {
        this.sendHeartbeat();
        this.pingInterval = setInterval(() => this.sendHeartbeat(), this.webSocketPingIntervalMs);
    }

    public get open(): boolean {
        return this.readyState === WebSocket.OPEN;
    }

    /**
     * Send a message to the WebSocket server
     * @param data The message to send
     * @returns True if the message was sent, false otherwise
     */
    public sendBytes(data: Uint8Array): boolean {
        if (this.open) {
            super.send(Buffer.from(data));
            return true;
        }
        return false;
    }

    protected onDisconnect() {
        clearInterval(this.pingInterval);
        this.pingInterval = null;
    }

    /**
     * Handle incoming messages
     * @param message The incoming WebSocket message (type => Buffer)
     * @protected
     */
    protected async onMessage(message: Buffer) {

        // Emit WebSocket data
        this.emit('webSocketData', message);

        //  If the message is binary, decode it
        try {
            const decodedContainer: DecodedWebcastPushFrame = await deserializeWebSocketMessage(message);

            // If the message has a decoded protoMessageFetchResult, emit it
            if (decodedContainer.protoMessageFetchResult) {

                // If it needs an ack, send the ack
                if (decodedContainer.protoMessageFetchResult.needsAck) {
                    this.sendAck(decodedContainer);
                }

                this.emit('protoMessageFetchResult', decodedContainer.protoMessageFetchResult);
            }

            // If it's a room enter, emit
            if (decodedContainer.payloadType === 'im_enter_room_resp') {
                this.emit('imEnteredRoom', decodedContainer);
            }

        } catch (err) {
            this.emit('messageDecodingFailed', err);
        }

    }

    /**
     * Static Keep-Alive ping
     */
    protected sendHeartbeat() {
        const { room_id } = this.webSocketParams;

        // Create the heartbeat
        const hb: BinaryWriter = HeartbeatMessage.encode({ roomId: room_id });

        // Wrap it in the WebcastPushFrame
        const webcastPushFrame: BinaryWriter = createBaseWebcastPushFrame(
            {
                payloadEncoding: 'pb',
                payloadType: 'hb',
                payload: hb.finish(),
                service: undefined,
                method: undefined,
                headers: {}
            }
        );

        this.sendBytes(Buffer.from(webcastPushFrame.finish()));
    }

    /**
     * EXPERIMENTAL: Switch to a different TikTok LIVE room while connected to the WebSocket
     * @param roomId The room ID to switch to
     */
    public switchRooms(roomId: string): void {

        const imEnterRoomMessage: BinaryWriter = WebcastImEnterRoomMessage.encode(
            {
                roomId: roomId,
                roomTag: '',
                liveRegion: '',
                liveId: '12', // Static value for all streams (via decompiled APK)
                identity: 'audience',
                cursor: '',
                accountType: '0',
                enterUniqueId: '',
                filterWelcomeMsg: '0',
                isAnchorContinueKeepMsg: false
            }
        );

        const webcastPushFrame: BinaryWriter = createBaseWebcastPushFrame(
            {
                payloadEncoding: 'pb',
                payloadType: 'im_enter_room',
                payload: imEnterRoomMessage.finish()
            }
        );

        this.sendBytes(Buffer.from(webcastPushFrame.finish()));

    }


    /**
     * Acknowledge the message was received
     */
    protected sendAck({ logId, protoMessageFetchResult: { internalExt } }: DecodedWebcastPushFrame): void {

        // Always send an ACK for the message
        if (!logId) {
            return;
        }

        const webcastPushFrame: BinaryWriter = createBaseWebcastPushFrame(
            {
                logId: logId,
                payloadEncoding: 'pb',
                payloadType: 'ack',
                payload: textEncoder.encode(internalExt)
            }
        );

        this.sendBytes(Buffer.from(webcastPushFrame.finish()));
    }

}

