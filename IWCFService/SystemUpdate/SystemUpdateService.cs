using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SeatManage.ClassModel;
using SeatManage.EnumType;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 获取参数中包含的文件信息
        /// </summary>
        /// <param name="FilesName">文件名称</param>
        /// <returns></returns>
        [OperationContract]
        List<FileSimpleInfo> GetFilesInfo(List<string> filesName,SeatManageSubsystem system);
        /// <summary>
        /// 获取更新信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        FileUpdateInfo GetUpdateInfo(SeatManageSubsystem system);
        /// <summary>
        /// 更新自动更新信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateFileInfo(FileUpdateInfo model);
        /// <summary>
        /// 添加自动更新信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddUpdaterFileInfo(FileUpdateInfo model);
    }
}
