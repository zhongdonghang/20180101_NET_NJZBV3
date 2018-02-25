using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:View_AdvertUsage
	/// </summary>
	public partial class View_AdvertUsage
	{
		public View_AdvertUsage()
		{}
		#region  Method



//        /// <summary>
//        /// 增加一条数据
//        /// </summary>
//        public bool Add(AMS.Model.View_AdvertUsage model)
//        {
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("insert into View_AdvertUsage(");
//            strSql.Append(")");
//            strSql.Append(" values (");
//            strSql.Append(")");
//            SqlParameter[] parameters = {
//};

//            int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
//            if (rows > 0)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }
//        /// <summary>
//        /// 更新一条数据
//        /// </summary>
//        public bool Update(AMS.Model.View_AdvertUsage model)
//        {
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("update View_AdvertUsage set ");
//#warning 系统发现缺少更新的字段，请手工确认如此更新是否正确！ 
//            strSql.Append(" where ");
//            SqlParameter[] parameters = {
//};

//            int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
//            if (rows > 0)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        /// <summary>
//        /// 删除一条数据
//        /// </summary>
//        public bool Delete()
//        {
//            //该表无主键信息，请自定义主键/条件字段
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("delete from View_AdvertUsage ");
//            strSql.Append(" where ");
//            SqlParameter[] parameters = {
//};

//            int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
//            if (rows > 0)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public AMS.Model.AMS_AdvertUsage GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  from View_AdvertUsage ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

            AMS.Model.AMS_AdvertUsage model = new AMS.Model.AMS_AdvertUsage();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  [AdvertID],[ID],[AdvertUsage],[LastUpdateTime],[SchoolID],[CustomerID],[OriginalID],[UserName],[Type],[AdContent],[IsNew],[OperatorName],[EndDate],[EffectDate],[Name],[Num],[SchoolName],[Number],[CompanyName],[CustomerNo],[OriginalNum],[OriginaName],[OriginaEffectDate],[OriginaEndDate],[OriginaContent]");
			strSql.Append(" FROM View_AdvertUsage ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select [AdvertID],[ID],[AdvertUsage],[LastUpdateTime],[SchoolID],[CustomerID],[OriginalID],[UserName],[Type],[AdContent],[IsNew],[OperatorName],[EndDate],[EffectDate],[Name],[Num],[SchoolName],[Number],[CompanyName],[CustomerNo],[OriginalNum],[OriginaName],[OriginaEffectDate],[OriginaEndDate],[OriginaContent]");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append("  ");
			strSql.Append(" FROM View_AdvertUsage ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

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
			parameters[0].Value = "View_AdvertUsage";
			parameters[1].Value = "ID";
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

