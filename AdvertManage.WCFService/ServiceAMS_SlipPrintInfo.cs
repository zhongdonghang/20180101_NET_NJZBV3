using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvertManage.IWCFService;
using System.ServiceModel;
using AdvertManage.Model.Enum;

namespace AdvertManage.WCFService
{
    public partial class AdvertManageService : IAdvertManageService
    {
        DAL.AMS_SlipPrintInfoDal SlipPrintDal = new DAL.AMS_SlipPrintInfoDal();
        /// <summary>
        /// 添加凭条优惠信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public HandleResult AddSlipPrintInfo(List<Model.AMS_SlipPrintInfoModel> modelList)
        {
            try
            {
                foreach (Model.AMS_SlipPrintInfoModel model in modelList)
                {
                    Model.AMS_CampusModel campusModel = GetCampusInfoByNum(model.CompusNum);
                    Model.AMS_SlipCustomerModel slipCustomer = GetSlipCustomerByNum(model.SlipCustomerNum);
                    if (slipCustomer != null)
                    {
                        model.CampusId = campusModel.Id;
                        model.SlipCustomerId = slipCustomer.Id;
                        SlipPrintDal.Add(model);
                    }
                }
                return HandleResult.Successed;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
