using System;
using System.Collections.Generic;
using AMS.Model;
using AMS.ServiceProxy;
using SeatManage.AppJsonModel;
using SeatManage.MobileAppDataObtainProxy;
using SeatManage.SeatManageComm;

namespace WeiXinService
{
    public partial class WeiXinServiceHepler : IWeiXinService
    {
        /// <summary>
        /// 获取全部的学校
        /// </summary>
        /// <returns></returns>
        public List<AMS_School> GetWeCharSchoolList()
        {
            return AMS_SchoolProxy.GetAllSchool();
        }
        /// <summary>
        /// 获取单个图书馆信息
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public AMS_School GetSingleSchoolInfoByID(string schoolId)
        {
            return AMS_SchoolProxy.GetSchoolById(int.Parse(schoolId));
        }
        /// <summary>
        /// 获取读者信息
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public AJM_Reader CheckReader(string loginId, string password, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.CheckUser(loginId, password);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("验证读者信息失败！");
                }
                if (!ajmResult.Result)
                {
                    ajmResult = null;
                    throw new Exception(ajmResult.Msg);
                }
                result = appService.GetUserInfo(loginId);
                ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("验证读者信息失败！");
                }
                if (!ajmResult.Result)
                {
                    ajmResult = null;
                    throw new Exception(ajmResult.Msg);
                }
                AJM_Reader ajmReader = JSONSerializer.Deserialize<AJM_Reader>(ajmResult.Msg);
                return ajmReader;
            }
            catch (Exception ex)
            {
                WriteLog.Write("验证读者信息失败：" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 获取读者信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public AJM_Reader GetReaderInfo(string cardNo, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.GetUserInfo(cardNo);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("获取读者信息失败！");
                }
                if (!ajmResult.Result)
                {
                    throw new Exception(ajmResult.Msg);
                }
                AJM_Reader ajm_reader = JSONSerializer.Deserialize<AJM_Reader>(ajmResult.Msg);
                return ajm_reader;
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取读者信息失败：" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 获取读者当前状态
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public AJM_ReaderStatus GetReaderNowState(string cardNo, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.GetUserNowState(cardNo);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("获取读者当前状态失败！");
                }
                if (!ajmResult.Result)
                {
                    throw new Exception(ajmResult.Msg);
                }
                AJM_ReaderStatus ajmReaderStatus = JSONSerializer.Deserialize<AJM_ReaderStatus>(ajmResult.Msg);
                return ajmReaderStatus;
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取读者当前状态失败：" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 获取登录读者详细信息
        /// </summary>
        /// <param name="studentNo"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public AJM_WeiXinUserInfo GetUserInfo_WeiXin(string studentNo, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.GetUserInfo_WeiXin(studentNo);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("获取登录读者详细信息失败！");
                }
                if (!ajmResult.Result)
                {
                    throw new Exception(ajmResult.Msg);
                }
                AJM_WeiXinUserInfo ajmWeiXinUserInfo = JSONSerializer.Deserialize<AJM_WeiXinUserInfo>(ajmResult.Msg);
                return ajmWeiXinUserInfo;
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取登录读者详细信息失败：" + ex.Message);
                return null;
            }
        }
    }
}
