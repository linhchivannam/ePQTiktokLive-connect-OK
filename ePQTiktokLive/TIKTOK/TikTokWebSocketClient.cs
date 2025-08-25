using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ePQTiktokLive.protobuf;
using Google.Protobuf;
using TikTok;

namespace ePQTiktokLive.TIKTOK
{
    

    public class TikTokWebSocketClient
    {
        private readonly string _wsUrl;
        private readonly string _roomId;
        private readonly string _cookie;
        private readonly ClientWebSocket _ws;
        private CancellationTokenSource _cts;
        private Timer _heartbeatTimer;

        public event Action<WebcastPushFrame> OnFrameReceived;

        public TikTokWebSocketClient(string wsUrl, string roomId, string cookie)
        {
            _wsUrl = wsUrl;
            _roomId = roomId;
            _cookie = cookie;

            _ws = new ClientWebSocket();
            //_ws.Options.SetRequestHeader("User-Agent",
            //    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36");
            //_ws.Options.SetRequestHeader("Referer", $"https://www.tiktok.com/");
            _ws.Options.SetRequestHeader("Origin", "https://www.tiktok.com");
            //_ws.Options.SetRequestHeader("Accept-Encoding", "gzip, deflate, br, zstd");
            //_ws.Options.SetRequestHeader("Accept-Language", "en-US,en;q=0.9");
            _ws.Options.SetRequestHeader("Cookie", _cookie);


        }

        public async Task ConnectAsync()
        {
            _cts = new CancellationTokenSource();

            await _ws.ConnectAsync(new Uri(_wsUrl), _cts.Token);
            Console.WriteLine("Connected to TikTok WS");

            // gửi enter + sub ngay khi kết nối
            await _ws.SendAsync(BuildEnterFrame(_roomId), WebSocketMessageType.Binary, true, _cts.Token);
            await _ws.SendAsync(BuildSubFrame(_roomId, 1), WebSocketMessageType.Binary, true, _cts.Token);
            await _ws.SendAsync(BuildSubFrame(_roomId, 2), WebSocketMessageType.Binary, true, _cts.Token);
            await _ws.SendAsync(BuildSubFrame(_roomId, 3), WebSocketMessageType.Binary, true, _cts.Token);
            await _ws.SendAsync(BuildSubFrame(_roomId, 4), WebSocketMessageType.Binary, true, _cts.Token);

            // Bắt heartbeat timer
            _heartbeatTimer = new Timer(async _ =>
            {
                await _ws.SendAsync(BuildHeartbeatFrame(), WebSocketMessageType.Binary, true, _cts.Token);
            }, null, TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(20));

            // Bắt đầu vòng đọc dữ liệu
            _ = Task.Run(ReceiveLoop);
        }

        private async Task ReceiveLoop()
        {
            var buffer = new byte[1024 * 64];
            try
            {
                while (_ws.State == WebSocketState.Open)
                {
                    var result = await _ws.ReceiveAsync(new ArraySegment<byte>(buffer), _cts.Token);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        Console.WriteLine("Server closed connection");
                        break;
                    }

                    var data = new byte[result.Count];
                    Array.Copy(buffer, data, result.Count);

                    try
                    {
                        var frame = WebcastPushFrame.Parser.ParseFrom(data);
                        OnFrameReceived?.Invoke(frame);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Parse error: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Receive loop error: " + ex.Message);
            }
        }

        public async Task DisconnectAsync()
        {
            try
            {
                _cts?.Cancel();
                if (_ws.State == WebSocketState.Open)
                    await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "bye", CancellationToken.None);
            }
            catch { }
        }

        // wrapper để gửi mảng byte
        private ArraySegment<byte> BuildEnterFrame(string roomId)
            => new ArraySegment<byte>(WsFrames.BuildEnterFrame(roomId));

        private ArraySegment<byte> BuildSubFrame(string roomId, int subType)
            => new ArraySegment<byte>(WsFrames.BuildSubFrame(roomId, subType));

        private ArraySegment<byte> BuildHeartbeatFrame()
            => new ArraySegment<byte>(WsFrames.BuildHeartbeatFrame());

        public ArraySegment<byte> BuildAckFrame(long serverTimestamp, params long[] received)
            => new ArraySegment<byte>(WsFrames.BuildAckFrame(serverTimestamp, received));
    }

}
