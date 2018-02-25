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
        AMS.DAL.AMS_EnterOutLog EDAL = new DAL.AMS_EnterOutLog();
        public Model.Enum.HandleResult AddEnterOutLogList(List<Model.AMS_EnterOutLog> logList)
        {
            try
            {
                Model.AMS_School schoolModel = null;
                foreach (AMS.Model.AMS_EnterOutLog model in logList)
                {
                    if (schoolModel == null)
                    {
                        schoolModel = GetSchoolInfoById(model.Schoolid);
                    }
                    model.Schoolid = schoolModel.Id;
                    EDAL.Add(model);
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
