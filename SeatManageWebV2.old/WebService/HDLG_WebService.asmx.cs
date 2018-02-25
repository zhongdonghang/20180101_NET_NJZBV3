using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SeatManageWebV2.WebService
{
    /// <summary>
    /// HDLG_WebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class HDLG_WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        /// <summary>
        /// 设置座位暂离
        /// </summary>
        /// <param name="cardNo">学工号</param>
        /// <param name="clientNo">设备编号，可以不填</param>
        /// <param name="remark">备注，格式为“用户通过Web接口”“读者通过手机客户端”等</param>
        /// <returns>错误信息，执行正确不返回空消息（非null值）</returns>
        [WebMethod]
        public string SeatShortLeave(string cardNo, string clientNo, string remark)
        {
            return SeatManage.Bll.SeatOperation.SeatShortLeave(cardNo, clientNo, remark);
        }
        /// <summary>
        /// 释放座位
        /// </summary>
        /// <param name="cardNo">学工号</param>
        /// <param name="clientNo">设备编号，可以不填</param>
        /// <param name="remark">备注，格式为“用户通过Web接口”“读者通过手机客户端”等</param>
        /// <returns>错误信息，执行正确不返回空消息（非null值）</returns>
        [WebMethod]
        public string SeatLeave(string cardNo, string clientNo, string remark)
        {
            return SeatManage.Bll.SeatOperation.SeatLeave(cardNo, clientNo, remark);
        }
        /// <summary>
        /// 获取读者信息
        /// </summary>
        /// <param name="cardNo">学工号</param>
        /// <returns>XML格式的信息，格式详见接口说明</returns>
        [WebMethod]
        public string GetReaderStatus(string cardNo)
        {
            try
            {
                string result = "";
                SeatManage.ClassModel.ReaderInfo readerModel = SeatManage.Bll.EnterOutOperate.GetReaderInfo(cardNo);
                string state = "";
                string seatNo = "";
                string readingRoomName = "";
                if (!string.IsNullOrEmpty(readerModel.CardNo))
                {
                    if (readerModel.EnterOutLog != null)
                    {
                        switch (readerModel.EnterOutLog.EnterOutState)
                        {
                            case SeatManage.EnumType.EnterOutLogType.ComeBack:
                            case SeatManage.EnumType.EnterOutLogType.ContinuedTime:
                            case SeatManage.EnumType.EnterOutLogType.ReselectSeat:
                            case SeatManage.EnumType.EnterOutLogType.SelectSeat:
                            case SeatManage.EnumType.EnterOutLogType.WaitingCancel:
                            case SeatManage.EnumType.EnterOutLogType.WaitingSuccess:
                            case SeatManage.EnumType.EnterOutLogType.BookingConfirmation:
                                state = "在座";
                                seatNo = readerModel.EnterOutLog.SeatNo;
                                string rrId = readerModel.EnterOutLog.ReadingRoomNo;
                                readingRoomName = readerModel.EnterOutLog.ReadingRoomName;
                                break;
                            case SeatManage.EnumType.EnterOutLogType.Leave:
                            case SeatManage.EnumType.EnterOutLogType.None:
                            case SeatManage.EnumType.EnterOutLogType.BookingCancel:
                                state = "无座";
                                break;
                            case SeatManage.EnumType.EnterOutLogType.ShortLeave:
                                state = "暂离";
                                seatNo = readerModel.EnterOutLog.SeatNo;
                                readingRoomName = readerModel.EnterOutLog.ReadingRoomName;
                                break;
                            case SeatManage.EnumType.EnterOutLogType.Waiting:
                                state = "等待座位";
                                seatNo = readerModel.EnterOutLog.SeatNo;
                                readingRoomName = readerModel.EnterOutLog.ReadingRoomName;
                                break;
                            case SeatManage.EnumType.EnterOutLogType.BespeakWaiting:
                                state = "存在未确认预约座位";
                                seatNo = readerModel.EnterOutLog.SeatNo;
                                readingRoomName = readerModel.EnterOutLog.ReadingRoomName;
                                break;
                        }
                    }
                    result = string.Format("<StuState><Student Name='{0}' CardNo='{1}' RoomName='{2}'  SeatNo='{3}' Status='{4}'></Student></StuState>", readerModel.Name, readerModel.CardNo, readingRoomName, seatNo, state);

                }
                else
                {
                    result = "<StuState><Student Name='' CardNo='' RoomName=''  SeatNo='' Status=''></Student></StuState>";
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
