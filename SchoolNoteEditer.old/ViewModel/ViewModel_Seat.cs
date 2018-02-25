using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_Seat : INotifyPropertyChanged
    {
        SeatManage.ClassModel.Seat _SeatModel = new SeatManage.ClassModel.Seat();
        /// <summary>
        /// 座位的model
        /// </summary>
        public SeatManage.ClassModel.Seat SeatModel
        {
            get { return _SeatModel; }
            set { _SeatModel = value; Changed("SeatModel"); }
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
