using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:AMS_Device
	/// </summary>
	public partial class AMS_Device
	{
		public AMS_Device()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "AMS_Device"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AMS_Device");
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
		public int Add(AMS.Model.AMS_Device model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AMS_Device(");
			strSql.Append("Number,DeviceType,CampusId,IsDel,Flag,Describe,CaputrePath,CaputreTime)");
			strSql.Append(" values (");
			strSql.Append("@Number,@DeviceType,@CampusId,@IsDel,@Flag,@Describe,@CaputrePath,@CaputreTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@DeviceType", SqlDbType.Int,4),
					new SqlParameter("@CampusId", SqlDbType.Int,4),
					new SqlParameter("@IsDel", SqlDbType.Bit,1),
					new SqlParameter("@Flag", SqlDbType.Bit,1),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@CaputrePath", SqlDbType.NVarChar,100),
					new SqlParameter("@CaputreTime", SqlDbType.DateTime)};
			parameters[0].Value = model.Number;
			parameters[1].Value = model.DeviceType;
			parameters[2].Value = model.CampusId;
			parameters[3].Value = model.IsDel;
			parameters[4].Value = model.Flag;
			parameters[5].Value = model.Describe;
			parameters[6].Value = model.CaputrePath;
			parameters[7].Value = model.CaputreTime;

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
		public bool Update(AMS.Model.AMS_Device model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AMS_Device set ");
			strSql.Append("Number=@Number,");
			strSql.Append("DeviceType=@DeviceType,");
			strSql.Append("CampusId=@CampusId,");
			strSql.Append("IsDel=@IsDel,");
			strSql.Append("Flag=@Flag,");
			strSql.Append("Describe=@Describe,");
			strSql.Append("CaputrePath=@CaputrePath,");
			strSql.Append("CaputreTime=@CaputreTime");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@DeviceType", SqlDbType.Int,4),
					new SqlParameter("@CampusId", SqlDbType.Int,4),
					new SqlParameter("@IsDel", SqlDbType.Bit,1),
					new SqlParameter("@Flag", SqlDbType.Bit,1),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@CaputrePath", SqlDbType.NVarChar,100),
					new SqlParameter("@CaputreTime", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.Number;
			parameters[1].Value = model.DeviceType;
			parameters[2].Value = model.CampusId;
			parameters[3].Value = model.IsDel;
			parameters[4].Value = model.Flag;
			parameters[5].Value = model.Describe;
			parameters[6].Value = model.CaputrePath;
			parameters[7].Value = model.CaputreTime;
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
			strSql.Append("delete from AMS_Device ");
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
			strSql.Append("delete from AMS_Device ");
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
		public AMS.Model.AMS_Device GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,Number,DeviceType,CampusId,IsDel,Flag,Describe,CaputrePath,CaputreTime from AMS_Device ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
};
			parameters[0].Value = Id;

			AMS.Model.AMS_Device model=new AMS.Model.AMS_Device();
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
				if(ds.Tables[0].Rows[0]["DeviceType"]!=null && ds.Tables[0].Rows[0]["DeviceType"].ToString()!="")
				{
					model.DeviceType=int.Parse(ds.Tables[0].Rows[0]["DeviceType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CampusId"]!=null && ds.Tables[0].Rows[0]["CampusId"].ToString()!="")
				{
					model.CampusId=int.Parse(ds.Tables[0].Rows[0]["CampusId"].ToString());
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
				return model;
			}
			else
			{
				return null;
			}
		}
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public AMS.Model.AMS_Device GetModel(string strWhere)
        { 
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,Number,DeviceType,CampusId,IsDel,Flag,Describe,CaputrePath,CaputreTime from AMS_Device ");
            if (!string.IsNullOrEmpty(strWhere) && strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }  
            AMS.Model.AMS_Device model = new AMS.Model.AMS_Device();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"] != null && ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Number"] != null && ds.Tables[0].Rows[0]["Number"].ToString() != "")
                {
                    model.Number = ds.Tables[0].Rows[0]["Number"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DeviceType"] != null && ds.Tables[0].Rows[0]["DeviceType"].ToString() != "")
                {
                    model.DeviceType = int.Parse(ds.Tables[0].Rows[0]["DeviceType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CampusId"] != null && ds.Tables[0].Rows[0]["CampusId"].ToString() != "")
                {
                    model.CampusId = int.Parse(ds.Tables[0].Rows[0]["CampusId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsDel"] != null && ds.Tables[0].Rows[0]["IsDel"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsDel"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsDel"].ToString().ToLower() == "true"))
                    {
                        model.IsDel = true;
                    }
                    else
                    {
                        model.IsDel = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Flag"] != null && ds.Tables[0].Rows[0]["Flag"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Flag"].ToString() == "1") || (ds.Tables[0].Rows[0]["Flag"].ToString().ToLower() == "true"))
                    {
                        model.Flag = true;
                    }
                    else
                    {
                        model.Flag = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Describe"] != null && ds.Tables[0].Rows[0]["Describe"].ToString() != "")
                {
                    model.Describe = ds.Tables[0].Rows[0]["Describe"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CaputrePath"] != null && ds.Tables[0].Rows[0]["CaputrePath"].ToString() != "")
                {
                    model.CaputrePath = ds.Tables[0].Rows[0]["CaputrePath"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CaputreTime"] != null && ds.Tables[0].Rows[0]["CaputreTime"].ToString() != "")
                {
                    model.CaputreTime = DateTime.Parse(ds.Tables[0].Rows[0]["CaputreTime"].ToString());
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
			strSql.Append("select Id,Number,DeviceType,CampusId,IsDel,Flag,Describe,CaputrePath,CaputreTime ");
			strSql.Append(" FROM AMS_Device ");
			if(!string.IsNullOrEmpty(strWhere) && strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" order by Number");
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
			strSql.Append(" Id,Number,DeviceType,CampusId,IsDel,Flag,Describe,CaputrePath,CaputreTime ");
			strSql.Append(" FROM AMS_Device ");
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
			parameters[0].Value = "AMS_Device";
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

