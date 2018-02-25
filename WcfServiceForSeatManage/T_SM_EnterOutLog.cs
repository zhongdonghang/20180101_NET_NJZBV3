using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.ClassModel;
using System.Data.SqlClient;
using System.Data;
using SeatManage.DAL;
using SeatManage.EnumType;
using System.Threading;
using System.Configuration;
using SeatManage.SeatManageComm;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        T_SM_EnterOutLog t_sm_enterOutLog_Dal = new T_SM_EnterOutLog();
        static ServerIp ipEndPoint = new ServerIp(AESAlgorithm.AESDecrypt(ConfigurationManager.ConnectionStrings["AppServiceEndpointAddress"].ConnectionString));

        #region 进出记录相关操作
        /// <summary>
        /// 获取当前有效的进出记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public EnterOutLogInfo GetEnterOutLogInfoByCardNo(string cardNo)
        {
            if (string.IsNullOrEmpty(cardNo))
            {
                return null;
            }
            SeatManage.ClassModel.EnterOutLogInfo enterOutInfo = null;//创建一个进出记录实体
            string strWhere = " [CardNo]=@cardNo and [EnterOutType]=1";
            SqlParameter[] parameters =  { 
                                             new SqlParameter("@cardNo",cardNo)
                                         };
            try
            {
                DataSet dsEnterOutLog = t_sm_enterOutLog_Dal.GetList(strWhere, parameters);//获取读者的信息
                if (dsEnterOutLog.Tables[0].Rows.Count > 0)
                {
                    enterOutInfo = DataRowToEnterOutLogInfo(dsEnterOutLog.Tables[0].Rows[0]);
                }

            }
            catch (Exception ex)
            {
                //TODO:记录错误日志
                return
                    null;
            }
            return enterOutInfo;
        }
        /// <summary>
        /// 根据记录ID查询记录
        /// </summary>
        /// <param name="enterOutLogID"></param>
        /// <returns></returns>
        public EnterOutLogInfo GetEnterOutLogInfoById(int enterOutLogID)
        {
            SeatManage.ClassModel.EnterOutLogInfo enterOutInfo = null;//创建一个进出记录实体
            string strWhere = " [EnterOutLogID]=@EnterOutLogID ";
            SqlParameter[] parameters =  { 
                                             new SqlParameter("@EnterOutLogID",enterOutLogID)
                                         };
            try
            {
                DataSet dsEnterOutLog = t_sm_enterOutLog_Dal.GetList(strWhere, parameters);//获取读者的信息
                if (dsEnterOutLog.Tables[0].Rows.Count > 0)
                {
                    enterOutInfo = DataRowToEnterOutLogInfo(dsEnterOutLog.Tables[0].Rows[0]);
                }

            }
            catch (Exception ex)
            {
                //TODO:记录错误日志
                return
                    null;
            }
            return enterOutInfo;
        }
        /// <summary>
        /// 根据座位号查询有效进出记录
        /// </summary>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        public EnterOutLogInfo GetEnterOutLogInfoBySeatNum(string seatNo)
        {
            string strWhere = string.Format(" [SeatNo]=@SeatNo and [EnterOutType]=1 and EnterOutState>0", (int)EnterOutLogType.Leave);
            SqlParameter[] parameters =  { 
                                             new SqlParameter("@SeatNo",seatNo)
                                         };
            SeatManage.ClassModel.EnterOutLogInfo enterOutInfo = null;//创建一个进出记录实体
            DataSet dsEnterOutLog = t_sm_enterOutLog_Dal.GetList(strWhere, parameters);//获取读者的信息
            if (dsEnterOutLog.Tables[0].Rows.Count > 0)
            {
                enterOutInfo = DataRowToEnterOutLogInfo(dsEnterOutLog.Tables[0].Rows[0]);
            }
            return enterOutInfo;
        }
        public EnterOutLogInfo GetEnterOutLogInfoWithBookWaitBySeatNum(string seatNo)
        {
            string strWhere = string.Format(" [SeatNo]=@SeatNo and [EnterOutType]=1 and EnterOutState>0", (int)EnterOutLogType.Leave);
            SqlParameter[] parameters =  { 
                                             new SqlParameter("@SeatNo",seatNo)
                                         };
            SeatManage.ClassModel.EnterOutLogInfo enterOutInfo = new EnterOutLogInfo();//创建一个进出记录实体
            DataSet dsEnterOutLog = t_sm_enterOutLog_Dal.GetList(strWhere, parameters);//获取读者的信息
            if (dsEnterOutLog.Tables[0].Rows.Count > 0)
            {
                enterOutInfo = DataRowToEnterOutLogInfo(dsEnterOutLog.Tables[0].Rows[0]);
            }
            List<SeatManage.ClassModel.BespeakLogInfo> bespeaklist = GetBespeakLogInfoBySeatNo(seatNo, DateTime.Now.Date);
            if (bespeaklist.Count > 0)
            {
                foreach (BespeakLogInfo beslog in bespeaklist)
                {
                    if (beslog.BsepeakState == SeatManage.EnumType.BookingStatus.Waiting)
                    {
                        enterOutInfo.EnterOutState = EnterOutLogType.BespeakWaiting;
                        break;
                    }
                }
                return enterOutInfo;
            }
            if (enterOutInfo.EnterOutLogNo != null)
            {
                List<SeatManage.ClassModel.WaitSeatLogInfo> waitlist = GetWaitLogList(null, enterOutInfo.EnterOutLogID, null, null, new List<EnterOutLogType>() { SeatManage.EnumType.EnterOutLogType.Waiting });
                if (waitlist.Count > 0)
                {
                    enterOutInfo.EnterOutState = EnterOutLogType.Waiting;
                    return enterOutInfo;
                }
            }
            return enterOutInfo;
        }

        /// <summary>
        /// 分页查询进出记录，用于android客户端上拉刷新
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<EnterOutLogInfo> GetEnterOutLogsByPage(string cardNo, int pageIndex, int pageSize)
        {
            List<EnterOutLogInfo> logs = new List<EnterOutLogInfo>();
            try
            {
                DataSet ds = t_sm_enterOutLog_Dal.GetList(pageSize, pageIndex, cardNo);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    EnterOutLogInfo model = DataRowToEnterOutLogInfo(ds.Tables[0].Rows[i]);
                    logs.Add(model);
                }
                return logs;

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取进出记录
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="roomNum">阅览室号</param>
        /// <param name="seatNo">座位号</param>
        /// <param name="beginDate">查询的开始时间</param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<EnterOutLogInfo> GetEnterOutLogs(string cardNo, string roomNum, string seatNo, string beginDate, string endDate)
        {
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
            if (!string.IsNullOrEmpty(seatNo))
            {
                if (String.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" seatNo='{0}'", seatNo));
                }
                else
                {
                    strWhere.Append(string.Format(" and seatNo='{0}'", seatNo));
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

            if (!string.IsNullOrEmpty(beginDate))
            {
                if (!string.IsNullOrEmpty(endDate))
                {
                    if (String.IsNullOrEmpty(strWhere.ToString()))
                    {
                        strWhere.Append(string.Format(" EnterOutTime between '{0}' and '{1}'", beginDate, endDate));
                    }
                    else
                    {
                        strWhere.Append(string.Format(" and EnterOutTime between '{0}' and '{1}'", beginDate, endDate));
                    }
                }

                else
                {
                    if (String.IsNullOrEmpty(strWhere.ToString()))
                    {
                        strWhere.Append(string.Format(" EnterOutTime between '{0}' and '{1}'", beginDate, GetServerDateTime().ToString()));
                    }
                    else
                    {
                        strWhere.Append(string.Format(" and EnterOutTime between '{0}' and '{1}'", beginDate, GetServerDateTime().ToString()));
                    }
                }
            }
            List<EnterOutLogInfo> list = new List<EnterOutLogInfo>();
            try
            {
                DataSet ds = t_sm_enterOutLog_Dal.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    EnterOutLogInfo enterOutLog = DataRowToEnterOutLogInfo(ds.Tables[0].Rows[i]); ;
                    list.Add(enterOutLog);
                }
                return list;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 按学号模糊查询获取进出记录
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="roomNum">阅览室号</param>
        /// <param name="seatNo">座位号</param>
        /// <param name="beginDate">查询的开始时间</param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<EnterOutLogInfo> GetEnterOutLogs_ByFuzzySearch(string cardNo, string roomNum, string seatNo, string beginDate, string endDate)
        {
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
            if (!string.IsNullOrEmpty(seatNo))
            {
                if (String.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" seatNo='{0}'", seatNo));
                }
                else
                {
                    strWhere.Append(string.Format(" and seatNo='{0}'", seatNo));
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

            if (!string.IsNullOrEmpty(beginDate))
            {
                if (!string.IsNullOrEmpty(endDate))
                {
                    if (String.IsNullOrEmpty(strWhere.ToString()))
                    {
                        strWhere.Append(string.Format(" EnterOutTime between '{0}' and '{1}'", beginDate, endDate));
                    }
                    else
                    {
                        strWhere.Append(string.Format(" and EnterOutTime between '{0}' and '{1}'", beginDate, endDate));
                    }
                }

                else
                {
                    if (String.IsNullOrEmpty(strWhere.ToString()))
                    {
                        strWhere.Append(string.Format(" EnterOutTime between '{0}' and '{1}'", beginDate, GetServerDateTime().ToString()));
                    }
                    else
                    {
                        strWhere.Append(string.Format(" and EnterOutTime between '{0}' and '{1}'", beginDate, GetServerDateTime().ToString()));
                    }
                }
            }
            List<EnterOutLogInfo> list = new List<EnterOutLogInfo>();
            try
            {
                DataSet ds = t_sm_enterOutLog_Dal.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    EnterOutLogInfo enterOutLog = DataRowToEnterOutLogInfo(ds.Tables[0].Rows[i]); ;
                    list.Add(enterOutLog);
                }
                return list;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 根据记录状态和类型获取进出记录
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="roomNum">阅览室编号</param>
        /// <param name="seatNo">座位号</param>
        /// <param name="logType">记录类型（多选）</param>
        /// <param name="logStatus">记录状态</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public List<EnterOutLogInfo> GetEnterOutLogsByStatus(string cardNo, string roomNum, string seatNo, List<EnterOutLogType> logType, LogStatus logStatus, string beginDate, string endDate)
        {
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
            if (logType != null)
            {
                for (int i = 0; i < logType.Count; i++)
                {
                    if (i == 0)
                    {
                        if (String.IsNullOrEmpty(strWhere.ToString()))
                        {
                            strWhere.Append(string.Format(" EnterOutState in ('{0}'", (int)logType[i]));
                        }
                        else
                        {
                            strWhere.Append(string.Format(" and EnterOutState in ('{0}'", (int)logType[i]));
                        }
                    }
                    else if (i != logType.Count - 1)
                    {
                        strWhere.Append(string.Format(",'{0}' ", (int)logType[i]));
                    }
                    if (i == logType.Count - 1)
                    {
                        strWhere.Append(string.Format(",'{0}' )", (int)logType[i]));
                    }
                }
            }
            if (logStatus != LogStatus.None)
            {
                if (String.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" EnterOutType='{0}'", (int)logStatus));
                }
                else
                {
                    strWhere.Append(string.Format(" and EnterOutType='{0}'", (int)logStatus));
                }
            }
            if (!string.IsNullOrEmpty(beginDate))
            {
                if (!string.IsNullOrEmpty(endDate))
                {
                    if (String.IsNullOrEmpty(strWhere.ToString()))
                    {
                        strWhere.Append(string.Format(" EnterOutTime between '{0}' and '{1}'", beginDate, endDate));
                    }
                    else
                    {
                        strWhere.Append(string.Format(" and EnterOutTime between '{0}' and '{1}'", beginDate, endDate));
                    }
                }

                else
                {
                    if (String.IsNullOrEmpty(strWhere.ToString()))
                    {
                        strWhere.Append(string.Format(" EnterOutTime between '{0}' and '{1}'", beginDate, GetServerDateTime().ToString()));
                    }
                    else
                    {
                        strWhere.Append(string.Format(" and EnterOutTime between '{0}' and '{1}'", beginDate, GetServerDateTime().ToString()));
                    }
                }
            }
            List<EnterOutLogInfo> list = new List<EnterOutLogInfo>();
            try
            {
                DataSet ds = t_sm_enterOutLog_Dal.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    EnterOutLogInfo enterOutLog = DataRowToEnterOutLogInfo(ds.Tables[0].Rows[i]); ;
                    list.Add(enterOutLog);
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        public HandleResult AddEnterOutLogInfo(EnterOutLogInfo model, ref int newLogId)
        {
            //TODO:不跟据阅览室状态添加进出记录
            model.EnterOutTime = DateTime.Now;
            int result = t_sm_enterOutLog_Dal.Add(model, ref newLogId);
            if (result == 1)
            {
                return HandleResult.Failed;
            }
            else
            {
                EnterOutLogInfo eol = GetEnterOutLogInfoById(newLogId);
                if (eol == null)
                {
                    return HandleResult.Successed;
                }
                PushMsgInfo msg = new PushMsgInfo();
                msg.MsgType = MsgPushType.UserOperation;
                switch (eol.EnterOutState)
                {
                    case EnterOutLogType.BookingConfirmation:
                        msg.Title = "您好，您已签到成功";
                        break;
                    case EnterOutLogType.ComeBack:
                        msg.Title = "您好，您已{0}恢复在座";
                        break;
                    case EnterOutLogType.ContinuedTime:
                        msg.Title = "您好，您续时成功";
                        break;
                    case EnterOutLogType.Leave:
                        msg.Title = "您好，您的座位已{0}释放";
                        break;
                    case EnterOutLogType.SelectSeat:
                        msg.Title = "您好，您已{0}入座";
                        break;
                    case EnterOutLogType.ShortLeave:
                        msg.Title = "您好，您已{0}暂离";
                        break;
                }
                switch (eol.Flag)
                {
                    case Operation.Reader:
                        msg.Title = string.Format(msg.Title, "");
                        break;
                    case Operation.OtherReader:
                        msg.Title = string.Format(msg.Title, "其它用户");
                        msg.MsgType = MsgPushType.OtherUser;
                        break;
                    case Operation.Admin:
                        msg.Title = string.Format(msg.Title, "管理员");
                        msg.MsgType = MsgPushType.AdminOperation;
                        break;
                    case Operation.Service:
                        msg.Title = string.Format(msg.Title, "系统");
                        msg.MsgType = MsgPushType.TimeOut;
                        break;

                }
                msg.RoomName = eol.ReadingRoomName;
                msg.SeatNum = eol.ShortSeatNo;

                msg.StudentNum = eol.CardNo;
                msg.Message = eol.Remark;
                SendMsg(msg);
                return HandleResult.Successed;
            }
        }
        /// <summary>
        /// 根据进出记录编号查找对应的进出记录
        /// </summary>
        /// <param name="EnterOutNo">进出记录编号</param>
        /// <param name="logType">记录类型</param>
        /// <param name="top">最后数目</param>
        /// <returns></returns>
        public List<EnterOutLogInfo> GetEnterOutLogsByNo(string EnterOutNo, List<EnterOutLogType> logType, int top)
        {
            List<EnterOutLogInfo> list = new List<EnterOutLogInfo>();

            try
            {
                if (logType != null)
                {
                    for (int i = 0; i < logType.Count; i++)
                    {
                        StringBuilder strWhere = new StringBuilder();
                        if (!string.IsNullOrEmpty(EnterOutNo))
                        {
                            if (String.IsNullOrEmpty(strWhere.ToString()))
                            {
                                strWhere.Append(string.Format(" EnterOutLogNo='{0}'", EnterOutNo));
                            }
                            else
                            {
                                strWhere.Append(string.Format(" and EnterOutLogNo='{0}'", EnterOutNo));
                            }
                        }
                        if (String.IsNullOrEmpty(strWhere.ToString()))
                        {
                            strWhere.Append(string.Format(" EnterOutState ='{0}'", (int)logType[i]));
                        }
                        else
                        {
                            strWhere.Append(string.Format(" and EnterOutState ='{0}'", (int)logType[i]));
                        }
                        DataSet ds = new DataSet();
                        if (string.IsNullOrEmpty(top.ToString()))
                        {
                            ds = t_sm_enterOutLog_Dal.GetList(strWhere.ToString(), null);
                        }
                        else
                        {
                            ds = t_sm_enterOutLog_Dal.GetList(top, strWhere.ToString(), "EnterOutTime desc", null);
                        }
                        for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                        {
                            EnterOutLogInfo enterOutLog = DataRowToEnterOutLogInfo(ds.Tables[0].Rows[k]); ;
                            list.Add(enterOutLog);
                        }

                    }
                }
                else
                {
                    StringBuilder strWhere = new StringBuilder();
                    if (!string.IsNullOrEmpty(EnterOutNo))
                    {
                        if (String.IsNullOrEmpty(strWhere.ToString()))
                        {
                            strWhere.Append(string.Format(" EnterOutLogNo='{0}'", EnterOutNo));
                        }
                        else
                        {
                            strWhere.Append(string.Format(" and EnterOutLogNo='{0}'", EnterOutNo));
                        }
                    }
                    DataSet ds = new DataSet();
                    if (string.IsNullOrEmpty(top.ToString()))
                    {
                        ds = t_sm_enterOutLog_Dal.GetList(strWhere.ToString(), null);
                    }
                    else
                    {
                        ds = t_sm_enterOutLog_Dal.GetList(top, strWhere.ToString(), "EnterOutTime desc", null);
                    }
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        EnterOutLogInfo enterOutLog = DataRowToEnterOutLogInfo(ds.Tables[0].Rows[0]); ;
                        list.Add(enterOutLog);
                    }
                }

            }
            catch
            {
                throw;
            }
            return list;

        }
        public int DelEnterOutLogInfo(string cardNo, string roomNum, string beginDate, string endDate)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取读者续时的次数
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        public int GetContinuedTimeCount(string CardNo)
        {
            EnterOutLogInfo enterOutLog = GetEnterOutLogInfoByCardNo(CardNo);
            if (enterOutLog == null)
            {
                return 0;
            }
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(string.Format("  EnterOutState='{0}' and [EnterOutLogNo]='{1}'", (int)EnterOutLogType.ContinuedTime, enterOutLog.EnterOutLogNo));
            try
            {
                DataSet ds = new DataSet();
                ds = t_sm_enterOutLog_Dal.GetList(strWhere.ToString(), null);
                return ds.Tables[0].Rows.Count;
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// 根据进出记录编号获取续时次数
        /// </summary>
        /// <param name="enterOutLogNo"></param>
        /// <returns></returns>
        public int GetContinuedTimeCountByEOLNo(string enterOutLogNo)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(string.Format("  EnterOutState='{0}' and [EnterOutLogNo]='{1}'", (int)EnterOutLogType.ContinuedTime, enterOutLogNo));
            try
            {
                DataSet ds = new DataSet();
                ds = t_sm_enterOutLog_Dal.GetList(strWhere.ToString(), null);
                return ds.Tables[0].Rows.Count;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取读者可以续时的时间
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        public DateTime GetCanContinuedTime(string CardNo)
        {
            EnterOutLogInfo enterOutLog = GetEnterOutLogInfoByCardNo(CardNo);
            DateTime time = new DateTime();
            if (enterOutLog == null)
            {
                return time;
            }
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(string.Format(" [EnterOutLogNo]='{3}' and  EnterOutState<>'{0}' and EnterOutState<>'{1}' and EnterOutState<>'{2}' ",
                (int)EnterOutLogType.ShortLeave, (int)EnterOutLogType.Leave, (int)EnterOutLogType.ComeBack, enterOutLog.EnterOutLogNo));
            try
            {
                DataSet ds = new DataSet();
                ds = t_sm_enterOutLog_Dal.GetList(1, strWhere.ToString(), " EnterOutTime desc", null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<string> roomno = new List<string>();
                    roomno.Add(ds.Tables[0].Rows[0]["ReadingRoomNo"].ToString());
                    List<ReadingRoomInfo> room = GetReadingRoomInfo(roomno);
                    if (room.Count > 0)
                    {
                        if (room[0].Setting.SeatUsedTimeLimit.Mode == "Free")
                        {
                            if (((EnterOutLogType)int.Parse(ds.Tables[0].Rows[0]["EnterOutState"].ToString())) == EnterOutLogType.ContinuedTime)
                            {
                                time = DateTime.Parse(ds.Tables[0].Rows[0]["EnterOutTime"].ToString()).AddMinutes(room[0].Setting.SeatUsedTimeLimit.DelayTimeLength - room[0].Setting.SeatUsedTimeLimit.CanDelayTime);
                            }
                            else
                            {
                                time = DateTime.Parse(ds.Tables[0].Rows[0]["EnterOutTime"].ToString()).AddMinutes(room[0].Setting.SeatUsedTimeLimit.UsedTimeLength - room[0].Setting.SeatUsedTimeLimit.CanDelayTime);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < room[0].Setting.SeatUsedTimeLimit.FixedTimes.Count; i++)
                            {
                                if (DateTime.Parse(ds.Tables[0].Rows[0]["EnterOutTime"].ToString()) < room[0].Setting.SeatUsedTimeLimit.FixedTimes[i] && DateTime.Parse(ds.Tables[0].Rows[0]["EnterOutTime"].ToString()) > room[0].Setting.SeatUsedTimeLimit.FixedTimes[i].AddMinutes(-room[0].Setting.SeatUsedTimeLimit.CanDelayTime))
                                {
                                    if (i + 1 < room[0].Setting.SeatUsedTimeLimit.FixedTimes.Count)
                                    {
                                        time = room[0].Setting.SeatUsedTimeLimit.FixedTimes[i + 1].AddMinutes(-room[0].Setting.SeatUsedTimeLimit.CanDelayTime);
                                    }
                                    else
                                    {
                                        time = DateTime.Parse(room[0].Setting.RoomOpenSet.DefaultOpenTime.EndTime);
                                    }
                                    break;
                                }
                                if (DateTime.Parse(ds.Tables[0].Rows[0]["EnterOutTime"].ToString()) < room[0].Setting.SeatUsedTimeLimit.FixedTimes[i])
                                {
                                    time = room[0].Setting.SeatUsedTimeLimit.FixedTimes[i].AddMinutes(-room[0].Setting.SeatUsedTimeLimit.CanDelayTime);
                                    break;
                                }
                                if (i + 1 == room[0].Setting.SeatUsedTimeLimit.FixedTimes.Count)
                                {
                                    time = DateTime.Parse(room[0].Setting.RoomOpenSet.DefaultOpenTime.EndTime);
                                    break;
                                }
                            }
                        }
                    }
                }
                return time;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 根据进出记录编号获取可续时的时间段
        /// </summary>
        /// <param name="enterOutLogNo"></param>
        /// <returns></returns>
        public DateTime GetCanContinuedTimeByEOLNo(string enterOutLogNo)
        {
            DateTime time = new DateTime();
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(string.Format("  [EnterOutLogNo]='{3}' and EnterOutState<>'{0}' and EnterOutState<>'{1}' and EnterOutState<>'{2}' ",
                (int)EnterOutLogType.ShortLeave, (int)EnterOutLogType.Leave, (int)EnterOutLogType.ComeBack, enterOutLogNo));
            try
            {
                DataSet ds = new DataSet();
                ds = t_sm_enterOutLog_Dal.GetList(1, strWhere.ToString(), " EnterOutTime desc", null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<string> roomno = new List<string>();
                    roomno.Add(ds.Tables[0].Rows[0]["ReadingRoomNo"].ToString());
                    List<ReadingRoomInfo> room = GetReadingRoomInfo(roomno);
                    if (room.Count > 0)
                    {
                        if (room[0].Setting.SeatUsedTimeLimit.Mode == "Free")
                        {
                            if (((EnterOutLogType)int.Parse(ds.Tables[0].Rows[0]["EnterOutState"].ToString())) == EnterOutLogType.ContinuedTime)
                            {
                                time = DateTime.Parse(ds.Tables[0].Rows[0]["EnterOutTime"].ToString()).AddMinutes(room[0].Setting.SeatUsedTimeLimit.DelayTimeLength - room[0].Setting.SeatUsedTimeLimit.CanDelayTime);
                            }
                            else
                            {
                                time = DateTime.Parse(ds.Tables[0].Rows[0]["EnterOutTime"].ToString()).AddMinutes(room[0].Setting.SeatUsedTimeLimit.UsedTimeLength - room[0].Setting.SeatUsedTimeLimit.CanDelayTime);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < room[0].Setting.SeatUsedTimeLimit.FixedTimes.Count; i++)
                            {
                                if (DateTime.Parse(ds.Tables[0].Rows[0]["EnterOutTime"].ToString()) < room[0].Setting.SeatUsedTimeLimit.FixedTimes[i] && DateTime.Parse(ds.Tables[0].Rows[0]["EnterOutTime"].ToString()) > room[0].Setting.SeatUsedTimeLimit.FixedTimes[i].AddMinutes(-room[0].Setting.SeatUsedTimeLimit.CanDelayTime))
                                {
                                    if (i + 1 < room[0].Setting.SeatUsedTimeLimit.FixedTimes.Count)
                                    {
                                        time = room[0].Setting.SeatUsedTimeLimit.FixedTimes[i + 1].AddMinutes(-room[0].Setting.SeatUsedTimeLimit.CanDelayTime);
                                    }
                                    else
                                    {
                                        time = DateTime.Parse(room[0].Setting.RoomOpenSet.DefaultOpenTime.EndTime);
                                    }
                                    break;
                                }
                                if (DateTime.Parse(ds.Tables[0].Rows[0]["EnterOutTime"].ToString()) < room[0].Setting.SeatUsedTimeLimit.FixedTimes[i])
                                {
                                    time = room[0].Setting.SeatUsedTimeLimit.FixedTimes[i].AddMinutes(-room[0].Setting.SeatUsedTimeLimit.CanDelayTime);
                                    break;
                                }
                                if (i + 1 == room[0].Setting.SeatUsedTimeLimit.FixedTimes.Count)
                                {
                                    if (room[0].Setting.RoomOpenSet.UninterruptibleModel)
                                    {
                                        time = room[0].Setting.SeatUsedTimeLimit.FixedTimes[0].AddMinutes(-room[0].Setting.SeatUsedTimeLimit.CanDelayTime).AddDays(1);
                                    }
                                    else
                                    {
                                        time = DateTime.Parse(room[0].Setting.RoomOpenSet.DefaultOpenTime.EndTime);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                return time;
            }
            catch
            {
                throw;
            }
        }
        private DateTime GetCanContinuedTimeByEOLNoRNo(string enterOutLogNo, ReadingRoomInfo room)
        {
            DateTime time = new DateTime();
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(string.Format(" [EnterOutLogNo]='{3}' and EnterOutState<>'{0}' and EnterOutState<>'{1}' and EnterOutState<>'{2}' ", (int)EnterOutLogType.ShortLeave, (int)EnterOutLogType.Leave, (int)EnterOutLogType.ComeBack, enterOutLogNo));
            try
            {
                DataSet ds = new DataSet();
                ds = t_sm_enterOutLog_Dal.GetList(1, strWhere.ToString(), " EnterOutTime desc", null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (room.Setting.SeatUsedTimeLimit.Mode == "Free")
                    {
                        if (((EnterOutLogType)int.Parse(ds.Tables[0].Rows[0]["EnterOutState"].ToString())) == EnterOutLogType.ContinuedTime)
                        {
                            time = DateTime.Parse(ds.Tables[0].Rows[0]["EnterOutTime"].ToString()).AddMinutes(room.Setting.SeatUsedTimeLimit.DelayTimeLength - room.Setting.SeatUsedTimeLimit.CanDelayTime);
                        }
                        else
                        {
                            time = DateTime.Parse(ds.Tables[0].Rows[0]["EnterOutTime"].ToString()).AddMinutes(room.Setting.SeatUsedTimeLimit.UsedTimeLength - room.Setting.SeatUsedTimeLimit.CanDelayTime);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < room.Setting.SeatUsedTimeLimit.FixedTimes.Count; i++)
                        {
                            if (DateTime.Parse(ds.Tables[0].Rows[0]["EnterOutTime"].ToString()) < room.Setting.SeatUsedTimeLimit.FixedTimes[i] && DateTime.Parse(ds.Tables[0].Rows[0]["EnterOutTime"].ToString()) > room.Setting.SeatUsedTimeLimit.FixedTimes[i].AddMinutes(-room.Setting.SeatUsedTimeLimit.CanDelayTime))
                            {
                                if (i + 1 < room.Setting.SeatUsedTimeLimit.FixedTimes.Count)
                                {
                                    time = room.Setting.SeatUsedTimeLimit.FixedTimes[i + 1].AddMinutes(-room.Setting.SeatUsedTimeLimit.CanDelayTime);
                                }
                                else
                                {
                                    time = DateTime.Parse(room.Setting.RoomOpenSet.DefaultOpenTime.EndTime);
                                }
                                break;
                            }
                            if (DateTime.Parse(ds.Tables[0].Rows[0]["EnterOutTime"].ToString()) < room.Setting.SeatUsedTimeLimit.FixedTimes[i])
                            {
                                time = room.Setting.SeatUsedTimeLimit.FixedTimes[i].AddMinutes(-room.Setting.SeatUsedTimeLimit.CanDelayTime);
                                break;
                            }
                            if (i + 1 == room.Setting.SeatUsedTimeLimit.FixedTimes.Count)
                            {
                                if (room.Setting.RoomOpenSet.UninterruptibleModel)
                                {
                                    time = room.Setting.SeatUsedTimeLimit.FixedTimes[0].AddMinutes(-room.Setting.SeatUsedTimeLimit.CanDelayTime).AddDays(1);
                                }
                                else
                                {
                                    time = DateTime.Parse(room.Setting.RoomOpenSet.DefaultOpenTime.EndTime);
                                    break;
                                }
                            }
                        }
                    }
                }
                return time;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取使用中的座位 
        /// </summary>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        public List<EnterOutLogInfo> GetRoomUsingSeatEnterOutLogInfo(List<string> roomNum)
        {
            List<EnterOutLogInfo> enterOutList = new List<EnterOutLogInfo>();
            if (roomNum != null)
            {
                for (int i = 0; i < roomNum.Count; i++)
                {
                    StringBuilder strWhere = new StringBuilder();
                    strWhere.Append(string.Format(" ReadingRoomNo ='{0}'", roomNum[i]));
                    strWhere.Append(string.Format(" and EnterOutState>0 and EnterOutType=1", (int)EnterOutLogType.Leave));
                    DataSet ds = t_sm_enterOutLog_Dal.GetList(strWhere.ToString(), null);
                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                    {
                        EnterOutLogInfo model = DataRowToEnterOutLogInfo(ds.Tables[0].Rows[i]);
                        enterOutList.Add(model);
                    }
                }
            }
            else
            {
                StringBuilder strWhere = new StringBuilder();
                strWhere.Append(string.Format(" EnterOutState>0 and EnterOutType=1", (int)EnterOutLogType.Leave));
                DataSet ds = t_sm_enterOutLog_Dal.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    EnterOutLogInfo model = DataRowToEnterOutLogInfo(ds.Tables[0].Rows[i]);
                    enterOutList.Add(model);
                }
            }

            return enterOutList;
        }
        /// <summary>
        /// 获取使用中的座位 
        /// </summary>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        public List<EnterOutLogInfo> GetRoomUsingSeatEnterOutLogInfoV2(List<string> roomNum)
        {
            List<EnterOutLogInfo> enterOutList = new List<EnterOutLogInfo>();
            StringBuilder strWhere = new StringBuilder();
            if (roomNum != null && roomNum.Count > 0)
            {
                foreach (string no in roomNum)
                {
                    strWhere = new StringBuilder();
                    strWhere.Append(string.Format(" ReadingRoomNo = '{0}'", no));
                    strWhere.Append(string.Format(" and EnterOutState>0 and EnterOutType=1", (int)EnterOutLogType.Leave));
                    DataSet ds = t_sm_enterOutLog_Dal.GetList(strWhere.ToString(), null);
                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                    {
                        EnterOutLogInfo model = DataRowToEnterOutLogInfo(ds.Tables[0].Rows[k]);
                        enterOutList.Add(model);
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" EnterOutState>0 and EnterOutType=1", (int)EnterOutLogType.Leave));
                }
                else
                {
                    strWhere.Append(string.Format(" and EnterOutState>0 and EnterOutType=1", (int)EnterOutLogType.Leave));
                }
                DataSet ds = t_sm_enterOutLog_Dal.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    EnterOutLogInfo model = DataRowToEnterOutLogInfo(ds.Tables[0].Rows[i]);
                    enterOutList.Add(model);
                }
            }
            return enterOutList;
        }

        /// <summary>
        /// 获取使用中的座位 
        /// </summary>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        public Dictionary<string, int> GetRoomUsingSeatCount(List<string> roomNum)
        {
            Dictionary<string, int> usedCount = new Dictionary<string, int>();
            StringBuilder strWhere = new StringBuilder();
            if (roomNum != null && roomNum.Count > 0)
            {
                foreach (string no in roomNum)
                {
                    strWhere = new StringBuilder();
                    strWhere.Append(string.Format(" ReadingRoomNo = '{0}'", no));
                    strWhere.Append(string.Format(" and EnterOutState>0 and EnterOutType=1", (int)EnterOutLogType.Leave));
                    DataSet ds = t_sm_enterOutLog_Dal.GetCount(strWhere.ToString(), null);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        usedCount.Add(no, int.Parse(ds.Tables[0].Rows[0][0].ToString()));
                    }
                    else
                    {
                        usedCount.Add(no, 0);
                    }
                }
            }
            return usedCount;
        }
        /// <summary>
        /// 获取使用中的座位 
        /// </summary>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        public List<EnterOutLogInfo> GetUsingSeatEnterOutLogInfo(string roomNum)
        {
            List<EnterOutLogInfo> enterOutList = new List<EnterOutLogInfo>();
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(string.Format(" ReadingRoomNo='{0}' and EnterOutState>0 and EnterOutType=1", roomNum, (int)EnterOutLogType.Leave));
            DataSet ds = t_sm_enterOutLog_Dal.GetList(strWhere.ToString(), null);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                EnterOutLogInfo model = DataRowToEnterOutLogInfo(ds.Tables[0].Rows[i]);
                enterOutList.Add(model);
            }
            return enterOutList;
        }



        /// <summary>
        /// 获取Id大于参数值的进出记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        public List<EnterOutLogInfo> GetEnterOutLogInfoGreaterThanId(int id, bool isorder)
        {
            List<EnterOutLogInfo> enterOutList = new List<EnterOutLogInfo>();
            StringBuilder strWhere = new StringBuilder();
            DataSet ds = new DataSet();
            if (isorder)
            {
                strWhere.AppendFormat(" EnterOutLogID>{0} order by EnterOutLogNo,EnterOutLogID", id);
            }
            else
            {
                strWhere.AppendFormat(" EnterOutLogID>{0}", id);
            }
            try
            {
                ds = t_sm_enterOutLog_Dal.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    EnterOutLogInfo model = DataRowToEnterOutLogInfo(ds.Tables[0].Rows[i]);
                    enterOutList.Add(model);
                }
                return enterOutList;

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 插入计时时间
        /// </summary>
        /// <param name="enterOutLigNo"></param>
        /// <param name="markTime"></param>
        /// <returns></returns>
        public bool UpdateMarkTime(string enterOutLigNo, DateTime markTime)
        {
            try
            {
                return t_sm_enterOutLog_Dal.UpdateMarkTime(enterOutLigNo, markTime);
            }
            catch
            {
                throw;
            }
        }

        public string DelaySeatUsedTime(ReaderInfo reader)
        {
            string handleResult = "";
            if (reader.CanContinuedTime == DateTime.Parse(reader.AtReadingRoom.Setting.RoomOpenSet.DefaultOpenTime.EndTime))
            {
                return "您可以继续使用座位到闭馆，无需再次续时。";

            }
            else if (reader.CanContinuedTime > GetServerDateTime())
            {
                return string.Format("您使用座位时间过短，还没有到达可续时时间，请在 {0} 后续时。", reader.CanContinuedTime.ToShortTimeString());
            }
            if (reader.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes != 0 && (reader.ContinuedTimeCount >= reader.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes))
            {
                return "您的续时次数不足，无法续时。";
            }
            int newLogId = -1;
            HandleResult result = AddEnterOutLogInfo(reader.EnterOutLog, ref newLogId);//插入进出记录
            if (result == HandleResult.Successed)
            {
                string LastCount = "";
                string StartTime = "";
                string SingleTime = "";
                string EndTime = "";
                DateTime nowDateTime = DateTime.Now;
                DateTime dt = new DateTime();
                if (reader.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes != 0)
                {
                    LastCount = (reader.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes - reader.ContinuedTimeCount - 1).ToString();
                }
                if (reader.AtReadingRoom.Setting.SeatUsedTimeLimit.Mode == "Free")
                {

                    dt = nowDateTime.AddMinutes(reader.AtReadingRoom.Setting.SeatUsedTimeLimit.DelayTimeLength);


                    if (dt > reader.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime))
                    {
                        dt = reader.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                    }
                    else
                    {
                        StartTime = (dt.AddMinutes(-reader.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime)).ToShortTimeString();
                        EndTime = dt.ToShortTimeString();
                    }
                }
                else
                {
                    for (int i = 0; i < reader.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count; i++)
                    {
                        if (nowDateTime < reader.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i])
                        {
                            if (i + 1 < reader.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count)
                            {
                                dt = reader.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i + 1];
                                StartTime = (dt.AddMinutes(-reader.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime)).ToShortTimeString();
                                EndTime = dt.ToShortTimeString();
                            }
                            else
                            {
                                dt = reader.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                            }
                            break;
                        }
                    }
                }
                SingleTime = dt.ToShortTimeString();
                if (StartTime != "")
                {
                    handleResult = string.Format("续时成功，您还有{0}次续时机会，您可以使用座位到{1}，如果想继续使用座位在{2}至{3}间再次续时", LastCount, EndTime, StartTime, EndTime);
                }
                else
                {
                    handleResult = string.Format("续时成功，您可以使用座位直到闭馆时间{0}，您无需再次续时", SingleTime);
                }
            }
            else
            {
                handleResult = "续时失败";
            }
            return handleResult;
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 发送座位状态变化消息，告诉app，需要更座位状态
        /// </summary>
        /// <param name="model"></param>
        //private void sendSeatStateChanged(object model)
        //{
        //    try
        //    {

        //        EnterOutLogInfo log = model as EnterOutLogInfo;

        //        Console.WriteLine("当前座位状态为：" + log.EnterOutState.ToString());
        //        switch (log.EnterOutState)
        //        {
        //            case EnterOutLogType.BookingConfirmation:
        //            case EnterOutLogType.SelectSeat:
        //            case EnterOutLogType.ContinuedTime:
        //            case EnterOutLogType.ComeBack:
        //            case EnterOutLogType.ReselectSeat:
        //            case EnterOutLogType.WaitingSuccess:
        //            case EnterOutLogType.Leave:
        //            case EnterOutLogType.ShortLeave:
        //            case EnterOutLogType.Waiting:

        //                MsgPushCenter.MsgPushHandler pushMsgHandler = MsgPushCenter.MsgPushHandler.GetInstance();
        //                SeatManage.JsonModel.JM_NotifyEvent notify = new SeatManage.JsonModel.JM_NotifyEvent(log.CardNo, schoolNum);
        //                notify.NotifyType = SeatManage.JsonModel.SeatNotifyType.Event;
        //                notify.Event = SeatManage.JsonModel.JM_NotifyEvent.SEATSTATECHANGED;
        //                notify.Obj = log.EnterOutState.ToString();
        //                pushMsgHandler.PushMsg(notify);
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("座位状态变化，发送座位状态时，出现错误：" + ex.Message);
        //    }


        //}


        /// <summary>
        /// 发送进出记录通知信息，如果状态为在座，则
        /// 管理员在终端做的操作在该方法中处理，其他通知在服务中处理。
        /// </summary>
        /// <param name="log"></param>
        //private void NotifyMsg(object model)
        //{
        //    EnterOutLogInfo log = model as EnterOutLogInfo;
        //    List<string> roomNums = new List<string>();
        //    roomNums.Add(log.ReadingRoomNo);
        //    List<ReadingRoomInfo> rooms = GetReadingRoomInfo(roomNums);
        //    if (rooms.Count > 0)
        //    {
        //        log.ReadingRoomName = string.Format("{0} {1}", rooms[0].Libaray.Name, rooms[0].Name);
        //        log.ShortSeatNo = SeatManage.SeatManageComm.SeatComm.SeatNoToShortSeatNo(rooms[0].Setting.SeatNumAmount, log.SeatNo);
        //    }
        //    if (log.EnterOutState == EnterOutLogType.BookingConfirmation || log.EnterOutState == EnterOutLogType.SelectSeat || log.EnterOutState == EnterOutLogType.WaitingSuccess || log.EnterOutState == EnterOutLogType.ReselectSeat)
        //    {
        //        ReaderNoticeInfo notify = new ReaderNoticeInfo();
        //        notify.CardNo = log.CardNo;
        //        notify.Type = NoticeType.RecoverSeat;
        //        switch (log.EnterOutState)
        //        {
        //            case EnterOutLogType.ComeBack:
        //                if (log.Flag == Operation.Admin)
        //                {
        //                    notify.Note = string.Format("已被管理员恢复为在座状态");
        //                    notify.Type = NoticeType.RecoverSeat;
        //                }
        //                break;
        //            default: return;
        //        }
        //        AddReaderNotice(notify);
        //        pushMsg(notify);

        //    }
        //    else if (log.EnterOutState == EnterOutLogType.Leave)
        //    {
        //        ReaderNoticeInfo notify = new ReaderNoticeInfo();
        //        notify.CardNo = log.CardNo;
        //        notify.Type = NoticeType.ManagerFreeSetWarning;
        //        switch (log.Flag)
        //        {
        //            case Operation.Admin://被管理员释放座位
        //                notify.Note = string.Format("您的座位被管理员释放。离开时请记得释放座位。");
        //                notify.Type = NoticeType.ManagerFreeSetWarning;
        //                break;
        //            default:
        //                return;
        //        }
        //        AddReaderNotice(notify);
        //        pushMsg(notify);
        //    }
        //    else if (log.EnterOutState == EnterOutLogType.ShortLeave)
        //    {
        //        ReaderNoticeInfo notify = new ReaderNoticeInfo();
        //        notify.CardNo = log.CardNo;
        //        switch (log.Flag)
        //        {
        //            case Operation.Admin:
        //                notify.Note = string.Format("你的座位被管理员设置为暂时离开,如果您离开时需要保留座位，请记得刷卡暂离。");
        //                notify.Type = NoticeType.ManagerSetShortLeaveWarning;
        //                break;

        //            case Operation.OtherReader:
        //                notify.Note = string.Format("你的座位被其他读者设置为暂时离开，如果您离开时需要保留座位，请记得刷卡暂离。");
        //                notify.Type = NoticeType.OtherSetShortLeaveWarning;
        //                break;
        //            default:
        //                return;
        //        }

        //        AddReaderNotice(notify);
        //        pushMsg(notify);
        //        //msg.ReaderName = log.ReaderName;
        //        //msg.ReadingRoomName = log.ReadingRoomName;
        //        //msg.ViolationItem = log.Remark;
        //        //msg.ViolationTime = string.Format("{0:M} {1:t}", log.EnterOutTime, log.EnterOutTime);
        //        //msg.SchoolNum = schoolNum;

        //        //SocketMsgData.SocketRequest request = new SocketMsgData.SocketRequest();
        //        //request.MsgType = SocketMsgData.TcpMsgDataType.WeiXinNotice;
        //        //request.Parameters.Add(msg);
        //        //request.SubSystem = SocketMsgData.TcpSeatManageSubSystem.SchoolService;
        //        //client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
        //    }
        //}
        /// <summary>
        /// 发送预约信息
        /// </summary>
        /// <param name="log"></param>
        //private void sendBespeakLogMsg(BespeakLogInfo log)
        //{
        //    try
        //    {
        //        if (log.BsepeakState == BookingStatus.Waiting)
        //        {
        //            List<string> roomNums = new List<string>();
        //            roomNums.Add(log.ReadingRoomNo);
        //            List<ReadingRoomInfo> rooms = GetReadingRoomInfo(roomNums);//获取阅览室信息
        //            if (rooms.Count > 0)
        //            {
        //                log.ReadingRoomName = string.Format("{0} {1}", rooms[0].Libaray.Name, rooms[0].Name);
        //                // log.ShortSeatNo = SeatManage.SeatManageComm.SeatComm.SeatNoToShortSeatNo(rooms[0].Setting.SeatNumAmount, log.SeatNo);
        //            }
        //            ReaderInfo reader = GetReader(log.CardNo, false);
        //            if (reader != null)//获取读者信息
        //            {
        //                log.ReaderName = reader.Name;
        //            }
        //            else
        //            {
        //                log.ReaderName = log.CardNo;
        //            }
        //            WeiXinBespeakMsg msg = new WeiXinBespeakMsg(log.CardNo, GetSchoolNum());
        //            msg.MsgHeader = string.Format("{0}你好，您已成功预约座位。", log.ReaderName);

        //            msg.ReaderName = log.ReaderName;
        //            msg.SchoolNum = GetSchoolNum();
        //            msg.ChooseTime = string.Format("{0:M} {1:t}", log.BsepeakTime, log.BsepeakTime);
        //            msg.SeatNum = SeatManage.SeatManageComm.SeatComm.SeatNoToShortSeatNo(rooms[0].Setting.SeatNumAmount, log.SeatNo);
        //            msg.ReadingRoomName = log.ReadingRoomName;


        //            msg.SigninTimeBegin = log.BsepeakTime.AddMinutes(-double.Parse(rooms[0].Setting.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString();
        //            msg.SigninTimeEnd = log.BsepeakTime.AddMinutes(double.Parse(rooms[0].Setting.SeatBespeak.ConfirmTime.EndTime)).ToShortTimeString();
        //            msg.Remark = "请在规定的时间里刷卡确认。\n每天点击服务号菜单与我们互动，以便我们更好的为您提供服务";
        //            SocketMsgData.SocketRequest request = new SocketMsgData.SocketRequest();
        //            request.MsgType = SocketMsgData.TcpMsgDataType.WeiXinNotice;
        //            request.Parameters.Add(msg);
        //            request.SubSystem = SocketMsgData.TcpSeatManageSubSystem.SchoolService;
        //            client.Send(SeatManage.SeatManageComm.ByteSerializer.ObjectToByte(request));
        //        }
        //    }
        //    catch { }
        //}

        /// <summary>
        /// 获取阅览室当天的进出人次
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <returns></returns>
        private int GetTodaySeatPerson(string roomNum)
        {
            if (!string.IsNullOrEmpty(roomNum))
            {
                return t_sm_enterOutLog_Dal.GetSeatPersonTimes(GetServerDateTime(), roomNum);
            }
            else
            {
                return 0;
            }
        }




        /// <summary>
        /// 进出记录数据集行转换为实体对象
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private EnterOutLogInfo DataRowToEnterOutLogInfo(DataRow dr)
        {
            EnterOutLogInfo model = new EnterOutLogInfo();
            model.CardNo = dr["CardNo"].ToString();
            model.EnterOutLogNo = dr["EnterOutLogNo"].ToString();
            model.EnterOutTime = Convert.ToDateTime(dr["EnterOutTime"]);
            model.Flag = (Operation)int.Parse(dr["EnterFlag"].ToString());
            model.EnterOutType = (LogStatus)int.Parse(dr["EnterOutType"].ToString());
            model.SeatNo = dr["SeatNo"].ToString();
            model.EnterOutState = (EnterOutLogType)int.Parse(dr["EnterOutState"].ToString());
            model.Remark = dr["Remark"].ToString();
            model.ReaderName = dr["ReaderName"].ToString();
            model.ReadingRoomName = dr["ReadingRoomName"].ToString();
            string strtemp = dr["MarkTime"].ToString();
            if (!string.IsNullOrEmpty(strtemp))
            {
                model.MarkTime = DateTime.Parse(strtemp);
            }
            else
            {
                model.MarkTime = DateTime.Parse("1900-1-1");
            }
            model.ReadingRoomNo = dr["ReadingRoomNo"].ToString();
            model.EnterOutLogID = dr["EnterOutLogID"].ToString();
            model.TerminalNum = dr["TerminalNum"].ToString();
            if (dr["ReadingSetting"] != null)
            {
                ReadingRoomSetting set = new ReadingRoomSetting(dr["ReadingSetting"].ToString());
                model.ShortSeatNo = model.SeatNo.Substring(model.SeatNo.Length - set.SeatNumAmount, set.SeatNumAmount);
            }
            return model;
        }

        #endregion


    }
}
