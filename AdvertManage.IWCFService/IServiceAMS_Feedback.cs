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
        /// 添加学校信息
        /// </summary>
        /// <param name="model"> 学校信息</param>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult AddFeedback(Model.AMS_FeedbackModel model);
        /// <summary>
        /// 捕获异常信息到数据库
        /// </summary>
        /// <param name="error"></param>
        [OperationContract]
        void AddErrorMessage(string error);

        /// <summary>
        /// 网站访问量
        /// </summary>
        /// <param name="error"></param>
        [OperationContract]
        void SetPageView(string id);
    }
}
