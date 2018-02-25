using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SeatManage.ClassModel
{
    public class PrintReceiptInfo : AMS_Advertisement
    {
        private List<PrintItem> _TemplateItem = new List<PrintItem>();
        /// <summary>
        /// 打印子项
        /// </summary>
        public List<PrintItem> TemplateItem
        {
            get { return _TemplateItem; }
            set { _TemplateItem = value; }
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
        public static string ToXml(PrintReceiptInfo model)
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
            root.SetAttribute("Type", model.Type.ToString());
            //创建二级节点
            XmlElement SecNode = xmlDoc.CreateElement("PrintTemplate");
            foreach (PrintItem pItem in model.TemplateItem)
            {
                XmlElement ThrNode;
                if (pItem.IsImage)
                {
                    ThrNode = xmlDoc.CreateElement("Pic");
                    ThrNode.InnerText = pItem.ImagePath;
                    ThrNode.SetAttribute("height", pItem.ImageHeight.ToString());
                    ThrNode.SetAttribute("width", pItem.ImageWidth.ToString());
                }
                else
                {
                    ThrNode = xmlDoc.CreateElement("Content");
                    ThrNode.InnerText = pItem.TextInfo;
                    ThrNode.SetAttribute("font", "宋体");
                    ThrNode.SetAttribute("bold", pItem.IsBold ? "Y" : "N");
                    ThrNode.SetAttribute("italic", pItem.IsItalic ? "Y" : "N");
                    ThrNode.SetAttribute("size", pItem.FontSize.ToString());
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
        public static PrintReceiptInfo ToModel(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            XmlDocument xmlDoc = new XmlDocument();
            PrintReceiptInfo model = new PrintReceiptInfo();
            try
            {
                xmlDoc.LoadXml(xml);
                //查找根节点
                XmlNode node = xmlDoc.SelectSingleNode("//Root");
                model.Num = node.Attributes["Num"].Value;//列表编号
                model.Name = node.Attributes["Name"].Value;//列表编号
                model.EffectDate = DateTime.Parse(node.Attributes["EffectDate"].Value);
                model.EndDate = DateTime.Parse(node.Attributes["EndDate"].Value);
                model.Type = (SeatManage.EnumType.AdType)System.Enum.Parse(typeof(SeatManage.EnumType.AdType), node.Attributes["Type"].Value);
                node = xmlDoc.SelectSingleNode("//Root/PrintTemplate");
                foreach (XmlNode pItem in node.ChildNodes)
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
                                model.TemplateItem.Add(item);
                            }
                            break;
                        case "Pic":
                            {
                                PrintItem item = new PrintItem();
                                item.IsImage = true;
                                item.ImagePath = pItem.InnerText;
                                item.ImageHeight = double.Parse(pItem.Attributes["height"].Value);
                                item.ImageWidth = double.Parse(pItem.Attributes["width"].Value);
                                model.TemplateItem.Add(item);
                            }
                            break;
                        default:
                            break;
                    }
                }
                XmlNodeList nodes = xmlDoc.SelectNodes("//Root/ImageFilePath/FilePath");
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
}
