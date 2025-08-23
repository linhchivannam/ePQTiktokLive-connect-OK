using Google.Protobuf;
using ICSharpCode.SharpZipLib.GZip;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PuppeteerSharp;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;
using TikTok;

namespace ePQTiktokLive
{
    public partial class frm_TiktokLive : Form
    {
        public frm_TiktokLive()
        {
            InitializeComponent();
        }
        private static readonly List<(string Name, Func<byte[], IMessage> Parse)> parsers = new List<(string Name, Func<byte[], IMessage> Parse)>
    {
        ("WebcastChatMessage", bytes => WebcastChatMessage.Parser.ParseFrom(bytes)),
        ("WebcastGiftMessage", bytes => WebcastGiftMessage.Parser.ParseFrom(bytes)),
        ("WebcastLikeMessage", bytes => WebcastLikeMessage.Parser.ParseFrom(bytes)),
        ("WebcastMemberMessage", bytes => WebcastMemberMessage.Parser.ParseFrom(bytes)),
        ("WebcastControlMessage", bytes => WebcastControlMessage.Parser.ParseFrom(bytes)),
        ("WebcastRoomUserSeqMessage", bytes => WebcastRoomUserSeqMessage.Parser.ParseFrom(bytes)),
        //("SyntheticWebcastMessage", bytes => SyntheticWebcastMessage.Parser.ParseFrom(bytes)),
        //// Nếu có WebcastEnvelope bạn có thể thêm:
        //// ("WebcastEnvelope", bytes => WebcastEnvelope.Parser.ParseFrom(bytes)),
    };
        private static long totalViewers = 0;
        private static int totalComments = 0;
        private static long totalLikes = 0;
        string filelog = "";

        List<RegLog> ds;//= new List<RegLog>();
        private void button3_Click(object sender, EventArgs e)
        {
            filelog ="pq_log-"+ DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            ConnectNew();
        }
        private async void ConnectNew()
        {
            // await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();

            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = false, // Hiện cửa sổ để bạn có thể login nếu cần
                DefaultViewport = null,
                Args = new[] { "--start-maximized", "--disable-blink-features=AutomationControlled" }
            });

            var page = await browser.NewPageAsync();

            // Bắt WebSocket URL
            page.Client.MessageReceived += (sender, e) =>
            {
                Invoke(new Action(() =>
                {
                    //   listBoxLog.Items.Add($"[Debug] MessageID: {e.MessageID}");
                }));

                //  Console.WriteLine("[Log] " + e.MessageID);

                if (e.MessageID == "Network.webSocketCreated")
                {
                    if (e.MessageData.TryGetProperty("url", out var urlElement))
                    {
                        string wsUrl = urlElement.GetString();
                        if (!string.IsNullOrEmpty(wsUrl) && wsUrl.StartsWith("wss://"))
                        {
                            Console.WriteLine("\n✅ Found WebSocket: " + wsUrl);
                            Invoke(new Action(() =>
                            {
                              //  listBoxLog.Items.Add("✅ wrss: " + wsUrl);
                                txtWebSocketUrl.Text = wsUrl;
                            }));

                        }
                    }
                }

                if (e.MessageID == "Network.webSocketFrameReceived")
                {

                    if (e.MessageData.TryGetProperty("response", out var response) &&
                        response.TryGetProperty("payloadData", out var payloadData))
                    {
                        // Lấy toàn bộ JSON string và deserialize
                        string res = payloadData.ToString();
                        if (!string.IsNullOrEmpty(res))
                        {
                            byte[] payloadBytes1 = Convert.FromBase64String(res);

                            var webcastPushFrame = WebcastPushFrame.Parser.ParseFrom(payloadBytes1);

                           // GhiLogFrame(webcastPushFrame);
                            if (webcastPushFrame.PayloadType == "msg")
                            {
                                TryDecodePayload(webcastPushFrame.Payload.ToBase64());
                                //byte[] payloadBytes = Convert.FromBase64String(webcastPushFrame.Payload.ToString());
                                //if (IsGzip(payloadBytes))
                                //{
                                //    payloadBytes = DecompressGzip(payloadBytes);

                                //    TryDecodePayload(webcastPushFrame.Payload.ToBase64());
                                //}
                            }
                            //var logEntry = JObject.Parse(webcastPushFrame.);
                            //string logid = logEntry["logId"]?.ToString();
                            //string payloadBase64 = logEntry["payload"]?.ToString();
                            //string compressType = logEntry["headers"]?["compress_type"]?.ToString();
                            //string payloadEncoding = logEntry["payloadEncoding"]?.ToString();
                            //string payloadType = logEntry["payloadType"]?.ToString();
                            //string im_cursor = logEntry["headers"]?["im_cursor"]?.ToString();

                        }
                    }
                }
            };

