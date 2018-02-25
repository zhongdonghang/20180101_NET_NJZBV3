using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;

namespace SeatService.StatisticsService.Code
{
    public class TerminalUsageStatistics
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
                List<TerminalInfoV2> terminals = TerminalOperatorService.GetAllTeminalInfo();
                DateTime sdt = SeatUsageDataOperating.GetLastTerminalUsageStatisticsDate();
                if (sdt <= DateTime.Parse("2000-1-1"))
                {
                    return;
                }
                sdt = sdt.AddDays(1);
                while (true)
                {
                    //获取进出记录
                    List<EnterOutLogInfo> enterOutLogList = T_SM_EnterOutLog_bak.GetStatisticsLogsByDate(sdt);
                    if (enterOutLogList.Count <= 0 && sdt >= ServiceDateTime.Now.Date.AddDays(-1))
                    {
                        break;
                    }
                    List<SeatManage.ClassModel.TerminalUsageStatistics> ftsList = SeatUsageDataOperating.GetTerminalUsageStatisticsist(null, sdt.AddDays(-1).Date, sdt.Date.AddSeconds(-1));
                    Dictionary<string, SeatManage.ClassModel.TerminalUsageStatistics> terminalDir = terminals.ToDictionary(u => u.ClientNo, u => new SeatManage.ClassModel.TerminalUsageStatistics());

                    //基本数据及排序处理
                    foreach (TerminalInfoV2 terminal in terminals)
                    {
                        terminalDir[terminal.ClientNo].StatisticsDate = sdt;
                        terminalDir[terminal.ClientNo].TerminalNo = terminal.ClientNo;
                    }
                    foreach (EnterOutLogInfo eol in enterOutLogList.FindAll(u => !string.IsNullOrEmpty(u.TerminalNum) && u.Flag == Operation.Reader))
                    {
                        if (!terminalDir.ContainsKey(eol.TerminalNum))
                        {
                            continue;
                        }
                        terminalDir[eol.TerminalNum].RushCardCount++;
                        //记录类型
                        switch (eol.EnterOutState)
                        {
                            case EnterOutLogType.BookingConfirmation:
                                terminalDir[eol.TerminalNum].CheckBespeakCount++;
                                break;
                            case EnterOutLogType.ComeBack:
                                terminalDir[eol.TerminalNum].ComeBackCount++;
                                break;
                            case EnterOutLogType.ContinuedTime:
                                terminalDir[eol.TerminalNum].ContinueTimeCount++;
                                break;
                            case EnterOutLogType.Leave:
                                terminalDir[eol.TerminalNum].LeaveCount++;
                                break;
                            case EnterOutLogType.ReselectSeat:
                                terminalDir[eol.TerminalNum].ReselectSeatCount++;
                                break;
                            case EnterOutLogType.SelectSeat:
                                terminalDir[eol.TerminalNum].SelectSeatCount++;
                                break;
                            case EnterOutLogType.ShortLeave:
                                terminalDir[eol.TerminalNum].ShortLeaveCount++;
                                break;
                            case EnterOutLogType.WaitingSuccess:
                                terminalDir[eol.TerminalNum].WaitSeatCount++;
                                break;
                        }
                    }

                    foreach (SeatManage.ClassModel.TerminalUsageStatistics terminalUS in terminalDir.Values)
                    {
                        SeatManage.ClassModel.TerminalUsageStatistics fts = ftsList.Find(u => u.TerminalNo == terminalUS.TerminalNo);
                        TerminalInfoV2 terminal = terminals.Find(u => u.ClientNo == terminalUS.TerminalNo);
                        if (fts != null)
                        {
                            terminalUS.IsChangePage = terminal.LastPrintTimes != fts.BeforePagePrintCount ? 1 : 0;
                            terminalUS.NowPagePrintCount = terminal.PrintedTimes;
                            terminalUS.BeforePagePrintCount = terminal.LastPrintTimes;
                            terminalUS.TodayPrintCount = terminal.LastPrintTimes != fts.BeforePagePrintCount ? terminal.LastPrintTimes - fts.NowPagePrintCount + terminal.PrintedTimes : terminal.PrintedTimes - fts.NowPagePrintCount;
                        }
                        else
                        {
                            terminalUS.IsChangePage = 0;
                            terminalUS.NowPagePrintCount = terminal.PrintedTimes;
                            terminalUS.BeforePagePrintCount = terminal.LastPrintTimes;
                            terminalUS.TodayPrintCount = 0;
                        }


                        if (!SeatUsageDataOperating.AddTerminalUsageStatistics(terminalUS))
                        {
                            WriteLog.Write(string.Format("数据统计服务：添加终端:{0} {1} 使用情况统计出错", terminalUS.TerminalNo, terminalUS.StatisticsDate));
                            throw new Exception(string.Format("数据统计服务：添加终端:{0} {1} 使用情况统计出错", terminalUS.TerminalNo, terminalUS.StatisticsDate));
                        }
                    }

                    sdt = sdt.AddDays(1);
                    if (sdt >= ServiceDateTime.Now.Date)
                    {
                        break;
                    }
                    terminalDir = null;
                }
                WriteLog.Write("数据统计服务：统计终端使用情况完成");
            }

            catch (Exception ex)
            {
                WriteLog.Write(string.Format("数据统计服务：统计阅终端人流量失败：{0}", ex.Message));
            }
        }
    }
}
