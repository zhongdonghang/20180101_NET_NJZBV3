using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using SeatManage.ClassModel;
using System.ComponentModel;

namespace AMS.ViewModel
{
    public class ViewModelProjectVersionEditWindow : ViewModelObject
    {
        #region 构造函数
        public ViewModelProjectVersionEditWindow()
        {
            _ProgramUpgrade = new ObservableCollection<Model.ProgramUpgrade>();
        }
        #endregion

        private static readonly string CLASSNAME = "ViewModelProjectVersionEditWindow";

        #region 成员
        private ObservableCollection<AMS.Model.ProgramUpgrade> _ProgramUpgrade;

        public ObservableCollection<AMS.Model.ProgramUpgrade> ProgramUpgrade
        {
            get { return _ProgramUpgrade; }
            set { _ProgramUpgrade = value; OnPropertyChanged("ProgramUpgrade"); }
        }
        ObservableCollection<ProgramUpgradeItem> _ProgramUpgradeItems = new ObservableCollection<ProgramUpgradeItem>();
        /// <summary>
        /// 项目名称
        /// </summary>
        public ObservableCollection<ProgramUpgradeItem> ProgramUpgradeItems
        {
            get { return _ProgramUpgradeItems; }
            set { _ProgramUpgradeItems = value; OnPropertyChanged("ProgramUpgradeItems"); }
        }

