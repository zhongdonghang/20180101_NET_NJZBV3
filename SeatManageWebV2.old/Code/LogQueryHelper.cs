using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SeatManage.ClassModel;
using SeatManage.Bll;
using System.Threading;

namespace SeatManageWebV2.Code
{
    /// <summary>
    /// 记录查询助手
    /// </summary>
    public class LogQueryHelper
    {
        /// <summary>
        /// 进出记录list转换为DataTable
        /// </summary>
        /// <param name="num"></param>
        /// <param name="queryMethod"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataTable GetEnterOutLogDataSet(string num, string roomNum, EnumEnterOutLogQueryMethod queryMethod, DateTime startDate, DateTime endDate)
        {

            List<EnterOutLogInfo> enterOutLogList = new List<EnterOutLogInfo>();
            switch (queryMethod)
            {
                case EnumEnterOutLogQueryMethod.CardNo:
                    enterOutLogList = SeatManage.Bll.T_SM_EnterOutLog.GetEnterOutLogs(num, roomNum, null, startDate, endDate);
                    break;
                case EnumEnterOutLogQueryMethod.SeatNum:
                    ReadingRoomInfo room = null;
                    string seatNo = roomNum;
                    if (!string.IsNullOrEmpty(roomNum))
                    {
                        room = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(roomNum);
                        seatNo += "000";

                        seatNo = SeatManage.SeatManageComm.SeatComm.SeatNoToSeatNoHeader(room.Setting.SeatNumAmount, seatNo) + num; enterOutLogList = SeatManage.Bll.T_SM_EnterOutLog.GetEnterOutLogs(null, roomNum, seatNo, startDate, endDate);
                    }
                    else
                    {
                        FineUI.Alert.Show("请选择座位所在的阅览室");
                        // return null;
                    }

                    break;
            }

            return enterOutLogListToDataTable(enterOutLogList);
        }
        /// <summary>
        /// 进出记录list转换为DataTable
        /// </summary>
        /// <param name="num"></param>
        /// <param name="queryMethod"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataTable GetEnterOutLogDataSet_ByFuzzySearch(string num, string roomNum, EnumEnterOutLogQueryMethod queryMethod, DateTime startDate, DateTime endDate)
        {

            List<EnterOutLogInfo> enterOutLogList = new List<EnterOutLogInfo>();
            switch (queryMethod)
            {
                case EnumEnterOutLogQueryMethod.CardNo:
                    enterOutLogList = SeatManage.Bll.T_SM_EnterOutLog.GetEnterOutLogs_ByFuzzySearch(num, roomNum, null, startDate, endDate);
                    break;
                case EnumEnterOutLogQueryMethod.SeatNum:
                    ReadingRoomInfo room = null;
                    string seatNo = roomNum;
                    if (!string.IsNullOrEmpty(roomNum))
                    {
                        room = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(roomNum);
                        seatNo += "000";

                        seatNo = SeatManage.SeatManageComm.SeatComm.SeatNoToSeatNoHeader(room.Setting.SeatNumAmount, seatNo) + num; enterOutLogList = SeatManage.Bll.T_SM_EnterOutLog.GetEnterOutLogs(null, roomNum, seatNo, startDate, endDate);
                    }
                    else
                    {
                        FineUI.Alert.Show("请选择座位所在的阅览室");
                        // return null;
                    }

                    break;
            }

            return enterOutLogListToDataTable(enterOutLogList);
        }
        /// <summary>
        /// 座位预约记录查询
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="seatNo"></param>
        /// <param name="roomNum"></param>
        /// <param name="bespeakStatus"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataTable BespeakLogQuery(string cardNo, string roomNum, List<SeatManage.EnumType.BookingStatus> bespeakStatus, DateTime startDate, DateTime endDate)
        {
            TimeSpan span = endDate - startDate;
            List<BespeakLogInfo> bespeakLogList = SeatManage.Bll.T_SM_SeatBespeak.GetBespeakList(cardNo, roomNum, startDate, span.Days, bespeakStatus);
            DataTable dt = new DataTable();
            dt.Columns.Add("BespeakID", typeof(string));
            dt.Columns.Add("CardNo", typeof(string));
            dt.Columns.Add("ReaderName", typeof(string));
            dt.Columns.Add("ReadingRoomName", typeof(string));
            dt.Columns.Add("SeatNum", typeof(string));
            dt.Columns.Add("BsepeakState", typeof(string));
            dt.Columns.Add("SubmitTime", typeof(DateTime));
            dt.Columns.Add("BespeakTime", typeof(DateTime));
            dt.Columns.Add("CancelTime", typeof(DateTime));
            dt.Columns.Add("Remark", typeof(string));
            dt.Columns.Add("BsepeakStateEnum", typeof(string));
            foreach (BespeakLogInfo model in bespeakLogList)
            {
                DataRow dr = dt.NewRow();
                dr["BespeakID"] = model.BsepeaklogID;
                dr["CardNo"] = model.CardNo;
                dr["ReaderName"] = model.ReaderName;
                dr["ReadingRoomName"] = model.ReadingRoomName;
                dr["SeatNum"] = model.ShortSeatNum;
                dr["SubmitTime"] = model.SubmitTime;
                dr["BespeakTime"] = model.BsepeakTime;
                dr["BsepeakState"] = SeatManage.SeatManageComm.SeatComm.ConvertBookingStatus(model.BsepeakState);
                dr["BsepeakStateEnum"] = (int)model.BsepeakState;
                if (model.CancelTime.CompareTo(DateTime.Parse("1900-1-1")) != 0)
                {
                    dr["CancelTime"] = model.CancelTime;
                }
                dr["Remark"] = model.Remark;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 通过学号模糊查询座位预约记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="seatNo"></param>
        /// <param name="roomNum"></param>
        /// <param name="bespeakStatus"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataTable BespeakLogQuery_ByFuzzySearch(string cardNo, string roomNum, List<SeatManage.EnumType.BookingStatus> bespeakStatus, DateTime startDate, DateTime endDate)
        {
            TimeSpan span = endDate - startDate;
            List<BespeakLogInfo> bespeakLogList = SeatManage.Bll.T_SM_SeatBespeak.GetBespeakList_ByFuzzySearch(cardNo, roomNum, endDate, span.Days, bespeakStatus);
            DataTable dt = new DataTable();
            dt.Columns.Add("BespeakID", typeof(string));
            dt.Columns.Add("CardNo", typeof(string));
            dt.Columns.Add("ReaderName", typeof(string));
            dt.Columns.Add("ReadingRoomName", typeof(string));
            dt.Columns.Add("SeatNum", typeof(string));
            dt.Columns.Add("BsepeakState", typeof(string));
            dt.Columns.Add("SubmitTime", typeof(DateTime));
            dt.Columns.Add("BespeakTime", typeof(DateTime));
            dt.Columns.Add("CancelTime", typeof(DateTime));
            dt.Columns.Add("Remark", typeof(string));
            dt.Columns.Add("BsepeakStateEnum", typeof(string));
            foreach (BespeakLogInfo model in bespeakLogList)
            {
                DataRow dr = dt.NewRow();
                dr["BespeakID"] = model.BsepeaklogID;
                dr["CardNo"] = model.CardNo;
                dr["ReaderName"] = model.ReaderName;
                dr["ReadingRoomName"] = model.ReadingRoomName;
                dr["SeatNum"] = model.ShortSeatNum;
                dr["SubmitTime"] = model.SubmitTime;
                dr["BespeakTime"] = model.BsepeakTime;
                dr["BsepeakState"] = SeatManage.SeatManageComm.SeatComm.ConvertBookingStatus(model.BsepeakState);
                dr["BsepeakStateEnum"] = (int)model.BsepeakState;
                if (model.CancelTime.CompareTo(DateTime.Parse("1900-1-1")) != 0)
                {
                    dr["CancelTime"] = model.CancelTime;
                }
                dr["Remark"] = model.Remark;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 获取阅览室列表
        /// </summary>
        /// <param name="LoginID"></param>
        /// <returns></returns>
        public static DataTable GetMonitorGraphReadingRoomList(string LoginID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("roomNum", typeof(string));
            dt.Columns.Add("roomName", typeof(string));
            dt.Columns.Add("libraryName", typeof(string));
            dt.Columns.Add("seatCountAll", typeof(int));
            dt.Columns.Add("seatCountUsed", typeof(int));
            dt.Columns.Add("seatCountShortLeave", typeof(int));

            SeatManage.ClassModel.ManagerPotency potency = SeatManage.Bll.T_SM_ManagerPotency.GetManangePotencyByLoginID(LoginID);
            List<string> roomNums = new List<string>();
            Dictionary<string, ReadingRoomSeatUsedState> roomSeatUsingState = new Dictionary<string, ReadingRoomSeatUsedState>();
            if (potency != null)
            {
                for (int i = 0; i < potency.RightRoomList.Count; i++)
                {
                    roomNums.Add(potency.RightRoomList[i].No);
                }
            }
            roomSeatUsingState = NowReadingRoomState.GetRoomSeatUsedState(roomNums);
            foreach (ReadingRoomInfo model in potency.RightRoomList)
            {
                DataRow dr = dt.NewRow();
                dr["roomNum"] = model.No;
                dr["roomName"] = model.Name;
                dr["libraryName"] = model.Libaray.Name;
                int stopSeatCount = 0;
                foreach (KeyValuePair<string, Seat> item in model.SeatList.Seats)
                {
                    if (item.Value.IsSuspended)
                    {
                        stopSeatCount++;
                    }
                }
                dr["seatCountAll"] = roomSeatUsingState[model.No].SeatAmountAll - stopSeatCount;
                dr["seatCountUsed"] = roomSeatUsingState[model.No].SeatAmountUsed;
                dr["seatCountShortLeave"] = roomSeatUsingState[model.No].SeatAmountShortLeave;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static DataTable UsingSeatReader(string roomId)
        {
            List<EnterOutLogInfo> logList = SeatManage.Bll.T_SM_EnterOutLog.GetUsingSeatEnterOutLogInfo(roomId);
            return enterOutLogListToDataTable(logList);
        }
        /// <summary>
        /// 读者通知记录表
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public static DataTable ReaderNoticeList(string cardNo)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NoticeId");
            dt.Columns.Add("CardNo");
            dt.Columns.Add("AddTime");
            dt.Columns.Add("NoticeContent");
            dt.Columns.Add("IsRead");
            List<ReaderNoticeInfo> notictList = SeatManage.Bll.T_SM_ReaderNotice.GetReaderNoticeByCardNoStatus(cardNo, SeatManage.EnumType.LogStatus.None);
            for (int i = 0; i < notictList.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["NoticeId"] = notictList[i].NoticeID;
                dr["CardNo"] = cardNo;
                dr["AddTime"] = notictList[i].AddTime;
                dr["NoticeContent"] = notictList[i].Note;
                dr["IsRead"] = notictList[i].IsRead;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 预约座位的阅览室
        /// </summary>
        /// <returns></returns>
        public static DataTable BespeakRoomList(DateTime date, string libNo)
        {
            List<string> libNums = new List<string>();
            libNums.Add(libNo);
            List<ReadingRoomInfo> roomList = SeatManage.Bll.T_SM_ReadingRoom.GetReadingRooms(null, libNums, null);
            List<string> roomNums = new List<string>();
            for (int i = 0; i < roomList.Count; i++)
            {
                roomNums.Add(roomList[i].No);
            }
            Dictionary<string, ReadingRoomSeatBespeakState> seatBespeakState = SeatManage.Bll.T_SM_SeatBespeak.GetRoomBespeakSeatState(roomNums, date);
            DataTable dt = new DataTable();
            dt.Columns.Add("roomNum", typeof(string));
            dt.Columns.Add("roomName", typeof(string));
            dt.Columns.Add("libraryName", typeof(string));
            dt.Columns.Add("CanBespeakAmcount", typeof(int));
            dt.Columns.Add("SurplusBespeskAmcount", typeof(int));
            dt.Columns.Add("RoomSetting", typeof(string));
            foreach (ReadingRoomInfo room in roomList)
            {
                DataRow dr = dt.NewRow();
                dr["roomNum"] = room.No;
                dr["roomName"] = room.Name;
                dr["libraryName"] = room.Libaray.Name;
                int stopSeatCount = 0;
                foreach (KeyValuePair<string, Seat> item in room.SeatList.Seats)
                {
                    if (item.Value.IsSuspended)
                    {
                        stopSeatCount++;
                    }
                }
                dr["CanBespeakAmcount"] = seatBespeakState[room.No].CanBespeakAmcount;
                dr["SurplusBespeskAmcount"] = seatBespeakState[room.No].CanBespeakAmcount - seatBespeakState[room.No].BespeakedAmcount;
                dr["RoomSetting"] = room.Setting.ToString();
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 图书馆座位信息绑定
        /// </summary>
        /// <param name="libId"></param>
        /// <returns></returns>
        public static DataTable LibrarySeatInfo(string libNum)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ReadingRoomName", typeof(string));
            dt.Columns.Add("SeatAmount", typeof(int));
            dt.Columns.Add("SeatUsedAmount", typeof(int));
            dt.Columns.Add("PersonTimes", typeof(int));
            dt.Columns.Add("LeisureSeat", typeof(int));
            //获取图书馆下的阅览室信息
            List<string> libNums = new List<string>();
            libNums.Add(libNum);
            List<SeatManage.ClassModel.ReadingRoomInfo> rooms = SeatManage.Bll.T_SM_ReadingRoom.GetReadingRooms(null, libNums, null);
            List<string> roomNums = new List<string>();
            foreach (SeatManage.ClassModel.ReadingRoomInfo room in rooms)
            {
                roomNums.Add(room.No);
            }
            //获取阅览室中的座位使用信息
            Dictionary<string, ReadingRoomSeatUsedState> roomSeatUsingState = new Dictionary<string, ReadingRoomSeatUsedState>();
            roomSeatUsingState = NowReadingRoomState.GetRoomSeatUsedState(roomNums);
            for (int i = 0; i < rooms.Count; i++)
            {
                DataRow dr = dt.NewRow();

                dr["ReadingRoomName"] = rooms[i].Name;
                dr["SeatAmount"] = roomSeatUsingState[rooms[i].No].SeatAmountAll;
                dr["SeatUsedAmount"] = roomSeatUsingState[rooms[i].No].SeatAmountUsed;
                dr["PersonTimes"] = roomSeatUsingState[rooms[i].No].PersonTimes;
                dr["LeisureSeat"] = roomSeatUsingState[rooms[i].No].SeatAmountAll - roomSeatUsingState[rooms[i].No].SeatAmountUsed;
                dt.Rows.Add(dr);

            }
            return dt;
        }
        /// <summary>
        /// 获取当天预约座位
        /// </summary>
        /// <param name="libNum"></param>
        /// <returns></returns>
        public static DataTable NowDayBespeakRoomInfo(string libNum)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("roomNum", typeof(string));
            dt.Columns.Add("roomName", typeof(string));
            dt.Columns.Add("libraryName", typeof(string));
            dt.Columns.Add("SeatAmount", typeof(int));
            dt.Columns.Add("SurplusBespeskAmcount", typeof(int));
            dt.Columns.Add("RoomSetting", typeof(string));
            //获取图书馆下的阅览室信息
            List<string> libNums = new List<string>();
            libNums.Add(libNum);
            List<SeatManage.ClassModel.ReadingRoomInfo> rooms = SeatManage.Bll.T_SM_ReadingRoom.GetReadingRooms(null, libNums, null);
            List<string> roomNums = new List<string>();
            foreach (SeatManage.ClassModel.ReadingRoomInfo room in rooms)
            {
                roomNums.Add(room.No);
            }
            //获取阅览室中的座位使用信息
            Dictionary<string, ReadingRoomSeatUsedState> roomSeatUsingState = new Dictionary<string, ReadingRoomSeatUsedState>();
            roomSeatUsingState = NowReadingRoomState.GetRoomSeatUsedState(roomNums);
            for (int i = 0; i < rooms.Count; i++)
            {
                DataRow dr = dt.NewRow();

                dr["roomName"] = rooms[i].Name;
                dr["roomNum"] = rooms[i].No;
                dr["libraryName"] = rooms[i].Libaray.Name;
                dr["SeatAmount"] = roomSeatUsingState[rooms[i].No].SeatAmountAll;
                dr["SurplusBespeskAmcount"] = roomSeatUsingState[rooms[i].No].SeatAmountAll - roomSeatUsingState[rooms[i].No].SeatAmountUsed - roomSeatUsingState[rooms[i].No].SeatBookingCount + roomSeatUsingState[rooms[i].No].SeatTemUseCount;
                dr["RoomSetting"] = rooms[i].Setting.ToString();
                dt.Rows.Add(dr);

            }
            return dt;
        }

        /// <summary>
        /// 进出记录列表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static DataTable enterOutLogListToDataTable(List<EnterOutLogInfo> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CardNo", typeof(string));
            dt.Columns.Add("ReaderName", typeof(string));
            dt.Columns.Add("ReadingRoomName", typeof(string));
            dt.Columns.Add("SeatNum", typeof(string));
            dt.Columns.Add("SeatShortNum", typeof(string));
            dt.Columns.Add("ReadingRoomNum", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("EnterOutTime", typeof(DateTime));
            dt.Columns.Add("Remark", typeof(string));
            foreach (EnterOutLogInfo model in list)
            {
                DataRow dr = dt.NewRow();
                dr["SeatNum"] = model.SeatNo;
                dr["ReadingRoomNum"] = model.ReadingRoomNo;
                dr["Status"] = SeatManage.SeatManageComm.SeatComm.ConvertReaderState(model.EnterOutState);
                dr["CardNo"] = model.CardNo;
                dr["ReaderName"] = model.ReaderName;
                dr["ReadingRoomName"] = model.ReadingRoomName;
                dr["SeatShortNum"] = model.ShortSeatNo;
                dr["EnterOutTime"] = model.EnterOutTime;
                dr["Remark"] = model.Remark;
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}