using AuthorizeVerify;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 接口授权工具
{
    public partial class WebServiceAuthorization : Form
    {
        public WebServiceAuthorization()
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
        private void WebServiceAuthorization_Load(object sender, EventArgs e)
        {
            AuthorizeEnabled(false);
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
                authorizes.IsAdmin = cb_IsAdmin.Checked;
                authorizes.SchoolNo = txt_SchoolNo.Text.Trim();
                if (!string.IsNullOrEmpty(authorizes.UserName) && !string.IsNullOrEmpty(authorizes.UserPwd) && !string.IsNullOrEmpty(authorizes.SchoolNo))
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
                    AuthorizeEnabled(false);
                    AuthorizeEnabled(true);
                    //initCheckBoxs();//初始化UI上的Checkbox
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
            txt_SchoolNo.Text = serviceAuthorizes.SchoolNo;
            cb_IsAdmin.Checked = serviceAuthorizes.IsAdmin;
            foreach (AuthorizeVerify.ServiceAuthorizeItem authorizeItem in serviceAuthorizes.ServiceAuthorizeItems)
            {
                GroupBox gb = Controls.Find("gb_" + authorizeItem.ServiceName, false)[0] as GroupBox;
                CheckBox cb = gb.Controls.Find("cb_" + authorizeItem.ServiceName, false)[0] as CheckBox;
                if (cb != null)
                {
                    cb.Checked = true;
                    foreach (string methodName in authorizeItem.AllowMethodName)
                    {
                        CheckBox cbc = gb.Controls.Find("cb_" + authorizeItem.ServiceName + "_" + methodName, false)[0] as CheckBox;
                        if (cbc != null)
                        {
                            cbc.Checked = true;
                        }
                    }
                }
            }
        }
        void AuthorizeEnabled(bool enabled)
        {
            List<GroupBox> gbl = new List<GroupBox>();
            gbl.Add(gb_GetReaderInfoService);
            gbl.Add(gb_SeatOperationService);
            gbl.Add(gb_GetSeatInfoService);
            gbl.Add(gb_GetReadingRoomInfoService);
            foreach (GroupBox gb in gbl)
            {
                gb.Enabled = enabled;
                if (!enabled)
                {
                    for (int i = 0; i < gb.Controls.Count; i++)
                    {
                        ((CheckBox)gb.Controls[i]).Checked = false;
                    }
                }
            }
        }
        /// <summary>
        /// 获取授权 信息
        /// </summary>
        /// <returns></returns>
        private List<ServiceAuthorizeItem> getGetReaderInfoService()
        {
            List<ServiceAuthorizeItem> items = new List<ServiceAuthorizeItem>();
            List<CheckBox> cbl = new List<CheckBox>();
            cbl.Add(cb_GetReaderInfoService);
            cbl.Add(cb_SeatOperationService);
            cbl.Add(cb_GetSeatInfoService);
            cbl.Add(cb_GetReadingRoomInfoService);
            foreach (CheckBox cb in cbl)
            {
                if (cb.Checked)
                {
                    ServiceAuthorizeItem authorizeItem = new ServiceAuthorizeItem();
                    authorizeItem.ServiceName = cb.Name.Split('_')[1];
                    GroupBox gb = Controls.Find("gb_" + cb.Name.Split('_')[1], false)[0] as GroupBox;
                    for (int i = 0; i < gb.Controls.Count; i++)
                    {
                        if (((CheckBox)gb.Controls[i]) != cb && ((CheckBox)gb.Controls[i]).Checked)
                        {
                            authorizeItem.AllowMethodName.Add(((CheckBox)gb.Controls[i]).Name.Split('_')[2]);
                        }
                    }
                    items.Add(authorizeItem);
                }
            }
            return items;
        }

        private void cb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = (CheckBox)sender;
            GroupBox gb = Controls.Find("gb_" + ckb.Name.Split('_')[1], false)[0] as GroupBox;
            if (!ckb.Checked)
            {
                for (int i = 0; i < gb.Controls.Count; i++)
                {
                    if (((CheckBox)gb.Controls[i]) == ckb)
                    {
                        continue;
                    }
                    ((CheckBox)gb.Controls[i]).Checked = false;

                }
            }
        }
        private void cbc_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbc = (CheckBox)sender;
            if (cbc.Checked)
            {
                GroupBox gb = Controls.Find("gb_" + cbc.Name.Split('_')[1], false)[0] as GroupBox;
                CheckBox cbf = (CheckBox)gb.Controls.Find(cbc.Name.Split('_')[0] + "_" + cbc.Name.Split('_')[1], false)[0];
                cbf.Checked = true;
            }
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 && !string.IsNullOrEmpty(comboBox1.SelectedItem.ToString()))
            {
                authorizeInfos.Remove(comboBox1.SelectedItem.ToString());
                comboBox1.Items.Remove(comboBox1.SelectedItem);
                AuthorizeEnabled(false);
                //AuthorizeEnabled(true);
                textBox1.Text = "";
                textBox2.Text = "";

            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("请输入用户名");
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text.Trim()))
            {
                MessageBox.Show("请输入密码");
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
            AuthorizeEnabled(false);
            AuthorizeEnabled(true);
            if (authorizeInfos.ContainsKey(textBox1.Text))
            {
                MessageBox.Show("用户名已存在");
                return;
            }
            authorizes = new ServiceAuthorize();
            authorizes.UserName = textBox1.Text;
            authorizes.UserPwd = textBox2.Text;

            //initCheckBoxs();
            this.comboBox1.Items.Add(textBox1.Text);
            this.comboBox1.SelectedIndex = this.comboBox1.Items.Count - 1;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChanageAuthorizesInfo();
            loadAuthorizeInfos();
            AuthorizeEnabled(false);
            //AuthorizeEnabled(true);
            //initCheckBoxs();//初始化UI上的Checkbox
            textBox1.Text = "";
            textBox2.Text = "";

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