        private string _FilePath;
        /// <summary>
        /// 文件夹位置
        /// </summary>
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; OnPropertyChanged("FilePath"); }
        }

        ObservableCollection<FileNodeItem> _FileNodeItems = new ObservableCollection<FileNodeItem>();

        public ObservableCollection<FileNodeItem> FileNodeItems
        {
            get { return _FileNodeItems; }
            set { _FileNodeItems = value; OnPropertyChanged("FileNodeItems"); }
        }

        private string _ProgramUpgradeVersion;
        /// <summary>
        /// 项目版本号
        /// </summary>
        public string ProgramUpgradeVersion
        {
            get { return _ProgramUpgradeVersion; }
            set { _ProgramUpgradeVersion = value; OnPropertyChanged("ProgramUpgradeVersion"); }
        }

        private AMS.Model.Enum.SeatManageSubsystem _ProjectType;
        /// <summary>
        /// 要发布的系统类型
        /// </summary>
        public AMS.Model.Enum.SeatManageSubsystem ProjectType
        {
            get { return _ProjectType; }
            set { _ProjectType = value; OnPropertyChanged("ProjectType"); }
        }

        private string _UpdateLog;
        /// <summary>
        /// 更新日志
        /// </summary>
        public string UpdateLog
        {
            get { return _UpdateLog; }
            set
            {
                _UpdateLog = value;
                OnPropertyChanged("UpdateLog");
            }
        }

        private string _StartProgram = "";
        public string StartProgram
        {
            get { return _StartProgram; }
            set { _StartProgram = value; }
        }

        private string _ErrorMessage = "";

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 绑定应用程序类型下拉框项
        /// </summary>
        public void ProgramUpgradeViewModel()
        {
            ProgramUpgradeItems.Add(new ProgramUpgradeItem() { Text = "请选择", Value = -1 });
            ProgramUpgradeItems.Add(new ProgramUpgradeItem() { Text = "选座程序", Value = (int)AMS.Model.Enum.SeatManageSubsystem.SeatClient });
            ProgramUpgradeItems.Add(new ProgramUpgradeItem() { Text = "播放器", Value = (int)AMS.Model.Enum.SeatManageSubsystem.MediaFiles });
            ProgramUpgradeItems.Add(new ProgramUpgradeItem() { Text = "监控服务", Value = (int)AMS.Model.Enum.SeatManageSubsystem.SeatService });
            ProgramUpgradeItems.Add(new ProgramUpgradeItem() { Text = "后台管理", Value = (int)AMS.Model.Enum.SeatManageSubsystem.SeatManageWeb });
        }
        FileUpdateInfo system = null;

        public void BuildUpdateConfigFile(string path)
        {
            system = new FileUpdateInfo(path);
            FileNodeItems.Clear();
            FileNodeItem fnItem = BuildUpdateConfigFile(system.Files);
            if (fnItem != null)
            {
                FileNodeItems.Add(fnItem);
            }
        }

        private FileNodeItem BuildUpdateConfigFile(MyDirectory dir)
        {
            string functionName = "BuildUpdateConfigFile";
            try
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
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
                return null;
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
                return null;
            }
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
        /// <summary>
        /// 项目类型下拉框选择改变事件
        /// </summary>
        /// <param name="selectedNode"></param>
        public void ComboxSelectedItemHandle(ProgramUpgradeItem selectedNode)
        {
            if (selectedNode == null)
            {
                return;
            }
            List<AMS.Model.ProgramUpgrade> modelList = new List<Model.ProgramUpgrade>();
            modelList = AMS.ServiceProxy.ProjecVersionWindow.GetProjectList();
            ProjectType = (AMS.Model.Enum.SeatManageSubsystem)selectedNode.Value;
            foreach (AMS.Model.ProgramUpgrade model in modelList)
            {
                if (selectedNode.Value == model.Application)
                {
                    ProgramUpgradeVersion = model.Version;
                    //ProjectType = (AMS.Model.Enum.SeatManageSubsystem)model.Application;
                }
            }
        }
        /// <summary>
        /// 验证下发文件
        /// </summary>
        /// <returns></returns>
        public bool ProjectFileRelease()
        {
            string functionName = "ProjectFileRelease";
            try
            {
                if (ProjectType == AMS.Model.Enum.SeatManageSubsystem.None)
                {
                    ErrorMessage = "请选择要发布的系统类型！";
                    return false;
                }
                if (string.IsNullOrEmpty(ProgramUpgradeVersion))
                {
                    ErrorMessage = "请填写版本号！";
                    return false;
                }
                if (string.IsNullOrEmpty(FilePath))
                {
                    ErrorMessage = "请选择要发布的程序路径！";
                    return false;
                }

                string startProgram = CheckIsCheckFile(FileNodeItems);
                if (string.IsNullOrEmpty(startProgram))
                {
                    ErrorMessage = "请展开树状菜单，选择程序的启动文件！";
                    return false;
                }
                if (startProgram.Substring(startProgram.LastIndexOf('.') + 1) != "exe")
                {
                    ErrorMessage = "启动程序必须是以.exe为后缀的可执行文件！";
                    return false;
                }
                if (string.IsNullOrEmpty(UpdateLog))
                {
                    ErrorMessage = "请填写更新说明！";
                    return false;
                }
                StartProgram = startProgram;
                return true;
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
                return false;
            }
        }

        public bool FileUpload()
        {

            AMS.ServiceProxy.FileOperate fileUpload = new ServiceProxy.FileOperate();
            //string result = fileUpload.UpdateFile(FilePath, Name, ProjectType);
            //if (!string.IsNullOrEmpty(result))
            //{
            //    ErrorMessage = string.Format("上传文件失败！{0}", result);
            //    return false;
            //}
            //AMS.Model.FileSharingInfo model = new Model.FileSharingInfo();
            //result = AMS.ServiceProxy.FileSharingWindow.AddNewFile(_FileModel);
            //if (!string.IsNullOrEmpty(result))
            //{
            //    ErrorMessage = string.Format("保存失败！{0}", result);
            //    return false;
            //}
            return true;
        }
        #endregion
    }
    /// <summary>
    /// 座位管理系统子系统类型
    /// </summary>
    public class ProgramUpgradeItem : ViewModelObject
    {
        private int _Value;

        public int Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                OnPropertyChanged("ProgramUpgradeItem");
            }
        }

        private string _Text;

        public string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
                OnPropertyChanged("ProgramUpgradeItem");
            }
        }
    }

    /// <summary>
    /// 节点
    /// </summary>
    public class FileNodeItem : ViewModelObject
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
    }
}
