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

namespace InterfaceAuthorze
{
    public partial class LoginWindow : Form
    {
        public LoginWindow()
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
        string authorized_keys_filePath = string.Format(@"{0}ws_authorized_keys", AppDomain.CurrentDomain.BaseDirectory);
        string sPath = string.Format(@"{0}", AppDomain.CurrentDomain.BaseDirectory);
        private void LoginWindow_Load(object sender, EventArgs e)
        {
            loadAuthorizeInfos();
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {

            MainWindow m = new MainWindow();
            this.Hide();
            m.ShowDialog();
            this.Close();
        }
        /// <summary>
        /// 加载授权文件并显示授权信息
        /// </summary>
        private void loadAuthorizeInfos()
        {
            if (File.Exists(authorized_keys_filePath))
            {
                try
                {
                    authorizeInfos = AuthorizeVerify.ServiceAuthorize.AnalyzeAuthorize(authorized_keys_filePath);
                    string admin = "";
                    foreach (string author in authorizeInfos.Keys)
                    {
                        if (authorizeInfos[author].IsAdmin)
                        {
                            admin = author;
                            continue;
                        }
                    }
                    if (string.IsNullOrEmpty(admin))
                    {
                        MessageBox.Show("对不起！您没有权限！");
                        this.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("对不起！您没有权限！");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("对不起！您没有权限！");
                this.Close();
            }
        }
    }
}
