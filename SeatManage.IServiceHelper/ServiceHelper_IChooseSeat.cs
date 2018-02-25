using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.JsonModel;
using SeatManage.SeatManageComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ServiceHelper
{
    public partial class ServiceHelper : IChooseSeat
    {
        /// <summary>
        /// 判断读者是否可以进入阅览室
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        public string VerifyCanDoIt(string cardNo, string roomNum)
        {
            try
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = true;
                result.Msg = "";
                if (string.IsNullOrEmpty(cardNo))
                {
                    result.Result = false;
                    result.Msg = "对不起，输入的学号不能为空。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                List<ReadingRoomInfo> rooms = seatDataService.GetReadingRooms(new List<string>() { roomNum }, null, null);
                if (rooms.Count < 1)
                {
                    result.Result = false;
                    result.Msg = "对不起，此阅览室不存在。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                //判断阅览室是否开放
                if (ReadingRoomOpenState(rooms[0].Setting.RoomOpenSet, seatDataService.GetServerDateTime()) == ReadingRoomStatus.Close)
                {
                    result.Result = false;
                    result.Msg = "对不起，此阅览室尚未开放。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                //判断阅览室是否已满
                ReadingRoomSeatUsedState roomStatus = GetRoomSeatUsedState(roomNum);
                if (roomStatus != null && roomStatus.SeatAmountFree <= 0 && !rooms[0].Setting.NoManagement.Used)
                {
                    result.Result = false;
                    result.Msg = "对不起，此阅览室座位已满。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                //判断黑名单
                if (!string.IsNullOrEmpty(checkBlacklist(cardNo, rooms[0])))
                {
                    result.Result = false;
                    result.Msg = "对不起，您在黑名单中存在记录。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                //判断选座次数
                if (rooms[0].Setting.PosTimes.IsUsed && seatDataService.GetReaderChooseSeatTimes(cardNo, rooms[0].Setting.PosTimes.Minutes) >= rooms[0].Setting.PosTimes.Times)
                {
                    result.Result = false;
                    result.Msg = "操作失败，选座频繁。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                //判断读者类型
                ReaderInfo reader = seatDataService.GetReader(cardNo, false);
                if(reader==null)
                {
                    reader = new ReaderInfo();
                    reader.CardNo = cardNo;
                    reader.ReaderType = "未指定";
                }
                if (!ProvenReaderType(reader, rooms[0].Setting))
                {
                    result.Result = false;
                    result.Msg = "对不起，您的用户类型'" + reader.ReaderType + "'不允许在此阅览室选座。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                return SeatManageComm.JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("判断读者是否允许进入阅览室遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 座位锁定操作
        /// </summary>
        /// <param name="seatNum"></param>
        /// <returns></returns>
        public string SeatLock(string seatNum)
        {
            try
            {
                if (seatDataService.SeatLocked(seatNum) != SeatLockState.Locked)
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "座位加锁失败。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                else
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = true;
                    result.Msg = "";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("座位加锁遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 座位解锁操作
        /// </summary>
        /// <param name="seatNum"></param>
        /// <returns></returns>
        public string SeatUnLock(string seatNum)
        {
            try
            {
                if (seatDataService.SeatUnLocked(seatNum) != SeatLockState.UnLock)
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "座位解锁失败。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                else
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = true;
                    result.Msg = "";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("座位解锁遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 选座
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="seatNum"></param>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        public string SubmitChooseResult(string cardNum, string seatNum, string roomNum)
        {
            try
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = true;
                result.Msg = "";
                if (string.IsNullOrEmpty(cardNum))
                {
                    result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "读者的学号不能为空";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (seatNum.Length != 9)
                {
                    result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "座位编号不正确";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                string roomNo = seatNum.Substring(0, 6);
                //验证读者是否可以选择座位
                result = SeatManageComm.JSONSerializer.Deserialize<JM_HandleResult>(VerifyCanDoIt(cardNum, roomNo));
                if (!result.Result)
                {
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                SeatManage.ClassModel.Seat seatInfo = seatDataService.GetSeatInfoBySeatNum(seatNum);
                if (seatInfo == null)
                {
                    result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "此座位不存在";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (seatInfo.IsSuspended)
                {
                    result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "座位已停用";
                    return SeatManageComm.JSONSerializer.Serialize(result);

                }
                if (seatInfo.SeatUsedState != EnterOutLogType.Leave)
                {
                    result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "座位正在被使用";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                List<BespeakLogInfo> bespeaklogList = seatDataService.GetBespeakLogInfoBySeatNo(seatNum, DateTime.Now);
                if (bespeaklogList.Count > 0)
                {
                    if (!seatInfo.ReadingRoom.Setting.SeatBespeak.SelectBespeakSeat)
                    {
                        result = new JM_HandleResult();
                        result.Result = false;
                        result.Msg = "此座位座位已被预约";
                        return SeatManageComm.JSONSerializer.Serialize(result);
                    }
                    if (bespeaklogList[0].BsepeakTime == bespeaklogList[0].SubmitTime)
                    {
                        result = new JM_HandleResult();
                        result.Result = false;
                        result.Msg = "此座位座位已被预约";
                        return SeatManageComm.JSONSerializer.Serialize(result);
                    }
                    if (bespeaklogList[0].BsepeakTime.AddMinutes(-double.Parse(seatInfo.ReadingRoom.Setting.SeatBespeak.ConfirmTime.BeginTime)) <= DateTime.Now)
                    {
                        result = new JM_HandleResult();
                        result.Result = false;
                        result.Msg = "此座位座位已被预约";
                        return SeatManageComm.JSONSerializer.Serialize(result);
                    }
                }

                EnterOutLogInfo enterOutlog = seatDataService.GetEnterOutLogInfoByCardNo(cardNum);
                if (enterOutlog != null && enterOutlog.EnterOutState != EnterOutLogType.Leave)
                {

                    result.Result = false;
                    result.Msg = "对不起，您已有正在使用中的座位。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (seatDataService.GetSingleBespeakLogForWait(cardNum) != null)
                {
                    result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "您有等待签到的预约记录。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (seatDataService.GetWaitLogList(cardNum, null, null, null, new List<EnterOutLogType>() { EnterOutLogType.Waiting }).Count > 0)
                {
                    result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "您有正在等待的座位。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }

                if (enterOutlog == null)
                {
                    enterOutlog = new EnterOutLogInfo();
                }
                enterOutlog.CardNo = cardNum;
                enterOutlog.ReadingRoomNo = seatInfo.ReadingRoomNum;
                enterOutlog.Remark = string.Format("通过移动终端，选择{0} {1}号座位", seatInfo.ReadingRoom.Name, seatInfo.ShortSeatNo);
                enterOutlog.SeatNo = seatInfo.SeatNo;
                enterOutlog.Flag = EnumType.Operation.Reader;
                enterOutlog.EnterOutType = EnumType.LogStatus.Valid;
                enterOutlog.EnterOutState = EnumType.EnterOutLogType.SelectSeat;
                enterOutlog.EnterOutLogNo = SeatManage.SeatManageComm.SeatComm.RndNum();
                int newLogId = -1;
                if (seatDataService.AddEnterOutLogInfo(enterOutlog, ref newLogId) == HandleResult.Successed)
                {
                    result.Result = false;
                    result.Msg = "选座成功。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                else
                {
                    result.Result = false;
                    result.Msg = "对不起，选座失败，请重新尝试。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("选择座位遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }

        /// <summary>
        /// 验证读者类型
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="roomSet"></param>
        /// <returns></returns>
        private bool ProvenReaderType(ReaderInfo reader, ReadingRoomSetting roomSet)
        {
            if (roomSet.LimitReaderEnter.Used)
            {
                string[] readerTypes = roomSet.LimitReaderEnter.ReaderTypes.Split(';');
                for (int i = 0; i < readerTypes.Length; i++)
                {
                    if (reader.ReaderType == readerTypes[i])//如果读者类型和限制的类型一致，则返回该类型的选择权限。
                    {
                        return roomSet.LimitReaderEnter.CanEnter;
                    }
                }
                return !roomSet.LimitReaderEnter.CanEnter;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 阅览室开放状态
        /// </summary>
        /// <param name="openSeat"></param>
        /// <returns></returns>
        private ReadingRoomStatus ReadingRoomOpenState(RoomOpenTimeSet openSeat, DateTime time)
        {
            if (openSeat.UninterruptibleModel)
            {
                return ReadingRoomStatus.Open;
            }
            ReadingRoomStatus openState = ReadingRoomStatus.Close;

            if (openSeat.UsedAdvancedSet)//启用高级设置
            {
                DayOfWeek day = time.DayOfWeek;
                try
                {
                    RoomOpenPlanSet plan = openSeat.RoomOpenPlan[day];

                    if (plan.Used)
                    {
                        foreach (TimeSpace t in plan.OpenTime)
                        {
                            openState = calcRoomState(t.BeginTime, t.EndTime, time, openSeat.OpenBeforeTimeLength, openSeat.CloseBeforeTimeLength);
                            switch (openState)
                            { //当前时间阅览室状态为非关闭状态，直接返回结果。否则继续判断
                                case ReadingRoomStatus.BeforeClose:
                                case ReadingRoomStatus.BeforeOpen:
                                case ReadingRoomStatus.Open:
                                    return openState;
                            }
                        }
                        //遍历结束没有返回，则返回最后一次计算的结果
                        return openState;
                    }
                    else
                    {
                        //否则当天没启用高级设置，返回默认开馆状态
                        openState = calcRoomState(openSeat.DefaultOpenTime.BeginTime, openSeat.DefaultOpenTime.EndTime, time, openSeat.OpenBeforeTimeLength, openSeat.CloseBeforeTimeLength);
                        return openState;
                    }
                }
                catch
                {
                    //当天没有高级设置，则返回默认开馆状态。
                    openState = calcRoomState(openSeat.DefaultOpenTime.BeginTime, openSeat.DefaultOpenTime.EndTime, time, openSeat.OpenBeforeTimeLength, openSeat.CloseBeforeTimeLength);
                    return openState;
                }
            }
            else
            {
                //没有开启高级设置，则返回默认开馆状态。
                openState = calcRoomState(openSeat.DefaultOpenTime.BeginTime, openSeat.DefaultOpenTime.EndTime, time, openSeat.OpenBeforeTimeLength, openSeat.CloseBeforeTimeLength);
                return openState;
            }

        }
        /// <summary>
        /// 根据时间计算阅览室的状态
        /// </summary>
        /// <param name="beginTime">开馆时间</param>
        /// <param name="endTime">闭馆时间</param>
        /// <param name="datetime">要判断开放状态的时间</param>
        /// <param name="openBeforeTimeLength">开馆预处理</param>
        /// <param name="closeBeforeTimeLength">闭馆预处理</param>
        /// <returns></returns>
        private static ReadingRoomStatus calcRoomState(string beginTime, string endTime, DateTime datetime, double openBeforeTimeLength, double closeBeforeTimeLength)
        {
            DateTime begindate = DateTime.Parse(datetime.ToShortDateString() + " " + beginTime);
            DateTime enddate = DateTime.Parse(datetime.ToShortDateString() + " " + endTime);

            if (DateTimeOperate.DateAccord(enddate.AddMinutes(-closeBeforeTimeLength), enddate, datetime))//判断是否符合闭馆预处理
            {
                return ReadingRoomStatus.BeforeClose;
            }
            else if (DateTimeOperate.DateAccord(begindate, enddate, datetime))
            {
                return ReadingRoomStatus.Open;
            }
            else if (DateTimeOperate.DateAccord(begindate.AddMinutes(-openBeforeTimeLength), begindate, datetime))//判断是否符合开馆预处理
            {
                return ReadingRoomStatus.BeforeOpen;
            }
            else
            {
                return ReadingRoomStatus.Close;//条件都不符合，则为闭馆。
            }
        }
        /// <summary>
        /// 获取阅览室内座位的使用状态
        /// </summary>
        /// <param name="roomNum">房间号</param>
        /// <returns></returns>
        private ReadingRoomSeatUsedState GetRoomSeatUsedState(string roomNum)
        {
            Dictionary<string, ReadingRoomSeatUsedState> list = seatDataService.GetRoomSeatUsedState(new List<string>() { roomNum });
            if (list.Count > 0)
            {
                return list[roomNum];
            }
            else
            {
                return null;
            }
        }
    }
}
