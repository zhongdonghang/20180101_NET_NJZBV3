using System.Collections.Generic;
using SeatManage.AppJsonModel;

namespace WeiXinService
{
    public partial interface IWeiXinService
    {
        /// <summary>
        /// 获取常用座位
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <param name="seatCount">获取的座位数目</param>
        /// <param name="dayCount">统计的天数</param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        List<AJM_Seat> GetOftenSeat(string studentNo, int seatCount, int dayCount, string schoolNo);

        /// <summary>
        /// 获取随机座位
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        AJM_Seat GetRandomSeat(string roomNo, string schoolNo);

        /// <summary>
        /// 获取座位预约信息
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="bespeakTime"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        AJM_SeatBespeakInfo GetSeatBespeakInfo(string seatNo, string roomNo, string bespeakTime,string schoolNo);
    }
}
