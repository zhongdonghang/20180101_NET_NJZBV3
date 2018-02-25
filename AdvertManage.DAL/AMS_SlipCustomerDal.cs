using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AdvertManage.DAL
{
    /// <summary>
    /// 数据访问类:AMS_SlipCustomer
    /// </summary>
    public partial class AMS_SlipCustomerDal
    {
        public AMS_SlipCustomerDal()
        { }
        #region  Method



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(AdvertManage.Model.AMS_SlipCustomerModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AMS_SlipCustomer(");
            strSql.Append("Number,ImageUrl,SlipTemplate,CustomerImage,CampusNum,EffectDate,EndDate,Type,IsPrint)");
            strSql.Append(" values (");
            strSql.Append("@Number,@ImageUrl,@SlipTemplate,@CustomerImage,@CampusNum,@EffectDate,@EndDate,@Type,@IsPrint)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar,20),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@SlipTemplate", SqlDbType.Text),
					new SqlParameter("@CustomerImage", SqlDbType.NVarChar,200),
					new SqlParameter("@CampusNum", SqlDbType.NVarChar,50),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@IsPrint",SqlDbType.Bit)};
            parameters[0].Value = model.Number;
            parameters[1].Value = model.ImageUrl;
            parameters[2].Value = model.SlipTemplate;
            parameters[3].Value = model.CustomerImage;
            parameters[4].Value = model.CampusNum;
            parameters[5].Value = model.EffectDate;
            parameters[6].Value = model.EndDate;
            parameters[7].Value = model.Type;
            parameters[8].Value = model.IsPrint;
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
        public bool Update(AdvertManage.Model.AMS_SlipCustomerModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AMS_SlipCustomer set ");
            strSql.Append("Number=@Number,");
            strSql.Append("ImageUrl=@ImageUrl,");
            strSql.Append("SlipTemplate=@SlipTemplate,");
            strSql.Append("CustomerImage=@CustomerImage,");
            strSql.Append("CampusNum=@CampusNum,");
            strSql.Append("EffectDate=@EffectDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("Type=@Type,");
            strSql.Append("IsPrint=@IsPrint");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar,20),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@SlipTemplate", SqlDbType.Text),
					new SqlParameter("@CustomerImage", SqlDbType.NVarChar,200),
					new SqlParameter("@CampusNum", SqlDbType.NVarChar,50),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@IsPrint",SqlDbType.Bit),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.Number;
            parameters[1].Value = model.ImageUrl;
            parameters[2].Value = model.SlipTemplate;
            parameters[3].Value = model.CustomerImage;
            parameters[4].Value = model.CampusNum;
            parameters[5].Value = model.EffectDate;
            parameters[6].Value = model.EndDate;
            parameters[7].Value = model.Type;
            parameters[8].Value = model.IsPrint;
            parameters[9].Value = model.Id;

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
            strSql.Append("select Id,Number,ImageUrl,SlipTemplate,CustomerImage,CampusNum,EffectDate,EndDate,Type,IsPrint ");
            strSql.Append(" FROM AMS_SlipCustomer ");
            if (strWhere != null && strWhere.Trim() != "")
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
            parameters[0].Value = "AMS_SlipCustomer";
            parameters[1].Value = "Id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method

        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ");
            strSql.Append(" FROM AMS_SlipCustomer ");

            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                                        new SqlParameter("@id",id)
                                        };

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
    }
}

