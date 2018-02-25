﻿using System;
using System.Xml;

namespace SeatManage.DataModel
{
    public class SchoolNoteInfo : AMS_Advertisement
    {
        private string _NoteImagePath = "";
        /// <summary>
        /// 广告图片的地址
        /// </summary>
        public string NoteImagePath
        {
            get { return _NoteImagePath; }
            set { _NoteImagePath = value; }
        }
        private System.Windows.Media.Imaging.BitmapImage _NoteImage;
        /// <summary>
        /// 通知图片
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage NoteImage
        {
            get { return _NoteImage; }
            set { _NoteImage = value; }
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
        public static string ToXml(SchoolNoteInfo model)
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
            XmlElement SecNode = xmlDoc.CreateElement("Note");
            SecNode.SetAttribute("NoteImagePath", model.NoteImagePath);
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
        public static SchoolNoteInfo ToModel(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            XmlDocument xmlDoc = new XmlDocument();
            SchoolNoteInfo model = new SchoolNoteInfo();
            try
            {
                xmlDoc.LoadXml(xml);
                //查找根节点
                XmlNode node = xmlDoc.SelectSingleNode("//Root");
                model.Num = node.Attributes["Num"].Value;//列表编号
                model.Name = node.Attributes["Name"].Value;
                model.EffectDate = DateTime.Parse(node.Attributes["EffectDate"].Value);
                model.EndDate = DateTime.Parse(node.Attributes["EndDate"].Value);
                model.Type = (SystemCode.EnumType.AdType)System.Enum.Parse(typeof(SystemCode.EnumType.AdType), node.Attributes["Type"].Value);
                node = xmlDoc.SelectSingleNode("//Root/Note");
                model.NoteImagePath = node.Attributes["NoteImagePath"].Value;

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