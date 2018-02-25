using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ServiceHelper
{
    /// <summary>
    /// 选座
    /// </summary>
    public interface IChooseSeat
    {
        /// <summary>
        /// 验证读者是否可以执行选座操作，如果可以返回空，如果不可以返回原因。
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <returns></returns>
        string VerifyCanDoIt(string cardNo,string roomNo);
        string SeatLock(string seatNum);
        string SeatUnLock(string seatNum);
        string SubmitChooseResult(string cardNum,string seatNum,string roomNum);
    } 
}
