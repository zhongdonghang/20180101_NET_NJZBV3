using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:View_TitleAd
	/// </summary>
	public partial class View_TitleAd
	{
		public View_TitleAd()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(AMS.Model.View_TitleAd model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into View_TitleAd(");
			strSql.Append("Name,EffectDate,EndDate,AdContent,CompanyName,AdCustomerNo,AdCustomerLinkWay,AdCustomerDes,OperatorBranchName,OperatorName,OperatorLoginId,OperatorPwd,OperatorRemark)");
			strSql.Append(" values (");
			strSql.Append("@Name,@EffectDate,@EndDate,@AdContent,@CompanyName,@AdCustomerNo,@AdCustomerLinkWay,@AdCustomerDes,@OperatorBranchName,@OperatorName,@OperatorLoginId,@OperatorPwd,@OperatorRemark)");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@AdContent", SqlDbType.NVarChar,300),
					new SqlParameter("@CompanyName", SqlDbType.NVarChar,20),
					new SqlParameter("@AdCustomerNo", SqlDbType.NVarChar,20),
					new SqlParameter("@AdCustomerLinkWay", SqlDbType.NVarChar,500),
					new SqlParameter("@AdCustomerDes", SqlDbType.NVarChar,500),
					new SqlParameter("@OperatorBranchName", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorName", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorLoginId", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorPwd", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorRemark", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.EffectDate;
			parameters[2].Value = model.EndDate;
			parameters[3].Value = model.AdContent;
			parameters[4].Value = model.CompanyName;
			parameters[5].Value = model.AdCustomerNo;
			parameters[6].Value = model.AdCustomerLinkWay;
			parameters[7].Value = model.AdCustomerDes;
			parameters[8].Value = model.OperatorBranchName;
			parameters[9].Value = model.OperatorName;
			parameters[10].Value = model.OperatorLoginId;
			parameters[11].Value = model.OperatorPwd;
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
		public bool Update(AMS.Model.View_TitleAd model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update View_TitleAd set ");
			strSql.Append("Name=@Name,");
			strSql.Append("EffectDate=@EffectDate,");
			strSql.Append("EndDate=@EndDate,");
			strSql.Append("AdContent=@AdContent,");
			strSql.Append("CompanyName=@CompanyName,");
			strSql.Append("AdCustomerNo=@AdCustomerNo,");
			strSql.Append("AdCustomerLinkWay=@AdCustomerLinkWay,");
			strSql.Append("AdCustomerDes=@AdCustomerDes,");
			strSql.Append("OperatorBranchName=@OperatorBranchName,");
			strSql.Append("OperatorName=@OperatorName,");
			strSql.Append("OperatorLoginId=@OperatorLoginId,");
			strSql.Append("OperatorPwd=@OperatorPwd,");
			strSql.Append("OperatorRemark=@OperatorRemark");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@AdContent", SqlDbType.NVarChar,300),
					new SqlParameter("@CompanyName", SqlDbType.NVarChar,20),
					new SqlParameter("@AdCustomerNo", SqlDbType.NVarChar,20),
					new SqlParameter("@AdCustomerLinkWay", SqlDbType.NVarChar,500),
					new SqlParameter("@AdCustomerDes", SqlDbType.NVarChar,500),
					new SqlParameter("@OperatorBranchName", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorName", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorLoginId", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorPwd", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorRemark", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.EffectDate;
			parameters[2].Value = model.EndDate;
			parameters[3].Value = model.AdContent;
			parameters[4].Value = model.CompanyName;
			parameters[5].Value = model.AdCustomerNo;
			parameters[6].Value = model.AdCustomerLinkWay;
			parameters[7].Value = model.AdCustomerDes;
			parameters[8].Value = model.OperatorBranchName;
			parameters[9].Value = model.OperatorName;
			parameters[10].Value = model.OperatorLoginId;
			parameters[11].Value = model.OperatorPwd;
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
			strSql.Append("delete from View_TitleAd ");
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
		public AMS.Model.View_TitleAd GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Name,EffectDate,EndDate,AdContent,CompanyName,AdCustomerNo,AdCustomerLinkWay,AdCustomerDes,OperatorBranchName,OperatorName,OperatorLoginId,OperatorPwd,OperatorRemark from View_TitleAd ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			AMS.Model.View_TitleAd model=new AMS.Model.View_TitleAd();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Name"]!=null && ds.Tables[0].Rows[0]["Name"].ToString()!="")
				{
					model.Name=ds.Tables[0].Rows[0]["Name"].ToString();
				}
				if(ds.Tables[0].Rows[0]["EffectDate"]!=null && ds.Tables[0].Rows[0]["EffectDate"].ToString()!="")
				{
					model.EffectDate=DateTime.Parse(ds.Tables[0].Rows[0]["EffectDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EndDate"]!=null && ds.Tables[0].Rows[0]["EndDate"].ToString()!="")
				{
					model.EndDate=DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AdContent"]!=null && ds.Tables[0].Rows[0]["AdContent"].ToString()!="")
				{
					model.AdContent=ds.Tables[0].Rows[0]["AdContent"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CompanyName"]!=null && ds.Tables[0].Rows[0]["CompanyName"].ToString()!="")
				{
					model.CompanyName=ds.Tables[0].Rows[0]["CompanyName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["AdCustomerNo"]!=null && ds.Tables[0].Rows[0]["AdCustomerNo"].ToString()!="")
				{
					model.AdCustomerNo=ds.Tables[0].Rows[0]["AdCustomerNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["AdCustomerLinkWay"]!=null && ds.Tables[0].Rows[0]["AdCustomerLinkWay"].ToString()!="")
				{
					model.AdCustomerLinkWay=ds.Tables[0].Rows[0]["AdCustomerLinkWay"].ToString();
				}
				if(ds.Tables[0].Rows[0]["AdCustomerDes"]!=null && ds.Tables[0].Rows[0]["AdCustomerDes"].ToString()!="")
				{
					model.AdCustomerDes=ds.Tables[0].Rows[0]["AdCustomerDes"].ToString();
				}
				if(ds.Tables[0].Rows[0]["OperatorBranchName"]!=null && ds.Tables[0].Rows[0]["OperatorBranchName"].ToString()!="")
				{
					model.OperatorBranchName=ds.Tables[0].Rows[0]["OperatorBranchName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["OperatorName"]!=null && ds.Tables[0].Rows[0]["OperatorName"].ToString()!="")
				{
					model.OperatorName=ds.Tables[0].Rows[0]["OperatorName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["OperatorLoginId"]!=null && ds.Tables[0].Rows[0]["OperatorLoginId"].ToString()!="")
				{
					model.OperatorLoginId=ds.Tables[0].Rows[0]["OperatorLoginId"].ToString();
				}
				if(ds.Tables[0].Rows[0]["OperatorPwd"]!=null && ds.Tables[0].Rows[0]["OperatorPwd"].ToString()!="")
				{
					model.OperatorPwd=ds.Tables[0].Rows[0]["OperatorPwd"].ToString();
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
			strSql.Append("select Name,EffectDate,EndDate,AdContent,CompanyName,AdCustomerNo,AdCustomerLinkWay,AdCustomerDes,OperatorBranchName,OperatorName,OperatorLoginId,OperatorPwd,OperatorRemark ");
			strSql.Append(" FROM View_TitleAd ");
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
			strSql.Append(" Name,EffectDate,EndDate,AdContent,CompanyName,AdCustomerNo,AdCustomerLinkWay,AdCustomerDes,OperatorBranchName,OperatorName,OperatorLoginId,OperatorPwd,OperatorRemark ");
			strSql.Append(" FROM View_TitleAd ");
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
			parameters[0].Value = "View_TitleAd";
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

