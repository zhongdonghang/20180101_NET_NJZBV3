using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SeatManage.ServiceHelper
{
    public interface ISeat
    {
        /// <summary>
        /// 获取座位的预约信息
        /// </summary>
        /// <param name="seatNum"></param>
        /// <returns></returns>
        string GetSeatBespeakInfo(string seatNum);
        /// <summary>
        /// 获取座位当前使用情况
        /// </summary>
        /// <param name="seatNum"></param>
        /// <returns></returns>
        string GetSeatUsage(string seatNum);
        /// <summary>
        /// 根据二维码获取座位信息
        /// </summary>
        /// <param name="strQRcode"></param>
        /// <returns></returns>
        string GetQRcodeInfo(string strQRcode);
    }
}
