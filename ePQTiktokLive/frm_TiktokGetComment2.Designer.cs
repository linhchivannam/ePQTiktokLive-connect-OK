namespace ePQTiktokLive
{
    partial class frm_TiktokGetComment2
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtTiktoklive = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.labelTotalComment = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelProgress = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lbSLDong = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.commentIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uniqueIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nicknameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diggCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avatarUrlDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.videoIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.replyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentReplyIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commentInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // webView21
            // 
            this.webView21.AllowExternalDrop = true;
            this.webView21.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webView21.BackColor = System.Drawing.Color.White;
            this.webView21.CreationProperties = null;
            this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView21.Location = new System.Drawing.Point(15, 41);
            this.webView21.Name = "webView21";
            this.webView21.Size = new System.Drawing.Size(278, 0);
            this.webView21.TabIndex = 9;
            this.webView21.Visible = false;
            this.webView21.ZoomFactor = 1D;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(249, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(65, 23);
            this.btnConnect.TabIndex = 8;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtTiktoklive
            // 
            this.txtTiktoklive.Location = new System.Drawing.Point(11, 12);
            this.txtTiktoklive.Name = "txtTiktoklive";
            this.txtTiktoklive.Size = new System.Drawing.Size(231, 20);
            this.txtTiktoklive.TabIndex = 7;
            this.txtTiktoklive.Text = "https://www.tiktok.com/@miumiuaudio_/video/7507865863492472081";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.commentIdDataGridViewTextBoxColumn,
            this.userIdDataGridViewTextBoxColumn,
            this.uniqueIdDataGridViewTextBoxColumn,
            this.nicknameDataGridViewTextBoxColumn,
            this.textDataGridViewTextBoxColumn,
            this.diggCountDataGridViewTextBoxColumn,
            this.createTimeDataGridViewTextBoxColumn,
            this.avatarUrlDataGridViewTextBoxColumn,
            this.videoIdDataGridViewTextBoxColumn,
            this.replyDataGridViewTextBoxColumn,
            this.commentReplyIdDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.commentInfoBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(15, 41);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 10;
            this.dataGridView1.Size = new System.Drawing.Size(873, 322);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // labelTotalComment
            // 
            this.labelTotalComment.AutoSize = true;
            this.labelTotalComment.Location = new System.Drawing.Point(333, 18);
            this.labelTotalComment.Name = "labelTotalComment";
            this.labelTotalComment.Size = new System.Drawing.Size(25, 13);
            this.labelTotalComment.TabIndex = 11;
            this.labelTotalComment.Text = "000";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(457, 13);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(212, 19);
            this.progressBar1.TabIndex = 12;
            // 
            // labelProgress
            // 
            this.labelProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(674, 18);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(25, 13);
            this.labelProgress.TabIndex = 13;
            this.labelProgress.Text = "000";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(15, 369);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "xem nhóm người";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbSLDong
            // 
            this.lbSLDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSLDong.AutoSize = true;
            this.lbSLDong.ForeColor = System.Drawing.Color.Blue;
            this.lbSLDong.Location = new System.Drawing.Point(758, 374);
            this.lbSLDong.Name = "lbSLDong";
            this.lbSLDong.Size = new System.Drawing.Size(73, 13);
            this.lbSLDong.TabIndex = 15;
            this.lbSLDong.Text = "Số lượng: 000";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(131, 369);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Xuất dữ liệu Excel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // commentIdDataGridViewTextBoxColumn
            // 
            this.commentIdDataGridViewTextBoxColumn.DataPropertyName = "CommentId";
            this.commentIdDataGridViewTextBoxColumn.HeaderText = "CommentId";
            this.commentIdDataGridViewTextBoxColumn.Name = "commentIdDataGridViewTextBoxColumn";
            this.commentIdDataGridViewTextBoxColumn.Width = 120;
            // 
            // userIdDataGridViewTextBoxColumn
            // 
            this.userIdDataGridViewTextBoxColumn.DataPropertyName = "UserId";
            this.userIdDataGridViewTextBoxColumn.HeaderText = "UserId";
            this.userIdDataGridViewTextBoxColumn.Name = "userIdDataGridViewTextBoxColumn";
            this.userIdDataGridViewTextBoxColumn.Width = 120;
            // 
            // uniqueIdDataGridViewTextBoxColumn
            // 
            this.uniqueIdDataGridViewTextBoxColumn.DataPropertyName = "UniqueId";
            this.uniqueIdDataGridViewTextBoxColumn.HeaderText = "UniqueId";
            this.uniqueIdDataGridViewTextBoxColumn.Name = "uniqueIdDataGridViewTextBoxColumn";
            this.uniqueIdDataGridViewTextBoxColumn.Width = 120;
            // 
            // nicknameDataGridViewTextBoxColumn
            // 
            this.nicknameDataGridViewTextBoxColumn.DataPropertyName = "Nickname";
            this.nicknameDataGridViewTextBoxColumn.HeaderText = "Nickname";
            this.nicknameDataGridViewTextBoxColumn.Name = "nicknameDataGridViewTextBoxColumn";
            this.nicknameDataGridViewTextBoxColumn.Width = 120;
            // 
            // textDataGridViewTextBoxColumn
            // 
            this.textDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.textDataGridViewTextBoxColumn.DataPropertyName = "Text";
            this.textDataGridViewTextBoxColumn.HeaderText = "Text";
            this.textDataGridViewTextBoxColumn.Name = "textDataGridViewTextBoxColumn";
            // 
            // diggCountDataGridViewTextBoxColumn
            // 
            this.diggCountDataGridViewTextBoxColumn.DataPropertyName = "DiggCount";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.diggCountDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.diggCountDataGridViewTextBoxColumn.HeaderText = "Like";
            this.diggCountDataGridViewTextBoxColumn.Name = "diggCountDataGridViewTextBoxColumn";
            this.diggCountDataGridViewTextBoxColumn.Width = 60;
            // 
            // createTimeDataGridViewTextBoxColumn
            // 
            this.createTimeDataGridViewTextBoxColumn.DataPropertyName = "CreateTime";
            this.createTimeDataGridViewTextBoxColumn.HeaderText = "CreateTime";
            this.createTimeDataGridViewTextBoxColumn.Name = "createTimeDataGridViewTextBoxColumn";
            this.createTimeDataGridViewTextBoxColumn.Width = 130;
            // 
            // avatarUrlDataGridViewTextBoxColumn
            // 
            this.avatarUrlDataGridViewTextBoxColumn.DataPropertyName = "AvatarUrl";
            this.avatarUrlDataGridViewTextBoxColumn.HeaderText = "AvatarUrl";
            this.avatarUrlDataGridViewTextBoxColumn.Name = "avatarUrlDataGridViewTextBoxColumn";
            // 
            // videoIdDataGridViewTextBoxColumn
            // 
            this.videoIdDataGridViewTextBoxColumn.DataPropertyName = "VideoId";
            this.videoIdDataGridViewTextBoxColumn.HeaderText = "VideoId";
            this.videoIdDataGridViewTextBoxColumn.Name = "videoIdDataGridViewTextBoxColumn";
            this.videoIdDataGridViewTextBoxColumn.Visible = false;
            // 
            // replyDataGridViewTextBoxColumn
            // 
            this.replyDataGridViewTextBoxColumn.DataPropertyName = "Reply";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.replyDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.replyDataGridViewTextBoxColumn.HeaderText = "Reply";
            this.replyDataGridViewTextBoxColumn.Name = "replyDataGridViewTextBoxColumn";
            this.replyDataGridViewTextBoxColumn.Width = 60;
            // 
            // commentReplyIdDataGridViewTextBoxColumn
            // 
            this.commentReplyIdDataGridViewTextBoxColumn.DataPropertyName = "CommentReplyId";
            this.commentReplyIdDataGridViewTextBoxColumn.HeaderText = "CommentReplyId";
            this.commentReplyIdDataGridViewTextBoxColumn.Name = "commentReplyIdDataGridViewTextBoxColumn";
            this.commentReplyIdDataGridViewTextBoxColumn.Width = 120;
            // 
            // commentInfoBindingSource
            // 
            this.commentInfoBindingSource.DataSource = typeof(ePQTiktokLive.CommentInfo);
            // 
            // frm_TiktokGetComment2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 396);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lbSLDong);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.labelTotalComment);
            this.Controls.Add(this.webView21);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtTiktoklive);
            this.Name = "frm_TiktokGetComment2";
            this.Text = "frm_TiktokGetComment";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_TiktokGetComment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commentInfoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtTiktoklive;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label labelTotalComment;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource commentInfoBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uniqueIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nicknameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn textDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diggCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn avatarUrlDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn videoIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn replyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentReplyIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label lbSLDong;
        private System.Windows.Forms.Button button2;
    }
}