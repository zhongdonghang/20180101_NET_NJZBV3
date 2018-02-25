using System.Collections.Generic;
using AMS.Model;
using SeatManage.AppJsonModel;

namespace WeiXinService
{
    public partial interface IWeiXinService
    {
        /// <summary>
        /// 获取全部的学校
        /// </summary>
        /// <returns></returns>
        List<AMS_School> GetWeCharSchoolList();
        /// <summary>
        /// 获取单个学校的信息
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        AMS_School GetSingleSchoolInfoByID(string schoolId);

        /// <summary>
        /// 验证读者信息
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        AJM_Reader CheckReader(string loginId, string password, string schoolNo);
        /// <summary>
        /// 获取读者信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        AJM_Reader GetReaderInfo(string cardNo, string schoolNo);

        /// <summary>
        /// 获取读者当前状态
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        AJM_ReaderStatus GetReaderNowState(string cardNo, string schoolNo);

        /// <summary>
        /// 获取登录读者详细信息
        /// </summary>
        /// <param name="studentNo"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        AJM_WeiXinUserInfo GetUserInfo_WeiXin(string studentNo, string schoolNo);
    }
}
