using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;

namespace SeatManage.ClassModel
{
    /// <summary>
    /// 违规记录
    /// </summary>
    [Serializable]
    public class ViolationRecordsLogInfo
    {
        string _ID = "";
        /// <summary>
        /// 违规记录编号
        /// </summary>
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        string _CardNo = "";
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo
        {
            get { return _CardNo; }
            set { _CardNo = value; }
        }
        private string _ReaderName = "";
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
        string _SeatID = "";
        /// <summary>
        /// 座位编号
        /// </summary>
        public string SeatID
        {
            get { return _SeatID; }
            set { _SeatID = value; }
        }
        string _ReadingRoomID = "";
        /// <summary>
        /// 阅览室ID
        /// </summary>
        public string ReadingRoomID
        {
            get { return _ReadingRoomID; }
            set { _ReadingRoomID = value; }
        }
        string _ReadingRoomName = "";
        /// <summary>
        /// 阅览室名称
        /// </summary>
        public string ReadingRoomName
        {
            get { return _ReadingRoomName; }
            set { _ReadingRoomName = value; }
        }
        ViolationRecordsType _EnterFlag;
        /// <summary>
        /// 标识
        /// </summary>
        public ViolationRecordsType EnterFlag
        {
            get { return _EnterFlag; }
            set { _EnterFlag = value; }
        }
        string _EnterOutTime = "";
        /// <summary>
        /// 违规时间
        /// </summary>
        public string EnterOutTime
        {
            get { return _EnterOutTime; }
            set { _EnterOutTime = value; }
        }
        string _ViolateType = "";
        /// <summary>
        /// 标注
        /// </summary>
        public string Remark
        {
            get { return _ViolateType; }
            set { _ViolateType = value; }
        }
        string _BlacklistID = "-1";
        /// <summary>
        /// 黑名单Id，如果没被加入黑名单，Id为-1。
        /// </summary>
        public string BlacklistID
        {
            get { return _BlacklistID; }
            set { _BlacklistID = value; }
        }
        string _WarningState = "";
        /// <summary>
        /// 提醒状态
        /// </summary>
        public string WarningState
        {
            get { return _WarningState; }
            set { _WarningState = value; }
        }
        LogStatus _Flag = LogStatus.Valid;
        /// <summary>
        /// 标示此条记录是否有效
        /// </summary>
        public LogStatus Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
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
