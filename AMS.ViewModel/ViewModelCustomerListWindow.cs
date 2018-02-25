using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using AMS.Model;

namespace AMS.ViewModel
{
    public class ViewModelCustomerListWindow:ViewModelObject
    {

        private static readonly string CLASSNAME = "ViewModelCustomerListWindow";
        #region 私有字段
        private string _ErrorMessage = "";
        private ObservableCollection<Model.AMS_AdCustomer> _CustomerInfoList = new ObservableCollection<AMS_AdCustomer>();
        #endregion

        #region 构造函数
        public ViewModelCustomerListWindow()
        {
            _CustomerInfoList = new ObservableCollection<AMS_AdCustomer>();
        }
        #endregion

        #region 属性
        ///<summary>
        /// 客户列表
        ///<summary>
        public ObservableCollection<AMS_AdCustomer> CustomerInfoList
        {
            get { return _CustomerInfoList; }
            set { _CustomerInfoList = value; OnPropertyChanged("CustomerInfoList"); }
        }
       
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public bool GetDataList()
        {
            string functionName = "GetDataList";
            try
            {
                List<AMS.Model.AMS_AdCustomer> modellist = new List<Model.AMS_AdCustomer>();
                modellist = AMS.ServiceProxy.IAdCustomerService.GetCustomerList();
                CustomerInfoList.Clear();
                foreach (AMS.Model.AMS_AdCustomer model in modellist)
                {
                    CustomerInfoList.Add(model);
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
        #endregion
       
    }
}
