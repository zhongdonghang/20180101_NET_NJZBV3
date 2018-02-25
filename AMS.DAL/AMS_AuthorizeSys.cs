using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:AMS_AuthorizeSys
	/// </summary>
	public partial class AMS_AuthorizeSys
	{
		public AMS_AuthorizeSys()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "AMS_AuthorizeSys"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AMS_AuthorizeSys");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(AMS.Model.AMS_AuthorizeSys model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AMS_AuthorizeSys(");
			strSql.Append("SchoolId,AuthorizeStatus,InterfaceType,EffectTime,EndTime,AuthorizeCode,IsComOrSch,Describe)");
			strSql.Append(" values (");
			strSql.Append("@SchoolId,@AuthorizeStatus,@InterfaceType,@EffectTime,@EndTime,@AuthorizeCode,@IsComOrSch,@Describe)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@SchoolId", SqlDbType.Int,4),
					new SqlParameter("@AuthorizeStatus", SqlDbType.Bit,1),
					new SqlParameter("@InterfaceType", SqlDbType.NVarChar,20),
					new SqlParameter("@EffectTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@AuthorizeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@IsComOrSch", SqlDbType.Bit,1),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.SchoolId;
			parameters[1].Value = model.AuthorizeStatus;
			parameters[2].Value = model.InterfaceType;
			parameters[3].Value = model.EffectTime;
			parameters[4].Value = model.EndTime;
			parameters[5].Value = model.AuthorizeCode;
			parameters[6].Value = model.IsComOrSch;
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
		public bool Update(AMS.Model.AMS_AuthorizeSys model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AMS_AuthorizeSys set ");
			strSql.Append("SchoolId=@SchoolId,");
			strSql.Append("AuthorizeStatus=@AuthorizeStatus,");
			strSql.Append("InterfaceType=@InterfaceType,");
			strSql.Append("EffectTime=@EffectTime,");
			strSql.Append("EndTime=@EndTime,");
			strSql.Append("AuthorizeCode=@AuthorizeCode,");
			strSql.Append("IsComOrSch=@IsComOrSch,");
			strSql.Append("Describe=@Describe");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@SchoolId", SqlDbType.Int,4),
					new SqlParameter("@AuthorizeStatus", SqlDbType.Bit,1),
					new SqlParameter("@InterfaceType", SqlDbType.NVarChar,20),
					new SqlParameter("@EffectTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@AuthorizeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@IsComOrSch", SqlDbType.Bit,1),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.SchoolId;
			parameters[1].Value = model.AuthorizeStatus;
			parameters[2].Value = model.InterfaceType;
			parameters[3].Value = model.EffectTime;
			parameters[4].Value = model.EndTime;
			parameters[5].Value = model.AuthorizeCode;
			parameters[6].Value = model.IsComOrSch;
			parameters[7].Value = model.Describe;
			parameters[8].Value = model.ID;

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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from AMS_AuthorizeSys ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
			parameters[0].Value = ID;

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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from AMS_AuthorizeSys ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
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
		public AMS.Model.AMS_AuthorizeSys GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,SchoolId,AuthorizeStatus,InterfaceType,EffectTime,EndTime,AuthorizeCode,IsComOrSch,Describe from AMS_AuthorizeSys ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
			parameters[0].Value = ID;

			AMS.Model.AMS_AuthorizeSys model=new AMS.Model.AMS_AuthorizeSys();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SchoolId"]!=null && ds.Tables[0].Rows[0]["SchoolId"].ToString()!="")
				{
					model.SchoolId=int.Parse(ds.Tables[0].Rows[0]["SchoolId"].ToString());
				}
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
			strSql.Append("select ID,SchoolId,AuthorizeStatus,InterfaceType,EffectTime,EndTime,AuthorizeCode,IsComOrSch,Describe ");
			strSql.Append(" FROM AMS_AuthorizeSys ");
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
			strSql.Append(" ID,SchoolId,AuthorizeStatus,InterfaceType,EffectTime,EndTime,AuthorizeCode,IsComOrSch,Describe ");
			strSql.Append(" FROM AMS_AuthorizeSys ");
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
			parameters[0].Value = "AMS_AuthorizeSys";
			parameters[1].Value = "ID";
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

