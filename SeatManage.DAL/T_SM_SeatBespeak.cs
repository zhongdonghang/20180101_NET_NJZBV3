using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
using SeatManage.EnumType;
namespace SeatManage.DAL
{
    /// <summary>
    /// 数据访问类:T_SM_SeatBook
    /// </summary>
    public partial class T_SM_SeatBespeak
    {
        public T_SM_SeatBespeak()
        { }
        #region  Method


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [BespeakID],[CardNo],[ReadingRoomNo],[SeatNo],[BespeakTime],[BespeakState],[SubmitTime],[CancelTime],[BespeakCancelPerson],[Remark],[ReadingRoomName],[ReadingSetting],[ReaderName],[Sex],[ReaderTypeName],[ReaderDeptName],[ReaderProName],[BespeakFlag] ");
            strSql.Append(" FROM ViewSeatBespeak ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetCount(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT count(BespeakID) ");
            strSql.Append(" FROM ViewSeatBespeak ");
            if (!string.IsNullOrEmpty(strWhere))
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
            strSql.Append(" BespeakID,BespeakNo,CardNo,ReadingRoomNo,SeatNo,BespeakTime,SubmitTime,BespeakState,CancelTime,BespeakCancelPerson,remark,BespeakFlag ");
            strSql.Append(" FROM T_SM_SeatBespeak ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(BespeakLogInfo model)
        {
            SqlParameter[] parameters = {
                        new SqlParameter("@ExcResult", SqlDbType.Int,4),
                        new SqlParameter("@CardNo",SqlDbType.NVarChar),
                        new SqlParameter("@ReadingRoomNo", SqlDbType.NVarChar),
                        new SqlParameter("@SeatNo", SqlDbType.NVarChar),
                        new SqlParameter("@BespeakTime",SqlDbType.DateTime),
                        new SqlParameter("@BespeakState", SqlDbType.Int),
                        new SqlParameter("@Remark",SqlDbType.Text),
                        new SqlParameter("@SubmitTime", SqlDbType.DateTime),
                        new SqlParameter("@BespeakFlag",SqlDbType.NVarChar)};


            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.CardNo;
            parameters[2].Value = model.ReadingRoomNo;
            parameters[3].Value = model.SeatNo;
            parameters[4].Value = model.BsepeakTime;
            parameters[5].Value = (int)model.BsepeakState;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.SubmitTime;
            parameters[8].Value = model.FlagKey;
            try
            {
                DbHelperSQL.Execute_Proc("Proc_AddNewBookLog", parameters);
                int result = (int)parameters[0].Value;
                return result;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(BespeakLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SM_SeatBespeak set ");
            strSql.Append("CardNo=@CardNo,");
            strSql.Append("ReadingRoomNo=@ReadingRoomNo,");
            strSql.Append("SeatNo=@SeatNo,");
            strSql.Append("BespeakTime=@BespeakTime,");
            strSql.Append("BespeakState=@BespeakState,");
            strSql.Append("SubmitTime=@SubmitTime,");
            strSql.Append("CancelTime=@CancelTime,");
            strSql.Append("BespeakCancelPerson=@BespeakCancelPerson,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("BespeakFlag=@BespeakFlag");
            strSql.Append(" where BespeakID=@BespeakID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@CardNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@ReadingRoomNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@SeatNo", SqlDbType.NVarChar,100),
                    new SqlParameter("@BespeakTime", SqlDbType.DateTime), 
                    new SqlParameter("@BespeakState", SqlDbType.Int,4), 
                    new SqlParameter("@SubmitTime", SqlDbType.DateTime),
                    new SqlParameter("@CancelTime", SqlDbType.DateTime),
                    new SqlParameter("@BespeakCancelPerson", SqlDbType.Int,4),
                    new SqlParameter("@Remark",SqlDbType.Text),
                    new SqlParameter("@BespeakFlag",SqlDbType.NVarChar),
                    new SqlParameter("@BespeakID", SqlDbType.Int,4)};
            parameters[0].Value = model.CardNo;
            parameters[1].Value = model.ReadingRoomNo;
            parameters[2].Value = model.SeatNo;
            parameters[3].Value = model.BsepeakTime;
            parameters[4].Value = (int)model.BsepeakState;
            parameters[5].Value = model.SubmitTime;
            parameters[6].Value = model.CancelTime;
            parameters[7].Value = (int)model.CancelPerson;
            parameters[8].Value = model.Remark;
            parameters[9].Value = model.FlagKey;
            parameters[10].Value = model.BsepeaklogID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        ///// <summary>
        ///// 删除一条数据
        ///// </summary>
        //public bool Delete(int BookID)
        //{

        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from T_SM_SeatBook ");
        //    strSql.Append(" where BookID=@BookID ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@BookID", SqlDbType.Int,4)};
        //    parameters[0].Value = BookID;

        //    int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
        //    if (rows > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        ///// <summary>
        ///// 批量删除数据
        ///// </summary>
        //public bool DeleteList(string BookIDlist )
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from T_SM_SeatBook ");
        //    strSql.Append(" where BookID in ("+BookIDlist + ")  ");
        //    int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
        //    if (rows > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}







        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(string cardNo, int pageSize, int pageIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT BespeakID,CardNo,ReaderName,ReadingRoomNo,ReadingRoomName,ReadingSetting,SeatNo,BespeakTime,SubmitTime,BespeakState,CancelTime,BespeakCancelPerson,remark,BespeakFlag,ReaderTypeName,ReaderDeptName,Sex ");
            strSql.Append("FROM (SELECT ROW_NUMBER() OVER (ORDER BY SubmitTime DESC) AS SerialNumber,BespeakID,CardNo,ReaderName,ReadingRoomNo,ReadingRoomName,ReadingSetting,SeatNo,BespeakTime,BespeakState,SubmitTime,CancelTime,BespeakCancelPerson,remark,BespeakFlag,ReaderTypeName,ReaderDeptName,Sex FROM ViewSeatBespeak where cardNo=@cardNo) ");
            strSql.Append("AS T WHERE T.SerialNumber >@pageStartIndex  and T.SerialNumber <= @pageEndIndex");
            SqlParameter[] parameters = {
                    new SqlParameter("@cardNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@pageStartIndex", SqlDbType.Int),
                    new SqlParameter("@pageEndIndex", SqlDbType.Int)};
            parameters[0].Value = cardNo;
            parameters[1].Value = pageIndex * pageSize;
            parameters[2].Value = (pageIndex + 1) * pageSize;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        #endregion  Method
    }
}

