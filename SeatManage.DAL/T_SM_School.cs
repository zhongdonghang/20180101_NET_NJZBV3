using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
namespace SeatManage.DAL
{
	/// <summary>
	/// 数据访问类:T_SM_School
	/// </summary>
	public partial class T_SM_School
	{
		public T_SM_School()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string SchoolNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_SM_School");
			strSql.Append(" where SchoolNo=@SchoolNo ");
			SqlParameter[] parameters = {
					new SqlParameter("@SchoolNo", SqlDbType.NVarChar,50)};
			parameters[0].Value = SchoolNo;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere,SqlParameter[] patameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SchoolNo,SchoolName ");
            strSql.Append(" FROM T_SM_School ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), patameters);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder, SqlParameter[] patameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" SchoolNo,SchoolName ");
            strSql.Append(" FROM T_SM_School ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString(), patameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SeatManage.ClassModel.School model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SM_School(");
            strSql.Append("SchoolNo,SchoolName)");
            strSql.Append(" values (");
            strSql.Append("@SchoolNo,@SchoolName)");
            SqlParameter[] parameters = {
                    new SqlParameter("@SchoolNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@SchoolName", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.No;
            parameters[1].Value = model.Name;

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
        public bool Update(SeatManage.ClassModel.School model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SM_School set ");
            strSql.Append("SchoolName=@SchoolName");
            strSql.Append(" where SchoolNo=@SchoolNo ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SchoolName", SqlDbType.NVarChar,100),
                    new SqlParameter("@SchoolNo", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.No;

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
        public bool Delete(SeatManage.ClassModel.School model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SM_School ");
            strSql.Append(" where SchoolNo=@SchoolNo ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SchoolNo", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.No;

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
        ///// 批量删除数据
        ///// </summary>
        //public bool DeleteList(string SchoolNolist )
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from T_SM_School ");
        //    strSql.Append(" where SchoolNo in ("+SchoolNolist + ")  ");
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


        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public SeatManage.Model.T_SM_School GetModel(string SchoolNo)
        //{
			
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select  top 1 SchoolNo,SchoolName from T_SM_School ");
        //    strSql.Append(" where SchoolNo=@SchoolNo ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@SchoolNo", SqlDbType.NVarChar,50)};
        //    parameters[0].Value = SchoolNo;

        //    SeatManage.Model.T_SM_School model=new SeatManage.Model.T_SM_School();
        //    DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
        //    if(ds.Tables[0].Rows.Count>0)
        //    {
        //        if(ds.Tables[0].Rows[0]["SchoolNo"]!=null && ds.Tables[0].Rows[0]["SchoolNo"].ToString()!="")
        //        {
        //            model.SchoolNo=ds.Tables[0].Rows[0]["SchoolNo"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["SchoolName"]!=null && ds.Tables[0].Rows[0]["SchoolName"].ToString()!="")
        //        {
        //            model.SchoolName=ds.Tables[0].Rows[0]["SchoolName"].ToString();
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
			parameters[0].Value = "T_SM_School";
			parameters[1].Value = "SchoolNo";
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

