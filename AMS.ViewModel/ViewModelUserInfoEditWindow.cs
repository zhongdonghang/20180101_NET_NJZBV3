using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using AMS.Model;

namespace AMS.ViewModel
{

    public class ViewModelUserInfoEditWindow : ViewModelObject
    {
        #region 构造函数
        public ViewModelUserInfoEditWindow()
        {
            _UserModel = new AMS_UserInfo();
        }
        #endregion

        #region 私有成员
        private static readonly string CLASSNAME = "ViewModelUserInfoEditWindow";
        /// <summary>
        /// model
        /// </summary>
        private AMS.Model.AMS_UserInfo _UserModel;
        private string _ConfirmPassword = "";
        private string _ErrorMessage = "";
        private bool _IsEdit = false;
        private string _TextBoxVisibility = "Visible";
        private int _FormHight = 365;



        #endregion

        #region 属性

        #endregion
        public AMS.Model.AMS_UserInfo UserModel
        {
            get { return _UserModel; }
            set { _UserModel = value; OnPropertyChanged("UserModel"); }
        }
        /// <summary>
        /// 登录账号
        /// </summary>
        public string Account
        {
            get { return _UserModel.LoginId; }
            set { _UserModel.LoginId = value; OnPropertyChanged("Account"); }
        }


        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword
        {
            get { return _UserModel.UserPwd; }
            set { _UserModel.UserPwd = value.Trim(); OnPropertyChanged("NewPassword"); }
        }


        /// <summary>
        /// 确认密码
        /// </summary>
        public string ConfirmPassword
        {
            get { return _ConfirmPassword; }
            set { _ConfirmPassword = value.Trim(); OnPropertyChanged("ConfirmPassword"); }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UseName
        {
            get { return _UserModel.UserName; }
            set { _UserModel.UserName = value; OnPropertyChanged("UseName"); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _UserModel.Remark; }
            set { _UserModel.Remark = value; OnPropertyChanged("Remark"); }
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        /// <summary>
        /// 部门
        /// </summary>
        public string BranchName
        {
            get { return _UserModel.BranchName; }
            set { _UserModel.BranchName = value; OnPropertyChanged("BranchName"); }
        }
        /// <summary>
        /// 编辑模式
        /// </summary>
        public bool IsEdit
        {
            get { return _IsEdit; }
            set
            {
                _IsEdit = value;
                if (_IsEdit)
                {
                    _TextBoxVisibility = "Collapsed";
                    _FormHight = 260;
                }

                OnPropertyChanged("IsEdit");
                OnPropertyChanged("TextBoxVisibility");
                OnPropertyChanged("FormHight");
            }
        }
        /// <summary>
        /// 窗体高度
        /// </summary>
        public int FormHight
        {
            get { return _FormHight; }
        }
        /// <summary>
        /// 输入框隐藏
        /// </summary>
        public string TextBoxVisibility
        {
            get { return _TextBoxVisibility; }
        }

        public bool Save()
        {
            string functionName = "Save";
            try
            {
                if (string.IsNullOrEmpty(_UserModel.UserName))
                {
                    ErrorMessage = "请填写用户姓名！";
                    return false;
                }
                if (string.IsNullOrEmpty(_UserModel.BranchName))
                {
                    ErrorMessage = "请填写用户部门！";
                    return false;
                }
                string result = "";
                if (_IsEdit)
                {
                    result = AMS.ServiceProxy.IUserInfoService.UpdateUser(_UserModel);
                }
                else
                {
                    if (string.IsNullOrEmpty(_UserModel.LoginId))
                    {
                        ErrorMessage = "请填写登录名！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(_UserModel.UserPwd))
                    {
                        ErrorMessage = "请填写密码！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(_ConfirmPassword))
                    {
                        ErrorMessage = "请填写确认密码！";
                        return false;
                    }
                    if (_UserModel.UserPwd != _ConfirmPassword)
                    {
                        ErrorMessage = "两次密码输入不一致！";
                        return false;
                    }
                    _UserModel.UserPwd = SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(_UserModel.UserPwd);
                    result = AMS.ServiceProxy.IUserInfoService.AddNewUser(_UserModel);
                }
                if (!string.IsNullOrEmpty(result))
                {
                    ErrorMessage = string.Format("用户保存失败！{0}", result);
                    _UserModel.UserPwd = _ConfirmPassword;
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
        public bool Delete()
        {
            string functionName = "Delete";
            try
            {
                string result = "";
                result = AMS.ServiceProxy.IUserInfoService.DeleteUser(_UserModel);
                if (!string.IsNullOrEmpty(result))
                {
                    ErrorMessage = string.Format("用户删除失败！{0}", result);
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
