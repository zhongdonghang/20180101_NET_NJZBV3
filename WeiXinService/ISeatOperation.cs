namespace WeiXinService
{
    public partial interface IWeiXinService
    {
        /// <summary>
        /// 预约提交
        /// </summary>
        /// <param name="seatNo">座位编号（9位）</param>
        /// <param name="roomNo">阅览室编号</param>
        /// <param name="cardNo"></param>
        /// <param name="besapeakTime">预约的时间（立即预约次处值无效可为空）</param>
        /// <param name="isNowBesapeak">是否是立即预约</param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        string SubmitBesapeskSeat(string seatNo, string roomNo, string cardNo, string besapeakTime, bool isNowBesapeak, string schoolNo);

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="bespeakId">预约记录的ID</param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        string CancelBesapeakById(int bespeakId, string schoolNo);

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="bespeakDate">预约时间</param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        string CancelBesapeakByCardNo(string cardNo,string bespeakDate, string schoolNo);

        /// <summary>
        /// 座位暂离
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        string ShortLeave(string cardNo, string schoolNo);

        /// <summary>
        /// 释放座位
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        string ReleaseSeat(string cardNo, string schoolNo);

        /// <summary>
        /// 取消等待座位
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        string CancelWait(string cardNo, string schoolNo);
        /// <summary>
        /// 更换座位
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="cardNo"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        string ChangeSeat(string seatNo, string roomNo, string cardNo, string schoolNo);
        /// <summary>
        /// 暂离回来
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        string ComeBack(string studentNo, string schoolNo);
        /// <summary>
        /// 选择座位
        /// </summary>
        /// <param name="studentNo"></param>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        string SelectSeat(string studentNo, string seatNo, string roomNo, string schoolNo);
        /// <summary>
        /// 座位续时
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        string DelayTime(string studentNo, string schoolNo);
        /// <summary>
        /// 等待座位
        /// </summary>
        /// <param name="studentNo_A"></param>
        /// <param name="studentNo_B"></param>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        string WaitSeat(string studentNo_A, string studentNo_B, string seatNo, string schoolNo);
        /// <summary>
        /// 预约签到
        /// </summary>
        /// <param name="besapeakNo"></param>
        /// <returns></returns>
        string ConfirmSeat(string besapeakNo,string schoolNo);
    }
}
