using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using AdvertManage.Model; 
namespace AdvertManage.DAL
{
	/// <summary>
	/// 数据访问类:AMS_Campus
	/// </summary>
	public partial class AMS_Campus
	{
		public AMS_Campus()
		{}
		#region  Method 
		/// <summary>
		/// 增加一条数据
		/// </summary>
        public bool Add( AMS_CampusModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AMS_Campus(");
			strSql.Append("Number,SchoolId,Name,Describe)");
			strSql.Append(" values (");
			strSql.Append("@Number,@SchoolId,@Name,@Describe)");
			SqlParameter[] parameters = { 
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@SchoolId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200)};
		 
			parameters[0].Value = model.Number;
			parameters[1].Value = model.SchoolId;
			parameters[2].Value = model.Name;
			parameters[3].Value = model.Describe;

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
		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(AMS_CampusModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AMS_Campus set ");
			strSql.Append("Number=@Number,");
			strSql.Append("SchoolId=@SchoolId,");
			strSql.Append("Name=@Name,");
			strSql.Append("Describe=@Describe");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@SchoolId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.Number;
			parameters[1].Value = model.SchoolId;
			parameters[2].Value = model.Name;
			parameters[3].Value = model.Describe;
			parameters[4].Value = model.Id;

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

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from AMS_Campus ");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = Id;

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
	  
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere,SqlParameter[] parameters)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Number,SchoolId,Name,Describe,SchoolNum,SchoolName,SchoolDTUIp,SchoolDescribe,SchoolConnectionString ");
            strSql.Append(" FROM View_Campus ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            } 
            return DbHelperSQL.Query(strSql.ToString(), parameters);
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder,SqlParameter[] parameters)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
            strSql.Append("  [Id] ,[Number],[SchoolId],[Name],[Describe],[SchoolNum],[SchoolName],[SchoolDTUIp],[SchoolDescribe] ,[SchoolConnectionString] ");
			strSql.Append(" FROM AMS_Campus ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString(), parameters);
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
			parameters[0].Value = "AMS_Campus";
			parameters[1].Value = "Id";
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

