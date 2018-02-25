using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.IPocketBespeakBllService;
using SeatBespeakException;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;
namespace SeatManage.PocketBespeakBllService
{
    public partial class PocketBespeakBllService : IPocketBespeakBllService.IPocketBespeakBllService
    {
        /// <summary>
        /// 设置暂离，返回操作结果。
        /// </summary>
        /// <param name="school">学校</param>
        /// <param name="reader">读者</param>
        /// <returns></returns>
        public string SetShortLeave(string cardNo)
        {
            ReaderInfo reader = null;
            try
            {
                reader = seatManage.GetReader(cardNo, true);
            }
            catch (Exception ex)
            {
                throw new Exception("获取读者状态遇到异常");
            }
            if (reader != null)
            {
                if (reader.EnterOutLog == null)
                {
                    throw new Exception("暂离失败，您还没有选座。");
                }
                switch (reader.EnterOutLog.EnterOutState)
                {
                    case EnterOutLogType.BookingConfirmation://预约入座
                    case EnterOutLogType.SelectSeat:    //选座
                    case EnterOutLogType.ContinuedTime: //续时
                    case EnterOutLogType.ComeBack:      //暂离回来
                    case EnterOutLogType.ReselectSeat:  //重新选座
                    case EnterOutLogType.WaitingSuccess: //等待入座
                        reader.EnterOutLog.EnterOutState = EnterOutLogType.ShortLeave;
                        reader.EnterOutLog.Remark = "读者通过手机客户端设置暂时离开。";
                        reader.EnterOutLog.Flag = Operation.Reader;
                        reader.EnterOutLog.EnterOutTime = DateTime.Now;
                        return ShortLeave(reader);
                    case EnterOutLogType.ShortLeave:
                        throw new Exception("暂离失败，您已经是暂离状态。");
                    case EnterOutLogType.Leave:
                        throw new Exception("暂离失败，您还没有入座。");
                    default:
                        throw new Exception("暂离失败，您当前不是在座状态");
                }
            }
            else
            {
                throw new Exception("执行遇到错误");
            }
        }


        /// <summary>
        /// 释放座位，返回操作结果
        /// </summary>
        /// <param name="school"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        public string FreeSeat(string cardNo)
        {
            ReaderInfo reader = null;
            try
            {
                reader = seatManage.GetReader(cardNo, true);
            }
            catch (Exception ex)
            {
                throw new Exception("获取读者状态遇到异常");
            }
            if (reader != null)
            {
                if (reader.EnterOutLog == null)
                {
                    throw new Exception("释放座位失败，您还没有选座。");
                }
                switch (reader.EnterOutLog.EnterOutState)
                {
                    case EnterOutLogType.BookingConfirmation://预约入座
                    case EnterOutLogType.SelectSeat:    //选座
                    case EnterOutLogType.ContinuedTime: //续时
                    case EnterOutLogType.ComeBack:      //暂离回来
                    case EnterOutLogType.ReselectSeat:  //重新选座
                    case EnterOutLogType.WaitingSuccess: //读者通过等待座位入座
                    case EnterOutLogType.ShortLeave:    //读者暂离 
                        reader.EnterOutLog.EnterOutState = EnterOutLogType.Leave;
                        reader.EnterOutLog.Remark = "读者通过手机客户端释放座位。";
                        reader.EnterOutLog.Flag = Operation.Reader;
                        reader.EnterOutLog.EnterOutTime = DateTime.Now;

                        int newId = -1;
                        HandleResult returnResult = seatManage.AddEnterOutLogInfo(reader.EnterOutLog, ref newId);
                        if (returnResult == HandleResult.Successed)
                        {
                            return "座位已释放留给下一个读者使用，谢谢。";
                        }
                        else
                        {
                            throw new Exception("未知原因释放座位失败");
                        }
                    case EnterOutLogType.Leave:
                        throw new Exception("您当前没有座位。");
                    default:
                        throw new Exception("您当前没有座位。");
                }
            }
            else
            {
                throw new Exception("执行遇到错误");
            }
        }


