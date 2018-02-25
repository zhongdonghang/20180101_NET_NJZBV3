using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using AMS.Model;

namespace AMS.ViewModel
{
    public class ViewModelTitleAdListWindow:ViewModelObject
    {
          private static readonly string CLASSNAME = "ViewModelTitleAdListWindow";
        #region 私有字段
        private string _ErrorMessage = "";
        private ObservableCollection<Model.AMS_TitleAd> _TitleAdInfoList = new ObservableCollection<AMS_TitleAd>();
        #endregion

        #region 构造函数
        public ViewModelTitleAdListWindow()
        {
            _TitleAdInfoList = new ObservableCollection<AMS_TitleAd>();
        }
        #endregion

        #region 属性
        ///<summary>
        /// 冠名广告列表
        ///<summary>
        public ObservableCollection<AMS_TitleAd> TitleAdInfoList
        {
            get { return _TitleAdInfoList; }
            set { _TitleAdInfoList = value; OnPropertyChanged("TitleAdInfoList"); }
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
                List<AMS.Model.AMS_TitleAd> modellist = new List<Model.AMS_TitleAd>();
                modellist = AMS.ServiceProxy.ITitleAdService.GetTitleAdList();
                TitleAdInfoList.Clear();
                foreach (AMS.Model.AMS_TitleAd model in modellist)
                {
                    TitleAdInfoList.Add(model);
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
