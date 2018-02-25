using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
    /// <summary>
    /// 数据访问类:AMS_AdCustomer
    /// </summary>
    public partial class AMS_AdCustomer
    {
        public AMS_AdCustomer()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "AMS_AdCustomer");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AMS_AdCustomer");
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
        public int Add(AMS.Model.AMS_AdCustomer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AMS_AdCustomer(");
            strSql.Append("CustomerNo,CompanyName,LinkWay,Describe)");
            strSql.Append(" values (");
            strSql.Append("@CustomerNo,@CompanyName,@LinkWay,@Describe)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerNo", SqlDbType.NVarChar,20),
					new SqlParameter("@CompanyName", SqlDbType.NVarChar,20),
					new SqlParameter("@LinkWay", SqlDbType.NVarChar,500),
					new SqlParameter("@Describe", SqlDbType.NVarChar,500)};
            parameters[0].Value = model.CustomerNo;
            parameters[1].Value = model.CompanyName;
            parameters[2].Value = model.LinkWay;
            parameters[3].Value = model.Describe;

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
        public bool Update(AMS.Model.AMS_AdCustomer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AMS_AdCustomer set ");
            strSql.Append("CustomerNo=@CustomerNo,");
            strSql.Append("CompanyName=@CompanyName,");
            strSql.Append("LinkWay=@LinkWay,");
            strSql.Append("Describe=@Describe");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerNo", SqlDbType.NVarChar,20),
					new SqlParameter("@CompanyName", SqlDbType.NVarChar,20),
					new SqlParameter("@LinkWay", SqlDbType.NVarChar,500),
					new SqlParameter("@Describe", SqlDbType.NVarChar,500),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.CustomerNo;
            parameters[1].Value = model.CompanyName;
            parameters[2].Value = model.LinkWay;
            parameters[3].Value = model.Describe;
            parameters[4].Value = model.ID;

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
            strSql.Append("delete from AMS_AdCustomer ");
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
            strSql.Append("delete from AMS_AdCustomer ");
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
        public AMS.Model.AMS_AdCustomer GetModel(string CompanyName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,CustomerNo,CompanyName,LinkWay,Describe from AMS_AdCustomer ");
            strSql.Append(" where CompanyName=@CompanyName");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyName", SqlDbType.NVarChar)
};
            parameters[0].Value = CompanyName;

            AMS.Model.AMS_AdCustomer model = new AMS.Model.AMS_AdCustomer();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CustomerNo"] != null && ds.Tables[0].Rows[0]["CustomerNo"].ToString() != "")
                {
                    model.CustomerNo = ds.Tables[0].Rows[0]["CustomerNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CompanyName"] != null && ds.Tables[0].Rows[0]["CompanyName"].ToString() != "")
                {
                    model.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LinkWay"] != null && ds.Tables[0].Rows[0]["LinkWay"].ToString() != "")
                {
                    model.LinkWay = ds.Tables[0].Rows[0]["LinkWay"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Describe"] != null && ds.Tables[0].Rows[0]["Describe"].ToString() != "")
                {
                    model.Describe = ds.Tables[0].Rows[0]["Describe"].ToString();
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
            strSql.Append("select ID,CustomerNo,CompanyName,LinkWay,Describe ");
            strSql.Append(" FROM AMS_AdCustomer ");
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
            strSql.Append(" ID,CustomerNo,CompanyName,LinkWay,Describe ");
            strSql.Append(" FROM AMS_AdCustomer ");
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
            parameters[0].Value = "AMS_AdCustomer";
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

