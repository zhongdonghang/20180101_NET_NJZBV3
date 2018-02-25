using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using SeatManage.ClassModel;

namespace SeatManage.IOperationBLL
{
    [ServiceContract]
    partial interface ISeatOperationService : IExceptionService
    {
        /// <summary>
        /// 获取阅览室的当前状态
        /// </summary>
        /// <param name="roomNoList">阅览室编号</param>
        /// <returns></returns>
        [OperationContract]
        string GetRoomNowUsageStatus(List<string> roomNoList);
        /// <summary>
        /// 获取阅览室预约状态
        /// </summary>
        /// <param name="roomNoList">阅览室编号</param>
        /// <param name="bespeakDate">查询的日期</param>
        /// <returns></returns>
        [OperationContract]
        string GetRoomBesapeakStatus(List<string> roomNoList, string bespeakDate);
        /// <summary>
        /// 获取当前开闭馆状态
        /// </summary>
        /// <param name="roomNoList">阅览室编号</param>
        /// <returns></returns>
        [OperationContract]
        string GetRoomOCStatus(List<string> roomNoList);
        /// <summary>
        /// 获取当前阅览室状态布局
        /// </summary>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        [OperationContract]
        string GetRoomNowSeatLayout(string roomNo);
        /// <summary>
        /// 获取阅览室预约布局
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <param name="bespeakDate">查询的日期</param>
        /// <returns></returns>
        [OperationContract]
        string GetRoomBespeakLayout(string roomNo, string bespeakDate);
    }
}
