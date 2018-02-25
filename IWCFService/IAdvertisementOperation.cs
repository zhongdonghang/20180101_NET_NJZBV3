using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 获取单个广告
        /// </summary>
        /// <param name="adNum"></param>
        /// <param name="adType"></param>
        /// <returns></returns>
        [OperationContract]
        ClassModel.AMS_Advertisement GetAdModel(string adNum, SeatManage.EnumType.AdType adType);
        /// <summary>
        /// 获取广告的List
        /// </summary>
        /// <param name="isOverTime">是否过期True为过期广告，False为处于有效期的广告，Null为全部广告</param>
        /// <param name="adType">广告类型</param>
        /// <returns></returns>
        [OperationContract]
        List<SeatManage.ClassModel.AMS_Advertisement> GetAdList(bool? isOverTime, SeatManage.EnumType.AdType adType);

        /// <summary>
        /// 添加广告
        /// </summary>
        /// <param name="adInfo">广告的Model</param>
        /// <returns></returns>
        [OperationContract]
        string AddAdModel(ClassModel.AMS_Advertisement adInfo);

        /// <summary>
        /// 更新广告
        /// </summary>
        /// <param name="adInfo">广告的Model</param>
        /// <returns></returns>
        [OperationContract]
        string UpdateAdModel(ClassModel.AMS_Advertisement adInfo);

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="adInfo">广告的Model</param>
        /// <returns></returns>
        [OperationContract]
        string DeleteAdModel(ClassModel.AMS_Advertisement adInfo);

        /// <summary>
        /// 更新广告使用情况
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string UpdateAdvertUsage(ClassModel.AMS_AdvertUsage model);

        /// <summary>
        /// 获取广告使用状态
        /// </summary>
        /// <param name="AdNum"></param>
        /// <param name="adType"></param>
        /// <returns></returns>
        [OperationContract]
        ClassModel.AMS_AdvertUsage GetAdvertUsage(string AdNum, SeatManage.EnumType.AdType adType);

        /// <summary>
        /// 获取所有广告使用情况
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ClassModel.AMS_AdvertUsage> GetAdvertUsageList();
    }
}
