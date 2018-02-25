using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;
using AuthorizeVerify;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 获取功能授权信息（Host服务根目录）
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        FunctionAuthorizeInfo GetFunctionAuthorize();
        /// <summary>
        /// 从指定路径获取授权文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [OperationContract]
        FunctionAuthorizeInfo GetFunctionAuthorizeFile(string filePath);
        /// <summary>
        /// 保存服务授权文件到本地(Host服务根目录)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool SaveFunctionAuthorize(FunctionAuthorizeInfo model);
        /// <summary>
        /// 授权功能名称
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        [OperationContract]
        bool IsAuthorize(string functionName);
    }
}
