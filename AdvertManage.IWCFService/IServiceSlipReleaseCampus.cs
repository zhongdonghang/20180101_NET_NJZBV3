using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using AdvertManage.Model;

namespace AdvertManage.IWCFService
{
    public partial interface IAdvertManageService
    {
        /// <summary>
        /// 添加发布信息
        /// </summary>
        /// <param name="slipId"></param>
        /// <param name="CampusId"></param>
        /// <returns></returns>
        [OperationContract]
        int AddSlipRelease(int slipId, int CampusId);
        /// <summary>
        /// 根据学校id查询
        /// </summary>
        /// <param name="schoolid"></param>
        /// <returns></returns>
        [OperationContract]
        List<AMS_SlipCustomerModel> GetSlipReleaseListBySchoolId(int schoolid);
        /// <summary>
        /// 根据Id查询优惠券下发信息
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [OperationContract]
         AMS_SlipCustomerModel  GetSlipReleaseListById(int  id);
        /// <summary>
        /// 根据学校ID查询
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        [OperationContract]
        List<AMS_SlipCustomerModel> GetSlipReleaseListBySchoolNum(string num);
        /// <summary>
        /// 根据校区id查询
        /// </summary>
        /// <param name="campusid"></param>
        /// <returns></returns>
        [OperationContract]
        List<AMS_SlipCustomerModel> GetSlipReleaseListByCampusId(int campusid);
        /// <summary>
        /// 根据校区编号查询
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        [OperationContract]
        List<AMS_SlipCustomerModel> GetSlipReleaseListByCampusNum(string num);
    }
}
