using ePQTiktokLive.TIKTOK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Google.Protobuf;
using TikTok;
using WebSocketSharp;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.GZip;
using ePQTiktokLive.MODEL;
using ePQTiktokLive.USERCONTROL;
using System.Text.Json;
using ePQTiktokLive.protobuf;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net.Sockets;
using System.Net.WebSockets;
using TikTok.Proto;

namespace ePQTiktokLive.LIVE
{
    public partial class frm_TTLiveConnect : Form
    {
        public frm_TTLiveConnect()
        {
            InitializeComponent();
        }

        private static System.Threading.Timer _timer;
        List<PQComment> dsComment;

        string filelog = "";
        bool ghilog = false;


        private readonly string _wsUrl;
        private readonly string _roomId;
        private readonly string _cookie;
        private readonly ClientWebSocket _ws;
        private CancellationTokenSource _cts;
        private System.Threading.Timer _heartbeatTimer;

        private static readonly List<(string Name, Func<byte[], IMessage> Parse)> parsers = new List<(string Name, Func<byte[], IMessage> Parse)>
    {
        ("WebcastChatMessage", bytes => WebcastChatMessage.Parser.ParseFrom(bytes)),
        ("WebcastGiftMessage", bytes => WebcastGiftMessage.Parser.ParseFrom(bytes)),
        ("WebcastLikeMessage", bytes => WebcastLikeMessage.Parser.ParseFrom(bytes)),
        ("WebcastMemberMessage", bytes => WebcastMemberMessage.Parser.ParseFrom(bytes)),
        ("WebcastControlMessage", bytes => WebcastControlMessage.Parser.ParseFrom(bytes)),
        ("WebcastRoomUserSeqMessage", bytes => WebcastRoomUserSeqMessage.Parser.ParseFrom(bytes)),
        
    };

