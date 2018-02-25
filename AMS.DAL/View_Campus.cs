using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:View_Campus
	/// </summary>
	public partial class View_Campus
	{
		public View_Campus()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(AMS.Model.View_Campus model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into View_Campus(");
			strSql.Append("Id,Number,SchoolId,Name,Describe,SchoolNum,SchoolName,SchoolDTUIp,SchoolDescribe,SchoolConnectionString,SchoolLinkMan,SchoolCardInfo,SchoolInterfaceInfo,SchoolAddress,ProvinceName,ProvinceRemark)");
			strSql.Append(" values (");
			strSql.Append("@Id,@Number,@SchoolId,@Name,@Describe,@SchoolNum,@SchoolName,@SchoolDTUIp,@SchoolDescribe,@SchoolConnectionString,@SchoolLinkMan,@SchoolCardInfo,@SchoolInterfaceInfo,@SchoolAddress,@ProvinceName,@ProvinceRemark)");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@SchoolId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Describe", SqlDbType.NVarChar,500),
					new SqlParameter("@SchoolNum", SqlDbType.NVarChar,50),
					new SqlParameter("@SchoolName", SqlDbType.NVarChar,30),
					new SqlParameter("@SchoolDTUIp", SqlDbType.NVarChar,20),
					new SqlParameter("@SchoolDescribe", SqlDbType.NVarChar,200),
					new SqlParameter("@SchoolConnectionString", SqlDbType.NVarChar,200),
					new SqlParameter("@SchoolLinkMan", SqlDbType.NVarChar,300),
					new SqlParameter("@SchoolCardInfo", SqlDbType.Text),
					new SqlParameter("@SchoolInterfaceInfo", SqlDbType.Text),
					new SqlParameter("@SchoolAddress", SqlDbType.NVarChar,300),
					new SqlParameter("@ProvinceName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProvinceRemark", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.Number;
			parameters[2].Value = model.SchoolId;
			parameters[3].Value = model.Name;
			parameters[4].Value = model.Describe;
			parameters[5].Value = model.SchoolNum;
			parameters[6].Value = model.SchoolName;
			parameters[7].Value = model.SchoolDTUIp;
			parameters[8].Value = model.SchoolDescribe;
			parameters[9].Value = model.SchoolConnectionString;
			parameters[10].Value = model.SchoolLinkMan;
			parameters[11].Value = model.SchoolCardInfo;
			parameters[12].Value = model.SchoolInterfaceInfo;
			parameters[13].Value = model.SchoolAddress;
			parameters[14].Value = model.ProvinceName;
			parameters[15].Value = model.ProvinceRemark;

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
		public bool Update(AMS.Model.View_Campus model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update View_Campus set ");
			strSql.Append("Id=@Id,");
			strSql.Append("Number=@Number,");
			strSql.Append("SchoolId=@SchoolId,");
			strSql.Append("Name=@Name,");
			strSql.Append("Describe=@Describe,");
			strSql.Append("SchoolNum=@SchoolNum,");
			strSql.Append("SchoolName=@SchoolName,");
			strSql.Append("SchoolDTUIp=@SchoolDTUIp,");
			strSql.Append("SchoolDescribe=@SchoolDescribe,");
			strSql.Append("SchoolConnectionString=@SchoolConnectionString,");
			strSql.Append("SchoolLinkMan=@SchoolLinkMan,");
			strSql.Append("SchoolCardInfo=@SchoolCardInfo,");
			strSql.Append("SchoolInterfaceInfo=@SchoolInterfaceInfo,");
			strSql.Append("SchoolAddress=@SchoolAddress,");
			strSql.Append("ProvinceName=@ProvinceName,");
			strSql.Append("ProvinceRemark=@ProvinceRemark");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@SchoolId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Describe", SqlDbType.NVarChar,500),
					new SqlParameter("@SchoolNum", SqlDbType.NVarChar,50),
					new SqlParameter("@SchoolName", SqlDbType.NVarChar,30),
					new SqlParameter("@SchoolDTUIp", SqlDbType.NVarChar,20),
					new SqlParameter("@SchoolDescribe", SqlDbType.NVarChar,200),
					new SqlParameter("@SchoolConnectionString", SqlDbType.NVarChar,200),
					new SqlParameter("@SchoolLinkMan", SqlDbType.NVarChar,300),
					new SqlParameter("@SchoolCardInfo", SqlDbType.Text),
					new SqlParameter("@SchoolInterfaceInfo", SqlDbType.Text),
					new SqlParameter("@SchoolAddress", SqlDbType.NVarChar,300),
					new SqlParameter("@ProvinceName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProvinceRemark", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.Number;
			parameters[2].Value = model.SchoolId;
			parameters[3].Value = model.Name;
			parameters[4].Value = model.Describe;
			parameters[5].Value = model.SchoolNum;
			parameters[6].Value = model.SchoolName;
			parameters[7].Value = model.SchoolDTUIp;
			parameters[8].Value = model.SchoolDescribe;
			parameters[9].Value = model.SchoolConnectionString;
			parameters[10].Value = model.SchoolLinkMan;
			parameters[11].Value = model.SchoolCardInfo;
			parameters[12].Value = model.SchoolInterfaceInfo;
			parameters[13].Value = model.SchoolAddress;
			parameters[14].Value = model.ProvinceName;
			parameters[15].Value = model.ProvinceRemark;

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
			strSql.Append("delete from View_Campus ");
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
		public AMS.Model.View_Campus GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,Number,SchoolId,Name,Describe,SchoolNum,SchoolName,SchoolDTUIp,SchoolDescribe,SchoolConnectionString,SchoolLinkMan,SchoolCardInfo,SchoolInterfaceInfo,SchoolAddress,ProvinceName,ProvinceRemark from View_Campus ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			AMS.Model.View_Campus model=new AMS.Model.View_Campus();
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
				if(ds.Tables[0].Rows[0]["SchoolId"]!=null && ds.Tables[0].Rows[0]["SchoolId"].ToString()!="")
				{
					model.SchoolId=int.Parse(ds.Tables[0].Rows[0]["SchoolId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Name"]!=null && ds.Tables[0].Rows[0]["Name"].ToString()!="")
				{
					model.Name=ds.Tables[0].Rows[0]["Name"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Describe"]!=null && ds.Tables[0].Rows[0]["Describe"].ToString()!="")
				{
					model.Describe=ds.Tables[0].Rows[0]["Describe"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolNum"]!=null && ds.Tables[0].Rows[0]["SchoolNum"].ToString()!="")
				{
					model.SchoolNum=ds.Tables[0].Rows[0]["SchoolNum"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolName"]!=null && ds.Tables[0].Rows[0]["SchoolName"].ToString()!="")
				{
					model.SchoolName=ds.Tables[0].Rows[0]["SchoolName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolDTUIp"]!=null && ds.Tables[0].Rows[0]["SchoolDTUIp"].ToString()!="")
				{
					model.SchoolDTUIp=ds.Tables[0].Rows[0]["SchoolDTUIp"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolDescribe"]!=null && ds.Tables[0].Rows[0]["SchoolDescribe"].ToString()!="")
				{
					model.SchoolDescribe=ds.Tables[0].Rows[0]["SchoolDescribe"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolConnectionString"]!=null && ds.Tables[0].Rows[0]["SchoolConnectionString"].ToString()!="")
				{
					model.SchoolConnectionString=ds.Tables[0].Rows[0]["SchoolConnectionString"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolLinkMan"]!=null && ds.Tables[0].Rows[0]["SchoolLinkMan"].ToString()!="")
				{
					model.SchoolLinkMan=ds.Tables[0].Rows[0]["SchoolLinkMan"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolCardInfo"]!=null && ds.Tables[0].Rows[0]["SchoolCardInfo"].ToString()!="")
				{
					model.SchoolCardInfo=ds.Tables[0].Rows[0]["SchoolCardInfo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolInterfaceInfo"]!=null && ds.Tables[0].Rows[0]["SchoolInterfaceInfo"].ToString()!="")
				{
					model.SchoolInterfaceInfo=ds.Tables[0].Rows[0]["SchoolInterfaceInfo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolAddress"]!=null && ds.Tables[0].Rows[0]["SchoolAddress"].ToString()!="")
				{
					model.SchoolAddress=ds.Tables[0].Rows[0]["SchoolAddress"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ProvinceName"]!=null && ds.Tables[0].Rows[0]["ProvinceName"].ToString()!="")
				{
					model.ProvinceName=ds.Tables[0].Rows[0]["ProvinceName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ProvinceRemark"]!=null && ds.Tables[0].Rows[0]["ProvinceRemark"].ToString()!="")
				{
					model.ProvinceRemark=ds.Tables[0].Rows[0]["ProvinceRemark"].ToString();
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
			strSql.Append("select Id,Number,SchoolId,Name,Describe,SchoolNum,SchoolName,SchoolDTUIp,SchoolDescribe,SchoolConnectionString,SchoolLinkMan,SchoolCardInfo,SchoolInterfaceInfo,SchoolAddress,ProvinceName,ProvinceRemark ");
			strSql.Append(" FROM View_Campus ");
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
			strSql.Append(" Id,Number,SchoolId,Name,Describe,SchoolNum,SchoolName,SchoolDTUIp,SchoolDescribe,SchoolConnectionString,SchoolLinkMan,SchoolCardInfo,SchoolInterfaceInfo,SchoolAddress,ProvinceName,ProvinceRemark ");
			strSql.Append(" FROM View_Campus ");
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
			parameters[0].Value = "View_Campus";
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

