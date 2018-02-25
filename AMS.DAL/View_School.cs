using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:View_School
	/// </summary>
	public partial class View_School
	{
		public View_School()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(AMS.Model.View_School model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into View_School(");
			strSql.Append("Id,Number,Name,DTUip,Describe,ConnectionString,LinkMan,LinkAddress,CardInfo,InterfaceInfo,ProvinceName,ProvinceRemark)");
			strSql.Append(" values (");
			strSql.Append("@Id,@Number,@Name,@DTUip,@Describe,@ConnectionString,@LinkMan,@LinkAddress,@CardInfo,@InterfaceInfo,@ProvinceName,@ProvinceRemark)");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,30),
					new SqlParameter("@DTUip", SqlDbType.NVarChar,20),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@ConnectionString", SqlDbType.NVarChar,200),
					new SqlParameter("@LinkMan", SqlDbType.NVarChar,300),
					new SqlParameter("@LinkAddress", SqlDbType.NVarChar,300),
					new SqlParameter("@CardInfo", SqlDbType.Text),
					new SqlParameter("@InterfaceInfo", SqlDbType.Text),
					new SqlParameter("@ProvinceName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProvinceRemark", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.Number;
			parameters[2].Value = model.Name;
			parameters[3].Value = model.DTUip;
			parameters[4].Value = model.Describe;
			parameters[5].Value = model.ConnectionString;
			parameters[6].Value = model.LinkMan;
			parameters[7].Value = model.LinkAddress;
			parameters[8].Value = model.CardInfo;
			parameters[9].Value = model.InterfaceInfo;
			parameters[10].Value = model.ProvinceName;
			parameters[11].Value = model.ProvinceRemark;

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
		public bool Update(AMS.Model.View_School model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update View_School set ");
			strSql.Append("Id=@Id,");
			strSql.Append("Number=@Number,");
			strSql.Append("Name=@Name,");
			strSql.Append("DTUip=@DTUip,");
			strSql.Append("Describe=@Describe,");
			strSql.Append("ConnectionString=@ConnectionString,");
			strSql.Append("LinkMan=@LinkMan,");
			strSql.Append("LinkAddress=@LinkAddress,");
			strSql.Append("CardInfo=@CardInfo,");
			strSql.Append("InterfaceInfo=@InterfaceInfo,");
			strSql.Append("ProvinceName=@ProvinceName,");
			strSql.Append("ProvinceRemark=@ProvinceRemark");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,30),
					new SqlParameter("@DTUip", SqlDbType.NVarChar,20),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@ConnectionString", SqlDbType.NVarChar,200),
					new SqlParameter("@LinkMan", SqlDbType.NVarChar,300),
					new SqlParameter("@LinkAddress", SqlDbType.NVarChar,300),
					new SqlParameter("@CardInfo", SqlDbType.Text),
					new SqlParameter("@InterfaceInfo", SqlDbType.Text),
					new SqlParameter("@ProvinceName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProvinceRemark", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.Number;
			parameters[2].Value = model.Name;
			parameters[3].Value = model.DTUip;
			parameters[4].Value = model.Describe;
			parameters[5].Value = model.ConnectionString;
			parameters[6].Value = model.LinkMan;
			parameters[7].Value = model.LinkAddress;
			parameters[8].Value = model.CardInfo;
			parameters[9].Value = model.InterfaceInfo;
			parameters[10].Value = model.ProvinceName;
			parameters[11].Value = model.ProvinceRemark;

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
			strSql.Append("delete from View_School ");
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
		public AMS.Model.View_School GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,Number,Name,DTUip,Describe,ConnectionString,LinkMan,LinkAddress,CardInfo,InterfaceInfo,ProvinceName,ProvinceRemark from View_School ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			AMS.Model.View_School model=new AMS.Model.View_School();
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
				if(ds.Tables[0].Rows[0]["Name"]!=null && ds.Tables[0].Rows[0]["Name"].ToString()!="")
				{
					model.Name=ds.Tables[0].Rows[0]["Name"].ToString();
				}
				if(ds.Tables[0].Rows[0]["DTUip"]!=null && ds.Tables[0].Rows[0]["DTUip"].ToString()!="")
				{
					model.DTUip=ds.Tables[0].Rows[0]["DTUip"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Describe"]!=null && ds.Tables[0].Rows[0]["Describe"].ToString()!="")
				{
					model.Describe=ds.Tables[0].Rows[0]["Describe"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ConnectionString"]!=null && ds.Tables[0].Rows[0]["ConnectionString"].ToString()!="")
				{
					model.ConnectionString=ds.Tables[0].Rows[0]["ConnectionString"].ToString();
				}
				if(ds.Tables[0].Rows[0]["LinkMan"]!=null && ds.Tables[0].Rows[0]["LinkMan"].ToString()!="")
				{
					model.LinkMan=ds.Tables[0].Rows[0]["LinkMan"].ToString();
				}
				if(ds.Tables[0].Rows[0]["LinkAddress"]!=null && ds.Tables[0].Rows[0]["LinkAddress"].ToString()!="")
				{
					model.LinkAddress=ds.Tables[0].Rows[0]["LinkAddress"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CardInfo"]!=null && ds.Tables[0].Rows[0]["CardInfo"].ToString()!="")
				{
					model.CardInfo=ds.Tables[0].Rows[0]["CardInfo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["InterfaceInfo"]!=null && ds.Tables[0].Rows[0]["InterfaceInfo"].ToString()!="")
				{
					model.InterfaceInfo=ds.Tables[0].Rows[0]["InterfaceInfo"].ToString();
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
			strSql.Append("select Id,Number,Name,DTUip,Describe,ConnectionString,LinkMan,LinkAddress,CardInfo,InterfaceInfo,ProvinceName,ProvinceRemark ");
			strSql.Append(" FROM View_School ");
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
			strSql.Append(" Id,Number,Name,DTUip,Describe,ConnectionString,LinkMan,LinkAddress,CardInfo,InterfaceInfo,ProvinceName,ProvinceRemark ");
			strSql.Append(" FROM View_School ");
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
			parameters[0].Value = "View_School";
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

