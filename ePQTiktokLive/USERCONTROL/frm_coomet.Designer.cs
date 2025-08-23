namespace ePQTiktokLive.USERCONTROL
{
    partial class frm_coomet
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
            this.commentsPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // commentsPanel
            // 
            this.commentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commentsPanel.Location = new System.Drawing.Point(0, 0);
            this.commentsPanel.Name = "commentsPanel";
            this.commentsPanel.Size = new System.Drawing.Size(800, 450);
            this.commentsPanel.TabIndex = 0;
            // 
            // frm_coomet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.commentsPanel);
            this.Name = "frm_coomet";
            this.Text = "frm_coomet";
            this.Load += new System.EventHandler(this.frm_coomet_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel commentsPanel;
    }
}