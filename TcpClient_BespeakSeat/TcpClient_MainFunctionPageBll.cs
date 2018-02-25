using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IPocketBespeak;

namespace TcpClient_BespeakSeat
{
    public class TcpClient_MainFunctionPageBll : IMainFunctionPageBll
    {
        public string SetShortLeave(AMS.Model.AMS_School school, SeatManage.ClassModel.ReaderInfo reader)
        {
            SMS.BespeakServerProxy.BespeakServerProxy bespeakProxy = new SMS.BespeakServerProxy.BespeakServerProxy(school.ConnectionString);
            return bespeakProxy.SetShortLeave(reader.CardNo);
        }

        public string FreeSeat(AMS.Model.AMS_School school, SeatManage.ClassModel.ReaderInfo reader)
        {
            SMS.BespeakServerProxy.BespeakServerProxy bespeakProxy = new SMS.BespeakServerProxy.BespeakServerProxy(school.ConnectionString);
            return bespeakProxy.FreeSeat(reader.CardNo);
        }

        public Dictionary<string, SeatManage.ClassModel.ReadingRoomSeatUsedState_Ex> GetAllRoomSeatUsedState(AMS.Model.AMS_School school)
        {
            SMS.BespeakServerProxy.BespeakServerProxy bespeakProxy = new SMS.BespeakServerProxy.BespeakServerProxy(school.ConnectionString);
            return bespeakProxy.GetAllRoomSeatUsedState();
        }

        public SeatManage.ClassModel.ReaderInfo GetReaderInfo(AMS.Model.AMS_School school, string cardNo)
        {
            SMS.BespeakServerProxy.BespeakServerProxy bespeakProxy = new SMS.BespeakServerProxy.BespeakServerProxy(school.ConnectionString);
            return bespeakProxy.GetReaderInfo(cardNo);
        }

        public string DelaySeatUsedTime(AMS.Model.AMS_School school, SeatManage.ClassModel.ReaderInfo reader)
        {
            SMS.BespeakServerProxy.BespeakServerProxy bespeakProxy = new SMS.BespeakServerProxy.BespeakServerProxy(school.ConnectionString);
            return bespeakProxy.DelaySeatUsedTime(reader);
        }
    }
}
