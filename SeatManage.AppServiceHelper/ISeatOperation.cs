using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManage.AppServiceHelper
{
    public partial interface IAppServiceHelper
    {
        /// <summary>
        /// 预约提交
        /// </summary>
        /// <param name="seatNo">座位编号（9位）</param>
        /// <param name="roomNo">阅览室编号</param>
        /// <param name="studentNo">学生学号</param>
        /// <param name="besapeakTime">预约的时间（立即预约次处值无效可为空）</param>
        /// <param name="isNowBesapeak">是否是立即预约</param>
        /// <returns></returns>
        string SubmitBesapeskSeat(string seatNo, string roomNo, string studentNo, string besapeakTime, bool isNowBesapeak);
        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="bespeakId">预约记录的ID</param>
        /// <returns></returns>
        string CancelBesapeak(int bespeakId);
        /// 根据学号和日期取消预约
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <param name="bespeakDate">预约日期</param>
        /// <returns></returns>
        string CancelBespeakLogByCardNo(string studentNo, string bespeakDate);
        /// <summary>
        /// 座位暂离
        /// </summary>
        /// <param name="studentNo">读者学号</param>
        /// <returns></returns>
        string ShortLeave(string studentNo);
        /// <summary>
        /// 释放座位
        /// </summary>
        /// <param name="studentNo">读者学号</param>
        /// <returns></returns>
        string ReleaseSeat(string studentNo);
        /// <summary>
        /// 取消等待座位
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        string CancelWait(string studentNo);
        /// <summary>
        /// 暂离回来
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        string ComeBack(string studentNo);
        /// <summary>
        /// 选择座位
        /// </summary>
        /// <param name="studentNo"></param>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        string SelectSeat(string studentNo, string seatNo, string roomNo);
        /// <summary>
        /// 选择座位
        /// </summary>
        /// <param name="studentNo"></param>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        string SelectSeatByMessager(string studentNo, string seatNo, string roomNo);
        /// <summary>
        /// 座位续时
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        string DelayTime(string studentNo);
        /// <summary>
        /// 等待座位
        /// </summary>
        /// <param name="studentNo_A"></param>
        /// <param name="studentNo_B"></param>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        string WaitSeat(string studentNo_A, string studentNo_B, string seatNo);
        /// <summary>
        /// 预约签到
        /// </summary>
        /// <param name="besapeakNo"></param>
        /// <returns></returns>
        string ConfirmSeat(string besapeakNo);
        /// <summary>
        /// 更换座位
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        string ChangeSeat(string seatNo, string roomNo, string cardNo);
        /// <summary>
        /// 座位签到
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        string CheckSeat(string studentNo);
    }
}
