using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AMS.ViewModel
{
    public class ViewModelFileSharingWindow : ViewModelObject
    {
        #region 构造函数
        public ViewModelFileSharingWindow()
        {

        }
        #endregion
        private ViewModelProgressBar _vm_ProgressBar = new ViewModelProgressBar();
        /// <summary>
        /// 进度条
        /// </summary>
        public ViewModelProgressBar Vm_ProgressBar
        {
            get { return _vm_ProgressBar; }
            set { _vm_ProgressBar = value; OnPropertyChanged("Vm_ProgressBar"); }
        }
        private static readonly string CLASSNAME = "ViewModelFileSharingWindow";
        #region 成员
        private ObservableCollection<ViewModelFileSharingShow> _FileSharingList = new ObservableCollection<ViewModelFileSharingShow>();
        /// <summary>
        /// 共享文件列表
        /// </summary>
        public ObservableCollection<ViewModelFileSharingShow> FileSharingList
        {
            get { return _FileSharingList; }
            set { _FileSharingList = value; OnPropertyChanged("FileSharingList"); }
        }
        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取共享文件列表
        /// </summary>
        public void GetFileSharingList()
        {
            string functionName = "GetFileSharingList";
            try
            {
                List<AMS.Model.FileSharingInfo> fileSharing = AMS.ServiceProxy.FileSharingWindow.GetFileSharingList();
                FileSharingList.Clear();
                foreach (AMS.Model.FileSharingInfo model in fileSharing)
                {
                    FileSharingList.Add(new ViewModelFileSharingShow() { FileModel = model });
                }
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
        /// <summary>
        /// 下载文件
        /// </summary>
        public bool DownLoadFile(string fileLocalPath, int index)
        {

            string functionName = "DownLoadFile";
            try
            {
                AMS.ServiceProxy.FileOperate fileDownLoad = new ServiceProxy.FileOperate();
                fileDownLoad.HandleProgress += new ServiceProxy.EventHandleFileTransport(fileDownLoad_HandleProgress);
                Vm_ProgressBar.ProgressType = "文件下载进度";
                Vm_ProgressBar.ProgressName = "正在下载\"" + _FileSharingList[index].FileModel.Name + "\"……";
                string result = fileDownLoad.FileDownLoad(fileLocalPath + "\\" + _FileSharingList[index].FileModel.Name, _FileSharingList[index].FileModel.FilePath, SeatManage.EnumType.SeatManageSubsystem.SharingFile);
                if (!string.IsNullOrEmpty(result))
                {
                    ErrorMessage = string.Format("下载失败！{0}", result);
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

        void fileDownLoad_HandleProgress(int message)
        {
            Vm_ProgressBar.NowProgress = message.ToString();
        }
        #endregion
    }
    public class ViewModelFileSharingShow : ViewModelObject
    {
        private AMS.Model.FileSharingInfo _FileModel = new Model.FileSharingInfo();
        /// <summary>
        /// 文件model
        /// </summary>
        public AMS.Model.FileSharingInfo FileModel
        {
            get { return _FileModel; }
            set { _FileModel = value; OnPropertyChanged("FileModel"); OnPropertyChanged("FileType"); }
        }
        public string FileType
        {
            get
            {
                switch ((AMS.Model.Enum.FileSharingType)_FileModel.FileType)
                {
                    case Model.Enum.FileSharingType.CardReaderInterface: return "读卡器接口";
                    case Model.Enum.FileSharingType.DataBase: return "数据库文件";
                    case Model.Enum.FileSharingType.Documentation: return "文档文件";
                    case Model.Enum.FileSharingType.Other: return "其他文件";
                    case Model.Enum.FileSharingType.ReaderSyncInterface: return "读者同步接口";
                    case Model.Enum.FileSharingType.SchoolInforation: return "学校资料";
                    case Model.Enum.FileSharingType.SeatManageSystem: return "座位管理系统";
                    default: return "未知类型";
                }
            }
        }

    }
}
