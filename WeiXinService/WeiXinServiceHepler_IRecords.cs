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
        /// 获取进出记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public List<AJM_EnterOutLog> GetEnterOutLog(string studentNo, int pageIndex, int pageSize, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.GetEnterOutLog(studentNo, pageIndex, pageSize);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("获取进出记录失败！");
                }
                if (!ajmResult.Result)
                {
                    throw new Exception(ajmResult.Msg);
                }
                List<AJM_EnterOutLog> ajmEnterOutLogs = JSONSerializer.Deserialize<List<AJM_EnterOutLog>>(ajmResult.Msg);
                return ajmEnterOutLogs;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取进出记录失败：{0}", ex.Message));
                return null;
            }
        }

        /// <summary>
        /// 获取预约记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public List<AJM_BespeakLog> GetBesapsekLog(string studentNo, int pageIndex, int pageSize, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.GetBesapsekLog(studentNo, pageIndex, pageSize);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("获取预约记录失败！");
                }
                if (!ajmResult.Result)
                {
                    throw new Exception(ajmResult.Msg);
                }
                List<AJM_BespeakLog> ajmBespeakLogs =JSONSerializer.Deserialize<List<AJM_BespeakLog>>(ajmResult.Msg);
                return ajmBespeakLogs;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取预约记录失败：{0}", ex.Message));
                return null;
            }
        }

        /// <summary>
        /// 获取黑名单记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public List<AJM_BlacklistLog> GetBlacklist(string studentNo, int pageIndex, int pageSize, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.GetBlacklist(studentNo, pageIndex, pageSize);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("获取黑名单记录失败！");
                }
                if (!ajmResult.Result)
                {
                    throw new Exception(ajmResult.Msg);
                }
                List<AJM_BlacklistLog> ajmBlacklistLogs = JSONSerializer.Deserialize<List<AJM_BlacklistLog>>(ajmResult.Msg);
                return ajmBlacklistLogs;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取黑名单记录失败：{0}", ex.Message));
                return null;
            }
        }

        /// <summary>
        /// 获取违规记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public List<AJM_ViolationRecordsLogInfo> GetViolationLog(string studentNo, int pageIndex, int pageSize, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.GetViolationLog(studentNo, pageIndex, pageSize);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("获取违规记录失败！");
                }
                if (!ajmResult.Result)
                {
                    throw new Exception(ajmResult.Msg);
                }
                List<AJM_ViolationRecordsLogInfo> ajmViolationRecordsLogInfos = JSONSerializer.Deserialize<List<AJM_ViolationRecordsLogInfo>>(ajmResult.Msg);
                return ajmViolationRecordsLogInfos;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取违规记录失败：{0}", ex.Message));
                return null;
            }
        }
    }
}
