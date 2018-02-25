using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 获取在座时长排行榜
        /// </summary>
        /// <param name="top"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable TopSeatTimeList(int top, string startDate, string endDate, int type);
        /// <summary>
        /// 获取选座次数排行榜
        /// </summary>
        /// <param name="top"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable TopSeatCountList(int top, string startDate, string endDate, int type);
        /// <summary>
        /// 获取黑名单排行榜
        /// </summary>
        /// <param name="top"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable TopBlacklistList(int top, string startDate, string endDate, int type);
        /// <summary>
        /// 获取违规记录排行榜
        /// </summary>
        /// <param name="top"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable TopViolateDisciplineList(int top, string startDate, string endDate, int type);
    }
}
