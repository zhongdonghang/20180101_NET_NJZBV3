using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using AdvertManage.Model.Enum;

namespace AdvertManage.IWCFService
{
    public partial interface IAdvertManageService
    {
        /// <summary>
        /// 添加凭条优惠信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        HandleResult AddSlipPrintInfo(List<Model.AMS_SlipPrintInfoModel> model);
    }
}
