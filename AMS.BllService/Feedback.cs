using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.BllService
{
    public partial class AdvertManageBllService : IBllService.IAdvertManageBllService
    {
        DAL.AMS_Feedback feedbackDal = new DAL.AMS_Feedback();
        public AMS.Model.Enum.HandleResult AddFeedback(AMS.Model.AMS_Feedback model)
        {
            if (feedbackDal.Add(model) > 0)
            {
                return Model.Enum.HandleResult.Successed;
            }
            else
            {
                return Model.Enum.HandleResult.Failed;
            }
        }
    }
}
