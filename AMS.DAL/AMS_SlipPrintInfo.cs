using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:AMS_SlipPrintInfo
	/// </summary>
	public partial class AMS_SlipPrintInfo
	{
		public AMS_SlipPrintInfo()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "AMS_SlipPrintInfo"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AMS_SlipPrintInfo");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
};
			parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(AMS.Model.AMS_SlipPrintInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AMS_SlipPrintInfo(");
			strSql.Append("SlipCustomerId,CampusId,Date,PrintAmount,LookOverAmount)");
			strSql.Append(" values (");
			strSql.Append("@SlipCustomerId,@CampusId,@Date,@PrintAmount,@LookOverAmount)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@SlipCustomerId", SqlDbType.Int,4),
					new SqlParameter("@CampusId", SqlDbType.Int,4),
					new SqlParameter("@Date", SqlDbType.DateTime),
					new SqlParameter("@PrintAmount", SqlDbType.Int,4),
					new SqlParameter("@LookOverAmount", SqlDbType.Int,4)};
			parameters[0].Value = model.SlipCustomerId;
			parameters[1].Value = model.CampusId;
			parameters[2].Value = model.Date;
			parameters[3].Value = model.PrintAmount;
			parameters[4].Value = model.LookOverAmount;

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
		public bool Update(AMS.Model.AMS_SlipPrintInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AMS_SlipPrintInfo set ");
			strSql.Append("SlipCustomerId=@SlipCustomerId,");
			strSql.Append("CampusId=@CampusId,");
			strSql.Append("Date=@Date,");
			strSql.Append("PrintAmount=@PrintAmount,");
			strSql.Append("LookOverAmount=@LookOverAmount");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@SlipCustomerId", SqlDbType.Int,4),
					new SqlParameter("@CampusId", SqlDbType.Int,4),
					new SqlParameter("@Date", SqlDbType.DateTime),
					new SqlParameter("@PrintAmount", SqlDbType.Int,4),
					new SqlParameter("@LookOverAmount", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.SlipCustomerId;
			parameters[1].Value = model.CampusId;
			parameters[2].Value = model.Date;
			parameters[3].Value = model.PrintAmount;
			parameters[4].Value = model.LookOverAmount;
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
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from AMS_SlipPrintInfo ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
};
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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from AMS_SlipPrintInfo ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
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
		public AMS.Model.AMS_SlipPrintInfo GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,SlipCustomerId,CampusId,Date,PrintAmount,LookOverAmount from AMS_SlipPrintInfo ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
};
			parameters[0].Value = Id;

			AMS.Model.AMS_SlipPrintInfo model=new AMS.Model.AMS_SlipPrintInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"]!=null && ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SlipCustomerId"]!=null && ds.Tables[0].Rows[0]["SlipCustomerId"].ToString()!="")
				{
					model.SlipCustomerId=int.Parse(ds.Tables[0].Rows[0]["SlipCustomerId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CampusId"]!=null && ds.Tables[0].Rows[0]["CampusId"].ToString()!="")
				{
					model.CampusId=int.Parse(ds.Tables[0].Rows[0]["CampusId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Date"]!=null && ds.Tables[0].Rows[0]["Date"].ToString()!="")
				{
					model.Date=DateTime.Parse(ds.Tables[0].Rows[0]["Date"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PrintAmount"]!=null && ds.Tables[0].Rows[0]["PrintAmount"].ToString()!="")
				{
					model.PrintAmount=int.Parse(ds.Tables[0].Rows[0]["PrintAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LookOverAmount"]!=null && ds.Tables[0].Rows[0]["LookOverAmount"].ToString()!="")
				{
					model.LookOverAmount=int.Parse(ds.Tables[0].Rows[0]["LookOverAmount"].ToString());
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
			strSql.Append("select Id,SlipCustomerId,CampusId,Date,PrintAmount,LookOverAmount ");
			strSql.Append(" FROM AMS_SlipPrintInfo ");
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
			strSql.Append(" Id,SlipCustomerId,CampusId,Date,PrintAmount,LookOverAmount ");
			strSql.Append(" FROM AMS_SlipPrintInfo ");
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
			parameters[0].Value = "AMS_SlipPrintInfo";
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

