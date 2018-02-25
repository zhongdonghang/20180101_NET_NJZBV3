using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using System.Data;

namespace SeatManage.DAL
{
    public class AMS_RollTitles
    {
        public bool AddRollTitles(SeatManage.ClassModel.RollTitlesInfo model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO AMS_RollTitles");
            sql.Append("           (EffectDate,EndDate,Type,Num)");
            sql.Append("     VALUES");
            sql.Append("           (@EffectDate,@EndDate,@Type,@Num)");
            SqlParameter[] sqlpar ={
                                       new SqlParameter("@EffectDate",model.EffectDate),
                                       new SqlParameter("@EndDate",model.EndDate),
                                       new SqlParameter("@Type",model.Type),
                                       new SqlParameter("@Num",model.Num)
                                  };
            int rows = DbHelperSQL.ExecuteSql(sql.ToString(), sqlpar);
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
        public DataSet GetList(string strWhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select EffectDate, EndDate, Type,Num from dbo.AMS_RollTitles");
            if (!string.IsNullOrEmpty(strWhere))
            {
                sql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(sql.ToString());
        }

        public bool UpdateRollTitles(ClassModel.RollTitlesInfo model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE AMS_RollTitles");
            sql.Append("   SET [EffectDate] = @EffectDate");
            sql.Append("      ,[EndDate] = @EndDate");
            sql.Append("      ,[Type] = @Type");
            sql.Append(" WHERE Num=@Num");
            SqlParameter[] sqlpar ={
                                       new SqlParameter("@EffectDate",model.EffectDate),
                                       new SqlParameter("@EndDate",model.EndDate),
                                       new SqlParameter("@Type",model.Type),
                                       new SqlParameter("@Num",model.Num)
                                  };
            int rows = DbHelperSQL.ExecuteSql(sql.ToString(), sqlpar);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeleteRollTitles(string num)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE AMS_RollTitles");
            sql.Append(" WHERE Num=@Num");
            SqlParameter[] sqlpar ={
                                       new SqlParameter("@Num",num)
                                  };
            int rows = DbHelperSQL.ExecuteSql(sql.ToString(), sqlpar);
            if (rows > 0)
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
