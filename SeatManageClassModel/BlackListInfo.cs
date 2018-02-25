using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;

namespace SeatManage.ClassModel
{
    /// <summary>
    /// 黑名单消息
    /// </summary>
    [Serializable]
    public class BlackListInfo
    {
        string _ID = "";
        /// <summary>
        /// 黑名单序号
        /// </summary>
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        string _CardNo = "";
        /// <summary>
        /// 读者学号
        /// </summary>
        public string CardNo
        {
            get { return _CardNo; }
            set { _CardNo = value; }
        }
        string _ReaderName = "";
        /// <summary>
        /// 读者姓名
        /// </summary>
        public string ReaderName
        {
            get { return _ReaderName; }
            set { _ReaderName = value; }
        }
        private string _TypeName;
        /// <summary>
        /// 读者类型
        /// </summary>
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }
        private string _DeptName;
        /// <summary>
        /// 读者院系
        /// </summary>
        public string DeptName
        {
            get { return _DeptName; }
            set { _DeptName = value; }
        }
        string _ReadingRoomID = "";
        /// <summary>
        /// 黑名单记录所在阅览室ID
        /// </summary>
        public string ReadingRoomID
        {
            get { return _ReadingRoomID; }
            set { _ReadingRoomID = value; }
        }
        string _ReadingRoomName = "";
        /// <summary>
        /// 黑名单记录所在阅览室名称
        /// </summary>
        public string ReadingRoomName
        {
            get { return _ReadingRoomName; }
            set { _ReadingRoomName = value; }
        }
        DateTime _AddTime = DateTime.Parse("1900-1-1");
        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime AddTime
        {
            get { return _AddTime; }
            set { _AddTime = value; }
        }
        DateTime _OutTime = DateTime.Parse("1900-1-1");
        /// <summary>
        /// 离开时间
        /// </summary>
        public DateTime OutTime
        {
            get { return _OutTime; }
            set { _OutTime = value; }
        }
        string _ReMark = "";
        /// <summary>
        /// 备注
        /// </summary>
        public string ReMark
        {
            get { return _ReMark; }
            set { _ReMark = value; }
        }
        LogStatus _BlacklistState = LogStatus.Valid;
        /// <summary>
        /// 记录状态
        /// </summary>
        public LogStatus BlacklistState
        {
            get { return _BlacklistState; }
            set { _BlacklistState = value; }
        }
        LeaveBlacklistMode _OutBlacklistMode = LeaveBlacklistMode.AutomaticMode;
        /// <summary>
        /// 离开黑名单方式
        /// </summary>
        public LeaveBlacklistMode OutBlacklistMode
        {
            get { return _OutBlacklistMode; }
            set { _OutBlacklistMode = value; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get { return _Sex; }
            set { _Sex = value; }
        }

        private string _Sex;
    }
}
