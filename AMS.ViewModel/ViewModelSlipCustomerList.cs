using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AMS.ViewModel
{
    public class ViewModelSlipCustomerList : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModelSlipCustomerList";
        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        private ObservableCollection<AMS.Model.AMS_SlipCustomer> _SlipCustomerList = new ObservableCollection<AMS.Model.AMS_SlipCustomer>();
        /// <summary>
        /// 列表
        /// </summary>
        public ObservableCollection<AMS.Model.AMS_SlipCustomer> SlipCustomerList
        {
            get { return _SlipCustomerList; }
            set { _SlipCustomerList = value; OnPropertyChanged("SlipCustomerList"); }
        }
        public bool GetDataList()
        {
            string functionName = "GetDataList";
            try
            {
                List<AMS.Model.AMS_SlipCustomer> modellist = new List<AMS.Model.AMS_SlipCustomer>();
                //TODO:获取数据
                modellist = AMS.ServiceProxy.ISlipCustomerService.GetSlipCustomerList();
                SlipCustomerList.Clear();
                foreach (AMS.Model.AMS_SlipCustomer model in modellist)
                {
                    SlipCustomerList.Add(model);
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
