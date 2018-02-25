using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AMS.ViewModel
{
    public class ViewModel_ReaderAdverList : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModel_ReaderAdverList";
        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        private ObservableCollection<AMS.Model.ReaderAdvertInfo> _ReaderAdverList = new ObservableCollection<AMS.Model.ReaderAdvertInfo>();
        /// <summary>
        /// 列表
        /// </summary>
        public ObservableCollection<AMS.Model.ReaderAdvertInfo> ReaderAdverList
        {
            get { return _ReaderAdverList; }
            set { _ReaderAdverList = value; OnPropertyChanged("ReaderAdverList"); }
        }
        public bool GetDataList()
        {
            string functionName = "GetDataList";
            try
            {
                //TODO:获取数据
                List<AMS.Model.AMS_Advertisement> modellist = AMS.ServiceProxy.AdvertisementOperationService.GetAdvertList(Model.Enum.AdType.ReaderAd);
                ReaderAdverList.Clear();
                foreach (AMS.Model.AMS_Advertisement model in modellist)
                {
                    AMS.Model.ReaderAdvertInfo newModel = new Model.ReaderAdvertInfo();
                    newModel = AMS.Model.ReaderAdvertInfo.ToModel(model.AdContent);
                    newModel.AdContent = model.AdContent;
                    newModel.CustomerID = model.CustomerID;
                    newModel.ID = model.ID;
                    newModel.OperatorID = model.OperatorID;
                    newModel.OperatorName = model.OperatorName;
                    newModel.ReleaseDate = model.ReleaseDate;
                    ReaderAdverList.Add(newModel);
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
