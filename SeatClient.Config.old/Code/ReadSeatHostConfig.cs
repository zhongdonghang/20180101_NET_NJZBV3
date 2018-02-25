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
    public class ReadSeatHostConfig
    {
        static XmlDocument doc = null;
        public static bool ReadConfig(ref HostConfig hostconfig)
        {
            doc = new XmlDocument();
            string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = string.Format("{0}WindowsServiceHost.exe.config", fileDircetoryPath);
            if (File.Exists(filePath))
            {
                doc.Load(filePath);
                XmlNodeList nodes = doc.SelectNodes("//configuration/connectionStrings/add");
                foreach (XmlNode node in nodes)
                {
                    if (node.Attributes["name"].Value == "ConnectionString")
                    {
                        string[] db = node.Attributes["connectionString"].Value.Split(';');
                        for (int i = 0; i < db.Length; i++)
                        {
                            switch (db[i].Split('=')[0])
                            {
                                case "Data Source":
                                    hostconfig.DBIP = db[i].Split('=')[1];
                                    break;
                                case "Initial Catalog":
                                    hostconfig.DBName = db[i].Split('=')[1];
                                    break;
                                case "User ID":
                                    hostconfig.DBUser = db[i].Split('=')[1];
                                    break;
                                case "Password":
                                    hostconfig.DBPW = db[i].Split('=')[1];
                                    break;
                            }
                        }
                    }
                    else if (node.Attributes["name"].Value == "EndpointAddress")
                    {
                        hostconfig.WCFString = AESAlgorithm.AESDecrypt(node.Attributes["connectionString"].Value);
                    }
                }
                nodes = doc.SelectNodes("//configuration/appSettings/add");
                foreach (XmlNode node in nodes)
                {
                    if (node.Attributes["key"].Value == "ServiceAssembly")
                    {
                        hostconfig.HostServer.Add(node.Attributes["value"].Value.Split(',')[0].Split('.')[2]);
                    }
                    else if (node.Attributes["key"].Value == "SaveFilePath")
                    {
                        hostconfig.MediaFilePath = node.Attributes["value"].Value;
                    }
                    else if (node.Attributes["key"].Value == "LogUploadTime")
                    {
                        hostconfig.UploadTime = node.Attributes["value"].Value;
                    }
                    else if (node.Attributes["key"].Value == "Interval")
                    {
                        hostconfig.LoopTime = node.Attributes["value"].Value;
                    }
                    else if (node.Attributes["key"].Value == "SchoolNo")
                    {
                        hostconfig.SchoolNo = node.Attributes["value"].Value;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool SaveConfig(HostConfig hostconfig)
        {
            try
            {
                doc = new XmlDocument();
                string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = string.Format("{0}WindowsServiceHost.exe.config", fileDircetoryPath);
                doc.Load(filePath);
                XmlNodeList nodes = doc.SelectNodes("//configuration/connectionStrings/add");
                foreach (XmlNode node in nodes)
                {
                    if (node.Attributes["name"].Value == "ConnectionString")
                    {
                        node.Attributes["connectionString"].Value = "Data Source=" + hostconfig.DBIP + ";Initial Catalog=" + hostconfig.DBName + ";Persist Security Info=True" + ";User ID=" + hostconfig.DBUser + ";Password=" + hostconfig.DBPW;
                    }
                    else if (node.Attributes["name"].Value == "EndpointAddress")
                    {
                        node.Attributes["connectionString"].Value = AESAlgorithm.AESEncrypt(hostconfig.WCFString);
                    }
                    else if (node.Attributes["name"].Value == "AdvertServiceEndpointAddress")
                    {
                        node.Attributes["connectionString"].Value = AESAlgorithm.AESEncrypt(hostconfig.AdrWcfString);
                    }
                    else if (node.Attributes["name"].Value == "ip")
                    {
                        node.Attributes["connectionString"].Value = hostconfig.AdvSocketString;
                    }
                }
                nodes = doc.SelectNodes("//configuration/system.serviceModel/services/service/host/baseAddresses/add");
                foreach (XmlNode node in nodes)
                {
                    string[] sp = node.Attributes["baseAddress"].Value.Split('/');
                    node.Attributes["baseAddress"].Value = hostconfig.WCFString + sp[sp.Length - 2] + "/";
                }
                nodes = doc.SelectNodes("//configuration/appSettings/add");
                foreach (XmlNode node in nodes)
                {
                    if (node.Attributes["key"].Value == "ServiceAssembly")
                    {
                        XmlNode fnode = doc.SelectSingleNode("//configuration/appSettings");
                        fnode.RemoveChild(node);
                    }
                }
                foreach (string server in hostconfig.HostServer)
                {
                    if (server == "WcfHost")
                    {
                        XmlNode node = doc.SelectSingleNode("//configuration/appSettings");
                        XmlElement element = doc.CreateElement("add");
                        element.SetAttribute("key", "ServiceAssembly");
                        element.SetAttribute("value", "SeatManage.WCFService.WcfHost,SeatManage.WCFService");
                        node.AppendChild(element);
                    }
                    else if (server == "SeatWatch")
                    {
                        XmlNode node = doc.SelectSingleNode("//configuration/appSettings");
                        XmlElement element = doc.CreateElement("add");
                        element.SetAttribute("key", "ServiceAssembly");
                        element.SetAttribute("value", "SeatService.Service.SeatWatch,SeatService.Service");
                        node.AppendChild(element);
                    }
                    else if (server == "DataTransferService")
                    {
                        XmlNode node = doc.SelectSingleNode("//configuration/appSettings");
                        XmlElement element = doc.CreateElement("add");
                        element.SetAttribute("key", "ServiceAssembly");
                        element.SetAttribute("value", "AMS.DataTransfer.DataTransferService,AMS.DataTransfer");
                        node.AppendChild(element);
                    }
                }
                XmlNode nodeso = doc.SelectSingleNode("//configuration/appSettings");
                XmlElement elementso = doc.CreateElement("add");
                elementso.SetAttribute("key", "ServiceAssembly");
                elementso.SetAttribute("value", "SMS.SeatTcpServer.SeatBespeakTcpProxy,SMS.SeatTcpServer");
                nodeso.AppendChild(elementso);
                foreach (XmlNode node in nodes)
                {
                    if (node.Attributes["key"] == null)
                    {
                        continue;
                    }
                    if (node.Attributes["key"].Value == "SaveFilePath")
                    {
                        node.Attributes["value"].Value = hostconfig.MediaFilePath;
                    }
                    else if (node.Attributes["key"].Value == "LogUploadTime")
                    {
                        node.Attributes["value"].Value = hostconfig.UploadTime;
                    }
                    else if (node.Attributes["key"].Value == "Interval")
                    {
                        node.Attributes["value"].Value = hostconfig.LoopTime;
                    }
                    else if (node.Attributes["key"].Value == "SchoolNo")
                    {
                        node.Attributes["value"].Value = hostconfig.SchoolNo;
                    }
                }
                doc.Save(filePath);
                XmlDocument docx = new XmlDocument();
                XmlDeclaration dec = docx.CreateXmlDeclaration("1.0", "utf-8", null);
                docx.AppendChild(dec);
                XmlElement root = docx.CreateElement("configuration");//创建根节点
                XmlElement elementx = docx.CreateElement("connectionStrings");
                XmlElement addelement = docx.CreateElement("add");
                addelement.SetAttribute("name", "EndpointAddress");
                addelement.SetAttribute("connectionString", AESAlgorithm.AESEncrypt(hostconfig.WCFString));
                elementx.AppendChild(addelement);
                root.AppendChild(elementx);
                XmlElement setelement = docx.CreateElement("startup");
                XmlElement supelement = docx.CreateElement("supportedRuntime");
                supelement.SetAttribute("version", "v4.0");
                supelement.SetAttribute("sku", ".NETFramework,Version=v4.0");
                setelement.AppendChild(supelement);
                root.AppendChild(setelement);
                docx.AppendChild(root);
                docx.Save(Process.GetCurrentProcess().MainModule.FileName + ".config");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    /// <summary>
    /// 宿主服务配置
    /// </summary>
    public class HostConfig
    {
        List<string> _HostServer = new List<string>();
        /// <summary>
        /// 服务所要启动的服务
        /// </summary>
        public List<string> HostServer
        {
            get { return _HostServer; }
            set { _HostServer = value; }
        }
        string _DBIP = "";
        //数据库ip
        public string DBIP
        {
            get { return _DBIP; }
            set { _DBIP = value; }
        }
        string _DBName = "";
        /// <summary>
        /// 数据库名
        /// </summary>
        public string DBName
        {
            get { return _DBName; }
            set { _DBName = value; }
        }
        string _DBUser = "";
        /// <summary>
        /// 数据库登录名
        /// </summary>
        public string DBUser
        {
            get { return _DBUser; }
            set { _DBUser = value; }
        }
        string _DBPW = "";
        /// <summary>
        /// 数据库密码
        /// </summary>
        public string DBPW
        {
            get { return _DBPW; }
            set { _DBPW = value; }
        }
        string _WCFString = "";
        /// <summary>
        /// WCF连接字符串
        /// </summary>
        public string WCFString
        {
            get { return _WCFString; }
            set { _WCFString = value; }
        }
        string _AdrWcfString = "net.tcp://180.96.23.82:10086/";
        /// <summary>
        /// 180wcf连接字符串
        /// </summary>
        public string AdrWcfString
        {
            get { return _AdrWcfString; }
            set { _AdrWcfString = value; }
        }
        string _AdvSocketString = "180.96.23.82:10010";
        /// <summary>
        /// Socket连接字符
        /// </summary>
        public string AdvSocketString
        {
            get { return _AdvSocketString; }
            set { _AdvSocketString = value; }
        }
        string _MediaFilePath = "D:\\SeatManageV2\\FileTransport\\School\\";
        /// <summary>
        /// 文件存储地址
        /// </summary>
        public string MediaFilePath
        {
            get { return _MediaFilePath; }
            set { _MediaFilePath = value; }
        }
        string _SchoolNo = "";

        public string SchoolNo
        {
            get { return _SchoolNo; }
            set { _SchoolNo = value; }
        }
        string _LoopTime = "";
        /// <summary>
        /// 服务循环间隔
        /// </summary>
        public string LoopTime
        {
            get { return _LoopTime; }
            set { _LoopTime = value; }
        }
        string _UploadTime = "";
        /// <summary>
        /// 上传时间
        /// </summary>
        public string UploadTime
        {
            get { return _UploadTime; }
            set { _UploadTime = value; }
        }
    }
}
