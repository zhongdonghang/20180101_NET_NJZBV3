using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WeiXinJK.Model
{
    /// <summary>
    /// 微信消息基类
    /// </summary>
    public class WeiXinMessageBase
    {
        public WeiXinMessageBase()
        {
            
        }
        /// <summary>
        /// 微信消息解析
        /// </summary>
        /// <param name="weixinMsg"></param>
        public WeiXinMessageBase(string weixinMsg)
        {
            if (string.IsNullOrEmpty(weixinMsg))
            {
                return;
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(weixinMsg);//读取xml字符串
                XmlElement root = doc.DocumentElement;
                XmlNode node = root.SelectSingleNode("ToUserName");
                if (node != null)
                {
                    this.ToUserName = node.InnerText;
                }
                node = root.SelectSingleNode("FromUserName");
                if (node != null)
                {
                    this.FromUserName = node.InnerText;
                }
                node = root.SelectSingleNode("CreateTime");
                if (node != null)
                {
                    this.CreateTime = node.InnerText;
                }
                XmlNode MsgType = root.SelectSingleNode("MsgType");//获取收到的消息类型。文本(text)，图片(image)，语音等。
                XmlNode MsgEvent = root.SelectSingleNode("Event");//获取收到的消息类型。文本(text)，图片(image)，语音等。
                if (MsgType != null)
                {
                    switch (MsgType.InnerText)
                    {
                        case "text":
                            this.MsgType = EnumWeiXinMsgType.Text;
                            break;
                        case "image":
                            this.MsgType = EnumWeiXinMsgType.Image;
                            break;
                        case "voice":
                            this.MsgType = EnumWeiXinMsgType.Voice;
                            break;
                        case "video":
                            this.MsgType = EnumWeiXinMsgType.Video;
                            break;
                        case "location":
                            this.MsgType = EnumWeiXinMsgType.Location;
                            break;
                        case "link":
                            this.MsgType = EnumWeiXinMsgType.Link;
                            break;
                        case "event":
                            this.MsgType = EnumWeiXinMsgType.Event;
                            break;

                    }
                }

            }
        }

        private string _ToUserName;
        private string _FromUserName;
        private EnumWeiXinMsgType _MsgType;
        private string _CreateTime = "";
        /// <summary>
        /// 接收方账号
        /// </summary>
        public string ToUserName
        {
            get { return _ToUserName; }
            set { _ToUserName = value; }
        }
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string FromUserName
        {
            get { return _FromUserName; }
            set { _FromUserName = value; }
        }
        /// <summary>
        /// 消息类型
        /// </summary>
        public EnumWeiXinMsgType MsgType
        {
            get { return _MsgType; }
            set { _MsgType = value; }
        }

        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public string CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }
        /// <summary>
        /// 返回XML格式
        /// </summary>
        /// <returns></returns>
        public string ToXML()
        {
            string XLMStr = string.Format("<xml>");
            XLMStr += string.Format("<ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName);
            XLMStr += string.Format("<FromUserName><![CDATA[{0}]]></FromUserName>",FromUserName);
            XLMStr += string.Format("<CreateTime>{0}</CreateTime>", ConvertDateTimeInt(DateTime.Now));
            XLMStr += string.Format("<MsgType><![CDATA[{0}]]></MsgType>",MsgType);
            return XLMStr;
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

    /// <summary>
    /// 接收用户发送的文本消息
    /// </summary>
    public class WeiXinTextMsg : WeiXinMessageBase
    {
        public WeiXinTextMsg() { }
        public WeiXinTextMsg(string weiXinMsg)
            : base(weiXinMsg)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(weiXinMsg);//读取xml字符串
            XmlElement root = doc.DocumentElement;
            XmlNode node = root.SelectSingleNode("Content");
            if (node != null)
            {
                Content = node.InnerText;
            }
            node = root.SelectSingleNode("MsgId");
            if (node != null)
            {
                this.MsgId = node.InnerText;
            }
        }
        private string _Content;
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        private string _MsgId;
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public string MsgId
        {
            get { return _MsgId; }
            set { _MsgId = value; }
        }

        /// <summary>
        /// 返回文本消息
        /// </summary>
        /// <returns></returns>
        public string ToXML()
        {
            string strBase = base.ToXML();
            strBase += string.Format("<Content><![CDATA[{0}]]></Content>",Content);
            strBase += string.Format("</xml>");
            return strBase;
        }
    }

    /// <summary>
    /// 用户发来的图片消息实体
    /// </summary>
    public class WeiXinImageMsg : WeiXinMessageBase
    {
        public WeiXinImageMsg()
        { }
        public WeiXinImageMsg(string weixinMsg)
            : base(weixinMsg)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(weixinMsg);//读取xml字符串
            XmlElement root = doc.DocumentElement;
            XmlNode node = root.SelectSingleNode("PicUrl");
            if (node != null)
            {
                this.PicUrl = node.InnerText;
            }
              node = root.SelectSingleNode("MediaId");
            if (node != null)
            {
                this.MediaId = node.InnerText;
            }
            node = root.SelectSingleNode("MsgId");
            if (node != null)
            {
                this.MsgId = node.InnerText;
            }
        }

        private string _PicUrl;
        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl
        {
            get { return _PicUrl; }
            set { _PicUrl = value; }
        }
        private string _MediaId;
        /// <summary>
        /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId
        {
            get { return _MediaId; }
            set { _MediaId = value; }
        }

        private string _MsgId;
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public string MsgId
        {
            get { return _MsgId; }
            set { _MsgId = value; }
        }
        public string ToXML()
        {
            string strXml = base.ToXML();
            strXml += string.Format("<Image><MediaId><![CDATA[{0}]]></MediaId></Image>",MediaId);
            strXml += string.Format("</xml>");
            return strXml;
        }

    }
    /// <summary>
    /// 用户发来的语音消息
    /// </summary>
    public class WeiXinVoiceMsg : WeiXinMessageBase
    {
        public WeiXinVoiceMsg()
        { }
        public WeiXinVoiceMsg(string weiXinMsg)
            : base(weiXinMsg)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(weiXinMsg);//读取xml字符串
            XmlElement root = doc.DocumentElement;
            XmlNode node = root.SelectSingleNode("MediaId");
            if (node != null)
            {
                MediaId = node.InnerText;
            }
            node = root.SelectSingleNode("MsgId");
            if (node != null)
            {
                this.MsgId = node.InnerText;
            }
            node = root.SelectSingleNode("Format");
            if (node != null)
            {
                this.Format = node.InnerText;
            }
            node = root.SelectSingleNode("MediaId");
            if (node != null)
            {
                this.MediaId = node.InnerText;
            }
            node=root.SelectSingleNode("MsgId");
            if (node != null)
            {
                this.MsgId = node.InnerText;
            }
        }
        private string _MediaId;
        private string _Format;
        private string _MsgID;
        /// <summary>
        /// 语音消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId
        {
            get { return _MediaId; }
            set { _MediaId = value; }
        }

        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        public string Format
        {
            get { return _Format; }
            set { _Format = value; }
        }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public string MsgId
        {
            get { return _MsgID; }
            set { _MsgID = value; }
        }


    }
    /// <summary>
    /// 用户发来的视频 消息
    /// </summary>
    public class WeiXinVideoMsg : WeiXinMessageBase
    {
        public WeiXinVideoMsg()
        { }
        public WeiXinVideoMsg(string weiXinMsg)
            : base(weiXinMsg)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(weiXinMsg);//读取xml字符串
            XmlElement root = doc.DocumentElement;
            XmlNode node = root.SelectSingleNode("MediaId");
            
            if (node != null)
            {
                this.MediaId = node.InnerText;
            }
            node = root.SelectSingleNode("ThumbMediaId");
            if (node != null)
            {
                this.ThumbMediaId = node.InnerText;
            }
            node = root.SelectSingleNode("MsgId");
            if (node != null)
            {
                this.MsgId = node.InnerText;
            }
        }

        private string _MediaId;
        /// <summary>
        /// 视频消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId
        {
            get { return _MediaId; }
            set { _MediaId = value; }
        }
        private string _ThumbMediaId;
        /// <summary>
        /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string ThumbMediaId
        {
            get { return _ThumbMediaId; }
            set { _ThumbMediaId = value; }
        }

        private string _MsgId;
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public string MsgId
        {
            get { return _MsgId; }
            set { _MsgId = value; }
        }
    }
    /// <summary>
    /// 用户发来的地理位置信息
    /// </summary>
    public class WeiXinLocationMsg : WeiXinMessageBase
    {
        public WeiXinLocationMsg()
        { }
        public WeiXinLocationMsg(string weiXinMsg)
            : base(weiXinMsg)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(weiXinMsg);//读取xml字符串
            XmlElement root = doc.DocumentElement;
            XmlNode node = root.SelectSingleNode("Location_X");
            if (node != null)
            {
                Location_X = node.InnerText;
            }
            node = root.SelectSingleNode("Location_Y");
            if (node != null)
            {
                Location_Y = node.InnerText;
            }
            node = root.SelectSingleNode("Scale");
            if (node != null)
            {
                Scale = node.InnerText;
            }
            node = root.SelectSingleNode("Label");
            if (node != null)
            {
                Label = node.InnerText;
            }
            node = root.SelectSingleNode("MsgId");
            if (node != null)
            {
                MsgId = node.InnerText;
            }
        }

        private string _Location_X;  
        /// <summary>
        /// 地理位置维度
        /// </summary>
        public string Location_X
        {
            get { return _Location_X; }
            set { _Location_X = value; }
        }
        private string _Location_Y;  
         /// <summary>
        /// 地理位置经度
         /// </summary>
        public string Location_Y
        {
            get { return _Location_Y; }
            set { _Location_Y = value; }
        }
        private string _Scale; 
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public string Scale
        {
            get { return _Scale; }
            set { _Scale = value; }
        }
        private string _Label; 
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label
        {
            get { return _Label; }
            set { _Label = value; }
        }
        private string _MsgId;  
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public string MsgId
        {
            get { return _MsgId; }
            set { _MsgId = value; }
        }
    }
    /// <summary>
    /// 用户发送超链接
    /// </summary>
    public class WeiXinLinkMsg : WeiXinMessageBase
    {
        public WeiXinLinkMsg() { }
        public WeiXinLinkMsg(string weiXinMsg)
            : base(weiXinMsg)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(weiXinMsg);//读取xml字符串
            XmlElement root = doc.DocumentElement;
            XmlNode node = root.SelectSingleNode("Title");
            if (node != null)
            {
                Title = node.InnerText;
            }
            node = root.SelectSingleNode("Description");
            if (node != null)
            {
                Description = node.InnerText;
            }
            node = root.SelectSingleNode("Url");
            if (node != null)
            {
                Url = node.InnerText;
            }
            node = root.SelectSingleNode("MsgId");
            if (node != null)
            {
                MsgId = node.InnerText;
            }
        }

        private string _Title; 
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _Description; 
        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _Url;	 
        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
        private string _MsgId;	  
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public string MsgId
        {
            get { return _MsgId; }
            set { _MsgId = value; }
        }
    }

    /// <summary>
    /// 返回XML格式的图文消息
    /// </summary>
    public class WeixinArticle : WeiXinMessageBase
    {
        public WeixinArticle(string weixinMsg)
            : base(weixinMsg)
        {

        }
    }

    /// <summary>
    /// 返回XML格式的图文消息
    /// </summary>
    public class WeixinTransfer : WeiXinMessageBase
    {
        public WeixinTransfer() { }
        public WeixinTransfer(string weixinMsg)
            : base(weixinMsg)
        {

        }
        public string ToXML()
        {
            string str = base.ToXML();
            str += string.Format("</xml>");
            return str;
        }
    }
}
