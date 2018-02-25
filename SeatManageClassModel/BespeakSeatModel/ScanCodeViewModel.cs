using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel.BespeakSeatModel
{
    /// <summary>
    /// 扫码页面的ViewModel
    /// </summary>
    [Serializable]
    public class ScanCodeViewModel
    {
        ReaderInfo readerInfo;
        Seat seatInfo;
        /// <summary>
        /// 读者信息
        /// </summary>
        public ReaderInfo ReaderInfo
        {
            get { return readerInfo; }
            set { readerInfo = value; }
        }
        /// <summary>
        /// 座位信息
        /// </summary>
        public Seat SeatInfo
        { 
            get { return seatInfo;}
            set { seatInfo = value;}
        }

        private BespeakLogInfo bespeakLog;
        /// <summary>
        /// 读者是否存在有效的预约记录
        /// </summary>
        public BespeakLogInfo BespeakLog
        {
            get { return bespeakLog; }
            set { bespeakLog = value; }
        }
    }
}
