using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.IBllService;
using System.Data;

namespace AMS.BllService
{
    public partial class AdvertManageBllService : IAdvertManageBllService
    {
        AMS.DAL.AMS_UserInfo dal_User = new DAL.AMS_UserInfo();
        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <returns></returns>
        public List<Model.AMS_UserInfo> GetUserInfo()
        {
            try
            {
                List<Model.AMS_UserInfo> modellist = new List<Model.AMS_UserInfo>();
                DataSet ds = dal_User.GetList(null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modellist.Add(DataRowToAMS_UserInfoModel(ds.Tables[0].Rows[i]));
                }
                return modellist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取单个用户
        /// </summary>
        /// <param name="loginID"></param>
        /// <returns></returns>
        public Model.AMS_UserInfo GetSingleUserInfo(string loginID)
        {
            try
            {
                return dal_User.GetModel(loginID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加新用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddNewUser(Model.AMS_UserInfo model)
        {
            try
            {
                AMS.Model.AMS_UserInfo sameModel = dal_User.GetModel(model.LoginId);
                if (sameModel != null)
                {
                    return "存在相同的登录名！";
                }
                if (dal_User.Add(model) == 0)
                {
                    return "添加失败！";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string UpdateUser(Model.AMS_UserInfo model)
        {
            try
            {
                AMS.Model.AMS_UserInfo sameModel = dal_User.GetModel(model.LoginId);
                if (sameModel != null && model.ID != sameModel.ID)
                {
                    return "存在相同的登录名！";
                }
                if (!dal_User.Update(model))
                {
                    return "更新失败！";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string DeleteUser(Model.AMS_UserInfo model)
        {
            try
            {
                if (!dal_User.Delete(model.ID))
                {
                    return "删除失败！";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private AMS.Model.AMS_UserInfo DataRowToAMS_UserInfoModel(DataRow dr)
        {
            AMS.Model.AMS_UserInfo model = new Model.AMS_UserInfo();
            model.ID = int.Parse(dr["ID"].ToString());
            model.LoginId = dr["LoginId"].ToString();
            model.Remark = dr["Remark"].ToString();
            model.BranchName = dr["BranchName"].ToString();
            model.UserName = dr["UserName"].ToString();
            model.UserPwd = dr["UserPwd"].ToString();
            return model;
        }
    }
}
