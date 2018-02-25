using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;
using System.ServiceModel;

namespace SeatManage.Bll
{
    public class T_SM_Seat
    {
        /// <summary>
        /// 座位加锁
        /// </summary>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.SeatLockState LockSeat(string seatNo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                SeatManage.EnumType.SeatLockState seatLock = seatService.SeatLocked(seatNo);
                return seatLock;
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("座位锁定失败：" + ex.Message);
                return EnumType.SeatLockState.None;
            }
           
        }
        /// <summary>
        /// 解锁座位
        /// </summary>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.SeatLockState UnLockSeat(string seatNo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                SeatManage.EnumType.SeatLockState seatLock = seatService.SeatUnLocked(seatNo);
                return seatLock;
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("座位解锁失败：" + ex.Message);
                return EnumType.SeatLockState.None;
            }
            
        }
        /// <summary>
        /// 获取读者常坐座位
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="days"></param>
        /// <param name="roomNums"></param>
        /// <returns></returns>
        public static List<Seat> GetReaderOftenUsedSeat(string cardNo, int days, List<string> roomNums)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                List<Seat> seats = seatService.GetOftenUsedSeatByCardNo(cardNo, days, roomNums);
                return seats;
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取常坐在座失败：" + ex.Message);
                throw new Exception("获取常坐在座失败：" + ex.Message);
               // return new List<Seat>();
            }
          
        }
        /// <summary>
        /// 随机获取座位编号
        /// </summary>
        /// <param name="reandingRoom">阅览室编号</param>
        /// <returns></returns>
        public static string RandomAllotSeat(string reandingRoomNum)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                string seats = seatService.RandomAllotSeat(reandingRoomNum);
                return seats;
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取随机座位失败：" + ex.Message);
                return null;
            }
           
        }
        /// <summary>
        /// 根据阅览室编号获取座位
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <param name="lockstat">是否锁定</param>
        /// <returns></returns>
        public static List<Seat> GetSeatListByRoomNum(string roomNum, bool lockstat)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetSeatListByReadingRoom(roomNum, lockstat);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取阅览室编号错误：" + ex.Message);
                return new List<Seat>();
            }
           
        }
        /// <summary>
        /// 根据座位编号获取座位信息
        /// </summary>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        public static Seat GetSeatInfoBySeatNo(string seatNo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool isError = false;
            try
            {
                return seatService.GetSeatInfoBySeatNum(seatNo);
            }
            catch (Exception EX)
            {
                isError = true;
                WriteLog.Write("获取座位信息出错：" + EX.Message);
                return null;
            }
           
        }
    }
}
