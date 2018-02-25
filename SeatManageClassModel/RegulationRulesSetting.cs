using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SeatManage.EnumType;

namespace SeatManage.ClassModel
{
    /// <summary>
    /// 阅览室的管理规则设置
    /// </summary>
    [Serializable]
    public class RegulationRulesSetting
    {
        static XmlDocument doc;
        BlacklistSetting _BlacklistSet = new BlacklistSetting();
        public RegulationRulesSetting()
        {

        }
        /// <summary>
        /// 黑名单设置
        /// </summary>
        public BlacklistSetting BlacklistSet
        {
            get { return _BlacklistSet; }
            set { _BlacklistSet = value; }
        }
        List<RealNameSeat> _RealNameSeatObj = new List<RealNameSeat>();
        /// <summary>
        /// 实名制座位
        /// </summary>
        public List<RealNameSeat> RealNameSeatList
        {
            get { return _RealNameSeatObj; }
            set { _RealNameSeatObj = value; }
        }
        public override string ToString()
        {
            return Convert(this);
        }

        /// <summary>
        /// 创建黑名单设置节点
        /// </summary>
        /// <param name="blacklist"></param>
        /// <returns></returns>
        private static string Convert(RegulationRulesSetting set)
        {
            doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("regulationRulesSetting");
            XmlElement blacklistNode = ConvertBlacklistSet(set.BlacklistSet);
            XmlElement realNameSeatList = ConvertRealNameSeat(set.RealNameSeatList);
            root.AppendChild(blacklistNode);
            root.AppendChild(realNameSeatList);
            doc.AppendChild(root);
            return doc.OuterXml.ToString();
        }
        /// <summary>
        /// 解析黑名单设置节点
        /// </summary>
        /// <param name="xmlBlacklist">黑名单的Xml</param>
        /// <returns></returns>
        public static RegulationRulesSetting Convert(string xmlBlacklist)
        {
            doc = new XmlDocument();
            RegulationRulesSetting set = new RegulationRulesSetting();
            if (!string.IsNullOrEmpty(xmlBlacklist))
            {
                doc.LoadXml(xmlBlacklist);
                XmlNode node = doc.SelectSingleNode("//regulationRulesSetting/blacklist");
                if (node != null)
                {
                    set.BlacklistSet = ConvertBlacklistSet(node);
                }
                node = doc.SelectSingleNode("//regulationRulesSetting/realNameSeatList");
                if (node != null)
                {
                    set.RealNameSeatList = ConvertRealNameSeat(node);
                }
            }
            return set;

        }

        private static XmlElement ConvertBlacklistSet(BlacklistSetting blacklist)
        {
            XmlElement element = doc.CreateElement("blacklist");
            element.SetAttribute("used", ConfigConvert.ConvertToString(blacklist.Used));
            element.SetAttribute("violateTimes", blacklist.ViolateTimes.ToString());
            element.SetAttribute("limitDays", blacklist.LimitDays.ToString());
            element.SetAttribute("leaveBlacklist", ((int)blacklist.LeaveBlacklist).ToString());
            element.SetAttribute("ViolateFailDays", blacklist.ViolateFailDays.ToString());
            foreach (ViolationRecordsType violateType in blacklist.ViolateRoule.Keys)
            {
                XmlElement child = doc.CreateElement("violateType");
                child.SetAttribute("used", ConfigConvert.ConvertToString(blacklist.ViolateRoule[violateType]));
                child.SetAttribute("typeValue", ((int)violateType).ToString());
                element.AppendChild(child);
            }
            return element;
        }
        private static BlacklistSetting ConvertBlacklistSet(XmlNode node)
        {
            //node = doc.SelectSingleNode("//blacklist");
            BlacklistSetting set = new BlacklistSetting();
            set.LeaveBlacklist = (LeaveBlacklistMode)int.Parse(node.Attributes["leaveBlacklist"].Value);
            set.LimitDays = int.Parse(node.Attributes["limitDays"].Value);
            set.Used = ConfigConvert.ConvertToBool(node.Attributes["used"].Value);
            set.ViolateTimes = int.Parse(node.Attributes["violateTimes"].Value);
            set.ViolateFailDays = int.Parse(node.Attributes["ViolateFailDays"].Value);
            XmlNodeList nodes = node.ChildNodes;// SelectNodes("//blacklist/violateType");
            foreach (XmlNode element in nodes)
            {
                set.ViolateRoule[(ViolationRecordsType)int.Parse(element.Attributes["typeValue"].Value)] = ConfigConvert.ConvertToBool(element.Attributes["used"].Value);
            }
            return set;
        }

