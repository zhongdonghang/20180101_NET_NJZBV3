using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using AMS.Model;

namespace AMS.ViewModel
{
    public class ViewModelCustomerEditWindow : ViewModelObject
    {
        #region 构造函数
        public ViewModelCustomerEditWindow()
        {
            _CustomerModel = new AMS_AdCustomer();
        }
        public ViewModelCustomerEditWindow(AMS_AdCustomer model)
        {
            _CustomerModel = model;
        }
        #endregion

        #region 私有成员
        private static readonly string CLASSNAME = "ViewModelCustomerEditWindow";
        private string _TextBoxVisibility = "Visible";
        private bool _IsEdit = false;
        private int _FormHight = 365;
        private string _ErrorMessage = "";
        private AMS_AdCustomer _CustomerModel;
     
        #endregion

        #region 属性
        /// <summary>
        /// 客户ID
        /// </summary>
        public int ID
        {
            get { return _CustomerModel.ID; }
            set { _CustomerModel.ID = value; OnPropertyChanged("ID"); }
        }
        /// <summary>
        /// Model
        /// </summary>
        public AMS_AdCustomer CustomerModel
        {
            get { return _CustomerModel; }
            set { _CustomerModel = value; OnPropertyChanged("CustomerModel"); }
        }
        /// <summary>
        /// 隐藏属性
        /// </summary>
        public string TextBoxVisibility
        {
            get { return _TextBoxVisibility; }
        }
        /// <summary>
        ///  窗体高度
        /// </summary>
        public int FormHight
        {
            get { return _FormHight; }  
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get { return _CustomerModel.CompanyName; }
            set { _CustomerModel.CompanyName = value; OnPropertyChanged("CustomerName"); }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _CustomerModel.Describe; }
            set { _CustomerModel.Describe = value; OnPropertyChanged("Remark"); }
        }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string LinkWay
        {
            get { return _CustomerModel.LinkWay; }
            set { _CustomerModel.LinkWay = value; OnPropertyChanged("LinkWay"); }
        }
        /// <summary>
        /// 客户编号
        /// </summary>
        public string CustomerNo
        {
            get { return _CustomerModel.CustomerNo; }
            set { _CustomerModel.CustomerNo = value; OnPropertyChanged("CustomerNo"); }
        }
        /// <summary>
        /// 是否更新
        /// </summary>
        public bool IsEdit
        {
            get { return _IsEdit; }
            set { _IsEdit = value;
            if (_IsEdit)
            {
                _TextBoxVisibility = "Collapsed";
                _FormHight = 450;
            }

            OnPropertyChanged("IsEdit");
            OnPropertyChanged("TextBoxVisibility");
            OnPropertyChanged("FormHight");
            }
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        #endregion

        #region 方法
        public bool Save()
        {
            string functionName = "SaveCustomer";
            try
            {
                
                if (_IsEdit)
                {
                    if (string.IsNullOrEmpty(_CustomerModel.CompanyName))
                    {
                        ErrorMessage = "客户名称不能为空！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(_CustomerModel.CustomerNo))
                    {
                        ErrorMessage = "客户编号不能为空!";
                        return false;
                    }
                    AMS.ServiceProxy.IAdCustomerService.UpdateCustomer(_CustomerModel);
                }
                else
                {
                    if (string.IsNullOrEmpty(_CustomerModel.CompanyName))
                    {
                        ErrorMessage = "请填写客户名称";
                        return false;
                    }
                    if (string.IsNullOrEmpty(_CustomerModel.CustomerNo))
                    {
                        ErrorMessage = "请填写客户编号";
                        return false;
                    }
                    string result = "";
                    result=AMS.ServiceProxy.IAdCustomerService.AddNewCustomer(_CustomerModel);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ErrorMessage = "添加失败!";
                        return false;
                    }
                }
                return true;
            }
            catch (CustomerException ex)
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
        public bool Delete()
        {
            string functionName = "DeleteCustomer";
            try
            {
                AMS.ServiceProxy.IAdCustomerService.DeleteCustomer(_CustomerModel);
                return true;
            }
            catch (CustomerException ex)
            {

                ErrorMessage = string.Format("{0}出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0}出自{1}.{2}", ex.Message, CLASSNAME, functionName);
                return false;
            }
        }
        #endregion
    }
}
