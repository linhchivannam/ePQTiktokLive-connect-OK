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
        private static long NewLogId() => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        private static long NewSeqId() => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        /// Gửi khung "enter room": payload là WebcastImEnterRoomMessage, bọc trong WebcastPushFrame
        public static byte[] BuildEnterFrame(long roomId, string identity = "audience")
        {
            var enter = new WebcastImEnterRoomMessage
            {
                RoomId = roomId,
                LiveId = 12,               // live_id=12 cho web
                Identity = identity,       // "audience"
                RoomTag = "",
                LiveRegion = "",
                AccountType = 1            // an toàn: 1 = normal (tuỳ proto của bạn)
                                           // ... Nếu proto bạn có thêm field bắt buộc thì set thêm ở đây
            };

            var frame = new WebcastPushFrame
            {
                // Quan trọng
                Payload = enter.ToByteString(),
                PayloadType = "protobuf",
                PayloadEncoding = "gzip",       // hoặc "protobuf" – tuỳ server (thường "gzip" OK)
                //NeedAck = true,

                //// Dùng string thay vì mã số service/method để tránh sai mapping
                //ServicePath = "webcast.im",
                //MethodName = "enter_room",

                // Metadata
                SeqId = NewSeqId(),
                LogId = NewLogId(),
            };

            // Nếu server không chấp nhận string, bạn có thể set thêm số:
            // frame.Service = 7;         // ví dụ: IM service (tuỳ bản proto/mapping của bạn)
            // frame.Method  = 10000;     // ví dụ: ENTER (tuỳ)
            return frame.ToByteArray();
        }
        /// Gửi subscribe frame – thường chỉ cần set Cursor (nếu có)
        public static byte[] BuildSubscribeFrame(string cursor = "")
        {
            var frame = new WebcastPushFrame
            {
                Payload = ByteString.Empty,
                PayloadType = "protobuf",
                PayloadEncoding = "gzip",
               // NeedAck = false,

                //ServicePath = "webcast.im",
                //MethodName = "websocket_sub", // đúng tinh thần Node

                //Cursor = cursor ?? "",

                SeqId = NewSeqId(),
                LogId = NewLogId(),
            };

            return frame.ToByteArray();
        }
        /// Gửi heartbeat – payload: HeartbeatMessage(room_id)
        public static byte[] BuildHeartbeatFrame(long roomId)
        {
            var hb = new HeartbeatMessage
            {
                RoomId = (ulong)roomId
            };

            var frame = new WebcastPushFrame
            {
                Payload = hb.ToByteString(),
                PayloadType = "protobuf",
                PayloadEncoding = "gzip",
                //NeedAck = false,

                //ServicePath = "webcast.im",
                //MethodName = "heartbeat",

                SeqId = NewSeqId(),
                LogId = NewLogId(),
            };

            return frame.ToByteArray();
        }
    }
}
