using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    /// <summary>
    /// 二维码扫描结果处理，
    ///   包含实时座位信息、座位是否可以被预约、以及座位使用信息
    ///   如果读者有暂离、预约记录、并且是在该座位上，还包含记录入座处理结果
    /// </summary>
    public class JM_ScanResult
    {
        /// <summary>
        /// 读者实时座位信息
        /// </summary>
        public JM_ActualTimeRecords ActualTimeRecords
        {
            get;
            set;
        }
        /// <summary>
        /// 扫描到的座位信息
        /// </summary>
        public JM_Seat SeatInfo
        {
            get;
            set;
        }
        public JM_OpenBespeakReadingRoom Room
        {
            set;
            get;
        }
        /// <summary>
        /// 是否处理了记录
        /// </summary>
        public bool IsHandleLog
        {
            get;
            set;
        }
        /// <summary>
        /// 处理结果
        /// </summary>
        public JM_HandleResult LogHandleResult
        { 
            get;
            set;
        }
    }
}
