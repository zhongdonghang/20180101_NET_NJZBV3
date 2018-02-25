using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.Model;
using System.Collections.ObjectModel;

namespace AMS.ViewModel
{
    public class ViewModelCallBackErrorInfoListWindow : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModelCallBackErrorInfoListWindow";
        #region 私有字段
        private string _ErrorMessage = "";
        private ObservableCollection<ViewModelCallBackErrorInfoShow> _CallBackErrorInfoList = new ObservableCollection<ViewModelCallBackErrorInfoShow>();
        #endregion

        #region 构造函数
        public ViewModelCallBackErrorInfoListWindow()
        {
            _CallBackErrorInfoList = new ObservableCollection<ViewModelCallBackErrorInfoShow>();
        }
        #endregion

        #region 属性
        ///<summary>
        /// 反馈列表
        ///<summary>
        public ObservableCollection<ViewModelCallBackErrorInfoShow> CallBackErrorInfoList
        {
            get { return _CallBackErrorInfoList; }
            set { _CallBackErrorInfoList = value; OnPropertyChanged("CallBackErrorInfoList"); }
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }


        #endregion

        #region 方法
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public bool GetDataList()
        {
            string functionName = "GetDataList";
            try
            {
                List<AMS.Model.AMS_CallBackErrorInfo> modellist = new List<Model.AMS_CallBackErrorInfo>();
                modellist = AMS.ServiceProxy.ICallBackInfoService.GetCallBackInfoList();
                CallBackErrorInfoList.Clear();
                foreach (AMS.Model.AMS_CallBackErrorInfo model in modellist)
                {
                    ViewModelCallBackErrorInfoShow vm = new ViewModelCallBackErrorInfoShow();
                    vm.CallBackErrorModel = model;
                    switch ((AMS.Model.Enum.CallBackResult)vm.CallBackErrorModel.Solvestatic)
                    {
                        case Model.Enum.CallBackResult.Walting:
                            vm.WatchMenuVisibility = "Visible";
                            if (vm.CallBackErrorModel.MarkManID == User.ID)
                            {
                                vm.DeleteMenuVisibility = "Visible";
                            }
                            vm.SolveMenuVisibility = "Visible";
                            break;
                        case Model.Enum.CallBackResult.Solving:
                            vm.WatchMenuVisibility = "Visible";
                            if (vm.CallBackErrorModel.SolveManID == User.ID)
                            {
                                vm.FinishMenuVisibility = "Visible";
                            }
                            break;
                        case Model.Enum.CallBackResult.Finished:
                            vm.WatchMenuVisibility = "Visible";
                            break;
                    }
                    CallBackErrorInfoList.Add(vm);

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
        #endregion
    }
    public class ViewModelCallBackErrorInfoShow : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModelCallBackErrorInfoShow";
        private AMS_CallBackErrorInfo _CallBackErrorModel = new AMS_CallBackErrorInfo();
        /// <summary>
        /// Model
        /// </summary>
        public AMS_CallBackErrorInfo CallBackErrorModel
        {
            get { return _CallBackErrorModel; }
            set { _CallBackErrorModel = value; OnPropertyChanged("CallBackErrorModel"); }
        }
        /// <summary>
        /// 反馈人
        /// </summary>
        public string Markman
        {
            get
            {
                if (string.IsNullOrEmpty(_CallBackErrorModel.Markman))
                {
                    return User.UserName;
                }
                else
                {
                    return _CallBackErrorModel.Markman;
                }
            }
        }
        /// <summary>
        /// 反馈时间
        /// </summary>
        public string FbTime
        {
            get { return _CallBackErrorModel.FbTime.Value.ToLongDateString(); }
        }
        public string FbDescribe
        {
            get { return _CallBackErrorModel.FbDescribe; }
            set { _CallBackErrorModel.FbDescribe = value; OnPropertyChanged("FbDescribe"); }
        }
        /// <summary>
        /// 解决时间
        /// </summary>
        public string SolveTime
        {
            get
            {
                if (!_CallBackErrorModel.SolveTime.HasValue || string.IsNullOrEmpty(_CallBackErrorModel.SolveTime.Value.ToLongDateString()))
                {
                    return "无";
                }
                else
                {
                    return _CallBackErrorModel.SolveTime.Value.ToLongDateString();
                };
            }
        }
        /// <summary>
        /// 解决状态
        /// </summary>
        public string SolveStatic
        {
            get
            {
                switch ((AMS.Model.Enum.CallBackResult)_CallBackErrorModel.Solvestatic)
                {
                    case Model.Enum.CallBackResult.Finished: return "处理完成";
                    case Model.Enum.CallBackResult.Solving: return "解决中";
                    case Model.Enum.CallBackResult.Walting: return "等待处理";
                    default: return "未知状态";
                }
            }
        }
        /// <summary>
        /// 解决人
        /// </summary>
        public string SolveMan
        {
            get
            {
                if (string.IsNullOrEmpty(_CallBackErrorModel.Solveman))
                {
                    return "暂无人";
                }
                else
                {
                    return _CallBackErrorModel.Solveman;
                }
            }
        }
        /// <summary>
        /// 问题类型
        /// </summary>
        public string ProblemType
        {
            get
            {
                switch ((AMS.Model.Enum.CallBackType)_CallBackErrorModel.ProblemType)
                {
                    case Model.Enum.CallBackType.Advertisement: return "广告问题";
                    case Model.Enum.CallBackType.Hardware: return "硬件问题";
                    case Model.Enum.CallBackType.Software: return "软件问题";
                    case Model.Enum.CallBackType.Unknown: return "未知问题";
                    default: return "获取错误";
                }
            }
        }
        /// <summary>
        /// 解决方法
        /// </summary>
        public string SolveWay
        {
            get
            {
                if (string.IsNullOrEmpty(_CallBackErrorModel.SolveWay))
                {
                    return "暂无方案";
                }
                else
                {
                    return _CallBackErrorModel.SolveWay;
                }
            }
        }
        /// <summary>
        /// 学校
        /// </summary>
        public string SchoolName
        {
            get { return _CallBackErrorModel.Schoolname; }
            set { _CallBackErrorModel.Schoolname = value; OnPropertyChanged("SchoolName"); }

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

        private string _WatchMenuVisibility = "Collapsed";
        /// <summary>
        /// 添加菜单显示
        /// </summary>
        public string WatchMenuVisibility
        {
            get { return _WatchMenuVisibility; }
            set { _WatchMenuVisibility = value; OnPropertyChanged("WatchMenuVisibility"); }
        }
        private string _DeleteMenuVisibility = "Collapsed";
        /// <summary>
        /// 删除菜单
        /// </summary>
        public string DeleteMenuVisibility
        {
            get { return _DeleteMenuVisibility; }
            set { _DeleteMenuVisibility = value; OnPropertyChanged("DeleteMenuVisibility"); }
        }
        private string _SolveMenuVisibility = "Collapsed";
        /// <summary>
        /// 接收任务
        /// </summary>
        public string SolveMenuVisibility
        {
            get { return _SolveMenuVisibility; }
            set { _SolveMenuVisibility = value; OnPropertyChanged("SolveMenuVisibility"); }
        }
        private string _FinishMenuVisibility = "Collapsed";
        /// <summary>
        /// 完成任务
        /// </summary>
        public string FinishMenuVisibility
        {
            get { return _FinishMenuVisibility; }
            set { _FinishMenuVisibility = value; OnPropertyChanged("FinishMenuVisibility"); }
        }
    }
}
