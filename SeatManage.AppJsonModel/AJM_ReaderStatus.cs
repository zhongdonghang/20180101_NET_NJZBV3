using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;

namespace SeatManage.AppJsonModel
{
    public class AJM_ReaderStatus
    {
        private string _readerStatus = EnumType.ReaderStatus.None.ToString();
        AJM_EnterOutLog _ajmEnterOutLog;
        List<AJM_BespeakLog> _ajmBespeakLogs;
        AJM_WaitSeatLog _ajmWaitSeatLog; 
        /// <summary>
        /// 读者当前状态
        /// </summary>
        public string Status
        {
            get { return _readerStatus; }
            set { _readerStatus = value; }
        }
        /// <summary>
        /// 当前有效进出记录
        /// </summary>
        public AJM_EnterOutLog AjmEnterOutLog
        {
            get { return _ajmEnterOutLog; }
            set { _ajmEnterOutLog = value; }
        }
        /// <summary>
        /// 当前有效预约记录
        /// </summary>
        public List<AJM_BespeakLog> AjmBespeakLogs
        {
            get { return _ajmBespeakLogs; }
            set { _ajmBespeakLogs = value; }
        }
        /// <summary>
        /// 当前有效等待记录
        /// </summary>
        public AJM_WaitSeatLog AjmWaitSeatLogs
        {
            get { return _ajmWaitSeatLog; }
            set { _ajmWaitSeatLog = value; }
        }
    }
}
