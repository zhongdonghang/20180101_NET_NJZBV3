using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.ClassModel;
using System.Data;
using SeatManage.DAL;
using System.Data.SqlClient;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        private T_SM_ReadingRoom t_sm_readingRoom_DAL = new T_SM_ReadingRoom();
        private ViewReadingRoomState viewReadingRoomState_DAL = new ViewReadingRoomState();
        #region 阅览室相关操作
        public List<ReadingRoomInfo> GetReadingRoomInfo(List<string> roomNum)
        {
            List<ReadingRoomInfo> readingRooms = new List<ReadingRoomInfo>();
            try
            {
                if (roomNum != null && roomNum.Count > 0)
                {
                    for (int i = 0; i < roomNum.Count; i++)
                    {
                        StringBuilder strWhere = new StringBuilder();
                        strWhere.Append(string.Format(" ReadingRoomNo = '{0}'", roomNum[i]));
                        DataSet ds = t_sm_readingRoom_DAL.GetList(strWhere.ToString(), null);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ReadingRoomInfo room = DataRowToReadingRoomInfo(ds.Tables[0].Rows[0]);
                            readingRooms.Add(room);
                        }
                    }
                }
                else
                {
                    DataSet ds = t_sm_readingRoom_DAL.GetList("", null);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ReadingRoomInfo room = DataRowToReadingRoomInfo(ds.Tables[0].Rows[i]);
                        readingRooms.Add(room);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("获取阅览室信息失败：{0}", ex.Message));
            }
            return readingRooms;
        }

        public List<ReadingRoomInfo> GetReadingRooms(List<string> roomNum, List<string> libraryNum, List<string> schoolNum)
        {
            List<ReadingRoomInfo> readingRooms = new List<ReadingRoomInfo>();
            StringBuilder strWhere = new StringBuilder();
            if (roomNum != null)
            {
                for (int i = 0; i < roomNum.Count; i++)
                {
                    if (i == 0)
                    {
                        strWhere.Append(string.Format(" ReadingRoomNo in ('{0}'", roomNum[i]));
                    }
                    else if (i != roomNum.Count - 1)
                    {
                        strWhere.Append(string.Format(",'{0}'  ", roomNum[i]));
                    }
                    if (i == roomNum.Count - 1)
                    {
                        strWhere.Append(string.Format(" ,'{0}')", roomNum[i]));
                    }
                }
            }
            if (libraryNum != null)
            {
                for (int i = 0; i < libraryNum.Count; i++)
                {
                    if (i == 0)
                    {
                        if (String.IsNullOrEmpty(strWhere.ToString()))
                        {
                            strWhere.Append(string.Format(" LibraryNo in ('{0}'", libraryNum[i]));
                        }
                        else
                        {
                            strWhere.Append(string.Format(" and LibraryNo in ('{0}'", libraryNum[i]));
                        }
                    }
                    else if (i != libraryNum.Count - 1)
                    {
                        strWhere.Append(string.Format(",'{0}'", libraryNum[i]));
                    }
                    if (i == libraryNum.Count - 1)
                    {
                        strWhere.Append(string.Format(",'{0}')", libraryNum[i]));
                    }
                }
            }
            if (schoolNum != null)
            {
                for (int i = 0; i < schoolNum.Count; i++)
                {
                    if (i == 0)
                    {
                        if (String.IsNullOrEmpty(strWhere.ToString()))
                        {
                            strWhere.Append(string.Format(" SchoolNo in ('{0}'", schoolNum[i]));
                        }
                        else
                        {
                            strWhere.Append(string.Format(" and SchoolNo in ('{0}'", schoolNum[i]));
                        }
                    }
                    else if (i != schoolNum.Count - 1)
                    {
                        strWhere.Append(string.Format(",'{0}'  ", schoolNum[i]));
                    }
                    if (i == schoolNum.Count - 1)
                    {
                        strWhere.Append(string.Format(",'{0}')", schoolNum[i]));
                    }
                }
            }
            try
            {
                DataSet ds = t_sm_readingRoom_DAL.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        ReadingRoomInfo room = DataRowToReadingRoomInfo(ds.Tables[0].Rows[i]);
                        readingRooms.Add(room);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("解析阅览室设置失败：{0}", ex.Message));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("获取阅览室信息失败：{0}", ex.Message));
            }
            return readingRooms;
        }
        /// <summary>
        /// 新增阅览室
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool AddReadingRoom(ReadingRoomInfo room)
        {
            try
            {
                return t_sm_readingRoom_DAL.Add(room);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 更新阅览室
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool UpdateReadingRoom(ReadingRoomInfo room)
        {
            try
            {
                return t_sm_readingRoom_DAL.Update(room);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 删除阅览室
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool deleteReadingRoom(ReadingRoomInfo room)
        {
            try
            {
                return t_sm_readingRoom_DAL.Delete(room);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 判断阅览室编号是否存在
        /// </summary>
        /// <param name="ReadingRoomNo"></param>
        /// <returns></returns>
        public bool ReadingRoomIsExists(string ReadingRoomNo)
        {
            try
            {
                return t_sm_readingRoom_DAL.Exists(ReadingRoomNo);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取多个阅览室座位使用状态
        /// </summary>
        /// <param name="roomNums">阅览室编号列表</param>
        /// <param name="date"></param>
        /// <returns></returns>
        public Dictionary<string, ReadingRoomSeatUsedState> GetRoomSeatUsedState(List<string> roomNums)
        {
            List<ReadingRoomInfo> rooms = GetReadingRoomInfo(roomNums);
            Dictionary<string, ReadingRoomSeatUsedState> list = new Dictionary<string, ReadingRoomSeatUsedState>();
            for (int i = 0; i < rooms.Count; i++)
            {
                ReadingRoomSeatUsedState usedState = new ReadingRoomSeatUsedState();
                usedState.LibraryNum = rooms[i].Libaray.No;
                usedState.LibraryName = string.Format("{0} {1}", rooms[i].Libaray.School.Name, rooms[i].Libaray.Name);
                usedState.RoomName = rooms[i].Name;
                usedState.RoomNum = rooms[i].No;
                usedState.PersonTimes = GetTodaySeatPerson(rooms[i].No);
                //usedState.SeatAmountAll = GetSeatAmountAll(rooms[i].No);
                usedState.SeatAmountAll = rooms[i].SeatList.Seats.Count(u => u.Value.IsSuspended != true);
                //foreach (KeyValuePair<string, Seat> seat in rooms[i].SeatList.Seats)
                //{
                //    if (seat.Value.IsSuspended)
                //    {
                //        usedState.SeatAmountAll--;
                //    }
                //}
                List<EnterOutLogInfo> enterOutLoglist = GetUsingSeatEnterOutLogInfo(rooms[i].No);
                List<BespeakLogInfo> bespeakLogs = GetBespeakLogInfoByRoomNum(rooms[i].No, GetServerDateTime());
                for (int j = 0; j < enterOutLoglist.Count; j++)
                {
                    if (enterOutLoglist[j].EnterOutState == SeatManage.EnumType.EnterOutLogType.ShortLeave)
                    {
                        usedState.SeatAmountShortLeave += 1;
                    }
                    for (int k = 0; k < bespeakLogs.Count; k++)
                    {
                        if (enterOutLoglist[j].SeatNo == bespeakLogs[k].SeatNo)
                        {
                            usedState.SeatTemUseCount += 1;
                            break;
                        }
                    }
                }
                //usedState.SeatTemUseCount = enterOutLoglist.FindAll(u => u.ReadingRoomNo == roomNums[i] && bespeakLogs.Exists(p => p.SeatNo == u.SeatNo)).Count;
                usedState.SeatBookingCount = bespeakLogs.Count;
                usedState.SeatAmountUsed = enterOutLoglist.Count;
                //usedState.SeatAmountFree = usedState.SeatAmountAll - usedState.SeatAmountUsed - usedState.SeatBookingCount + usedState.SeatTemUseCount;
                list.Add(rooms[i].No, usedState);

            }
            return list;

        }
        /// <summary>
        /// 获取多个阅览室座位使用状态V2
        /// </summary>
        /// <param name="roomNums">阅览室编号列表</param>
        /// <param name="date"></param>
        /// <returns></returns>
        public Dictionary<string, ReadingRoomSeatUsedState> GetRoomSeatUsedStateV2(List<string> roomNums)
        {
            List<ReadingRoomInfo> rooms = GetReadingRoomInfo(roomNums);
            List<EnterOutLogInfo> roomEnterOutLoglist = GetRoomUsingSeatEnterOutLogInfo(roomNums);
            List<BespeakLogInfo> roomBespeakLogs = GetBespeakLogInfoByRoomsNum(roomNums, GetServerDateTime());
            Dictionary<string, ReadingRoomSeatUsedState> list = new Dictionary<string, ReadingRoomSeatUsedState>();
            for (int i = 0; i < rooms.Count; i++)
            {
                ReadingRoomSeatUsedState usedState = new ReadingRoomSeatUsedState();
                usedState.PersonTimes = GetTodaySeatPerson(rooms[i].No);
                usedState.SeatAmountAll = rooms[i].SeatList.Seats.Count(u => u.Value.IsSuspended != true);
                List<EnterOutLogInfo> enterOutLoglist = roomEnterOutLoglist.FindAll(u => u.ReadingRoomNo == rooms[i].No);
                List<BespeakLogInfo> bespeakLogs = roomBespeakLogs.FindAll(u => u.ReadingRoomNo == rooms[i].No);
                usedState.SeatAmountShortLeave = enterOutLoglist.Count(u => u.EnterOutState == SeatManage.EnumType.EnterOutLogType.ShortLeave);
                for (int j = 0; j < enterOutLoglist.Count; j++)
                {
                    if (bespeakLogs.Find(u => u.SeatNo == enterOutLoglist[j].SeatNo) != null)
                    {
                        usedState.SeatTemUseCount += 1;
                        break;
                    }
                }
                usedState.SeatBookingCount = bespeakLogs.Count;
                usedState.SeatAmountUsed = enterOutLoglist.Count;
                list.Add(rooms[i].No, usedState);
            }
            return list;
        }
        /// <summary>
        /// 获取多个阅览室座位使用状态V3
        /// </summary>
        /// <param name="roomNums">阅览室编号列表</param>
        /// <param name="date"></param>
        /// <returns></returns>
        public Dictionary<string, ReadingRoomSeatUsedState> GetRoomSeatUsedStateV3(List<string> roomNums)
        {
            Dictionary<string, ReadingRoomSeatUsedState> stateList = new Dictionary<string, ReadingRoomSeatUsedState>();
            List<ReadingRoomInfo> readingRooms = new List<ReadingRoomInfo>();
            try
            {
                if (roomNums != null && roomNums.Count > 0)
                {
                    for (int i = 0; i < roomNums.Count; i++)
                    {
                        StringBuilder strWhere = new StringBuilder();
                        strWhere.Append(string.Format(" ReadingRoomNo = '{0}'", roomNums[i]));
                        DataSet ds = viewReadingRoomState_DAL.GetList(strWhere.ToString(), null);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ReadingRoomSeatUsedState_Ex roomState = DataRowToReadingRoomSeatUsedState(ds.Tables[0].Rows[0]);
                            ReadingRoomSeatUsedState state = new ReadingRoomSeatUsedState();
                            state.SeatAmountAll = roomState.ReadingRoom.SeatList.Seats.Count(u => u.Value.IsSuspended != true);
                            state.SeatAmountUsed = roomState.SeatAmountUsed;
                            state.SeatBookingCount = roomState.SeatBookingCount;
                            stateList.Add(roomState.ReadingRoom.No, state);
                        }
                    }
                }
                else
                {
                    DataSet ds = viewReadingRoomState_DAL.GetList("", null);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ReadingRoomSeatUsedState_Ex roomState = DataRowToReadingRoomSeatUsedState(ds.Tables[0].Rows[i]);
                        ReadingRoomSeatUsedState state = new ReadingRoomSeatUsedState();
                        state.SeatAmountAll = roomState.ReadingRoom.SeatList.Seats.Count(u => u.Value.IsSuspended != true);
                        state.SeatAmountUsed = roomState.SeatAmountUsed;
                        state.SeatBookingCount = roomState.SeatBookingCount;
                        stateList.Add(roomState.ReadingRoom.No, state);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("获取阅览室信息失败：{0}", ex.Message));
            }
            return stateList;
        }
         //<summary>
         //获取多个阅览室座位使用状态V4
         //</summary>
         //<param name="roomNums">阅览室编号列表</param>
         //<param name="date"></param>
         //<returns></returns>
        public Dictionary<string, ReadingRoomSeatUsedState> GetRoomSeatUsedStateV4(List<string> roomNums)
        {
            //List<ReadingRoomInfo> rooms = GetReadingRoomInfo(roomNums);
            //List<EnterOutLogInfo> roomEnterOutLoglist = GetRoomUsingSeatEnterOutLogInfoV2(roomNums);
            //List<BespeakLogInfo> roomBespeakLogs = GetBespeakLogInfoByRoomsNum(roomNums, GetServerDateTime());
            //Dictionary<string, ReadingRoomSeatUsedState> list = new Dictionary<string, ReadingRoomSeatUsedState>();
            //for (int i = 0; i < roomNums.Count; i++)
            //{
            //    ReadingRoomSeatUsedState usedState = new ReadingRoomSeatUsedState();
            //    //usedState.PersonTimes = GetTodaySeatPerson(rooms[i].No);
            //    //usedState.SeatAmountAll = rooms[i].SeatList.Seats.Count(u => u.Value.IsSuspended != true);
            //    usedState.SeatAmountUsed = roomEnterOutLoglist.FindAll(u => u.ReadingRoomNo == roomNums[i]).Count;
            //    usedState.SeatBookingCount = roomBespeakLogs.FindAll(u => u.ReadingRoomNo == roomNums[i]).Count;
            //    list.Add(roomNums[i], usedState);
            //}
            //return list;

            Dictionary<string, int> roomEnterOutLogCount = GetRoomUsingSeatCount(roomNums);
            Dictionary<string, int> bookingCount = GetBookingCount(roomNums, GetServerDateTime());
            Dictionary<string, ReadingRoomSeatUsedState> list = new Dictionary<string, ReadingRoomSeatUsedState>();
            for (int i = 0; i < roomNums.Count; i++)
            {
                ReadingRoomSeatUsedState usedState = new ReadingRoomSeatUsedState();
                usedState.SeatAmountUsed = roomEnterOutLogCount[roomNums[i]];
                usedState.SeatBookingCount = bookingCount[roomNums[i]];
                //usedState.SeatTemUseCount = roomEnterOutLoglist.FindAll(u => u.ReadingRoomNo == roomNums[i] && roomBespeakLogs.Exists(p => p.SeatNo == u.SeatNo)).Count;
                list.Add(roomNums[i], usedState);
            }
            return list;
        }
        /// <summary>
        /// 获取多个阅览室座位使用状态V4
        /// </summary>
        /// <param name="roomNums">阅览室编号列表</param>
        /// <param name="date"></param>
        /// <returns></returns>
        public Dictionary<string, ReadingRoomSeatUsedState> GetRoomSeatUsedStateV5(List<string> roomNums)
        {
            Dictionary<string, int> roomEnterOutLogCount = GetRoomUsingSeatCount(roomNums);
            Dictionary<string, int> bookingCount = GetBookingCount(roomNums, GetServerDateTime());
            Dictionary<string, ReadingRoomSeatUsedState> list = new Dictionary<string, ReadingRoomSeatUsedState>();
            for (int i = 0; i < roomNums.Count; i++)
            {
                ReadingRoomSeatUsedState usedState = new ReadingRoomSeatUsedState();
                usedState.SeatAmountUsed = roomEnterOutLogCount[roomNums[i]];
                usedState.SeatBookingCount = bookingCount[roomNums[i]];
                //usedState.SeatTemUseCount = roomEnterOutLoglist.FindAll(u => u.ReadingRoomNo == roomNums[i] && roomBespeakLogs.Exists(p => p.SeatNo == u.SeatNo)).Count;
                list.Add(roomNums[i], usedState);
            }
            return list;
        }
        /// <summary>
        /// 获取阅览室座位布局
        /// </summary>
        /// <param name="roomNm"></param>
        /// <returns></returns>
        public SeatLayout GetRoomSeatLayOut(string roomNum)
        {
            try
            {
                string strWhere = string.Format(" readingRoomNo='{0}'", roomNum);
                //获取读者座位列表
                DataSet ds = t_sm_readingRoom_DAL.GetList(strWhere, null);
                //if (ds.Tables[0].Rows.Count == 0)
                //{
                //    throw new Exception("当前阅览室" + roomNum + "不存在");
                //}
                ReadingRoomInfo room = DataRowToReadingRoomInfo(ds.Tables[0].Rows[0]);
                SeatLayout layout = room.SeatList;

                //获取有效的进出记录
                List<EnterOutLogInfo> log = GetUsingSeatEnterOutLogInfo(roomNum);
                for (int i = 0; i < log.Count; i++)
                {
                    try
                    {
                        layout.Seats[log[i].SeatNo].SeatUsedState = log[i].EnterOutState;
                        layout.Seats[log[i].SeatNo].UserCardNo = log[i].CardNo;
                        layout.Seats[log[i].SeatNo].UserName = log[i].ReaderName;
                        layout.Seats[log[i].SeatNo].BeginUsedTime = log[i].EnterOutTime;
                        layout.Seats[log[i].SeatNo].MarkTime = log[i].MarkTime;
                        if (layout.Seats[log[i].SeatNo].SeatUsedState != SeatManage.EnumType.EnterOutLogType.Leave)
                        {
                            layout.Seats[log[i].SeatNo].CanBeBespeak = false;
                        }
                        else
                        {
                            layout.Seats[log[i].SeatNo].CanBeBespeak = true; ;
                        }
                    }
                    catch
                    { }
                }
                //获取有效预约记录
                DateTime NowDateTime = GetServerDateTime();
                List<BespeakLogInfo> bespeakLog = GetBespeakLogInfoByRoomNum(roomNum, NowDateTime);
                for (int j = 0; j < bespeakLog.Count; j++)
                {
                    try
                    {
                        if (layout.Seats[bespeakLog[j].SeatNo].SeatUsedState == SeatManage.EnumType.EnterOutLogType.None || layout.Seats[bespeakLog[j].SeatNo].SeatUsedState == SeatManage.EnumType.EnterOutLogType.Leave)
                        {
                            layout.Seats[bespeakLog[j].SeatNo].SeatUsedState = SeatManage.EnumType.EnterOutLogType.BespeakWaiting;
                            layout.Seats[bespeakLog[j].SeatNo].UserCardNo = bespeakLog[j].CardNo;
                            layout.Seats[bespeakLog[j].SeatNo].UserName = bespeakLog[j].ReaderName;
                            layout.Seats[bespeakLog[j].SeatNo].BeginUsedTime = bespeakLog[j].BsepeakTime;
                            layout.Seats[bespeakLog[j].SeatNo].MarkTime = Convert.ToDateTime("1900-1-1");
                            layout.Seats[bespeakLog[j].SeatNo].CanBeBespeak = false;
                        }
                    }
                    catch
                    { }
                }
                foreach (Seat s in layout.Seats.Values)
                {
                    s.ShortSeatNo = SeatManage.SeatManageComm.SeatComm.SeatNoToShortSeatNo(room.Setting.SeatNumAmount, s.SeatNo);
                    s.ReadingRoom = new ReadingRoomInfo() { Name = room.Name, No = room.No };

                }
                return layout;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 更新阅览室座位布局
        /// </summary>
        /// <param name="seatLayout"></param>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult UpdateSeatLayout(SeatLayout seatLayout)
        {
            try
            {
                List<string> roomNums = new List<string>();
                roomNums.Add(seatLayout.RoomNo);
                List<ReadingRoomInfo> roomInfos = GetReadingRoomInfo(roomNums);
                if (roomInfos.Count > 0)
                {
                    string delSeatWhere = string.Format(" ReadingRoomNo=@ReadingRoomNo");
                    SqlParameter[] parameters = {
                                            new SqlParameter("@ReadingRoomNo",SqlDbType.NVarChar,50)
                                        };
                    parameters[0].Value = seatLayout.RoomNo;
                    //删除原来的座位
                    t_sm_seat.Delete(delSeatWhere.ToString(), parameters);

                    //添加新的座位
                    foreach (Seat seat in seatLayout.Seats.Values)
                    {
                        seat.ReadingRoomNum = seatLayout.RoomNo;
                        t_sm_seat.Add(seat);
                    }

                    roomInfos[0].SeatList = seatLayout;
                    t_sm_readingRoom_DAL.Update(roomInfos[0]);
                    return SeatManage.EnumType.HandleResult.Successed;
                }
                else
                {
                    throw new Exception("阅览室编号不存在");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
        #endregion

        #region 私有方法
        private ReadingRoomInfo DataRowToReadingRoomInfo(DataRow dr)
        {
            ReadingRoomInfo roomInfo = new ReadingRoomInfo();
            roomInfo.No = dr["ReadingRoomNo"].ToString();
            roomInfo.Name = dr["ReadingRoomName"].ToString();
            if (!String.IsNullOrEmpty(dr["ReadingSetting"].ToString()))
            {
                roomInfo.Setting = new ReadingRoomSetting(dr["ReadingSetting"].ToString());
            }
            else
            {
                roomInfo.Setting = new ReadingRoomSetting();
            }
            if (!string.IsNullOrEmpty(dr["RoomSeat"].ToString()))
            {
                roomInfo.SeatList = SeatLayout.GetSeatLayout(dr["RoomSeat"].ToString());
            }
            else
            {
                roomInfo.SeatList = new SeatLayout();
            }
            roomInfo.Libaray.No = dr["LibraryNo"].ToString();
            roomInfo.Libaray.Name = dr["LibraryName"].ToString();
            roomInfo.Libaray.School.No = dr["SchoolNo"].ToString();
            roomInfo.Libaray.School.Name = dr["SchoolName"].ToString();
            roomInfo.Libaray.AreaList = roomInfo.Libaray.ToList(dr["AreaInfo"].ToString());
            if (dr["AreaName"] != null && !string.IsNullOrEmpty(dr["AreaName"].ToString()))
            {
                foreach (AreaInfo item in roomInfo.Libaray.AreaList)
                {
                    if (dr["AreaName"].ToString() == item.AreaName)
                    {
                        roomInfo.Area = item;
                        break;
                    }
                }
            }
            return roomInfo;
        }
        private ReadingRoomSeatUsedState_Ex DataRowToReadingRoomSeatUsedState(DataRow dr)
        {
            ReadingRoomSeatUsedState_Ex state = new ReadingRoomSeatUsedState_Ex();
            state.ReadingRoom = new ReadingRoomInfo();
            state.ReadingRoom.No = dr["ReadingRoomNo"].ToString();
            state.ReadingRoom.Name = dr["ReadingRoomName"].ToString();
            if (!String.IsNullOrEmpty(dr["ReadingSetting"].ToString()))
            {
                state.ReadingRoom.Setting = new ReadingRoomSetting(dr["ReadingSetting"].ToString());
            }
            else
            {
                state.ReadingRoom.Setting = new ReadingRoomSetting();
            }
            if (!string.IsNullOrEmpty(dr["RoomSeat"].ToString()))
            {
                state.ReadingRoom.SeatList = SeatLayout.GetSeatLayout(dr["RoomSeat"].ToString());
            }
            else
            {
                state.ReadingRoom.SeatList = new SeatLayout();
            }
            state.ReadingRoom.Libaray.No = dr["LibraryNo"].ToString();
            state.ReadingRoom.Libaray.Name = dr["LibraryName"].ToString();
            state.ReadingRoom.Libaray.School.No = dr["SchoolNo"].ToString();
            state.ReadingRoom.Libaray.School.Name = dr["SchoolName"].ToString();
            state.ReadingRoom.Libaray.AreaList = state.ReadingRoom.Libaray.ToList(dr["AreaInfo"].ToString());
            if (dr["AreaName"] != null && !string.IsNullOrEmpty(dr["AreaName"].ToString()))
            {
                foreach (AreaInfo item in state.ReadingRoom.Libaray.AreaList)
                {
                    if (dr["AreaName"].ToString() == item.AreaName)
                    {
                        state.ReadingRoom.Area = item;
                        break;
                    }
                }
            }
            state.SeatAmountUsed = int.Parse(dr["UsingCount"].ToString());
            state.SeatBookingCount = int.Parse(dr["BespeakCount"].ToString());
            return state;
        }
        #endregion
    }
}
