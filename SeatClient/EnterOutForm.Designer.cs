namespace SeatClient
{
    partial class EnterOutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnterOutForm));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.labnotice = new System.Windows.Forms.Label();
            this.btnQureyLog = new System.Windows.Forms.Button();
            this.BookActivation = new System.Windows.Forms.Button();
            this.btnResetPOS = new System.Windows.Forms.Button();
            this.picBoxPartnersLogo = new System.Windows.Forms.PictureBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPartnersLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(379, 340);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 5;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(485, 340);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "START";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // labnotice
            // 
            this.labnotice.BackColor = System.Drawing.Color.Transparent;
            this.labnotice.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labnotice.ForeColor = System.Drawing.Color.Red;
            this.labnotice.Location = new System.Drawing.Point(719, 627);
            this.labnotice.Name = "labnotice";
            this.labnotice.Size = new System.Drawing.Size(264, 38);
            this.labnotice.TabIndex = 8;
            this.labnotice.Text = "暂时无法提供座位凭条";
            this.labnotice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labnotice.Visible = false;
            // 
            // btnQureyLog
            // 
            this.btnQureyLog.BackColor = System.Drawing.Color.Transparent;
            this.btnQureyLog.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQureyLog.BackgroundImage")));
            this.btnQureyLog.FlatAppearance.BorderSize = 0;
            this.btnQureyLog.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnQureyLog.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnQureyLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQureyLog.Location = new System.Drawing.Point(545, 686);
            this.btnQureyLog.Name = "btnQureyLog";
            this.btnQureyLog.Size = new System.Drawing.Size(137, 54);
            this.btnQureyLog.TabIndex = 110;
            this.btnQureyLog.TabStop = false;
            this.btnQureyLog.UseVisualStyleBackColor = true;
            this.btnQureyLog.Click += new System.EventHandler(this.btnQureyLog_Click);
            // 
            // BookActivation
            // 
            this.BookActivation.BackColor = System.Drawing.Color.Transparent;
            this.BookActivation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BookActivation.BackgroundImage")));
            this.BookActivation.FlatAppearance.BorderSize = 0;
            this.BookActivation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BookActivation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BookActivation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BookActivation.Location = new System.Drawing.Point(312, 686);
            this.BookActivation.Name = "BookActivation";
            this.BookActivation.Size = new System.Drawing.Size(137, 54);
            this.BookActivation.TabIndex = 111;
            this.BookActivation.TabStop = false;
            this.BookActivation.UseVisualStyleBackColor = true;
            this.BookActivation.Click += new System.EventHandler(this.BookActivation_Click);
            // 
            // btnResetPOS
            // 
            this.btnResetPOS.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnResetPOS.Location = new System.Drawing.Point(862, 7);
            this.btnResetPOS.Name = "btnResetPOS";
            this.btnResetPOS.Size = new System.Drawing.Size(110, 50);
            this.btnResetPOS.TabIndex = 113;
            this.btnResetPOS.Text = "刷卡不响应？点击这里";
            this.btnResetPOS.UseVisualStyleBackColor = true;
            this.btnResetPOS.Click += new System.EventHandler(this.btnResetPOS_Click);
            // 
            // picBoxPartnersLogo
            // 
            this.picBoxPartnersLogo.BackColor = System.Drawing.Color.Transparent;
            this.picBoxPartnersLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picBoxPartnersLogo.ErrorImage = null;
            this.picBoxPartnersLogo.Location = new System.Drawing.Point(0, 225);
            this.picBoxPartnersLogo.Name = "picBoxPartnersLogo";
            this.picBoxPartnersLogo.Size = new System.Drawing.Size(1075, 520);
            this.picBoxPartnersLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxPartnersLogo.TabIndex = 114;
            this.picBoxPartnersLogo.TabStop = false;
            this.picBoxPartnersLogo.Click += new System.EventHandler(this.picBoxPartnersLogo_Click);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lblDate.ForeColor = System.Drawing.Color.White;
            this.lblDate.Location = new System.Drawing.Point(387, 15);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(36, 27);
            this.lblDate.TabIndex = 115;
            this.lblDate.Text = "    ";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(614, 15);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(30, 27);
            this.lblTime.TabIndex = 116;
            this.lblTime.Text = "   ";
            // 
            // EnterOutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.picBoxPartnersLogo);
            this.Controls.Add(this.btnResetPOS);
            this.Controls.Add(this.BookActivation);
            this.Controls.Add(this.btnQureyLog);
            this.Controls.Add(this.labnotice);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnterOutForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "图书馆座位管理系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EnterOutForm_FormClosing);
            this.Load += new System.EventHandler(this.EnterOutForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPartnersLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label labnotice;
        private System.Windows.Forms.Button btnQureyLog;
        private System.Windows.Forms.Button BookActivation;
        private System.Windows.Forms.Button btnResetPOS;
        private System.Windows.Forms.PictureBox picBoxPartnersLogo;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTime; 
        //private System.Windows.Forms.Button btnShowEnterOutLog;

         
    }
}

