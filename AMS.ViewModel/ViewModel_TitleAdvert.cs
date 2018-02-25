using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.ViewModel
{
    public class ViewModel_TitleAdvert : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModel_TitleAdvert";
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
        public int CustomerID
        {
            get { return _TitleAdvertModel.CustomerID; }
            set { _TitleAdvertModel.CustomerID = value; OnPropertyChanged("CustomerID"); }
        }
        private Model.TitleAdvertInfo _TitleAdvertModel = new Model.TitleAdvertInfo();
        /// <summary>
        /// 广告model
        /// </summary>
        public Model.TitleAdvertInfo TitleAdvertModel
        {
            get { return _TitleAdvertModel; }
            set { _TitleAdvertModel = value; }
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string Num
        {
            get { return _TitleAdvertModel.Num; }
            set { _TitleAdvertModel.Num = value; OnPropertyChanged("Num"); }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _TitleAdvertModel.Name; }
            set { _TitleAdvertModel.Name = value; OnPropertyChanged("Name"); }
        }
        /// <summary>
        /// 冠名
        /// </summary>
        public string TextContent
        {
            get { return _TitleAdvertModel.TextContent; }
            set { _TitleAdvertModel.TextContent = value; OnPropertyChanged("TextContent"); }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EffectDate
        {
            get
            {
                if (_TitleAdvertModel.EffectDate == null || _TitleAdvertModel.EffectDate < DateTime.Parse("2000-1-1"))
                {
                    _TitleAdvertModel.EffectDate = DateTime.Now.Date;
                }
                return _TitleAdvertModel.EffectDate;
            }
            set { _TitleAdvertModel.EffectDate = value; OnPropertyChanged("EffectDate"); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                if (_TitleAdvertModel.EndDate == null || _TitleAdvertModel.EndDate < DateTime.Parse("2000-1-1"))
                {
                    _TitleAdvertModel.EndDate = DateTime.Now.AddDays(30).Date;
                }
                return _TitleAdvertModel.EndDate;
            }
            set { _TitleAdvertModel.EndDate = value; OnPropertyChanged("EndDate"); }
        }
        /// <summary>
        /// 操作者
        /// </summary>
        public string Operator
        {
            get
            {
                if (string.IsNullOrEmpty(_TitleAdvertModel.OperatorName) && User != null)
                {
                    return "编辑人：" + User.UserName;
                }
                else
                {
                    return _TitleAdvertModel.OperatorName;
                }
            }
        }

        private bool _IsEdit = false;
        /// <summary>
        /// 是否是编辑模式
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

        public bool Save()
        {
            string functionName = "save";
            try
            {
                if (string.IsNullOrEmpty(TitleAdvertModel.Num))
                {
                    ErrorMessage = "广告的编号不能为空！";
                    return false;
                }
                if (string.IsNullOrEmpty(TitleAdvertModel.Name))
                {
                    ErrorMessage = "广告的名称不能为空！";
                    return false;
                }
                if (_TitleAdvertModel.TextContent == "")
                {
                    ErrorMessage = "内容不能为空！";
                    return false;
                }
                if (TitleAdvertModel.EffectDate == null)
                {
                    ErrorMessage = "广告的开始时间不能为空！";
                    return false;
                }
                if (TitleAdvertModel.EndDate == null)
                {
                    ErrorMessage = "广告的结束时间不能为空！";
                    return false;
                }
                if (TitleAdvertModel.EndDate < TitleAdvertModel.EffectDate)
                {
                    ErrorMessage = "广告的结束时间要大于开始时间！";
                    return false;
                }
                if (TitleAdvertModel.CustomerID < 0)
                {
                    ErrorMessage = "请选择客户！";
                    return false;
                }
                if (!IsEdit && AMS.ServiceProxy.AdvertisementOperationService.ExistSameAdvert(TitleAdvertModel.Num, TitleAdvertModel.Name, Model.Enum.AdType.TitleAd))
                {
                    ErrorMessage = "已存在存在相同名称或编号的冠名广告！";
                    return false;
                }
                TitleAdvertModel.OperatorID = User.ID;
                TitleAdvertModel.ImageFilePath.Clear();

                string resultstr = "";

                TitleAdvertModel.Type = Model.Enum.AdType.TitleAd;
                AMS.Model.AMS_Advertisement model = new Model.AMS_Advertisement();
                model.Type = Model.Enum.AdType.TitleAd;
                model.ID = TitleAdvertModel.ID;
                model.Name = TitleAdvertModel.Name;
                model.Num = TitleAdvertModel.Num;
                model.OperatorID = TitleAdvertModel.OperatorID;
                model.CustomerID = TitleAdvertModel.CustomerID;
                model.AdContent = TitleAdvertModel.ToXml();
                model.EffectDate = TitleAdvertModel.EffectDate;
                model.EndDate = TitleAdvertModel.EndDate;
                if (IsEdit)
                {
                    //TODO:更新                  
                    return AMS.ServiceProxy.AdvertisementOperationService.UpdateAdvertisement(model);
                }
                else
                {
                    //DOTO:添加
                    return AMS.ServiceProxy.AdvertisementOperationService.AddAdvertisement(model);
                }
                if (!string.IsNullOrEmpty(resultstr))
                {
                    ErrorMessage = string.Format("保存失败！{0}", resultstr);
                    return false;
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
