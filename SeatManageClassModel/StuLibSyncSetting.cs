using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SeatManage.EnumType;

namespace SeatManage.ClassModel
{
    /// <summary>
    /// 读者库同步配置
    /// </summary>
    [Serializable]
    public class StuLibSyncSetting
    {

        /// <summary>
        /// 同步设置
        /// </summary> 
        public StuLibSyncSetting()
        {
        }

        public override string ToString()
        {
            return Convert(this);
        }

        public static string Convert(StuLibSyncSetting set)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);

            XmlElement rootElement = doc.CreateElement("rootNote");
            XmlElement children1 = doc.CreateElement("sourceConnectionString");
            children1.SetAttribute("servierIP", set.SouIP);
            children1.SetAttribute("dBName", set.SouDBName);
            children1.SetAttribute("password", set.SouPW);
            children1.SetAttribute("uerName", set.SouUserName);
            rootElement.AppendChild(children1);
            //目标读者库连接字符串
            //XmlElement children2 = doc.CreateElement("targetConnectionString");
            //children2.SetAttribute("servierIP", set.TarIP);
            //children2.SetAttribute("dBName", set.TarDBName);
            //children2.SetAttribute("password", set.TarPW);
            //children2.SetAttribute("uerName", set.TarUserName);
            //rootElement.AppendChild(children2);

            XmlElement children3 = doc.CreateElement("syncModel");
            children3.SetAttribute("syncModel", ((int)set.SyncMode).ToString());
            children3.SetAttribute("syncTime", set.SyncTime);
            rootElement.AppendChild(children3);

            //XmlElement children4 = doc.CreateElement("showMessage");
            //children4.SetAttribute("rrID", set.RollNowsRRid);
            //children4.InnerText = set.RollNows;
            //rootElement.AppendChild(children4);
            doc.AppendChild(rootElement);
            return doc.OuterXml;

        }

        public static StuLibSyncSetting Convert(string strSet)
        {
            StuLibSyncSetting set = new StuLibSyncSetting();
            if (!string.IsNullOrEmpty(strSet))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(strSet);
                //源读者库连接字符串
                XmlNode node = doc.SelectSingleNode("//rootNote/sourceConnectionString");
                set.SouIP = node.Attributes["servierIP"].Value;
                set.SouDBName = node.Attributes["dBName"].Value;
                set.SouPW = node.Attributes["password"].Value;
                set.SouUserName = node.Attributes["uerName"].Value;


                //目标读者库连接字符串
                //node = doc.SelectSingleNode("//rootNote/targetConnectionString");
                //set.TarIP = node.Attributes["servierIP"].Value;
                //set.TarDBName = node.Attributes["dBName"].Value;
                //set.TarPW = node.Attributes["password"].Value;
                //set.TarUserName = node.Attributes["uerName"].Value;

                node = doc.SelectSingleNode("//rootNote/syncModel");
                set.SyncMode = (StudentSyncMode)int.Parse(node.Attributes["syncModel"].Value);
                set.SyncTime = node.Attributes["syncTime"].Value;

                //node = doc.SelectSingleNode("//rootNote/showMessage");
                //set.RollNowsRRid = node.Attributes["rrID"].Value;
                //set.RollNows = node.InnerText;
            }
            return set;
        }


        ///// <summary>
        ///// 读者库同步配置的XML
        ///// </summary>
        //private XmlDocument _XMLServiceSetting = new XmlDocument();

        //public XmlDocument XMLServiceSetting
        //{
        //    get { return _XMLServiceSetting; }
        //}

        //private string _RollRRid;
        ///// <summary>
        ///// 滚动显示的阅览室
        ///// </summary>
        //public string RollNowsRRid
        //{
        //    get { return _RollRRid; }
        //    set { _RollRRid = value; }
        //}
        //private string _RollNows;
        ///// <summary>
        ///// 要显示的信息
        ///// </summary>
        //public string RollNows
        //{
        //    get { return _RollNows; }
        //    set { _RollNows = value; }
        //}


        /// <summary>
        /// 源读者库地址
        /// </summary>
        private string _SouIP = "";
        /// <summary>
        /// 源读者库地址
        /// </summary>
        public string SouIP
        {
            get { return _SouIP; }
            set
            {
                _SouIP = value;
            }
        }

        /// <summary>
        /// 源读者库登录名
        /// </summary>
        private string _SouUserName = "";
        /// <summary>
        /// 源读者库登录名
        /// </summary>
        public string SouUserName
        {
            get { return _SouUserName; }
            set
            {
                _SouUserName = value;
            }
        }

        /// <summary>
        /// 源数据库名
        /// </summary>
        private string _SouDBName = "";
        /// <summary>
        /// 源数据库名
        /// </summary>
        public string SouDBName
        {
            get { return _SouDBName; }
            set
            {
                _SouDBName = value;
            }
        }

        /// <summary>
        /// 源登录密码
        /// </summary>
        private string _SouPW = "";
        /// <summary>
        /// 源登录密码
        /// </summary>
        public string SouPW
        {
            get { return _SouPW; }
            set
            {
                _SouPW = value;
            }
        }


        ///// <summary>
        ///// 目标读者库地址
        ///// </summary>
        //private string _TarIP = "";
        ///// <summary>
        ///// 目标读者库地址
        ///// </summary>
        //public string TarIP
        //{
        //    get { return _TarIP; }
        //    set
        //    {
        //        _TarIP = value;
        //    }
        //}

        ///// <summary>
        ///// 目标读者库登录名
        ///// </summary>
        //private string _TarUserName = "";
        ///// <summary>
        ///// 目标读者库登录名
        ///// </summary>
        //public string TarUserName
        //{
        //    get { return _TarUserName; }
        //    set
        //    {
        //        _TarUserName = value;
        //    }
        //}

        ///// <summary>
        ///// 目标数据库名
        ///// </summary>
        //private string _TarDBName = "";
        ///// <summary>
        ///// 目标数据库名
        ///// </summary>
        //public string TarDBName
        //{
        //    get { return _TarDBName; }
        //    set
        //    {
        //        _TarDBName = value;
        //    }
        //}

        ///// <summary>
        ///// 目标登录密码
        ///// </summary>
        //private string _TarPW = "";
        ///// <summary>
        ///// 目标登录密码
        ///// </summary>
        //public string TarPW
        //{
        //    get { return _TarPW; }
        //    set
        //    {
        //        _TarPW = value;
        //    }
        //}


        /// <summary>
        /// 同步模式
        /// </summary>
        private StudentSyncMode _SyncMode = StudentSyncMode.None;
        /// <summary>
        /// 同步模式：1自动；0 手动
        /// </summary>
        public StudentSyncMode SyncMode
        {
            get { return _SyncMode; }
            set { _SyncMode = value; }
        }

        /// <summary>
        /// 同步时间
        /// </summary>
        private string _SyncTime = "";
        /// <summary>
        /// 同步时间
        /// </summary>
        public string SyncTime
        {
            get { return _SyncTime; }
            set { _SyncTime = value; }
        }
    }
}
