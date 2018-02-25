using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:View_ReaderInfo
	/// </summary>
	public partial class View_ReaderInfo
	{
		public View_ReaderInfo()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(AMS.Model.View_ReaderInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into View_ReaderInfo(");
			strSql.Append("CardNo,Name,sex,dept,type,SchoolName,SchoolNum)");
			strSql.Append(" values (");
			strSql.Append("@CardNo,@Name,@sex,@dept,@type,@SchoolName,@SchoolNum)");
			SqlParameter[] parameters = {
					new SqlParameter("@CardNo", SqlDbType.NVarChar,20),
					new SqlParameter("@Name", SqlDbType.NVarChar,40),
					new SqlParameter("@sex", SqlDbType.NVarChar,4),
					new SqlParameter("@dept", SqlDbType.NVarChar,50),
					new SqlParameter("@type", SqlDbType.NVarChar,50),
					new SqlParameter("@SchoolName", SqlDbType.NVarChar,30),
					new SqlParameter("@SchoolNum", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.CardNo;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.sex;
			parameters[3].Value = model.dept;
			parameters[4].Value = model.type;
			parameters[5].Value = model.SchoolName;
			parameters[6].Value = model.SchoolNum;

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
		public bool Update(AMS.Model.View_ReaderInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update View_ReaderInfo set ");
			strSql.Append("CardNo=@CardNo,");
			strSql.Append("Name=@Name,");
			strSql.Append("sex=@sex,");
			strSql.Append("dept=@dept,");
			strSql.Append("type=@type,");
			strSql.Append("SchoolName=@SchoolName,");
			strSql.Append("SchoolNum=@SchoolNum");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@CardNo", SqlDbType.NVarChar,20),
					new SqlParameter("@Name", SqlDbType.NVarChar,40),
					new SqlParameter("@sex", SqlDbType.NVarChar,4),
					new SqlParameter("@dept", SqlDbType.NVarChar,50),
					new SqlParameter("@type", SqlDbType.NVarChar,50),
					new SqlParameter("@SchoolName", SqlDbType.NVarChar,30),
					new SqlParameter("@SchoolNum", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.CardNo;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.sex;
			parameters[3].Value = model.dept;
			parameters[4].Value = model.type;
			parameters[5].Value = model.SchoolName;
			parameters[6].Value = model.SchoolNum;

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
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from View_ReaderInfo ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

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
		public AMS.Model.View_ReaderInfo GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 CardNo,Name,sex,dept,type,SchoolName,SchoolNum from View_ReaderInfo ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			AMS.Model.View_ReaderInfo model=new AMS.Model.View_ReaderInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
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
				if(ds.Tables[0].Rows[0]["SchoolName"]!=null && ds.Tables[0].Rows[0]["SchoolName"].ToString()!="")
				{
					model.SchoolName=ds.Tables[0].Rows[0]["SchoolName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolNum"]!=null && ds.Tables[0].Rows[0]["SchoolNum"].ToString()!="")
				{
					model.SchoolNum=ds.Tables[0].Rows[0]["SchoolNum"].ToString();
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
			strSql.Append("select CardNo,Name,sex,dept,type,SchoolName,SchoolNum ");
			strSql.Append(" FROM View_ReaderInfo ");
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
			strSql.Append(" CardNo,Name,sex,dept,type,SchoolName,SchoolNum ");
			strSql.Append(" FROM View_ReaderInfo ");
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
			parameters[0].Value = "View_ReaderInfo";
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

