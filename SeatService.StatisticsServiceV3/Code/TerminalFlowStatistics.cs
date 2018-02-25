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
    public class TerminalFlowStatistics
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
                DateTime sdt = SeatUsageDataOperating.GetLastTerminalFlowStatisticsDate();
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
                    Dictionary<string, SeatManage.ClassModel.TerminalFlowStatistics> terminalDir = terminals.ToDictionary(u => u.ClientNo, u => new SeatManage.ClassModel.TerminalFlowStatistics());

                    //基本数据及排序处理
                    foreach (TerminalInfoV2 terminal in terminals)
                    {
                        terminalDir[terminal.ClientNo].StatisticsDate = sdt;
                        terminalDir[terminal.ClientNo].TerminalNo = terminal.ClientNo;
                        for (int i = 0; i < 24; i++)
                        {
                            terminalDir[terminal.ClientNo].CheckBespeakFlowDic.Add(i, 0);
                            terminalDir[terminal.ClientNo].ComeBackFlowDic.Add(i, 0);
                            terminalDir[terminal.ClientNo].ContinueTimeFlowDic.Add(i, 0);
                            terminalDir[terminal.ClientNo].LeaveFlowDic.Add(i, 0);
                            terminalDir[terminal.ClientNo].ReselectSeatFlowDic.Add(i, 0);
                            terminalDir[terminal.ClientNo].RushCardFlowDic.Add(i, 0);
                            terminalDir[terminal.ClientNo].SelectSeatFlowDic.Add(i, 0);
                            terminalDir[terminal.ClientNo].ShortLeaveFlowDic.Add(i, 0);
                            terminalDir[terminal.ClientNo].WaitSeatFlowDic.Add(i, 0);
                        }
                    }
                    foreach (EnterOutLogInfo eol in enterOutLogList.FindAll(u => !string.IsNullOrEmpty(u.TerminalNum) && u.Flag == Operation.Reader))
                    {
                        if (!terminalDir.ContainsKey(eol.TerminalNum))
                        {
                            continue;
                        }
                        terminalDir[eol.TerminalNum].RushCardFlowDic[eol.EnterOutTime.Hour]++;
                        //记录类型
                        switch (eol.EnterOutState)
                        {
                            case EnterOutLogType.BookingConfirmation:
                                terminalDir[eol.TerminalNum].CheckBespeakFlowDic[eol.EnterOutTime.Hour]++;
                                break;
                            case EnterOutLogType.ComeBack:
                                terminalDir[eol.TerminalNum].ComeBackFlowDic[eol.EnterOutTime.Hour]++;
                                break;
                            case EnterOutLogType.ContinuedTime:
                                terminalDir[eol.TerminalNum].ContinueTimeFlowDic[eol.EnterOutTime.Hour]++;
                                break;
                            case EnterOutLogType.Leave:
                                terminalDir[eol.TerminalNum].LeaveFlowDic[eol.EnterOutTime.Hour]++;
                                break;
                            case EnterOutLogType.ReselectSeat:
                                terminalDir[eol.TerminalNum].ReselectSeatFlowDic[eol.EnterOutTime.Hour]++;
                                break;
                            case EnterOutLogType.SelectSeat:
                                terminalDir[eol.TerminalNum].SelectSeatFlowDic[eol.EnterOutTime.Hour]++;
                                break;
                            case EnterOutLogType.ShortLeave:
                                terminalDir[eol.TerminalNum].ShortLeaveFlowDic[eol.EnterOutTime.Hour]++;
                                break;
                            case EnterOutLogType.WaitingSuccess:
                                terminalDir[eol.TerminalNum].WaitSeatFlowDic[eol.EnterOutTime.Hour]++;
                                break;
                        }
                    }
                    foreach (SeatManage.ClassModel.TerminalFlowStatistics terminalFS in terminalDir.Values)
                    {
                        foreach (KeyValuePair<int, int> item in terminalFS.CheckBespeakFlowDic)
                        {
                            terminalFS.CheckBespeakFlow += item.Key + ":" + item.Value + ";";
                        }
                        foreach (KeyValuePair<int, int> item in terminalFS.ComeBackFlowDic)
                        {
                            terminalFS.ComeBackFlow += item.Key + ":" + item.Value + ";";
                        }
                        foreach (KeyValuePair<int, int> item in terminalFS.ContinueTimeFlowDic)
                        {
                            terminalFS.ContinueTimeFlow += item.Key + ":" + item.Value + ";";
                        }
                        foreach (KeyValuePair<int, int> item in terminalFS.LeaveFlowDic)
                        {
                            terminalFS.LeaveFlow += item.Key + ":" + item.Value + ";";
                        }
                        foreach (KeyValuePair<int, int> item in terminalFS.ReselectSeatFlowDic)
                        {
                            terminalFS.ReselectSeatFlow += item.Key + ":" + item.Value + ";";
                        }
                        foreach (KeyValuePair<int, int> item in terminalFS.SelectSeatFlowDic)
                        {
                            terminalFS.SelectSeatFlow += item.Key + ":" + item.Value + ";";
                        }
                        foreach (KeyValuePair<int, int> item in terminalFS.ShortLeaveFlowDic)
                        {
                            terminalFS.ShortLeaveFlow += item.Key + ":" + item.Value + ";";
                        }
                        foreach (KeyValuePair<int, int> item in terminalFS.WaitSeatFlowDic)
                        {
                            terminalFS.WaitSeatFlow += item.Key + ":" + item.Value + ";";
                        }
                        foreach (KeyValuePair<int, int> item in terminalFS.RushCardFlowDic)
                        {
                            terminalFS.RushCardFlow += item.Key + ":" + item.Value + ";";
                        }
                        if (!SeatUsageDataOperating.AddTerminalFlowStatistics(terminalFS))
                        {
                            WriteLog.Write(string.Format("数据统计服务：添加终端:{0} {1} 人流量统计出错", terminalFS.TerminalNo, terminalFS.StatisticsDate));
                            throw new Exception(string.Format("数据统计服务：添加终端:{0} {1} 人流量统计出错", terminalFS.TerminalNo, terminalFS.StatisticsDate));
                        }
                    }

                    sdt = sdt.AddDays(1);
                    if (sdt >= ServiceDateTime.Now.Date)
                    {
                        break;
                    }
                    terminalDir = null;
                }
                WriteLog.Write("数据统计服务：统计终端人流量完成");
            }

            catch (Exception ex)
            {
                WriteLog.Write(string.Format("数据统计服务：统计阅终端人流量失败：{0}", ex.Message));
            }
        }
    }
}
