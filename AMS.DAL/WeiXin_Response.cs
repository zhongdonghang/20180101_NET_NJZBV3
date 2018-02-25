using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.Model;
using DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace AMS.DAL
{
    public  class WeiXin_Response
    {
        /// <summary>
        /// 根据ID获得一个回复实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public WeiXinResponse GetResponse(int ID)
        {
            
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Text,Type,LoginID,AddDateTime,IsUsed from WeiXin_Response ");
            strSql.Append(" where ID=@ID" );
            strSql.Append(" and IsUsed=1 ");
            strSql.Append(" order by AddDateTime desc");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
                        };
            parameters[0].Value = ID;

           WeiXinResponse model = new WeiXinResponse();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Text"] != null && ds.Tables[0].Rows[0]["Text"].ToString() != "")
                {
                    model.Text = ds.Tables[0].Rows[0]["Text"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Type"] != null && ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LoginID"] != null && ds.Tables[0].Rows[0]["LoginID"].ToString() != "")
                {
                    model.LoginID = new Model.AMS_UserInfo {  ID=int.Parse ( ds.Tables[0].Rows[0]["LoginID"].ToString()) };
                }
                if (ds.Tables[0].Rows[0]["AddDateTime"] != null && ds.Tables[0].Rows[0]["AddDateTime"].ToString() != "")
                {
                    model.AddDateTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddDateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsUsed"] != null && ds.Tables[0].Rows[0]["IsUsed"].ToString() != "")
                {
                    model.IsUsed = int.Parse(ds.Tables[0].Rows[0]["IsUsed"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public WeiXinResponse GetResponseById(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Text,Type,LoginID,AddDateTime,IsUsed from WeiXin_Response ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
                        };
            parameters[0].Value = id;

            WeiXinResponse model = new WeiXinResponse();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Text"] != null && ds.Tables[0].Rows[0]["Text"].ToString() != "")
                {
                    model.Text = ds.Tables[0].Rows[0]["Text"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Type"] != null && ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LoginID"] != null && ds.Tables[0].Rows[0]["LoginID"].ToString() != "")
                {
                    model.LoginID = new Model.AMS_UserInfo { ID = int.Parse(ds.Tables[0].Rows[0]["LoginID"].ToString()) };
                }
                if (ds.Tables[0].Rows[0]["AddDateTime"] != null && ds.Tables[0].Rows[0]["AddDateTime"].ToString() != "")
                {
                    model.AddDateTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddDateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsUsed"] != null && ds.Tables[0].Rows[0]["IsUsed"].ToString() != "")
                {
                    model.IsUsed = int.Parse(ds.Tables[0].Rows[0]["IsUsed"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public WeiXinResponse GetResponseByType(int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Text,Type,LoginID,AddDateTime,IsUsed from WeiXin_Response ");
            strSql.Append(" where Type=@Type and IsUsed=1");
            SqlParameter[] parameters = {
					new SqlParameter("@Type", SqlDbType.Int,4)
                        };
            parameters[0].Value = type;

            WeiXinResponse model = new WeiXinResponse();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Text"] != null && ds.Tables[0].Rows[0]["Text"].ToString() != "")
                {
                    model.Text = ds.Tables[0].Rows[0]["Text"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Type"] != null && ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LoginID"] != null && ds.Tables[0].Rows[0]["LoginID"].ToString() != "")
                {
                    model.LoginID = new Model.AMS_UserInfo { ID = int.Parse(ds.Tables[0].Rows[0]["LoginID"].ToString()) };
                }
                if (ds.Tables[0].Rows[0]["AddDateTime"] != null && ds.Tables[0].Rows[0]["AddDateTime"].ToString() != "")
                {
                    model.AddDateTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddDateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsUsed"] != null && ds.Tables[0].Rows[0]["IsUsed"].ToString() != "")
                {
                    model.IsUsed = int.Parse(ds.Tables[0].Rows[0]["IsUsed"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 更新回复信息
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public bool UpdateResponse(WeiXinResponse response)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeiXin_Response set ");
            strSql.Append("Text=@Text,");
            strSql.Append("Type=@Type,");
            strSql.Append("LoginID=@LoginID,");
            strSql.Append("AddDateTime=@AddDateTime,");
            strSql.Append("IsUsed=@IsUsed");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@Text", SqlDbType.VarChar,50),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@LoginID", SqlDbType.Int),
					new SqlParameter("@AddDateTime", SqlDbType.DateTime),
					new SqlParameter("@IsUsed", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = response.Text;
            parameters[1].Value = response.Type;
            parameters[2].Value = response.LoginID.ID;
            parameters[3].Value = response.AddDateTime;
            parameters[4].Value = response.IsUsed;
            parameters[5].Value = response.ID;

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
        /// 删除一条回复信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  bool DeleteResponse(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeiXin_Response set IsUsed=0 ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int)};
            parameters[0].Value =id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>   
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpResponstype(int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WeiXin_Response set IsUsed=0 ");
            strSql.Append(" where type=@type ");
            SqlParameter[] parameters = {
					new SqlParameter("@type", SqlDbType.Int)};
            parameters[0].Value = type;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 增加一条回复信息
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public  bool AddResponse(WeiXinResponse response)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WeiXin_Response(");
            strSql.Append("Text,Type,LoginID,AddDateTime,IsUsed)");
            strSql.Append(" values (");
            strSql.Append("@Text,@Type,@LoginID,@AddDateTime,@IsUsed)");
           
            SqlParameter[] parameters = {
					new SqlParameter("@Text", SqlDbType.VarChar,50),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@LoginID", SqlDbType.Int),
					new SqlParameter("@AddDateTime", SqlDbType.DateTime),
					new SqlParameter("@IsUsed", SqlDbType.Int,4)};
            parameters[0].Value = response.Text;
            parameters[1].Value = response.Type;
            parameters[2].Value = response.LoginID.ID;
            parameters[3].Value = response.AddDateTime;
            parameters[4].Value = response.IsUsed;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取所有回复信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllResponse()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select r.ID,Text,Type,UserName,AddDateTime,IsUsed from weixin_response r ,Ams_UserInfo u where r.LoginID=u.ID and IsUsed=1");

            return DbHelperSQL.Query(sql.ToString());
        }

        /// <summary>
        /// 获取天气编码
        /// 
        /// </summary>
        /// <returns></returns>
        public AMS.Model.Weather GetWeather(string CityName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select ID,citycode,cityname from weather where cityname=@cityname");
            SqlParameter[] parameters = {
					new SqlParameter("@cityname",CityName)
                                         };
            Weather model = new Weather();
            DataSet ds = DbHelperSQL.Query(sql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["citycode"] != null && ds.Tables[0].Rows[0]["citycode"].ToString() != "")
                {
                    model.WeatherNo = ds.Tables[0].Rows[0]["citycode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["cityname"] != null && ds.Tables[0].Rows[0]["cityname"].ToString() != "")
                {
                    model.WeatherName = ds.Tables[0].Rows[0]["cityname"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }
    }
}
