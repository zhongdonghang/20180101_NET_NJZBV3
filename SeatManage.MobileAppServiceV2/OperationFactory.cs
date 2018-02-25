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

        static AppServiceHelper.AppServiceHelper appService = new AppServiceHelper.AppServiceHelper();
        /// <summary>
        /// 根据requestMethod，执行相应的方法，并从param中取参数。
        /// </summary>
        /// <param name="param">方法执行所需要的参数列表</param>
        /// <param name="requestMethod">要执行的方法</param>
        /// <returns>返回序列化后的字符串</returns>
        public static string Execute(List<object> param, string requestMethod)
        {
            switch (requestMethod)
            {
                case "GetUserInfo":
                    return appService.GetUserInfo((string)param[0]);
                case "CancelBesapeak":
                    return appService.CancelBesapeak((int)param[0]);
                case "CancelBesapeakByCardNo":
                    return appService.CancelBespeakLogByCardNo((string)param[0], (string)param[1]);
                case "CancelWait":
                    return appService.CancelWait((string)param[0]);
                case "CheckUser":
                    return appService.CheckUser((string)param[0], (string)param[1]);
                case "ChangeSeat":
                    return appService.ChangeSeat((string)param[0], (string)param[1], (string)param[2]);
                case "GetAllRoomInfo":
                    return appService.GetAllRoomInfo();
                case "GetAllRoomNowState":
                    return appService.GetAllRoomNowState();
                case "GetSingleRoomOpenState":
                    return appService.GetSingleRoomOpenState((string)param[0], (string)param[1]);
                case "GetBesapsekLog":
                    return appService.GetBesapsekLog((string)param[0], (int)param[1], (int)param[2]);
                case "GetBlacklist":
                    return appService.GetBlacklist((string)param[0], (int)param[1], (int)param[2]);
                case "GetEnterOutLog":
                    return appService.GetEnterOutLog((string)param[0], (int)param[1], (int)param[2]);
                case "GetOftenSeat":
                    return appService.GetOftenSeat((string)param[0], (int)param[1], (int)param[2]);
                case "GetRandomSeat":
                    return appService.GetRandomSeat((string)param[0]);
                case "GetRoomBesapeakState":
                    return appService.GetRoomBesapeakState((string)param[0], (string)param[1]);
                case "GetRoomSeatLayout":
                    return appService.GetRoomSeatLayout((string)param[0]);
                case "GetUserNowState":
                    return appService.GetUserNowState((string)param[0]);
                case "GetUserNowStateV2":
                    return appService.GetUserNowStateV2((string)param[0], (bool)param[1]);
                case "GetViolationLog":
                    return appService.GetViolationLog((string)param[0], (int)param[1], (int)param[2]);
                case "QRcodeOperation":
                    return appService.QRcodeOperation((string)param[0], (string)param[1]);
                case "QRcodeSeatInfo":
                    return appService.QRcodeSeatInfo((string)param[0]);
                case "ReleaseSeat":
                    return appService.ReleaseSeat((string)param[0]);
                case "ShortLeave":
                    return appService.ShortLeave((string)param[0]);
                case "SubmitBesapeskSeat":
                    return appService.SubmitBesapeskSeat((string)param[0], (string)param[1], (string)param[2], (string)param[3], (bool)param[4]);
                case "GetUserInfo_WeiXin":
                    return appService.GetUserInfo_WeiXin((string)param[0]);
                case "ComeBack":
                    return appService.ComeBack((string)param[0]);
                case "DelayTime":
                    return appService.DelayTime((string)param[0]);
                case "SelectSeat":
                    return appService.SelectSeat((string)param[0], (string)param[1], (string)param[2]);
                case "WaitSeat":
                    return appService.WaitSeat((string)param[0], (string)param[1], (string)param[2]);
                case "ConfirmSeat":
                    return appService.ConfirmSeat((string)param[0]);
                case "GetCanBespeakRoomInfo":
                    return appService.GetCanBespeakRoomInfo((string)param[0]);
                case "GetSeatBespeakInfo":
                    return appService.GetSeatBespeakInfo((string)param[0], (string)param[1], (string)param[2]);
                case "GetSeatNowStatus":
                    return appService.GetSeatNowStatus((string)param[0], (string)param[1], (string)param[2]);
                case "CheckSeat":
                    return appService.CheckSeat((string)param[0]);
                case "SelectSeatByMessager":
                    return appService.SelectSeatByMessager((string)param[0], (string)param[1], (string)param[2]);
                case "GetMessageSeatStatus":
                    return appService.GetMessageSeatStatus((string)param[0], (string)param[1]);
                case "GetLibraryNowState":
                    return appService.GetLibraryNowState();
                case "GetCanBespeakRoom":
                    return appService.GetCanBespeakRoom();
                case "GetBespeakDate":
                    return appService.GetBespeakDate((string)param[0]);
                default:
                    return null;
            }
        }
    }
}
