using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.Model;
using SeatManage.SeatManageComm; 
using IWCFService.TransportService;
using SeatManage.EnumType;
using System.ServiceModel;
using SeatManage.ClassModel;


namespace AMS.ServiceProxy
{
    /// <summary>
    /// 文件上传下载操作
    /// </summary>
    public class FileTransportBll
    { 

        public static FileSliceInfo GetFileSliceInfo(string fileName, SeatManageSubsystem system)
        {
            IFileTransportService fileTransport = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateChannelFileTransportService();
            bool isError = false;
            try
            {
                return fileTransport.GetFilesSliceInfo(fileName, system);
            }
            catch (Exception ex)
            {
                isError = true;
                 WriteLog.Write(string.Format("获取文件片段信息失败，异常信息：{0}", ex.Message));
                throw ex;
                //return null;
            }
            finally
            {
                ICommunicationObject ICommObjectService = fileTransport as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }
        }
        /// <summary>
        /// 获取包含Md5的信息
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static FileSliceInfo GetFilesSlice(string fileName, SeatManageSubsystem system)
        {
            IFileTransportService fileTransport = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateChannelFileTransportService();
           
            try
            {
                return fileTransport.GetFilesSliceInfo(fileName, system);
            }
            catch (Exception ex)
            { 
                WriteLog.Write(string.Format("获取包含Md5的文件片段信息失败，异常信息：{0}", ex.Message));
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = fileTransport as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }
        }

        /// <summary>
        /// 获取文件片段信息
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static FileSliceInfo GetFileInfo(string fileName, SeatManageSubsystem system)
        {
            IFileTransportService fileTransport = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateChannelFileTransportService();
            bool isError = false;
            try
            {
                return fileTransport.GetFileInfo(fileName,  system);
            }
            catch (Exception ex)
            {
                isError = true;
                WriteLog.Write(string.Format("获取文件片段信息失败，异常信息：{0}", ex.Message));
               throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = fileTransport as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }
        }
        /// <summary>
        /// 文件上传断点续传
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="fileByte">文件流</param>
        /// <param name="Length">文件大小</param>
        /// <param name="Offset">偏移量</param>
        /// <returns></returns>
        public static FileSliceInfo FileUpLoad(FileSliceInfo file, SeatManageSubsystem system)
        {
            IFileTransportService fileTransport = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateChannelFileTransportService();
            bool isError = false;
            try
            {
                file.Offset = fileTransport.FileUpLoad(file.Name, file.Data, file.Length, file.Offset,  system);
                return file;
            }
            catch (Exception ex)
            {
                isError = true;
                WriteLog.Write(string.Format("文件上传出错，异常信息：{0}", ex.Message));
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = fileTransport as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }
        }
        /// <summary>
        /// 文件下载断点续传
        /// </summary>
        /// <param name="fileName"></param> 
        /// <param name="Offset"></param>
        /// <returns></returns>
        public static FileSliceInfo FileDownLoad(FileSliceInfo file,SeatManageSubsystem system)
        {
            IFileTransportService fileTransport = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateChannelFileTransportService();
            bool isError = false;
            try
            {
                file.Data = fileTransport.FileDownLoad(file.Name, file.Offset,  system);
                return file;
            }
            catch (Exception ex)
            {
                isError = true;
                WriteLog.Write(string.Format("文件下载出错，异常信息：{0}", ex.Message));
                throw;
            }
            finally
            {
                ICommunicationObject ICommObjectService = fileTransport as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }
        }
       
    }
}
