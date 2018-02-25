using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;
using SeatManage.JsonModel;

namespace SeatManage.ServiceHelper
{
    public partial class ServiceHelper : IShortLeave
    {
        public string ShortLeave(string cardNo)
        {
            try
            {
                JsonModel.JM_HandleResult result = new JsonModel.JM_HandleResult();
                if (string.IsNullOrEmpty(cardNo))
                {
                    result.Result = false;
                    result.Msg = "读者学号为空。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                ReaderInfo reader = seatDataService.GetReader(cardNo, true);
                if (reader == null)
                {
                    result.Result = false;
                    result.Msg = "获取读者信息失败。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (reader.EnterOutLog == null)
                {
                    result.Result = false;
                    result.Msg = "您还没有选座。";
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
                        result.Result = true;
                        result.Msg = setShortLeave(reader);
                        break;
                    case EnterOutLogType.ShortLeave:
                        result.Result = false;
                        result.Msg = "暂离失败，您已经是暂离状态。";
                        break;
                    case EnterOutLogType.Leave:
                        result.Result = false;
                        result.Msg = "暂离失败，您还没有入座。";
                        break;
                    default:
                        result.Result = false;
                        result.Msg = "暂离失败，您当前不是在座状态";
                        break;
                }
                return SeatManageComm.JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("读者暂离遇到异常：" + ex.Message);
                JsonModel.JM_HandleResult result = new JsonModel.JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常";
                return SeatManageComm.JSONSerializer.Serialize(result);
            }
        }

        /// <summary>
        /// 设置读者暂离，返回暂离信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        private string setShortLeave(ReaderInfo reader)
        {
            string returnValue = "";
            int newId = -1;
            HandleResult result;
            try
            {
                result = seatDataService.AddEnterOutLogInfo(reader.EnterOutLog, ref newId);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public string ComeBack(string cardNo)
        {
            try
            {
                JsonModel.JM_HandleResult result = new JsonModel.JM_HandleResult();
                if (string.IsNullOrEmpty(cardNo))
                {
                    result.Result = false;
                    result.Msg = "读者学号为空。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                ReaderInfo reader = seatDataService.GetReader(cardNo, true);
                if (reader == null)
                {
                    result.Result = false;
                    result.Msg = "获取读者信息失败。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (reader.EnterOutLog == null || reader.EnterOutLog.EnterOutState == EnterOutLogType.Leave)
                {
                    result.Result = false;
                    result.Msg = "您还没有选座。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (reader.EnterOutLog.EnterOutState != EnterOutLogType.ShortLeave)
                {
                    result.Result = false;
                    result.Msg = "您当前不是暂离状态。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                reader.EnterOutLog.EnterOutState = EnterOutLogType.ComeBack;
                reader.EnterOutLog.Remark = "读者通过移动客户端设置暂离回来。";
                reader.EnterOutLog.Flag = Operation.Reader;
                reader.EnterOutLog.EnterOutTime = DateTime.Now;
                result.Result = true;
                int newId = -1;
                if (seatDataService.AddEnterOutLogInfo(reader.EnterOutLog, ref newId) != HandleResult.Successed)
                {
                    result.Result = false;
                    result.Msg = "执行遇到异常!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                else
                {
                    result.Result = true;
                    result.Msg = "欢迎回来";
                    List<WaitSeatLogInfo> waitSeatLogs = seatDataService.GetWaitLogList("", reader.EnterOutLog.EnterOutLogID, null, null, null);
                    if (waitSeatLogs.Count > 0)
                    {
                        WaitSeatLogInfo waitSeatLog = waitSeatLogs[0];
                        waitSeatLog.NowState = LogStatus.Fail;
                        waitSeatLog.OperateType = Operation.OtherReader;
                        waitSeatLog.WaitingState = EnterOutLogType.WaitingCancel;
                        seatDataService.UpdateWaitLog(waitSeatLog);
                    }
                }
                return SeatManageComm.JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("暂离回来遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }
    }
}
