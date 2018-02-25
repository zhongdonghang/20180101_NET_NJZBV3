using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    public class JM_ActualTimeRecordParam
    {
        bool getBespeakLog;
        /// <summary>
        /// 获取预约记录
        /// </summary>
        public bool GetBespeakLog
        {
            get { return getBespeakLog; }
            set { getBespeakLog = value; }
        }
        bool getEnterOutLog;
        /// <summary>
        /// 获取进出记录
        /// </summary>
        public bool GetEnterOutLog
        {
            get { return getEnterOutLog; }
            set { getEnterOutLog = value; }
        }
        bool getWaitLog;
        /// <summary>
        /// 获取等待记录
        /// </summary>
        public bool GetWaitLog
        {
            get { return getWaitLog; }
            set { getWaitLog = value; }
        }
        bool getBlackList;
        /// <summary>
        /// 获取黑名单记录
        /// </summary>
        public bool GetBlackList
        {
            get { return getBlackList; }
            set { getBlackList = value; }
        }
    }
}
