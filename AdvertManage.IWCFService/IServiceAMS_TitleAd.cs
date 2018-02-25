using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AdvertManage.IWCFService
{
    public partial interface IAdvertManageService
    {
        /// <summary>
        /// 根据Id获取优惠券信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        AdvertManage.Model.AMS_TitleAdModel GetTitleAdById(int id);
        
        /// <summary>
        /// 获取冠名广告信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Model.AMS_TitleAdModel> GetTitleAd();
        /// <summary>
        /// 添加冠名广告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult AddTitleAd(Model.AMS_TitleAdModel model);
        /// <summary>
        /// 更新冠名广告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult UpdateTitleAd(Model.AMS_TitleAdModel model);
        /// <summary>
        /// 删除冠名广告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult DeleteTitleAd(int id);
    }
}
