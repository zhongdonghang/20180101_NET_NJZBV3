using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using SeatManage.Bll;
using SeatManage.SeatManageComm;

namespace LastSeatClient.ViewModel
{
    public class VM_UC_DateClock : VM_BasicModel
    {

        /// <summary>
        /// 当前时间
        /// </summary>
        private DateTime _NowDateTime = new DateTime();
        /// <summary>
        /// 刷新时间次数
        /// </summary>
        private int c = 0;
        /// <summary>
        /// 日期
        /// </summary>
        public string DateText
        {
            get { return _NowDateTime.ToString("yyyy年MM月dd日"); }
        }
        /// <summary>
        /// 星期
        /// </summary>
        public string WeekText
        {
            get { return CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(_NowDateTime.DayOfWeek); }
        }
        /// <summary>
        /// 时间
        /// </summary>
        public string TimeText
        {
            get { return _NowDateTime.ToString("HH:mm"); }
        }

        private TimeLoop timeDateTimeShow;
        private Thread showTimeThread;
        /// <summary>
        /// 同步服务器时间
        /// </summary>
        private void SyncServerTime()
        {
            try
            {
                _NowDateTime = ServiceDateTime.Now;
            }
            catch
            {
                _NowDateTime = DateTime.Now;
            }
            Changed("DateText");
            Changed("WeekText");
            Changed("TimeText");
        }
        /// <summary>
        /// 时间开始
        /// </summary>
        public void ShowTimeRun()
        {
            SyncServerTime();
            timeDateTimeShow = new TimeLoop(1000);
            timeDateTimeShow.TimeTo += timeDateTimeShow_TimeTo;
            showTimeThread = new Thread(timeDateTimeShow.TimeStrat);
            showTimeThread.Start();

        }
        //一秒执行
        void timeDateTimeShow_TimeTo(object sender, EventArgs e)
        {
            _NowDateTime = _NowDateTime.AddSeconds(1);
            Changed("TimeText");
            c++;
            if (c < 300)
            {
                return;
            }
            c = 0;
            SyncServerTime();
        }
    }
}
