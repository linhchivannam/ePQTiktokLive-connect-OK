using ePQTiktokLive.MODEL;
using ePQTiktokLive.USERCONTROL;
using Google.Protobuf;
using ICSharpCode.SharpZipLib.GZip;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using TikTok;

namespace ePQTiktokLive
{
    public partial class frm_ePQTikTok : Form
    {
        public frm_ePQTikTok()
        {
            InitializeComponent();
        }
        string filelog = "";
        bool ghilog = false;
        bool ghidata = false;
        Page page = null; // Biến lưu trữ trang hiện tại của Puppeteer
        string filePath = "ePQsendFrames.txt";

        private Dictionary<string, string> _lastRequestHeaders = new Dictionary<string, string>();

        List<RegLog> ds;
        List<PQComment> dsComment;
       static readonly HttpClient myclient = new HttpClient();

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

        private void frm_ePQTikTokLive_Load(object sender, EventArgs e)
        {
            ds = new List<RegLog>();
        }
        private void btnConnet_Click(object sender, EventArgs e)
        {
            filelog = "pq_log-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            ghilog = chkGhilog.Checked;
            ghidata = chkGhidata.Checked;
            if (ghilog)
            { 
                txtFileLog.Text = filelog + ".txt";
            }
            ds.Clear();
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

            // var page = await browser.NewPageAsync();

            page = (Page)await browser.NewPageAsync();

            // lắng nghe request để lưu lại header mới nhất
            page.Request += (s, req) =>
            {
                try
                {
                    var url = req.Request.Url;
                    if (url.Contains("room_id="))
                    {
                        var uri = new Uri(url);
                        var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
                        var roomId = query.Get("room_id");
                        if (!string.IsNullOrEmpty(roomId))
                        {
                            File.WriteAllText("room_id.txt", url);
                            Console.WriteLine("RoomID link: " + roomId);
                            Invoke(new Action(() =>
                            {
                                txtRomID.Text = roomId;
                            }));
                        }
                    }

                    _lastRequestHeaders = new Dictionary<string, string>(req.Request.Headers);
                }
                catch { }
            };
            
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
                           // Console.WriteLine("\n✅ Found WebSocket: " + wsUrl);
                            Invoke(new Action(() =>
                            {
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
                            if (res.Length % 4 == 0)
                            {
                                // Nếu là Base64 hợp lệ, giải mã
                                byte[] payloadBytes1 = Convert.FromBase64String(res);

                                var webcastPushFrame = WebcastPushFrame.Parser.ParseFrom(payloadBytes1);
                                
                                
                                if (webcastPushFrame.PayloadType == "msg")
                                {
                                    //Console.WriteLine(webcastPushFrame.LogId);
                                    TryDecodePayload(webcastPushFrame.Payload.ToBase64());
                                }
                            }                   
                        }
                    }
                }
                if (e.MessageID == "Network.webSocketFrameSent")
                {
                    if (e.MessageData.TryGetProperty("response", out var response) &&
                        response.TryGetProperty("payloadData", out var payloadData))
                    {
                        string payload = payloadData.GetString();

                        byte[] frameBytes = Convert.FromBase64String(payload);
                        var frame = WebcastPushFrame.Parser.ParseFrom(frameBytes);

                        System.IO.File.AppendAllText(filePath, frame + Environment.NewLine);
                        Console.WriteLine($"Saved frame: {frame}");


                        //if (!string.IsNullOrEmpty(payload))
                        //{
                        //    // Lọc payload nếu cần, ví dụ chỉ lưu ping / enter / sub
                        //    if (payload.Contains("ping") || payload.Contains("enter") || payload.Contains("sub"))
                        //    {
                        //        System.IO.File.AppendAllText(filePath, payload + Environment.NewLine);
                        //        Console.WriteLine($"Saved frame: {payload.Substring(0, Math.Min(80, payload.Length))}...");
                        //    }
                        //}
                    }
                }
            };

