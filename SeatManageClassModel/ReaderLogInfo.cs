using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    /// <summary>
    /// 读者的记录信息
    /// </summary>
      [Serializable]
    public class ReaderLogInfo
    {
        private List<EnterOutLogInfo> _EnterOutLogList = new List<EnterOutLogInfo>();
        /// <summary>
        /// 进出记录
        /// </summary>
        public List<EnterOutLogInfo> EnterOutLogList
        {
            get { return _EnterOutLogList; }
            set { _EnterOutLogList = value; }
        }
        private List<BespeakLogInfo> _BespeakLogList = new List<BespeakLogInfo>();
        /// <summary>
        /// 预约记录
        /// </summary>
        public List<BespeakLogInfo> BespeakLogList
        {
            get { return _BespeakLogList; }
            set { _BespeakLogList = value; }
        }
        private List<WaitSeatLogInfo> _WaitSeatLogList = new List<WaitSeatLogInfo>();
        /// <summary>
        /// 等待记录
        /// </summary>
        public List<WaitSeatLogInfo> WaitSeatLogList
        {
            get { return _WaitSeatLogList; }
            set { _WaitSeatLogList = value; }
        }
        private List<BlackListInfo> _BlackListLogInfo = new List<BlackListInfo>();
        /// <summary>
        /// 黑名单记录
        /// </summary>
        public List<BlackListInfo> BlackListLogInfo
        {
            get { return _BlackListLogInfo; }
            set { _BlackListLogInfo = value; }
        }
        private List<ViolationRecordsLogInfo> _ViolationRecordsLogList = new List<ViolationRecordsLogInfo>();
        /// <summary>
        /// 违规记录
        /// </summary>
        public List<ViolationRecordsLogInfo> ViolationRecordsLogList
        {
            get { return _ViolationRecordsLogList; }
            set { _ViolationRecordsLogList = value; }
        }
        private List<ReaderNoticeInfo> _NoticeList = new List<ReaderNoticeInfo>();
        /// <summary>
        /// 消息记录
        /// </summary>
        public List<ReaderNoticeInfo> NoticeList
        {
            get { return _NoticeList; }
            set { _NoticeList = value; }
        }
    }
}
