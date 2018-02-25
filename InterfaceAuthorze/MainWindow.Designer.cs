namespace InterfaceAuthorze
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.gb_SeatOperationService = new System.Windows.Forms.GroupBox();
            this.cb_SeatOperationService_ComeBack = new System.Windows.Forms.CheckBox();
            this.cb_SeatOperationService_BespeakCheck = new System.Windows.Forms.CheckBox();
            this.cb_SeatOperationService_CancelBespeakLog = new System.Windows.Forms.CheckBox();
            this.cb_SeatOperationService_SubmitBespeakInfo = new System.Windows.Forms.CheckBox();
            this.cb_SeatOperationService_SubmitDelayResult = new System.Windows.Forms.CheckBox();
            this.cb_SeatOperationService_FreeSeat = new System.Windows.Forms.CheckBox();
            this.cb_SeatOperationService_ShortLeave = new System.Windows.Forms.CheckBox();
            this.cb_SeatOperationService_SeatUnLock = new System.Windows.Forms.CheckBox();
            this.cb_SeatOperationService_SeatLock = new System.Windows.Forms.CheckBox();
            this.cb_SeatOperationService_ChangeSeat = new System.Windows.Forms.CheckBox();
            this.cb_SeatOperationService_SubmitChooseResult = new System.Windows.Forms.CheckBox();
            this.cb_SeatOperationService_VerifyCanDoIt = new System.Windows.Forms.CheckBox();
            this.cb_SeatOperationService = new System.Windows.Forms.CheckBox();
            this.gb_GetSeatInfoService = new System.Windows.Forms.GroupBox();
            this.cb_GetSeatInfoService_GetQRcodeInfo = new System.Windows.Forms.CheckBox();
            this.cb_GetSeatInfoService_GetSeatUsage = new System.Windows.Forms.CheckBox();
            this.cb_GetSeatInfoService_GetSeatBespeakInfo = new System.Windows.Forms.CheckBox();
            this.cb_GetSeatInfoService = new System.Windows.Forms.CheckBox();
            this.gb_GetReadingRoomInfoService = new System.Windows.Forms.GroupBox();
            this.cb_GetReadingRoomInfoService_GetSeatsLayoutByRoomNum = new System.Windows.Forms.CheckBox();
            this.cb_GetReadingRoomInfoService_GetAllRoomSeatUsedInfo = new System.Windows.Forms.CheckBox();
            this.cb_GetReadingRoomInfoService_GetSeatsBespeakInfoByRoomNum = new System.Windows.Forms.CheckBox();
            this.cb_GetReadingRoomInfoService_GetSeatsUsedInfoByRoomNum = new System.Windows.Forms.CheckBox();
            this.cb_GetReadingRoomInfoService_GetReadingRoomSetInfoByRoomNum = new System.Windows.Forms.CheckBox();
            this.cb_GetReadingRoomInfoService_GetAllReadingRoomBaseInfo = new System.Windows.Forms.CheckBox();
            this.cb_GetReadingRoomInfoService = new System.Windows.Forms.CheckBox();
            this.gb_GetReaderInfoService = new System.Windows.Forms.GroupBox();
            this.cb_GetReaderInfoService_GetReaderAccount = new System.Windows.Forms.CheckBox();
            this.cb_GetReaderInfoService_GetReaderBlacklistRecord = new System.Windows.Forms.CheckBox();
            this.cb_GetReaderInfoService_GetViolateDiscipline = new System.Windows.Forms.CheckBox();
            this.cb_GetReaderInfoService_GetReaderChooseSeatRecord = new System.Windows.Forms.CheckBox();
            this.cb_GetReaderInfoService_GetReaderBespeakRecord = new System.Windows.Forms.CheckBox();
            this.cb_GetReaderInfoService_GetReaderActualTimeRecord = new System.Windows.Forms.CheckBox();
            this.cb_GetReaderInfoService_GetBaseReaderInfoByCardId = new System.Windows.Forms.CheckBox();
            this.cb_GetReaderInfoService_GetBaseReaderInfo = new System.Windows.Forms.CheckBox();
            this.cb_GetReaderInfoService = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_SchoolNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gb_SeatOperationService.SuspendLayout();
            this.gb_GetSeatInfoService.SuspendLayout();
            this.gb_GetReadingRoomInfoService.SuspendLayout();
            this.gb_GetReaderInfoService.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(650, 328);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 30;
            this.button2.Text = "删除";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(731, 328);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 29;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gb_SeatOperationService
            // 
            this.gb_SeatOperationService.Controls.Add(this.cb_SeatOperationService_ComeBack);
            this.gb_SeatOperationService.Controls.Add(this.cb_SeatOperationService_BespeakCheck);
            this.gb_SeatOperationService.Controls.Add(this.cb_SeatOperationService_CancelBespeakLog);
            this.gb_SeatOperationService.Controls.Add(this.cb_SeatOperationService_SubmitBespeakInfo);
            this.gb_SeatOperationService.Controls.Add(this.cb_SeatOperationService_SubmitDelayResult);
            this.gb_SeatOperationService.Controls.Add(this.cb_SeatOperationService_FreeSeat);
            this.gb_SeatOperationService.Controls.Add(this.cb_SeatOperationService_ShortLeave);
            this.gb_SeatOperationService.Controls.Add(this.cb_SeatOperationService_SeatUnLock);
            this.gb_SeatOperationService.Controls.Add(this.cb_SeatOperationService_SeatLock);
            this.gb_SeatOperationService.Controls.Add(this.cb_SeatOperationService_ChangeSeat);
            this.gb_SeatOperationService.Controls.Add(this.cb_SeatOperationService_SubmitChooseResult);
            this.gb_SeatOperationService.Controls.Add(this.cb_SeatOperationService_VerifyCanDoIt);
            this.gb_SeatOperationService.Controls.Add(this.cb_SeatOperationService);
            this.gb_SeatOperationService.Location = new System.Drawing.Point(570, 36);
            this.gb_SeatOperationService.Name = "gb_SeatOperationService";
            this.gb_SeatOperationService.Size = new System.Drawing.Size(236, 286);
            this.gb_SeatOperationService.TabIndex = 28;
            this.gb_SeatOperationService.TabStop = false;
            this.gb_SeatOperationService.Text = "座位操作类接口";
            // 
            // cb_SeatOperationService_ComeBack
            // 
            this.cb_SeatOperationService_ComeBack.AutoSize = true;
            this.cb_SeatOperationService_ComeBack.Location = new System.Drawing.Point(6, 262);
            this.cb_SeatOperationService_ComeBack.Name = "cb_SeatOperationService_ComeBack";
            this.cb_SeatOperationService_ComeBack.Size = new System.Drawing.Size(72, 16);
            this.cb_SeatOperationService_ComeBack.TabIndex = 29;
            this.cb_SeatOperationService_ComeBack.Text = "暂离回来";
            this.cb_SeatOperationService_ComeBack.UseVisualStyleBackColor = true;
            this.cb_SeatOperationService_ComeBack.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_SeatOperationService_BespeakCheck
            // 
            this.cb_SeatOperationService_BespeakCheck.AutoSize = true;
            this.cb_SeatOperationService_BespeakCheck.Location = new System.Drawing.Point(6, 240);
            this.cb_SeatOperationService_BespeakCheck.Name = "cb_SeatOperationService_BespeakCheck";
            this.cb_SeatOperationService_BespeakCheck.Size = new System.Drawing.Size(72, 16);
            this.cb_SeatOperationService_BespeakCheck.TabIndex = 28;
            this.cb_SeatOperationService_BespeakCheck.Text = "预约签到";
            this.cb_SeatOperationService_BespeakCheck.UseVisualStyleBackColor = true;
            this.cb_SeatOperationService_BespeakCheck.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_SeatOperationService_CancelBespeakLog
            // 
            this.cb_SeatOperationService_CancelBespeakLog.AutoSize = true;
            this.cb_SeatOperationService_CancelBespeakLog.Location = new System.Drawing.Point(6, 218);
            this.cb_SeatOperationService_CancelBespeakLog.Name = "cb_SeatOperationService_CancelBespeakLog";
            this.cb_SeatOperationService_CancelBespeakLog.Size = new System.Drawing.Size(108, 16);
            this.cb_SeatOperationService_CancelBespeakLog.TabIndex = 27;
            this.cb_SeatOperationService_CancelBespeakLog.Text = "取消预约的座位";
            this.cb_SeatOperationService_CancelBespeakLog.UseVisualStyleBackColor = true;
            this.cb_SeatOperationService_CancelBespeakLog.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_SeatOperationService_SubmitBespeakInfo
            // 
            this.cb_SeatOperationService_SubmitBespeakInfo.AutoSize = true;
            this.cb_SeatOperationService_SubmitBespeakInfo.Location = new System.Drawing.Point(6, 196);
            this.cb_SeatOperationService_SubmitBespeakInfo.Name = "cb_SeatOperationService_SubmitBespeakInfo";
            this.cb_SeatOperationService_SubmitBespeakInfo.Size = new System.Drawing.Size(72, 16);
            this.cb_SeatOperationService_SubmitBespeakInfo.TabIndex = 26;
            this.cb_SeatOperationService_SubmitBespeakInfo.Text = "预约座位";
            this.cb_SeatOperationService_SubmitBespeakInfo.UseVisualStyleBackColor = true;
            this.cb_SeatOperationService_SubmitBespeakInfo.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_SeatOperationService_SubmitDelayResult
            // 
            this.cb_SeatOperationService_SubmitDelayResult.AutoSize = true;
            this.cb_SeatOperationService_SubmitDelayResult.Location = new System.Drawing.Point(6, 174);
            this.cb_SeatOperationService_SubmitDelayResult.Name = "cb_SeatOperationService_SubmitDelayResult";
            this.cb_SeatOperationService_SubmitDelayResult.Size = new System.Drawing.Size(96, 16);
            this.cb_SeatOperationService_SubmitDelayResult.TabIndex = 25;
            this.cb_SeatOperationService_SubmitDelayResult.Text = "座位续时操作";
            this.cb_SeatOperationService_SubmitDelayResult.UseVisualStyleBackColor = true;
            this.cb_SeatOperationService_SubmitDelayResult.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_SeatOperationService_FreeSeat
            // 
            this.cb_SeatOperationService_FreeSeat.AutoSize = true;
            this.cb_SeatOperationService_FreeSeat.Location = new System.Drawing.Point(6, 152);
            this.cb_SeatOperationService_FreeSeat.Name = "cb_SeatOperationService_FreeSeat";
            this.cb_SeatOperationService_FreeSeat.Size = new System.Drawing.Size(96, 16);
            this.cb_SeatOperationService_FreeSeat.TabIndex = 24;
            this.cb_SeatOperationService_FreeSeat.Text = "座位释放操作";
            this.cb_SeatOperationService_FreeSeat.UseVisualStyleBackColor = true;
            this.cb_SeatOperationService_FreeSeat.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_SeatOperationService_ShortLeave
            // 
            this.cb_SeatOperationService_ShortLeave.AutoSize = true;
            this.cb_SeatOperationService_ShortLeave.Location = new System.Drawing.Point(6, 130);
            this.cb_SeatOperationService_ShortLeave.Name = "cb_SeatOperationService_ShortLeave";
            this.cb_SeatOperationService_ShortLeave.Size = new System.Drawing.Size(96, 16);
            this.cb_SeatOperationService_ShortLeave.TabIndex = 23;
            this.cb_SeatOperationService_ShortLeave.Text = "座位暂离操作";
            this.cb_SeatOperationService_ShortLeave.UseVisualStyleBackColor = true;
            this.cb_SeatOperationService_ShortLeave.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_SeatOperationService_SeatUnLock
            // 
            this.cb_SeatOperationService_SeatUnLock.AutoSize = true;
            this.cb_SeatOperationService_SeatUnLock.Location = new System.Drawing.Point(6, 108);
            this.cb_SeatOperationService_SeatUnLock.Name = "cb_SeatOperationService_SeatUnLock";
            this.cb_SeatOperationService_SeatUnLock.Size = new System.Drawing.Size(72, 16);
            this.cb_SeatOperationService_SeatUnLock.TabIndex = 22;
            this.cb_SeatOperationService_SeatUnLock.Text = "解锁座位";
            this.cb_SeatOperationService_SeatUnLock.UseVisualStyleBackColor = true;
            this.cb_SeatOperationService_SeatUnLock.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_SeatOperationService_SeatLock
            // 
            this.cb_SeatOperationService_SeatLock.AutoSize = true;
            this.cb_SeatOperationService_SeatLock.Location = new System.Drawing.Point(6, 86);
            this.cb_SeatOperationService_SeatLock.Name = "cb_SeatOperationService_SeatLock";
            this.cb_SeatOperationService_SeatLock.Size = new System.Drawing.Size(72, 16);
            this.cb_SeatOperationService_SeatLock.TabIndex = 21;
            this.cb_SeatOperationService_SeatLock.Text = "锁定座位";
            this.cb_SeatOperationService_SeatLock.UseVisualStyleBackColor = true;
            this.cb_SeatOperationService_SeatLock.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_SeatOperationService_ChangeSeat
            // 
            this.cb_SeatOperationService_ChangeSeat.AutoSize = true;
            this.cb_SeatOperationService_ChangeSeat.Location = new System.Drawing.Point(6, 64);
            this.cb_SeatOperationService_ChangeSeat.Name = "cb_SeatOperationService_ChangeSeat";
            this.cb_SeatOperationService_ChangeSeat.Size = new System.Drawing.Size(96, 16);
            this.cb_SeatOperationService_ChangeSeat.TabIndex = 20;
            this.cb_SeatOperationService_ChangeSeat.Text = "更换座位操作";
            this.cb_SeatOperationService_ChangeSeat.UseVisualStyleBackColor = true;
            this.cb_SeatOperationService_ChangeSeat.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_SeatOperationService_SubmitChooseResult
            // 
            this.cb_SeatOperationService_SubmitChooseResult.AutoSize = true;
            this.cb_SeatOperationService_SubmitChooseResult.Location = new System.Drawing.Point(6, 42);
            this.cb_SeatOperationService_SubmitChooseResult.Name = "cb_SeatOperationService_SubmitChooseResult";
            this.cb_SeatOperationService_SubmitChooseResult.Size = new System.Drawing.Size(72, 16);
            this.cb_SeatOperationService_SubmitChooseResult.TabIndex = 19;
            this.cb_SeatOperationService_SubmitChooseResult.Text = "座位选择";
            this.cb_SeatOperationService_SubmitChooseResult.UseVisualStyleBackColor = true;
            this.cb_SeatOperationService_SubmitChooseResult.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_SeatOperationService_VerifyCanDoIt
            // 
            this.cb_SeatOperationService_VerifyCanDoIt.AutoSize = true;
            this.cb_SeatOperationService_VerifyCanDoIt.Location = new System.Drawing.Point(6, 20);
            this.cb_SeatOperationService_VerifyCanDoIt.Name = "cb_SeatOperationService_VerifyCanDoIt";
            this.cb_SeatOperationService_VerifyCanDoIt.Size = new System.Drawing.Size(144, 16);
            this.cb_SeatOperationService_VerifyCanDoIt.TabIndex = 18;
            this.cb_SeatOperationService_VerifyCanDoIt.Text = "验证用户是否可以选座";
            this.cb_SeatOperationService_VerifyCanDoIt.UseVisualStyleBackColor = true;
            this.cb_SeatOperationService_VerifyCanDoIt.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_SeatOperationService
            // 
            this.cb_SeatOperationService.AutoSize = true;
            this.cb_SeatOperationService.Location = new System.Drawing.Point(183, 0);
            this.cb_SeatOperationService.Name = "cb_SeatOperationService";
            this.cb_SeatOperationService.Size = new System.Drawing.Size(48, 16);
            this.cb_SeatOperationService.TabIndex = 17;
            this.cb_SeatOperationService.Text = "启用";
            this.cb_SeatOperationService.UseVisualStyleBackColor = true;
            this.cb_SeatOperationService.Click += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // gb_GetSeatInfoService
            // 
            this.gb_GetSeatInfoService.Controls.Add(this.cb_GetSeatInfoService_GetQRcodeInfo);
            this.gb_GetSeatInfoService.Controls.Add(this.cb_GetSeatInfoService_GetSeatUsage);
            this.gb_GetSeatInfoService.Controls.Add(this.cb_GetSeatInfoService_GetSeatBespeakInfo);
            this.gb_GetSeatInfoService.Controls.Add(this.cb_GetSeatInfoService);
            this.gb_GetSeatInfoService.Location = new System.Drawing.Point(262, 195);
            this.gb_GetSeatInfoService.Name = "gb_GetSeatInfoService";
            this.gb_GetSeatInfoService.Size = new System.Drawing.Size(305, 87);
            this.gb_GetSeatInfoService.TabIndex = 27;
            this.gb_GetSeatInfoService.TabStop = false;
            this.gb_GetSeatInfoService.Text = "座位信息查询接口";
            // 
            // cb_GetSeatInfoService_GetQRcodeInfo
            // 
            this.cb_GetSeatInfoService_GetQRcodeInfo.AutoSize = true;
            this.cb_GetSeatInfoService_GetQRcodeInfo.Location = new System.Drawing.Point(6, 64);
            this.cb_GetSeatInfoService_GetQRcodeInfo.Name = "cb_GetSeatInfoService_GetQRcodeInfo";
            this.cb_GetSeatInfoService_GetQRcodeInfo.Size = new System.Drawing.Size(180, 16);
            this.cb_GetSeatInfoService_GetQRcodeInfo.TabIndex = 20;
            this.cb_GetSeatInfoService_GetQRcodeInfo.Text = "根据二维字符串获取座位信息";
            this.cb_GetSeatInfoService_GetQRcodeInfo.UseVisualStyleBackColor = true;
            this.cb_GetSeatInfoService_GetQRcodeInfo.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_GetSeatInfoService_GetSeatUsage
            // 
            this.cb_GetSeatInfoService_GetSeatUsage.AutoSize = true;
            this.cb_GetSeatInfoService_GetSeatUsage.Location = new System.Drawing.Point(6, 42);
            this.cb_GetSeatInfoService_GetSeatUsage.Name = "cb_GetSeatInfoService_GetSeatUsage";
            this.cb_GetSeatInfoService_GetSeatUsage.Size = new System.Drawing.Size(216, 16);
            this.cb_GetSeatInfoService_GetSeatUsage.TabIndex = 19;
            this.cb_GetSeatInfoService_GetSeatUsage.Text = "根据座位号查询当前座位的使用情况";
            this.cb_GetSeatInfoService_GetSeatUsage.UseVisualStyleBackColor = true;
            this.cb_GetSeatInfoService_GetSeatUsage.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_GetSeatInfoService_GetSeatBespeakInfo
            // 
            this.cb_GetSeatInfoService_GetSeatBespeakInfo.AutoSize = true;
            this.cb_GetSeatInfoService_GetSeatBespeakInfo.Location = new System.Drawing.Point(6, 20);
            this.cb_GetSeatInfoService_GetSeatBespeakInfo.Name = "cb_GetSeatInfoService_GetSeatBespeakInfo";
            this.cb_GetSeatInfoService_GetSeatBespeakInfo.Size = new System.Drawing.Size(180, 16);
            this.cb_GetSeatInfoService_GetSeatBespeakInfo.TabIndex = 18;
            this.cb_GetSeatInfoService_GetSeatBespeakInfo.Text = "根据座位号查询座位预约信息";
            this.cb_GetSeatInfoService_GetSeatBespeakInfo.UseVisualStyleBackColor = true;
            this.cb_GetSeatInfoService_GetSeatBespeakInfo.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_GetSeatInfoService
            // 
            this.cb_GetSeatInfoService.AutoSize = true;
            this.cb_GetSeatInfoService.Location = new System.Drawing.Point(252, 0);
            this.cb_GetSeatInfoService.Name = "cb_GetSeatInfoService";
            this.cb_GetSeatInfoService.Size = new System.Drawing.Size(48, 16);
            this.cb_GetSeatInfoService.TabIndex = 17;
            this.cb_GetSeatInfoService.Text = "启用";
            this.cb_GetSeatInfoService.UseVisualStyleBackColor = true;
            this.cb_GetSeatInfoService.Click += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // gb_GetReadingRoomInfoService
            // 
            this.gb_GetReadingRoomInfoService.Controls.Add(this.cb_GetReadingRoomInfoService_GetSeatsLayoutByRoomNum);
            this.gb_GetReadingRoomInfoService.Controls.Add(this.cb_GetReadingRoomInfoService_GetAllRoomSeatUsedInfo);
            this.gb_GetReadingRoomInfoService.Controls.Add(this.cb_GetReadingRoomInfoService_GetSeatsBespeakInfoByRoomNum);
            this.gb_GetReadingRoomInfoService.Controls.Add(this.cb_GetReadingRoomInfoService_GetSeatsUsedInfoByRoomNum);
            this.gb_GetReadingRoomInfoService.Controls.Add(this.cb_GetReadingRoomInfoService_GetReadingRoomSetInfoByRoomNum);
            this.gb_GetReadingRoomInfoService.Controls.Add(this.cb_GetReadingRoomInfoService_GetAllReadingRoomBaseInfo);
            this.gb_GetReadingRoomInfoService.Controls.Add(this.cb_GetReadingRoomInfoService);
            this.gb_GetReadingRoomInfoService.Location = new System.Drawing.Point(262, 36);
            this.gb_GetReadingRoomInfoService.Name = "gb_GetReadingRoomInfoService";
            this.gb_GetReadingRoomInfoService.Size = new System.Drawing.Size(305, 153);
            this.gb_GetReadingRoomInfoService.TabIndex = 26;
            this.gb_GetReadingRoomInfoService.TabStop = false;
            this.gb_GetReadingRoomInfoService.Text = "阅览室信息查询接口";
            // 
            // cb_GetReadingRoomInfoService_GetSeatsLayoutByRoomNum
            // 
            this.cb_GetReadingRoomInfoService_GetSeatsLayoutByRoomNum.AutoSize = true;
            this.cb_GetReadingRoomInfoService_GetSeatsLayoutByRoomNum.Location = new System.Drawing.Point(6, 130);
            this.cb_GetReadingRoomInfoService_GetSeatsLayoutByRoomNum.Name = "cb_GetReadingRoomInfoService_GetSeatsLayoutByRoomNum";
            this.cb_GetReadingRoomInfoService_GetSeatsLayoutByRoomNum.Size = new System.Drawing.Size(192, 16);
            this.cb_GetReadingRoomInfoService_GetSeatsLayoutByRoomNum.TabIndex = 23;
            this.cb_GetReadingRoomInfoService_GetSeatsLayoutByRoomNum.Text = "根据阅览室编号获取座位布局图";
            this.cb_GetReadingRoomInfoService_GetSeatsLayoutByRoomNum.UseVisualStyleBackColor = true;
            this.cb_GetReadingRoomInfoService_GetSeatsLayoutByRoomNum.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_GetReadingRoomInfoService_GetAllRoomSeatUsedInfo
            // 
            this.cb_GetReadingRoomInfoService_GetAllRoomSeatUsedInfo.AutoSize = true;
            this.cb_GetReadingRoomInfoService_GetAllRoomSeatUsedInfo.Location = new System.Drawing.Point(6, 108);
            this.cb_GetReadingRoomInfoService_GetAllRoomSeatUsedInfo.Name = "cb_GetReadingRoomInfoService_GetAllRoomSeatUsedInfo";
            this.cb_GetReadingRoomInfoService_GetAllRoomSeatUsedInfo.Size = new System.Drawing.Size(168, 16);
            this.cb_GetReadingRoomInfoService_GetAllRoomSeatUsedInfo.TabIndex = 22;
            this.cb_GetReadingRoomInfoService_GetAllRoomSeatUsedInfo.Text = "获取全部阅览室的使用情况";
            this.cb_GetReadingRoomInfoService_GetAllRoomSeatUsedInfo.UseVisualStyleBackColor = true;
            this.cb_GetReadingRoomInfoService_GetAllRoomSeatUsedInfo.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_GetReadingRoomInfoService_GetSeatsBespeakInfoByRoomNum
            // 
            this.cb_GetReadingRoomInfoService_GetSeatsBespeakInfoByRoomNum.AutoSize = true;
            this.cb_GetReadingRoomInfoService_GetSeatsBespeakInfoByRoomNum.Location = new System.Drawing.Point(6, 86);
            this.cb_GetReadingRoomInfoService_GetSeatsBespeakInfoByRoomNum.Name = "cb_GetReadingRoomInfoService_GetSeatsBespeakInfoByRoomNum";
            this.cb_GetReadingRoomInfoService_GetSeatsBespeakInfoByRoomNum.Size = new System.Drawing.Size(294, 16);
            this.cb_GetReadingRoomInfoService_GetSeatsBespeakInfoByRoomNum.TabIndex = 21;
            this.cb_GetReadingRoomInfoService_GetSeatsBespeakInfoByRoomNum.Text = "获取阅览室可预约的座位信息/已被预约的座位信息";
            this.cb_GetReadingRoomInfoService_GetSeatsBespeakInfoByRoomNum.UseVisualStyleBackColor = true;
            this.cb_GetReadingRoomInfoService_GetSeatsBespeakInfoByRoomNum.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_GetReadingRoomInfoService_GetSeatsUsedInfoByRoomNum
            // 
            this.cb_GetReadingRoomInfoService_GetSeatsUsedInfoByRoomNum.AutoSize = true;
            this.cb_GetReadingRoomInfoService_GetSeatsUsedInfoByRoomNum.Location = new System.Drawing.Point(6, 64);
            this.cb_GetReadingRoomInfoService_GetSeatsUsedInfoByRoomNum.Name = "cb_GetReadingRoomInfoService_GetSeatsUsedInfoByRoomNum";
            this.cb_GetReadingRoomInfoService_GetSeatsUsedInfoByRoomNum.Size = new System.Drawing.Size(294, 16);
            this.cb_GetReadingRoomInfoService_GetSeatsUsedInfoByRoomNum.TabIndex = 20;
            this.cb_GetReadingRoomInfoService_GetSeatsUsedInfoByRoomNum.Text = "根据阅览室编号获取阅览室当前座位信息/使用信息";
            this.cb_GetReadingRoomInfoService_GetSeatsUsedInfoByRoomNum.UseVisualStyleBackColor = true;
            this.cb_GetReadingRoomInfoService_GetSeatsUsedInfoByRoomNum.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_GetReadingRoomInfoService_GetReadingRoomSetInfoByRoomNum
            // 
            this.cb_GetReadingRoomInfoService_GetReadingRoomSetInfoByRoomNum.AutoSize = true;
            this.cb_GetReadingRoomInfoService_GetReadingRoomSetInfoByRoomNum.Location = new System.Drawing.Point(6, 42);
            this.cb_GetReadingRoomInfoService_GetReadingRoomSetInfoByRoomNum.Name = "cb_GetReadingRoomInfoService_GetReadingRoomSetInfoByRoomNum";
            this.cb_GetReadingRoomInfoService_GetReadingRoomSetInfoByRoomNum.Size = new System.Drawing.Size(240, 16);
            this.cb_GetReadingRoomInfoService_GetReadingRoomSetInfoByRoomNum.TabIndex = 19;
            this.cb_GetReadingRoomInfoService_GetReadingRoomSetInfoByRoomNum.Text = "根据阅览室编号获取阅览室当前配置信息";
            this.cb_GetReadingRoomInfoService_GetReadingRoomSetInfoByRoomNum.UseVisualStyleBackColor = true;
            this.cb_GetReadingRoomInfoService_GetReadingRoomSetInfoByRoomNum.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_GetReadingRoomInfoService_GetAllReadingRoomBaseInfo
            // 
            this.cb_GetReadingRoomInfoService_GetAllReadingRoomBaseInfo.AutoSize = true;
            this.cb_GetReadingRoomInfoService_GetAllReadingRoomBaseInfo.Location = new System.Drawing.Point(6, 20);
            this.cb_GetReadingRoomInfoService_GetAllReadingRoomBaseInfo.Name = "cb_GetReadingRoomInfoService_GetAllReadingRoomBaseInfo";
            this.cb_GetReadingRoomInfoService_GetAllReadingRoomBaseInfo.Size = new System.Drawing.Size(132, 16);
            this.cb_GetReadingRoomInfoService_GetAllReadingRoomBaseInfo.TabIndex = 18;
            this.cb_GetReadingRoomInfoService_GetAllReadingRoomBaseInfo.Text = "获取所有阅览室信息";
            this.cb_GetReadingRoomInfoService_GetAllReadingRoomBaseInfo.UseVisualStyleBackColor = true;
            this.cb_GetReadingRoomInfoService_GetAllReadingRoomBaseInfo.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_GetReadingRoomInfoService
            // 
            this.cb_GetReadingRoomInfoService.AutoSize = true;
            this.cb_GetReadingRoomInfoService.Location = new System.Drawing.Point(251, 0);
            this.cb_GetReadingRoomInfoService.Name = "cb_GetReadingRoomInfoService";
            this.cb_GetReadingRoomInfoService.Size = new System.Drawing.Size(48, 16);
            this.cb_GetReadingRoomInfoService.TabIndex = 17;
            this.cb_GetReadingRoomInfoService.Text = "启用";
            this.cb_GetReadingRoomInfoService.UseVisualStyleBackColor = true;
            this.cb_GetReadingRoomInfoService.Click += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // gb_GetReaderInfoService
            // 
            this.gb_GetReaderInfoService.Controls.Add(this.cb_GetReaderInfoService_GetReaderAccount);
            this.gb_GetReaderInfoService.Controls.Add(this.cb_GetReaderInfoService_GetReaderBlacklistRecord);
            this.gb_GetReaderInfoService.Controls.Add(this.cb_GetReaderInfoService_GetViolateDiscipline);
            this.gb_GetReaderInfoService.Controls.Add(this.cb_GetReaderInfoService_GetReaderChooseSeatRecord);
            this.gb_GetReaderInfoService.Controls.Add(this.cb_GetReaderInfoService_GetReaderBespeakRecord);
            this.gb_GetReaderInfoService.Controls.Add(this.cb_GetReaderInfoService_GetReaderActualTimeRecord);
            this.gb_GetReaderInfoService.Controls.Add(this.cb_GetReaderInfoService_GetBaseReaderInfoByCardId);
            this.gb_GetReaderInfoService.Controls.Add(this.cb_GetReaderInfoService_GetBaseReaderInfo);
            this.gb_GetReaderInfoService.Controls.Add(this.cb_GetReaderInfoService);
            this.gb_GetReaderInfoService.Location = new System.Drawing.Point(12, 116);
            this.gb_GetReaderInfoService.Name = "gb_GetReaderInfoService";
            this.gb_GetReaderInfoService.Size = new System.Drawing.Size(236, 198);
            this.gb_GetReaderInfoService.TabIndex = 25;
            this.gb_GetReaderInfoService.TabStop = false;
            this.gb_GetReaderInfoService.Text = "读者信息查询接口";
            // 
            // cb_GetReaderInfoService_GetReaderAccount
            // 
            this.cb_GetReaderInfoService_GetReaderAccount.AutoSize = true;
            this.cb_GetReaderInfoService_GetReaderAccount.Location = new System.Drawing.Point(6, 174);
            this.cb_GetReaderInfoService_GetReaderAccount.Name = "cb_GetReaderInfoService_GetReaderAccount";
            this.cb_GetReaderInfoService_GetReaderAccount.Size = new System.Drawing.Size(120, 16);
            this.cb_GetReaderInfoService_GetReaderAccount.TabIndex = 25;
            this.cb_GetReaderInfoService_GetReaderAccount.Text = "验证读者帐号信息";
            this.cb_GetReaderInfoService_GetReaderAccount.UseVisualStyleBackColor = true;
            this.cb_GetReaderInfoService_GetReaderAccount.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_GetReaderInfoService_GetReaderBlacklistRecord
            // 
            this.cb_GetReaderInfoService_GetReaderBlacklistRecord.AutoSize = true;
            this.cb_GetReaderInfoService_GetReaderBlacklistRecord.Location = new System.Drawing.Point(6, 152);
            this.cb_GetReaderInfoService_GetReaderBlacklistRecord.Name = "cb_GetReaderInfoService_GetReaderBlacklistRecord";
            this.cb_GetReaderInfoService_GetReaderBlacklistRecord.Size = new System.Drawing.Size(144, 16);
            this.cb_GetReaderInfoService_GetReaderBlacklistRecord.TabIndex = 24;
            this.cb_GetReaderInfoService_GetReaderBlacklistRecord.Text = "查询读者的黑名单记录";
            this.cb_GetReaderInfoService_GetReaderBlacklistRecord.UseVisualStyleBackColor = true;
            this.cb_GetReaderInfoService_GetReaderBlacklistRecord.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_GetReaderInfoService_GetViolateDiscipline
            // 
            this.cb_GetReaderInfoService_GetViolateDiscipline.AutoSize = true;
            this.cb_GetReaderInfoService_GetViolateDiscipline.Location = new System.Drawing.Point(6, 130);
            this.cb_GetReaderInfoService_GetViolateDiscipline.Name = "cb_GetReaderInfoService_GetViolateDiscipline";
            this.cb_GetReaderInfoService_GetViolateDiscipline.Size = new System.Drawing.Size(132, 16);
            this.cb_GetReaderInfoService_GetViolateDiscipline.TabIndex = 23;
            this.cb_GetReaderInfoService_GetViolateDiscipline.Text = "查询读者的违规记录";
            this.cb_GetReaderInfoService_GetViolateDiscipline.UseVisualStyleBackColor = true;
            this.cb_GetReaderInfoService_GetViolateDiscipline.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_GetReaderInfoService_GetReaderChooseSeatRecord
            // 
            this.cb_GetReaderInfoService_GetReaderChooseSeatRecord.AutoSize = true;
            this.cb_GetReaderInfoService_GetReaderChooseSeatRecord.Location = new System.Drawing.Point(6, 108);
            this.cb_GetReaderInfoService_GetReaderChooseSeatRecord.Name = "cb_GetReaderInfoService_GetReaderChooseSeatRecord";
            this.cb_GetReaderInfoService_GetReaderChooseSeatRecord.Size = new System.Drawing.Size(132, 16);
            this.cb_GetReaderInfoService_GetReaderChooseSeatRecord.TabIndex = 22;
            this.cb_GetReaderInfoService_GetReaderChooseSeatRecord.Text = "查询读者的选座记录";
            this.cb_GetReaderInfoService_GetReaderChooseSeatRecord.UseVisualStyleBackColor = true;
            this.cb_GetReaderInfoService_GetReaderChooseSeatRecord.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_GetReaderInfoService_GetReaderBespeakRecord
            // 
            this.cb_GetReaderInfoService_GetReaderBespeakRecord.AutoSize = true;
            this.cb_GetReaderInfoService_GetReaderBespeakRecord.Location = new System.Drawing.Point(6, 86);
            this.cb_GetReaderInfoService_GetReaderBespeakRecord.Name = "cb_GetReaderInfoService_GetReaderBespeakRecord";
            this.cb_GetReaderInfoService_GetReaderBespeakRecord.Size = new System.Drawing.Size(132, 16);
            this.cb_GetReaderInfoService_GetReaderBespeakRecord.TabIndex = 21;
            this.cb_GetReaderInfoService_GetReaderBespeakRecord.Text = "查询读者的预约记录";
            this.cb_GetReaderInfoService_GetReaderBespeakRecord.UseVisualStyleBackColor = true;
            this.cb_GetReaderInfoService_GetReaderBespeakRecord.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_GetReaderInfoService_GetReaderActualTimeRecord
            // 
            this.cb_GetReaderInfoService_GetReaderActualTimeRecord.AutoSize = true;
            this.cb_GetReaderInfoService_GetReaderActualTimeRecord.Location = new System.Drawing.Point(6, 64);
            this.cb_GetReaderInfoService_GetReaderActualTimeRecord.Name = "cb_GetReaderInfoService_GetReaderActualTimeRecord";
            this.cb_GetReaderInfoService_GetReaderActualTimeRecord.Size = new System.Drawing.Size(168, 16);
            this.cb_GetReaderInfoService_GetReaderActualTimeRecord.TabIndex = 20;
            this.cb_GetReaderInfoService_GetReaderActualTimeRecord.Text = "根据学号查询读者实时记录";
            this.cb_GetReaderInfoService_GetReaderActualTimeRecord.UseVisualStyleBackColor = true;
            this.cb_GetReaderInfoService_GetReaderActualTimeRecord.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_GetReaderInfoService_GetBaseReaderInfoByCardId
            // 
            this.cb_GetReaderInfoService_GetBaseReaderInfoByCardId.AutoSize = true;
            this.cb_GetReaderInfoService_GetBaseReaderInfoByCardId.Location = new System.Drawing.Point(6, 42);
            this.cb_GetReaderInfoService_GetBaseReaderInfoByCardId.Name = "cb_GetReaderInfoService_GetBaseReaderInfoByCardId";
            this.cb_GetReaderInfoService_GetBaseReaderInfoByCardId.Size = new System.Drawing.Size(216, 16);
            this.cb_GetReaderInfoService_GetBaseReaderInfoByCardId.TabIndex = 19;
            this.cb_GetReaderInfoService_GetBaseReaderInfoByCardId.Text = "根据一卡通序列号查询读者基本信息";
            this.cb_GetReaderInfoService_GetBaseReaderInfoByCardId.UseVisualStyleBackColor = true;
            this.cb_GetReaderInfoService_GetBaseReaderInfoByCardId.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_GetReaderInfoService_GetBaseReaderInfo
            // 
            this.cb_GetReaderInfoService_GetBaseReaderInfo.AutoSize = true;
            this.cb_GetReaderInfoService_GetBaseReaderInfo.Location = new System.Drawing.Point(6, 20);
            this.cb_GetReaderInfoService_GetBaseReaderInfo.Name = "cb_GetReaderInfoService_GetBaseReaderInfo";
            this.cb_GetReaderInfoService_GetBaseReaderInfo.Size = new System.Drawing.Size(168, 16);
            this.cb_GetReaderInfoService_GetBaseReaderInfo.TabIndex = 18;
            this.cb_GetReaderInfoService_GetBaseReaderInfo.Text = "根据学号查询读者基本信息";
            this.cb_GetReaderInfoService_GetBaseReaderInfo.UseVisualStyleBackColor = true;
            this.cb_GetReaderInfoService_GetBaseReaderInfo.Click += new System.EventHandler(this.cbc_CheckedChanged);
            // 
            // cb_GetReaderInfoService
            // 
            this.cb_GetReaderInfoService.AutoSize = true;
            this.cb_GetReaderInfoService.Location = new System.Drawing.Point(183, 0);
            this.cb_GetReaderInfoService.Name = "cb_GetReaderInfoService";
            this.cb_GetReaderInfoService.Size = new System.Drawing.Size(48, 16);
            this.cb_GetReaderInfoService.TabIndex = 17;
            this.cb_GetReaderInfoService.Text = "启用";
            this.cb_GetReaderInfoService.UseVisualStyleBackColor = true;
            this.cb_GetReaderInfoService.Click += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(345, 8);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 22;
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(260, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 24;
            this.label3.Text = "选择授权用户：";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txt_SchoolNo);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 95);
            this.panel1.TabIndex = 23;
            // 
            // txt_SchoolNo
            // 
            this.txt_SchoolNo.Enabled = false;
            this.txt_SchoolNo.Location = new System.Drawing.Point(73, 61);
            this.txt_SchoolNo.Name = "txt_SchoolNo";
            this.txt_SchoolNo.Size = new System.Drawing.Size(100, 21);
            this.txt_SchoolNo.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "学校编号：";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(175, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(58, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "添加";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(73, 34);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "密码：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(73, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户名：";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 359);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gb_SeatOperationService);
            this.Controls.Add(this.gb_GetSeatInfoService);
            this.Controls.Add(this.gb_GetReadingRoomInfoService);
            this.Controls.Add(this.gb_GetReaderInfoService);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "接口授权";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.gb_SeatOperationService.ResumeLayout(false);
            this.gb_SeatOperationService.PerformLayout();
            this.gb_GetSeatInfoService.ResumeLayout(false);
            this.gb_GetSeatInfoService.PerformLayout();
            this.gb_GetReadingRoomInfoService.ResumeLayout(false);
            this.gb_GetReadingRoomInfoService.PerformLayout();
            this.gb_GetReaderInfoService.ResumeLayout(false);
            this.gb_GetReaderInfoService.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox gb_SeatOperationService;
        private System.Windows.Forms.CheckBox cb_SeatOperationService_ComeBack;
        private System.Windows.Forms.CheckBox cb_SeatOperationService_BespeakCheck;
        private System.Windows.Forms.CheckBox cb_SeatOperationService_CancelBespeakLog;
        private System.Windows.Forms.CheckBox cb_SeatOperationService_SubmitBespeakInfo;
        private System.Windows.Forms.CheckBox cb_SeatOperationService_SubmitDelayResult;
        private System.Windows.Forms.CheckBox cb_SeatOperationService_FreeSeat;
        private System.Windows.Forms.CheckBox cb_SeatOperationService_ShortLeave;
        private System.Windows.Forms.CheckBox cb_SeatOperationService_SeatUnLock;
        private System.Windows.Forms.CheckBox cb_SeatOperationService_SeatLock;
        private System.Windows.Forms.CheckBox cb_SeatOperationService_ChangeSeat;
        private System.Windows.Forms.CheckBox cb_SeatOperationService_SubmitChooseResult;
        private System.Windows.Forms.CheckBox cb_SeatOperationService_VerifyCanDoIt;
        private System.Windows.Forms.CheckBox cb_SeatOperationService;
        private System.Windows.Forms.GroupBox gb_GetSeatInfoService;
        private System.Windows.Forms.CheckBox cb_GetSeatInfoService_GetQRcodeInfo;
        private System.Windows.Forms.CheckBox cb_GetSeatInfoService_GetSeatUsage;
        private System.Windows.Forms.CheckBox cb_GetSeatInfoService_GetSeatBespeakInfo;
        private System.Windows.Forms.CheckBox cb_GetSeatInfoService;
        private System.Windows.Forms.GroupBox gb_GetReadingRoomInfoService;
        private System.Windows.Forms.CheckBox cb_GetReadingRoomInfoService_GetSeatsLayoutByRoomNum;
        private System.Windows.Forms.CheckBox cb_GetReadingRoomInfoService_GetAllRoomSeatUsedInfo;
        private System.Windows.Forms.CheckBox cb_GetReadingRoomInfoService_GetSeatsBespeakInfoByRoomNum;
        private System.Windows.Forms.CheckBox cb_GetReadingRoomInfoService_GetSeatsUsedInfoByRoomNum;
        private System.Windows.Forms.CheckBox cb_GetReadingRoomInfoService_GetReadingRoomSetInfoByRoomNum;
        private System.Windows.Forms.CheckBox cb_GetReadingRoomInfoService_GetAllReadingRoomBaseInfo;
        private System.Windows.Forms.CheckBox cb_GetReadingRoomInfoService;
        private System.Windows.Forms.GroupBox gb_GetReaderInfoService;
        private System.Windows.Forms.CheckBox cb_GetReaderInfoService_GetReaderAccount;
        private System.Windows.Forms.CheckBox cb_GetReaderInfoService_GetReaderBlacklistRecord;
        private System.Windows.Forms.CheckBox cb_GetReaderInfoService_GetViolateDiscipline;
        private System.Windows.Forms.CheckBox cb_GetReaderInfoService_GetReaderChooseSeatRecord;
        private System.Windows.Forms.CheckBox cb_GetReaderInfoService_GetReaderBespeakRecord;
        private System.Windows.Forms.CheckBox cb_GetReaderInfoService_GetReaderActualTimeRecord;
        private System.Windows.Forms.CheckBox cb_GetReaderInfoService_GetBaseReaderInfoByCardId;
        private System.Windows.Forms.CheckBox cb_GetReaderInfoService_GetBaseReaderInfo;
        private System.Windows.Forms.CheckBox cb_GetReaderInfoService;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_SchoolNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}

