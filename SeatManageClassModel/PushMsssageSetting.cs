using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SeatManage.EnumType;

namespace SeatManage.ClassModel
{
    public class PushMsssageSetting
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PushMsssageSetting()
        {
            foreach (MsgPushType type in Enum.GetValues(typeof(MsgPushType)))
            {
                _pushSetting.Add(type, false);
            }
        }
        /// <summary>
        /// xml构造函数
        /// </summary>
        /// <param name="xml"></param>
        public PushMsssageSetting(string xml)
        {
            foreach (MsgPushType type in Enum.GetValues(typeof(MsgPushType)))
            {
                _pushSetting.Add(type, false);
            }
            this.PushSetting = ToModel(xml).PushSetting;
        }

        private Dictionary<EnumType.MsgPushType, bool> _pushSetting = new Dictionary<EnumType.MsgPushType, bool>();

        /// <summary>
        /// 启用的推送；类型
        /// </summary>
        public Dictionary<MsgPushType, bool> PushSetting
        {
            get { return _pushSetting; }
            set { _pushSetting = value; }
        }

        /// <summary>
        /// 转换成XMl
        /// </summary>
        /// <returns></returns>
        public string ToXml()
        {
            return ToXml(this);
        }

        /// <summary>
        /// 转换成XML
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string ToXml(PushMsssageSetting model)
        {
            //TODO:转换成xml结构的算法
            //创建一个xml对象
            XmlDocument xmlDoc = new XmlDocument();
            //创建开头
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(dec);
            //创建根节点
            XmlElement root = xmlDoc.CreateElement("Root");
            XmlElement typeNode = xmlDoc.CreateElement("MsgTypes");
            foreach (var v in model.PushSetting)
            {
                XmlElement FirNode;
                FirNode = xmlDoc.CreateElement("Type");
                FirNode.SetAttribute("TypeName", v.Key.ToString());
                FirNode.SetAttribute("IsUsed", v.Value.ToString());
                typeNode.AppendChild(FirNode);
            }
            root.AppendChild(typeNode);
            //添加根节点
            xmlDoc.AppendChild(root);
            return xmlDoc.OuterXml;
        }
        /// <summary>
        /// 转换为Model
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static PushMsssageSetting ToModel(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            XmlDocument xmlDoc = new XmlDocument();
            PushMsssageSetting model = new PushMsssageSetting();
            try
            {
                xmlDoc.LoadXml(xml);
                //查找根节点
                XmlNodeList nodes = xmlDoc.SelectNodes("//Root/MsgTypes/Type");
                foreach (XmlNode itemNode in nodes)
                {
                    model.PushSetting[(MsgPushType)Enum.Parse(typeof(MsgPushType), itemNode.Attributes["TypeName"].Value)] = bool.Parse(itemNode.Attributes["IsUsed"].Value);
                }
                return model;
            }
            catch
            {
                return null;
            }
        }
    }
}
