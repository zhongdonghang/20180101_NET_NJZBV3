using System.Collections.Generic;
using System.ServiceModel;
using SeatManage.ClassModel;

namespace SeatManage.IPocketBespeakBllServiceV2
{
    public partial interface IPocketBespeakBllService
    {
        /// <summary>
        /// 获取违规记录信息
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="readingRoomID">阅览室编号</param>
        /// <param name="enterDate">记录违规日期</param>
        /// <returns></returns>
        [OperationContract]
        List<ViolationRecordsLogInfo> GetViolateDiscipline(string cardNo, string readingRoomID, int queryDate);

        /// <summary>
        /// 获取进出记录信息
        /// </summary>
        /// <param name="enterDate">学号</param>
        /// <param name="readingRoomID">阅览室编号</param>
        /// <param name="cardNo">日期</param>
        [OperationContract]
        List<EnterOutLogInfo> GetEnterOutLogs(string cardNo, string readingRoomID, int queryDate);

        /// <summary>
        /// 获取查询时间内的预约记录
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="readingRoomID">阅览室编号</param>
        /// <param name="queryDate">查询日期</param>
        /// <param name="connectionString">链接地址</param>
        /// <returns></returns>
        [OperationContract]
        List<ClassModel.BespeakLogInfo> GetBookLogs(string cardNo, string readingRoomID, int queryDate);

        /// <summary>
        /// 更新预约记录状态
        /// </summary>
        /// <param name="bookNo"></param>
        /// <param name="bookCancelPerson"></param>
        /// <param name="bookState"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateBookLogsState(int bookNo);

        /// <summary>
        /// 获取黑名单信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="readingRoomID"></param>
        /// <param name="queryDate"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        [OperationContract]
        List<ClassModel.BlackListInfo> GetBlackList(string cardNo, int queryDate);
        /// <summary>
        /// 获取所有的阅览室信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ClassModel.ReadingRoomInfo> GetAllReadingRoomInfo();
    }
}
