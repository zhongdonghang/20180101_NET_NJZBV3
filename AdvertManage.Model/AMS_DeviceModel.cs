using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvertManage.Model
{
    /// <summary>
    /// 设备
    /// </summary>
    [Serializable]
    public class AMS_DeviceModel
    {
        #region Model
        private int _id;
        private string _number;
        private int _campusid;
        private bool? _isdel;
        private bool? _flag;
        private string _describe;
        private string _caputrepath;
        private DateTime? _caputretime;
        private int _schoolid;
        private string _schoolnumber;
        private string _schooname;
        private string _campusname;
        private string _campusnumber;
        /// <summary>
        /// 设备Id
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string Number
        {
            set { _number = value; }
            get { return _number; }
        }
        /// <summary>
        /// 所在校区ID
        /// </summary>
        public int CampusId
        {
            set { _campusid = value; }
            get { return _campusid; }
        }
        /// <summary>
        /// 标识是否删除
        /// </summary>
        public bool? IsDel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }
        /// <summary>
        /// 标识是否需要获取操作
        /// </summary>
        public bool? Flag
        {
            set { _flag = value; }
            get { return _flag; }
        }
        /// <summary>
        /// 设备描述
        /// </summary>
        public string Describe
        {
            set { _describe = value; }
            get { return _describe; }
        }
        /// <summary>
        /// 设备截图文件路径/(文件名)
        /// </summary>
        public string CaputrePath
        {
            set { _caputrepath = value; }
            get { return _caputrepath; }
        }
        /// <summary>
        /// 截图上传时间
        /// </summary>
        public DateTime? CaputreTime
        {
            set { _caputretime = value; }
            get { return _caputretime; }
        }
        /// <summary>
        /// 设备所在学校Id
        /// </summary>
        public int SchoolId
        {
            set { _schoolid = value; }
            get { return _schoolid; }
        }
        /// <summary>
        /// 设备所在学校的编号
        /// </summary>
        public string SchoolNumber
        {
            set { _schoolnumber = value; }
            get { return _schoolnumber; }
        }
        /// <summary>
        /// 设备所在学校的名称
        /// </summary>
        public string SchooName
        {
            set { _schooname = value; }
            get { return _schooname; }
        }
        /// <summary>
        /// 设备所在校区的名称
        /// </summary>
        public string CampusName
        {
            set { _campusname = value; }
            get { return _campusname; }
        }
        /// <summary>
        /// 设备所在校区的编号
        /// </summary>
        public string CampusNumber
        {
            set { _campusnumber = value; }
            get { return _campusnumber; }
        }
        #endregion Model
    }
}
