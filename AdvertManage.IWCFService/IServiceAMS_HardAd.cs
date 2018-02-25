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
        /// 根据Id获取硬广信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
       AdvertManage.Model.AMS_HardAdModel GetHardAdById(int id);

        /// <summary>
        /// 根据编号获取硬广
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [OperationContract]
        AdvertManage.Model.AMS_HardAdModel GetHardAdByNum(string number);
        /// <summary>
        /// 获取硬广
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<AdvertManage.Model.AMS_HardAdModel> GetHardAdList();
        /// <summary>
        /// 添加硬广
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult AddHardAd(Model.AMS_HardAdModel model);
        /// <summary>
        /// 更新硬广
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult UpdateHardAd(Model.AMS_HardAdModel model);

        /// <summary>
        /// 根据Id删除硬广
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult DeleteHardAd(int id);
    }
}