        private void pTiktok_Click(object sender, EventArgs e)
        {

        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            string liveUrl = "https://www.tiktok.com/@"+txtTiktoklive.Text.Trim().ToLower()+"/live";

            // B1: Nếu chưa có cookies.json thì mở Puppeteer login và lưu session
            if (!File.Exists("cookies.json"))
            {
                var roomInfo = await TikTokSessionSaver.SaveSessionAsync(liveUrl);
                if (roomInfo != null)
                {
                    if (roomInfo.is_live)
                    {
                        btnConnect.Text = "Stop";
                        btnConnect.BackColor = Color.Green;
                        lbTiktokUser.Text = roomInfo.host_name;
                        TikTokWebsocket(roomInfo);
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                var roomInfo =  TikTokSessionSaver.ReadRoomInfo("roominfo.json");
                if (roomInfo != null)
                {
                    if (roomInfo.is_live)
                    {
                        btnConnect.Text = "Stop";
                        btnConnect.BackColor = Color.Green;
                        lbTiktokUser.Text = roomInfo.host_name;
                        TikTokWebsocket(roomInfo);
                    }
                    else
                    {

                    }
                }
            }
            
        }
        private async void TikTokWebsocket(TikTokRoomInfo myroom)
        {
            string roomId = myroom.room_id;
            string wsUrl = TikTokWebSocketUrlBuilder.BuildUrl(roomId);
            string cookieHeader = TikTokWebSocketUrlBuilder.GetCookieHeader();

            var client = new TikTokWebSocketClient(wsUrl, roomId, cookieHeader);

            client.OnFrameReceived += (frame) =>
            {
                Console.WriteLine($"Got frame: {frame.PayloadType}");

                //if (frame.PayloadType == "ack")
                //{
                //    var ackMsg = WebcastAckMessage.Parser.ParseFrom(frame.Payload);
                //    _ = client.SendAsync(client.BuildAckFrame(ackMsg.ServerTimestamp, ackMsg.ReceivedMessages.ToArray()));
                //}
                //else if (frame.PayloadType == "msg")
                //{
                //    // TODO: parse chat/gift/like
                //}
            };

            await client.ConnectAsync();


            // 1. In ra URL và Header để kiểm tra
            //Console.WriteLine($"Đang kết nối tới: {wsUrl}");
            //Console.WriteLine($"Sử dụng Cookie Header: {cookieHeader}");

            //var ws = new WebSocket(wsUrl);
            //var bytes = new byte[16];
            //using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(bytes);
            //}

            //// Encode base64
            //string secWebSocketKey = Convert.ToBase64String(bytes);

            //// 2. Bổ sung đầy đủ các HTTP Header
            //ws.CustomHeaders = new Dictionary<string, string>
            //{
            //    ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36",
            //    ["Origin"] = "https://www.tiktok.com",
            //    ["Cookie"] = cookieHeader,
            //    ["Pragma"] = "no-cache",
            //    ["Cache-Control"] = "no-cache",
            //    ["Accept-Encoding"] = "gzip, deflate, br, zstd",
            //    ["Accept-Language"] = "en-US,en;q=0.9",
            //    //["Sec-WebSocket-Key"]= secWebSocketKey
            //};

            ////string referer = $"https://www.tiktok.com/@{myroom.unique_id}/live/{roomId}";
            ////string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/139.0.0.0 Safari/537.36";

            //ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;

            //// 3. Theo dõi trạng thái kết nối
            //ws.OnOpen += (s, e) =>
            //{
            //    Console.WriteLine("✅ Connected to TikTok WebSocket!");

            //    // sau khi ws.Connect hoặc ws.OnOpen:
            //    var enterBytes = WsFrames.BuildEnterFrame(roomId);
            //    ws.Send(enterBytes); // SEND BINARY

            //     Task.Delay(120); // small delay to let server process

            //    ws.Send(WsFrames.BuildSubFrame(roomId, 1)); // chat
            //    ws.Send(WsFrames.BuildSubFrame(roomId, 2)); // gift
            //    ws.Send(WsFrames.BuildSubFrame(roomId, 3)); // like
            //    ws.Send(WsFrames.BuildSubFrame(roomId, 4)); // room info / social

               

            //    // start heartbeat loop (send every 10s or as url heartbeat_duration)
            //    _ = Task.Run(async () =>
            //    {
            //        while (ws.IsAlive)
            //        {
            //            var hb = WsFrames.BuildHeartbeatFrame();
            //            ws.Send(hb);
            //            await Task.Delay(20000); // use heartbeat_duration from wsUrl (10s in your case)
            //        }
            //    });
            //   // Console.WriteLine("Nhan (ws.OnMessage)");
            //    //SendEnterFrame(roomId, ws);
            //    //SendSubscribeFrame(roomId, ws);
            //    //Task.Run(async () => await HeartbeatLoop(ws, roomId));
            //};
            
            //ws.OnMessage += (s, e) =>
            //{
            //    Console.WriteLine("🎉 Đã nhận được dữ liệu (ws.OnMessage)");
            //    // parse data -> nếu thấy server yêu cầu ack thì gửi
            //    //long serverTimestamp = ...;
            //    //long[] receivedIds = ...;

            //    //ws.Send(WsFrames.BuildAckFrame(serverTimestamp, receivedIds));
            //    // Logic xử lý dữ liệu của bạn sẽ ở đây
            //};

            //ws.OnError += (s, e) =>
            //{
            //    Console.WriteLine($"❌ Lỗi WebSocket: {e.Message}");
            //};

            //ws.OnClose += (s, e) =>
            //{
            //    Console.WriteLine($"🔌 Kết nối WebSocket đã ĐÓNG. Nguyên nhân: {e.Reason}");
            //};

            //ws.Connect();
        }


      
        
    

   
        void TryDecodePayload(string base64)
        {
            byte[] payloadBytes = Convert.FromBase64String(base64);

            // Kiểm tra GZip
            if (IsGzip(payloadBytes))
            {
                payloadBytes = DecompressGzip(payloadBytes);
            }


            var webcastResponse = WebcastResponse.Parser.ParseFrom(payloadBytes);

            foreach (var message in webcastResponse.Messages)
            {
                //Console.WriteLine($"  - Found Message Type: {message.Type}");
                ProcessMessage(message);
            }


        }
        bool IsGzip(byte[] data)
        {
            return data.Length >= 2 && data[0] == 0x1f && data[1] == 0x8b;
        }
        private byte[] DecompressGzip(byte[] compressed)
        {
            using (var input = new MemoryStream(compressed))
            using (var gzip = new GZipInputStream(input))
            using (var output = new MemoryStream())
            {
                gzip.CopyTo(output);
                return output.ToArray();
            }
        }
        private void ProcessMessage(TikTok.Message messageWrapper)
        {
            foreach (var parser in parsers)
            {
                if (messageWrapper.Type == parser.Name)
                {
                    try
                    {
                        // Giải mã payload bằng parser tương ứng
                        var decodedMessage = parser.Parse(messageWrapper.Binary.ToByteArray());
                        //  Console.WriteLine(decodedMessage);
                        ExtractUserInfoListLog(decodedMessage);
                        return; // Đã tìm thấy và xử lý message, thoát khỏi vòng lặp
                    }
                    catch (InvalidProtocolBufferException)
                    {
                        // Lỗi giải mã, có thể do định nghĩa Protobuf không khớp.
                        //  Console.WriteLine($"[CẢNH BÁO] Không thể giải mã {parser.Name}. Dữ liệu có thể bị hỏng hoặc định nghĩa .proto không chính xác.");
                    }
                }
            }

        }
        private void ExtractUserInfoListLog(IMessage message)
        {
            // Trích xuất thông tin người dùng từ các loại message cụ thể
            switch (message)
            {
                case WebcastChatMessage chatMessage:

                    if (ghilog)
                    {
                        GhiLogFrame(chatMessage);
                    }

                    Invoke(new Action(() =>
                    {
                        PQComment a = new PQComment();
                        a.CommentId = chatMessage.Common.MsgId.ToString();
                        a.Timestamp = DateTime.Now;
                        a.Text = chatMessage.Comment;
                        a.UserId = chatMessage.User.UniqueId;
                        a.UserName = chatMessage.User.Nickname;
                        a.UserAvatar = chatMessage.User.ProfilePicture.Url.ToString();
                        a.IsHighlighted = false; // Chưa có thông tin về việc bình luận có được làm nổi bật hay không
                        a.phoneNumber = a.UserId;
                        // var (phone2, isValid2) = PQMyFunctions.ExtractPhoneNumber(a.Text);
                        //if (isValid2)
                        //{
                        //    a.IsHighlighted = true; // phat hien dien thoai
                        //    a.phoneNumber = phone2; // Lưu số điện thoại nếu hợp lệ
                        //}
                        //   Console.WriteLine($"Avartar: {chatMessage.User.ProfilePicture.Url.ToString()}");
                        dsComment.Add(a);

                        dgvComment.Rows.Add(a.Timestamp, a.UserName, a.Text, a.phoneNumber);
                        dgvComment.ClearSelection();
                        dgvComment.FirstDisplayedScrollingRowIndex = dgvComment.RowCount - 1;
                        dgvComment.Rows[dgvComment.Rows.Count - 1].Selected = true;

                     //   LoadCommentUserControl(a);

                    }));
                    // Console.WriteLine();
                    break;
                case WebcastLikeMessage likeMessage:
                    //Invoke(new Action(() => {
                    //    listBoxLog.Items.Add($"    -> [LIKE] User: {likeMessage.User.Nickname}, Like count: {likeMessage.LikeCount}");
                    //}));
                    // Console.WriteLine();
                    break;
                case WebcastGiftMessage giftMessage:
                    //Invoke(new Action(() => {
                    //    listBoxLog.Items.Add($"    -> [GIFT] User: {giftMessage.User.Nickname}, Gift: {giftMessage.GiftDetails}");
                    //}));
                    //Console.WriteLine($"    -> [GIFT] User: {giftMessage.User.Nickname}, Gift: {giftMessage.GiftDetails}");
                    break;
                case WebcastMemberMessage memberMessage:
                    Invoke(new Action(() =>
                    {
                        if (memberMessage.User != null)
                        {
                            lbJoin.Text = $"Join: {memberMessage.User.Nickname}";
                        }

                    }));
                    // Console.WriteLine($"    -> [JOIN] User: {memberMessage.User.Nickname}");
                    break;
                case WebcastRoomUserSeqMessage countMember:
                    Invoke(new Action(() =>
                    {
                        lbView.Text = "View: " + countMember.ViewerCount;
                        //listBoxLog.Items.Add($"    -> [VIEW]:  {countMember.TotalUser}");
                        //AutoScrollAndSelectLastItem();
                    }));
                    break;
                //case WebcastLikeMessage l:
                //    // Cộng dồn số lượt thích
                //    Invoke(new Action(() => {
                //        listBoxLog.Items.Add($"    -> [LIKE]:  {L.TotalLikeCount}");
                //        AutoScrollAndSelectLastItem();
                //    }));
                //    break;
                // Thêm các case khác nếu cần
                default:
                    // Console.WriteLine($"    -> Message được giải mã thành công nhưng không có thông tin người dùng cụ thể.");
                    break;
            }
        }
        private void GhiLogFrame(WebcastChatMessage frame)
        {
            // Ghi log từng dòng ra file
            string logFilePath = filelog + ".txt";
            using (StreamWriter writer = new StreamWriter(logFilePath, append: true, Encoding.UTF8))
            {
                writer.WriteLine(frame);
            }
        }
       

        // Extension method nén/gải nén gzip



        private void button1_Click(object sender, EventArgs e)
        {
            string base64Frame = txt64.Text;

            // 1️⃣ Chuyển Base64 → byte[]
            byte[] frameBytes = Convert.FromBase64String(base64Frame);

            // 2️⃣ Giải nén gzip nếu cần
           // byte[] decompressed = frameBytes.GzipDecompress(); // nếu server nén

            // 3️⃣ Parse thành WebcastPushFrame
            var frame = WebcastPushFrame.Parser.ParseFrom(frameBytes);

            // 4️⃣ Kiểm tra type
            Console.WriteLine($"PayloadType={frame.PayloadType}, PayloadLength={frame.Payload.Length}");

            // 5️⃣ Nếu PayloadType = "msg" hoặc "gift", tiếp tục parse WebcastWebsocketMessage
            if (frame.PayloadType == "msg" || frame.PayloadType == "gift")
            {
                var wsMsg = WebcastWebsocketMessage.Parser.ParseFrom(frame.Payload);
                Console.WriteLine($"Type={wsMsg.Type}, PayloadLength={wsMsg.Payload.Length}");
            }
        }
    }
    public static class GzipExtensions
    {
        public static byte[] GzipCompress(this byte[] data)
        {
            using (var ms = new MemoryStream())
            using (var gzip = new GZipStream(ms, CompressionMode.Compress))
            {
                gzip.Write(data, 0, data.Length);
                gzip.Close();
                return ms.ToArray();
            }
        }

        public static byte[] GzipDecompress(this byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var gzip = new GZipStream(ms, CompressionMode.Decompress))
            using (var outMs = new MemoryStream())
            {
                gzip.CopyTo(outMs);
                return outMs.ToArray();
            }
        }
    }
}
