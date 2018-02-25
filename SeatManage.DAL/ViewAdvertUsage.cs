using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace SeatManage.DAL
{
	/// <summary>
	/// 数据访问类:ViewAdvertUsage
	/// </summary>
	public partial class ViewAdvertUsage
	{
		public ViewAdvertUsage()
		{}
		#region  Method



//        /// <summary>
//        /// 增加一条数据
//        /// </summary>
//        public bool Add(Maticsoft.Model.ViewAdvertUsage model)
//        {
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("insert into ViewAdvertUsage(");
//            strSql.Append("ID,AdvertID,AdvertUsage,LastUpdateTime,AdvertType,AdvertNum)");
//            strSql.Append(" values (");
//            strSql.Append("@ID,@AdvertID,@AdvertUsage,@LastUpdateTime,@AdvertType,@AdvertNum)");
//            SqlParameter[] parameters = {
//                    new SqlParameter("@ID", SqlDbType.Int,4),
//                    new SqlParameter("@AdvertID", SqlDbType.Int,4),
//                    new SqlParameter("@AdvertUsage", SqlDbType.Text),
//                    new SqlParameter("@LastUpdateTime", SqlDbType.DateTime),
//                    new SqlParameter("@AdvertType", SqlDbType.Int,4),
//                    new SqlParameter("@AdvertNum", SqlDbType.NVarChar,50)};
//            parameters[0].Value = model.ID;
//            parameters[1].Value = model.AdvertID;
//            parameters[2].Value = model.AdvertUsage;
//            parameters[3].Value = model.LastUpdateTime;
//            parameters[4].Value = model.AdvertType;
//            parameters[5].Value = model.AdvertNum;

//            int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
//            if (rows > 0)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }
//        /// <summary>
//        /// 更新一条数据
//        /// </summary>
//        public bool Update(Maticsoft.Model.ViewAdvertUsage model)
//        {
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("update ViewAdvertUsage set ");
//            strSql.Append("ID=@ID,");
//            strSql.Append("AdvertID=@AdvertID,");
//            strSql.Append("AdvertUsage=@AdvertUsage,");
//            strSql.Append("LastUpdateTime=@LastUpdateTime,");
//            strSql.Append("AdvertType=@AdvertType,");
//            strSql.Append("AdvertNum=@AdvertNum");
//            strSql.Append(" where ");
//            SqlParameter[] parameters = {
//                    new SqlParameter("@ID", SqlDbType.Int,4),
//                    new SqlParameter("@AdvertID", SqlDbType.Int,4),
//                    new SqlParameter("@AdvertUsage", SqlDbType.Text),
//                    new SqlParameter("@LastUpdateTime", SqlDbType.DateTime),
//                    new SqlParameter("@AdvertType", SqlDbType.Int,4),
//                    new SqlParameter("@AdvertNum", SqlDbType.NVarChar,50)};
//            parameters[0].Value = model.ID;
//            parameters[1].Value = model.AdvertID;
//            parameters[2].Value = model.AdvertUsage;
//            parameters[3].Value = model.LastUpdateTime;
//            parameters[4].Value = model.AdvertType;
//            parameters[5].Value = model.AdvertNum;

//            int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
//            if (rows > 0)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        /// <summary>
//        /// 删除一条数据
//        /// </summary>
//        public bool Delete()
//        {
//            //该表无主键信息，请自定义主键/条件字段
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("delete from ViewAdvertUsage ");
//            strSql.Append(" where ");
//            SqlParameter[] parameters = {
//};

//            int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
//            if (rows > 0)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SeatManage.ClassModel.AMS_AdvertUsage GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,AdvertID,AdvertUsage,LastUpdateTime,AdvertType,AdvertNum from ViewAdvertUsage ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

            SeatManage.ClassModel.AMS_AdvertUsage model = new SeatManage.ClassModel.AMS_AdvertUsage();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AdvertID"]!=null && ds.Tables[0].Rows[0]["AdvertID"].ToString()!="")
				{
					model.AdvertID=int.Parse(ds.Tables[0].Rows[0]["AdvertID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AdvertUsage"]!=null && ds.Tables[0].Rows[0]["AdvertUsage"].ToString()!="")
				{
					model.AdvertUsage=ds.Tables[0].Rows[0]["AdvertUsage"].ToString();
				}
				if(ds.Tables[0].Rows[0]["LastUpdateTime"]!=null && ds.Tables[0].Rows[0]["LastUpdateTime"].ToString()!="")
				{
					model.LastUpdateTime=DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AdvertType"]!=null && ds.Tables[0].Rows[0]["AdvertType"].ToString()!="")
				{
                    model.AdvertType = (SeatManage.EnumType.AdType)int.Parse(ds.Tables[0].Rows[0]["AdvertType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AdvertNum"]!=null && ds.Tables[0].Rows[0]["AdvertNum"].ToString()!="")
				{
					model.AdvertNum=ds.Tables[0].Rows[0]["AdvertNum"].ToString();
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
			strSql.Append("select ID,AdvertID,AdvertUsage,LastUpdateTime,AdvertType,AdvertNum ");
			strSql.Append(" FROM ViewAdvertUsage ");
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
			strSql.Append(" ID,AdvertID,AdvertUsage,LastUpdateTime,AdvertType,AdvertNum ");
			strSql.Append(" FROM ViewAdvertUsage ");
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
			parameters[0].Value = "ViewAdvertUsage";
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

