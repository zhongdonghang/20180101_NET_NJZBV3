using System;
using System.Collections.Generic;
using SeatManage.ClassModel;

namespace SeatManage.PocketBespeakBllServiceV2
{
    public partial class PocketBespeakBllService : IPocketBespeakBllServiceV2.IPocketBespeakBllService
    {
        /// <summary>
        /// 获取违规记录信息
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="readingRoomID">阅览室编号</param>
        /// <param name="enterDate">记录违规日期</param>
        /// <returns></returns>
        public List<ViolationRecordsLogInfo> GetViolateDiscipline(string cardNo, string readingRoomID, int queryDate)
        {
            List<ViolationRecordsLogInfo> logs = new List<ViolationRecordsLogInfo>();
            DateTime endDate = DateTime.Now;
            DateTime begDate = endDate.AddDays(-queryDate);
            return seatManage.GetViolationRecordsLogs(cardNo, readingRoomID, begDate.ToString(), endDate.ToString(), SeatManage.EnumType.LogStatus.None, SeatManage.EnumType.LogStatus.None);
        }

        /// <summary>
        /// 获取进出记录信息
        /// </summary>
        /// <param name="enterDate">学号</param>
        /// <param name="readingRoomID">阅览室编号</param>
        /// <param name="cardNo">日期</param>
        public List<EnterOutLogInfo> GetEnterOutLogs(string cardNo, string readingRoomID, int queryDate)
        {
            DateTime endDate = DateTime.Now;
            DateTime begDate = endDate.AddDays(-queryDate);
            return seatManage.GetEnterOutLogs(cardNo, readingRoomID, null, begDate.ToString(), endDate.ToString());
        }

        /// <summary>
        /// 获取查询时间内的预约记录
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="readingRoomID">阅览室编号</param>
        /// <param name="queryDate">查询日期</param>
        /// <param name="connectionString">链接地址</param>
        /// <returns></returns>
        public List<ClassModel.BespeakLogInfo> GetBookLogs(string cardNo, string readingRoomID, int queryDate)
        {
            DateTime endDate = DateTime.Now;
            return seatManage.GetBespeakLogs(cardNo, readingRoomID, endDate, queryDate, null);
        }

        /// <summary>
        /// 更新预约记录状态
        /// </summary>
        /// <param name="bookNo"></param>
        /// <param name="bookCancelPerson"></param>
        /// <param name="bookState"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public bool UpdateBookLogsState(int bookNo)
        {
            SeatManage.ClassModel.BespeakLogInfo model = seatManage.GetBespeaklogById(bookNo);
            if (model != null)
            {
                model.BsepeakState = SeatManage.EnumType.BookingStatus.Cencaled;
                model.CancelPerson = SeatManage.EnumType.Operation.Reader;
                model.CancelTime = DateTime.Now;
                model.Remark = "读者通过手机终端取消预约";
                int result = seatManage.UpdateBespeakLogInfo(model);
                if (result > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// 获取黑名单信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="readingRoomID"></param>
        /// <param name="queryDate"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public List<ClassModel.BlackListInfo> GetBlackList(string cardNo, int queryDate)
        {
            DateTime endDate = seatManage.GetServerDateTime();
            DateTime begDate = endDate.AddDays(-queryDate);
            try
            {
                return seatManage.GetBlacklistInfos(cardNo, SeatManage.EnumType.LogStatus.None, begDate.ToString(), endDate.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ClassModel.ReadingRoomInfo> GetAllReadingRoomInfo()
        {
            try
            {
                return seatManage.GetReadingRoomInfo(null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
