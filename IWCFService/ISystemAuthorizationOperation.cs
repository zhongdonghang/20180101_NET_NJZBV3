using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 获取系统授权
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ClassModel.SystemAuthorization GetSystemAuthorization();
        /// <summary>
        /// 保存授权文件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool SaveSystemAuthorization(ClassModel.SystemAuthorization model);
    }
}
