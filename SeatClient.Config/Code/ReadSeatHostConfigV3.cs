using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using SeatManage.SeatManageComm;
using System.Diagnostics;
using System.Windows.Forms;

namespace SeatManage.SeatClient.Config.Code
{
    public class ReadSeatHostConfigV3
    {
        static XmlDocument docMonitorService = null;
        static XmlDocument docTimerHost = null;
        static XmlDocument docWCFHost = null;
        static XmlDocument docWeChar = null;
        static XmlDocument docWeCharWcf = null;

        public static bool ReadConfig(ref HostConfig hostconfig)
        {
            docMonitorService = new XmlDocument();
            docTimerHost = new XmlDocument();
            docWCFHost = new XmlDocument();
            docWeChar = new XmlDocument();
            docWeCharWcf = new XmlDocument();
            string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory;
            if (File.Exists(Path.GetDirectoryName(Application.StartupPath) + "\\MonitorService\\ServiceHostMonitorService.exe.config"))
            {
                docMonitorService.Load(Path.GetDirectoryName(Application.StartupPath) + "\\MonitorService\\ServiceHostMonitorService.exe.config");

                if (File.Exists(fileDircetoryPath + "ServiceHostTimerHost.exe.config"))
                {
                    docTimerHost.Load(fileDircetoryPath + "ServiceHostTimerHost.exe.config");
                }
                else
                {
                    return false;
                }
                if (File.Exists(fileDircetoryPath + "ServiceHostWCFHost.exe.config"))
                {
                    docWCFHost.Load(fileDircetoryPath + "ServiceHostWCFHost.exe.config");
                }
                else
                {
                    return false;
                }
                if (File.Exists(fileDircetoryPath + "ServiceHostWeChar.exe.config"))
                {
                    docWeChar.Load(fileDircetoryPath + "ServiceHostWeChar.exe.config");
                }
                else
                {
                    return false;
                }
                if (File.Exists(fileDircetoryPath + "ServiceHostWeChatWCFHost.exe.config"))
                {
                    docWeChar.Load(fileDircetoryPath + "ServiceHostWeChatWCFHost.exe.config");
                }
                else
                {
                    return false;
                }
                string[] db = docWCFHost.SelectNodes("//configuration/connectionStrings/add").Cast<XmlNode>().First(p => p.Attributes["name"].Value == "ConnectionString").Attributes["connectionString"].Value.Split(';');
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
                XmlNode xmlNode = docTimerHost.SelectNodes("//configuration/connectionStrings/add").Cast<XmlNode>().First(p => p.Attributes["name"].Value == "EndpointAddress");
                hostconfig.WCFString = AESAlgorithm.AESDecrypt(xmlNode.Attributes["connectionString"].Value);

                xmlNode = docMonitorService.SelectNodes("//configuration/connectionStrings/add").Cast<XmlNode>().First(p => p.Attributes["name"].Value == "WeChatEndpointAddress");
                hostconfig.WeChatWCFString = AESAlgorithm.AESDecrypt(xmlNode.Attributes["connectionString"].Value);
                XmlNodeList nodes = docTimerHost.SelectNodes("//configuration/appSettings/add");
                foreach (XmlNode node in nodes)
                {
                    if (node.Attributes["key"].Value == "ServiceAssembly" && (node.Attributes["value"].Value == "SeatService.MonitorService.MonitorService,SeatService.MonitorService" || node.Attributes["value"].Value == "SeatManage.WCFService.WcfHost,SeatManage.WCFService" || node.Attributes["value"].Value == "AMS.DataTransfer.DataTransferService,AMS.DataTransfer"))
                    {
                        hostconfig.HostServer.Add(node.Attributes["value"].Value.Split(',')[0].Split('.')[2]);
                    }
                    else
                    {
                        switch (node.Attributes["key"].Value)
                        {
                            case "SaveFilePath":
                                hostconfig.MediaFilePath = node.Attributes["value"].Value;
                                break;
                            case "LogUploadTime":
                                hostconfig.UploadTime = node.Attributes["value"].Value;
                                break;
                            case "Interval":
                                hostconfig.LoopTime = node.Attributes["value"].Value;
                                break;
                            case "SchoolNo":
                                hostconfig.SchoolNo = node.Attributes["value"].Value;
                                break;
                        }
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
                docMonitorService = new XmlDocument();
                docTimerHost = new XmlDocument();
                docWCFHost = new XmlDocument();
                docWeChar = new XmlDocument();
                docWeCharWcf = new XmlDocument();
                string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory;
                if (File.Exists(Path.GetDirectoryName(Application.StartupPath) + "\\MonitorService\\ServiceHostMonitorService.exe.config"))
                {
                    docMonitorService.Load(Path.GetDirectoryName(Application.StartupPath) + "\\MonitorService\\ServiceHostMonitorService.exe.config");
                }
                if (File.Exists(fileDircetoryPath + "ServiceHostTimerHost.exe.config"))
                {
                    docTimerHost.Load(fileDircetoryPath + "ServiceHostTimerHost.exe.config");
                }
                if (File.Exists(fileDircetoryPath + "ServiceHostWCFHost.exe.config"))
                {
                    docWCFHost.Load(fileDircetoryPath + "ServiceHostWCFHost.exe.config");
                }
                if (File.Exists(fileDircetoryPath + "ServiceHostWeChar.exe.config"))
                {
                    docWeChar.Load(fileDircetoryPath + "ServiceHostWeChar.exe.config");
                }
                if (File.Exists(fileDircetoryPath + "ServiceHostWeChatWCFHost.exe.config"))
                {
                    docWeCharWcf.Load(fileDircetoryPath + "ServiceHostWeChatWCFHost.exe.config");
                }
                docTimerHost.SelectNodes("//configuration/connectionStrings/add").Cast<XmlNode>().First(p => p.Attributes["name"].Value == "ConnectionString").Attributes["connectionString"].Value = "Data Source=" + hostconfig.DBIP + ";Initial Catalog=" + hostconfig.DBName + ";Persist Security Info=True" + ";User ID=" + hostconfig.DBUser + ";Password=" + hostconfig.DBPW;
                docTimerHost.SelectNodes("//configuration/connectionStrings/add").Cast<XmlNode>().First(p => p.Attributes["name"].Value == "EndpointAddress").Attributes["connectionString"].Value = AESAlgorithm.AESEncrypt(hostconfig.WCFString);
                docTimerHost.SelectNodes("//configuration/connectionStrings/add").Cast<XmlNode>().First(p => p.Attributes["name"].Value == "AdvertServiceEndpointAddress").Attributes["connectionString"].Value = AESAlgorithm.AESEncrypt(hostconfig.AdrWcfString);
                docMonitorService.SelectNodes("//configuration/connectionStrings/add").Cast<XmlNode>().First(p => p.Attributes["name"].Value == "EndpointAddress").Attributes["connectionString"].Value = AESAlgorithm.AESEncrypt(hostconfig.WCFString);
                docMonitorService.SelectNodes("//configuration/connectionStrings/add").Cast<XmlNode>().First(p => p.Attributes["name"].Value == "WeChatEndpointAddress").Attributes["connectionString"].Value = AESAlgorithm.AESEncrypt(hostconfig.WeChatWCFString);
                docWCFHost.SelectNodes("//configuration/connectionStrings/add").Cast<XmlNode>().First(p => p.Attributes["name"].Value == "ConnectionString").Attributes["connectionString"].Value = "Data Source=" + hostconfig.DBIP + ";Initial Catalog=" + hostconfig.DBName + ";Persist Security Info=True" + ";User ID=" + hostconfig.DBUser + ";Password=" + hostconfig.DBPW;
                docWCFHost.SelectNodes("//configuration/connectionStrings/add").Cast<XmlNode>().First(p => p.Attributes["name"].Value == "EndpointAddress").Attributes["connectionString"].Value = AESAlgorithm.AESEncrypt(hostconfig.WCFString);
                docWeChar.SelectNodes("//configuration/connectionStrings/add").Cast<XmlNode>().First(p => p.Attributes["name"].Value == "ConnectionString").Attributes["connectionString"].Value = "Data Source=" + hostconfig.DBIP + ";Initial Catalog=" + hostconfig.DBName + ";Persist Security Info=True" + ";User ID=" + hostconfig.DBUser + ";Password=" + hostconfig.DBPW;
                //docTimerHost.SelectNodes("//configuration/connectionStrings/add").Cast<XmlNode>().First(p => p.Attributes["name"].Value == "WeChatEndpointAddress").Attributes["connectionString"].Value = AESAlgorithm.AESEncrypt(hostconfig.WeChatWCFString);
                docWeCharWcf.SelectNodes("//configuration/connectionStrings/add").Cast<XmlNode>().First(p => p.Attributes["name"].Value == "ConnectionString").Attributes["connectionString"].Value = "Data Source=" + hostconfig.DBIP + ";Initial Catalog=" + hostconfig.DBName + ";Persist Security Info=True" + ";User ID=" + hostconfig.DBUser + ";Password=" + hostconfig.DBPW;
                docWeCharWcf.SelectNodes("//configuration/connectionStrings/add").Cast<XmlNode>().First(p => p.Attributes["name"].Value == "EndpointAddress").Attributes["connectionString"].Value = AESAlgorithm.AESEncrypt(hostconfig.WCFString);


                XmlNodeList nodes = docWCFHost.SelectNodes("//configuration/system.serviceModel/services/service/host/baseAddresses/add");
                foreach (XmlNode node in nodes)
                {
                    string[] sp = node.Attributes["baseAddress"].Value.Split('/');
                    node.Attributes["baseAddress"].Value = hostconfig.WCFString + sp[sp.Length - 2] + "/";
                }
                nodes = docWCFHost.SelectNodes("//configuration/appSettings/add");

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
                docWCFHost.Save(fileDircetoryPath + "ServiceHostWCFHost.exe.config");

                nodes = docWeCharWcf.SelectNodes("//configuration/system.serviceModel/services/service/host/baseAddresses/add");
                foreach (XmlNode node in nodes)
                {
                    string[] sp = node.Attributes["baseAddress"].Value.Split('/');
                    node.Attributes["baseAddress"].Value = hostconfig.WeChatWCFString + sp[sp.Length - 2] + "/";
                }
                nodes = docWeCharWcf.SelectNodes("//configuration/appSettings/add");

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
                docWeCharWcf.Save(fileDircetoryPath + "ServiceHostWeChatWCFHost.exe.config");

                nodes = docTimerHost.SelectNodes("//configuration/appSettings/add");
                foreach (XmlNode node in nodes)
                {
                    if (node.Attributes["key"].Value == "ServiceAssembly"
                        && (node.Attributes["value"].Value == "SeatService.MonitorService.MonitorService,SeatService.MonitorService"
                        || node.Attributes["value"].Value == "SeatService.StatisticsService.StatisticsService,SeatService.StatisticsService"
                        || node.Attributes["value"].Value == "SeatService.SyncService.SyncService,SeatService.SyncService"
                        || node.Attributes["value"].Value == "SeatManage.WCFService.WcfHost,SeatManage.WCFService"
                        || node.Attributes["value"].Value == "AMS.DataTransfer.DataTransferService,AMS.DataTransfer"))
                    {
                        XmlNode fnode = docTimerHost.SelectSingleNode("//configuration/appSettings");
                        fnode.RemoveChild(node);
                    }
                }
                foreach (string server in hostconfig.HostServer)
                {
                    switch (server)
                    {
                        case "MonitorService":
                            {
                                XmlNode node = docTimerHost.SelectSingleNode("//configuration/appSettings");
                                XmlElement element1 = docTimerHost.CreateElement("add");
                                element1.SetAttribute("key", "ServiceAssembly");
                                element1.SetAttribute("value", "SeatService.MonitorService.MonitorService,SeatService.MonitorService");
                                node.AppendChild(element1);
                                node = docTimerHost.SelectSingleNode("//configuration/appSettings");
                                XmlElement element2 = docTimerHost.CreateElement("add");
                                element2.SetAttribute("key", "ServiceAssembly");
                                element2.SetAttribute("value", "SeatService.StatisticsService.StatisticsService,SeatService.StatisticsService");
                                node.AppendChild(element2);
                                node = docTimerHost.SelectSingleNode("//configuration/appSettings");
                                XmlElement element3 = docTimerHost.CreateElement("add");
                                element3.SetAttribute("key", "ServiceAssembly");
                                element3.SetAttribute("value", "SeatService.SyncService.SyncService,SeatService.SyncService");
                                node.AppendChild(element3);
                            }
                            break;
                        case "DataTransferService":
                            {
                                XmlNode node = docTimerHost.SelectSingleNode("//configuration/appSettings");
                                XmlElement element = docTimerHost.CreateElement("add");
                                element.SetAttribute("key", "ServiceAssembly");
                                element.SetAttribute("value", "AMS.DataTransfer.DataTransferService,AMS.DataTransfer");
                                node.AppendChild(element);
                            }
                            break;
                    }
                }




                nodes = docTimerHost.SelectNodes("//configuration/appSettings/add");

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
                docTimerHost.Save(fileDircetoryPath + "ServiceHostTimerHost.exe.config");

                nodes = docWeChar.SelectNodes("//configuration/appSettings/add");



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
                docWeChar.Save(fileDircetoryPath + "ServiceHostWeChar.exe.config");
                docMonitorService.Save(Path.GetDirectoryName(Application.StartupPath) + "\\MonitorService\\ServiceHostMonitorService.exe.config");
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
