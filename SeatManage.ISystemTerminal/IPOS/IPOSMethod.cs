using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ISystemTerminal.IPOS
{

    public delegate void EventPosCardNo(object sender, CardEventArgs e);
    public interface IPOSMethod
    { 
        /// <summary>
        /// 开始读卡
        /// </summary>
        void Start();
        /// <summary>
        /// 暂停读卡
        /// </summary>
        void Stop();

        void Reset();
        event EventPosCardNo CardNoGeted;
    }

    public class CardEventArgs : EventArgs
    {
        private string cardNo;
        /// <summary>
        /// 学号
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        private bool posResult;
        /// <summary>
        /// 处理结果，true为成功
        /// </summary>
        public bool PosResult
        {
            get { return posResult; }
            set { posResult = value; }
        }
        private string errorInfo;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorInfo
        {
            get { return errorInfo; }
            set { errorInfo = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo">读卡器读到的学号</param>
        /// <param name="posResult">读取的结果，如果正确获取到，该值为true，获取异常，该值为false，返回错误信息</param>
        /// <param name="errorInfo">错误信息，posResult为true时，该值为null</param>
        public CardEventArgs(string cardNo, bool posResult, string errorInfo)
            : base()
        {
            this.CardNo = cardNo;
            this.posResult = posResult;
            this.errorInfo = errorInfo;
        }
    }
}
