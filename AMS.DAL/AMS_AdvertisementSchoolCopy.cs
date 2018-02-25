using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:AMS_AdvertisementSchoolCopy
	/// </summary>
	public partial class AMS_AdvertisementSchoolCopy
	{
		public AMS_AdvertisementSchoolCopy()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "AMS_AdvertisementSchoolCopy"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AMS_AdvertisementSchoolCopy");
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
		public int Add(AMS.Model.AMS_AdvertisementSchoolCopy model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AMS_AdvertisementSchoolCopy(");
			strSql.Append("Num,Name,EffectDate,EndDate,OperatorName,CustomerID,Type,AdContent,SchoolID,OriginalID,IsNew)");
			strSql.Append(" values (");
			strSql.Append("@Num,@Name,@EffectDate,@EndDate,@OperatorName,@CustomerID,@Type,@AdContent,@SchoolID,@OriginalID,@IsNew)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Num", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@OperatorName", SqlDbType.Int,4),
					new SqlParameter("@CustomerID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@AdContent", SqlDbType.Text),
					new SqlParameter("@SchoolID", SqlDbType.Int,4),
					new SqlParameter("@OriginalID", SqlDbType.Int,4),
					new SqlParameter("@IsNew", SqlDbType.Bit,1)};
			parameters[0].Value = model.Num;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.EffectDate;
			parameters[3].Value = model.EndDate;
			parameters[4].Value = model.OperatorID;
			parameters[5].Value = model.CustomerID;
			parameters[6].Value = (int)model.Type;
			parameters[7].Value = model.AdContent;
			parameters[8].Value = model.SchoolID;
			parameters[9].Value = model.OriginalID;
			parameters[10].Value = model.IsNew;

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
		public bool Update(AMS.Model.AMS_AdvertisementSchoolCopy model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AMS_AdvertisementSchoolCopy set ");
			strSql.Append("Num=@Num,");
			strSql.Append("Name=@Name,");
			strSql.Append("EffectDate=@EffectDate,");
			strSql.Append("EndDate=@EndDate,");
			strSql.Append("OperatorName=@OperatorName,");
			strSql.Append("CustomerID=@CustomerID,");
			strSql.Append("Type=@Type,");
			strSql.Append("AdContent=@AdContent,");
			strSql.Append("SchoolID=@SchoolID,");
			strSql.Append("OriginalID=@OriginalID,");
			strSql.Append("IsNew=@IsNew");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@Num", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@OperatorName", SqlDbType.Int,4),
					new SqlParameter("@CustomerID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@AdContent", SqlDbType.Text),
					new SqlParameter("@SchoolID", SqlDbType.Int,4),
					new SqlParameter("@OriginalID", SqlDbType.Int,4),
					new SqlParameter("@IsNew", SqlDbType.Bit,1),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.Num;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.EffectDate;
			parameters[3].Value = model.EndDate;
			parameters[4].Value = model.OperatorID;
			parameters[5].Value = model.CustomerID;
			parameters[6].Value = (int)model.Type;
			parameters[7].Value = model.AdContent;
			parameters[8].Value = model.SchoolID;
			parameters[9].Value = model.OriginalID;
			parameters[10].Value = model.IsNew;
			parameters[11].Value = model.ID;

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
			strSql.Append("delete from AMS_AdvertisementSchoolCopy ");
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
			strSql.Append("delete from AMS_AdvertisementSchoolCopy ");
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
		public AMS.Model.AMS_AdvertisementSchoolCopy GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,Num,Name,EffectDate,EndDate,OperatorName,CustomerID,Type,AdContent,SchoolID,OriginalID,IsNew from AMS_AdvertisementSchoolCopy ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
			parameters[0].Value = ID;

			AMS.Model.AMS_AdvertisementSchoolCopy model=new AMS.Model.AMS_AdvertisementSchoolCopy();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Num"]!=null && ds.Tables[0].Rows[0]["Num"].ToString()!="")
				{
					model.Num=ds.Tables[0].Rows[0]["Num"].ToString();
				}
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
				if(ds.Tables[0].Rows[0]["OperatorName"]!=null && ds.Tables[0].Rows[0]["OperatorName"].ToString()!="")
				{
					model.OperatorID=int.Parse(ds.Tables[0].Rows[0]["OperatorName"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CustomerID"]!=null && ds.Tables[0].Rows[0]["CustomerID"].ToString()!="")
				{
					model.CustomerID=int.Parse(ds.Tables[0].Rows[0]["CustomerID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Type"]!=null && ds.Tables[0].Rows[0]["Type"].ToString()!="")
				{
					model.Type=(AMS.Model.Enum.AdType)int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AdContent"]!=null && ds.Tables[0].Rows[0]["AdContent"].ToString()!="")
				{
					model.AdContent=ds.Tables[0].Rows[0]["AdContent"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolID"]!=null && ds.Tables[0].Rows[0]["SchoolID"].ToString()!="")
				{
					model.SchoolID=int.Parse(ds.Tables[0].Rows[0]["SchoolID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OriginalID"]!=null && ds.Tables[0].Rows[0]["OriginalID"].ToString()!="")
				{
					model.OriginalID=int.Parse(ds.Tables[0].Rows[0]["OriginalID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsNew"]!=null && ds.Tables[0].Rows[0]["IsNew"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["IsNew"].ToString()=="1")||(ds.Tables[0].Rows[0]["IsNew"].ToString().ToLower()=="true"))
					{
						model.IsNew=true;
					}
					else
					{
						model.IsNew=false;
					}
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
			strSql.Append("select ID,Num,Name,EffectDate,EndDate,OperatorName,CustomerID,Type,AdContent,SchoolID,OriginalID,IsNew ");
			strSql.Append(" FROM AMS_AdvertisementSchoolCopy ");
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
			strSql.Append(" ID,Num,Name,EffectDate,EndDate,OperatorName,CustomerID,Type,AdContent,SchoolID,OriginalID,IsNew ");
			strSql.Append(" FROM AMS_AdvertisementSchoolCopy ");
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
			parameters[0].Value = "AMS_AdvertisementSchoolCopy";
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

