using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SeatManage.ClassModel;

namespace SeatManage.DAL
{
    public class AutoUpdater
    {
        /// <summary>
        /// 添加一条新的纪录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public bool Add(FileUpdateInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AutoUpdater ([Application]  ,[AutoUpdaterXml] ,[UpdateLog]  ,[ReleaseDate] )");
            strSql.Append(" values (@Application ,@AutoUpdaterXml  ,@UpdateLog ,@ReleaseDate )");
            SqlParameter[] parameters = {
                                            new SqlParameter("@Application",(int)model.SubsystemType),
                                            new SqlParameter("@AutoUpdaterXml",model.ToString()),
                                            new SqlParameter("@UpdateLog",model.UpdateLog),
                                            new SqlParameter("@ReleaseDate",model.ReleaseDate.ToString()) 
                                        };
            try
            {
                int i = DBUtility.DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataSet GetList(string strWhere, SqlParameter[] parameters)
        { 
          StringBuilder strSql= new StringBuilder();
          strSql.Append("SELECT [Id] ,[Application]  ,[AutoUpdaterXml] ,[UpdateLog]  ,[ReleaseDate] FROM  [dbo].[AutoUpdater] ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(string.Format(" where {0}",strWhere));
            }
            try
            {
                return DBUtility.DbHelperSQL.Query(strSql.ToString(), parameters); 
            }
            catch (Exception ex)
            {
                throw ex;
            } 
 
        }

        public bool Update(FileUpdateInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE  [dbo].[AutoUpdater]  SET ");  
             strSql.Append("[AutoUpdaterXml] =@AutoUpdaterXml, ");
             strSql.Append("[UpdateLog] =@Remart, ");
             strSql.Append("[ReleaseDate] =@ReleaseDate ");
             strSql.Append(" WHERE Application=@Application ");
             SqlParameter[] parameters = {  
                                            new SqlParameter("@AutoUpdaterXml",model.ToString()),
                                            new SqlParameter("@Remart",model.UpdateLog) ,
                                            new SqlParameter("@ReleaseDate",model.ReleaseDate),
                                            new SqlParameter("@Application",(int)model.SubsystemType)
                                        };
             try
             {
                 int i = DBUtility.DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                 if (i > 0)
                 {
                     return true;
                 }
                 else
                 {
                     return false;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
  
        }
    }
}
