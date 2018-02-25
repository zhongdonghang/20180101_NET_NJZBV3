using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.IO;

namespace SeatManage.SeatClient.Config.Code
{
    public delegate void EventHanleBackup(BackupProgressInfo arge);
    /// <summary>
    /// 终端程序备份到服务器（选座程序，媒体播放程序；忽略Log，Video和SlipImage文件夹中的内容以及config文件）。
    /// </summary>
    public class ProgramBackup
    {
        /*
         *包含两个事件： 上传进度和完成事件。
         *
        */
        /// <summary>
        /// 上传进度
        /// </summary>
        public event EventHanleBackup Progress;
        /// <summary>
        /// 文件备份失败
        /// </summary>
        public event EventHanleBackup BackupFiled;
        /// <summary>
        /// 备份完成
        /// </summary>
        public event EventHandler BackupOver;

        public string SeatFileNoRead()
        {
            //根据类型设置要上传的程序路径
            string programDirMediaPlayer = string.Format("{0}MediaPlayer", AppDomain.CurrentDomain.BaseDirectory);
            string programDirSeatClient = string.Format("{0}SeatClient", AppDomain.CurrentDomain.BaseDirectory);
            //判断路径是否存在
            if (!Directory.Exists(programDirMediaPlayer) || !Directory.Exists(programDirSeatClient))
            {
                return "路径不存在，请检查终端配置是否有误";
            }
            //去除文件只读属性
            SetReadOnly(programDirMediaPlayer);
            SetReadOnly(programDirSeatClient);
            return "设置成功！";
        }
        /// <summary>
        /// 去除只读
        /// </summary>
        /// <param name="dirPath"></param>
        private void SetReadOnly(string dirPath)
        {
            string[] dirPathes = Directory.GetDirectories(dirPath, "*.*", SearchOption.AllDirectories);
            string[] filePathes = Directory.GetFiles(dirPath, "*.*", SearchOption.AllDirectories);
            foreach (var dp in dirPathes)
            {
                DirectoryInfo dir = new DirectoryInfo(dirPath);
                dir.Attributes = FileAttributes.Normal & FileAttributes.Directory;
            }
            foreach (var fp in filePathes)
            {
                File.SetAttributes(fp, System.IO.FileAttributes.Normal);
            }
        }
        public void Backup()
        {

            BackupProgram(SeatManage.EnumType.SeatManageSubsystem.Mediaplayer);
            BackupProgram(SeatManage.EnumType.SeatManageSubsystem.SeatClient);
            if (BackupOver != null)
            {
                BackupOver(this, new EventArgs());
            }
        }
        private void BackupProgram(SeatManage.EnumType.SeatManageSubsystem systemType)
        {
            try
            {
                string programDir = "";
                //根据类型设置要上传的程序路径
                if (systemType == EnumType.SeatManageSubsystem.Mediaplayer)
                {
                    programDir = string.Format("{0}MediaPlayer", AppDomain.CurrentDomain.BaseDirectory);
                }
                else if (systemType == EnumType.SeatManageSubsystem.SeatClient)
                {
                    programDir = string.Format("{0}SeatClient", AppDomain.CurrentDomain.BaseDirectory);
                }
                //判断路径是否存在
                if (!Directory.Exists(programDir))
                {
                    if (BackupFiled != null)
                    {
                        string errorMessage = string.Format("路径{0}不存在，请检查终端配置是否有误", programDir);
                        BackupFiled(new BackupProgressInfo() { Message = errorMessage });
                    }
                    return;
                }
                //去除文件只读属性
                SetReadOnly(programDir);
                //构造系统文件结构
                FileUpdateInfo files = new FileUpdateInfo(programDir);
                files.SubsystemType = systemType;
                files.UpdateLog = "初始版本";
                files.ReleaseDate = DateTime.Now;
                //设置文件启动程序
                if (systemType == EnumType.SeatManageSubsystem.Mediaplayer)
                {
                    files.StartProgram = "MediaPlayerClient.exe";
                }
                else if (systemType == EnumType.SeatManageSubsystem.SeatClient)
                {
                    files.StartProgram = "SeatClient.exe";
                }
                //删除Log，Video和SlipImage
                for (int i = 0; i < files.Files.Directories.Count; i++)
                {
                    if (files.Files.Directories[i].Name == "Caputre" || files.Files.Directories[i].Name == "Log" || files.Files.Directories[i].Name == "SlipImage")
                    {
                        files.Files.Directories.Remove(files.Files.Directories[i]);
                    }
                }
                //删除配置文件
                for (int i = 0; i < files.Files.Files.Count; i++)
                {
                    string exName = files.Files.Files[i].Name.Substring(files.Files.Files[i].Name.LastIndexOf(".") + 1);
                    if (exName == "config")
                    {
                        files.Files.Files.Remove(files.Files.Files[i]);
                    }
                }
                SeatManage.Bll.FileOperate fileUpload = new Bll.FileOperate();
                List<string> filesPathList = files.BuildUpdateFilePaths();
                for (int i = 0; i < filesPathList.Count; i++)
                {

                    string fileFullName = string.Format("{0}{1}", programDir, filesPathList[i]);
                    if (Progress != null)
                    {//注册上传的消息
                        BackupProgressInfo arge = new BackupProgressInfo();
                        arge.ProgramName = systemType.ToString();
                        arge.UpdateFileName = filesPathList[i];
                        arge.Progress = (int)(((double)i / (filesPathList.Count - 1)) * 100);
                        Progress(arge);
                    }
                    if (!fileUpload.UpdateFile(fileFullName, filesPathList[i], systemType))
                    {
                        if (BackupFiled != null)
                        {
                            BackupProgressInfo arge = new BackupProgressInfo();
                            arge.Message = string.Format("文件{0}上传失败", filesPathList[i]);
                            BackupFiled(arge);
                        }
                    }
                }
                FileUpdateInfo oldFile = SeatManage.Bll.FileTransportBll.GetUpdateInfo(systemType);
                if (oldFile == null)
                {
                    SeatManage.Bll.FileTransportBll.Add(files);
                }
                else
                {
                    SeatManage.Bll.FileTransportBll.Update(files);
                }
            }
            catch (Exception ex)
            {
                if (BackupFiled != null)
                {
                    BackupProgressInfo arge = new BackupProgressInfo();
                    arge.Message = string.Format("备份失败，请检查是否缺失了“座位管理系统终端设置程序.exe.config”文件");
                    BackupFiled(arge);
                }
            }
        }
    }

    /// <summary>
    /// 进度信息
    /// </summary>
    public class BackupProgressInfo
    {
        /// <summary>
        /// 正在上传的文件名称
        /// </summary>
        string _UpdateFileName;
        /// <summary>
        /// 正在上传的文件名称
        /// </summary>
        public string UpdateFileName
        {
            get { return _UpdateFileName; }
            set { _UpdateFileName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        string _ProgramName;
        /// <summary>
        /// 程序名称
        /// </summary>
        public string ProgramName
        {
            get { return _ProgramName; }
            set { _ProgramName = value; }
        }
        string _Message;
        /// <summary>
        /// 消息
        /// </summary>
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        int _Progress;
        /// <summary>
        /// 进度
        /// </summary>
        public int Progress
        {
            get { return _Progress; }
            set { _Progress = value; }
        }
    }
}
