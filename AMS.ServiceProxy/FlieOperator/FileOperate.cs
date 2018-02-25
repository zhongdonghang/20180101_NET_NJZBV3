using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using SeatManage.ClassModel;
using SeatManage.EnumType;

namespace AMS.ServiceProxy
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
        public string UpdateFile(string fullPath,string relativePath, SeatManageSubsystem system)
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
                    return "";
                }
                catch
                { 
                }
            }
            return "上传失败！";
        }
        /// <summary>
        /// 下载文件
        /// 每次下载遇到错误重试50次
        /// </summary>
        /// <param name="filePath">文件的完整</param>
        /// <param name="relativePath">文件的相对路径</param>
        /// <param name="system">系统名称</param>
        /// <returns></returns>
        public string FileDownLoad(string filePath,string relativePath, SeatManageSubsystem system)
        { 
            //添加下载错误自动断点下载,每次出错重试50次。 2013-9-4 作者：王随
            for (int i = 0; i < 50;i++ )
            {
                //获取文件的路径,已经保存的文件名  
                FileStream fs = null;
                BinaryWriter writer = null;
                try
                {
                    string fileName = relativePath;// filePath.Substring(filePath.LastIndexOf(@"\") + 1);
                    string dirPath = filePath.Substring(0, filePath.LastIndexOf(@"\") + 1);
                    FileSliceInfo file2 = FileTransportBll.GetFileInfo(fileName, system);
                    if (file2 == null)
                    {
                        if (DownloadError != null)
                        {
                            DownloadError(string.Format("文件{0}下载失败。", fileName));
                        }
                        return string.Format("文件{0}下载失败。", fileName);
                    }
                    file2.Name = fileName;
                    file2.Offset = 0;

                    //如果文件目录不存在,创建文件所存放的目录.
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }

                   
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    fs = new FileStream(filePath + ".tz", FileMode.OpenOrCreate);

                    file2.Offset = fs.Length;
                    if (file2.Offset != file2.Length)
                    {
                        //文件偏移位置,表示从这个位置开始进行后面的数据添加  
                          writer = new BinaryWriter(fs);//初始化文件写入器  
                        file2 = FileTransportBll.FileDownLoad(file2, system);
                        i = 0;//下载成功，把重试次数初始化
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

                        if (File.Exists(filePath + ".tz"))
                        {
                            File.Move(filePath + ".tz", filePath);
                        }
                    }
                    return "";
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
                   // result = false; 
                }
            }
            return "下载失败！";
        }

    }
}
