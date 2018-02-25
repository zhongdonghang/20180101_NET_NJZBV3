using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SeatManage.IWeChatWcfService
{
    public partial interface IWeChatService
    {
        /// <summary>
        /// 获取进出记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        [OperationContract]
        string GetEnterOutLog(string studentNo, int pageIndex, int pageSize);
        /// <summary>
        /// 获取预约记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        [OperationContract]
        string GetBesapsekLog(string studentNo, int pageIndex, int pageSize);
        /// <summary>
        /// 获取黑名单记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        [OperationContract]
        string GetBlacklist(string studentNo, int pageIndex, int pageSize);
        /// <summary>
        /// 获取违规记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        [OperationContract]
        string GetViolationLog(string studentNo, int pageIndex, int pageSize);
    }
}
