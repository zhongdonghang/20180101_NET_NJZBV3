using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.Collections.ObjectModel;
using System.IO;

namespace AMS.ViewModel
{
    public class ViewModelFileSharingUploadWindow : ViewModelObject
    {
        #region 成员
        private static readonly string CLASSNAME = "ViewModelFileSharingUploadWindow";
        //public delegate void ProgressBarStatus(string progress);
        ///// <summary>
        ///// 完成事件
        ///// </summary>
        //public event ProgressBarStatus ProgressStatus;
        private ViewModelProgressBar _vm_ProgressBar = new ViewModelProgressBar();
        /// <summary>
        /// 进度条
        /// </summary>
        public ViewModelProgressBar Vm_ProgressBar
        {
            get { return _vm_ProgressBar; }
            set { _vm_ProgressBar = value; OnPropertyChanged("Vm_ProgressBar"); }
        }
        private AMS.Model.FileSharingInfo _FileModel = new Model.FileSharingInfo();
        /// <summary>
        /// 文件model
        /// </summary>
        public AMS.Model.FileSharingInfo FileModel
        {
            get { return _FileModel; }
            set { _FileModel = value; OnPropertyChanged("FileModel"); }
        }
        private string _FilePath;
        /// <summary>
        /// 文件夹位置
        /// </summary>
        public string FilePath
        {
            get { return "文件名称：  " + _FileModel.FilePath.Substring(_FileModel.FilePath.LastIndexOf("\\") + 1); }
        }

        private string _Name;

        public string Name
        {
            get { return _FileModel.Name; }
            set { _FileModel.Name = value; OnPropertyChanged("Name"); }
        }
        private string _Remark;

        public string Remark
        {
            get { return _FileModel.Remark; }
            set { _FileModel.Remark = value; OnPropertyChanged("Remark"); }
        }

        private string _Size;

        public string Size
        {
            get
            {
                double filesize = double.Parse(_FileModel.Size) / 1024;
                if (filesize > 1024)
                {
                    return "文件大小：  " + (filesize / 1024).ToString("0.00") + "mb";
                }
                else
                {
                    return "文件大小：  " + filesize.ToString("0.00") + "kb";
                }
            }
        }

        private AMS.Model.Enum.FileSharingType _FileType;

        public int FileType
        {
            get { return _FileModel.FileType.Value; }
            set { _FileModel.FileType = value; OnPropertyChanged("FileType"); }
        }

        private ObservableCollection<FileTypeItem> _FilesTypeItems = new ObservableCollection<FileTypeItem>();

        public ObservableCollection<FileTypeItem> FilesTypeItems
        {
            get { return _FilesTypeItems; }
            set { _FilesTypeItems = value; OnPropertyChanged("FilesTypeItems"); }
        }

        private string _ErrorMessage = "";

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }

        #endregion

