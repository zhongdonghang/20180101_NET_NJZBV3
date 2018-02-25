using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;
using SeatManage.EnumType;


namespace SeatManage.Bll
{
    public class StudyRoomOperation
    {
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddStudyBookingLog(StudyBookingLog model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.AddStudyBookingLog(model);
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("添加研习间预约记录失败：" + ex.Message);
                return false;
            }
            
        }
        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateStudyBookingLog(StudyBookingLog model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.UpdateStudyBookingLog(model);
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("修改研习间预约记录失败：" + ex.Message);
                return false;
            }
           
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool DeleteStudyBookingLog(StudyBookingLog model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.DeleteStudyBookingLog(model);
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("删除研习间预约记录失败：" + ex.Message);
                return false;
            }
            
        }

        /// <summary>
        /// 获取记录列表
        /// </summary>
        /// <param name="CardNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="staetDate"></param>
        /// <param name="endDate"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static List<StudyBookingLog> GetStudyBookingLogList(string CardNo, List<string> roomNo, DateTime startDate, DateTime endDate, List<CheckStatus> status)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetStudyBookingLogList(CardNo, roomNo, startDate, endDate, status);
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("获取研习间预约记录失败：" + ex.Message);
                return new List<StudyBookingLog>();
            }
            
        }
        /// <summary>
        /// 根据ID获取记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static StudyBookingLog GetStudyBookingLogByID(int id)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetStudyBookingLogByID(id);
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("获取研习间预约记录失败：" + ex.Message);
                return null;
            }
            
        }
        /// <summary>
        /// 添加研习间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddNewStudyRoom(StudyRoomInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.AddNewStudyRoom(model);
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("添加研习间失败：" + ex.Message);
                return false;
            }
            
        }
        /// <summary>
        /// 更新研习间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateStudyRoom(StudyRoomInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.UpdateStudyRoom(model);
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("修改研习间失败：" + ex.Message);
                return false;
            }
           
        }
        /// <summary>
        /// 删除研习间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool DeleteStudyRoom(StudyRoomInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.DeleteStudyRoom(model);
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("删除研习间失败：" + ex.Message);
                return false;
            }
           
        }
        /// <summary>
        /// 获取单个研习间信息
        /// </summary>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        public static StudyRoomInfo GetSingleStudyRoonInfo(string roomNo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetSingleStudyRoonInfo(roomNo);
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("获取单个研习间信息失败：" + ex.Message);
                return null;
            }
           
        }
        /// <summary>
        /// 获取研习间列表
        /// </summary>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        public static List<StudyRoomInfo> GetStudyRoonInfoList(List<string> roomNo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetStudyRoonInfoList(roomNo);
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("获取研习间列表失败：" + ex.Message);
                return new List<StudyRoomInfo>();
            }
            
        }
        /// <summary>
        /// 判断是否此时间段已被预约
        /// </summary>
        /// <param name="bookTime"></param>
        /// <param name="useTime"></param>
        /// <returns></returns>
        public static string CheckBookTime(DateTime bookTime, int useTime, string roomNo,int logID)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.CheckBookTime(bookTime, useTime, roomNo, logID);
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("验证预约时间失败：" + ex.Message);
                return "";
            }
            
        }
    }
}
