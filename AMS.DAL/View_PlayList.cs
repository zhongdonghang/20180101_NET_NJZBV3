using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:View_PlayList
	/// </summary>
	public partial class View_PlayList
	{
		public View_PlayList()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(AMS.Model.View_PlayList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into View_PlayList(");
			strSql.Append("Id,Number,PlayListName,ReleaseDate,EffectDate,EndDate,Describe,PlayList,OperatorLoginId,OperatorPwd,OperatorBranchName,OperatorName,OperatorRemark)");
			strSql.Append(" values (");
			strSql.Append("@Id,@Number,@PlayListName,@ReleaseDate,@EffectDate,@EndDate,@Describe,@PlayList,@OperatorLoginId,@OperatorPwd,@OperatorBranchName,@OperatorName,@OperatorRemark)");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@PlayListName", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleaseDate", SqlDbType.DateTime),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@PlayList", SqlDbType.Text),
					new SqlParameter("@OperatorLoginId", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorPwd", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorBranchName", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorName", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorRemark", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.Number;
			parameters[2].Value = model.PlayListName;
			parameters[3].Value = model.ReleaseDate;
			parameters[4].Value = model.EffectDate;
			parameters[5].Value = model.EndDate;
			parameters[6].Value = model.Describe;
			parameters[7].Value = model.PlayList;
			parameters[8].Value = model.OperatorLoginId;
			parameters[9].Value = model.OperatorPwd;
			parameters[10].Value = model.OperatorBranchName;
			parameters[11].Value = model.OperatorName;
			parameters[12].Value = model.OperatorRemark;

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
		public bool Update(AMS.Model.View_PlayList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update View_PlayList set ");
			strSql.Append("Id=@Id,");
			strSql.Append("Number=@Number,");
			strSql.Append("PlayListName=@PlayListName,");
			strSql.Append("ReleaseDate=@ReleaseDate,");
			strSql.Append("EffectDate=@EffectDate,");
			strSql.Append("EndDate=@EndDate,");
			strSql.Append("Describe=@Describe,");
			strSql.Append("PlayList=@PlayList,");
			strSql.Append("OperatorLoginId=@OperatorLoginId,");
			strSql.Append("OperatorPwd=@OperatorPwd,");
			strSql.Append("OperatorBranchName=@OperatorBranchName,");
			strSql.Append("OperatorName=@OperatorName,");
			strSql.Append("OperatorRemark=@OperatorRemark");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@PlayListName", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleaseDate", SqlDbType.DateTime),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@PlayList", SqlDbType.Text),
					new SqlParameter("@OperatorLoginId", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorPwd", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorBranchName", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorName", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorRemark", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.Number;
			parameters[2].Value = model.PlayListName;
			parameters[3].Value = model.ReleaseDate;
			parameters[4].Value = model.EffectDate;
			parameters[5].Value = model.EndDate;
			parameters[6].Value = model.Describe;
			parameters[7].Value = model.PlayList;
			parameters[8].Value = model.OperatorLoginId;
			parameters[9].Value = model.OperatorPwd;
			parameters[10].Value = model.OperatorBranchName;
			parameters[11].Value = model.OperatorName;
			parameters[12].Value = model.OperatorRemark;

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
			strSql.Append("delete from View_PlayList ");
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
		public AMS.Model.View_PlayList GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,Number,PlayListName,ReleaseDate,EffectDate,EndDate,Describe,PlayList,OperatorLoginId,OperatorPwd,OperatorBranchName,OperatorName,OperatorRemark from View_PlayList ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			AMS.Model.View_PlayList model=new AMS.Model.View_PlayList();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"]!=null && ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Number"]!=null && ds.Tables[0].Rows[0]["Number"].ToString()!="")
				{
					model.Number=ds.Tables[0].Rows[0]["Number"].ToString();
				}
				if(ds.Tables[0].Rows[0]["PlayListName"]!=null && ds.Tables[0].Rows[0]["PlayListName"].ToString()!="")
				{
					model.PlayListName=ds.Tables[0].Rows[0]["PlayListName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ReleaseDate"]!=null && ds.Tables[0].Rows[0]["ReleaseDate"].ToString()!="")
				{
					model.ReleaseDate=DateTime.Parse(ds.Tables[0].Rows[0]["ReleaseDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EffectDate"]!=null && ds.Tables[0].Rows[0]["EffectDate"].ToString()!="")
				{
					model.EffectDate=DateTime.Parse(ds.Tables[0].Rows[0]["EffectDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EndDate"]!=null && ds.Tables[0].Rows[0]["EndDate"].ToString()!="")
				{
					model.EndDate=DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Describe"]!=null && ds.Tables[0].Rows[0]["Describe"].ToString()!="")
				{
					model.Describe=ds.Tables[0].Rows[0]["Describe"].ToString();
				}
				if(ds.Tables[0].Rows[0]["PlayList"]!=null && ds.Tables[0].Rows[0]["PlayList"].ToString()!="")
				{
					model.PlayList=ds.Tables[0].Rows[0]["PlayList"].ToString();
				}
				if(ds.Tables[0].Rows[0]["OperatorLoginId"]!=null && ds.Tables[0].Rows[0]["OperatorLoginId"].ToString()!="")
				{
					model.OperatorLoginId=ds.Tables[0].Rows[0]["OperatorLoginId"].ToString();
				}
				if(ds.Tables[0].Rows[0]["OperatorPwd"]!=null && ds.Tables[0].Rows[0]["OperatorPwd"].ToString()!="")
				{
					model.OperatorPwd=ds.Tables[0].Rows[0]["OperatorPwd"].ToString();
				}
				if(ds.Tables[0].Rows[0]["OperatorBranchName"]!=null && ds.Tables[0].Rows[0]["OperatorBranchName"].ToString()!="")
				{
					model.OperatorBranchName=ds.Tables[0].Rows[0]["OperatorBranchName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["OperatorName"]!=null && ds.Tables[0].Rows[0]["OperatorName"].ToString()!="")
				{
					model.OperatorName=ds.Tables[0].Rows[0]["OperatorName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["OperatorRemark"]!=null && ds.Tables[0].Rows[0]["OperatorRemark"].ToString()!="")
				{
					model.OperatorRemark=ds.Tables[0].Rows[0]["OperatorRemark"].ToString();
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
			strSql.Append("select Id,Number,PlayListName,ReleaseDate,EffectDate,EndDate,Describe,PlayList,OperatorLoginId,OperatorPwd,OperatorBranchName,OperatorName,OperatorRemark ");
			strSql.Append(" FROM View_PlayList ");
			if(!string.IsNullOrEmpty(strWhere))
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
			strSql.Append(" Id,Number,PlayListName,ReleaseDate,EffectDate,EndDate,Describe,PlayList,OperatorLoginId,OperatorPwd,OperatorBranchName,OperatorName,OperatorRemark ");
			strSql.Append(" FROM View_PlayList ");
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
			parameters[0].Value = "View_PlayList";
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

