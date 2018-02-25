using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AMS.ViewModel
{
    public class ViewModel_SchoolSeatUsage : ViewModelObject
    {
        public ViewModel_SchoolSeatUsage()
        {
            _SchoolList = new ObservableCollection<Model.AMS_School>();
            BindSchoolList();
        }

        private static readonly string CLASSNAME = "ViewModel_SchoolSeatUsage";

        private ObservableCollection<AMS.Model.AMS_School> _SchoolList;
        /// <summary>
        /// 学校list
        /// </summary>
        public ObservableCollection<AMS.Model.AMS_School> SchoolList
        {
            get { return _SchoolList; }
            set { _SchoolList = value; OnPropertyChanged("SchoolList"); }
        }
        private AMS.Model.SMS_SeatUsage _UsageModel = new Model.SMS_SeatUsage();
        /// <summary>
        /// model
        /// </summary>
        public AMS.Model.SMS_SeatUsage UsageModel
        {
            get { return _UsageModel; }
            set
            {
                _UsageModel = value;
                OnPropertyChanged("LeaveByAdmin");
                OnPropertyChanged("SeatOutTime");
                OnPropertyChanged("ShortLeaveOutTime");
                OnPropertyChanged("ShortLeaveByAdminOutTime");
                OnPropertyChanged("ShortLeaveByReaderOutTime");
                OnPropertyChanged("ShortLeaveByServiceOutTime");
                OnPropertyChanged("LeaveNotReadCard");
                OnPropertyChanged("None");
                OnPropertyChanged("BookingTimeOut");
                OnPropertyChanged("UsageModel");
            }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EffectDate
        {
            get
            {
                if (_UsageModel.StartTime == null || _UsageModel.StartTime < DateTime.Parse("2000-1-1"))
                {
                    _UsageModel.StartTime = DateTime.Now.AddMonths(-3).Date;
                }
                return _UsageModel.StartTime;
            }
            set { _UsageModel.StartTime = value; OnPropertyChanged("EffectDate"); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                if (_UsageModel.EndTime == null || _UsageModel.EndTime < DateTime.Parse("2000-1-1"))
                {
                    _UsageModel.EndTime = DateTime.Now.Date;
                }
                return _UsageModel.EndTime;
            }
            set { _UsageModel.EndTime = value; OnPropertyChanged("EndDate"); }
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
        public int BookingTimeOut
        {
            get { return UsageModel.BlackListRecords.ViolationRecords[SeatManage.EnumType.ViolationRecordsType.BookingTimeOut].Count; }
        }
        public int LeaveByAdmin
        {
            get { return UsageModel.BlackListRecords.ViolationRecords[SeatManage.EnumType.ViolationRecordsType.LeaveByAdmin].Count; }
        }
        public int SeatOutTime
        {
            get { return UsageModel.BlackListRecords.ViolationRecords[SeatManage.EnumType.ViolationRecordsType.SeatOutTime].Count; }
        }
        public int ShortLeaveOutTime
        {
            get { return UsageModel.BlackListRecords.ViolationRecords[SeatManage.EnumType.ViolationRecordsType.ShortLeaveOutTime].Count; }
        }
        public int ShortLeaveByAdminOutTime
        {
            get { return UsageModel.BlackListRecords.ViolationRecords[SeatManage.EnumType.ViolationRecordsType.ShortLeaveByAdminOutTime].Count; }
        }
        public int ShortLeaveByReaderOutTime
        {
            get { return UsageModel.BlackListRecords.ViolationRecords[SeatManage.EnumType.ViolationRecordsType.ShortLeaveByReaderOutTime].Count; }
        }
        public int ShortLeaveByServiceOutTime
        {
            get { return UsageModel.BlackListRecords.ViolationRecords[SeatManage.EnumType.ViolationRecordsType.ShortLeaveByServiceOutTime].Count; }
        }
        public int LeaveNotReadCard
        {
            get { return UsageModel.BlackListRecords.ViolationRecords[SeatManage.EnumType.ViolationRecordsType.LeaveNotReadCard].Count; }
        }
        public int None
        {
            get { return UsageModel.BlackListRecords.ViolationRecords[SeatManage.EnumType.ViolationRecordsType.None].Count; }
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



        public void GetDate()
        {
            string functionName = "GetDate";
            try
            {
                List<AMS.Model.SMS_SeatUsage> modelList = new List<Model.SMS_SeatUsage>();
                modelList = AMS.ServiceProxy.SeatUsageOperationService.GetSeatUsageList(_UsageModel.SchoolNum, _UsageModel.StartTime.Date, _UsageModel.EndTime.AddDays(1).Date);
                AMS.Model.SMS_SeatUsage newModel = new Model.SMS_SeatUsage();
                newModel.SchoolNum = _UsageModel.SchoolNum;
                newModel.StartTime = _UsageModel.StartTime;
                newModel.EndTime = _UsageModel.EndTime;
                if (modelList.Count < 1)
                {
                    ErrorMessage = string.Format("对不起，此时间段内暂无数据");
                }
                foreach (AMS.Model.SMS_SeatUsage model in modelList)
                {
                    newModel.BlackListRecords.BlackListCount += model.BlackListRecords.BlackListCount;
                    newModel.BlackListRecords.ViolationRecordsCount += model.BlackListRecords.ViolationRecordsCount;
                    foreach (KeyValuePair<SeatManage.EnumType.ViolationRecordsType, AMS.Model.ViolationInfo> item in model.BlackListRecords.ViolationRecords)
                    {
                        if (item.Value.Count > 0)
                        {
                        }
                        newModel.BlackListRecords.ViolationRecords[item.Key].Count += item.Value.Count;
                    }
                    foreach (KeyValuePair<string, AMS.Model.DeviceUsageInfo> item in model.DeviceUsage)
                    {
                        if (newModel.DeviceUsage.ContainsKey(item.Key))
                        {
                            newModel.DeviceUsage[item.Key].BookingConfirmCount += item.Value.BookingConfirmCount;
                            newModel.DeviceUsage[item.Key].ComeBackCount += item.Value.ComeBackCount;
                            newModel.DeviceUsage[item.Key].ContniueTimeCount += item.Value.ContniueTimeCount;
                            newModel.DeviceUsage[item.Key].LeaveCount += item.Value.LeaveCount;
                            newModel.DeviceUsage[item.Key].RushCardCount += item.Value.RushCardCount;
                            newModel.DeviceUsage[item.Key].SelectSeatCount += item.Value.SelectSeatCount;
                            newModel.DeviceUsage[item.Key].ShortLeaveCount += item.Value.ShortLeaveCount;
                        }
                        else
                        {
                            newModel.DeviceUsage.Add(item.Key, item.Value);
                        }
                    }
                    newModel.RushCardCount += model.RushCardCount;
                    if (newModel.SeatCount < model.SeatCount)
                    {
                        newModel.SeatCount = model.SeatCount;
                    }
                    if (newModel.UserCount < model.UserCount)
                    {
                        newModel.UserCount = model.UserCount;
                    }
                    newModel.SeatUeage.BookingCancelCount += model.SeatUeage.BookingCancelCount;
                    newModel.SeatUeage.BookingConfirmCount += model.SeatUeage.BookingConfirmCount;
                    newModel.SeatUeage.BookingCount += model.SeatUeage.BookingCount;
                    newModel.SeatUeage.BookingOverTimeCount += model.SeatUeage.BookingOverTimeCount;
                    newModel.SeatUeage.ContniueTimeCount += model.SeatUeage.ContniueTimeCount;
                    newModel.SeatUeage.ContniueTimeCountByAdmin += model.SeatUeage.ContniueTimeCountByAdmin;
                    newModel.SeatUeage.ContniueTimeCountByService += model.SeatUeage.ContniueTimeCountByService;
                    newModel.SeatUeage.ContniueTimeCountByUser += model.SeatUeage.ContniueTimeCountByUser;
                    newModel.SeatUeage.EnterOutVisitors += model.SeatUeage.EnterOutVisitors;
                    newModel.SeatUeage.LeaveCountByAdmin += model.SeatUeage.LeaveCountByAdmin;
                    newModel.SeatUeage.LeaveCountByService += model.SeatUeage.LeaveCountByService;
                    newModel.SeatUeage.LeaveCountByUser += model.SeatUeage.LeaveCountByUser;
                    newModel.SeatUeage.ReselectSeatCount += model.SeatUeage.ReselectSeatCount;
                    newModel.SeatUeage.SelectSeatCount += model.SeatUeage.SelectSeatCount;
                    newModel.SeatUeage.SelectSeatCountByAdmin += model.SeatUeage.SelectSeatCountByAdmin;
                    newModel.SeatUeage.ShortLeaveCount += model.SeatUeage.ShortLeaveCount;
                    newModel.SeatUeage.ShortLeaveCountByAdmin += model.SeatUeage.ShortLeaveCountByAdmin;
                    newModel.SeatUeage.ShortLeaveCountByReader += model.SeatUeage.ShortLeaveCountByReader;
                    newModel.SeatUeage.ShortLeaveCountByService += model.SeatUeage.ShortLeaveCountByService;
                    newModel.SeatUeage.ShortLeaveCountByUser += model.SeatUeage.ShortLeaveCountByUser;
                    newModel.SeatUeage.WaitSeatCount += model.SeatUeage.WaitSeatCount;
                }
                UsageModel = newModel;
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
    }
}
