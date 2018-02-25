/***********************************************************
 * 作者：王昊天
 * 创建时间：2013-5-22
 * 说明阅览室开闭计划实现
 * 修改人：
 * 修改时间：
 * *********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.ClassModel;
using System.Data;
using SeatManage.DAL;
using SeatManage.EnumType;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        T_SM_RROpenCloseLog t_sm_rropencloselog = new T_SM_RROpenCloseLog();
        /// <summary>
        /// 获取阅览室开闭馆计划，条件为null获取全部
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <param name="logstatus">记录状态</param>
        /// <param name="beginDate">记录起始时间</param>
        /// <param name="endDate">记录结束时间</param>
        /// <returns></returns>
        public List<ReadingRoomOpenCloseLogInfo> GetReadingRoomOCLog(string roomNum, LogStatus logstatus, string beginDate, string endDate)
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
            if (logstatus !=  LogStatus.None)
            {
                if (String.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" OpenCloseType='{0}'", (int)logstatus));
                }
                else
                {
                    strWhere.Append(string.Format(" and OpenCloseType='{0}'", (int)logstatus));
                }
            }
            if (string.IsNullOrEmpty(beginDate))
            {
                beginDate = "1900-1-1";
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                if (String.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" OperateTime between '{0}' and '{1}'", beginDate, endDate));
                }
                else
                {
                    strWhere.Append(string.Format(" and OperateTime between '{0}' and '{1}'", beginDate, endDate));
                }
            }

            else
            {
                if (String.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(string.Format(" (OperateTime between '{0}' and '{1}')", beginDate, GetServerDateTime().ToString()));
                }
                else
                {
                    strWhere.Append(string.Format(" and (OperateTime between '{0}' and '{1}')", beginDate, GetServerDateTime().ToString()));
                }
            }
            List<ReadingRoomOpenCloseLogInfo> list = new List<ReadingRoomOpenCloseLogInfo>();
            try
            {
                DataSet ds = t_sm_rropencloselog.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    list.Add(DataRowToReadingRoomOpenCloseLogInfo(ds.Tables[0].Rows[i]));
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 增加开闭馆记录
        /// </summary>
        /// <param name="model">开闭馆记录</param>
        /// <returns></returns>
        public int AddReadingRoomOClog(ReadingRoomOpenCloseLogInfo model, ref int newid)
        {
            try
            {
                return t_sm_rropencloselog.Add(model,ref newid);
            }
            catch
            {
                throw;
            }
        }

        private ReadingRoomOpenCloseLogInfo DataRowToReadingRoomOpenCloseLogInfo(DataRow dr)
        {
            //id,ReadingRoomNo,OperateTime,OperateNo,OpenCloseState,OpenCloseType
            ReadingRoomOpenCloseLogInfo model = new ReadingRoomOpenCloseLogInfo();
            model.ID = dr["id"].ToString();
            model.ReadingRoomNo = dr["ReadingRoomNo"].ToString();
            model.OperateNo = dr["OperateNo"].ToString();
            model.OperateTime = DateTime.Parse(dr["OperateTime"].ToString());
            model.OpenCloseState = (ReadingRoomStatus)int.Parse(dr["OpenCloseState"].ToString());
            model.Logstatus = (LogStatus)int.Parse(dr["OpenCloseType"].ToString());
            return model;
        }
    }
}
