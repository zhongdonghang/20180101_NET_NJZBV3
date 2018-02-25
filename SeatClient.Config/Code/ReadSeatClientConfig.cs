using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using SeatManage.SeatManageComm;
using System.Diagnostics;

namespace SeatManage.SeatClient.Config.Code
{
    public class ReadSeatClientConfig
    {
        static XmlDocument Updoc = null;
        static XmlDocument SCdoc = null;
        static XmlDocument MPdoc = null;
        /// <summary>
        /// 获取终端的基本配置
        /// </summary>
        /// <returns></returns>
        public static bool GetSeatClientBaseConfig(ref ClientBasicConfig config)
        {
            Updoc = new XmlDocument();
            SCdoc = new XmlDocument();
            MPdoc = new XmlDocument();
            string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string upfilePath = string.Format("{0}ClientLauncher.exe.config", fileDircetoryPath);
            string scfilePath = string.Format("{0}SeatClient\\SeatClient.exe.config", fileDircetoryPath);
            string mpfilePath = string.Format("{0}MediaPlayer\\MediaPlayerClient.exe.config", fileDircetoryPath);
            if (File.Exists(upfilePath) && File.Exists(scfilePath) && File.Exists(mpfilePath))
            {
                try
                {
                    Updoc.Load(upfilePath);
                    SCdoc.Load(scfilePath);
                    MPdoc.Load(mpfilePath);
                    XmlNodeList nodes = Updoc.SelectNodes("//configuration/connectionStrings/add");
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Attributes["name"].Value == "EndpointAddress")
                        {
                            config.WCFConnString = AESAlgorithm.AESDecrypt(node.Attributes["connectionString"].Value);
                            break;
                        }
                    }
                    nodes = MPdoc.SelectNodes("//configuration/appSettings/add");
                    foreach (XmlNode node in nodes)
                    {
                        switch (node.Attributes["key"].Value)
                        {
                            case "ADLoopTime":
                                config.SCLoopTime = node.Attributes["value"].Value;
                                break;
                            case "defaultVideo":
                                config.DefaultMedia = node.Attributes["value"].Value;
                                break;
                            case "SchoolNo":
                                config.SchoolNo = node.Attributes["value"].Value;
                                break;
                            case "CampusNo":
                                config.CampusNo = node.Attributes["value"].Value;
                                break;
                            case "SendMessageInterval":
                                config.SentStatusTime = node.Attributes["value"].Value;
                                break;
                            case "UpdateTime":
                                config.UpdateTime = node.Attributes["value"].Value;
                                break;
                        }
                    }
                    nodes = SCdoc.SelectNodes("//configuration/appSettings/add");
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Attributes["key"].Value == "ClientNo")
                        {
                            config.TerminalNum = node.Attributes["value"].Value;
                            break;
                        }
                    }
                    return true;

                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static string getIpFromConnectionString(string connectionString)
        {
            string[] tempArray = connectionString.Split('/');
            try
            {
                return tempArray[2];
            }
            catch
            {
                return "";
            }
        }

