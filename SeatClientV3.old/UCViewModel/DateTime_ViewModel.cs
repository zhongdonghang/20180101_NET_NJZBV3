using SeatManage.Bll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace SeatClientV3.UCViewModel
{
    public class DateTime_ViewModel : INotifyPropertyChanged
    {

        public string Date
        {
            get { return NowDateTime.ToLongDateString(); }
        }

        public string Time
        {
            get { return NowDateTime.ToShortTimeString(); }
        }

        public string Week
        {
            get { return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(NowDateTime.DayOfWeek); }
        }

        private DateTime NowDateTime = new DateTime();

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region 时间更新
        public SeatManage.SeatManageComm.TimeLoop timeDateTimeShow = null;
        public SeatManage.SeatManageComm.TimeLoop timeDateTimeSync = null;
        Thread showTimeThread = null;
        Thread syncTimeThread = null;
        public void ShowTimeRun()
        {
            NowDateTime = ServiceDateTime.Now;
            timeDateTimeShow = new SeatManage.SeatManageComm.TimeLoop(1000);
            timeDateTimeShow.TimeTo += new EventHandler(timeDateTimeShow_TimeTo);
            showTimeThread = new Thread(new ThreadStart(timeDateTimeShow.TimeStrat));
            showTimeThread.Start();
            timeDateTimeSync = new SeatManage.SeatManageComm.TimeLoop(300000);
            timeDateTimeSync.TimeTo += new EventHandler(timeDateTimeSync_TimeTo);
            syncTimeThread = new Thread(new ThreadStart(timeDateTimeSync.TimeStrat));
            syncTimeThread.Start();
        }
        //一秒执行
        void timeDateTimeShow_TimeTo(object sender, EventArgs e)
        {
            NowDateTime = NowDateTime.AddSeconds(1);
            OnPropertyChanged("Date");
            OnPropertyChanged("Time");
            OnPropertyChanged("Week");
        }
        //5min执行
        void timeDateTimeSync_TimeTo(object sender, EventArgs e)
        {
            NowDateTime = ServiceDateTime.Now;
            OnPropertyChanged("Date");
            OnPropertyChanged("Time");
            OnPropertyChanged("Week");
        }
        #endregion
    }
}
