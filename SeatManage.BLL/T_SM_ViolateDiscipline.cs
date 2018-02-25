using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using System.ServiceModel;

namespace SeatManage.Bll
{
    public class T_SM_ViolateDiscipline
    {
        /// <summary>
        /// 添加违规记录
        /// </summary>
        /// <param name="violationrecords"></param>
        /// <returns></returns>
        public static bool AddViolationRecords(ViolationRecordsLogInfo violationrecords)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                if (seatService.AddViolationRecordsLog(violationrecords) == EnumType.HandleResult.Successed)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加进出记录失败：" + ex.Message);
                return false;
            }
            
        }
        /// <summary>
        /// 获取有效的违规记录
        /// </summary>
        /// <returns></returns>
        public static List<ViolationRecordsLogInfo> GetViolationRecordsLog(string cardNo, string roomNums)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetViolationRecordsLogInfo(cardNo, roomNums);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取违规记录失败：" + ex.Message);
                return new List<ViolationRecordsLogInfo>();
            }
           
        }
        /// <summary>
        /// 根据id获取违规记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ViolationRecordsLogInfo GetViolationRecords(string id)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetViolationRecordsLog(id);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取违规记录失败：" + ex.Message);
                return null;
            }
           
        }
        /// <summary>
        /// 根据blacklistid获取违规记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<ViolationRecordsLogInfo> GetViolationRecordsByBlackliatID(string blacklistid)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetViolationRecordsLogByblacklistID(blacklistid);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取违规记录失败：" + ex.Message);
                return new List<ViolationRecordsLogInfo>();
            }
          
        }
        /// <summary>
        /// 获取违规记录
        /// </summary>
        /// <returns></returns>
        public static List<ViolationRecordsLogInfo> GetViolationRecords(string cardNo, string roomNums, string beginDate, string endDate, LogStatus logstatus, LogStatus blackliststatus)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetViolationRecordsLogs(cardNo, roomNums, beginDate, endDate, logstatus, blackliststatus);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取违规记录失败：" + ex.Message);
                return new List<ViolationRecordsLogInfo>();
            }
            
        }
        /// <summary>
        /// 获取违规记录
        /// </summary>
        /// <returns></returns>
        public static List<ViolationRecordsLogInfo> GetViolationRecords(string cardNo, string roomNums, string beginDate, string endDate, LogStatus logstatus, LogStatus blackliststatus, ViolationRecordsType vrtype)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetViolationRecordsLogsByType(cardNo, roomNums, beginDate, endDate, logstatus, blackliststatus, vrtype);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取违规记录失败：" + ex.Message);
                return new List<ViolationRecordsLogInfo>();
            }
           
        }
        /// <summary>
        /// 通过学号模糊查询，获取违规记录
        /// </summary>
        /// <returns></returns>
        public static List<ViolationRecordsLogInfo> GetViolationRecords_ByFuzzySearch(string cardNo, string roomNums, string beginDate, string endDate, LogStatus logstatus, LogStatus blackliststatus, ViolationRecordsType vrtype)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetViolationRecordsLogsByType_ByFuzzySearch(cardNo, roomNums, beginDate, endDate, logstatus, blackliststatus, vrtype);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取违规记录失败：" + ex.Message);
                return new List<ViolationRecordsLogInfo>();
            }
           
        }
        /// <summary>
        /// 更新违规记录
        /// </summary>
        /// <param name="violationrecords"></param>
        /// <returns></returns>
        public static bool UpdateViolationRecords(ViolationRecordsLogInfo violationrecords)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                if (seatService.UpdateViolationRecordsLog(violationrecords) == EnumType.HandleResult.Successed)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("修改违规记录失败：" + ex.Message);
                return false;
            }
          
        }
        /// <summary>
        /// 根据查询条件删除违规记录
        /// </summary>
        /// <param name="begDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="roomNo">阅览室编号</param>
        /// <returns></returns>
        public static int DelBySearch(string begDate, string endDate, string roomNo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            List<ViolationRecordsLogInfo> vlists = seatService.GetViolationRecordsLogs(null, roomNo, begDate + " 0:00:00", endDate + " 23:59:59", SeatManage.EnumType.LogStatus.Valid, SeatManage.EnumType.LogStatus.None);
            bool error = false;
            List<int> lst = new List<int>();
            try
            {
                if (vlists.Count > 0)
                {
                    foreach (ViolationRecordsLogInfo vlist in vlists)
                    {
                        vlist.Flag = SeatManage.EnumType.LogStatus.Fail;
                        seatService.UpdateViolationRecordsLog(vlist);
                        //SeatManage.ClassModel.ReaderNoticeInfo rni = new SeatManage.ClassModel.ReaderNoticeInfo();
                        //rni.CardNo = vlist.CardNo;
                        //rni.Note = string.Format("{0}记录的违规，{1}，被管理员手动移除", vlist.EnterOutTime, vlist.Remark);
                        //SeatManage.Bll.T_SM_ReaderNotice.AddReaderNotice(rni);
                    }
                }
                return vlists.Count;
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("修改违规记录失败：" + ex.Message);
                return -1;
            }
           
        }
    }
}
