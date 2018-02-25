using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using AMS.Model;

namespace AMS.ViewModel
{
    public class ViewModelIssuedInfoWindow : ViewModelObject
    {
        #region 构造函数
        public ViewModelIssuedInfoWindow()
        {
            _IssuedInfoList = new ObservableCollection<ViewModelCommandDetail>();
            _SchoolList = new ObservableCollection<Model.AMS_School>();
        }
        #endregion

        private static readonly string CLASSNAME = "ViewModelIssuedInfoWindow";
        #region 成员
        private ObservableCollection<ViewModelCommandDetail> _IssuedInfoList;
        /// <summary>
        /// 下发记录列表
        /// </summary>
        public ObservableCollection<ViewModelCommandDetail> IssuedInfoList
        {
            get { return _IssuedInfoList; }
            set { _IssuedInfoList = value; OnPropertyChanged("IssuedInfoList"); }
        }

        private ObservableCollection<AMS.Model.AMS_School> _SchoolList;
        /// <summary>
        /// 学校list
        /// </summary>
        public ObservableCollection<AMS.Model.AMS_School> SchoolList
        {
            get { return _SchoolList; }
            set { _SchoolList = value; OnPropertyChanged("SchoolList"); }
        }

        ObservableCollection<IssuedInfoTypeItem> _IssuedInfoTypeItems = new ObservableCollection<IssuedInfoTypeItem>();
        /// <summary>
        /// 下发文件类型
        /// </summary>
        public ObservableCollection<IssuedInfoTypeItem> IssuedInfoTypeItems
        {
            get { return _IssuedInfoTypeItems; }
            set { _IssuedInfoTypeItems = value; OnPropertyChanged("IssuedInfoTypeItems"); }
        }

        ObservableCollection<IssuedHandleResultItem> _IssuedHandleResultItems = new ObservableCollection<IssuedHandleResultItem>();

        /// <summary>
        /// 下发结果类型
        /// </summary>
        public ObservableCollection<IssuedHandleResultItem> IssuedHandleResultItems
        {
            get { return _IssuedHandleResultItems; }
            set { _IssuedHandleResultItems = value; }
        }

