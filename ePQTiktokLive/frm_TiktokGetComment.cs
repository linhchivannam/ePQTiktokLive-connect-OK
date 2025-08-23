using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ePQTiktokLive
{
    public partial class frm_TiktokGetComment : Form
    {
        public frm_TiktokGetComment()
        {
            InitializeComponent();
            InitializeAsync();
        }
        string videoId, userId;
        private List<CommentInfo> allCommentList;

        private List<CommentItem> commentList = new List<CommentItem>();
        private int totalExpectedComments = 0;
        private int loadedComments = 0;
        private async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
           // webView21.CoreWebView2.WebResourceResponseReceived += CoreWebView2_WebResourceResponseReceived;
            allCommentList = new List<CommentInfo>();
        }

        private async void CoreWebView2_WebResourceResponseReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebResourceResponseReceivedEventArgs e)
        {
            try
            {
                string url = e.Request.Uri;

                // TikTok API comment  
                if (url.Contains("/api/comment/list/"))
                {
                    using (var responseStream = await e.Response.GetContentAsync())
                    using (var memoryStream = new MemoryStream())
                    {
                        await responseStream.CopyToAsync(memoryStream);
                        string json = Encoding.UTF8.GetString(memoryStream.ToArray());

                        var obj = JObject.Parse(json);

                        // Đọc tổng số comment nếu có  
                        if (totalExpectedComments == 0 && obj["total"] != null)
                        {
                            totalExpectedComments = (int)obj["total"];
                            Invoke(new Action(() =>
                            {
                                progressBar1.Maximum = totalExpectedComments;
                            }));
                        }

                        var comments = obj["comments"];
                        if (comments != null)
                        {
                            foreach (var c in comments)
                            {
                                string commentId = (string)c["cid"];
                                if (!commentList.Any(x => x.CommentId == commentId))
                                {
                                    commentList.Add(new CommentItem
                                    {
                                        CommentId = commentId,
                                        Text = (string)c["text"],
                                        UserName = (string)c["user"]["unique_id"],
                                        UserNickName = (string)c["user"]["nickname"],
                                        LikeCount = (int?)c["digg_count"] ?? 0,
                                        CreateTime = DateTimeOffset.FromUnixTimeSeconds((long)c["create_time"]).LocalDateTime
                                    });
                                    loadedComments++;
                                }
                            }

                            // Cập nhật tiến trình  
                            Invoke(new Action(() =>
                            {
                                progressBar1.Value = Math.Min(loadedComments, progressBar1.Maximum);
                                labelProgress.Text = $"Đã tải: {loadedComments}/{totalExpectedComments}";
                                dataGridView1.DataSource = null;
                                dataGridView1.DataSource = commentList.ToList();
                            }));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi đọc comment: " + ex.Message);
            }
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

            // Scroll liên tục để TikTok load comment
        string scrollScript = @"
            let count = 0;
            let interval = setInterval(() => {
                window.scrollBy(0, 500);
                count++;
                if (count > 100) clearInterval(interval);
            }, 1000);
        ";
            await webView21.ExecuteScriptAsync(scrollScript);
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

        private async Task FetchComments(string videoId, string cookieHeader)
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

            while (hasMore && retryCount < 5)
            {
                string apiUrl = $"https://www.tiktok.com/api/comment/list/?aid=1988&aweme_id={videoId}&count=50&cursor={cursor}";
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
                        UserId = c.user.id,
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

                    allCommentList.Add(info);
                    //if (info.Reply > 0)
                    //{
                    //    // MessageBox.Show("Có reply");
                    //    string replyBaseUrl = $"https://www.tiktok.com/api/comment/list/reply/?aid=1988&count=20&comment_id={info.CommentId}";
                    //    await FetchReplies(info.CommentId, videoId, cookieHeader);
                    //}
                }

                totalFetched += result.comments.Count;
                lastCursor = cursor;
                cursor = result.cursor?.ToString();
                hasMore = result.has_more == true && cursor != lastCursor;

                int progress = Math.Min(100, totalFetched * 100 / (total == 0 ? 1 : total));
                progressBar1.Value = progress;
                labelProgress.Text = $"Đã tải: {totalFetched}/{total} comment ({progress}%)";

                retryCount = 0;
                await Task.Delay(800); // delay nhỏ tránh bị chặn
            }

           // ShowComments(allCommentList);
        }

        private void ShowComments(List<CommentInfo> list)
        {
            //dataGridView1.Invoke(() =>
            //{
            //    dataGridView1.Columns.Clear();
            //    dataGridView1.Rows.Clear();

            //    dataGridView1.Columns.Add("UserId", "User ID");
            //    dataGridView1.Columns.Add("Nickname", "Nickname");
            //    dataGridView1.Columns.Add("Text", "Comment");
            //    dataGridView1.Columns.Add("Like", "Like");
            //    dataGridView1.Columns.Add("Time", "Time");
            //    dataGridView1.Columns.Add("CommentId", "Comment ID");

            //    foreach (var c in list)
            //    {
            //        dataGridView1.Rows.Add(c.UniqueId, c.Nickname, c.Text, c.DiggCount, c.CreateTime, c.CommentId);
            //    }
            //});
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
            string rowNumber = (e.RowIndex + 1).ToString();

            // Tạo một đối tượng StringFormat để căn giữa văn bản
            StringFormat sf = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // Vẽ số thứ tự vào Header của dòng
            e.Graphics.DrawString(
                rowNumber,
                this.dataGridView1.DefaultCellStyle.Font, // Sử dụng font mặc định của DataGridView
                System.Drawing.Brushes.Black,             // Màu chữ
                e.RowBounds.Location.X + (e.RowBounds.Width / 2), // Căn giữa theo chiều ngang
                e.RowBounds.Location.Y + (e.RowBounds.Height / 2),// Căn giữa theo chiều dọc
                sf
            );
        }

        private DateTime UnixTimeStampToDateTime(long unixTime)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixTime).ToLocalTime().DateTime;
        }
    }
    public class CommentItem
    {
        public string CommentId { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public string UserNickName { get; set; }
        public int LikeCount { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
