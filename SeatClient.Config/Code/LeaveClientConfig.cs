using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using SeatManage.SeatManageComm;

namespace SeatManage.SeatClient.Config.Code
{
    public class LeaveClientConfig
    {
        static XmlDocument doc = null;
        public static bool GetLeaveClientConfig(ref LeaveClientBesicConfig config)
        {
            doc = new XmlDocument();
            string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = string.Format("{0}LeaveClient.exe.config", fileDircetoryPath);
            if (File.Exists(filePath))
            {
                try
                {
                    doc.Load(filePath);
                    XmlNodeList nodes = doc.SelectNodes("//configuration/connectionStrings/add");
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Attributes["name"].Value == "EndpointAddress")
                        {
                            config.WCFConnString = AESAlgorithm.AESDecrypt(node.Attributes["connectionString"].Value);
                            break;
                        }
                    }
                    nodes = doc.SelectNodes("//configuration/appSettings/add");
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Attributes["key"].Value == "windowState")
                        {
                            config.SetUpMode = node.Attributes["value"].Value;
                        }
                        else if (node.Attributes["key"].Value == "LeaveState")
                        {
                            config.LeaveMode = node.Attributes["value"].Value;
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
        public static bool SaveLeaveClientConfig(LeaveClientBesicConfig config)
        {
            doc = new XmlDocument();
            string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = string.Format("{0}LeaveClient.exe.config", fileDircetoryPath);
            if (File.Exists(filePath))
            {
                try
                {
                    doc.Load(filePath);
                    XmlNodeList nodes = doc.SelectNodes("//configuration/connectionStrings/add");
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Attributes["name"].Value == "EndpointAddress")
                        {
                            node.Attributes["connectionString"].Value = AESAlgorithm.AESEncrypt(config.WCFConnString);
                            break;
                        }
                    }
                    nodes = doc.SelectNodes("//configuration/appSettings/add");
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Attributes["key"].Value == "windowState")
                        {
                            node.Attributes["value"].Value = config.SetUpMode;
                        }
                        else if (node.Attributes["key"].Value == "LeaveState")
                        {
                            node.Attributes["value"].Value = config.LeaveMode;
                        }
                    }
                    doc.Save(filePath);
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
    public class LeaveClientBesicConfig
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
        private string _SetUpMode = "1";
        /// <summary>
        /// 窗体启动模式
        /// </summary>
        public string SetUpMode
        {
            get { return _SetUpMode; }
            set { _SetUpMode = value; }
        }
        private string _LeaveMode = "1";
        /// <summary>
        /// 离开模式
        /// </summary>
        public string LeaveMode
        {
            get { return _LeaveMode; }
            set { _LeaveMode = value; }
        }
    }
}
