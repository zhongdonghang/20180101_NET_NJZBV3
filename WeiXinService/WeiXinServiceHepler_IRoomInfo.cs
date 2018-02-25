using System;
using System.Collections.Generic;
using SeatManage.AppJsonModel;
using SeatManage.MobileAppDataObtainProxy;
using SeatManage.SeatManageComm;

namespace WeiXinService
{
    public partial class WeiXinServiceHepler : IWeiXinService
    {
        /// <summary>
        /// 获取全部阅览室的基础信息
        /// </summary>
        /// <returns></returns>
        public List<AJM_ReadingRoom> GetAllRoomInfo(string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.GetAllRoomInfo();
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("获取全部阅览室基础信息失败！");
                }
                if (!ajmResult.Result)
                {
                    throw new Exception(ajmResult.Msg);
                }
                List<AJM_ReadingRoom> ajmReadingRooms = JSONSerializer.Deserialize<List<AJM_ReadingRoom>>(ajmResult.Msg);
                return ajmReadingRooms;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取全部阅览室基础信息失败：{0}", ex.Message));
                return null;
            }
        }
        /// <summary>
        /// 获取单个阅览室开闭状态
        /// </summary>
        /// <param name="roomNo"></param>
        /// <param name="datetime"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public AJM_ReadingRoomState GetSingleRoomOpenState(string roomNo, string datetime, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.GetAllRoomNowState();
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("获取单个阅览室的当前的开闭状态失败！");
                }
                if (!ajmResult.Result)
                {
                    throw new Exception(ajmResult.Msg);
                }
                AJM_ReadingRoomState ajmReadingRoomState =JSONSerializer.Deserialize<AJM_ReadingRoomState>(ajmResult.Msg);
                return ajmReadingRoomState;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取单个阅览室的当前的开闭状态失败：{0}", ex.Message));
                return null;
            }
        }

        /// <summary>
        /// 获取全部阅览室的当前的使用状态（在座人数，是否开馆等）
        /// </summary>
        /// <returns></returns>
        public List<AJM_ReadingRoomState> GetAllRoomNowState(string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.GetAllRoomNowState();
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("获取全部阅览室的当前的使用状态失败！");
                }
                if (!ajmResult.Result)
                {
                    throw new Exception(ajmResult.Msg);
                }
                List<AJM_ReadingRoomState> ajmReadingRoomStates =JSONSerializer.Deserialize<List<AJM_ReadingRoomState>>(ajmResult.Msg);
                return ajmReadingRoomStates;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取全部阅览室的当前的使用状态失败：{0}", ex.Message));
                return null;
            }
        }

        /// <summary>
        /// 获取阅览室布局图
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public AJM_SeatLayout GetRoomSeatLayout(string roomNo, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.GetRoomSeatLayout(roomNo);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("获取全部阅览室的当前的使用状态失败！");
                }
                if (!ajmResult.Result)
                {
                    throw new Exception(ajmResult.Msg);
                }
                AJM_SeatLayout ajmSeatLayout = JSONSerializer.Deserialize<AJM_SeatLayout>(ajmResult.Msg);
                return ajmSeatLayout;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取全部阅览室的当前的使用状态失败：{0}", ex.Message));
                return null;
            }
        }
        /// <summary>
        /// 获取阅览室可预约的座位
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <param name="bespeakTime"></param>
        /// <returns></returns>
        public List<AJM_BespeakSeat> GetRoomBesapeakSeat(string roomNo, string bespeakTime, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.GetRoomBesapeakState(roomNo, bespeakTime);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("获取阅览室的可预约座位失败！");
                }
                if (!ajmResult.Result)
                {
                    throw new Exception(ajmResult.Msg);
                }
                List<AJM_BespeakSeat> ajmBespeakSeats = JSONSerializer.Deserialize<List<AJM_BespeakSeat>>(ajmResult.Msg);
                return ajmBespeakSeats;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取阅览室的可预约座位失败：{0}", ex.Message));
                throw ex;
            }
        }

        /// <summary>
        /// 获取可预约阅览室的座位
        /// </summary>
        /// <param name="date"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public List<AJM_ReadingRoom> GetBesapeakRoomList(string date, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.GetCanBespeakRoomInfo(date);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("获取可预约的阅览室列表失败！");
                }
                if (!ajmResult.Result)
                {
                    throw new Exception(ajmResult.Msg);
                }
                List<AJM_ReadingRoom> ajmReadingRooms = JSONSerializer.Deserialize<List<AJM_ReadingRoom>>(ajmResult.Msg);
                return ajmReadingRooms;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取可预约的阅览室列表失败：{0}", ex.Message));
                return null;
            }
        }
    }
}
