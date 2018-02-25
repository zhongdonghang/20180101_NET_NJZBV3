using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using AdvertManage.Model;//Please add references
namespace AdvertManage.DAL
{
    /// <summary>
    /// 数据访问类:AMS_Device
    /// </summary>
    public partial class AMS_DeviceDal
    {
        public AMS_DeviceDal()
        { }
        #region  Method

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(AMS_DeviceModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AMS_Device(");
            strSql.Append("Number,CampusId,IsDel,Flag,Describe,CaputrePath,CaputreTime)");
            strSql.Append(" values (");
            strSql.Append("@Number,@CampusId,@IsDel,@Flag,@Describe,@CaputrePath,@CaputreTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@CampusId", SqlDbType.Int,4),
					new SqlParameter("@IsDel", SqlDbType.Bit,1),
					new SqlParameter("@Flag", SqlDbType.Bit,1),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@CaputrePath", SqlDbType.NVarChar,100),
					new SqlParameter("@CaputreTime", SqlDbType.DateTime)};
            parameters[0].Value = model.Number;
            parameters[1].Value = model.CampusId;
            parameters[2].Value = model.IsDel;
            parameters[3].Value = model.Flag;
            parameters[4].Value = model.Describe;
            parameters[5].Value = model.CaputrePath;
            parameters[6].Value = model.CaputreTime;

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
        public bool Update(AMS_DeviceModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AMS_Device set ");
            strSql.Append("Number=@Number,");
            strSql.Append("CampusId=@CampusId,");
            strSql.Append("IsDel=@IsDel,");
            strSql.Append("Flag=@Flag,");
            strSql.Append("Describe=@Describe,");
            strSql.Append("CaputrePath=@CaputrePath,");
            strSql.Append("CaputreTime=@CaputreTime");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@CampusId", SqlDbType.Int,4),
					new SqlParameter("@IsDel", SqlDbType.Bit,1),
					new SqlParameter("@Flag", SqlDbType.Bit,1),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@CaputrePath", SqlDbType.NVarChar,100),
					new SqlParameter("@CaputreTime", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.Number;
            parameters[1].Value = model.CampusId;
            parameters[2].Value = model.IsDel;
            parameters[3].Value = model.Flag;
            parameters[4].Value = model.Describe;
            parameters[5].Value = model.CaputrePath;
            parameters[6].Value = model.CaputreTime;
            parameters[7].Value = model.Id;

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
            strSql.Append("delete from AMS_Device ");
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere,SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Number,CampusId,IsDel,Flag,Describe,CaputrePath,CaputreTime,SchoolId,SchoolNumber,SchooName,CampusName,CampusNumber ");
            strSql.Append(" FROM View_Device ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(),parameters);
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
            strSql.Append(" Id,Number,CampusId,IsDel,Flag,Describe,CaputrePath,CaputreTime ");
            strSql.Append(" FROM AMS_Device ");
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
            parameters[0].Value = "AMS_Device";
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

