using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AMS.IBllService
{
    public partial interface IAdvertManageBllService
    {
        /// <summary>
        /// 添加使用记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddNewSeatUsage(AMS.Model.SMS_SeatUsage model);
        /// <summary>
        /// 获取最后的日期
        /// </summary>
        /// <param name="SchoolNum"></param>
        /// <returns></returns>
        [OperationContract]
        DateTime LastSeatUsageUploadDate(string SchoolNum);
        /// <summary>
        /// 获取记录列表
        /// </summary>
        /// <param name="SchoolNum"></param>
        /// <param name="stratDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [OperationContract]
        List<AMS.Model.SMS_SeatUsage> GetSeatUsageList(string SchoolNum, DateTime stratDate, DateTime endDate);
    }
}
