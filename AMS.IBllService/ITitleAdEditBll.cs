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
        /// 获取冠名信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<AMS.Model.AMS_TitleAd> GetTitleAdList();
        /// <summary>
        /// 新增冠名广告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string AddNewTitleAd(AMS.Model.AMS_TitleAd model);
        /// <summary>
        /// 更新冠名广告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string UpdateTitleAd(AMS.Model.AMS_TitleAd model);
        /// <summary>
        /// 删除冠名广告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string DeleteTitleAd(AMS.Model.AMS_TitleAd model);

        /// <summary>
        /// 获取单个冠名广告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        Model.AMS_TitleAd GetTitleAdByNum(int Num);
    }
}
