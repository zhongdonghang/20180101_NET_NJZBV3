using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeatManage.SeatManageComm;
using System.Threading;
using SeatManage.ClassModel;
using SeatClient.Config;
using System.Diagnostics;
using System.IO;

namespace SeatManage.SeatClient.Config
{
    public partial class SeatManageConfigTool : Form
    {
        public SeatManageConfigTool()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 数据加密
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_pw_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ConnString.Text))
            {
                MessageBox.Show("输入的字符串不能为空！");
                return;
            }
            try
            {
                this.txt_Connstring_PW.Text = AESAlgorithm.AESEncrypt(this.txt_ConnString.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("加密出错：{0}", ex.Message));
            }
        }

        private void SeatManageConfigTool_Load(object sender, EventArgs e)
        {
            Code.HostConfig hostconfig = new Code.HostConfig();
            Code.ClientBasicConfig clientconfig = new Code.ClientBasicConfig();
            Code.CardReaderBasicConfig cardReaderConfig = new Code.CardReaderBasicConfig();
            Code.WebConfigSetting webConfigSeeting = new Code.WebConfigSetting();
            Code.LeaveClientBesicConfig leaveConfigSetting = new Code.LeaveClientBesicConfig();
            Code.DeviceSettingConfig deviceSettingConfig = new Code.DeviceSettingConfig();
            Code.ShutDownConfig shutDownConfig = new Code.ShutDownConfig();
            if (Code.ReadSeatHostConfigV3.ReadConfig(ref hostconfig))
            {
                foreach (string server in hostconfig.HostServer)
                {
                    if (server == "WcfHost")
                    {
                        cb_WCF.Checked = true;
                    }
                    else if (server == "MonitorService")
                    {
                        cb_Watch.Checked = true;
                    }
                    else if (server == "DataTransferService")
                    {
                        cb_DT.Checked = true;
                    }
                }
                txt_DBIP.Text = hostconfig.DBIP;
                txt_DBName.Text = hostconfig.DBName;
                txt_DBpw.Text = hostconfig.DBPW;
                txt_DBUser.Text = hostconfig.DBUser;
                txt_SeverWCFConnString.Text = hostconfig.WCFString;
                txt_weixinendportwcf.Text = hostconfig.WeChatWCFString;
                txt_FilePath.Text = hostconfig.MediaFilePath;
                txt_ServerSchoolNo.Text = hostconfig.SchoolNo;
                txt_ServerLoopTime.Text = hostconfig.LoopTime;
                txt_Uploadtime.Text = hostconfig.UploadTime;
                if (Code.WebConfig.ReadConfig(ref webConfigSeeting))
                {
                    cb_web_pw_change.Checked = webConfigSeeting.IsChangePW;
                }
                else
                {
                    MessageBox.Show("无法自动获取管理网站配置文件，请确保和Host服务文件夹放置在同一目录，并且文件夹为“SeatManageWebV2”");
                    gb_web.Enabled = false;
                }
                foreach (Control c in tabControl1.TabPages[1].Controls)
                {
                    c.Enabled = false;
                }
                foreach (Control c in tabControl1.TabPages[2].Controls)
                {
                    c.Enabled = false;
                }
                foreach (Control c in tabControl1.TabPages[3].Controls)
                {
                    c.Enabled = false;
                }
            }
            else if (Code.ReadSeatClientConfig.GetSeatClientBaseConfig(ref clientconfig) && Code.CardReaderConfig.GetCardReaderBaseConfig(ref cardReaderConfig, "Select"))
            {
                clientmode = "Select";
                txt_clientwcfstring.Text = clientconfig.WCFConnString;
                txt_schoolno.Text = clientconfig.SchoolNo;
                txt_campusno.Text = clientconfig.CampusNo;
                txt_deviceno.Text = clientconfig.TerminalNum;
                txt_defualtmedia.Text = clientconfig.DefaultMedia;
                txt_adloop.Text = clientconfig.SCLoopTime;
                txt_sentloop.Text = clientconfig.SentStatusTime;
                txt_update.Text = clientconfig.UpdateTime;
                txt_mac.Text = GetMacAddress.GetLocalAddress()[0];
                gb_xzx.Enabled = false;
                gb_mh.Enabled = false;
                gb_fk.Enabled = false;
                gb_gz.Enabled = false;
                gb_cut.Enabled = false;
                gb_dz.Enabled = false;

                switch (cardReaderConfig.CardReaderTye)
                {
                    case 0: rb_mh.Checked = true; break;
                    case 1: rb_xzx.Checked = true; break;
                    case 2: rb_fk.Checked = true; break;
                    case 3: rb_gz.Checked = true; break;
                    case 4: rb_cut.Checked = true; break;
                    case 5: rb_dz.Checked = true; break;
                }
                if (cardReaderConfig.CardID10Or16 == 10)
                {
                    fk_10.Checked = true;
                    rb_10.Checked = true;
                }
                else
                {
                    fk_16.Checked = true;
                    rb_16.Checked = true;
                }
                cb_Isbeep.Checked = cardReaderConfig.IsBeep;
                fk_port.Text = cardReaderConfig.FKport;
                txt_xzx_ip.Text = cardReaderConfig.XZX_ServerEndPort;
                txt_xzx_sys.Text = cardReaderConfig.XZX_SysCode;
                txt_xzx_tre.Text = cardReaderConfig.XZX_TerminalNo;
                xzx_addreader.Checked = cardReaderConfig.XZX_AddReader;
                xzx_off.Checked = cardReaderConfig.XZX_Offline;
                if (cardReaderConfig.Hook_isCardNo)
                {
                    rb_gz_cardno.Checked = true;
                }
                else
                {
                    rb_gz_cardid.Checked = true;
                }
                if (cardReaderConfig.XZX_IsOnelyReaderCardId)
                {
                    rb_xzx_readcardid.Checked = true;
                }
                else
                {
                    rb_xzx_readcardno.Checked = true;
                }
                cb_change.Checked = cardReaderConfig.CardIDIsChange;
                fk_change.Checked = cardReaderConfig.CardIDIsChange;
                cb_add0.Checked = cardReaderConfig.IsAdd0;
                fk_add0.Checked = cardReaderConfig.IsAdd0;

                if (Code.DeviceSetting.GetDeviceSetting(ref deviceSettingConfig))
                {
                    txt_d_IP.Text = deviceSettingConfig.IP;
                    txt_d_dns.Text = deviceSettingConfig.DNS;
                    txt_d_getway.Text = deviceSettingConfig.Gateway;
                    txt_d_mask.Text = deviceSettingConfig.Mask;
                    txt_pc_name.Text = deviceSettingConfig.PCName;
                    cb_staticIP.Checked = deviceSettingConfig.IsStaticIP;
                }
                if (Code.DeviceSetting.GetShotDownSetting(ref shutDownConfig))
                {
                    cb_sd.Checked = shutDownConfig.IsUsed;
                    txt_sd_h.Text = shutDownConfig.ShutDownHour;
                    txt_sd_m.Text = shutDownConfig.ShutDownMin;
                    txt_sd_s.Text = shutDownConfig.ShutDownWaitSec;
                }
                foreach (Control c in tabControl1.TabPages[0].Controls)
                {
                    c.Enabled = false;
                }
                foreach (Control c in tabControl1.TabPages[2].Controls)
                {
                    c.Enabled = false;
                }
                tabControl1.SelectedIndex = 1;
            }
            else if (Code.LeaveClientConfig.GetLeaveClientConfig(ref leaveConfigSetting) && Code.CardReaderConfig.GetCardReaderBaseConfig(ref cardReaderConfig, "Leave"))
            {
                clientmode = "Leave";
                txt_leave_wcfconn.Text = leaveConfigSetting.WCFConnString;
                switch (leaveConfigSetting.LeaveMode)
                {
                    case "0": rb_leave_s.Checked = true; break;
                    case "1": rb_leave_sl.Checked = true; break;
                    case "2": rb_leave_l.Checked = true; break;
                    case "3": rb_leave_s.Checked = true; break;
                }
                if (leaveConfigSetting.SetUpMode == "1")
                {
                    rb_win_max.Checked = true;
                }
                else
                {
                    rb_win_min.Checked = true;
                }
                gb_xzx.Enabled = false;
                gb_mh.Enabled = false;
                gb_fk.Enabled = false;
                gb_gz.Enabled = false;
                gb_cut.Enabled = false;
                gb_dz.Enabled = false;

                switch (cardReaderConfig.CardReaderTye)
                {
                    case 0: rb_mh.Checked = true; break;
                    case 1: rb_xzx.Checked = true; break;
                    case 2: rb_fk.Checked = true; break;
                    case 3: rb_gz.Checked = true; break;
                    case 4: rb_cut.Checked = true; break;
                    case 5: rb_dz.Checked = true; break;
                }
                if (cardReaderConfig.CardID10Or16 == 10)
                {
                    fk_10.Checked = true;
                    rb_10.Checked = true;
                }
                else
                {
                    fk_16.Checked = true;
                    rb_16.Checked = true;
                }
                cb_Isbeep.Checked = cardReaderConfig.IsBeep;
                fk_port.Text = cardReaderConfig.FKport;
                txt_xzx_ip.Text = cardReaderConfig.XZX_ServerEndPort;
                txt_xzx_sys.Text = cardReaderConfig.XZX_SysCode;
                txt_xzx_tre.Text = cardReaderConfig.XZX_TerminalNo;
                xzx_addreader.Checked = cardReaderConfig.XZX_AddReader;
                xzx_off.Checked = cardReaderConfig.XZX_Offline;
                if (cardReaderConfig.Hook_isCardNo)
                {
                    rb_gz_cardno.Checked = true;
                }
                else
                {
                    rb_gz_cardid.Checked = true;
                }
                if (cardReaderConfig.XZX_IsOnelyReaderCardId)
                {
                    rb_xzx_readcardid.Checked = true;
                }
                else
                {
                    rb_xzx_readcardno.Checked = true;
                }
                cb_change.Checked = cardReaderConfig.CardIDIsChange;
                fk_change.Checked = cardReaderConfig.CardIDIsChange;
                cb_add0.Checked = cardReaderConfig.IsAdd0;
                fk_add0.Checked = cardReaderConfig.IsAdd0;
                foreach (Control c in tabControl1.TabPages[0].Controls)
                {
                    c.Enabled = false;
                }
                foreach (Control c in tabControl1.TabPages[1].Controls)
                {
                    c.Enabled = false;
                }
                tabControl1.SelectedIndex = 2;
            }
            else
            {
                MessageBox.Show("获取配置文件失败，请把配置工具拷贝到宿主服务或者终端的根目录下,再次尝试！");
                foreach (Control c in tabControl1.TabPages[0].Controls)
                {
                    c.Enabled = false;
                }
                foreach (Control c in tabControl1.TabPages[1].Controls)
                {
                    c.Enabled = false;
                }
                foreach (Control c in tabControl1.TabPages[2].Controls)
                {
                    c.Enabled = false;
                }
                foreach (Control c in tabControl1.TabPages[3].Controls)
                {
                    c.Enabled = false;
                }
                btnBackupProgram.Enabled = false;
                tabControl1.SelectedIndex = 4;
            }
        }
        /// <summary>
        /// 服务器配置保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            Code.HostConfig hostconfig = new Code.HostConfig();
            if (cb_WCF.Checked)
            {
                string server = "WcfHost";
                hostconfig.HostServer.Add(server);
            }
            if (cb_Watch.Checked)
            {
                string server = "MonitorService";
                hostconfig.HostServer.Add(server);
            }
            if (cb_DT.Checked)
            {
                string server = "DataTransferService";
                hostconfig.HostServer.Add(server);
            }
            hostconfig.DBIP = txt_DBIP.Text.Trim();
            hostconfig.DBName = txt_DBName.Text.Trim();
            hostconfig.DBPW = txt_DBpw.Text.Trim();
            hostconfig.DBUser = txt_DBUser.Text.Trim();
            hostconfig.WCFString = txt_SeverWCFConnString.Text.Trim();
            hostconfig.WeChatWCFString = txt_weixinendportwcf.Text.Trim();
            hostconfig.MediaFilePath = txt_FilePath.Text.Trim();
            hostconfig.SchoolNo = txt_ServerSchoolNo.Text.Trim();
            hostconfig.LoopTime = txt_ServerLoopTime.Text.Trim();
            hostconfig.UploadTime = txt_Uploadtime.Text.Trim();
            if (Code.ReadSeatHostConfigV3.SaveConfig(hostconfig))
            {
                MessageBox.Show("保存成功！");
                if (gb_web.Enabled)
                {
                    Code.WebConfigSetting webSetting = new Code.WebConfigSetting();
                    webSetting.IsChangePW = cb_web_pw_change.Checked;
                    webSetting.ConnString = txt_SeverWCFConnString.Text.Trim();
                    webSetting.SchoolNo= txt_ServerSchoolNo.Text.Trim();
                    webSetting.SqlConn = "Data Source=" + hostconfig.DBIP + ";Initial Catalog=" + hostconfig.DBName + ";Persist Security Info=True" + ";User ID=" + hostconfig.DBUser + ";Password=" + hostconfig.DBPW;
                    if (!Code.WebConfig.SaveConfig(webSetting))
                    {
                        MessageBox.Show("管理网站设置保存失败！请手动修改！");
                    }
                }
            }
            else
            {
                MessageBox.Show("保存失败！");
            }
            //修改预约记录
            if (colbtnclickCount >= 5)
            {
                string connstr_new = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", txt_DBIP.Text, txt_DBName.Text, txt_DBUser.Text, txt_DBpw.Text);
                Code.AddBookKey bk = new Code.AddBookKey();
                bk.BookKeyon(connstr_new);
            }

        }

        private void btnBackupProgram_Click(object sender, EventArgs e)
        {
            try
            {
                btnBackupProgram.Enabled = false;
                Code.ProgramBackup backHandler = new Code.ProgramBackup();
                backHandler.BackupFiled += new Code.EventHanleBackup(backHandler_BackupFiled);
                backHandler.BackupOver += new EventHandler(backHandler_BackupOver);
                backHandler.Progress += new Code.EventHanleBackup(backHandler_Progress);
                progressBar1.Visible = true;
                myThread = new Thread(new ThreadStart(backHandler.Backup));
                myThread.Start();
            }
            catch (Exception ex)
            {
                lblBackingFileName.Text = ex.Message;
            }
        }
        Thread myThread = null;
        //备份进度
        void backHandler_Progress(Code.BackupProgressInfo arge)
        {
            this.Invoke(new Action(() =>
                {
                    progressBar1.Value = arge.Progress;
                    lblBackingFileName.Text = arge.UpdateFileName;
                    lblProgramName.Text = arge.ProgramName;
                    iSuccess += 1;
                }));
        }
        //备份完成
        void backHandler_BackupOver(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                btnBackupProgram.Enabled = true;
                lblBackingFileName.Text = string.Format("备份完成，总共上传{0}个文件，错误{1}个文件", iSuccess, iError);
            }));
        }
        int iSuccess = 0;
        int iError = 0;
        //备份失败
        void backHandler_BackupFiled(Code.BackupProgressInfo arge)
        {
            this.Invoke(new Action(() =>
            {
                lblErrorMessage.Text = arge.Message;
                iError += 1;
            }));
        }
        //终端设置备份
        private void button6_Click(object sender, EventArgs e)
        {
            Code.ClientBasicConfig clientconfig = new Code.ClientBasicConfig();
            clientconfig.WCFConnString = txt_clientwcfstring.Text.Trim();
            clientconfig.SchoolNo = txt_schoolno.Text.Trim();
            clientconfig.CampusNo = txt_campusno.Text.Trim();
            clientconfig.TerminalNum = txt_deviceno.Text.Trim();
            clientconfig.DefaultMedia = txt_defualtmedia.Text.Trim();
            clientconfig.SCLoopTime = txt_adloop.Text.Trim();
            clientconfig.SentStatusTime = txt_sentloop.Text.Trim();
            clientconfig.UpdateTime = txt_update.Text.Trim();

            if (Code.ReadSeatClientConfig.SaveConfig(clientconfig))
            {
                MessageBox.Show("保存成功！");
            }
            else
            {
                MessageBox.Show("保存失败！");
            }
        }
        /// <summary>
        /// Mac绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            TerminalInfoV2 terminal = SeatManage.Bll.TerminalOperatorService.GetTeminalSetting(txt_deviceno.Text.Trim());
            if (terminal == null)
            {
                MessageBox.Show("绑定失败，请保证设备编号正确！");
                return;
            }
            terminal.TerminalMacAddress = txt_mac.Text.Trim();
            if (SeatManage.Bll.TerminalOperatorService.UpdateTeminalSetting(terminal) == "")
            {
                MessageBox.Show("绑定成功");
            }
            else
            {
                MessageBox.Show("绑定失败，请检查网络连接！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Code.ClientBasicConfig clientconfig = new Code.ClientBasicConfig();
            clientconfig.WCFConnString = txt_clientwcfstring.Text.Trim();
            clientconfig.SchoolNo = txt_schoolno.Text.Trim();
            clientconfig.CampusNo = txt_campusno.Text.Trim();
            clientconfig.TerminalNum = txt_deviceno.Text.Trim();
            clientconfig.DefaultMedia = txt_defualtmedia.Text.Trim();
            clientconfig.SCLoopTime = txt_adloop.Text.Trim();
            clientconfig.SentStatusTime = txt_sentloop.Text.Trim();
            clientconfig.UpdateTime = txt_update.Text.Trim();
            Form1 f = new Form1(clientconfig);
            f.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Connstring_PW.Text))
            {
                MessageBox.Show("输入的密文不能为空！");
                return;
            }
            try
            {
                this.txt_ConnString.Text = AESAlgorithm.AESDecrypt(this.txt_Connstring_PW.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("解密出错：{0}", ex.Message));
            }
        }

        private void rb_mh_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_mh.Checked)
            {
                gb_mh.Enabled = true;
            }
            else
            {
                gb_mh.Enabled = false;
            }
        }

        private void rb_xzx_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_xzx.Checked)
            {
                gb_xzx.Enabled = true;
            }
            else
            {
                gb_xzx.Enabled = false;
            }
        }

        private void rb_gz_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_gz.Checked)
            {
                gb_gz.Enabled = true;
            }
            else
            {
                gb_gz.Enabled = false;
            }
        }

        private void rb_fk_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_fk.Checked)
            {
                gb_fk.Enabled = true;
            }
            else
            {
                gb_fk.Enabled = false;
            }
        }
        private void rb_cut_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_cut.Checked)
            {
                gb_cut.Enabled = true;
            }
            else
            {
                gb_cut.Enabled = false;
            }
        }
        /// <summary>
        /// 旧数据同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            string connstr_old = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", txt_18_IP.Text, txt_18_name.Text, txt_18_sa.Text, txt_18_pw.Text);
            string connstr_new = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", txt_DBIP.Text, txt_DBName.Text, txt_DBUser.Text, txt_DBpw.Text);
            Code.SyncOldData sod = new Code.SyncOldData(connstr_old, connstr_new);
            sod.Progress += new Code.SyncOldData.EventHanleProgress(sod_Progress);
            myThread = new Thread(new ThreadStart(sod.Update));
            myThread.Start();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Code.CardReaderBasicConfig config = new Code.CardReaderBasicConfig();
            if (rb_mh.Checked)
            {
                if (rb_10.Checked)
                {
                    config.CardID10Or16 = 10;
                }
                else
                {
                    config.CardID10Or16 = 16;
                }
                config.CardIDIsChange = cb_change.Checked;
                config.IsAdd0 = cb_add0.Checked;
            }

            else if (rb_fk.Checked)
            {
                if (fk_10.Checked)
                {
                    config.CardID10Or16 = 10;
                }
                else
                {
                    config.CardID10Or16 = 16;
                }
                config.CardIDIsChange = fk_change.Checked;
                config.IsAdd0 = fk_add0.Checked;
            }
            config.IsBeep = cb_Isbeep.Checked;
            config.FKport = fk_port.Text;
            config.XZX_Offline = xzx_off.Checked;
            config.XZX_ServerEndPort = txt_xzx_ip.Text;
            config.XZX_SysCode = txt_xzx_sys.Text;
            config.XZX_TerminalNo = txt_xzx_tre.Text;
            config.XZX_AddReader = xzx_addreader.Checked;
            if (rb_xzx_readcardid.Checked)
            {
                config.XZX_IsOnelyReaderCardId = true;
            }
            else
            {
                config.XZX_IsOnelyReaderCardId = false;
            }
            if (rb_gz_cardid.Checked)
            {
                config.Hook_isCardNo = false;
            }
            else
            {
                config.Hook_isCardNo = true;
            }
            if (rb_mh.Checked)
            {
                config.CardReaderTye = 0;
            }
            else if (rb_xzx.Checked)
            {
                config.CardReaderTye = 1;
            }
            else if (rb_fk.Checked)
            {
                config.CardReaderTye = 2;
            }
            else if (rb_gz.Checked)
            {
                config.CardReaderTye = 3;
            }
            else if (rb_cut.Checked)
            {
                config.CardReaderTye = 4;
            }
            else if (rb_dz.Checked)
            {
                config.CardReaderTye = 5;
            }
            if (Code.CardReaderConfig.SaveConfig(config, clientmode))
            {
                MessageBox.Show("保存成功！");
            }
            else
            {
                MessageBox.Show("保存失败！");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 导入进出记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            string connstr_old = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", txt_18_IP.Text, txt_18_name.Text, txt_18_sa.Text, txt_18_pw.Text);
            string connstr_new = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", txt_DBIP.Text, txt_DBName.Text, txt_DBUser.Text, txt_DBpw.Text);
            Code.SyncOldData sod = new Code.SyncOldData(connstr_old, connstr_new);
            sod.Progress += new Code.SyncOldData.EventHanleProgress(sod_Progress);
            myThread = new Thread(new ThreadStart(sod.GetOldEOlog));
            myThread.Start();
        }
        void sod_Progress(string message)
        {
            this.Invoke(new Action(() =>
            {
                rtu_syncmessage.Text = message;
            }));
        }
        /// <summary>
        /// 批处理脚本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, EventArgs e)
        {
            Code.batSQLFiles bat = new Code.batSQLFiles(txt_DBIP.Text, txt_DBName.Text, txt_DBUser.Text, txt_DBpw.Text, txt_sqlpath.Text);
            string info = bat.run();
            MessageBox.Show(info);
            if (info == "创建成功！")
            {
                Process.Start("explorer.exe", txt_sqlpath.Text + "SQL批处理脚本\\");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string connstr_old = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", txt_18_IP.Text, txt_18_name.Text, txt_18_sa.Text, txt_18_pw.Text);
            string connstr_new = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", txt_DBIP.Text, txt_DBName.Text, txt_DBUser.Text, txt_DBpw.Text);
            Code.SyncOldData sod = new Code.SyncOldData(connstr_old, connstr_new);
            sod.Progress += new Code.SyncOldData.EventHanleProgress(sod_Progress);
            myThread = new Thread(new ThreadStart(sod.BookUserData));
            myThread.Start();
        }

        private void txt_SeverWCFConnString_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_SeverWCFConnString.Text))
            {
                txt_SeverWCFConnString_PW.Text = AESAlgorithm.AESEncrypt(txt_SeverWCFConnString.Text);
            }
        }
        string MediaType = "Null";
        /// <summary>
        /// 选择媒体路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button16_Click(object sender, EventArgs e)
        {
            MediaType = "Null";
            System.Windows.Forms.FolderBrowserDialog foldBrowerDialog = new System.Windows.Forms.FolderBrowserDialog();
            foldBrowerDialog.ShowDialog();
            if (!string.IsNullOrEmpty(foldBrowerDialog.SelectedPath))
            {
                txt_UpmediaPath.Text = foldBrowerDialog.SelectedPath + "\\";
            }
            if (!string.IsNullOrEmpty(txt_UpmediaPath.Text))
            {
                if (Directory.Exists(txt_UpmediaPath.Text))
                {
                    string[] filename = Directory.GetFiles(txt_UpmediaPath.Text);
                    foreach (string name in filename)
                    {
                        if (name.Substring(name.LastIndexOf("\\") + 1) == "SlipCustomerList.xml")
                        {
                            rtx_uploadmessage.Text = "此文件夹中的是优惠券！请点击右边按钮进行上传！";
                            MediaType = "SlipCustomer";
                            break;
                        }
                        else if (name.Substring(name.LastIndexOf("\\") + 1) == "playList.xml")
                        {
                            rtx_uploadmessage.Text = "此文件夹中的是播放列表！请点击右边按钮进行上传！";
                            MediaType = "PlayList";
                            break;
                        }
                        else if (name.Substring(name.LastIndexOf("\\") + 1) == "TemplateInfo.xml")
                        {
                            rtx_uploadmessage.Text = "此文件夹中的是打印凭条！请点击右边按钮进行上传！";
                            MediaType = "PrintTemplate";
                            break;
                        }
                    }
                    if (MediaType == "Null")
                    {
                        rtx_uploadmessage.Text = "此文件夹不包含媒体信息，请重新选择！";
                    }
                }
                else
                {
                    rtx_uploadmessage.Text = "此文件夹不存在！";
                }
            }
        }

        //上传媒体文件
        private void button17_Click(object sender, EventArgs e)
        {
            Code.MediaFilesUpload MediaUpload = new Code.MediaFilesUpload(txt_UpmediaPath.Text, MediaType);
            MediaUpload.Progress += new Code.MediaFilesUpload.EventHanleProgress(MediaUpload_Progress);
            myThread = new Thread(new ThreadStart(MediaUpload.Upload));
            myThread.Start();
        }

        void MediaUpload_Progress(string message)
        {
            this.Invoke(new Action(() =>
            {
                rtx_uploadmessage.Text = rtx_uploadmessage.Text + "\n" + message;
                rtx_uploadmessage.SelectionStart = rtx_uploadmessage.Text.Length;
                rtx_uploadmessage.ScrollToCaret();
            }));
        }

        private void button18_Click(object sender, EventArgs e)
        {
            string connstr_new = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", txt_DBIP.Text, txt_DBName.Text, txt_DBUser.Text, txt_DBpw.Text);
            Code.DataStatistics ds = new Code.DataStatistics(connstr_new);
            ds.Progress += new Code.DataStatistics.EventHanleProgress(ds_Progress);
            myThread = new Thread(new ThreadStart(ds.StartStatistics));
            myThread.Start();
        }

        void ds_Progress(string message)
        {
            this.Invoke(new Action(() =>
            {
                rtx_str.Text = message;
                //rtx_str.SelectionStart = rtx_str.Text.Length;
                //rtx_str.ScrollToCaret();
            }));
        }
        string clientmode = "";
        private void button20_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Code.LeaveClientBesicConfig leaveConfigSetting = new Code.LeaveClientBesicConfig();
            leaveConfigSetting.WCFConnString = txt_leave_wcfconn.Text;
            if (rb_leave_s.Checked)
            {
                leaveConfigSetting.LeaveMode = "0";
            }
            if (rb_leave_sl.Checked)
            {
                leaveConfigSetting.LeaveMode = "1";
            }
            if (rb_leave_l.Checked)
            {
                leaveConfigSetting.LeaveMode = "2";
            }
            if (rb_leave_ct.Checked)
            {
                leaveConfigSetting.LeaveMode = "3";
            }
            if (rb_win_max.Checked)
            {
                leaveConfigSetting.SetUpMode = "1";
            }
            else
            {
                leaveConfigSetting.SetUpMode = "0";
            }

            if (Code.LeaveClientConfig.SaveLeaveClientConfig(leaveConfigSetting))
            {
                MessageBox.Show("保存成功！");
            }
            else
            {
                MessageBox.Show("保存失败！");
            }
        }

        private void cb_staticIP_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_staticIP.Checked)
            {
                txt_d_IP.Enabled = true;
                txt_d_dns.Enabled = true;
                txt_d_getway.Enabled = true;
                txt_d_mask.Enabled = true;
            }
            else
            {
                txt_d_IP.Enabled = false;
                txt_d_dns.Enabled = false;
                txt_d_getway.Enabled = false;
                txt_d_mask.Enabled = false;
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Code.DeviceSettingConfig config = new Code.DeviceSettingConfig();
            config.IP = txt_d_IP.Text;
            config.DNS = txt_d_dns.Text;
            config.Gateway = txt_d_getway.Text;
            config.Mask = txt_d_mask.Text;
            config.PCName = txt_pc_name.Text;
            config.IsStaticIP = cb_staticIP.Checked;
            if (Code.DeviceSetting.SaveDeviceSetting(config))
            {
                MessageBox.Show("设置成功！");
            }
            else
            {
                MessageBox.Show("设置失败！");
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Code.ShutDownConfig config = new Code.ShutDownConfig();
            config.IsUsed = cb_sd.Checked;
            config.ShutDownHour = txt_sd_h.Text;
            config.ShutDownMin = txt_sd_m.Text;
            config.ShutDownWaitSec = txt_sd_s.Text;
            if (Code.DeviceSetting.CreateShutDown(config))
            {
                MessageBox.Show("设置成功！");
            }
            else
            {
                MessageBox.Show("设置失败！");
            }
        }

        private void cb_sd_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_sd.Checked)
            {
                txt_sd_h.Enabled = true;
                txt_sd_m.Enabled = true;
                txt_sd_s.Enabled = true;
            }
            else
            {
                txt_sd_h.Enabled = false;
                txt_sd_m.Enabled = false;
                txt_sd_s.Enabled = false;
            }
        }

        private void rb_dz_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_dz.Checked)
            {
                gb_dz.Enabled = true;
            }
            else
            {
                gb_dz.Enabled = false;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (Code.DeviceSetting.SetAppBarAutoDisplay(true))
            {
                DialogResult MsgBoxResult;
                MsgBoxResult = MessageBox.Show("设置成功！是否立即重启终端？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (MsgBoxResult == DialogResult.Yes)
                {
                    ProcessStartInfo ps = new ProcessStartInfo();
                    ps.FileName = "shutdown.exe";
                    ps.Arguments = "-r -t 5";
                    Process.Start(ps);
                }
            }
            else
            {
                MessageBox.Show("设置失败！");
            }

        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (Code.DeviceSetting.SetAppBarAutoDisplay(false))
            {
                MessageBox.Show("设置成功！");
            }
            else
            {
                MessageBox.Show("设置失败！");
            }
        }

        private void txt_deviceno_TextChanged(object sender, EventArgs e)
        {
            if (txt_deviceno.Text.Length >= 2)
            {
                txt_pc_name.Text = "Seat" + txt_deviceno.Text.Substring(txt_deviceno.Text.Length - 2);
            }
        }
        int colbtnclickCount = 0;
        private void label65_Click(object sender, EventArgs e)
        {
            colbtnclickCount++;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Code.ProgramBackup backHandler = new Code.ProgramBackup();
            MessageBox.Show(backHandler.SeatFileNoRead());
        }

        private void button_SyncUser_Click(object sender, EventArgs e)
        {
            string connstr_new = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", txt_DBIP.Text, txt_DBName.Text, txt_DBUser.Text, txt_DBpw.Text);
            Code.SyncUserAll ds = new Code.SyncUserAll(connstr_new);
            ds.Progress += new Code.SyncUserAll.EventHanleProgress(ds_Progress_user);
            myThread = new Thread(new ThreadStart(ds.Run));
            myThread.Start();
        }
        void ds_Progress_user(string message)
        {
            this.Invoke(new Action(() =>
            {

                richTextBox_syncUser.Text = message;
            }));
        }

        private void cb_Watch_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button26_Click(object sender, EventArgs e)
        {
            Code.DeviceSetting.SetAppBarAutoDisplay(false);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            Code.DeviceSetting.SetAppBarAutoDisplay(true);
        }

        private void txt_weixinendportwcf_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_weixinendportwcf.Text))
            {
                txt_weixinepPW.Text = AESAlgorithm.AESEncrypt(txt_weixinendportwcf.Text);
            }
        }
    }
}
