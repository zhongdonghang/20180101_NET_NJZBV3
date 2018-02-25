using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
namespace SeatManage.DAL
{
	/// <summary>
	/// 数据访问类:AMS_PlayList
	/// </summary>
	public partial class AMS_PlayList
	{
		public AMS_PlayList()
		{}
		#region  Method

		 
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere,SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Number,CampusNum,PlayList,ReleaseDate,EffectDate,EndDate ");
            strSql.Append(" FROM AMS_PlayList ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder,SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Number,CampusNum,PlayList,ReleaseDate,EffectDate,EndDate ");
            strSql.Append(" FROM AMS_PlayList ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SeatManage.ClassModel.AMS_PlayList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AMS_PlayList(");
            strSql.Append("Number,CampusNum,PlayList,ReleaseDate,EffectDate,EndDate)");
            strSql.Append(" values (");
            strSql.Append("@Number,@CampusNum,@PlayList,@ReleaseDate,@EffectDate,@EndDate)");
            SqlParameter[] parameters = {
                    new SqlParameter("@Number", SqlDbType.NVarChar,50),
                    new SqlParameter("@CampusNum", SqlDbType.NVarChar,50),
                    new SqlParameter("@PlayList", SqlDbType.Text),
                    new SqlParameter("@ReleaseDate", SqlDbType.DateTime),
                    new SqlParameter("@EffectDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate", SqlDbType.DateTime)};
            parameters[0].Value = model.PlayListNo;
            parameters[1].Value = "";
            parameters[2].Value = model.ToXml();
            parameters[3].Value = model.ReleaseDate.Value;
            parameters[4].Value = model.EffectDate;
            parameters[5].Value = model.EndDate;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        public bool Update(SeatManage.ClassModel.AMS_PlayList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AMS_PlayList set ");
            strSql.Append("CampusNum=@CampusNum,");
            strSql.Append("PlayList=@PlayList,");
            strSql.Append("ReleaseDate=@ReleaseDate,");
            strSql.Append("EffectDate=@EffectDate,");
            strSql.Append("EndDate=@EndDate");
            strSql.Append(" where Number=@Number ");
            SqlParameter[] parameters = {
                    new SqlParameter("@CampusNum", SqlDbType.NVarChar,50),
                    new SqlParameter("@PlayList", SqlDbType.Text),
                    new SqlParameter("@ReleaseDate", SqlDbType.DateTime),
                    new SqlParameter("@EffectDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate", SqlDbType.DateTime),
                    new SqlParameter("@Number", SqlDbType.NVarChar,50)};
            parameters[0].Value = "";
            parameters[1].Value = model.ToXml();
            if (model.ReleaseDate.HasValue)
            {
                parameters[2].Value = model.ReleaseDate.Value;
            }
            else
            {
                parameters[2].Value = DateTime.Parse("1900-1-1");
            }
            parameters[3].Value = model.EffectDate;
            parameters[4].Value = model.EndDate;
            parameters[5].Value = model.PlayListNo;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        public bool Delete(string Number)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AMS_PlayList ");
            strSql.Append(" where Number=@Number ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Number", SqlDbType.NVarChar,50)};
            parameters[0].Value = Number;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        /// 增加一条包含MD5的数据
        /// </summary>
        public bool AddMd5(SeatManage.ClassModel.AMS_PlayListMd5 model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AMS_PlayList(");
            strSql.Append("Number,CampusNum,PlayList,ReleaseDate,EffectDate,EndDate)");
            strSql.Append(" values (");
            strSql.Append("@Number,@CampusNum,@PlayList,@ReleaseDate,@EffectDate,@EndDate)");
            SqlParameter[] parameters = {
                    new SqlParameter("@Number", SqlDbType.NVarChar,50),
                    new SqlParameter("@CampusNum", SqlDbType.NVarChar,50),
                    new SqlParameter("@PlayList", SqlDbType.Text),
                    new SqlParameter("@ReleaseDate", SqlDbType.DateTime),
                    new SqlParameter("@EffectDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate", SqlDbType.DateTime)};
            parameters[0].Value = model.PlayListNo;
            parameters[1].Value = "";
            parameters[2].Value = model.ToXml();
            parameters[3].Value = model.ReleaseDate.Value;
            parameters[4].Value = model.EffectDate;
            parameters[5].Value = model.EndDate;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        /// 更新一条包含MD5的数据
        /// </summary>
        public bool UpdateMd5(SeatManage.ClassModel.AMS_PlayListMd5 model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AMS_PlayList set ");
            strSql.Append("CampusNum=@CampusNum,");
            strSql.Append("PlayList=@PlayList,");
            strSql.Append("ReleaseDate=@ReleaseDate,");
            strSql.Append("EffectDate=@EffectDate,");
            strSql.Append("EndDate=@EndDate");
            strSql.Append(" where Number=@Number ");
            SqlParameter[] parameters = {
                    new SqlParameter("@CampusNum", SqlDbType.NVarChar,50),
                    new SqlParameter("@PlayList", SqlDbType.Text),
                    new SqlParameter("@ReleaseDate", SqlDbType.DateTime),
                    new SqlParameter("@EffectDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate", SqlDbType.DateTime),
                    new SqlParameter("@Number", SqlDbType.NVarChar,50)};
            parameters[0].Value = "";
            parameters[1].Value = model.ToXml();
            if (model.ReleaseDate.HasValue)
            {
                parameters[2].Value = model.ReleaseDate.Value;
            }
            else
            {
                parameters[2].Value = DateTime.Parse("1900-1-1");
            }
            parameters[3].Value = model.EffectDate;
            parameters[4].Value = model.EndDate;
            parameters[5].Value = model.PlayListNo;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        ///// <summary>
        ///// 批量删除数据
        ///// </summary>
        //public bool DeleteList(string Numberlist )
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from AMS_PlayList ");
        //    strSql.Append(" where Number in ("+Numberlist + ")  ");
        //    int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
        //    if (rows > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public SeatManage.Model.AMS_PlayList GetModel(string Number)
        //{
			
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select  top 1 Number,CampusNum,PlayList,ReleaseDate,EffectDate,EndDate from AMS_PlayList ");
        //    strSql.Append(" where Number=@Number ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@Number", SqlDbType.NVarChar,50)};
        //    parameters[0].Value = Number;

        //    SeatManage.Model.AMS_PlayList model=new SeatManage.Model.AMS_PlayList();
        //    DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
        //    if(ds.Tables[0].Rows.Count>0)
        //    {
        //        if(ds.Tables[0].Rows[0]["Number"]!=null && ds.Tables[0].Rows[0]["Number"].ToString()!="")
        //        {
        //            model.Number=ds.Tables[0].Rows[0]["Number"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["CampusNum"]!=null && ds.Tables[0].Rows[0]["CampusNum"].ToString()!="")
        //        {
        //            model.CampusNum=ds.Tables[0].Rows[0]["CampusNum"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["PlayList"]!=null && ds.Tables[0].Rows[0]["PlayList"].ToString()!="")
        //        {
        //            model.PlayList=ds.Tables[0].Rows[0]["PlayList"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["ReleaseDate"]!=null && ds.Tables[0].Rows[0]["ReleaseDate"].ToString()!="")
        //        {
        //            model.ReleaseDate=DateTime.Parse(ds.Tables[0].Rows[0]["ReleaseDate"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["EffectDate"]!=null && ds.Tables[0].Rows[0]["EffectDate"].ToString()!="")
        //        {
        //            model.EffectDate=DateTime.Parse(ds.Tables[0].Rows[0]["EffectDate"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["EndDate"]!=null && ds.Tables[0].Rows[0]["EndDate"].ToString()!="")
        //        {
        //            model.EndDate=DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
        //        }
        //        return model;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

		

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
			parameters[0].Value = "AMS_PlayList";
			parameters[1].Value = "Number";
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

