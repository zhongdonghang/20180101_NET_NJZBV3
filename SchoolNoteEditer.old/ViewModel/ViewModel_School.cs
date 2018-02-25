using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_School : INotifyPropertyChanged
    {
        SeatManage.ClassModel.School _SchoolModel = new SeatManage.ClassModel.School();
        /// <summary>
        /// 学校model
        /// </summary>
        public SeatManage.ClassModel.School SchoolModel
        {
            get { return _SchoolModel; }
            set { _SchoolModel = value; Changed("SchoolModel"); }
        }
        public string No
        {
            get { return _SchoolModel.No; }
            set { _SchoolModel.No = value; Changed("No"); }
        }
        public string Name
        {
            get { return _SchoolModel.Name; }
            set { _SchoolModel.Name = value; Changed("Name"); }
        }
        private bool _IsEdit = false;
        /// <summary>
        /// 是否是编辑模式
        /// </summary>
        public bool IsEdit
        {
            get { return _IsEdit; }
            set { _IsEdit = value; Changed("IsEdit"); }
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
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public void Save()
        {
            ErrorMessage = "";
            if (string.IsNullOrEmpty(No))
            {
                ErrorMessage = "编号不能为空";
                return;
            }
            if (No.Length > 1)
            {
                ErrorMessage = "编号不能超过一位";
                return;
            }
            if (string.IsNullOrEmpty(Name))
            {
                ErrorMessage = "名称不能为空";
                return;
            }
            if (_IsEdit)
            {
                if (SeatManage.Bll.T_SM_School.GetSchoolInfoList(null, Name).Count > 0)
                {
                    ErrorMessage = "名称已存在";
                    return;
                }
                if (!SeatManage.Bll.T_SM_School.UpdataSchoolInfo(_SchoolModel))
                {
                    ErrorMessage = "保存失败";
                }
            }
            else
            {
                if (SeatManage.Bll.T_SM_School.GetSchoolInfoList(No, null).Count > 0)
                {
                    ErrorMessage = "编号已存在";
                    return;
                }
                if (SeatManage.Bll.T_SM_School.GetSchoolInfoList(null, Name).Count > 0)
                {
                    ErrorMessage = "名称已存在";
                    return;
                }
                if (!SeatManage.Bll.T_SM_School.AddNewSchool(_SchoolModel))
                {
                    ErrorMessage = "保存失败";
                }
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public void Delete()
        {
            if (!SeatManage.Bll.T_SM_School.DeleteSchool(_SchoolModel))
            {
                ErrorMessage = "删除失败";
            }
        }
        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
