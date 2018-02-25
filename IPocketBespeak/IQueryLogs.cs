using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
namespace SeatManage.IPocketBespeak
{
    public interface IQueryLogs
    {
        /// <summary>
        /// 获取违规记录信息
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="readingRoomID">阅览室编号</param>
        /// <param name="enterDate">记录违规日期</param>
        /// <returns></returns>
        List<ViolationRecordsLogInfo> GetViolateDiscipline(AMS.Model.AMS_School school, string cardNo, string readingRoomID, int queryDate);

        /// <summary>
        /// 获取进出记录信息
        /// </summary>
        /// <param name="enterDate">学号</param>
        /// <param name="readingRoomID">阅览室编号</param>
        /// <param name="cardNo">日期</param>
        List<EnterOutLogInfo> GetEnterOutLogs(AMS.Model.AMS_School school, string cardNo, string readingRoomID, int queryDate);

        /// <summary>
        /// 获取查询时间内的预约记录
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="readingRoomID">阅览室编号</param>
        /// <param name="queryDate">查询日期</param>
        /// <param name="connectionString">链接地址</param>
        /// <returns></returns>
        List<ClassModel.BespeakLogInfo> GetBookLogs(AMS.Model.AMS_School school, string cardNo, string readingRoomID, int queryDate);

        /// <summary>
        /// 更新预约记录状态
        /// </summary>
        /// <param name="bookNo"></param>
        /// <param name="bookCancelPerson"></param>
        /// <param name="bookState"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        bool UpdateBookLogsState(AMS.Model.AMS_School school, int bookNo);

        /// <summary>
        /// 获取黑名单信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="readingRoomID"></param>
        /// <param name="queryDate"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        List<ClassModel.BlackListInfo> GetBlackList(AMS.Model.AMS_School school, string cardNo, int queryDate);
        /// <summary>
        /// 获取所有的阅览室
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        List<ClassModel.ReadingRoomInfo> GetAllReadingRoomInfo(AMS.Model.AMS_School school);
    }
}
