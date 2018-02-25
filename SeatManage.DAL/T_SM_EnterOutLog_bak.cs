using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
namespace SeatManage.DAL
{
    /// <summary>
    /// 数据访问类:T_SM_EnterOutLog
    /// </summary>
    public partial class T_SM_EnterOutLog_Bak
    {
        public T_SM_EnterOutLog_Bak()
        { }
        #region  Method



        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int EnterOutLogID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_SM_EnterOutLog_bak");
            strSql.Append(" where EnterOutLogID=@EnterOutLogID ");
            SqlParameter[] parameters = {
					new SqlParameter("@EnterOutLogID", SqlDbType.Int,4)};
            parameters[0].Value = EnterOutLogID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, SqlParameter[] parameters)
        {
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select EnterOutLogID,CardNo,EnterOutLogNo,ReadingRoomNo,SeatNo,EnterOutState,EnterOutTime,EnterOutType,EnterFlag,Remark,MarkTime ");
            //strSql.Append(" FROM T_SM_EnterOutLog ");
            //if (!string.IsNullOrEmpty(strWhere))
            //{
            //    strSql.Append(" where " + strWhere);
            //}
            //return DbHelperSQL.Query(strSql.ToString(), parameters);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [SchoolName],[LibraryName],[ReadingRoomName],[EnterOutTime],[EnterOutType],[EnterOutLogNo],[EnterOutLogID],[SchoolNo],[LibraryNo],[ReadingRoomNo],[EnterOutState],[SeatNo],[EnterFlag],[Remark],[MarkTime],[CardNo],[ReadingSetting],[TerminalNum],[ReaderName],[ReaderTypeName],[ReaderDeptName],[Sex]");
            strSql.Append(" FROM ViewEnterOutLog_bak ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append("  [SchoolName],[LibraryName],[ReadingRoomName],[EnterOutTime],[EnterOutType],[EnterOutLogNo],[EnterOutLogID],[SchoolNo],[LibraryNo],[ReadingRoomNo],[EnterOutState],[SeatNo],[EnterFlag],[Remark],[MarkTime],[CardNo],[ReadingSetting],[TerminalNum],[ReaderName],[ReaderTypeName],[ReaderDeptName],[Sex]");
            strSql.Append(" FROM ViewEnterOutLog_bak ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetListByT(int Top, string strWhere, string filedOrder, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" [EnterOutLogID],[CardNo],[EnterOutLogNo],[TerminalNum],[ReadingRoomNo],[SeatNo],[EnterOutState],[EnterOutTime],[EnterOutType],[EnterFlag],[Remark],[MarkTime]");
            strSql.Append("  FROM [dbo].[T_SM_EnterOutLog_bak] ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        ///<summary>
        ///分页获取数据列表
        ///</summary>
        public DataSet GetList(int PageSize, int PageIndex, string strWhere)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                new SqlParameter("@PageSize", SqlDbType.Int),
                new SqlParameter("@PageIndex", SqlDbType.Int),
                new SqlParameter("@IsReCount", SqlDbType.Bit),
                new SqlParameter("@OrderType", SqlDbType.Bit),
                new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                };
            parameters[0].Value = "T_SM_EnterOutLog_bak";
            parameters[1].Value = "EnterOutLogID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage", parameters, "ds");
        }

    }
        #endregion  Method
}

