using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IPocketBespeak;

namespace TcpClient_BespeakSeat
{
    public class TcpClient_QueryLogs : IQueryLogs
    {
        public List<SeatManage.ClassModel.ViolationRecordsLogInfo> GetViolateDiscipline(AMS.Model.AMS_School school, string cardNo, string readingRoomID, int queryDate)
        {
            SMS.BespeakServerProxy.BespeakServerProxy bespeakProxy = new SMS.BespeakServerProxy.BespeakServerProxy(school.ConnectionString);
            return bespeakProxy.GetViolateDiscipline(cardNo, readingRoomID, queryDate);
        }

        public List<SeatManage.ClassModel.EnterOutLogInfo> GetEnterOutLogs(AMS.Model.AMS_School school, string cardNo, string readingRoomID, int queryDate)
        {
            SMS.BespeakServerProxy.BespeakServerProxy bespeakProxy = new SMS.BespeakServerProxy.BespeakServerProxy(school.ConnectionString);
            return bespeakProxy.GetEnterOutLogs(cardNo, readingRoomID, queryDate);
        }

        public List<SeatManage.ClassModel.BespeakLogInfo> GetBookLogs(AMS.Model.AMS_School school, string cardNo, string readingRoomID, int queryDate)
        {
            SMS.BespeakServerProxy.BespeakServerProxy bespeakProxy = new SMS.BespeakServerProxy.BespeakServerProxy(school.ConnectionString);
            return bespeakProxy.GetBookLogs(cardNo, readingRoomID, queryDate);
        }

        public bool UpdateBookLogsState(AMS.Model.AMS_School school, int bookNo)
        {
            SMS.BespeakServerProxy.BespeakServerProxy bespeakProxy = new SMS.BespeakServerProxy.BespeakServerProxy(school.ConnectionString);
            return bespeakProxy.UpdateBookLogsState(bookNo);
        }

        public List<SeatManage.ClassModel.BlackListInfo> GetBlackList(AMS.Model.AMS_School school, string cardNo, int queryDate)
        {
            SMS.BespeakServerProxy.BespeakServerProxy bespeakProxy = new SMS.BespeakServerProxy.BespeakServerProxy(school.ConnectionString);
            return bespeakProxy.GetBlackList(cardNo, queryDate);
        }

        public List<SeatManage.ClassModel.ReadingRoomInfo> GetAllReadingRoomInfo(AMS.Model.AMS_School school)
        {
            SMS.BespeakServerProxy.BespeakServerProxy bespeakProxy = new SMS.BespeakServerProxy.BespeakServerProxy(school.ConnectionString);
            return bespeakProxy.GetAllReadingRoomInfo();
        }
    }
}
