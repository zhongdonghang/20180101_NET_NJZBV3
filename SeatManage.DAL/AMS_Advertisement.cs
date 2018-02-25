using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
using SeatManage.ClassModel;
using System.Data;

namespace SeatManage.DAL
{
    public partial class AMS_Advertisement
    {
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string LibraryNo, EnumType.AdType adType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AMS_Advertisement");
            strSql.Append(" where Num=@Num and  Type=@Type");
            SqlParameter[] parameters = {
					new SqlParameter("@Num", SqlDbType.NVarChar,50),
                    new SqlParameter("@Type", SqlDbType.Int)};
            parameters[0].Value = LibraryNo;
            parameters[1].Value = (int)adType;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [ID],[Num],[Name],[EffectDate],[EndDate],[Type],[AdContent] ");
            strSql.Append(" FROM AMS_Advertisement ");
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
            strSql.Append("[ID],[Num],[Name],[EffectDate],[EndDate],[Type],[AdContent] ");
            strSql.Append(" FROM AMS_Advertisement ");
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
        public bool Add(SeatManage.ClassModel.AMS_Advertisement model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AMS_Advertisement(");
            strSql.Append("[Num],[Name],[EffectDate],[EndDate],[Type],[AdContent])");
            strSql.Append(" values (");
            strSql.Append("@Num,@Name,@EffectDate,@EndDate,@Type,@AdContent)");
            SqlParameter[] parameters = {
                    new SqlParameter("@Num", SqlDbType.NVarChar,50),
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@EffectDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate", SqlDbType.DateTime),
                    new SqlParameter("@Type", SqlDbType.Int),
                    new SqlParameter("@AdContent", SqlDbType.Text)};
            parameters[0].Value = model.Num;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.EffectDate;
            parameters[3].Value = model.EndDate;
            parameters[4].Value = (int)model.Type;
            parameters[5].Value = model.AdContent;
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
        public bool Update(SeatManage.ClassModel.AMS_Advertisement model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AMS_Advertisement set ");
            strSql.Append("Num=@Num,");
            strSql.Append("Name=@Name,");
            strSql.Append("EffectDate=@EffectDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("[Type]=@Type,");
            strSql.Append("AdContent=@AdContent");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Num", SqlDbType.NVarChar,50),
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@EffectDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate", SqlDbType.DateTime),
                    new SqlParameter("@Type", SqlDbType.Int),
                    new SqlParameter("@AdContent", SqlDbType.Text),
                    new SqlParameter("@ID", SqlDbType.Int)};
            parameters[0].Value = model.Num;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.EffectDate;
            parameters[3].Value = model.EndDate;
            parameters[4].Value = (int)model.Type;
            parameters[5].Value = model.AdContent;
            parameters[6].Value = model.ID;
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
        public bool Delete(SeatManage.ClassModel.AMS_Advertisement model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AMS_Advertisement ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int)};
            parameters[0].Value = model.ID;

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

        #endregion  Method
    }
}
