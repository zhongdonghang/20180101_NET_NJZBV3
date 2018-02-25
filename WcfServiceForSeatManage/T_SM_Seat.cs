using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.DAL;
using System.Data;
using SeatManage.ClassModel;
using SeatManage.EnumType;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        T_SM_Seat t_sm_seat = new T_SM_Seat();
        /// <summary>
        /// 座位加锁
        /// </summary>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        public SeatManage.EnumType.SeatLockState SeatLocked(string seatNo)
        {
            return
                t_sm_seat.SeatLocked(seatNo, GetServerDateTime());
        }
        /// <summary>
        /// 座位解锁
        /// </summary>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        public SeatManage.EnumType.SeatLockState SeatUnLocked(string seatNo)
        {
            return t_sm_seat.SeatUnLocked(seatNo);
        }
        /// <summary>
        /// 根据阅览室获取座位，不填默认不做条件判断
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <param name="lockstate">锁定状态</param>
        /// <param name="state">座位状态</param>
        /// <returns></returns>
        public List<Seat> GetSeatListByReadingRoom(string roomNum, bool lockstate)
        {
            StringBuilder strWhere = new StringBuilder();

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
            if (String.IsNullOrEmpty(strWhere.ToString()))
            {
                if (lockstate)
                {
                    strWhere.Append(" IsLock='1'");
                }
                else
                {
                    strWhere.Append(" IsLock='0'");
                }
            }
            else
            {
                if (lockstate)
                {
                    strWhere.Append(" and IsLock='1'");
                }
                else
                {
                    strWhere.Append(" and IsLock='0'");
                }
            }
            DataSet ds = t_sm_seat.GetList(strWhere.ToString(), null);
            List<Seat> ls = new List<Seat>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ls.Add(DataRowToSeatModel(dr));
                }
            }
            return ls;
        }

        /// <summary>
        /// 根据座位号获取座位信息
        /// </summary>
        /// <param name="seatNum"></param>
        /// <returns></returns>
        public SeatManage.ClassModel.Seat GetSeatInfoBySeatNum(string seatNum)
        {

            string strWhere = string.Format(" SeatNo='{0}' ", seatNum);
            DataSet ds = t_sm_seat.GetList(strWhere.ToString(), null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                SeatManage.ClassModel.Seat seat = DataRowToSeatModel(ds.Tables[0].Rows[0]);
                if (!string.IsNullOrEmpty(seat.ReadingRoomNum))
                {
                    List<SeatManage.ClassModel.ReadingRoomInfo> room = GetReadingRoomInfo(new List<string>() { seat.ReadingRoomNum });
                    if (room.Count > 0 && room[0].SeatList.Seats.ContainsKey(seat.SeatNo))
                    {
                        seat.IsSuspended = room[0].SeatList.Seats[seat.SeatNo].IsSuspended;
                        seat.HavePower = room[0].SeatList.Seats[seat.SeatNo].HavePower;
                        seat.ReadingRoom = room[0];
                    }
                }
                SeatManage.ClassModel.EnterOutLogInfo enteroutlog = GetEnterOutLogInfoBySeatNum(seatNum);
                if (enteroutlog != null)
                {
                    seat.SeatUsedState = enteroutlog.EnterOutState;
                    seat.UserCardNo = enteroutlog.CardNo;
                    seat.UserName = enteroutlog.ReaderName;
                    seat.ShortSeatNo = enteroutlog.ShortSeatNo;
                    seat.BeginUsedTime = enteroutlog.EnterOutTime;
                }
                else
                {
                    seat.SeatUsedState = EnterOutLogType.Leave;
                    seat.UserCardNo = "";
                    seat.UserName = "";
                    seat.ShortSeatNo = SeatManage.SeatManageComm.SeatComm.SeatNoToShortSeatNo(seat.ReadingRoom.Setting.SeatNumAmount, seat.SeatNo);
                }

                return seat;
            }
            else
            {
                return null;
            }
        }
        #region 私有方法
        private int GetSeatAmountAll(string roomNum)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(string.Format(" readingRoomNo ='{0}'", roomNum));
            DataSet ds = t_sm_seat.GetList(strWhere.ToString(), null);
            return ds.Tables[0].Rows.Count;
        }

        public List<Seat> GetOftenUsedSeatByCardNo(string cardNo, int days, List<string> roomNums)
        {
            List<ReadingRoomInfo> rooms = GetReadingRoomInfo(roomNums);
            List<string> noList = rooms.FindAll(room => room.Setting.ReadingRoomOpenState(DateTime.Now) == ReadingRoomStatus.Open).Select(room => room.No).ToList();
            DataSet ds = t_sm_seat.ReaderUsedSeat(cardNo, days, noList, GetServerDateTime());
            List<Seat> seats = new List<Seat>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Seat s = new Seat();
                s.SeatNo = ds.Tables[0].Rows[i]["SeatNo"].ToString();
                s.ReadingRoomNum = ds.Tables[0].Rows[i]["ReadingRoomNo"].ToString();
                //bool isstopUse = false;
                ReadingRoomInfo r = rooms.Find(u => u.No == s.ReadingRoomNum && u.SeatList.Seats.ContainsKey(s.SeatNo) && !u.SeatList.Seats[s.SeatNo].IsSuspended && u.Setting.ReadingRoomOpenState(DateTime.Now) == ReadingRoomStatus.Open);
                if (r == null)
                {
                    continue;
                }
                else
                {
                    s.ReadingRoom = r;
                }

                //foreach (ReadingRoomInfo r in rooms)
                //{
                //    if (r.No == s.ReadingRoomNum && r.SeatList.Seats.ContainsKey(s.SeatNo) && r.SeatList.Seats[s.SeatNo].IsSuspended)
                //    {
                //        s.ReadingRoom = r;
                //        isstopUse = true;
                //        break;
                //    }
                //}
                //if (isstopUse)
                //{
                //    continue;
                //}
                EnterOutLogInfo enterOutLog = GetEnterOutLogInfoBySeatNum(s.SeatNo);
                if (enterOutLog == null || enterOutLog.EnterOutState == SeatManage.EnumType.EnterOutLogType.Leave)
                {
                    seats.Add(s);
                }
                if (seats.Count >= 12)
                {
                    break;
                }
            }
            return seats;
        }
        /// <summary>
        /// 随机分配座位编号
        /// </summary>
        /// <param name="reandingRoom">阅览室编号</param>
        /// <returns></returns>
        public string RandomAllotSeat(string readingRoomNum)
        {
            SeatLayout seatLayout = GetRoomSeatLayOut(readingRoomNum);
            List<string> seatList = (from seat in seatLayout.Seats.Values where !seat.IsSuspended && seat.SeatUsedState == EnterOutLogType.Leave select seat.SeatNo).ToList();
            if (seatList.Count > 0)
            {
                Random rNum = new Random();
                return seatList[rNum.Next(0, seatList.Count - 1)];
            }
            else
            {
                return "";
            }

            //DataSet ds = t_sm_seat.RandomAllotSeat(readingRoomNum, GetServerDateTime());
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    return ds.Tables[0].Rows[0]["SeatNo"].ToString();
            //}
            //else
            //{
            //    return "";
            //}
        }
        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private SeatManage.ClassModel.Seat DataRowToSeatModel(DataRow dr)
        {
            SeatManage.ClassModel.Seat s = new Seat();

            s.SeatNo = dr["SeatNo"].ToString();
            s.ReadingRoomNum = dr["ReadingRoomNo"].ToString();
            s.IsLocked = bool.Parse(dr["IsUsing"].ToString());
            if (!string.IsNullOrEmpty(dr["LockTime"].ToString()))
            {
                s.LockedTime = DateTime.Parse(dr["LockTime"].ToString());
            }
            return s;
        }
        #endregion
    }
}
