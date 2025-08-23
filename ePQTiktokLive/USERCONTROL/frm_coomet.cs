using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ePQTiktokLive.USERCONTROL
{
    public partial class frm_coomet : Form
    {
        public frm_coomet()
        {
            InitializeComponent();
        }
        private void AddCommentItem(string name, string phoneNumber, string code)
        {
            Panel commentItemPanel = new Panel();
            commentItemPanel.Dock = DockStyle.Top;
            commentItemPanel.Height = 80;
            commentItemPanel.Padding = new Padding(10);
            commentItemPanel.BackColor = Color.White;
            commentItemPanel.BorderStyle = BorderStyle.FixedSingle;

            // Ảnh đại diện
            PictureBox avatarBox = new PictureBox();
            avatarBox.Size = new Size(50, 50);
            avatarBox.Location = new Point(10, 15);
            avatarBox.Image = GetRandomAvatar(); // Tải ảnh đại diện mẫu
            avatarBox.SizeMode = PictureBoxSizeMode.Zoom;
            avatarBox.BorderStyle = BorderStyle.FixedSingle;
            commentItemPanel.Controls.Add(avatarBox);

            // Tên và số điện thoại
            Label nameLabel = new Label();
            nameLabel.Text = name;
            nameLabel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            nameLabel.Location = new Point(70, 10);
            nameLabel.AutoSize = true;
            commentItemPanel.Controls.Add(nameLabel);

            Label phoneLabel = new Label();
            phoneLabel.Text = "M1 " + phoneNumber;
            phoneLabel.Font = new Font("Segoe UI", 10);
            phoneLabel.Location = new Point(70, 30);
            phoneLabel.AutoSize = true;
            commentItemPanel.Controls.Add(phoneLabel);

            // Các nút
            Button createOrderButton = new Button();
            createOrderButton.Text = "Tạo đơn hàng";
            createOrderButton.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            createOrderButton.BackColor = Color.FromArgb(0, 122, 204); // Màu xanh dương
            createOrderButton.ForeColor = Color.White;
            createOrderButton.FlatStyle = FlatStyle.Flat;
            createOrderButton.FlatAppearance.BorderSize = 0;
            createOrderButton.Size = new Size(120, 30);
            createOrderButton.Location = new Point(70, 45);
            commentItemPanel.Controls.Add(createOrderButton);

            Button infoButton = new Button();
            infoButton.Text = "Thông tin";
            infoButton.Font = new Font("Segoe UI", 9);
            infoButton.BackColor = Color.FromArgb(240, 242, 245);
            infoButton.FlatStyle = FlatStyle.Flat;
            infoButton.FlatAppearance.BorderSize = 1;
            infoButton.Size = new Size(80, 30);
            infoButton.Location = new Point(200, 45);
            commentItemPanel.Controls.Add(infoButton);

            Button statusButton = new Button();
            statusButton.Text = "Bình thường";
            statusButton.Font = new Font("Segoe UI", 9);
            statusButton.BackColor = Color.FromArgb(240, 242, 245);
            statusButton.FlatStyle = FlatStyle.Flat;
            statusButton.FlatAppearance.BorderSize = 1;
            statusButton.Size = new Size(100, 30);
            statusButton.Location = new Point(290, 45);
            commentItemPanel.Controls.Add(statusButton);

            commentsPanel.Controls.Add(commentItemPanel);
        }
        private void LoadSampleComments()
        {
            string[] sampleNames = { "Lã Kim Huyên", "Hoàng Mẫn Nhi", "Cao Minh Trí", "Thái Hoàng", "Lan Anh" };
            string[] samplePhones = { "0902492094", "0803245691", "0903765389", "0903787589", "0906095212" };
            string[] sampleCodes = { "M1", "M1", "M1", "M1", "M1" };

            for (int i = 0; i < sampleNames.Length; i++)
            {
                AddCommentItem(sampleNames[i], samplePhones[i], sampleCodes[i]);
            }
        }
        // Hàm tạo ảnh đại diện ngẫu nhiên
        private Image GetRandomAvatar()
        {
            // Trong thực tế, bạn sẽ tải ảnh từ một đường dẫn hoặc cơ sở dữ liệu.
            // Ở đây, chúng ta sẽ tạo một ảnh màu ngẫu nhiên đơn giản
            Bitmap bmp = new Bitmap(50, 50);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                Random r = new Random();
                Color randomColor = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
                g.Clear(randomColor);
            }
            return bmp;
        }

        private void frm_coomet_Load(object sender, EventArgs e)
        {
            LoadSampleComments();
        }
    }
}
