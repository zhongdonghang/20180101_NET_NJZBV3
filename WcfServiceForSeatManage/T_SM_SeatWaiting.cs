using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.ClassModel;
using SeatManage.DAL;
using SeatManage.EnumType;
using System.Data;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        T_SM_SeatWaiting t_sm_seatwaiting_dal = new T_SM_SeatWaiting();
        #region 等待记录操作
        /// <summary>
        /// 获取等待记录
        /// </summary>
        /// <param name="cardNo">读者学号</param>
        /// <param name="SaetNo">座位号</param>
        /// <param name="enterOutLogNo">进出记录编号</param>
        /// <param name="logStatus">记录状态</param>
        /// <returns></returns>
        public List<WaitSeatLogInfo> GetWaitLogList(string cardNo, string enterOutLogNo, string begindate, string enddate, List<EnterOutLogType> logType)
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
            if (!string.IsNullOrEmpty(enterOutLogNo))
            {
                if (String.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" EnterOutLogID='{0}'", enterOutLogNo));
                }
                else
                {
                    strWhere.Append(string.Format(" and EnterOutLogID='{0}'", enterOutLogNo));
                }
            }
            if (!string.IsNullOrEmpty(begindate))
            {

                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.AppendFormat(" StateChangeTime >= {0}", begindate);
                }
                else
                {
                    strWhere.AppendFormat(" and StateChangeTime >= {0}", begindate);
                }
            }
            if (!string.IsNullOrEmpty(enddate))
            {

                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.AppendFormat(" StateChangeTime <= {0}", enddate);
                }
                else
                {
                    strWhere.AppendFormat(" and StateChangeTime <= {0}", enddate);
                }
            }
            if (logType != null && logType.Count > 0)
            {


                for (int i = 0; i < logType.Count; i++)
                {
                    if (i == 0)
                    {
                        if (string.IsNullOrEmpty(strWhere.ToString()))
                        {
                            strWhere.AppendFormat(" WaitingState in ({0}", (int)logType[i]);
                        }
                        else
                        {
                            strWhere.AppendFormat(" and WaitingState in ({0}", (int)logType[i]);
                        }
                    }
                    else if (i != logType.Count - 1)
                    {
                        strWhere.Append(string.Format("，{0}", (int)logType[i]));
                    }
                    if (i == logType.Count - 1)
                    {
                        strWhere.Append(string.Format(" ,{0})", (int)logType[i]));
                    }

                }
            }
            List<WaitSeatLogInfo> list = new List<WaitSeatLogInfo>();
            try
            {
                DataSet ds = t_sm_seatwaiting_dal.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    WaitSeatLogInfo waitSeatLog = DataRowToWaitSeatLogInfo(ds.Tables[0].Rows[0]);
                    list.Add(waitSeatLog);
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取读者最后一条等待记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public WaitSeatLogInfo GetListWaitLogByCardNo(string cardNo, string roomNum)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(cardNo))
            {
                strSql.Append(string.Format(" cardNo='{0}'", cardNo));
            }
            if (!string.IsNullOrEmpty(roomNum))
            {
                if (!string.IsNullOrEmpty(strSql.ToString()))
                {
                    strSql.Append(string.Format(" and ReadingRoomNo ='{0}'", roomNum));
                }
                else
                {
                    strSql.Append(string.Format(" ReadingRoomNo ='{0}'", roomNum));
                }
            }
            DataSet ds = t_sm_seatwaiting_dal.GetList(1, strSql.ToString(), "[StateChangeTime] desc", null);
            WaitSeatLogInfo waitSeatLog = null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                waitSeatLog = DataRowToWaitSeatLogInfo(ds.Tables[0].Rows[0]);
            }
            return waitSeatLog;
        }
        /// <summary>
        /// 增加一条等待记录
        /// </summary>
        /// <param name="model">等待记录</param>
        /// <returns></returns>
        public int AddWaitLog(WaitSeatLogInfo model)
        {
            int r = t_sm_seatwaiting_dal.Add(model);
            WaitSeatLogInfo wsli = GetListWaitLogByCardNo(model.CardNo, null);
            if (wsli == null)
            {
                return r;
            }
            PushMsgInfo msg = new PushMsgInfo();
            msg.Title = "您好，您已等待成功";
            msg.MsgType = MsgPushType.UserOperation;
            msg.StudentNum = wsli.CardNo;
            msg.SeatNum = wsli.EnterOutLog.ShortSeatNo;
            msg.RoomName = wsli.EnterOutLog.ReadingRoomName;
            msg.Message = string.Format("您已成功等待{0} {1}号座位。", msg.RoomName, msg.SeatNum);
            SendMsg(msg);

            return r;
        }
        /// <summary>
        /// 修改一条等待记录
        /// </summary>
        /// <param name="model">等待记录</param>
        /// <returns></returns>
        public bool UpdateWaitLog(WaitSeatLogInfo model)
        {
            model.SeatWaitTime = GetServerDateTime();
            if (model.OperateType == Operation.OtherReader)
            {
                if (model.WaitingState == EnterOutLogType.WaitingCancel)
                {
                    PushMsgInfo msg = new PushMsgInfo();
                    msg.Title = "您好，您等待已失效";
                    msg.MsgType = MsgPushType.UserOperation;
                    msg.StudentNum = model.CardNo;
                    msg.SeatNum = model.EnterOutLog.ShortSeatNo;
                    msg.RoomName = model.EnterOutLog.ReadingRoomName;

                    msg.Message = string.Format("您等待的{0} {1}号座位已取消", msg.RoomName, msg.SeatNum);

                    switch (model.OperateType)
                    {
                        case Operation.Admin:
                            msg.Message = string.Format("您等待的{0} {1}号座位已被管理员取消", msg.RoomName, msg.SeatNum);
                            break;
                        case Operation.OtherReader:
                            msg.Message = string.Format("原用户归来，您等待的{0} {1}号座位已取消", msg.RoomName, msg.SeatNum);
                            break;
                        case Operation.Service:
                            msg.Message = string.Format("您等待的{0} {1}号座位已被系统取消", msg.RoomName, msg.SeatNum);
                            break;
                    }

                    SendMsg(msg);
                }
            }



            return t_sm_seatwaiting_dal.Update(model);
        }
        #endregion
        private WaitSeatLogInfo DataRowToWaitSeatLogInfo(DataRow dr)
        {
            WaitSeatLogInfo wsli = new WaitSeatLogInfo();
            //SeatWaitingID,CardNo,CardNoB,ReadingRoomNo,EnterOutLogID,SeatWaitTime,StateChangeTime,WaitingState,OperateType
            wsli.SeatWaitingID = dr["SeatWaitingID"].ToString();
            wsli.CardNo = dr["CardNo"].ToString();
            wsli.CardNoB = dr["CardNoB"].ToString();
            wsli.ReadingRoomNo = dr["ReadingRoomNo"].ToString();
            wsli.EnterOutLogID = int.Parse(dr["EnterOutLogID"].ToString());
            wsli.SeatWaitTime = DateTime.Parse(dr["SeatWaitTime"].ToString());
            wsli.StatsChangeTime = DateTime.Parse(dr["StateChangeTime"].ToString());
            wsli.WaitingState = (EnterOutLogType)int.Parse(dr["WaitingState"].ToString());
            wsli.OperateType = (Operation)int.Parse(dr["OperateType"].ToString());
            wsli.SeatNo = dr["SeatNo"].ToString();
            return wsli;
        }
    }
}
