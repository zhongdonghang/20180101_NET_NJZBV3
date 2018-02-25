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
        AdvertManage.Model.AMS_SlipCustomerModel GetSlipCustomerById(int id);
        /// <summary>
        /// 根据编号获取优惠券客户信息
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [OperationContract]
        AdvertManage.Model.AMS_SlipCustomerModel GetSlipCustomerByNum(string number);
        /// <summary>
        /// 获取所有优惠券客户信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Model.AMS_SlipCustomerModel> GetSlipCustomerList();
        /// <summary>
        /// 更新优惠券客户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult UpdateSlipCustomer(Model.AMS_SlipCustomerModel model);
        /// <summary>
        /// 添加优惠券客户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult AddSlipCustomer(Model.AMS_SlipCustomerModel model);

        /// <summary>
        /// 删除优惠券客户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult DeleteSlipCustomer(int id);
       
    }
}
