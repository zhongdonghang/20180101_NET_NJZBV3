using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using AdvertManage.Model;//Please add references
namespace AdvertManage.DAL
{
    /// <summary>
    /// 数据访问类:AMS_PrintTemplate
    /// </summary>
    public partial class AMS_PrintTemplateDal
    {
        public AMS_PrintTemplateDal()
        { }
        #region  Method

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(AMS_PrintTemplateModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AMS_PrintTemplate(");
            strSql.Append(" Template,EffectDate,EndDate,Describe)");
            strSql.Append(" values (");
            strSql.Append(" @Template,@EffectDate,@EndDate,@Describe)");
            SqlParameter[] parameters = { 
					new SqlParameter("@Template", SqlDbType.Text),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.Template;
            parameters[1].Value = model.EffectDate;
            parameters[2].Value = model.EndDate;
            parameters[3].Value = model.Describe;

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
        public bool Update(AMS_PrintTemplateModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AMS_PrintTemplate set ");
            strSql.Append("Template=@Template,");
            strSql.Append("EffectDate=@EffectDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("Describe=@Describe");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Template", SqlDbType.Text),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.Template;
            parameters[1].Value = model.EffectDate;
            parameters[2].Value = model.EndDate;
            parameters[3].Value = model.Describe;
            parameters[4].Value = model.Id;

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
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AMS_PrintTemplate ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

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
            strSql.Append("select Id,Template,EffectDate,EndDate,Describe ");
            strSql.Append(" FROM AMS_PrintTemplate ");
            if (!string.IsNullOrEmpty(strWhere))
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
            parameters[0].Value = "AMS_PrintTemplate";
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

