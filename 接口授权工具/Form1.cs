using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AuthorizeVerify;

namespace 接口授权工具
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 存放授权对象
        /// </summary>
        private Dictionary<string, ServiceAuthorize> authorizeInfos = new Dictionary<string, ServiceAuthorize>();

        /// <summary>
        /// 当前选中的用户授权信息
        /// </summary>
        private ServiceAuthorize authorizes;
        string authorized_keys_filePath = string.Format(@"{0}authorized\ws_authorized_keys", AppDomain.CurrentDomain.BaseDirectory);
        string sPath = string.Format(@"{0}authorized\", AppDomain.CurrentDomain.BaseDirectory);
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
            loadAuthorizeInfos();
        }
        /// <summary>
        /// 加载授权文件并显示授权信息
        /// </summary>
        private void loadAuthorizeInfos()
        {
            if (File.Exists(authorized_keys_filePath))
            {
                authorizeInfos = AuthorizeVerify.ServiceAuthorize.AnalyzeAuthorize(authorized_keys_filePath);
                comboBox1.Items.Clear();
                foreach (string author in authorizeInfos.Keys)
                {
                    comboBox1.Items.Add(author);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                //如果当前已经做了权限设置，先保存当前设置的内容。
                if (authorizes == null)
                {
                    authorizes = new ServiceAuthorize();
                }
                authorizes.UserName = textBox1.Text.Trim();
                authorizes.UserPwd = textBox2.Text.Trim();
                if (!string.IsNullOrEmpty(authorizes.UserName) && !string.IsNullOrEmpty(authorizes.UserPwd))
                {
                    authorizes.ServiceAuthorizeItems = getGetReaderInfoService();
                    if (authorizeInfos.ContainsKey(authorizes.UserName))
                    {
                        authorizeInfos[authorizes.UserName] = authorizes;
                    }
                    else
                    {
                        authorizeInfos.Add(authorizes.UserName, authorizes);
                    }
                }


                string selectUserName = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                if (authorizeInfos.ContainsKey(selectUserName))
                {
                    AuthorizeEnabled(true);
                    initCheckBoxs();//初始化UI上的Checkbox
                    authorizes = authorizeInfos[selectUserName];//获取下拉列表中的授权
                    bindAutorizedInfo(authorizes);//绑定授权信息
                }
                else
                {
                    AuthorizeEnabled(false);
                }
            }
            else
            {
                AuthorizeEnabled(false);
            }
        }
        /// <summary>
        /// 显示授权信息
        /// </summary>
        /// <param name="serviceAuthorizes"></param>
        private void bindAutorizedInfo(ServiceAuthorize serviceAuthorizes)
        {
            textBox1.Text = serviceAuthorizes.UserName;
            textBox2.Text = serviceAuthorizes.UserPwd;
            foreach (AuthorizeVerify.ServiceAuthorizeItem authorizeItem in serviceAuthorizes.ServiceAuthorizeItems)
            {
                switch (authorizeItem.ServiceName)
                {
                    case "GetReaderInfoService":
                        ckbReaderService.Checked = true;
                        showReaderServiceAuthorize(authorizeItem.AllowMethodName);
                        break;
                    case "GetReadingRoomInfoService":
                        ckbRoomService.Checked = true;
                        showGetReadingRoomInfoServiceAuthorize(authorizeItem.AllowMethodName);
                        break;
                    case "DelaySeatUsedTimeService":
                        ckbDelaySeatUsedTimeService.Checked = true;
                        showDelaySeatUsedTimeService(authorizeItem.AllowMethodName);
                        break;
                    case "ShortLeaveService":
                        ckbShortLeaveService.Checked = true;
                        showShortLeaveService(authorizeItem.AllowMethodName);
                        break;
                    case "BespeakSeatService":
                        ckbBespeakService.Checked = true;
                        showBespeakSeatService(authorizeItem.AllowMethodName);
                        break;
                    case "ChooseSeatService":
                        ckbChooseSeatService.Checked = true;
                        showChooseSeatService(authorizeItem.AllowMethodName);
                        break;
                    case "FreeSeatService":
                        ckbFreeSeatService.Checked = true;
                        showFreeSeatService(authorizeItem.AllowMethodName);
                        break;
                    case "Other":
                        showOtherAuthorize(authorizeItem.AllowMethodName);
                        break;
                }
            }
        }
        /// <summary>
        /// 释放座位服务的授权
        /// </summary>
        /// <param name="methods"></param>
        private void showFreeSeatService(List<string> methods)
        {
            for (int i = 0; i < methods.Count; i++)
            {
                switch (methods[i])
                {
                    case "FreeSeat":
                        ckbFreeSeat.Checked = true;
                        break;
                    case "GetBaseReaderInfo":
                        ckbGetBaseReaderInfo_freeSeat.Checked = true;
                        break;
                    case "GetReaderActualTimeRecord":
                        ckbGetReaderActualTimeRecord_freeSeat.Checked = true;
                        break;
                }
            }
        }
        /// <summary>
        /// 显示选座服务的授权信息
        /// </summary>
        /// <param name="methods"></param>
        private void showChooseSeatService(List<string> methods)
        {
            for (int i = 0; i < methods.Count; i++)
            {
                switch (methods[i])
                {
                    case "GetAllReadingRoomBaseInfo":
                        ckbGetAllReadingRoomBaseInfo_chooseSeat.Checked = true;
                        break;
                    case "GetReadingRoomSetInfoByRoomNum":
                        ckbGetReadingRoomSetInfoByRoomNum_chooseSeat.Checked = true;
                        break;
                    case "GetBaseReaderInfo":
                        ckbGetBaseReaderInfo_chooseSeat.Checked = true;
                        break;
                    case "GetReaderBlacklistRecord":
                        ckbGetReaderBlacklistRecord_chooseSeat.Checked = true;
                        break;
                    case "GetReaderActualTimeRecord":
                        ckbGetReaderActualTimeRecord_chooseSeat.Checked = true;
                        break;
                    case "GetReaderChooseSeatRecord":
                        ckbGetReaderChooseSeatRecord_chooseSeat.Checked = true;
                        break;
                    case "GetSeatsUsedInfoByRoomNum":
                        ckbGetSeatsUsedInfoByRoomNum_chooseSeat.Checked = true;
                        break;
                    case "VerifyCanDoIt":
                        ckbVerifyCanDoIt_chooseSeat.Checked = true;
                        break;
                    case "SeatLock":
                        ckbSeatLock_chooseSeat.Checked = true;
                        break;
                    case "SubmitChooseResult":
                        ckbSubmitChooseResult.Checked = true;
                        break;
                }
            }
        }
        /// <summary>
        /// 显示预约服务的授权信息
        /// </summary>
        /// <param name="methods"></param>
        private void showBespeakSeatService(List<string> methods)
        {
            for (int i = 0; i < methods.Count; i++)
            {
                switch (methods[i])
                {
                    case "GetOpenBespeakRooms":
                        ckbGetOpenBespeakRooms.Checked = true;
                        break;
                    case "SubmitBespeakInfo":
                        ckbSubmitBespeakInfo.Checked = true;
                        break;
                    case "GetBespeakSeatRoomSet":
                        ckbGetBespeakSeatRoomSet_bespeak.Checked = true;
                        break;
                    case "GetBaseReaderInfo":
                        ckbGetBaseReaderInfo_bespeak.Checked = true;
                        break;
                    case "GetReaderActualTimeBespeakRecord":
                        ckbGetReaderActualTimeBespeakRecord.Checked = true;
                        break;
                    case "GetReaderBespeakRecord":
                        ckbGetReaderBespeakRecord_bespeak.Checked = true;
                        break;
                    case "GetReaderAccount":
                        ckbGetReaderAccount_bespeak.Checked = true;
                        break;
                    case "CancelBespeakLog":
                        ckbCancelBespeakLog_bespeak.Checked = true;
                        break;
                }
            }
        }

        /// <summary>
        /// 显示暂离授权
        /// </summary>
        /// <param name="methods"></param>
        private void showShortLeaveService(List<string> methods)
        {
            for (int i = 0; i < methods.Count; i++)
            {
                switch (methods[i])
                {
                    case "ShortLeave":
                        ckbShortLeave_shortLeave.Checked = true;
                        break;
                    case "GetBaseReaderInfo":
                        ckbGetBaseReaderInfo_shortLeave.Checked = true;
                        break;
                    case "GetReaderActualTimeRecord":
                        ckbGetReaderActualTimeRecord_shortLeave.Checked = true;
                        break;
                }
            }
        }

        /// <summary>
        /// 显示延时授权
        /// </summary>
        /// <param name="methods"></param>
        private void showDelaySeatUsedTimeService(List<string> methods)
        {
            for (int i = 0; i < methods.Count; i++)
            {
                switch (methods[i])
                {
                    case "GetDelaySet":
                        ckbGetDelaySet_delayTime.Checked = true;
                        break;
                    case "GetBaseReaderInfo":
                        ckb_GetBaseReaderInfo_delay.Checked = true;
                        break;
                    case "GetReaderActualTimeRecord":
                        ckbGetReaderActualTimeRecord_delay.Checked = true;
                        break;
                    case "SubmitDelayResult":
                        ckbSubmitDelayResult_delay.Checked = true;
                        break;
                }
            }
        }
        /// <summary>
        /// 显示读者服务授权
        /// </summary>
        private void showReaderServiceAuthorize(List<string> methods)
        {
            for (int i = 0; i < methods.Count; i++)
            {
                switch (methods[i])
                {
                    case "GetBaseReaderInfoByCardId":
                        ckbGetBaseReaderInfoByCardId.Checked = true;
                        break;
                    case "GetBaseReaderInfo":
                        ckbGetBaseReaderInfoByCardNo.Checked = true;
                        break;
                    case "GetReaderActualTimeRecord":
                        ckbGetReaderActualTimeRecord.Checked = true;
                        break;
                    case "GetReaderBespeakRecord":
                        ckbGetReaderBespeakRecord.Checked = true;
                        break;
                    case "GetReaderChooseSeatRecord":
                        ckbGetReaderChooseSeatRecord.Checked = true;
                        break;
                    case "GetReaderBlacklistRecord":
                        ckbGetReaderBlacklistRecord.Checked = true;
                        break;
                    case "GetReaderAccount":
                        ckbGetReaderAccount.Checked = true;
                        break;
                    case "GetViolateDiscipline":
                        ckbViolationRecordsLog_ReaderService.Checked = true;
                        break;
                }
            }
        }
        /// <summary>
        /// 显示阅览室服务授权
        /// </summary>
        /// <param name="methods"></param>
        private void showGetReadingRoomInfoServiceAuthorize(List<string> methods)
        {
            for (int i = 0; i < methods.Count; i++)
            {
                switch (methods[i])
                {
                    case "GetAllReadingRoomBaseInfo":
                        ckbGetAllReadingRoomBaseInfo.Checked = true;
                        break;
                    case "GetReadingRoomSetInfoByRoomNum":
                        ckbGetReadingRoomSetInfoByRoomNum.Checked = true;
                        break;
                    case "GetSeatsLayoutByRoomNum":
                        ckbGetSeatsLayoutByRoomNum.Checked = true;
                        break;
                    case "GetSeatsUsedInfoByRoomNum":
                        ckbGetSeatsUsedInfoByRoomNum.Checked = true;
                        break;
                    case "GetCanBespeakSeatsLayout":
                        ckbGetCanBespeakSeatsLayout.Checked = true;
                        break;
                    case "GetSeatsBespeakInfoByRoomNum":
                        ckbGetSeatsBespeakInfoByRoomNum.Checked = true;
                        break;
                    case "GetAllRoomSeatUsedInfo":
                        ckbGetAllRoomSeatUsedInfo_RoomService.Checked = true;
                        break;
                }
            }
        }

        private void showOtherAuthorize(List<string> itemNames)
        {
            for (int i = 0; i < itemNames.Count; i++)
            {
                switch (itemNames[i])
                { 
                    case "PostMsg":
                        ckbOther_postMsg.Checked = true;
                        break;
                }
            }
        }
        /// <summary>
        /// 读者信息服务接口以及方法授权
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbReaderService_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = (CheckBox)sender;
            if (ckb.Checked)
            {
                for (int i = 0; i < groupBox1.Controls.Count; i++)
                {
                    ((CheckBox)groupBox1.Controls[i]).Checked = true;
                }
            }
            else
            {
                for (int i = 0; i < groupBox1.Controls.Count; i++)
                {
                    ((CheckBox)groupBox1.Controls[i]).Checked = false;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                toolTip1.Show("请输入用户名", textBox1, 3000);
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text.Trim()))
            {
                toolTip1.Show("请输入密码", textBox2, 3000);
                return;
            }
            if (authorizes != null)
            {
                authorizes.ServiceAuthorizeItems = getGetReaderInfoService();
                if (authorizeInfos.ContainsKey(authorizes.UserName))
                {
                    authorizeInfos[authorizes.UserName] = authorizes;
                }
                else
                {
                    authorizeInfos.Add(authorizes.UserName, authorizes);
                }
            }

            AuthorizeEnabled(true);
            if (authorizeInfos.ContainsKey(textBox1.Text))
            {
                toolTip1.Show("用户名已存在", textBox1, 3000);
                return;
            }
            authorizes = new ServiceAuthorize();
            authorizes.UserName = textBox1.Text;
            authorizes.UserPwd = textBox2.Text;

            initCheckBoxs();
            this.comboBox1.Items.Add(textBox1.Text);
            this.comboBox1.SelectedIndex = this.comboBox1.Items.Count - 1;


        }
        /// <summary>
        /// 初始化UI上的CheckBox
        /// </summary>
        private void initCheckBoxs()
        {
            ckbReaderService.Checked = false;
            ckbGetBaseReaderInfoByCardId.Checked = false;
            ckbGetBaseReaderInfoByCardNo.Checked = false;
            ckbGetReaderActualTimeRecord.Checked = false;
            ckbGetReaderBespeakRecord.Checked = false;
            ckbGetReaderChooseSeatRecord.Checked = false;
            ckbGetReaderAccount.Checked = false;
            ckbGetReaderBlacklistRecord.Checked = false;
            ckbGetReadingRoomSetInfoByRoomNum.Checked = false;
            ckbGetSeatsBespeakInfoByRoomNum.Checked = false;
            ckbGetSeatsLayoutByRoomNum.Checked = false;
            ckbGetCanBespeakSeatsLayout.Checked = false;
            ckbGetSeatsUsedInfoByRoomNum.Checked = false;
            ckbGetAllReadingRoomBaseInfo.Checked = false;
            ckbRoomService.Checked = false;
            ckbGetBaseReaderInfo_bespeak.Checked = false;
            ckbGetBespeakSeatRoomSet_bespeak.Checked = false;
            ckbGetReaderBespeakRecord_bespeak.Checked = false;
            ckbGetReaderActualTimeBespeakRecord.Checked = false;
            ckbSubmitBespeakInfo.Checked = false;
            ckbGetOpenBespeakRooms.Checked = false;
            ckbBespeakService.Checked = false;
            ckbGetReaderChooseSeatRecord_chooseSeat.Checked = false;
            ckbGetBaseReaderInfo_chooseSeat.Checked = false;
            ckbGetReaderActualTimeRecord_chooseSeat.Checked = false;
            ckbGetReaderBlacklistRecord_chooseSeat.Checked = false;
            ckbGetReadingRoomSetInfoByRoomNum_chooseSeat.Checked = false;
            ckbGetAllReadingRoomBaseInfo_chooseSeat.Checked = false;
            ckbChooseSeatService.Checked = false;
            ckbGetSeatsUsedInfoByRoomNum_chooseSeat.Checked = false;
            ckbVerifyCanDoIt_chooseSeat.Checked = false;
            ckbSubmitChooseResult.Checked = false;
            ckbSeatLock_chooseSeat.Checked = false;
            ckbGetReaderActualTimeRecord_delay.Checked = false;
            ckbSubmitDelayResult_delay.Checked = false;
            ckb_GetBaseReaderInfo_delay.Checked = false;
            ckbGetDelaySet_delayTime.Checked = false;
            ckbDelaySeatUsedTimeService.Checked = false;
            ckbGetReaderActualTimeRecord_freeSeat.Checked = false;
            ckbGetBaseReaderInfo_freeSeat.Checked = false;
            ckbFreeSeat.Checked = false;
            ckbFreeSeatService.Checked = false;
            ckbGetReaderActualTimeRecord_shortLeave.Checked = false;
            ckbGetBaseReaderInfo_shortLeave.Checked = false;
            ckbShortLeave_shortLeave.Checked = false;
            ckbShortLeaveService.Checked = false;
            ckbGetReaderAccount_bespeak.Checked = false;
            ckbGetAllRoomSeatUsedInfo_RoomService.Checked = false;
            ckbViolationRecordsLog_ReaderService.Checked = false;
            ckbCancelBespeakLog_bespeak.Checked = false;
            ckbOther_postMsg.Checked = false;
        }

        private void AuthorizeEnabled(bool enabled)
        {
            groupBox1.Enabled = enabled;
            groupBox2.Enabled = enabled;
            groupBox3.Enabled = enabled;
            groupBox4.Enabled = enabled;
            groupBox5.Enabled = enabled;
            groupBox6.Enabled = enabled;
            groupBox7.Enabled = enabled;
            groupBox8.Enabled = enabled;
            button1.Enabled = enabled;
            button4.Enabled = enabled;
        }



        private void ckbDelaySeatUsedTimeService_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = (CheckBox)sender;
            if (ckb.Checked)
            {
                for (int i = 0; i < groupBox5.Controls.Count; i++)
                {
                    ((CheckBox)groupBox5.Controls[i]).Checked = true;
                }
            }
            else
            {
                for (int i = 0; i < groupBox5.Controls.Count; i++)
                {
                    ((CheckBox)groupBox5.Controls[i]).Checked = false;
                }
            }
        }

        private void ckbShortLeaveService_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = (CheckBox)sender;
            if (ckb.Checked)
            {
                for (int i = 0; i < groupBox7.Controls.Count; i++)
                {
                    ((CheckBox)groupBox7.Controls[i]).Checked = true;
                }
            }
            else
            {
                for (int i = 0; i < groupBox7.Controls.Count; i++)
                {
                    ((CheckBox)groupBox7.Controls[i]).Checked = false;
                }
            }
        }

        private void ckbBespeakService_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = (CheckBox)sender;
            if (ckb.Checked)
            {
                for (int i = 0; i < groupBox3.Controls.Count; i++)
                {
                    ((CheckBox)groupBox3.Controls[i]).Checked = true;
                }
            }
            else
            {
                for (int i = 0; i < groupBox3.Controls.Count; i++)
                {
                    ((CheckBox)groupBox3.Controls[i]).Checked = false;
                }
            }
        }

        private void ckbChooseSeatService_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = (CheckBox)sender;
            if (ckb.Checked)
            {
                for (int i = 0; i < groupBox4.Controls.Count; i++)
                {
                    ((CheckBox)groupBox4.Controls[i]).Checked = true;
                }
            }
            else
            {
                for (int i = 0; i < groupBox4.Controls.Count; i++)
                {
                    ((CheckBox)groupBox4.Controls[i]).Checked = false;
                }
            }
        }

        private void ckbFreeSeatService_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = (CheckBox)sender;
            if (ckb.Checked)
            {
                for (int i = 0; i < groupBox6.Controls.Count; i++)
                {
                    ((CheckBox)groupBox6.Controls[i]).Checked = true;
                }
            }
            else
            {
                for (int i = 0; i < groupBox6.Controls.Count; i++)
                {
                    ((CheckBox)groupBox6.Controls[i]).Checked = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChanageAuthorizesInfo();
            loadAuthorizeInfos();
            initCheckBoxs();//初始化UI上的Checkbox
            textBox1.Text = "";
            textBox2.Text = "";

        }
        /// <summary>
        /// 改变授权信息
        /// </summary>
        private void ChanageAuthorizesInfo()
        {
            if (!string.IsNullOrEmpty(textBox1.Text.Trim()) && !string.IsNullOrEmpty(textBox2.Text.Trim()))
            {
                authorizes.UserName = textBox1.Text;
                authorizes.UserPwd = textBox2.Text;
                authorizes.ServiceAuthorizeItems = getGetReaderInfoService();
                //验证是否存在
                if (comboBox1.SelectedItem.ToString() != authorizes.UserName)
                {
                    if (authorizeInfos.ContainsKey(comboBox1.SelectedItem.ToString()))
                    {
                        authorizeInfos.Remove(comboBox1.SelectedItem.ToString());
                    }
                }

                if (authorizeInfos.ContainsKey(authorizes.UserName))
                {
                    authorizeInfos[authorizes.UserName] = authorizes;
                }
                else
                {
                    authorizeInfos.Add(authorizes.UserName, authorizes);
                }
            }
            if (authorizeInfos.Count > 0)
            {
                if (DialogResult.OK == MessageBox.Show(string.Format("确定保存授权信息吗？", authorizes.UserName), "消息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
                {
                    StringBuilder strAuthorizeInfo = new StringBuilder();
                    foreach (ServiceAuthorize item in authorizeInfos.Values)
                    {
                        if (!string.IsNullOrEmpty(item.UserName))
                        {
                            string strJson = SeatManage.SeatManageComm.JSONSerializer.Serialize(item);
                            string ciphertext = SeatManage.SeatManageComm.AESAlgorithm.AESEncrypt(strJson);
                            strAuthorizeInfo.AppendLine(string.Format("{0}={1}", item.UserName, ciphertext));
                        }
                    }
                    try
                    {
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(authorized_keys_filePath, false, Encoding.ASCII))
                        {
                            file.WriteLine(strAuthorizeInfo.ToString());
                            file.Flush();
                        }
                        MessageBox.Show("保存成功");
                        System.Diagnostics.Process.Start("Explorer.exe", sPath);
                    }
                    catch (Exception ex)
                    { 
                        MessageBox.Show("保存失败");
                    }

                }
            }
            else
            {
                MessageBox.Show("没有要保存的授权信息");
            }  
        }

        /// <summary>
        /// 获取授权 信息
        /// </summary>
        /// <returns></returns>
        private List<ServiceAuthorizeItem> getGetReaderInfoService()
        {

            List<ServiceAuthorizeItem> items = new List<ServiceAuthorizeItem>();
            #region 读者服务授权
            if (ckbReaderService.Checked)
            {
                ServiceAuthorizeItem GetReaderInfoService = new ServiceAuthorizeItem();
                GetReaderInfoService.ServiceName = "GetReaderInfoService";
                if (ckbGetBaseReaderInfoByCardId.Checked)
                {
                    GetReaderInfoService.AllowMethodName.Add("GetBaseReaderInfoByCardId");
                }
                if (ckbGetBaseReaderInfoByCardNo.Checked)
                {
                    GetReaderInfoService.AllowMethodName.Add("GetBaseReaderInfo");
                }
                if (ckbGetReaderActualTimeRecord.Checked)
                {
                    GetReaderInfoService.AllowMethodName.Add("GetReaderActualTimeRecord");

                }
                if (ckbGetReaderBespeakRecord.Checked)
                {
                    GetReaderInfoService.AllowMethodName.Add("GetReaderBespeakRecord");
                }
                if (ckbGetReaderChooseSeatRecord.Checked)
                {
                    GetReaderInfoService.AllowMethodName.Add("GetReaderChooseSeatRecord");
                }
                if (ckbGetReaderAccount.Checked)
                {
                    GetReaderInfoService.AllowMethodName.Add("GetReaderAccount");
                }
                if (ckbGetReaderBlacklistRecord.Checked)
                {
                    GetReaderInfoService.AllowMethodName.Add("GetReaderBlacklistRecord");
                }
                if (ckbViolationRecordsLog_ReaderService.Checked)
                {
                    GetReaderInfoService.AllowMethodName.Add("GetViolateDiscipline");
                }
                items.Add(GetReaderInfoService);
            }
            #endregion

            #region 阅览室服务授权
            if (ckbRoomService.Checked)
            {
                ServiceAuthorizeItem GetReadingRoomInfoService = new ServiceAuthorizeItem();
                GetReadingRoomInfoService.ServiceName = "GetReadingRoomInfoService";
                if (ckbGetReadingRoomSetInfoByRoomNum.Checked)
                {
                    GetReadingRoomInfoService.AllowMethodName.Add("GetReadingRoomSetInfoByRoomNum");
                }
                if (ckbGetSeatsBespeakInfoByRoomNum.Checked)
                {
                    GetReadingRoomInfoService.AllowMethodName.Add("GetSeatsBespeakInfoByRoomNum");
                }
                if (ckbGetSeatsLayoutByRoomNum.Checked)
                {
                    GetReadingRoomInfoService.AllowMethodName.Add("GetSeatsLayoutByRoomNum");
                }
                if (ckbGetCanBespeakSeatsLayout.Checked)
                {
                    GetReadingRoomInfoService.AllowMethodName.Add("GetCanBespeakSeatsLayout");
                }
                if (ckbGetSeatsUsedInfoByRoomNum.Checked)
                {
                    GetReadingRoomInfoService.AllowMethodName.Add("GetSeatsUsedInfoByRoomNum");
                }
                if (ckbGetAllReadingRoomBaseInfo.Checked)
                {
                    GetReadingRoomInfoService.AllowMethodName.Add("GetAllReadingRoomBaseInfo");
                }
                if (ckbGetAllRoomSeatUsedInfo_RoomService.Checked)
                {
                    GetReadingRoomInfoService.AllowMethodName.Add("GetAllRoomSeatUsedInfo");
                }
                items.Add(GetReadingRoomInfoService);

            }
            #endregion

            #region 延时服务授权
            if (ckbDelaySeatUsedTimeService.Checked)
            {
                ServiceAuthorizeItem DelaySeatUsedTimeService = new ServiceAuthorizeItem();
                DelaySeatUsedTimeService.ServiceName = "DelaySeatUsedTimeService";
                if (ckbGetReaderActualTimeRecord_delay.Checked)
                {
                    DelaySeatUsedTimeService.AllowMethodName.Add("GetReaderActualTimeRecord");
                }
                if (ckbSubmitDelayResult_delay.Checked)
                {
                    DelaySeatUsedTimeService.AllowMethodName.Add("SubmitDelayResult");
                }
                if (ckb_GetBaseReaderInfo_delay.Checked)
                {
                    DelaySeatUsedTimeService.AllowMethodName.Add("GetBaseReaderInfo");
                }
                if (ckbGetDelaySet_delayTime.Checked)
                {
                    DelaySeatUsedTimeService.AllowMethodName.Add("GetDelaySet");
                }
                items.Add(DelaySeatUsedTimeService);
            }
            #endregion

            #region 暂离服务授权
            if (ckbShortLeaveService.Checked)
            {

                ServiceAuthorizeItem shortLeaveService = new ServiceAuthorizeItem();
                shortLeaveService.ServiceName = "ShortLeaveService";
                if (ckbGetReaderActualTimeRecord_shortLeave.Checked)
                {
                    shortLeaveService.AllowMethodName.Add("GetReaderActualTimeRecord");
                }
                if (ckbGetBaseReaderInfo_shortLeave.Checked)
                {
                    shortLeaveService.AllowMethodName.Add("GetBaseReaderInfo");
                }
                if (ckbShortLeave_shortLeave.Checked)
                {
                    shortLeaveService.AllowMethodName.Add("ShortLeave");
                }
                
                items.Add(shortLeaveService);
                
            }
            #endregion

            #region 预约功能服务授权
            if (ckbBespeakService.Checked)
            {
                ServiceAuthorizeItem bespeakSeatService = new ServiceAuthorizeItem();
                bespeakSeatService.ServiceName = "BespeakSeatService";

                if (ckbGetBaseReaderInfo_bespeak.Checked)
                {
                    bespeakSeatService.AllowMethodName.Add("GetBaseReaderInfo");
                }
                if (ckbGetBespeakSeatRoomSet_bespeak.Checked)
                {
                    bespeakSeatService.AllowMethodName.Add("GetBespeakSeatRoomSet");
                }
                if (ckbGetReaderBespeakRecord_bespeak.Checked)
                {
                    bespeakSeatService.AllowMethodName.Add("GetReaderBespeakRecord");
                }
                if (ckbGetReaderActualTimeBespeakRecord.Checked)
                {
                    bespeakSeatService.AllowMethodName.Add("GetReaderActualTimeBespeakRecord");
                }
                if (ckbSubmitBespeakInfo.Checked)
                {
                    bespeakSeatService.AllowMethodName.Add("SubmitBespeakInfo");
                }
                if (ckbGetOpenBespeakRooms.Checked)
                {
                    bespeakSeatService.AllowMethodName.Add("GetOpenBespeakRooms");
                }
                if (ckbGetReaderAccount_bespeak.Checked)
                {
                    bespeakSeatService.AllowMethodName.Add("GetReaderAccount");
                }
                if (ckbCancelBespeakLog_bespeak.Checked)
                {
                    bespeakSeatService.AllowMethodName.Add("CancelBespeakLog");
                }
                items.Add(bespeakSeatService);
            }
            #endregion

            #region 选座功能服务授权
            if (ckbChooseSeatService.Checked)
            {
                ServiceAuthorizeItem chooseSeatAuthorizeItem = new ServiceAuthorizeItem();
                chooseSeatAuthorizeItem.ServiceName = "ChooseSeatService";

                if (ckbGetReaderChooseSeatRecord_chooseSeat.Checked)
                {
                    chooseSeatAuthorizeItem.AllowMethodName.Add("GetReaderChooseSeatRecord");
                }
                if (ckbGetBaseReaderInfo_chooseSeat.Checked)
                {
                    chooseSeatAuthorizeItem.AllowMethodName.Add("GetBaseReaderInfo");
                }
                if (ckbGetReaderActualTimeRecord_chooseSeat.Checked)
                {
                    chooseSeatAuthorizeItem.AllowMethodName.Add("GetReaderActualTimeRecord");
                }
                if (ckbGetReaderBlacklistRecord_chooseSeat.Checked)
                {
                    chooseSeatAuthorizeItem.AllowMethodName.Add("GetReaderBlacklistRecord");
                }
                if (ckbGetReadingRoomSetInfoByRoomNum_chooseSeat.Checked)
                {
                    chooseSeatAuthorizeItem.AllowMethodName.Add("GetReadingRoomSetInfoByRoomNum");
                }
                if (ckbGetAllReadingRoomBaseInfo_chooseSeat.Checked)
                {
                    chooseSeatAuthorizeItem.AllowMethodName.Add("GetAllReadingRoomBaseInfo");
                }
                if (ckbGetSeatsUsedInfoByRoomNum_chooseSeat.Checked)
                {
                    chooseSeatAuthorizeItem.AllowMethodName.Add("GetSeatsUsedInfoByRoomNum");
                }
                if (ckbVerifyCanDoIt_chooseSeat.Checked)
                {
                    chooseSeatAuthorizeItem.AllowMethodName.Add("VerifyCanDoIt");
                }
                if (ckbSubmitChooseResult.Checked)
                {
                    chooseSeatAuthorizeItem.AllowMethodName.Add("SubmitChooseResult");
                }
                if (ckbSeatLock_chooseSeat.Checked)
                {
                    chooseSeatAuthorizeItem.AllowMethodName.Add("SeatLock");
                }
                items.Add(chooseSeatAuthorizeItem);
            }
            #endregion

            #region 释放座位功能服务授权
            if (ckbFreeSeatService.Checked)
            {
                ServiceAuthorizeItem freeSeatAuthorizeItem = new ServiceAuthorizeItem();
                freeSeatAuthorizeItem.ServiceName = "FreeSeatService";
                if (ckbGetReaderActualTimeRecord_freeSeat.Checked)
                {
                    freeSeatAuthorizeItem.AllowMethodName.Add("GetReaderActualTimeRecord");
                }
                if (ckbGetBaseReaderInfo_freeSeat.Checked)
                {
                    freeSeatAuthorizeItem.AllowMethodName.Add("GetBaseReaderInfo");
                }
                if (ckbFreeSeat.Checked)
                {
                    freeSeatAuthorizeItem.AllowMethodName.Add("FreeSeat");
                }
                items.Add(freeSeatAuthorizeItem);
            }
            #endregion

            #region 其他服务授权
            ServiceAuthorizeItem OtherAuthorizeItem = new ServiceAuthorizeItem();
            OtherAuthorizeItem.ServiceName = "Other";
            if (ckbOther_postMsg.Checked)
            {
                OtherAuthorizeItem.AllowMethodName.Add("PostMsg");
            }
            items.Add(OtherAuthorizeItem);
            #endregion

            return items;
        }

        private void ckbRoomService_Click(object sender, EventArgs e)
        {
            CheckBox ckb = (CheckBox)sender;
            if (ckb.Checked)
            {
                for (int i = 0; i < groupBox2.Controls.Count; i++)
                {
                    ((CheckBox)groupBox2.Controls[i]).Checked = true;
                }
            }
            else
            {
                for (int i = 0; i < groupBox2.Controls.Count; i++)
                {
                    ((CheckBox)groupBox2.Controls[i]).Checked = false;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 && !string.IsNullOrEmpty(comboBox1.SelectedItem.ToString()))
            {
                authorizeInfos.Remove(comboBox1.SelectedItem.ToString());
                comboBox1.Items.Remove(comboBox1.SelectedItem);
                initCheckBoxs();
                textBox1.Text = "";
                textBox2.Text = "";

            }
        }



    }
}
