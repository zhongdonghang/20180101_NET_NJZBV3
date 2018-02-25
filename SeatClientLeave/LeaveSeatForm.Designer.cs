namespace SeatClientLeave
{
    partial class LeaveSeatForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.btnShortLeave = new System.Windows.Forms.Button();
            this.btnForeverLeave = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnTimeDelay = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitleAd = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 22F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(168, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "请您选择离开方式：";
            // 
            // btnShortLeave
            // 
            this.btnShortLeave.BackColor = System.Drawing.Color.Transparent;
            this.btnShortLeave.BackgroundImage = global::SeatClientLeave.Properties.Resources.btn_leave;
            this.btnShortLeave.FlatAppearance.BorderSize = 0;
            this.btnShortLeave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShortLeave.ForeColor = System.Drawing.Color.Transparent;
            this.btnShortLeave.Location = new System.Drawing.Point(270, 221);
            this.btnShortLeave.Name = "btnShortLeave";
            this.btnShortLeave.Size = new System.Drawing.Size(81, 54);
            this.btnShortLeave.TabIndex = 1;
            this.btnShortLeave.TabStop = false;
            this.btnShortLeave.UseVisualStyleBackColor = false;
            this.btnShortLeave.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnForeverLeave
            // 
            this.btnForeverLeave.BackColor = System.Drawing.Color.Transparent;
            this.btnForeverLeave.BackgroundImage = global::SeatClientLeave.Properties.Resources.btn_reset;
            this.btnForeverLeave.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnForeverLeave.FlatAppearance.BorderSize = 0;
            this.btnForeverLeave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForeverLeave.Location = new System.Drawing.Point(362, 221);
            this.btnForeverLeave.Name = "btnForeverLeave";
            this.btnForeverLeave.Size = new System.Drawing.Size(118, 54);
            this.btnForeverLeave.TabIndex = 2;
            this.btnForeverLeave.TabStop = false;
            this.btnForeverLeave.UseVisualStyleBackColor = false;
            this.btnForeverLeave.Click += new System.EventHandler(this.button2_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            // 
            // btnTimeDelay
            // 
            this.btnTimeDelay.BackColor = System.Drawing.Color.Transparent;
            this.btnTimeDelay.BackgroundImage = global::SeatClientLeave.Properties.Resources.btn_xs;
            this.btnTimeDelay.FlatAppearance.BorderSize = 0;
            this.btnTimeDelay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimeDelay.Location = new System.Drawing.Point(175, 221);
            this.btnTimeDelay.Name = "btnTimeDelay";
            this.btnTimeDelay.Size = new System.Drawing.Size(81, 54);
            this.btnTimeDelay.TabIndex = 4;
            this.btnTimeDelay.TabStop = false;
            this.btnTimeDelay.UseVisualStyleBackColor = false;
            this.btnTimeDelay.Visible = false;
            this.btnTimeDelay.Click += new System.EventHandler(this.btnTimeDelay_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = global::SeatClientLeave.Properties.Resources.btnClose1;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(453, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 24);
            this.button1.TabIndex = 7;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::SeatClientLeave.Properties.Resources.question;
            this.pictureBox1.Location = new System.Drawing.Point(47, 73);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(60, 126);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // lblTitleAd
            // 
            this.lblTitleAd.BackColor = System.Drawing.Color.Transparent;
            this.lblTitleAd.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitleAd.ForeColor = System.Drawing.Color.FloralWhite;
            this.lblTitleAd.Location = new System.Drawing.Point(12, 10);
            this.lblTitleAd.Name = "lblTitleAd";
            this.lblTitleAd.Size = new System.Drawing.Size(435, 32);
            this.lblTitleAd.TabIndex = 24;
            this.lblTitleAd.Text = "中国电信友情提醒";
            // 
            // LeaveSeatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SeatClientLeave.Properties.Resources.img_bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(490, 288);
            this.Controls.Add(this.lblTitleAd);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnTimeDelay);
            this.Controls.Add(this.btnForeverLeave);
            this.Controls.Add(this.btnShortLeave);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LeaveSeatForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "LeaveSeatForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LeaveSeatForm_FormClosing);
            this.Load += new System.EventHandler(this.LeaveSeatForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnShortLeave;
        private System.Windows.Forms.Button btnForeverLeave;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnTimeDelay;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitleAd;
    }
}