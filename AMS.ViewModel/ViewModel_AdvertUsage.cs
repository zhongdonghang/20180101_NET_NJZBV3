using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AMS.ViewModel
{
    public class ViewModel_AdvertUsage : ViewModelObject
    {
        private ObservableCollection<ViewModel_AdvertUsageItem> _SchoolUsageList = new ObservableCollection<ViewModel_AdvertUsageItem>();
        /// <summary>
        /// 学校使用记录列表
        /// </summary>
        public ObservableCollection<ViewModel_AdvertUsageItem> SchoolUsageList
        {
            get { return _SchoolUsageList; }
            set { _SchoolUsageList = value; OnPropertyChanged("SchoolUsageList"); }
        }

        private AMS.Model.AMS_Advertisement _AdvertModel = new Model.AMS_Advertisement();
        /// <summary>
        /// 广告信息
        /// </summary>
        public AMS.Model.AMS_Advertisement AdvertModel
        {
            get { return _AdvertModel; }
            set { _AdvertModel = value; OnPropertyChanged("AdvertModel"); OnPropertyChanged("AdvertInfo"); }
        }

        private int _AdvertID = -1;
        /// <summary>
        /// 广告编号
        /// </summary>
        public int AdvertID
        {
            get { return _AdvertID; }
            set { _AdvertID = value; }
        }

        private ViewModel_AdvertUsageItem _SelectedUsage = new ViewModel_AdvertUsageItem();
        /// <summary>
        /// 选中查看的记录
        /// </summary>
        public ViewModel_AdvertUsageItem SelectedUsage
        {
            get { return _SelectedUsage; }
            set { _SelectedUsage = value; OnPropertyChanged("SelectedUsage"); }
        }
        /// <summary>
        /// 显示消息
        /// </summary>
        public string AdvertInfo
        {
            get { return "广告编号：" + _AdvertModel.Num + "   广告名称：" + _AdvertModel.Name; }
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
        public void GetDataList()
        {
            AMS.Model.AMS_AdvertUsage all = new Model.AMS_AdvertUsage();
            all.SchoolID = -1;
            all.SchoolName = "总计";
            SchoolUsageList.Add(new ViewModel_AdvertUsageItem() { UsageModel = all });
            List<AMS.Model.AMS_AdvertUsage> usagelist = AMS.ServiceProxy.AdvertisementOperationService.GetAdvertUsage(-1, _AdvertID);
            foreach (AMS.Model.AMS_AdvertUsage u in usagelist)
            {
                SchoolUsageList[0].UsageModel.PlayCount += u.PlayCount;
                SchoolUsageList[0].UsageModel.PrintCount += u.PrintCount;
                SchoolUsageList[0].UsageModel.WatchCount += u.WatchCount;
                foreach (KeyValuePair<string, AMS.Model.AdvertisementUsage> item in u.ItemUsage)
                {
                    if (SchoolUsageList[0].UsageModel.ItemUsage.ContainsKey(item.Key))
                    {
                        SchoolUsageList[0].UsageModel.ItemUsage[item.Key].PlayCount += item.Value.PlayCount;
                        SchoolUsageList[0].UsageModel.ItemUsage[item.Key].PrintCount += item.Value.PrintCount;
                        SchoolUsageList[0].UsageModel.ItemUsage[item.Key].WatchCount += item.Value.WatchCount;
                    }
                    else
                    {
                        SchoolUsageList[0].UsageModel.ItemUsage.Add(item.Key, item.Value);
                    }
                }
                SchoolUsageList.Add(new ViewModel_AdvertUsageItem() { UsageModel = u });
            }
            SelectedUsage = SchoolUsageList[0];
        }
    }



    public class ViewModel_AdvertUsageItem : ViewModelObject
    {
        AMS.Model.AMS_AdvertUsage _UsageModel = new Model.AMS_AdvertUsage();
        /// <summary>
        /// model
        /// </summary>
        public AMS.Model.AMS_AdvertUsage UsageModel
        {
            get { return _UsageModel; }
            set { _UsageModel = value; OnPropertyChanged("UsageModel"); }
        }
    }
}
