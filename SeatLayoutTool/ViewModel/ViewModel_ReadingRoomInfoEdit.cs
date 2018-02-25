using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SeatLayoutTool.ViewModel
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
                Schools = SeatLayoutTool.Code.ReadingRoomEdit.GetSchools();
                Librarys = SeatLayoutTool.Code.ReadingRoomEdit.GetLibrarys();
                ReadingRooms = SeatLayoutTool.Code.ReadingRoomEdit.GetReadingRooms();
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
