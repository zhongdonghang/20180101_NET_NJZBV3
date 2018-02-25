using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
    /// <summary>
    /// 数据访问类:ProgramUpgrade
    /// </summary>
    public partial class ProgramUpgrade
    {
        public ProgramUpgrade()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "ProgramUpgrade");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ProgramUpgrade");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(AMS.Model.ProgramUpgrade model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ProgramUpgrade(");
            strSql.Append("Application,AutoUpdaterXml,UpdateLog,ReleaseDate,Version,Remark)");
            strSql.Append(" values (");
            strSql.Append("@Application,@AutoUpdaterXml,@UpdateLog,@ReleaseDate,@Version,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Application", SqlDbType.Int,4),
					new SqlParameter("@AutoUpdaterXml", SqlDbType.Text),
					new SqlParameter("@UpdateLog", SqlDbType.NVarChar,4000),
					new SqlParameter("@ReleaseDate", SqlDbType.DateTime),
					new SqlParameter("@Version", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300)};
            parameters[0].Value = model.Application;
            parameters[1].Value = model.AutoUpdaterXml;
            parameters[2].Value = model.UpdateLog;
            parameters[3].Value = model.ReleaseDate;
            parameters[4].Value = model.Version;
            parameters[5].Value = model.Remark;

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
        public bool Update(AMS.Model.ProgramUpgrade model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ProgramUpgrade set ");
            strSql.Append("Application=@Application,");
            strSql.Append("AutoUpdaterXml=@AutoUpdaterXml,");
            strSql.Append("UpdateLog=@UpdateLog,");
            strSql.Append("ReleaseDate=@ReleaseDate,");
            strSql.Append("Version=@Version,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Application", SqlDbType.Int,4),
					new SqlParameter("@AutoUpdaterXml", SqlDbType.Text),
					new SqlParameter("@UpdateLog", SqlDbType.NVarChar,4000),
					new SqlParameter("@ReleaseDate", SqlDbType.DateTime),
					new SqlParameter("@Version", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.Application;
            parameters[1].Value = model.AutoUpdaterXml;
            parameters[2].Value = model.UpdateLog;
            parameters[3].Value = model.ReleaseDate;
            parameters[4].Value = model.Version;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.Id;

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
            strSql.Append("delete from ProgramUpgrade ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
};
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ProgramUpgrade ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public AMS.Model.ProgramUpgrade GetModel(string Version,int application)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,Application,AutoUpdaterXml,UpdateLog,ReleaseDate,Version,Remark from ProgramUpgrade ");
            strSql.Append(" where Version=@Version");
            strSql.Append(" and  Application=@Application");
            SqlParameter[] parameters = {
					new SqlParameter("@Version", SqlDbType.NVarChar),
                    new SqlParameter("@Application", SqlDbType.Int)
};
            parameters[0].Value = Version;
            parameters[1].Value = application;
            AMS.Model.ProgramUpgrade model = new AMS.Model.ProgramUpgrade();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"] != null && ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Application"] != null && ds.Tables[0].Rows[0]["Application"].ToString() != "")
                {
                    model.Application = int.Parse(ds.Tables[0].Rows[0]["Application"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AutoUpdaterXml"] != null && ds.Tables[0].Rows[0]["AutoUpdaterXml"].ToString() != "")
                {
                    model.AutoUpdaterXml = ds.Tables[0].Rows[0]["AutoUpdaterXml"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UpdateLog"] != null && ds.Tables[0].Rows[0]["UpdateLog"].ToString() != "")
                {
                    model.UpdateLog = ds.Tables[0].Rows[0]["UpdateLog"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ReleaseDate"] != null && ds.Tables[0].Rows[0]["ReleaseDate"].ToString() != "")
                {
                    model.ReleaseDate = DateTime.Parse(ds.Tables[0].Rows[0]["ReleaseDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Version"] != null && ds.Tables[0].Rows[0]["Version"].ToString() != "")
                {
                    model.Version = ds.Tables[0].Rows[0]["Version"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Remark"] != null && ds.Tables[0].Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
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
        public AMS.Model.ProgramUpgrade GetModelByID(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,Application,AutoUpdaterXml,UpdateLog,ReleaseDate,Version,Remark from ProgramUpgrade ");
            strSql.Append(" where Id=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", id)
                                        };
            AMS.Model.ProgramUpgrade model = new AMS.Model.ProgramUpgrade();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"] != null && ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Application"] != null && ds.Tables[0].Rows[0]["Application"].ToString() != "")
                {
                    model.Application = int.Parse(ds.Tables[0].Rows[0]["Application"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AutoUpdaterXml"] != null && ds.Tables[0].Rows[0]["AutoUpdaterXml"].ToString() != "")
                {
                    model.AutoUpdaterXml = ds.Tables[0].Rows[0]["AutoUpdaterXml"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UpdateLog"] != null && ds.Tables[0].Rows[0]["UpdateLog"].ToString() != "")
                {
                    model.UpdateLog = ds.Tables[0].Rows[0]["UpdateLog"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ReleaseDate"] != null && ds.Tables[0].Rows[0]["ReleaseDate"].ToString() != "")
                {
                    model.ReleaseDate = DateTime.Parse(ds.Tables[0].Rows[0]["ReleaseDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Version"] != null && ds.Tables[0].Rows[0]["Version"].ToString() != "")
                {
                    model.Version = ds.Tables[0].Rows[0]["Version"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Remark"] != null && ds.Tables[0].Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Application,AutoUpdaterXml,UpdateLog,ReleaseDate,Version,Remark ");
            strSql.Append(" FROM ProgramUpgrade ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id,Application,AutoUpdaterXml,UpdateLog,ReleaseDate,Version,Remark ");
            strSql.Append(" FROM ProgramUpgrade ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            parameters[0].Value = "ProgramUpgrade";
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

