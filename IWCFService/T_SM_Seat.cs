using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SeatManage.EnumType;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 尝试锁定座位，如果座位锁定失败，说明座位已经被加锁，则返回false，
        /// 锁定成功返回true
        /// </summary>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.EnumType.SeatLockState SeatLocked(string seatNo);
        /// <summary>
        /// 解锁座位
        /// </summary>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.EnumType.SeatLockState SeatUnLocked(string seatNo);
        /// <summary>
        /// 获取读者常坐的座位
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="days"></param>
        /// <param name="roomNums"></param>
        /// <returns></returns>
        [OperationContract]
        List<SeatManage.ClassModel.Seat> GetOftenUsedSeatByCardNo(string cardNo, int days, List<string> roomNums);
        /// <summary>
        /// 随机分配座位编号
        /// </summary>
        /// <param name="reandingRoom">阅览室编号</param>
        /// <returns></returns>
        [OperationContract]
        string RandomAllotSeat(string reandingRoomNum);
        /// <summary>
        /// 根据阅览室获取座位，不填默认不做条件判断
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <param name="lockstate">锁定状态</param>
        /// <param name="state">座位状态</param>
        /// <returns></returns>
        [OperationContract]
        List<SeatManage.ClassModel.Seat> GetSeatListByReadingRoom(string roomNum, bool lockstate);
        /// <summary>
        /// 通过座位号获取座位信息
        /// </summary>
        /// <param name="seatNum"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.ClassModel.Seat GetSeatInfoBySeatNum(string seatNum);
    }
}
