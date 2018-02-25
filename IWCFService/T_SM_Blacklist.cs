using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;
using SeatManage.EnumType;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        #region  黑名单记录相关操作
        [OperationContract]
        BlackListInfo GetBlistList(string ID);
        /// <summary>
        /// 查询有效的黑名单记录
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="roomNum">阅览室编号</param>
        /// <returns></returns>
        [OperationContract]
        List<BlackListInfo> GetBlacklistInfo(string cardNo);
        /// <summary>
        /// 根据条件查询黑名单记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="roonNum"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [OperationContract]
        List<BlackListInfo> GetBlacklistInfos(string cardNo, LogStatus status, string beginDate, string endDate);

        /// <summary>
        /// 根据学号模糊查询黑名单记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="status"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [OperationContract]
        List<BlackListInfo> GetBlacklistInfos_ByFuzzySearch(string cardNo, LogStatus status, string beginDate, string endDate);
        /// <summary>
        /// 添加黑名单记录
        /// </summary>
        /// <param name="blacklist"></param>
        [OperationContract]
        int AddBlacklist(BlackListInfo blacklist);
        /// <summary>
        /// 更新黑名单记录
        /// </summary>
        /// <param name="blacklist"></param>
        [OperationContract]
        int UpdateBlacklist(BlackListInfo blacklist);
        /// <summary>
        /// 根据条件删除黑名单记录
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="roomNum">阅览室号</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        [OperationContract]
        int DeleteBlacklist(string cardNo, string roomNum, string beginDate, string endDate);


        #endregion
    }
}
