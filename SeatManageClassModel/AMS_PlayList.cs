using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SeatManage.ClassModel
{

    /// <summary>
    /// 播放列表
    /// </summary>
    [Serializable]
    public class AMS_PlayList
    {
        public AMS_PlayList()
        {
            PlayListNo = "";
            PlayVideoItems = new List<AMS_VideoItem>();
            VideoFiles = new List<AMS_VideoItem>();
        }

        /// <summary>
        /// 循环间隔(秒)
        /// </summary>
        public int PlayElapsed
        {
            get;
            set;
        }
        /// <summary>
        /// 下发到的校区编号
        /// </summary>
        public string CampusNo
        {
            get;
            set;
        }
        /// <summary>
        /// 列表编号
        /// </summary>
        public string PlayListNo
        {
            get;
            set;
        }
        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectDate
        {
            get;
            set;
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime? ReleaseDate
        {
            get;
            set;
        }
        /// <summary>
        /// 视频文件播放列表
        /// </summary>
        public List<AMS_VideoItem> PlayVideoItems
        {
            get;
            set;
        }
        /// <summary>
        /// 视频文件
        /// </summary>
        public List<AMS_VideoItem> VideoFiles
        {
            get;
            set;
        }
        /// <summary>
        /// 播放列表总时长(秒)
        /// </summary>
        public int PlayListTimeLength
        {
            get;
            set;
        }

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
        public string ToXml(AMS_PlayList playlist)
        {
            //TODO:转换成xml结构的算法
            //创建一个xml对象
            XmlDocument xmlDoc = new XmlDocument();
            //创建开头
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(dec);
            //创建根节点
            XmlElement root = xmlDoc.CreateElement("Root");
            root.SetAttribute("Num", playlist.PlayListNo);
            root.SetAttribute("EffectDate", playlist.EffectDate.ToShortDateString());
            root.SetAttribute("EndDate", playlist.EndDate.ToShortDateString());
            root.SetAttribute("PlayElapsed", playlist.PlayElapsed.ToString());
            //创建二级节点
            XmlElement SecNode = xmlDoc.CreateElement("VideoItems");
            root.AppendChild(SecNode);
            //遍历媒体文件并添加到节点中
            foreach (AMS_VideoItem video in playlist.PlayVideoItems)
            {
                XmlElement ThirdNode = xmlDoc.CreateElement("Video");
                ThirdNode.SetAttribute("name", video.Name);
                ThirdNode.SetAttribute("playtime", video.PlayTime);
                ThirdNode.SetAttribute("source", video.RelativeUrl); 
                SecNode.AppendChild(ThirdNode);
                //不重复的视频文件名称
                int i;
                for (i = 0; i < VideoFiles.Count; i++)
                {
                    if (VideoFiles[i].Name == video.Name)
                    {
                        break;
                    }
                }
                if (i >= VideoFiles.Count)
                {
                    VideoFiles.Add(video);
                }
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
            List<AMS_VideoItem> videoFiles = new List<AMS_VideoItem>();
            XmlDocument xmlDoc = xmlPlayList;
            try
            {

                //查找根节点
                XmlNode node = xmlDoc.SelectSingleNode("//Root");
                plm.PlayListNo = node.Attributes["Num"].Value;//列表编号
                plm.EffectDate = DateTime.Parse(node.Attributes["EffectDate"].Value);//生效日期
                plm.PlayElapsed = int.Parse(node.Attributes["PlayElapsed"].Value);
                plm.EndDate = DateTime.Parse(node.Attributes["EndDate"].Value);
                List<AMS_VideoItem> lists = new List<AMS_VideoItem>();
                XmlNodeList nodes = xmlDoc.SelectNodes("//Root/VideoItems/Video");
                //遍历找到的视频项
                foreach (XmlNode n in nodes)
                {
                    lists.Add(
                        new AMS_VideoItem()
                        {
                            Name = n.Attributes["name"].Value,
                            PlayTime = n.Attributes["playtime"].Value,
                            RelativeUrl = n.Attributes["source"].Value,
                        }
                    );
                    //不重复的视频文件名称
                    int i;
                    for (i = 0; i < videoFiles.Count; i++)
                    {
                        if (videoFiles[i].Name == n.Attributes["name"].Value)
                        {
                            break;
                        }
                    }
                    if (i >= videoFiles.Count)
                    {
                        videoFiles.Add(new AMS_VideoItem() { Name = n.Attributes["name"].Value, RelativeUrl = n.Attributes["source"].Value});
                    }
                }
                plm.VideoFiles = videoFiles;
                plm.PlayVideoItems = lists;
                plm.PlayListTimeLength = GetTimeLength(DateTime.Parse(DateTime.Now.ToShortDateString() + " " + lists[0].PlayTime), DateTime.Parse(DateTime.Now.ToShortDateString() + " " + lists[lists.Count - 1].PlayTime));

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return plm;
        }
        /// <summary>
        /// 判断相差时长，单位秒
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        private static int GetTimeLength(DateTime startTime, DateTime endTime)
        {
            TimeSpan ts = endTime.Subtract(startTime);
            int timeLength = ts.Hours * 3600 + ts.Minutes * 60 + ts.Seconds;
           // ts = new TimeSpan(0, 0, 465);
            return timeLength;
        }
    }

}
