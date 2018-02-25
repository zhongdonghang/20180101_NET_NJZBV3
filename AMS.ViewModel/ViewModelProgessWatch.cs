using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AMS.ViewModel
{
    public class ViewModelProgessWatch : ViewModelObject
    {
        #region 构造函数
        public ViewModelProgessWatch()
        {
            _IssuedInfoList = new ObservableCollection<ViewModelIssureShowItem>();
            _SchoolList = new ObservableCollection<Model.AMS_School>();
            BindCommandTypeItem();
            BindCommandHandleResultItem();
            BindSchoolList();
        }
        #endregion

        private static readonly string CLASSNAME = "ViewModelProgessWatch";
        #region 成员
        private ObservableCollection<ViewModelIssureShowItem> _IssuedInfoList;
        /// <summary>
        /// 下发记录列表
        /// </summary>
        public ObservableCollection<ViewModelIssureShowItem> IssuedInfoList
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
        private int _SelectedSchoolID = 0;
        /// <summary>
        /// 选择的学校ID
        /// </summary>
        public int SelectedSchoolID
        {
            get { return _SelectedSchoolID; }
            set { _SelectedSchoolID = value; }
        }
        private AMS.Model.Enum.IsureCommandType _SelectCommandType = Model.Enum.IsureCommandType.None;
        /// <summary>
        /// 命令类型
        /// </summary>
        public AMS.Model.Enum.IsureCommandType SelectCommandType
        {
            get { return _SelectCommandType; }
            set { _SelectCommandType = value; }
        }
        private AMS.Model.Enum.CommandHandleResult _SelectCommandState = Model.Enum.CommandHandleResult.None;
        /// <summary>
        /// 命令状态
        /// </summary>
        public AMS.Model.Enum.CommandHandleResult SelectCommandState
        {
            get { return _SelectCommandState; }
            set { _SelectCommandState = value; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取下发记录信息
        /// </summary>
        /// <returns></returns>
        public bool GetDataList()
        {
            string functionName = "GetDataList";
            try
            {
                List<AMS.Model.AMS_IssureList> modelList = new List<Model.AMS_IssureList>();
                modelList = AMS.ServiceProxy.IssuredCommandService.GetCommandState(SelectCommandType, SelectedSchoolID, SelectCommandState);
                IssuedInfoList.Clear();
                foreach (AMS.Model.AMS_IssureList model in modelList)
                {
                    ViewModelIssureShowItem vm = new ViewModelIssureShowItem();
                    vm.IssureItemModel = model;
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
            IssuedInfoTypeItems.Add(new IssuedInfoTypeItem() { Value = (int)AMS.Model.Enum.IsureCommandType.None, Text = "全部类型" });
            IssuedInfoTypeItems.Add(new IssuedInfoTypeItem() { Value = (int)AMS.Model.Enum.IsureCommandType.Advertisement, Text = "广告" });
            IssuedInfoTypeItems.Add(new IssuedInfoTypeItem() { Value = (int)AMS.Model.Enum.IsureCommandType.AdvertUsage, Text = "广告使用状态" });
            IssuedInfoTypeItems.Add(new IssuedInfoTypeItem() { Value = (int)AMS.Model.Enum.IsureCommandType.EnterOutLog, Text = "使用记录" });
            IssuedInfoTypeItems.Add(new IssuedInfoTypeItem() { Value = (int)AMS.Model.Enum.IsureCommandType.State, Text = "设备状态" });
        }
        /// <summary>
        /// 绑定命令处理结果下拉列表
        /// </summary>
        public void BindCommandHandleResultItem()
        {
            IssuedHandleResultItems.Add(new IssuedHandleResultItem() { Value = (int)AMS.Model.Enum.CommandHandleResult.None, Text = "全部状态" });
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
        public bool DelectCommand(int id)
        {
            return AMS.ServiceProxy.IssuredCommandService.DeleteCommand(new Model.AMS_IssureList() { ID = id });
        }
        #endregion
        #endregion
    }
    /// <summary>
    /// 显示子项目
    /// </summary>
    public class ViewModelIssureShowItem : ViewModelObject
    {
        private AMS.Model.AMS_IssureList _IssureItemModel = new Model.AMS_IssureList();
        /// <summary>
        /// model
        /// </summary>
        public AMS.Model.AMS_IssureList IssureItemModel
        {
            get { return _IssureItemModel; }
            set { _IssureItemModel = value; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string FinishFlag
        {
            get
            {
                switch ((AMS.Model.Enum.CommandHandleResult)_IssureItemModel.Flag)
                {
                    case Model.Enum.CommandHandleResult.Failed:
                        return "获取失败";
                    case Model.Enum.CommandHandleResult.Getting:
                        return "正在获取";
                    case Model.Enum.CommandHandleResult.Success:
                        return "获取成功";
                    case Model.Enum.CommandHandleResult.Wait:
                        return "等待获取";
                    default: return "状态未知";
                }
            }
        }

        /// <summary>
        /// 下发命令类型
        /// </summary>
        public string CommandType
        {
            get
            {
                switch (_IssureItemModel.CommandType)
                {
                    case Model.Enum.IsureCommandType.Advertisement:
                        switch (_IssureItemModel.AdvertType)
                        {
                            case Model.Enum.AdType.PlaylistAd: return "播放列表";
                            case Model.Enum.AdType.PopAd: return "弹窗广告";
                            case Model.Enum.AdType.PrintReceiptAd: return "打印凭条";
                            case Model.Enum.AdType.PromotionAd: return "校园推广";
                            case Model.Enum.AdType.ReaderAd: return "读者广告";
                            case Model.Enum.AdType.SlipCustomerAd: return "优惠券";
                            case Model.Enum.AdType.TitleAd: return "冠名广告";
                        }
                        return "下发广告";
                    case Model.Enum.IsureCommandType.AdvertUsage:
                        return "获取广告使用状态";
                    case Model.Enum.IsureCommandType.EnterOutLog:
                        return "获取进出记录";
                    case Model.Enum.IsureCommandType.State:
                        return "获取使用状态";
                    default: return "命令未知或出错";
                }
            }
        }
        /// <summary>
        /// 下发时间
        /// </summary>
        public DateTime ReleaseTime
        {
            get { return _IssureItemModel.SubmitTime; }
        }
        /// <summary>
        /// 下发内容编号
        /// </summary>
        public string Content
        {
            get { return _IssureItemModel.AdInfo; }
        }
        /// <summary>
        /// 下发学校名称
        /// </summary>
        public string SchoolName
        {
            get { return _IssureItemModel.SchoolName; }
        }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime CompleteTime
        {
            get { return _IssureItemModel.CompleteTime; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime GetTime
        {
            get { return _IssureItemModel.GetTime; }
        }
        /// <summary>
        /// 下发时间
        /// </summary>
        public string OperatorName
        {
            get { return _IssureItemModel.OperatorName; }
        }
    }
}
