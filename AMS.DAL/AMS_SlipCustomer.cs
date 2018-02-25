using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:AMS_SlipCustomer
	/// </summary>
	public partial class AMS_SlipCustomer
	{
		public AMS_SlipCustomer()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "AMS_SlipCustomer"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AMS_SlipCustomer");
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
		public int Add(AMS.Model.AMS_SlipCustomer model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AMS_SlipCustomer(");
			strSql.Append("Number,Operator,CustomerId,SlipName,ImageUrl,SlipTemplate,CouponsXml,CustomerImage,CampusNum,EffectDate,EndDate,Type,IsPrint,Describe)");
			strSql.Append(" values (");
			strSql.Append("@Number,@Operator,@CustomerId,@SlipName,@ImageUrl,@SlipTemplate,@CouponsXml,@CustomerImage,@CampusNum,@EffectDate,@EndDate,@Type,@IsPrint,@Describe)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@Operator", SqlDbType.Int,4),
					new SqlParameter("@CustomerId", SqlDbType.Int,4),
					new SqlParameter("@SlipName", SqlDbType.NVarChar,50),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@SlipTemplate", SqlDbType.Text),
					new SqlParameter("@CouponsXml", SqlDbType.Text),
					new SqlParameter("@CustomerImage", SqlDbType.NVarChar,200),
					new SqlParameter("@CampusNum", SqlDbType.NVarChar,50),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@IsPrint", SqlDbType.Bit,1),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.Number;
			parameters[1].Value = model.Operator;
			parameters[2].Value = model.CustomerId;
			parameters[3].Value = model.SlipName;
			parameters[4].Value = model.ImageUrl;
			parameters[5].Value = model.SlipTemplate;
			parameters[6].Value = model.CouponsXml;
			parameters[7].Value = model.CustomerImage;
			parameters[8].Value = model.CampusNum;
			parameters[9].Value = model.EffectDate;
			parameters[10].Value = model.EndDate;
			parameters[11].Value = model.Type;
			parameters[12].Value = model.IsPrint;
			parameters[13].Value = model.Describe;

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
		public bool Update(AMS.Model.AMS_SlipCustomer model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AMS_SlipCustomer set ");
			strSql.Append("Number=@Number,");
			strSql.Append("Operator=@Operator,");
			strSql.Append("CustomerId=@CustomerId,");
			strSql.Append("SlipName=@SlipName,");
			strSql.Append("ImageUrl=@ImageUrl,");
			strSql.Append("SlipTemplate=@SlipTemplate,");
			strSql.Append("CouponsXml=@CouponsXml,");
			strSql.Append("CustomerImage=@CustomerImage,");
			strSql.Append("CampusNum=@CampusNum,");
			strSql.Append("EffectDate=@EffectDate,");
			strSql.Append("EndDate=@EndDate,");
			strSql.Append("Type=@Type,");
			strSql.Append("IsPrint=@IsPrint,");
			strSql.Append("Describe=@Describe");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@Operator", SqlDbType.Int,4),
					new SqlParameter("@CustomerId", SqlDbType.Int,4),
					new SqlParameter("@SlipName", SqlDbType.NVarChar,50),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@SlipTemplate", SqlDbType.Text),
					new SqlParameter("@CouponsXml", SqlDbType.Text),
					new SqlParameter("@CustomerImage", SqlDbType.NVarChar,200),
					new SqlParameter("@CampusNum", SqlDbType.NVarChar,50),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@IsPrint", SqlDbType.Bit,1),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.Number;
			parameters[1].Value = model.Operator;
			parameters[2].Value = model.CustomerId;
			parameters[3].Value = model.SlipName;
			parameters[4].Value = model.ImageUrl;
			parameters[5].Value = model.SlipTemplate;
			parameters[6].Value = model.CouponsXml;
			parameters[7].Value = model.CustomerImage;
			parameters[8].Value = model.CampusNum;
			parameters[9].Value = model.EffectDate;
			parameters[10].Value = model.EndDate;
			parameters[11].Value = model.Type;
			parameters[12].Value = model.IsPrint;
			parameters[13].Value = model.Describe;
			parameters[14].Value = model.Id;

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
			strSql.Append("delete from AMS_SlipCustomer ");
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
			strSql.Append("delete from AMS_SlipCustomer ");
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
        public AMS.Model.AMS_SlipCustomer GetModel(string Number)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,Number,Operator,CustomerId,SlipName,ImageUrl,SlipTemplate,CouponsXml,CustomerImage,CampusNum,EffectDate,EndDate,Type,IsPrint,Describe from AMS_SlipCustomer ");
            strSql.Append(" where Number=@Number");
			SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar)
};
			parameters[0].Value = Number;

			AMS.Model.AMS_SlipCustomer model=new AMS.Model.AMS_SlipCustomer();
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
				if(ds.Tables[0].Rows[0]["Operator"]!=null && ds.Tables[0].Rows[0]["Operator"].ToString()!="")
				{
					model.Operator=int.Parse(ds.Tables[0].Rows[0]["Operator"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CustomerId"]!=null && ds.Tables[0].Rows[0]["CustomerId"].ToString()!="")
				{
					model.CustomerId=int.Parse(ds.Tables[0].Rows[0]["CustomerId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SlipName"]!=null && ds.Tables[0].Rows[0]["SlipName"].ToString()!="")
				{
					model.SlipName=ds.Tables[0].Rows[0]["SlipName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ImageUrl"]!=null && ds.Tables[0].Rows[0]["ImageUrl"].ToString()!="")
				{
					model.ImageUrl=ds.Tables[0].Rows[0]["ImageUrl"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SlipTemplate"]!=null && ds.Tables[0].Rows[0]["SlipTemplate"].ToString()!="")
				{
					model.SlipTemplate=ds.Tables[0].Rows[0]["SlipTemplate"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CouponsXml"]!=null && ds.Tables[0].Rows[0]["CouponsXml"].ToString()!="")
				{
					model.CouponsXml=ds.Tables[0].Rows[0]["CouponsXml"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CustomerImage"]!=null && ds.Tables[0].Rows[0]["CustomerImage"].ToString()!="")
				{
					model.CustomerImage=ds.Tables[0].Rows[0]["CustomerImage"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CampusNum"]!=null && ds.Tables[0].Rows[0]["CampusNum"].ToString()!="")
				{
					model.CampusNum=ds.Tables[0].Rows[0]["CampusNum"].ToString();
				}
				if(ds.Tables[0].Rows[0]["EffectDate"]!=null && ds.Tables[0].Rows[0]["EffectDate"].ToString()!="")
				{
					model.EffectDate=DateTime.Parse(ds.Tables[0].Rows[0]["EffectDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EndDate"]!=null && ds.Tables[0].Rows[0]["EndDate"].ToString()!="")
				{
					model.EndDate=DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Type"]!=null && ds.Tables[0].Rows[0]["Type"].ToString()!="")
				{
					model.Type=int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsPrint"]!=null && ds.Tables[0].Rows[0]["IsPrint"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["IsPrint"].ToString()=="1")||(ds.Tables[0].Rows[0]["IsPrint"].ToString().ToLower()=="true"))
					{
						model.IsPrint=true;
					}
					else
					{
						model.IsPrint=false;
					}
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
			strSql.Append("select Id,Number,Operator,CustomerId,SlipName,ImageUrl,SlipTemplate,CouponsXml,CustomerImage,CampusNum,EffectDate,EndDate,Type,IsPrint,Describe ");
			strSql.Append(" FROM AMS_SlipCustomer ");
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
			strSql.Append(" Id,Number,Operator,CustomerId,SlipName,ImageUrl,SlipTemplate,CouponsXml,CustomerImage,CampusNum,EffectDate,EndDate,Type,IsPrint,Describe ");
			strSql.Append(" FROM AMS_SlipCustomer ");
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
			parameters[0].Value = "AMS_SlipCustomer";
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

