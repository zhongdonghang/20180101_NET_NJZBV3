using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IWCFService.TransportService;
using SeatManage.ClassModel;
using System.Configuration;
using System.ServiceModel;
using SeatManage.EnumType;

namespace WcfServiceForTransportService
{
    /// <summary>
    /// 文件上传下载服务
    /// </summary> 
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class TransportService : IFileTransportService
    {
        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public FileSliceInfo GetFileInfo(string fileName, SeatManageSubsystem system)
        {
            try
            {
                FileTransport filetransport = new FileTransport();
                return filetransport.GetFileInfo(fileName, system);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FileSliceInfo_Md5 GetFilesSlice_Md5Info(string fileName, SeatManageSubsystem system)
        {
            try
            {
                FileTransport filetransport = new FileTransport();
                return filetransport.GetFilesSlice_Md5Info(fileName, system);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取文件片段信息
        /// </summary>
        /// <param name="filesName"></param>
        /// <param name="system"></param>
        /// <returns></returns>
        public FileSliceInfo GetFilesSliceInfo(string filesName, SeatManageSubsystem system)
        {
            try
            {
                FileTransport filetransport = new FileTransport();
                return filetransport.GetFileSliceInfo(filesName, system);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 文件上传，可进行断点续传
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="fileByte">文件二进制流</param>
        /// <param name="Length">文件长度</param>
        /// <param name="Offset">文件起始位置</param>
        /// <returns></returns>
        public long FileUpLoad(string fileName, byte[] fileByte, long Length, long Offset, SeatManageSubsystem system)
        {
            try
            {
                FileTransport filetransport = new FileTransport();
                return filetransport.FileUpLoad(fileName, fileByte, Length, Offset, system);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 文件下载，可以进行断点续传
        /// </summary>
        /// <param name="fileName">需要下载的文件名称</param>
        /// <param name="Offset">需要下载的文件起始位置</param>
        /// <returns></returns>
        public byte[] FileDownLoad(string fileName, long Offset, SeatManageSubsystem system)
        {
            try
            {
                FileTransport filetransport = new FileTransport();
                return filetransport.FileDownLoad(fileName, Offset, system);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="system"></param>
        /// <returns></returns>
        public bool DeleteFile(string fileName, SeatManageSubsystem system)
        {
            try
            {
                FileTransport filetransport = new FileTransport();
                return filetransport.DeleteFile(fileName, system);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
