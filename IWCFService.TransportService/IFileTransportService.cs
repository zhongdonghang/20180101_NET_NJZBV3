using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SeatManage.ClassModel;
using SeatManage.EnumType;

namespace IWCFService.TransportService
{
    /// <summary>
    /// 文件传输服务
    /// </summary>
    [ServiceContract]
    public interface IFileTransportService
    {
        /// <summary>
        /// 获取文件信息 
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        [OperationContract]
        FileSliceInfo GetFileInfo(string fileName,SeatManageSubsystem system);
        /// <summary>
        /// 删除文件 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="system"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteFile(string fileName, SeatManageSubsystem system);
         /// <summary>
        /// 获取参数中包含的文件片段信息
        /// </summary>
        /// <param name="FilesName">文件名称</param>
        /// <returns></returns>
        [OperationContract]
        FileSliceInfo GetFilesSliceInfo(string filesName, SeatManageSubsystem system);
        /// <summary>
        /// 获取参数中包含的文件片段信息,含Md5
        /// </summary>
        /// <param name="filesName"></param>
        /// <param name="system"></param>
        /// <returns></returns>
         [OperationContract]
        FileSliceInfo_Md5 GetFilesSlice_Md5Info(string filesName, SeatManageSubsystem system);
        /// <summary>
        /// 文件上传断点续传
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="fileByte">文件流</param>
        /// <param name="Length">文件大小</param>
        /// <param name="Offset">偏移量</param>
        /// <returns></returns>
        [OperationContract]
        Int64 FileUpLoad(String fileName, Byte[] fileByte, Int64 Length, Int64 Offset, SeatManageSubsystem system); //上传文件  
        /// <summary>
        /// 文件下载断点续传
        /// </summary>
        /// <param name="fileName"></param> 
        /// <param name="Offset"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] FileDownLoad(String fileName, Int64 Offset, SeatManageSubsystem system);
       
         
    }

    
}
