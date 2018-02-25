using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    /// <summary>
    /// 进出记录统计
    /// </summary>
    [Serializable]
    public class EnterOutLogStatistics
    {
        private int _ID;
        /// <summary>
        /// 记录ID
        /// </summary>
        public int ID
        {

            get { return _ID; }
            set { _ID = value; }
        }
        private string _EnterOutLogNo;
        /// <summary>
        /// 进出记录编号
        /// </summary>
        public string EnterOutLogNo
        {
            get { return _EnterOutLogNo; }
            set { _EnterOutLogNo = value; }
        }
        private int _LastEnterOutLogID;
        /// <summary>
        /// 最后的记录ID
        /// </summary>
        public int LastEnterOutLogID
        {
            get { return _LastEnterOutLogID; }
            set { _LastEnterOutLogID = value; }
        }
        private string _CardNo;
        /// <summary>
        /// 读者学号
        /// </summary>
        public string CardNo
        {
            get { return _CardNo; }
            set { _CardNo = value; }
        }
        private string _ReaderName;
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
        private string _SeatNo;
        /// <summary>
        /// 座位号
        /// </summary>
        public string SeatNo
        {
            get { return _SeatNo; }
            set { _SeatNo = value; }
        }
        private string _ReadingRoomNo;
        /// <summary>
        /// 阅览室编号
        /// </summary>
        public string ReadingRoomNo
        {
            get { return _ReadingRoomNo; }
            set { _ReadingRoomNo = value; }
        }
        private string _ReadingRoomName;
        /// <summary>
        /// 阅览室名称
        /// </summary>
        public string ReadingRoomName
        {
            get { return _ReadingRoomName; }
            set { _ReadingRoomName = value; }
        }
        private string _LibraryNo;
        /// <summary>
        /// 图书馆编号
        /// </summary>
        public string LibraryNo
        {
            get { return _LibraryNo; }
            set { _LibraryNo = value; }
        }
        private string _LibraryName;
        /// <summary>
        /// 图书馆名称
        /// </summary>
        public string LibraryName
        {
            get { return _LibraryName; }
            set { _LibraryName = value; }
        }
        private string _SchoolNo;
        /// <summary>
        /// 学校编号
        /// </summary>
        public string SchoolNo
        {
            get { return _SchoolNo; }
            set { _SchoolNo = value; }
        }
        private string _SchoolName;
        /// <summary>
        /// 校区名称
        /// </summary>
        public string SchoolName
        {
            get { return _SchoolName; }
            set { _SchoolName = value; }
        }
        private EnterOutLogSelectSeatMode _SelectSeat;
        /// <summary>
        /// 选座方式
        /// </summary>
        public EnterOutLogSelectSeatMode SelectSeat
        {
            get { return _SelectSeat; }
            set { _SelectSeat = value; }
        }
        private EnterOutLogLeaveSeatMode _LeaveSeat;
        /// <summary>
        /// 离开方式
        /// </summary>
        public EnterOutLogLeaveSeatMode LeaveSeat
        {
            get { return _LeaveSeat; }
            set { _LeaveSeat = value; }
        }
        private DateTime _SelectSeatTime;
        /// <summary>
        /// 选座时间
        /// </summary>
        public DateTime SelectSeatTime
        {
            get { return _SelectSeatTime; }
            set { _SelectSeatTime = value; }
        }
        private DateTime _LeaveSeatTime;
        /// <summary>
        /// 释放座位时间
        /// </summary>
        public DateTime LeaveSeatTime
        {
            get { return _LeaveSeatTime; }
            set { _LeaveSeatTime = value; }
        }
        private int _SeatTime = 0;
        /// <summary>
        /// 在座时长
        /// </summary>
        public int SeatTime
        {
            get { return _SeatTime; }
            set { _SeatTime = value; }
        }
        private int _ShortLeaveCount = 0;
        /// <summary>
        /// 暂离次数
        /// </summary>
        public int ShortLeaveCount
        {
            get { return _ShortLeaveCount; }
            set { _ShortLeaveCount = value; }
        }
        private int _ContinueTimeCount = 0;
        /// <summary>
        /// 续时次数
        /// </summary>
        public int ContinueTimeCount
        {
            get { return _ContinueTimeCount; }
            set { _ContinueTimeCount = value; }
        }
        private bool _IsViolation = false;
        /// <summary>
        /// 是否违规
        /// </summary>
        public bool IsViolation
        {
            get { return _IsViolation; }
            set { _IsViolation = value; }
        }
        private int _AllOperationCount = 0;
        /// <summary>
        /// 操作次数
        /// </summary>
        public int AllOperationCount
        {
            get { return _AllOperationCount; }
            set { _AllOperationCount = value; }
        }
        private int _AdminOperationCount = 0;
        /// <summary>
        /// 管理员操作次数
        /// </summary>
        public int AdminOperationCount
        {
            get { return _AdminOperationCount; }
            set { _AdminOperationCount = value; }
        }
        private int _ReaderOperationCount = 0;
        /// <summary>
        /// 读者操作次数
        /// </summary>
        public int ReaderOperationCount
        {
            get { return _ReaderOperationCount; }
            set { _ReaderOperationCount = value; }
        }
        private int _OtherOperationCount = 0;
        /// <summary>
        /// 被其他读者操作次数
        /// </summary>
        public int OtherOperationCount
        {
            get { return _OtherOperationCount; }
            set { _OtherOperationCount = value; }
        }
        private int _ServerOperationCount = 0;
        /// <summary>
        /// 被监控服务操作次数
        /// </summary>
        public int ServerOperationCount
        {
            get { return _ServerOperationCount; }
            set { _ServerOperationCount = value; }
        }
    }
    /// <summary>
    /// 选座模式
    /// </summary>
    public enum EnterOutLogSelectSeatMode
    {
        /// <summary>
        /// 空值
        /// </summary>
        None = -1,
        /// <summary>
        /// 读者刷卡选座
        /// </summary>
        ReadCardSelect = 0,
        /// <summary>
        /// 重新选座
        /// </summary>
        ReSelect = 1,
        /// <summary>
        /// 管理员分配座位
        /// </summary>
        AdminAllocation = 2,
        /// <summary>
        /// 预约分配座位
        /// </summary>
        BookAdmission = 3,
        /// <summary>
        /// 等待进入座位
        /// </summary>
        WaitAdmission = 4,

    }
    /// <summary>
    /// 离开模式
    /// </summary>
    public enum EnterOutLogLeaveSeatMode
    {
        /// <summary>
        /// 空值
        /// </summary>
        None = -1,
        /// <summary>
        /// 读者直接刷卡离开
        /// </summary>
        ReaderReleased = 0,
        /// <summary>
        /// 管理员释放座位
        /// </summary>
        AdminReleased = 1,
        /// <summary>
        /// 监控服务释放座位
        /// </summary>
        ServerReleased = 2,
    }
}