        /// <summary>
        /// 获取所有阅览室的座位使用状态
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        public Dictionary<string, ReadingRoomSeatUsedState_Ex> GetAllRoomSeatUsedState()
        {
            try
            {
                Dictionary<string, ReadingRoomSeatUsedState_Ex> roomStatusEx = new Dictionary<string, ReadingRoomSeatUsedState_Ex>();
                List<SeatManage.ClassModel.ReadingRoomInfo> rooms = seatManage.GetReadingRooms(null, null, null);
                List<string> roomNums = new List<string>();
                foreach (SeatManage.ClassModel.ReadingRoomInfo room in rooms)
                {
                    roomNums.Add(room.No);
                }
                Dictionary<string, ReadingRoomSeatUsedState> roomsUsedState = seatManage.GetRoomSeatUsedState(roomNums);
                foreach (SeatManage.ClassModel.ReadingRoomInfo room in rooms)
                {
                    ReadingRoomSeatUsedState_Ex roomSeatState = new ReadingRoomSeatUsedState_Ex();
                    roomSeatState.ReadingRoom = room;
                    roomSeatState.PersonTimes = roomsUsedState[room.No].PersonTimes;
                    roomSeatState.SeatAmountAll = roomsUsedState[room.No].SeatAmountAll;
                    roomSeatState.SeatAmountShortLeave = roomsUsedState[room.No].SeatAmountShortLeave;
                    roomSeatState.SeatAmountUsed = roomsUsedState[room.No].SeatAmountUsed;
                    roomStatusEx.Add(room.No, roomSeatState);
                }
                return roomStatusEx;
            }
            catch (Exception ex)
            {
                throw new Exception("获取阅览室座位使用情况失败" + ex.Message);
            }
        }
        /// <summary>
        /// 获取读者信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public SeatManage.ClassModel.ReaderInfo GetReaderInfo(string cardNo)
        {
            try
            {
                ReaderInfo reader = seatManage.GetReader(cardNo, true);
                return reader;
            }
            catch (Exception ex)
            {
                throw new ReaderHandlerFailed(string.Format("获取读者{0}的信息时遇到异常：{1}", cardNo, ex.Message));
            }
        }
        /// <summary>
        /// 续时
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public string DelaySeatUsedTime(ReaderInfo reader)
        {
            try
            {
                string str = seatManage.DelaySeatUsedTime(reader);
                return str;
            }
            catch (Exception ex)
            {
                throw new ReaderHandlerFailed(ex.Message);
            }
        }
        /// <summary>
        /// 暂离回来
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public string ReaderComeBack(ReaderInfo reader)
        {
            try
            {
                string returnValue = "";
                int newId = -1;
                HandleResult result;
                try
                {
                    if (reader == null || reader.EnterOutLog == null)
                    {
                        throw new Exception("暂离失败，信息获取失败！");
                    }
                    reader.EnterOutLog.EnterOutState = EnterOutLogType.ComeBack;
                    reader.EnterOutLog.Remark = "读者通过手机客户端暂离回来。";
                    reader.EnterOutLog.Flag = Operation.Reader;
                    reader.EnterOutLog.EnterOutTime = DateTime.Now;
                    result = seatManage.AddEnterOutLogInfo(reader.EnterOutLog, ref newId);
                    if (result == HandleResult.Successed)
                    {
                        returnValue = "欢迎回来。";
                        return returnValue;
                    }
                    else
                    {
                        throw new Exception("未知原因暂离失败");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("暂离失败：" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new ReaderHandlerFailed(ex.Message);
            }
        }
        #region 私有方法
        /// <summary>
        /// 设置读者暂离，返回暂离信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string ShortLeave(ReaderInfo reader)
        {
            string returnValue = "";
            int newId = -1;
            HandleResult result;
            try
            {
                result = seatManage.AddEnterOutLogInfo(reader.EnterOutLog, ref newId);
                if (result == HandleResult.Successed)
                {
                    double shortTimeMin = GetSeatHoldTime(reader.AtReadingRoom.Setting.SeatHoldTime, DateTime.Now);
                    returnValue = string.Format("暂离成功，请在{0}分钟内在触摸屏上刷卡回来。", shortTimeMin);
                    return returnValue;
                }
                else
                {
                    throw new Exception("未知原因暂离失败");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("暂离失败：" + ex.Message);
            }
        }
        /// <summary>
        /// 计算座位保留时长
        /// </summary>
        /// <param name="set">设置</param>
        /// <param name="time"></param>
        /// <returns></returns>
        private double GetSeatHoldTime(SeatHoldTimeSet set, DateTime time)
        {
            if (set.UsedAdvancedSet)
            {
                foreach (SeatHoldTimeOption option in set.AdvancedSeatHoldTime)
                {
                    if (option.Used)
                    { //判断指定的时间是否在开始时间和结束时间中间
                        DateTime begintime = DateTime.Parse(time.ToShortDateString() + " " + option.UsedTime.BeginTime);
                        DateTime endtime = DateTime.Parse(time.ToShortDateString() + " " + option.UsedTime.EndTime);
                        if (DateTimeOperate.DateAccord(begintime, endtime, time))
                        {
                            return option.HoldTimeLength;
                        }
                    }
                }
                //遍历结束没有返回，则返回默认保留时长
                return set.DefaultHoldTimeLength;
            }
            else
            {
                //没有启用阅览室设置，则返回默认保留时长
                return set.DefaultHoldTimeLength;
            }
        }
        #endregion




        /// <summary>
        /// 座位选择
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="seatNum"></param>
        /// <param name="readingRoomNum"></param>
        /// <returns></returns>
        public string SelectSeat(string cardNo, string seatNum, string readingRoomNum)
        {
            try
            {
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
                            return "";
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
    }
}
