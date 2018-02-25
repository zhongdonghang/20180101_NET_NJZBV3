namespace SeatClient
{
    partial class FrmShowEnterOutLog
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
            this.btnReturn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCardNo = new System.Windows.Forms.Label();
            this.lblSeatNo = new System.Windows.Forms.Label();
            this.lblReadingRoomName = new System.Windows.Forms.Label();
            this.lblNowState = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.Transparent;
            this.btnReturn.FlatAppearance.BorderSize = 0;
            this.btnReturn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Location = new System.Drawing.Point(24, 675);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(104, 54);
            this.btnReturn.TabIndex = 108;
            this.btnReturn.TabStop = false;
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturnWaiting_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(935, 82);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 113;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(807, 55);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(136, 21);
            this.textBox1.TabIndex = 114;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.panel1.Location = new System.Drawing.Point(17, 217);
            this.panel1.Name = "panel1";
            this.panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel1.Size = new System.Drawing.Size(992, 650);
            this.panel1.TabIndex = 115;
            // 
            // lblCardNo
            // 
            this.lblCardNo.BackColor = System.Drawing.Color.Transparent;
            this.lblCardNo.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCardNo.ForeColor = System.Drawing.Color.Black;
            this.lblCardNo.Location = new System.Drawing.Point(297, 127);
            this.lblCardNo.Name = "lblCardNo";
            this.lblCardNo.Size = new System.Drawing.Size(186, 29);
            this.lblCardNo.TabIndex = 116;
            this.lblCardNo.Text = "请刷卡！";
            // 
            // lblSeatNo
            // 
            this.lblSeatNo.BackColor = System.Drawing.Color.Transparent;
            this.lblSeatNo.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSeatNo.ForeColor = System.Drawing.Color.Black;
            this.lblSeatNo.Location = new System.Drawing.Point(297, 179);
            this.lblSeatNo.Name = "lblSeatNo";
            this.lblSeatNo.Size = new System.Drawing.Size(186, 34);
            this.lblSeatNo.TabIndex = 117;
            // 
            // lblReadingRoomName
            // 
            this.lblReadingRoomName.BackColor = System.Drawing.Color.Transparent;
            this.lblReadingRoomName.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblReadingRoomName.ForeColor = System.Drawing.Color.Black;
            this.lblReadingRoomName.Location = new System.Drawing.Point(628, 128);
            this.lblReadingRoomName.Name = "lblReadingRoomName";
            this.lblReadingRoomName.Size = new System.Drawing.Size(207, 30);
            this.lblReadingRoomName.TabIndex = 118;
            // 
            // lblNowState
            // 
            this.lblNowState.BackColor = System.Drawing.Color.Transparent;
            this.lblNowState.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNowState.ForeColor = System.Drawing.Color.Black;
            this.lblNowState.Location = new System.Drawing.Point(628, 180);
            this.lblNowState.Name = "lblNowState";
            this.lblNowState.Size = new System.Drawing.Size(123, 34);
            this.lblNowState.TabIndex = 119;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 45F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(4)))));
            this.label1.Location = new System.Drawing.Point(448, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 59);
            this.label1.TabIndex = 120;
            this.label1.Text = "50";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmShowEnterOutLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblNowState);
            this.Controls.Add(this.lblReadingRoomName);
            this.Controls.Add(this.lblSeatNo);
            this.Controls.Add(this.lblCardNo);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnReturn);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmShowEnterOutLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FrmShowEnterOutLog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmShowEnterOutLog_FormClosing);
            this.Load += new System.EventHandler(this.FrmShowEnterOutLog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCardNo;
        private System.Windows.Forms.Label lblSeatNo;
        private System.Windows.Forms.Label lblReadingRoomName;
        private System.Windows.Forms.Label lblNowState;
        private System.Windows.Forms.Label label1;
    }
}