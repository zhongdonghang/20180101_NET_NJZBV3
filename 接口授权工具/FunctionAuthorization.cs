using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AuthorizeVerify;
using System.IO;

namespace 接口授权工具
{
    public partial class FunctionAuthorization : Form
    {
        public FunctionAuthorization()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 当前选中的用户授权信息
        /// </summary>
        private FunctionAuthorizeInfo authorizes;
        string authorized_keys_filePath = string.Format(@"{0}authorized\sf_authorized_keys", AppDomain.CurrentDomain.BaseDirectory);
        string sPath = string.Format(@"{0}authorized\", AppDomain.CurrentDomain.BaseDirectory);
        private void FunctionAuthorization_Load(object sender, EventArgs e)
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
                authorizes = AuthorizeVerify.FunctionAuthorizeInfo.AnalyzeAuthorize(authorized_keys_filePath);
                DataBinding();
            }
        }
        private void DataBinding()
        {
            if (authorizes == null)
            {
                return;
            }
            txt_SchoolNum.Text = authorizes.SchoolNum;
            foreach (string item in authorizes.SystemFunction)
            {
                switch (item)
                {
                    case "Bespeak_AppointTime": cb_Bespeak_AppointTime.Checked = true; break;
                    case "Bespeak_NowDay": cb_Bespeak_NowDay.Checked = true; break;
                    case "Client_SeachBespeak": cb_Client_SeachBespeak.Checked = true; break;
                    case "Client_SeachBlasklist": cb_Client_SeachBlasklist.Checked = true; break;
                    case "Client_SeachViolation": cb_Client_SeachViolation.Checked = true; break;
                    case "Client_ShowLastSeat": cb_Client_ShowLastSeat.Checked = true; break;
                    case "Client_ShowReaderInfo": cb_Client_ShowReaderInfo.Checked = true; break;
                    case "Client_ShowReaderMeg": cb_Client_ShowReaderMeg.Checked = true; break;
                    case "LimitTime_SpanMode": cb_LimitTime_SpanMode.Checked = true; break;
                    case "Media_MediaPlayer": cb_Media_MediaPlayer.Checked = true; break;
                    case "Media_PopAd": cb_Media_PopAd.Checked = true; break;
                    case "Media_SchoolNotice": cb_Media_SchoolNotice.Checked = true; break;
                    case "Media_TitleAd": cb_Media_TitleAd.Checked = true; break;
                    case "MoveClient_AdminManage": cb_MoveClient_AdminManage.Checked = true; break;
                    case "MoveClient_ContinueTime": cb_MoveClient_ContinueTime.Checked = true; break;
                    case "MoveClient_QRcodeDecode": cb_MoveClient_QRcodeDecode.Checked = true; break;
                    case "RoomOC_24HModel": cb_24HModel.Checked = true; break;
                    case "MoveClient_SeatSelect": cb_MoveClient_SeatSelect.Checked = true; break;
                    case "MoveClient_SeatWait": cb_MoveClient_SeatWait.Checked = true; break;
                    case "SendMsg": cb_SendMsg.Checked = true; break;
                }
            }
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (authorizes == null)
            {
                authorizes = new FunctionAuthorizeInfo();
            }
            if (string.IsNullOrEmpty(txt_SchoolNum.Text))
            {
                MessageBox.Show("学校编号不能为空！");
                return;
            }
            authorizes.SchoolNum = txt_SchoolNum.Text;
            authorizes.SystemFunction = new List<string>();
            if (cb_Bespeak_AppointTime.Checked)
                authorizes.SystemFunction.Add("Bespeak_AppointTime");
            if (cb_Bespeak_NowDay.Checked)
                authorizes.SystemFunction.Add("Bespeak_NowDay");
            if (cb_Client_SeachBespeak.Checked)
                authorizes.SystemFunction.Add("Client_SeachBespeak");
            if (cb_Client_SeachBlasklist.Checked)
                authorizes.SystemFunction.Add("Client_SeachBlasklist");
            if (cb_Client_SeachViolation.Checked)
                authorizes.SystemFunction.Add("Client_SeachViolation");
            if (cb_Client_ShowLastSeat.Checked)
                authorizes.SystemFunction.Add("Client_ShowLastSeat");
            if (cb_Client_ShowReaderInfo.Checked)
                authorizes.SystemFunction.Add("Client_ShowReaderInfo");
            if (cb_Client_ShowReaderMeg.Checked)
                authorizes.SystemFunction.Add("Client_ShowReaderMeg");
            if (cb_LimitTime_SpanMode.Checked)
                authorizes.SystemFunction.Add("LimitTime_SpanMode");
            if (cb_Media_MediaPlayer.Checked)
                authorizes.SystemFunction.Add("Media_MediaPlayer");
            if (cb_Media_PopAd.Checked)
                authorizes.SystemFunction.Add("Media_PopAd");
            if (cb_Media_SchoolNotice.Checked)
                authorizes.SystemFunction.Add("Media_SchoolNotice");
            if (cb_Media_TitleAd.Checked)
                authorizes.SystemFunction.Add("Media_TitleAd");
            if (cb_MoveClient_AdminManage.Checked)
                authorizes.SystemFunction.Add("MoveClient_AdminManage");
            if (cb_MoveClient_ContinueTime.Checked)
                authorizes.SystemFunction.Add("MoveClient_ContinueTime");
            if (cb_MoveClient_QRcodeDecode.Checked)
                authorizes.SystemFunction.Add("MoveClient_QRcodeDecode");
            if (cb_24HModel.Checked)
                authorizes.SystemFunction.Add("RoomOC_24HModel");
            if (cb_MoveClient_SeatSelect.Checked)
                authorizes.SystemFunction.Add("MoveClient_SeatSelect");
            if (cb_MoveClient_SeatWait.Checked)
                authorizes.SystemFunction.Add("MoveClient_SeatWait");
            if (cb_SendMsg.Checked)
                authorizes.SystemFunction.Add("SendMsg");
            if (DialogResult.OK == MessageBox.Show("确定保存授权信息吗？", "消息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
            {
                try
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(authorized_keys_filePath, false, Encoding.ASCII))
                    {
                        string strJson = SeatManage.SeatManageComm.JSONSerializer.Serialize(authorizes);
                        string ciphertext = SeatManage.SeatManageComm.AESAlgorithm.AESEncrypt(strJson);
                        file.WriteLine(ciphertext);
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

        



    }
}
