using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ServiceHelper
{
    public interface IDelaySeatUsedTime
    {
        /// <summary>
        /// 获取阅览室延时设置
        /// </summary>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        string GetDelaySet(string roomNum);
         
       
        /// <summary>
        /// 延长续时操作
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        string SubmitDelayResult(string cardNo);
    }
}
