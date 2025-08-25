namespace ePQTiktokLive.LIVE
{
    partial class frm_TTLiveConnect2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pTop = new System.Windows.Forms.Panel();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTiktoklive = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.pTiktok = new System.Windows.Forms.PictureBox();
            this.pBottom = new System.Windows.Forms.Panel();
            this.pLeft = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pView = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.txtSau = new System.Windows.Forms.TextBox();
            this.txt64 = new System.Windows.Forms.TextBox();
            this.lbView = new System.Windows.Forms.Label();
            this.lbJoin = new System.Windows.Forms.Label();
            this.pComment = new System.Windows.Forms.Panel();
            this.dgvComment = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pCommentTop = new System.Windows.Forms.Panel();
            this.lbTiktokUser = new System.Windows.Forms.Label();
            this.pTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pTiktok)).BeginInit();
            this.pLeft.SuspendLayout();
            this.pView.SuspendLayout();
            this.pComment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComment)).BeginInit();
            this.pCommentTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.White;
            this.pTop.Controls.Add(this.btnConnect);
            this.pTop.Controls.Add(this.label4);
            this.pTop.Controls.Add(this.txtTiktoklive);
            this.pTop.Controls.Add(this.label1);
            this.pTop.Controls.Add(this.lbTitle);
            this.pTop.Controls.Add(this.pTiktok);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(1283, 51);
            this.pTop.TabIndex = 1;
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnConnect.FlatAppearance.BorderSize = 0;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ForeColor = System.Drawing.Color.White;
            this.btnConnect.Location = new System.Drawing.Point(1187, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(84, 27);
            this.btnConnect.TabIndex = 29;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(957, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "UserId Tiktok:";
            // 
            // txtTiktoklive
            // 
            this.txtTiktoklive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTiktoklive.Location = new System.Drawing.Point(1037, 18);
            this.txtTiktoklive.Name = "txtTiktoklive";
            this.txtTiktoklive.Size = new System.Drawing.Size(138, 20);
            this.txtTiktoklive.TabIndex = 27;
            this.txtTiktoklive.Text = "kimtronsalan1994";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "đọc bình luận, chốt đơn live TikTok";
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.Location = new System.Drawing.Point(69, 5);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(216, 29);
            this.lbTitle.TabIndex = 1;
            this.lbTitle.Text = "Get live Comments";
            // 
            // pTiktok
            // 
            this.pTiktok.Image = global::ePQTiktokLive.Properties.Resources.tiktok;
            this.pTiktok.Location = new System.Drawing.Point(24, 7);
            this.pTiktok.Name = "pTiktok";
            this.pTiktok.Size = new System.Drawing.Size(40, 40);
            this.pTiktok.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pTiktok.TabIndex = 0;
            this.pTiktok.TabStop = false;
            this.pTiktok.Click += new System.EventHandler(this.pTiktok_Click);
            // 
            // pBottom
            // 
            this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottom.Location = new System.Drawing.Point(0, 678);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(1283, 14);
            this.pBottom.TabIndex = 2;
            // 
            // pLeft
            // 
            this.pLeft.Controls.Add(this.flowLayoutPanel1);
            this.pLeft.Controls.Add(this.pView);
            this.pLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pLeft.Location = new System.Drawing.Point(0, 51);
            this.pLeft.Name = "pLeft";
            this.pLeft.Size = new System.Drawing.Size(864, 627);
            this.pLeft.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 39);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(864, 588);
            this.flowLayoutPanel1.TabIndex = 2;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // pView
            // 
            this.pView.Controls.Add(this.button1);
            this.pView.Controls.Add(this.txtSau);
            this.pView.Controls.Add(this.txt64);
            this.pView.Controls.Add(this.lbView);
            this.pView.Controls.Add(this.lbJoin);
            this.pView.Dock = System.Windows.Forms.DockStyle.Top;
            this.pView.Location = new System.Drawing.Point(0, 0);
            this.pView.Name = "pView";
            this.pView.Size = new System.Drawing.Size(864, 39);
            this.pView.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(429, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 23);
            this.button1.TabIndex = 27;
            this.button1.Text = "giả mã";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtSau
            // 
            this.txtSau.Location = new System.Drawing.Point(495, 8);
            this.txtSau.Name = "txtSau";
            this.txtSau.Size = new System.Drawing.Size(207, 20);
            this.txtSau.TabIndex = 26;
            // 
            // txt64
            // 
            this.txt64.Location = new System.Drawing.Point(215, 8);
            this.txt64.Name = "txt64";
            this.txt64.Size = new System.Drawing.Size(207, 20);
            this.txt64.TabIndex = 0;
            // 
            // lbView
            // 
            this.lbView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbView.AutoSize = true;
            this.lbView.ForeColor = System.Drawing.Color.Blue;
            this.lbView.Location = new System.Drawing.Point(784, 8);
            this.lbView.Name = "lbView";
            this.lbView.Size = new System.Drawing.Size(54, 13);
            this.lbView.TabIndex = 25;
            this.lbView.Text = "View: 000";
            // 
            // lbJoin
            // 
            this.lbJoin.AutoSize = true;
            this.lbJoin.ForeColor = System.Drawing.Color.Green;
            this.lbJoin.Location = new System.Drawing.Point(21, 8);
            this.lbJoin.Name = "lbJoin";
            this.lbJoin.Size = new System.Drawing.Size(32, 13);
            this.lbJoin.TabIndex = 24;
            this.lbJoin.Text = "Join: ";
            // 
            // pComment
            // 
            this.pComment.Controls.Add(this.dgvComment);
            this.pComment.Controls.Add(this.pCommentTop);
            this.pComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pComment.Location = new System.Drawing.Point(864, 51);
            this.pComment.Name = "pComment";
            this.pComment.Size = new System.Drawing.Size(419, 627);
            this.pComment.TabIndex = 4;
            // 
            // dgvComment
            // 
            this.dgvComment.AllowUserToAddRows = false;
            this.dgvComment.AllowUserToResizeColumns = false;
            this.dgvComment.AllowUserToResizeRows = false;
            this.dgvComment.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvComment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgvComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvComment.Location = new System.Drawing.Point(0, 39);
            this.dgvComment.Name = "dgvComment";
            this.dgvComment.RowHeadersWidth = 10;
            this.dgvComment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvComment.Size = new System.Drawing.Size(419, 588);
            this.dgvComment.TabIndex = 12;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Time";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            this.Column1.Width = 135;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "User";
            this.Column2.Name = "Column2";
            this.Column2.Width = 120;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Comment";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Phone / ID";
            this.Column4.Name = "Column4";
            this.Column4.Visible = false;
            // 
            // pCommentTop
            // 
            this.pCommentTop.Controls.Add(this.lbTiktokUser);
            this.pCommentTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pCommentTop.Location = new System.Drawing.Point(0, 0);
            this.pCommentTop.Name = "pCommentTop";
            this.pCommentTop.Size = new System.Drawing.Size(419, 39);
            this.pCommentTop.TabIndex = 0;
            // 
            // lbTiktokUser
            // 
            this.lbTiktokUser.AutoSize = true;
            this.lbTiktokUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTiktokUser.Location = new System.Drawing.Point(20, 8);
            this.lbTiktokUser.Name = "lbTiktokUser";
            this.lbTiktokUser.Size = new System.Drawing.Size(84, 13);
            this.lbTiktokUser.TabIndex = 29;
            this.lbTiktokUser.Text = "UserId Tiktok";
            // 
            // frm_TTLiveConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 692);
            this.Controls.Add(this.pComment);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pBottom);
            this.Controls.Add(this.pTop);
            this.Name = "frm_TTLiveConnect";
            this.Text = "Chốt đơn TikTok - Ks. Quí 0974.36.76.76";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pTop.ResumeLayout(false);
            this.pTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pTiktok)).EndInit();
            this.pLeft.ResumeLayout(false);
            this.pView.ResumeLayout(false);
            this.pView.PerformLayout();
            this.pComment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvComment)).EndInit();
            this.pCommentTop.ResumeLayout(false);
            this.pCommentTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.PictureBox pTiktok;
        private System.Windows.Forms.Panel pBottom;
        private System.Windows.Forms.Panel pLeft;
        private System.Windows.Forms.Panel pComment;
        private System.Windows.Forms.Panel pCommentTop;
        private System.Windows.Forms.DataGridView dgvComment;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Panel pView;
        private System.Windows.Forms.Label lbView;
        private System.Windows.Forms.Label lbJoin;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTiktoklive;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lbTiktokUser;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSau;
        private System.Windows.Forms.TextBox txt64;
    }
}