using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;
using SeatManage.EnumType;
using IWCFService.TransportService;
using System.ServiceModel;

namespace SeatManage.Bll
{
    /// <summary>
    /// 系统更新
    /// </summary>
    public class FileTransportBll
    {
       
        public static List<FileSimpleInfo> GetFilesInfo(List<string> filesName, SeatManageSubsystem system)
        {
            IWCFService.ISeatManageService seatService = WcfAccessProxy.ServiceProxy.CreateChannelSeatManageService();
            bool isError = false;
            try
            {
                return seatService.GetFilesInfo(filesName, system);
            }
            catch (Exception ex)
            {
                isError = true;
                WriteLog.Write(string.Format("更新出错，获取文件信息失败：{0}",ex.Message));
                return new List<FileSimpleInfo>();
            }
            finally
            {
                ICommunicationObject ICommObjectService = seatService as ICommunicationObject;
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
        /// 获取更新信息
        /// </summary>
        /// <returns></returns>
        public static FileUpdateInfo GetUpdateInfo(SeatManageSubsystem system)
        {
            IWCFService.ISeatManageService seatService = WcfAccessProxy.ServiceProxy.CreateChannelSeatManageService();
            bool isError = false;
            try
            {
                return seatService.GetUpdateInfo(system);
            }
            catch (Exception ex)
            {
                isError = true;
                WriteLog.Write(string.Format("更新出错，获取更新信息失败：{0}", ex.Message));
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = seatService as ICommunicationObject;
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
        public static FileSliceInfo_Md5 GetFilesSlice_Md5Info(string fileName, SeatManageSubsystem system)
        {
            IFileTransportService fileTransport = WcfAccessProxy.ServiceProxy.CreateChannelFileTransportService();

            try
            {
                return fileTransport.GetFilesSlice_Md5Info(fileName, system);
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

        public static FileSliceInfo GetFileSliceInfo(string fileName, SeatManageSubsystem system)
        {
            IFileTransportService  fileTransport= WcfAccessProxy.ServiceProxy.CreateChannelFileTransportService();
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
            }
            finally
            {
                ICommunicationObject ICommObjectService =  fileTransport as ICommunicationObject;
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
            IFileTransportService fileTransport = WcfAccessProxy.ServiceProxy.CreateChannelFileTransportService();
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
            IFileTransportService fileTransport = WcfAccessProxy.ServiceProxy.CreateChannelFileTransportService();
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
            IFileTransportService fileTransport = WcfAccessProxy.ServiceProxy.CreateChannelFileTransportService();
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
        public static bool Add(FileUpdateInfo model)
        {
            IWCFService.ISeatManageService service = new WcfServiceForSeatManage.SeatManageDateService();
            bool isError = false;
            try
            {
                 return service.AddUpdaterFileInfo(model);
                 
            }
            catch (Exception ex)
            {
                isError = true;
                WriteLog.Write(string.Format("添加出错，异常信息：{0}", ex.Message));
               throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = service as ICommunicationObject;
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
        public static bool Update(FileUpdateInfo model)
        {
            IWCFService.ISeatManageService service = new WcfServiceForSeatManage.SeatManageDateService();
            bool isError = false;
            try
            {
                return service.UpdateFileInfo(model); 
            }
            catch (Exception ex)
            {
                isError = true;
                WriteLog.Write(string.Format("添加出错，异常信息：{0}", ex.Message));
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = service as ICommunicationObject;
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
