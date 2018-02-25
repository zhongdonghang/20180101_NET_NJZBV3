using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SeatManage.ClassModel
{
    public class StudyRoomSetting
    {
        /// <summary>
        /// 空构造函数
        /// </summary>
        public StudyRoomSetting()
        { }
        /// <summary>
        /// 初始化构造函数
        /// </summary>
        /// <param name="xml"></param>
        public StudyRoomSetting(string xml)
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
        public StudyRoomSetting ToModel(string xml)
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
                if (node.Attributes["IsUseStudyRoom"] != null)
                {
                    this.IsUseStudyRoom = bool.Parse(node.Attributes["IsUseStudyRoom"].Value);
                }

                this.OpenTime = DateTime.Parse(node.Attributes["OpenTime"].Value);
                this.CloseTime = DateTime.Parse(node.Attributes["CloseTime"].Value);
                this.MaxBookTime = int.Parse(node.Attributes["MaxBookTime"].Value);
                this.FacilitiesRenmark = node.Attributes["FacilitiesRenmark"].Value;
                this.ApplicationInfo = node.Attributes["ApplicationInfo"].Value;
                this.Precautions = node.Attributes["Precautions"].Value;
                if (node.Attributes["CanUseFacilities"] != null)
                {
                    this.CanUseFacilities = node.Attributes["CanUseFacilities"].Value;
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
        private string ConvertToXml(StudyRoomSetting model)
        {
            //TODO:转换成xml结构的算法
            //创建一个xml对象
            XmlDocument xmlDoc = new XmlDocument();
            //创建开头
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(dec);
            //创建根节点
            XmlElement root = xmlDoc.CreateElement("Root");
            root.SetAttribute("IsUseStudyRoom", model.IsUseStudyRoom.ToString());
            root.SetAttribute("OpenTime", model.OpenTime.ToShortTimeString());
            root.SetAttribute("CloseTime", model.CloseTime.ToShortTimeString());
            root.SetAttribute("MaxBookTime", model.MaxBookTime.ToString());
            root.SetAttribute("FacilitiesRenmark", model.FacilitiesRenmark);
            root.SetAttribute("ApplicationInfo", model.ApplicationInfo);
            root.SetAttribute("Precautions", model.Precautions);
            root.SetAttribute("CanUseFacilities", model.CanUseFacilities);

            //添加根节点
            xmlDoc.AppendChild(root);
            return xmlDoc.OuterXml;
        }

        private bool _IsUseStudyRoom = true;
        /// <summary>
        /// 是否启用研习间
        /// </summary>
        public bool IsUseStudyRoom
        {
            get { return _IsUseStudyRoom; }
            set { _IsUseStudyRoom = value; }
        }

        private DateTime _OpenTime = DateTime.Parse("8:00");
        /// <summary>
        /// 开放时间
        /// </summary>
        public DateTime OpenTime
        {
            get { return _OpenTime; }
            set { _OpenTime = value; }
        }
        private DateTime _CloseTime = DateTime.Parse("22:00");
        /// <summary>
        /// 关闭时间
        /// </summary>
        public DateTime CloseTime
        {
            get { return _CloseTime; }
            set { _CloseTime = value; }
        }
        private int _MaxBookTime = 120;
        /// <summary>
        /// 最大使用时间
        /// </summary>
        public int MaxBookTime
        {
            get { return _MaxBookTime; }
            set { _MaxBookTime = value; }
        }
        private string _FacilitiesRenmark = "无";
        /// <summary>
        /// 设施设备描述
        /// </summary>
        public string FacilitiesRenmark
        {
            get { return _FacilitiesRenmark; }
            set { _FacilitiesRenmark = value; }
        }
        private string _Precautions = "无";
        /// <summary>
        /// 注事事项
        /// </summary>
        public string Precautions
        {
            get { return _Precautions; }
            set { _Precautions = value; }
        }
        private string _ApplicationInfo = "无";
        /// <summary>
        /// 申请说明
        /// </summary>
        public string ApplicationInfo
        {
            get { return _ApplicationInfo; }
            set { _ApplicationInfo = value; }
        }
        private string _CanUseFacilities = "电脑;投影仪";
        /// <summary>
        /// 可使用设备
        /// </summary>
        public string CanUseFacilities
        {
            get { return _CanUseFacilities; }
            set { _CanUseFacilities = value; }
        }

    }
}
