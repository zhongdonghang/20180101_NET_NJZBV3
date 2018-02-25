namespace SeatClient.Config
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblResultMessage = new System.Windows.Forms.Label();
            this.btnGetConfig = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cb_pos = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTimes = new System.Windows.Forms.TextBox();
            this.txtTimeLength = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTerminalNum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdb1080 = new System.Windows.Forms.RadioButton();
            this.rdb1024 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtRooms = new System.Windows.Forms.TextBox();
            this.btnAddRoomNums = new System.Windows.Forms.Button();
            this.lstReadingRoomNums = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ckbIsShowInitPOS = new System.Windows.Forms.CheckBox();
            this.ckbActiveBespeakSeat = new System.Windows.Forms.CheckBox();
            this.ckbOftenUsedSeat = new System.Windows.Forms.CheckBox();
            this.ckbEnterNoForSeat = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoAutomaticMode = new System.Windows.Forms.RadioButton();
            this.rdoOptionalMode = new System.Windows.Forms.RadioButton();
            this.rdoManualMode = new System.Windows.Forms.RadioButton();
            this.rdoChoseMethodDefault = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rbPrint = new System.Windows.Forms.RadioButton();
            this.rbChangePrint = new System.Windows.Forms.RadioButton();
            this.rbNoPrint = new System.Windows.Forms.RadioButton();
            this.rdb1280 = new System.Windows.Forms.RadioButton();
            this.rdb1440 = new System.Windows.Forms.RadioButton();
            this.rdb1920 = new System.Windows.Forms.RadioButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(4, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(499, 371);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.lblResultMessage);
            this.tabPage1.Controls.Add(this.btnGetConfig);
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.txtTerminalNum);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.btnClose);
            this.tabPage1.Controls.Add(this.btnSubmit);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(491, 345);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "选座终端设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblResultMessage
            // 
            this.lblResultMessage.AutoSize = true;
            this.lblResultMessage.ForeColor = System.Drawing.Color.Red;
            this.lblResultMessage.Location = new System.Drawing.Point(26, 42);
            this.lblResultMessage.Name = "lblResultMessage";
            this.lblResultMessage.Size = new System.Drawing.Size(0, 12);
            this.lblResultMessage.TabIndex = 21;
            // 
            // btnGetConfig
            // 
            this.btnGetConfig.Enabled = false;
            this.btnGetConfig.Location = new System.Drawing.Point(186, 9);
            this.btnGetConfig.Name = "btnGetConfig";
            this.btnGetConfig.Size = new System.Drawing.Size(75, 23);
            this.btnGetConfig.TabIndex = 20;
            this.btnGetConfig.Text = "获取设置";
            this.btnGetConfig.UseVisualStyleBackColor = true;
            this.btnGetConfig.Click += new System.EventHandler(this.btnGetConfig_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cb_pos);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.txtTimes);
            this.groupBox5.Controls.Add(this.txtTimeLength);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Location = new System.Drawing.Point(16, 149);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(294, 70);
            this.groupBox5.TabIndex = 19;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "刷卡次数设置";
            // 
            // cb_pos
            // 
            this.cb_pos.AutoSize = true;
            this.cb_pos.Location = new System.Drawing.Point(12, 21);
            this.cb_pos.Name = "cb_pos";
            this.cb_pos.Size = new System.Drawing.Size(120, 16);
            this.cb_pos.TabIndex = 6;
            this.cb_pos.Text = "启用数卡次数限制";
            this.cb_pos.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(209, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "(次)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(88, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "(分)";
            // 
            // txtTimes
            // 
            this.txtTimes.Location = new System.Drawing.Point(163, 43);
            this.txtTimes.Name = "txtTimes";
            this.txtTimes.Size = new System.Drawing.Size(45, 21);
            this.txtTimes.TabIndex = 3;
            // 
            // txtTimeLength
            // 
            this.txtTimeLength.Location = new System.Drawing.Point(39, 43);
            this.txtTimeLength.Name = "txtTimeLength";
            this.txtTimeLength.Size = new System.Drawing.Size(45, 21);
            this.txtTimeLength.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(131, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "次数";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "时长";
            // 
            // txtTerminalNum
            // 
            this.txtTerminalNum.Location = new System.Drawing.Point(88, 10);
            this.txtTerminalNum.Name = "txtTerminalNum";
            this.txtTerminalNum.Size = new System.Drawing.Size(90, 21);
            this.txtTerminalNum.TabIndex = 18;
            this.txtTerminalNum.TextChanged += new System.EventHandler(this.txtTerminalNum_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "终端编号：";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(235, 315);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(149, 315);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 13;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdb1920);
            this.groupBox4.Controls.Add(this.rdb1440);
            this.groupBox4.Controls.Add(this.rdb1280);
            this.groupBox4.Controls.Add(this.rdb1080);
            this.groupBox4.Controls.Add(this.rdb1024);
            this.groupBox4.Location = new System.Drawing.Point(16, 225);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(294, 85);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "屏幕分辨率";
            this.toolTip1.SetToolTip(this.groupBox4, "设置终端的分辨率");
            // 
            // rdb1080
            // 
            this.rdb1080.AutoSize = true;
            this.rdb1080.Checked = true;
            this.rdb1080.Location = new System.Drawing.Point(9, 62);
            this.rdb1080.Name = "rdb1080";
            this.rdb1080.Size = new System.Drawing.Size(137, 16);
            this.rdb1080.TabIndex = 1;
            this.rdb1080.TabStop = true;
            this.rdb1080.Text = "1080*1000（触摸屏）";
            this.rdb1080.UseVisualStyleBackColor = true;
            // 
            // rdb1024
            // 
            this.rdb1024.AutoSize = true;
            this.rdb1024.Location = new System.Drawing.Point(9, 18);
            this.rdb1024.Name = "rdb1024";
            this.rdb1024.Size = new System.Drawing.Size(71, 16);
            this.rdb1024.TabIndex = 0;
            this.rdb1024.Text = "1024*768";
            this.rdb1024.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtRooms);
            this.groupBox2.Controls.Add(this.btnAddRoomNums);
            this.groupBox2.Controls.Add(this.lstReadingRoomNums);
            this.groupBox2.Location = new System.Drawing.Point(316, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(168, 188);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "区域编号管理";
            this.toolTip1.SetToolTip(this.groupBox2, "设置该终端所管理的阅览室编号");
            // 
            // txtRooms
            // 
            this.txtRooms.Location = new System.Drawing.Point(11, 128);
            this.txtRooms.Name = "txtRooms";
            this.txtRooms.Size = new System.Drawing.Size(148, 21);
            this.txtRooms.TabIndex = 4;
            this.toolTip1.SetToolTip(this.txtRooms, "添加终端所管理的阅览室，多个阅览室编号之间用分号隔开");
            // 
            // btnAddRoomNums
            // 
            this.btnAddRoomNums.Location = new System.Drawing.Point(89, 155);
            this.btnAddRoomNums.Name = "btnAddRoomNums";
            this.btnAddRoomNums.Size = new System.Drawing.Size(70, 23);
            this.btnAddRoomNums.TabIndex = 3;
            this.btnAddRoomNums.Text = "添加编号";
            this.toolTip1.SetToolTip(this.btnAddRoomNums, "添加终端所管理的阅览室，多个阅览室编号之间用分号隔开");
            this.btnAddRoomNums.UseVisualStyleBackColor = true;
            this.btnAddRoomNums.Click += new System.EventHandler(this.btnAddRoomNums_Click);
            // 
            // lstReadingRoomNums
            // 
            this.lstReadingRoomNums.ColumnWidth = 45;
            this.lstReadingRoomNums.FormattingEnabled = true;
            this.lstReadingRoomNums.ItemHeight = 12;
            this.lstReadingRoomNums.Location = new System.Drawing.Point(11, 22);
            this.lstReadingRoomNums.MultiColumn = true;
            this.lstReadingRoomNums.Name = "lstReadingRoomNums";
            this.lstReadingRoomNums.Size = new System.Drawing.Size(148, 100);
            this.lstReadingRoomNums.TabIndex = 2;
            this.toolTip1.SetToolTip(this.lstReadingRoomNums, "触摸屏所管理的阅览室编号，双击可以删除");
            this.lstReadingRoomNums.DoubleClick += new System.EventHandler(this.lstReadingRoomNums_DoubleClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ckbIsShowInitPOS);
            this.groupBox3.Controls.Add(this.ckbActiveBespeakSeat);
            this.groupBox3.Controls.Add(this.ckbOftenUsedSeat);
            this.groupBox3.Controls.Add(this.ckbEnterNoForSeat);
            this.groupBox3.Location = new System.Drawing.Point(16, 93);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(468, 50);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "系统功能";
            this.toolTip1.SetToolTip(this.groupBox3, "选座终端上的功能设置");
            // 
            // ckbIsShowInitPOS
            // 
            this.ckbIsShowInitPOS.AutoSize = true;
            this.ckbIsShowInitPOS.Checked = true;
            this.ckbIsShowInitPOS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbIsShowInitPOS.Location = new System.Drawing.Point(304, 18);
            this.ckbIsShowInitPOS.Name = "ckbIsShowInitPOS";
            this.ckbIsShowInitPOS.Size = new System.Drawing.Size(144, 16);
            this.ckbIsShowInitPOS.TabIndex = 4;
            this.ckbIsShowInitPOS.Text = "显示读卡器初始化按钮";
            this.toolTip1.SetToolTip(this.ckbIsShowInitPOS, "设置终端是否显示读卡器初始化按钮");
            this.ckbIsShowInitPOS.UseVisualStyleBackColor = true;
            // 
            // ckbActiveBespeakSeat
            // 
            this.ckbActiveBespeakSeat.AutoSize = true;
            this.ckbActiveBespeakSeat.Checked = true;
            this.ckbActiveBespeakSeat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbActiveBespeakSeat.Location = new System.Drawing.Point(211, 18);
            this.ckbActiveBespeakSeat.Name = "ckbActiveBespeakSeat";
            this.ckbActiveBespeakSeat.Size = new System.Drawing.Size(96, 16);
            this.ckbActiveBespeakSeat.TabIndex = 3;
            this.ckbActiveBespeakSeat.Text = "预约功能激活";
            this.toolTip1.SetToolTip(this.ckbActiveBespeakSeat, "设置终端是否启用预约激活的功能");
            this.ckbActiveBespeakSeat.UseVisualStyleBackColor = true;
            // 
            // ckbOftenUsedSeat
            // 
            this.ckbOftenUsedSeat.AutoSize = true;
            this.ckbOftenUsedSeat.Checked = true;
            this.ckbOftenUsedSeat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbOftenUsedSeat.Location = new System.Drawing.Point(114, 18);
            this.ckbOftenUsedSeat.Name = "ckbOftenUsedSeat";
            this.ckbOftenUsedSeat.Size = new System.Drawing.Size(96, 16);
            this.ckbOftenUsedSeat.TabIndex = 1;
            this.ckbOftenUsedSeat.Text = "选择常坐座位";
            this.toolTip1.SetToolTip(this.ckbOftenUsedSeat, "设置终端是否可以选择常坐座位");
            this.ckbOftenUsedSeat.UseVisualStyleBackColor = true;
            // 
            // ckbEnterNoForSeat
            // 
            this.ckbEnterNoForSeat.AutoSize = true;
            this.ckbEnterNoForSeat.Checked = true;
            this.ckbEnterNoForSeat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbEnterNoForSeat.Location = new System.Drawing.Point(12, 18);
            this.ckbEnterNoForSeat.Name = "ckbEnterNoForSeat";
            this.ckbEnterNoForSeat.Size = new System.Drawing.Size(96, 16);
            this.ckbEnterNoForSeat.TabIndex = 0;
            this.ckbEnterNoForSeat.Text = "键盘输入选座";
            this.toolTip1.SetToolTip(this.ckbEnterNoForSeat, "指示读者是否可以通过输入座位号选择座位");
            this.ckbEnterNoForSeat.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoAutomaticMode);
            this.groupBox1.Controls.Add(this.rdoOptionalMode);
            this.groupBox1.Controls.Add(this.rdoManualMode);
            this.groupBox1.Controls.Add(this.rdoChoseMethodDefault);
            this.groupBox1.Location = new System.Drawing.Point(16, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 45);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选座方式";
            this.toolTip1.SetToolTip(this.groupBox1, "设置终端上的座位选择方式");
            // 
            // rdoAutomaticMode
            // 
            this.rdoAutomaticMode.AutoSize = true;
            this.rdoAutomaticMode.Location = new System.Drawing.Point(124, 20);
            this.rdoAutomaticMode.Name = "rdoAutomaticMode";
            this.rdoAutomaticMode.Size = new System.Drawing.Size(47, 16);
            this.rdoAutomaticMode.TabIndex = 2;
            this.rdoAutomaticMode.Text = "随机";
            this.toolTip1.SetToolTip(this.rdoAutomaticMode, "系统给读者随机分配座位。");
            this.rdoAutomaticMode.UseVisualStyleBackColor = true;
            // 
            // rdoOptionalMode
            // 
            this.rdoOptionalMode.AutoSize = true;
            this.rdoOptionalMode.Location = new System.Drawing.Point(177, 20);
            this.rdoOptionalMode.Name = "rdoOptionalMode";
            this.rdoOptionalMode.Size = new System.Drawing.Size(71, 16);
            this.rdoOptionalMode.TabIndex = 3;
            this.rdoOptionalMode.Text = "读者选择";
            this.toolTip1.SetToolTip(this.rdoOptionalMode, "显示两种选座方式，让读者自己选择。");
            this.rdoOptionalMode.UseVisualStyleBackColor = true;
            // 
            // rdoManualMode
            // 
            this.rdoManualMode.AutoSize = true;
            this.rdoManualMode.Location = new System.Drawing.Point(71, 20);
            this.rdoManualMode.Name = "rdoManualMode";
            this.rdoManualMode.Size = new System.Drawing.Size(47, 16);
            this.rdoManualMode.TabIndex = 1;
            this.rdoManualMode.Text = "手动";
            this.toolTip1.SetToolTip(this.rdoManualMode, "读者可以通过座位布局图选择座位");
            this.rdoManualMode.UseVisualStyleBackColor = true;
            // 
            // rdoChoseMethodDefault
            // 
            this.rdoChoseMethodDefault.AutoSize = true;
            this.rdoChoseMethodDefault.Checked = true;
            this.rdoChoseMethodDefault.Location = new System.Drawing.Point(23, 20);
            this.rdoChoseMethodDefault.Name = "rdoChoseMethodDefault";
            this.rdoChoseMethodDefault.Size = new System.Drawing.Size(47, 16);
            this.rdoChoseMethodDefault.TabIndex = 0;
            this.rdoChoseMethodDefault.TabStop = true;
            this.rdoChoseMethodDefault.Text = "默认";
            this.toolTip1.SetToolTip(this.rdoChoseMethodDefault, "按照阅览室设置的选座方式执行");
            this.rdoChoseMethodDefault.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rbPrint);
            this.groupBox6.Controls.Add(this.rbChangePrint);
            this.groupBox6.Controls.Add(this.rbNoPrint);
            this.groupBox6.Location = new System.Drawing.Point(280, 42);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(204, 45);
            this.groupBox6.TabIndex = 22;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "选座凭条";
            this.toolTip1.SetToolTip(this.groupBox6, "设置终端上的座位选择方式");
            // 
            // rbPrint
            // 
            this.rbPrint.AutoSize = true;
            this.rbPrint.Location = new System.Drawing.Point(71, 20);
            this.rbPrint.Name = "rbPrint";
            this.rbPrint.Size = new System.Drawing.Size(47, 16);
            this.rbPrint.TabIndex = 2;
            this.rbPrint.Text = "打印";
            this.toolTip1.SetToolTip(this.rbPrint, "系统给读者随机分配座位。");
            this.rbPrint.UseVisualStyleBackColor = true;
            // 
            // rbChangePrint
            // 
            this.rbChangePrint.AutoSize = true;
            this.rbChangePrint.Location = new System.Drawing.Point(124, 20);
            this.rbChangePrint.Name = "rbChangePrint";
            this.rbChangePrint.Size = new System.Drawing.Size(71, 16);
            this.rbChangePrint.TabIndex = 3;
            this.rbChangePrint.Text = "读者选择";
            this.toolTip1.SetToolTip(this.rbChangePrint, "显示两种选座方式，让读者自己选择。");
            this.rbChangePrint.UseVisualStyleBackColor = true;
            // 
            // rbNoPrint
            // 
            this.rbNoPrint.AutoSize = true;
            this.rbNoPrint.Location = new System.Drawing.Point(6, 20);
            this.rbNoPrint.Name = "rbNoPrint";
            this.rbNoPrint.Size = new System.Drawing.Size(59, 16);
            this.rbNoPrint.TabIndex = 1;
            this.rbNoPrint.Text = "不打印";
            this.toolTip1.SetToolTip(this.rbNoPrint, "读者可以通过座位布局图选择座位");
            this.rbNoPrint.UseVisualStyleBackColor = true;
            // 
            // rdb1280
            // 
            this.rdb1280.AutoSize = true;
            this.rdb1280.Location = new System.Drawing.Point(91, 18);
            this.rdb1280.Name = "rdb1280";
            this.rdb1280.Size = new System.Drawing.Size(71, 16);
            this.rdb1280.TabIndex = 2;
            this.rdb1280.Text = "1280*800";
            this.rdb1280.UseVisualStyleBackColor = true;
            // 
            // rdb1440
            // 
            this.rdb1440.AutoSize = true;
            this.rdb1440.Location = new System.Drawing.Point(9, 40);
            this.rdb1440.Name = "rdb1440";
            this.rdb1440.Size = new System.Drawing.Size(71, 16);
            this.rdb1440.TabIndex = 4;
            this.rdb1440.Text = "1440*900";
            this.rdb1440.UseVisualStyleBackColor = true;
            // 
            // rdb1920
            // 
            this.rdb1920.AutoSize = true;
            this.rdb1920.Location = new System.Drawing.Point(91, 40);
            this.rdb1920.Name = "rdb1920";
            this.rdb1920.Size = new System.Drawing.Size(77, 16);
            this.rdb1920.TabIndex = 5;
            this.rdb1920.Text = "1920*1080";
            this.rdb1920.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 387);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "终端设置程序";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoAutomaticMode;
        private System.Windows.Forms.RadioButton rdoManualMode;
        private System.Windows.Forms.RadioButton rdoChoseMethodDefault;
        private System.Windows.Forms.RadioButton rdoOptionalMode;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox ckbIsShowInitPOS;
        private System.Windows.Forms.CheckBox ckbActiveBespeakSeat;
        private System.Windows.Forms.CheckBox ckbOftenUsedSeat;
        private System.Windows.Forms.CheckBox ckbEnterNoForSeat;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstReadingRoomNums;
        private System.Windows.Forms.TextBox txtRooms;
        private System.Windows.Forms.Button btnAddRoomNums;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rdb1080;
        private System.Windows.Forms.RadioButton rdb1024;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTerminalNum;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTimes;
        private System.Windows.Forms.TextBox txtTimeLength;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetConfig;
        private System.Windows.Forms.Label lblResultMessage;
        private System.Windows.Forms.CheckBox cb_pos;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rbPrint;
        private System.Windows.Forms.RadioButton rbChangePrint;
        private System.Windows.Forms.RadioButton rbNoPrint;
        private System.Windows.Forms.RadioButton rdb1920;
        private System.Windows.Forms.RadioButton rdb1440;
        private System.Windows.Forms.RadioButton rdb1280;

    }
}

