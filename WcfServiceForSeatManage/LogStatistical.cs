using System;
using System.Data;
using System.Text;
using SeatManage.DAL;
using SeatManage.IWCFService;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        private LogStatistical dal_LogStatistical = new LogStatistical();
        public DataTable TopSeatTimeList(int top, string startDate, string endDate, int type)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                StringBuilder strGroup = new StringBuilder();
                if (startDate != null)
                {
                    strWhere.Append(" SelectSeatTime>='" + startDate + "'");
                }
                if (endDate != null)
                {
                    strWhere.Append(!string.IsNullOrEmpty(strWhere.ToString()) ? " and" : "" + " SelectSeatTime<='" + endDate + "'");
                }
                switch (type)
                {
                    case 0:
                        strGroup.Append("ReaderName,ReaderDeptName,ReaderTypeName");
                        break;
                    case 1:
                        strGroup.Append("ReaderTypeName");
                        break;
                    case 2:
                        strGroup.Append("ReaderDeptName");
                        break;
                }
                return dal_LogStatistical.GetTopSeatTimeList(top, strWhere.ToString(), strGroup.ToString()).Tables[0];
            }
            catch
            {
                throw;
            }
        }

        public DataTable TopSeatCountList(int top, string startDate, string endDate, int type)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                StringBuilder strGroup = new StringBuilder();
                if (startDate != null)
                {
                    strWhere.Append(" SelectSeatTime>='" + startDate + "'");
                }
                if (endDate != null)
                {
                    strWhere.Append((!string.IsNullOrEmpty(strWhere.ToString()) ? " and" : "") + " SelectSeatTime<='" + endDate + "'");
                }
                switch (type)
                {
                    case 0:
                        strGroup.Append("ReaderName,ReaderDeptName,ReaderTypeName");
                        break;
                    case 1:
                        strGroup.Append("ReaderTypeName");
                        break;
                    case 2:
                        strGroup.Append("ReaderDeptName");
                        break;
                }
                return dal_LogStatistical.GetTopSelectTimeList(top, strWhere.ToString(), strGroup.ToString()).Tables[0];
            }
            catch
            {
                throw;
            }
        }

        public DataTable TopBlacklistList(int top, string startDate, string endDate, int type)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                StringBuilder strGroup = new StringBuilder();
                if (startDate != null)
                {
                    strWhere.Append(" SelectSeatTime>='" + startDate + "'");
                }
                if (endDate != null)
                {
                    strWhere.Append((!string.IsNullOrEmpty(strWhere.ToString()) ? " and" : "" )+ " SelectSeatTime<='" + endDate + "'");
                }
                switch (type)
                {
                    case 0:
                        strGroup.Append("ReaderName,ReaderDeptName,ReaderTypeName");
                        break;
                    case 1:
                        strGroup.Append("ReaderTypeName");
                        break;
                    case 2:
                        strGroup.Append("ReaderDeptName");
                        break;
                }
                return dal_LogStatistical.GetTopSelectTimeList(top, strWhere.ToString(), strGroup.ToString()).Tables[0];
            }
            catch
            {
                throw;
            }
        }

        public DataTable TopViolateDisciplineList(int top, string startDate, string endDate, int type)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                StringBuilder strGroup = new StringBuilder();
                if (startDate != null)
                {
                    strWhere.Append(" AddTime>='" + startDate + "'");
                }
                if (endDate != null)
                {
                    strWhere.Append((!string.IsNullOrEmpty(strWhere.ToString()) ? " and" : "") + " AddTime<='" + endDate + "'");
                }
                switch (type)
                {
                    case 0:
                        strGroup.Append("ReaderName,ReaderDeptName,ReaderTypeName");
                        break;
                    case 1:
                        strGroup.Append("ReaderTypeName");
                        break;
                    case 2:
                        strGroup.Append("ReaderDeptName");
                        break;
                }
                return dal_LogStatistical.GetTopSelectTimeList(top, strWhere.ToString(), strGroup.ToString()).Tables[0];
            }
            catch
            {
                throw;
            }
        }
    }
}
