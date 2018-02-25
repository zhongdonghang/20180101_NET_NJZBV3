using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;
using System.IO;

namespace AMS.MediaPlayer.Code
{
    public class PlayerSetting
    {
        #region 配置文件

        /// <summary>
        /// 程序所在目录
        /// </summary>
        public static string SysPath
        {
            get { return System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase; ; }

        }

        /// <summary>
        /// 默认视频
        /// </summary>
        public static string DefaultVideo
        {
            get { return ConfigurationManager.AppSettings["defaultVideo"]; }

        }

        /// <summary>
        ///播放列表的媒体目录
        /// </summary>
        public static string DefaultVideosPath
        {
            get { return System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Video\"; }

        }
        /// <summary>
        /// 学校编号
        /// </summary>
        public static string SchoolNo
        {
            get { return ConfigurationManager.AppSettings["SchoolNo"]; }
        }

        /// <summary>
        /// 校区编号
        /// </summary>
        public static string CampusNo
        {
            get { return ConfigurationManager.AppSettings["CampusNo"]; }
        }

        static string _DeviceNo = "";
        /// <summary>
        /// 设备终端编号
        /// </summary>
        public static string DeviceNo
        {
            get
            {
                if (string.IsNullOrEmpty(_DeviceNo))
                {
                    _DeviceNo = getTerminalNum(); 
                }
                return _DeviceNo;
            }
        }

        /// <summary>
        /// 间隔发送设备消息时间
        /// </summary>
        public static string SendMessageInterval
        {
            get { return ConfigurationManager.AppSettings["SendMessageInterval"]; }
        }

        /// <summary>
        /// 播放列表更新时间间隔
        /// </summary>
        public static string UpdateTime
        {
            get { return ConfigurationManager.AppSettings["UpdateTime"]; }
        }


        /// <summary>
        /// 截图所存放的路径
        /// </summary>
        public static string ImagelocadPath
        {
            get { return System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Caputre\"; }
        }

        /// <summary>
        /// 指示媒体播放器是否脱机工作
        /// </summary>
        public static string IsOffline
        {
            get { return ConfigurationManager.AppSettings["IsOffline"]; }
        }

        /// <summary>
        /// 广告播放时间判断间隔
        /// </summary>
        public static int AdLoopTime
        {
            get { return int.Parse(ConfigurationManager.AppSettings["ADLoopTime"]); }
        }


        #endregion
        /// <summary>
        /// 获取终端设备的编号
        /// </summary>
        /// <returns></returns>
        static string getTerminalNum()
        {
            XmlDocument doc = new XmlDocument();
            string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory;
            //获取相同根目录下的SeatClient配置文件。
             string configDircetory = fileDircetoryPath.Substring(0, fileDircetoryPath.Remove( fileDircetoryPath.Length - 1,1).LastIndexOf('\\'));
            string filePath = string.Format(@"{0}\SeatClient\SeatClient.exe.config",configDircetory, fileDircetoryPath);
            if (File.Exists(filePath))
            {
                try
                {
                    doc.Load(filePath);
                    XmlNodeList nodes = doc.SelectNodes("//configuration/appSettings/add");
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Attributes["key"].Value == "ClientNo")
                        {
                            return node.Attributes["value"].Value;
                        }
                    }
                }
                catch
                { }
            }
            return "";
        }
    }
}
