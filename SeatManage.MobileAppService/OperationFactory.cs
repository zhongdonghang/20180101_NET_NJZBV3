using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketMsgData;

namespace SeatManage.MobileAppService
{
    /// <summary>
    /// 方法执行工厂。用于socket客户端侦听到请求时，执行对应的方法。
    /// </summary>
    public class OperationFactory
    {

        static SeatManage.ServiceHelper.ServiceHelper appService = new ServiceHelper.ServiceHelper();
        /// <summary>
        /// 根据requestMethod，执行相应的方法，并从param中取参数。
        /// </summary>
        /// <param name="param">方法执行所需要的参数列表</param>
        /// <param name="requestMethod">要执行的方法</param>
        /// <returns>返回序列化后的字符串</returns>
        public static string Execute(List<object> param, RequestMethodEnum requestMethod)
        {
            switch (requestMethod)
            {
                case RequestMethodEnum.CancelBespeakLog:
                    {
                        int bespeakId = (int)param[0];
                        string remark = (string)param[1];
                        return appService.CancelBespeakLog(bespeakId, remark);
                    }
                case RequestMethodEnum.ChangeSeat:
                    {
                        string cardNo = (string)param[0];
                        string seatNo = (string)param[1];
                        string readingRoom = (string)param[2];
                        return appService.ChangeSeat(cardNo, seatNo, readingRoom);
                    }
                case RequestMethodEnum.FreeSeat:
                    {
                        string cardNo = (string)param[0];
                        return appService.FreeSeat(cardNo);
                    }
                case RequestMethodEnum.GetAllRoomSeatUsedInfo:
                    return appService.GetAllRoomSeatUsedInfo();
                case RequestMethodEnum.GetOpenBespeakRooms:
                    {
                        string strDate = (string)param[0];
                        return appService.GetOpenBespeakRooms(strDate);
                    }
                case RequestMethodEnum.GetReaderActualTimeRecord:
                    {
                        string cardNo = (string)param[0];
                        string getItemsParameter = (string)param[1];
                        return appService.GetReaderActualTimeRecord(cardNo, getItemsParameter);
                    }
                case RequestMethodEnum.GetReaderBespeakRecord:
                    {
                        string cardNo = (string)param[0];
                        int pageIndex = (int)param[1];
                        int pageSize = (int)param[2];
                        return appService.GetReaderBespeakRecord(cardNo, pageIndex, pageSize);
                    }
                case RequestMethodEnum.GetReaderBlacklistRecord:
                    {
                        string cardNo = (string)param[0];
                        int pageIndex = (int)param[1];
                        int pageSize = (int)param[2];
                        return appService.GetReaderBlacklistRecord(cardNo, pageIndex, pageSize);
                    }
                case RequestMethodEnum.GetReaderChooseSeatRecord:
                    {
                        string cardNo = (string)param[0];
                        int pageIndex = (int)param[1];
                        int pageSize = (int)param[2];
                        return appService.GetReaderChooseSeatRecord(cardNo, pageIndex, pageSize);
                    }
                case RequestMethodEnum.GetReaderAccount:
                    {
                        string cardNum = (string)param[0];
                        string password = (string)param[1];
                        return appService.GetReaderAccount(cardNum, password);
                    }
                case RequestMethodEnum.GetScanCodeSeatInfo:
                    {
                        string scanResult = (string)param[0];
                        string cardNo = (string)param[1];
                        return appService.GetScanCodeSeatInfo(scanResult, cardNo);
                    }
                case RequestMethodEnum.GetSeatsBespeakInfoByRoomNum:
                    {
                        string roomNum = (string)param[0];
                        string date = (string)param[1];
                        return appService.GetSeatsBespeakInfoByRoomNum(roomNum, date);
                    }
                case RequestMethodEnum.GetViolateDiscipline:
                    {
                        string cardNo = (string)param[0];
                        int pageIndex = (int)param[1];
                        int pageSize = (int)param[2];
                        return appService.GetViolateDiscipline(cardNo, pageIndex, pageSize);
                    }
                case RequestMethodEnum.ShortLeave:
                    {
                        string cardNo = (string)param[0];
                        return appService.ShortLeave(cardNo);
                    }
                case RequestMethodEnum.SubmitBespeakInfo:
                    {
                        string cardNo = (string)param[0];
                        string roomNum = (string)param[1];
                        string seatNum = (string)param[2];
                        string bespeakDatetime = (string)param[3];
                        string remark = (string)param[4];
                        return appService.SubmitBespeakInfo(cardNo, roomNum, seatNum, bespeakDatetime, remark);
                    }
                case RequestMethodEnum.SubmitBespeakInfoCustomTime:
                    {
                        string cardNo = (string)param[0];
                        string roomNum = (string)param[1];
                        string seatNum = (string)param[2];
                        string bespeakDatetime = (string)param[3];
                        string remark = (string)param[4];
                        return appService.SubmitBespeakInfoCustomTime(cardNo, roomNum, seatNum, bespeakDatetime, remark);
                    }
                case RequestMethodEnum.GetReaderNotice:
                    {
                        string cardNo = (string)param[0];
                        int pageIndex = (int)param[1];
                        int pageSize = (int)param[2];
                        return appService.GetReaderNoticeList(cardNo, pageIndex, pageSize);
                    }
                case RequestMethodEnum.GetReaderSeatState:
                    {
                        string cardNo = (string)param[0];
                        return appService.GetReaderSeatState(cardNo);
                    }
                default:
                    return null;
            }

        }
    }
}
