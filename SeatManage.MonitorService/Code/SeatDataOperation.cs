using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;

namespace SeatService.MonitorService.Code
{
    public partial class SeatDataOperation
    {
        /// <summary>
        /// 阅览室列表
        /// </summary>
        private Dictionary<string, ReadingRoomInfo> roomList = new Dictionary<string, ReadingRoomInfo>();
        /// <summary>
        /// 违规设置
        /// </summary>
        private static RegulationRulesSetting regulationRulesSetting;
        /// <summary>
        /// 获取的进出记录类型
        /// </summary>
        private List<EnterOutLogType> enterOutLogTypeList = new List<EnterOutLogType>();


        /// <summary>
        /// 构造函数
        /// </summary>
        public SeatDataOperation()
        {
            GetSetting();
            enterOutLogTypeList.Add(EnterOutLogType.BookingConfirmation);
            enterOutLogTypeList.Add(EnterOutLogType.ComeBack);
            enterOutLogTypeList.Add(EnterOutLogType.SelectSeat);
            enterOutLogTypeList.Add(EnterOutLogType.ReselectSeat);
            enterOutLogTypeList.Add(EnterOutLogType.WaitingSuccess);
            enterOutLogTypeList.Add(EnterOutLogType.ShortLeave);
            enterOutLogTypeList.Add(EnterOutLogType.ContinuedTime);
        }
        /// <summary>
        /// 获取最新设置
        /// </summary>
        public void GetSetting()
        {
            try
            {
                //获取全部阅览室信息和黑名单设置
                List<ReadingRoomInfo> rooms = ClientConfigOperate.GetReadingRooms(null);
                regulationRulesSetting = T_SM_SystemSet.GetRegulationRulesSetting();
                roomList.Clear();
                foreach (ReadingRoomInfo room in rooms)
                {
                    roomList.Add(room.No, room);
                }
            }
            catch (Exception e)
            {
                WriteLog.Write(string.Format("监控服务：获取阅览室设置时遇到错误：{0}", e.Message));
            }
        }
        /// <summary>
        /// 服务启动清除异常处理
        /// </summary>
        public void ServiceStartOperate()
        {
            try
            {
                foreach (ReadingRoomInfo rri in roomList.Values)
                {
                    if (rri.Setting != null)
                    {
                        OpenReadingRoom(rri);
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog.Write("监控服务：服务启动清除异常处理失败：" + e.Message);
            }
        }
        /// <summary>
        /// 处理锁定超时
        /// </summary>
        public void LockOverTime()
        {
            try
            {
                DateTime nowDateTime = ServiceDateTime.Now;
                //获取全部的锁定座位
                List<Seat> SeatList = T_SM_Seat.GetSeatListByRoomNum(null, true);
                if (SeatList != null)
                {
                    foreach (Seat s in SeatList)
                    {
                        if (s.LockedTime.AddMinutes(1.0) < nowDateTime)
                        {
                            T_SM_Seat.UnLockSeat(s.SeatNo);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog.Write(e.Message);
            }
        }
    }
}
