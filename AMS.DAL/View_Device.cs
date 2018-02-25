using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:View_Device
	/// </summary>
	public partial class View_Device
	{
		public View_Device()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(AMS.Model.View_Device model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into View_Device(");
			strSql.Append("Number,IsDel,Flag,Describe,CaputrePath,CaputreTime,SchoolNumber,SchooName,CampusName,CampusNumber,DeviceType,CampusDescribe,ProvinceName,Id)");
			strSql.Append(" values (");
			strSql.Append("@Number,@IsDel,@Flag,@Describe,@CaputrePath,@CaputreTime,@SchoolNumber,@SchooName,@CampusName,@CampusNumber,@DeviceType,@CampusDescribe,@ProvinceName,@Id)");
			SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@IsDel", SqlDbType.Bit,1),
					new SqlParameter("@Flag", SqlDbType.Bit,1),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@CaputrePath", SqlDbType.NVarChar,100),
					new SqlParameter("@CaputreTime", SqlDbType.DateTime),
					new SqlParameter("@SchoolNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@SchooName", SqlDbType.NVarChar,30),
					new SqlParameter("@CampusName", SqlDbType.NVarChar,50),
					new SqlParameter("@CampusNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@DeviceType", SqlDbType.Int,4),
					new SqlParameter("@CampusDescribe", SqlDbType.NVarChar,500),
					new SqlParameter("@ProvinceName", SqlDbType.NVarChar,50),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.Number;
			parameters[1].Value = model.IsDel;
			parameters[2].Value = model.Flag;
			parameters[3].Value = model.Describe;
			parameters[4].Value = model.CaputrePath;
			parameters[5].Value = model.CaputreTime;
			parameters[6].Value = model.SchoolNumber;
			parameters[7].Value = model.SchooName;
			parameters[8].Value = model.CampusName;
			parameters[9].Value = model.CampusNumber;
			parameters[10].Value = model.DeviceType;
			parameters[11].Value = model.CampusDescribe;
			parameters[12].Value = model.ProvinceName;
			parameters[13].Value = model.Id;

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
		public bool Update(AMS.Model.View_Device model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update View_Device set ");
			strSql.Append("Number=@Number,");
			strSql.Append("IsDel=@IsDel,");
			strSql.Append("Flag=@Flag,");
			strSql.Append("Describe=@Describe,");
			strSql.Append("CaputrePath=@CaputrePath,");
			strSql.Append("CaputreTime=@CaputreTime,");
			strSql.Append("SchoolNumber=@SchoolNumber,");
			strSql.Append("SchooName=@SchooName,");
			strSql.Append("CampusName=@CampusName,");
			strSql.Append("CampusNumber=@CampusNumber,");
			strSql.Append("DeviceType=@DeviceType,");
			strSql.Append("CampusDescribe=@CampusDescribe,");
			strSql.Append("ProvinceName=@ProvinceName,");
			strSql.Append("Id=@Id");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@IsDel", SqlDbType.Bit,1),
					new SqlParameter("@Flag", SqlDbType.Bit,1),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@CaputrePath", SqlDbType.NVarChar,100),
					new SqlParameter("@CaputreTime", SqlDbType.DateTime),
					new SqlParameter("@SchoolNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@SchooName", SqlDbType.NVarChar,30),
					new SqlParameter("@CampusName", SqlDbType.NVarChar,50),
					new SqlParameter("@CampusNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@DeviceType", SqlDbType.Int,4),
					new SqlParameter("@CampusDescribe", SqlDbType.NVarChar,500),
					new SqlParameter("@ProvinceName", SqlDbType.NVarChar,50),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.Number;
			parameters[1].Value = model.IsDel;
			parameters[2].Value = model.Flag;
			parameters[3].Value = model.Describe;
			parameters[4].Value = model.CaputrePath;
			parameters[5].Value = model.CaputreTime;
			parameters[6].Value = model.SchoolNumber;
			parameters[7].Value = model.SchooName;
			parameters[8].Value = model.CampusName;
			parameters[9].Value = model.CampusNumber;
			parameters[10].Value = model.DeviceType;
			parameters[11].Value = model.CampusDescribe;
			parameters[12].Value = model.ProvinceName;
			parameters[13].Value = model.Id;

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
			strSql.Append("delete from View_Device ");
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
		public AMS.Model.View_Device GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Number,IsDel,Flag,Describe,CaputrePath,CaputreTime,SchoolNumber,SchooName,CampusName,CampusNumber,DeviceType,CampusDescribe,ProvinceName,Id from View_Device ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			AMS.Model.View_Device model=new AMS.Model.View_Device();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Number"]!=null && ds.Tables[0].Rows[0]["Number"].ToString()!="")
				{
					model.Number=ds.Tables[0].Rows[0]["Number"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IsDel"]!=null && ds.Tables[0].Rows[0]["IsDel"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["IsDel"].ToString()=="1")||(ds.Tables[0].Rows[0]["IsDel"].ToString().ToLower()=="true"))
					{
						model.IsDel=true;
					}
					else
					{
						model.IsDel=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Flag"]!=null && ds.Tables[0].Rows[0]["Flag"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Flag"].ToString()=="1")||(ds.Tables[0].Rows[0]["Flag"].ToString().ToLower()=="true"))
					{
						model.Flag=true;
					}
					else
					{
						model.Flag=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Describe"]!=null && ds.Tables[0].Rows[0]["Describe"].ToString()!="")
				{
					model.Describe=ds.Tables[0].Rows[0]["Describe"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CaputrePath"]!=null && ds.Tables[0].Rows[0]["CaputrePath"].ToString()!="")
				{
					model.CaputrePath=ds.Tables[0].Rows[0]["CaputrePath"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CaputreTime"]!=null && ds.Tables[0].Rows[0]["CaputreTime"].ToString()!="")
				{
					model.CaputreTime=DateTime.Parse(ds.Tables[0].Rows[0]["CaputreTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SchoolNumber"]!=null && ds.Tables[0].Rows[0]["SchoolNumber"].ToString()!="")
				{
					model.SchoolNumber=ds.Tables[0].Rows[0]["SchoolNumber"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchooName"]!=null && ds.Tables[0].Rows[0]["SchooName"].ToString()!="")
				{
					model.SchooName=ds.Tables[0].Rows[0]["SchooName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CampusName"]!=null && ds.Tables[0].Rows[0]["CampusName"].ToString()!="")
				{
					model.CampusName=ds.Tables[0].Rows[0]["CampusName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CampusNumber"]!=null && ds.Tables[0].Rows[0]["CampusNumber"].ToString()!="")
				{
					model.CampusNumber=ds.Tables[0].Rows[0]["CampusNumber"].ToString();
				}
				if(ds.Tables[0].Rows[0]["DeviceType"]!=null && ds.Tables[0].Rows[0]["DeviceType"].ToString()!="")
				{
					model.DeviceType=int.Parse(ds.Tables[0].Rows[0]["DeviceType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CampusDescribe"]!=null && ds.Tables[0].Rows[0]["CampusDescribe"].ToString()!="")
				{
					model.CampusDescribe=ds.Tables[0].Rows[0]["CampusDescribe"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ProvinceName"]!=null && ds.Tables[0].Rows[0]["ProvinceName"].ToString()!="")
				{
					model.ProvinceName=ds.Tables[0].Rows[0]["ProvinceName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Id"]!=null && ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
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
			strSql.Append("select Number,IsDel,Flag,Describe,CaputrePath,CaputreTime,SchoolNumber,SchooName,CampusName,CampusNumber,DeviceType,CampusDescribe,ProvinceName,Id,CampusId ");
			strSql.Append(" FROM View_Device ");
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
			strSql.Append(" Number,IsDel,Flag,Describe,CaputrePath,CaputreTime,SchoolNumber,SchooName,CampusName,CampusNumber,DeviceType,CampusDescribe,ProvinceName,Id ");
			strSql.Append(" FROM View_Device ");
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
			parameters[0].Value = "View_Device";
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

