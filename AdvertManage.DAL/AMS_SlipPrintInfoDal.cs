using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
namespace AdvertManage.DAL
{
    /// <summary>
    /// 数据访问类:AMS_SlipPrintInfo
    /// </summary>
    public partial class AMS_SlipPrintInfoDal
    {
        public AMS_SlipPrintInfoDal()
        { }
        #region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(AdvertManage.Model.AMS_SlipPrintInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AMS_SlipPrintInfo(");
            strSql.Append("SlipCustomerId,CampusId,Date,PrintAmount,LookOverAmount)");
            strSql.Append(" values (");
            strSql.Append("@SlipCustomerId,@CampusId,@Date,@PrintAmount,@LookOverAmount)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = { 
					new SqlParameter("@SlipCustomerId", SqlDbType.Int,4),
					new SqlParameter("@CampusId", SqlDbType.Int,4),
					new SqlParameter("@Date", SqlDbType.DateTime),
					new SqlParameter("@PrintAmount", SqlDbType.Int,4),
					new SqlParameter("@LookOverAmount", SqlDbType.Int,4)};
            parameters[0].Value = model.SlipCustomerId;
            parameters[1].Value = model.CampusId;
            parameters[2].Value = model.Date;
            parameters[3].Value = model.PrintAmount;
            parameters[4].Value = model.LookOverAmount;

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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AMS_SlipPrintInfo ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere,SqlParameter[] parameters)
        { 
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CampusId,CampusNum,CampusName,SlipCustomerId,SlipCustomerNum,Date,PrintAmount,LookOverAmount ");
            strSql.Append(" FROM View_SlipPrintInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parameters); 
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
            parameters[0].Value = "AMS_SlipPrintInfo";
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

