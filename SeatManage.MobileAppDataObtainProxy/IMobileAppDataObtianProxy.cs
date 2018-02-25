using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManage.MobileAppDataObtainProxy
{
    /// <summary>
    /// 移动App数据访问代理接口。
    /// </summary>
    public interface IMobileAppDataObtianProxy
    {
        /// <summary>
        /// 取消预约记录
        /// </summary>
        string CancelBespeakLog(int bespeakId, string remark);
        /// <summary>
        /// 扫码更换座位的服务
        /// </summary> 
        string ChangeSeat(string cardNo, string seatNo, string readingRoom);
        /// <summary>
        /// 释放座位操作
        /// </summary>
        string FreeSeat(string cardNo);
        /// <summary>
        /// 获取阅览室座位使用情况
        /// </summary>
        string GetAllRoomSeatUsedInfo();
        /// <summary>
        /// 获取可预约的阅览室
        /// </summary>
        string GetOpenBespeakRooms(string strDate);
        /// <summary>
        /// 获取读者实时记录
        /// </summary>
        string GetReaderActualTimeRecord(string cardNum, string getItemsParameter);
        /// <summary>
        /// 获取读者预约记录
        /// </summary>
        string GetReaderBespeakRecord(string cardNum, int pageIndex, int pageSize);
        /// <summary>
        /// 获取读者的黑名单记录
        /// </summary>
        string GetReaderBlacklistRecord(string cardNum, int pageIndex, int pageSize);
        /// <summary>
        /// 获取读者的选座记录
        /// </summary>
        string GetReaderChooseSeatRecord(string cardNum, int pageIndex, int pageSize);
        /// <summary>
        /// 验证用户密码，并返回读者信息
        /// </summary>
        string GetReaderAccount(string cardNum, string password);
        /// <summary>
        /// 获取扫描的座位信息
        /// </summary>
        string GetScanCodeSeatInfo(string scanResult, string cardNo);
        /// <summary>
        /// 获取阅览室可预约的座位信息
        /// </summary>
        string GetSeatsBespeakInfoByRoomNum(string roomNum, string date);
        /// <summary>
        /// 获取读者的违规记录
        /// </summary>
        string GetViolateDiscipline(string cardNum, int pageIndex, int pageSize);
        /// <summary>
        /// 设置暂时离开
        /// </summary>
        string ShortLeave(string cardNo);
        /// <summary>
        /// 提交预约信息
        /// </summary>
        string SubmitBespeakInfo(string cardNo, string roomNum, string seatNum, string bespeakDatetime, string remark);
        /// <summary>
        ///  提预约信息 用于提交自定义预约信息。 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="roomNum"></param>
        /// <param name="seatNum"></param>
        /// <param name="bespeakDatetime"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        string SubmitBespeakInfoCustomTime(string cardNo, string roomNum, string seatNum, string bespeakDatetime, string remark);
        /// <summary>
        /// 获取读者的提醒消息
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        string GetReaderNotice(string cardNum, int pageIndex, int pageSize);
        /// <summary>
        /// 获取读者当前座位状态
        /// </summary>
        /// <param name="cardNum"></param>
        /// <returns></returns>
        string GetReaderSeatState(string cardNum);
        /// <summary>
        /// 关闭连接并释放资源
        /// </summary>
         void Dispose();
    }
}
