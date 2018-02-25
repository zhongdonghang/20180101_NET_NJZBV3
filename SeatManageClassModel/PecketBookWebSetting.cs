using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SeatManage.ClassModel
{
    public class PecketBookWebSetting
    {
        private bool _UseShortLeave = true;
        /// <summary>
        /// 允许用户设置座位暂离
        /// </summary>
        public bool UseShortLeave
        {
            get { return _UseShortLeave; }
            set { _UseShortLeave = value; }
        }
        private bool _UseComeBack = false;
        /// <summary>
        /// 允许用户暂离回来
        /// </summary>
        public bool UseComeBack
        {
            get { return _UseComeBack; }
            set { _UseComeBack = value; }
        }
        private bool _UseCanLeave = true;
        /// <summary>
        /// 允许用户释放座位
        /// </summary>
        public bool UseCanLeave
        {
            get { return _UseCanLeave; }
            set { _UseCanLeave = value; }
        }
        private bool _UseContinue = false;
        /// <summary>
        /// 允许用户续时
        /// </summary>
        public bool UseContinue
        {
            get { return _UseContinue; }
            set { _UseContinue = value; }
        }
        private bool _UseWaitSeat = false;
        /// <summary>
        /// 允许用户等待座位
        /// </summary>
        public bool UseWaitSeat
        {
            get { return _UseWaitSeat; }
            set { _UseWaitSeat = value; }
        }
        private bool _UseCancelWait = false;
        /// <summary>
        /// 允许用户取消座位等待
        /// </summary>
        public bool UseCancelWait
        {
            get { return _UseCancelWait; }
            set { _UseCancelWait = value; }
        }
        private bool _UseBookComfirm = false;
        /// <summary>
        /// 允许用户预约签到
        /// </summary>
        public bool UseBookComfirm
        {
            get { return _UseBookComfirm; }
            set { _UseBookComfirm = value; }
        }
        private bool _UseBookSeat = true;
        /// <summary>
        /// 允许用户预约座位
        /// </summary>
        public bool UseBookSeat
        {
            get { return _UseBookSeat; }
            set { _UseBookSeat = value; }
        }
        private bool _UseBookNowDaySeat = true;
        /// <summary>
        /// 允许用户预约当天座位
        /// </summary>
        public bool UseBookNowDaySeat
        {
            get { return _UseBookNowDaySeat; }
            set { _UseBookNowDaySeat = value; }
        }
        private bool _UseBookNextDaySeat = true;
        /// <summary>
        /// 允许用户预约隔天座位
        /// </summary>
        public bool UseBookNextDaySeat
        {
            get { return _UseBookNextDaySeat; }
            set { _UseBookNextDaySeat = value; }
        }
        private bool _UseCancelBook = true;
        /// <summary>
        /// 允许用户取消预约
        /// </summary>
        public bool UseCancelBook
        {
            get { return _UseCancelBook; }
            set { _UseCancelBook = value; }
        }
        private bool _UseSelectSeat = false;
        /// <summary>
        /// 允许用户选择座位
        /// </summary>
        public bool UseSelectSeat
        {
            get { return _UseSelectSeat; }
            set { _UseSelectSeat = value; }
        }
        private bool _UseChangeSeat = true;
        /// <summary>
        /// 允许用户更换座位
        /// </summary>
        public bool UseChangeSeat
        {
            get { return _UseChangeSeat; }
            set { _UseChangeSeat = value; }
        }
        private bool _UseSelectLog = true;
        /// <summary>
        /// 空构造函数
        /// </summary>
        public PecketBookWebSetting()
        { }
        /// <summary>
        /// 初始化构造函数
        /// </summary>
        /// <param name="xml"></param>
        public PecketBookWebSetting(string xml)
        {
            try
            {
                ToModel(xml);
            }
            catch
            {
                throw;
            }
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
        public PecketBookWebSetting ToModel(string xml)
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
                this.UseBookComfirm = ConfigConvert.ConvertToBool(node.Attributes["UseBookComfirm"].Value);
                this.UseBookNextDaySeat = ConfigConvert.ConvertToBool(node.Attributes["UseBookNextDaySeat"].Value);
                this.UseBookNowDaySeat = ConfigConvert.ConvertToBool(node.Attributes["UseBookNowDaySeat"].Value);
                this.UseBookSeat = ConfigConvert.ConvertToBool(node.Attributes["UseBookSeat"].Value);
                this.UseCancelBook = ConfigConvert.ConvertToBool(node.Attributes["UseCancelBook"].Value);
                this.UseCancelWait = ConfigConvert.ConvertToBool(node.Attributes["UseCancelWait"].Value);
                this.UseCanLeave = ConfigConvert.ConvertToBool(node.Attributes["UseCanLeave"].Value);
                this.UseComeBack = ConfigConvert.ConvertToBool(node.Attributes["UseComeBack"].Value);
                this.UseContinue = ConfigConvert.ConvertToBool(node.Attributes["UseContinue"].Value);
                this.UseShortLeave = ConfigConvert.ConvertToBool(node.Attributes["UseShortLeave"].Value);
                this.UseWaitSeat = ConfigConvert.ConvertToBool(node.Attributes["UseWaitSeat"].Value);
                if (node.Attributes["UseSelectSeat"] != null)
                {
                    this.UseSelectSeat = ConfigConvert.ConvertToBool(node.Attributes["UseSelectSeat"].Value);
                }
                if (node.Attributes["UseChangeSeat"] != null)
                {
                    this.UseChangeSeat = ConfigConvert.ConvertToBool(node.Attributes["UseChangeSeat"].Value);
                }
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
        private string ConvertToXml(PecketBookWebSetting model)
        {
            //TODO:转换成xml结构的算法
            //创建一个xml对象
            XmlDocument xmlDoc = new XmlDocument();
            //创建开头
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(dec);
            //创建根节点
            XmlElement root = xmlDoc.CreateElement("Root");
            root.SetAttribute("UseBookComfirm", ConfigConvert.ConvertToString(model.UseBookComfirm));
            root.SetAttribute("UseBookNextDaySeat", ConfigConvert.ConvertToString(model.UseBookNextDaySeat));
            root.SetAttribute("UseBookNowDaySeat", ConfigConvert.ConvertToString(model.UseBookNowDaySeat));
            root.SetAttribute("UseBookSeat", ConfigConvert.ConvertToString(model.UseBookSeat));
            root.SetAttribute("UseCancelBook", ConfigConvert.ConvertToString(model.UseCancelBook));
            root.SetAttribute("UseCancelWait", ConfigConvert.ConvertToString(model.UseCancelWait));
            root.SetAttribute("UseCanLeave", ConfigConvert.ConvertToString(model.UseCanLeave));
            root.SetAttribute("UseComeBack", ConfigConvert.ConvertToString(model.UseComeBack));
            root.SetAttribute("UseContinue", ConfigConvert.ConvertToString(model.UseContinue));
            root.SetAttribute("UseShortLeave", ConfigConvert.ConvertToString(model.UseShortLeave));
            root.SetAttribute("UseWaitSeat", ConfigConvert.ConvertToString(model.UseWaitSeat));
            root.SetAttribute("UseSelectSeat", ConfigConvert.ConvertToString(model.UseSelectSeat));
            root.SetAttribute("UseChangeSeat", ConfigConvert.ConvertToString(model.UseChangeSeat));
            //添加根节点
            xmlDoc.AppendChild(root);
            return xmlDoc.OuterXml;
        }
    }
}
