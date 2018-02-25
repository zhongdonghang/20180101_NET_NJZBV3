using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using System.Data;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        SeatManage.DAL.T_SM_EnterOutLogStatistics enteroutlogStatistics = new SeatManage.DAL.T_SM_EnterOutLogStatistics();
        /// <summary>
        /// 添加一条统计记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult AddEnterOutStatistics(SeatManage.ClassModel.EnterOutLogStatistics model)
        {
            try
            {
                if (enteroutlogStatistics.Add(model) > 0)
                {
                    return SeatManage.EnumType.HandleResult.Successed;
                }
                else
                {
                    return SeatManage.EnumType.HandleResult.Failed;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 查询统计记录，NULL为查询全部
        /// </summary>
        /// <param name="roomNo"></param>
        /// <param name="cardNo"></param>
        /// <param name="SeatNo"></param>
        /// <returns></returns>
        public List<SeatManage.ClassModel.EnterOutLogStatistics> GetEnterOutLogStatisticsList(List<string> roomNo, string cardNo, string SeatNo)
        {
            StringBuilder strWhere = new StringBuilder();
            if (roomNo != null)
            {
                for (int i = 0; i < roomNo.Count; i++)
                {
                    if (i == 0)
                    {
                        strWhere.Append(string.Format(" ReadingRoomNo in ('{0}'", roomNo[i]));
                    }
                    else if (i != roomNo.Count - 1)
                    {
                        strWhere.Append(string.Format(",'{0}'  ", roomNo[i]));
                    }
                    if (i == roomNo.Count - 1)
                    {
                        strWhere.Append(string.Format(",'{0}')", roomNo[i]));
                    }
                }
            }
            if (!string.IsNullOrEmpty(cardNo))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.AppendFormat(" CardNo='{0}'", cardNo);
                }
                else
                {
                    strWhere.AppendFormat(" and CardNo='{0}'", cardNo);
                }
            }
            if (!string.IsNullOrEmpty(SeatNo))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.AppendFormat(" SeatNo='{0}'", SeatNo);
                }
                else
                {
                    strWhere.AppendFormat(" and SeatNo='{0}'", SeatNo);
                }
            }
            List<SeatManage.ClassModel.EnterOutLogStatistics> list = new List<SeatManage.ClassModel.EnterOutLogStatistics>();
            try
            {
                DataSet ds = enteroutlogStatistics.GetList(strWhere.ToString());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    list.Add(DataRowToEnterOutLogStatisticsModel(ds.Tables[0].Rows[i]));
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        public List<SeatManage.ClassModel.EnterOutLogStatistics> GetEnterOutLogStatisticsListByDate(string roomNo, string starttime, string endtime)
        {
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(roomNo))
            {

                strWhere.Append(string.Format(" ReadingRoomNo = '{0}'", roomNo));

            }
            if (!string.IsNullOrEmpty(starttime))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.AppendFormat(" SelectSeatTime>'{0}'", starttime);
                }
                else
                {
                    strWhere.AppendFormat(" and SelectSeatTime>'{0}'", starttime);
                }
            }
            if (!string.IsNullOrEmpty(endtime))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.AppendFormat(" SelectSeatTime<'{0}'", endtime);
                }
                else
                {
                    strWhere.AppendFormat(" and SelectSeatTime<'{0}'", endtime);
                }
            }
            List<SeatManage.ClassModel.EnterOutLogStatistics> list = new List<SeatManage.ClassModel.EnterOutLogStatistics>();
            try
            {
                DataSet ds = enteroutlogStatistics.GetList(strWhere.ToString());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    list.Add(DataRowToEnterOutLogStatisticsModel(ds.Tables[0].Rows[i]));
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取最后条记录
        /// </summary>
        /// <returns></returns>
        public SeatManage.ClassModel.EnterOutLogStatistics GetLastLog()
        {
            try
            {
                //DataSet ds = enteroutlogStatistics.GetList(1, "", " LastEnterOutID desc");
                DataSet ds = enteroutlogStatistics.GetList("[LastEnterOutID]=(select max( [LastEnterOutID] ) from [ViewEnterOutLogStatistics])");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToEnterOutLogStatisticsModel(ds.Tables[0].Rows[0]);
                }

                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        //[LastEnterOutID]=(select max( [LastEnterOutID] ) from [SeatManageDBV2].[dbo].[ViewEnterOutLogStatistics])
        private SeatManage.ClassModel.EnterOutLogStatistics DataRowToEnterOutLogStatisticsModel(DataRow dr)
        {
            SeatManage.ClassModel.EnterOutLogStatistics model = new SeatManage.ClassModel.EnterOutLogStatistics();
            if (dr["ID"] != null && dr["ID"].ToString() != "")
            {
                model.ID = int.Parse(dr["ID"].ToString());
            }
            if (dr["EnterOutLogNo"] != null && dr["EnterOutLogNo"].ToString() != "")
            {
                model.EnterOutLogNo = dr["EnterOutLogNo"].ToString();
            }
            if (dr["LastEnterOutID"] != null && dr["LastEnterOutID"].ToString() != "")
            {
                model.LastEnterOutLogID = int.Parse(dr["LastEnterOutID"].ToString());
            }
            if (dr["CardNo"] != null && dr["CardNo"].ToString() != "")
            {
                model.CardNo = dr["CardNo"].ToString();
            }
            if (dr["SeatNo"] != null && dr["SeatNo"].ToString() != "")
            {
                model.SeatNo = dr["SeatNo"].ToString();
            }
            if (dr["ReadingRoomNo"] != null && dr["ReadingRoomNo"].ToString() != "")
            {
                model.ReadingRoomNo = dr["ReadingRoomNo"].ToString();
            }
            if (dr["SelectSeatMode"] != null && dr["SelectSeatMode"].ToString() != "")
            {
                model.SelectSeat = (SeatManage.ClassModel.EnterOutLogSelectSeatMode)int.Parse(dr["SelectSeatMode"].ToString());
            }
            if (dr["LeaveModel"] != null && dr["LeaveModel"].ToString() != "")
            {
                model.LeaveSeat = (SeatManage.ClassModel.EnterOutLogLeaveSeatMode)int.Parse(dr["LeaveModel"].ToString());
            }
            if (dr["SelectSeatTime"] != null && dr["SelectSeatTime"].ToString() != "")
            {
                model.SelectSeatTime = DateTime.Parse(dr["SelectSeatTime"].ToString());
            }
            if (dr["LeaveSeatTime"] != null && dr["LeaveSeatTime"].ToString() != "")
            {
                model.LeaveSeatTime = DateTime.Parse(dr["LeaveSeatTime"].ToString());
            }
            if (dr["SeatTime"] != null && dr["SeatTime"].ToString() != "")
            {
                model.SeatTime = int.Parse(dr["SeatTime"].ToString());
            }
            if (dr["ShortLeaveCount"] != null && dr["ShortLeaveCount"].ToString() != "")
            {
                model.ShortLeaveCount = int.Parse(dr["ShortLeaveCount"].ToString());
            }
            if (dr["ContinueTimeCount"] != null && dr["ContinueTimeCount"].ToString() != "")
            {
                model.ContinueTimeCount = int.Parse(dr["ContinueTimeCount"].ToString());
            }
            if (dr["AllOperationCount"] != null && dr["AllOperationCount"].ToString() != "")
            {
                model.AllOperationCount = int.Parse(dr["AllOperationCount"].ToString());
            }
            if (dr["AdminOperationCount"] != null && dr["AdminOperationCount"].ToString() != "")
            {
                model.AdminOperationCount = int.Parse(dr["AdminOperationCount"].ToString());
            }
            if (dr["ReaderOperationCount"] != null && dr["ReaderOperationCount"].ToString() != "")
            {
                model.ReaderOperationCount = int.Parse(dr["ReaderOperationCount"].ToString());
            }
            if (dr["OtherOperationCount"] != null && dr["OtherOperationCount"].ToString() != "")
            {
                model.OtherOperationCount = int.Parse(dr["OtherOperationCount"].ToString());
            }
            if (dr["ServerOperationCount"] != null && dr["ServerOperationCount"].ToString() != "")
            {
                model.ServerOperationCount = int.Parse(dr["ServerOperationCount"].ToString());
            }
            if (dr["IsViolation"] != null && dr["IsViolation"].ToString() != "")
            {
                if ((dr["IsViolation"].ToString() == "1") || (dr["IsViolation"].ToString().ToLower() == "true"))
                {
                    model.IsViolation = true;
                }
                else
                {
                    model.IsViolation = false;
                }
            }
            if (dr["ReadingRoomName"] != null && dr["ReadingRoomName"].ToString() != "")
            {
                model.ReadingRoomName = dr["ReadingRoomName"].ToString();
            }
            if (dr["LibraryName"] != null && dr["LibraryName"].ToString() != "")
            {
                model.LibraryName = dr["LibraryName"].ToString();
            }
            if (dr["SchoolName"] != null && dr["SchoolName"].ToString() != "")
            {
                model.SchoolName = dr["SchoolName"].ToString();
            }
            if (dr["LibraryNo"] != null && dr["LibraryNo"].ToString() != "")
            {
                model.LibraryNo = dr["LibraryNo"].ToString();
            }
            if (dr["SchoolNo"] != null && dr["SchoolNo"].ToString() != "")
            {
                model.SchoolNo = dr["SchoolNo"].ToString();
            }
            if (dr["ReaderName"] != null && dr["ReaderName"].ToString() != "")
            {
                model.ReaderName = dr["ReaderName"].ToString();
            }
            if (dr["ReaderDeptName"] != null && dr["ReaderDeptName"].ToString() != "")
            {
                model.DeptName = dr["ReaderDeptName"].ToString();
            }
            if (dr["ReaderTypeName"] != null && dr["ReaderTypeName"].ToString() != "")
            {
                model.TypeName = dr["ReaderTypeName"].ToString();
            }
            return model;
        }
    }
}