        #region 方法
        public void AddFile(string filepath)
        {
            string functionName = "AddFile";
            try
            {
                FileInfo fi = new FileInfo(filepath);
                _FileModel.Size = fi.Length.ToString();
                _FileModel.FilePath = filepath;
                _FileModel.Name = fi.Name;
                OnPropertyChanged("FilePath");
                OnPropertyChanged("Size");
                OnPropertyChanged("Name");
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        public bool FileUpLoad()
        {
            string functionName = "FileUpLoad";
            try
            {
                if (FileType == (int)Model.Enum.FileSharingType.None)
                {
                    ErrorMessage = "请选择文件类型！";
                    return false;
                }
                if (string.IsNullOrEmpty(Name))
                {
                    ErrorMessage = "文件名不能为空！";
                    return false;
                }
                Vm_ProgressBar.ProgressType = "文件上传进度";
                Vm_ProgressBar.FullProgress = 1;
                Vm_ProgressBar.ProgressName = "正在上传\"" + _FileModel.Name + "\"……";
                AMS.ServiceProxy.FileOperate fileUpload = new ServiceProxy.FileOperate();
                fileUpload.HandleProgress += new ServiceProxy.EventHandleFileTransport(fileUpload_HandleProgress);
                string result = fileUpload.UpdateFile(_FileModel.FilePath, ((AMS.Model.Enum.FileSharingType)_FileModel.FileType).ToString() + "\\" + _FileModel.Name, SeatManage.EnumType.SeatManageSubsystem.SharingFile);
                if (!string.IsNullOrEmpty(result))
                {
                    ErrorMessage = string.Format("上传文件失败！{0}", result);
                    Vm_ProgressBar.IsFinish();
                    return false;
                }
                //Vm_ProgressBar.IsFinish();
                _FileModel.FilePath = ((AMS.Model.Enum.FileSharingType)_FileModel.FileType).ToString() + "\\" + _FileModel.Name;
                _FileModel.UpManID = User.ID;
                result = AMS.ServiceProxy.FileSharingWindow.AddNewFile(_FileModel);
                if (!string.IsNullOrEmpty(result))
                {
                    ErrorMessage = string.Format("保存失败！{0}", result);
                    return false;
                }
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

        void fileUpload_HandleProgress(int message)
        {
            //ProgressStatus(message.ToString());
            Vm_ProgressBar.NowProgress = message.ToString();
        }
        public bool FileDownLoad(string filedownloadpath)
        {
            string functionName = "FileDownLoad";
            try
            {
                AMS.ServiceProxy.FileOperate filedownload = new ServiceProxy.FileOperate();
                string result = filedownload.FileDownLoad(filedownloadpath + "\\" + _FileModel.Name, _FileModel.FilePath, SeatManage.EnumType.SeatManageSubsystem.SharingFile);
                if (!string.IsNullOrEmpty(result))
                {
                    ErrorMessage = string.Format("下载文件失败！{0}", result);
                    return false;
                }
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
        public bool DeleteFile()
        {
            string functionName = "DeleteFile";
            try
            {
                string result = AMS.ServiceProxy.FileSharingWindow.DeleteFile(_FileModel);
                if (!string.IsNullOrEmpty(result))
                {
                    ErrorMessage = string.Format("删除文件失败！{0}", result);
                    return false;
                }
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
        /// <summary>
        /// 绑定应用程序类型下拉框项
        /// </summary>
        public void GetFileType()
        {
            string functionName = "GetFileType";
            try
            {
                FilesTypeItems.Add(new FileTypeItem() { Text = "请选择", Value = -1 });
                FilesTypeItems.Add(new FileTypeItem() { Text = "读卡器接口", Value = (int)AMS.Model.Enum.FileSharingType.CardReaderInterface });
                FilesTypeItems.Add(new FileTypeItem() { Text = "读者同步接口", Value = (int)AMS.Model.Enum.FileSharingType.ReaderSyncInterface });
                FilesTypeItems.Add(new FileTypeItem() { Text = "座位管理程序", Value = (int)AMS.Model.Enum.FileSharingType.SeatManageSystem });
                FilesTypeItems.Add(new FileTypeItem() { Text = "学校资料", Value = (int)AMS.Model.Enum.FileSharingType.SchoolInforation });
                FilesTypeItems.Add(new FileTypeItem() { Text = "文档文件", Value = (int)AMS.Model.Enum.FileSharingType.Documentation });
                FilesTypeItems.Add(new FileTypeItem() { Text = "数据库备份", Value = (int)AMS.Model.Enum.FileSharingType.DataBase });
                FilesTypeItems.Add(new FileTypeItem() { Text = "其他", Value = (int)AMS.Model.Enum.FileSharingType.Other });
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        #endregion
    }

    /// <summary>
    /// 座位管理系统子系统类型
    /// </summary>
    public class FileTypeItem : ViewModelObject
    {
        private int _Value;

        public int Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                OnPropertyChanged("FileTypeItem");
            }
        }

        private string _Text;

        public string Text
        {
            get { return _Text; }
            set
            {

                _Text = value;
                OnPropertyChanged("FileTypeItem");
            }
        }
    }
}
