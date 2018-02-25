using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using SeatManage.SeatManageComm;

namespace SeatManage.SeatClient.Config.Code
{
    public class WebConfig
    {
        static XmlDocument doc = null;
        public static bool ReadConfig(ref WebConfigSetting config)
        {
            doc = new XmlDocument();
            string fileDircetoryPath = Path.GetDirectoryName(Application.StartupPath);
            string filePath = string.Format("{0}\\SeatManageWebV2\\Web.config", fileDircetoryPath);
            if (File.Exists(filePath))
            {
                doc.Load(filePath);
                foreach (XmlNode node in doc.ChildNodes)
                {
                    if (node.Name == "configuration")
                    {
                        foreach (XmlNode nodeA in node.ChildNodes)
                        {
                            if (nodeA.Name == "connectionStrings")
                            {
                                foreach (XmlNode nodeB in nodeA.ChildNodes)
                                {
                                    if (nodeB.Attributes != null && nodeB.Attributes["name"].Value == "EndpointAddress")
                                    {
                                        config.ConnString = AESAlgorithm.AESDecrypt(nodeB.Attributes["connectionString"].Value);
                                        break;
                                    }
                                }
                            }
                            else if (nodeA.Name == "appSettings")
                            {
                                foreach (XmlNode nodeB in nodeA.ChildNodes)
                                {
                                    if (nodeB.Attributes != null && nodeB.Attributes["key"].Value == "ChangePassWord")
                                    {
                                        if (nodeB.Attributes["value"].Value == "open")
                                        {
                                            config.IsChangePW = true;
                                        }
                                        else
                                        {
                                            config.IsChangePW = false;
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    }

                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool SaveConfig(WebConfigSetting config)
        {
            try
            {
                doc = new XmlDocument();
                string fileDircetoryPath = Path.GetDirectoryName(Application.StartupPath);
                string filePath = string.Format("{0}\\SeatManageWebV2\\Web.config", fileDircetoryPath);
                doc.Load(filePath);

                foreach (XmlNode node in doc.ChildNodes)
                {
                    if (node.Name == "configuration")
                    {
                        foreach (XmlNode nodeA in node.ChildNodes)
                        {
                            if (nodeA.Name == "connectionStrings")
                            {
                                foreach (XmlNode nodeB in nodeA.ChildNodes)
                                {
                                    if (nodeB.Attributes != null && nodeB.Attributes["name"].Value == "EndpointAddress")
                                    {
                                        nodeB.Attributes["connectionString"].Value = AESAlgorithm.AESEncrypt(config.ConnString);
                                    }
                                    if (nodeB.Attributes != null && nodeB.Attributes["name"].Value == "ConnectionString")
                                    {
                                        nodeB.Attributes["connectionString"].Value = config.SqlConn;
                                    }

                                }
                            }
                            else if (nodeA.Name == "appSettings")
                            {
                                foreach (XmlNode nodeB in nodeA.ChildNodes)
                                {
                                    if (nodeB.Attributes != null && nodeB.Attributes["key"].Value == "ChangePassWord")
                                    {
                                        if (config.IsChangePW)
                                        {
                                            nodeB.Attributes["value"].Value = "open";
                                        }
                                        else
                                        {
                                            nodeB.Attributes["value"].Value = "true";
                                        }

                                    }
                                    else if (nodeB.Attributes != null && nodeB.Attributes["key"].Value == "SchoolNo")
                                    {
                                        nodeB.Attributes["value"].Value = config.SchoolNo;
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
                doc.Save(filePath);
                if (File.Exists(string.Format("{0}\\SchoolPocketBookWeb\\Web.config", fileDircetoryPath)))
                {
                    doc = new XmlDocument();
                    fileDircetoryPath = Path.GetDirectoryName(Application.StartupPath);
                    filePath = string.Format("{0}\\SchoolPocketBookWeb\\Web.config", fileDircetoryPath);
                    doc.Load(filePath);
                    foreach (XmlNode node in doc.ChildNodes)
                    {
                        if (node.Name == "configuration")
                        {
                            foreach (XmlNode nodeA in node.ChildNodes)
                            {
                                if (nodeA.Name == "connectionStrings")
                                {
                                    foreach (XmlNode nodeB in nodeA.ChildNodes)
                                    {
                                        if (nodeB.Attributes != null && nodeB.Attributes["name"].Value == "EndpointAddress")
                                        {
                                            nodeB.Attributes["connectionString"].Value = AESAlgorithm.AESEncrypt(config.ConnString);
                                        }
                                        if (nodeB.Attributes != null && nodeB.Attributes["name"].Value == "ConnectionString")
                                        {
                                            nodeB.Attributes["connectionString"].Value = config.SqlConn;
                                        }
                                    }
                                }
                                else if (nodeA.Name == "appSettings")
                                {
                                    foreach (XmlNode nodeB in nodeA.ChildNodes)
                                    {
                                        if (nodeB.Attributes != null && nodeB.Attributes["key"].Value == "SchoolNo")
                                        {
                                            nodeB.Attributes["value"].Value = config.SchoolNo;
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
                doc.Save(filePath);
                if (File.Exists(string.Format("{0}\\SchoolNoteEditer\\SchoolNoteEditer.exe.config", fileDircetoryPath)))
                {
                    doc = new XmlDocument();
                    fileDircetoryPath = Path.GetDirectoryName(Application.StartupPath);
                    filePath = string.Format("{0}\\SchoolNoteEditer\\SchoolNoteEditer.exe.config", fileDircetoryPath);
                    doc.Load(filePath);
                    foreach (XmlNode node in doc.ChildNodes)
                    {
                        if (node.Name == "configuration")
                        {
                            foreach (XmlNode nodeA in node.ChildNodes)
                            {
                                if (nodeA.Name == "connectionStrings")
                                {
                                    foreach (XmlNode nodeB in nodeA.ChildNodes)
                                    {
                                        if (nodeB.Attributes != null && nodeB.Attributes["name"].Value == "EndpointAddress")
                                        {
                                            nodeB.Attributes["connectionString"].Value = AESAlgorithm.AESEncrypt(config.ConnString);
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    }
                    doc.Save(filePath);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    public class WebConfigSetting
    {
        private string _ConnString = "";
        /// <summary>
        /// 加密字符串
        /// </summary>
        public string ConnString
        {
            get { return _ConnString; }
            set { _ConnString = value; }
        }
        bool _IsChangePW = true;
        /// <summary>
        /// 是否可以修改密码
        /// </summary>
        public bool IsChangePW
        {
            get { return _IsChangePW; }
            set { _IsChangePW = value; }
        }

        private string _SchoolNo = "";

        public string SqlConn
        {
            get { return _SQLConn; }
            set { _SQLConn = value; }
        }

        public string SchoolNo
        {
            get { return _SchoolNo; }
            set { _SchoolNo = value; }
        }

        private string _SQLConn = "";
    }
}
