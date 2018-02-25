using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:AMS_IssureList
	/// </summary>
	public partial class AMS_IssureList
	{
		public AMS_IssureList()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "AMS_IssureList"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AMS_IssureList");
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
		public int Add(AMS.Model.AMS_IssureList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AMS_IssureList(");
			strSql.Append("CommandType,CommandID,SchoolID,SubmitTime,GetTime,CompleteTime,OperatorID,Flag)");
			strSql.Append(" values (");
			strSql.Append("@CommandType,@CommandID,@SchoolID,@SubmitTime,@GetTime,@CompleteTime,@OperatorID,@Flag)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CommandType", SqlDbType.Int,4),
					new SqlParameter("@CommandID", SqlDbType.Int,4),
					new SqlParameter("@SchoolID", SqlDbType.Int,4),
					new SqlParameter("@SubmitTime", SqlDbType.DateTime),
					new SqlParameter("@GetTime", SqlDbType.DateTime),
					new SqlParameter("@CompleteTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@Flag", SqlDbType.Int,4)};
			parameters[0].Value = (int)model.CommandType;
			parameters[1].Value = model.CommandID;
			parameters[2].Value = model.SchoolID;
			parameters[3].Value = model.SubmitTime;
			parameters[4].Value = model.GetTime;
			parameters[5].Value = model.CompleteTime;
			parameters[6].Value = model.OperatorID;
			parameters[7].Value = model.Flag;

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
		public bool Update(AMS.Model.AMS_IssureList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AMS_IssureList set ");
			strSql.Append("CommandType=@CommandType,");
			strSql.Append("CommandID=@CommandID,");
			strSql.Append("SchoolID=@SchoolID,");
			strSql.Append("SubmitTime=@SubmitTime,");
			strSql.Append("GetTime=@GetTime,");
			strSql.Append("CompleteTime=@CompleteTime,");
			strSql.Append("OperatorID=@OperatorID,");
			strSql.Append("Flag=@Flag");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@CommandType", SqlDbType.Int,4),
					new SqlParameter("@CommandID", SqlDbType.Int,4),
					new SqlParameter("@SchoolID", SqlDbType.Int,4),
					new SqlParameter("@SubmitTime", SqlDbType.DateTime),
					new SqlParameter("@GetTime", SqlDbType.DateTime),
					new SqlParameter("@CompleteTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@Flag", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.CommandType;
			parameters[1].Value = model.CommandID;
			parameters[2].Value = model.SchoolID;
			parameters[3].Value = model.SubmitTime;
			parameters[4].Value = model.GetTime;
			parameters[5].Value = model.CompleteTime;
			parameters[6].Value = model.OperatorID;
			parameters[7].Value = model.Flag;
			parameters[8].Value = model.ID;

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
			strSql.Append("delete from AMS_IssureList ");
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
			strSql.Append("delete from AMS_IssureList ");
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
		public AMS.Model.AMS_IssureList GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,CommandType,CommandID,SchoolID,SubmitTime,GetTime,CompleteTime,OperatorID,Flag from AMS_IssureList ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
			parameters[0].Value = ID;

			AMS.Model.AMS_IssureList model=new AMS.Model.AMS_IssureList();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommandType"]!=null && ds.Tables[0].Rows[0]["CommandType"].ToString()!="")
				{
					model.CommandType=(AMS.Model.Enum.IsureCommandType)int.Parse(ds.Tables[0].Rows[0]["CommandType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommandID"]!=null && ds.Tables[0].Rows[0]["CommandID"].ToString()!="")
				{
					model.CommandID=int.Parse(ds.Tables[0].Rows[0]["CommandID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SchoolID"]!=null && ds.Tables[0].Rows[0]["SchoolID"].ToString()!="")
				{
					model.SchoolID=int.Parse(ds.Tables[0].Rows[0]["SchoolID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SubmitTime"]!=null && ds.Tables[0].Rows[0]["SubmitTime"].ToString()!="")
				{
					model.SubmitTime=DateTime.Parse(ds.Tables[0].Rows[0]["SubmitTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GetTime"]!=null && ds.Tables[0].Rows[0]["GetTime"].ToString()!="")
				{
					model.GetTime=DateTime.Parse(ds.Tables[0].Rows[0]["GetTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CompleteTime"]!=null && ds.Tables[0].Rows[0]["CompleteTime"].ToString()!="")
				{
					model.CompleteTime=DateTime.Parse(ds.Tables[0].Rows[0]["CompleteTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OperatorID"]!=null && ds.Tables[0].Rows[0]["OperatorID"].ToString()!="")
				{
					model.OperatorID=int.Parse(ds.Tables[0].Rows[0]["OperatorID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Flag"]!=null && ds.Tables[0].Rows[0]["Flag"].ToString()!="")
				{
					model.Flag=int.Parse(ds.Tables[0].Rows[0]["Flag"].ToString());
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
			strSql.Append("select ID,CommandType,CommandID,SchoolID,SubmitTime,GetTime,CompleteTime,OperatorID,Flag ");
			strSql.Append(" FROM AMS_IssureList ");
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
			strSql.Append(" ID,CommandType,CommandID,SchoolID,SubmitTime,GetTime,CompleteTime,OperatorID,Flag ");
			strSql.Append(" FROM AMS_IssureList ");
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
			parameters[0].Value = "AMS_IssureList";
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

