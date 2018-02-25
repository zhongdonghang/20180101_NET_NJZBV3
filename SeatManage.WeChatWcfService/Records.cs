using System;
using System.Collections.Generic;
using SeatManage.AppJsonModel;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.IWeChatWcfService;
using SeatManage.SeatManageComm;
using WcfServiceForSeatManage;

namespace SeatManage.WeChatWcfService
{
    public partial class WeChatService : IWeChatService
    {

        /// <summary>
        /// 获取进出记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        public string GetEnterOutLog(string studentNo, int pageIndex, int pageSize)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                if (string.IsNullOrEmpty(studentNo))
                {
                    result.Result = false;
                    result.Msg = "学号不能为空！";
                    return JSONSerializer.Serialize(result);
                }
                if (pageIndex < 0 || pageSize < 0)
                {
                    result.Result = false;
                    result.Msg = "页数和每页显示数目必须大于等于0";
                    return JSONSerializer.Serialize(result);
                }
                List<EnterOutLogInfo> enterOutLogInfos = seatManageDateService.GetEnterOutLogsByPage(studentNo, pageIndex, pageSize);
                List<AJM_EnterOutLog> ajmEnterOutLogs = new List<AJM_EnterOutLog>();
                for (int i = 0; i < enterOutLogInfos.Count; i++)
                {
                    AJM_EnterOutLog ajmEnterOutLog = new AJM_EnterOutLog();
                    ajmEnterOutLog = new AJM_EnterOutLog();
                    ajmEnterOutLog.EnterOutState = enterOutLogInfos[i].EnterOutState.ToString();
                    ajmEnterOutLog.EnterOutTime = enterOutLogInfos[i].EnterOutTime.ToString("yyyy-MM-d HH:mm:ss");
                    ajmEnterOutLog.Id = enterOutLogInfos[i].EnterOutLogID;
                    ajmEnterOutLog.Remark = enterOutLogInfos[i].Remark;
                    ajmEnterOutLog.RoomName = enterOutLogInfos[i].ReadingRoomName;
                    ajmEnterOutLog.RoomNo = enterOutLogInfos[i].ReadingRoomNo;
                    ajmEnterOutLog.SeatNo = enterOutLogInfos[i].SeatNo;
                    ajmEnterOutLog.SeatShortNo = enterOutLogInfos[i].ShortSeatNo;
                    ajmEnterOutLogs.Add(ajmEnterOutLog);
                }
                if (ajmEnterOutLogs.Count < 1)
                {
                    result.Result = false;
                    result.Msg = "暂时没有进出记录！";
                    return JSONSerializer.Serialize(result);
                }
                result.Result = true;
                result.Msg = JSONSerializer.Serialize(ajmEnterOutLogs);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取进出记录遇到异常：{0}", ex.Message));
                result.Result = false;
                result.Msg = "获取进出记录执行异常！";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取预约记录
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        public string GetBesapsekLog(string studentNo, int pageIndex, int pageSize)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                if (string.IsNullOrEmpty(studentNo))
                {
                    result.Result = false;
                    result.Msg = "学号不能为空！";
                    return JSONSerializer.Serialize(result);
                }
                if (pageIndex < 0 || pageSize < 0)
                {
                    result.Result = false;
                    result.Msg = "页数和每页显示数目必须大于等于0";
                    return JSONSerializer.Serialize(result);
                }
                List<BespeakLogInfo> logInfos = seatManageDateService.GetBespeakLogsByPage(studentNo, pageIndex, pageSize);
                List<AJM_BespeakLog> ajmBespeakLogs = new List<AJM_BespeakLog>();
                for (int i = 0; i < logInfos.Count; i++)
                {
                    AJM_BespeakLog ajmBespeakLog = new AJM_BespeakLog();
                    ajmBespeakLog.Id = logInfos[i].BsepeaklogID;
                    ajmBespeakLog.BookTime = logInfos[i].BsepeakTime.ToString("yyyy-MM-dd HH:mm:ss");
                    ajmBespeakLog.IsValid = logInfos[i].BsepeakState == BookingStatus.Waiting;
                    ajmBespeakLog.Remark = logInfos[i].Remark;
                    ajmBespeakLog.RoomName = logInfos[i].ReadingRoomName;
                    ajmBespeakLog.RoomNo = logInfos[i].ReadingRoomNo;
                    ajmBespeakLog.SeatShortNo = logInfos[i].ShortSeatNum;
                    ajmBespeakLog.SeatNo = logInfos[i].SeatNo;
                    ajmBespeakLog.SubmitDateTime = logInfos[i].SubmitTime.ToString("yyyy-MM-dd HH:mm:ss");
                    ajmBespeakLog.CancelTime = logInfos[i].CancelTime.ToString("yyyy-MM-dd HH:mm:ss");
                    ajmBespeakLogs.Add(ajmBespeakLog);
                }
                result.Result = true;
                result.Msg = JSONSerializer.Serialize(ajmBespeakLogs);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取预约记录遇到异常：{0}", ex.Message));
                result.Result = false;
                result.Msg = "获取预约记录执行异常！";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取黑名单记录
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        public string GetBlacklist(string studentNo, int pageIndex, int pageSize)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                if (string.IsNullOrEmpty(studentNo))
                {
                    result.Result = false;
                    result.Msg = "学号不能为空！";
                    return JSONSerializer.Serialize(result);
                }
                if (pageIndex < 0 || pageSize < 0)
                {
                    result.Result = false;
                    result.Msg = "页数和每页显示数目必须大于等于0";
                    return JSONSerializer.Serialize(result);
                }
                List<BlackListInfo> blackListInfos = seatManageDateService.GetBlacklistInfosByPage(studentNo, pageIndex, pageSize);
                List<AJM_BlacklistLog> ajmBlacklistLogs = new List<AJM_BlacklistLog>();
                for (int i = 0; i < blackListInfos.Count; i++)
                {
                    AJM_BlacklistLog ajmBlacklistLog = new AJM_BlacklistLog();
                    ajmBlacklistLog.AddTime = blackListInfos[i].AddTime.ToString("yyyy-MM-dd HH:mm:ss");
                    ajmBlacklistLog.StudentNo = blackListInfos[i].CardNo;
                    ajmBlacklistLog.ID = blackListInfos[i].ID;
                    ajmBlacklistLog.IsValid = blackListInfos[i].BlacklistState == LogStatus.Valid;
                    ajmBlacklistLog.OutBlacklistMode = blackListInfos[i].OutBlacklistMode.ToString();
                    ajmBlacklistLog.OutTime = blackListInfos[i].OutTime.ToString("yyyy-MM-dd HH:mm:ss");
                    ajmBlacklistLog.ReMark = blackListInfos[i].ReMark;
                    ajmBlacklistLogs.Add(ajmBlacklistLog);
                }
                result.Result = true;
                result.Msg = JSONSerializer.Serialize(ajmBlacklistLogs);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("查询黑名单遇到异常：{0}", ex.Message));
                result.Result = false;
                result.Msg = "查询黑名单执行遇到异常！";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取违规记录
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        public string GetViolationLog(string studentNo, int pageIndex, int pageSize)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                if (string.IsNullOrEmpty(studentNo))
                {
                    result.Result = false;
                    result.Msg = "学号不能为空";
                    JSONSerializer.Serialize(result);
                }
                if (pageIndex < 0 || pageSize < 0)
                {
                    result.Result = false;
                    result.Msg = "页数和每页显示数目必须大于等于0";
                    return JSONSerializer.Serialize(result);
                }
                List<ViolationRecordsLogInfo> violationRecordsLogInfos =
                    seatManageDateService.GetViolationRecordsLogsByPage(studentNo, pageIndex, pageSize);
                List<AJM_ViolationRecordsLogInfo> ajmViolationRecordsLogInfos = new List<AJM_ViolationRecordsLogInfo>();
                for (int i = 0; i < violationRecordsLogInfos.Count; i++)
                {
                    AJM_ViolationRecordsLogInfo ajmViolationRecordsLogInfo = new AJM_ViolationRecordsLogInfo();
                    ajmViolationRecordsLogInfo.StudentNo = violationRecordsLogInfos[i].CardNo;
                    ajmViolationRecordsLogInfo.EnterOutTime = violationRecordsLogInfos[i].EnterOutTime;
                    ajmViolationRecordsLogInfo.RoomName = violationRecordsLogInfos[i].ReadingRoomName;
                    ajmViolationRecordsLogInfo.Remark = violationRecordsLogInfos[i].Remark;
                    ajmViolationRecordsLogInfo.SeatNo = violationRecordsLogInfos[i].SeatID;
                    ajmViolationRecordsLogInfos.Add(ajmViolationRecordsLogInfo);
                }
                result.Result = true;
                result.Msg = JSONSerializer.Serialize(ajmViolationRecordsLogInfos);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("查询违规记录遇到异常：{0}", ex.Message));
                result.Result = false;
                result.Msg = "查询违规记录执行遇到异常！";
                return JSONSerializer.Serialize(result);
            }
        }
    }
}
