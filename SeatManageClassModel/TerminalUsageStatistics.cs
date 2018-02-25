/**  版本信息模板在安装目录下，可自行修改。
* T_SM_TerminalUsageStatistics.cs
*
* 功 能： N/A
* 类 名： T_SM_TerminalUsageStatistics
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
	/// T_SM_TerminalUsageStatistics:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TerminalUsageStatistics
	{
		public TerminalUsageStatistics()
		{}
        #region Model
        private int _id;
        private string _terminalno;
        private DateTime _statisticsdate;
        private int _rushcardcount;
        private int _todayprintcount;
        private int _ischangepage;
        private int _nowpageprintcount;
        private int _beforepageprintcount;
        private int _selectseatcount;
        private int _reselectseatcount;
        private int _checkbespeakcount;
        private int _waitseatcount;
        private int _shortleavecount;
        private int _comebackcount;
        private int _continuetimecount;
        private int _leavecount;
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
        public string TerminalNo
        {
            set { _terminalno = value; }
            get { return _terminalno; }
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
        public int RushCardCount
        {
            set { _rushcardcount = value; }
            get { return _rushcardcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TodayPrintCount
        {
            set { _todayprintcount = value; }
            get { return _todayprintcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsChangePage
        {
            set { _ischangepage = value; }
            get { return _ischangepage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int NowPagePrintCount
        {
            set { _nowpageprintcount = value; }
            get { return _nowpageprintcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int BeforePagePrintCount
        {
            set { _beforepageprintcount = value; }
            get { return _beforepageprintcount; }
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
        public int ReselectSeatCount
        {
            set { _reselectseatcount = value; }
            get { return _reselectseatcount; }
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
        public int ComeBackCount
        {
            set { _comebackcount = value; }
            get { return _comebackcount; }
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
        public int LeaveCount
        {
            set { _leavecount = value; }
            get { return _leavecount; }
        }
        #endregion Model

	}
}

