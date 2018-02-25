using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.IWCFService;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        /// <summary>
        /// 座位暂离
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public string SeatShortLeave(string cardNo, string clientNo, string remark)
        {
            ReaderInfo reader = null;
            try
            {
                reader = GetReader(cardNo, true);
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
                        double shortTimeMin = GetSeatHoldTime(reader.AtReadingRoom.Setting.SeatHoldTime, DateTime.Now);
                        string rem = "，设置" + reader.EnterOutLog.ReadingRoomName + "第" + reader.EnterOutLog.ShortSeatNo + "号座位暂离，座位保留" + shortTimeMin + "分钟";
                        reader.EnterOutLog.Remark = remark + rem;
                        reader.EnterOutLog.Flag = Operation.Reader;
                        reader.EnterOutLog.EnterOutTime = DateTime.Now;
                        reader.EnterOutLog.TerminalNum = clientNo;
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
        /// 释放座位
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public string SeatLeave(string cardNo, string clientNo, string remark)
        {
            ReaderInfo reader = null;
            try
            {
                reader = GetReader(cardNo, true);
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
                        string rem = "，释放" + reader.EnterOutLog.ReadingRoomName + "第" + reader.EnterOutLog.ShortSeatNo + "号座位";
                        reader.EnterOutLog.Remark = remark + rem;
                        reader.EnterOutLog.Flag = Operation.Reader;
                        reader.EnterOutLog.EnterOutTime = DateTime.Now;
                        reader.EnterOutLog.TerminalNum = clientNo;
                        int newId = -1;
                        HandleResult returnResult = AddEnterOutLogInfo(reader.EnterOutLog, ref newId);
                        if (returnResult == HandleResult.Successed)
                        {
                            return "";
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
                result = AddEnterOutLogInfo(reader.EnterOutLog, ref newId);
                if (result == HandleResult.Successed)
                {
                    //double shortTimeMin = GetSeatHoldTime(reader.AtReadingRoom.Setting.SeatHoldTime, DateTime.Now);
                    //returnValue = string.Format("暂离成功，请在{0}分钟内在触摸屏上刷卡回来。", shortTimeMin);
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
    }
}
