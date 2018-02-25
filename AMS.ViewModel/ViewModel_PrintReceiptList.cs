using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AMS.ViewModel
{
    public class ViewModel_PrintReceiptList : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModel_PrintReceiptList";
        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        private ObservableCollection<AMS.Model.PrintReceiptInfo> _PrintReceiptList = new ObservableCollection<AMS.Model.PrintReceiptInfo>();
        /// <summary>
        /// 列表
        /// </summary>
        public ObservableCollection<AMS.Model.PrintReceiptInfo> PrintReceiptList
        {
            get { return _PrintReceiptList; }
            set { _PrintReceiptList = value; OnPropertyChanged("PrintReceiptList"); }
        }
        public bool GetDataList()
        {
            string functionName = "GetDataList";
            try
            {
                //TODO:获取数据
                List<AMS.Model.AMS_Advertisement> modellist = AMS.ServiceProxy.AdvertisementOperationService.GetAdvertList(Model.Enum.AdType.PrintReceiptAd);
                PrintReceiptList.Clear();
                foreach (AMS.Model.AMS_Advertisement model in modellist)
                {
                    AMS.Model.PrintReceiptInfo newModel = new Model.PrintReceiptInfo();
                    newModel = AMS.Model.PrintReceiptInfo.ToModel(model.AdContent);
                    newModel.AdContent = model.AdContent;
                    newModel.CustomerID = model.CustomerID;
                    newModel.ID = model.ID;
                    newModel.OperatorID = model.OperatorID;
                    newModel.OperatorName = model.OperatorName;
                    newModel.ReleaseDate = model.ReleaseDate;
                    PrintReceiptList.Add(newModel);
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
