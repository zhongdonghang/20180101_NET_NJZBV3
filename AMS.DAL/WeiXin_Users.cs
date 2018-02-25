using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBUtility;
using System.Data;
using AMS.Model;
using System.Data.SqlClient; 

namespace AMS.DAL
{
    public class Weixin_Users
    {
        AMS_School schoolDal = new AMS_School();
        public WeiXinUsers GetWeiXinUser(string weixinID)
        {
            WeiXinUsers weixinuser = null;
            StringBuilder sql = new StringBuilder();
            sql.Append("select CardNo,SchoolID,WeiXinID from WeiXin_Reader ");
            sql.Append("where WeiXinID =@weixinID");
            SqlParameter[] parameter ={
                          new SqlParameter("@weixinID", SqlDbType.VarChar)      
                                        };
            parameter[0].Value = weixinID;
            DataSet ds = DbHelperSQL.Query(sql.ToString(), parameter);
            if (ds.Tables[0].Rows.Count > 0)
            {
                weixinuser = new WeiXinUsers();
                weixinuser.SchoolInfo = new AMS.Model.AMS_School();
                if (ds.Tables[0].Rows[0]["CardNo"] != null && ds.Tables[0].Rows[0]["CardNo"].ToString() != "")
                {
                    weixinuser.CardNo = ds.Tables[0].Rows[0]["CardNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WeiXinID"] != null && ds.Tables[0].Rows[0]["WeiXinID"].ToString() != "")
                {
                    weixinuser.WeixinID = ds.Tables[0].Rows[0]["WeiXinID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SchoolID"] != null && ds.Tables[0].Rows[0]["SchoolID"].ToString() != "")
                {
                    weixinuser.SchoolInfo = schoolDal.GetModel(int.Parse(ds.Tables[0].Rows[0]["SchoolID"].ToString()));
                  
                }
                return weixinuser;
            }
            return null;
        }
        /// <summary>
        /// 根据用户Id，
        /// </summary>
        /// <param name="weixinUser">微信用户</param>
        /// <returns></returns>
        public WeiXinUsers GetWeiXinUserByWeixinId(string weixinId)
        {  
            WeiXinUsers weixinuser = null;
            StringBuilder sql = new StringBuilder();
            sql.Append("select [CardNo],[SchoolID]  ,[WeiXinID] ,[SchoolNum]  ,[SchoolName] ,[ConnectionString] ,[Describe] from [View_WeiXinReader]");
            sql.Append(" where WeiXinID =@weixinID");
            SqlParameter[] parameter ={ 
                          new SqlParameter("@weixinID", SqlDbType.VarChar) 
                                        };
            parameter[0].Value = weixinId; 
            DataSet ds = DbHelperSQL.Query(sql.ToString(), parameter);
            if (ds.Tables[0].Rows.Count > 0)
            {
                weixinuser = new WeiXinUsers();
                weixinuser.SchoolInfo = new  Model.AMS_School();
                if (ds.Tables[0].Rows[0]["CardNo"] != null && ds.Tables[0].Rows[0]["CardNo"].ToString() != "")
                {
                    weixinuser.CardNo = ds.Tables[0].Rows[0]["CardNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WeiXinID"] != null && ds.Tables[0].Rows[0]["WeiXinID"].ToString() != "")
                {
                    weixinuser.WeixinID = ds.Tables[0].Rows[0]["WeiXinID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SchoolID"] != null && ds.Tables[0].Rows[0]["SchoolID"].ToString() != "")
                {
                    weixinuser.SchoolInfo.Id = (int)ds.Tables[0].Rows[0]["SchoolID"];

                }
                weixinuser.SchoolInfo.Name = ds.Tables[0].Rows[0]["SchoolName"].ToString();
                weixinuser.SchoolInfo.Number = ds.Tables[0].Rows[0]["SchoolNum"].ToString();
                weixinuser.SchoolInfo.ConnectionString = ds.Tables[0].Rows[0]["ConnectionString"].ToString();
                return weixinuser;
            }
            return null;
        }

        public WeiXinUsers GetWeiXinUserByUserInfo(string cardNo, string schoolNum)
        {
            WeiXinUsers weixinuser = null;
            StringBuilder sql = new StringBuilder();
            sql.Append("select [CardNo],[SchoolID]  ,[WeiXinID] ,[SchoolNum]  ,[SchoolName] ,[ConnectionString] ,[Describe] from [View_WeiXinReader]");
            sql.Append(" where CardNo=@CardNo and SchoolNum=@SchoolNum");
            SqlParameter[] parameter ={ 
                          new SqlParameter("@SchoolNum",SqlDbType.NVarChar),
                          new SqlParameter("@CardNo",SqlDbType.NVarChar)
                                        };
            parameter[0].Value = schoolNum;
            parameter[1].Value = cardNo;
            DataSet ds = DbHelperSQL.Query(sql.ToString(), parameter);
            if (ds.Tables[0].Rows.Count > 0)
            {
                weixinuser = new WeiXinUsers();
                weixinuser.SchoolInfo = new  Model.AMS_School();
                if (ds.Tables[0].Rows[0]["CardNo"] != null && ds.Tables[0].Rows[0]["CardNo"].ToString() != "")
                {
                    weixinuser.CardNo = ds.Tables[0].Rows[0]["CardNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WeiXinID"] != null && ds.Tables[0].Rows[0]["WeiXinID"].ToString() != "")
                {
                    weixinuser.WeixinID = ds.Tables[0].Rows[0]["WeiXinID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SchoolID"] != null && ds.Tables[0].Rows[0]["SchoolID"].ToString() != "")
                {
                    weixinuser.SchoolInfo.Id = (int)ds.Tables[0].Rows[0]["SchoolID"];

                }
                weixinuser.SchoolInfo.Name = ds.Tables[0].Rows[0]["SchoolName"].ToString();
                weixinuser.SchoolInfo.Number = ds.Tables[0].Rows[0]["SchoolNum"].ToString();
                weixinuser.SchoolInfo.ConnectionString = ds.Tables[0].Rows[0]["ConnectionString"].ToString();
                return weixinuser;
            }
            return null;
        }


        /// <summary>
        /// 添加绑定
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public bool InsertUserInfo(WeiXinUsers userinfo)
        {
            string weixinID = userinfo.WeixinID;
            string schoolID = userinfo.SchoolInfo.Id.ToString();
            string CardNo = userinfo.CardNo;

            string sql = string.Format("insert into WeiXin_Reader (CardNo,SchoolID,WeiXinID) values('{0}',{1},'{2}')", CardNo, schoolID, weixinID);
            if (DbHelperSQL.ExecuteSql(sql) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除绑定
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public bool DeleteBind(string weixinID)
        {

            string sql = string.Format("delete from  WeiXin_Reader where WeiXinID='{0}'", weixinID);
            if (DbHelperSQL.ExecuteSql(sql) > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 更新绑定
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public bool UpdateBind(WeiXinUsers userinfo)
        {
            string weixinID = userinfo.WeixinID;
            string schoolID = userinfo.SchoolInfo.Id.ToString();
            string CardNo = userinfo.CardNo;
            string sql = string.Format("update WeiXin_Reader set CardNo='{0}',SchoolID='{1}',WeiXinID='{2}' where WeiXinID='{2}' or (SchoolID='{1}' and CardNo='{0}')", CardNo, schoolID, weixinID);
            if (DbHelperSQL.ExecuteSql(sql) > 0)
            {
                return true;
            }
            return false;
        }


    }
}
