using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTok;
using Google.Protobuf;
using System.IO.Compression;
using System.IO;
using System.Threading;
using TikTok.Proto;

namespace ePQTiktokLive.protobuf
{
    public static class WsFrames
    {
        public static byte[] BuildEnterFrame(string roomId)
        {
            var enter = new WebcastImEnterRoomMessage
            {
                RoomId = long.Parse(roomId),
                Identity = TikTokSession.Identity,
                // nếu file proto của bạn có thêm appId / clientType thì set thêm ở đây
            };

            var request = new WebcastWebsocketRequest
            {
                PayloadType = "enter",
                Payload = enter.ToByteString(),
                LogId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(),
                SeqId = TikTokSession.NextSeqId()
            };

            var frame = new WebcastPushFrame
            {
                //Type = "msg",
                PayloadType = "enter",
                Payload = request.ToByteString()
            };

            return frame.ToByteArray();
        }

        public static byte[] BuildSubFrame(string roomId, int subType)
        {
            var sub = new WebcastImClientSendMessage
            {
                RoomId = long.Parse(roomId),
                SubType = subType
            };

            var request = new WebcastWebsocketRequest
            {
                PayloadType = "sub",
                Payload = sub.ToByteString(),
                LogId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(),
                SeqId = TikTokSession.NextSeqId()
            };

            var frame = new WebcastPushFrame
            {
                //Type = "msg",
                PayloadType = "sub",
                Payload = request.ToByteString()
            };

            return frame.ToByteArray();
        }



        public static byte[] BuildHeartbeatFrame()
        {
            var hb = new WebcastImHeartbeatMessage
            {
                ClientSendTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                LastRtt = 0
            };

            var request = new WebcastWebsocketRequest
            {
                PayloadType = "heartbeat",
                Payload = hb.ToByteString(),
                LogId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(),
                SeqId = TikTokSession.NextSeqId()
            };

            var frame = new WebcastPushFrame
            {
                //Type = "msg",
                PayloadType = "heartbeat",
                Payload = request.ToByteString()
            };

            return frame.ToByteArray();
        }
        public static byte[] BuildAckFrame(long serverTimestamp, params long[] received)
        {
            var ack = new WebcastAckMessage
            {
                ServerTimestamp = serverTimestamp
            };
            ack.ReceivedMessages.AddRange(received);

            var request = new WebcastWebsocketRequest
            {
                PayloadType = "ack",
                Payload = ack.ToByteString(),
                LogId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(),
                SeqId = TikTokSession.NextSeqId()
            };

            var frame = new WebcastPushFrame
            {
                //Type = "msg",
                PayloadType = "ack",
                Payload = request.ToByteString()
            };

            return frame.ToByteArray();
        }


    }
    public static class TikTokSession
    {
        // Sinh identity một lần cho cả session
        public static readonly string Identity = Guid.NewGuid().ToString("N");

        private static long _seqId = 0;

        public static long NextSeqId()
        {
            return Interlocked.Increment(ref _seqId);
        }
    }
}
