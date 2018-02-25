namespace 接口授权工具
{
    partial class FunctionAuthorization
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
            System.Windows.Forms.Button btn_Close;
            this.txt_SchoolNum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_Client_ShowReaderInfo = new System.Windows.Forms.CheckBox();
            this.cb_Media_TitleAd = new System.Windows.Forms.CheckBox();
            this.cb_Client_ShowLastSeat = new System.Windows.Forms.CheckBox();
            this.cb_Client_SeachBlasklist = new System.Windows.Forms.CheckBox();
            this.cb_Client_SeachViolation = new System.Windows.Forms.CheckBox();
            this.cb_MoveClient_AdminManage = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_Media_MediaPlayer = new System.Windows.Forms.CheckBox();
            this.cb_Media_SchoolNotice = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_Bespeak_NowDay = new System.Windows.Forms.CheckBox();
            this.cb_Bespeak_AppointTime = new System.Windows.Forms.CheckBox();
            this.cb_Client_SeachBespeak = new System.Windows.Forms.CheckBox();
            this.cb_Client_ShowReaderMeg = new System.Windows.Forms.CheckBox();
            this.cb_LimitTime_SpanMode = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_Media_PopAd = new System.Windows.Forms.CheckBox();
            this.cb_MoveClient_ContinueTime = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_MoveClient_SeatSelect = new System.Windows.Forms.CheckBox();
            this.cb_MoveClient_QRcodeDecode = new System.Windows.Forms.CheckBox();
            this.cb_MoveClient_SeatWait = new System.Windows.Forms.CheckBox();
            this.cb_24HModel = new System.Windows.Forms.CheckBox();
            this.cb_SendMsg = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            btn_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            btn_Close.Location = new System.Drawing.Point(654, 295);
            btn_Close.Name = "btn_Close";
            btn_Close.Size = new System.Drawing.Size(75, 23);
            btn_Close.TabIndex = 32;
            btn_Close.Text = "取消";
            btn_Close.UseVisualStyleBackColor = true;
            btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // txt_SchoolNum
            // 
            this.txt_SchoolNum.Location = new System.Drawing.Point(84, 10);
            this.txt_SchoolNum.Name = "txt_SchoolNum";
            this.txt_SchoolNum.Size = new System.Drawing.Size(117, 21);
            this.txt_SchoolNum.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "学校编号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "触摸屏终端：";
            // 
            // cb_Client_ShowReaderInfo
            // 
            this.cb_Client_ShowReaderInfo.AutoSize = true;
            this.cb_Client_ShowReaderInfo.Location = new System.Drawing.Point(15, 74);
            this.cb_Client_ShowReaderInfo.Name = "cb_Client_ShowReaderInfo";
            this.cb_Client_ShowReaderInfo.Size = new System.Drawing.Size(204, 16);
            this.cb_Client_ShowReaderInfo.TabIndex = 3;
            this.cb_Client_ShowReaderInfo.Text = "显示刷卡用户信息（阅览室界面）";
            this.cb_Client_ShowReaderInfo.UseVisualStyleBackColor = true;
            // 
            // cb_Media_TitleAd
            // 
            this.cb_Media_TitleAd.AutoSize = true;
            this.cb_Media_TitleAd.Location = new System.Drawing.Point(561, 116);
            this.cb_Media_TitleAd.Name = "cb_Media_TitleAd";
            this.cb_Media_TitleAd.Size = new System.Drawing.Size(168, 16);
            this.cb_Media_TitleAd.TabIndex = 3;
            this.cb_Media_TitleAd.Text = "窗口标题（冠名广告部分）";
            this.cb_Media_TitleAd.UseVisualStyleBackColor = true;
            // 
            // cb_Client_ShowLastSeat
            // 
            this.cb_Client_ShowLastSeat.AutoSize = true;
            this.cb_Client_ShowLastSeat.Location = new System.Drawing.Point(15, 96);
            this.cb_Client_ShowLastSeat.Name = "cb_Client_ShowLastSeat";
            this.cb_Client_ShowLastSeat.Size = new System.Drawing.Size(120, 16);
            this.cb_Client_ShowLastSeat.TabIndex = 3;
            this.cb_Client_ShowLastSeat.Text = "实时剩余座位信息";
            this.cb_Client_ShowLastSeat.UseVisualStyleBackColor = true;
            // 
            // cb_Client_SeachBlasklist
            // 
            this.cb_Client_SeachBlasklist.AutoSize = true;
            this.cb_Client_SeachBlasklist.Location = new System.Drawing.Point(15, 118);
            this.cb_Client_SeachBlasklist.Name = "cb_Client_SeachBlasklist";
            this.cb_Client_SeachBlasklist.Size = new System.Drawing.Size(108, 16);
            this.cb_Client_SeachBlasklist.TabIndex = 3;
            this.cb_Client_SeachBlasklist.Text = "黑名单记录查询";
            this.cb_Client_SeachBlasklist.UseVisualStyleBackColor = true;
            // 
            // cb_Client_SeachViolation
            // 
            this.cb_Client_SeachViolation.AutoSize = true;
            this.cb_Client_SeachViolation.Location = new System.Drawing.Point(15, 140);
            this.cb_Client_SeachViolation.Name = "cb_Client_SeachViolation";
            this.cb_Client_SeachViolation.Size = new System.Drawing.Size(96, 16);
            this.cb_Client_SeachViolation.TabIndex = 3;
            this.cb_Client_SeachViolation.Text = "违规记录查询";
            this.cb_Client_SeachViolation.UseVisualStyleBackColor = true;
            // 
            // cb_MoveClient_AdminManage
            // 
            this.cb_MoveClient_AdminManage.AutoSize = true;
            this.cb_MoveClient_AdminManage.Location = new System.Drawing.Point(293, 69);
            this.cb_MoveClient_AdminManage.Name = "cb_MoveClient_AdminManage";
            this.cb_MoveClient_AdminManage.Size = new System.Drawing.Size(228, 16);
            this.cb_MoveClient_AdminManage.TabIndex = 12;
            this.cb_MoveClient_AdminManage.Text = "座位管理功能（移动平台管理员功能）";
            this.cb_MoveClient_AdminManage.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(559, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "校园通知与媒体播放：";
            // 
            // cb_Media_MediaPlayer
            // 
            this.cb_Media_MediaPlayer.AutoSize = true;
            this.cb_Media_MediaPlayer.Location = new System.Drawing.Point(561, 71);
            this.cb_Media_MediaPlayer.Name = "cb_Media_MediaPlayer";
            this.cb_Media_MediaPlayer.Size = new System.Drawing.Size(108, 16);
            this.cb_Media_MediaPlayer.TabIndex = 3;
            this.cb_Media_MediaPlayer.Text = "大屏媒体播放器";
            this.cb_Media_MediaPlayer.UseVisualStyleBackColor = true;
            // 
            // cb_Media_SchoolNotice
            // 
            this.cb_Media_SchoolNotice.AutoSize = true;
            this.cb_Media_SchoolNotice.Location = new System.Drawing.Point(561, 94);
            this.cb_Media_SchoolNotice.Name = "cb_Media_SchoolNotice";
            this.cb_Media_SchoolNotice.Size = new System.Drawing.Size(96, 16);
            this.cb_Media_SchoolNotice.TabIndex = 3;
            this.cb_Media_SchoolNotice.Text = "校园通知公告";
            this.cb_Media_SchoolNotice.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 229);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "座位预约功能：";
            // 
            // cb_Bespeak_NowDay
            // 
            this.cb_Bespeak_NowDay.AutoSize = true;
            this.cb_Bespeak_NowDay.Location = new System.Drawing.Point(14, 253);
            this.cb_Bespeak_NowDay.Name = "cb_Bespeak_NowDay";
            this.cb_Bespeak_NowDay.Size = new System.Drawing.Size(120, 16);
            this.cb_Bespeak_NowDay.TabIndex = 15;
            this.cb_Bespeak_NowDay.Text = "预约当天座位功能";
            this.cb_Bespeak_NowDay.UseVisualStyleBackColor = true;
            // 
            // cb_Bespeak_AppointTime
            // 
            this.cb_Bespeak_AppointTime.AutoSize = true;
            this.cb_Bespeak_AppointTime.Location = new System.Drawing.Point(14, 275);
            this.cb_Bespeak_AppointTime.Name = "cb_Bespeak_AppointTime";
            this.cb_Bespeak_AppointTime.Size = new System.Drawing.Size(120, 16);
            this.cb_Bespeak_AppointTime.TabIndex = 17;
            this.cb_Bespeak_AppointTime.Text = "指定时间预约功能";
            this.cb_Bespeak_AppointTime.UseVisualStyleBackColor = true;
            // 
            // cb_Client_SeachBespeak
            // 
            this.cb_Client_SeachBespeak.AutoSize = true;
            this.cb_Client_SeachBespeak.Location = new System.Drawing.Point(15, 162);
            this.cb_Client_SeachBespeak.Name = "cb_Client_SeachBespeak";
            this.cb_Client_SeachBespeak.Size = new System.Drawing.Size(96, 16);
            this.cb_Client_SeachBespeak.TabIndex = 3;
            this.cb_Client_SeachBespeak.Text = "预约记录查询";
            this.cb_Client_SeachBespeak.UseVisualStyleBackColor = true;
            // 
            // cb_Client_ShowReaderMeg
            // 
            this.cb_Client_ShowReaderMeg.AutoSize = true;
            this.cb_Client_ShowReaderMeg.Location = new System.Drawing.Point(15, 184);
            this.cb_Client_ShowReaderMeg.Name = "cb_Client_ShowReaderMeg";
            this.cb_Client_ShowReaderMeg.Size = new System.Drawing.Size(252, 16);
            this.cb_Client_ShowReaderMeg.TabIndex = 19;
            this.cb_Client_ShowReaderMeg.Text = "读者违规系统提示（刷卡时弹出提示系统）";
            this.cb_Client_ShowReaderMeg.UseVisualStyleBackColor = true;
            // 
            // cb_LimitTime_SpanMode
            // 
            this.cb_LimitTime_SpanMode.AutoSize = true;
            this.cb_LimitTime_SpanMode.Location = new System.Drawing.Point(293, 253);
            this.cb_LimitTime_SpanMode.Name = "cb_LimitTime_SpanMode";
            this.cb_LimitTime_SpanMode.Size = new System.Drawing.Size(108, 16);
            this.cb_LimitTime_SpanMode.TabIndex = 21;
            this.cb_LimitTime_SpanMode.Text = "按时段计时功能";
            this.cb_LimitTime_SpanMode.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(291, 229);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "座位计时功能：";
            // 
            // cb_Media_PopAd
            // 
            this.cb_Media_PopAd.AutoSize = true;
            this.cb_Media_PopAd.Location = new System.Drawing.Point(561, 138);
            this.cb_Media_PopAd.Name = "cb_Media_PopAd";
            this.cb_Media_PopAd.Size = new System.Drawing.Size(168, 16);
            this.cb_Media_PopAd.TabIndex = 22;
            this.cb_Media_PopAd.Text = "窗口图片（弹窗广告部分）";
            this.cb_Media_PopAd.UseVisualStyleBackColor = true;
            // 
            // cb_MoveClient_ContinueTime
            // 
            this.cb_MoveClient_ContinueTime.AutoSize = true;
            this.cb_MoveClient_ContinueTime.Location = new System.Drawing.Point(293, 91);
            this.cb_MoveClient_ContinueTime.Name = "cb_MoveClient_ContinueTime";
            this.cb_MoveClient_ContinueTime.Size = new System.Drawing.Size(96, 16);
            this.cb_MoveClient_ContinueTime.TabIndex = 23;
            this.cb_MoveClient_ContinueTime.Text = "座位续时功能";
            this.cb_MoveClient_ContinueTime.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(291, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "移动终端功能：";
            // 
            // cb_MoveClient_SeatSelect
            // 
            this.cb_MoveClient_SeatSelect.AutoSize = true;
            this.cb_MoveClient_SeatSelect.Location = new System.Drawing.Point(293, 135);
            this.cb_MoveClient_SeatSelect.Name = "cb_MoveClient_SeatSelect";
            this.cb_MoveClient_SeatSelect.Size = new System.Drawing.Size(168, 16);
            this.cb_MoveClient_SeatSelect.TabIndex = 26;
            this.cb_MoveClient_SeatSelect.Text = "座位选座功能（限二维码）";
            this.cb_MoveClient_SeatSelect.UseVisualStyleBackColor = true;
            // 
            // cb_MoveClient_QRcodeDecode
            // 
            this.cb_MoveClient_QRcodeDecode.AutoSize = true;
            this.cb_MoveClient_QRcodeDecode.Location = new System.Drawing.Point(293, 113);
            this.cb_MoveClient_QRcodeDecode.Name = "cb_MoveClient_QRcodeDecode";
            this.cb_MoveClient_QRcodeDecode.Size = new System.Drawing.Size(108, 16);
            this.cb_MoveClient_QRcodeDecode.TabIndex = 25;
            this.cb_MoveClient_QRcodeDecode.Text = "二维码扫描功能";
            this.cb_MoveClient_QRcodeDecode.UseVisualStyleBackColor = true;
            // 
            // cb_MoveClient_SeatWait
            // 
            this.cb_MoveClient_SeatWait.AutoSize = true;
            this.cb_MoveClient_SeatWait.Location = new System.Drawing.Point(293, 157);
            this.cb_MoveClient_SeatWait.Name = "cb_MoveClient_SeatWait";
            this.cb_MoveClient_SeatWait.Size = new System.Drawing.Size(168, 16);
            this.cb_MoveClient_SeatWait.TabIndex = 28;
            this.cb_MoveClient_SeatWait.Text = "座位等待功能（限二维码）";
            this.cb_MoveClient_SeatWait.UseVisualStyleBackColor = true;
            // 
            // cb_24HModel
            // 
            this.cb_24HModel.AutoSize = true;
            this.cb_24HModel.Location = new System.Drawing.Point(561, 273);
            this.cb_24HModel.Name = "cb_24HModel";
            this.cb_24HModel.Size = new System.Drawing.Size(84, 16);
            this.cb_24HModel.TabIndex = 27;
            this.cb_24HModel.Text = "24小时模式";
            this.cb_24HModel.UseVisualStyleBackColor = true;
            // 
            // cb_SendMsg
            // 
            this.cb_SendMsg.AutoSize = true;
            this.cb_SendMsg.Location = new System.Drawing.Point(561, 253);
            this.cb_SendMsg.Name = "cb_SendMsg";
            this.cb_SendMsg.Size = new System.Drawing.Size(72, 16);
            this.cb_SendMsg.TabIndex = 29;
            this.cb_SendMsg.Text = "消息推送";
            this.cb_SendMsg.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(559, 229);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 30;
            this.label3.Text = "其他：";
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(573, 295);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 31;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // FunctionAuthorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 330);
            this.Controls.Add(btn_Close);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cb_SendMsg);
            this.Controls.Add(this.cb_MoveClient_SeatWait);
            this.Controls.Add(this.cb_24HModel);
            this.Controls.Add(this.cb_MoveClient_SeatSelect);
            this.Controls.Add(this.cb_MoveClient_QRcodeDecode);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cb_MoveClient_ContinueTime);
            this.Controls.Add(this.cb_Media_PopAd);
            this.Controls.Add(this.cb_LimitTime_SpanMode);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cb_Client_ShowReaderMeg);
            this.Controls.Add(this.cb_Bespeak_AppointTime);
            this.Controls.Add(this.cb_Bespeak_NowDay);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cb_MoveClient_AdminManage);
            this.Controls.Add(this.cb_Client_SeachBespeak);
            this.Controls.Add(this.cb_Client_SeachViolation);
            this.Controls.Add(this.cb_Client_SeachBlasklist);
            this.Controls.Add(this.cb_Client_ShowLastSeat);
            this.Controls.Add(this.cb_Media_MediaPlayer);
            this.Controls.Add(this.cb_Media_TitleAd);
            this.Controls.Add(this.cb_Media_SchoolNotice);
            this.Controls.Add(this.cb_Client_ShowReaderInfo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_SchoolNum);
            this.Name = "FunctionAuthorization";
            this.Text = "功能配置页";
            this.Load += new System.EventHandler(this.FunctionAuthorization_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_SchoolNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cb_Client_ShowReaderInfo;
        private System.Windows.Forms.CheckBox cb_Media_TitleAd;
        private System.Windows.Forms.CheckBox cb_Client_ShowLastSeat;
        private System.Windows.Forms.CheckBox cb_Client_SeachBlasklist;
        private System.Windows.Forms.CheckBox cb_Client_SeachViolation;
        private System.Windows.Forms.CheckBox cb_MoveClient_AdminManage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cb_Media_MediaPlayer;
        private System.Windows.Forms.CheckBox cb_Media_SchoolNotice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cb_Bespeak_NowDay;
        private System.Windows.Forms.CheckBox cb_Bespeak_AppointTime;
        private System.Windows.Forms.CheckBox cb_Client_SeachBespeak;
        private System.Windows.Forms.CheckBox cb_Client_ShowReaderMeg;
        private System.Windows.Forms.CheckBox cb_LimitTime_SpanMode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cb_Media_PopAd;
        private System.Windows.Forms.CheckBox cb_MoveClient_ContinueTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cb_MoveClient_SeatSelect;
        private System.Windows.Forms.CheckBox cb_MoveClient_QRcodeDecode;
        private System.Windows.Forms.CheckBox cb_MoveClient_SeatWait;
        private System.Windows.Forms.CheckBox cb_24HModel;
        private System.Windows.Forms.CheckBox cb_SendMsg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Save;
    }
}