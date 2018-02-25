using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.IPocketBespeak
{
    public interface IFeedbackBll
    {
        /// <summary>
        /// 添加反馈信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        AMS.Model.Enum.HandleResult AddFeedback(AMS.Model.AMS_Feedback model);
    }
}
