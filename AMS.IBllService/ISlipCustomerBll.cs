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
        /// 获取全部优惠券
        /// *异常处理：向上抛出
        /// </summary>
        /// <returns>返回优惠券model的list</returns>
        [OperationContract]
        List<AMS.Model.AMS_SlipCustomer> GetSlipCustomerList();

        /// <summary>
        /// 获单个优惠券
        /// *异常处理：向上抛出
        /// </summary>
        /// <returns>返回优惠券model的list</returns>
        [OperationContract]
        AMS.Model.AMS_SlipCustomer GetSlipCustomerByNum(string Num);


        [OperationContract]
        AMS.Model.AMS_SlipCustomer GetSlipCustomerById(int ID);
        /// <summary>
        /// 新增优惠券
        /// *异常处理：catch后return异常信息
        /// *判断：优惠券的编号不能相同
        /// </summary>
        /// <param name="model">优惠券的model</param>
        /// <returns>成功返回null或""失败返回异常或错误信息</returns>
        [OperationContract]
        string AddNewSlipCustomer(AMS.Model.AMS_SlipCustomer model);

        /// <summary>
        /// 更新优惠券
        /// *异常处理：catch后return异常信息
        /// 判断：除了当前需要修改的优惠券，编号不能和其他优惠券重复
        /// </summary>
        /// <param name="model">优惠券的model</param>
        /// <returns>成功返回null或""失败返回异常或错误信息</returns>
        [OperationContract]
        string UpdateSlipCustomer(AMS.Model.AMS_SlipCustomer model);

        /// <summary>
        /// 删除优惠券
        /// *异常处理：catch后return异常信息
        /// </summary>
        /// <param name="model">优惠券的model，只需要ID就行了，其余属性可以为空</param>
        /// <returns>成功返回null或""失败返回异常或错误信息</returns>
        [OperationContract]
        string DeleteSlipCustomer(AMS.Model.AMS_SlipCustomer model);
    }
}
