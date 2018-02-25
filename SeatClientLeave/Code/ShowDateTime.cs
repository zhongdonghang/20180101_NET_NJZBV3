using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.Bll;

namespace SeatClientLeave
{
    public class ShowDateTime
    {
        public event EventHandler ShowHandle;

        public ShowDateTime()
        {
            ShowTime();
        }
        /// <summary>
        /// 根据服务器时间 获取终端显示的时间
        /// </summary>
        /// <returns></returns>
        private void ShowTime()
        {
            serviceDatetime = ServiceDateTime.Now;
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Start();

            timer2 = new System.Timers.Timer();
            timer2.Interval = 1000 * 60 * 5;//20分钟同步一次
            timer2.Elapsed += new System.Timers.ElapsedEventHandler(timer2_Elapsed);
            timer2.Start();
        }

        void timer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer2.Stop();
            try
            {
                //从服务器上获取时间，并且赋值。
                serviceDatetime = ServiceDateTime.Now;
            }
            finally
            {
                timer2.Start();
            }
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
            serviceDatetime = serviceDatetime.AddSeconds(1);
            string date = serviceDatetime.ToLongDateString();
            string week = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(serviceDatetime.DayOfWeek);
            _ServiceDate = date + " " + week + " ";
            _ServiceTime = serviceDatetime.ToLongTimeString();
            if (ShowHandle != null)
            {
                ShowHandle(this, new EventArgs());
            }
            timer.Start();
        }

        public void Stop()
        {
            timer.Close();
            timer.Dispose();
        }
        private static DateTime serviceDatetime = DateTime.Parse("1900-1-1");
        private static string _ServiceDate = "";
        private static string _ServiceTime = "";

        public string ServiceDate
        {
            get
            {
                if (serviceDatetime.CompareTo(DateTime.Parse("1900-1-1")) == 0)
                {
                    ShowTime();
                }
                return _ServiceDate;
            }
        }
        public string ServiceTime
        {
            get
            {
                if (serviceDatetime.CompareTo(DateTime.Parse("1900-1-1")) == 0)
                {
                    ShowTime();
                }
                return _ServiceTime;
            }
        }

        System.Timers.Timer timer;
        /// <summary>
        /// 用来执行时间同步
        /// </summary>
        System.Timers.Timer timer2 = null;
    }
}
