using System.Collections.Generic;
using SeatManage.AppJsonModel;

namespace WeiXinService
{
    public partial interface IWeiXinService
    {
        /// <summary>
        /// 获取进出记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        List<AJM_EnterOutLog> GetEnterOutLog(string studentNo, int pageIndex, int pageSize,string schoolNo);

        /// <summary>
        /// 获取预约记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        List<AJM_BespeakLog> GetBesapsekLog(string studentNo, int pageIndex, int pageSize, string schoolNo);

        /// <summary>
        /// 获取黑名单记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        List<AJM_BlacklistLog> GetBlacklist(string studentNo, int pageIndex, int pageSize, string schoolNo);

        /// <summary>
        /// 获取违规记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        List<AJM_ViolationRecordsLogInfo> GetViolationLog(string studentNo, int pageIndex, int pageSize, string schoolNo); 
    }
}
