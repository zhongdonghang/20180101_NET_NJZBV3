using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
    /// <summary>
    /// 数据访问类:AMS_UserInfo
    /// </summary>
    public partial class AMS_UserInfo
    {
        public AMS_UserInfo()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "AMS_UserInfo");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AMS_UserInfo");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(AMS.Model.AMS_UserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AMS_UserInfo(");
            strSql.Append("LoginId,UserPwd,BranchName,UserName,Remark)");
            strSql.Append(" values (");
            strSql.Append("@LoginId,@UserPwd,@BranchName,@UserName,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@LoginId", SqlDbType.NVarChar,30),
					new SqlParameter("@UserPwd", SqlDbType.NVarChar,50),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,30),
					new SqlParameter("@UserName", SqlDbType.NVarChar,30),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.LoginId;
            parameters[1].Value = model.UserPwd;
            parameters[2].Value = model.BranchName;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.Remark;

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
        public bool Update(AMS.Model.AMS_UserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AMS_UserInfo set ");
            strSql.Append("LoginId=@LoginId,");
            strSql.Append("UserPwd=@UserPwd,");
            strSql.Append("BranchName=@BranchName,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@LoginId", SqlDbType.NVarChar,30),
					new SqlParameter("@UserPwd", SqlDbType.NVarChar,50),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,30),
					new SqlParameter("@UserName", SqlDbType.NVarChar,30),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.LoginId;
            parameters[1].Value = model.UserPwd;
            parameters[2].Value = model.BranchName;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.ID;

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
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AMS_UserInfo ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
            parameters[0].Value = ID;

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
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AMS_UserInfo ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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
        public AMS.Model.AMS_UserInfo GetModel(string LoginId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,LoginId,UserPwd,BranchName,UserName,Remark from AMS_UserInfo ");
            strSql.Append(" where LoginId=@LoginId");
            SqlParameter[] parameters = {
					new SqlParameter("@LoginId", SqlDbType.NChar,40)
        };
            parameters[0].Value = LoginId;

            AMS.Model.AMS_UserInfo model = new AMS.Model.AMS_UserInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LoginId"] != null && ds.Tables[0].Rows[0]["LoginId"].ToString() != "")
                {
                    model.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserPwd"] != null && ds.Tables[0].Rows[0]["UserPwd"].ToString() != "")
                {
                    model.UserPwd = ds.Tables[0].Rows[0]["UserPwd"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BranchName"] != null && ds.Tables[0].Rows[0]["BranchName"].ToString() != "")
                {
                    model.BranchName = ds.Tables[0].Rows[0]["BranchName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null && ds.Tables[0].Rows[0]["UserName"].ToString() != "")
                {
                    model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
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
            strSql.Append("select ID,LoginId,UserPwd,BranchName,UserName,Remark ");
            strSql.Append(" FROM AMS_UserInfo ");
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
            strSql.Append(" ID,LoginId,UserPwd,BranchName,UserName,Remark ");
            strSql.Append(" FROM AMS_UserInfo ");
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
            parameters[0].Value = "AMS_UserInfo";
            parameters[1].Value = "ID";
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

