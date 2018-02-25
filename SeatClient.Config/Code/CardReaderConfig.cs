using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace SeatManage.SeatClient.Config.Code
{
    public class CardReaderConfig
    {
        static XmlDocument SCdoc = null;
        /// <summary>
        /// 获取读卡器设置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static bool GetCardReaderBaseConfig(ref CardReaderBasicConfig config, string clienttype)
        {
            SCdoc = new XmlDocument();
            string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string scfilePath = "";
            if (clienttype == "Select")
            {
                scfilePath = string.Format("{0}SeatClient\\SeatClient.exe.config", fileDircetoryPath);
            }
            else if (clienttype == "Leave")
            {
                scfilePath = string.Format("{0}LeaveClient.exe.config", fileDircetoryPath);
            }
            if (File.Exists(scfilePath))
            {
                try
                {
                    SCdoc.Load(scfilePath);

                    XmlNodeList nodes = SCdoc.SelectNodes("//configuration/appSettings/add");
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Attributes["key"].Value == "CardSnType")
                        {
                            int t = int.Parse(node.Attributes["value"].Value);
                            switch (t)
                            {
                                case 0:
                                    config.CardID10Or16 = 10;
                                    config.CardIDIsChange = false;
                                    break;
                                case 1:
                                    config.CardID10Or16 = 16;
                                    config.CardIDIsChange = false;
                                    break;
                                case 2:
                                    config.CardID10Or16 = 16;
                                    config.CardIDIsChange = true;
                                    break;
                                case 3:
                                    config.CardID10Or16 = 10;
                                    config.CardIDIsChange = true;
                                    break;
                            }
                        }
                        else if (node.Attributes["key"].Value == "IsBeep")
                        {
                            config.IsBeep = bool.Parse(node.Attributes["value"].Value);
                        }
                        else if (node.Attributes["key"].Value == "cardSnLength")
                        {
                            if (node.Attributes["value"].Value == "Full")
                            {
                                config.IsAdd0 = true;
                            }
                            else
                            {
                                config.IsAdd0 = false;
                            }
                        }
                        else if (node.Attributes["key"].Value == "XZX_ServerEndPort")
                        {
                            config.XZX_ServerEndPort = node.Attributes["value"].Value;
                        }
                        else if (node.Attributes["key"].Value == "XZX_SysCode")
                        {
                            config.XZX_SysCode = node.Attributes["value"].Value;
                        }
                        else if (node.Attributes["key"].Value == "XZX_TerminalNo")
                        {
                            config.XZX_TerminalNo = node.Attributes["value"].Value;
                        }
                        else if (node.Attributes["key"].Value == "XZX_Offline")
                        {
                            config.XZX_Offline = bool.Parse(node.Attributes["value"].Value);
                        }
                        else if (node.Attributes["key"].Value == "XZX_AddReader")
                        {
                            config.XZX_AddReader = bool.Parse(node.Attributes["value"].Value);
                        }
                        else if (node.Attributes["key"].Value == "XZX_IsOnelyReaderCardId")
                        {
                            config.XZX_IsOnelyReaderCardId = bool.Parse(node.Attributes["value"].Value);
                        }
                        else if (node.Attributes["key"].Value == "Hook_isCardNo")
                        {
                            config.Hook_isCardNo = bool.Parse(node.Attributes["value"].Value);
                        }
                        else if (node.Attributes["key"].Value == "PosPort")
                        {
                            config.FKport = node.Attributes["value"].Value;
                        }
                        else if (node.Attributes["key"].Value == "IPOSMethod")
                        {
                            switch (node.Attributes["value"].Value)
                            {
                                case "PosObject.PosObject,PosObject": config.CardReaderTye = 0; break;
                                case "PosObject.PosObject,PosObject_XZX": config.CardReaderTye = 1; break;
                                case "PosObject.PosObject,PosObject_FK": config.CardReaderTye = 2; break;
                                case "PosObject.PosObject,PosObject_HOOK": config.CardReaderTye = 3; break;
                                case "PosObject.PosObject,PosObject_CUT": config.CardReaderTye = 4; break;
                                case "PosObject.PosObject,PosObject_Customer": config.CardReaderTye = 5; break;

                            }
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
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static bool SaveConfig(CardReaderBasicConfig config, string clienttype)
        {
            SCdoc = new XmlDocument();
            string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string scfilePath = "";
            if (clienttype == "Select")
            {
                scfilePath = string.Format("{0}SeatClient\\SeatClient.exe.config", fileDircetoryPath);
            }
            else if (clienttype == "Leave")
            {
                scfilePath = string.Format("{0}LeaveClient.exe.config", fileDircetoryPath);
            }
            if (File.Exists(scfilePath))
            {
                bool isbeepUpdate = false;
                bool isxzx_isreadcarddidupdate = false;
                bool ishook_isreadecard = false;
                try
                {
                    SCdoc.Load(scfilePath);

                    XmlNodeList nodes = SCdoc.SelectNodes("//configuration/appSettings/add");
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Attributes["key"].Value == "CardSnType")
                        {
                            if (config.CardID10Or16 == 10)
                            {
                                if (config.CardIDIsChange)
                                {
                                    node.Attributes["value"].Value = "3";
                                }
                                else
                                {
                                    node.Attributes["value"].Value = "0";
                                }
                            }
                            else
                            {
                                if (config.CardIDIsChange)
                                {
                                    node.Attributes["value"].Value = "2";
                                }
                                else
                                {
                                    node.Attributes["value"].Value = "1";
                                }
                            }
                        }
                        else if (node.Attributes["key"].Value == "IsBeep")
                        {
                            if (config.IsBeep)
                            {
                                node.Attributes["value"].Value = "true";
                            }
                            else
                            {
                                node.Attributes["value"].Value = "false";
                            }
                            isbeepUpdate = true;
                        }
                        else if (node.Attributes["key"].Value == "cardSnLength")
                        {
                            if (config.IsAdd0)
                            {
                                node.Attributes["value"].Value = "Full";
                            }
                            else
                            {
                                node.Attributes["value"].Value = "Part";
                            }
                        }
                        else if (node.Attributes["key"].Value == "XZX_ServerEndPort")
                        {
                            node.Attributes["value"].Value = config.XZX_ServerEndPort;
                        }
                        else if (node.Attributes["key"].Value == "XZX_SysCode")
                        {
                            node.Attributes["value"].Value = config.XZX_SysCode;
                        }
                        else if (node.Attributes["key"].Value == "XZX_TerminalNo")
                        {
                            node.Attributes["value"].Value = config.XZX_TerminalNo;
                        }
                        else if (node.Attributes["key"].Value == "XZX_Offline")
                        {
                            if (config.XZX_Offline)
                            {
                                node.Attributes["value"].Value = "true";
                            }
                            else
                            {
                                node.Attributes["value"].Value = "false";
                            }
                        }
                        else if (node.Attributes["key"].Value == "XZX_AddReader")
                        {
                            if (config.XZX_AddReader)
                            {
                                node.Attributes["value"].Value = "true";
                            }
                            else
                            {
                                node.Attributes["value"].Value = "false";
                            }
                        }
                        else if (node.Attributes["key"].Value == "XZX_IsOnelyReaderCardId")
                        {
                            if (config.XZX_IsOnelyReaderCardId)
                            {
                                node.Attributes["value"].Value = "true";
                            }
                            else
                            {
                                node.Attributes["value"].Value = "false";
                            }
                            isxzx_isreadcarddidupdate = true;
                        }
                        else if (node.Attributes["key"].Value == "Hook_isCardNo")
                        {
                            if (config.Hook_isCardNo)
                            {
                                node.Attributes["value"].Value = "true";
                            }
                            else
                            {
                                node.Attributes["value"].Value = "false";
                            }
                            ishook_isreadecard = true;
                        }
                        else if (node.Attributes["key"].Value == "PosPort")
                        {
                            node.Attributes["value"].Value = config.FKport;
                        }
                        else if (node.Attributes["key"].Value == "IPOSMethod")
                        {
                            switch (config.CardReaderTye)
                            {
                                case 0: node.Attributes["value"].Value = "PosObject.PosObject,PosObject"; break;
                                case 1: node.Attributes["value"].Value = "PosObject.PosObject,PosObject_XZX"; break;
                                case 2: node.Attributes["value"].Value = "PosObject.PosObject,PosObject_FK"; break;
                                case 3: node.Attributes["value"].Value = "PosObject.PosObject,PosObject_HOOK"; break;
                                case 4: node.Attributes["value"].Value = "PosObject.PosObject,PosObject_CUT"; break;
                                case 5: node.Attributes["value"].Value = "PosObject.PosObject,PosObject_Customer"; break;
                            }
                        }
                        else if (node.Attributes["key"].Value == "ICardReader")
                        {
                            switch (config.CardReaderTye)
                            {
                                case 0: node.Attributes["value"].Value = "CardReaderObject.CardReaderObject,CardReaderObject"; break;
                                case 1: node.Attributes["value"].Value = "CardReaderObject.CardReaderObject,CardReaderObject_XZX"; break;
                                case 2: node.Attributes["value"].Value = "CardReaderObject.CardReaderObject,CardReaderObject_FK"; break;
                                case 3: node.Attributes["value"].Value = "CardReaderObject.CardReaderObject,CardReaderObject_HOOK"; break;
                                case 4: node.Attributes["value"].Value = "CardReaderObject.CardReaderObject,CardReaderObject_CUT"; break;
                                case 5: node.Attributes["value"].Value = "CardReaderObject.CardReaderObject,CardReaderObject_Customer"; break;
                            }
                        }
                    }
                    if (!isbeepUpdate)
                    {
                        XmlNode node = SCdoc.SelectSingleNode("//configuration/appSettings");
                        XmlElement element = SCdoc.CreateElement("add");
                        element.SetAttribute("key", "IsBeep");
                        element.SetAttribute("value", "true");
                        node.AppendChild(element);
                    }
                    if (!isxzx_isreadcarddidupdate)
                    {
                        XmlNode node = SCdoc.SelectSingleNode("//configuration/appSettings");
                        XmlElement element = SCdoc.CreateElement("add");
                        element.SetAttribute("key", "XZX_IsOnelyReaderCardId");
                        element.SetAttribute("value", "false");
                        node.AppendChild(element);
                    }
                    if (!ishook_isreadecard)
                    {
                        XmlNode node = SCdoc.SelectSingleNode("//configuration/appSettings");
                        XmlElement element = SCdoc.CreateElement("add");
                        element.SetAttribute("key", "Hook_isCardNo");
                        element.SetAttribute("value", "true");
                        node.AppendChild(element);
                    }
                    SCdoc.Save(scfilePath);
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
    public class CardReaderBasicConfig
    {
        private int _CardReaderTye = 0;
        /// <summary>
        /// 读卡器类型
        /// </summary>
        public int CardReaderTye
        {
            get { return _CardReaderTye; }
            set { _CardReaderTye = value; }
        }
        private bool _IsBeep = true;
        /// <summary>
        /// 是否鸣叫
        /// </summary>
        public bool IsBeep
        {
            get { return _IsBeep; }
            set { _IsBeep = value; }
        }
        private int _CardID10Or16 = 10;
        /// <summary>
        /// 物理ID进制
        /// </summary>
        public int CardID10Or16
        {
            get { return _CardID10Or16; }
            set { _CardID10Or16 = value; }
        }
        private bool _CardIDIsChange = true;
        /// <summary>
        /// 是否转换数位
        /// </summary>
        public bool CardIDIsChange
        {
            get { return _CardIDIsChange; }
            set { _CardIDIsChange = value; }
        }
        private bool _IsAdd0 = false;
        /// <summary>
        /// 是否补零
        /// </summary>
        public bool IsAdd0
        {
            get { return _IsAdd0; }
            set { _IsAdd0 = value; }
        }
        private string _FKport = "";
        /// <summary>
        /// 方卡端口号
        /// </summary>
        public string FKport
        {
            get { return _FKport; }
            set { _FKport = value; }
        }
        private string _XZX_ServerEndPort = "";
        /// <summary>
        /// 第三方ip
        /// </summary>
        public string XZX_ServerEndPort
        {
            get { return _XZX_ServerEndPort; }
            set { _XZX_ServerEndPort = value; }
        }
        private string _XZX_SysCode = "";
        /// <summary>
        /// 子系统号
        /// </summary>
        public string XZX_SysCode
        {
            get { return _XZX_SysCode; }
            set { _XZX_SysCode = value; }
        }
        private string _XZX_TerminalNo = "";
        /// <summary>
        /// 站点号
        /// </summary>
        public string XZX_TerminalNo
        {
            get { return _XZX_TerminalNo; }
            set { _XZX_TerminalNo = value; }
        }
        private bool _XZX_Offline = true;
        /// <summary>
        /// 是否脱机
        /// </summary>
        public bool XZX_Offline
        {
            get { return _XZX_Offline; }
            set { _XZX_Offline = value; }
        }
        private bool _XZX_AddReader = false;
        /// <summary>
        /// 是否添加学生
        /// </summary>
        public bool XZX_AddReader
        {
            get { return _XZX_AddReader; }
            set { _XZX_AddReader = value; }
        }
        private bool _XZX_IsOnelyReaderCardId = true;
        /// <summary>
        /// 是否读取物理卡号
        /// </summary>
        public bool XZX_IsOnelyReaderCardId
        {
            get { return _XZX_IsOnelyReaderCardId; }
            set { _XZX_IsOnelyReaderCardId = value; }
        }
        private bool _Hook_isCardId = false;
        /// <summary>
        /// 是否读取物理卡号
        /// </summary>
        public bool Hook_isCardNo
        {
            get { return _Hook_isCardId; }
            set { _Hook_isCardId = value; }
        }
    }
}
