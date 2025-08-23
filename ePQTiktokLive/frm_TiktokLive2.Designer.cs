namespace ePQTiktokLive
{
    partial class frm_TiktokLive2
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.RoomID = new System.Windows.Forms.Label();
            this.txtRomID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTTUser = new System.Windows.Forms.TextBox();
            this.txtWebSocketUrl = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.txtTiktoklive = new System.Windows.Forms.TextBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.txtPayload = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.txtLogID = new System.Windows.Forms.TextBox();
            this.payload = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.payloadEncodingDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.payloadTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.compresstypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imcursorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regLogBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtResult = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.regLogBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.RoomID);
            this.panel1.Controls.Add(this.txtRomID);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtTTUser);
            this.panel1.Controls.Add(this.txtWebSocketUrl);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.txtTiktoklive);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1165, 128);
            this.panel1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 99);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(145, 23);
            this.button2.TabIndex = 21;
            this.button2.Text = "Đọc file Log";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(634, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RoomID
            // 
            this.RoomID.AutoSize = true;
            this.RoomID.Location = new System.Drawing.Point(323, 43);
            this.RoomID.Name = "RoomID";
            this.RoomID.Size = new System.Drawing.Size(52, 13);
            this.RoomID.TabIndex = 19;
            this.RoomID.Text = "Room ID:";
            // 
            // txtRomID
            // 
            this.txtRomID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRomID.Location = new System.Drawing.Point(403, 40);
            this.txtRomID.Name = "txtRomID";
            this.txtRomID.Size = new System.Drawing.Size(684, 20);
            this.txtRomID.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "WWS Url::";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "TTK User:";
            // 
            // txtTTUser
            // 
            this.txtTTUser.Location = new System.Drawing.Point(88, 38);
            this.txtTTUser.Name = "txtTTUser";
            this.txtTTUser.Size = new System.Drawing.Size(222, 20);
            this.txtTTUser.TabIndex = 15;
            // 
            // txtWebSocketUrl
            // 
            this.txtWebSocketUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWebSocketUrl.Location = new System.Drawing.Point(88, 64);
            this.txtWebSocketUrl.Name = "txtWebSocketUrl";
            this.txtWebSocketUrl.Size = new System.Drawing.Size(999, 20);
            this.txtWebSocketUrl.TabIndex = 14;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(364, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(145, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "Connect Websocket";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtTiktoklive
            // 
            this.txtTiktoklive.Location = new System.Drawing.Point(12, 12);
            this.txtTiktoklive.Name = "txtTiktoklive";
            this.txtTiktoklive.Size = new System.Drawing.Size(346, 20);
            this.txtTiktoklive.TabIndex = 12;
            this.txtTiktoklive.Text = "https://www.tiktok.com/@bida.thanh.minh/live";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.AutoGenerateColumns = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.logId,
            this.payloadEncodingDataGridViewTextBoxColumn,
            this.payloadTypeDataGridViewTextBoxColumn,
            this.compresstypeDataGridViewTextBoxColumn,
            this.imcursorDataGridViewTextBoxColumn,
            this.payload});
            this.dgv.DataSource = this.regLogBindingSource;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgv.Location = new System.Drawing.Point(0, 128);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersWidth = 10;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(731, 608);
            this.dgv.TabIndex = 11;
            this.dgv.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel2.Controls.Add(this.txtResult);
            this.panel2.Controls.Add(this.txtLogID);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.txtPayload);
            this.panel2.Controls.Add(this.listBoxLog);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(731, 128);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(434, 608);
            this.panel2.TabIndex = 12;
            // 
            // listBoxLog
            // 
            this.listBoxLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.HorizontalScrollbar = true;
            this.listBoxLog.Location = new System.Drawing.Point(0, 461);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.ScrollAlwaysVisible = true;
            this.listBoxLog.Size = new System.Drawing.Size(434, 147);
            this.listBoxLog.TabIndex = 2;
            // 
            // txtPayload
            // 
            this.txtPayload.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtPayload.Location = new System.Drawing.Point(0, 127);
            this.txtPayload.Multiline = true;
            this.txtPayload.Name = "txtPayload";
            this.txtPayload.Size = new System.Drawing.Size(434, 334);
            this.txtPayload.TabIndex = 3;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(18, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(102, 23);
            this.button4.TabIndex = 21;
            this.button4.Text = "Giải mã";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtLogID
            // 
            this.txtLogID.Location = new System.Drawing.Point(126, 9);
            this.txtLogID.Name = "txtLogID";
            this.txtLogID.Size = new System.Drawing.Size(222, 20);
            this.txtLogID.TabIndex = 22;
            // 
            // payload
            // 
            this.payload.DataPropertyName = "payload";
            this.payload.HeaderText = "payload";
            this.payload.Name = "payload";
            this.payload.Visible = false;
            // 
            // logId
            // 
            this.logId.DataPropertyName = "logId";
            this.logId.HeaderText = "logId";
            this.logId.Name = "logId";
            this.logId.Width = 150;
            // 
            // payloadEncodingDataGridViewTextBoxColumn
            // 
            this.payloadEncodingDataGridViewTextBoxColumn.DataPropertyName = "payloadEncoding";
            this.payloadEncodingDataGridViewTextBoxColumn.HeaderText = "payloadEncoding";
            this.payloadEncodingDataGridViewTextBoxColumn.Name = "payloadEncodingDataGridViewTextBoxColumn";
            // 
            // payloadTypeDataGridViewTextBoxColumn
            // 
            this.payloadTypeDataGridViewTextBoxColumn.DataPropertyName = "payloadType";
            this.payloadTypeDataGridViewTextBoxColumn.HeaderText = "payloadType";
            this.payloadTypeDataGridViewTextBoxColumn.Name = "payloadTypeDataGridViewTextBoxColumn";
            // 
            // compresstypeDataGridViewTextBoxColumn
            // 
            this.compresstypeDataGridViewTextBoxColumn.DataPropertyName = "compress_type";
            this.compresstypeDataGridViewTextBoxColumn.HeaderText = "compress_type";
            this.compresstypeDataGridViewTextBoxColumn.Name = "compresstypeDataGridViewTextBoxColumn";
            // 
            // imcursorDataGridViewTextBoxColumn
            // 
            this.imcursorDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.imcursorDataGridViewTextBoxColumn.DataPropertyName = "im_cursor";
            this.imcursorDataGridViewTextBoxColumn.HeaderText = "im_cursor";
            this.imcursorDataGridViewTextBoxColumn.Name = "imcursorDataGridViewTextBoxColumn";
            // 
            // regLogBindingSource
            // 
            this.regLogBindingSource.DataSource = typeof(ePQTiktokLive.RegLog);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(18, 35);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(404, 86);
            this.txtResult.TabIndex = 23;
            // 
            // frm_TiktokLive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 736);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.panel1);
            this.Name = "frm_TiktokLive";
            this.Text = "frm_TiktokLive";
            this.Load += new System.EventHandler(this.frm_TiktokLive_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.regLogBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtTiktoklive;
        private System.Windows.Forms.Label RoomID;
        private System.Windows.Forms.TextBox txtRomID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTTUser;
        private System.Windows.Forms.TextBox txtWebSocketUrl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.BindingSource regLogBindingSource;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.TextBox txtPayload;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtLogID;
        private System.Windows.Forms.DataGridViewTextBoxColumn logId;
        private System.Windows.Forms.DataGridViewTextBoxColumn payloadEncodingDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn payloadTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn compresstypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn imcursorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn payload;
        private System.Windows.Forms.TextBox txtResult;
    }
}