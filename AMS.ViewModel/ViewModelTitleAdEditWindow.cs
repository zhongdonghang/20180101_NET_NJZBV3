using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.Model;

namespace AMS.ViewModel
{
    public class ViewModelTitleAdEditWindow:ViewModelObject
    {
          #region 构造函数
        public ViewModelTitleAdEditWindow()
        {
            _TitleAdModel = new AMS_TitleAd();
        }
        public ViewModelTitleAdEditWindow(AMS_TitleAd model)
        {
            _TitleAdModel = model;
        }
        #endregion

        #region 私有成员
        private static readonly string CLASSNAME = "ViewModelTitleAdEditWindow";
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
            get { return _TitleAdModel.CustomerId; }
            set { _TitleAdModel.CustomerId = value; OnPropertyChanged("CustomerId"); }
        }
        /// <summary>
        /// Model
        /// </summary>
        private AMS_TitleAd _TitleAdModel;

        public AMS_TitleAd TitleAdModel
        {
            get { return _TitleAdModel; }
            set { _TitleAdModel = value; OnPropertyChanged("TitleAdModel"); }
        }

        #endregion

        #region 属性
        /// <summary>
        /// 冠名广告ID
        /// </summary>
        public int ID
        {
            get { return _TitleAdModel.Id; }
            set { _TitleAdModel.Id = value; OnPropertyChanged("ID"); }
        }
       
        /// <summary>
        /// 冠名广告名称
        /// </summary>
        public string TitleAdName
        {
            get { return _TitleAdModel.Name; }
            set { _TitleAdModel.Name = value; OnPropertyChanged("TitleAdName"); }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string AdContent
        {
            get { return _TitleAdModel.AdContent; }
            set { _TitleAdModel.AdContent = value; OnPropertyChanged("AdContent"); }
        }
        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime? EffectDate
        {
            get { return _TitleAdModel.EffectDate; }
            set { _TitleAdModel.EffectDate = value; OnPropertyChanged("EffectDate"); }
        }
       /// <summary>
       /// 结束时间
       /// </summary>
        public DateTime? EndDate
        {
            get { return _TitleAdModel.EndDate; }
            set { _TitleAdModel.EndDate = value; OnPropertyChanged("EndDate"); }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string Num
        {
            get { return _TitleAdModel.Num; }
            set { _TitleAdModel.Num = value; OnPropertyChanged("}"); }
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
            get {
                if (string.IsNullOrEmpty(TitleAdModel.Operatorname))
                {
                    return "编辑人：" + User.UserName;
                }
                else
                {
                    return _TitleAdModel.Operatorname;
                }
            }
        }
        #region 方法
        public bool Save()
        {
            string functionName = "SaveTitleAd";
            try
            {

                if (_IsEdit)
                {
                    if (string.IsNullOrEmpty(_TitleAdModel.Name))
                    {
                        ErrorMessage = "名称不能为空！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(_TitleAdModel.AdContent))
                    {
                        ErrorMessage = "内容不能为空！";
                        return false;
                    }
                    if (_TitleAdModel.CustomerId < 0)
                    {
                        ErrorMessage = "未选择客户!";
                        return false;
                    }
                    if (_TitleAdModel.EndDate < _TitleAdModel.EffectDate)
                    {
                        ErrorMessage = "结束日期不能小于开始日期!";
                        return false;
                    }
                    string result = "";
                    _TitleAdModel.Operator = User.ID;
                    result=AMS.ServiceProxy.ITitleAdService.UpdateTitleAd(_TitleAdModel);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ErrorMessage = string.Format("更新失败！{0}", result);
                        return false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(_TitleAdModel.Name))
                    {
                        ErrorMessage = "请填写冠名名称！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(_TitleAdModel.AdContent))
                    {
                        ErrorMessage = "请填写广告内容！";
                        return false;
                    }
                    if (_TitleAdModel.CustomerId <0)
                    {
                        ErrorMessage = "请选择客户!";
                        return false;
                    }
                    if (string.IsNullOrEmpty(TitleAdModel.EffectDate.ToString()))
                    {
                        ErrorMessage = "发布日期不能为空！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(TitleAdModel.EndDate.ToString()))
                    {
                        ErrorMessage = "结束日期不能为空！";
                        return false;
                    }
                    if (_TitleAdModel.EndDate < _TitleAdModel.EffectDate)
                    {
                        ErrorMessage = "结束日期不能小于开始日期!";
                        return false;
                    }
                    _TitleAdModel.Operator = User.ID;
                    string result = "";
                    result = AMS.ServiceProxy.ITitleAdService.AddNewtTitleAd(_TitleAdModel); 
                    if (!string.IsNullOrEmpty(result))
                    {
                        ErrorMessage = string.Format("发布失败！{0}", result);
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
            string functionName = "DeleteTitleAd";
            try
            {
                AMS.ServiceProxy.ITitleAdService.DeleteTitleAd(_TitleAdModel);
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
