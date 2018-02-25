using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SeatManage.ClassModel;
using SeatManage.EnumType;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {

        /// <summary>
        /// 根据id查找违规记录
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [OperationContract]
        List<ViolationRecordsLogInfo> GetViolationRecordsLogByblacklistID(string blacklistid);
        /// <summary>
        /// 根据id查找违规记录
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [OperationContract]
        ViolationRecordsLogInfo GetViolationRecordsLog(string ID);
        /// <summary>
        /// 查询有效的违规单记录
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="roomNum">阅览室编号</param>
        /// <returns></returns>
        [OperationContract]
        List<ViolationRecordsLogInfo> GetViolationRecordsLogInfo(string cardNo, string roomNums);
        /// <summary>
        /// 根据条件查询违规记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="roonNum"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [OperationContract]
        List<ViolationRecordsLogInfo> GetViolationRecordsLogs(string cardNo, string roomNums, string beginDate, string endDate, LogStatus logstatus, LogStatus blackliststatus);
        /// <summary>
        /// 根据违规类型查询
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="roomNums"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="logstatus"></param>
        /// <param name="blackliststatus"></param>
        /// <returns></returns>
        [OperationContract]
        List<ViolationRecordsLogInfo> GetViolationRecordsLogsByType(string cardNo, string roomNums, string beginDate, string endDate, LogStatus logstatus, LogStatus blackliststatus, ViolationRecordsType vrtype);
        /// <summary>
        /// 根据条件、学号模糊查询违规记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="roonNum"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [OperationContract]
        List<ViolationRecordsLogInfo> GetViolationRecordsLogsByType_ByFuzzySearch(string cardNo, string roomNums, string beginDate, string endDate, LogStatus logstatus, LogStatus blackliststatus, ViolationRecordsType vrtype);
        /// <summary>
        /// 添加违规记录
        /// </summary>
        /// <param name="blacklist"></param>
        [OperationContract]
        HandleResult AddViolationRecordsLog(ViolationRecordsLogInfo ViolationRecordsLog);
        /// <summary>
        /// 更新违规记录
        /// </summary>
        /// <param name="blacklist"></param>
        [OperationContract]
        HandleResult UpdateViolationRecordsLog(ViolationRecordsLogInfo ViolationRecordsLog);
        /// <summary>
        /// 根据条件删除违规记录
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="roomNum">阅览室号</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        [OperationContract]
        int DeleteViolationRecordsLog(string cardNo, string roomNum, string beginDate, string endDate);
    }
}
