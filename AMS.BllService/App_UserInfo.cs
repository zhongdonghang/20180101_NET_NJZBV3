using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.IBllService;

namespace AMS.BllService
{
    public partial class AdvertManageBllService : IAdvertManageBllService
    {
        AMS.DAL.App_UserInfoDal app_userInfoDal = new DAL.App_UserInfoDal();
        /// <summary>
        /// 根据学号和学校编号湖区用户的app信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="schoolNum"></param>
        /// <returns></returns>
        public Model.AppUserInfo GetAppUserInfoByCardNoAndSchoolNum(string cardNo, string schoolNum)
        {
            return app_userInfoDal.GetAppUserInfoByCardNoAndSchoolNum(cardNo, schoolNum);
        }
        /// <summary>
        /// 绑定用户的app信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool BindAppUserInfo(Model.AppUserInfo model)
        {
            if (!string.IsNullOrEmpty(model.ChannelId))
            {
                if (app_userInfoDal.Exists(model.ChannelId))
                {
                    return app_userInfoDal.UpdateAppUserInfoByChannelId(model);
                }
                else
                {
                    Model.AppUserInfo user = app_userInfoDal.GetAppUserInfoByCardNoAndSchoolNum(model.CardNo, model.SchoolNumber);
                    if (user != null)
                    {
                        model.SchoolId = user.SchoolId;
                       
                        return app_userInfoDal.UpdateAppUserInfoByCardNoAndSchoolId(model);
                    }
                    else
                    {
                        return app_userInfoDal.AddAppUserInfo(model);
                    } 
                }

            }
            else
            {
                return false;
            }
        }
    }
}
