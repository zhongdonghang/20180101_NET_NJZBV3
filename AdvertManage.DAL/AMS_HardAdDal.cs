using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using AdvertManage.Model;//Please add references
namespace AdvertManage.DAL
{
    /// <summary>
    /// 数据访问类:AMS_HardAd
    /// </summary>
    public partial class AMS_HardAdDal
    {
        public AMS_HardAdDal()
        { }
        #region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(AMS_HardAdModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AMS_HardAd(");
            strSql.Append("Number,EffectDate,EndDate,AdImage)");
            strSql.Append(" values (");
            strSql.Append("@Number,@EffectDate,@EndDate,@AdImage)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@AdImage", SqlDbType.Image)};
            parameters[0].Value = model.Number;
            parameters[1].Value = model.EffectDate;
            parameters[2].Value = model.EndDate;
            parameters[3].Value = model.AdImage;

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
        public bool Update(AMS_HardAdModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AMS_HardAd set ");
            strSql.Append("Number=@Number,");
            strSql.Append("EffectDate=@EffectDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("AdImage=@AdImage");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@AdImage", SqlDbType.Image),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.Number;
            parameters[1].Value = model.EffectDate;
            parameters[2].Value = model.EndDate;
            parameters[3].Value = model.AdImage;
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Number,EffectDate,EndDate,AdImage ");
            strSql.Append(" FROM AMS_HardAd ");
            if (strWhere != null && strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }


        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AMS_HardAd");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,50),
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
            parameters[0].Value = "AMS_HardAd";
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

