using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AdvertManage.DAL
{
    /// <summary>
    /// 数据访问类:AMS_School
    /// </summary>
    public partial class AMS_SchoolDal
    {
        public AMS_SchoolDal()
        { }
        #region  Method

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(AdvertManage.Model.AMS_SchoolModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AMS_School(");
            strSql.Append("Number,Name,DTUip,Describe,ConnectionString,Flag)");
            strSql.Append(" values (");
            strSql.Append("@Number,@Name,@DTUip,@Describe,@ConnectionString,@Flag)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,30),
					new SqlParameter("@DTUip", SqlDbType.NVarChar,20),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@ConnectionString", SqlDbType.NVarChar,200),
                    new SqlParameter("@Flag",SqlDbType.Int)};
            parameters[0].Value = model.Number;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.DTUip;
            parameters[3].Value = model.Describe;
            parameters[4].Value = model.ConnectionString;
            parameters[5].Value = model.Flag;
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
        public bool Update(AdvertManage.Model.AMS_SchoolModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AMS_School set ");
            strSql.Append("Number=@Number,");
            strSql.Append("Name=@Name,");
            strSql.Append("DTUip=@DTUip,");
            strSql.Append("Describe=@Describe,");
            strSql.Append("ConnectionString=@ConnectionString,");
            strSql.Append("Flag=@Flag");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,30),
					new SqlParameter("@DTUip", SqlDbType.NVarChar,20),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@ConnectionString", SqlDbType.NVarChar,200),
                    new SqlParameter("@Flag",SqlDbType.Int),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.Number;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.DTUip;
            parameters[3].Value = model.Describe;
            parameters[4].Value = model.ConnectionString;
            parameters[5].Value = model.Flag;
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Number,Name,DTUip,Describe,ConnectionString,Flag ");
            strSql.Append(" FROM AMS_School ");
            if (strWhere!=null)
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }


        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" delete from AMS_School where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int )
                                        };
            parameters[0].Value = id;
            int i = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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
            parameters[0].Value = "AMS_School";
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

