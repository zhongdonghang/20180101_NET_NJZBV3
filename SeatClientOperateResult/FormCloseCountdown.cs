using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace SeatClient.OperateResult
{
    /// <summary>
    /// 窗体倒计时关闭器
    /// </summary>
    public class FormCloseCountdown
    {
        int _CountdownSceonds = 0;
        /// <summary>
        /// 窗体关闭倒计时秒数
        /// </summary>
        public int CountdownSceonds
        {
            get { return _CountdownSceonds; } 
        } 
        System.Timers.Timer timer = null;
        public event EventHandler EventCountdown;
        private bool _IsDisopse = false;
        /// <summary>
        /// 是否已经释放
        /// </summary>
        public bool IsDisopse
        {
            get { return _IsDisopse; }
            set { _IsDisopse = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="countdownMinutes">倒计时秒数</param>
        /// <param name="form"></param>
        public FormCloseCountdown(int countdownSeconds)
        {
            _CountdownSceonds = countdownSeconds; 
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }
        /// <summary>
        /// 暂停计时
        /// </summary>
        public void Pause()
        {
            timer.Stop();
        }

        private static object lock1 = new object();
        /// <summary>
        /// 重新计时
        /// </summary>
        /// <param name="countdownSeconds"></param>
        public void ReStartTime(int countdownSeconds)
        {
            lock (lock1)
            {
                _CountdownSceonds = countdownSeconds;
            }
        }
        /// <summary>
        /// 停止计时。如果需要再计时需要重新初始化。
        /// </summary>
        public void Stop()
        { 
            timer.Close(); 
        }
        /// <summary>
        /// 开始计时
        /// </summary>
        public void Start()
        { 
            timer.Start();
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                timer.Stop();
                _CountdownSceonds -= 1;
                if (_CountdownSceonds < 0)
                {
                    timer.Dispose();
                }
                else
                {
                    if (EventCountdown != null)
                    {
                        EventCountdown(this, new EventArgs());
                    }
                    timer.Start();
                }
            }
            catch
            { }
        }

 
    }
}
