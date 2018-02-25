using System;
using System.Collections.Generic;
using System.Xml;
namespace AMS.Model
{
    /// <summary>
    /// AMS_PlayList:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class AMS_PlayList
    {
        public AMS_PlayList()
        { }
        #region Model
        private int _id = -1;
        private int? _operator = -1;
        private string _number = "";
        private string _playlistname = "";
        private string _playlist = "";
        private DateTime? _releasedate = DateTime.Now;
        private DateTime? _effectdate;
        private DateTime? _enddate;
        private string _describe = "";
        private string _OperatorName = "";
        private List<AMS_VideoItem> _MediaFiles = new List<AMS_VideoItem>();
        private List<AMS_VideoItem> _PlayFileList = new List<AMS_VideoItem>();
        /// <summary>
        /// 操作者
        /// </summary>
        public string OperatorName
        {
            get { return _OperatorName; }
            set { _OperatorName = value; }
        }
        /// <summary>
        /// 媒体文件列表
        /// </summary>
        public List<AMS_VideoItem> MediaFiles
        {
            get { return _MediaFiles; }
            set { _MediaFiles = value; }
        }

        /// <summary>
        /// 播放顺序列表
        /// </summary>
        public List<AMS_VideoItem> PlayFileList
        {
            get { return _PlayFileList; }
            set { _PlayFileList = value; }
        }
        /// <summary>
        /// 播放列表ID
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 操作人
        /// </summary>
        public int? Operator
        {
            set { _operator = value; }
            get { return _operator; }
        }
        /// <summary>
        /// 播放列表编号
        /// </summary>
        public string Number
        {
            set { _number = value; }
            get { return _number; }
        }
        /// <summary>
        /// 播放列表名称
        /// </summary>
        public string PlayListName
        {
            set { _playlistname = value; }
            get { return _playlistname; }
        }
        /// <summary>
        /// xml存放的播放列表
        /// </summary>
        public string PlayList
        {
            set { _playlist = value; }
            get { return _playlist; }
        }
        /// <summary>
        /// 播放列表下发时间
        /// </summary>
        public DateTime? ReleaseDate
        {
            set { _releasedate = value; }
            get { return _releasedate; }
        }
        /// <summary>
        /// 播放列表发布时间
        /// </summary>
        public DateTime? EffectDate
        {
            set { _effectdate = value; }
            get { return _effectdate; }
        }
        /// <summary>
        /// 播放列表结束时间
        /// </summary>
        public DateTime? EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe
        {
            set { _describe = value; }
            get { return _describe; }
        }
        /// <summary>
        /// 播放列表总时长(秒)
        /// </summary>
        public int PlayListTimeLength
        {
            get;
            set;
        }
        #endregion Model


        /// <summary>
        /// 结构转换成XML
        /// </summary>
        /// <returns></returns>
        public string ToXml()
        {
            return ToXml(this);
        }
        /// <summary>
        /// 结构转换成XML
        /// </summary>
        /// <param name="playlist">播放列表</param>
        /// <returns></returns>
        public static string ToXml(AMS_PlayList playlist)
        {
            //TODO:转换成xml结构的算法
            //创建一个xml对象
            XmlDocument xmlDoc = new XmlDocument();
            //创建开头
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(dec);
            //创建根节点
            XmlElement root = xmlDoc.CreateElement("Root");
            root.SetAttribute("Num", playlist.Number);
            root.SetAttribute("EffectDate", playlist.EffectDate.Value.ToShortDateString());
            root.SetAttribute("EndDate", playlist.EndDate.Value.ToShortDateString());
            root.SetAttribute("PlayElapsed", playlist.PlayFileList[playlist.PlayFileList.Count - 1].SunTime.ToString());
            //创建二级节点
            XmlElement SecNode = xmlDoc.CreateElement("VideoItems");
            root.AppendChild(SecNode);
            //遍历媒体文件并添加到节点中
            foreach (AMS_VideoItem video in playlist.PlayFileList)
            {
                XmlElement ThirdNode = xmlDoc.CreateElement("Video");
                ThirdNode.SetAttribute("name", video.Name);
                ThirdNode.SetAttribute("playtime", video.PlayTime);
                ThirdNode.SetAttribute("source", video.ReRelativeUrl);
                ThirdNode.SetAttribute("md5value", video.MD5Value);
                SecNode.AppendChild(ThirdNode);
                //不重复的视频文件名称
            }
            //在根节点中添加二级节点
            root.AppendChild(SecNode);
            //添加根节点
            xmlDoc.AppendChild(root);
            return xmlDoc.OuterXml;

        }

        /// <summary>
        /// 播放列表xml 转换成对象
        /// </summary>
        /// <param name="xmlPlayList">xml结构的播放列表</param>
        /// <returns></returns>
        public static AMS_PlayList Parse(string xmlPlayList)
        {
            AMS_PlayList plm = new AMS_PlayList();
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                //载入字符串类型的XML
                xmlDoc.LoadXml(xmlPlayList);
                plm = Parse(xmlDoc);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return plm;
        }


        /// <summary>
        /// 播放列表xml 转换成对象
        /// </summary>
        /// <param name="xmlPlayList">xml结构的播放列表</param>
        /// <returns></returns>
        private static AMS_PlayList Parse(XmlDocument xmlPlayList)
        {
            AMS_PlayList plm = new AMS_PlayList();
            XmlDocument xmlDoc = xmlPlayList;
            try
            {

                //查找根节点
                XmlNode node = xmlDoc.SelectSingleNode("//Root");
                plm.Number = node.Attributes["Num"].Value;//列表编号
                plm.EffectDate = DateTime.Parse(node.Attributes["EffectDate"].Value);//生效日期
                //plm.PlayElapsed = int.Parse(node.Attributes["PlayElapsed"].Value);
                plm.EndDate = DateTime.Parse(node.Attributes["EndDate"].Value);
                XmlNodeList nodes = xmlDoc.SelectNodes("//Root/VideoItems/Video");
                //遍历找到的视频项
                foreach (XmlNode n in nodes)
                {
                    AMS_VideoItem item = new AMS_VideoItem();
                    item.Name = n.Attributes["name"].Value;
                    item.PlayTime = n.Attributes["playtime"].Value;
                    item.ReRelativeUrl = n.Attributes["source"].Value;
                    item.MD5Value = n.Attributes["md5value"].Value;
                    if (plm.PlayFileList.Count > 0)
                    {
                        plm.PlayFileList[plm.PlayFileList.Count - 1].SunTime = int.Parse((DateTime.Parse(item.PlayTime) - DateTime.Parse(plm.PlayFileList[plm.PlayFileList.Count - 1].PlayTime)).TotalSeconds.ToString().Split('.')[0]);
                    }
                    plm.PlayFileList.Add(item);
                    //不重复的视频文件名称
                    int i;
                    for (i = 0; i < plm.MediaFiles.Count; i++)
                    {
                        if (plm.MediaFiles[i].Name == item.Name)
                        {
                            break;
                        }
                    }
                    if (i >= plm.MediaFiles.Count)
                    {
                        AMS_VideoItem Fileitem = new AMS_VideoItem();
                        Fileitem.Name = item.Name;
                        Fileitem.ReRelativeUrl = item.ReRelativeUrl;
                        Fileitem.MD5Value = item.MD5Value;
                        plm.MediaFiles.Add(Fileitem);
                    }
                }
                plm.PlayFileList[plm.PlayFileList.Count - 1].SunTime = int.Parse(node.Attributes["PlayElapsed"].Value);
                plm.PlayListTimeLength = int.Parse((DateTime.Parse(plm.PlayFileList[plm.PlayFileList.Count - 1].PlayTime) - DateTime.Parse(plm.PlayFileList[0].PlayTime)).TotalSeconds.ToString().Split('.')[0]) + plm.PlayFileList[plm.PlayFileList.Count - 1].SunTime;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return plm;
        }
    }
}

