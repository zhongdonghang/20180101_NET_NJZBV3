using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.IO;
using System.Configuration;

namespace SeatClient.Updater.Code
{
    public delegate void EventHandlerUpdateMessage(string message);
    public class UpdateOperate
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 下载结束事件
        /// </summary>
        public event EventHandler Downloaded;
        /// <summary>
        /// 处理错误事件
        /// </summary>
        public event EventHandlerUpdateMessage HandlerError;

        SeatManage.Bll.FileOperate downloadFile = new SeatManage.Bll.FileOperate();
        /// <summary>
        /// 
        /// </summary>
        public SeatManage.Bll.FileOperate DownloadFile
        {
            get { return downloadFile; }
            set { downloadFile = value; }
        }
        ///// <summary>
        ///// 获取本地的文件更新信息
        ///// </summary>
        ///// <param name="system">系统名称</param>
        ///// <returns></returns>
        //private FileUpdateInfo GetLocalUpdateInfo(SeatManage.EnumType.SeatManageSubsystem system)
        //{
        //    string sysDirectory = string.Format(@"{0}{1}\updater.xml", baseDirectory, system.ToString());
        //    if (File.Exists(sysDirectory))
        //    {
        //        FileStream fs = new FileStream(sysDirectory, FileMode.Open);

        //        StreamReader streamReader = new StreamReader(fs);
        //        streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
        //        string arry = "";
        //        string strLine = streamReader.ReadLine();
        //        strLine = streamReader.ReadToEnd();
        //        streamReader.Close();
        //        streamReader.Dispose();
        //        fs.Close();
        //        fs.Dispose();
        //        Console.Write(arry);
        //        Console.ReadLine();
        //        if (strLine != null)
        //        {
        //            FileUpdateInfo updateInfo = FileUpdateInfo.Convert(strLine);
        //            return updateInfo;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public void DownloadUpdateFiles()
        {
            SeatManage.EnumType.SeatManageSubsystem mysystem = SeatManage.EnumType.SeatManageSubsystem.Mediaplayer;

            FileUpdateInfo serviceUpateInfo = SeatManage.Bll.FileTransportBll.GetUpdateInfo(mysystem);
            List<FileSimpleInfo> isUpdateFiles = null;
            if (serviceUpateInfo != null)
            {
                //下载所有Xml中包含的文件，不对比本地文件版本
                isUpdateFiles = serviceUpateInfo.BuildSystemFileSilmpleList();// GetNewVerFiles(serviceUpateInfo, mysystem);//下载播放器
                for (int i = 0; i < isUpdateFiles.Count; i++)
                {
                    //TODO:把文件先下载到临时文件夹中，下载无误，再移动到指定目录中。 （以后优化）
                     
                    string sysDirectory = string.Format(@"{0}{1}\{2}", baseDirectory, mysystem.ToString(), isUpdateFiles[i].Name);
                    if (!downloadFile.FileDownLoad(sysDirectory, isUpdateFiles[i].Name, mysystem))
                    {
                        break;
                    }
                } 
            }
            string StartProgram = "";
            if (serviceUpateInfo != null)
            {
                StartProgram = serviceUpateInfo.StartProgram;
            }
            else
            {
                StartProgram = ConfigurationManager.AppSettings["MediaPlayerProgram"];
            }
            if (string.IsNullOrEmpty(StartProgram))
            {
                HandlerError("播放器启动失败，没有配置媒体播放器启动程序。");
            }
            else
            {
                string mediaplayerStartPath = string.Format(@"{0}{1}\{2}", baseDirectory, mysystem.ToString(), StartProgram);
                if (File.Exists(mediaplayerStartPath))
                {
                    System.Diagnostics.Process.Start(mediaplayerStartPath);
                }
                else
                {
                    if (HandlerError != null)
                    {
                        HandlerError(string.Format("播放器启动失败，文件{0}不存在。",mediaplayerStartPath));
                    }
                }
            }
            StartProgram = "";


            mysystem = SeatManage.EnumType.SeatManageSubsystem.SeatClient;
            serviceUpateInfo = SeatManage.Bll.FileTransportBll.GetUpdateInfo(mysystem);
            if (serviceUpateInfo != null)
            {
                //下载所有Xml中包含的文件，不对比本地文件版本
                isUpdateFiles = serviceUpateInfo.BuildSystemFileSilmpleList();// GetNewVerFiles(serviceUpateInfo, mysystem); //播放器下载完成下载选座终端。
                for (int i = 0; i < isUpdateFiles.Count; i++)
                {
                    string sysDirectory = string.Format(@"{0}{1}\{2}", baseDirectory, mysystem.ToString(), isUpdateFiles[i].Name);
                    if (!downloadFile.FileDownLoad(sysDirectory, isUpdateFiles[i].Name, mysystem))
                    {
                        break;
                    }
                }
                serviceUpateInfo.Save(string.Format(@"{0}{1}\updater.xml", baseDirectory, mysystem.ToString()));//下载完成，保存信息
            }

            if (serviceUpateInfo != null)
            {
                StartProgram = serviceUpateInfo.StartProgram;
            }
            else
            {
                StartProgram = ConfigurationManager.AppSettings["SeatClientProgram"];
            }
            if (string.IsNullOrEmpty(StartProgram))
            { 
                HandlerError("选座终端启动失败，没有配置选座终端启动程序。");
            }
            else
            {
                string seatClient = string.Format(@"{0}{1}\{2}", baseDirectory, mysystem.ToString(), StartProgram);
                if (File.Exists(seatClient))
                {
                    System.Diagnostics.Process.Start(seatClient);
                }
                else
                {
                    HandlerError(string.Format("选座终端启动失败，文件{0}不存在。", seatClient));
                }
            }
            if (Downloaded != null)//通知终端下载已经完成
            {
                Downloaded(this, new EventArgs());
            }
        }
        ///// <summary>
        ///// 返回新版本的文件列表
        ///// </summary>
        ///// <param name="serviceUpdateInfo"></param>
        ///// <param name="system"></param>
        ///// <returns></returns>
        //private List<FileSimpleInfo> GetNewVerFiles(FileUpdateInfo serviceUpdateInfo, SeatManage.EnumType.SeatManageSubsystem system)
        //{
        //    FileUpdateInfo localUpateInfo = GetLocalUpdateInfo(system);
        //    List<FileSimpleInfo> isUpdateFiles = new List<FileSimpleInfo>();

            
        //    if (localUpateInfo == null && serviceUpdateInfo != null)
        //    {
        //        return serviceUpdateInfo.BuildSystemFileSilmpleList() ;
        //    }
        //    else if (serviceUpdateInfo != null && localUpateInfo != null)
        //    {
        //        if (serviceUpdateInfo.Version == localUpateInfo.Version)
        //        {
        //            return isUpdateFiles;
        //        }
        //        List<FileSimpleInfo> newFiles = serviceUpdateInfo.BuildSystemFileSilmpleList();
        //        for (int i = 0; i < newFiles.Count; i++)
        //        {
        //            bool isBreak = false;
        //            bool isExists = false;
        //            List<FileSimpleInfo> oldFiles = localUpateInfo.BuildSystemFileSilmpleList();
        //            for (int j = 0; j < oldFiles.Count; j++)
        //            {
        //                if (newFiles[i].Name == oldFiles[j].Name)
        //                {
        //                    isExists = true;
        //                    if (newFiles[i].Version != oldFiles[j].Version)
        //                    {
        //                        isBreak = true;
        //                        isUpdateFiles.Add(newFiles[i]);
        //                        break;
        //                    }
        //                }
        //            }
        //            if (!isBreak && !isExists)
        //            {
        //                isUpdateFiles.Add(newFiles[i]);

        //            }
        //        }
        //    }
        //    return isUpdateFiles;
        //}
    }
}
