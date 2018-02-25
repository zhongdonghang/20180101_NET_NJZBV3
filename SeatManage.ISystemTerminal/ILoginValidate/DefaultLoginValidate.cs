using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SeatManage.DAL;
using SeatManage.EnumType;

namespace SeatManage.ISystemTerminal.ILoginValidate
{
    public class DefaultLoginValidate : ILoginValidate
    {
        public string CheckUser(string loginId, string password)
        {
            try
            {
                IWCFService.ISeatManageService seatService = WcfAccessProxy.ServiceProxy.CreateChannelSeatManageService();
                using (seatService as IDisposable)
                {
                    SeatManage.ClassModel.UserInfo reader = seatService.GetUserInfo(loginId);
                    if (reader != null)
                    {
                        if (reader.Password.Equals(SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(password)) && reader.IsUsing == EnumType.LogStatus.Valid)
                            return reader.LoginId;
                        else
                            return "";
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class DataBaseModeLogin : ILoginValidate
    {
        public string CheckUser(string loginId, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(loginId))
                {
                    return null;
                }
                Users_ALL dalUser_All = new Users_ALL();
                string strWhere = "LoginId=@loginId";
                SqlParameter[] parameters = {
                                            new SqlParameter("@loginId",loginId)
                                        };
                DataSet ds = dalUser_All.GetList(strWhere, parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ClassModel.UserInfo userInfo = new ClassModel.UserInfo();
                    userInfo.LoginId = ds.Tables[0].Rows[0]["LoginID"].ToString();
                    userInfo.Password = ds.Tables[0].Rows[0]["UsrPwd"].ToString();
                    userInfo.IsUsing = (LogStatus)int.Parse(ds.Tables[0].Rows[0]["UsrEnabled"].ToString());
                    if (userInfo.Password.Equals(SeatManageComm.MD5Algorithm.GetMD5Str32(password)) && userInfo.IsUsing == LogStatus.Valid)
                    {
                        return userInfo.LoginId;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
