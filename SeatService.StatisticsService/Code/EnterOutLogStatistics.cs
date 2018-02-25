using System;
using System.Collections.Generic;
using System.Linq;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;
using System.Text;

namespace SeatService.StatisticsService.Code
{
    public class EnterOutLogStatistics
    {
        public void Run(ref bool isStatistics, DateTime runTime)
        {
            DateTime nowDateTime = ServiceDateTime.Now;
            if (nowDateTime > DateTime.Parse(nowDateTime.ToShortDateString() + " " + runTime.ToShortTimeString()))
            {
                if (!isStatistics)
                {
                    return;
                }
                Statistics();
                isStatistics = false;
            }
            else
            {
                isStatistics = true;
            }

        }
        /// <summary>
        /// 开始计算
        /// </summary>
        private void Statistics()
        {
            try
            {
                int allLogCount = 0;
                int errorEnterOutLogCount = 0;
                int validFullEnterOutLogCount = 0;
                //获取统计的最后一条数据的ID
                SeatManage.ClassModel.EnterOutLogStatistics lastStatisticsLog = T_SM_EnterOutLogStatistics.GetLastEnterOutLogStatistics();
                DateTime sdt = lastStatisticsLog != null ? lastStatisticsLog.SelectSeatTime.AddDays(1) : T_SM_EnterOutLog_bak.GetFristLogDate();
                if (sdt <= DateTime.Parse("2000-1-1"))
                {
                    return;
                }
                //sdt = sdt.AddDays(1);
                while (true)
                {
                    //获取进出记录
                    List<EnterOutLogInfo> enterOutLogList = T_SM_EnterOutLog_bak.GetStatisticsLogsByDate(sdt);
                    if (enterOutLogList.Count <= 0)
                    {
                        if (sdt >= ServiceDateTime.Now.Date.AddDays(-1))
                        {
                            break;
                        }
                        else
                        {
                            sdt = sdt.AddDays(1);
                            continue;
                        }
                        
                    }
                    sdt = sdt.AddDays(1);

                    allLogCount += enterOutLogList.Count;
                    List<EnterOutLogInfo> eolSelectList = enterOutLogList.FindAll(u => u.EnterOutState == EnterOutLogType.BookingConfirmation || u.EnterOutState == EnterOutLogType.SelectSeat || u.EnterOutState == EnterOutLogType.ReselectSeat || u.EnterOutState == EnterOutLogType.WaitingSuccess).OrderBy(u => u.EnterOutLogID).ToList();
                    foreach (List<EnterOutLogInfo> eolNoList in eolSelectList.Select(eols => enterOutLogList.FindAll(u => u.EnterOutLogNo == eols.EnterOutLogNo).OrderBy(u => u.EnterOutLogID).ToList()).Where(eolNoList => eolNoList.Count >= 1))
                    {
                        SeatManage.ClassModel.EnterOutLogStatistics newStatistics = new SeatManage.ClassModel.EnterOutLogStatistics();
                        newStatistics.CardNo = eolNoList[0].CardNo;
                        newStatistics.SeatNo = eolNoList[0].SeatNo;
                        newStatistics.ReadingRoomNo = eolNoList[0].ReadingRoomNo;
                        newStatistics.EnterOutLogNo = eolNoList[0].EnterOutLogNo;
                        foreach (EnterOutLogInfo eol in eolNoList)
                        {
                            //判断状态
                            switch (eol.EnterOutState)
                            {
                                case EnterOutLogType.BookingConfirmation:
                                    newStatistics.SelectSeat = EnterOutLogSelectSeatMode.BookAdmission;
                                    newStatistics.SelectSeatTime = eol.EnterOutTime;
                                    break;
                                case EnterOutLogType.WaitingSuccess:
                                    newStatistics.SelectSeat = EnterOutLogSelectSeatMode.WaitAdmission;
                                    newStatistics.SelectSeatTime = eol.EnterOutTime;
                                    break;
                                case EnterOutLogType.ReselectSeat:
                                    newStatistics.SelectSeat = EnterOutLogSelectSeatMode.ReSelect;
                                    newStatistics.SelectSeatTime = eol.EnterOutTime;
                                    break;
                                case EnterOutLogType.SelectSeat:
                                    newStatistics.SelectSeat = eol.Flag == Operation.Admin ? EnterOutLogSelectSeatMode.AdminAllocation : EnterOutLogSelectSeatMode.ReadCardSelect;
                                    newStatistics.SelectSeatTime = eol.EnterOutTime;
                                    break;
                                case EnterOutLogType.ContinuedTime:
                                    newStatistics.ContinueTimeCount++;
                                    break;
                                case EnterOutLogType.ShortLeave:
                                    newStatistics.ShortLeaveCount++;
                                    break;
                                case EnterOutLogType.Leave:
                                    switch (eol.Flag)
                                    {
                                        case Operation.Admin: newStatistics.LeaveSeat = EnterOutLogLeaveSeatMode.AdminReleased; break;
                                        case Operation.Reader: newStatistics.LeaveSeat = EnterOutLogLeaveSeatMode.ReaderReleased; break;
                                        case Operation.Service: newStatistics.LeaveSeat = EnterOutLogLeaveSeatMode.ServerReleased; break;
                                    }
                                    newStatistics.LastEnterOutLogID = int.Parse(eol.EnterOutLogID);
                                    newStatistics.LeaveSeatTime = eol.EnterOutTime;
                                    break;
                            }
                        }
                        if (newStatistics.LeaveSeatTime.Date != newStatistics.SelectSeatTime.Date)
                        {
                            errorEnterOutLogCount++;
                            continue;
                        }
                        newStatistics.SeatTime = (int)(newStatistics.LeaveSeatTime - newStatistics.SelectSeatTime).TotalMinutes;

                        //操作次数
                        newStatistics.AllOperationCount = eolNoList.Count;
                        newStatistics.AdminOperationCount = eolNoList.Count(u => u.Flag == Operation.Admin);
                        newStatistics.OtherOperationCount = eolNoList.Count(u => u.Flag == Operation.OtherReader);
                        newStatistics.ReaderOperationCount = eolNoList.Count(u => u.Flag == Operation.Reader);
                        newStatistics.ServerOperationCount = eolNoList.Count(u => u.Flag == Operation.Service);

                        if (T_SM_EnterOutLogStatistics.AddEnterOutLogStatistics(newStatistics) == HandleResult.Successed)
                        {
                            validFullEnterOutLogCount++;
                        }
                        else
                        {
                            errorEnterOutLogCount++;
                        }

                    }
                }
                WriteLog.Write(string.Format("数据统计服务：统计进出记录数据{0}条，有效数据{1}条，无效数据{2}条", allLogCount, validFullEnterOutLogCount, errorEnterOutLogCount));
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("数据统计服务：统计进出记录失败：{0}", ex.Message));
            }
        }
    }
}
