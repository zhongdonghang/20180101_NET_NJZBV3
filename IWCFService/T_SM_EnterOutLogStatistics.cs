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
        /// 添加一条统计记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        HandleResult AddEnterOutStatistics(EnterOutLogStatistics model);
        /// <summary>
        /// 查询统计记录，NULL为查询全部
        /// </summary>
        /// <param name="roomNo"></param>
        /// <param name="cardNo"></param>
        /// <param name="SeatNo"></param>
        /// <returns></returns>
        [OperationContract]
        List<EnterOutLogStatistics> GetEnterOutLogStatisticsList(List<string> roomNo, string cardNo, string SeatNo);
        /// <summary>
        /// 按日期查询
        /// </summary>
        /// <param name="roomNo"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        [OperationContract]
        List<EnterOutLogStatistics> GetEnterOutLogStatisticsListByDate(string roomNo, string starttime, string endtime);
        /// <summary>
        /// 获取最后一条记录
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        EnterOutLogStatistics GetLastLog();

    }
}
