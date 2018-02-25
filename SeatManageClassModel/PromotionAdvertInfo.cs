using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SeatManage.ClassModel
{
    public class PromotionAdvertInfo : AMS_Advertisement
    {
        private string _AdImagePath = "";
        /// <summary>
        /// 广告图片的地址
        /// </summary>
        public string AdImagePath
        {
            get { return _AdImagePath; }
            set { _AdImagePath = value; }
        }
        private System.Windows.Media.Imaging.BitmapImage _PromotionImage;
        /// <summary>
        /// 推广图片
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage PromotionImage
        {
            get { return _PromotionImage; }
            set { _PromotionImage = value; }
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
        public static string ToXml(PromotionAdvertInfo model)
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
            XmlElement SecNode = xmlDoc.CreateElement("Promotion");
            SecNode.SetAttribute("AdImagePath", model.AdImagePath);
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
        public static PromotionAdvertInfo ToModel(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            XmlDocument xmlDoc = new XmlDocument();
            PromotionAdvertInfo model = new PromotionAdvertInfo();
            try
            {
                xmlDoc.LoadXml(xml);
                //查找根节点
                XmlNode node = xmlDoc.SelectSingleNode("//Root");
                model.Num = node.Attributes["Num"].Value;//列表编号
                model.Name = node.Attributes["Name"].Value;
                model.EffectDate = DateTime.Parse(node.Attributes["EffectDate"].Value);
                model.EndDate = DateTime.Parse(node.Attributes["EndDate"].Value);
                model.Type = (SeatManage.EnumType.AdType)System.Enum.Parse(typeof(SeatManage.EnumType.AdType), node.Attributes["Type"].Value);
                node = xmlDoc.SelectSingleNode("//Root/Promotion");
                model.AdImagePath = node.Attributes["AdImagePath"].Value;

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