        ObservableCollection<ViewModelIssudeInfoScreening> _ViewModelIssudeInfoScreening = new ObservableCollection<ViewModelIssudeInfoScreening>();
        /// <summary>
        /// 筛选条件
        /// </summary>
        public ObservableCollection<ViewModelIssudeInfoScreening> ViewModelIssudeInfoScreening
        {
            get { return _ViewModelIssudeInfoScreening; }
            set { _ViewModelIssudeInfoScreening = value; }
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
        /// 获取下发记录信息
        /// </summary>
        /// <returns></returns>
        public bool GetDataList(ViewModelIssudeInfoScreening commandList)
        {
            string functionName = "GetDataList";
            try
            {
                List<AMS.Model.AMS_CommandDetail> modelList = new List<Model.AMS_CommandDetail>();
                if (commandList == null)
                {
                    modelList = AMS.ServiceProxy.IssuedInfoWindow.GetCommandListByCondition("-1", (int)AMS.Model.Enum.CommandType.None, (int)AMS.Model.Enum.CommandHandleResult.None);
                }
                else
                {
                    modelList = AMS.ServiceProxy.IssuedInfoWindow.GetCommandListByCondition(commandList.SelectedSchoolNumValue, commandList.SelectedCommandTypeValue, commandList.SelectedHandleResultTypeValue);
                }
                IssuedInfoList.Clear();
                foreach (AMS.Model.AMS_CommandDetail model in modelList)
                {
                    ViewModelCommandDetail vm = new ViewModelCommandDetail();
                    vm.CommandDetail = model;
                    IssuedInfoList.Add(vm);
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
        public bool DelCommandDetailInfo(int id)
        {
            string functionName = "GetDataList";
            try
            {
                List<AMS.Model.AMS_CommandDetail> modelList = new List<Model.AMS_CommandDetail>();
                if (AMS.ServiceProxy.IssuedInfoWindow.DelCommandDetailInfo(id))
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
        #region 绑定下拉框方法
        /// <summary>
        /// 绑定命令类型下拉列表
        /// </summary>
        public void BindCommandTypeItem()
        {
            IssuedInfoTypeItems.Add(new IssuedInfoTypeItem() { Value = (int)AMS.Model.Enum.CommandType.None, Text = "全部" });
            IssuedInfoTypeItems.Add(new IssuedInfoTypeItem() { Value = (int)AMS.Model.Enum.CommandType.Playlist, Text = "播放列表" });
            IssuedInfoTypeItems.Add(new IssuedInfoTypeItem() { Value = (int)AMS.Model.Enum.CommandType.SlipCustomer, Text = "优惠券" });
            IssuedInfoTypeItems.Add(new IssuedInfoTypeItem() { Value = (int)AMS.Model.Enum.CommandType.ProgramUpgrade, Text = "程序更新" });
            IssuedInfoTypeItems.Add(new IssuedInfoTypeItem() { Value = (int)AMS.Model.Enum.CommandType.HardAd, Text = "硬广" });
            IssuedInfoTypeItems.Add(new IssuedInfoTypeItem() { Value = (int)AMS.Model.Enum.CommandType.PrintTemplate, Text = "凭条模板" });
            IssuedInfoTypeItems.Add(new IssuedInfoTypeItem() { Value = (int)AMS.Model.Enum.CommandType.TitleAd, Text = "冠名广告" });
            IssuedInfoTypeItems.Add(new IssuedInfoTypeItem() { Value = (int)AMS.Model.Enum.CommandType.RollTitles, Text = "滚动广告" });
        }
        /// <summary>
        /// 绑定命令处理结果下拉列表
        /// </summary>
        public void BindCommandHandleResultItem()
        {
            IssuedHandleResultItems.Add(new IssuedHandleResultItem() { Value = (int)AMS.Model.Enum.CommandHandleResult.None, Text = "默认" });
            IssuedHandleResultItems.Add(new IssuedHandleResultItem() { Value = (int)AMS.Model.Enum.CommandHandleResult.Wait, Text = "等待获取" });
            IssuedHandleResultItems.Add(new IssuedHandleResultItem() { Value = (int)AMS.Model.Enum.CommandHandleResult.Getting, Text = "正在获取" });
            IssuedHandleResultItems.Add(new IssuedHandleResultItem() { Value = (int)AMS.Model.Enum.CommandHandleResult.Success, Text = "获取完成" });
            IssuedHandleResultItems.Add(new IssuedHandleResultItem() { Value = (int)AMS.Model.Enum.CommandHandleResult.Failed, Text = "获取失败" });
        }
        /// <summary>
        /// 绑定学校列表
        /// </summary>
        public void BindSchoolList()
        {
            string functionName = "BindSchoolList";
            try
            {
                List<AMS.Model.AMS_ProvinceSchoolInfo> List = AMS.ServiceProxy.IssuedInfoWindow.GetSchoolList();
                SchoolList.Clear();
                SchoolList.Add(new AMS.Model.AMS_School() { Number = "-1", Name = "请选择" });
                foreach (AMS.Model.AMS_ProvinceSchoolInfo model in List)
                {
                    foreach (AMS.Model.AMS_School modelChild in model.Schools)
                    {
                        SchoolList.Add(modelChild);
                    }
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
        #endregion
        #endregion
    }
    #region 枚举model
    /// <summary>
    /// 下发信息类型Model
    /// </summary>
    public class IssuedInfoTypeItem : ViewModelObject
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
    /// 下发结果类型Model
    /// </summary>
    public class IssuedHandleResultItem : ViewModelObject
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
    #endregion

    #region 转换下发记录“记录状态”、“记录类型”ViewModel
    /// <summary>
    /// 转换下发记录“记录状态”、“记录类型”ViewModel
    /// </summary>
    public class ViewModelCommandDetail : ViewModelObject
    {
        private AMS.Model.AMS_CommandDetail _CommandList = new AMS_CommandDetail();

        public AMS.Model.AMS_CommandDetail CommandDetail
        {
            get { return _CommandList; }
            set { _CommandList = value; }
        }
        private string _FinishFlags = "";
        /// <summary>
        /// 下发记录状态
        /// </summary>
        public string FinishFlags
        {
            get
            {
                switch ((AMS.Model.Enum.CommandHandleResult)_CommandList.FinishFlag.Value)
                {
                    case Model.Enum.CommandHandleResult.Failed:
                        return "获取失败";
                    case Model.Enum.CommandHandleResult.Getting:
                        return "正在获取";
                    case Model.Enum.CommandHandleResult.None:
                        return "未知";
                    case Model.Enum.CommandHandleResult.Success:
                        return "成功";
                    case Model.Enum.CommandHandleResult.Wait:
                        return "等待";
                }
                return _FinishFlags;
            }
        }

        private string _CommandType = "";
        /// <summary>
        /// 下发命令类型
        /// </summary>
        public string CommandType
        {
            get
            {
                switch ((Model.Enum.CommandType)_CommandList.Command)
                {
                    case Model.Enum.CommandType.Caputre:
                        return "截图";
                    case Model.Enum.CommandType.HardAd:
                        return "硬广";
                    case Model.Enum.CommandType.None:
                        return "未知";
                    case Model.Enum.CommandType.Playlist:
                        return "播放列表";
                    case Model.Enum.CommandType.PrintTemplate:
                        return "凭条模板";
                    case Model.Enum.CommandType.ProgramUpgrade:
                        return "程序更新";
                    case Model.Enum.CommandType.SlipCustomer:
                        return "优惠券";
                    case Model.Enum.CommandType.TitleAd:
                        return "冠名广告";
                    case Model.Enum.CommandType.RollTitles:
                        return "滚动广告";
                }
                return _CommandType;
            }
        }

        private DateTime _ReleaseTime;
        /// <summary>
        /// 下发时间
        /// </summary>
        public DateTime ReleaseTime
        {
            get { return _CommandList.ReleaseTime; }
        }
        private string _ContentNumber;
        /// <summary>
        /// 下发内容编号
        /// </summary>
        public string ContentNumber
        {
            get { return _CommandList.ContentNumber; }
        }
        private string _ContentName;
        /// <summary>
        /// 下发内容名称
        /// </summary>
        public string ContentName
        {
            get { return _CommandList.ContentName; }
        }
        private string _SchoolName;
        /// <summary>
        /// 下发学校名称
        /// </summary>
        public string SchoolName
        {
            get { return _CommandList.SchoolName; }
        }
        private DateTime? _FinishTime;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? FinishTime
        {
            get
            {

                return _CommandList.FinishTime;
            }
        }

        private int? _Id;

        public int? Id
        {
            get { return _CommandList.ID; }
        }

        private int _ContentID;

        public int ContentID
        {
            get { return _CommandList.ContentID; }
        }

        private string _OperatorName;

        public string OperatorName
        {
            get { return _CommandList.OperatorName; }
        }
    }
    #endregion

    #region 筛选下发记录ViewModel
    public class ViewModelIssudeInfoScreening : ViewModelObject
    {
        private AMS.Model.View_CommandList _CommandList = new View_CommandList();

        public AMS.Model.View_CommandList CommandList
        {
            get { return _CommandList; }
            set { _CommandList = value; OnPropertyChanged("CommandList"); }
        }

        private string _SelectedSchoolNumValue = "-1";
        /// <summary>
        /// 记录学校列表选中的Item
        /// </summary>
        public string SelectedSchoolNumValue
        {
            get { return _SelectedSchoolNumValue; }
            set
            {
                _SelectedSchoolNumValue = value;
                OnPropertyChanged("SelectedSchoolNumValue");
            }
        }

        private int _SelectedHandleResultTypeValue = -1;
        /// <summary>
        /// 记录下发结果选中的Item
        /// </summary>
        public int SelectedHandleResultTypeValue
        {
            get { return _SelectedHandleResultTypeValue; }
            set
            {
                _SelectedHandleResultTypeValue = value;
                OnPropertyChanged("SelectedHandleResultTypeValue");
            }
        }

        private int _SelectedCommandTypeValue = -1;
        /// <summary>
        /// 记录下发类型选中的Item
        /// </summary>
        public int SelectedCommandTypeValue
        {
            get { return _SelectedCommandTypeValue; }
            set
            {
                _SelectedCommandTypeValue = value;
                OnPropertyChanged("SelectedCommandTypeValue");
            }
        }
    }
    #endregion
}
