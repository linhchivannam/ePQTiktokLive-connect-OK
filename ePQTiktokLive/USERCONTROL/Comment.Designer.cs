namespace ePQTiktokLive.USERCONTROL
{
    partial class Comment
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pvalue = new System.Windows.Forms.Panel();
            this.btnBinhthuong = new System.Windows.Forms.Button();
            this.btnThongtin = new System.Windows.Forms.Button();
            this.btnTaodon = new System.Windows.Forms.Button();
            this.commentlabel = new System.Windows.Forms.Label();
            this.phoneLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.avatarBox = new System.Windows.Forms.PictureBox();
            this.pvalue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.avatarBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pvalue
            // 
            this.pvalue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pvalue.Controls.Add(this.btnBinhthuong);
            this.pvalue.Controls.Add(this.btnThongtin);
            this.pvalue.Controls.Add(this.btnTaodon);
            this.pvalue.Controls.Add(this.commentlabel);
            this.pvalue.Controls.Add(this.phoneLabel);
            this.pvalue.Controls.Add(this.nameLabel);
            this.pvalue.Controls.Add(this.avatarBox);
            this.pvalue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pvalue.Location = new System.Drawing.Point(0, 0);
            this.pvalue.Name = "pvalue";
            this.pvalue.Size = new System.Drawing.Size(543, 79);
            this.pvalue.TabIndex = 0;
            // 
            // btnBinhthuong
            // 
            this.btnBinhthuong.BackColor = System.Drawing.Color.Transparent;
            this.btnBinhthuong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBinhthuong.Location = new System.Drawing.Point(318, 44);
            this.btnBinhthuong.Name = "btnBinhthuong";
            this.btnBinhthuong.Size = new System.Drawing.Size(114, 27);
            this.btnBinhthuong.TabIndex = 6;
            this.btnBinhthuong.Text = "Bình thường";
            this.btnBinhthuong.UseVisualStyleBackColor = false;
            this.btnBinhthuong.Click += new System.EventHandler(this.btnBinhthuong_Click);
            // 
            // btnThongtin
            // 
            this.btnThongtin.BackColor = System.Drawing.Color.Transparent;
            this.btnThongtin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongtin.Location = new System.Drawing.Point(210, 44);
            this.btnThongtin.Name = "btnThongtin";
            this.btnThongtin.Size = new System.Drawing.Size(90, 27);
            this.btnThongtin.TabIndex = 5;
            this.btnThongtin.Text = "Thông tin";
            this.btnThongtin.UseVisualStyleBackColor = false;
            this.btnThongtin.Click += new System.EventHandler(this.btnThongtin_Click);
            // 
            // btnTaodon
            // 
            this.btnTaodon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnTaodon.FlatAppearance.BorderSize = 0;
            this.btnTaodon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaodon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaodon.ForeColor = System.Drawing.Color.White;
            this.btnTaodon.Location = new System.Drawing.Point(82, 44);
            this.btnTaodon.Name = "btnTaodon";
            this.btnTaodon.Size = new System.Drawing.Size(114, 27);
            this.btnTaodon.TabIndex = 4;
            this.btnTaodon.Text = "Tạo đơn hàng";
            this.btnTaodon.UseVisualStyleBackColor = false;
            this.btnTaodon.Click += new System.EventHandler(this.btnTaodon_Click);
            // 
            // commentlabel
            // 
            this.commentlabel.AutoSize = true;
            this.commentlabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commentlabel.ForeColor = System.Drawing.Color.Black;
            this.commentlabel.Location = new System.Drawing.Point(78, 22);
            this.commentlabel.Name = "commentlabel";
            this.commentlabel.Size = new System.Drawing.Size(126, 19);
            this.commentlabel.TabIndex = 3;
            this.commentlabel.Text = "nội dung comment";
            // 
            // phoneLabel
            // 
            this.phoneLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.phoneLabel.AutoSize = true;
            this.phoneLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phoneLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.phoneLabel.Location = new System.Drawing.Point(397, 6);
            this.phoneLabel.Name = "phoneLabel";
            this.phoneLabel.Size = new System.Drawing.Size(72, 19);
            this.phoneLabel.TabIndex = 2;
            this.phoneLabel.Text = "Điện thoại";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(78, 2);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(80, 20);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Username";
            // 
            // avatarBox
            // 
            this.avatarBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.avatarBox.Location = new System.Drawing.Point(14, 6);
            this.avatarBox.Name = "avatarBox";
            this.avatarBox.Size = new System.Drawing.Size(60, 60);
            this.avatarBox.TabIndex = 0;
            this.avatarBox.TabStop = false;
            this.avatarBox.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.avatarBox_LoadCompleted);
            // 
            // Comment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pvalue);
            this.Name = "Comment";
            this.Size = new System.Drawing.Size(543, 79);
            this.Load += new System.EventHandler(this.Comment_Load);
            this.pvalue.ResumeLayout(false);
            this.pvalue.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.avatarBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pvalue;
        private System.Windows.Forms.PictureBox avatarBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label phoneLabel;
        private System.Windows.Forms.Label commentlabel;
        private System.Windows.Forms.Button btnTaodon;
        private System.Windows.Forms.Button btnBinhthuong;
        private System.Windows.Forms.Button btnThongtin;
    }
}
