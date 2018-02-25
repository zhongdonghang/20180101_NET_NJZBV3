using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AMS.Model;
using DBUtility;

namespace AMS.DAL
{
    public  class WeiXin_Advertise
    {
        /// <summary>
        /// 获取所有微信广告
        /// </summary>
        /// <returns></returns>
        public DataSet GetAdvertises()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.ID,Image,Title,Url ,ContentXML,Username,DateTime from WeiXin_Advertise a,dbo.AMS_UserInfo am where a.LoginID=am.ID");
            return DbHelperSQL.Query(sql.ToString());
        }
        /// <summary>
        /// 更新一条广告信息
        /// </summary>
        /// <param name="advertise"></param>
        /// <returns></returns>
        public bool UpdateAdvertise(WeiXinAdvertse advertise)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeiXin_Advertise set ");
            if (advertise.Image!=null)
            {
                strSql.Append("Image=@Image,");
            }
            strSql.Append("Title=@Title,");
            strSql.Append("Url=@Url,");
            strSql.Append("ContentXML=@ContentXML,");
            strSql.Append("LoginID=@LoginID,");
            strSql.Append("DateTime=@DateTime");
            strSql.Append(" where ID=@ID");
            List<SqlParameter> par =  new List<SqlParameter>{
					new SqlParameter("@Title", advertise.Title),
					new SqlParameter("@Url", advertise.Url),
					new SqlParameter("@ContentXML", advertise.ContentXML),
					new SqlParameter("@LoginID", advertise.LoginID.ID),
					new SqlParameter("@DateTime", advertise.DateTime),
					new SqlParameter("@ID", advertise.ID)};
            if (advertise.Image!=""&&advertise.Image!=null)
            {
                par.Add(new SqlParameter("@Image", advertise.Image));
            }
            SqlParameter[] parameters = par.ToArray();

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteAdvertise(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WeiXin_Advertise ");
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
        /// 添加一条广告信息
        /// </summary>
        /// <param name="advertise"></param>
        /// <returns></returns>
        public bool AddAdvertise(WeiXinAdvertse advertise)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WeiXin_Advertise(");
            strSql.Append("Image,Title,Url,ContentXML,LoginID,DateTime)");
            strSql.Append(" values (");
            strSql.Append("@Image,@Title,@Url,@ContentXML,@LoginID,@DateTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Image", SqlDbType.VarChar),
					new SqlParameter("@Title", SqlDbType.VarChar,20),
					new SqlParameter("@Url", SqlDbType.VarChar,50),
					new SqlParameter("@ContentXML", SqlDbType.VarChar),
					new SqlParameter("@LoginID", SqlDbType.Int),
					new SqlParameter("@DateTime", SqlDbType.DateTime)};
            parameters[0].Value = advertise.Image;
            parameters[1].Value = advertise.Title;
            parameters[2].Value = advertise.Url;
            parameters[3].Value = advertise.ContentXML;
            parameters[4].Value = advertise.LoginID;
            parameters[5].Value = advertise.DateTime;


            if (DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据ID获得广告MODEL
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public WeiXinAdvertse GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Image,Title,Url,ContentXML,LoginID,DateTime from WeiXin_Advertise ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
                            };
            parameters[0].Value = ID;

            WeiXinAdvertse model = new WeiXinAdvertse();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Image"] != null && ds.Tables[0].Rows[0]["Image"].ToString() != "")
                {
                    model.Image = ds.Tables[0].Rows[0]["Image"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Title"] != null && ds.Tables[0].Rows[0]["Title"].ToString() != "")
                {
                    model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Url"] != null && ds.Tables[0].Rows[0]["Url"].ToString() != "")
                {
                    model.Url = ds.Tables[0].Rows[0]["Url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ContentXML"] != null && ds.Tables[0].Rows[0]["ContentXML"].ToString() != "")
                {
                    model.ContentXML = ds.Tables[0].Rows[0]["ContentXML"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LoginID"] != null && ds.Tables[0].Rows[0]["LoginID"].ToString() != "")
                {
                    model.LoginID = new Model.AMS_UserInfo { ID = int.Parse(ds.Tables[0].Rows[0]["LoginID"].ToString()) };
                }
                if (ds.Tables[0].Rows[0]["DateTime"] != null && ds.Tables[0].Rows[0]["DateTime"].ToString() != "")
                {
                    model.DateTime = DateTime.Parse(ds.Tables[0].Rows[0]["DateTime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public List<string> GetJosn(string id)
        {
            string sql = string.Format("select [ContentXML] from [WeiXin_Advertise] where ID in ({0})",id);
            DataSet ds = DbHelperSQL.Query(sql, null);
            List<string> list = new List<string>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    list.Add(ds.Tables[0].Rows[i][0].ToString());
                }
                return list;
            }
            return null;
        }
    }
}
