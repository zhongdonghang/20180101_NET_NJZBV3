using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AMS.ViewModel
{
    public class ViewModel_CouponsList : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModelCouponsList";
        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        private ObservableCollection<AMS.Model.CouponsInfo> _CouponsList = new ObservableCollection<AMS.Model.CouponsInfo>();
        /// <summary>
        /// 列表
        /// </summary>
        public ObservableCollection<AMS.Model.CouponsInfo> CouponsList
        {
            get { return _CouponsList; }
            set { _CouponsList = value; OnPropertyChanged("CouponsList"); }
        }
        public bool GetDataList()
        {
            string functionName = "GetDataList";
            try
            {
                //TODO:获取数据
                List<AMS.Model.AMS_Advertisement> modellist = AMS.ServiceProxy.AdvertisementOperationService.GetAdvertList(Model.Enum.AdType.SlipCustomerAd);
                CouponsList.Clear();
                foreach (AMS.Model.AMS_Advertisement model in modellist)
                {
                    AMS.Model.CouponsInfo newModel = new Model.CouponsInfo();
                    newModel = AMS.Model.CouponsInfo.ToModel(model.AdContent);
                    newModel.AdContent = model.AdContent;
                    newModel.CustomerID = model.CustomerID;
                    newModel.ID = model.ID;
                    newModel.OperatorID = model.OperatorID;
                    newModel.OperatorName = model.OperatorName;
                    newModel.ReleaseDate = model.ReleaseDate;
                    CouponsList.Add(newModel);
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