        private static List<RealNameSeat> ConvertRealNameSeat(XmlNode node)
        {
            List<RealNameSeat> seatList = new List<RealNameSeat>();
            foreach (XmlNode childNode in node.ChildNodes)
            {
                RealNameSeat realNameSeat = new RealNameSeat();
                realNameSeat.CardNo = childNode.Attributes["cardNo"].Value;
                realNameSeat.SeatNum = childNode.Attributes["seatNum"].Value;
                seatList.Add(realNameSeat);
            }
            return seatList;
        }
        private static XmlElement ConvertRealNameSeat(List<RealNameSeat> realNameSeatList)
        {
            XmlElement element = doc.CreateElement("realNameSeatList");
            for (int i = 0; i < realNameSeatList.Count; i++)
            {
                XmlElement childElement = doc.CreateElement("realNameSeat");
                childElement.SetAttribute("cardNo", realNameSeatList[i].CardNo);
                childElement.SetAttribute("seatNum", realNameSeatList[i].SeatNum);
                element.AppendChild(childElement);
            }
            return element;
        }


    }
    /// <summary>
    /// 实名制座位
    /// </summary>
    public class RealNameSeat
    {
        string _SeatNum = "";
        /// <summary>
        /// 座位号
        /// </summary>
        public string SeatNum
        {
            get { return _SeatNum; }
            set { _SeatNum = value; }
        }
        string _CardNo = "";
        /// <summary>
        /// 学号
        /// </summary>
        public string CardNo
        {
            get { return _CardNo; }
            set { _CardNo = value; }
        }


    }

    /// <summary>
    /// 黑名单设置
    /// </summary>
    [Serializable]
    public class BlacklistSetting
    {
        private bool _Used = true;
        private Dictionary<ViolationRecordsType, bool> _ViolateRoule = new Dictionary<ViolationRecordsType, bool>();
        private int _ViolateTimes = 3;
        private LeaveBlacklistMode _LeaveBlacklist = LeaveBlacklistMode.AutomaticMode;
        private int _LimitDays = 7;
        private int _ViolateFailDays = 60;
        public BlacklistSetting()
        {
            //初始化7种违规规则
            _ViolateRoule.Add(ViolationRecordsType.BookingTimeOut
                             , true);
            _ViolateRoule.Add(ViolationRecordsType.LeaveByAdmin
                            , true);
            _ViolateRoule.Add(ViolationRecordsType.SeatOutTime
                            , false);
            _ViolateRoule.Add(ViolationRecordsType.ShortLeaveByAdminOutTime
                            , true);
            _ViolateRoule.Add(ViolationRecordsType.ShortLeaveByReaderOutTime
                            , true);
            _ViolateRoule.Add(ViolationRecordsType.ShortLeaveByServiceOutTime
                            , false);
            _ViolateRoule.Add(ViolationRecordsType.ShortLeaveOutTime
                            , false);
            _ViolateRoule.Add(ViolationRecordsType.CancelWaitByAdmin
                            , false);
        }
        /// <summary>
        /// 违规规则，值为true说明需要记录违规
        /// </summary>
        public Dictionary<ViolationRecordsType, bool> ViolateRoule
        {
            get { return _ViolateRoule; }
            set { _ViolateRoule = value; }
        }
        /// <summary>
        /// 是否使用黑名单设置
        /// </summary>
        public bool Used
        {
            get { return _Used; }
            set { _Used = value; }
        }

        /// <summary>
        /// 违规次数，超过该次数加到黑名单
        /// </summary>
        public int ViolateTimes
        {
            get { return _ViolateTimes; }
            set { _ViolateTimes = value; }
        }
        /// <summary>
        /// 离开黑名单方式
        /// </summary>
        public LeaveBlacklistMode LeaveBlacklist
        {
            get { return _LeaveBlacklist; }
            set { _LeaveBlacklist = value; }
        }
        /// <summary>
        /// 限制天数
        /// </summary>
        public int LimitDays
        {
            get { return _LimitDays; }
            set { _LimitDays = value; }
        }
        /// <summary>
        /// 违规失效天数
        /// </summary>
        public int ViolateFailDays
        {
            get { return _ViolateFailDays; }
            set { _ViolateFailDays = value; }
        }

    }
}
