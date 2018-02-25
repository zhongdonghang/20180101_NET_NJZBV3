using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SeatManage.ClassModel
{
    public class MoveWebAppSetting
    {
        private bool isUseBespeak = true;
        /// <summary>
        /// 是否启用预约
        /// </summary>
        public bool IsUseBespeak
        {
            get { return isUseBespeak; }
            set { isUseBespeak = value; }
        }
        private bool isUseNowDayBespeak = false;
        /// <summary>
        /// 是否启用当天预约
        /// </summary>
        public bool IsUseNowDayBespeak
        {
            get { return isUseNowDayBespeak; }
            set { isUseNowDayBespeak = value; }
        }
        private bool isUseNextDayBespeak = true;
        /// <summary>
        /// 是否启用隔天预约
        /// </summary>
        public bool IsUseNextDayBespeak
        {
            get { return isUseNextDayBespeak; }
            set { isUseNextDayBespeak = value; }
        }

        private bool isCanShortLeave = true;
        /// <summary>
        /// 是否允许用户暂离
        /// </summary>
        public bool IsCanShortLeave
        {
            get { return isCanShortLeave; }
            set { isCanShortLeave = value; }
        }
        private bool isCanLeave = true;
        /// <summary>
        /// 是否允许用户释放座位
        /// </summary>
        public bool IsCanLeave
        {
            get { return isCanLeave; }
            set { isCanLeave = value; }
        }
        private bool isUseDcode = false;
        /// <summary>
        /// 是否启用二维码功能
        /// </summary>
        public bool IsUseDcode
        {
            get { return isUseDcode; }
            set { isUseDcode = value; }
        }
        private bool isCanDcodeShortLeave = true;
        /// <summary>
        /// 是否允许二维码暂离
        /// </summary>
        public bool IsCanDcodeShortLeave
        {
            get { return isCanDcodeShortLeave; }
            set { isCanDcodeShortLeave = value; }
        }
        private bool isCanDcodeLeave = true;
        /// <summary>
        /// 是否允许二维码释放座位
        /// </summary>
        public bool IsCanDcodeLeave
        {
            get { return isCanDcodeLeave; }
            set { isCanDcodeLeave = value; }
        }
        private bool isCanDcodeComeBack = false;
        /// <summary>
        /// 是否允许二维码暂离回来
        /// </summary>
        public bool IsCanDcodeComeBack
        {
            get { return isCanDcodeComeBack; }
            set { isCanDcodeComeBack = value; }
        }
        private bool isCanDcodeContinueTime = false;
        /// <summary>
        /// 是否允许二维码续时
        /// </summary>
        public bool IsCanDcodeContinueTime
        {
            get { return isCanDcodeContinueTime; }
            set { isCanDcodeContinueTime = value; }
        }
        private bool isCanDcodeCheckTime = false;
        /// <summary>
        /// 是否允许二维码签到
        /// </summary>
        public bool IsCanDcodeCheckTime
        {
            get { return isCanDcodeCheckTime; }
            set { isCanDcodeCheckTime = value; }
        }
        private bool isCanDcodeSelectSeat = false;
        /// <summary>
        /// 是否允许二维码选座
        /// </summary>
        public bool IsCanDcodeSelectSeat
        {
            get { return isCanDcodeSelectSeat; }
            set { isCanDcodeSelectSeat = value; }
        }
        private bool isCanDcodeReselectSeat = false;
        /// <summary>
        /// 是否允许二维码重选座位
        /// </summary>
        public bool IsCanDcodeReselectSeat
        {
            get { return isCanDcodeReselectSeat; }
            set { isCanDcodeReselectSeat = value; }
        }
        private bool isCanDcodeWaitSeat = false;
        /// <summary>
        /// 是否允许二维码等待座位
        /// </summary>
        public bool IsCanDcodeWaitSeat
        {
            get { return isCanDcodeWaitSeat; }
            set { isCanDcodeWaitSeat = value; }
        }

        /// <summary>
        /// 输出XML
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {

            return ConvertToXml(this);
        }
        /// <summary>
        /// 转换为mode
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public MoveWebAppSetting ToModel(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(xml);
                //查找根节点
                XmlNode node = xmlDoc.SelectSingleNode("//Root");
                this.IsCanDcodeCheckTime = ConfigConvert.ConvertToBool(node.Attributes["IsCanDcodeCheckTime"].Value);
                this.IsCanDcodeComeBack = ConfigConvert.ConvertToBool(node.Attributes["IsCanDcodeComeBack"].Value);
                this.IsCanDcodeContinueTime = ConfigConvert.ConvertToBool(node.Attributes["IsCanDcodeContinueTime"].Value);
                this.IsCanDcodeLeave = ConfigConvert.ConvertToBool(node.Attributes["IsCanDcodeLeave"].Value);
                this.IsCanDcodeReselectSeat = ConfigConvert.ConvertToBool(node.Attributes["IsCanDcodeReselectSeat"].Value);
                this.IsCanDcodeSelectSeat = ConfigConvert.ConvertToBool(node.Attributes["IsCanDcodeSelectSeat"].Value);
                this.IsCanDcodeShortLeave = ConfigConvert.ConvertToBool(node.Attributes["IsCanDcodeShortLeave"].Value);
                this.IsCanDcodeWaitSeat = ConfigConvert.ConvertToBool(node.Attributes["IsCanDcodeWaitSeat"].Value);
                this.IsCanLeave = ConfigConvert.ConvertToBool(node.Attributes["IsCanLeave"].Value);
                this.IsCanShortLeave = ConfigConvert.ConvertToBool(node.Attributes["IsCanShortLeave"].Value);
                this.IsUseBespeak = ConfigConvert.ConvertToBool(node.Attributes["IsUseBespeak"].Value);
                this.IsUseNextDayBespeak = ConfigConvert.ConvertToBool(node.Attributes["IsUseNextDayBespeak"].Value);
                this.IsUseNowDayBespeak = ConfigConvert.ConvertToBool(node.Attributes["IsUseNowDayBespeak"].Value);
                this.IsUseDcode = ConfigConvert.ConvertToBool(node.Attributes["IsUseDcode"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return this;
        }
        /// <summary>
        /// 输出xml
        /// </summary>
        /// <param name="stting"></param>
        /// <returns></returns>
        private string ConvertToXml(MoveWebAppSetting model)
        {
            //TODO:转换成xml结构的算法
            //创建一个xml对象
            XmlDocument xmlDoc = new XmlDocument();
            //创建开头
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(dec);
            //创建根节点
            XmlElement root = xmlDoc.CreateElement("Root");
            root.SetAttribute("IsCanDcodeCheckTime", ConfigConvert.ConvertToString(model.IsCanDcodeCheckTime));
            root.SetAttribute("IsCanDcodeComeBack", ConfigConvert.ConvertToString(model.IsCanDcodeComeBack));
            root.SetAttribute("IsCanDcodeContinueTime", ConfigConvert.ConvertToString(model.IsCanDcodeContinueTime));
            root.SetAttribute("IsCanDcodeLeave", ConfigConvert.ConvertToString(model.IsCanDcodeLeave));
            root.SetAttribute("IsCanDcodeReselectSeat", ConfigConvert.ConvertToString(model.IsCanDcodeReselectSeat));
            root.SetAttribute("IsCanDcodeSelectSeat", ConfigConvert.ConvertToString(model.IsCanDcodeSelectSeat));
            root.SetAttribute("IsCanDcodeShortLeave", ConfigConvert.ConvertToString(model.IsCanDcodeShortLeave));
            root.SetAttribute("IsCanDcodeWaitSeat", ConfigConvert.ConvertToString(model.IsCanDcodeWaitSeat));
            root.SetAttribute("IsCanLeave", ConfigConvert.ConvertToString(model.IsCanLeave));
            root.SetAttribute("IsCanShortLeave", ConfigConvert.ConvertToString(model.IsCanShortLeave));
            root.SetAttribute("IsUseBespeak", ConfigConvert.ConvertToString(model.IsUseBespeak));
            root.SetAttribute("IsUseNextDayBespeak", ConfigConvert.ConvertToString(model.IsUseNextDayBespeak));
            root.SetAttribute("IsUseNowDayBespeak", ConfigConvert.ConvertToString(model.IsUseNowDayBespeak));
            root.SetAttribute("IsUseDcode", ConfigConvert.ConvertToString(model.IsUseDcode));
            //添加根节点
            xmlDoc.AppendChild(root);
            return xmlDoc.OuterXml;
        }
    }
}
