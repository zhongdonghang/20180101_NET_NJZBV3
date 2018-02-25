using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AdvertManage.DAL
{
    /// <summary>
    /// 数据访问类:AMS_TitleAd
    /// </summary>
    public partial class AMS_TitleAdDal
    {
        public AMS_TitleAdDal()
        { }
        #region  Method



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(AdvertManage.Model.AMS_TitleAdModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AMS_TitleAd(");
            strSql.Append("EffectDate,EndDate,AdContent)");
            strSql.Append(" values (");
            strSql.Append("@EffectDate,@EndDate,@AdContent)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@AdContent", SqlDbType.NVarChar,300)};
            parameters[0].Value = model.EffectDate;
            parameters[1].Value = model.EndDate;
            parameters[2].Value = model.AdContent;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(AdvertManage.Model.AMS_TitleAdModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AMS_TitleAd set ");
            strSql.Append("EffectDate=@EffectDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("AdContent=@AdContent");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@AdContent", SqlDbType.NVarChar,300),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.EffectDate;
            parameters[1].Value = model.EndDate;
            parameters[2].Value = model.AdContent;
            parameters[3].Value = model.Id;

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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,EffectDate,EndDate,AdContent ");
            strSql.Append(" FROM AMS_TitleAd ");
            if (strWhere != null && strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parameters);
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
            parameters[0].Value = "AMS_TitleAd";
            parameters[1].Value = "Id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method

        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ");
            strSql.Append(" FROM AMS_TitleAd ");
            strSql.Append(" where id=@id");

            SqlParameter[] parameters = {
                                        new SqlParameter("@id",id)
                                        };

            int i = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

