namespace SeatManage.MyUserControl
{
    partial class ReadingRoomButton
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.roomName = new System.Windows.Forms.Label();
            this.roomUsedTip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // roomName
            // 
            this.roomName.BackColor = System.Drawing.Color.Transparent;
            this.roomName.Font = new System.Drawing.Font("微软雅黑", 23F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.roomName.ForeColor = System.Drawing.Color.White;
            this.roomName.Location = new System.Drawing.Point(10, 11);
            this.roomName.Name = "roomName";
            this.roomName.Size = new System.Drawing.Size(179, 63);
            this.roomName.TabIndex = 0;
            this.roomName.Text = "阅览室名称";
            this.roomName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.roomName.Click += new System.EventHandler(this.roomName_Click);
            // 
            // roomUsedTip
            // 
            this.roomUsedTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.roomUsedTip.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.roomUsedTip.ForeColor = System.Drawing.Color.White;
            this.roomUsedTip.Location = new System.Drawing.Point(5, 82);
            this.roomUsedTip.Name = "roomUsedTip";
            this.roomUsedTip.Size = new System.Drawing.Size(184, 31);
            this.roomUsedTip.TabIndex = 1;
            this.roomUsedTip.Text = "座位使用情况";
            this.roomUsedTip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.roomUsedTip.Click += new System.EventHandler(this.roomName_Click);
            // 
            // ReadingRoomButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::SeatManage.Properties.Resources.gray;
            this.Controls.Add(this.roomUsedTip);
            this.Controls.Add(this.roomName);
            this.Name = "ReadingRoomButton";
            this.Size = new System.Drawing.Size(200, 131);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label roomName;
        private System.Windows.Forms.Label roomUsedTip;
    }
}
