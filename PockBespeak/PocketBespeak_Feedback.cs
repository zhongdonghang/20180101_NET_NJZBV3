using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.PocketBespeak
{
    public class PocketBespeak_Feedback : SeatManage.IPocketBespeak.IFeedbackBll
    {
        /// <summary>
        /// 反馈信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AMS.Model.Enum.HandleResult AddFeedback(AMS.Model.AMS_Feedback model)
        {
            return AMS.ServiceProxy.AMS_Feedback.Add_Feedback(model);
        }
    }
}
