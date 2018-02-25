using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.Model;

namespace AMS.ViewModel
{
    public class ViewModelRollTitlesEditWindow:ViewModelObject
    {
        #region 构造函数
        public ViewModelRollTitlesEditWindow()
        {
            _RollTitles = new AMS_RollTitles();
        }
        public ViewModelRollTitlesEditWindow(AMS_RollTitles model)
        {
            _RollTitles = model;
        } 
        #endregion

        #region 私有成员
        private static readonly string CLASSNAME = "ViewModelRollTitlesWindow";

        private ViewModelCustomerListWindow _CustomerList = new ViewModelCustomerListWindow();
        /// <summary>
        /// 客户列表
        /// </summary>
        public ViewModelCustomerListWindow CustomerList
        {
            get { return _CustomerList; }
            set { _CustomerList = value; OnPropertyChanged("CustomerList"); }
        }

        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerId
        {
            get { return _RollTitles.CustomerId; }
            set { _RollTitles.CustomerId = value; OnPropertyChanged("CustomerId"); }
        }



        private AMS_RollTitles _RollTitles;
        /// <summary>
        /// 滚动文字model
        /// </summary>
        public AMS_RollTitles RollTitles
        {
            get { return _RollTitles; }
            set { _RollTitles = value; OnPropertyChanged("RollTitles"); }
        } 
        #endregion

        #region 属性
        /// <summary>
        /// 滚动广告编号
        /// </summary>
        public int ID
        {
            get { return _RollTitles.ID; }
            set { _RollTitles.ID = value; OnPropertyChanged("ID"); }
        }

        /// <summary>
        /// 滚动标题
        /// </summary>
        public string Name
        {
            get { return _RollTitles.Name; }
            set { _RollTitles.Name = value; OnPropertyChanged("Name"); }
        }

        /// <summary>
        /// 滚动内容
        /// </summary>
        public string Type
        {
            get { return _RollTitles.Type; }
            set { _RollTitles.Type = value; OnPropertyChanged("Type"); }
        }

        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime? EffectDate
        {
            get { return _RollTitles.EffectDate; }
            set { _RollTitles.EffectDate = value; OnPropertyChanged("EffectDate"); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate
        {
            get { return _RollTitles.EndDate; }
            set { _RollTitles.EndDate = value; OnPropertyChanged("EndDate"); }
        }

        public string Num
        {
            get { return _RollTitles.Num; }
            set { _RollTitles.Num = value; OnPropertyChanged("Num"); }
        }

        private bool _IsEdit;
        /// <summary>
        /// 是否更新
        /// </summary>
        public bool IsEdit
        {
            get { return _IsEdit; }
            set { _IsEdit = value; OnPropertyChanged("IsEdit"); }
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
        #endregion

        public string Operator
        {
            get
            {
                if (string.IsNullOrEmpty(RollTitles.OpratorName))
                {
                    return "编辑人：" + User.UserName;
                }
                else
                {
                    return _RollTitles.OpratorName;
                }
            }
        }

        public bool Save()
        {
            string functionName = "SaveRollTitles";
            try
            {
                if (_IsEdit)
                {
                    if (string.IsNullOrEmpty(_RollTitles.Num))
                    {
                        ErrorMessage = "编号不能为空！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(_RollTitles.Name))
                    {
                        ErrorMessage = "名称不能为空！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(_RollTitles.Type))
                    {
                        ErrorMessage = "内容不能为空！";
                        return false;
                    }
                    if (_RollTitles.CustomerId < 0)
                    {
                        ErrorMessage = "未选择客户!";
                        return false;
                    }
                    if (_RollTitles.EndDate < _RollTitles.EffectDate)
                    {
                        ErrorMessage = "结束日期不能小于开始日期!";
                        return false;
                    }
                    string result = "";
                    _RollTitles.OperatorID = User.ID;
                    result = AMS.ServiceProxy.IRollTitlesService.UpdateModel(_RollTitles);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ErrorMessage = string.Format("更新失败！{0}", result);
                        return false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(_RollTitles.Num))
                    {
                        ErrorMessage = "编号不能为空！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(_RollTitles.Name))
                    {
                        ErrorMessage = "名称不能为空！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(_RollTitles.Type))
                    {
                        ErrorMessage = "内容不能为空！";
                        return false;
                    }
                    if (_RollTitles.CustomerId < 0)
                    {
                        ErrorMessage = "未选择客户!";
                        return false;
                    }
                    if (_RollTitles.EndDate < _RollTitles.EffectDate)
                    {
                        ErrorMessage = "结束日期不能小于开始日期!";
                        return false;
                    }
                    string result = "";
                    _RollTitles.OperatorID = User.ID;
                    result = AMS.ServiceProxy.IRollTitlesService.AddRollTitles(_RollTitles);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ErrorMessage = string.Format("更新失败！{0}", result);
                        return false;
                    }
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

        public bool Del()
        {
            string functionName = "RollTitles";
            try
            {
                AMS.ServiceProxy.IRollTitlesService.DelModel(ID);
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
    }
}
