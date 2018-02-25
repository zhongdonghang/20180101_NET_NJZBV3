using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:AMS_CommandList
	/// </summary>
	public partial class AMS_CommandList
	{
		public AMS_CommandList()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "AMS_CommandList"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AMS_CommandList");
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
		public int Add(AMS.Model.AMS_CommandList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AMS_CommandList(");
			strSql.Append("SchoolId,Command,Operator,CommandId,ReleaseTime,FinishTime,FinishFlag)");
			strSql.Append(" values (");
			strSql.Append("@SchoolId,@Command,@Operator,@CommandId,@ReleaseTime,@FinishTime,@FinishFlag)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@SchoolId", SqlDbType.Int,4),
					new SqlParameter("@Command", SqlDbType.Int,4),
					new SqlParameter("@Operator", SqlDbType.Int,4),
					new SqlParameter("@CommandId", SqlDbType.Int,4),
					new SqlParameter("@ReleaseTime", SqlDbType.DateTime),
					new SqlParameter("@FinishTime", SqlDbType.DateTime),
					new SqlParameter("@FinishFlag", SqlDbType.Int,4)};
			parameters[0].Value = model.SchoolId;
			parameters[1].Value = model.Command;
			parameters[2].Value = model.Operator;
			parameters[3].Value = model.CommandId;
			parameters[4].Value = model.ReleaseTime;
			parameters[5].Value = model.FinishTime;
			parameters[6].Value = model.FinishFlag;

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
		public bool Update(AMS.Model.AMS_CommandList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AMS_CommandList set ");
			strSql.Append("SchoolId=@SchoolId,");
			strSql.Append("Command=@Command,");
			strSql.Append("Operator=@Operator,");
			strSql.Append("CommandId=@CommandId,");
			strSql.Append("ReleaseTime=@ReleaseTime,");
			strSql.Append("FinishTime=@FinishTime,");
			strSql.Append("FinishFlag=@FinishFlag");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@SchoolId", SqlDbType.Int,4),
					new SqlParameter("@Command", SqlDbType.Int,4),
					new SqlParameter("@Operator", SqlDbType.Int,4),
					new SqlParameter("@CommandId", SqlDbType.Int,4),
					new SqlParameter("@ReleaseTime", SqlDbType.DateTime),
					new SqlParameter("@FinishTime", SqlDbType.DateTime),
					new SqlParameter("@FinishFlag", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.SchoolId;
			parameters[1].Value = model.Command;
			parameters[2].Value = model.Operator;
			parameters[3].Value = model.CommandId;
			parameters[4].Value = model.ReleaseTime;
			parameters[5].Value = model.FinishTime;
			parameters[6].Value = model.FinishFlag;
			parameters[7].Value = model.ID;

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
			strSql.Append("delete from AMS_CommandList ");
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
			strSql.Append("delete from AMS_CommandList ");
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
		public AMS.Model.AMS_CommandList GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,SchoolId,Command,Operator,CommandId,ReleaseTime,FinishTime,FinishFlag from AMS_CommandList ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
			parameters[0].Value = ID;

			AMS.Model.AMS_CommandList model=new AMS.Model.AMS_CommandList();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SchoolId"]!=null && ds.Tables[0].Rows[0]["SchoolId"].ToString()!="")
				{
					model.SchoolId=int.Parse(ds.Tables[0].Rows[0]["SchoolId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Command"]!=null && ds.Tables[0].Rows[0]["Command"].ToString()!="")
				{
					model.Command=(AMS.Model.Enum.CommandType)int.Parse(ds.Tables[0].Rows[0]["Command"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Operator"]!=null && ds.Tables[0].Rows[0]["Operator"].ToString()!="")
				{
					model.Operator=int.Parse(ds.Tables[0].Rows[0]["Operator"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommandId"]!=null && ds.Tables[0].Rows[0]["CommandId"].ToString()!="")
				{
					model.CommandId=int.Parse(ds.Tables[0].Rows[0]["CommandId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReleaseTime"]!=null && ds.Tables[0].Rows[0]["ReleaseTime"].ToString()!="")
				{
					model.ReleaseTime=DateTime.Parse(ds.Tables[0].Rows[0]["ReleaseTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FinishTime"]!=null && ds.Tables[0].Rows[0]["FinishTime"].ToString()!="")
				{
					model.FinishTime=DateTime.Parse(ds.Tables[0].Rows[0]["FinishTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FinishFlag"]!=null && ds.Tables[0].Rows[0]["FinishFlag"].ToString()!="")
				{
					model.FinishFlag=int.Parse(ds.Tables[0].Rows[0]["FinishFlag"].ToString());
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
			strSql.Append("select ID,SchoolId,Command,Operator,CommandId,ReleaseTime,FinishTime,FinishFlag ");
			strSql.Append(" FROM AMS_CommandList ");
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
			strSql.Append(" ID,SchoolId,Command,Operator,CommandId,ReleaseTime,FinishTime,FinishFlag ");
			strSql.Append(" FROM AMS_CommandList ");
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
			parameters[0].Value = "AMS_CommandList";
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

