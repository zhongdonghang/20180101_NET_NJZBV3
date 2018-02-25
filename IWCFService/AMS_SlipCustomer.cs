using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 获取有效的优惠券客户信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SeatManage.ClassModel.AMS_SlipCustomer> GetSlipCustomerList(string campusNum);
        /// <summary>
        /// 更新优惠券信息
        /// </summary>
        /// <param name="slipCustomer"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.EnumType.HandleResult UpdateSlipCustomer(SeatManage.ClassModel.AMS_SlipCustomer slipCustomer);
        /// <summary>
        /// 更新优惠券信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SeatManage.EnumType.HandleResult AddSlipCustomer(SeatManage.ClassModel.AMS_SlipCustomer model);

        /// <summary>
        /// 根据编号获取优惠客户信息
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.ClassModel.AMS_SlipCustomer GetSlipCustomerByNum(string num);
        /// <summary>
        /// 删除优惠券
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SeatManage.EnumType.HandleResult DeleteSlipCustomer(SeatManage.ClassModel.AMS_SlipCustomer model);
        /// <summary>
        /// 添加优惠券打印次数
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SeatManage.EnumType.HandleResult AddSlipCustomerPrintCount(string num);
        /// <summary>
        /// 添加优惠券查看次数
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SeatManage.EnumType.HandleResult AddSlipCustomerLookCount(string num);
        /// <summary>
        /// 获取过期的优惠券客户信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SeatManage.ClassModel.AMS_SlipCustomer> GetSlipCustomerListOverTime(SeatManage.EnumType.LogStatus status);
       
    }
}
