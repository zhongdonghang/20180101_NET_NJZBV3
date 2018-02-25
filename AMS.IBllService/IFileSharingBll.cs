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
        /// 获取全部共享文件列表
        /// *异常处理：catch后向上抛出
        /// </summary>
        /// <returns>获取成功后返回一个model的list</returns>
        [OperationContract]
        List<AMS.Model.FileSharingInfo> GetSharingFileList();
        /// <summary>
        /// 添加共享文件
        /// *异常处理：catch后return异常
        /// </summary>
        /// <param name="model">共享文件的model</param>
        /// <returns>成功返回空，失败返回错误信息</returns>
        [OperationContract]
        string AddNewSharingFile(AMS.Model.FileSharingInfo model);
        /// <summary>
        /// 修改共享文件
        /// *异常处理：catch后return异常
        /// </summary>
        /// <param name="model">共享文件的model</param>
        /// <returns>成功返回空，否则返回错误信息</returns>
        [OperationContract]
        string UpdateSharingFile(AMS.Model.FileSharingInfo model);
        /// <summary>
        /// 删除共享文件
        /// *异常处理：catch后return异常
        /// </summary>
        /// <param name="model">共享文件的model，只需要ID，其他属性可以为空</param>
        /// <returns>成功返回空，失败返回错误信息</returns>
        [OperationContract]
        string DeleteSharingFile(AMS.Model.FileSharingInfo model);
    }
}
