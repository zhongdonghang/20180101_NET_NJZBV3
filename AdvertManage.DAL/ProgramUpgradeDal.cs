using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using  DBUtility;//Please add references
namespace AdvertManage.DAL
{
	/// <summary>
	/// 数据访问类:ProgramUpgrade
	/// </summary>
	public partial class ProgramUpgradeDal
	{
		public ProgramUpgradeDal()
		{}
		#region  Method

		  
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(AdvertManage.Model.ProgramUpgradeModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ProgramUpgrade(");
			strSql.Append("Application,AutoUpdaterXml,UpdateLog,ReleaseDate,Version)");
			strSql.Append(" values (");
			strSql.Append("@Application,@AutoUpdaterXml,@UpdateLog,@ReleaseDate,@Version)");
			SqlParameter[] parameters = {
					new SqlParameter("@Application", SqlDbType.Int,4),
					new SqlParameter("@AutoUpdaterXml", SqlDbType.Text),
					new SqlParameter("@UpdateLog", SqlDbType.NVarChar,4000),
					new SqlParameter("@ReleaseDate", SqlDbType.DateTime),
					new SqlParameter("@Version", SqlDbType.NVarChar,50)};

			parameters[0].Value = (int)model.Application;
			parameters[1].Value = model.AutoUpdaterXml;
			parameters[2].Value = model.UpdateLog;
			parameters[3].Value = model.ReleaseDate;
			parameters[4].Value = model.Version;

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
        public bool Update(AdvertManage.Model.ProgramUpgradeModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ProgramUpgrade set ");
			strSql.Append("Application=@Application,");
			strSql.Append("AutoUpdaterXml=@AutoUpdaterXml,");
			strSql.Append("UpdateLog=@UpdateLog,");
			strSql.Append("ReleaseDate=@ReleaseDate,");
			strSql.Append("Version=@Version");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Application", SqlDbType.Int,4),
					new SqlParameter("@AutoUpdaterXml", SqlDbType.Text),
					new SqlParameter("@UpdateLog", SqlDbType.NVarChar,4000),
					new SqlParameter("@ReleaseDate", SqlDbType.DateTime),
					new SqlParameter("@Version", SqlDbType.NVarChar,50),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = (int)model.Application;
			parameters[1].Value = model.AutoUpdaterXml;
			parameters[2].Value = model.UpdateLog;
			parameters[3].Value = model.ReleaseDate;
			parameters[4].Value = model.Version;
			parameters[5].Value = model.Id;

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
		/// 得到一个对象实体
		/// </summary>
        public AdvertManage.Model.ProgramUpgradeModel GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,Application,AutoUpdaterXml,UpdateLog,ReleaseDate,Version from ProgramUpgrade ");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = Id;

            AdvertManage.Model.ProgramUpgradeModel model = new AdvertManage.Model.ProgramUpgradeModel();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"]!=null && ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Application"]!=null && ds.Tables[0].Rows[0]["Application"].ToString()!="")
				{
					model.Application=(Model.Enum.SeatManageSubsystem)int.Parse(ds.Tables[0].Rows[0]["Application"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AutoUpdaterXml"]!=null && ds.Tables[0].Rows[0]["AutoUpdaterXml"].ToString()!="")
				{
					model.AutoUpdaterXml=ds.Tables[0].Rows[0]["AutoUpdaterXml"].ToString();
				}
				if(ds.Tables[0].Rows[0]["UpdateLog"]!=null && ds.Tables[0].Rows[0]["UpdateLog"].ToString()!="")
				{
					model.UpdateLog=ds.Tables[0].Rows[0]["UpdateLog"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ReleaseDate"]!=null && ds.Tables[0].Rows[0]["ReleaseDate"].ToString()!="")
				{
					model.ReleaseDate=DateTime.Parse(ds.Tables[0].Rows[0]["ReleaseDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Version"]!=null && ds.Tables[0].Rows[0]["Version"].ToString()!="")
				{
					model.Version=ds.Tables[0].Rows[0]["Version"].ToString();
				}
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
		public DataSet GetList(string strWhere,SqlParameter[] parameters)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,Application,AutoUpdaterXml,UpdateLog,ReleaseDate,Version ");
			strSql.Append(" FROM ProgramUpgrade ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
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
			parameters[0].Value = "ProgramUpgrade";
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

