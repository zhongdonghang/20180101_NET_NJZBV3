using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TcpClient_BespeakSeat
{
    public class TcpClient_BespeakSeat : SeatManage.IPocketBespeak.IBespeakSeatListForm
    {
        public List<SeatManage.ClassModel.ReadingRoomInfo> GetCanBespeakReaderRoomInfo(AMS.Model.AMS_School school, DateTime bespeakDate)
        {
            SMS.BespeakServerProxy.BespeakServerProxy bespeakProxy = new SMS.BespeakServerProxy.BespeakServerProxy(school.ConnectionString);
            return bespeakProxy.GetCanBespeakReaderRoomInfo(bespeakDate);
        }

        public List<SeatManage.ClassModel.Seat> GetBookSeatList(AMS.Model.AMS_School school, DateTime bespeakDate, string RoomId)
        {
            SMS.BespeakServerProxy.BespeakServerProxy bespeakProxy = new SMS.BespeakServerProxy.BespeakServerProxy(school.ConnectionString);
            return bespeakProxy.GetBookSeatList(bespeakDate, RoomId);
        }

        public string SubmitBespeakInfo(AMS.Model.AMS_School school, SeatManage.ClassModel.BespeakLogInfo bespeakInfo)
        {
            SMS.BespeakServerProxy.BespeakServerProxy bespeakProxy = new SMS.BespeakServerProxy.BespeakServerProxy(school.ConnectionString);
            return bespeakProxy.SubmitBespeakInfo(bespeakInfo);
        }


        public SeatManage.ClassModel.BespeakSeatModel.ScanCodeViewModel GetScanCodeSeatInfo(AMS.Model.AMS_School school, string cardNo, string seatNum, string readingRoomNum)
        {
            SMS.BespeakServerProxy.BespeakServerProxy bespeakProxy = new SMS.BespeakServerProxy.BespeakServerProxy(school.ConnectionString);
            return bespeakProxy.GetScanCodeSeatInfo(cardNo, seatNum, readingRoomNum);
        }

        public string ChangeSeat(AMS.Model.AMS_School school, string cardNo, string seatNum, string readingRoomNum)
        {
            SMS.BespeakServerProxy.BespeakServerProxy bespeakProxy = new SMS.BespeakServerProxy.BespeakServerProxy(school.ConnectionString);
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
