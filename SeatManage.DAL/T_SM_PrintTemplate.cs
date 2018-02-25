using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
namespace SeatManage.DAL
{
	/// <summary>
	/// 数据访问类:T_SM_PrintTemplate
	/// </summary>
	public partial class T_SM_PrintTemplate
	{
		public T_SM_PrintTemplate()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_SM_PrintTemplate");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.NVarChar,50)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,Template,UsedTimeStart,UsedTimeEnd,IsUsed,Describe,Num ");
            strSql.Append(" FROM T_SM_PrintTemplate ");
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
            strSql.Append(" id,Template,UsedTimeStart,UsedTimeEnd,IsUsed,Describe,Num ");
            strSql.Append(" FROM T_SM_PrintTemplate ");
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
        public bool Add(SeatManage.ClassModel.AMS_PrintTemplateModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SM_PrintTemplate(");
            strSql.Append("Template,UsedTimeStart,UsedTimeEnd,IsUsed,Describe,Num)");
            strSql.Append(" values (");
            strSql.Append("@Template,@UsedTimeStart,@UsedTimeEnd,@IsUsed,@Describe,@Num)");
            SqlParameter[] parameters = {
                    new SqlParameter("@Template", SqlDbType.Text),
                    new SqlParameter("@UsedTimeStart", SqlDbType.DateTime),
                    new SqlParameter("@UsedTimeEnd", SqlDbType.DateTime),
                    new SqlParameter("@IsUsed", SqlDbType.Int,4),
                    new SqlParameter("@Describe", SqlDbType.NVarChar,200),
                    new SqlParameter ("@Num",model.Num)}; 
            parameters[0].Value = model.Template;
            parameters[1].Value = model.EffectDate;
            parameters[2].Value = model.EndDate;
            parameters[3].Value = 1;
            parameters[4].Value = model.Describe;

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
        public bool Update(SeatManage.ClassModel.AMS_PrintTemplateModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SM_PrintTemplate set ");
            strSql.Append("Template=@Template,");
            strSql.Append("UsedTimeStart=@UsedTimeStart,");
            strSql.Append("UsedTimeEnd=@UsedTimeEnd,");
            strSql.Append("IsUsed=@IsUsed,");
            strSql.Append("Describe=@Describe");
            strSql.Append(" where Num=@Num ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Template", SqlDbType.Text),
                    new SqlParameter("@UsedTimeStart", SqlDbType.DateTime),
                    new SqlParameter("@UsedTimeEnd", SqlDbType.DateTime),
                    new SqlParameter("@IsUsed", SqlDbType.Int,4),
                    new SqlParameter("@Describe", SqlDbType.NVarChar,200),
                    new SqlParameter("@Num", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Template;
            parameters[1].Value = model.EffectDate;
            parameters[2].Value = model.EndDate;
            parameters[3].Value = 1;
            parameters[4].Value = model.Describe;
            parameters[5].Value = model.Num;

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
        public bool Delete(string id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SM_PrintTemplate ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.NVarChar,50)};
            parameters[0].Value = id;

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
        //public bool DeleteList(string idlist )
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from T_SM_PrintTemplate ");
        //    strSql.Append(" where id in ("+idlist + ")  ");
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
        //public SeatManage.Model.T_SM_PrintTemplate GetModel(string id)
        //{
			
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select  top 1 id,Template,UsedTimeStart,UsedTimeEnd,IsUsed,Name from T_SM_PrintTemplate ");
        //    strSql.Append(" where id=@id ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@id", SqlDbType.NVarChar,50)};
        //    parameters[0].Value = id;

        //    SeatManage.Model.T_SM_PrintTemplate model=new SeatManage.Model.T_SM_PrintTemplate();
        //    DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
        //    if(ds.Tables[0].Rows.Count>0)
        //    {
        //        if(ds.Tables[0].Rows[0]["id"]!=null && ds.Tables[0].Rows[0]["id"].ToString()!="")
        //        {
        //            model.id=ds.Tables[0].Rows[0]["id"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["Template"]!=null && ds.Tables[0].Rows[0]["Template"].ToString()!="")
        //        {
        //            model.Template=ds.Tables[0].Rows[0]["Template"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["UsedTimeStart"]!=null && ds.Tables[0].Rows[0]["UsedTimeStart"].ToString()!="")
        //        {
        //            model.UsedTimeStart=DateTime.Parse(ds.Tables[0].Rows[0]["UsedTimeStart"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["UsedTimeEnd"]!=null && ds.Tables[0].Rows[0]["UsedTimeEnd"].ToString()!="")
        //        {
        //            model.UsedTimeEnd=DateTime.Parse(ds.Tables[0].Rows[0]["UsedTimeEnd"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["IsUsed"]!=null && ds.Tables[0].Rows[0]["IsUsed"].ToString()!="")
        //        {
        //            model.IsUsed=int.Parse(ds.Tables[0].Rows[0]["IsUsed"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["Name"]!=null && ds.Tables[0].Rows[0]["Name"].ToString()!="")
        //        {
        //            model.Name=ds.Tables[0].Rows[0]["Name"].ToString();
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
			parameters[0].Value = "T_SM_PrintTemplate";
			parameters[1].Value = "id";
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

