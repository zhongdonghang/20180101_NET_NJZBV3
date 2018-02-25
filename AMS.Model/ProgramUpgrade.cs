using System;
namespace AMS.Model
{
    /// <summary>
    /// ProgramUpgrade:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ProgramUpgrade
    {
        public ProgramUpgrade()
        { }
        #region Model
        private int _id;
        private int _application;
        private string _autoupdaterxml;
        private string _updatelog;
        private DateTime _releasedate;
        private string _version;
        private string _remark;
        /// <summary>
        /// 程序更新ID
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 当前应用程序id
        /// </summary>
        public int Application
        {
            set { _application = value; }
            get { return _application; }
        }
        /// <summary>
        /// 以xml格式存储的更新信息
        /// </summary>
        public string AutoUpdaterXml
        {
            set { _autoupdaterxml = value; }
            get { return _autoupdaterxml; }
        }
        /// <summary>
        /// 更新日志
        /// </summary>
        public string UpdateLog
        {
            set { _updatelog = value; }
            get { return _updatelog; }
        }
        /// <summary>
        /// 新版本发布日期
        /// </summary>
        public DateTime ReleaseDate
        {
            set { _releasedate = value; }
            get { return _releasedate; }
        }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version
        {
            set { _version = value; }
            get { return _version; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

    }
}

