using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using System.Data;
using SeatManage.ClassModel;
using System.Data.SqlClient;
using SeatManage.EnumType;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        SeatManage.DAL.T_SM_EnterOutLog_Bak enteroutlog_bak_dal = new SeatManage.DAL.T_SM_EnterOutLog_Bak();
        /// <summary>
        /// 根据记录ID查询记录
        /// </summary>
        /// <param name="enterOutLogID"></param>
        /// <returns></returns>
        public EnterOutLogInfo GetEnterOutLogBakInfoById(int enterOutLogID)
        {
            SeatManage.ClassModel.EnterOutLogInfo enterOutInfo = null;//创建一个进出记录实体
            string strWhere = " [EnterOutLogID]=@EnterOutLogID ";
            SqlParameter[] parameters =  { 
                                             new SqlParameter("@EnterOutLogID",enterOutLogID)
                                         };
            try
            {
                DataSet dsEnterOutLog = enteroutlog_bak_dal.GetList(strWhere, parameters);//获取读者的信息
                if (dsEnterOutLog.Tables[0].Rows.Count > 0)
                {
                    enterOutInfo = DataRowToEnterOutLogBakInfo(dsEnterOutLog.Tables[0].Rows[0]);
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
        /// 获取进出记录
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="roomNum">阅览室号</param>
        /// <param name="seatNo">座位号</param>
        /// <param name="beginDate">查询的开始时间</param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<EnterOutLogInfo> GetEnterOutBakLogs(string cardNo, string roomNum, string seatNo, string beginDate, string endDate)
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
                DataSet ds = enteroutlog_bak_dal.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    EnterOutLogInfo enterOutLog = DataRowToEnterOutLogBakInfo(ds.Tables[0].Rows[i]); ;
                    list.Add(enterOutLog);
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        public List<EnterOutLogInfo> GetEnterOutBakLogsByLastID(int ID)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" EnterOutLogID>{0} order by EnterOutLogNo,EnterOutLogID", ID);
            List<EnterOutLogInfo> list = new List<EnterOutLogInfo>();
            try
            {
                DataSet ds = enteroutlog_bak_dal.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    EnterOutLogInfo enterOutLog = DataRowToEnterOutLogBakInfo(ds.Tables[0].Rows[i]); ;
                    list.Add(enterOutLog);
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        public List<EnterOutLogInfo> GetEnterOutBakLogsByDate(DateTime date)
        {
            StringBuilder strWhere = new StringBuilder();
            //strWhere.AppendFormat(" DATEDIFF(DAY,EnterOutTime,'{0}')=0 ", date);
            strWhere.AppendFormat(" EnterOutTime>'{0} 0:00:00' and EnterOutTime<'{0} 23:59:59'", date.ToShortDateString());
            List<EnterOutLogInfo> list = new List<EnterOutLogInfo>();
            try
            {
                DataSet ds = enteroutlog_bak_dal.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    EnterOutLogInfo enterOutLog = DataRowToEnterOutLogBakInfo(ds.Tables[0].Rows[i]); ;
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
        /// 获取第一条记录的日期
        /// </summary>
        /// <returns></returns>
        public DateTime GetFristLogDate()
        {
            try
            {
                DataSet ds = enteroutlog_bak_dal.GetListByT(1, "", "EnterOutLogID", null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DateTime.Parse(ds.Tables[0].Rows[0]["EnterOutTime"].ToString());
                }
                else
                {
                    return DateTime.Parse("2000-1-1");
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 进出记录数据集行转换为实体对象
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private EnterOutLogInfo DataRowToEnterOutLogBakInfo(DataRow dr)
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
            model.TypeName = dr["ReaderTypeName"].ToString();
            model.DeptName = dr["ReaderDeptName"].ToString();
            model.Sex = dr["Sex"].ToString();
            return model;
        }





    }
}
