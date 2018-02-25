using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
namespace SeatManage.DAL
{
	/// <summary>
	/// 数据访问类:T_SM_ViolateDiscipline
	/// </summary>
	public partial class T_SM_ViolateDiscipline
	{
		public T_SM_ViolateDiscipline()
		{}
		#region  Method

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere,SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select violateID,CardNo,SeatNo,ReadingRoomNo,ReadingRoomName,EnterFlag,EnterOutTime,BlacklistID,WarningState,Remark,Flag,ReaderName,ReaderDeptName,ReaderTypeName,Sex ");
            strSql.Append(" FROM ViewViolateDiscopline ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(),parameters);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder,SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" violateID,CardNo,SeatNo,ReadingRoomNo,EnterFlag,EnterOutTime,BlacklistID,WarningState,Remark,Flag,ReaderName,ReaderDeptName,ReaderTypeName ");
            strSql.Append(" FROM ViewViolateDiscopline ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString(),parameters);
        }

        ///// <summary>
        ///// 是否存在该记录
        ///// </summary>
        //public bool Exists(int violateID)
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select count(1) from T_SM_ViolateDiscipline");
        //    strSql.Append(" where violateID=@violateID ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@violateID", SqlDbType.Int,4)};
        //    parameters[0].Value = violateID;

        //    return DbHelperSQL.Exists(strSql.ToString(),parameters);
        //}


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ViolationRecordsLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SM_ViolateDiscipline(");
            strSql.Append("CardNo,SeatNo,ReadingRoomNo,EnterFlag,EnterOutTime,BlacklistID,Remark,Flag)");
            strSql.Append(" values (");
            strSql.Append("@CardNo,@SeatNo,@ReadingRoomNo,@EnterFlag,@EnterOutTime,@BlacklistID,@Remark,@Flag)");
            SqlParameter[] parameters = new SqlParameter[8];
            parameters[0] = new SqlParameter("@CardNo", model.CardNo);
            parameters[1] = new SqlParameter("@SeatNo", model.SeatID);
            parameters[2] = new SqlParameter("@ReadingRoomNo", model.ReadingRoomID);
            parameters[3] = new SqlParameter("@EnterFlag", model.EnterFlag);
            parameters[4] = new SqlParameter("@EnterOutTime", model.EnterOutTime);
            parameters[5] = new SqlParameter("@BlacklistID", model.BlacklistID);
            parameters[6] = new SqlParameter("@Remark", model.Remark);
            parameters[7] = new SqlParameter("@Flag", (int)model.Flag);
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ViolationRecordsLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SM_ViolateDiscipline set ");
            strSql.Append("CardNo=@CardNo,");
            strSql.Append("SeatNo=@SeatNo,");
            strSql.Append("ReadingRoomNo=@ReadingRoomNo,");
            strSql.Append("EnterFlag=@EnterFlag,");
            strSql.Append("EnterOutTime=@EnterOutTime,");
            strSql.Append("BlacklistID=@BlacklistID,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Flag=@Flag");
            strSql.Append(" where violateID=@violateID ");
            SqlParameter[] parameters = new SqlParameter[9];
            parameters[0] = new SqlParameter("@CardNo", model.CardNo);
            parameters[1] = new SqlParameter("@SeatNo", model.SeatID);
            parameters[2] = new SqlParameter("@ReadingRoomNo", model.ReadingRoomID);
            parameters[3] = new SqlParameter("@EnterFlag", model.EnterFlag);
            parameters[4] = new SqlParameter("@EnterOutTime", model.EnterOutTime);
            parameters[5] = new SqlParameter("@BlacklistID", model.BlacklistID);
            parameters[6] = new SqlParameter("@Remark", model.Remark);
            parameters[7] = new SqlParameter("@Flag", (int)model.Flag);
            parameters[8] = new SqlParameter("@violateID", model.ID);
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        ///// <summary>
        ///// 删除一条数据
        ///// </summary>
        //public bool Delete(int violateID)
        //{
			
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from T_SM_ViolateDiscipline ");
        //    strSql.Append(" where violateID=@violateID ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@violateID", SqlDbType.Int,4)};
        //    parameters[0].Value = violateID;

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
        //public bool DeleteList(string violateIDlist )
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from T_SM_ViolateDiscipline ");
        //    strSql.Append(" where violateID in ("+violateIDlist + ")  ");
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
        public DataSet GetList(string cardNo, int pageIndex, int pageSize)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT violateID,CardNo,SeatNo,ReadingRoomNo,ReadingRoomName,EnterFlag,EnterOutTime,BlacklistID,WarningState,Remark,Flag,ReaderName,ReaderDeptName,ReaderTypeName,Sex  FROM");
            strSql.AppendFormat(" (SELECT ROW_NUMBER() OVER (ORDER BY EnterOutTime DESC) AS SerialNumber, violateID,CardNo,SeatNo,ReadingRoomNo,ReadingRoomName,EnterFlag,EnterOutTime,BlacklistID,WarningState,Remark,Flag,ReaderName,ReaderDeptName,ReaderTypeName,Sex  FROM ViewViolateDiscopline where  cardNo=@cardNo ) AS T", cardNo);
            strSql.AppendFormat(" WHERE T.SerialNumber >@pageStartIndex   and T.SerialNumber <=  @pageEndIndex", pageIndex * pageSize, (pageIndex + 1) * pageSize);

            SqlParameter[] parameters = {
                    new SqlParameter("@cardNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@pageStartIndex", SqlDbType.Int),
                    new SqlParameter("@pageEndIndex", SqlDbType.Int)};
            parameters[0].Value = cardNo;
            parameters[1].Value = pageIndex * pageSize;
            parameters[2].Value = (pageIndex + 1) * pageSize;
			return DbHelperSQL.Query(strSql.ToString(),parameters);
		} 

		#endregion  Method
	}
}

