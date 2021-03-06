﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.AppServiceHelper
{
    public partial interface IAppServiceHelper
    {
        /// <summary>
        /// 获取常用座位
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <param name="seatCount">获取的座位数目</param>
        /// <param name="dayCount">统计的天数</param>
        /// <returns></returns>
        string GetOftenSeat(string studentNo, int seatCount, int dayCount);
        /// <summary>
        /// 获取随机座位
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <returns></returns>
        string GetRandomSeat(string roomNo);

        /// <summary>
        /// 获取座位预约信息
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="bespeakTime"></param>
        /// <returns></returns>
        string GetSeatBespeakInfo(string seatNo, string roomNo, string bespeakTime);

        /// <summary>
        /// 获取座位当前状态
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="studentNo"></param>
        /// <param name="isMessager"></param>
        /// <returns></returns>
        string GetSeatNowStatus(string seatNo, string roomNo,string studentNo);

        /// <summary>
        /// 管理员获取座位的状态
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        string GetMessageSeatStatus(string seatNo, string roomNo);
    }
}
