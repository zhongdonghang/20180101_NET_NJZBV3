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
        /// 根据记录ID查询有效进出记录
        /// </summary>
        /// <param name="enterOutLogID"></param>
        /// <returns></returns>
        [OperationContract]
        EnterOutLogInfo GetEnterOutLogBakInfoById(int enterOutLogID);
        [OperationContract]
        List<EnterOutLogInfo> GetEnterOutBakLogs(string cardNo, string roomNum, string seatNo, string beginDate, string endDate);
        [OperationContract]
        List<EnterOutLogInfo> GetEnterOutBakLogsByLastID(int ID);
        [OperationContract]
        List<EnterOutLogInfo> GetEnterOutBakLogsByDate(DateTime date);
        [OperationContract]
        DateTime GetFristLogDate();
    }
}
