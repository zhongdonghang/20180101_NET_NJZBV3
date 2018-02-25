using System;
using System.Collections.Generic;
using System.Dynamic;
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
        /// 获取用户的基本信息
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <returns></returns>
        string GetUserInfo(string studentNo);
        /// <summary>
        /// 获取用户当前的在座状态
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <returns></returns>
        string GetUserNowState(string studentNo);
        /// <summary>
        /// 获取用户当前的在座状态
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <returns></returns>
        string GetUserNowStateV2(string studentNo,bool isCheckCode);
        /// <summary>
        /// 验证用户信息
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        string CheckUser(string loginId, string password);
        /// <summary>
        /// 获取全部阅览室的基础信息
        /// </summary>
        /// <returns></returns>
        string GetAllRoomInfo();
        /// <summary>
        /// 获取全部阅览室的当前的使用状态（在座人数，是否开馆等）
        /// </summary>
        /// <returns></returns>
        string GetAllRoomNowState();

        /// <summary>
        /// 根据阅览室编号获取但个阅览室开闭状态
        /// </summary>
        /// <returns></returns>
        string GetSingleRoomOpenState(string roomNo, string datetime);
        /// <summary>
        /// 获取阅览室布局图
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <returns></returns>
        string GetRoomSeatLayout(string roomNo);
        /// <summary>
        /// 获取阅览室可预约的座位
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <returns></returns>
        string GetRoomBesapeakState(string roomNo,string bespeakTime);
        /// <summary>
        /// 获取进出记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        string GetEnterOutLog(string studentNo, int pageIndex, int pageSize);
        /// <summary>
        /// 获取预约记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        string GetBesapsekLog(string studentNo, int pageIndex, int pageSize);
        /// <summary>
        /// 获取黑名单记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        string GetBlacklist(string studentNo, int pageIndex, int pageSize);
        /// <summary>
        /// 获取违规记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        string GetViolationLog(string studentNo, int pageIndex, int pageSize);
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
        /// <param name="roomNo">如果为空则随机全部阅览室</param>
        /// <returns></returns>
        string GetRandomSeat(string roomNo);
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
        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="bespeakDate">预约时间</param>
        /// <returns></returns>
        string CancelBesapeakByCardNo(string cardNo, string bespeakDate);
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
        /// 扫描选座终端二维码操作
        /// </summary>
        /// <param name="codeStr">扫描后的二维码</param>
        /// <returns></returns>
        string QRcodeOperation(string codeStr, string studentNo);
        /// <summary>
        /// 扫描座位二维码
        /// </summary>
        /// <param name="codeStr">扫描后的二维码</param>
        /// <returns></returns>
        string QRcodeSeatInfo(string codeStr);
        /// <summary>
        /// 更换座位
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="cardNo"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        string ChangeSeat(string seatNo, string roomNo, string cardNo);
        /// <summary>
        /// 获取登录读者详细信息
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        string GetUserInfo_WeiXin(string studentNo);
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
        /// 获取全部阅览室的基础信息
        /// </summary>
        /// <returns></returns>
        string GetCanBespeakRoomInfo(string date);
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
        string GetSeatNowStatus(string seatNo, string roomNo, string studentNo);
        /// <summary>
        /// 座位签到
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        string CheckSeat(string studentNo);
        /// <summary>
        /// 管理员获取座位状态
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        string GetMessageSeatStatus(string seatNo, string roomNo);
        /// <summary>
        /// 管理员分配座位
        /// </summary>
        /// <param name="studentNo"></param>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        string SelectSeatByMessager(string studentNo, string seatNo, string roomNo);
        /// <summary>
        /// 获取当期图书馆使用情况
        /// </summary>
        /// <returns></returns>
        string GetLibraryNowState();
        /// <summary>
        /// 关闭连接并释放资源
        /// </summary>
        void Dispose();
        /// <summary>
        /// 获取可预约的阅览室
        /// </summary>
        /// <returns></returns>
        string GetCanBespeakRoom();
        /// <summary>
        /// 根据阅览室获取可预约的日期
        /// </summary>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        string GetBespeakDate(string roomNo);
    }
}
