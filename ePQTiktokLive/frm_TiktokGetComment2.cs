using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ePQTiktokLive
{
    public partial class frm_TiktokGetComment2 : Form
    {
        public frm_TiktokGetComment2()
        {
            InitializeComponent();
            InitializeAsync();
        }
        string videoId, userId;
        private List<CommentInfo> allCommentList;
        private async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
            allCommentList = new List<CommentInfo>();
        }
        private void frm_TiktokGetComment_Load(object sender, EventArgs e)
        {

        }
        private (string user, string videoId) ParseTikTokUrl(string url)
        {
            try
            {
                var uri = new Uri(url);
                var parts = uri.AbsolutePath.Split('/');
                string _user=string.Empty;
                string _videoId =string.Empty;

                for (int i = 0; i < parts.Length; i++)
                {
                    if (parts[i].Length > 0)
                    {
                        if (parts[i].Substring(0, 1) == "@")
                        {
                             _user= parts[i].Substring(1, parts[i].Length - 1);
                        }
                        if (parts[i]== "video" && i + 1 < parts.Length)
                        {
                             _videoId = parts[i + 1];
                            
                        }
                    }
                }

                return (_user, _videoId); // trả về user và videoId
            }
            catch { }

            throw new Exception("Link TikTok không hợp lệ.");
        }
        private async void btnConnect_Click(object sender, EventArgs e)
        {
            string inputUrl = txtTiktoklive.Text.Trim();
            if (string.IsNullOrEmpty(inputUrl))
            {
                MessageBox.Show("Vui lòng nhập link TikTok.");
                return;
            }

            allCommentList.Clear();
            progressBar1.Value = 0;
            labelProgress.Text = "Đang tải comment...";

            var (user, videoId) = ParseTikTokUrl(inputUrl);
            videoId = videoId.Trim();
            userId = user.Trim('@'); // Lấy ID người dùng không có dấu @
            // string videoId = "7530961991779798279"; // Đổi sang video TikTok khác nếu muốn
            string videoUrl = $"https://www.tiktok.com/{userId}/video/{videoId}";
            videoUrl = txtTiktoklive.Text;
            // Mở TikTok trong WebView2
            webView21.Source = new Uri(videoUrl);

            await Task.Delay(5000); // Đợi load trang



            // Lấy cookie
            var cookieList = await webView21.CoreWebView2.CookieManager.GetCookiesAsync(videoUrl);
            string cookieHeader = string.Join("; ", cookieList.Select(c => $"{c.Name}={c.Value}"));

            string msToken = cookieList.FirstOrDefault(c => c.Name == "msToken")?.Value ?? "";
            string verifyFp = cookieList.FirstOrDefault(c => c.Name == "verifyFp")?.Value ?? "";

            // 1. Lấy số lượng comment
            int totalComments = await GetCommentCount(videoId, cookieHeader);
            labelTotalComment.Text = $"Tổng số comment: {totalComments:N0}";

            // 2. Lấy chi tiết tất cả comment
            await FetchComments(videoId, msToken, verifyFp, cookieHeader);
        }
        private async Task<int> GetCommentCount(string videoId, string cookieHeader)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
            client.DefaultRequestHeaders.Add("Referer", $"https://www.tiktok.com/{userId}/video/{videoId}");
            client.DefaultRequestHeaders.Add("Cookie", cookieHeader);


            string url = $"https://www.tiktok.com/api/comment/list/?aid=1988&aweme_id={videoId}&count=1&cursor=0";
            var response = await client.GetAsync(url);
            string json = await response.Content.ReadAsStringAsync();

            dynamic result = JsonConvert.DeserializeObject(json);
            if (result != null && result.total != null)
                return (int)result.total;

            return result?.itemInfo?.itemStruct?.stats?.commentCount ?? 0;
        }

        private async Task FetchComments(string videoId, string msToken, string verifyFp, string cookieHeader)
        {
            allCommentList.Clear();
            progressBar1.Value = 0;
            labelProgress.Text = "Đang tải comment...";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
            client.DefaultRequestHeaders.Add("Referer", $"https://www.tiktok.com/video/{videoId}");
            client.DefaultRequestHeaders.Add("Cookie", cookieHeader);

            string cursor = "0";
            bool hasMore = true;
            int totalFetched = 0;
            int total = await GetCommentCount(videoId, cookieHeader);
            int retryCount = 0;
            string lastCursor = "";

            while (hasMore && retryCount <5)
            {
                string apiUrl = $"https://www.tiktok.com/api/comment/list/?aid=1988&aweme_id={videoId}&count=20&cursor={cursor}";

                //string apiUrl = $"https://www.tiktok.com/api/comment/list/?" +
                //     $"aid=1988&count=50&cursor={cursor}" +
                //     $"&item_id={videoId}" +
                //     $"&msToken={HttpUtility.UrlEncode(msToken)}" +
                //     $"&verifyFp={verifyFp}";

                HttpResponseMessage res;
                try
                {
                    res = await client.GetAsync(apiUrl);
                }
                catch
                {
                    retryCount++;
                    await Task.Delay(1000);
                    continue;
                }

                var json = await res.Content.ReadAsStringAsync();
                // dynamic result = SafeDeserializeJson(json);
                //string cleanedJson = json
                //                            .Replace("‘", "'")     // Dấu nháy trái không chuẩn
                //                            .Replace("’", "'")     // Dấu nháy phải không chuẩn
                //                            .Replace("“", "\"")    // Dấu ngoặc kép trái
                //                            .Replace("”", "\"");   // Dấu ngoặc kép phải
                //cleanedJson = cleanedJson.Normalize(NormalizationForm.FormC);
                dynamic result = JsonConvert.DeserializeObject(json);
                if (result?.comments == null || result.comments.Count == 0)
                {
                    retryCount++;
                    await Task.Delay(1000);
                    continue;
                }

                foreach (var c in result.comments)
                {
                    var info = new CommentInfo
                    {
                        CommentId = c.cid,
                        UserId = c.user.uid,
                        UniqueId = c.user.unique_id,
                        Nickname = c.user.nickname,
                        Text = c.text,
                        DiggCount = c.digg_count,
                        CreateTime = UnixTimeStampToDateTime((long)c.create_time),
                        AvatarUrl = c.user.avatar_thumb?.url_list?[0]?.ToString(),
                        VideoId = videoId,
                        CommentReplyId = "",
                        Reply = c.reply_comment_total
                    };
                    //if (info.CommentId == "507876321273365249")
                    //    MessageBox.Show("Thấy: 507876321273365249 ");
                    allCommentList.Add(info);
                    if (info.Reply > 0)
                    {
                        string referer = $"https://www.tiktok.com/video/{videoId}";
                        await FetchReplies(info.CommentId, videoId, msToken, verifyFp, cookieHeader, referer);
                    }
                    
                }

                totalFetched += result.comments.Count;
                lastCursor = cursor;
                cursor = result.cursor?.ToString();
                hasMore = result.has_more == true && cursor != lastCursor;

                totalFetched = allCommentList.Count;

                int progress = Math.Min(100, totalFetched * 100 / (total == 0 ? 1 : total));
                progressBar1.Value = progress;
                labelProgress.Text = $"Đã tải: {totalFetched}/{total} ({progress}%)";

                retryCount = 0;
                await Task.Delay(800); // delay nhỏ tránh bị chặn
                if(totalFetched<total)
                    hasMore = true; // tiếp tục nếu chưa đủ số lượng comment
                //else
                //    hasMore = false; // dừng nếu đã đủ số lượng comment
            }

            ShowComments(allCommentList);
        }

        private async Task FetchReplies(string commentId, string itemId, string msToken, string verifyFp, string cookieHeader, string referer)
        {
            string cursor = "0";
            bool hasMore = true;
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
            client.DefaultRequestHeaders.Add("Referer", referer);
            client.DefaultRequestHeaders.Add("Cookie", cookieHeader);

            while (hasMore)
            {
                string replyUrl = $"https://www.tiktok.com/api/comment/list/reply/" +
                                  $"?aid=1988&count=20&comment_id={commentId}" +
                                  $"&cursor={cursor}" +
                                  $"&item_id={itemId}" +
                                  $"&verifyFp={verifyFp}" +
                                  $"&msToken={HttpUtility.UrlEncode(msToken)}";

                try
                {
                    var response = await client.GetAsync(replyUrl);
                    var json = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(json);

                    if (result?.comments == null || result.comments.Count == 0)
                        break;

                    foreach (var r in result.comments)
                    {
                        allCommentList.Add(new CommentInfo
                        {
                            CommentId = r.cid,
                            UserId = r.user.uid,
                            CommentReplyId = commentId,
                            UniqueId = r.user.unique_id,
                            Nickname = r.user.nickname,
                            Text = "[Reply] " + r.text,
                            DiggCount = r.digg_count,
                            CreateTime = UnixTimeStampToDateTime((long)r.create_time),
                            AvatarUrl = r.user.avatar_thumb?.url_list?[0]?.ToString(),
                            VideoId = itemId,
                            
                            Reply = 0
                        });
                    }

                    cursor = result.cursor?.ToString() ?? "0";
                    hasMore = result.has_more == true;
                    await Task.Delay(300); // delay nhỏ tránh chặn
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Reply fetch failed: " + ex.Message);
                    break;
                }
            }
        }

        private void ShowComments(List<CommentInfo> list)
        {
            //dataGridView1.Invoke(() =>
            //{
            //    commentInfoBindingSource.DataSource = list.ToList();
            //    //dataGridView1.Columns.Clear();
            //    //dataGridView1.Rows.Clear();

            //    //dataGridView1.Columns.Add("UserId", "User ID");
            //    //dataGridView1.Columns.Add("Nickname", "Nickname");
            //    //dataGridView1.Columns.Add("Text", "Comment");
            //    //dataGridView1.Columns.Add("Like", "Like");
            //    //dataGridView1.Columns.Add("Time", "Time");
            //    //dataGridView1.Columns.Add("CommentId", "Comment ID");
            //    //dataGridView1.Columns.Add("CommentCounbt", "Comment Count");
            //    //dataGridView1.Columns.Add("CommentReplyId", "Reply Comment ID");

            //    //foreach (var c in list)
            //    //{
            //    //    dataGridView1.Rows.Add(c.UniqueId, c.Nickname, c.Text, c.DiggCount, c.CreateTime, c.CommentId,c.Reply,c.CommentReplyId);
            //    //}
            //});
            //lbSLDong.Invoke(() =>
            //{
            //    lbSLDong.Text = $"Số lượng comment: {list.Count:N0}";
            //});
        }


        private dynamic SafeDeserializeJson(string json)
        {
            try
            {
                // Làm sạch các ký tự không chuẩn
                json = json
                    .Replace("‘", "'")
                    .Replace("’", "'")
                    .Replace("“", "\"")
                    .Replace("”", "\"")
                    .Trim();

                // Nếu chuỗi là JSON escape lồng nhau
                if (json.StartsWith("\"{") && json.EndsWith("}\""))
                {
                    json = JsonConvert.DeserializeObject<string>(json); // bóc lớp 1
                }

                return JsonConvert.DeserializeObject(json); // lớp 2
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi phân tích JSON: " + ex.Message);
                return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (allCommentList.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu comment.");
                return;
            }

            var grouped = allCommentList
                .GroupBy(c => c.UniqueId)
                .Select(g => new
                {
                    User = g.Key,
                    Count = g.Count(),
                    Nickname = g.First().Nickname,
                    FirstCommentId = g.First().CommentId,
                    VideoId = g.First().VideoId
                })
                .OrderByDescending(g => g.Count)
                .ToList();

            //dataGridView1.Invoke(() =>
            //{
            //    dataGridView1.Columns.Clear();
            //    dataGridView1.Rows.Clear();

            //    dataGridView1.Columns.Add("User", "User ID");
            //    dataGridView1.Columns.Add("Nickname", "Nickname");
            //    dataGridView1.Columns.Add("Count", "Số comment");
            //    dataGridView1.Columns.Add("Link", "Link đến comment");

            //    foreach (var g in grouped)
            //    {
            //        string link = $"https://www.tiktok.com/@{g.User}/video/{g.VideoId}?cid={g.FirstCommentId}";
            //        dataGridView1.Rows.Add(g.User, g.Nickname, g.Count, link);
            //    }
            //});
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Lấy số thứ tự dòng (bắt đầu từ 1)
            //string rowNumber = (e.RowIndex + 1).ToString();

            //// Tạo một đối tượng StringFormat để căn giữa văn bản
            //StringFormat sf = new StringFormat()
            //{
            //    Alignment = StringAlignment.Center,
            //    LineAlignment = StringAlignment.Center
            //};

            //// Vẽ số thứ tự vào Header của dòng
            //e.Graphics.DrawString(
            //    rowNumber,
            //    this.dataGridView1.DefaultCellStyle.Font, // Sử dụng font mặc định của DataGridView
            //    System.Drawing.Brushes.Black,             // Màu chữ
            //    e.RowBounds.Location.X + (e.RowBounds.Width / 2), // Căn giữa theo chiều ngang
            //    e.RowBounds.Location.Y + (e.RowBounds.Height / 2),// Căn giữa theo chiều dọc
            //    sf
            //);
        }

        private DateTime UnixTimeStampToDateTime(long unixTime)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixTime).ToLocalTime().DateTime;
        }
    }
    public class CommentInfo
    {
        public string CommentId { get; set; }
        public string UserId { get; set; }
        public string UniqueId { get; set; }
        public string Nickname { get; set; }
        public string Text { get; set; }
        public int DiggCount { get; set; }
        public DateTime CreateTime { get; set; }
        public string AvatarUrl { get; set; }
        public string VideoId { get; set; }
        public int Reply { get; set; }
        public string CommentReplyId { get; set; }
    }
}
