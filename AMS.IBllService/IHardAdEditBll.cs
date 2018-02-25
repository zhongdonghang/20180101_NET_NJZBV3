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
        /// 获取硬广信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<AMS.Model.AMS_HardAd> GetHardAdList();
        /// <summary>
        /// 获得硬广Model
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        AMS.Model.AMS_HardAd GetHardAdModel(string name);
        /// <summary>
        /// 新增硬广
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string AddNewHardAd(AMS.Model.AMS_HardAd model);
        /// <summary>
        /// 更新硬广
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string UpdateHardAd(AMS.Model.AMS_HardAd model);
        /// <summary>
        /// 删除硬广
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string DeleteHardAd(AMS.Model.AMS_HardAd model);

        /// <summary>
        /// 获得硬广Model
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        AMS.Model.AMS_HardAd GetHardAdModelByNum(int Num);
    }
}
