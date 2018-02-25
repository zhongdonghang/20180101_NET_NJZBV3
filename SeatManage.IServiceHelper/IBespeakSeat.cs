using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ServiceHelper
{
    public interface IBespeakSeat
    {
        /// <summary>
        /// 获取指定时间开放预约的房间
        /// </summary>
        /// <returns></returns>
        string GetOpenBespeakRooms(string strDate);  
        /// <summary>
        /// 提交预约信息。返回结果为HandleResult对象，成功设置为true，result设置返回信息。
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="roomNum">房间编号</param>
        /// <param name="seatNum">座位号</param>
        /// <param name="bespeakDatetime">预约时间</param>
        /// <returns></returns>
        string SubmitBespeakInfo(string cardNo,  string roomNum,string seatNum,string bespeakDatetime, string remark);
        /// <summary>
        /// 根据阅览室编号获取预约设置
        /// </summary>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        //string GetBespeakSeatRoomSet(string roomNum);
        /// <summary>
        /// 提交自定义时间的预约信息（需要验证）
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="roomNum"></param>
        /// <param name="seatNum"></param>
        /// <param name="bespeakDatetime"></param>
        /// <returns></returns>
        string SubmitBespeakInfoCustomTime(string cardNo, string roomNum, string seatNum, string bespeakDatetime, string remark);
        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="bespeakId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        string CancelBespeakLog(int bespeakId,string remark);
        /// <summary>
        /// 获取扫描的二维码信息
        /// </summary>
        /// <param name="scanResult"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        string GetScanCodeSeatInfo(string scanResult, string cardNo);
       
        /// <summary>
        /// 更换座位
        /// </summary>
        /// <param name="cardNo">更换座位</param>
        /// <returns></returns>
        string ChangeSeat(string cardNo, string seatNo, string readingRoom);
        /// <summary>
        /// 预约签到
        /// </summary>
        /// <param name="cardNum"></param>
        /// <returns></returns>
        string BespeakCheck(string cardNum);
        /// <summary>
        /// 获取读者的消息提醒列表
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        string GetReaderNoticeList(string cardNum, int pageIndex, int pageSize);

        /// <summary>
        /// 获取读者座位状态
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        string GetReaderSeatState(string cardNo);
        
    }
}
