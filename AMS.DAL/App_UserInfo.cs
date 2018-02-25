using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AMS.Model;
using DBUtility;

namespace AMS.DAL
{
    public class App_UserInfoDal
    {
        /// <summary>
        /// 通过学号和学校编号获取app的用户信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="schoolNum"></param>
        /// <returns></returns>
        public AppUserInfo GetAppUserInfoByCardNoAndSchoolNum(string cardNo, string schoolNum)
        {
            string cmdStr = "SELECT [schoolNum] ,[cardNo],[userId] ,[channelId] ,[schoolId] ,[schoolName] ,[ConnectionString] FROM  [View_AppUserInfo] where cardNo=@cardNo and schoolNum=@schoolNum";
            SqlParameter[] parameters = {
					new SqlParameter("@cardNo", SqlDbType.NVarChar,30),
					new SqlParameter("@schoolNum", SqlDbType.NVarChar,50)
                                       };
            parameters[0].Value = cardNo;
            parameters[1].Value = schoolNum;
            DataSet ds = DBUtility.DbHelperSQL.Query(cmdStr, parameters);
            AppUserInfo model = null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                model = new AppUserInfo();
                model.CardNo = ds.Tables[0].Rows[0]["cardNo"].ToString();
                model.ChannelId = ds.Tables[0].Rows[0]["channelId"].ToString();
                model.SchoolId = int.Parse(ds.Tables[0].Rows[0]["schoolId"].ToString());
                model.SchoolName = ds.Tables[0].Rows[0]["schoolName"].ToString();
                model.SchoolNumber = ds.Tables[0].Rows[0]["schoolNum"].ToString();
                model.UserId = ds.Tables[0].Rows[0]["userId"].ToString();
            }
            return model;
        }

        public bool AddAppUserInfo(AppUserInfo model)
        {
            string cmdstr = "INSERT INTO  [App_UserInfo] ([cardNo] ,[userId]  ,[channelId]  ,[schoolId])  VALUES  (@cardNo ,@userId ,@channelId ,@schoolId )";
            SqlParameter[] parameters = {
					new SqlParameter("@cardNo", SqlDbType.NVarChar,30),
					new SqlParameter("@userId", SqlDbType.NVarChar,50),
                    new SqlParameter("@channelId", SqlDbType.NVarChar,50),
                    new SqlParameter("@schoolId", SqlDbType.Int),
                                       };
            parameters[0].Value = model.CardNo;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.ChannelId;
            parameters[3].Value = model.SchoolId;

            int i = DBUtility.DbHelperSQL.ExecuteSql(cmdstr, parameters);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据channelId执行更新操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateAppUserInfoByChannelId(AppUserInfo model)
        {
            string cmdstr = "UPDATE  [App_UserInfo]  SET [cardNo] = @cardNo ,[userId] = @userId  ,[schoolId] = @schoolId WHERE  [channelId] = @channelId ";
            SqlParameter[] parameters = {
					new SqlParameter("@cardNo", SqlDbType.NVarChar,30),
					new SqlParameter("@userId", SqlDbType.NVarChar,50),
                    new SqlParameter("@channelId", SqlDbType.NVarChar,50),
                    new SqlParameter("@schoolId", SqlDbType.Int),
                                       };
            parameters[0].Value = model.CardNo;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.ChannelId;
            parameters[3].Value = model.SchoolId;
            int i = DBUtility.DbHelperSQL.ExecuteSql(cmdstr, parameters);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据学校Id和学号执行更新操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateAppUserInfoByCardNoAndSchoolId(AppUserInfo model) 
        {
            string cmdstr = "UPDATE  [App_UserInfo]  SET  [userId] = @userId ,[channelId] = @channelId  WHERE  [cardNo] = @cardNo and [schoolId] = @schoolId ";
            SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.NVarChar,50),
                    new SqlParameter("@channelId", SqlDbType.NVarChar,50),
					new SqlParameter("@cardNo", SqlDbType.NVarChar,30),
                    new SqlParameter("@schoolId", SqlDbType.Int),
                                       };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.ChannelId;
            parameters[2].Value = model.CardNo;
            parameters[3].Value = model.SchoolId;
            int i = DBUtility.DbHelperSQL.ExecuteSql(cmdstr, parameters);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否存在该channelId
        /// </summary>
        public bool Exists(string channelId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from App_UserInfo");
            strSql.Append(" where channelId=@channelId");
            SqlParameter[] parameters = {
					new SqlParameter("@channelId", SqlDbType.NVarChar,50)
            };
            parameters[0].Value = channelId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        //public bool Exists(string cardNo, string schoolNum)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select count(1) from View_AppUserInfo");
        //    strSql.Append(" where schoolNum=@schoolNum and cardNo=@cardNo ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@schoolNum", SqlDbType.NVarChar,50),
        //            new SqlParameter("@cardNo", SqlDbType.NVarChar,20)
        //    };
        //    parameters[0].Value = cardNo;
        //    parameters[1].Value = schoolNum;
        //    return DbHelperSQL.Exists(strSql.ToString(), parameters);
        //}

    }
}
