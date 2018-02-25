using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AMS.Model
{
    public class PlaylistInfo : AMS_Advertisement
    {
        private List<PlaylistItemInfo> _MediaPlayList = new List<PlaylistItemInfo>();
        /// <summary>
        /// 播放列表
        /// </summary>
        public List<PlaylistItemInfo> MediaPlayList
        {
            get { return _MediaPlayList; }
            set { _MediaPlayList = value; }
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
        public static string ToXml(PlaylistInfo model)
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
            XmlElement SecNode = xmlDoc.CreateElement("VideoItem");
            foreach (PlaylistItemInfo videoItem in model.MediaPlayList)
            {
                XmlElement ThrNode = xmlDoc.CreateElement("Video");
                ThrNode.SetAttribute("MediaFileName", videoItem.MediaFileName);
                ThrNode.SetAttribute("PlayTime", videoItem.PlayTime.ToString());
                ThrNode.SetAttribute("MD5Key", videoItem.MD5Key);
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
        public static PlaylistInfo ToModel(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            XmlDocument xmlDoc = new XmlDocument();
            PlaylistInfo model = new PlaylistInfo();
            try
            {
                xmlDoc.LoadXml(xml);
                //查找根节点
                XmlNode node = xmlDoc.SelectSingleNode("//Root");
                model.Num = node.Attributes["Num"].Value;//列表编号
                model.Name = node.Attributes["Name"].Value;
                model.EffectDate = DateTime.Parse(node.Attributes["EffectDate"].Value);
                model.EndDate = DateTime.Parse(node.Attributes["EndDate"].Value);
                model.Type = (Enum.AdType)System.Enum.Parse(typeof(Enum.AdType), node.Attributes["Type"].Value);
                XmlNodeList nodes = xmlDoc.SelectNodes("//Root/VideoItem/Video");
                foreach (XmlNode videotemNode in nodes)
                {
                    PlaylistItemInfo videoItem = new PlaylistItemInfo();
                    videoItem.MediaFileName = videotemNode.Attributes["MediaFileName"].Value;
                    videoItem.PlayTime = int.Parse(videotemNode.Attributes["PlayTime"].Value);
                    videoItem.MD5Key = videotemNode.Attributes["MD5Key"].Value;
                    model.MediaPlayList.Add(videoItem);
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
    /// 播放列表项
    /// </summary>
    public class PlaylistItemInfo
    {
        
        private string _MediaFileName = "";
        /// <summary>
        /// 播放文件名称
        /// </summary>
        public string MediaFileName
        {
            get { return _MediaFileName; }
            set { _MediaFileName = value; }
        }
        private int _PlayTime = 0;
        /// <summary>
        /// 播放时间（s）
        /// </summary>
        public int PlayTime
        {
            get { return _PlayTime; }
            set { _PlayTime = value; }
        }
        private string _MD5Key = "";
        /// <summary>
        /// MD5码
        /// </summary>
        public string MD5Key
        {
            get { return _MD5Key; }
            set { _MD5Key = value; }
        }
    }
}
