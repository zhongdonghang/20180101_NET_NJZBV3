using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.EnumType;

namespace SeatManage.PocketBespeakBllService
{
    public partial class PocketBespeakBllService : SeatManage.IPocketBespeakBllService.IPocketBespeakBllService
    {
        public List<ClassModel.ReadingRoomInfo> GetReadingRoomUsingUsingState()
        {
            try
            {
                //List<ReadingRoomInfo> rooms = new List<ReadingRoomInfo>();
                //List<ReadingRoomInfo> roomList = seatManage.GetReadingRoomInfo(null);
                //rooms = roomList.FindAll(u => u.Setting.ReadingRoomOpenState(DateTime.Now) != EnumType.ReadingRoomStatus.Close);
                //return rooms;
                return seatManage.GetReadingRoomInfo(null).FindAll(u => u.Setting.ReadingRoomOpenState(DateTime.Now) != EnumType.ReadingRoomStatus.Close);
            }
            catch (Exception ex)
            {
                throw new Exception("获取阅览室失败" + ex.Message);
            }
        }

        public List<ClassModel.Seat> GetReadingRoomSeatList(string RoomId)
        {
            try
            {
                List<ClassModel.Seat> canBespeakSeat = new List<Seat>();
                List<string> roomNums = new List<string>();
                roomNums.Add(RoomId);
                List<ReadingRoomInfo> rooms = seatManage.GetReadingRooms(roomNums, null, null);
                SeatLayout bespeakSeatLayout = seatManage.GetBeseakSeatLayout(RoomId, DateTime.Now);
                SeatLayout useSeatLayout = seatManage.GetRoomSeatLayOut(RoomId);
                foreach (KeyValuePair<string, Seat> uSeat in useSeatLayout.Seats)
                {
                    if (uSeat.Value.SeatUsedState == EnterOutLogType.BespeakWaiting)
                    {
                        continue;
                    }
                    if ((uSeat.Value.SeatUsedState == EnterOutLogType.Leave || uSeat.Value.SeatUsedState == EnterOutLogType.None) && !uSeat.Value.IsSuspended)
                    {
                        canBespeakSeat.Add(uSeat.Value);
                    }
                }
                return canBespeakSeat;
            }
            catch (Exception ex)
            {
                throw new Exception("获取可选座位失败" + ex.Message);
            }
        }

        public string SubmitSeat(string cardNo, string seatNum, string readingRoomNum)
        {
            try
            {
                if (!IsPushAccount(cardNo))
                {
                    return "请先刷卡";
                }
                List<string> roomNums = new List<string>();
                roomNums.Add(readingRoomNum);
                List<ReadingRoomInfo> rooms = seatManage.GetReadingRoomInfo(roomNums);
                if (rooms.Count == 0)
                {
                    return "没有找到对应的阅览室";
                }
                ReadingRoomSetting roomSet = rooms[0].Setting;
                ReaderInfo reader = GetReaderInfo(cardNo);
                EnterOutLogType nowReaderStatus = EnterOutLogType.Leave;
                if (reader.EnterOutLog != null && reader.EnterOutLog.EnterOutState != EnterOutLogType.Leave)
                {
                    nowReaderStatus = reader.EnterOutLog.EnterOutState;
                }
                else if (reader.BespeakLog.Count > 0)
                {
                    nowReaderStatus = EnterOutLogType.BespeakWaiting;
                }
                else if (reader.WaitSeatLog != null)
                {
                    nowReaderStatus = EnterOutLogType.Waiting;
                }

                switch (nowReaderStatus)
                {
                    case EnterOutLogType.Leave:
                        if (seatManage.GetReaderChooseSeatTimes(cardNo, roomSet.PosTimes.Minutes) >= roomSet.PosTimes.Times)
                        {
                            return "选座频繁。";
                        }
                        EnterOutLogInfo enterOutlog = new EnterOutLogInfo();
                        enterOutlog.CardNo = cardNo;
                        enterOutlog.ReadingRoomNo = readingRoomNum;
                        enterOutlog.Remark = "读者通过手机客户端选择座位";
                        enterOutlog.SeatNo = seatNum;
                        enterOutlog.Flag = EnumType.Operation.Reader;
                        enterOutlog.EnterOutType = EnumType.LogStatus.Valid;
                        enterOutlog.EnterOutState = EnumType.EnterOutLogType.SelectSeat;
                        enterOutlog.EnterOutLogNo = SeatManage.SeatManageComm.SeatComm.RndNum();
                        int newLogId = -1;
                        if (seatManage.AddEnterOutLogInfo(enterOutlog, ref newLogId) == HandleResult.Successed)
                        {
                            return "选座成功！";
                        }
                        else
                        {
                            return "未知原因，选择座位失败";
                        }
                        break;
                    case EnterOutLogType.BespeakWaiting:
                        return "您已有等待签到的座位";
                    case EnterOutLogType.BookingConfirmation:
                    case EnterOutLogType.SelectSeat:
                    case EnterOutLogType.ContinuedTime:
                    case EnterOutLogType.ComeBack:
                    case EnterOutLogType.ReselectSeat:
                    case EnterOutLogType.WaitingSuccess:
                    case EnterOutLogType.ShortLeave:
                        return "您已有座位";
                    case EnterOutLogType.Waiting:
                        return "您当前在等待其他座位";
                }
                return "读者状态错误";
            }
            catch (Exception ex)
            {
                SeatManageComm.WriteLog.Write(string.Format("扫码入座失败：{0}", ex.Message));
                return "系统错误，选择座位失败";
            }
        }

        /// <summary>
        /// 获取还没处理的刷卡数据
        /// </summary>
        /// <returns></returns>
        public bool IsPushAccount(string cardNo)
        {
            StringBuilder strSql = new StringBuilder();
            int effectiveTime = ConfigurationManager.AppSettings["BushEffectiveTime"] != null ? int.Parse(ConfigurationManager.AppSettings["BushEffectiveTime"]) : 60;
            bool selectCheckAccount = ConfigurationManager.AppSettings["SelectCheckAccount"] != null && ConfigurationManager.AppSettings["SelectCheckAccount"] =="1"? true:false;
            if (!selectCheckAccount)
            {
                return true;
            }
            strSql.AppendFormat("select VisitNo,CardId,Passed,VisitTime,direction,HandleFlag from shuakajilu where  datediff(mi,visitTime,getdate())<{0} and VisitNo='{1}'", effectiveTime, cardNo);
            try
            {
                DataSet ds = DBUtility.DbHelperSQL.Query(strSql.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
