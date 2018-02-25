using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;



namespace SeatManageWebV2.Code
{
    public class ReadingRoomStatistics
    {
        public delegate void EventHanleProgress(string message);
        public event EventHanleProgress Progress;
        public DataTable StatisticsHoursLibData(List<string> libNo, DateTime starttime, DateTime endtime)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Hour", typeof(string));
                dt.Columns.Add("EnterCount", typeof(int));
                dt.Columns.Add("OutCount", typeof(int));
                dt.Columns.Add("SeatCount", typeof(int));
                Dictionary<int, int> enterCountList = new Dictionary<int, int>();
                Dictionary<int, int> outCountList = new Dictionary<int, int>();
                for (int i = 0; i < 24; i++)
                {
                    enterCountList.Add(i, 0);
                    outCountList.Add(i, 0);
                }
                Dictionary<string, int> countDay = new Dictionary<string, int>();
                starttime = DateTime.Parse(starttime.Date.ToShortDateString() + " 0:00:00");
                endtime = DateTime.Parse(endtime.Date.ToShortDateString() + " 23:59:59");
                List<SeatManage.ClassModel.ReadingRoomInfo> rooms = SeatManage.Bll.T_SM_ReadingRoom.GetReadingRooms(null, libNo, null);
                foreach (SeatManage.ClassModel.ReadingRoomInfo room in rooms)
                {
                    List<SeatManage.ClassModel.EnterOutLogInfo> loglist = new List<SeatManage.ClassModel.EnterOutLogInfo>();
                    DateTime sTime = starttime;
                    DateTime eTime = endtime;
                    while (true)
                    {
                        if (sTime > endtime)
                        {
                            break;
                        }
                        eTime = sTime.AddDays(10);
                        if (eTime > endtime)
                        {
                            eTime = endtime;
                        }
                        List<SeatManage.ClassModel.EnterOutLogInfo> List = SeatManage.Bll.T_SM_EnterOutLog_bak.GetEnterOutLogs(null, room.No, null, sTime, eTime);
                        if (List.Count > 0)
                        {
                            loglist.AddRange(List);
                        }
                        sTime = sTime.AddDays(10);
                    }
                    foreach (SeatManage.ClassModel.EnterOutLogInfo info in loglist)
                    {
                        if (!countDay.ContainsKey(info.EnterOutTime.ToShortDateString()))
                        {
                            countDay.Add(info.EnterOutTime.ToShortDateString(), 0);
                        }
                        switch (info.EnterOutState)
                        {
                            case SeatManage.EnumType.EnterOutLogType.BookingConfirmation:
                            case SeatManage.EnumType.EnterOutLogType.ReselectSeat:
                            case SeatManage.EnumType.EnterOutLogType.SelectSeat:
                            case SeatManage.EnumType.EnterOutLogType.WaitingSuccess:
                                //case SeatManage.EnumType.EnterOutLogType.ComeBack:
                                enterCountList[info.EnterOutTime.Hour]++; break;
                            case SeatManage.EnumType.EnterOutLogType.Leave:
                                //case SeatManage.EnumType.EnterOutLogType.ShortLeave:
                                outCountList[info.EnterOutTime.Hour]++; break;
                        }
                    }
                }
                int daycount = countDay.Count;
                if (daycount == 0)
                {
                    daycount = 1;
                }
                for (int i = 6; i < 24; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["Hour"] = i.ToString() + ":00";
                    dr["EnterCount"] = enterCountList[i] / daycount;
                    dr["OutCount"] = outCountList[i] / daycount;
                    int seatconut = 0;
                    for (int j = 6; j < i + 1; j++)
                    {
                        seatconut += enterCountList[j] / daycount;
                        seatconut -= outCountList[j] / daycount;
                    }
                    dr["SeatCount"] = seatconut;
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 按小时统计
        /// </summary>
        public SeatManage.ClassModel.ReadingRoomUsageStatistics StatisticsHoursRoomData(SeatManage.ClassModel.ReadingRoomInfo room, DateTime starttime, DateTime endtime)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Hour", typeof(string));
                dt.Columns.Add("EnterCount", typeof(int));
                dt.Columns.Add("OutCount", typeof(int));
                dt.Columns.Add("SeatCount", typeof(int));
                Dictionary<int, int> enterCountList = new Dictionary<int, int>();
                Dictionary<int, int> outCountList = new Dictionary<int, int>();
                for (int i = 0; i < 24; i++)
                {
                    enterCountList.Add(i, 0);
                    outCountList.Add(i, 0);
                }
                Dictionary<string, int> countDay = new Dictionary<string, int>();
                starttime = DateTime.Parse(starttime.Date.ToShortDateString() + " 0:00:00");
                endtime = DateTime.Parse(endtime.Date.ToShortDateString() + " 23:59:59");
                //List<SeatManage.ClassModel.EnterOutLogInfo> loglist = SeatManage.Bll.T_SM_EnterOutLog_bak.GetEnterOutLogs(null, room.No, null, starttime, endtime);
                List<SeatManage.ClassModel.EnterOutLogInfo> loglist = new List<SeatManage.ClassModel.EnterOutLogInfo>();
                DateTime sTime = starttime;
                DateTime eTime = endtime;
                while (true)
                {
                    if (sTime > endtime)
                    {
                        break;
                    }
                    eTime = sTime.AddDays(10);
                    if (eTime > endtime)
                    {
                        eTime = endtime;
                    }
                    List<SeatManage.ClassModel.EnterOutLogInfo> List = SeatManage.Bll.T_SM_EnterOutLog_bak.GetEnterOutLogs(null, room.No, null, sTime, eTime);
                    if (List.Count > 0)
                    {
                        loglist.AddRange(List);
                    }
                    sTime = sTime.AddDays(10);
                }

                foreach (SeatManage.ClassModel.EnterOutLogInfo info in loglist)
                {
                    if (!countDay.ContainsKey(info.EnterOutTime.ToShortDateString()))
                    {
                        countDay.Add(info.EnterOutTime.ToShortDateString(), 0);
                    }
                    switch (info.EnterOutState)
                    {
                        case SeatManage.EnumType.EnterOutLogType.BookingConfirmation:
                        case SeatManage.EnumType.EnterOutLogType.ReselectSeat:
                        case SeatManage.EnumType.EnterOutLogType.SelectSeat:
                        case SeatManage.EnumType.EnterOutLogType.WaitingSuccess:
                            //case SeatManage.EnumType.EnterOutLogType.ComeBack:
                            enterCountList[info.EnterOutTime.Hour]++; break;
                        case SeatManage.EnumType.EnterOutLogType.Leave:
                            //case SeatManage.EnumType.EnterOutLogType.ShortLeave:
                            outCountList[info.EnterOutTime.Hour]++; break;
                    }
                }
                SeatManage.ClassModel.ReadingRoomUsageStatistics statisticsRoomData = new SeatManage.ClassModel.ReadingRoomUsageStatistics();
                statisticsRoomData.RoomInfo = room;
                statisticsRoomData.StartDate = starttime;
                statisticsRoomData.EndDate = endtime;
                int daycount = countDay.Count;
                if (daycount == 0)
                {
                    daycount = 1;
                }
                for (int i = 6; i < 24; i++)
                {
                    statisticsRoomData.EnterCount += enterCountList[i];
                    statisticsRoomData.OutCount += outCountList[i];
                    statisticsRoomData.EnterOurCount += outCountList[i];
                    DataRow dr = dt.NewRow();
                    dr["Hour"] = (i + 1).ToString() + ":00";
                    dr["EnterCount"] = enterCountList[i] / daycount;
                    dr["OutCount"] = outCountList[i] / daycount;
                    int seatconut = 0;
                    for (int j = 6; j < i + 1; j++)
                    {
                        seatconut += enterCountList[j] / daycount;
                        seatconut -= outCountList[j] / daycount;
                    }
                    dr["SeatCount"] = seatconut;
                    dt.Rows.Add(dr);
                }
                statisticsRoomData.StatisticsData = dt;
                statisticsRoomData.EnterCount = statisticsRoomData.EnterCount / daycount;
                statisticsRoomData.OutCount = statisticsRoomData.OutCount / daycount;
                statisticsRoomData.EnterOurCount = statisticsRoomData.EnterOurCount / daycount;
                return statisticsRoomData;
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 按照日期统计图书馆
        /// </summary>
        public SeatManage.ClassModel.ReadingRoomUsageStatistics StatisticsDayLibData(List<string> libNo, DateTime starttime, DateTime endtime, SeatManage.ClassModel.StatisticsType type)
        {
            try
            {
                Dictionary<int, int> enteroutCountList = new Dictionary<int, int>();
                Dictionary<int, Dictionary<string, int>> dateDay = new Dictionary<int, Dictionary<string, int>>();
                for (int i = 0; i < (int)type + 1; i++)
                {
                    enteroutCountList.Add(i, 0);
                    dateDay.Add(i, new Dictionary<string, int>());
                }
                starttime = DateTime.Parse(starttime.Date.ToShortDateString() + " 0:00:00");
                endtime = DateTime.Parse(endtime.Date.ToShortDateString() + " 23:59:59");
                List<SeatManage.ClassModel.ReadingRoomInfo> rooms = SeatManage.Bll.T_SM_ReadingRoom.GetReadingRooms(null, libNo, null);
                foreach (SeatManage.ClassModel.ReadingRoomInfo room in rooms)
                {
                    room.SeatList = SeatManage.Bll.EnterOutOperate.GetRoomSeatLayOut(room.No);
                    //List<SeatManage.ClassModel.EnterOutLogStatistics> staList = SeatManage.Bll.T_SM_EnterOutLogStatistics.GetEnterOutLogStatisticsByDate(room.No, starttime.ToString(), endtime.ToString());
                    List<SeatManage.ClassModel.EnterOutLogStatistics> staList = new List<SeatManage.ClassModel.EnterOutLogStatistics>();
                    DateTime sTime = starttime;
                    DateTime eTime = endtime;
                    while (true)
                    {
                        if (sTime > endtime)
                        {
                            break;
                        }
                        eTime = sTime.AddDays(10);
                        if (eTime > endtime)
                        {
                            eTime = endtime;
                        }
                        List<SeatManage.ClassModel.EnterOutLogStatistics> List = SeatManage.Bll.T_SM_EnterOutLogStatistics.GetEnterOutLogStatisticsByDate(room.No, sTime.ToString(), eTime.ToString());
                        if (List.Count > 0)
                        {
                            staList.AddRange(List);
                        }
                        sTime = sTime.AddDays(10);
                    }
                    foreach (SeatManage.ClassModel.EnterOutLogStatistics log in staList)
                    {
                        int d = 1;
                        switch (type)
                        {
                            case SeatManage.ClassModel.StatisticsType.DayOfWeek:
                                d = (int)log.SelectSeatTime.DayOfWeek;
                                enteroutCountList[d]++;
                                if (!dateDay[d].ContainsKey(log.SelectSeatTime.ToShortDateString()))
                                {
                                    dateDay[d].Add(log.SelectSeatTime.ToShortDateString(), 0);
                                }
                                break;
                            case SeatManage.ClassModel.StatisticsType.DayOfMonth:
                                d = log.SelectSeatTime.Day;
                                enteroutCountList[d]++;
                                if (!dateDay[d].ContainsKey(log.SelectSeatTime.ToShortDateString()))
                                {
                                    dateDay[d].Add(log.SelectSeatTime.ToShortDateString(), 0);
                                }
                                break;
                            case SeatManage.ClassModel.StatisticsType.DayOfYear:
                                d = log.SelectSeatTime.DayOfYear;
                                enteroutCountList[d]++;
                                if (!dateDay[d].ContainsKey(log.SelectSeatTime.ToShortDateString()))
                                {
                                    dateDay[d].Add(log.SelectSeatTime.ToShortDateString(), 0);
                                }
                                break;
                            case SeatManage.ClassModel.StatisticsType.WeekOfYear:
                                int fd = (int)DateTime.Parse(log.SelectSeatTime.Year.ToString() + "-1-1").DayOfWeek;
                                int nd = log.SelectSeatTime.DayOfYear;
                                d = (nd + fd - 1) / 7;
                                enteroutCountList[d]++;
                                if (!dateDay[d].ContainsKey(log.SelectSeatTime.Year.ToString()))
                                {
                                    dateDay[d].Add(log.SelectSeatTime.Year.ToString(), 0);
                                }
                                break;
                            case SeatManage.ClassModel.StatisticsType.MonthOfYear:
                                d = log.SelectSeatTime.Month;
                                enteroutCountList[d]++;
                                if (!dateDay[d].ContainsKey(log.SelectSeatTime.Year.ToString()))
                                {
                                    dateDay[d].Add(log.SelectSeatTime.Year.ToString(), 0);
                                }
                                break;
                        }
                    }
                }
                int seatcount = 0;
                foreach (SeatManage.ClassModel.ReadingRoomInfo room in rooms)
                {
                    seatcount += room.SeatList.Seats.Count;
                }
                if (seatcount == 0)
                {
                    seatcount = 1;
                }
                int daycount = 0;
                foreach (KeyValuePair<int, Dictionary<string, int>> item in dateDay)
                {
                    daycount += item.Value.Count;
                }
                if (daycount == 0)
                {
                    daycount = 1;
                }
                SeatManage.ClassModel.ReadingRoomUsageStatistics statisticsRoomData = new SeatManage.ClassModel.ReadingRoomUsageStatistics();
                statisticsRoomData.StartDate = starttime;
                statisticsRoomData.EndDate = endtime;
                DataTable dt = new DataTable();
                dt.Columns.Add("EnterOutCount", typeof(int));
                dt.Columns.Add("Attendance", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                if (type == SeatManage.ClassModel.StatisticsType.DayOfWeek)
                {
                    for (int i = 0; i < (int)type; i++)
                    {
                        DataRow dr = dt.NewRow();
                        switch (i)
                        {
                            case 0: dr["Date"] = "星期天"; break;
                            case 1: dr["Date"] = "星期一"; break;
                            case 2: dr["Date"] = "星期二"; break;
                            case 3: dr["Date"] = "星期三"; break;
                            case 4: dr["Date"] = "星期四"; break;
                            case 5: dr["Date"] = "星期五"; break;
                            case 6: dr["Date"] = "星期六"; break;
                        }
                        if (dateDay[i].Count > 0)
                        {
                            dr["EnterOutCount"] = enteroutCountList[i] / dateDay[i].Count;
                            dr["Attendance"] = (float.Parse(enteroutCountList[i].ToString()) / float.Parse(dateDay[i].Count.ToString()) / float.Parse(seatcount.ToString()) * 100).ToString("0.00");
                        }
                        else
                        {
                            dr["EnterOutCount"] = enteroutCountList[i];
                            dr["Attendance"] = (float.Parse(enteroutCountList[i].ToString()) / float.Parse(seatcount.ToString()) * 100).ToString("0.00");
                        }
                        dt.Rows.Add(dr);
                        statisticsRoomData.EnterOurCount += enteroutCountList[i];
                    }
                }
                else
                {
                    for (int i = 1; i < (int)type + 1; i++)
                    {
                        DataRow dr = dt.NewRow();
                        switch (type)
                        {
                            case SeatManage.ClassModel.StatisticsType.DayOfMonth:
                                dr["Date"] = i + "日";
                                break;
                            case SeatManage.ClassModel.StatisticsType.DayOfYear:
                                dr["Date"] = "第" + i + "天";
                                break;
                            case SeatManage.ClassModel.StatisticsType.WeekOfYear:
                                dr["Date"] = "第" + i + "周";
                                break;
                            case SeatManage.ClassModel.StatisticsType.MonthOfYear:
                                dr["Date"] = i + "月";
                                break;
                        }
                        if (dateDay[i].Count > 0)
                        {
                            dr["EnterOutCount"] = enteroutCountList[i] / dateDay[i].Count;
                            dr["Attendance"] = (float.Parse(enteroutCountList[i].ToString()) / float.Parse(dateDay[i].Count.ToString()) / float.Parse(seatcount.ToString()) * 100).ToString("0.00");
                        }
                        else
                        {
                            dr["EnterOutCount"] = enteroutCountList[i];
                            dr["Attendance"] = (float.Parse(enteroutCountList[i].ToString()) / float.Parse(seatcount.ToString()) * 100).ToString("0.00");
                        }
                        dt.Rows.Add(dr);
                        statisticsRoomData.EnterOurCount += enteroutCountList[i];
                    }
                }
                statisticsRoomData.StatisticsData = dt;
                statisticsRoomData.Attendance = float.Parse(statisticsRoomData.EnterOurCount.ToString()) / float.Parse(daycount.ToString()) / float.Parse(seatcount.ToString());
                return statisticsRoomData;
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 按照日期统计阅览室
        /// </summary>
        public SeatManage.ClassModel.ReadingRoomUsageStatistics StatisticsDayRoomData(SeatManage.ClassModel.ReadingRoomInfo room, DateTime starttime, DateTime endtime, SeatManage.ClassModel.StatisticsType type)
        {
            try
            {
                Dictionary<int, int> enteroutCountList = new Dictionary<int, int>();
                Dictionary<int, Dictionary<string, int>> dateDay = new Dictionary<int, Dictionary<string, int>>();
                for (int i = 0; i < (int)type + 1; i++)
                {
                    enteroutCountList.Add(i, 0);
                    dateDay.Add(i, new Dictionary<string, int>());
                }
                starttime = DateTime.Parse(starttime.Date.ToShortDateString() + " 0:00:00");
                endtime = DateTime.Parse(endtime.Date.ToShortDateString() + " 23:59:59");
                room.SeatList = SeatManage.Bll.EnterOutOperate.GetRoomSeatLayOut(room.No);
                //List<SeatManage.ClassModel.EnterOutLogStatistics> staList = SeatManage.Bll.T_SM_EnterOutLogStatistics.GetEnterOutLogStatisticsByDate(room.No, starttime.ToString(), endtime.ToString());
                List<SeatManage.ClassModel.EnterOutLogStatistics> staList = new List<SeatManage.ClassModel.EnterOutLogStatistics>();
                DateTime sTime = starttime;
                DateTime eTime = endtime;
                while (true)
                {
                    if (sTime > endtime)
                    {
                        break;
                    }
                    eTime = sTime.AddDays(10);
                    if (eTime > endtime)
                    {
                        eTime = endtime;
                    }
                    List<SeatManage.ClassModel.EnterOutLogStatistics> List = SeatManage.Bll.T_SM_EnterOutLogStatistics.GetEnterOutLogStatisticsByDate(room.No, sTime.ToString(), eTime.ToString());
                    if (List.Count > 0)
                    {
                        staList.AddRange(List);
                    }
                    sTime = sTime.AddDays(10);
                }
                foreach (SeatManage.ClassModel.EnterOutLogStatistics log in staList)
                {
                    int d = 1;
                    switch (type)
                    {
                        case SeatManage.ClassModel.StatisticsType.DayOfWeek:
                            d = (int)log.SelectSeatTime.DayOfWeek;
                            enteroutCountList[d]++;
                            if (!dateDay[d].ContainsKey(log.SelectSeatTime.ToShortDateString()))
                            {
                                dateDay[d].Add(log.SelectSeatTime.ToShortDateString(), 0);
                            }
                            break;
                        case SeatManage.ClassModel.StatisticsType.DayOfMonth:
                            d = log.SelectSeatTime.Day;
                            enteroutCountList[d]++;
                            if (!dateDay[d].ContainsKey(log.SelectSeatTime.ToShortDateString()))
                            {
                                dateDay[d].Add(log.SelectSeatTime.ToShortDateString(), 0);
                            }
                            break;
                        case SeatManage.ClassModel.StatisticsType.DayOfYear:
                            d = log.SelectSeatTime.DayOfYear;
                            enteroutCountList[d]++;
                            if (!dateDay[d].ContainsKey(log.SelectSeatTime.ToShortDateString()))
                            {
                                dateDay[d].Add(log.SelectSeatTime.ToShortDateString(), 0);
                            }
                            break;
                        case SeatManage.ClassModel.StatisticsType.WeekOfYear:
                            int fd = (int)DateTime.Parse(log.SelectSeatTime.Year.ToString() + "-1-1").DayOfWeek;
                            int nd = log.SelectSeatTime.DayOfYear;
                            d = (nd + fd - 1) / 7;
                            enteroutCountList[d]++;
                            if (!dateDay[d].ContainsKey(log.SelectSeatTime.Year.ToString()))
                            {
                                dateDay[d].Add(log.SelectSeatTime.Year.ToString(), 0);
                            }
                            break;
                        case SeatManage.ClassModel.StatisticsType.MonthOfYear:
                            d = log.SelectSeatTime.Month;
                            enteroutCountList[d]++;
                            if (!dateDay[d].ContainsKey(log.SelectSeatTime.Year.ToString()))
                            {
                                dateDay[d].Add(log.SelectSeatTime.Year.ToString(), 0);
                            }
                            break;
                    }
                }
                int daycount = 0;
                foreach (KeyValuePair<int, Dictionary<string, int>> item in dateDay)
                {
                    daycount += item.Value.Count;
                }
                if (daycount == 0)
                {
                    daycount = 1;
                }
                SeatManage.ClassModel.ReadingRoomUsageStatistics statisticsRoomData = new SeatManage.ClassModel.ReadingRoomUsageStatistics();
                statisticsRoomData.RoomInfo = room;
                statisticsRoomData.StartDate = starttime;
                statisticsRoomData.EndDate = endtime;
                DataTable dt = new DataTable();
                dt.Columns.Add("EnterOutCount", typeof(int));
                dt.Columns.Add("Attendance", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                if (type == SeatManage.ClassModel.StatisticsType.DayOfWeek)
                {
                    for (int i = 0; i < (int)type; i++)
                    {
                        DataRow dr = dt.NewRow();
                        switch (i)
                        {
                            case 0: dr["Date"] = "星期天"; break;
                            case 1: dr["Date"] = "星期一"; break;
                            case 2: dr["Date"] = "星期二"; break;
                            case 3: dr["Date"] = "星期三"; break;
                            case 4: dr["Date"] = "星期四"; break;
                            case 5: dr["Date"] = "星期五"; break;
                            case 6: dr["Date"] = "星期六"; break;
                        }
                        if (dateDay[i].Count > 0)
                        {
                            dr["EnterOutCount"] = enteroutCountList[i] / dateDay[i].Count;
                            dr["Attendance"] = (float.Parse(enteroutCountList[i].ToString()) / float.Parse(dateDay[i].Count.ToString()) / float.Parse(statisticsRoomData.RoomInfo.SeatList.Seats.Count.ToString()) * 100).ToString("0.00");
                        }
                        else
                        {
                            dr["EnterOutCount"] = enteroutCountList[i];
                            dr["Attendance"] = (float.Parse(enteroutCountList[i].ToString()) / float.Parse(statisticsRoomData.RoomInfo.SeatList.Seats.Count.ToString()) * 100).ToString("0.00");
                        }
                        dt.Rows.Add(dr);
                        statisticsRoomData.EnterOurCount += enteroutCountList[i];
                    }
                }
                else
                {
                    for (int i = 1; i < (int)type + 1; i++)
                    {
                        DataRow dr = dt.NewRow();
                        switch (type)
                        {
                            case SeatManage.ClassModel.StatisticsType.DayOfMonth:
                                dr["Date"] = i + "日";
                                break;
                            case SeatManage.ClassModel.StatisticsType.DayOfYear:
                                dr["Date"] = "第" + i + "天";
                                break;
                            case SeatManage.ClassModel.StatisticsType.WeekOfYear:
                                dr["Date"] = "第" + i + "周";
                                break;
                            case SeatManage.ClassModel.StatisticsType.MonthOfYear:
                                dr["Date"] = i + "月";
                                break;
                        }
                        if (dateDay[i].Count > 0)
                        {
                            dr["EnterOutCount"] = enteroutCountList[i] / dateDay[i].Count;
                            dr["Attendance"] = (float.Parse(enteroutCountList[i].ToString()) / float.Parse(dateDay[i].Count.ToString()) / float.Parse(statisticsRoomData.RoomInfo.SeatList.Seats.Count.ToString()) * 100).ToString("0.00");
                        }
                        else
                        {
                            dr["EnterOutCount"] = enteroutCountList[i];
                            dr["Attendance"] = (float.Parse(enteroutCountList[i].ToString()) / float.Parse(statisticsRoomData.RoomInfo.SeatList.Seats.Count.ToString()) * 100).ToString("0.00");
                        }
                        dt.Rows.Add(dr);
                        statisticsRoomData.EnterOurCount += enteroutCountList[i];
                    }
                }
                statisticsRoomData.StatisticsData = dt;
                statisticsRoomData.Attendance = float.Parse(statisticsRoomData.EnterOurCount.ToString()) / float.Parse(daycount.ToString()) / float.Parse(statisticsRoomData.RoomInfo.SeatList.Seats.Count.ToString());
                return statisticsRoomData;
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 统计记录记录
        /// </summary>
        public SeatManage.ClassModel.ReadingRoomUsageStatistics StatisticsSelectAndLeave(List<SeatManage.ClassModel.ReadingRoomInfo> rooms, DateTime starttime, DateTime endtime)
        {
            try
            {
                starttime = DateTime.Parse(starttime.Date.ToShortDateString() + " 0:00:00");
                endtime = DateTime.Parse(endtime.Date.ToShortDateString() + " 23:59:59");
                Dictionary<SeatManage.ClassModel.EnterOutLogSelectSeatMode, int> selectCountList = new Dictionary<SeatManage.ClassModel.EnterOutLogSelectSeatMode, int>();
                selectCountList.Add(SeatManage.ClassModel.EnterOutLogSelectSeatMode.AdminAllocation, 0);
                selectCountList.Add(SeatManage.ClassModel.EnterOutLogSelectSeatMode.BookAdmission, 0);
                selectCountList.Add(SeatManage.ClassModel.EnterOutLogSelectSeatMode.ReadCardSelect, 0);
                selectCountList.Add(SeatManage.ClassModel.EnterOutLogSelectSeatMode.ReSelect, 0);
                selectCountList.Add(SeatManage.ClassModel.EnterOutLogSelectSeatMode.WaitAdmission, 0);
                Dictionary<SeatManage.ClassModel.EnterOutLogLeaveSeatMode, int> leaveCountList = new Dictionary<SeatManage.ClassModel.EnterOutLogLeaveSeatMode, int>();
                leaveCountList.Add(SeatManage.ClassModel.EnterOutLogLeaveSeatMode.AdminReleased, 0);
                leaveCountList.Add(SeatManage.ClassModel.EnterOutLogLeaveSeatMode.ReaderReleased, 0);
                leaveCountList.Add(SeatManage.ClassModel.EnterOutLogLeaveSeatMode.ServerReleased, 0);
                Dictionary<string, int> seatTimeList = new Dictionary<string, int>();
                seatTimeList.Add("<1小时", 0);
                seatTimeList.Add("1-2小时", 0);
                seatTimeList.Add("2-5小时", 0);
                seatTimeList.Add(">5小时", 0);
                foreach (SeatManage.ClassModel.ReadingRoomInfo room in rooms)
                {
                    //List<SeatManage.ClassModel.EnterOutLogStatistics> staList = SeatManage.Bll.T_SM_EnterOutLogStatistics.GetEnterOutLogStatisticsByDate(room.No, starttime.ToString(), endtime.ToString());
                    List<SeatManage.ClassModel.EnterOutLogStatistics> staList = new List<SeatManage.ClassModel.EnterOutLogStatistics>();
                    DateTime sTime = starttime;
                    DateTime eTime = endtime;
                    while (true)
                    {
                        if (sTime > endtime)
                        {
                            break;
                        }
                        eTime = sTime.AddDays(10);
                        if (eTime > endtime)
                        {
                            eTime = endtime;
                        }
                        List<SeatManage.ClassModel.EnterOutLogStatistics> List = SeatManage.Bll.T_SM_EnterOutLogStatistics.GetEnterOutLogStatisticsByDate(room.No, sTime.ToString(), eTime.ToString());
                        if (List.Count > 0)
                        {
                            staList.AddRange(List);
                        }
                        sTime = sTime.AddDays(10);
                    }
                    foreach (SeatManage.ClassModel.EnterOutLogStatistics log in staList)
                    {
                        selectCountList[log.SelectSeat]++;
                        leaveCountList[log.LeaveSeat]++;
                        if (log.SeatTime < 60)
                        {
                            seatTimeList["<1小时"]++;
                        }
                        else if (log.SeatTime < 120)
                        {
                            seatTimeList["1-2小时"]++;
                        }
                        else if (log.SeatTime < 300)
                        {
                            seatTimeList["2-5小时"]++;
                        }
                        else
                        {
                            seatTimeList[">5小时"]++;
                        }
                    }
                }
                SeatManage.ClassModel.ReadingRoomUsageStatistics statisticsRoomData = new SeatManage.ClassModel.ReadingRoomUsageStatistics();
                statisticsRoomData.StartDate = starttime;
                statisticsRoomData.EndDate = endtime;
                DataTable dt_st = new DataTable();
                dt_st.Columns.Add("SelectType", typeof(string));
                dt_st.Columns.Add("Count", typeof(int));
                foreach (KeyValuePair<SeatManage.ClassModel.EnterOutLogSelectSeatMode, int> item in selectCountList)
                {
                    DataRow dr = dt_st.NewRow();
                    switch (item.Key)
                    {
                        case SeatManage.ClassModel.EnterOutLogSelectSeatMode.AdminAllocation:
                            dr["SelectType"] = "管理员分配"; break;
                        case SeatManage.ClassModel.EnterOutLogSelectSeatMode.BookAdmission:
                            dr["SelectType"] = "预约座位"; break;
                        case SeatManage.ClassModel.EnterOutLogSelectSeatMode.ReadCardSelect:
                            dr["SelectType"] = "刷卡选座"; break;
                        case SeatManage.ClassModel.EnterOutLogSelectSeatMode.ReSelect:
                            dr["SelectType"] = "重新选座"; break;
                        case SeatManage.ClassModel.EnterOutLogSelectSeatMode.WaitAdmission:
                            dr["SelectType"] = "等待入座"; break;
                    }
                    dr["Count"] = item.Value;
                    dt_st.Rows.Add(dr);
                }
                DataTable dt_lt = new DataTable();
                dt_lt.Columns.Add("LeaveType", typeof(string));
                dt_lt.Columns.Add("Count", typeof(int));
                foreach (KeyValuePair<SeatManage.ClassModel.EnterOutLogLeaveSeatMode, int> item in leaveCountList)
                {
                    DataRow dr = dt_lt.NewRow();
                    switch (item.Key)
                    {
                        case SeatManage.ClassModel.EnterOutLogLeaveSeatMode.AdminReleased:
                            dr["LeaveType"] = "管理员释放"; break;
                        case SeatManage.ClassModel.EnterOutLogLeaveSeatMode.ReaderReleased:
                            dr["LeaveType"] = "手动释放"; break;
                        case SeatManage.ClassModel.EnterOutLogLeaveSeatMode.ServerReleased:
                            dr["LeaveType"] = "被系统释放"; break;
                    }
                    dr["Count"] = item.Value;
                    dt_lt.Rows.Add(dr);
                }
                DataTable dt_t = new DataTable();
                dt_t.Columns.Add("SeatTime", typeof(string));
                dt_t.Columns.Add("Count", typeof(int));
                foreach (KeyValuePair<string, int> item in seatTimeList)
                {
                    DataRow dr = dt_t.NewRow();
                    dr["SeatTime"] = item.Key;
                    dr["Count"] = item.Value;
                    dt_t.Rows.Add(dr);
                }
                statisticsRoomData.SeatSelect = dt_st;
                statisticsRoomData.SeatLeave = dt_lt;
                statisticsRoomData.SeatTime = dt_t;
                return statisticsRoomData;
            }
            catch(Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 统计阅览室信息
        /// </summary>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        public HtmlTable GetReadingRoomUsageInfo(string roomNo, DateTime starttime, DateTime endtime)
        {
            starttime = DateTime.Parse(starttime.Date.ToShortDateString() + " 0:00:00");
            endtime = DateTime.Parse(endtime.Date.ToShortDateString() + " 23:59:59");
            SeatManage.ClassModel.ReadingRoomInfo room = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(roomNo);
            room.SeatList = SeatManage.Bll.EnterOutOperate.GetRoomSeatLayOut(room.No);
            //List<SeatManage.ClassModel.EnterOutLogStatistics> staList = SeatManage.Bll.T_SM_EnterOutLogStatistics.GetEnterOutLogStatisticsByDate(room.No, starttime.ToString(), endtime.ToString());
            List<SeatManage.ClassModel.EnterOutLogStatistics> staList = new List<SeatManage.ClassModel.EnterOutLogStatistics>();
            DateTime sTime = starttime;
            DateTime eTime = endtime;
            while (true)
            {
                if (sTime > endtime)
                {
                    break;
                }
                eTime = sTime.AddDays(10);
                if (eTime > endtime)
                {
                    eTime = endtime;
                }
                List<SeatManage.ClassModel.EnterOutLogStatistics> List = SeatManage.Bll.T_SM_EnterOutLogStatistics.GetEnterOutLogStatisticsByDate(room.No, sTime.ToString(), eTime.ToString());
                if (List.Count > 0)
                {
                    staList.AddRange(List);
                }
                sTime = sTime.AddDays(10);
            }
            double seatTime = 0;
            double seatCount = 0;
            double otime = (DateTime.Parse(room.Setting.RoomOpenSet.DefaultOpenTime.EndTime) - DateTime.Parse(room.Setting.RoomOpenSet.DefaultOpenTime.BeginTime)).TotalHours;
            Dictionary<string, int> daycount = new Dictionary<string, int>();
            foreach (SeatManage.ClassModel.EnterOutLogStatistics eols in staList)
            {
                if (!daycount.ContainsKey(eols.SelectSeatTime.ToShortDateString()))
                {
                    daycount.Add(eols.SelectSeatTime.ToShortDateString(), 0);
                }
                if (otime > 0)
                {
                    if (eols.SeatTime <= otime)
                    {
                        seatTime += eols.SeatTime;
                        seatCount += 1;
                    }
                }
                else
                {
                    if (eols.SeatTime <= 24)
                    {
                        seatTime += eols.SeatTime;
                        seatCount += 1;
                    }
                }

            }
            HtmlTable roomInfoTable = new HtmlTable();
            roomInfoTable.BorderColor = "#2E9ED7";
            roomInfoTable.Width = "450px";
            roomInfoTable.Border = 1;
            HtmlTableRow tr = new HtmlTableRow();
            HtmlTableCell td = new HtmlTableCell();
            //阅览室标题
            td.InnerText = room.Name;
            td.Align = "center";
            td.ColSpan = 4;
            td.BgColor = "#FEFFDC";
            tr.Cells.Add(td);
            roomInfoTable.Rows.Add(tr);
            //阅览室编号&座位数
            tr = new HtmlTableRow();
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            td.InnerText = "阅览室编号：";
            tr.Cells.Add(td);
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            td.InnerText = room.No;
            td.ColSpan = 3;
            tr.Cells.Add(td);
            roomInfoTable.Rows.Add(tr);
            //阅览室所属

            //阅览室开闭馆时间
            tr = new HtmlTableRow();
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            td.InnerText = "开放时间：";
            tr.Cells.Add(td);
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            td.InnerText = room.Setting.RoomOpenSet.DefaultOpenTime.BeginTime;
            tr.Cells.Add(td);
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            td.InnerText = "关闭时间：";
            tr.Cells.Add(td);
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            td.InnerText = room.Setting.RoomOpenSet.DefaultOpenTime.EndTime;
            tr.Cells.Add(td);
            roomInfoTable.Rows.Add(tr);
            //阅览室开闭馆时长
            tr = new HtmlTableRow();
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            td.InnerText = "开放时长：";
            tr.Cells.Add(td);
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            if (otime > 0)
            {
                td.InnerText = otime.ToString("0.00") + "小时";
            }
            else
            {
                td.InnerText = "0小时";
            }
            tr.Cells.Add(td);
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            td.InnerText = "累计时长：";
            tr.Cells.Add(td);
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            double opentime = 0;
            List<SeatManage.ClassModel.ReadingRoomOpenCloseLogInfo> ocLogs = SeatManage.Bll.T_SM_RROpenCloseLog.GetReadingRoomOClog(room.No, SeatManage.EnumType.LogStatus.Fail, starttime.ToString(), endtime.ToString());
            foreach (SeatManage.ClassModel.ReadingRoomOpenCloseLogInfo oclog in ocLogs)
            {
                if (oclog.OpenCloseState == SeatManage.EnumType.ReadingRoomStatus.Open)
                {
                    SeatManage.ClassModel.ReadingRoomOpenCloseLogInfo closelog = ocLogs.Find(delegate(SeatManage.ClassModel.ReadingRoomOpenCloseLogInfo r) { return r.OpenCloseState == SeatManage.EnumType.ReadingRoomStatus.Close && r.OperateNo == oclog.OperateNo && (r.OperateTime.Date - oclog.OperateTime.Date).Days == 0; });
                    if (closelog != null)
                    {
                        opentime += (closelog.OperateTime - oclog.OperateTime).TotalHours;
                    }
                }
            }
            if (opentime < 0)
            {
                opentime = 0;
            }
            td.InnerText = opentime.ToString("0.00") + "小时";
            tr.Cells.Add(td);
            roomInfoTable.Rows.Add(tr);
            //阅览室开座位信息
            tr = new HtmlTableRow();
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            td.InnerText = "座位总数：";
            tr.Cells.Add(td);
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            td.InnerText = room.SeatList.Seats.Count.ToString() + "个";
            tr.Cells.Add(td);
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            td.InnerText = "进出人次：";
            tr.Cells.Add(td);
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            td.InnerText = staList.Count.ToString() + "人次";
            tr.Cells.Add(td);
            roomInfoTable.Rows.Add(tr);

            //阅览室座位使用信息
            tr = new HtmlTableRow();
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            td.InnerText = "上座率：";
            tr.Cells.Add(td);
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            td.InnerText = (double.Parse(staList.Count.ToString()) / double.Parse(daycount.Count.ToString()) / double.Parse(room.SeatList.Seats.Count.ToString()) * 100).ToString("0.00") + "%";
            tr.Cells.Add(td);
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            td.InnerText = "平均在座时长：";
            tr.Cells.Add(td);
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            if (!(seatCount > 0))
            {
                seatCount = 1;
            }
            td.InnerText = (seatTime / seatCount).ToString("0.00") + "小时";
            tr.Cells.Add(td);
            roomInfoTable.Rows.Add(tr);

            //阅览室功能信息
            tr = new HtmlTableRow();
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            td.InnerText = "启用功能：";
            tr.Cells.Add(td);
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            string funstr = "";
            if (room.Setting.NoManagement.Used)
            {
                funstr += "无人值守 ";
            }
            if (room.Setting.SeatBespeak.Used)
            {
                funstr += "座位预约 ";
            }
            if (room.Setting.SeatUsedTimeLimit.Used)
            {
                funstr += "计时模式 ";
            }
            td.InnerText = funstr;
            td.ColSpan = 3;
            tr.Cells.Add(td);
            roomInfoTable.Rows.Add(tr);

            //阅览室黑名单信息
            tr = new HtmlTableRow();
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            td.InnerText = "违规设置：";
            tr.Cells.Add(td);
            td = new HtmlTableCell();
            td.BgColor = "#E7E7E3";
            string vbstr = "";
            if (room.Setting.UsedBlacklistLimit)
            {
                vbstr += "黑名单禁入 ";
            }
            if (room.Setting.IsRecordViolate)
            {
                vbstr += "记录违规 ";
            }
            else
            {
                vbstr += "不记录违规 ";
            }
            if (room.Setting.BlackListSetting.Used)
            {
                vbstr += "独立黑名单 ";
            }
            else
            {
                vbstr += "全局黑名单 ";
            }
            td.InnerText = vbstr;
            td.ColSpan = 3;
            tr.Cells.Add(td);
            roomInfoTable.Rows.Add(tr);

            return roomInfoTable;
        }
        /// <summary>
        /// 记录排行榜统计
        /// </summary>
        /// <returns></returns>
        public DataTable LogTop(TopLogType logtype, TopReaderType type, string startDate, string endDate)
        {
            DateTime starttime = DateTime.Parse(startDate + " 0:00:00");
            DateTime endtime = DateTime.Parse(endDate + " 23:59:59");
            DataTable dt = new DataTable();
            dt.Columns.Add("TopNum", typeof(int));
            dt.Columns.Add("CardNo", typeof(string));
            dt.Columns.Add("ReaderName", typeof(string));
            dt.Columns.Add("TypeName", typeof(string));
            dt.Columns.Add("DeptName", typeof(string));
            dt.Columns.Add("LogCount", typeof(string));
            dt.Columns.Add("Remark", typeof(string));
            if (Progress != null)
            {
                Progress("(1/4)正在获取数据请耐心等待……");
            }
            try
            {
                switch (logtype)
                {
                    case TopLogType.EnterOutLog:
                        {
                            List<SeatManage.ClassModel.EnterOutLogStatistics> staList = new List<SeatManage.ClassModel.EnterOutLogStatistics>();
                            DateTime sTime = starttime;
                            DateTime eTime = endtime;
                            float days = 0;
                            float allday = (endtime - starttime).Days;
                            while (true)
                            {
                                if (sTime > endtime)
                                {
                                    break;
                                }
                                eTime = sTime.AddDays(10);
                                if (eTime > endtime)
                                {
                                    eTime = endtime;
                                }
                                List<SeatManage.ClassModel.EnterOutLogStatistics> List = SeatManage.Bll.T_SM_EnterOutLogStatistics.GetEnterOutLogStatisticsByDate(null, sTime.ToString(), eTime.ToString());
                                if (List.Count > 0)
                                {
                                    staList.AddRange(List);
                                }
                                sTime = sTime.AddDays(10);
                                days = (sTime - starttime).Days;
                                if (Progress != null)
                                {
                                    Progress("(1/4)正在获取数据请耐心等待……" + (days / allday * 100).ToString("0.00") + "%");
                                }
                            }
                            Dictionary<string, TopDataInfo> topDataList = new Dictionary<string, TopDataInfo>();
                            //统计数据
                            switch (type)
                            {
                                case TopReaderType.OnlyReader:
                                    foreach (SeatManage.ClassModel.EnterOutLogStatistics sta in staList)
                                    {
                                        if (Progress != null)
                                        {
                                            Progress("(2/4)正在处理数据请耐心等待……" + (float.Parse(staList.IndexOf(sta).ToString()) / float.Parse(staList.Count.ToString()) * 100).ToString("0.00") + "%");
                                        }
                                        if (!topDataList.ContainsKey(sta.CardNo))
                                        {
                                            TopDataInfo info = new TopDataInfo();
                                            info.CardNo = sta.CardNo;
                                            info.Name = sta.ReaderName;
                                            info.Type = sta.TypeName;
                                            info.Dept = sta.DeptName;
                                            topDataList.Add(sta.CardNo, info);
                                        }
                                        topDataList[sta.CardNo].SelectSeatConnt[sta.SelectSeat]++;
                                    };
                                    break;
                                case TopReaderType.ReaderDept:
                                    foreach (SeatManage.ClassModel.EnterOutLogStatistics sta in staList)
                                    {
                                        if (Progress != null)
                                        {
                                            Progress("(2/4)正在处理数据请耐心等待……" + (float.Parse(staList.IndexOf(sta).ToString()) / float.Parse(staList.Count.ToString()) * 100).ToString("0.00") + "%");
                                        }
                                        if (string.IsNullOrEmpty(sta.DeptName))
                                        {
                                            sta.DeptName = "未指定";
                                        }
                                        if (!topDataList.ContainsKey(sta.DeptName))
                                        {
                                            TopDataInfo info = new TopDataInfo();
                                            info.Dept = sta.DeptName;
                                            topDataList.Add(sta.DeptName, info);
                                        }
                                        topDataList[sta.DeptName].SelectSeatConnt[sta.SelectSeat]++;
                                    }
                                    break;
                                case TopReaderType.ReaderType:
                                    foreach (SeatManage.ClassModel.EnterOutLogStatistics sta in staList)
                                    {
                                        if (Progress != null)
                                        {
                                            Progress("(2/4)正在处理数据请耐心等待……" + (float.Parse(staList.IndexOf(sta).ToString()) / float.Parse(staList.Count.ToString()) * 100).ToString("0.00") + "%");
                                        }
                                        if (string.IsNullOrEmpty(sta.TypeName))
                                        {
                                            sta.TypeName = "未指定";
                                        }
                                        if (!topDataList.ContainsKey(sta.TypeName))
                                        {
                                            TopDataInfo info = new TopDataInfo();
                                            info.Type = sta.TypeName;
                                            topDataList.Add(sta.TypeName, info);
                                        }
                                        topDataList[sta.TypeName].SelectSeatConnt[sta.SelectSeat]++;
                                    }
                                    break;
                            }


                            List<TopDataInfo> topDataInfoList = new List<TopDataInfo>();
                            //转换为List进行排序
                            foreach (KeyValuePair<string, TopDataInfo> item in topDataList)
                            {
                                topDataInfoList.Add(item.Value);
                            }
                            //冒泡排序
                            for (int i = 0; i < topDataInfoList.Count; i++)
                            {
                                if (Progress != null)
                                {
                                    Progress("(3/4)正在对数据进行排序请耐心等待……" + (float.Parse(i.ToString()) / float.Parse(topDataInfoList.Count.ToString()) * 100).ToString("0.00") + "%");
                                }
                                for (int j = topDataInfoList.Count - 1; j > 0; j--)
                                {
                                    if (topDataInfoList[j].AllSelectConnt > topDataInfoList[j - 1].AllSelectConnt)
                                    {
                                        topDataInfoList.Insert(j - 1, topDataInfoList[j]);
                                        topDataInfoList.RemoveAt(j + 1);
                                    }
                                }
                            }
                            //转换成DataTable
                            for (int i = 0; i < topDataInfoList.Count; i++)
                            {

                                DataRow dr = dt.NewRow();
                                dr["TopNum"] = i + 1;
                                dr["CardNo"] = topDataInfoList[i].CardNo;
                                dr["ReaderName"] = topDataInfoList[i].Name;
                                dr["TypeName"] = topDataInfoList[i].Type;
                                dr["DeptName"] = topDataInfoList[i].Dept;
                                dr["LogCount"] = topDataInfoList[i].AllSelectConnt + "次";
                                dr["Remark"] = "刷卡选座" + topDataInfoList[i].SelectSeatConnt[SeatManage.ClassModel.EnterOutLogSelectSeatMode.ReadCardSelect]
                                    + "次，重新选座" + topDataInfoList[i].SelectSeatConnt[SeatManage.ClassModel.EnterOutLogSelectSeatMode.ReSelect]
                                    + "次，预约选座" + topDataInfoList[i].SelectSeatConnt[SeatManage.ClassModel.EnterOutLogSelectSeatMode.BookAdmission]
                                    + "次，等待入座" + topDataInfoList[i].SelectSeatConnt[SeatManage.ClassModel.EnterOutLogSelectSeatMode.WaitAdmission]
                                    + "次，管理员分配" + topDataInfoList[i].SelectSeatConnt[SeatManage.ClassModel.EnterOutLogSelectSeatMode.AdminAllocation]
                                    + "次";
                                dt.Rows.Add(dr);
                            }
                        }
                        break;
                    case TopLogType.SeatTime:
                        {
                            List<SeatManage.ClassModel.EnterOutLogStatistics> staListst = new List<SeatManage.ClassModel.EnterOutLogStatistics>();
                            DateTime sTime = starttime;
                            DateTime eTime = endtime;
                            float days = 0;
                            float allday = (endtime - starttime).Days;
                            while (true)
                            {
                                if (sTime > endtime)
                                {
                                    break;
                                }
                                eTime = sTime.AddDays(10);
                                if (eTime > endtime)
                                {
                                    eTime = endtime;
                                }
                                List<SeatManage.ClassModel.EnterOutLogStatistics> List = SeatManage.Bll.T_SM_EnterOutLogStatistics.GetEnterOutLogStatisticsByDate(null, sTime.ToString(), eTime.ToString());
                                if (List.Count > 0)
                                {
                                    staListst.AddRange(List);
                                }
                                sTime = sTime.AddDays(10);
                                days = (sTime - starttime).Days;
                                if (Progress != null)
                                {
                                    Progress("(1/4)正在获取数据请耐心等待……" + (days / allday * 100).ToString("0.00") + "%");
                                }
                            }
                            Dictionary<string, TopDataInfo> topDataListst = new Dictionary<string, TopDataInfo>();
                            //统计数据
                            switch (type)
                            {
                                case TopReaderType.OnlyReader:
                                    foreach (SeatManage.ClassModel.EnterOutLogStatistics sta in staListst)
                                    {
                                        if (Progress != null)
                                        {
                                            Progress("(2/4)正在处理数据请耐心等待……" + (float.Parse(staListst.IndexOf(sta).ToString()) / float.Parse(staListst.Count.ToString()) * 100).ToString("0.00") + "%");
                                        }
                                        if (!topDataListst.ContainsKey(sta.CardNo))
                                        {
                                            TopDataInfo info = new TopDataInfo();
                                            info.CardNo = sta.CardNo;
                                            info.Name = sta.ReaderName;
                                            info.Type = sta.TypeName;
                                            info.Dept = sta.DeptName;
                                            topDataListst.Add(sta.CardNo, info);
                                        }
                                        topDataListst[sta.CardNo].SelectSeatConnt[sta.SelectSeat]++;
                                        topDataListst[sta.CardNo].SeatTime += sta.SeatTime;
                                    };
                                    break;
                                case TopReaderType.ReaderDept:
                                    foreach (SeatManage.ClassModel.EnterOutLogStatistics sta in staListst)
                                    {
                                        if (Progress != null)
                                        {
                                            Progress("(2/4)正在处理数据请耐心等待……" + (float.Parse(staListst.IndexOf(sta).ToString()) / float.Parse(staListst.Count.ToString()) * 100).ToString("0.00") + "%");
                                        }
                                        if (string.IsNullOrEmpty(sta.DeptName))
                                        {
                                            sta.DeptName = "未指定";
                                        }
                                        if (!topDataListst.ContainsKey(sta.DeptName))
                                        {
                                            TopDataInfo info = new TopDataInfo();
                                            info.Dept = sta.DeptName;
                                            topDataListst.Add(sta.DeptName, info);
                                        }
                                        topDataListst[sta.DeptName].SelectSeatConnt[sta.SelectSeat]++;
                                        topDataListst[sta.DeptName].SeatTime += sta.SeatTime;
                                    }
                                    break;
                                case TopReaderType.ReaderType:
                                    foreach (SeatManage.ClassModel.EnterOutLogStatistics sta in staListst)
                                    {
                                        if (Progress != null)
                                        {
                                            Progress("(2/4)正在处理数据请耐心等待……" + (float.Parse(staListst.IndexOf(sta).ToString()) / float.Parse(staListst.Count.ToString()) * 100).ToString("0.00") + "%");
                                        }
                                        if (string.IsNullOrEmpty(sta.TypeName))
                                        {
                                            sta.TypeName = "未指定";
                                        }
                                        if (!topDataListst.ContainsKey(sta.TypeName))
                                        {
                                            TopDataInfo info = new TopDataInfo();
                                            info.Type = sta.TypeName;
                                            topDataListst.Add(sta.TypeName, info);
                                        }
                                        topDataListst[sta.TypeName].SelectSeatConnt[sta.SelectSeat]++;
                                        topDataListst[sta.TypeName].SeatTime += sta.SeatTime;
                                    }
                                    break;
                            }


                            List<TopDataInfo> topDataInfoListst = new List<TopDataInfo>();
                            //转换为List进行排序
                            foreach (KeyValuePair<string, TopDataInfo> item in topDataListst)
                            {
                                topDataInfoListst.Add(item.Value);
                            }
                            //冒泡排序
                            for (int i = 0; i < topDataInfoListst.Count; i++)
                            {
                                if (Progress != null)
                                {
                                    Progress("(3/4)正在对数据进行排序请耐心等待……" + (float.Parse(i.ToString()) / float.Parse(topDataInfoListst.Count.ToString()) * 100).ToString("0.00") + "%");
                                }
                                for (int j = topDataInfoListst.Count - 1; j > 0; j--)
                                {
                                    if (topDataInfoListst[j].SeatTime > topDataInfoListst[j - 1].SeatTime)
                                    {
                                        topDataInfoListst.Insert(j - 1, topDataInfoListst[j]);
                                        topDataInfoListst.RemoveAt(j + 1);
                                    }
                                }
                            }
                            //转换成DataTable
                            for (int i = 0; i < topDataInfoListst.Count; i++)
                            {

                                DataRow dr = dt.NewRow();
                                dr["TopNum"] = i + 1;
                                dr["CardNo"] = topDataInfoListst[i].CardNo;
                                dr["ReaderName"] = topDataInfoListst[i].Name;
                                dr["TypeName"] = topDataInfoListst[i].Type;
                                dr["DeptName"] = topDataInfoListst[i].Dept;
                                dr["LogCount"] = (topDataInfoListst[i].SeatTime / 60).ToString("0.0") + "小时";
                                dr["Remark"] = "平均在座时长为" + (topDataInfoListst[i].SeatTime / 60 / topDataInfoListst[i].AllSelectConnt).ToString("0.0") + "小时";
                                dt.Rows.Add(dr);
                            }
                        }
                        break;
                    case TopLogType.ViolateDiscipline:
                        {
                            List<SeatManage.ClassModel.ViolationRecordsLogInfo> vrloglist = new List<SeatManage.ClassModel.ViolationRecordsLogInfo>();
                            DateTime sTime = starttime;
                            DateTime eTime = endtime;
                            float days = 0;
                            float allday = (endtime - starttime).Days;
                            while (true)
                            {
                                if (sTime > endtime)
                                {
                                    break;
                                }
                                eTime = sTime.AddDays(10);
                                if (eTime > endtime)
                                {
                                    eTime = endtime;
                                }
                                List<SeatManage.ClassModel.ViolationRecordsLogInfo> List = SeatManage.Bll.T_SM_ViolateDiscipline.GetViolationRecords(null, null, sTime.ToString(), eTime.ToString(), SeatManage.EnumType.LogStatus.None, SeatManage.EnumType.LogStatus.None);
                                if (List.Count > 0)
                                {
                                    vrloglist.AddRange(List);
                                }
                                sTime = sTime.AddDays(10);
                                days = (sTime - starttime).Days;
                                if (Progress != null)
                                {
                                    Progress("(1/4)正在获取数据请耐心等待……" + (days / allday * 100).ToString("0.00") + "%");
                                }
                            }
                            Dictionary<string, TopDataInfo> topDataListvr = new Dictionary<string, TopDataInfo>();
                            //统计数据
                            switch (type)
                            {
                                case TopReaderType.OnlyReader:
                                    foreach (SeatManage.ClassModel.ViolationRecordsLogInfo sta in vrloglist)
                                    {
                                        if (Progress != null)
                                        {
                                            Progress("(2/4)正在处理数据请耐心等待……" + (float.Parse(vrloglist.IndexOf(sta).ToString()) / float.Parse(vrloglist.Count.ToString()) * 100).ToString("0.00") + "%");
                                        }
                                        if (string.IsNullOrEmpty(sta.CardNo))
                                        {
                                            continue;
                                        }
                                        if (!topDataListvr.ContainsKey(sta.CardNo))
                                        {
                                            TopDataInfo info = new TopDataInfo();
                                            info.CardNo = sta.CardNo;
                                            info.Name = sta.ReaderName;
                                            info.Type = sta.TypeName;
                                            info.Dept = sta.DeptName;
                                            topDataListvr.Add(sta.CardNo, info);
                                        }
                                        topDataListvr[sta.CardNo].ViolateDisciplineConnt[sta.EnterFlag.ToString()]++;
                                    };
                                    break;
                                case TopReaderType.ReaderDept:
                                    foreach (SeatManage.ClassModel.ViolationRecordsLogInfo sta in vrloglist)
                                    {
                                        if (Progress != null)
                                        {
                                            Progress("(2/4)正在处理数据请耐心等待……" + (float.Parse(vrloglist.IndexOf(sta).ToString()) / float.Parse(vrloglist.Count.ToString()) * 100).ToString("0.00") + "%");
                                        }
                                        if (string.IsNullOrEmpty(sta.DeptName))
                                        {
                                            sta.DeptName = "未指定";
                                        }
                                        if (!topDataListvr.ContainsKey(sta.DeptName))
                                        {
                                            TopDataInfo info = new TopDataInfo();
                                            info.Dept = sta.DeptName;
                                            topDataListvr.Add(sta.DeptName, info);
                                        }
                                        topDataListvr[sta.DeptName].ViolateDisciplineConnt[sta.EnterFlag.ToString()]++;
                                    }
                                    break;
                                case TopReaderType.ReaderType:
                                    foreach (SeatManage.ClassModel.ViolationRecordsLogInfo sta in vrloglist)
                                    {
                                        if (Progress != null)
                                        {
                                            Progress("(2/4)正在处理数据请耐心等待……" + (float.Parse(vrloglist.IndexOf(sta).ToString()) / float.Parse(vrloglist.Count.ToString()) * 100).ToString("0.00") + "%");
                                        }
                                        if (string.IsNullOrEmpty(sta.TypeName))
                                        {
                                            sta.TypeName = "未指定";
                                        }
                                        if (!topDataListvr.ContainsKey(sta.TypeName))
                                        {
                                            TopDataInfo info = new TopDataInfo();
                                            info.Type = sta.TypeName;
                                            topDataListvr.Add(sta.TypeName, info);
                                        }
                                        topDataListvr[sta.TypeName].ViolateDisciplineConnt[sta.EnterFlag.ToString()]++;
                                    }
                                    break;
                            }


                            List<TopDataInfo> topDataInfoListvr = new List<TopDataInfo>();
                            //转换为List进行排序
                            foreach (KeyValuePair<string, TopDataInfo> item in topDataListvr)
                            {
                                topDataInfoListvr.Add(item.Value);
                            }
                            //冒泡排序
                            for (int i = 0; i < topDataInfoListvr.Count; i++)
                            {
                                if (Progress != null)
                                {
                                    Progress("(3/4)正在对数据进行排序请耐心等待……" + (float.Parse(i.ToString()) / float.Parse(topDataInfoListvr.Count.ToString()) * 100).ToString("0.00") + "%");
                                }
                                for (int j = topDataInfoListvr.Count - 1; j > 0; j--)
                                {
                                    if (topDataInfoListvr[j].AllViolateDisciplineCount > topDataInfoListvr[j - 1].AllViolateDisciplineCount)
                                    {
                                        topDataInfoListvr.Insert(j - 1, topDataInfoListvr[j]);
                                        topDataInfoListvr.RemoveAt(j + 1);
                                    }
                                }
                            }
                            //转换成DataTable
                            for (int i = 0; i < topDataInfoListvr.Count; i++)
                            {

                                DataRow dr = dt.NewRow();
                                dr["TopNum"] = i + 1;
                                dr["CardNo"] = topDataInfoListvr[i].CardNo;
                                dr["ReaderName"] = topDataInfoListvr[i].Name;
                                dr["TypeName"] = topDataInfoListvr[i].Type;
                                dr["DeptName"] = topDataInfoListvr[i].Dept;
                                dr["LogCount"] = topDataInfoListvr[i].AllViolateDisciplineCount + "次";
                                dr["Remark"] = "预约超时" + topDataInfoListvr[i].ViolateDisciplineConnt[SeatManage.EnumType.ViolationRecordsType.BookingTimeOut.ToString()]
                                    + "次，在座超时" + topDataInfoListvr[i].ViolateDisciplineConnt[SeatManage.EnumType.ViolationRecordsType.SeatOutTime.ToString()]
                                    + "次，暂离超时" + (topDataInfoListvr[i].ViolateDisciplineConnt[SeatManage.EnumType.ViolationRecordsType.ShortLeaveOutTime.ToString()] + topDataInfoListvr[i].ViolateDisciplineConnt[SeatManage.EnumType.ViolationRecordsType.ShortLeaveByAdminOutTime.ToString()] + topDataInfoListvr[i].ViolateDisciplineConnt[SeatManage.EnumType.ViolationRecordsType.ShortLeaveByReaderOutTime.ToString()])
                                    + "次,被管理员释放座位" + topDataInfoListvr[i].ViolateDisciplineConnt[SeatManage.EnumType.ViolationRecordsType.LeaveByAdmin.ToString()]
                                    + "次，离开未刷卡" + topDataInfoListvr[i].ViolateDisciplineConnt[SeatManage.EnumType.ViolationRecordsType.LeaveNotReadCard.ToString()]
                                    + "次";
                                dt.Rows.Add(dr);
                            }
                        }
                        break;
                    case TopLogType.Blastlist:
                        {
                            List<SeatManage.ClassModel.BlackListInfo> blacklistlist = new List<SeatManage.ClassModel.BlackListInfo>();
                            DateTime sTime = starttime;
                            DateTime eTime = endtime;
                            float days = 0;
                            float allday = (endtime - starttime).Days;
                            while (true)
                            {
                                if (sTime > endtime)
                                {
                                    break;
                                }
                                eTime = sTime.AddDays(10);
                                if (eTime > endtime)
                                {
                                    eTime = endtime;
                                }
                                List<SeatManage.ClassModel.BlackListInfo> List = SeatManage.Bll.T_SM_Blacklist.GetAllBlackListInfo(null, SeatManage.EnumType.LogStatus.None, sTime.ToString(), eTime.ToString());
                                if (List.Count > 0)
                                {
                                    blacklistlist.AddRange(List);
                                }
                                sTime = sTime.AddDays(10);
                                days = (sTime - starttime).Days;
                                if (Progress != null)
                                {
                                    Progress("(1/4)正在获取数据请耐心等待……" + (days / allday * 100).ToString("0.00") + "%");
                                }
                            }
                            Dictionary<string, TopDataInfo> topDataListstbl = new Dictionary<string, TopDataInfo>();
                            //统计数据
                            switch (type)
                            {
                                case TopReaderType.OnlyReader:
                                    foreach (SeatManage.ClassModel.BlackListInfo sta in blacklistlist)
                                    {
                                        if (Progress != null)
                                        {
                                            Progress("(2/4)正在处理数据请耐心等待……" + (float.Parse(blacklistlist.IndexOf(sta).ToString()) / float.Parse(blacklistlist.Count.ToString()) * 100).ToString("0.00") + "%");
                                        }
                                        if (!topDataListstbl.ContainsKey(sta.CardNo))
                                        {
                                            TopDataInfo info = new TopDataInfo();
                                            info.CardNo = sta.CardNo;
                                            info.Name = sta.ReaderName;
                                            info.Type = sta.TypeName;
                                            info.Dept = sta.DeptName;
                                            topDataListstbl.Add(sta.CardNo, info);
                                        }
                                        topDataListstbl[sta.CardNo].BlackListCount++;
                                        topDataListstbl[sta.CardNo].InBlacklistDays += (sta.OutTime - sta.AddTime).Days;
                                    };
                                    break;
                                case TopReaderType.ReaderDept:
                                    foreach (SeatManage.ClassModel.BlackListInfo sta in blacklistlist)
                                    {
                                        if (Progress != null)
                                        {
                                            Progress("(2/4)正在处理数据请耐心等待……" + (float.Parse(blacklistlist.IndexOf(sta).ToString()) / float.Parse(blacklistlist.Count.ToString()) * 100).ToString("0.00") + "%");
                                        }
                                        if (string.IsNullOrEmpty(sta.DeptName))
                                        {
                                            sta.DeptName = "未指定";
                                        }
                                        if (!topDataListstbl.ContainsKey(sta.DeptName))
                                        {
                                            TopDataInfo info = new TopDataInfo();
                                            info.Dept = sta.DeptName;
                                            topDataListstbl.Add(sta.DeptName, info);
                                        }
                                        topDataListstbl[sta.DeptName].BlackListCount++;
                                        topDataListstbl[sta.DeptName].InBlacklistDays += (sta.OutTime - sta.AddTime).Days;
                                    }
                                    break;
                                case TopReaderType.ReaderType:
                                    foreach (SeatManage.ClassModel.BlackListInfo sta in blacklistlist)
                                    {
                                        if (Progress != null)
                                        {
                                            Progress("(2/4)正在处理数据请耐心等待……" + (float.Parse(blacklistlist.IndexOf(sta).ToString()) / float.Parse(blacklistlist.Count.ToString()) * 100).ToString("0.00") + "%");
                                        }

                                        if (string.IsNullOrEmpty(sta.TypeName))
                                        {
                                            sta.TypeName = "未指定";
                                        }
                                        if (!topDataListstbl.ContainsKey(sta.TypeName))
                                        {
                                            TopDataInfo info = new TopDataInfo();
                                            info.Type = sta.TypeName;
                                            topDataListstbl.Add(sta.TypeName, info);
                                        }
                                        topDataListstbl[sta.TypeName].BlackListCount++;
                                        topDataListstbl[sta.TypeName].InBlacklistDays += (sta.OutTime - sta.AddTime).Days;
                                    }
                                    break;
                            }


                            List<TopDataInfo> topDataInfoListbl = new List<TopDataInfo>();
                            //转换为List进行排序
                            foreach (KeyValuePair<string, TopDataInfo> item in topDataListstbl)
                            {
                                topDataInfoListbl.Add(item.Value);
                            }
                            //冒泡排序
                            for (int i = 0; i < topDataInfoListbl.Count; i++)
                            {
                                if (Progress != null)
                                {
                                    Progress("(3/4)正在对数据进行排序请耐心等待……" + (float.Parse(i.ToString()) / float.Parse(topDataInfoListbl.Count.ToString()) * 100).ToString("0.00") + "%");
                                }
                                for (int j = topDataInfoListbl.Count - 1; j > 0; j--)
                                {
                                    if (topDataInfoListbl[j].BlackListCount > topDataInfoListbl[j - 1].BlackListCount)
                                    {
                                        topDataInfoListbl.Insert(j - 1, topDataInfoListbl[j]);
                                        topDataInfoListbl.RemoveAt(j + 1);
                                    }
                                }
                            }
                            //转换成DataTable
                            for (int i = 0; i < topDataInfoListbl.Count; i++)
                            {

                                DataRow dr = dt.NewRow();
                                dr["TopNum"] = i + 1;
                                dr["CardNo"] = topDataInfoListbl[i].CardNo;
                                dr["ReaderName"] = topDataInfoListbl[i].Name;
                                dr["TypeName"] = topDataInfoListbl[i].Type;
                                dr["DeptName"] = topDataInfoListbl[i].Dept;
                                dr["LogCount"] = topDataInfoListbl[i].BlackListCount;
                                dr["Remark"] = "累计惩罚天数" + topDataInfoListbl[i].InBlacklistDays + "天";
                                dt.Rows.Add(dr);
                            }
                        }
                        break;
                }
                if (Progress != null)
                {
                    Progress("(3/4)正在对数据进行绑定请耐心等待……");
                }
                return dt;
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
                throw ex;
            }
        }
        public class TopDataInfo
        {
            public TopDataInfo()
            {
                _SelectSeatConnt.Add(SeatManage.ClassModel.EnterOutLogSelectSeatMode.AdminAllocation, 0);
                _SelectSeatConnt.Add(SeatManage.ClassModel.EnterOutLogSelectSeatMode.BookAdmission, 0);
                _SelectSeatConnt.Add(SeatManage.ClassModel.EnterOutLogSelectSeatMode.ReadCardSelect, 0);
                _SelectSeatConnt.Add(SeatManage.ClassModel.EnterOutLogSelectSeatMode.ReSelect, 0);
                _SelectSeatConnt.Add(SeatManage.ClassModel.EnterOutLogSelectSeatMode.WaitAdmission, 0);

                _ViolateDisciplineConnt.Add(SeatManage.EnumType.ViolationRecordsType.BookingTimeOut.ToString(), 0);
                _ViolateDisciplineConnt.Add(SeatManage.EnumType.ViolationRecordsType.LeaveByAdmin.ToString(), 0);
                _ViolateDisciplineConnt.Add(SeatManage.EnumType.ViolationRecordsType.LeaveNotReadCard.ToString(), 0);
                _ViolateDisciplineConnt.Add(SeatManage.EnumType.ViolationRecordsType.SeatOutTime.ToString(), 0);
                _ViolateDisciplineConnt.Add(SeatManage.EnumType.ViolationRecordsType.ShortLeaveByAdminOutTime.ToString(), 0);
                _ViolateDisciplineConnt.Add(SeatManage.EnumType.ViolationRecordsType.ShortLeaveByReaderOutTime.ToString(), 0);
                _ViolateDisciplineConnt.Add(SeatManage.EnumType.ViolationRecordsType.ShortLeaveOutTime.ToString(), 0);
            }
            private string _CardNo = "";
            /// <summary>
            /// 学号
            /// </summary>
            public string CardNo
            {
                get { return _CardNo; }
                set { _CardNo = value; }
            }
            private string _Name = "";
            /// <summary>
            /// 姓名
            /// </summary>
            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }
            private string _Dept = "";
            /// <summary>
            /// 院系
            /// </summary>
            public string Dept
            {
                get { return _Dept; }
                set { _Dept = value; }
            }
            private string _Type = "";
            /// <summary>
            /// 类型
            /// </summary>
            public string Type
            {
                get { return _Type; }
                set { _Type = value; }
            }
            private int _AllSelectConnt = 0;
            /// <summary>
            /// 总选座次数
            /// </summary>
            public int AllSelectConnt
            {
                get
                {
                    int count = 0;
                    foreach (KeyValuePair<SeatManage.ClassModel.EnterOutLogSelectSeatMode, int> item in _SelectSeatConnt)
                    {
                        count += item.Value;
                    }
                    return count;
                }
            }

            private Dictionary<SeatManage.ClassModel.EnterOutLogSelectSeatMode, int> _SelectSeatConnt = new Dictionary<SeatManage.ClassModel.EnterOutLogSelectSeatMode, int>();
            /// <summary>
            /// 选座类型次数
            /// </summary>
            public Dictionary<SeatManage.ClassModel.EnterOutLogSelectSeatMode, int> SelectSeatConnt
            {
                get { return _SelectSeatConnt; }
                set { _SelectSeatConnt = value; }
            }


            private int _SeatTime = 0;
            /// <summary>
            /// 在座时长
            /// </summary>
            public int SeatTime
            {
                get { return _SeatTime; }
                set { _SeatTime = value; }
            }

            private int _BlackListCount = 0;
            /// <summary>
            /// 黑名单次数
            /// </summary>
            public int BlackListCount
            {
                get { return _BlackListCount; }
                set { _BlackListCount = value; }
            }
            private int _InBlacklistDays = 0;
            /// <summary>
            /// 黑名单天数
            /// </summary>
            public int InBlacklistDays
            {
                get { return _InBlacklistDays; }
                set { _InBlacklistDays = value; }
            }
            private int _AllViolateDisciplineCount = 0;
            /// <summary>
            /// 全部违规次数
            /// </summary>
            public int AllViolateDisciplineCount
            {
                get
                {
                    int count = 0;
                    foreach (KeyValuePair<string, int> item in _ViolateDisciplineConnt)
                    {
                        count += item.Value;
                    }
                    return count;
                }
            }
            private Dictionary<string, int> _ViolateDisciplineConnt = new Dictionary<string, int>();
            /// <summary>
            /// 违规类型次数
            /// </summary>
            public Dictionary<string, int> ViolateDisciplineConnt
            {
                get { return _ViolateDisciplineConnt; }
                set { _ViolateDisciplineConnt = value; }
            }

        }
        public enum TopLogType
        {
            None = -1,
            /// <summary>
            /// 进出人次
            /// </summary>
            EnterOutLog = 0,
            /// <summary>
            /// 在座时长
            /// </summary>
            SeatTime = 1,
            /// <summary>
            /// 违规记录
            /// </summary>
            ViolateDiscipline = 2,
            /// <summary>
            /// 黑名单记录
            /// </summary>
            Blastlist = 3,

        }
        public enum TopReaderType
        {
            None = -1,
            /// <summary>
            /// 按读者统计
            /// </summary>
            OnlyReader = 0,
            /// <summary>
            /// 按类型统计
            /// </summary>
            ReaderType = 1,
            /// <summary>
            /// 按院系统计
            /// </summary>
            ReaderDept = 2,
        }
    }
}