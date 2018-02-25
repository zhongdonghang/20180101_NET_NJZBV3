using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:AMS_PrintTemplate
	/// </summary>
	public partial class AMS_PrintTemplate
	{
		public AMS_PrintTemplate()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "AMS_PrintTemplate"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AMS_PrintTemplate");
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
		public int Add(AMS.Model.AMS_PrintTemplate model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AMS_PrintTemplate(");
			strSql.Append("CustomerId,Name,Number,Operator,Template,EffectDate,EndDate,Describe)");
			strSql.Append(" values (");
			strSql.Append("@CustomerId,@Name,@Number,@Operator,@Template,@EffectDate,@EndDate,@Describe)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CustomerId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@Number", SqlDbType.NVarChar,20),
					new SqlParameter("@Operator", SqlDbType.Int,4),
					new SqlParameter("@Template", SqlDbType.Text),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.CustomerId;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.Number;
			parameters[3].Value = model.Operator;
			parameters[4].Value = model.Template;
			parameters[5].Value = model.EffectDate;
			parameters[6].Value = model.EndDate;
			parameters[7].Value = model.Describe;

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
		public bool Update(AMS.Model.AMS_PrintTemplate model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AMS_PrintTemplate set ");
			strSql.Append("CustomerId=@CustomerId,");
			strSql.Append("Name=@Name,");
			strSql.Append("Number=@Number,");
			strSql.Append("Operator=@Operator,");
			strSql.Append("Template=@Template,");
			strSql.Append("EffectDate=@EffectDate,");
			strSql.Append("EndDate=@EndDate,");
			strSql.Append("Describe=@Describe");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@CustomerId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,20),
					new SqlParameter("@Number", SqlDbType.NVarChar,20),
					new SqlParameter("@Operator", SqlDbType.Int,4),
					new SqlParameter("@Template", SqlDbType.Text),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.CustomerId;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.Number;
			parameters[3].Value = model.Operator;
			parameters[4].Value = model.Template;
			parameters[5].Value = model.EffectDate;
			parameters[6].Value = model.EndDate;
			parameters[7].Value = model.Describe;
			parameters[8].Value = model.Id;

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
			strSql.Append("delete from AMS_PrintTemplate ");
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
			strSql.Append("delete from AMS_PrintTemplate ");
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
		public AMS.Model.AMS_PrintTemplate GetModel(string No)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,CustomerId,Name,Number,Operator,Template,EffectDate,EndDate,Describe from AMS_PrintTemplate ");
            strSql.Append(" where Number=@Number");
			SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar)
};
            parameters[0].Value = No;

			AMS.Model.AMS_PrintTemplate model=new AMS.Model.AMS_PrintTemplate();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"]!=null && ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
                if (ds.Tables[0].Rows[0]["CustomerId"] != null && ds.Tables[0].Rows[0]["CustomerId"].ToString() != "")
				{
                    model.CustomerId = int.Parse(ds.Tables[0].Rows[0]["CustomerId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Name"]!=null && ds.Tables[0].Rows[0]["Name"].ToString()!="")
				{
					model.Name=ds.Tables[0].Rows[0]["Name"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Number"]!=null && ds.Tables[0].Rows[0]["Number"].ToString()!="")
				{
					model.Number=ds.Tables[0].Rows[0]["Number"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Operator"]!=null && ds.Tables[0].Rows[0]["Operator"].ToString()!="")
				{
					model.Operator=int.Parse(ds.Tables[0].Rows[0]["Operator"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Template"]!=null && ds.Tables[0].Rows[0]["Template"].ToString()!="")
				{
					model.Template=ds.Tables[0].Rows[0]["Template"].ToString();
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
            strSql.Append("select Id,CustomerId,CustomerName,Name,Number,Operator,Template,EffectDate,EndDate,Describe ");
			strSql.Append(" FROM AMS_PrintTemplate ");
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
			strSql.Append(" Id,CustomerId,Name,Number,Operator,Template,EffectDate,EndDate,Describe ");
			strSql.Append(" FROM AMS_PrintTemplate ");
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
			parameters[0].Value = "AMS_PrintTemplate";
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

