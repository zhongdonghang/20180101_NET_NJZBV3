using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.ClassModel;
using System.Data;
using System.Data.SqlClient;
using SeatManage.DAL;
using SeatManage.EnumType;
using System.Threading;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        private T_SM_SeatBespeak seatBespeakDal = new T_SM_SeatBespeak();

        #region 预约记录信息操作
        /// <summary>
        /// 获取预约记录信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="shortDate"></param>
        /// <returns></returns>
        public List<BespeakLogInfo> GetBespeakLogInfo(string cardNo, DateTime shortDate)
        {
            List<BespeakLogInfo> bespeatlogs = new List<BespeakLogInfo>();
            StringBuilder strWhere = new StringBuilder();
            if (string.IsNullOrEmpty(cardNo))
            {
                return bespeatlogs;
            }
            strWhere.Append(" cardNo=@cardNo ");
            strWhere.Append(" and BespeakTime>='" + shortDate.Date + "' and BespeakTime<'" + shortDate.AddDays(1).Date + "' ");
            strWhere.Append(" and [BespeakState]=@bookState");
            SqlParameter[] parameters = {
                                         new SqlParameter("@cardNo",SqlDbType.NVarChar),
                                         new SqlParameter("@bookState",SqlDbType.Int), 
                                        };
            parameters[0].Value = cardNo;
            parameters[1].Value = (int)BookingStatus.Waiting;
            try
            {
                DataSet ds = seatBespeakDal.GetList(strWhere.ToString(), parameters);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    BespeakLogInfo log = DataRowToBespeakLogInfo(ds.Tables[0].Rows[i]);
                    if (log != null)
                    {
                        bespeatlogs.Add(log);
                    }
                }
            }
            catch
            {
            }

            return bespeatlogs;
        }
        /// <summary>
        /// 获取单条有效的预约记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public BespeakLogInfo GetSingleBespeakLogForWait(string cardNo)
        {
            BespeakLogInfo bespeatlogs = null;
            StringBuilder strWhere = new StringBuilder();
            if (string.IsNullOrEmpty(cardNo))
            {
                return bespeatlogs;
            }
            strWhere.Append(" cardNo=@cardNo ");
            strWhere.Append(" and [BespeakState]=@bookState order by BespeakTime asc");
            SqlParameter[] parameters = {
                                         new SqlParameter("@cardNo",SqlDbType.NVarChar),
                                         new SqlParameter("@bookState",SqlDbType.Int), 
                                        };
            parameters[0].Value = cardNo;
            parameters[1].Value = (int)BookingStatus.Waiting;
            try
            {
                DataSet ds = seatBespeakDal.GetList(strWhere.ToString(), parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    bespeatlogs = DataRowToBespeakLogInfo(ds.Tables[0].Rows[0]);
                }
            }
            catch
            {
            }

            return bespeatlogs;
        }


        /// <summary>
        /// 分页查询预约记录，用于android客户端上拉刷新
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<BespeakLogInfo> GetBespeakLogsByPage(string cardNo, int pageIndex, int pageSize)
        {
            List<BespeakLogInfo> bespeatlogs = new List<BespeakLogInfo>();
            DataSet ds = seatBespeakDal.GetList(cardNo, pageSize, pageIndex);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                BespeakLogInfo log = DataRowToBespeakLogInfo(ds.Tables[0].Rows[i]);
                if (log != null)
                {
                    bespeatlogs.Add(log);
                }
            }
            return bespeatlogs;
        }


        /// <summary>
        /// 获取全部的记录，为空就是不设条件查询
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="roomNum">阅览室编号</param>
        /// <param name="date">查询当前日期的记录</param>
        /// /// <param name="status">预约状态</param>
        /// <returns></returns>
        public List<BespeakLogInfo> GetBespeakLogs(string cardNo, string roomNum, DateTime date, int spanDays, List<BookingStatus> status)
        {
            List<BespeakLogInfo> bespeatlogs = new List<BespeakLogInfo>();
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(cardNo))
            {
                if (String.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" cardNo='{0}'", cardNo));
                }
                else
                {
                    strWhere.Append(string.Format(" and cardNo='{0}'", cardNo));
                }
            }
            if (!string.IsNullOrEmpty(roomNum))
            {
                if (String.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" readingRoomNo='{0}'", roomNum));
                }
                else
                {
                    strWhere.Append(string.Format(" and readingRoomNo='{0}'", roomNum));
                }
            }
            if (status != null)
            {
                for (int i = 0; i < status.Count; i++)
                {
                    if (i == 0)
                    {
                        if (String.IsNullOrEmpty(strWhere.ToString()))
                        {
                            strWhere.Append(string.Format(" BespeakState in ('{0}' ", (int)status[i]));
                        }
                        else
                        {
                            strWhere.Append(string.Format(" and BespeakState in ('{0}'", (int)status[i]));
                        }
                    }
                    else if (i != status.Count - 1)
                    {
                        strWhere.Append(string.Format(",'{0}'", (int)status[i]));

                    }
                    if (i == status.Count - 1)
                    {
                        strWhere.Append(string.Format(" ,'{0}') ", (int)status[i]));
                    }



                }
            }
            if (date.CompareTo(DateTime.Parse("1900-1-1")) != 0)
            {
                if (String.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(" BespeakTime>='" + date.Date + "' and BespeakTime<'" + date.AddDays(spanDays + 1).Date + "' ");
                }
                else
                {
                    strWhere.Append(" and BespeakTime>='" + date.Date + "' and BespeakTime<'" + date.AddDays(spanDays + 1).Date + "' ");
                }
            }
            //SqlParameter[] parameters = {
            //                            new SqlParameter("@shortDate",SqlDbType.DateTime),
            //                            new SqlParameter("@spanDays",SqlDbType.Int)
            //                          };
            //parameters[0].Value = date;
            //parameters[1].Value = spanDays;
            try
            {
                DataSet ds = seatBespeakDal.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    BespeakLogInfo log = DataRowToBespeakLogInfo(ds.Tables[0].Rows[i]);
                    if (log != null)
                    {
                        bespeatlogs.Add(log);
                    }
                }
            }
            catch
            {
                throw;
            }

            return bespeatlogs;
        }
        /// <summary>
        /// 根据学号模糊查询获取全部的记录，为空就是不设条件查询
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="roomNum">阅览室编号</param>
        /// <param name="date">查询当前日期的记录</param>
        /// /// <param name="status">预约状态</param>
        /// <returns></returns>
        public List<BespeakLogInfo> GetBespeakLogs_ByFuzzySearch(string cardNo, string roomNum, DateTime date, int spanDays, List<BookingStatus> status)
        {
            List<BespeakLogInfo> bespeatlogs = new List<BespeakLogInfo>();
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(cardNo))
            {
                if (String.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" cardNo like '%{0}%'", cardNo));
                }
                else
                {
                    strWhere.Append(string.Format(" and cardNo like '%{0}%'", cardNo));
                }
            }
            if (!string.IsNullOrEmpty(roomNum))
            {
                if (String.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" readingRoomNo='{0}'", roomNum));
                }
                else
                {
                    strWhere.Append(string.Format(" and readingRoomNo='{0}'", roomNum));
                }
            }
            if (status != null)
            {
                for (int i = 0; i < status.Count; i++)
                {
                    if (i == 0)
                    {
                        if (String.IsNullOrEmpty(strWhere.ToString()))
                        {
                            strWhere.Append(string.Format(" BespeakState in ('{0}' ", (int)status[i]));
                        }
                        else
                        {
                            strWhere.Append(string.Format(" and BespeakState in ('{0}'", (int)status[i]));
                        }
                    }
                    else if (i != status.Count - 1)
                    {
                        strWhere.Append(string.Format(",'{0}'", (int)status[i]));

                    }
                    if (i == status.Count - 1)
                    {
                        strWhere.Append(string.Format(" ,'{0}') ", (int)status[i]));
                    }



                }
            }
            if (date.CompareTo(DateTime.Parse("1900-1-1")) != 0)
            {
                if (String.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(" BespeakTime>='" + date.Date + "' and BespeakTime<'" + date.AddDays(spanDays+1).Date + "' ");
                }
                else
                {
                    strWhere.Append(" and BespeakTime>='" + date.Date + "' and BespeakTime<'" + date.AddDays(spanDays + 1).Date + "' ");
                }
            }
            //SqlParameter[] parameters = {
            //                            new SqlParameter("@shortDate",SqlDbType.DateTime),
            //                            new SqlParameter("@spanDays",SqlDbType.Int)
            //                          };
            //parameters[0].Value = date;
            //parameters[1].Value = spanDays;
            try
            {
                DataSet ds = seatBespeakDal.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    BespeakLogInfo log = DataRowToBespeakLogInfo(ds.Tables[0].Rows[i]);
                    if (log != null)
                    {
                        bespeatlogs.Add(log);
                    }
                }
            }
            catch
            {
                throw;
            }

            return bespeatlogs;
        }

        /// <summary>
        /// 获取所有的预约记录信息
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="roomNum">阅览室号，为空则查询所有</param>
        /// <returns></returns>
        public List<BespeakLogInfo> GetBespeakLogInfos(string cardNo, string roomNum)
        {
            List<BespeakLogInfo> bespeaklog = new List<BespeakLogInfo>();
            if (string.IsNullOrEmpty(cardNo))
            {
                return bespeaklog;
            }
            string strWhere = "  [ReadingRoomNo]=@readingRoomNo  ";
            SqlParameter[] parameters =  { 
                                             new SqlParameter("@readingRoomNo",roomNum) 
                                         };

            List<BespeakLogInfo> bookLogInfoList = new List<BespeakLogInfo>();
            DataSet ds = seatBespeakDal.GetList(strWhere, parameters);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                BespeakLogInfo log = DataRowToBespeakLogInfo(ds.Tables[0].Rows[i]);
                if (log != null)
                {
                    bookLogInfoList.Add(log);
                }
            }
            return bookLogInfoList;
        }
        /// <summary>
        /// 添加预约记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public HandleResult AddBespeakLogInfo(BespeakLogInfo model)
        {
            try
            {
                model.FlagKey = GetBespeakMD5Key(model);
                List<BespeakLogInfo> list = GetBespeakLogInfoBySeatNoNotCheck(model.SeatNo, model.BsepeakTime);
                if (list.Count > 0)
                {
                    foreach (BespeakLogInfo bespeaklog in list)
                    {
                        if (GetBespeakMD5Key(bespeaklog) != bespeaklog.FlagKey)
                        {
                            bespeaklog.BsepeakState = BookingStatus.Cencaled;
                            bespeaklog.CancelPerson = Operation.Service;
                            bespeaklog.Remark = "预约记录校验失败，系统取消此次预约";
                            bespeaklog.FlagKey = GetBespeakMD5Key(bespeaklog);
                            UpdateBespeakLogInfo(bespeaklog);
                        }
                    }
                }
                int resultValue = seatBespeakDal.Add(model);
                if (resultValue == 0)
                {
                    List<ReadingRoomInfo> rri = GetReadingRoomInfo(new List<string>() { model.ReadingRoomNo });
                    if (rri == null || rri.Count == 0)
                    {
                        return HandleResult.Successed;
                    }

                    PushMsgInfo msg = new PushMsgInfo();
                    msg.Title = "您好，您已预约成功";
                    msg.MsgType = MsgPushType.UserOperation;
                    msg.StudentNum = model.CardNo;
                    msg.Message = model.Remark;
                    msg.RoomName = rri[0].Name;
                    msg.SeatNum = SeatManage.SeatManageComm.SeatComm.SeatNoToShortSeatNo(rri[0].Setting.SeatNumAmount, model.SeatNo);
                    SendMsg(msg);
                    return HandleResult.Successed;
                }
                else
                {
                    return HandleResult.Failed;
                }
            }
            catch
            {
                throw;
            }
        }

        public int UpdateBespeakLogInfo(BespeakLogInfo bespeakLog)
        {
            try
            {
                bespeakLog.FlagKey = GetBespeakMD5Key(bespeakLog);
                int i = seatBespeakDal.Update(bespeakLog);
                if (i > 0)
                {

                    if (bespeakLog.BsepeakState == BookingStatus.Cencaled)
                    {
                        PushMsgInfo msg = new PushMsgInfo();
                        msg.Title = "您好，您的预约已取消";
                        switch (bespeakLog.CancelPerson)
                        {
                            case Operation.Admin:
                                msg.Title = string.Format(msg.Title, "您好，您的预约已被管理员取消");
                                break;
                            case Operation.Service:
                                msg.Title = string.Format(msg.Title, "您好，您的预约已超时");
                                break;
                        }
                        msg.MsgType = MsgPushType.UserOperation;
                        msg.StudentNum = bespeakLog.CardNo;
                        msg.Message = bespeakLog.Remark;
                        msg.RoomName = bespeakLog.ReadingRoomName;
                        msg.SeatNum = bespeakLog.ShortSeatNum;
                        SendMsg(msg);
                    }

                }
                return i;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 根据Id获取进出记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BespeakLogInfo GetBespeaklogById(int id)
        {
            string strWhere = "  [BespeakID]=@BespeakID  ";
            SqlParameter[] parameters =  { 
                                             new SqlParameter("@BespeakID",id) 
                                         };

            DataSet ds = seatBespeakDal.GetList(strWhere, parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                BespeakLogInfo log = DataRowToBespeakLogInfo(ds.Tables[0].Rows[0]);
                return log;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据阅览室编号获取当前提供预约的座位布局
        /// </summary>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        public SeatLayout GetBeseakSeatSettingLayout(string roomNum)
        {
            List<string> roomNums = new List<string>();
            roomNums.Add(roomNum);
            List<ReadingRoomInfo> roomList = GetReadingRoomInfo(roomNums);
            if (roomList.Count > 0)
            {
                if (roomList[0].Setting.SeatBespeak.BespeakArea.BespeakType == BespeakAreaType.AppointSeat)
                {
                    foreach (string seatNo in roomList[0].SeatList.Seats.Keys)
                    {
                        roomList[0].SeatList.Seats[seatNo].ShortSeatNo = seatNo.Substring(seatNo.Length - roomList[0].Setting.SeatNumAmount, roomList[0].Setting.SeatNumAmount);
                    }
                }
                return roomList[0].SeatList;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 获取阅览室中预约座位的座位布局
        /// </summary>
        /// <param name="roomNum"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public SeatLayout GetBeseakSeatLayout(string roomNum, DateTime date)
        {
            List<string> roomNums = new List<string>();
            roomNums.Add(roomNum);
            List<ReadingRoomInfo> roomList = GetReadingRoomInfo(roomNums);
            if (roomList.Count > 0)
            {
                //判断阅览室座位预约的方式是指定座位还是百分比
                if (roomList[0].Setting.SeatBespeak.BespeakArea.BespeakType == BespeakAreaType.Percentage)
                {
                    foreach (string seatNo in roomList[0].SeatList.Seats.Keys)
                    {
                        roomList[0].SeatList.Seats[seatNo].CanBeBespeak = true;
                        roomList[0].SeatList.Seats[seatNo].ShortSeatNo = seatNo.Substring(seatNo.Length - roomList[0].Setting.SeatNumAmount, roomList[0].Setting.SeatNumAmount);
                        roomList[0].SeatList.Seats[seatNo].ReadingRoom = new ReadingRoomInfo { Name = roomList[0].Name, No = roomList[0].No };
                    }
                    List<BespeakLogInfo> bespeaklist = GetBespeakLogInfoByRoomNum(roomNum, date);
                    foreach (BespeakLogInfo info in bespeaklist)
                    {
                        roomList[0].SeatList.Seats[info.SeatNo].SeatUsedState = EnterOutLogType.BookingConfirmation;
                        roomList[0].SeatList.Seats[info.SeatNo].UserCardNo = info.CardNo;
                    }
                }
                //指定座位预约
                else if ((roomList[0].Setting.SeatBespeak.BespeakArea.BespeakType == BespeakAreaType.AppointSeat))
                {
                    foreach (string seatNo in roomList[0].SeatList.Seats.Keys)
                    {
                        roomList[0].SeatList.Seats[seatNo].ShortSeatNo = seatNo.Substring(seatNo.Length - roomList[0].Setting.SeatNumAmount, roomList[0].Setting.SeatNumAmount);
                        roomList[0].SeatList.Seats[seatNo].ReadingRoom = new ReadingRoomInfo { Name = roomList[0].Name, No = roomList[0].No };
                    }
                    List<BespeakLogInfo> bespeaklist = GetBespeakLogInfoByRoomNum(roomNum, date);
                    foreach (BespeakLogInfo info in bespeaklist)
                    {
                        roomList[0].SeatList.Seats[info.SeatNo].SeatUsedState = EnterOutLogType.BookingConfirmation;
                        roomList[0].SeatList.Seats[info.SeatNo].UserCardNo = info.CardNo;
                    }
                }
                return roomList[0].SeatList;
            }
            else
            {
                return null;
            }
        }


        public List<BespeakLogInfo> GetBespeakLogInfoByRoomNum(string roomNum, DateTime date)
        {
            List<BespeakLogInfo> list = new List<BespeakLogInfo>();
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" ReadingRoomNo=@ReadingRoomNo");
            strWhere.Append(" and BespeakTime>='" + date.Date + "' and BespeakTime<'" + date.AddDays(1).Date + "' and BespeakState=@BespeakState");
            SqlParameter[] parameters = {
                                         new SqlParameter("@ReadingRoomNo",SqlDbType.NVarChar),
                                         new SqlParameter("@BespeakState",SqlDbType.Int), 
                                        };
            parameters[0].Value = roomNum;
            parameters[1].Value = (int)BookingStatus.Waiting;
            try
            {
                DataSet ds = seatBespeakDal.GetList(strWhere.ToString(), parameters);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    BespeakLogInfo log = DataRowToBespeakLogInfo(ds.Tables[0].Rows[i]);
                    if (log != null)
                    {
                        list.Add(log);
                    }
                }
            }
            catch
            {
                throw;
            }

            return list;
        }
        /// <summary>
        /// 获取阅览室座位的预约状态
        /// </summary>
        /// <param name="roomNums"></param>
        /// <returns></returns>
        public Dictionary<string, ReadingRoomSeatBespeakState> GetRoomBespeakSeatState(List<string> roomNums, DateTime date)
        {
            List<ReadingRoomInfo> roomList = GetReadingRoomInfo(roomNums);
            Dictionary<string, ReadingRoomSeatBespeakState> roomBespeakStateList = new Dictionary<string, ReadingRoomSeatBespeakState>();
            foreach (ReadingRoomInfo room in roomList)
            {
                //获取进出记录
                ReadingRoomSeatBespeakState state = new ReadingRoomSeatBespeakState();
                //不开放预约既不现实
                if (!room.Setting.SeatBespeak.Used)
                {
                    continue;
                }
                //if (!room.Setting.SeatBespeak.Used)
                //{
                //    state.CanBespeakAmcount = 0;
                //}
                else if (room.Setting.SeatBespeak.BespeakArea.BespeakType == BespeakAreaType.Percentage)
                {
                    if (room.Setting.SeatBespeak.BespeakArea.Scale != 0.0)
                    {
                        int stopSeatCount = 0;
                        foreach (KeyValuePair<string, Seat> item in room.SeatList.Seats)
                        {
                            if (item.Value.IsSuspended)
                            {
                                stopSeatCount++;
                            }
                        }
                        state.CanBespeakAmcount = (int)((room.SeatList.Seats.Values.Count - stopSeatCount) * room.Setting.SeatBespeak.BespeakArea.Scale);
                    }
                    else
                    {
                        state.CanBespeakAmcount = 0;

                    }
                }
                else if (room.Setting.SeatBespeak.BespeakArea.BespeakType == BespeakAreaType.AppointSeat)
                {
                    foreach (Seat seat in room.SeatList.Seats.Values)
                    {
                        if (seat.CanBeBespeak && !seat.IsSuspended)
                        {
                            state.CanBespeakAmcount += 1;
                        }
                    }
                }
                state.BespeakedAmcount = GetBespeakLogInfoByRoomNum(room.No, date).Count;
                roomBespeakStateList.Add(room.No, state);
            }
            return roomBespeakStateList;
        }

        /// <summary>
        /// 根据座位编号获取预约信息
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<BespeakLogInfo> GetBespeakLogInfoBySeatNo(string seatNo, DateTime date)
        {
            List<BespeakLogInfo> list = new List<BespeakLogInfo>();
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" SeatNo=@SeatNo");
            strWhere.Append(" and BespeakTime>='" + date.Date + "' and BespeakTime<'" + date.AddDays(1).Date + "' and BespeakState=@BespeakState");
            SqlParameter[] parameters = {
                                         new SqlParameter("@SeatNo",SqlDbType.NVarChar),
                                         new SqlParameter("@BespeakState",SqlDbType.Int), 
                                        };
            parameters[0].Value = seatNo;
            parameters[1].Value = (int)BookingStatus.Waiting;
            try
            {
                DataSet ds = seatBespeakDal.GetList(strWhere.ToString(), parameters);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    BespeakLogInfo log = DataRowToBespeakLogInfo(ds.Tables[0].Rows[i]);
                    if (log != null)
                    {
                        list.Add(log);
                    }
                }
            }
            catch
            {
                throw;
            }

            return list;
        }
        /// <summary>
        /// 根据座位编号获取预约信息
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<BespeakLogInfo> GetBespeakLogInfoBySeatNoNotCheck(string seatNo, DateTime date)
        {
            List<BespeakLogInfo> list = new List<BespeakLogInfo>();
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" SeatNo=@SeatNo");
            strWhere.Append(" and BespeakTime>='" + date.Date + "' and BespeakTime<'" + date.AddDays(1).Date + "' and BespeakState=@BespeakState");
            SqlParameter[] parameters = {
                                         new SqlParameter("@SeatNo",SqlDbType.NVarChar),
                                         new SqlParameter("@BespeakState",SqlDbType.Int), 
                                        };
            parameters[0].Value = seatNo;
            parameters[1].Value = (int)BookingStatus.Waiting;
            try
            {
                DataSet ds = seatBespeakDal.GetList(strWhere.ToString(), parameters);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    BespeakLogInfo log = DataRowToBespeakLogInfoNotCheck(ds.Tables[0].Rows[i]);
                    if (log != null)
                    {
                        list.Add(log);
                    }
                }
            }
            catch
            {
                throw;
            }

            return list;
        }
        /// <summary>
        /// 获取预约记录
        /// </summary>
        /// <param name="roomNum"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<BespeakLogInfo> GetBespeakLogInfoByRoomsNum(List<string> roomNum, DateTime date)
        {
            List<BespeakLogInfo> list = new List<BespeakLogInfo>();
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
            //if (string.IsNullOrEmpty(strWhere.ToString()))
            //{
            //    strWhere.Append(" datediff(day, BespeakTime,@shortDate)=0 and BespeakState=@BespeakState");
            //}
            //else
            //{
            //    strWhere.Append(" and datediff(day, BespeakTime,@shortDate)=0 and BespeakState=@BespeakState");
            //}
            //SqlParameter[] parameters = {
            //                             new SqlParameter("@shortDate",SqlDbType.DateTime),
            //                             new SqlParameter("@BespeakState",SqlDbType.Int), 
            //                            };
            //parameters[0].Value = date;
            //parameters[1].Value = (int)BookingStatus.Waiting;
            if (string.IsNullOrEmpty(strWhere.ToString()))
            {
                strWhere.Append(" BespeakTime>='" + date.Date + "' and BespeakTime<'" + date.AddDays(1).Date + "' and BespeakState='" + (int)BookingStatus.Waiting + "'");
            }
            else
            {
                strWhere.Append(" and  BespeakTime>='" + date.Date + "' and BespeakTime<'" + date.AddDays(1).Date + "' and BespeakState='" + (int)BookingStatus.Waiting + "'");
            }
            try
            {
                DataSet ds = seatBespeakDal.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    BespeakLogInfo log = DataRowToBespeakLogInfo(ds.Tables[0].Rows[i]);
                    if (log != null)
                    {
                        list.Add(log);
                    }
                }
            }
            catch
            {
                throw;
            }

            return list;
        }
        /// <summary>
        /// 获取日期前未签到的座位
        /// </summary>
        /// <param name="roomNum"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<BespeakLogInfo> GetNotCheckedBespeakLogInfo(List<string> roomNum, DateTime date)
        {
            List<BespeakLogInfo> list = new List<BespeakLogInfo>();
            //StringBuilder strWhere = new StringBuilder();
            //if (roomNum != null)
            //{
            //    for (int i = 0; i < roomNum.Count; i++)
            //    {

            //        if (i == 0)
            //        {
            //            strWhere.Append(string.Format(" ReadingRoomNo in ('{0}'", roomNum[i]));
            //        }
            //        else if (i != roomNum.Count - 1)
            //        {
            //            strWhere.Append(string.Format(",'{0}'  ", roomNum[i]));
            //        }
            //        if (i == roomNum.Count - 1)
            //        {
            //            strWhere.Append(string.Format(" ,'{0}')", roomNum[i]));
            //        }
            //    }
            //}
            //if (string.IsNullOrEmpty(strWhere.ToString()))
            //{
            //    strWhere.Append(" datediff(day, BespeakTime,@shortDate)>=0 and BespeakState=@BespeakState");
            //}
            //else
            //{
            //    strWhere.Append(" and datediff(day, BespeakTime,@shortDate)>=0 and BespeakState=@BespeakState");
            //}
            //SqlParameter[] parameters = {
            //                             new SqlParameter("@shortDate",SqlDbType.DateTime),
            //                             new SqlParameter("@BespeakState",SqlDbType.Int), 
            //                            };
            //parameters[0].Value = date;
            //parameters[1].Value = (int)BookingStatus.Waiting;
            try
            {
                if (roomNum != null && roomNum.Count > 0)
                {
                    for (int k = 0; k < roomNum.Count; k++)
                    {
                        StringBuilder strWhere = new StringBuilder();
                        strWhere.Append(string.Format(" ReadingRoomNo = '{0}'", roomNum[k]));
                        strWhere.Append(" and  BespeakTime>='" + date.Date + "' and BespeakTime<'" + date.AddDays(1).Date + "' and BespeakState='" + (int)BookingStatus.Waiting + "'");
                        DataSet ds = seatBespeakDal.GetList(strWhere.ToString(), null);
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            BespeakLogInfo log = DataRowToBespeakLogInfo(ds.Tables[0].Rows[i]);
                            if (log != null)
                            {
                                list.Add(log);
                            }
                        }
                    }
                }
                else
                {
                    StringBuilder strWhere = new StringBuilder();
                    strWhere.Append(" BespeakTime>='" + date.Date + "' and BespeakTime<'" + date.AddDays(1).Date + "' and BespeakState='" + (int)BookingStatus.Waiting + "'");
                    DataSet ds = seatBespeakDal.GetList(strWhere.ToString(), null);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        BespeakLogInfo log = DataRowToBespeakLogInfo(ds.Tables[0].Rows[i]);
                        if (log != null)
                        {
                            list.Add(log);
                        }
                    }
                }

            }
            catch
            {
                throw;
            }

            return list;
        }
        /// <summary>
        /// 获取预约记录数目
        /// </summary>
        /// <param name="roomNum"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public Dictionary<string, int> GetBookingCount(List<string> roomNum, DateTime date)
        {
            Dictionary<string, int> bookCount = new Dictionary<string, int>();
            try
            {
                if (roomNum != null || roomNum.Count > 0)
                {
                    for (int k = 0; k < roomNum.Count; k++)
                    {
                        StringBuilder strWhere = new StringBuilder();
                        strWhere.Append(string.Format(" ReadingRoomNo = '{0}'", roomNum[k]));
                        strWhere.Append(" and  BespeakTime>='" + date.Date + "' and BespeakTime<'" + date.AddDays(1).Date + "' and BespeakState='" + (int)BookingStatus.Waiting + "'");
                        DataSet ds = seatBespeakDal.GetCount(strWhere.ToString(), null);
                        bookCount.Add(roomNum[k], ds.Tables[0].Rows.Count > 0 ? int.Parse(ds.Tables[0].Rows[0][0].ToString()) : 0);
                    }
                }
            }
            catch
            {
                throw;
            }
            return bookCount;
        }


        #endregion

        #region 私有方法
        //dataset里的行转换成对应的实体。
        private BespeakLogInfo DataRowToBespeakLogInfo(DataRow dr)
        {
            try
            {
                BespeakLogInfo bookLogInfo = new BespeakLogInfo();
                bookLogInfo.BsepeaklogID = dr["BespeakID"].ToString();
                bookLogInfo.ReadingRoomNo = dr["ReadingRoomNo"].ToString();
                bookLogInfo.SeatNo = dr["SeatNo"].ToString();
                bookLogInfo.BsepeakState = (BookingStatus)int.Parse(dr["BespeakState"].ToString());
                bookLogInfo.BsepeakTime = Convert.ToDateTime(dr["BespeakTime"]);
                bookLogInfo.CardNo = dr["CardNo"].ToString();
                if (!string.IsNullOrEmpty(dr["CancelTime"].ToString()))
                {
                    bookLogInfo.CancelTime = Convert.ToDateTime(dr["CancelTime"]);
                }
                bookLogInfo.SubmitTime = Convert.ToDateTime(dr["SubmitTime"]);
                if (!string.IsNullOrEmpty(dr["BespeakCancelPerson"].ToString()))
                {
                    bookLogInfo.CancelPerson = (Operation)int.Parse(dr["BespeakCancelPerson"].ToString());
                }

                ReadingRoomSetting roomSet = new ReadingRoomSetting(dr["ReadingSetting"].ToString());

                bookLogInfo.ReadingRoomName = dr["ReadingRoomName"].ToString();
                bookLogInfo.ReaderName = dr["ReaderName"].ToString();
                if (dr["ReaderTypeName"] != null)
                {
                    bookLogInfo.TypeName = dr["ReaderTypeName"].ToString();
                }
                if (dr["ReaderDeptName"] != null)
                {
                    bookLogInfo.DeptName = dr["ReaderDeptName"].ToString();
                }
                if (dr["Sex"] != null)
                {
                    bookLogInfo.Sex = dr["Sex"].ToString();
                }

                bookLogInfo.FlagKey = dr["BespeakFlag"].ToString();
                if (bookLogInfo.BsepeakState == BookingStatus.Waiting)
                {
                    DateTime bespeakDate = bookLogInfo.BsepeakTime;
                    if (bookLogInfo.BsepeakTime == bookLogInfo.SubmitTime)
                    {
                        bookLogInfo.Remark = string.Format("请在{0}至{1}之间到图书馆刷卡确认。", bespeakDate.ToShortTimeString(), bespeakDate.AddMinutes(roomSet.SeatBespeak.SeatKeepTime).ToShortTimeString());

                    }
                    else
                    {
                        bookLogInfo.Remark = string.Format("请在{0}至{1}之间到图书馆刷卡确认。", bespeakDate.AddMinutes(-int.Parse(roomSet.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString(), bespeakDate.AddMinutes(int.Parse(roomSet.SeatBespeak.ConfirmTime.EndTime)).ToShortTimeString());
                    }
                }
                else
                {
                    bookLogInfo.Remark = dr["remark"].ToString();
                }

                //if (GetBespeakMD5Key(bookLogInfo) != bookLogInfo.FlagKey)
                //{
                //    return null;
                //}
                bookLogInfo.ShortSeatNum = bookLogInfo.SeatNo.Substring(bookLogInfo.SeatNo.Length - roomSet.SeatNumAmount, roomSet.SeatNumAmount);
                return bookLogInfo;
            }
            catch
            {
                return null;
            }
        }
        private BespeakLogInfo DataRowToBespeakLogInfoNotCheck(DataRow dr)
        {
            try
            {
                BespeakLogInfo bookLogInfo = new BespeakLogInfo();
                bookLogInfo.BsepeaklogID = dr["BespeakID"].ToString();
                bookLogInfo.ReadingRoomNo = dr["ReadingRoomNo"].ToString();
                bookLogInfo.SeatNo = dr["SeatNo"].ToString();
                bookLogInfo.BsepeakState = (BookingStatus)int.Parse(dr["BespeakState"].ToString());
                bookLogInfo.BsepeakTime = Convert.ToDateTime(dr["BespeakTime"]);
                bookLogInfo.CardNo = dr["CardNo"].ToString();
                if (!string.IsNullOrEmpty(dr["CancelTime"].ToString()))
                {
                    bookLogInfo.CancelTime = Convert.ToDateTime(dr["CancelTime"]);
                }
                bookLogInfo.SubmitTime = Convert.ToDateTime(dr["SubmitTime"]);
                if (!string.IsNullOrEmpty(dr["BespeakCancelPerson"].ToString()))
                {
                    bookLogInfo.CancelPerson = (Operation)int.Parse(dr["BespeakCancelPerson"].ToString());
                }

                ReadingRoomSetting roomSet = new ReadingRoomSetting(dr["ReadingSetting"].ToString());

                bookLogInfo.ReadingRoomName = dr["ReadingRoomName"].ToString();
                bookLogInfo.ReaderName = dr["ReaderName"].ToString();
                if (dr["ReaderTypeName"] != null)
                {
                    bookLogInfo.TypeName = dr["ReaderTypeName"].ToString();
                }
                if (dr["ReaderDeptName"] != null)
                {
                    bookLogInfo.DeptName = dr["ReaderDeptName"].ToString();
                }
                if (dr["Sex"] != null)
                {
                    bookLogInfo.Sex = dr["Sex"].ToString();
                }

                bookLogInfo.FlagKey = dr["BespeakFlag"].ToString();
                //if (bookLogInfo.BsepeakState == BookingStatus.Waiting)
                //{
                //    DateTime bespeakDate = bookLogInfo.BsepeakTime;
                //    bookLogInfo.Remark = string.Format("请在{0}至{1}之间到图书馆刷卡确认。", bespeakDate.AddMinutes(-int.Parse(roomSet.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString(), bespeakDate.AddMinutes(int.Parse(roomSet.SeatBespeak.ConfirmTime.EndTime)).ToShortTimeString());

                //}
                //else
                //{
                bookLogInfo.Remark = dr["remark"].ToString();
                //}

                //if (GetBespeakMD5Key(bookLogInfo) != bookLogInfo.FlagKey)
                //{
                //    return null;
                //}
                bookLogInfo.ShortSeatNum = bookLogInfo.SeatNo.Substring(bookLogInfo.SeatNo.Length - roomSet.SeatNumAmount, roomSet.SeatNumAmount);
                return bookLogInfo;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// MD5 32位加密 小写
        /// </summary>
        /// <param name="pwd">明文</param>
        /// <returns>pwdMD5密文</returns>
        private static string GetBespeakMD5Key(BespeakLogInfo bespeakInfo)
        {
            List<string> keylist = new List<string>();
            keylist.Add(bespeakInfo.BsepeakTime.ToString());
            keylist.Add(bespeakInfo.SubmitTime.ToString());
            keylist.Add(bespeakInfo.SeatNo);
            keylist.Add(bespeakInfo.CardNo);
            keylist.Add("Bespeak");
            return SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32WithListKey(keylist);
        }
        #endregion




    }
}
