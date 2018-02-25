using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Xml;
using System.Net;
using System.IO;
using WeiXinJK.Model;

namespace WeiXinJK
{
    public class WeiXinService:IWeixinService
    {
        /// <summary>
        /// 接收消息事件
        /// </summary>
        public event WeixinMsgHandle ReceiveMsgEvent;
        /// <summary>
        /// 菜单点击事件
        /// </summary>
        public event WeixinEventHandle MenuClickEvent;
        /// <summary>
        /// 点击菜单连接触发事件
        /// </summary>
        public event WeixinEventHandle LinkMenuClickEvent;
        /// <summary>
        /// 用户关注事件
        /// </summary>
        public event WeixinEventHandle SubscribeEvent;
        /// <summary>
        /// 用户取消关注事件
        /// </summary>
        public event WeixinEventHandle UnSubscribeEvent;
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public bool CheckSignature(string signature, string timestamp, string nonce)
        {
            List<string> l = new List<string>();
            l.Add(timestamp);
            l.Add(WeiXinJKPram.TOKEN);
            l.Add(nonce);
            l.Sort();
            StringBuilder tmpStr = new StringBuilder();
            for (int i = 0; i < l.Count; i++)
            {
                tmpStr.Append(l[i]);
            }
            SHA1 sha = new SHA1CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] dataToHash = enc.GetBytes(tmpStr.ToString());//Hash运算
            byte[] dataHashed = sha.ComputeHash(dataToHash);//将运算结果转换成string
            string hash = BitConverter.ToString(dataHashed).Replace("-", "").ToLower();
            return hash.Equals(signature);
        }
        /// <summary>
        /// 模拟Http Get请求
        /// </summary>
        /// <param name="postDataStr"></param>
        /// <returns></returns>
        private string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
     
        /// <summary>
        /// 消息处理
        /// </summary>
        /// <param name="msg"></param>
        public void MessageHandle(string msg)
        {
            string messageType = getMessageType(msg);
            string MeEvent = getEvent(msg);
            Model.WeiXinMessageBase mgMype = null;
            Model.WeixinEventMsg mgEvent = null;
            switch (messageType)
            {
                case "event":
                    switch (MeEvent)
                    {
                        case "subscribe":
                            mgEvent = new WeixinEventMsg(msg);
                            //TODO:xml转换成消息对象
                            if (SubscribeEvent != null)
                            {
                                SubscribeEvent(this, mgEvent);
                            }
                            break;
                        case "unsubscribe":
                            mgEvent = new WeixinEventMsg(msg);
                            if (UnSubscribeEvent != null)
                            {
                                UnSubscribeEvent(this, mgEvent);
                            }
                            break;
                        case "CLICK":
                            mgEvent = new WexinEventClickMenu(msg);
                            if (MenuClickEvent != null)
                            {
                                MenuClickEvent(this, mgEvent);
                            }
                            break;
                        case "VIEW":
                            mgEvent = new WexinEventClickMenu(msg);
                            if (LinkMenuClickEvent != null)
                            {
                                LinkMenuClickEvent(this, mgEvent);
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case "text":
                    mgMype = new WeiXinTextMsg(msg);
                    if (ReceiveMsgEvent != null)
                    {
                        ReceiveMsgEvent(this, mgMype);
                    }
                    break;
                case "image":
                    mgMype = new WeiXinImageMsg(msg);
                    if (ReceiveMsgEvent != null)
                    {
                        ReceiveMsgEvent(this, mgMype);
                    }
                    break;
                case "voice":
                    mgMype = new WeiXinVoiceMsg(msg);
                    if (ReceiveMsgEvent != null)
                    {
                        ReceiveMsgEvent(this, mgMype);
                    }
                    break;
                case "video":
                    mgMype = new WeiXinVideoMsg(msg);
                    if (ReceiveMsgEvent != null)
                    {
                        ReceiveMsgEvent(this, mgMype);
                    }
                    break;
                case "location":
                    mgMype = new WeiXinLocationMsg(msg);
                    if (ReceiveMsgEvent != null)
                    {
                        ReceiveMsgEvent(this, mgMype);
                    }
                    break;
                case "link":
                    mgMype = new WeiXinLinkMsg(msg);
                    if (ReceiveMsgEvent != null)
                    {
                        ReceiveMsgEvent(this, mgMype);
                    }
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 获取 消息类型
        /// </summary>
        /// <param name="weixinMessage"></param>
        /// <returns></returns>
        private static string getMessageType(string weixinMessage)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(weixinMessage);//读取xml字符串
            XmlElement root = doc.DocumentElement;
            XmlNode MsgType = root.SelectSingleNode("MsgType");//获取收到的消息类型。文本(text)，图片(image)，语音等。
            if (MsgType != null)
            {
                return MsgType.InnerText;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取事件类型
        /// </summary>
        /// <param name="WenxinMessage"></param>
        /// <returns></returns>
        private static string getEvent(string WenxinMessage)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(WenxinMessage);//读取xml字符串
            XmlElement root = doc.DocumentElement;
            XmlNode MsgType = root.SelectSingleNode("Event");//获取收到的消息类型。文本(text)，图片(image)，语音等。
            if (MsgType != null)
            {
                return MsgType.InnerText;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 文本消息回复
        /// </summary>
        /// <param name="age"></param>
        /// <param name="Txt"></param>
        /// <returns></returns>
        public string SendWeiXinMsg(WeiXinMessageBase age )
        {
            string s =age.ToString();

            //StringBuilder s = new StringBuilder();
            //s.AppendFormat("<ToUserName><![CDATA[");
            //s.Append(age.FromUserName);
            //s.Append("]]></ToUserName>");

            //string xmlTXT="<xml>";
            //xmlTXT += string.Format("<ToUserName><![CDATA[{0}]]></ToUserName>", age.FromUserName);
            //xmlTXT += "<FromUserName><![CDATA[" + age.ToUserName + "]]></FromUserName>";
            //xmlTXT += "<CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime>";
            //xmlTXT += "<MsgType><![CDATA[text]]></MsgType>";
            //xmlTXT += "<Content><![CDATA["+Txt+"]]></Content>";
            //xmlTXT += "<FuncFlag>0</FuncFlag>";
            //xmlTXT += "</xml>";
            return "";
        }

        /// <summary>
        /// datetime转换为unixtime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
    }
}
