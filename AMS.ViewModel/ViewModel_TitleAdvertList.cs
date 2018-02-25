using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AMS.ViewModel
{
    public class ViewModel_TitleAdvertList : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModel_TitleAdvertList";
        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        private ObservableCollection<AMS.Model.TitleAdvertInfo> _TitleAdvertList = new ObservableCollection<AMS.Model.TitleAdvertInfo>();
        /// <summary>
        /// 列表
        /// </summary>
        public ObservableCollection<AMS.Model.TitleAdvertInfo> TitleAdvertList
        {
            get { return _TitleAdvertList; }
            set { _TitleAdvertList = value; OnPropertyChanged("TitleAdvertList"); }
        }
        public bool GetDataList()
        {
            string functionName = "GetDataList";
            try
            {
                //TODO:获取数据
                List<AMS.Model.AMS_Advertisement> modellist = AMS.ServiceProxy.AdvertisementOperationService.GetAdvertList(Model.Enum.AdType.TitleAd);
                TitleAdvertList.Clear();
                foreach (AMS.Model.AMS_Advertisement model in modellist)
                {
                    AMS.Model.TitleAdvertInfo newModel = new Model.TitleAdvertInfo();
                    newModel = AMS.Model.TitleAdvertInfo.ToModel(model.AdContent);
                    newModel.AdContent = model.AdContent;
                    newModel.CustomerID = model.CustomerID;
                    newModel.ID = model.ID;
                    newModel.OperatorID = model.OperatorID;
                    newModel.OperatorName = model.OperatorName;
                    newModel.ReleaseDate = model.ReleaseDate;
                    TitleAdvertList.Add(newModel);
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
    }
}
