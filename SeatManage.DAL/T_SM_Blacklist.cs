using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
namespace SeatManage.DAL
{
	/// <summary>
	/// 数据访问类:T_SM_Blacklist
	/// </summary>
	public partial class T_SM_Blacklist
	{
		public T_SM_Blacklist()
		{}
		#region  Method

		 

		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(string strWhere, SqlParameter[] parameters)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_SM_Blacklist");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            } 
			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere,SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BlacklistID,CardNo,ReadingRoomNo,ReadingRoomName,AddTime,OutBlacklist,OutTime,ReMark,BlacklistState,Flag,ReaderName,ReaderDeptName,ReaderTypeName,Sex ");
            strSql.Append(" FROM ViewBlacklist ");
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
            strSql.Append(" BlacklistID,CardNo,ReadingRoomNo,AddTime,OutBlacklist,OutTime,ReMark,BlacklistState,Flag ");
            strSql.Append(" FROM T_SM_Blacklist ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 添加一条黑名单记录
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int Add(BlackListInfo model)
        {
            //TODO:不跟据阅览室状态添加进出记录
            SqlParameter[] parameters = new SqlParameter[8];
            parameters[0] = new SqlParameter("@BlacklistID", SqlDbType.Int);
            parameters[0].Direction = ParameterDirection.Output;

            parameters[1] = new SqlParameter("@CardNo", model.CardNo);
            parameters[2] = new SqlParameter("@ReadingRoomNo", model.ReadingRoomID);
            parameters[3] = new SqlParameter("@AddTime", model.AddTime);
            parameters[4] = new SqlParameter("@OutBlacklist", (int)model.OutBlacklistMode);
            parameters[5] = new SqlParameter("@OutTime", model.OutTime);
            parameters[6] = new SqlParameter("@ReMark", model.ReMark);
            parameters[7] = new SqlParameter("@BlacklistState", (int)model.BlacklistState);

            DbHelperSQL.Execute_Proc("Proc_AddBlackList", parameters);
            return (int)parameters[0].Value;
        }
        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public bool Add(SeatManage.Model.T_SM_Blacklist model)
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("insert into T_SM_Blacklist(");
        //    strSql.Append("BlacklistID,CardNo,ReadingRoomNo,AddTime,OutBlacklist,OutTime,ReMark,BlacklistState,Flag)");
        //    strSql.Append(" values (");
        //    strSql.Append("@BlacklistID,@CardNo,@ReadingRoomNo,@AddTime,@OutBlacklist,@OutTime,@ReMark,@BlacklistState,@Flag)");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@BlacklistID", SqlDbType.Int,4),
        //            new SqlParameter("@CardNo", SqlDbType.NVarChar,50),
        //            new SqlParameter("@ReadingRoomNo", SqlDbType.Int,4),
        //            new SqlParameter("@AddTime", SqlDbType.DateTime),
        //            new SqlParameter("@OutBlacklist", SqlDbType.Int,4),
        //            new SqlParameter("@OutTime", SqlDbType.DateTime),
        //            new SqlParameter("@ReMark", SqlDbType.NVarChar,500),
        //            new SqlParameter("@BlacklistState", SqlDbType.Int,4),
        //            new SqlParameter("@Flag", SqlDbType.Bit,1)};
        //    parameters[0].Value = model.BlacklistID;
        //    parameters[1].Value = model.CardNo;
        //    parameters[2].Value = model.ReadingRoomNo;
        //    parameters[3].Value = model.AddTime;
        //    parameters[4].Value = model.OutBlacklist;
        //    parameters[5].Value = model.OutTime;
        //    parameters[6].Value = model.ReMark;
        //    parameters[7].Value = model.BlacklistState;
        //    parameters[8].Value = model.Flag;

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
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(BlackListInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SM_Blacklist set ");
            strSql.Append("CardNo=@CardNo,");
            strSql.Append("ReadingRoomNo=@ReadingRoomNo,");
            strSql.Append("AddTime=@AddTime,");
            strSql.Append("OutBlacklist=@OutBlacklist,");
            strSql.Append("OutTime=@OutTime,");
            strSql.Append("ReMark=@ReMark,");
            strSql.Append("BlacklistState=@BlacklistState");
            strSql.Append(" where BlacklistID=@BlacklistID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@CardNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@ReadingRoomNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@AddTime", SqlDbType.DateTime),
                    new SqlParameter("@OutBlacklist", SqlDbType.Int,4),
                    new SqlParameter("@OutTime", SqlDbType.DateTime),
                    new SqlParameter("@ReMark", SqlDbType.NVarChar,500),
                    new SqlParameter("@BlacklistState", SqlDbType.Int,4),
                    new SqlParameter("@BlacklistID", SqlDbType.Int,4)};
            parameters[0].Value = model.CardNo;
            parameters[1].Value = model.ReadingRoomID;
            parameters[2].Value = model.AddTime;
            parameters[3].Value = (int)model.OutBlacklistMode;
            parameters[4].Value = model.OutTime;
            parameters[5].Value = model.ReMark;
            parameters[6].Value = (int)model.BlacklistState;
            parameters[7].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

    

     
		

		 
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
        public DataSet GetList(string cardNo, int pageIndex, int pageSize)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT BlacklistID,CardNo,ReadingRoomNo,ReadingRoomName,AddTime,OutBlacklist,OutTime,ReMark,BlacklistState,Flag,ReaderName,ReaderDeptName,ReaderTypeName,ReaderDeptName,Sex FROM");
            strSql.AppendFormat(" (SELECT ROW_NUMBER() OVER (ORDER BY AddTime DESC) AS SerialNumber, BlacklistID,CardNo,ReadingRoomNo,ReadingRoomName,AddTime,OutBlacklist,OutTime,ReMark,BlacklistState,Flag,ReaderName,ReaderDeptName,ReaderTypeName,Sex FROM ViewBlacklist where  cardNo=@cardNo ) AS T", cardNo);
            strSql.AppendFormat(" WHERE T.SerialNumber >@pageStartIndex   and T.SerialNumber <=  @pageEndIndex", pageIndex * pageSize, (pageIndex + 1) * pageSize);

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

