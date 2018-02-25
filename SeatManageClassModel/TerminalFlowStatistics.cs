/**  版本信息模板在安装目录下，可自行修改。
* T_SM_TerminalFlowStatistics.cs
*
* 功 能： N/A
* 类 名： T_SM_TerminalFlowStatistics
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/7/29 13:18:10   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;

namespace SeatManage.ClassModel
{
	/// <summary>
	/// T_SM_TerminalFlowStatistics:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TerminalFlowStatistics
	{
	    public TerminalFlowStatistics()
	    {
	        CheckBespeakFlowDic = new Dictionary<int, int>();
	        ComeBackFlowDic = new Dictionary<int, int>();
	        ContinueTimeFlowDic = new Dictionary<int, int>();
	        LeaveFlowDic = new Dictionary<int, int>();
	        ReselectSeatFlowDic = new Dictionary<int, int>();
	        RushCardFlowDic = new Dictionary<int, int>();
	        SelectSeatFlowDic = new Dictionary<int, int>();
	        ShortLeaveFlowDic = new Dictionary<int, int>();
	        WaitSeatFlowDic = new Dictionary<int, int>();
	    }
		#region Model
		private int _id;
		private string _terminalno;
		private DateTime _statisticsdate;
		private string _rushcardflow;
		private string _selectseatflow;
		private string _reselectseatflow;
		private string _checkbespeakflow;
		private string _waitseatflow;
		private string _shortleaveflow;
		private string _comebackflow;
		private string _continuetimeflow;
		private string _leaveflow;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TerminalNo
		{
			set{ _terminalno=value;}
			get{return _terminalno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime StatisticsDate
		{
			set{ _statisticsdate=value;}
			get{return _statisticsdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RushCardFlow
		{
			set{ _rushcardflow=value;}
			get{return _rushcardflow;}
		}
        public Dictionary<int, int> RushCardFlowDic { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string SelectSeatFlow
		{
			set{ _selectseatflow=value;}
			get{return _selectseatflow;}
		}
        public Dictionary<int, int> SelectSeatFlowDic { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ReselectSeatFlow
		{
			set{ _reselectseatflow=value;}
			get{return _reselectseatflow;}
		}
        public Dictionary<int, int> ReselectSeatFlowDic { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CheckBespeakFlow
		{
			set{ _checkbespeakflow=value;}
			get{return _checkbespeakflow;}
		}
        public Dictionary<int, int> CheckBespeakFlowDic { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string WaitSeatFlow
		{
			set{ _waitseatflow=value;}
			get{return _waitseatflow;}
		}
        public Dictionary<int, int> WaitSeatFlowDic { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ShortLeaveFlow
		{
			set{ _shortleaveflow=value;}
			get{return _shortleaveflow;}
		}
        public Dictionary<int, int> ShortLeaveFlowDic { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ComeBackFlow
		{
			set{ _comebackflow=value;}
			get{return _comebackflow;}
		}
        public Dictionary<int, int> ComeBackFlowDic { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ContinueTimeFlow
		{
			set{ _continuetimeflow=value;}
			get{return _continuetimeflow;}
		}
        public Dictionary<int, int> ContinueTimeFlowDic { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string LeaveFlow
		{
			set{ _leaveflow=value;}
			get{return _leaveflow;}
		}
        public Dictionary<int, int> LeaveFlowDic { get; set; }
		#endregion Model

	}
}

