using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;

namespace SeatManage.PocketBespeakBllService
{
    public partial class PocketBespeakBllService : IPocketBespeakBllService.IPocketBespeakBllService
    {
        public List<ReadingRoomInfo> GetCanWaitSeatRoomInfo()
        {
            List<ClassModel.ReadingRoomInfo> canBespeakReadingRoom = new List<ReadingRoomInfo>();
            List<ClassModel.ReadingRoomInfo> allReadingRooms = seatManage.GetReadingRooms(null, null, null);
            List<string> roomNums = new List<string>();
            foreach (ClassModel.ReadingRoomInfo room in allReadingRooms)
            {

                try
                {
                    SeatManage.ClassModel.ReadingRoomSetting set = room.Setting;
                    if (!set.NoManagement.Used)
                    {
                        continue;
                    }
                    canBespeakReadingRoom.Add(room);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return canBespeakReadingRoom;
        }

        public List<Seat> GetWaitSeatList(string RoomId)
        {
            List<ClassModel.Seat> canWaitSeat = new List<Seat>();
            try
            {
                List<EnterOutLogInfo> enterOutLogs = seatManage.GetUsingSeatEnterOutLogInfo(RoomId);
                foreach (EnterOutLogInfo log in enterOutLogs)
                {
                    if (log.EnterOutState == EnterOutLogType.BookingConfirmation
                         || log.EnterOutState == EnterOutLogType.ComeBack
                         || log.EnterOutState == EnterOutLogType.ContinuedTime
                         || log.EnterOutState == EnterOutLogType.ReselectSeat
                         || log.EnterOutState == EnterOutLogType.SelectSeat
                         || log.EnterOutState == EnterOutLogType.WaitingSuccess)
                    {
                        ClassModel.Seat seat = new Seat();
                        seat.SeatNo = log.SeatNo;
                        seat.UserCardNo = log.CardNo;
                        seat.UserName = log.ReaderName;
                        seat.ShortSeatNo = log.ShortSeatNo;
                        seat.SeatUsedState = log.EnterOutState;
                        seat.ReadingRoomNum = log.ReadingRoomNo;
                        canWaitSeat.Add(seat);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return canWaitSeat;
        }

        public string SubmitWaitInfo(WaitSeatLogInfo waitInfo)
        {
            try
            {
                SeatManage.ClassModel.EnterOutLogInfo enterOutLog = seatManage.GetEnterOutLogInfoBySeatNum(waitInfo.SeatNo);
                if (enterOutLog == null)
                {
                    return "等待座位失败，请刷新页面重新尝试";
                }
                if (enterOutLog.EnterOutState == EnterOutLogType.BookingConfirmation
                         || enterOutLog.EnterOutState == EnterOutLogType.ComeBack
                         || enterOutLog.EnterOutState == EnterOutLogType.ContinuedTime
                         || enterOutLog.EnterOutState == EnterOutLogType.ReselectSeat
                         || enterOutLog.EnterOutState == EnterOutLogType.SelectSeat
                         || enterOutLog.EnterOutState == EnterOutLogType.WaitingSuccess)
                {
                    enterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.ShortLeave;
                    enterOutLog.EnterOutType = SeatManage.EnumType.LogStatus.Valid;
                    enterOutLog.Flag = SeatManage.EnumType.Operation.OtherReader;
                    enterOutLog.Remark = string.Format("在{0} {1}号座位，被读者{2}在手机预约网站设置为暂离并等待该座位", enterOutLog.ReadingRoomName, enterOutLog.ShortSeatNo, waitInfo.CardNo);
                    int newLogId = -1;
                    HandleResult result = seatManage.AddEnterOutLogInfo(enterOutLog, ref newLogId);//插入进出记录
                    if (result == HandleResult.Successed)
                    {
                        waitInfo.EnterOutLogID = newLogId;
                        seatManage.AddWaitLog(waitInfo);
                        return string.Format("您以成功等待{0} {1}号座位", enterOutLog.ReadingRoomName, enterOutLog.ShortSeatNo);
                    }
                    else
                    {
                        return "等待座位失败！";
                    }
                }
                else
                {
                    return "此座位不能被等待，请重新选择！";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool IsCanWaitSeat(string CardNo, string roomNo)
        {
            try
            {
                WaitSeatLogInfo lastWaitInfo = seatManage.GetListWaitLogByCardNo(CardNo, roomNo);
                if (lastWaitInfo == null)
                {
                    return true;
                }
                List<ReadingRoomInfo> roomInfos = seatManage.GetReadingRooms(new List<string>() { roomNo }, null, null);
                if (roomInfos.Count > 0)
                {

                    if (lastWaitInfo != null && lastWaitInfo.SeatWaitTime.AddMinutes(roomInfos[0].Setting.NoManagement.OperatingInterval).CompareTo(DateTime.Now) >= 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    throw new Exception("获取信息失败!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CancelWait(WaitSeatLogInfo waitInfo)
        {
            try
            {
                ReadingRoomInfo roomInfo;
                List<ReadingRoomInfo> roomInfos = seatManage.GetReadingRooms(new List<string>() { waitInfo.ReadingRoomNo }, null, null);
                if (roomInfos.Count > 0)
                {
                    roomInfo = roomInfos[0];
                }
                else
                {
                    throw new Exception("获取信息失败!");
                }
                //处理等待记录的Id
                waitInfo.OperateType = Operation.Reader;
                waitInfo.WaitingState = EnterOutLogType.WaitingCancel;
                waitInfo.NowState = LogStatus.Valid;
                if (seatManage.UpdateWaitLog(waitInfo))
                {  //恢复读者的在座状态
                    EnterOutLogInfo enterOutlog = seatManage.GetEnterOutLogInfoById(waitInfo.EnterOutLogID);
                    System.TimeSpan shortleavetimelong = DateTime.Now - enterOutlog.EnterOutTime;
                    enterOutlog.EnterOutState = EnterOutLogType.ComeBack;
                    enterOutlog.EnterOutType = LogStatus.Valid;
                    enterOutlog.Flag = Operation.OtherReader;
                    enterOutlog.Remark = string.Format("读者{0}在手机预约网站取消等待{1} {2}号座位，您暂离{3}分钟后恢复为在座状态",
                         waitInfo.CardNo,
                         enterOutlog.ReadingRoomName,
                         enterOutlog.ShortSeatNo,
                         shortleavetimelong.Minutes);
                    int newId = -1;
                    if (seatManage.AddEnterOutLogInfo(enterOutlog, ref newId) == HandleResult.Successed)
                    {
                        return "取消等待成功！";
                    }
                    else
                    {
                        return "操作失败！";
                    }
                }
                else
                {
                    return "操作失败！";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
