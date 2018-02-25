using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.IBllService;
using System.Data;
using AMS.Model;


namespace AMS.BllService
{
    public partial class AdvertManageBllService : IAdvertManageBllService
    {
        private AMS.DAL.AMS_UserInfo dal_UserInfo = new DAL.AMS_UserInfo();
        /// <summary>
        /// 验证登录用户名和密码
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPassword"></param>
        /// <returns></returns>
        public Model.AMS_UserInfo Login(string loginName, string loginPassword)
        {
            AMS_UserInfo model = dal_UserInfo.GetModel(loginName);//通过登录名，获取用户 信息
            if (model != null)//验证登录名是否存在
            {
                string pwdCiphertext = SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(loginPassword);//加密用户密码，
                if (model.UserPwd.Equals(pwdCiphertext))
                {
                    return model;//如果密码和用户名相同，则返回该用户。
                } 
            } 
            return null;//登录名不存在，密码错误，返回null。

        }


        
    }
}
