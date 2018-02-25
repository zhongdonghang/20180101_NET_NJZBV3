using SeatManage.SeatManageComm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeatManageWebV2
{
    public partial class _Default : DefaultBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// aes 解密方法
        /// </summary>
        /// <param name="text"></param>
        /// <param name="password"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string AESDecrypt(string text, string password, string iv)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] encryptedData = Convert.FromBase64String(text);
            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            System.Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            byte[] ivBytes = System.Text.Encoding.UTF8.GetBytes(iv);
            rijndaelCipher.IV = ivBytes;
            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdOK_Click(object sender, ImageClickEventArgs e)
        {
            if (txtUserName.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                Response.Write(@"<script language='javascript'>alert('用户名和密码不能为空！'); </script> ");
                return;
            }
            try
            {
                //验证用户登录
                string loginID = txtUserName.Text;
                string Password = txtPassword.Text;

                //解密
                //loginID = AESDecrypt(loginID, "AjQ0YQ0MvKKC1uTr", "AjQ0YQ0MvKKC1uTr");
                //Password = AESDecrypt(Password, "AjQ0YQ0MvKKC1uTr", "AjQ0YQ0MvKKC1uTr");
                SeatManage.Bll.Users_ALL userinfocheck = new SeatManage.Bll.Users_ALL();
                loginID = userinfocheck.CheckUser(loginID, Password);
                //判断返回信息是否为空
                if (string.IsNullOrEmpty(loginID))
                {
                    Response.Write(@"<script language='javascript'>alert('用户或密码错误，请重新输入'); </script> ");
                }
                else
                {
                    this.LoginId = loginID;
                    Response.Redirect("Florms/FormSYS.aspx");
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.ToString());
                Response.Write(@"<script language='javascript'>alert('数据库连接出错！'); </script> ");
            }
        }
    }
}
