using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DBUtility;

namespace AMS.DAL
{
    [Serializable]
    public class View_RollTitles
    {
        public View_RollTitles()
        {
        }
        /// <summary>
        /// 获取单个实例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.View_RollTitles SelectRollTitlesByID(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select  Name, EffectDate, EndDate, Type, CustomerId, CompanyName, CustomerNo,");
            sql.Append(" LinkWay, Describe, LoginId, UserPwd, BranchName, UserName, Remark, Operator, ID from dbo.View_RollTitles");
            sql.Append("where ID=@ID");
            SqlParameter[] sqlpar ={
                                       new SqlParameter("@ID",id)
                                };
            Model.View_RollTitles model = new Model.View_RollTitles();
            DataSet ds = DbHelperSQL.Query(sql.ToString(), sqlpar);
            if (ds.Tables[0].Rows.Count>0)
            {
                if (ds.Tables[0].Rows[0]["Name"] != null && ds.Tables[0].Rows[0]["Name"].ToString() != "")
                {
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EffectDate"] != null && ds.Tables[0].Rows[0]["EffectDate"].ToString() != "")
                {
                    model.EffectDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["EffectDate"]);
                }
                if (ds.Tables[0].Rows[0]["EndDate"] != null && ds.Tables[0].Rows[0]["EndDate"].ToString() != "")
                {
                    model.EndDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["EndDate"]);
                }
                if (ds.Tables[0].Rows[0]["Type"] != null && ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = ds.Tables[0].Rows[0]["Type"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CustomerId"] != null && ds.Tables[0].Rows[0]["CustomerId"].ToString() != "")
                {
                    model.CustomerId = Convert.ToInt32(ds.Tables[0].Rows[0]["CustomerId"]);
                }
                if (ds.Tables[0].Rows[0]["CompanyName"] != null && ds.Tables[0].Rows[0]["CompanyName"].ToString() != "")
                {
                    model.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CustomerNo"] != null && ds.Tables[0].Rows[0]["CustomerNo"].ToString() != "")
                {
                    model.CompanyName = ds.Tables[0].Rows[0]["CustomerNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LinkWay"] != null && ds.Tables[0].Rows[0]["LinkWay"].ToString() != "")
                {
                    model.LinkWay = ds.Tables[0].Rows[0]["LinkWay"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Describe"] != null && ds.Tables[0].Rows[0]["Describe"].ToString() != "")
                {
                    model.Describe = ds.Tables[0].Rows[0]["Describe"].ToString();
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
                if (ds.Tables[0].Rows[0]["Operator"] != null && ds.Tables[0].Rows[0]["Operator"].ToString() != "")
                {
                    model.Oprator = Convert.ToInt32(ds.Tables[0].Rows[0]["Operator"]);
                }
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public DataSet GetList()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select  Name, EffectDate, EndDate, Type, CustomerId, CompanyName, CustomerNo,");
            sql.Append(" LinkWay, Describe, LoginId, UserPwd, BranchName, UserName, Remark, Operator, ID from dbo.View_RollTitles");

            return DbHelperSQL.Query(sql.ToString());
        }
    }
}
