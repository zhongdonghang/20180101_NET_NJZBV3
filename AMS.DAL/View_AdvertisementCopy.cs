using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:View_AdvertisementCopy
	/// </summary>
	public partial class View_AdvertisementCopy
	{
		public View_AdvertisementCopy()
		{}
		#region  Method



		


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public AMS.Model.AMS_AdvertisementSchoolCopy GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  from View_AdvertisementCopy ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

            AMS.Model.AMS_AdvertisementSchoolCopy model = new AMS.Model.AMS_AdvertisementSchoolCopy();
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
            strSql.Append("select  [SchoolID],[CustomerID],[OriginalID],[UserName],[IsNew],[Type],[AdContent],[OperatorName],[EndDate],[EffectDate],[Name],[Num],[ID],[SchoolName],[Number],[CompanyName],[CustomerNo],[OriginalNum],[OriginaName],[OriginaEffectDate],[OriginaEndDate],[OriginaContent]");
			strSql.Append(" FROM View_AdvertisementCopy ");
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
            strSql.Append("select [SchoolID],[CustomerID],[OriginalID],[UserName],[IsNew],[Type],[AdContent],[OperatorName],[EndDate],[EffectDate],[Name],[Num],[ID],[SchoolName],[Number],[CompanyName],[CustomerNo],[OriginalNum],[OriginaName],[OriginaEffectDate],[OriginaEndDate],[OriginaContent]");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append("  ");
			strSql.Append(" FROM View_AdvertisementCopy ");
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
			parameters[0].Value = "View_AdvertisementCopy";
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

