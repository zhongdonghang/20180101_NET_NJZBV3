using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AMS.Model
{
    [Serializable]
    public class AMS_Advertisement
    {

        #region 属性
        private int _ID = 0;
        /// <summary>
        /// 自增长ID
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _Num = "";
        /// <summary>
        /// 广告编号
        /// </summary>
        public string Num
        {
            get { return _Num; }
            set { _Num = value; }
        }
        private string _Name = "";
        /// <summary>
        /// 广告名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private DateTime _ReleaseDate = new DateTime();
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime ReleaseDate
        {
            get { return _ReleaseDate; }
            set { _ReleaseDate = value; }
        }

        private DateTime _EffectDate = new DateTime();
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EffectDate
        {
            get { return _EffectDate; }
            set { _EffectDate = value; }
        }
        private DateTime _EndDate = new DateTime();
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        private int _OperatorID = -1;
        /// <summary>
        /// 用户ID
        /// </summary>
        public int OperatorID
        {
            get { return _OperatorID; }
            set { _OperatorID = value; }
        }
        private string _OperatorName = "";
        /// <summary>
        /// 操作者
        /// </summary>
        public string OperatorName
        {
            get { return _OperatorName; }
            set { _OperatorName = value; }
        }
        private int _CustomerID = -1;
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }

        private Enum.AdType _Type = Enum.AdType.None;
        /// <summary>
        /// 广告类型
        /// </summary>
        public Enum.AdType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private string _AdContent;
        /// <summary>
        /// 广告内容（XML）
        /// </summary>
        public string AdContent
        {
            get { return _AdContent; }
            set { _AdContent = value; }
        }
        private List<string> _ImageFilePath = new List<string>();
        /// <summary>
        /// 图片资源下载地址
        /// </summary>
        public List<string> ImageFilePath
        {
            get { return _ImageFilePath; }
            set { _ImageFilePath = value; }
        }
        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static List<string> GetDownloadFile(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return new List<string>();
            }
            XmlDocument xmlDoc = new XmlDocument();
            List<string> fileList = new List<string>();
            try
            {
                xmlDoc.LoadXml(xml);
                XmlNodeList nodes = xmlDoc.SelectNodes("//Root/ImageFilePath/FilePath");
                foreach (XmlNode ItemNode in nodes)
                {
                    fileList.Add(ItemNode.Attributes["Url"].Value);
                }
                return fileList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
