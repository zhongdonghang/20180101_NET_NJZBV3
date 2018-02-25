using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
namespace SeatManage.DAL
{
	/// <summary>
	/// 数据访问类:AMS_TitleAd
	/// </summary>
	public partial class AMS_TitleAd
	{
		public AMS_TitleAd()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string AdNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AMS_TitleAd");
			strSql.Append(" where AdNo=@AdNo ");
			SqlParameter[] parameters = {
					new SqlParameter("@AdNo", SqlDbType.NVarChar,50)};
			parameters[0].Value = AdNo;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public DataSet GetList(string strWhere,SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AdNo,EffectDate,EndDate,AdContent,Num ");
            strSql.Append(" FROM AMS_TitleAd ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" AdNo,EffectDate,EndDate,AdContent,Num ");
            strSql.Append(" FROM AMS_TitleAd ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString(),parameters);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(TitleAdvertInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AMS_TitleAd(");
            strSql.Append("EffectDate,EndDate,AdContent,Num)");
            strSql.Append(" values (");
            strSql.Append("@EffectDate,@EndDate,@AdContent,@Num)");
            SqlParameter[] parameters = {
                    new SqlParameter("@EffectDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate", SqlDbType.DateTime),
                    new SqlParameter("@AdContent", SqlDbType.Text),
                                        new SqlParameter("@Num",model.TitleAdvertNo)};
        
            parameters[0].Value = model.EffectDate;
            parameters[1].Value = model.EndDate;
            parameters[2].Value = model.TitleAdvert;

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
        public bool Update(SeatManage.ClassModel.TitleAdvertInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AMS_TitleAd set ");
            strSql.Append("EffectDate=@EffectDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("AdContent=@AdContent");
            strSql.Append(" where Num=@Num ");
            SqlParameter[] parameters = {
                    new SqlParameter("@EffectDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate", SqlDbType.DateTime),
                    new SqlParameter("@AdContent", SqlDbType.Text),
                    new SqlParameter("@Num", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.EffectDate;
            parameters[1].Value = model.EndDate;
            parameters[2].Value = model.TitleAdvert;
            parameters[3].Value = model.TitleAdvertNo;

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
        public bool Delete(int AdNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AMS_TitleAd ");
            strSql.Append(" where AdNo=@AdNo ");
            SqlParameter[] parameters = {
                    new SqlParameter("@AdNo", SqlDbType.Int)};
            parameters[0].Value = AdNo;

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
        //public bool DeleteList(string AdNolist )
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from AMS_TitleAd ");
        //    strSql.Append(" where AdNo in ("+AdNolist + ")  ");
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
        //public SeatManage.Model.AMS_TitleAd GetModel(string AdNo)
        //{
			
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select  top 1 AdNo,EffectDate,EndDate,AdList from AMS_TitleAd ");
        //    strSql.Append(" where AdNo=@AdNo ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@AdNo", SqlDbType.NVarChar,50)};
        //    parameters[0].Value = AdNo;

        //    SeatManage.Model.AMS_TitleAd model=new SeatManage.Model.AMS_TitleAd();
        //    DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
        //    if(ds.Tables[0].Rows.Count>0)
        //    {
        //        if(ds.Tables[0].Rows[0]["AdNo"]!=null && ds.Tables[0].Rows[0]["AdNo"].ToString()!="")
        //        {
        //            model.AdNo=ds.Tables[0].Rows[0]["AdNo"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["EffectDate"]!=null && ds.Tables[0].Rows[0]["EffectDate"].ToString()!="")
        //        {
        //            model.EffectDate=DateTime.Parse(ds.Tables[0].Rows[0]["EffectDate"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["EndDate"]!=null && ds.Tables[0].Rows[0]["EndDate"].ToString()!="")
        //        {
        //            model.EndDate=DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["AdList"]!=null && ds.Tables[0].Rows[0]["AdList"].ToString()!="")
        //        {
        //            model.AdList=ds.Tables[0].Rows[0]["AdList"].ToString();
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
			parameters[0].Value = "AMS_TitleAd";
			parameters[1].Value = "AdNo";
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

