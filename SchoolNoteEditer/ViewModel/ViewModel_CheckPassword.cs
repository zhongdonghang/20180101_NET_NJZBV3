using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_CheckPassword : INotifyPropertyChanged
    {
        private bool _IsJuneberry = false;
        /// <summary>
        /// 是否是管理员
        /// </summary>
        public bool IsJuneberry
        {
            get { return _IsJuneberry; }
            set { _IsJuneberry = value; }
        }
        private string _ErrorMessage = "";
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; Changed("ErrorMessage"); }
        }
        public bool Check(string userName, string password)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(userName))
            {
                ErrorMessage = "用户名或密码不能为空！";
                return false;
            }
            if (userName != "admin" && userName != "juneberry")
            {
                ErrorMessage = "用户名或密码错误！";
                return false;
            }
            try
            {
                if (userName == "admin")
                {
                    SeatManage.Bll.Users_ALL checkBll = new SeatManage.Bll.Users_ALL();
                    if (checkBll.CheckUser(userName, password) != "admin")
                    {
                        ErrorMessage = "用户名或密码错误！";
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    string schoolNo = SeatManage.Bll.Registry.GetSchoolNum();
                    DateTime dt = DateTime.Now;
                    string cps = SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(schoolNo);
                    string dts = SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32((dt.Year + dt.Month + dt.Day + dt.Hour).ToString());
                    string pw = SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32("Juneberry" + cps + dts);
                    if (password == pw && userName == "juneberry")
                    {
                        _IsJuneberry = true;
                        return true;
                    }
                    else
                    {
                        ErrorMessage = "用户名或密码错误！";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
