using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_ReadingRoomInfoEdit : INotifyPropertyChanged
    {
        ObservableCollection<ViewModel.ViewModel_School> _Schools = new ObservableCollection<ViewModel.ViewModel_School>();
        /// <summary>
        /// 校区列表
        /// </summary>
        public ObservableCollection<ViewModel.ViewModel_School> Schools
        {
            get { return _Schools; }
            set { _Schools = value; Changed("Schools"); }
        }
        ObservableCollection<ViewModel.ViewModel_Library> _Librarys = new ObservableCollection<ViewModel.ViewModel_Library>();
        /// <summary>
        /// 图书馆列表
        /// </summary>
        public ObservableCollection<ViewModel.ViewModel_Library> Librarys
        {
            get { return _Librarys; }
            set { _Librarys = value; Changed("Librarys"); }
        }
        ObservableCollection<ViewModel.ViewModel_ReadingRoom> _ReadingRooms = new ObservableCollection<ViewModel.ViewModel_ReadingRoom>();
        /// <summary>
        /// 阅览室列表
        /// </summary>
        public ObservableCollection<ViewModel.ViewModel_ReadingRoom> ReadingRooms
        {
            get { return _ReadingRooms; }
            set { _ReadingRooms = value; Changed("ReadingRooms"); }
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
        public void GetData()
        {
            try
            {
                Schools = SchoolNoteEditer.Code.ReadingRoomEdit.GetSchools();
                Librarys = SchoolNoteEditer.Code.ReadingRoomEdit.GetLibrarys();
                ReadingRooms = SchoolNoteEditer.Code.ReadingRoomEdit.GetReadingRooms();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void SaveLayout()
        {
            try
            {
                foreach (var room in ReadingRooms)
                {
                    room.ReadingRoomModel.SeatList.RoomNo = room.No;
                    SeatManage.Bll.T_SM_ReadingRoom.UpdateSeatLayout(room.ReadingRoomModel.SeatList);
                }
                ErrorMessage = "保存成功！";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
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
