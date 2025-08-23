using ePQTiktokLive.MODEL;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ePQTiktokLive.USERCONTROL
{
    public partial class Comment : UserControl
    {
        private PQComment _commentData;
        PictureBox pictureBox;

        public event EventHandler<CommentEventArgs> taodonghangClick;
        public event EventHandler<CommentEventArgs> thongtinClick;
        public event EventHandler<CommentEventArgs> binhthuongClick;

        HttpClient client;
        public Comment(PQComment data, HttpClient _client)
        {
            InitializeComponent();
            _commentData = data;
            client = _client;
            LoadCommentData();
        }
        private void LoadCommentData()
        {
            // Gán dữ liệu từ _commentData vào các controls
            //avatarBox.Image. = _commentData.UserAvatar;
            nameLabel.Text = _commentData.UserName;
            phoneLabel.Text = _commentData.phoneNumber;
            commentlabel.Text = _commentData.Text;

            if (_commentData.UserAvatar.Length > 0)
            {
                using (JsonDocument document = JsonDocument.Parse(_commentData.UserAvatar))
                {
                    // Lấy phần tử gốc của chuỗi JSON (là một mảng)
                    JsonElement root = document.RootElement;

                    // Kiểm tra xem phần tử gốc có phải là một mảng không
                    if (root.ValueKind == JsonValueKind.Array)
                    {
                        // Lấy phần tử đầu tiên của mảng (chỉ mục 0)
                        if (root.GetArrayLength() > 0)
                        {
                            string firstImageUrl = root[0].GetString();
                            LoadImageWithRefererAsync(firstImageUrl);
                           // avatarBox.LoadAsync(firstImageUrl);
                        }
                        else
                        {
                           // Console.WriteLine("Mảng JSON rỗng.");
                        }
                    }
                    else
                    {
                       // Console.WriteLine("Chuỗi JSON không phải là một mảng.");
                    }
                }
            }

            
        }
         async void LoadImageWithRefererAsync(string imageUrl)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, imageUrl);
                request.Headers.Add("Referer", "https://www.tiktok.com/");

                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                using (Stream stream = await response.Content.ReadAsStreamAsync())
                {
                    // Sử dụng MagickImage để đọc ảnh từ stream
                    using (var imageMagick = new MagickImage(stream))
                    {
                      using (var bitmapStream = new MemoryStream())
                        {
                            // Ghi ảnh vào stream với định dạng Bitmap
                            imageMagick.Write(bitmapStream, MagickFormat.Bmp);

                            // Đưa con trỏ stream về đầu để có thể đọc
                            bitmapStream.Position = 0;
                            avatarBox.Image = new Bitmap(bitmapStream);
                            // Trả về một Bitmap từ stream
                           // return new Bitmap(bitmapStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tải ảnh: {ex.Message}");
                //return null;
            }
        }


        private void Comment_Load(object sender, EventArgs e)
        {
            
        }

        private void avatarBox_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {

           // avatarBox.Image = pictureBox.Image;
       
         }

        private void btnTaodon_Click(object sender, EventArgs e)
        {
            taodonghangClick?.Invoke(this, new CommentEventArgs(_commentData));
        }

        private void btnThongtin_Click(object sender, EventArgs e)
        {
            thongtinClick?.Invoke(this, new CommentEventArgs(_commentData));
        }

        private void btnBinhthuong_Click(object sender, EventArgs e)
        {
            binhthuongClick?.Invoke(this, new CommentEventArgs(_commentData));
        }
    }
    public class CommentEventArgs : EventArgs
    {
        public PQComment Comment { get; }
        public CommentEventArgs(PQComment comment)
        {
            Comment = comment;
        }
    }
}
