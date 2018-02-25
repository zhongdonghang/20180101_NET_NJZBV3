using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.Model;
namespace AMS.ViewModel
{
    public class ViewModelLoginWindow : ViewModelObject
    {
       
        public ViewModelLoginWindow()
        {
           
        }
        private string _UserName="";
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                OnPropertyChanged("UserName"); 
            }
        }
        private string _Password;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                OnPropertyChanged("Password");
                 
            }
        }
        private string txtWarningContent="";
        /// <summary>
        /// 登录结果提示信息
        /// </summary>
        public string TxtWarningContent
        {
            get { return txtWarningContent; }
            set
            {
                txtWarningContent = value;
                OnPropertyChanged("TxtWarningContent");
            }
        }
         
        
        /// <summary>
        /// 登录
        /// </summary>
        public bool Login()
        {
            try
            {
                if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(UserName))
                {
                    TxtWarningContent = "请输入用户名或密码";
                    return false;
                }
                else
                {
                      User=AMS.ServiceProxy.LoginService.Login(UserName,Password);
                      if (User != null)
                      {
                          ViewModelObject.User = User;
                          return true;
                      }
                      else
                      {
                          TxtWarningContent = "用户名或密码错误";
                          return false;
                      }
                }
            }
            catch (CustomerException ex)
            {
                TxtWarningContent = string.Format("执行失败：{0}",ex.ErrorMessage);
                SeatManage.SeatManageComm.WriteLog.Write(ex.StackTrace.ToString());
                SeatManage.SeatManageComm.WriteLog.Write(ex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                TxtWarningContent = ex.Message;
                SeatManage.SeatManageComm.WriteLog.Write(ex.ToString());
                return false;
            }
           
        }
    }
}
