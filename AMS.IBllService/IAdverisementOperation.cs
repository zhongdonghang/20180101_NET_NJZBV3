using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AMS.IBllService
{
    public partial interface IAdvertManageBllService
    {
        /// <summary>
        /// 获取广告列表
        /// </summary>
        /// <param name="adType"></param>
        /// <returns></returns>
        [OperationContract]
        List<AMS.Model.AMS_Advertisement> GetAdvertList(AMS.Model.Enum.AdType adType);
        /// <summary>
        /// 获取当前学校正在播放的广告
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <returns></returns>
        [OperationContract]
        List<AMS.Model.AMS_AdvertisementSchoolCopy> GetSchoolNowAdvert(int SchoolID);

        /// <summary>
        /// 获取学校列表
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <param name="AdID"></param>
        /// <returns></returns>
        [OperationContract]
        AMS.Model.AMS_AdvertisementSchoolCopy GetSchoolAdvert(int AdID);

        [OperationContract]
        AMS.Model.AMS_AdvertisementSchoolCopy GetSchholAdvertByNum(string AdNum, string schoolNum, AMS.Model.Enum.AdType adType);

        [OperationContract]
        AMS.Model.AMS_AdvertisementSchoolCopy GetSameSchoolAdvert(int SchoolID, int OriginalID);
        /// <summary>
        /// 添加广告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddAdvertisement(AMS.Model.AMS_Advertisement model);
        /// <summary>
        /// 更新广告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateAdvertisement(AMS.Model.AMS_Advertisement model);
        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteAdvertisement(AMS.Model.AMS_Advertisement model);
        /// <summary>
        /// 添加广告备份
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddAdvertisementCopy(AMS.Model.AMS_AdvertisementSchoolCopy model);
        /// <summary>
        /// 更新广告备份
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateAdvertisementCopy(AMS.Model.AMS_AdvertisementSchoolCopy model);
        /// <summary>
        /// 删除广告备份
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteAdvertisementCopy(AMS.Model.AMS_AdvertisementSchoolCopy model);
        /// <summary>
        /// 获取单独的广告
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [OperationContract]
        AMS.Model.AMS_Advertisement GetSingleAdvertisement(int ID);
        /// <summary>
        /// 判断同名广告是否存在
        /// </summary>
        /// <param name="Num"></param>
        /// <param name="Name"></param>
        /// <param name="adType"></param>
        /// <returns></returns>
        [OperationContract]
        bool ExistSameAdvert(string Num, string Name, AMS.Model.Enum.AdType adType);
        /// <summary>
        /// 获取记录列表
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <param name="AdID"></param>
        /// <returns></returns>
        [OperationContract]
        List<AMS.Model.AMS_AdvertUsage> GetAdvertUsage(int SchoolID, int AdID);

        /// <summary>
        /// 上传使用记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpLoadGetAdvertUsage(AMS.Model.AMS_AdvertUsage model);
    }
}
