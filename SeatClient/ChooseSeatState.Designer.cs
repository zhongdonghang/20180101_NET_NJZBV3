namespace SeatClient
{
    partial class ChooseSeatState
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button41 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnManualMode = new System.Windows.Forms.Label();
            this.btnAutomaticMode = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            // 
            // button41
            // 
            this.button41.BackColor = System.Drawing.Color.Transparent;
            this.button41.FlatAppearance.BorderSize = 0;
            this.button41.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button41.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button41.Location = new System.Drawing.Point(81, 664);
            this.button41.Name = "button41";
            this.button41.Size = new System.Drawing.Size(146, 44);
            this.button41.TabIndex = 107;
            this.button41.TabStop = false;
            this.button41.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = global::SeatClient.Properties.Resources.btnClose1;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(453, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 24);
            this.button1.TabIndex = 7;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnManualMode
            // 
            this.btnManualMode.BackColor = System.Drawing.Color.Transparent;
            this.btnManualMode.Image = global::SeatClient.Properties.Resources.btn_sdxz;
            this.btnManualMode.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnManualMode.Location = new System.Drawing.Point(314, 73);
            this.btnManualMode.Name = "btnManualMode";
            this.btnManualMode.Size = new System.Drawing.Size(115, 186);
            this.btnManualMode.TabIndex = 108;
            this.btnManualMode.Click += new System.EventHandler(this.btnManualMode_Click);
            // 
            // btnAutomaticMode
            // 
            this.btnAutomaticMode.BackColor = System.Drawing.Color.Transparent;
            this.btnAutomaticMode.Image = global::SeatClient.Properties.Resources.btn_zdxz;
            this.btnAutomaticMode.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAutomaticMode.Location = new System.Drawing.Point(65, 73);
            this.btnAutomaticMode.Name = "btnAutomaticMode";
            this.btnAutomaticMode.Size = new System.Drawing.Size(115, 186);
            this.btnAutomaticMode.TabIndex = 109;
            this.btnAutomaticMode.Click += new System.EventHandler(this.btnAutomaticMode_Click);
            // 
            // ChooseSeatState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SeatClient.Properties.Resources.img_main_zwxzfs;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(490, 288);
            this.ControlBox = false;
            this.Controls.Add(this.btnAutomaticMode);
            this.Controls.Add(this.btnManualMode);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button41);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseSeatState";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ChooseSeatState";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChooseSeatState_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button41;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label btnManualMode;
        private System.Windows.Forms.Label btnAutomaticMode;
    }
}