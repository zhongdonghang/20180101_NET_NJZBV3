using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SeatManage.ClassModel
{
    /// <summary>
    /// 阅览室信息
    /// </summary>
      [Serializable]
   public class LibraryInfo:Room
    {
       School _School = new School();

        public School School
        {
            get { return _School; }
            set { _School = value; }
        }
        List<AreaInfo> _AreaList = new List<AreaInfo>();
        /// <summary>
        /// 区域列表
        /// </summary>
        public List<AreaInfo> AreaList
        {
            get { return _AreaList; }
            set { _AreaList = value; }
        }
        public string AreaToXml()
        {
            //TODO:转换成xml结构的算法
            //创建一个xml对象
            XmlDocument xmlDoc = new XmlDocument();
            //创建开头
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(dec);
            //创建根节点
            XmlElement root = xmlDoc.CreateElement("Root");
            XmlElement SecNode = xmlDoc.CreateElement("AreaList");
            foreach (AreaInfo item in this.AreaList)
            {
                XmlElement ThrNode = xmlDoc.CreateElement("Area");
                ThrNode.SetAttribute("AreaName", item.AreaName);
                ThrNode.SetAttribute("AreaNo", item.AreaNo.ToString());
                SecNode.AppendChild(ThrNode);
            }
            //在根节点中添加二级节点
            root.AppendChild(SecNode);
            //添加根节点
            xmlDoc.AppendChild(root);
            return xmlDoc.OuterXml;
        }
        public List<AreaInfo> ToList(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return new List<AreaInfo>();
            }
            XmlDocument xmlDoc = new XmlDocument();
            List<AreaInfo> modelList = new List<AreaInfo>();
            try
            {
                xmlDoc.LoadXml(xml);
                XmlNodeList nodes = xmlDoc.SelectNodes("//Root/AreaList/Area");
                foreach (XmlNode node in nodes)
                {
                    AreaInfo item = new AreaInfo();
                    item.AreaName = node.Attributes["AreaName"].Value;
                    item.AreaNo = int.Parse(node.Attributes["AreaNo"].Value);
                    modelList.Add(item);
                }
                return modelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
