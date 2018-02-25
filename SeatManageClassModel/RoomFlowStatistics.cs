/**  版本信息模板在安装目录下，可自行修改。
* T_SM_RoomFlowStatistics.cs
*
* 功 能： N/A
* 类 名： T_SM_RoomFlowStatistics
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/7/29 13:18:08   N/A    初版
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
    /// T_SM_RoomFlowStatistics:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class RoomFlowStatistics
    {
        public RoomFlowStatistics()
        {
            BespeakCheckFlowDic = new Dictionary<int, int>();
            ComeBackFlowDic = new Dictionary<int, int>();
            ContinueFlowDic = new Dictionary<int, int>();
            EnterFlowDic = new Dictionary<int, int>();
            LeaveFlowDic = new Dictionary<int, int>();
            OnSeatDic = new Dictionary<int, int>();
            OutFlowDic = new Dictionary<int, int>();
            ReselectFlowDic = new Dictionary<int, int>();
            SelectFlowDic = new Dictionary<int, int>();
            ShortLeaveFlowDic = new Dictionary<int, int>();
            WaitSelectFlowDic = new Dictionary<int, int>();
        }
        #region Model
        private int _id;
        private string _readingroomno;
        private DateTime _statisticsdate;
        private string _enterflow;
        private string _outflow;
        private string _onseat;
        private string _selectflow;
        private string _reselectflow;
        private string _bespeakcheckflow;
        private string _waitselectflow;
        private string _shortleaveflow;
        private string _comebackflow;
        private string _continueflow;
        private string _leaveflow;
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
        public string EnterFlow
        {
            set { _enterflow = value; }
            get { return _enterflow; }
        }
        public Dictionary<int, int> EnterFlowDic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OutFlow
        {
            set { _outflow = value; }
            get { return _outflow; }
        }
        public Dictionary<int, int> OutFlowDic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OnSeat
        {
            set { _onseat = value; }
            get { return _onseat; }
        }
        public Dictionary<int, int> OnSeatDic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SelectFlow
        {
            set { _selectflow = value; }
            get { return _selectflow; }
        }
        public Dictionary<int, int> SelectFlowDic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ReselectFlow
        {
            set { _reselectflow = value; }
            get { return _reselectflow; }
        }
        public Dictionary<int, int> ReselectFlowDic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BespeakCheckFlow
        {
            set { _bespeakcheckflow = value; }
            get { return _bespeakcheckflow; }
        }
        public Dictionary<int, int> BespeakCheckFlowDic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string WaitSelectFlow
        {
            set { _waitselectflow = value; }
            get { return _waitselectflow; }
        }
        public Dictionary<int, int> WaitSelectFlowDic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ShortLeaveFlow
        {
            set { _shortleaveflow = value; }
            get { return _shortleaveflow; }
        }
        public Dictionary<int, int> ShortLeaveFlowDic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ComeBackFlow
        {
            set { _comebackflow = value; }
            get { return _comebackflow; }
        }
        public Dictionary<int, int> ComeBackFlowDic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ContinueFlow
        {
            set { _continueflow = value; }
            get { return _continueflow; }
        }
        public Dictionary<int, int> ContinueFlowDic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LeaveFlow
        {
            set { _leaveflow = value; }
            get { return _leaveflow; }
        }
        public Dictionary<int, int> LeaveFlowDic { get; set; }
        #endregion Model

    }
}

