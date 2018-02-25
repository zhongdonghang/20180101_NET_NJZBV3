using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using SeatManage.ClassModel;

namespace AdvertManageTools.Code
{
    public class ProgramUpgradeViewModel : INotifyPropertyChanged
    {
        AdvertManage.Model.Enum.SeatManageSubsystem systemType = AdvertManage.Model.Enum.SeatManageSubsystem.None;
        ObservableCollection<SystemItem> listComboxItems = new ObservableCollection<SystemItem>();
        ObservableCollection<FileNodeItem> _Nodes = new ObservableCollection<FileNodeItem>();
        string version = "";
        string updateLog = "";  
        string _StartProgram = "";
        string filePath = "";
        public string StartProgram
        {
            get { return _StartProgram; }
            set { _StartProgram = value; }
        }
        /// <summary>
        /// 要发布的程序所在文件夹
        /// </summary>
        public string DirPath
        {
            get { return filePath; }
            set
            {
                filePath = value; Changed("DirPath");
            }
        }
        /// <summary>
        /// 要发布的系统类型
        /// </summary>
        public AdvertManage.Model.Enum.SeatManageSubsystem SystemType
        {
            get { return systemType; }
            set { systemType = value; }
        }
        /// <summary>
        /// 系统名称
        /// </summary>
        public ObservableCollection<SystemItem> ListComboxItems
        {
            get { return listComboxItems; }
            set
            {
                listComboxItems = value;
                Changed("ListComboxItems");
            }
        }
        ///<summary>
        /// 系统版本
        /// </summary>
        public string Version
        {
            get { return version; }
            set
            {
                version = value;
                Changed("Version");
            }
        }
        /// <summary>
        /// 节点
        /// </summary>
        public ObservableCollection<FileNodeItem> Nodes
        {
            get { return _Nodes; }
            set
            {
                _Nodes = value;
                Changed("Nodes");
            }
        }
        /// <summary>
        /// 更新日志
        /// </summary>
        public string UpdateLog
        {
            get { return updateLog; }
            set
            {
                updateLog = value;
                Changed("UpdateLog");
            }
        }

        public ProgramUpgradeViewModel()
        {
            listComboxItems.Add(new SystemItem() { Text = "请选择", Value = -1 });
            listComboxItems.Add(new SystemItem() { Text = "选座程序", Value = (int)AdvertManage.Model.Enum.SeatManageSubsystem.SeatClient });
            listComboxItems.Add(new SystemItem() { Text = "播放器", Value = (int)AdvertManage.Model.Enum.SeatManageSubsystem.MediaFiles });
            listComboxItems.Add(new SystemItem() { Text = "监控服务", Value = (int)AdvertManage.Model.Enum.SeatManageSubsystem.SeatService });
            listComboxItems.Add(new SystemItem() { Text = "后台管理", Value = (int)AdvertManage.Model.Enum.SeatManageSubsystem.SeatManageWeb });
        }


        /// <summary>
        /// 获取选中的系统类型
        /// </summary>
        public void FindSystemMessage()
        {
            AdvertManage.Model.ProgramUpgradeModel program = AdvertManage.BLL.ProgramUpgradeBLL.GetProgramInfoByProgramType((AdvertManage.Model.Enum.SeatManageSubsystem)(int)SystemType);
            if (program == null)
            {
                return;
            }
            oldSystem = FileUpdateInfo.Convert(program.AutoUpdaterXml);
            if (oldSystem != null)
            { 
                Version = oldSystem.Version;
            }
            else
            { 
            }
        }
        FileUpdateInfo oldSystem = null;
        FileUpdateInfo system = null;
        public void BuildUpdateConfigFile(string path)
        {
            system = new FileUpdateInfo(path);
            _Nodes.Clear();
            _Nodes.Add(BuildUpdateConfigFile(system.Files));
        }

