using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace SeatManage.DAL
{
	/// <summary>
	/// 数据访问类:AMS_AdvertUsage
	/// </summary>
	public partial class AMS_AdvertUsage
	{
		public AMS_AdvertUsage()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "AMS_AdvertUsage"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AMS_AdvertUsage");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
        public bool Add(SeatManage.ClassModel.AMS_AdvertUsage model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AMS_AdvertUsage(");
			strSql.Append("AdvertID,AdvertUsage,LastUpdateTime)");
			strSql.Append(" values (");
			strSql.Append("@AdvertID,@AdvertUsage,@LastUpdateTime)");
			SqlParameter[] parameters = {
					new SqlParameter("@AdvertID", SqlDbType.Int,4),
					new SqlParameter("@AdvertUsage", SqlDbType.Text),
					new SqlParameter("@LastUpdateTime", SqlDbType.DateTime)};
			parameters[0].Value = model.AdvertID;
			parameters[1].Value = model.ToXml();
			parameters[2].Value = model.LastUpdateTime;

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
        public bool Update(SeatManage.ClassModel.AMS_AdvertUsage model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AMS_AdvertUsage set ");
			strSql.Append("AdvertID=@AdvertID,");
			strSql.Append("AdvertUsage=@AdvertUsage,");
			strSql.Append("LastUpdateTime=@LastUpdateTime");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@AdvertID", SqlDbType.Int,4),
					new SqlParameter("@AdvertUsage", SqlDbType.Text),
					new SqlParameter("@LastUpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.AdvertID;
			parameters[1].Value = model.ToXml();
			parameters[2].Value = model.LastUpdateTime;
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
			strSql.Append("delete from AMS_AdvertUsage ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
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
			strSql.Append("delete from AMS_AdvertUsage ");
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
        public SeatManage.ClassModel.AMS_AdvertUsage GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,AdvertID,AdvertUsage,LastUpdateTime from AMS_AdvertUsage ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

            SeatManage.ClassModel.AMS_AdvertUsage model = new SeatManage.ClassModel.AMS_AdvertUsage();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AdvertID"]!=null && ds.Tables[0].Rows[0]["AdvertID"].ToString()!="")
				{
					model.AdvertID=int.Parse(ds.Tables[0].Rows[0]["AdvertID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AdvertUsage"]!=null && ds.Tables[0].Rows[0]["AdvertUsage"].ToString()!="")
				{
					model.AdvertUsage=ds.Tables[0].Rows[0]["AdvertUsage"].ToString();
				}
				if(ds.Tables[0].Rows[0]["LastUpdateTime"]!=null && ds.Tables[0].Rows[0]["LastUpdateTime"].ToString()!="")
				{
					model.LastUpdateTime=DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdateTime"].ToString());
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
			strSql.Append("select ID,AdvertID,AdvertUsage,LastUpdateTime ");
			strSql.Append(" FROM AMS_AdvertUsage ");
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
			strSql.Append(" ID,AdvertID,AdvertUsage,LastUpdateTime ");
			strSql.Append(" FROM AMS_AdvertUsage ");
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
			parameters[0].Value = "AMS_AdvertUsage";
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

