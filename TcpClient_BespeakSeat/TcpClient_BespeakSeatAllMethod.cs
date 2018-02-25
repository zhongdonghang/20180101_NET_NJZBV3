using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IPocketBespeak;

namespace TcpClient_BespeakSeat
{
    public class TcpClient_BespeakSeatAllMethod : SeatManage.IPocketBespeak.IBespeakSeatListForm, SeatManage.IPocketBespeak.ILogin, IMainFunctionPageBll, IQueryLogs, System.IDisposable
    {
        SMS.BespeakServerProxy.BespeakServerProxy bespeakProxy;
        public TcpClient_BespeakSeatAllMethod(AMS.Model.AMS_School school)
        {
            bespeakProxy = new SMS.BespeakServerProxy.BespeakServerProxy(school.ConnectionString);
        }
        public void Dispose()
        {
            bespeakProxy.Dispose();
        }

        public List<SeatManage.ClassModel.ReadingRoomInfo> GetCanBespeakReaderRoomInfo(AMS.Model.AMS_School school, DateTime bespeakDate)
        {
            return bespeakProxy.GetCanBespeakReaderRoomInfo(bespeakDate);
        }

        public List<SeatManage.ClassModel.Seat> GetBookSeatList(AMS.Model.AMS_School school, DateTime bespeakDate, string RoomId)
        {
            return bespeakProxy.GetBookSeatList(bespeakDate, RoomId);
        }

        public string SubmitBespeakInfo(AMS.Model.AMS_School school, SeatManage.ClassModel.BespeakLogInfo bespeakInfo)
        {
            return bespeakProxy.SubmitBespeakInfo(bespeakInfo);
        }

        public List<AMS.Model.AMS_School> GetAllSchoolFromLocal()
        {
            return AMS.ServiceProxy.ICallBackInfoService.GetSchoolList();
        }

        public SeatManage.ClassModel.ReaderInfo CheckAndGetReaderInfo(SeatManage.ClassModel.UserInfo user, AMS.Model.AMS_School school)
        {
            return bespeakProxy.CheckAndGetReaderInfo(user);
        }

        public SeatManage.ClassModel.ReaderInfo GetReaderInfoByCardNo(string cardNo, AMS.Model.AMS_School school)
        {
            return bespeakProxy.GetReaderInfoByCardNo(cardNo);
        }

        public AMS.Model.AMS_School GetSingleSchoolInfo(string schoolId)
        {
            return AMS.ServiceProxy.AMS_SchoolProxy.GetSchoolById(int.Parse(schoolId));
        }

        public SeatManage.ClassModel.ReaderInfo GetReaderInfoByCardNofalse(string cardNo, AMS.Model.AMS_School school)
        {
            return bespeakProxy.GetReaderInfoByCardNofalse(cardNo);
        }
        public string SetShortLeave(AMS.Model.AMS_School school, SeatManage.ClassModel.ReaderInfo reader)
        {
            return bespeakProxy.SetShortLeave(reader.CardNo);
        }

        public string FreeSeat(AMS.Model.AMS_School school, SeatManage.ClassModel.ReaderInfo reader)
        {
            return bespeakProxy.FreeSeat(reader.CardNo);
        }

        public Dictionary<string, SeatManage.ClassModel.ReadingRoomSeatUsedState_Ex> GetAllRoomSeatUsedState(AMS.Model.AMS_School school)
        {
            return bespeakProxy.GetAllRoomSeatUsedState();
        }

        public SeatManage.ClassModel.ReaderInfo GetReaderInfo(AMS.Model.AMS_School school, string cardNo)
        {
            return bespeakProxy.GetReaderInfo(cardNo);
        }

        public string DelaySeatUsedTime(AMS.Model.AMS_School school, SeatManage.ClassModel.ReaderInfo reader)
        {
            return bespeakProxy.DelaySeatUsedTime(reader);
        }
        public List<SeatManage.ClassModel.ViolationRecordsLogInfo> GetViolateDiscipline(AMS.Model.AMS_School school, string cardNo, string readingRoomID, int queryDate)
        {
            return bespeakProxy.GetViolateDiscipline(cardNo, readingRoomID, queryDate);
        }

        public List<SeatManage.ClassModel.EnterOutLogInfo> GetEnterOutLogs(AMS.Model.AMS_School school, string cardNo, string readingRoomID, int queryDate)
        {
            return bespeakProxy.GetEnterOutLogs(cardNo, readingRoomID, queryDate);
        }

        public List<SeatManage.ClassModel.BespeakLogInfo> GetBookLogs(AMS.Model.AMS_School school, string cardNo, string readingRoomID, int queryDate)
        {
            return bespeakProxy.GetBookLogs(cardNo, readingRoomID, queryDate);
        }

        public bool UpdateBookLogsState(AMS.Model.AMS_School school, int bookNo)
        {
            return bespeakProxy.UpdateBookLogsState(bookNo);
        }

        public List<SeatManage.ClassModel.BlackListInfo> GetBlackList(AMS.Model.AMS_School school, string cardNo, int queryDate)
        {
            return bespeakProxy.GetBlackList(cardNo, queryDate);
        }

        public List<SeatManage.ClassModel.ReadingRoomInfo> GetAllReadingRoomInfo(AMS.Model.AMS_School school)
        {
            return bespeakProxy.GetAllReadingRoomInfo();
        }


        public SeatManage.ClassModel.BespeakSeatModel.ScanCodeViewModel GetScanCodeSeatInfo(AMS.Model.AMS_School school, string cardNo, string seatNum, string readingRoomNum)
        {
            return bespeakProxy.GetScanCodeSeatInfo(cardNo, seatNum, readingRoomNum);
        }

        public string ChangeSeat(AMS.Model.AMS_School school, string cardNo, string seatNum, string readingRoomNum)
        {
            return bespeakProxy.ChangeSeat(cardNo, seatNum, readingRoomNum);
        }


        public List<SeatManage.ClassModel.ReadingRoomInfo> GetCanBespeakNowDayRoomInfo(AMS.Model.AMS_School school)
        {
            throw new NotImplementedException();
        }

        public List<SeatManage.ClassModel.Seat> GetNowDayBookSeatList(AMS.Model.AMS_School school, string RoomId)
        {
            throw new NotImplementedException();
        }

        public string SubmitNowDayBespeakInfo(AMS.Model.AMS_School school, SeatManage.ClassModel.BespeakLogInfo bespeakInfo)
        {
            throw new NotImplementedException();
        }


        public string ConfrimSeat(AMS.Model.AMS_School school, int bookNo)
        {
            throw new NotImplementedException();
        }
    }
}
