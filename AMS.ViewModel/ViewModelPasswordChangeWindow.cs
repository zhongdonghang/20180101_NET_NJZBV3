using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.ViewModel
{
    public class ViewModelPasswordChangeWindow : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModelPasswordChangeWindow";
        public ViewModelPasswordChangeWindow()
        {
            _UserInfo = new Model.AMS_UserInfo();
        }
        #region 属性
        private AMS.Model.AMS_UserInfo _UserInfo;
        /// <summary>
        /// 用户信息
        /// </summary>
        public AMS.Model.AMS_UserInfo UserInfo
        {
            get { return _UserInfo; }
            set { _UserInfo = value; OnPropertyChanged("UserInfo"); }
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
        private bool _IsAdmin = false;
        /// <summary>
        /// 是否是管理员操作
        /// </summary>
        public bool IsAdmin
        {
            get { return _IsAdmin; }
            set
            {
                _IsAdmin = value;
                if (_IsAdmin)
                {
                    _TextBoxVisibility = "Collapsed";
                    _FormHight = 200;
                }
                OnPropertyChanged("IsAdmin");
                OnPropertyChanged("TextBoxVisibility");
                OnPropertyChanged("FormHight");
            }
        }
        private string _TextBoxVisibility = "Visible";
        /// <summary>
        /// 输入框隐藏
        /// </summary>
        public string TextBoxVisibility
        {
            get { return _TextBoxVisibility; }
        }
        private int _FormHight = 250;
        /// <summary>
        /// 窗体大小
        /// </summary>
        public int FormHight
        {
            get { return _FormHight; }
        }
        private string _OldPassword = "";
        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPassword
        {
            get { return _OldPassword; }
            set { _OldPassword = value; OnPropertyChanged("OdlPassword"); }
        }
        private string _NewPassword = "";
        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword
        {
            get { return _NewPassword; }
            set { _NewPassword = value; OnPropertyChanged("NewPassword"); }
        }

        private string _ConfirmPassword = "";
        /// <summary>
        /// 确认密码
        /// </summary>
        public string ConfirmPassword
        {
            get { return _ConfirmPassword; }
            set { _ConfirmPassword = value; OnPropertyChanged("ConfirmPassword"); }
        }
        #endregion

        #region 方法
        public bool Save()
        {
            string functionName = "Save";
            try
            {
                if (string.IsNullOrEmpty(_NewPassword))
                {
                    ErrorMessage = "请填写密码！";
                    return false;
                }
                if (string.IsNullOrEmpty(_ConfirmPassword))
                {
                    ErrorMessage = "请填写确认密码！";
                    return false;
                }
                if (_NewPassword != _ConfirmPassword)
                {
                    ErrorMessage = "两次密码输入不一致！";
                    return false;
                }
                if (!_IsAdmin && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(_OldPassword) != _UserInfo.UserPwd)
                {
                    ErrorMessage = "原密码不正确！";
                    return false;
                }
                //密码赋值
                _UserInfo.UserPwd = SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(_NewPassword);
                //TODO:更新密码操作
                string resultstr = "";
                resultstr = AMS.ServiceProxy.IUserInfoService.UpdateUser(_UserInfo);
                if (!string.IsNullOrEmpty(resultstr))
                {
                    ErrorMessage = string.Format("密码更新失败！{0}", resultstr);
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
        #endregion

    }
}
