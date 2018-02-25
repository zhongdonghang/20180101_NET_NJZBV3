using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AMS.ViewModel
{
    public class ViewModelProvinceListWindow : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModelProvinceList";
        #region 构造函数
        public ViewModelProvinceListWindow()
        {
            _ProvinceList = new ObservableCollection<Model.AMS_Province>();
        }
        #endregion

        #region 私有成员
        /// <summary>
        /// 省份列表
        /// </summary>
        private ObservableCollection<AMS.Model.AMS_Province> _ProvinceList;
        /// <summary>
        /// 错误信息
        /// </summary>
        private string _ErrorMessage = "";
        #endregion

        #region 属性
        /// <summary>
        /// 省份modellist
        /// </summary>
        public ObservableCollection<AMS.Model.AMS_Province> ProvinceList
        {
            get { return _ProvinceList; }
            set { _ProvinceList = value; OnPropertyChanged("ProvinceList"); }
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
        public bool GetDataList()
        {
            string functionName = "GetDataList";
            try
            {
                List<AMS.Model.AMS_Province> modellist = new List<Model.AMS_Province>();
                //TODO:获取数据
                modellist = AMS.ServiceProxy.IProvinceService.GetProvinceList();
                ProvinceList.Clear();
                foreach (AMS.Model.AMS_Province model in modellist)
                {
                    ProvinceList.Add(model);
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
        #endregion
        
        }
    }
}
