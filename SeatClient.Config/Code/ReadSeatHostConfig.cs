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
            string filePath = string.Format("{0}ServiceHost_MonitorService.exe.config", fileDircetoryPath);
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
                    if (node.Attributes["key"].Value == "ServiceAssembly" && (node.Attributes["value"].Value == "SeatService.MonitorService.MonitorService,SeatService.MonitorService" || node.Attributes["value"].Value == "SeatManage.WCFService.WcfHost,SeatManage.WCFService" || node.Attributes["value"].Value == "AMS.DataTransfer.DataTransferService,AMS.DataTransfer"))
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
                    if (node.Attributes["key"].Value == "ServiceAssembly" && (node.Attributes["value"].Value == "SeatService.MonitorService.MonitorService,SeatService.MonitorService" || node.Attributes["value"].Value == "SeatService.StatisticsService.StatisticsService,SeatService.StatisticsService" || node.Attributes["value"].Value == "SeatService.SyncService.SyncService,SeatService.SyncService" || node.Attributes["value"].Value == "SeatManage.WCFService.WcfHost,SeatManage.WCFService" || node.Attributes["value"].Value == "AMS.DataTransfer.DataTransferService,AMS.DataTransfer"))
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
                    else if (server == "MonitorService")
                    {
                        XmlNode node = doc.SelectSingleNode("//configuration/appSettings");
                        XmlElement element1 = doc.CreateElement("add");
                        element1.SetAttribute("key", "ServiceAssembly");
                        element1.SetAttribute("value", "SeatService.MonitorService.MonitorService,SeatService.MonitorService");
                        node.AppendChild(element1);
                        node = doc.SelectSingleNode("//configuration/appSettings");
                        XmlElement element2 = doc.CreateElement("add");
                        element2.SetAttribute("key", "ServiceAssembly");
                        element2.SetAttribute("value", "SeatService.StatisticsService.StatisticsService,SeatService.StatisticsService");
                        node.AppendChild(element2);
                        node = doc.SelectSingleNode("//configuration/appSettings");
                        XmlElement element3 = doc.CreateElement("add");
                        element3.SetAttribute("key", "ServiceAssembly");
                        element3.SetAttribute("value", "SeatService.SyncService.SyncService,SeatService.SyncService");
                        node.AppendChild(element3);
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
                //XmlNode nodeso = doc.SelectSingleNode("//configuration/appSettings");
                //XmlElement elementso = doc.CreateElement("add");
                //elementso.SetAttribute("key", "ServiceAssembly");
                //elementso.SetAttribute("value", "SMS.SeatTcpServer.SeatBespeakTcpProxy,SMS.SeatTcpServer");
                //nodeso.AppendChild(elementso);
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
}
