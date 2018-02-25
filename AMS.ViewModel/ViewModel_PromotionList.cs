using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.IO;
using System.Collections.ObjectModel;

namespace AMS.ViewModel
{
    public class ViewModel_PromotionList : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModel_PromotionList";
        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        private ObservableCollection<AMS.Model.PromotionAdvertInfo> _PromotionAdverList = new ObservableCollection<AMS.Model.PromotionAdvertInfo>();
        /// <summary>
        /// 列表
        /// </summary>
        public ObservableCollection<AMS.Model.PromotionAdvertInfo> PromotionAdverList
        {
            get { return _PromotionAdverList; }
            set { _PromotionAdverList = value; OnPropertyChanged("PromotionAdverList"); }
        }
        public bool GetDataList()
        {
            string functionName = "GetDataList";
            try
            {
                //TODO:获取数据
                List<AMS.Model.AMS_Advertisement> modellist = AMS.ServiceProxy.AdvertisementOperationService.GetAdvertList(Model.Enum.AdType.PromotionAd);
                PromotionAdverList.Clear();
                foreach (AMS.Model.AMS_Advertisement model in modellist)
                {
                    AMS.Model.PromotionAdvertInfo newModel = new Model.PromotionAdvertInfo();
                    newModel = AMS.Model.PromotionAdvertInfo.ToModel(model.AdContent);
                    newModel.AdContent = model.AdContent;
                    newModel.CustomerID = model.CustomerID;
                    newModel.ID = model.ID;
                    newModel.OperatorID = model.OperatorID;
                    newModel.OperatorName = model.OperatorName;
                    newModel.ReleaseDate = model.ReleaseDate;
                    PromotionAdverList.Add(newModel);
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
