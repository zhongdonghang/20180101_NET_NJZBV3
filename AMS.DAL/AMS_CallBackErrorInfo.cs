using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
    /// <summary>
    /// 数据访问类:AMS_CallBackErrorInfo
    /// </summary>
    public partial class AMS_CallBackErrorInfo
    {
        public AMS_CallBackErrorInfo()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "AMS_CallBackErrorInfo");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AMS_CallBackErrorInfo");
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
        public int Add(AMS.Model.AMS_CallBackErrorInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AMS_CallBackErrorInfo(");
            strSql.Append("SchoolId,FbPerson,MarkManID,FbTime,SolveManID,SolveTime,SolveWay,ProblemType,FbDescribe,Status)");
            strSql.Append(" values (");
            strSql.Append("@SchoolId,@FbPerson,@MarkManID,@FbTime,@SolveManID,@SolveTime,@SolveWay,@ProblemType,@FbDescribe,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SchoolId", SqlDbType.Int,4),
					new SqlParameter("@FbPerson", SqlDbType.NVarChar,20),
					new SqlParameter("@MarkManID", SqlDbType.Int,4),
					new SqlParameter("@FbTime", SqlDbType.DateTime),
					new SqlParameter("@SolveManID", SqlDbType.Int,4),
					new SqlParameter("@SolveTime", SqlDbType.DateTime),
					new SqlParameter("@SolveWay", SqlDbType.NVarChar,500),
					new SqlParameter("@ProblemType", SqlDbType.Int,4),
					new SqlParameter("@FbDescribe", SqlDbType.NVarChar,500),
                     new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.SchoolId;
            parameters[1].Value = model.FbPerson;
            parameters[2].Value = model.MarkManID;
            parameters[3].Value = model.FbTime;
            parameters[4].Value = model.SolveManID;
            parameters[5].Value = model.SolveTime;
            parameters[6].Value = model.SolveWay;
            parameters[7].Value = model.ProblemType;
            parameters[8].Value = model.FbDescribe;
            parameters[9].Value = model.Solvestatic;

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
        public bool Update(AMS.Model.AMS_CallBackErrorInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AMS_CallBackErrorInfo set ");
            strSql.Append("SchoolId=@SchoolId,");
            strSql.Append("FbPerson=@FbPerson,");
            strSql.Append("MarkManID=@MarkManID,");
            strSql.Append("FbTime=@FbTime,");
            strSql.Append("SolveManID=@SolveManID,");
            strSql.Append("SolveTime=@SolveTime,");
            strSql.Append("SolveWay=@SolveWay,");
            strSql.Append("ProblemType=@ProblemType,");
            strSql.Append("FbDescribe=@FbDescribe,");
            strSql.Append("Status=@Status");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@SchoolId", SqlDbType.Int,4),
					new SqlParameter("@FbPerson", SqlDbType.NVarChar,20),
					new SqlParameter("@MarkManID", SqlDbType.Int,4),
					new SqlParameter("@FbTime", SqlDbType.DateTime),
					new SqlParameter("@SolveManID", SqlDbType.Int,4),
					new SqlParameter("@SolveTime", SqlDbType.DateTime),
					new SqlParameter("@SolveWay", SqlDbType.NVarChar,500),
					new SqlParameter("@ProblemType", SqlDbType.Int,4),
					new SqlParameter("@FbDescribe", SqlDbType.NVarChar,500),
                    new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.SchoolId;
            parameters[1].Value = model.FbPerson;
            parameters[2].Value = model.MarkManID;
            parameters[3].Value = model.FbTime;
            parameters[4].Value = model.SolveManID;
            parameters[5].Value = model.SolveTime;
            parameters[6].Value = model.SolveWay;
            parameters[7].Value = model.ProblemType;
            parameters[8].Value = model.FbDescribe;
            parameters[9].Value = model.Solvestatic;
            parameters[10].Value = model.ID;

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
            strSql.Append("delete from AMS_CallBackErrorInfo ");
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
            strSql.Append("delete from AMS_CallBackErrorInfo ");
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
        public AMS.Model.AMS_CallBackErrorInfo GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,SchoolId,FbPerson,MarkManID,FbTime,SolveManID,SolveTime,SolveWay,ProblemType,FbDescribe from AMS_CallBackErrorInfo ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
            parameters[0].Value = ID;

            AMS.Model.AMS_CallBackErrorInfo model = new AMS.Model.AMS_CallBackErrorInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SchoolId"] != null && ds.Tables[0].Rows[0]["SchoolId"].ToString() != "")
                {
                    model.SchoolId = int.Parse(ds.Tables[0].Rows[0]["SchoolId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FbPerson"] != null && ds.Tables[0].Rows[0]["FbPerson"].ToString() != "")
                {
                    model.FbPerson = ds.Tables[0].Rows[0]["FbPerson"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MarkManID"] != null && ds.Tables[0].Rows[0]["MarkManID"].ToString() != "")
                {
                    model.MarkManID = int.Parse(ds.Tables[0].Rows[0]["MarkManID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FbTime"] != null && ds.Tables[0].Rows[0]["FbTime"].ToString() != "")
                {
                    model.FbTime = DateTime.Parse(ds.Tables[0].Rows[0]["FbTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SolveManID"] != null && ds.Tables[0].Rows[0]["SolveManID"].ToString() != "")
                {
                    model.SolveManID = int.Parse(ds.Tables[0].Rows[0]["SolveManID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SolveTime"] != null && ds.Tables[0].Rows[0]["SolveTime"].ToString() != "")
                {
                    model.SolveTime = DateTime.Parse(ds.Tables[0].Rows[0]["SolveTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SolveWay"] != null && ds.Tables[0].Rows[0]["SolveWay"].ToString() != "")
                {
                    model.SolveWay = ds.Tables[0].Rows[0]["SolveWay"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ProblemType"] != null && ds.Tables[0].Rows[0]["ProblemType"].ToString() != "")
                {
                    model.ProblemType = int.Parse(ds.Tables[0].Rows[0]["ProblemType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FbDescribe"] != null && ds.Tables[0].Rows[0]["FbDescribe"].ToString() != "")
                {
                    model.FbDescribe = ds.Tables[0].Rows[0]["FbDescribe"].ToString();
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
            strSql.Append("select ID,SchoolId,FbPerson,MarkManID,FbTime,SolveManID,SolveTime,SolveWay,ProblemType,FbDescribe ");
            strSql.Append(" FROM AMS_CallBackErrorInfo ");
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
            strSql.Append(" ID,SchoolId,FbPerson,MarkManID,FbTime,SolveManID,SolveTime,SolveWay,ProblemType,FbDescribe ");
            strSql.Append(" FROM AMS_CallBackErrorInfo ");
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
            parameters[0].Value = "AMS_CallBackErrorInfo";
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

