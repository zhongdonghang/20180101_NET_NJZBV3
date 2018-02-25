using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DBUtility;

namespace AdvertManage.DAL
{
    public partial class SlipReleaseCampus
    {
        public SlipReleaseCampus()
        { }
        /// <summary>
        /// 下发优惠券
        /// </summary>
        /// <param name="CampusID"></param>
        /// <param name="SlipID"></param>
        /// <returns></returns>
        public int Add(int CampusID, int SlipID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SlipReleaseCampus(");
            strSql.Append("SlipCustomerId,CampusId)");
            strSql.Append(" values (");
            strSql.Append("@SlipCustomerId,@CampusId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SlipCustomerId", SqlDbType.Int),
					new SqlParameter("@CampusId", SqlDbType.Int)};
            parameters[0].Value = SlipID;
            parameters[1].Value = CampusID;
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
        /// 查询
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataSet GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT id,SchoolId,SchoolNum,SchoolName,CampusId ,CampusNum,CampusName,SlipCustomerId,SlipCustomerNum,SlipTemplate,IsPrint,LookOverAmount,PrintAmount,[Type],EndDate,EffectDate,CustomerImage,ImageUrl");
            strSql.Append(" FROM View_SlipReleaseCampus ");
            if (strWhere != null && strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

    }
}
