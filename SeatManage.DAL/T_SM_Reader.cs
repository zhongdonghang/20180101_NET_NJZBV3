using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
namespace SeatManage.DAL
{
	/// <summary>
	/// 数据访问类:T_SM_Reader
	/// </summary>
	public partial class T_SM_Reader
	{
		public T_SM_Reader()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string CardNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_SM_Reader");
            strSql.Append(" where CardNo=@CardNo ");
			SqlParameter[] parameters = {
					new SqlParameter("@CardNo", SqlDbType.NVarChar,20)};
            parameters[0].Value = CardNo;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 获取读者类型
        /// </summary>
        /// <returns></returns>
        public DataSet GetReaderType()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ReaderTypeName From T_SM_Reader Group by ReaderTypeName");
            return DbHelperSQL.Query(strSql.ToString(), null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CardNo,CardID,ReaderName,Sex,ReaderTypeName,ReaderDeptName,ReaderProName,Flag ");
            strSql.Append(" FROM T_SM_Reader ");
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
            strSql.Append(" CardNo,CardID,ReaderName,Sex,ReaderTypeName,ReaderDeptName,ReaderProName,Flag ");
            strSql.Append(" FROM T_SM_Reader ");
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
        public bool Add(SeatManage.ClassModel.ReaderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SM_Reader(");
            strSql.Append("CardNo,CardID,ReaderName,Sex,ReaderTypeName,ReaderDeptName,ReaderProName,Flag)");
            strSql.Append(" values (");
            strSql.Append("@CardNo,@CardID,@ReaderName,@Sex,@ReaderTypeName,@ReaderDeptName,@ReaderProName,@Flag)");
            SqlParameter[] parameters = {
                    new SqlParameter("@CardNo", SqlDbType.NVarChar,20),
                    new SqlParameter("@CardID", SqlDbType.NVarChar,20),
                    new SqlParameter("@ReaderName", SqlDbType.NVarChar,30),
                    new SqlParameter("@Sex", SqlDbType.NVarChar,1),
                    new SqlParameter("@ReaderTypeName", SqlDbType.NVarChar,300),
                    new SqlParameter("@ReaderDeptName", SqlDbType.NVarChar,300),
                    new SqlParameter("@ReaderProName", SqlDbType.NVarChar,300),
                    new SqlParameter("@Flag", SqlDbType.NVarChar,10)};
            parameters[0].Value = model.CardNo;
            parameters[1].Value = model.CardID;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.Sex;
            parameters[4].Value = model.ReaderType;
            parameters[5].Value = model.Dept;
            parameters[6].Value = model.Pro;
            parameters[7].Value = model.Flag;

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
        public bool Update(SeatManage.ClassModel.ReaderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SM_Reader set ");
            strSql.Append("CardId=@CardId,");
            strSql.Append("ReaderName=@ReaderName,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("ReaderTypeName=@ReaderTypeName,");
            strSql.Append("ReaderDeptName=@ReaderDeptName,");
            strSql.Append("ReaderProName=@ReaderProName,");
            strSql.Append("Flag=@Flag");
            strSql.Append(" where CardNo=@CardNo ");
            SqlParameter[] parameters = {
                    new SqlParameter("@CardNo", SqlDbType.NVarChar,20),
                    new SqlParameter("@ReaderName", SqlDbType.NVarChar,30),
                    new SqlParameter("@Sex", SqlDbType.NVarChar,1),
                    new SqlParameter("@ReaderTypeName", SqlDbType.NVarChar,30),
                    new SqlParameter("@ReaderDeptName", SqlDbType.NVarChar,30),
                    new SqlParameter("@ReaderProName", SqlDbType.NVarChar,30),
                    new SqlParameter("@Flag", SqlDbType.NVarChar,10),
                    new SqlParameter("@CardID", SqlDbType.NVarChar,20)};
            parameters[0].Value = model.CardNo;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Sex;
            parameters[3].Value = model.ReaderType;
            parameters[4].Value = model.Dept;
            parameters[5].Value = model.Pro;
            parameters[6].Value = model.Flag;
            parameters[7].Value = model.CardID;

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
        public bool UpdateByCardId(SeatManage.ClassModel.ReaderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SM_Reader set ");
            strSql.Append("CardNo=@CardNo,");
            strSql.Append("ReaderName=@ReaderName,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("ReaderTypeName=@ReaderTypeName,");
            strSql.Append("ReaderDeptName=@ReaderDeptName,");
            strSql.Append("ReaderProName=@ReaderProName,");
            strSql.Append("Flag=@Flag");
            strSql.Append(" where CardId=@CardId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@CardNo", SqlDbType.NVarChar,20),
                    new SqlParameter("@ReaderName", SqlDbType.NVarChar,30),
                    new SqlParameter("@Sex", SqlDbType.NVarChar,1),
                    new SqlParameter("@ReaderTypeName", SqlDbType.NVarChar,30),
                    new SqlParameter("@ReaderDeptName", SqlDbType.NVarChar,30),
                    new SqlParameter("@ReaderProName", SqlDbType.NVarChar,30),
                    new SqlParameter("@Flag", SqlDbType.NVarChar,10),
                    new SqlParameter("@CardID", SqlDbType.NVarChar,20)};
            parameters[0].Value = model.CardNo;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Sex;
            parameters[3].Value = model.ReaderType;
            parameters[4].Value = model.Dept;
            parameters[5].Value = model.Pro;
            parameters[6].Value = model.Flag;
            parameters[7].Value = model.CardID;

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
        /// 删除一条数据
        /// </summary>
        public bool Delete()
        { 
            StringBuilder strSql = new StringBuilder();
            strSql.Append("TRUNCATE table T_SM_Reader");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string cardNo)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from T_SM_Reader ");
            strSql.Append(" where cardNo =@CardNo");
            SqlParameter[] parameters = {
                    new SqlParameter("@CardNo", SqlDbType.NVarChar,20)};
            parameters[0].Value = cardNo;
            int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
        ///// 得到一个对象实体
        ///// </summary>
        //public SeatManage.Model.T_SM_Reader GetModel(string CardID)
        //{
			
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select  top 1 CardNo,CardID,ReaderName,Sex,ReaderTypeName,ReaderDeptName,ReaderProName,Flag from T_SM_Reader ");
        //    strSql.Append(" where CardID=@CardID ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@CardID", SqlDbType.NVarChar,20)};
        //    parameters[0].Value = CardID;

        //    SeatManage.Model.T_SM_Reader model=new SeatManage.Model.T_SM_Reader();
        //    DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
        //    if(ds.Tables[0].Rows.Count>0)
        //    {
        //        if(ds.Tables[0].Rows[0]["CardNo"]!=null && ds.Tables[0].Rows[0]["CardNo"].ToString()!="")
        //        {
        //            model.CardNo=ds.Tables[0].Rows[0]["CardNo"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["CardID"]!=null && ds.Tables[0].Rows[0]["CardID"].ToString()!="")
        //        {
        //            model.CardID=ds.Tables[0].Rows[0]["CardID"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["ReaderName"]!=null && ds.Tables[0].Rows[0]["ReaderName"].ToString()!="")
        //        {
        //            model.ReaderName=ds.Tables[0].Rows[0]["ReaderName"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["Sex"]!=null && ds.Tables[0].Rows[0]["Sex"].ToString()!="")
        //        {
        //            model.Sex=ds.Tables[0].Rows[0]["Sex"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["ReaderTypeName"]!=null && ds.Tables[0].Rows[0]["ReaderTypeName"].ToString()!="")
        //        {
        //            model.ReaderTypeName=ds.Tables[0].Rows[0]["ReaderTypeName"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["ReaderDeptName"]!=null && ds.Tables[0].Rows[0]["ReaderDeptName"].ToString()!="")
        //        {
        //            model.ReaderDeptName=ds.Tables[0].Rows[0]["ReaderDeptName"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["ReaderProName"]!=null && ds.Tables[0].Rows[0]["ReaderProName"].ToString()!="")
        //        {
        //            model.ReaderProName=ds.Tables[0].Rows[0]["ReaderProName"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["Flag"]!=null && ds.Tables[0].Rows[0]["Flag"].ToString()!="")
        //        {
        //            model.Flag=ds.Tables[0].Rows[0]["Flag"].ToString();
        //        }
        //        return model;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

		
		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
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
			parameters[0].Value = "T_SM_Reader";
			parameters[1].Value = "CardID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}

