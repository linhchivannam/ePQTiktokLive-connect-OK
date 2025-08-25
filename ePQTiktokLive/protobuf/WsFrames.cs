using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTok;
using Google.Protobuf;
namespace ePQTiktokLive.protobuf
{
    public static class WsFrames
    {
        // helper logId/seq
        private static long NewLogId() =>  BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        private static long NewSeqId() => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        /// Gửi khung "enter room": payload là WebcastImEnterRoomMessage, bọc trong WebcastPushFrame
        public static byte[] BuildEnterFrame(long roomId, string cursor = "")
        {
            var enter = new WebcastImEnterRoomMessage
            {
                RoomId = roomId,
                Identity = "audience",       // audience
                Cursor = cursor ?? ""
            };

            var frame = new WebcastPushFrame
            {
                PayloadType = "en",
                PayloadEncoding = "protobuf",
                Payload = enter.ToByteString(),
                SeqId = Convert.ToInt64(roomId),// NewSeqId(),
                LogId = NewLogId()
            };
            return frame.ToByteArray(); // ❗ gửi thẳng, KHÔNG gzip toàn frame
        }
        /// Gửi subscribe frame – thường chỉ cần set Cursor (nếu có)
        public static byte[] BuildSubscribeFrame(string roomId)
        {
            var json = "{\"room_id\":\"" + roomId + "\",\"message_type\":\"msg,gift,like,member,room_user_seq,control\"}";
            var subMsg = new WebcastWebsocketMessage
            {
                Type = "subscribe",
                Payload = ByteString.CopyFromUtf8(json)
            };

            var frame = new WebcastPushFrame
            {
                PayloadType = "sub",
                PayloadEncoding = "json",
                Payload = subMsg.ToByteString(),
                SeqId = Convert.ToInt64(roomId),// NewSeqId(),
                LogId = NewLogId()
            };
            return frame.ToByteArray(); // ❗ KHÔNG gzip toàn frame
        }

        // Heartbeat: payload = HeartbeatMessage(room_id), type = "hb"
        public static byte[] BuildHeartbeatFrame(long roomId)
        {
            var hb = new HeartbeatMessage { RoomId = (ulong)roomId };

            var frame = new WebcastPushFrame
            {
                PayloadType = "hb",
                PayloadEncoding = "protobuf",
                Payload = hb.ToByteString(),
                SeqId = Convert.ToInt64(roomId),
                LogId = NewLogId()
            };
            return frame.ToByteArray(); // ❗ KHÔNG base64, KHÔNG gzip toàn frame
        }
    }
}
