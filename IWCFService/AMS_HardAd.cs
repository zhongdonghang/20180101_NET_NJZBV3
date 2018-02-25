using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SeatManage.ClassModel;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 添加硬广
        /// </summary>
        /// <param name="hardAdvert"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.EnumType.HandleResult AddHardAd(HardAdvertInfo hardAdvert);
        /// <summary>
        /// 获取有效的硬广
        /// </summary>
        /// <returns></returns>
        [OperationContract]
          HardAdvertInfo GetHardAdvert();

        /// <summary>
        /// 根据编号获取硬广
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        [OperationContract]
        HardAdvertInfo GetHardAdvertByNum(string num);

        /// <summary>
        /// 更新硬广
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.EnumType.HandleResult UpdateHardAdvert(HardAdvertInfo model);
        /// <summary>
        /// 删除硬广
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.EnumType.HandleResult DeleteHardAdvert(HardAdvertInfo model);
        /// <summary>
        /// 获取过期的硬广
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<HardAdvertInfo> GetHardAdvertOverTime();
    }
}
