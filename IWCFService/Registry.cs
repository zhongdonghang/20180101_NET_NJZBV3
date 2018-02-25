using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 获取注册表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ClassModel.RegistryKey GetRegistryKey();
        /// <summary>
        /// 保存注册表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool SaveRegistryKey(ClassModel.RegistryKey model);
        /// <summary>
        /// 获取服务上的学校编号
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetSchoolNum();
        /// <summary>
        /// 验证查询接口
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool ReadingRoomInterfaceIsAuthorize();
        /// <summary>
        /// 验证门禁接口
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool AccessInterfaceIsAuthorize();
        /// <summary>
        /// 验证发布器接口
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool MediaReleaseIsAuthorize();
    }
}
