using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AppMessagePush
{
    public class AppServiceMsgPush
    {
        public AppServiceMsgPush()
        {
            url = ConfigurationManager.AppSettings["WeCharPushUrl"];
        }
        public void MsgPush(string cardNo, string schoolNo, string dateTime, string type, string message)
        {

        }
        public void MsgPush(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine(webRequest(message));


        }
        public string url { get; set; }
        /// <summary>
        /// 请求对应的Url。
        /// </summary>
        /// <param name="postParameter">要发送的参数</param>
        /// <param name="postMethod">请求目标</param>
        /// <returns></returns>
        private string requestUrl(string postmsg)
        {
            string pushUrl = "http://zhanzuo.jeeshu.com/Sendmessage";
            byte[] data = Encoding.UTF8.GetBytes(postmsg);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
            WebClient webClient = new WebClient();
            try
            {
                //webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=utf-8");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
                //webClient.Headers.Add("User-Agent", "BCCS_SDK/3.0 (Windows 7 Ultimate ) C#/4.0 (Baidu Push SDK for PHP v4.0) .net framework/4.0 ZEND/2.6.0");

                //byte[] responseData = webClient.UploadData(pushUrl, "POST", data);//得到返回字符流  
                //string srcString = Encoding.UTF8.GetString(responseData);//解码  
                //return srcString;


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://zhanzuo.desiver.com/Sendmessage");
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                string aesStr = SeatManage.SeatManageComm.AESAlgorithm.AESEncrypt(postmsg);
                request.ContentLength = Encoding.UTF8.GetByteCount(aesStr);
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("UTF-8"));
                myStreamWriter.Write(aesStr);
                myStreamWriter.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                return retString;

            }
            catch (WebException ex)
            {
                Stream stream = ex.Response.GetResponseStream();
                string m = ex.Response.Headers.ToString();
                byte[] buf = new byte[256];
                stream.Read(buf, 0, 256);
                stream.Close();
                int count = 0;
                foreach (var b in buf)
                {
                    if (b > 0)
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }
                return Encoding.UTF8.GetString(buf, 0, count);
            }
        }

        private string webRequest(string msg)
        {
            try
            {
                string service = url;
                string aesStr = SeatManage.SeatManageComm.AESAlgorithm.AESEncrypt(msg, "SeatManage_WeiCharCode");
                string contenttype = "application/x-www-form-urlencoded"; //更网站该方法支持的类型要一致
                //根据接口，写参数
                string para = "msg=" + aesStr;
                if (msg.IndexOf("2014101603", StringComparison.Ordinal) > 0)
                {
                    SeatManage.SeatManageComm.WriteLog.Write(msg);
                }

                //para += "&action=" + aesStr;
                //发送请求
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(service);
                myRequest.Method = "POST";
                myRequest.ContentType = contenttype;
                myRequest.ContentLength = para.Length;
                Stream newStream = myRequest.GetRequestStream();
                // Send the data.
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] postdata = encoding.GetBytes(para);
                newStream.Write(postdata, 0, para.Length);
                newStream.Close();
                // Get response
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string content = reader.ReadToEnd(); //得到结果
                return content;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
