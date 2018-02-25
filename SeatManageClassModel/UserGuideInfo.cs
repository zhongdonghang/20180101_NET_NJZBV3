using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SeatManage.ClassModel
{
    public class UserGuideInfo
    {
        private string _XMLContent;
        /// <summary>
        /// 广告内容（XML）
        /// </summary>
        public string XMLContent
        {
            get { return _XMLContent; }
            set { _XMLContent = value; }
        }

        private List<string> _ImageFilePath = new List<string>();
        /// <summary>
        /// 图片资源下载地址
        /// </summary>
        public List<string> ImageFilePath
        {
            get { return _ImageFilePath; }
            set { _ImageFilePath = value; }
        }

        private List<System.Windows.Media.Imaging.BitmapImage> _GuideImage;
        /// <summary>
        /// 图片
        /// </summary>
        public List<System.Windows.Media.Imaging.BitmapImage> GuideImage
        {
            get { return _GuideImage; }
            set { _GuideImage = value; }
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
        public static string ToXml(UserGuideInfo model)
        {
            //TODO:转换成xml结构的算法
            //创建一个xml对象
            XmlDocument xmlDoc = new XmlDocument();
            //创建开头
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(dec);
            //创建根节点
            XmlElement root = xmlDoc.CreateElement("Root");
            //创建二级节点
            XmlElement SecNode = xmlDoc.CreateElement("GuideImage");
            foreach (string item in model.ImageFilePath)
            {
                XmlElement ThrNode = xmlDoc.CreateElement("Image");
                ThrNode.SetAttribute("Url", item);
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
        public static UserGuideInfo ToModel(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            XmlDocument xmlDoc = new XmlDocument();
            UserGuideInfo model = new UserGuideInfo();
            try
            {
                xmlDoc.LoadXml(xml);
                //查找根节点
                XmlNodeList nodes = xmlDoc.SelectNodes("//Root/GuideImage/Image");
                foreach (XmlNode item in nodes)
                {
                    model.ImageFilePath.Add(item.Attributes["Url"].Value);
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
