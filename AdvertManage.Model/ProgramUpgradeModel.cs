using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvertManage.Model
{
    /// <summary>
    /// 程序更新Model
    /// </summary>
    [Serializable]
    public class ProgramUpgradeModel
    {
        #region Model
        private int _id;
        private Enum.SeatManageSubsystem _application;
        private string _autoupdaterxml;
        private string _updatelog;
        private DateTime _releasedate;
        private string _version;
      
        /// <summary>
        /// 程序更新的Id
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 应用程序类型，为枚举结构
        /// </summary>
        public Enum.SeatManageSubsystem Application
        {
            set { _application = value; }
            get { return _application; }
        }
        /// <summary>
        /// 应用程序需要升级的文件（Xml结构）
        /// </summary>
        public string AutoUpdaterXml
        {
            set { _autoupdaterxml = value; }
            get { return _autoupdaterxml; }
        }
        /// <summary>
        /// 升级日志
        /// </summary>
        public string UpdateLog
        {
            set { _updatelog = value; }
            get { return _updatelog; }
        }
        /// <summary>
        /// 发布时间
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
        #endregion Model
    }
}
