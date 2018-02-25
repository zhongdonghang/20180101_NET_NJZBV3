using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:View_AuthorizeSys
	/// </summary>
	public partial class View_AuthorizeSys
	{
		public View_AuthorizeSys()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(AMS.Model.View_AuthorizeSys model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into View_AuthorizeSys(");
			strSql.Append("AuthorizeStatus,ID,InterfaceType,EffectTime,EndTime,AuthorizeCode,Describe,IsComOrSch,SchoolNumber,SchoolName,SchoolDTUip,SchoolDescribe,SchoolCon,SchoolLinkMan)");
			strSql.Append(" values (");
			strSql.Append("@AuthorizeStatus,@ID,@InterfaceType,@EffectTime,@EndTime,@AuthorizeCode,@Describe,@IsComOrSch,@SchoolNumber,@SchoolName,@SchoolDTUip,@SchoolDescribe,@SchoolCon,@SchoolLinkMan)");
			SqlParameter[] parameters = {
					new SqlParameter("@AuthorizeStatus", SqlDbType.Bit,1),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@InterfaceType", SqlDbType.NVarChar,20),
					new SqlParameter("@EffectTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@AuthorizeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@IsComOrSch", SqlDbType.Bit,1),
					new SqlParameter("@SchoolNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@SchoolName", SqlDbType.NVarChar,30),
					new SqlParameter("@SchoolDTUip", SqlDbType.NVarChar,20),
					new SqlParameter("@SchoolDescribe", SqlDbType.NVarChar,200),
					new SqlParameter("@SchoolCon", SqlDbType.NVarChar,200),
					new SqlParameter("@SchoolLinkMan", SqlDbType.NVarChar,300)};
			parameters[0].Value = model.AuthorizeStatus;
			parameters[1].Value = model.ID;
			parameters[2].Value = model.InterfaceType;
			parameters[3].Value = model.EffectTime;
			parameters[4].Value = model.EndTime;
			parameters[5].Value = model.AuthorizeCode;
			parameters[6].Value = model.Describe;
			parameters[7].Value = model.IsComOrSch;
			parameters[8].Value = model.SchoolNumber;
			parameters[9].Value = model.SchoolName;
			parameters[10].Value = model.SchoolDTUip;
			parameters[11].Value = model.SchoolDescribe;
			parameters[12].Value = model.SchoolCon;
			parameters[13].Value = model.SchoolLinkMan;

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
		public bool Update(AMS.Model.View_AuthorizeSys model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update View_AuthorizeSys set ");
			strSql.Append("AuthorizeStatus=@AuthorizeStatus,");
			strSql.Append("ID=@ID,");
			strSql.Append("InterfaceType=@InterfaceType,");
			strSql.Append("EffectTime=@EffectTime,");
			strSql.Append("EndTime=@EndTime,");
			strSql.Append("AuthorizeCode=@AuthorizeCode,");
			strSql.Append("Describe=@Describe,");
			strSql.Append("IsComOrSch=@IsComOrSch,");
			strSql.Append("SchoolNumber=@SchoolNumber,");
			strSql.Append("SchoolName=@SchoolName,");
			strSql.Append("SchoolDTUip=@SchoolDTUip,");
			strSql.Append("SchoolDescribe=@SchoolDescribe,");
			strSql.Append("SchoolCon=@SchoolCon,");
			strSql.Append("SchoolLinkMan=@SchoolLinkMan");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@AuthorizeStatus", SqlDbType.Bit,1),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@InterfaceType", SqlDbType.NVarChar,20),
					new SqlParameter("@EffectTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@AuthorizeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@IsComOrSch", SqlDbType.Bit,1),
					new SqlParameter("@SchoolNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@SchoolName", SqlDbType.NVarChar,30),
					new SqlParameter("@SchoolDTUip", SqlDbType.NVarChar,20),
					new SqlParameter("@SchoolDescribe", SqlDbType.NVarChar,200),
					new SqlParameter("@SchoolCon", SqlDbType.NVarChar,200),
					new SqlParameter("@SchoolLinkMan", SqlDbType.NVarChar,300)};
			parameters[0].Value = model.AuthorizeStatus;
			parameters[1].Value = model.ID;
			parameters[2].Value = model.InterfaceType;
			parameters[3].Value = model.EffectTime;
			parameters[4].Value = model.EndTime;
			parameters[5].Value = model.AuthorizeCode;
			parameters[6].Value = model.Describe;
			parameters[7].Value = model.IsComOrSch;
			parameters[8].Value = model.SchoolNumber;
			parameters[9].Value = model.SchoolName;
			parameters[10].Value = model.SchoolDTUip;
			parameters[11].Value = model.SchoolDescribe;
			parameters[12].Value = model.SchoolCon;
			parameters[13].Value = model.SchoolLinkMan;

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
			strSql.Append("delete from View_AuthorizeSys ");
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
		public AMS.Model.View_AuthorizeSys GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 AuthorizeStatus,ID,InterfaceType,EffectTime,EndTime,AuthorizeCode,Describe,IsComOrSch,SchoolNumber,SchoolName,SchoolDTUip,SchoolDescribe,SchoolCon,SchoolLinkMan from View_AuthorizeSys ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			AMS.Model.View_AuthorizeSys model=new AMS.Model.View_AuthorizeSys();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["AuthorizeStatus"]!=null && ds.Tables[0].Rows[0]["AuthorizeStatus"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["AuthorizeStatus"].ToString()=="1")||(ds.Tables[0].Rows[0]["AuthorizeStatus"].ToString().ToLower()=="true"))
					{
						model.AuthorizeStatus=true;
					}
					else
					{
						model.AuthorizeStatus=false;
					}
				}
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["InterfaceType"]!=null && ds.Tables[0].Rows[0]["InterfaceType"].ToString()!="")
				{
					model.InterfaceType=ds.Tables[0].Rows[0]["InterfaceType"].ToString();
				}
				if(ds.Tables[0].Rows[0]["EffectTime"]!=null && ds.Tables[0].Rows[0]["EffectTime"].ToString()!="")
				{
					model.EffectTime=DateTime.Parse(ds.Tables[0].Rows[0]["EffectTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EndTime"]!=null && ds.Tables[0].Rows[0]["EndTime"].ToString()!="")
				{
					model.EndTime=DateTime.Parse(ds.Tables[0].Rows[0]["EndTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AuthorizeCode"]!=null && ds.Tables[0].Rows[0]["AuthorizeCode"].ToString()!="")
				{
					model.AuthorizeCode=ds.Tables[0].Rows[0]["AuthorizeCode"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Describe"]!=null && ds.Tables[0].Rows[0]["Describe"].ToString()!="")
				{
					model.Describe=ds.Tables[0].Rows[0]["Describe"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IsComOrSch"]!=null && ds.Tables[0].Rows[0]["IsComOrSch"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["IsComOrSch"].ToString()=="1")||(ds.Tables[0].Rows[0]["IsComOrSch"].ToString().ToLower()=="true"))
					{
						model.IsComOrSch=true;
					}
					else
					{
						model.IsComOrSch=false;
					}
				}
				if(ds.Tables[0].Rows[0]["SchoolNumber"]!=null && ds.Tables[0].Rows[0]["SchoolNumber"].ToString()!="")
				{
					model.SchoolNumber=ds.Tables[0].Rows[0]["SchoolNumber"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolName"]!=null && ds.Tables[0].Rows[0]["SchoolName"].ToString()!="")
				{
					model.SchoolName=ds.Tables[0].Rows[0]["SchoolName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolDTUip"]!=null && ds.Tables[0].Rows[0]["SchoolDTUip"].ToString()!="")
				{
					model.SchoolDTUip=ds.Tables[0].Rows[0]["SchoolDTUip"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolDescribe"]!=null && ds.Tables[0].Rows[0]["SchoolDescribe"].ToString()!="")
				{
					model.SchoolDescribe=ds.Tables[0].Rows[0]["SchoolDescribe"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolCon"]!=null && ds.Tables[0].Rows[0]["SchoolCon"].ToString()!="")
				{
					model.SchoolCon=ds.Tables[0].Rows[0]["SchoolCon"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolLinkMan"]!=null && ds.Tables[0].Rows[0]["SchoolLinkMan"].ToString()!="")
				{
					model.SchoolLinkMan=ds.Tables[0].Rows[0]["SchoolLinkMan"].ToString();
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
			strSql.Append("select AuthorizeStatus,ID,InterfaceType,EffectTime,EndTime,AuthorizeCode,Describe,IsComOrSch,SchoolNumber,SchoolName,SchoolDTUip,SchoolDescribe,SchoolCon,SchoolLinkMan ");
			strSql.Append(" FROM View_AuthorizeSys ");
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
			strSql.Append(" AuthorizeStatus,ID,InterfaceType,EffectTime,EndTime,AuthorizeCode,Describe,IsComOrSch,SchoolNumber,SchoolName,SchoolDTUip,SchoolDescribe,SchoolCon,SchoolLinkMan ");
			strSql.Append(" FROM View_AuthorizeSys ");
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
			parameters[0].Value = "View_AuthorizeSys";
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

