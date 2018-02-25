using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.JsonModel;
using System.Data.SqlClient;
using SeatManage.ClassModel;
using SeatManage.EnumType;

namespace SeatManage.ServiceHelper
{
    public partial class ServiceHelper : ISeat
    {
        /// <summary>
        /// 获取座位的预约记录
        /// </summary>
        /// <param name="seatNum"></param>
        /// <returns></returns>
        public string GetSeatBespeakInfo(string seatNum)
        {
            try
            {
                if (seatNum.Length < 9)
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "座位编号错误!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                string readingRoomNum = seatNum.Substring(0, 6);
                SeatManage.ClassModel.Seat seatInfo = new Seat();

                List<ReadingRoomInfo> rooms = seatDataService.GetReadingRoomInfo(new List<string>() { readingRoomNum });
                if (rooms.Count < 1)
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "该阅览室不存在!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (!rooms[0].SeatList.Seats.ContainsKey(seatNum))
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "该座位不存在!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                seatInfo = rooms[0].SeatList.Seats[seatNum];
                seatInfo.ReadingRoom = rooms[0];
                if (seatInfo.IsSuspended)
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "该座位暂停使用!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                List<JM_BespeakLog> jm_list = new List<JM_BespeakLog>();
                //获取可预约座位
                for (int i = 0; i <= seatInfo.ReadingRoom.Setting.SeatBespeak.BespeakBeforeDays; i++)
                {
                    List<SeatManage.ClassModel.BespeakLogInfo> list = seatDataService.GetBespeakLogInfoBySeatNo(seatNum, DateTime.Now.AddDays(i));
                    if (list.Count > 0)
                    {
                        JM_BespeakLog jm_bespeak = new JM_BespeakLog();
                        jm_bespeak.DateTime = list[0].BsepeakTime.ToString();
                        jm_bespeak.Id = list[0].BsepeaklogID;
                        jm_bespeak.IsValid = true;
                        jm_bespeak.Remark = list[0].Remark;
                        jm_bespeak.RoomName = list[0].ReadingRoomName;
                        jm_bespeak.RoomNum = list[0].ReadingRoomNo;
                        jm_bespeak.SeatId = list[0].SeatNo;
                        jm_bespeak.SeatNum = list[0].ShortSeatNum;
                        jm_bespeak.SubmitDateTime = list[0].SubmitTime.ToString();
                        jm_list.Add(jm_bespeak);
                    }
                }
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(jm_list);

            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("根据座位号获座位者预约信息遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取座位使用状态
        /// </summary>
        /// <param name="seatNum"></param>
        /// <returns></returns>
        public string GetSeatUsage(string seatNum)
        {
            try
            {
                if (seatNum.Length < 9)
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "座位编号错误!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                SeatManage.ClassModel.Seat seat = seatDataService.GetSeatInfoBySeatNum(seatNum);
                if (seat == null)
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "该座位不存在!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (seat.SeatUsedState == EnterOutLogType.Leave && seatDataService.GetBespeakLogInfoBySeatNo(seatNum, DateTime.Now).Count > 0)
                {
                    seat.SeatUsedState = EnterOutLogType.BespeakWaiting;
                }
                JM_Seat jm_seat = new JM_Seat();
                jm_seat.CanBeBespeak = seat.CanBeBespeak;
                jm_seat.HavePower = seat.HavePower;
                jm_seat.IsSuspended = seat.IsSuspended;
                jm_seat.ReadingRoomNum = seat.ReadingRoom.No;
                jm_seat.ReadingRoomName = seat.ReadingRoom.Name;
                jm_seat.SeatNo = seat.SeatNo;
                jm_seat.SeatUsedState = seat.SeatUsedState.ToString();
                jm_seat.ShortSeatNo = seat.ShortSeatNo;
                jm_seat.BaseHeight = seat.BaseHeight;
                jm_seat.BaseWidth = seat.BaseWidth;
                jm_seat.PositionX = seat.PositionX;
                jm_seat.PositionY = seat.PositionY;
                jm_seat.RotationAngle = seat.RotationAngle;
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(jm_seat);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("根据座位号获座位者预约信息遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }

        /// <summary>
        /// 获取二维码信息
        /// </summary>
        /// <param name="strQRcode"></param>
        /// <returns></returns>
        public string GetQRcodeInfo(string strQRcode)
        {
            try
            {
                string[] scanResultArray = strQRcode.Split('?');
                ScanCodeParamModel scancode = null;
                if (scanResultArray.Length >= 2)
                {
                    string[] strParam = scanResultArray[1].Split('=');
                    if (strParam.Length >= 2)
                    {
                        scancode = ScanCodeParamModel.Prase(strParam[1]);//兼容url的二维码。 
                    }
                }
                else
                {
                    scancode = ScanCodeParamModel.Prase(strQRcode);
                }
                if (scancode == null)
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "二维码错误!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                Seat seatInfo = seatDataService.GetSeatInfoBySeatNum(scancode.SeatNum);
                if (seatInfo == null)
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "此座位不存在!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                JM_QRcode jm_QRcode = new JM_QRcode();
                JM_Seat jm_seat = new JM_Seat();
                jm_seat.CanBeBespeak = seatInfo.CanBeBespeak;
                jm_seat.HavePower = seatInfo.HavePower;
                jm_seat.IsSuspended = seatInfo.IsSuspended;
                jm_seat.ReadingRoomNum = seatInfo.ReadingRoomNum;
                jm_seat.ReadingRoomName = seatInfo.ReadingRoom.Name;
                jm_seat.SeatNo = seatInfo.SeatNo;
                jm_seat.SeatUsedState = seatInfo.SeatUsedState.ToString();
                jm_seat.ShortSeatNo = seatInfo.ShortSeatNo;
                jm_seat.BaseHeight = seatInfo.BaseHeight;
                jm_seat.BaseWidth = seatInfo.BaseWidth;
                jm_seat.PositionX = seatInfo.PositionX;
                jm_seat.PositionY = seatInfo.PositionY;
                jm_seat.RotationAngle = seatInfo.RotationAngle;
                jm_QRcode.SeatInfo = jm_seat;

                if (seatInfo.ReadingRoom.Setting.SeatBespeak.Used && !seatInfo.IsSuspended)
                {
                    if (seatInfo.ReadingRoom.Setting.SeatBespeak.NowDayBespeak && seatInfo.SeatUsedState == EnterOutLogType.Leave)
                    {
                        JM_CanBespeakSeatInfo jm_bespeakInfo = new JM_CanBespeakSeatInfo();
                        jm_bespeakInfo.SeatID = seatInfo.SeatNo;
                        jm_bespeakInfo.SeatNum = seatInfo.ShortSeatNo;
                        jm_bespeakInfo.ReadingRoomNo = seatInfo.ReadingRoomNum;
                        jm_bespeakInfo.BespeakDate = DateTime.Now.ToShortDateString();
                        jm_QRcode.BeaspeakSeat.Add(jm_bespeakInfo);
                    }
                    for (int i = 1; i <= seatInfo.ReadingRoom.Setting.SeatBespeak.BespeakBeforeDays; i++)
                    {
                        List<SeatManage.ClassModel.BespeakLogInfo> list = seatDataService.GetBespeakLogInfoBySeatNo(seatInfo.SeatNo, DateTime.Now.AddDays(i));
                        if (list.Count > 0)
                        {
                            continue;
                        }
                        JM_CanBespeakSeatInfo jm_bespeakInfo = new JM_CanBespeakSeatInfo();
                        jm_bespeakInfo.SeatID = seatInfo.SeatNo;
                        jm_bespeakInfo.SeatNum = seatInfo.ShortSeatNo;
                        jm_bespeakInfo.ReadingRoomNo = seatInfo.ReadingRoomNum;
                        jm_bespeakInfo.BespeakDate = DateTime.Now.AddDays(i).ToShortDateString();
                        jm_QRcode.BeaspeakSeat.Add(jm_bespeakInfo);
                    }
                }
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(jm_QRcode);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("获取二维码信息遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }
    }
}
