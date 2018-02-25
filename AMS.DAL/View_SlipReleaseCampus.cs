using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:View_SlipReleaseCampus
	/// </summary>
	public partial class View_SlipReleaseCampus
	{
		public View_SlipReleaseCampus()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(AMS.Model.View_SlipReleaseCampus model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into View_SlipReleaseCampus(");
			strSql.Append("id,SchoolNum,SchoolName,CampusId,CampusNum,CampusName,SlipCustomerId,SlipCustomerNum,SlipTemplate,IsPrint,SlipType,SlipEndDate,SlipEffectDate,CustomerImage,SlipImageUrl,SlipName,CouponsXml,SlipCustomerDescribe)");
			strSql.Append(" values (");
			strSql.Append("@id,@SchoolNum,@SchoolName,@CampusId,@CampusNum,@CampusName,@SlipCustomerId,@SlipCustomerNum,@SlipTemplate,@IsPrint,@SlipType,@SlipEndDate,@SlipEffectDate,@CustomerImage,@SlipImageUrl,@SlipName,@CouponsXml,@SlipCustomerDescribe)");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@SchoolNum", SqlDbType.NVarChar,50),
					new SqlParameter("@SchoolName", SqlDbType.NVarChar,30),
					new SqlParameter("@CampusId", SqlDbType.Int,4),
					new SqlParameter("@CampusNum", SqlDbType.NVarChar,50),
					new SqlParameter("@CampusName", SqlDbType.NVarChar,50),
					new SqlParameter("@SlipCustomerId", SqlDbType.Int,4),
					new SqlParameter("@SlipCustomerNum", SqlDbType.NVarChar,50),
					new SqlParameter("@SlipTemplate", SqlDbType.Text),
					new SqlParameter("@IsPrint", SqlDbType.Bit,1),
					new SqlParameter("@SlipType", SqlDbType.Int,4),
					new SqlParameter("@SlipEndDate", SqlDbType.DateTime),
					new SqlParameter("@SlipEffectDate", SqlDbType.DateTime),
					new SqlParameter("@CustomerImage", SqlDbType.NVarChar,200),
					new SqlParameter("@SlipImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@SlipName", SqlDbType.NVarChar,50),
					new SqlParameter("@CouponsXml", SqlDbType.Text),
					new SqlParameter("@SlipCustomerDescribe", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.SchoolNum;
			parameters[2].Value = model.SchoolName;
			parameters[3].Value = model.CampusId;
			parameters[4].Value = model.CampusNum;
			parameters[5].Value = model.CampusName;
			parameters[6].Value = model.SlipCustomerId;
			parameters[7].Value = model.SlipCustomerNum;
			parameters[8].Value = model.SlipTemplate;
			parameters[9].Value = model.IsPrint;
			parameters[10].Value = model.SlipType;
			parameters[11].Value = model.SlipEndDate;
			parameters[12].Value = model.SlipEffectDate;
			parameters[13].Value = model.CustomerImage;
			parameters[14].Value = model.SlipImageUrl;
			parameters[15].Value = model.SlipName;
			parameters[16].Value = model.CouponsXml;
			parameters[17].Value = model.SlipCustomerDescribe;

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
		public bool Update(AMS.Model.View_SlipReleaseCampus model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update View_SlipReleaseCampus set ");
			strSql.Append("id=@id,");
			strSql.Append("SchoolNum=@SchoolNum,");
			strSql.Append("SchoolName=@SchoolName,");
			strSql.Append("CampusId=@CampusId,");
			strSql.Append("CampusNum=@CampusNum,");
			strSql.Append("CampusName=@CampusName,");
			strSql.Append("SlipCustomerId=@SlipCustomerId,");
			strSql.Append("SlipCustomerNum=@SlipCustomerNum,");
			strSql.Append("SlipTemplate=@SlipTemplate,");
			strSql.Append("IsPrint=@IsPrint,");
			strSql.Append("SlipType=@SlipType,");
			strSql.Append("SlipEndDate=@SlipEndDate,");
			strSql.Append("SlipEffectDate=@SlipEffectDate,");
			strSql.Append("CustomerImage=@CustomerImage,");
			strSql.Append("SlipImageUrl=@SlipImageUrl,");
			strSql.Append("SlipName=@SlipName,");
			strSql.Append("CouponsXml=@CouponsXml,");
			strSql.Append("SlipCustomerDescribe=@SlipCustomerDescribe");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@SchoolNum", SqlDbType.NVarChar,50),
					new SqlParameter("@SchoolName", SqlDbType.NVarChar,30),
					new SqlParameter("@CampusId", SqlDbType.Int,4),
					new SqlParameter("@CampusNum", SqlDbType.NVarChar,50),
					new SqlParameter("@CampusName", SqlDbType.NVarChar,50),
					new SqlParameter("@SlipCustomerId", SqlDbType.Int,4),
					new SqlParameter("@SlipCustomerNum", SqlDbType.NVarChar,50),
					new SqlParameter("@SlipTemplate", SqlDbType.Text),
					new SqlParameter("@IsPrint", SqlDbType.Bit,1),
					new SqlParameter("@SlipType", SqlDbType.Int,4),
					new SqlParameter("@SlipEndDate", SqlDbType.DateTime),
					new SqlParameter("@SlipEffectDate", SqlDbType.DateTime),
					new SqlParameter("@CustomerImage", SqlDbType.NVarChar,200),
					new SqlParameter("@SlipImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@SlipName", SqlDbType.NVarChar,50),
					new SqlParameter("@CouponsXml", SqlDbType.Text),
					new SqlParameter("@SlipCustomerDescribe", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.SchoolNum;
			parameters[2].Value = model.SchoolName;
			parameters[3].Value = model.CampusId;
			parameters[4].Value = model.CampusNum;
			parameters[5].Value = model.CampusName;
			parameters[6].Value = model.SlipCustomerId;
			parameters[7].Value = model.SlipCustomerNum;
			parameters[8].Value = model.SlipTemplate;
			parameters[9].Value = model.IsPrint;
			parameters[10].Value = model.SlipType;
			parameters[11].Value = model.SlipEndDate;
			parameters[12].Value = model.SlipEffectDate;
			parameters[13].Value = model.CustomerImage;
			parameters[14].Value = model.SlipImageUrl;
			parameters[15].Value = model.SlipName;
			parameters[16].Value = model.CouponsXml;
			parameters[17].Value = model.SlipCustomerDescribe;

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
			strSql.Append("delete from View_SlipReleaseCampus ");
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
		public AMS.Model.View_SlipReleaseCampus GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,SchoolNum,SchoolName,CampusId,CampusNum,CampusName,SlipCustomerId,SlipCustomerNum,SlipTemplate,IsPrint,SlipType,SlipEndDate,SlipEffectDate,CustomerImage,SlipImageUrl,SlipName,CouponsXml,SlipCustomerDescribe from View_SlipReleaseCampus ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			AMS.Model.View_SlipReleaseCampus model=new AMS.Model.View_SlipReleaseCampus();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"]!=null && ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SchoolNum"]!=null && ds.Tables[0].Rows[0]["SchoolNum"].ToString()!="")
				{
					model.SchoolNum=ds.Tables[0].Rows[0]["SchoolNum"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolName"]!=null && ds.Tables[0].Rows[0]["SchoolName"].ToString()!="")
				{
					model.SchoolName=ds.Tables[0].Rows[0]["SchoolName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CampusId"]!=null && ds.Tables[0].Rows[0]["CampusId"].ToString()!="")
				{
					model.CampusId=int.Parse(ds.Tables[0].Rows[0]["CampusId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CampusNum"]!=null && ds.Tables[0].Rows[0]["CampusNum"].ToString()!="")
				{
					model.CampusNum=ds.Tables[0].Rows[0]["CampusNum"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CampusName"]!=null && ds.Tables[0].Rows[0]["CampusName"].ToString()!="")
				{
					model.CampusName=ds.Tables[0].Rows[0]["CampusName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SlipCustomerId"]!=null && ds.Tables[0].Rows[0]["SlipCustomerId"].ToString()!="")
				{
					model.SlipCustomerId=int.Parse(ds.Tables[0].Rows[0]["SlipCustomerId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SlipCustomerNum"]!=null && ds.Tables[0].Rows[0]["SlipCustomerNum"].ToString()!="")
				{
					model.SlipCustomerNum=ds.Tables[0].Rows[0]["SlipCustomerNum"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SlipTemplate"]!=null && ds.Tables[0].Rows[0]["SlipTemplate"].ToString()!="")
				{
					model.SlipTemplate=ds.Tables[0].Rows[0]["SlipTemplate"].ToString();
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
				if(ds.Tables[0].Rows[0]["SlipType"]!=null && ds.Tables[0].Rows[0]["SlipType"].ToString()!="")
				{
					model.SlipType=int.Parse(ds.Tables[0].Rows[0]["SlipType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SlipEndDate"]!=null && ds.Tables[0].Rows[0]["SlipEndDate"].ToString()!="")
				{
					model.SlipEndDate=DateTime.Parse(ds.Tables[0].Rows[0]["SlipEndDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SlipEffectDate"]!=null && ds.Tables[0].Rows[0]["SlipEffectDate"].ToString()!="")
				{
					model.SlipEffectDate=DateTime.Parse(ds.Tables[0].Rows[0]["SlipEffectDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CustomerImage"]!=null && ds.Tables[0].Rows[0]["CustomerImage"].ToString()!="")
				{
					model.CustomerImage=ds.Tables[0].Rows[0]["CustomerImage"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SlipImageUrl"]!=null && ds.Tables[0].Rows[0]["SlipImageUrl"].ToString()!="")
				{
					model.SlipImageUrl=ds.Tables[0].Rows[0]["SlipImageUrl"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SlipName"]!=null && ds.Tables[0].Rows[0]["SlipName"].ToString()!="")
				{
					model.SlipName=ds.Tables[0].Rows[0]["SlipName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CouponsXml"]!=null && ds.Tables[0].Rows[0]["CouponsXml"].ToString()!="")
				{
					model.CouponsXml=ds.Tables[0].Rows[0]["CouponsXml"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SlipCustomerDescribe"]!=null && ds.Tables[0].Rows[0]["SlipCustomerDescribe"].ToString()!="")
				{
					model.SlipCustomerDescribe=ds.Tables[0].Rows[0]["SlipCustomerDescribe"].ToString();
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
            strSql.Append("select [id],[SchoolId],[SchoolNum],SlipName,[SchoolName],[CampusId],[CampusNum],[CampusName],[SlipCustomerId],[SlipCustomerNum] ,[SlipTemplate],[IsPrint],[LookOverAmount],[PrintAmount],[Type],[EndDate] ,[EffectDate],[CustomerImage],[ImageUrl] ");
			strSql.Append(" FROM View_SlipReleaseCampus ");
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
			strSql.Append(" id,SchoolNum,SchoolName,CampusId,CampusNum,CampusName,SlipCustomerId,SlipCustomerNum,SlipTemplate,IsPrint,SlipType,SlipEndDate,SlipEffectDate,CustomerImage,SlipImageUrl,SlipName,CouponsXml,SlipCustomerDescribe ");
			strSql.Append(" FROM View_SlipReleaseCampus ");
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
			parameters[0].Value = "View_SlipReleaseCampus";
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

