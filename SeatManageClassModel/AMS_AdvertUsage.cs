using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SeatManage.ClassModel
{
    public class AMS_AdvertUsage : AdvertisementUsage
    {
        private int _ID = -1;
        /// <summary>
        /// id
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int _AdvertID = -1;
        /// <summary>
        /// 广告ID
        /// </summary>
        public int AdvertID
        {
            get { return _AdvertID; }
            set { _AdvertID = value; }
        }

        private SeatManage.EnumType.AdType _AdvertType = EnumType.AdType.None;
        /// <summary>
        /// 广告类型
        /// </summary>
        public SeatManage.EnumType.AdType AdvertType
        {
            get { return _AdvertType; }
            set { _AdvertType = value; }
        }
        private DateTime _LastUpdateTime = new DateTime();
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime
        {
            get { return _LastUpdateTime; }
            set { _LastUpdateTime = value; }
        }
        private string _AdvertUsage = "";
        /// <summary>
        /// 使用情况的XML
        /// </summary>
        public string AdvertUsage
        {
            get { return _AdvertUsage; }
            set { _AdvertUsage = value; }
        }

        private Dictionary<string, AdvertisementUsage> _ItemUsage = new Dictionary<string, AdvertisementUsage>();
        /// <summary>
        /// 子项使用情况
        /// </summary>
        public Dictionary<string, AdvertisementUsage> ItemUsage
        {
            get { return _ItemUsage; }
            set { _ItemUsage = value; }
        }
        /// <summary>
        /// 清除记录
        /// </summary>
        public void Clean()
        {
            this.PlayCount = 0;
            this.PrintCount = 0;
            this.WatchCount = 0;
            foreach (KeyValuePair<string, AdvertisementUsage> item in this.ItemUsage)
            {
                item.Value.PlayCount = 0;
                item.Value.PrintCount = 0;
                item.Value.WatchCount = 0;
            }
        }
        #region Xml转换
        /// <summary>
        /// 自身转换Xml
        /// </summary>
        /// <returns></returns>
        public string ToXml()
        {
            return ToXml(this);
        }
        /// <summary>
        /// 转换成Xml
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string ToXml(AMS_AdvertUsage model)
        {
            //TODO:转换成xml结构的算法
            //创建一个xml对象
            XmlDocument xmlDoc = new XmlDocument();
            //创建开头
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(dec);
            //创建根节点
            XmlElement root = xmlDoc.CreateElement("Root");
            root.SetAttribute("AdvertNum", model.AdvertNum.ToString());
            root.SetAttribute("AdvertType", model.AdvertType.ToString());
            root.SetAttribute("PlayCount", model.PlayCount.ToString());
            root.SetAttribute("WatchCount", model.WatchCount.ToString());
            root.SetAttribute("PrintCount", model.PrintCount.ToString());
            XmlElement SecNode = xmlDoc.CreateElement("AdvertItem");
            foreach (KeyValuePair<string, AdvertisementUsage> item in model.ItemUsage)
            {
                XmlElement ThrNode = xmlDoc.CreateElement("Item");
                ThrNode.SetAttribute("AdvertNum", item.Value.AdvertNum.ToString());
                ThrNode.SetAttribute("PlayCount", item.Value.PlayCount.ToString());
                ThrNode.SetAttribute("WatchCount", item.Value.WatchCount.ToString());
                ThrNode.SetAttribute("PrintCount", item.Value.PrintCount.ToString());
                SecNode.AppendChild(ThrNode);
            }
            //在根节点中添加二级节点
            root.AppendChild(SecNode);
            //添加根节点
            xmlDoc.AppendChild(root);
            return xmlDoc.OuterXml;
        }
        /// <summary>
        /// 转换成Moldel
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static AMS_AdvertUsage ToModel(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            XmlDocument xmlDoc = new XmlDocument();
            AMS_AdvertUsage model = new AMS_AdvertUsage();
            try
            {
                xmlDoc.LoadXml(xml);
                //查找根节点
                XmlNode node = xmlDoc.SelectSingleNode("//Root");
                model.AdvertNum = node.Attributes["AdvertNum"].Value;//列表编号
                model.PlayCount = int.Parse(node.Attributes["PlayCount"].Value);
                model.WatchCount = int.Parse(node.Attributes["WatchCount"].Value);
                model.PrintCount = int.Parse(node.Attributes["PrintCount"].Value);
                model.AdvertType = (SeatManage.EnumType.AdType)System.Enum.Parse(typeof(SeatManage.EnumType.AdType), node.Attributes["AdvertType"].Value);
                XmlNodeList nodes = xmlDoc.SelectNodes("//Root/AdvertItem/Item");
                foreach (XmlNode ItemNode in nodes)
                {
                    AdvertisementUsage item = new AdvertisementUsage();
                    item.AdvertNum = ItemNode.Attributes["AdvertNum"].Value;//列表编号
                    item.PlayCount = int.Parse(ItemNode.Attributes["PlayCount"].Value);
                    item.WatchCount = int.Parse(ItemNode.Attributes["WatchCount"].Value);
                    item.PrintCount = int.Parse(ItemNode.Attributes["PrintCount"].Value);
                    if (!model.ItemUsage.ContainsKey(item.AdvertNum))
                    {
                        model.ItemUsage.Add(item.AdvertNum, item);
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
    /// <summary>
    /// 使用情况
    /// </summary>
    public class AdvertisementUsage
    {

        private string _AdvertNum = "";
        /// <summary>
        /// 广告编号
        /// </summary>
        public string AdvertNum
        {
            get { return _AdvertNum; }
            set { _AdvertNum = value; }
        }

        private int _PlayCount = 0;
        /// <summary>
        /// 播放次数
        /// </summary>
        public int PlayCount
        {
            get { return _PlayCount; }
            set { _PlayCount = value; }
        }

        private int _WatchCount = 0;
        /// <summary>
        /// 查看次数
        /// </summary>
        public int WatchCount
        {
            get { return _WatchCount; }
            set { _WatchCount = value; }
        }

        private int _PrintCount = 0;
        /// <summary>
        /// 打印次数
        /// </summary>
        public int PrintCount
        {
            get { return _PrintCount; }
            set { _PrintCount = value; }
        }
    }
}
