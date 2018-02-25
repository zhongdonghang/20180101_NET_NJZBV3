using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_Library : INotifyPropertyChanged
    {
        SeatManage.ClassModel.LibraryInfo _LibraryModel = new SeatManage.ClassModel.LibraryInfo();
        /// <summary>
        /// librarymodel
        /// </summary>
        public SeatManage.ClassModel.LibraryInfo LibraryModel
        {
            get { return _LibraryModel; }
            set { _LibraryModel = value; Changed("LibraryModel"); }
        }
        public string No
        {
            get { return _LibraryModel.No; }
            set { _LibraryModel.No = value; Changed("No"); }
        }
        public string Name
        {
            get { return _LibraryModel.Name; }
            set { _LibraryModel.Name = value; Changed("Name"); }
        }
        ObservableCollection<ViewModel_School> _SchoolList = new ObservableCollection<ViewModel_School>();
        /// <summary>
        /// 校区列表
        /// </summary>
        public ObservableCollection<ViewModel_School> SchoolList
        {
            get { return _SchoolList; }
            set { _SchoolList = value; Changed("SchoolList"); }
        }
        /// <summary>
        /// 绑定学校下拉列表
        /// </summary>
        public void ScholCBBinding()
        {
            try
            {
                SchoolList = SchoolNoteEditer.Code.ReadingRoomEdit.GetSchools();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
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
            if (No.Length > 2)
            {
                ErrorMessage = "编号不能超过两位";
                return;
            }
            if (string.IsNullOrEmpty(Name))
            {
                ErrorMessage = "名称不能为空";
                return;
            }
            if (_IsEdit)
            {

                if (!SeatManage.Bll.T_SM_Library.UpdataLibraryInfo(_LibraryModel))
                {
                    ErrorMessage = "保存失败";
                }
            }
            else
            {
                if (SeatManage.Bll.T_SM_Library.GetLibraryInfoList(null, No, null).Count > 0)
                {
                    ErrorMessage = "编号已存在";
                    return;
                }
                if (SeatManage.Bll.T_SM_Library.GetLibraryInfoList(null, null, Name).Count > 0)
                {
                    ErrorMessage = "名称已存在";
                    return;
                }
                if (!SeatManage.Bll.T_SM_Library.AddNewLibrary(_LibraryModel))
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
            if (!SeatManage.Bll.T_SM_Library.DeleteLibrary(_LibraryModel))
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
