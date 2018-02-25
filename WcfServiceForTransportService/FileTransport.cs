using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using SeatManage.ClassModel;
using SeatManage.EnumType;

namespace WcfServiceForTransportService
{
    /// <summary>
    /// 文件上传下载类
    /// </summary>
    public class FileTransport
    {
        private Int32 bufferLen = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["BufferSize"]);
        private String uploadFolder_ = System.Configuration.ConfigurationManager.AppSettings["SaveFilePath"];
        public String uploadFolder
        {
            get { return uploadFolder_; }
            set { uploadFolder_ = value; }
        }
        /// <summary>
        /// 获取文件片段信息
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="system"></param>
        /// <returns></returns>
        public FileSliceInfo GetFileSliceInfo(string fileName, SeatManageSubsystem system)
        {
            try
            {
                //fileName = fileName.Substring(fileName.IndexOf("\\"));
                string filepath = uploadFolder + string.Format(@"{0}\", system.ToString()) + fileName;
                FileSliceInfo file = new FileSliceInfo();
                file.Name = fileName;
                file.Length = 0;
                file.Offset = 0;
                file.Data = null;
                if (System.IO.File.Exists(filepath + ".tz"))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(filepath + ".tz");
                    file.Length = fi.Length;
                    file.Offset = fi.Length;//返回追加数据后的文件位置  
                    fi = null;
                    return file;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteFile(string fileName, SeatManageSubsystem system)
        {
            try
            {
                //fileName = fileName.Substring(fileName.IndexOf("\\"));
                string filepath = uploadFolder + string.Format(@"{0}\", system.ToString()) + fileName;

                if (System.IO.File.Exists(filepath))
                {
                    File.Delete(filepath);

                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="system"></param>
        /// <returns></returns>
        public FileSliceInfo GetFileInfo(string fileName, SeatManageSubsystem system)
        {
            try
            {
                string filepath = "";
                if (fileName.IndexOf(@"\") == 0)//如果\第一次出现的位置为1，则不用加斜杠
                {
                    filepath = string.Format("{0}{1}{2}", uploadFolder, system.ToString(), fileName);
                }
                else
                {
                    filepath = string.Format(@"{0}{1}\{2}", uploadFolder, system.ToString(), fileName);
                }
                FileSliceInfo file = new FileSliceInfo();
                file.Name = fileName;
                file.Length = 0;
                file.Offset = 0;
                file.Data = null;
                if (System.IO.File.Exists(filepath))
                {
                    //文件已经存在
                    System.IO.FileInfo fi = new System.IO.FileInfo(filepath);
                    file.Length = fi.Length;
                    file.Offset = fi.Length;//返回追加数据后的文件位置  
                    fi = null;
                    return file;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static object UploadObj = new object();
        public Int64 FileUpLoad(String fileName, Byte[] fileByte, Int64 Length, Int64 Offset, SeatManageSubsystem system)
        {
            Int64 ReturnOffset = 0;
            //uploadFolder + string.Format(@"{0}\", system.ToString()) + fileName;
            string filePath = "";
            if (fileName.IndexOf(@"\") == 0)//如果\第一次出现的位置为1，则不用加斜杠
            {
                filePath = string.Format("{0}{1}{2}", uploadFolder, system.ToString(), fileName);
            }
            else
            {
                filePath = string.Format(@"{0}{1}\{2}", uploadFolder, system.ToString(), fileName);
            }
            string directoryPath = filePath.Substring(0, filePath.LastIndexOf("\\"));
            //获取文件的路径,已经保存的文件名  
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            try
            {
                lock (UploadObj)
                {
                    FileStream fs = new FileStream(filePath + ".tz", FileMode.OpenOrCreate);
                    if (fs == null)
                        fs = new FileStream(filePath, FileMode.OpenOrCreate);
                    if (fs.Length == Length)
                    {
                        if (File.Exists(filePath + ".tz"))
                        {
                            File.Move(filePath + ".tz", filePath);
                        }
                        fs.Close();
                    }
                    try
                    {
                        //文件偏移位置,表示从这个位置开始进行后面的数据添加  
                        BinaryWriter writer = new BinaryWriter(fs);//初始化文件写入器 
                        if (Offset >= int.MaxValue)
                        {
                            writer.Seek(int.MaxValue, SeekOrigin.Begin);
                            Offset = Offset - int.MaxValue;
                            while (Offset > 0)//判断Offset是否大于int类型所能表示的最大值
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
                        writer.Write(fileByte);//写入数据  
                        ReturnOffset = fs.Length;//返回追加数据后的文件位置  
                        fileByte = null;
                        writer.Close();
                        fs.Close();
                    }
                    catch (Exception ex)
                    {
                        fs.Close();
                        throw ex;
                    }
                    if (ReturnOffset == Length)
                    {
                        if (File.Exists(filePath + ".tz"))
                        {
                            if (File.Exists(filePath))
                            {
                                bool isDel = false;
                                while (!isDel)
                                {
                                    try
                                    {
                                        File.Delete(filePath);
                                        isDel = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        isDel = false;
                                        System.Threading.Thread.Sleep(500);
                                    }
                                }
                            }
                            File.Move(filePath + ".tz", filePath);
                        }
                    }
                    
                }
                return ReturnOffset;
            }
            catch (Exception ex)
            {
               throw new Exception("锁定的代码内出错：" + ex.Message);
            }
            //try
            //{
                
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("出错在删除临时文件：" + ex.Message);
            //}

        }

        static object downloadObject = new object();
        public byte[] FileDownLoad(String fileName, Int64 Offset, SeatManageSubsystem system)
        {
            try
            {
                int maxSiz = 1024 * bufferLen; //设置每次传100k 
                string filePath = "";
                if (fileName.IndexOf(@"\") == 0)//如果\第一次出现的位置为1，则不用加斜杠
                {
                    filePath = string.Format("{0}{1}{2}", uploadFolder, system.ToString(), fileName);
                }
                else
                {
                    filePath = string.Format(@"{0}{1}\{2}", uploadFolder, system.ToString(), fileName);
                }


                // string filePath = uploadFolder + string.Format(@"{0}", system.ToString()) + fileName;
                //获得客户端信息
                OperationContext context = OperationContext.Current;
                MessageProperties messageProperties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpointProperty = (RemoteEndpointMessageProperty)messageProperties[RemoteEndpointMessageProperty.Name];
                lock(downloadObject)
                {
                    Stream stream =  new FileStream(filePath, FileMode.Open, FileAccess.Read) ;
                    Int64 Length = stream.Length;
                    if (Offset == Length)
                        return null;
                    byte[] ReData = new byte[Length - Offset <= maxSiz ? stream.Length - Offset : maxSiz]; //设置传递的数据的大小
                    stream.Position = Offset; //设置本地文件数据的读取位置  
                    stream.Read(ReData, 0, ReData.Length);//把数据写入到file.Data中  
                    stream.Close();
                    return ReData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        internal FileSliceInfo_Md5 GetFilesSlice_Md5Info(string fileName, SeatManageSubsystem system)
        {
            try
            {
                string filepath = "";
                if (fileName.IndexOf(@"\") == 0)//如果\第一次出现的位置为1，则不用加斜杠
                {
                    filepath = string.Format("{0}{1}{2}", uploadFolder, system.ToString(), fileName);
                }
                else
                {
                    filepath = string.Format(@"{0}{1}\{2}", uploadFolder, system.ToString(), fileName);
                }
                FileSliceInfo_Md5 file = new FileSliceInfo_Md5();
                file.Name = fileName;
                file.Length = 0;
                file.Offset = 0;
                file.Data = null;
                if (System.IO.File.Exists(filepath))
                {
                    //文件已经存在
                    System.IO.FileInfo fi = new System.IO.FileInfo(filepath);
                    file.Md5 = SeatManage.SeatManageComm.SeatComm.GetMD5HashFromFile(filepath);
                    file.Length = fi.Length;
                    file.Offset = fi.Length;//返回追加数据后的文件位置  
                    fi = null;
                    return file;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
