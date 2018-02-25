using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;
using System.ServiceModel;

namespace SeatManage.Bll
{
    [Serializable]
    public class T_SM_ReadingRoom
    {
        /// <summary>
        /// 根据条件获取阅览室信息
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <param name="libraryNum">图书馆编号</param>
        /// <param name="schoolNum">校区编号</param>
        /// <returns></returns>
        public static List<ReadingRoomInfo> GetReadingRooms(List<string> roomNum, List<string> libraryNum, List<string> schoolNum)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetReadingRooms(roomNum, libraryNum, schoolNum);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取阅览室信息失败：" + ex.Message);
                return new List<ReadingRoomInfo>();
            }
            
        }
        /// <summary>
        /// 根据阅览室编号获取阅览室信息
        /// </summary>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        public static ReadingRoomInfo GetSingleRoomInfo(string roomNum)
        {
            IWCFService.ISeatManageService SeatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                List<string> roomNums = new List<string>();
                roomNums.Add(roomNum);
                List<ReadingRoomInfo> rooms = SeatService.GetReadingRoomInfo(roomNums);
                if (rooms.Count > 0)
                {
                    return rooms[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("获取阅览室编号失败：" + ex.Message);
                return null;
            }
           
        }
        /// <summary>
        /// 添加阅览室
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddNewReadingRoom(ReadingRoomInfo model)
        {
            IWCFService.ISeatManageService SeatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return SeatService.AddReadingRoom(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("添加阅览室失败：" + ex.Message);
                return false;
            }
            
        }
        /// <summary>
        /// 修改阅览室信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateReadingRoom(ReadingRoomInfo model)
        {
            IWCFService.ISeatManageService SeatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return SeatService.UpdateReadingRoom(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("修改阅览室失败：" + ex.Message);
                return false;
            }
            
        }
        /// <summary>
        /// 删除阅览室，连通座位一起删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool DeleteReadingRoom(ReadingRoomInfo model)
        {
            IWCFService.ISeatManageService SeatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return SeatService.deleteReadingRoom(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("删除阅览室失败：" + ex.Message);
                return false;
            }
           
        }
        /// <summary>
        /// 判断阅览室编号是否重复
        /// </summary>
        /// <param name="ReadingRoomNo"></param>
        /// <returns></returns>
        public static bool ReadingRoomIsExists(string ReadingRoomNo)
        {
            IWCFService.ISeatManageService SeatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return SeatService.ReadingRoomIsExists(ReadingRoomNo);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("查询阅览室失败：" + ex.Message);
                return false;
            }
          
        }
        /// <summary>
        /// 更新座位布局
        /// </summary>
        /// <param name="seatLayout"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.HandleResult UpdateSeatLayout(SeatLayout seatLayout)
        {
            IWCFService.ISeatManageService SeatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return SeatService.UpdateSeatLayout(seatLayout);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("查询阅览室失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
            
        }
        /// <summary>
        /// 根据日期获取阅览室开闭时间
        /// </summary>
        /// <param name="set"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public TimeSpace GetRoomOpenTimeByDate(ReadingRoomSetting set, string date)
        {
            DayOfWeek day = Convert.ToDateTime(date).DayOfWeek;
            TimeSpace timeList = new TimeSpace();

            if (set.RoomOpenSet.UsedAdvancedSet)
            {
                if (set.RoomOpenSet.RoomOpenPlan[day].Used)
                {
                    timeList = set.RoomOpenSet.RoomOpenPlan[day].OpenTime[0];
                    //bespeakTime = DateTime.Parse(string.Format("{0} {1}", Convert.ToDateTime(date).ToShortDateString(), set.RoomOpenSet.RoomOpenPlan[day].OpenTime[0].BeginTime));
                }
                else
                {
                    timeList.BeginTime = set.RoomOpenSet.DefaultOpenTime.BeginTime;
                    timeList.EndTime = set.RoomOpenSet.DefaultOpenTime.EndTime;
                    //bespeakTime = DateTime.Parse(string.Format("{0} {1}", Convert.ToDateTime(date).ToShortDateString(), set.RoomOpenSet.DefaultOpenTime.BeginTime));
                }
            }
            else
            {
                timeList.BeginTime = set.RoomOpenSet.DefaultOpenTime.BeginTime;
                timeList.EndTime = set.RoomOpenSet.DefaultOpenTime.EndTime;
            }
            return timeList;
        }
    }
}
