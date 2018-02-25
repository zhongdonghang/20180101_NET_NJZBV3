using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AMS.IBllService
{
    public partial interface IAdvertManageBllService
    {
        /// <summary>
        /// 获取反馈信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<AMS.Model.AMS_CallBackErrorInfo> GetCallBackInfo();
        /// <summary>
        /// 根据解决状态获取信息
        /// </summary>
        /// <param name="SolveStatic">解决状态</param>
        /// <returns></returns>
        [OperationContract]
        List<AMS.Model.AMS_CallBackErrorInfo> GetCallBackInfoBySolveStatic(string SolveStatic);
        /// <summary>
        /// 新增反馈信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string AddNewCallBackInfo(AMS.Model.AMS_CallBackErrorInfo model);
        /// <summary>
        /// 更新反馈信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string UpdateCallBackInfo(AMS.Model.AMS_CallBackErrorInfo model);
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        string DeleteCallBackInfo(AMS.Model.AMS_CallBackErrorInfo model);
    }
}
