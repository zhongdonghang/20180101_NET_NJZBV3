using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.SeatManageComm;

namespace SeatService.Service.Code
{
    public class EnterOutLogStatistics
    {
        /// <summary>
        /// 获取的进出记录总数
        /// </summary>
        private int _AllLogCount = 0;
        /// <summary>
        /// 统计完有效的进出记录
        /// </summary>
        private int _validFullEnterOutLogCount = 0;
        /// <summary>
        /// 无效的进出记录
        /// </summary>
        private int _ErrorEnterOutLogCount = 0;
        /// <summary>
        /// 进出记录列表
        /// </summary>
        private List<SeatManage.ClassModel.EnterOutLogInfo> enterOutLogList = new List<SeatManage.ClassModel.EnterOutLogInfo>();
        /// <summary>
        /// 开始计算
        /// </summary>
        /// <returns></returns>
        public string StartStatistics()
        {
            try
            {
                Statistics();
                return string.Format("计算进出记录数据{0}条，有效计算记录{1}条，无效数据{2}条", _AllLogCount, _validFullEnterOutLogCount, _ErrorEnterOutLogCount);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        private void GetEnterOutLogs()
        {
            try
            {
                SeatManage.ClassModel.EnterOutLogStatistics lastStatisticsLog = SeatManage.Bll.T_SM_EnterOutLogStatistics.GetLastEnterOutLogStatistics();
                int id = 0;
                if (lastStatisticsLog != null)
                {
                    id = lastStatisticsLog.LastEnterOutLogID;
                }
                enterOutLogList = SeatManage.Bll.T_SM_EnterOutLog_bak.GetStatisticsLogs(id);
                _AllLogCount = enterOutLogList.Count;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 开始计算
        /// </summary>
        private void Statistics()
        {
            GetEnterOutLogs();
            SeatManage.ClassModel.EnterOutLogStatistics newStatistics = new SeatManage.ClassModel.EnterOutLogStatistics();
            for (int i = 0; i < enterOutLogList.Count; i++)
            {
                //判断状态
                switch (enterOutLogList[i].EnterOutState)
                {
                    case SeatManage.EnumType.EnterOutLogType.BookingConfirmation:
                        newStatistics.SelectSeat = SeatManage.ClassModel.EnterOutLogSelectSeatMode.BookAdmission;
                        newStatistics.CardNo = enterOutLogList[i].CardNo;
                        newStatistics.SeatNo = enterOutLogList[i].SeatNo;
                        newStatistics.ReadingRoomNo = enterOutLogList[i].ReadingRoomNo;
                        newStatistics.EnterOutLogNo = enterOutLogList[i].EnterOutLogNo;
                        newStatistics.SelectSeatTime = enterOutLogList[i].EnterOutTime;
                        break;
                    case SeatManage.EnumType.EnterOutLogType.WaitingSuccess:
                        newStatistics.SelectSeat = SeatManage.ClassModel.EnterOutLogSelectSeatMode.WaitAdmission;
                        newStatistics.CardNo = enterOutLogList[i].CardNo;
                        newStatistics.SeatNo = enterOutLogList[i].SeatNo;
                        newStatistics.ReadingRoomNo = enterOutLogList[i].ReadingRoomNo;
                        newStatistics.EnterOutLogNo = enterOutLogList[i].EnterOutLogNo;
                        newStatistics.SelectSeatTime = enterOutLogList[i].EnterOutTime;
                        break;
                    case SeatManage.EnumType.EnterOutLogType.ReselectSeat:
                        newStatistics.SelectSeat = SeatManage.ClassModel.EnterOutLogSelectSeatMode.ReSelect;
                        newStatistics.CardNo = enterOutLogList[i].CardNo;
                        newStatistics.SeatNo = enterOutLogList[i].SeatNo;
                        newStatistics.ReadingRoomNo = enterOutLogList[i].ReadingRoomNo;
                        newStatistics.EnterOutLogNo = enterOutLogList[i].EnterOutLogNo;
                        newStatistics.SelectSeatTime = enterOutLogList[i].EnterOutTime;
                        break;
                    case SeatManage.EnumType.EnterOutLogType.SelectSeat:
                        if (enterOutLogList[i].Flag == SeatManage.EnumType.Operation.Admin)
                        {
                            newStatistics.SelectSeat = SeatManage.ClassModel.EnterOutLogSelectSeatMode.AdminAllocation;
                        }
                        else
                        {
                            newStatistics.SelectSeat = SeatManage.ClassModel.EnterOutLogSelectSeatMode.ReadCardSelect;
                        }
                        newStatistics.CardNo = enterOutLogList[i].CardNo;
                        newStatistics.SeatNo = enterOutLogList[i].SeatNo;
                        newStatistics.ReadingRoomNo = enterOutLogList[i].ReadingRoomNo;
                        newStatistics.EnterOutLogNo = enterOutLogList[i].EnterOutLogNo;
                        newStatistics.SelectSeatTime = enterOutLogList[i].EnterOutTime;
                        break;
                    case SeatManage.EnumType.EnterOutLogType.ContinuedTime:
                        newStatistics.ContinueTimeCount++;
                        break;
                    case SeatManage.EnumType.EnterOutLogType.ShortLeave:
                        newStatistics.ShortLeaveCount++;
                        break;
                    case SeatManage.EnumType.EnterOutLogType.ComeBack:
                        newStatistics.AllOperationCount++;
                        continue;
                    case SeatManage.EnumType.EnterOutLogType.Leave:
                        switch (enterOutLogList[i].Flag)
                        {
                            case SeatManage.EnumType.Operation.Admin:
                                newStatistics.LeaveSeat = SeatManage.ClassModel.EnterOutLogLeaveSeatMode.AdminReleased;
                                break;
                            case SeatManage.EnumType.Operation.Reader:
                                newStatistics.LeaveSeat = SeatManage.ClassModel.EnterOutLogLeaveSeatMode.ReaderReleased;
                                break;
                            case SeatManage.EnumType.Operation.Service:
                                newStatistics.LeaveSeat = SeatManage.ClassModel.EnterOutLogLeaveSeatMode.ServerReleased;
                                break;
                        }
                        newStatistics.LastEnterOutLogID = int.Parse(enterOutLogList[i].EnterOutLogID);
                        newStatistics.LeaveSeatTime = enterOutLogList[i].EnterOutTime;
                        break;
                }
                //操作次数赋值
                newStatistics.AllOperationCount++;
                switch (enterOutLogList[i].Flag)
                {
                    case SeatManage.EnumType.Operation.Admin: newStatistics.AdminOperationCount++; break;
                    case SeatManage.EnumType.Operation.OtherReader: newStatistics.OtherOperationCount++; break;
                    case SeatManage.EnumType.Operation.Reader: newStatistics.ReaderOperationCount++; break;
                    case SeatManage.EnumType.Operation.Service: newStatistics.ServerOperationCount++; break;
                }
                //判断日期是否正确
                if (newStatistics.AllOperationCount > 1)
                {
                    if (enterOutLogList[i].EnterOutState == SeatManage.EnumType.EnterOutLogType.Leave || i + 1 >= enterOutLogList.Count || enterOutLogList[i + 1].EnterOutLogNo != newStatistics.EnterOutLogNo)
                    {
                        if (newStatistics.LeaveSeatTime < newStatistics.SelectSeatTime && newStatistics.SelectSeatTime < enterOutLogList[i].EnterOutTime)
                        {
                            newStatistics.LeaveSeatTime = enterOutLogList[i].EnterOutTime;
                        }
                        if ((newStatistics.LeaveSeatTime.Date - newStatistics.SelectSeatTime.Date).TotalDays != 0)
                        {
                            newStatistics = new SeatManage.ClassModel.EnterOutLogStatistics();
                            _ErrorEnterOutLogCount++;
                            continue;
                        }
                        newStatistics.SeatTime = int.Parse((newStatistics.LeaveSeatTime - newStatistics.SelectSeatTime).TotalMinutes.ToString().Split('.')[0]);
                        try
                        {
                            if (SeatManage.Bll.T_SM_EnterOutLogStatistics.AddEnterOutLogStatistics(newStatistics) == SeatManage.EnumType.HandleResult.Failed)
                            {
                                throw new Exception("添加进出记录统计失败！");
                            }
                            _validFullEnterOutLogCount++;
                        }
                        catch (Exception ex)
                        {
                            WriteLog.Write(ex.Message);
                        }
                        newStatistics = new SeatManage.ClassModel.EnterOutLogStatistics();
                    }
                }
                else if (enterOutLogList[i].EnterOutState == SeatManage.EnumType.EnterOutLogType.Leave
                    || enterOutLogList[i].EnterOutState == SeatManage.EnumType.EnterOutLogType.ComeBack
                    || enterOutLogList[i].EnterOutState == SeatManage.EnumType.EnterOutLogType.ContinuedTime
                    || enterOutLogList[i].EnterOutState == SeatManage.EnumType.EnterOutLogType.ShortLeave)
                {
                    newStatistics = new SeatManage.ClassModel.EnterOutLogStatistics();
                    _ErrorEnterOutLogCount++;
                    continue;
                }
            }
        }
    }
}
