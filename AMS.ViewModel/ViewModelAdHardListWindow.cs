using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using AMS.Model;

namespace AMS.ViewModel
{
    public class ViewModelAdHardListWindow : ViewModelObject
    {

        private static readonly string CLASSNAME = "ViewModelAdHardListWindow";
        #region 私有字段
        private string _ErrorMessage = "";
        private ObservableCollection<Model.AMS_HardAd> _HardAdInfoList;
        #endregion

        #region 构造函数
        public ViewModelAdHardListWindow()
        {
            _HardAdInfoList = new ObservableCollection<AMS_HardAd>();
        }
        #endregion

        #region 属性
        ///<summary>
        /// 硬广列表
        ///<summary>
        public ObservableCollection<AMS_HardAd> HardAdInfoList
        {
            get { return _HardAdInfoList; }
            set { _HardAdInfoList = value; OnPropertyChanged("HardAdInfoList"); }
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
                List<AMS.Model.AMS_HardAd> modellist = new List<Model.AMS_HardAd>();
                modellist = AMS.ServiceProxy.IHardAdService.GetAdHardList();
                HardAdInfoList.Clear();
                foreach (AMS.Model.AMS_HardAd model in modellist)
                {
                    HardAdInfoList.Add(model);
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
