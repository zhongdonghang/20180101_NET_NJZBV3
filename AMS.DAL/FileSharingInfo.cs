using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using AMS.DAL;
using DBUtility;//Please add references
namespace AMS.DAL
{
    /// <summary>
    /// 数据访问类:FileSharingInfo
    /// </summary>
    public partial class FileSharingInfo
    {
        public FileSharingInfo()
        { }
        #region  Method



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(AMS.Model.FileSharingInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FileSharingInfo(");
            strSql.Append("Name,Remark,FileType,UpManID,FilePath,DownLoadCount,Size)");
            strSql.Append(" values (");
            strSql.Append("@Name,@Remark,@FileType,@UpManID,@FilePath,@DownLoadCount,@Size)");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,300),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300),
					new SqlParameter("@FileType", SqlDbType.Int,4),
					new SqlParameter("@UpManID", SqlDbType.Int,4),
					new SqlParameter("@FilePath", SqlDbType.NVarChar,300),
					new SqlParameter("@DownLoadCount", SqlDbType.Int,4),
                    new SqlParameter("@Size", SqlDbType.NVarChar,500)};

            parameters[0].Value = model.Name;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.FileType;
            parameters[3].Value = model.UpManID;
            parameters[4].Value = model.FilePath;
            parameters[5].Value = model.DownLoadCount;
            parameters[6].Value = model.Size;
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
        public bool Update(AMS.Model.FileSharingInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FileSharingInfo set ");
            strSql.Append("Name=@Name,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("FileType=@FileType,");
            strSql.Append("UpManID=@UpManID,");
            strSql.Append("FilePath=@FilePath,");
            strSql.Append("DownLoadCount=@DownLoadCount,");
            strSql.Append("Size=@Size");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,300),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300),
					new SqlParameter("@FileType", SqlDbType.Int,4),
					new SqlParameter("@UpManID", SqlDbType.Int,4),
					new SqlParameter("@FilePath", SqlDbType.NVarChar,300),
					new SqlParameter("@DownLoadCount", SqlDbType.Int,4),
                    new SqlParameter("@Size", SqlDbType.NVarChar,500)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Remark;
            parameters[3].Value = model.FileType;
            parameters[4].Value = model.UpManID;
            parameters[5].Value = model.FilePath;
            parameters[6].Value = model.DownLoadCount;
            parameters[7].Value = model.Size;
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
        public bool Delete(int id)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from FileSharingInfo ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                          new SqlParameter("@Id", SqlDbType.Int,4)
};
            parameters[0].Value = id;
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
        /// 得到一个对象实体
        /// </summary>
        public AMS.Model.FileSharingInfo GetModel(string Name, int FileType)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,Name,Remark,FileType,UpManID,FilePath,DownLoadCount from FileSharingInfo ");
            strSql.Append(" where Name=@Name");
            strSql.Append(" and FileType=@FileType");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar),
                    new SqlParameter("@FileType", SqlDbType.Int)
};
            parameters[0].Value = Name;
            parameters[1].Value = FileType;
            AMS.Model.FileSharingInfo model = new AMS.Model.FileSharingInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"] != null && ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Name"] != null && ds.Tables[0].Rows[0]["Name"].ToString() != "")
                {
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Remark"] != null && ds.Tables[0].Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FileType"] != null && ds.Tables[0].Rows[0]["FileType"].ToString() != "")
                {
                    model.FileType = int.Parse(ds.Tables[0].Rows[0]["FileType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpManID"] != null && ds.Tables[0].Rows[0]["UpManID"].ToString() != "")
                {
                    model.UpManID = int.Parse(ds.Tables[0].Rows[0]["UpManID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FilePath"] != null && ds.Tables[0].Rows[0]["FilePath"].ToString() != "")
                {
                    model.FilePath = ds.Tables[0].Rows[0]["FilePath"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DownLoadCount"] != null && ds.Tables[0].Rows[0]["DownLoadCount"].ToString() != "")
                {
                    model.DownLoadCount = int.Parse(ds.Tables[0].Rows[0]["DownLoadCount"].ToString());
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
            strSql.Append("select Id,Name,Remark,FileType,UpManID,FilePath,DownLoadCount ");
            strSql.Append(" FROM FileSharingInfo ");
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
            strSql.Append(" Id,Name,Remark,FileType,UpManID,FilePath,DownLoadCount ");
            strSql.Append(" FROM FileSharingInfo ");
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
            parameters[0].Value = "FileSharingInfo";
            parameters[1].Value = "";
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

