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
    public class RoomFlowStatistics
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
                List<ReadingRoomInfo> rooms = ClientConfigOperate.GetReadingRooms(null);
                DateTime sdt = SeatUsageDataOperating.GetLastRoomFlowStatisticsDate();
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
                    Dictionary<string, SeatManage.ClassModel.RoomFlowStatistics> roomDir = rooms.ToDictionary(room => room.No, room => new SeatManage.ClassModel.RoomFlowStatistics());

                    //基本数据及排序处理
                    foreach (ReadingRoomInfo room in rooms)
                    {
                        roomDir[room.No].StatisticsDate = sdt;
                        roomDir[room.No].ReadingRoomNo = room.No;
                        for (int i = 0; i < 24; i++)
                        {
                            roomDir[room.No].BespeakCheckFlowDic.Add(i, 0);
                            roomDir[room.No].ComeBackFlowDic.Add(i, 0);
                            roomDir[room.No].ContinueFlowDic.Add(i, 0);
                            roomDir[room.No].EnterFlowDic.Add(i, 0);
                            roomDir[room.No].LeaveFlowDic.Add(i, 0);
                            roomDir[room.No].OnSeatDic.Add(i, 0);
                            roomDir[room.No].OutFlowDic.Add(i, 0);
                            roomDir[room.No].ReselectFlowDic.Add(i, 0);
                            roomDir[room.No].SelectFlowDic.Add(i, 0);
                            roomDir[room.No].ShortLeaveFlowDic.Add(i, 0);
                            roomDir[room.No].WaitSelectFlowDic.Add(i, 0);
                        }
                    }
                    foreach (EnterOutLogInfo eol in enterOutLogList)
                    {

                        //记录类型
                        switch (eol.EnterOutState)
                        {
                            case EnterOutLogType.BookingConfirmation:
                                roomDir[eol.ReadingRoomNo].BespeakCheckFlowDic[eol.EnterOutTime.Hour]++;
                                roomDir[eol.ReadingRoomNo].EnterFlowDic[eol.EnterOutTime.Hour]++;
                                break;
                            case EnterOutLogType.ComeBack:
                                roomDir[eol.ReadingRoomNo].ComeBackFlowDic[eol.EnterOutTime.Hour]++;
                                break;
                            case EnterOutLogType.ContinuedTime:
                                roomDir[eol.ReadingRoomNo].ContinueFlowDic[eol.EnterOutTime.Hour]++;
                                break;
                            case EnterOutLogType.Leave:
                                roomDir[eol.ReadingRoomNo].LeaveFlowDic[eol.EnterOutTime.Hour]++;
                                roomDir[eol.ReadingRoomNo].OutFlowDic[eol.EnterOutTime.Hour]++;
                                break;
                            case EnterOutLogType.ReselectSeat:
                                roomDir[eol.ReadingRoomNo].ReselectFlowDic[eol.EnterOutTime.Hour]++;
                                roomDir[eol.ReadingRoomNo].EnterFlowDic[eol.EnterOutTime.Hour]++;
                                break;
                            case EnterOutLogType.SelectSeat:
                                roomDir[eol.ReadingRoomNo].SelectFlowDic[eol.EnterOutTime.Hour]++;
                                roomDir[eol.ReadingRoomNo].EnterFlowDic[eol.EnterOutTime.Hour]++;
                                break;
                            case EnterOutLogType.ShortLeave:
                                roomDir[eol.ReadingRoomNo].ShortLeaveFlowDic[eol.EnterOutTime.Hour]++;
                                break;
                            case EnterOutLogType.WaitingSuccess:
                                roomDir[eol.ReadingRoomNo].WaitSelectFlowDic[eol.EnterOutTime.Hour]++;
                                roomDir[eol.ReadingRoomNo].EnterFlowDic[eol.EnterOutTime.Hour]++;
                                break;
                        }
                    }
                    foreach (SeatManage.ClassModel.RoomFlowStatistics roomFS in roomDir.Values)
                    {
                        foreach (KeyValuePair<int, int> item in roomFS.BespeakCheckFlowDic)
                        {
                            roomFS.BespeakCheckFlow += item.Key + ":" + item.Value + ";";
                        }
                        foreach (KeyValuePair<int, int> item in roomFS.ComeBackFlowDic)
                        {
                            roomFS.ComeBackFlow += item.Key + ":" + item.Value + ";";
                        }
                        foreach (KeyValuePair<int, int> item in roomFS.ContinueFlowDic)
                        {
                            roomFS.ContinueFlow += item.Key + ":" + item.Value + ";";
                        }
                        foreach (KeyValuePair<int, int> item in roomFS.EnterFlowDic)
                        {
                            roomFS.EnterFlow += item.Key + ":" + item.Value + ";";
                        }
                        foreach (KeyValuePair<int, int> item in roomFS.LeaveFlowDic)
                        {
                            roomFS.LeaveFlow += item.Key + ":" + item.Value + ";";
                        }

                        foreach (KeyValuePair<int, int> item in roomFS.OutFlowDic)
                        {
                            roomFS.OutFlow += item.Key + ":" + item.Value + ";";
                        }
                        foreach (KeyValuePair<int, int> item in roomFS.ReselectFlowDic)
                        {
                            roomFS.ReselectFlow += item.Key + ":" + item.Value + ";";
                        }
                        foreach (KeyValuePair<int, int> item in roomFS.SelectFlowDic)
                        {
                            roomFS.SelectFlow += item.Key + ":" + item.Value + ";";
                        }
                        foreach (KeyValuePair<int, int> item in roomFS.ShortLeaveFlowDic)
                        {
                            roomFS.ShortLeaveFlow += item.Key + ":" + item.Value + ";";
                        }
                        foreach (KeyValuePair<int, int> item in roomFS.WaitSelectFlowDic)
                        {
                            roomFS.WaitSelectFlow += item.Key + ":" + item.Value + ";";
                        }
                        //计算在座人数
                        for (int i = 0; i < 24; i++)
                        {
                            roomFS.OnSeatDic[i] = i > 0 ? roomFS.OnSeatDic[i - 1] + roomFS.EnterFlowDic[i] - roomFS.OutFlowDic[i] : roomFS.EnterFlowDic[i] - roomFS.OutFlowDic[i];
                        }
                        foreach (KeyValuePair<int, int> item in roomFS.OnSeatDic)
                        {
                            roomFS.OnSeat += item.Key + ":" + item.Value + ";";
                        }
                        if (!SeatUsageDataOperating.AddRoomFlowStatistics(roomFS))
                        {
                            WriteLog.Write(string.Format("数据统计服务：添加阅览室:{0} {1} 人流量统计出错", roomFS.ReadingRoomNo, roomFS.StatisticsDate));
                            throw new Exception(string.Format("数据统计服务：添加阅览室:{0} {1} 人流量统计出错", roomFS.ReadingRoomNo, roomFS.StatisticsDate));
                        }
                    }

                    sdt = sdt.AddDays(1);
                    if (sdt >= ServiceDateTime.Now.Date)
                    {
                        break;
                    }
                    roomDir = null;
                }
                WriteLog.Write("数据统计服务：统计阅览室完成人流量完成");
            }

            catch (Exception ex)
            {
                WriteLog.Write(string.Format("数据统计服务：统计阅览室人流量失败：{0}", ex.Message));
            }
        }
    }
}
