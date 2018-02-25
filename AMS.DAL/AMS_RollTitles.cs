using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using System.Data;

namespace AMS.DAL
{
    public class AMS_RollTitles
    {
        /// <summary>
        /// 添加滚动文字
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddRollTitles(AMS.Model.AMS_RollTitles model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO AMS_RollTitles");
            sql.Append("           (Name,EffectDate,EndDate,CustomerId,Type,Operator,Num)");
            sql.Append("     VALUES");
            sql.Append("           (@Name,@EffectDate,@EndDate,@CustomerId,@Type,@Operator,@Num)");
            SqlParameter[] sqlpar ={
                                       new SqlParameter("@Name",model.Name),
                                       new SqlParameter("@EffectDate",model.EffectDate),
                                       new SqlParameter("@EndDate",model.EndDate),
                                       new SqlParameter("@CustomerId",model.CustomerId),
                                       new SqlParameter("@Type",model.Type),
                                       new SqlParameter("@Operator",model.OperatorID),
                                       new SqlParameter("@Num",model.Num)
                                  };
            int rows = DbHelperSQL.ExecuteSql(sql.ToString(), sqlpar);
            if (rows>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改滚动文字
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateRollTitles(AMS.Model.AMS_RollTitles model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE AMS_RollTitles");
            sql.Append("   SET Name = @Name");
            sql.Append("      ,EffectDate = @EffectDate");
            sql.Append("      ,EndDate = @EndDate");
            sql.Append("      ,CustomerId = @CustomerId");
            sql.Append("      ,Type = @Type");
            sql.Append("      ,Operator = @Operator ");
            sql.Append("WHERE ID=@ID");
            SqlParameter[] sqlpar ={
                                       new SqlParameter("@Name",model.Name),
                                       new SqlParameter("@EffectDate",model.EffectDate),
                                       new SqlParameter("@EndDate",model.EndDate),
                                       new SqlParameter("@CustomerId",model.CustomerId),
                                       new SqlParameter("@Type",model.Type),
                                       new SqlParameter("@Operator",model.OperatorID),
                                       new SqlParameter("@ID",model.ID)
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
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelRollTitles(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE FROM AMS_RollTitles");
            sql.Append("      WHERE ID=@ID");
            SqlParameter[] sqlpar ={
                                       new SqlParameter("@ID",id)
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
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetModel(string strWhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select ID, Name, EffectDate, EndDate, CustomerId, Type, Operator,Num from dbo.AMS_RollTitles");
            if (!string.IsNullOrEmpty(strWhere))
            {
                sql.Append(" where "+strWhere);
            }
            return DbHelperSQL.Query(sql.ToString());
        }

        public bool GetModelByName(string Name)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from AMS_RollTitles where");
            sql.Append(" Name = '"+Name+"'");
            SqlParameter[] sqlpar ={
                                       new SqlParameter("@Name",Name)
                                  };
            int rows = DbHelperSQL.Query(sql.ToString()).Tables[0].Rows.Count;
            if (rows>0)
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
