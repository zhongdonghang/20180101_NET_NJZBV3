using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.IBllService;
using AMS.Model.Enum;

namespace AMS.BllService
{
    public partial class AdvertManageBllService : IAdvertManageBllService
    {
        DAL.AMS_SlipPrintInfo SlipPrintDal = new DAL.AMS_SlipPrintInfo();

        public Model.Enum.HandleResult AddSlipPrintInfo(List<Model.View_SlipPrintInfo> model)
        {
            try
            {
                foreach (Model.View_SlipPrintInfo models in model)
                {
                    Model.AMS_Campus campusModel = GetSingleCampusInfo(models.SchoolNumber,false);
                    Model.AMS_SlipCustomer slipCustomer = GetSlipCustomerByNum(models.SlipCustomerNum);
                    if (slipCustomer != null)
                    {
                        models.CampusId = campusModel.Id;
                        models.SlipCustomerId = slipCustomer.Id;
                        Model.AMS_SlipPrintInfo addmodel = new Model.AMS_SlipPrintInfo
                        {
                            SlipCustomerId = models.SlipCustomerId,
                            Date = models.Date,
                            CampusId = models.CampusId,
                            LookOverAmount = models.LookOverAmount,
                            PrintAmount = models.PrintAmount
                        };
                        SlipPrintDal.Add(addmodel);
                    }
                }
                return HandleResult.Successed;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
