using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_ReadingRoom : INotifyPropertyChanged
    {
        SeatManage.ClassModel.ReadingRoomInfo _ReadingRoomModel = new SeatManage.ClassModel.ReadingRoomInfo();
        /// <summary>
        /// readingRoom Model
        /// </summary>
        public SeatManage.ClassModel.ReadingRoomInfo ReadingRoomModel
        {
            get { return _ReadingRoomModel; }
            set { _ReadingRoomModel = value; Changed("ReadingRoomModel"); }
        }
        public string No
        {
            get { return _ReadingRoomModel.No; }
            set { _ReadingRoomModel.No = value; Changed("No"); }
        }
        public string Name
        {
            get { return _ReadingRoomModel.Name; }
            set { _ReadingRoomModel.Name = value; Changed("Name"); }
        }
        ObservableCollection<ViewModel_Library> _LibraryList = new ObservableCollection<ViewModel_Library>();
        /// <summary>
        /// 校区列表
        /// </summary>
        public ObservableCollection<ViewModel_Library> LibraryList
        {
            get { return _LibraryList; }
            set { _LibraryList = value; Changed("LibraryList"); }
        }
        /// <summary>
        /// 绑定图书馆下拉列表
        /// </summary>
        public void LibraryCBBinding()
        {
            try
            {
                LibraryList = SchoolNoteEditer.Code.ReadingRoomEdit.GetLibrarys();
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
            if (No.Length > 6)
            {
                ErrorMessage = "编号不能超过六位";
                return;
            }
            if (string.IsNullOrEmpty(Name))
            {
                ErrorMessage = "名称不能为空";
                return;
            }
            if (_IsEdit)
            {
                if (!SeatManage.Bll.T_SM_ReadingRoom.UpdateReadingRoom(_ReadingRoomModel))
                {
                    ErrorMessage = "保存失败";
                }
            }
            else
            {
                if (SeatManage.Bll.T_SM_ReadingRoom.ReadingRoomIsExists(No))
                {
                    ErrorMessage = "编号已存在";
                    return;
                }
                _ReadingRoomModel.SeatList = new SeatManage.ClassModel.SeatLayout();
                _ReadingRoomModel.Setting = new SeatManage.ClassModel.ReadingRoomSetting();
                if (!SeatManage.Bll.T_SM_ReadingRoom.AddNewReadingRoom(_ReadingRoomModel))
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
            if (!SeatManage.Bll.T_SM_ReadingRoom.DeleteReadingRoom(_ReadingRoomModel))
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
