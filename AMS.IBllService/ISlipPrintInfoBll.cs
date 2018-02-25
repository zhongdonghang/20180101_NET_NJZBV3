using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using AMS.Model.Enum;

namespace AMS.IBllService
{
    public partial interface IAdvertManageBllService 
    {
        /// <summary>
        /// 添加凭条优惠信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        HandleResult AddSlipPrintInfo(List<Model.View_SlipPrintInfo> model);
    }
}
