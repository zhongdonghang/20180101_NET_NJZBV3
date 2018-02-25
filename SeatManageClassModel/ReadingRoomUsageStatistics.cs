using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SeatManage.ClassModel
{
    [Serializable]
    public class ReadingRoomUsageStatistics
    {
        private ReadingRoomInfo _RoomInfo = new ReadingRoomInfo();
        /// <summary>
        /// 阅览室信息
        /// </summary>
        public ReadingRoomInfo RoomInfo
        {
            get { return _RoomInfo; }
            set { _RoomInfo = value; }
        }

        private DateTime _StartDate = DateTime.Parse("2001-1-1");
        /// <summary>
        /// 开始统计的日期
        /// </summary>
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        private DateTime _EndDate = DateTime.Now.Date;
        /// <summary>
        /// 结束统计的日期
        /// </summary>
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        private DataTable _StatisticsData = new DataTable();
        /// <summary>
        /// 统计折线图数据
        /// </summary>
        public DataTable StatisticsData
        {
            get { return _StatisticsData; }
            set { _StatisticsData = value; }
        }
        private int _EnterCount = 0;
        /// <summary>
        /// 进馆人次
        /// </summary>
        public int EnterCount
        {
            get { return _EnterCount; }
            set { _EnterCount = value; }
        }
        private int _OutCount = 0;
        /// <summary>
        /// 出馆人次
        /// </summary>
        public int OutCount
        {
            get { return _OutCount; }
            set { _OutCount = value; }
        }
        private int _EnterOurCount = 0;
        /// <summary>
        /// 进出人次
        /// </summary>
        public int EnterOurCount
        {
            get { return _EnterOurCount; }
            set { _EnterOurCount = value; }
        }
        private float _Attendance = 0;
        /// <summary>
        /// 上座率
        /// </summary>
        public float Attendance
        {
            get { return _Attendance; }
            set { _Attendance = value; }
        }
        private DataTable _SeatTime = new DataTable();
        /// <summary>
        /// 在座时长
        /// </summary>
        public DataTable SeatTime
        {
            get { return _SeatTime; }
            set { _SeatTime = value; }
        }
        private DataTable _SeatSelect = new DataTable();
        /// <summary>
        /// 在座时长
        /// </summary>
        public DataTable SeatSelect
        {
            get { return _SeatSelect; }
            set { _SeatSelect = value; }
        }
        private DataTable _SeatLeave = new DataTable();
        /// <summary>
        /// 在座时长
        /// </summary>
        public DataTable SeatLeave
        {
            get { return _SeatLeave; }
            set { _SeatLeave = value; }
        }

    }
    /// <summary>
    /// 统计类型
    /// </summary>
    public enum StatisticsType
    {
        /// <summary>
        /// 空值
        /// </summary>
        None = -1,
        /// <summary>
        /// 统计一周七天
        /// </summary>
        DayOfWeek = 7,
        /// <summary>
        /// 统计一个月31天
        /// </summary>
        DayOfMonth = 31,
        /// <summary>
        /// 统计一年366天
        /// </summary>
        DayOfYear = 366,
        /// <summary>
        /// 统计一年53个星期
        /// </summary>
        WeekOfYear = 53,
        /// <summary>
        /// 统计一年12个月
        /// </summary>
        MonthOfYear = 12,
    }
}