        private FileNodeItem BuildUpdateConfigFile(MyDirectory dir)
        {
            FileNodeItem item = new FileNodeItem();
            item.ListWritDate = DateTime.Parse(dir.LastUpdate);
            item.Name = dir.Name;
            item.Version = dir.Version;
            item.IsCanSelected = false;
            item.ToolTip = string.Format("文件夹  日期：{0}   版本：{1}", dir.ToString(), dir.Version);
            foreach (FileSimpleInfo file in dir.Files)
            {
                FileNodeItem child = new FileNodeItem();
                child.IsCanSelected = true;
                child.ListWritDate = file.ModifyDateTime;
                child.Name = file.Name;
                child.Version = file.Version;
                child.ToolTip = string.Format("文件  日期：{0}  版本：{1}", file.ModifyDateTime, file.Version);
                item.Nodes.Add(child);
            }
            foreach (MyDirectory directory in dir.Directories)
            {
                FileNodeItem child = new FileNodeItem();
                item.Nodes.Add(BuildUpdateConfigFile(directory));
            }
            return item;
        }
        /// <summary>
        /// 系统Xml信息上传
        /// </summary>
        public string SystemUpdate()
        {
            try
            {
                system.ReleaseDate = DateTime.Now;
                system.StartProgram = StartProgram;
                system.SubsystemType = (SeatManage.EnumType.SeatManageSubsystem)(int)SystemType;
                system.UpdateLog = string.Format("{0}{1}\r{2}\r\r", oldSystem == null ? "" : oldSystem.UpdateLog, DateTime.Now.ToShortDateString(), UpdateLog);
                system.Version = Version;

                AdvertManage.Model.ProgramUpgradeModel model = new AdvertManage.Model.ProgramUpgradeModel();
                model.Application = SystemType;
                model.AutoUpdaterXml = system.ToString();
                model.ReleaseDate = DateTime.Now;
                model.UpdateLog += string.Format("{0}{1}\r{2}\r\r", oldSystem == null ? "" : oldSystem.UpdateLog, DateTime.Now.ToShortDateString(), UpdateLog);
                model.Version = Version;
                if (AdvertManage.BLL.ProgramUpgradeBLL.ReleaseProgram(model) == AdvertManage.Model.Enum.HandleResult.Successed)
                {
                    return "更新成功";
                }
                else
                {
                    return "更新失败";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 构建文件路径，上传。
        /// </summary>
        public string BuildUpdateFile()
        {
            AdvertManage.Model.ProgramUpgradeModel program = AdvertManage.BLL.ProgramUpgradeBLL.GetProgramInfoByProgramType((AdvertManage.Model.Enum.SeatManageSubsystem)(int)SystemType);
            if (program != null)
            {
                oldSystem = FileUpdateInfo.Convert(program.AutoUpdaterXml);
                if (oldSystem != null && Version == oldSystem.Version)
                {
                    return "版本号重复";
                }
                filePaths = system.BuildUpdateFilePaths() ;
                return "";
            }
            else
            {
                return "";
            }

        }

        List<string> filePaths = new List<string>();
        /// <summary>
        /// 要上传的文件路径
        /// </summary>
        public List<string> FilePaths
        {
            get { return filePaths; }
            set { filePaths = value; }
        } 
        /// <summary>
        /// 检查是否选中了启动文件
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public string CheckIsCheckFile(ObservableCollection<FileNodeItem> files)
        {
            string checkFile = "";
            foreach (FileNodeItem item in files)
            {
                if (item.IsCanSelected && item.IsChecked)
                {
                    //checkFile = item.Name;
                    //break;
                    return item.Name;
                }
                checkFile = CheckIsCheckFile(item.Nodes);
                if (!string.IsNullOrEmpty(checkFile))
                {
                    return checkFile;
                }
            }
            return checkFile;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class SystemItem
    {
        private int _Value;

        public int Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private string _Text;

        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }
    }

    /// <summary>
    /// 节点
    /// </summary>
    public class FileNodeItem : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private bool _IsChecked = false;
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                _IsChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
        private string name = "";
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        string version = "";
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version
        {
            get { return version; }
            set
            {
                version = value;
                OnPropertyChanged("Version");
            }
        }

        private DateTime _ListWritDate;
        /// <summary>
        /// 最后写入日期
        /// </summary>
        public DateTime ListWritDate
        {
            get { return _ListWritDate; }
            set
            {
                _ListWritDate = value;
                OnPropertyChanged("ListWritDate");
            }
        }

        private string toolTip = "";

        public string ToolTip
        {
            get { return toolTip; }
            set
            {
                toolTip = value;
                OnPropertyChanged("ToolTip");
            }
        }

        private bool _IsCanSelected = false;
        /// <summary>
        /// 是否可选
        /// </summary>
        public bool IsCanSelected
        {
            get { return _IsCanSelected; }
            set { _IsCanSelected = value; OnPropertyChanged("IsCanSelected"); }
        }

        private ObservableCollection<FileNodeItem> _Nodes = new ObservableCollection<FileNodeItem>();
        /// <summary>
        /// 子节点
        /// </summary>
        public ObservableCollection<FileNodeItem> Nodes
        {
            get { return _Nodes; }
            set
            {
                _Nodes = value;
                OnPropertyChanged("Nodes");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
