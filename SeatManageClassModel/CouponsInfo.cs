using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SeatManage.ClassModel
{
    [Serializable]
    public class CouponsInfo : AMS_Advertisement
    {
        private List<CouponsInfoItem> _PopItemList = new List<CouponsInfoItem>();
        /// <summary>
        /// 弹窗子项
        /// </summary>
        public List<CouponsInfoItem> PopItemList
        {
            get { return _PopItemList; }
            set { _PopItemList = value; }
        }
        private int _Station = 0;
        /// <summary>
        /// 优惠券位置
        /// </summary>
        public int Station
        {
            get { return _Station; }
            set { _Station = value; }
        }
        private string _LogoImage = "";
        /// <summary>
        /// LOGO图片
        /// </summary>
        public string LogoImage
        {
            get { return _LogoImage; }
            set { _LogoImage = value; }
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
        public static string ToXml(CouponsInfo model)
        {
            //TODO:转换成xml结构的算法
            //创建一个xml对象
            XmlDocument xmlDoc = new XmlDocument();
            //创建开头
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(dec);
            //创建根节点
            XmlElement root = xmlDoc.CreateElement("Root");
            root.SetAttribute("Num", model.Num);
            root.SetAttribute("Name", model.Name);
            root.SetAttribute("EffectDate", model.EffectDate.ToShortDateString());
            root.SetAttribute("EndDate", model.EndDate.ToShortDateString());
            root.SetAttribute("Station", model.Station.ToString());
            root.SetAttribute("LogoImage", model.LogoImage);
            root.SetAttribute("Type", model.Type.ToString());
            XmlElement SecNode = xmlDoc.CreateElement("PopItem");
            foreach (CouponsInfoItem scItem in model.PopItemList)
            {
                XmlElement ThrNode = xmlDoc.CreateElement("Pop");
                ThrNode.SetAttribute("EffectDate", scItem.EffectDate.ToShortDateString());
                ThrNode.SetAttribute("EndDate", scItem.EndDate.ToShortDateString());
                ThrNode.SetAttribute("PpoImagePath", scItem.PpoImagePath);
                ThrNode.SetAttribute("IsPrint", scItem.IsPrint.ToString());
                ThrNode.SetAttribute("ID", scItem.ID.ToString());
                if (scItem.IsPrint)
                {
                    XmlElement ForNode = xmlDoc.CreateElement("PrintTemplate");
                    foreach (PrintItem pItem in scItem.TemplateItem)
                    {
                        XmlElement FirNode;
                        if (pItem.IsImage)
                        {
                            FirNode = xmlDoc.CreateElement("Pic");
                            FirNode.InnerText = pItem.ImagePath;
                            FirNode.SetAttribute("height", pItem.ImageHeight.ToString());
                            FirNode.SetAttribute("width", pItem.ImageWidth.ToString());
                        }
                        else
                        {
                            FirNode = xmlDoc.CreateElement("Content");
                            FirNode.InnerText = pItem.TextInfo;
                            FirNode.SetAttribute("bold", pItem.IsBold ? "Y" : "N");
                            FirNode.SetAttribute("italic", pItem.IsItalic ? "Y" : "N");
                            FirNode.SetAttribute("size", pItem.FontSize.ToString());
                        }
                        ForNode.AppendChild(FirNode);
                    }
                    ThrNode.AppendChild(ForNode);
                }
                SecNode.AppendChild(ThrNode);
            }
            //在根节点中添加二级节点
            root.AppendChild(SecNode);

            //需要下载的文件
            XmlElement FileNode = xmlDoc.CreateElement("ImageFilePath");
            foreach (string file in model.ImageFilePath)
            {
                XmlElement ThrNode = xmlDoc.CreateElement("FilePath");
                ThrNode.SetAttribute("Url", file);
                FileNode.AppendChild(ThrNode);
            }
            root.AppendChild(FileNode);


            //添加根节点
            xmlDoc.AppendChild(root);
            return xmlDoc.OuterXml;
        }
        /// <summary>
        /// 转换成Moldel
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static CouponsInfo ToModel(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            XmlDocument xmlDoc = new XmlDocument();
            CouponsInfo model = new CouponsInfo();
            try
            {
                xmlDoc.LoadXml(xml);
                //查找根节点
                XmlNode node = xmlDoc.SelectSingleNode("//Root");
                model.Num = node.Attributes["Num"].Value;//列表编号
                model.Name = node.Attributes["Name"].Value;
                model.EffectDate = DateTime.Parse(node.Attributes["EffectDate"].Value);
                model.EndDate = DateTime.Parse(node.Attributes["EndDate"].Value);
                model.Station = int.Parse(node.Attributes["Station"].Value);
                model.LogoImage = node.Attributes["LogoImage"].Value;
                model.Type = (SeatManage.EnumType.AdType)System.Enum.Parse(typeof(SeatManage.EnumType.AdType), node.Attributes["Type"].Value);
                XmlNodeList nodes = xmlDoc.SelectNodes("//Root/PopItem/Pop");
                foreach (XmlNode popItemNode in nodes)
                {
                    CouponsInfoItem scItem = new CouponsInfoItem();
                    scItem.EffectDate = DateTime.Parse(popItemNode.Attributes["EffectDate"].Value);
                    scItem.EndDate = DateTime.Parse(popItemNode.Attributes["EndDate"].Value);
                    scItem.IsPrint = bool.Parse(popItemNode.Attributes["IsPrint"].Value);
                    scItem.PpoImagePath = popItemNode.Attributes["PpoImagePath"].Value;
                    scItem.ID = popItemNode.Attributes["ID"].Value;
                    if (scItem.IsPrint)
                    {
                        foreach (XmlNode pItem in popItemNode.ChildNodes[0].ChildNodes)
                        {
                            switch (pItem.LocalName)
                            {
                                case "Content":
                                    {
                                        PrintItem item = new PrintItem();
                                        item.TextInfo = pItem.InnerText;
                                        item.IsBold = pItem.Attributes["bold"].Value == "Y" ? true : false;
                                        item.IsItalic = pItem.Attributes["italic"].Value == "Y" ? true : false;
                                        item.FontSize = int.Parse(pItem.Attributes["size"].Value);
                                        scItem.TemplateItem.Add(item);
                                    }
                                    break;
                                case "Pic":
                                    {
                                        PrintItem item = new PrintItem();
                                        item.IsImage = true;
                                        item.ImagePath = pItem.InnerText;
                                        item.ImageHeight = double.Parse(pItem.Attributes["height"].Value);
                                        item.ImageWidth = double.Parse(pItem.Attributes["width"].Value);
                                        scItem.TemplateItem.Add(item);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    model.PopItemList.Add(scItem);
                }
                nodes = xmlDoc.SelectNodes("//Root/ImageFilePath/FilePath");
                foreach (XmlNode ItemNode in nodes)
                {
                    model.ImageFilePath.Add(ItemNode.Attributes["Url"].Value);
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
    /// 优惠券子项
    /// </summary>
    public class CouponsInfoItem
    {
        private string _ID = "";
        /// <summary>
        /// id
        /// </summary>
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _PpoImagePath = "";
        /// <summary>
        /// 弹窗广告图片
        /// </summary>
        public string PpoImagePath
        {
            get { return _PpoImagePath; }
            set { _PpoImagePath = value; }
        }
        private DateTime _EffectDate = new DateTime();
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EffectDate
        {
            get { return _EffectDate; }
            set { _EffectDate = value; }
        }
        private DateTime _EndDate = new DateTime();
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        private bool _IsPrint = false;
        /// <summary>
        /// 是否打印
        /// </summary>
        public bool IsPrint
        {
            get { return _IsPrint; }
            set { _IsPrint = value; }
        }
        private List<PrintItem> _TemplateItem = new List<PrintItem>();
        /// <summary>
        /// 打印子项
        /// </summary>
        public List<PrintItem> TemplateItem
        {
            get { return _TemplateItem; }
            set { _TemplateItem = value; }
        }
        /// <summary>
        /// 打印模板
        /// </summary>
        public string PrintXml
        {
            get { return ToXml(this); }
        }
        /// <summary>
        /// 转换成Xml
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string ToXml(CouponsInfoItem model)
        {
            //TODO:转换成xml结构的算法
            //创建一个xml对象
            XmlDocument xmlDoc = new XmlDocument();
            //创建开头
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(dec);
            //创建根节点
            XmlElement root = xmlDoc.CreateElement("Print");
            foreach (PrintItem pItem in model.TemplateItem)
            {
                XmlElement FirNode;
                if (pItem.IsImage)
                {
                    FirNode = xmlDoc.CreateElement("Pic");
                    FirNode.InnerText = pItem.ImagePath;
                    FirNode.SetAttribute("height", pItem.ImageHeight.ToString());
                    FirNode.SetAttribute("width", pItem.ImageWidth.ToString());
                }
                else
                {
                    FirNode = xmlDoc.CreateElement("Content");
                    FirNode.InnerText = pItem.TextInfo;
                    FirNode.SetAttribute("font", "宋体");
                    FirNode.SetAttribute("bold", pItem.IsBold ? "Y" : "N");
                    FirNode.SetAttribute("italic", pItem.IsItalic ? "Y" : "N");
                    FirNode.SetAttribute("size", pItem.FontSize.ToString());
                }
                root.AppendChild(FirNode);
            }
            //添加根节点
            xmlDoc.AppendChild(root);
            return xmlDoc.OuterXml;
        }
    }
    public class PrintItem
    {
        private string _TextInfo = "";

        public string TextInfo
        {
            get { return _TextInfo; }
            set { _TextInfo = value; }
        }
        private double _ImageHeight;

        public double ImageHeight
        {
            get { return _ImageHeight; }
            set { _ImageHeight = value; }
        }
        private double _ImageWidth;

        public double ImageWidth
        {
            get { return _ImageWidth; }
            set { _ImageWidth = value; }
        }
        private string _ImagePath;

        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        private bool _IsImage = false;

        public bool IsImage
        {
            get { return _IsImage; }
            set { _IsImage = value; }
        }
        private int _FontSize = 8;

        public int FontSize
        {
            get { return _FontSize; }
            set { _FontSize = value; }
        }
        private bool _IsBold = false;

        public bool IsBold
        {
            get { return _IsBold; }
            set { _IsBold = value; }
        }
        private bool _IsItalic = false;

        public bool IsItalic
        {
            get { return _IsItalic; }
            set { _IsItalic = value; }
        }

    }

}
