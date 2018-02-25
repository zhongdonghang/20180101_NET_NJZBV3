using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:View_HardAd
	/// </summary>
	public partial class View_HardAd
	{
		public View_HardAd()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(AMS.Model.View_HardAd model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into View_HardAd(");
			strSql.Append("CompanyName,CustomerNo,CustomerLinkWay,CustomerDescribe,Name,Operator,Number,EffectDate,EndDate,AdImage,HardAdDes,OperatorLoginId,OperatorPwd,OperatorBranchName,OperatorName,OperatorRemark)");
			strSql.Append(" values (");
			strSql.Append("@CompanyName,@CustomerNo,@CustomerLinkWay,@CustomerDescribe,@Name,@Operator,@Number,@EffectDate,@EndDate,@AdImage,@HardAdDes,@OperatorLoginId,@OperatorPwd,@OperatorBranchName,@OperatorName,@OperatorRemark)");
			SqlParameter[] parameters = {
					new SqlParameter("@CompanyName", SqlDbType.NVarChar,20),
					new SqlParameter("@CustomerNo", SqlDbType.NVarChar,20),
					new SqlParameter("@CustomerLinkWay", SqlDbType.NVarChar,500),
					new SqlParameter("@CustomerDescribe", SqlDbType.NVarChar,500),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Operator", SqlDbType.Int,4),
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@AdImage", SqlDbType.Image),
					new SqlParameter("@HardAdDes", SqlDbType.NVarChar,200),
					new SqlParameter("@OperatorLoginId", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorPwd", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorBranchName", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorName", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorRemark", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.CompanyName;
			parameters[1].Value = model.CustomerNo;
			parameters[2].Value = model.CustomerLinkWay;
			parameters[3].Value = model.CustomerDescribe;
			parameters[4].Value = model.Name;
			parameters[5].Value = model.Operator;
			parameters[6].Value = model.Number;
			parameters[7].Value = model.EffectDate;
			parameters[8].Value = model.EndDate;
			parameters[9].Value = model.AdImage;
			parameters[10].Value = model.HardAdDes;
			parameters[11].Value = model.OperatorLoginId;
			parameters[12].Value = model.OperatorPwd;
			parameters[13].Value = model.OperatorBranchName;
			parameters[14].Value = model.OperatorName;
			parameters[15].Value = model.OperatorRemark;

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
		public bool Update(AMS.Model.View_HardAd model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update View_HardAd set ");
			strSql.Append("CompanyName=@CompanyName,");
			strSql.Append("CustomerNo=@CustomerNo,");
			strSql.Append("CustomerLinkWay=@CustomerLinkWay,");
			strSql.Append("CustomerDescribe=@CustomerDescribe,");
			strSql.Append("Name=@Name,");
			strSql.Append("Operator=@Operator,");
			strSql.Append("Number=@Number,");
			strSql.Append("EffectDate=@EffectDate,");
			strSql.Append("EndDate=@EndDate,");
			strSql.Append("AdImage=@AdImage,");
			strSql.Append("HardAdDes=@HardAdDes,");
			strSql.Append("OperatorLoginId=@OperatorLoginId,");
			strSql.Append("OperatorPwd=@OperatorPwd,");
			strSql.Append("OperatorBranchName=@OperatorBranchName,");
			strSql.Append("OperatorName=@OperatorName,");
			strSql.Append("OperatorRemark=@OperatorRemark");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@CompanyName", SqlDbType.NVarChar,20),
					new SqlParameter("@CustomerNo", SqlDbType.NVarChar,20),
					new SqlParameter("@CustomerLinkWay", SqlDbType.NVarChar,500),
					new SqlParameter("@CustomerDescribe", SqlDbType.NVarChar,500),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Operator", SqlDbType.Int,4),
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@AdImage", SqlDbType.Image),
					new SqlParameter("@HardAdDes", SqlDbType.NVarChar,200),
					new SqlParameter("@OperatorLoginId", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorPwd", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorBranchName", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorName", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorRemark", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.CompanyName;
			parameters[1].Value = model.CustomerNo;
			parameters[2].Value = model.CustomerLinkWay;
			parameters[3].Value = model.CustomerDescribe;
			parameters[4].Value = model.Name;
			parameters[5].Value = model.Operator;
			parameters[6].Value = model.Number;
			parameters[7].Value = model.EffectDate;
			parameters[8].Value = model.EndDate;
			parameters[9].Value = model.AdImage;
			parameters[10].Value = model.HardAdDes;
			parameters[11].Value = model.OperatorLoginId;
			parameters[12].Value = model.OperatorPwd;
			parameters[13].Value = model.OperatorBranchName;
			parameters[14].Value = model.OperatorName;
			parameters[15].Value = model.OperatorRemark;

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
			strSql.Append("delete from View_HardAd ");
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
		public AMS.Model.View_HardAd GetModel(string Name)
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 CompanyName,CustomerNo,CustomerLinkWay,CustomerDescribe,Name,Operator,Number,EffectDate,EndDate,AdImage,HardAdDes,OperatorLoginId,OperatorPwd,OperatorBranchName,OperatorName,OperatorRemark from View_HardAd ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
                                            	new SqlParameter("@Name", SqlDbType.NVarChar,20)
};
			parameters[0].Value = Name;
			AMS.Model.View_HardAd model=new AMS.Model.View_HardAd();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["CompanyName"]!=null && ds.Tables[0].Rows[0]["CompanyName"].ToString()!="")
				{
					model.CompanyName=ds.Tables[0].Rows[0]["CompanyName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CustomerNo"]!=null && ds.Tables[0].Rows[0]["CustomerNo"].ToString()!="")
				{
					model.CustomerNo=ds.Tables[0].Rows[0]["CustomerNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CustomerLinkWay"]!=null && ds.Tables[0].Rows[0]["CustomerLinkWay"].ToString()!="")
				{
					model.CustomerLinkWay=ds.Tables[0].Rows[0]["CustomerLinkWay"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CustomerDescribe"]!=null && ds.Tables[0].Rows[0]["CustomerDescribe"].ToString()!="")
				{
					model.CustomerDescribe=ds.Tables[0].Rows[0]["CustomerDescribe"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Name"]!=null && ds.Tables[0].Rows[0]["Name"].ToString()!="")
				{
					model.Name=ds.Tables[0].Rows[0]["Name"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Operator"]!=null && ds.Tables[0].Rows[0]["Operator"].ToString()!="")
				{
					model.Operator=int.Parse(ds.Tables[0].Rows[0]["Operator"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Number"]!=null && ds.Tables[0].Rows[0]["Number"].ToString()!="")
				{
					model.Number=ds.Tables[0].Rows[0]["Number"].ToString();
				}
				if(ds.Tables[0].Rows[0]["EffectDate"]!=null && ds.Tables[0].Rows[0]["EffectDate"].ToString()!="")
				{
					model.EffectDate=DateTime.Parse(ds.Tables[0].Rows[0]["EffectDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EndDate"]!=null && ds.Tables[0].Rows[0]["EndDate"].ToString()!="")
				{
					model.EndDate=DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AdImage"]!=null && ds.Tables[0].Rows[0]["AdImage"].ToString()!="")
				{
					model.AdImage=(byte[])ds.Tables[0].Rows[0]["AdImage"];
				}
				if(ds.Tables[0].Rows[0]["HardAdDes"]!=null && ds.Tables[0].Rows[0]["HardAdDes"].ToString()!="")
				{
					model.HardAdDes=ds.Tables[0].Rows[0]["HardAdDes"].ToString();
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
			strSql.Append("select CompanyName,CustomerNo,CustomerLinkWay,CustomerDescribe,Name,Operator,Number,EffectDate,EndDate,AdImage,HardAdDes,OperatorLoginId,OperatorPwd,OperatorBranchName,OperatorName,OperatorRemark ");
			strSql.Append(" FROM View_HardAd ");
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
			strSql.Append(" CompanyName,CustomerNo,CustomerLinkWay,CustomerDescribe,Name,Operator,Number,EffectDate,EndDate,AdImage,HardAdDes,OperatorLoginId,OperatorPwd,OperatorBranchName,OperatorName,OperatorRemark ");
			strSql.Append(" FROM View_HardAd ");
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
			parameters[0].Value = "View_HardAd";
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