            await page.Client.SendAsync("Network.enable");

            // Console.WriteLine("👉 Mở livestream...\n");
            await page.GoToAsync(txtTiktoklive.Text); // hoặc bất kỳ link livestream nào

            // Chờ 30 giây để user login & TikTok connect WebSocket
            await Task.Delay(60000);
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
        private void GhiLogFrame(WebcastPushFrame frame)
        {
            // Ghi log từng dòng ra file
            string logFilePath =filelog+ ".txt";
            using (StreamWriter writer = new StreamWriter(logFilePath, append: true, Encoding.UTF8))
            {
                writer.WriteLine(frame);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string payloadBase64 = "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";

            try
            {
                ProcessPayload(payloadBase64);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LỖI CHUNG] Xảy ra lỗi trong chương trình chính: {ex.Message}");
            }
        }
        void ProcessPayload(string payloadBase64)
        {
            Console.WriteLine("--- Bắt đầu phân tích payload ---");
            try
            {
                // Bước 1: Làm sạch và giải mã Base64
                byte[] payloadBytes = DecodeBase64(payloadBase64);

                // Bước 2: Thử giải nén Gzip. Nếu thất bại, sử dụng mảng byte gốc.
                byte[] decompressedBytes;
                try
                {
                    decompressedBytes = DecompressGzip(payloadBytes);
                    Console.WriteLine("-> Đã giải nén Gzip thành công.");
                }
                catch (Exception)
                {
                    decompressedBytes = payloadBytes;
                    Console.WriteLine("-> Payload không nén Gzip. Sử dụng mảng byte gốc.");
                }

                // Bước 3: Thử giải mã Protobuf ở các cấp độ khác nhau
                try
                {
                    // Thử giải mã cấp cao nhất: WebcastPushFrame
                    var webcastPushFrame = WebcastPushFrame.Parser.ParseFrom(decompressedBytes);
                    Console.WriteLine("-> Đã giải mã thành công WebcastPushFrame.");

                    // Giải mã payload bên trong của frame
                    ProcessFramePayload(webcastPushFrame.Payload);
                }
                catch (InvalidProtocolBufferException)
                {
                    Console.WriteLine("-> Không phải WebcastPushFrame. Thử giải mã trực tiếp.");

                    // Thử giải mã trực tiếp payload thành WebcastResponse hoặc Message
                    ProcessFramePayload(ByteString.CopyFrom(decompressedBytes));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LỖI] Không thể xử lý payload. Lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Hàm xử lý payload bên trong, có thể là WebcastResponse hoặc Message đơn lẻ.
        /// </summary>
        private  void ProcessFramePayload(ByteString payload)
        {
            try
            {
                // Thử giải mã là WebcastResponse
                var webcastResponse = WebcastResponse.Parser.ParseFrom(payload);

                if (webcastResponse.Messages.Count > 0)
                {
                    Console.WriteLine($"> Đã tìm thấy {webcastResponse.Messages.Count} message trong WebcastResponse.");
                    foreach (var message in webcastResponse.Messages)
                    {
                        ProcessMessage(message);
                    }
                }
                else
                {
                    Console.WriteLine("> WebcastResponse không chứa message nào. Bỏ qua.");
                }
            }
            catch (InvalidProtocolBufferException)
            {
                // Nếu không phải WebcastResponse, thử giải mã là Message đơn lẻ
                try
                {
                    var singleMessage = TikTok.Message.Parser.ParseFrom(payload);
                    Console.WriteLine("> Đã tìm thấy một Message đơn lẻ.");
                    ProcessMessage(singleMessage);
                }
                catch (InvalidProtocolBufferException ex)
                {
                    Console.WriteLine($"[LỖI GIẢI MÃ PAYLOAD] Không phải WebcastResponse hoặc Message. Lỗi: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Hàm xử lý và hiển thị thông tin từ từng message con.
        /// </summary>
        private  void ProcessMessage(TikTok.Message messageWrapper)
        {
            foreach (var parser in parsers)
            {
                if (messageWrapper.Type == parser.Name)
                {
                    try
                    {
                        // Giải mã payload bằng parser tương ứng
                        var decodedMessage = parser.Parse(messageWrapper.Binary.ToByteArray());
                        ExtractUserInfo(decodedMessage);
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
            // Nếu không có parser nào khớp hoặc giải mã thành công
            //Console.WriteLine($"[CẢNH BÁO] Không tìm thấy parser cho message type: {messageWrapper.Type}");
        }
        private void AutoScrollAndSelectLastItem()
        {
            // Kiểm tra xem có mục nào trong ListBox không
            if (listBoxLog.Items.Count > 0)
            {
                // Lấy chỉ mục của dòng cuối cùng
                int lastIndex = listBoxLog.Items.Count - 1;

                // Cuộn ListBox đến dòng cuối cùng
                listBoxLog.TopIndex = lastIndex;

                // Chọn dòng cuối cùng
                listBoxLog.SelectedIndex = lastIndex;
            }
        }
        private  void ExtractUserInfoListLog(IMessage message)
        {
            // Trích xuất thông tin người dùng từ các loại message cụ thể
            switch (message)
            {
                case WebcastChatMessage chatMessage:
                    Invoke(new Action(() => {
                        listBoxLog.Items.Add($"    -> [{chatMessage.User.Nickname}]:  {chatMessage.Comment}");
                        AutoScrollAndSelectLastItem();
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
                        lbJoin.Text = $"Join: {memberMessage.User.Nickname}";
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
        private static void ExtractUserInfo(IMessage message)
        {
            // Trích xuất thông tin người dùng từ các loại message cụ thể
            switch (message)
            {
                case WebcastChatMessage chatMessage:
                    Console.WriteLine($"    -> [COMMENT] User: {chatMessage.User.Nickname}, Text: {chatMessage.Comment}");
                    break;
                case WebcastLikeMessage likeMessage:
                    Console.WriteLine($"    -> [LIKE] User: {likeMessage.User.Nickname}, Like count: {likeMessage.LikeCount}");
                    break;
                case WebcastGiftMessage giftMessage:
                    Console.WriteLine($"    -> [GIFT] User: {giftMessage.User.Nickname}, Gift: {giftMessage.GiftDetails}");
                    break;
                case WebcastMemberMessage memberMessage:
                    Console.WriteLine($"    -> [JOIN] User: {memberMessage.User.Nickname}");
                    break;
                // Thêm các case khác nếu cần
                default:
                    Console.WriteLine($"    -> Message được giải mã thành công nhưng không có thông tin người dùng cụ thể.");
                    break;
            }
        }
        public static byte[] DecodeBase64(string payload)
        {
            // Loại bỏ các ký tự không hợp lệ, ví dụ như khoảng trắng, ký tự xuống dòng
            string cleanPayload = Regex.Replace(payload, "[^a-zA-Z0-9+/=]", string.Empty);

            // Xử lý padding
            int mod4 = cleanPayload.Length % 4;
            if (mod4 > 0)
            {
                cleanPayload += new string('=', 4 - mod4);
            }

            // Bây giờ, chuỗi đã được làm sạch và có padding hợp lệ, bạn có thể giải mã
            return Convert.FromBase64String(cleanPayload);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ds.Clear();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog.Title = "Chọn file log TikTok Live";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                ProcessLogFile(filePath);
            }
        }
        private void ProcessLogFile(string filePath)
        {
            listBoxLog.Items.Clear();
            //   Log("[CHƯƠNG TRÌNH] Bắt đầu đọc và phân tích file log...");

            
            try
            {
                foreach (string line in File.ReadLines(filePath))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // Phân tích dòng log dưới dạng JSON
                    var logEntry = JObject.Parse(line);
                    string logid =  logEntry["logId"]?.ToString();
                    string payloadBase64 = logEntry["payload"]?.ToString();
                    string compressType = logEntry["headers"]?["compress_type"]?.ToString();
                    string payloadEncoding = logEntry["payloadEncoding"]?.ToString();
                    string payloadType = logEntry["payloadType"]?.ToString();
                    string im_cursor = logEntry["headers"]?["im_cursor"]?.ToString();

                    RegLog a = new RegLog();
                    a.logId = logid;
                    a.payloadType = payloadType;
                    a.payloadEncoding= payloadEncoding;
                    a.compress_type = compressType;
                    a.im_cursor = im_cursor;
                    a.payload = payloadBase64;
                    ds.Add(a);

                    //Invoke(new Action(() =>
                    //{
                        
                    //    listBoxLog.Items.Add("logId: " + logid);
                    //    //listBoxLog.Items.Add("payload: " + payloadBase64);
                    //    listBoxLog.Items.Add("compress_type: " + compressType);
                    //    listBoxLog.Items.Add("payloadEncoding: " + payloadEncoding);
                    //    listBoxLog.Items.Add("payloadType: " + payloadType);
                    //    listBoxLog.Items.Add("im_cursor: " + im_cursor);
                    //}));
                    
                }
                regLogBindingSource.DataSource = ds.ToList();
            }
            catch (Exception ex)
            {
                Invoke(new Action(() =>
                {
                    listBoxLog.Items.Add($"[LỖI CHUNG] Đã xảy ra lỗi khi xử lý file: {ex.Message}");
                }));
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try {
                if (dgv.Rows[e.RowIndex].Cells["logId"].Value != null)
                {
                    txtPayload.Text = dgv.Rows[e.RowIndex].Cells["payload"].Value.ToString();
                    txtLogID.Text = dgv.Rows[e.RowIndex].Cells["logId"].Value.ToString();
                    Giaima(txtLogID.Text);
                }
            }
            catch (Exception a)
            { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Giaima(txtLogID.Text);
        }
        private void Giaima(string logid)
        {
            var tm = ds.Where(w=>w.logId==logid).FirstOrDefault();
            if (tm != null)
            {
                byte[] payloadBytes = Convert.FromBase64String(tm.payload);

               // Nếu frame được GZIP toàn bộ
                if (tm.compress_type == "gzip")
                {
                    payloadBytes = DecompressGzip(payloadBytes);
                }

                if (tm.payloadType == "msg")
                {
                    TryDecodePayload(tm.payload);
                    //byte[] innerPayload = payloadBytes;

                    //if (IsGzip(innerPayload))
                    //    innerPayload = DecompressGzip(innerPayload);
                    //WebcastResponse response = WebcastResponse.Parser.ParseFrom(innerPayload);
                    //foreach (var ok in response.Messages)
                    //{
                    //    Console.WriteLine($"mes type: {ok.Type}");
                    //}
                    //WebcastPushFrame frame = WebcastPushFrame.Parser.ParseFrom(innerPayload);
                    //Console.WriteLine($"PayloadType = {frame.PayloadType}");

                    //var chat = WebcastChatMessage.Parser.ParseFrom(innerPayload);
                    //string user = chat.User?.Nickname ?? "Unknown";
                    //string comment = chat.Comment ?? "[No content]";
                    //Console.WriteLine($"👤 {user}: {comment}");
                }

               // WebcastChatMessage
                  //  WebcastGiftMessage

                //var frame = WebcastPushFrame.Parser.ParseFrom(payloadBytes);

                //Console.WriteLine($"Frame.PayloadType: {frame.PayloadType}");

                //if (frame.PayloadType == "msg")
                //{
                //    byte[] innerPayload = frame.Payload.ToByteArray();

                //    // Nếu phần Payload bên trong cũng GZip
                //    if (innerPayload.Length >= 2 && innerPayload[0] == 0x1F && innerPayload[1] == 0x8B)
                //    {
                //        innerPayload = DecompressGzip(innerPayload);
                //    }

                //    try
                //    {
                //        var chat = WebcastChatMessage.Parser.ParseFrom(innerPayload);
                //        string user = chat.User?.Nickname ?? "Unknown";
                //        string comment = chat.Comment ?? "[No content]";
                //        Console.WriteLine($"👤 {user}: {comment}");
                //    }
                //    catch
                //    {
                //        Console.WriteLine("⚠️ PayloadType = msg nhưng không phải WebcastChatMessage.");
                //    }
                //}
                //else
                //{
                //    Console.WriteLine($"[Khác] PayloadType: {frame.PayloadType}");
                //}
            }
        }
        bool IsGzip(byte[] data)
        {
            return data.Length >= 2 && data[0] == 0x1f && data[1] == 0x8b;
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
        // bool IsGzip(byte[] data) => data.Length >= 2 && data[0] == 0x1F && data[1] == 0x8B;
        private void frm_TiktokLive_Load(object sender, EventArgs e)
        {
            ds = new List<RegLog>();
        }
    }
    
}
