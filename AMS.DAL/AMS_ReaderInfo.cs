using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:AMS_ReaderInfo
	/// </summary>
	public partial class AMS_ReaderInfo
	{
		public AMS_ReaderInfo()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "AMS_ReaderInfo"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AMS_ReaderInfo");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(AMS.Model.AMS_ReaderInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AMS_ReaderInfo(");
			strSql.Append("SchoolId,CardNo,Name,sex,dept,type)");
			strSql.Append(" values (");
			strSql.Append("@SchoolId,@CardNo,@Name,@sex,@dept,@type)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@SchoolId", SqlDbType.Int,4),
					new SqlParameter("@CardNo", SqlDbType.NVarChar,20),
					new SqlParameter("@Name", SqlDbType.NVarChar,40),
					new SqlParameter("@sex", SqlDbType.NVarChar,4),
					new SqlParameter("@dept", SqlDbType.NVarChar,50),
					new SqlParameter("@type", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.SchoolId;
			parameters[1].Value = model.CardNo;
			parameters[2].Value = model.Name;
			parameters[3].Value = model.sex;
			parameters[4].Value = model.dept;
			parameters[5].Value = model.type;

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
		public bool Update(AMS.Model.AMS_ReaderInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AMS_ReaderInfo set ");
			strSql.Append("SchoolId=@SchoolId,");
			strSql.Append("CardNo=@CardNo,");
			strSql.Append("Name=@Name,");
			strSql.Append("sex=@sex,");
			strSql.Append("dept=@dept,");
			strSql.Append("type=@type");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@SchoolId", SqlDbType.Int,4),
					new SqlParameter("@CardNo", SqlDbType.NVarChar,20),
					new SqlParameter("@Name", SqlDbType.NVarChar,40),
					new SqlParameter("@sex", SqlDbType.NVarChar,4),
					new SqlParameter("@dept", SqlDbType.NVarChar,50),
					new SqlParameter("@type", SqlDbType.NVarChar,50),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.SchoolId;
			parameters[1].Value = model.CardNo;
			parameters[2].Value = model.Name;
			parameters[3].Value = model.sex;
			parameters[4].Value = model.dept;
			parameters[5].Value = model.type;
			parameters[6].Value = model.id;

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
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from AMS_ReaderInfo ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
			parameters[0].Value = id;

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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from AMS_ReaderInfo ");
			strSql.Append(" where id in ("+idlist + ")  ");
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
		public AMS.Model.AMS_ReaderInfo GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,SchoolId,CardNo,Name,sex,dept,type from AMS_ReaderInfo ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
			parameters[0].Value = id;

			AMS.Model.AMS_ReaderInfo model=new AMS.Model.AMS_ReaderInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"]!=null && ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SchoolId"]!=null && ds.Tables[0].Rows[0]["SchoolId"].ToString()!="")
				{
					model.SchoolId=int.Parse(ds.Tables[0].Rows[0]["SchoolId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CardNo"]!=null && ds.Tables[0].Rows[0]["CardNo"].ToString()!="")
				{
					model.CardNo=ds.Tables[0].Rows[0]["CardNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Name"]!=null && ds.Tables[0].Rows[0]["Name"].ToString()!="")
				{
					model.Name=ds.Tables[0].Rows[0]["Name"].ToString();
				}
				if(ds.Tables[0].Rows[0]["sex"]!=null && ds.Tables[0].Rows[0]["sex"].ToString()!="")
				{
					model.sex=ds.Tables[0].Rows[0]["sex"].ToString();
				}
				if(ds.Tables[0].Rows[0]["dept"]!=null && ds.Tables[0].Rows[0]["dept"].ToString()!="")
				{
					model.dept=ds.Tables[0].Rows[0]["dept"].ToString();
				}
				if(ds.Tables[0].Rows[0]["type"]!=null && ds.Tables[0].Rows[0]["type"].ToString()!="")
				{
					model.type=ds.Tables[0].Rows[0]["type"].ToString();
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
			strSql.Append("select id,SchoolId,CardNo,Name,sex,dept,type ");
			strSql.Append(" FROM AMS_ReaderInfo ");
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
			strSql.Append(" id,SchoolId,CardNo,Name,sex,dept,type ");
			strSql.Append(" FROM AMS_ReaderInfo ");
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
			parameters[0].Value = "AMS_ReaderInfo";
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

