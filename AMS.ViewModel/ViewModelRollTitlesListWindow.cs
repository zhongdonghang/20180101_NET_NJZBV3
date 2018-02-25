using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using AMS.Model;

namespace AMS.ViewModel
{
    public class ViewModelRollTitlesListWindow:ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModelRollTitlesListWindow";

        #region 私有字段
        private string _ErrorMessage = "";
        private ObservableCollection<Model.AMS_RollTitles> _RollTitlesList = new ObservableCollection<AMS_RollTitles>();
        #endregion

        #region 构造函数
        public ViewModelRollTitlesListWindow()
        {
            _RollTitlesList = new ObservableCollection<AMS_RollTitles>();
        } 
        #endregion

        #region 私有函数
        /// <summary>
        /// 滚动文字列表
        /// </summary>
        public ObservableCollection<Model.AMS_RollTitles> RollTitlesList
        {
            get { return _RollTitlesList; }
            set { _RollTitlesList = value; OnPropertyChanged("RollTitlesList"); }
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
                List<AMS.Model.AMS_RollTitles> modellist = new List<Model.AMS_RollTitles>();
                modellist = AMS.ServiceProxy.IRollTitlesService.GetList();
                RollTitlesList.Clear();
                foreach (AMS.Model.AMS_RollTitles model in modellist)
                {
                    RollTitlesList.Add(model);
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
