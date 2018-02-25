using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;

namespace SeatManage.SeatManageComm
{
    public class SendEmail
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="sendService">发送服务器，例：smtp.qq.com</param>
        /// <param name="sendMailAddress">发送邮件邮箱，例：123456@qq.com</param>
        /// <param name="password">发送邮件密码</param>
        /// <param name="getMailAddress">发送目标邮箱</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="mailBody">邮件内容</param>
        /// <param name="IsHtml">内容是否是html格式的</param>
        public static void Send(string sendService, string sendMailAddress, string password, string getMailAddress, string subject, string mailBody, bool IsHtml)
        {
            try
            {
                SmtpClient client = new SmtpClient(sendService);
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(sendMailAddress, password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(sendMailAddress);
                mail.To.Add(new MailAddress(getMailAddress));
                mail.Subject = subject;
                mail.BodyEncoding = System.Text.Encoding.Default;
                mail.Body = mailBody;
                mail.IsBodyHtml = IsHtml;
                client.Send(mail);
            }
            catch (Exception ex)
            {
                WriteLog.Write("发送邮件失败：" + ex.Message);
            }
        }
        public static void Send(string getMailAddress, string subject, string mailBody, bool IsHtml)
        {
            try
            {
                string sendService = ConfigurationManager.AppSettings["EmailServer"];
                string sendMailAddress = ConfigurationManager.AppSettings["EmailAdderss"];
                string password = AESAlgorithm.AESDecrypt(ConfigurationManager.AppSettings["EmailPasssord"]);
                SmtpClient client = new SmtpClient(sendService);
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(sendMailAddress, password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(sendMailAddress);
                mail.To.Add(new MailAddress(getMailAddress));
                mail.Subject = subject;
                mail.BodyEncoding = System.Text.Encoding.Default;
                mail.Body = mailBody;
                mail.IsBodyHtml = IsHtml;
                client.Send(mail);
            }
            catch (Exception ex)
            {
                WriteLog.Write("发送邮件失败：" + ex.Message);
            }
        }
    }
}
