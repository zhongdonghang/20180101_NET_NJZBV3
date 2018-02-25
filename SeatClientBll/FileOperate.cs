using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.IO;
using System.Diagnostics;
using SeatManage.Bll;
using SeatManage.EnumType;
using IWCFService.TransportService;
using SeatManage.SeatManageComm;
using System.ServiceModel;

namespace SeatManage.Bll
{

    public delegate void EventHandleFileTransport(int message);
    public delegate void EventHandleFileOperateError(string message);
    /// <summary>
    /// 文件上传下载类
    /// </summary>
    public class FileOperate
    {
        /// <summary>
        /// 下载完成触发事件
        /// </summary>
        public event EventHandleFileTransport Downloaded;
        /// <summary>
        /// 上传完成触发事件
        /// </summary>
        public event EventHandleFileTransport Updated;
        /// <summary>
        /// 完成进度
        /// </summary>
        public event EventHandleFileTransport HandleProgress;
        /// <summary>
        /// 下载失败事件
        /// </summary>
        public event EventHandleFileOperateError DownloadError;
        public List<FileSimpleInfo> GetLocalFolderFile(string dirPath)
        {
            List<FileSimpleInfo> files = new List<FileSimpleInfo>();
            DirectoryInfo mydir = new DirectoryInfo(dirPath);
            foreach (FileInfo file in mydir.GetFiles())
            {
                FileSimpleInfo fileInfo = new FileSimpleInfo();
                fileInfo.Name = file.Name;
                fileInfo.Length = file.Length;
                fileInfo.ModifyDateTime = file.LastWriteTime;
                fileInfo.Version = FileVersionInfo.GetVersionInfo(file.FullName).FileVersion == null ? "" : FileVersionInfo.GetVersionInfo(file.FullName).FileVersion;
                files.Add(fileInfo);
            }
            return files;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fullPath">文件完整路径</param>
        /// <param name="relativePath">相对路径</param>
        /// <param name="system">系统类型</param>
        /// <returns></returns>
        public bool UpdateFile(string fullPath, string relativePath, SeatManageSubsystem system)
        {
            //上传出错重试，每次重试50次,每次上传成功，初始化重试次数  2013-9-4 王随
            for (int i = 0; i < 50; i++)
            {
                try
                {
                    string fileName = relativePath;// fullPath.Substring(fullPath.LastIndexOf(@"\") + 1);
                    int maxSiz = 1024 * 1000; //设置每次传100k   
                    FileStream stream = System.IO.File.OpenRead(fullPath);
                    //读取本地文件  
                    FileSliceInfo file = FileTransportBll.GetFileSliceInfo(fileName, system); //更加文件名,查询服务中是否存在该文件  
                    if (file == null) //表示文件不存在  
                    {
                        file = new FileSliceInfo();
                        file.Offset = 0; //设置文件从开始位置进行数据传递  
                    }
                    file.Length = stream.Length;
                    if (file.Length == file.Offset)
                    //如果文件的长度等于文件的偏移量，说明文件已经上传完成 ，重新上传
                    {
                        file.Offset = 0; //设置文件从开始位置进行数据传递    
                    }
                    file.Name = fileName;
                    while (file.Length != file.Offset) //循环的读取文件,上传，直到文件的长度等于文件的偏移量  
                    {
                        file.Data = new byte[file.Length - file.Offset <= maxSiz ? file.Length - file.Offset : maxSiz]; //设置传递的数据的大小
                        stream.Position = file.Offset; //设置本地文件数据的读取位置  
                        stream.Read(file.Data, 0, file.Data.Length);//把数据写入到file.Data中  
                        file = FileTransportBll.FileUpLoad(file, system); //上传 
                        i = 0;//初始化重试次数
                        if (HandleProgress != null)
                        {
                            int progress = (int)((double)file.Offset / (double)file.Length * 100);
                            HandleProgress(progress);
                        }
                    }

                    stream.Close();
                    stream.Dispose();
                    stream = null;

                    return true;
                }
                catch
                {
                }
            }
            return false;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath">文件的完整</param>
        /// <param name="relativePath">文件的相对路径</param>
        /// <param name="system">系统名称</param>
        /// <returns></returns>
        public bool FileDownLoad(string filePath, string relativePath, SeatManageSubsystem system)
        {
            for (int i = 0; i < 50; i++)
            {
                //获取文件的路径,已经保存的文件名  
                FileStream fs = null;
                BinaryWriter writer = null;//初始化文件写入器  
                try
                {

                    string fileName = relativePath;// filePath.Substring(filePath.LastIndexOf(@"\") + 1);
                    string dirPath = filePath.Substring(0, filePath.LastIndexOf(@"\") + 1);
                    FileSliceInfo file2 = FileTransportBll.GetFilesSlice_Md5Info(fileName, system);
                    FileSliceInfo_Md5 f = file2 as FileSliceInfo_Md5;
                    if (file2 == null)
                    {
                        if (DownloadError != null)
                        {
                            DownloadError(string.Format("文件{0}下载失败。", fileName));
                        }
                        return false;
                    }
                    else if (File.Exists(filePath))
                    {
                        //先设置文件只读属性，然后再删除，不然无法删除文件
                        File.SetAttributes(filePath, System.IO.FileAttributes.Normal);
                        if (f.Md5 == SeatManageComm.SeatComm.GetMD5HashFromFile(filePath))
                        {
                            return true;
                        }
                    }
                    file2.Name = fileName;
                    file2.Offset = 0;

                    //如果文件目录不存在,创建文件所存放的目录.
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }

                    //文件先下载，下载完成后再删除
                    fs = new FileStream(filePath + ".tz", FileMode.OpenOrCreate);

                    file2.Offset = fs.Length;
                    if (file2.Offset != file2.Length)
                    {
                        //文件偏移位置,表示从这个位置开始进行后面的数据添加  
                        writer = new BinaryWriter(fs);//初始化文件写入器  
                        file2 = FileTransportBll.FileDownLoad(file2, system);
                        i = 0;
                        while (file2.Data != null)
                        {
                            //打开文件  
                            long Offset = file2.Offset; //file.Offset 

                            if (Offset >= int.MaxValue)
                            {
                                writer.Seek(int.MaxValue, SeekOrigin.Begin);
                                Offset = Offset - int.MaxValue;
                                while (Offset > 0)
                                {
                                    if (Offset >= int.MaxValue)
                                    {
                                        writer.Seek(int.MaxValue, SeekOrigin.Current);
                                        Offset = Offset - int.MaxValue;
                                    }
                                    else
                                    {
                                        writer.Seek(int.Parse(Offset.ToString()), SeekOrigin.Current);
                                        Offset = 0;
                                    }
                                }
                            }
                            else
                            {
                                writer.Seek(Int32.Parse(Offset.ToString()), SeekOrigin.Begin);//设置文件的写入位置  
                            }

                            writer.Write(file2.Data);//写入数据  
                            file2.Offset = fs.Length;//返回追加数据后的文件位置  
                            file2.Data = null;
                            file2 = FileTransportBll.FileDownLoad(file2, system);
                            i = 0;
                            if (HandleProgress != null)
                            {
                                int progress = (int)(((double)file2.Offset / (double)((long)file2.Length)) * 100);
                                HandleProgress(progress);
                            }
                        }
                        writer.Close();
                        fs.Close();
                    }
                    else//否则文件已经下载完成，只是还没有更改后缀，则关闭文件流
                    {
                        fs.Close();
                    }
                    if (File.Exists(filePath + ".tz"))
                    {
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                        File.Move(filePath + ".tz", filePath);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 文件上传断点续传
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="fileByte">文件流</param>
        /// <param name="Length">文件大小</param>
        /// <param name="Offset">偏移量</param>
        /// <returns></returns>
        public static bool FileDelete(string file, SeatManageSubsystem system)
        {
            IFileTransportService fileTransport = WcfAccessProxy.ServiceProxy.CreateChannelFileTransportService();
            bool isError = false;
            try
            {
                return fileTransport.DeleteFile(file, system);
            }
            catch (Exception ex)
            {
                isError = true;
                WriteLog.Write(string.Format("文件删除出错，异常信息：{0}", ex.Message));
                return false;
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
