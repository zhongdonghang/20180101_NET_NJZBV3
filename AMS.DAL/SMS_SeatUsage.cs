using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:SMS_SeatUsage
	/// </summary>
	public partial class SMS_SeatUsage
	{
		public SMS_SeatUsage()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "SMS_SeatUsage"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SMS_SeatUsage");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(AMS.Model.SMS_SeatUsage model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SMS_SeatUsage(");
			strSql.Append("SchoolID,UploadDate,SeatUsageXml)");
			strSql.Append(" values (");
			strSql.Append("@SchoolID,@UploadDate,@SeatUsageXml)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@SchoolID", SqlDbType.Int,4),
					new SqlParameter("@UploadDate", SqlDbType.DateTime),
					new SqlParameter("@SeatUsageXml", SqlDbType.Text)};
			parameters[0].Value = model.SchoolID;
			parameters[1].Value = model.UploadDate;
            parameters[2].Value = model.ToXml();

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(AMS.Model.SMS_SeatUsage model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SMS_SeatUsage set ");
			strSql.Append("SchoolID=@SchoolID,");
			strSql.Append("UploadDate=@UploadDate,");
			strSql.Append("SeatUsageXml=@SeatUsageXml");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@SchoolID", SqlDbType.Int,4),
					new SqlParameter("@UploadDate", SqlDbType.DateTime),
					new SqlParameter("@SeatUsageXml", SqlDbType.Text),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.SchoolID;
			parameters[1].Value = model.UploadDate;
			parameters[2].Value = model.ToXml();
			parameters[3].Value = model.ID;

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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SMS_SeatUsage ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
			parameters[0].Value = ID;

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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SMS_SeatUsage ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public AMS.Model.SMS_SeatUsage GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,SchoolID,UploadDate,SeatUsageXml from SMS_SeatUsage ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
			parameters[0].Value = ID;

			AMS.Model.SMS_SeatUsage model=new AMS.Model.SMS_SeatUsage();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SchoolID"]!=null && ds.Tables[0].Rows[0]["SchoolID"].ToString()!="")
				{
					model.SchoolID=int.Parse(ds.Tables[0].Rows[0]["SchoolID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UploadDate"]!=null && ds.Tables[0].Rows[0]["UploadDate"].ToString()!="")
				{
					model.UploadDate=DateTime.Parse(ds.Tables[0].Rows[0]["UploadDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SeatUsageXml"]!=null && ds.Tables[0].Rows[0]["SeatUsageXml"].ToString()!="")
				{
					model.SeatUsageXml=ds.Tables[0].Rows[0]["SeatUsageXml"].ToString();
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
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,SchoolID,UploadDate,SeatUsageXml ");
			strSql.Append(" FROM SMS_SeatUsage ");
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
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ID,SchoolID,UploadDate,SeatUsageXml ");
			strSql.Append(" FROM SMS_SeatUsage ");
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
			parameters[0].Value = "SMS_SeatUsage";
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