        public static bool SaveConfig(ClientBasicConfig config)
        {
            Updoc = new XmlDocument();
            SCdoc = new XmlDocument();
            MPdoc = new XmlDocument();
            string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string upfilePath = string.Format("{0}ClientLauncher.exe.config", fileDircetoryPath);
            string scfilePath = string.Format("{0}SeatClient\\SeatClient.exe.config", fileDircetoryPath);
            string mpfilePath = string.Format("{0}MediaPlayer\\MediaPlayerClient.exe.config", fileDircetoryPath);
            if (File.Exists(upfilePath) && File.Exists(scfilePath) && File.Exists(mpfilePath))
            {
                try
                {
                    Updoc.Load(upfilePath);
                    SCdoc.Load(scfilePath);
                    MPdoc.Load(mpfilePath);
                    XmlNodeList nodes = Updoc.SelectNodes("//configuration/connectionStrings/add");
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Attributes["name"].Value == "EndpointAddress")
                        {
                            node.Attributes["connectionString"].Value = AESAlgorithm.AESEncrypt(config.WCFConnString);
                            break;
                        }
                    }
                    nodes = Updoc.SelectNodes("//configuration/appSettings/add");
                    foreach (XmlNode node in nodes)
                    {
                        switch (node.Attributes["key"].Value)
                        {
                            case "ClientNo":
                                node.Attributes["value"].Value = config.TerminalNum;
                                break;
                            case "SchoolNo":
                                node.Attributes["value"].Value = config.SchoolNo;
                                break;
                        }
                    }
                    nodes = SCdoc.SelectNodes("//configuration/connectionStrings/add");
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Attributes["name"].Value == "EndpointAddress")
                        {
                            node.Attributes["connectionString"].Value = AESAlgorithm.AESEncrypt(config.WCFConnString);
                            break;
                        }
                    }
                    nodes = MPdoc.SelectNodes("//configuration/connectionStrings/add");
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Attributes["name"].Value == "EndpointAddress")
                        {
                            node.Attributes["connectionString"].Value = AESAlgorithm.AESEncrypt(config.WCFConnString);
                            break;
                        }
                    }
                    nodes = MPdoc.SelectNodes("//configuration/appSettings/add");
                    foreach (XmlNode node in nodes)
                    {
                        switch (node.Attributes["key"].Value)
                        {
                            case "ADLoopTime":
                                node.Attributes["value"].Value = config.SCLoopTime;
                                break;
                            case "defaultVideo":
                                node.Attributes["value"].Value = config.DefaultMedia;
                                break;
                            case "SchoolNo":
                                node.Attributes["value"].Value = config.SchoolNo;
                                break;
                            case "CampusNo":
                                node.Attributes["value"].Value = config.CampusNo;
                                break;
                            case "SendMessageInterval":
                                node.Attributes["value"].Value = config.SentStatusTime;
                                break;
                            case "UpdateTime":
                                node.Attributes["value"].Value = config.UpdateTime;
                                break;
                        }
                    }
                    nodes = SCdoc.SelectNodes("//configuration/appSettings/add");
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Attributes["key"].Value == "ClientNo")
                        {
                            node.Attributes["value"].Value = config.TerminalNum;
                        }
                        
                    }
                    Updoc.Save(upfilePath);
                    SCdoc.Save(scfilePath);
                    MPdoc.Save(mpfilePath);
                    XmlDocument doc = new XmlDocument();
                    XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                    doc.AppendChild(dec);
                    XmlElement root = doc.CreateElement("configuration");//创建根节点
                    XmlElement element = doc.CreateElement("connectionStrings");
                    XmlElement addelement = doc.CreateElement("add");
                    addelement.SetAttribute("name", "EndpointAddress");
                    addelement.SetAttribute("connectionString", AESAlgorithm.AESEncrypt(config.WCFConnString));
                    element.AppendChild(addelement);
                    root.AppendChild(element);
                    XmlElement setelement = doc.CreateElement("startup");
                    XmlElement supelement = doc.CreateElement("supportedRuntime");
                    supelement.SetAttribute("version", "v4.0");
                    supelement.SetAttribute("sku", ".NETFramework,Version=v4.0");
                    setelement.AppendChild(supelement);
                    root.AppendChild(setelement);
                    doc.AppendChild(root);
                    doc.Save(Process.GetCurrentProcess().MainModule.FileName + ".config");
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }



    /// <summary>
    /// 终端基本配置
    /// </summary>
    public class ClientBasicConfig
    {
        private string _WCFConnString = "";
        /// <summary>
        /// wcf连接字符串
        /// </summary>
        public string WCFConnString
        {
            get { return _WCFConnString; }
            set { _WCFConnString = value; }
        }
        private string _SchoolNo = "";
        /// <summary>
        /// 学校编号
        /// </summary>
        public string SchoolNo
        {
            get { return _SchoolNo; }
            set { _SchoolNo = value; }
        }
        private string _CampusNo = "";
        /// <summary>
        /// 校区编号
        /// </summary>
        public string CampusNo
        {
            get { return _CampusNo; }
            set { _CampusNo = value; }
        }
        private string _TerminalNum = "";
        /// <summary>
        /// 设备编号
        /// </summary>
        public string TerminalNum
        {
            get { return _TerminalNum; }
            set { _TerminalNum = value; }
        }
        private string _Mac = "";
        /// <summary>
        /// mac地址
        /// </summary>
        public string Mac
        {
            get { return _Mac; }
            set { _Mac = value; }
        }
        private string _DefaultMedia = "";
        /// <summary>
        /// 默认媒体文件
        /// </summary>
        public string DefaultMedia
        {
            get { return _DefaultMedia; }
            set { _DefaultMedia = value; }
        }
        private string _SCLoopTime = "";
        /// <summary>
        /// 优惠券切换间隔
        /// </summary>
        public string SCLoopTime
        {
            get { return _SCLoopTime; }
            set { _SCLoopTime = value; }
        }
        private string _SentStatusTime = "";
        /// <summary>
        /// 发送设备状态
        /// </summary>
        public string SentStatusTime
        {
            get { return _SentStatusTime; }
            set { _SentStatusTime = value; }
        }
        private string _UpdateTime = "";
        /// <summary>
        /// 播放列表更新时间
        /// </summary>
        public string UpdateTime
        {
            get { return _UpdateTime; }
            set { _UpdateTime = value; }
        }
        
    }


}
