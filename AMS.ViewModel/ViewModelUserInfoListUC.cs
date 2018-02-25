using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AMS.ViewModel
{
    public class ViewModelUserInfoListUC : ViewModelObject
    {
        #region 构造函数
        public ViewModelUserInfoListUC()
        {
            _UserInfoList = new ObservableCollection<Model.AMS_UserInfo>();
        }
        #endregion

        #region 私有成员
        private static readonly string CLASSNAME = "ViewModelUserInfoListUC";
        private ObservableCollection<AMS.Model.AMS_UserInfo> _UserInfoList;
        private string _ErrorMessage = "";

        #endregion

        #region 属性
        public ObservableCollection<AMS.Model.AMS_UserInfo> UserInfoList
        {
            get { return _UserInfoList; }
            set { _UserInfoList = value; OnPropertyChanged("UserInfoList"); }
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
        public bool GetDataList()
        {
            string functionName = "GetDataList";
            try
            {
                List<AMS.Model.AMS_UserInfo> modelList = new List<Model.AMS_UserInfo>();
                //TODO:获取用户列表
                modelList = AMS.ServiceProxy.IUserInfoService.GetUserInfoList();
                UserInfoList.Clear();
                foreach (AMS.Model.AMS_UserInfo model in modelList)
                {
                    UserInfoList.Add(model);
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
