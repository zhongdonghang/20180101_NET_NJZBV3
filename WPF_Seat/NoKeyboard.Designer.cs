namespace WPF_Seat
{
    partial class NoKeyboard
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoKeyboard));
            this.btnBack = new System.Windows.Forms.Button();
            this.txtSeatNo = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.myKeyboard = new SeatManage.Keyboards.UC_Keyboard();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.BackgroundImage = global::WPF_Seat.Properties.Resources.btn_keybord_r1_c7;
            this.btnBack.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBack.Location = new System.Drawing.Point(461, 65);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(54, 40);
            this.btnBack.TabIndex = 25;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // txtSeatNo
            // 
            this.txtSeatNo.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSeatNo.Location = new System.Drawing.Point(149, 68);
            this.txtSeatNo.Name = "txtSeatNo";
            this.txtSeatNo.Size = new System.Drawing.Size(302, 35);
            this.txtSeatNo.TabIndex = 27;
            this.txtSeatNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.txtSeatNo, "请输入座位号最后四位");
            this.txtSeatNo.TextChanged += new System.EventHandler(this.txtSeatNo_TextChanged);
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::WPF_Seat.Properties.Resources.btnClose1;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Location = new System.Drawing.Point(505, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 35);
            this.button1.TabIndex = 30;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // myKeyboard
            // 
            this.myKeyboard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("myKeyboard.BackgroundImage")));
            this.myKeyboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.myKeyboard.Location = new System.Drawing.Point(3, 127);
            this.myKeyboard.Name = "myKeyboard";
            this.myKeyboard.Size = new System.Drawing.Size(548, 218);
            this.myKeyboard.TabIndex = 31;
            this.myKeyboard.MyKeyDown += new SeatManage.Keyboards.EventHandlerSubmit(this.myKeyboard_MyKeyDown);
            this.myKeyboard.MyEnter += new System.EventHandler(this.myKeyboard_MyEnter);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::WPF_Seat.Properties.Resources.seatNumLable;
            this.pictureBox1.Location = new System.Drawing.Point(30, 50);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(118, 63);
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            // 
            // NoKeyboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WPF_Seat.Properties.Resources.keyboardBackimage;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(554, 348);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtSeatNo);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.myKeyboard);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NoKeyboard";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NoKeyboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NoKeyboard_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private SeatManage.Keyboards.UC_Keyboard myKeyboard;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TextBox txtSeatNo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}