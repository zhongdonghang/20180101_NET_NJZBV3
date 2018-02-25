using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.Model;

namespace AMS.ViewModel
{
    public class ViewModelCallBackErrorInfoEditWindow : ViewModelObject
    {
        #region 构造函数
        public ViewModelCallBackErrorInfoEditWindow()
        {
            _TypeList.Add(new ViewModelCallBackType() { TypeName = "请选择问题类型", Type = -1 });
            _TypeList.Add(new ViewModelCallBackType() { TypeName = "硬件问题", Type = 0 });
            _TypeList.Add(new ViewModelCallBackType() { TypeName = "软件问题", Type = 1 });
            _TypeList.Add(new ViewModelCallBackType() { TypeName = "广告问题", Type = 2 });
            _TypeList.Add(new ViewModelCallBackType() { TypeName = "未知问题", Type = 3 });
        }
        #endregion

        #region 私有成员
        private static readonly string CLASSNAME = "ViewModelCallBackErrorInfoEditWindow";
        private bool _IsEdit = false;
        private string _ErrorMessage = "";
        private AMS_CallBackErrorInfo _CallBackErrorModel = new AMS_CallBackErrorInfo();
        private List<ViewModelCallBackType> _TypeList = new List<ViewModelCallBackType>();
        /// <summary>
        /// 类型列表
        /// </summary>
        public List<ViewModelCallBackType> TypeList
        {
            get { return _TypeList; }
        }
        private string _FbGBMargin = "10,22,10,0";

        public string FbGBMargin
        {
            get { return _FbGBMargin; }
            set { _FbGBMargin = value; OnPropertyChanged("FbGBMargin"); }
        }
        private string _SolveGBMargin = "10,212,10,0";

        public string SolveGBMargin
        {
            get { return _SolveGBMargin; }
            set { _SolveGBMargin = value; OnPropertyChanged("SolveGBMargin"); }
        }
        private string _FinishGBMargin = "10,212,10,0";

        public string FinishGBMargin
        {
            get { return _FinishGBMargin; }
            set { _FinishGBMargin = value; OnPropertyChanged("FinishGBMargin"); }
        }
        private string _WindowHeight = "730";

        public string WindowHeight
        {
            get { return _WindowHeight; }
            set { _WindowHeight = value; OnPropertyChanged("WindowHeight"); }
        }
        private string _BtnText = "保存";
        /// <summary>
        /// 按钮文字
        /// </summary>
        public string BtnText
        {
            get { return _BtnText; }
        }
        private string _BtnVisibility = "Visible";
        /// <summary>
        /// 按钮显示
        /// </summary>
        public string BtnVisibility
        {
            get { return _BtnVisibility; }
        }

