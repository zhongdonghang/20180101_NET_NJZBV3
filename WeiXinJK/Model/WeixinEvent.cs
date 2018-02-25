using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WeiXinJK.Model
{
    /// <summary>
    /// 事件
    /// </summary>
    public class WeixinEventMsg : WeiXinMessageBase
    {
        private EnumWeiXinEvent _MsgEvent;
        /// <summary>
        /// 事件Event
        /// </summary>
        public EnumWeiXinEvent MsgEvent
        {
            get { return _MsgEvent; }
            set { _MsgEvent = value; }
        }
        public WeixinEventMsg(string weixinMsg)
            : base(weixinMsg)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(weixinMsg);//读取xml字符串
            XmlElement root = doc.DocumentElement;
            XmlNode MsgEvent = root.SelectSingleNode("Event");//获取收到的事件类型
            switch (MsgEvent.InnerText)
            {
                case "subscribe":
                    this.MsgEvent = EnumWeiXinEvent.subscribe;
                    break;
                case "unsubscribe":
                    this.MsgEvent = EnumWeiXinEvent.unsubscribe;
                    break;
                case "CLICK":
                    this.MsgEvent = EnumWeiXinEvent.CLICK;
                    break;
                case "VIEW":
                    this.MsgEvent = EnumWeiXinEvent.VIEW;
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class WexinEventClickMenu : WeixinEventMsg
    {
        private EnumMenuKey _EventKey;
        /// <summary>
        /// 事件的KEY值
        /// </summary>
        public EnumMenuKey EventKey
        {
            get { return _EventKey; }
            set { _EventKey = value; }
        }
        public WexinEventClickMenu(string weixinMsg)
            : base(weixinMsg)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(weixinMsg);//读取xml字符串
            XmlElement root = doc.DocumentElement;
            XmlNode node = root.SelectSingleNode("EventKey");
            if (node != null)
            {
                switch (node.InnerText)
                {
                    case "0":
                        this.EventKey = EnumMenuKey.BindWeiXinId;
                        break;
                    case "1":
                        this.EventKey = EnumMenuKey.GetBespeakLog;
                        break;
                    case "2":
                        this.EventKey = EnumMenuKey.GetRoomUsedState;
                        break;
                    case "3":
                        this.EventKey = EnumMenuKey.ShortLeave;
                        break;
                    case "4":
                        this.EventKey = EnumMenuKey.FreeSeat;
                        break;
                    case "5":
                        this.EventKey = EnumMenuKey.ReserveSeat;
                        break;
                    case "6":
                        this.EventKey = EnumMenuKey.BlackList;
                        break;
                    case "7":
                        this.EventKey = EnumMenuKey.GetReaderState;
                        break;
                    case "8":
                        this.EventKey = EnumMenuKey.GetRules;
                        break;
                    case "9":
                        this.EventKey = EnumMenuKey.GetMyInfo;
                        break;
                    case "10":
                        this.EventKey = EnumMenuKey.ReservationService;
                        break;
                    case "11":
                        this.EventKey = EnumMenuKey.Weather;
                        break;
                    case "12":
                        this.EventKey = EnumMenuKey.Feedback;
                        break;
                    case "13":
                        this.EventKey = EnumMenuKey.Press;
                        break;
                    case "14":
                        this.EventKey = EnumMenuKey.Service;
                        break;
                    case "15":
                        this.EventKey = EnumMenuKey.CancelWait;
                        break;
                    case "16":
                        this.EventKey = EnumMenuKey.CancelBesapeak;
                        break;
                    case "17":
                        this.EventKey = EnumMenuKey.GetUsageLog;
                        break;
                    default:
                        this.EventKey = EnumMenuKey.None;
                        break;
                }
            }
        }
    }
}


