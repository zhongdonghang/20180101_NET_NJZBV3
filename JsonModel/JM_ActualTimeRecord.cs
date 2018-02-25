using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    /// <summary>
    /// 读者的实时记录
    /// 预约记录
    ///选座记录
    ///等待记录
    ///黑名单记录
    /// </summary>
    public class JM_ActualTimeRecords
    {
        JM_EnterOutLog enterOutLog = null;

        public JM_EnterOutLog EnterOutLog
        {
            get { return enterOutLog; }
            set { enterOutLog = value; }
        }
        JM_BespeakLog bespeakLog = null;

        public JM_BespeakLog BespeakLog
        {
            get { return bespeakLog; }
            set { bespeakLog = value; }
        }
        JM_WaitSeatLog waitSeatLog = null;

        public JM_WaitSeatLog WaitSeatLog
        {
            get { return waitSeatLog; }
            set { waitSeatLog = value; }
        }

        JM_Blacklist blacklistLog = null;

        public JM_Blacklist BlacklistLog
        {
            get { return blacklistLog; }
            set { blacklistLog = value; }
        }
    }
}
