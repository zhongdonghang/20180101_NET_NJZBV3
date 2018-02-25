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
        /// 获取顾客信息 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<AMS.Model.AMS_AdCustomer> GetAdCustomerList();
        /// <summary>
        /// 新增客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string AddNewCustomer(AMS.Model.AMS_AdCustomer model);
        /// <summary>
        /// 更新客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string UpdateCustomer(AMS.Model.AMS_AdCustomer model);
        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string DeleteCustomer(AMS.Model.AMS_AdCustomer model);
    }
}
