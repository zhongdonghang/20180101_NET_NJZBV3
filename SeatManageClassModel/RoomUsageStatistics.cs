/**  版本信息模板在安装目录下，可自行修改。
* T_SM_RoomUsageStatistics.cs
*
* 功 能： N/A
* 类 名： T_SM_RoomUsageStatistics
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/7/29 13:18:09   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace SeatManage.ClassModel
{
	/// <summary>
	/// T_SM_RoomUsageStatistics:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
    public partial class RoomUsageStatistics
    {
        public RoomUsageStatistics()
        { }
        #region Model
        private int _id;
        private string _readingroomno;
        private DateTime _statisticsdate;
        private DateTime _opentime;
        private DateTime _closetime;
        private int _roomusagetime;
        private int _seatallcount;
        private int _seatusagecount;
        private int _seatusagetime;
        private int _readerusagecount;
        private int _usedreadercount;
        private int _rushcardoperatingcount;
        private int _selectseatcount;
        private int _selectseatbyadmin;
        private int _selectseatbyreader;
        private int _selectseatinseatclient;
        private int _selectseatinotherclient;
        private int _reselectseatcount;
        private int _reselectseatinseatclient;
        private int _reselectseatinotherclient;
        private int _checkbespeakcount;
        private int _checkbespeakinseatclient;
        private int _ckeckbespeakinotherclient;
        private int _waitseatcount;
        private int _shortleavecount;
        private int _shortleavetime;
        private int _shortleavebyadmin;
        private int _shortleavebyreader;
        private int _shortleavebyotherreader;
        private int _shortleavebyservice;
        private int _shortleaveinseatclient;
        private int _shortleaveinotherclient;
        private int _leavecount;
        private int _leavebyadmin;
        private int _leavebyreader;
        private int _leavebyservice;
        private int _leaveinseatclient;
        private int _leaveinotherclient;
        private int _comebackcount;
        private int _comebackbyadmin;
        private int _comebackbyreader;
        private int _comebackbyotherreader;
        private int _comebackinseatclient;
        private int _comebackinotherclient;
        private int _continuetimecount;
        private int _continuetimebyreader;
        private int _continuetimebyservice;
        private int _continuetimeinseatclient;
        private int _continuetimeinotherclient;
        private int _allbespeakcount;
        private int _bespeakcount;
        private int _canbesapeakseat;
        private int _bespeakedseat;
        private int _bespeakcancel;
        private int _bespeakovertime;
        private int _bespeakcheck;
        private int _nowdaybespeakcheck;
        private int _nowdaybespeakcount;
        private int _nowdaybespeakovertime;
        private int _nowdaybespeakcancel;
        private int _violationrecordcount;
        private int _vrbookingtimeout;
        private int _vrseatouttime;
        private int _vrleavebyadmin;
        private int _vrshortleaveouttime;
        private int _vrshortleavebyadminouttime;
        private int _vrshortleavebyreaderouttime;
        private int _vrshortleavebyserviceouttime;
        private int _vrleavenotreadcard;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReadingRoomNo
        {
            set { _readingroomno = value; }
            get { return _readingroomno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime StatisticsDate
        {
            set { _statisticsdate = value; }
            get { return _statisticsdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime OpenTime
        {
            set { _opentime = value; }
            get { return _opentime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CloseTime
        {
            set { _closetime = value; }
            get { return _closetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int RoomUsageTime
        {
            set { _roomusagetime = value; }
            get { return _roomusagetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SeatAllCount
        {
            set { _seatallcount = value; }
            get { return _seatallcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SeatUsageCount
        {
            set { _seatusagecount = value; }
            get { return _seatusagecount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SeatUsageTime
        {
            set { _seatusagetime = value; }
            get { return _seatusagetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ReaderUsageCount
        {
            set { _readerusagecount = value; }
            get { return _readerusagecount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int UsedReaderCount
        {
            set { _usedreadercount = value; }
            get { return _usedreadercount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int RushCardOperatingCount
        {
            set { _rushcardoperatingcount = value; }
            get { return _rushcardoperatingcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SelectSeatCount
        {
            set { _selectseatcount = value; }
            get { return _selectseatcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SelectSeatByAdmin
        {
            set { _selectseatbyadmin = value; }
            get { return _selectseatbyadmin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SelectSeatByReader
        {
            set { _selectseatbyreader = value; }
            get { return _selectseatbyreader; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SelectSeatInSeatClient
        {
            set { _selectseatinseatclient = value; }
            get { return _selectseatinseatclient; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SelectSeatInOtherClient
        {
            set { _selectseatinotherclient = value; }
            get { return _selectseatinotherclient; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ReselectSeatCount
        {
            set { _reselectseatcount = value; }
            get { return _reselectseatcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ReselectSeatInSeatClient
        {
            set { _reselectseatinseatclient = value; }
            get { return _reselectseatinseatclient; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ReselectSeatInOtherClient
        {
            set { _reselectseatinotherclient = value; }
            get { return _reselectseatinotherclient; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CheckBespeakCount
        {
            set { _checkbespeakcount = value; }
            get { return _checkbespeakcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CheckBespeakInSeatClient
        {
            set { _checkbespeakinseatclient = value; }
            get { return _checkbespeakinseatclient; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CkeckBespeakInOtherClient
        {
            set { _ckeckbespeakinotherclient = value; }
            get { return _ckeckbespeakinotherclient; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int WaitSeatCount
        {
            set { _waitseatcount = value; }
            get { return _waitseatcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ShortLeaveCount
        {
            set { _shortleavecount = value; }
            get { return _shortleavecount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ShortLeaveTime
        {
            set { _shortleavetime = value; }
            get { return _shortleavetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ShortLeaveByAdmin
        {
            set { _shortleavebyadmin = value; }
            get { return _shortleavebyadmin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ShortLeaveByReader
        {
            set { _shortleavebyreader = value; }
            get { return _shortleavebyreader; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ShortLeaveByOtherReader
        {
            set { _shortleavebyotherreader = value; }
            get { return _shortleavebyotherreader; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ShortLeaveByService
        {
            set { _shortleavebyservice = value; }
            get { return _shortleavebyservice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ShortLeaveInSeatClient
        {
            set { _shortleaveinseatclient = value; }
            get { return _shortleaveinseatclient; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ShortLeaveInOtherClient
        {
            set { _shortleaveinotherclient = value; }
            get { return _shortleaveinotherclient; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LeaveCount
        {
            set { _leavecount = value; }
            get { return _leavecount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LeaveByAdmin
        {
            set { _leavebyadmin = value; }
            get { return _leavebyadmin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LeaveByReader
        {
            set { _leavebyreader = value; }
            get { return _leavebyreader; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LeaveByService
        {
            set { _leavebyservice = value; }
            get { return _leavebyservice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LeaveInSeatClient
        {
            set { _leaveinseatclient = value; }
            get { return _leaveinseatclient; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LeaveInOtherClient
        {
            set { _leaveinotherclient = value; }
            get { return _leaveinotherclient; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ComeBackCount
        {
            set { _comebackcount = value; }
            get { return _comebackcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ComeBackByAdmin
        {
            set { _comebackbyadmin = value; }
            get { return _comebackbyadmin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ComeBackByReader
        {
            set { _comebackbyreader = value; }
            get { return _comebackbyreader; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ComeBackByOtherReader
        {
            set { _comebackbyotherreader = value; }
            get { return _comebackbyotherreader; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ComeBackInSeatClient
        {
            set { _comebackinseatclient = value; }
            get { return _comebackinseatclient; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ComeBackInOtherClient
        {
            set { _comebackinotherclient = value; }
            get { return _comebackinotherclient; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ContinueTimeCount
        {
            set { _continuetimecount = value; }
            get { return _continuetimecount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ContinueTimeByReader
        {
            set { _continuetimebyreader = value; }
            get { return _continuetimebyreader; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ContinueTimeByService
        {
            set { _continuetimebyservice = value; }
            get { return _continuetimebyservice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ContinueTimeInSeatClient
        {
            set { _continuetimeinseatclient = value; }
            get { return _continuetimeinseatclient; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ContinueTimeInOtherClient
        {
            set { _continuetimeinotherclient = value; }
            get { return _continuetimeinotherclient; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int AllBespeakCount
        {
            set { _allbespeakcount = value; }
            get { return _allbespeakcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int BespeakCount
        {
            set { _bespeakcount = value; }
            get { return _bespeakcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CanBesapeakSeat
        {
            set { _canbesapeakseat = value; }
            get { return _canbesapeakseat; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int BespeakedSeat
        {
            set { _bespeakedseat = value; }
            get { return _bespeakedseat; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int BespeakCancel
        {
            set { _bespeakcancel = value; }
            get { return _bespeakcancel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int BespeakOverTime
        {
            set { _bespeakovertime = value; }
            get { return _bespeakovertime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int BespeakCheck
        {
            set { _bespeakcheck = value; }
            get { return _bespeakcheck; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int NowDayBespeakCheck
        {
            set { _nowdaybespeakcheck = value; }
            get { return _nowdaybespeakcheck; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int NowDayBespeakCount
        {
            set { _nowdaybespeakcount = value; }
            get { return _nowdaybespeakcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int NowDayBespeakOverTime
        {
            set { _nowdaybespeakovertime = value; }
            get { return _nowdaybespeakovertime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int NowDayBespeakCancel
        {
            set { _nowdaybespeakcancel = value; }
            get { return _nowdaybespeakcancel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ViolationRecordCount
        {
            set { _violationrecordcount = value; }
            get { return _violationrecordcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int VRBookingTimeOut
        {
            set { _vrbookingtimeout = value; }
            get { return _vrbookingtimeout; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int VRSeatOutTime
        {
            set { _vrseatouttime = value; }
            get { return _vrseatouttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int VRLeaveByAdmin
        {
            set { _vrleavebyadmin = value; }
            get { return _vrleavebyadmin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int VRShortLeaveOutTime
        {
            set { _vrshortleaveouttime = value; }
            get { return _vrshortleaveouttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int VRShortLeaveByAdminOutTime
        {
            set { _vrshortleavebyadminouttime = value; }
            get { return _vrshortleavebyadminouttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int VRShortLeaveByReaderOutTime
        {
            set { _vrshortleavebyreaderouttime = value; }
            get { return _vrshortleavebyreaderouttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int VRShortLeaveByServiceOutTime
        {
            set { _vrshortleavebyserviceouttime = value; }
            get { return _vrshortleavebyserviceouttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int VRLeaveNotReadCard
        {
            set { _vrleavenotreadcard = value; }
            get { return _vrleavenotreadcard; }
        }
        #endregion Model

    }
}

