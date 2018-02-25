using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SeatManage.ClassModel
{
    public class AccessSetting
    {
        //构造函数
        public AccessSetting()
        {

        }
        /// <summary>
        /// 执行转换
        /// </summary>
        /// <param name="settingXML"></param>
        public AccessSetting(string settingXML)
        {
            try
            {
                ToSetting(settingXML);
            }
            catch
            {
                throw;
            }
        }
        private bool _IsUsed = true;
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUsed
        {
            get { return _IsUsed; }
            set { _IsUsed = value; }
        }
        private bool _EnterLib = true;
        /// <summary>
        /// 是否开启入馆处理
        /// </summary>
        public bool EnterLib
        {
            get { return _EnterLib; }
            set { _EnterLib = value; }
        }
        private bool _OutLib = true;
        /// <summary>
        /// 开启出馆处理
        /// </summary>
        public bool OutLib
        {
            get { return _OutLib; }
            set { _OutLib = value; }
        }
        private SeatManage.EnumType.EnterOutLogType _LeaveMode = SeatManage.EnumType.EnterOutLogType.ShortLeave;
        /// <summary>
        /// 离开处理模式
        /// </summary>
        public SeatManage.EnumType.EnterOutLogType LeaveMode
        {
            get { return _LeaveMode; }
            set { _LeaveMode = value; }
        }
        private bool _IsReleaseOnSeat = true;
        /// <summary>
        /// 入馆是否处理未刷卡离开读者
        /// </summary>
        public bool IsReleaseOnSeat
        {
            get { return _IsReleaseOnSeat; }
            set { _IsReleaseOnSeat = value; }
        }
        private bool _IsComeBack = true;
        /// <summary>
        /// 是否把暂离的读者设置为回来
        /// </summary>
        public bool IsComeBack
        {
            get { return _IsComeBack; }
            set { _IsComeBack = value; }
        }
        private bool _IsBookingConfinmed = true;
        /// <summary>
        /// 是否确认预约
        /// </summary>
        public bool IsBookingConfinmed
        {
            get { return _IsBookingConfinmed; }
            set { _IsBookingConfinmed = value; }
        }
        private bool _AddViolationRecords = true;
        /// <summary>
        /// 是否添加违规记录
        /// </summary>
        public bool AddViolationRecords
        {
            get { return _AddViolationRecords; }
            set { _AddViolationRecords = value; }
        }
        private int _LeaveTimeSpan = 5;
        /// <summary>
        /// 处理时间间隔
        /// </summary>
        public int LeaveTimeSpan
        {
            get { return _LeaveTimeSpan; }
            set { _LeaveTimeSpan = value; }
        }
        private bool _IsLimitBlackList = true;
        /// <summary>
        /// 是否限制黑名单进入
        /// </summary>
        public bool IsLimitBlackList
        {
            get { return _IsLimitBlackList; }
            set { _IsLimitBlackList = value; }
        }
        public override string ToString()
        {
            return ToXML(this);
        }
        public AccessSetting ToSetting(string Xml)
        {
            XmlDocument xmlDocSet = new XmlDocument();
            xmlDocSet.LoadXml(Xml);
            XmlNode node = xmlDocSet.SelectSingleNode("//rootNode/AccessSetting");
            if (node == null)
            {
                this._IsUsed = true;
            }
            else
            {
                this._IsUsed = bool.Parse(node.Attributes["IsUsed"].Value);
            }
            node = xmlDocSet.SelectSingleNode("//rootNode/AccessSetting/EnterLib");
            if (node == null)
            {
                this._EnterLib = true;
                this._IsReleaseOnSeat = true;
                this._IsComeBack = true;
                this._IsBookingConfinmed = true;
                this._LeaveTimeSpan = 5;
            }
            else
            {
                this._EnterLib = bool.Parse(node.Attributes["IsUsed"].Value);
                this._IsReleaseOnSeat = bool.Parse(node.Attributes["IsReleaseOnSeat"].Value);
                this._IsComeBack = bool.Parse(node.Attributes["IsComeBack"].Value);
                this._IsBookingConfinmed = bool.Parse(node.Attributes["IsBookingConfinmed"].Value);
                this._LeaveTimeSpan = int.Parse(node.Attributes["LeaveTimeSpan"].Value);
            }
            node = xmlDocSet.SelectSingleNode("//rootNode/AccessSetting/OutLib");
            if (node == null)
            {
                this._OutLib = true;
                this.LeaveMode = SeatManage.EnumType.EnterOutLogType.ShortLeave;
            }
            else
            {
                this._OutLib = bool.Parse(node.Attributes["IsUsed"].Value);
                this.LeaveMode = (SeatManage.EnumType.EnterOutLogType)int.Parse(node.Attributes["LeaveMode"].Value);
            }
            node = xmlDocSet.SelectSingleNode("//rootNode/AccessSetting/BlackList");
            if (node == null)
            {
                this._IsLimitBlackList = true;
                this._AddViolationRecords = true;
            }
            else
            {
                this._IsLimitBlackList = bool.Parse(node.Attributes["IsUsed"].Value);
                this._AddViolationRecords = bool.Parse(node.Attributes["AddViolationRecords"].Value);
            }

            return this;
        }
        /// <summary>
        /// 转换成xml
        /// </summary>
        /// <param name="accset"></param>
        /// <returns></returns>
        public static string ToXML(AccessSetting accset)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("rootNode");//创建根节点
            //是否启用
            XmlElement secNode = doc.CreateElement("AccessSetting");
            secNode.SetAttribute("IsUsed", accset.IsUsed.ToString());
            //入馆设置
            XmlElement thrNode = doc.CreateElement("EnterLib");
            thrNode.SetAttribute("IsUsed", accset.EnterLib.ToString());
            thrNode.SetAttribute("IsReleaseOnSeat", accset.IsReleaseOnSeat.ToString());
            thrNode.SetAttribute("IsComeBack", accset.IsComeBack.ToString());
            thrNode.SetAttribute("IsBookingConfinmed", accset.IsBookingConfinmed.ToString());
            thrNode.SetAttribute("LeaveTimeSpan", accset.LeaveTimeSpan.ToString());
            secNode.AppendChild(thrNode);
            //出馆设置
            thrNode = doc.CreateElement("OutLib");
            thrNode.SetAttribute("IsUsed", accset.OutLib.ToString());
            thrNode.SetAttribute("LeaveMode", ((int)accset.LeaveMode).ToString());
            secNode.AppendChild(thrNode);
            //黑名单设置
            thrNode = doc.CreateElement("BlackList");
            thrNode.SetAttribute("IsUsed", accset.IsLimitBlackList.ToString());
            thrNode.SetAttribute("AddViolationRecords", accset.AddViolationRecords.ToString());
            secNode.AppendChild(thrNode);
            root.AppendChild(secNode);
            doc.AppendChild(root);
            return doc.OuterXml;
        }
    }
}