        private List<AMS.Model.AMS_School> _SchoolList = new List<AMS_School>();
        /// <summary>
        /// 学校列表
        /// </summary>
        public List<AMS.Model.AMS_School> SchoolList
        {
            get { return _SchoolList; }
        }
        private string _FbVisibility = "Collapsed";
        /// <summary>
        /// 反馈框
        /// </summary>
        public string FbVisibility
        {
            get { return _FbVisibility; }
            set { _FbVisibility = value; OnPropertyChanged("FbVisibility"); }
        }
        private string _SolveVisibility = "Collapsed";
        /// <summary>
        /// 解决框
        /// </summary>
        public string SolveVisibility
        {
            get { return _SolveVisibility; }
            set { _SolveVisibility = value; OnPropertyChanged("SolveVisibility"); }
        }
        private string _FinishVisibility = "Collapsed";
        /// <summary>
        /// 完成框
        /// </summary>
        public string FinishVisibility
        {
            get { return _FinishVisibility; }
            set { _FinishVisibility = value; OnPropertyChanged("FinishVisibility"); }
        }
        private bool _FbEnable = true;
        /// <summary>
        /// 反馈激活
        /// </summary>
        public bool FbEnable
        {
            get { return _FbEnable; }
            set { _FbEnable = value; OnPropertyChanged("FbEnable"); }
        }
        private bool _SolveEnable = true;
        /// <summary>
        /// 解决激活
        /// </summary>
        public bool SolveEnable
        {
            get { return _SolveEnable; }
            set { _SolveEnable = value; OnPropertyChanged("SolveEnable"); }
        }
        private bool _FinishEnable = true;
        /// <summary>
        /// 完成激活
        /// </summary>
        public bool FinishEnable
        {
            get { return _FinishEnable; }
            set { _FinishEnable = value; OnPropertyChanged("FinishEnable"); }
        }
        private string _CBVisibility = "Visible";
        /// <summary>
        /// 下拉框显示
        /// </summary>
        public string CBVisibility
        {
            get { return _CBVisibility; }
            set { _CBVisibility = value; OnPropertyChanged("CBVisibility"); }
        }
        private string _LbVisibility = "Collapsed";
        /// <summary>
        /// 标题显示
        /// </summary>
        public string LbVisibility
        {
            get { return _LbVisibility; }
            set { _LbVisibility = value; OnPropertyChanged("LbVisibility"); }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 反馈描述
        /// </summary>
        public string FbDescribe
        {
            get { return _CallBackErrorModel.FbDescribe; }
            set { _CallBackErrorModel.FbDescribe = value; OnPropertyChanged("FbDescribe"); }
        }
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
        public string FbPerson
        {
            get
            {
                if (string.IsNullOrEmpty(_CallBackErrorModel.FbPerson))
                {
                    return "反馈人：  " + User.UserName;
                }
                else
                {
                    return "反馈人：  " + _CallBackErrorModel.FbPerson;
                }
            }
        }
        /// <summary>
        /// 反馈时间
        /// </summary>
        public string FbTime
        {
            get { return "反馈时间：  " + _CallBackErrorModel.FbTime.Value.ToLongDateString(); }
        }
        /// <summary>
        /// 解决时间
        /// </summary>
        public string SolveTime
        {
            get
            {
                if (_CallBackErrorModel.SolveTime.HasValue)
                {
                    return "解决时间：  " + _CallBackErrorModel.SolveTime.Value.ToLongDateString();
                }
                else
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 解决方法
        /// </summary>
        public string SolveWay
        {
            get { return _CallBackErrorModel.SolveWay; }
            set { _CallBackErrorModel.SolveWay = value; OnPropertyChanged("SolveWay"); }
        }
        /// <summary>
        /// 学校
        /// </summary>
        public string SchoolName
        {
            get { return _CallBackErrorModel.Schoolname; }
            set { _CallBackErrorModel.Schoolname = value; OnPropertyChanged("SchoolName"); }

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
                if (string.IsNullOrEmpty(_CallBackErrorModel.FbPerson))
                {
                    return "解决人：  " + User.UserName;
                }
                else
                {
                    return "解决人：  " + _CallBackErrorModel.FbPerson;
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
        /// 是否更新
        /// </summary>
        public bool IsEdit
        {
            get { return _IsEdit; }
            set { _IsEdit = value; OnPropertyChanged("IsEdit"); }
        }
        /// <summary>
        /// 记录人
        /// </summary>
        public string MarkMan
        {
            get { return _CallBackErrorModel.Markman; }
            set { _CallBackErrorModel.Markman = value; OnPropertyChanged("MarkMan"); }
        }
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
        public bool Save()
        {
            string functionName = "Save";
            try
            {
                string result = "";
                if (_IsEdit)
                {
                    switch ((AMS.Model.Enum.CallBackResult)_CallBackErrorModel.Solvestatic)
                    {
                        case Model.Enum.CallBackResult.Walting:
                            _CallBackErrorModel.Solvestatic = (int)AMS.Model.Enum.CallBackResult.Solving;
                            break;
                        case Model.Enum.CallBackResult.Solving:
                            _CallBackErrorModel.Solvestatic = (int)AMS.Model.Enum.CallBackResult.Finished;
                            _CallBackErrorModel.SolveTime = DateTime.Now.Date;
                            break;
                    }
                    _CallBackErrorModel.SolveManID = User.ID;
                    result = AMS.ServiceProxy.ICallBackInfoService.UpdateCallBackInfo(_CallBackErrorModel);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ErrorMessage = result;
                        return false;
                    }
                }
                else
                {
                    if (_CallBackErrorModel.SchoolId < 0)
                    {
                        ErrorMessage = "请选择学校！";
                        return false;
                    }
                    if (_CallBackErrorModel.ProblemType < 0)
                    {
                        ErrorMessage = "请选择问题类型！";
                    }
                    if (string.IsNullOrEmpty(_CallBackErrorModel.FbDescribe))
                    {
                        ErrorMessage = "请填写反馈描述信息!";
                        return false;
                    }
                    _CallBackErrorModel.Solvestatic = (int)AMS.Model.Enum.CallBackResult.Walting;
                    _CallBackErrorModel.MarkManID = User.ID;
                    result = AMS.ServiceProxy.ICallBackInfoService.AddNewCallBackInfo(_CallBackErrorModel);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ErrorMessage = result;
                        return false;
                    }
                }
                return true;
            }
            catch (CustomerException ex)
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
        public bool Delete()
        {
            string functionName = "Delete";
            string result = "";
            try
            {
                result = AMS.ServiceProxy.ICallBackInfoService.DeleteCallBackInfo(_CallBackErrorModel);
                if (!string.IsNullOrEmpty(result))
                {
                    ErrorMessage = result;
                    return false;
                }
                return true;
            }
            catch (CustomerException ex)
            {

                ErrorMessage = string.Format("{0}出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0}出自{1}.{2}", ex.Message, CLASSNAME, functionName);
                return false;
            }
        }
        public void GetSchool()
        {
            string functionName = "GetSchool";
            try
            {
                _SchoolList = AMS.ServiceProxy.ICallBackInfoService.GetSchoolList();
                _SchoolList.Insert(0, new AMS_School() { Id = -1, Name = "请选择学校" });
            }
            catch (CustomerException ex)
            {

                ErrorMessage = string.Format("{0}出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0}出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        public void GetStatus()
        {
            string functionName = "GetStatus";
            try
            {
                if ((AMS.Model.Enum.CallBackResult)_CallBackErrorModel.Solvestatic == Model.Enum.CallBackResult.None)
                {
                    _CBVisibility = "Visible";
                    _FbVisibility = "Visible";
                    _FinishVisibility = "Collapsed";
                    _FbEnable = true;
                    _FinishEnable = false;
                    _FinishGBMargin = "10,252,10,0";
                    _FbGBMargin = "10,22,10,0";
                    _LbVisibility = "Collapsed";
                    _WindowHeight = "310";
                    _BtnText = "保存";
                    _BtnVisibility = "Visible";

                }
                else if ((AMS.Model.Enum.CallBackResult)_CallBackErrorModel.Solvestatic == Model.Enum.CallBackResult.Walting && _IsEdit)
                {
                    _CBVisibility = "Collapsed";
                    _FbVisibility = "Visible";
                    _FinishVisibility = "Collapsed";
                    _FbEnable = false;
                    _FinishEnable = true;
                    _FinishGBMargin = "10,252,10,0";
                    _FbGBMargin = "10,22,10,0";
                    _LbVisibility = "Visible";
                    _WindowHeight = "310";
                    _BtnText = "接手";
                    _BtnVisibility = "Visible";
                }
                else if ((AMS.Model.Enum.CallBackResult)_CallBackErrorModel.Solvestatic == Model.Enum.CallBackResult.Walting && !_IsEdit)
                {
                    _CBVisibility = "Collapsed";
                    _FbVisibility = "Visible";
                    _FinishVisibility = "Collapsed";
                    _FbEnable = false;
                    _FinishEnable = true;
                    _FinishGBMargin = "10,252,10,0";
                    _FbGBMargin = "10,22,10,0";
                    _LbVisibility = "Visible";
                    _WindowHeight = "310";
                    _BtnText = "接手";
                    _BtnVisibility = "Collapsed";
                }
                else if ((AMS.Model.Enum.CallBackResult)_CallBackErrorModel.Solvestatic == Model.Enum.CallBackResult.Solving && _IsEdit)
                {
                    _CBVisibility = "Collapsed";
                    _FbVisibility = "Visible";
                    _FinishVisibility = "Visible";
                    _FbEnable = false;
                    _FinishEnable = true;
                    _FinishGBMargin = "10,212,10,0";
                    _FbGBMargin = "10,22,10,0";
                    _LbVisibility = "Visible";
                    _WindowHeight = "600";
                    _BtnText = "保存";
                    _BtnVisibility = "Visible";
                }
                else if ((AMS.Model.Enum.CallBackResult)_CallBackErrorModel.Solvestatic == Model.Enum.CallBackResult.Solving && !_IsEdit)
                {
                    _CBVisibility = "Collapsed";
                    _FbVisibility = "Visible";
                    _FinishVisibility = "Visible";
                    _FbEnable = false;
                    _FinishEnable = false;
                    _FinishGBMargin = "10,212,10,0";
                    _FbGBMargin = "10,22,10,0";
                    _LbVisibility = "Visible";
                    _WindowHeight = "600";
                    _BtnText = "保存";
                    _BtnVisibility = "Collapsed";
                }
                else if ((AMS.Model.Enum.CallBackResult)_CallBackErrorModel.Solvestatic == Model.Enum.CallBackResult.Finished)
                {
                    _CBVisibility = "Collapsed";
                    _FbVisibility = "Visible";
                    _FinishVisibility = "Visible";
                    _FbEnable = false;
                    _FinishEnable = false;
                    _FinishGBMargin = "10,212,10,0";
                    _FbGBMargin = "10,22,10,0";
                    _LbVisibility = "Visible";
                    _WindowHeight = "600";
                    _BtnText = "保存";
                    _BtnVisibility = "Collapsed";
                }
                OnPropertyChanged("CBVisibility");
                OnPropertyChanged("FbVisibility");
                OnPropertyChanged("FinishVisibility");
                OnPropertyChanged("FbEnable");
                OnPropertyChanged("FinishEnable");
                OnPropertyChanged("FinishGBMargin");
                OnPropertyChanged("FbGBMargin");
                OnPropertyChanged("LbVisibility");
                OnPropertyChanged("WindowHeight");
                OnPropertyChanged("BtnText");
                OnPropertyChanged("BtnVisibility");
            }
            catch (CustomerException ex)
            {

                ErrorMessage = string.Format("{0}出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0}出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        #endregion
    }
    public class ViewModelCallBackType
    {
        private string _TypeName = "";
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }
        private int _Type = -1;
        /// <summary>
        /// 类型
        /// </summary>
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
    }


}