            await page.Client.SendAsync("Network.enable");
            await page.GoToAsync(txtTiktoklive.Text);
            await Task.Delay(60000);

            
        }
        private async void Ghi_header_cookies()
        {
            if (page == null)
            {
                MessageBox.Show("Chưa mở trình duyệt!");
                return;
            }

            // lấy cookies sau khi login
            var cookies = await page.GetCookiesAsync();
            var cookieJson = JsonSerializer.Serialize(cookies, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("cookies.json", cookieJson);


            // lấy headers từ 1 request gần nhất
            //var client = await page.Target.CreateCDPSessionAsync();
            //var ua = await client.SendAsync("Browser.getVersion");
            //string userAgent = ua?.GetProperty("userAgent").ToString();

            //var headerDict = new Dictionary<string, string>
            //{
            //    { "User-Agent", userAgent },
            //    { "Cookie", string.Join("; ", cookies.Select(c => $"{c.Name}={c.Value}")) }
            //};

            var headerJson = JsonSerializer.Serialize(_lastRequestHeaders, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("headers.json", headerJson);

            MessageBox.Show("Đã lưu cookies.json và headers.json sau khi login!");
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
        private DateTime UnixTimeStampToDateTime(long unixTime)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixTime).ToLocalTime().DateTime;
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

                        dgvComment.Rows.Add(a.Timestamp, a.UserName, a.Text,a.phoneNumber);
                        dgvComment.ClearSelection();
                        dgvComment.FirstDisplayedScrollingRowIndex = dgvComment.RowCount - 1;
                        dgvComment.Rows[dgvComment.Rows.Count - 1].Selected = true;

                        LoadCommentUserControl(a);

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
                        if(memberMessage.User != null)
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
        private void LoadCommentUserControl(PQComment comment)
        {
            Comment commentControl = new Comment(comment, myclient);
            commentControl.BackColor = Color.White;
           // commentControl.Dock = DockStyle.Top;
            commentControl.taodonghangClick += CommentControl_taodonghangClick;
            commentControl.thongtinClick += CommentControl_thongtinClick;
            commentControl.binhthuongClick += CommentControl_binhthuongClick;
            commentControl.Width = pComment.ClientSize.Width-10;
            pComment.Controls.Add(commentControl);

            if (pComment.InvokeRequired)
            {
                pComment.Invoke(new Action(() => pComment.Controls.Add(commentControl)));
                pComment.ScrollControlIntoView(commentControl.Controls[commentControl.Controls.Count - 1]);
                //pComment.ScrollControlIntoView(commentControl); 
            }
            else
            {
                pComment.Controls.Add(commentControl);
                pComment.ScrollControlIntoView(commentControl.Controls[commentControl.Controls.Count - 1]);
                //pComment.ScrollControlIntoView(commentControl);
            }
        }
        // Hàm xử lý sự kiện khi nút "Tạo đơn hàng" được click
        private void CommentControl_taodonghangClick(object sender, CommentEventArgs e)
        {
            MessageBox.Show($"Tạo đơn hàng cho: {e.Comment.UserName}");
            // Thực hiện logic tạo đơn hàng ở đây
        }

        // Hàm xử lý sự kiện khi nút "Thông tin" được click
        private void CommentControl_thongtinClick(object sender, CommentEventArgs e)
        {
            MessageBox.Show($"Xem thông tin của: {e.Comment.UserName}");
            // Thực hiện logic xem thông tin ở đây
        }

        // Hàm xử lý sự kiện khi nút "Bình thường" được click
        private void CommentControl_binhthuongClick(object sender, CommentEventArgs e)
        {
            MessageBox.Show($"Thay đổi trạng thái của: {e.Comment.UserName}");
            // Thực hiện logic thay đổi trạng thái ở đây
        }
        private void frm_ePQTikTok_Load(object sender, EventArgs e)
        {
            ds = new List<RegLog>();
            dsComment = new List<PQComment>();
            filelog = Path.Combine(Application.StartupPath, "ePQTikTokLive.log");
            pQCommentBindingSource.DataSource = dsComment;   
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form1 a = new Form1();
            //a.Show();
            Ghi_header_cookies();
        }
    }
}
