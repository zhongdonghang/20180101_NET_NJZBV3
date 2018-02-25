/***********************
 * 作者：王昊天
 * 创建时间：2013-5-23
 * 说明：监控服务循环控件
 * 修改人：
 * 修改时间：
 * **********************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.SeatManageComm
{
    public class TimeLoop
    {
        /// <summary>
        /// 时间循环
        /// </summary>
        private System.Timers.Timer looptime;

        /// <summary>
        /// 时间到达事件
        /// </summary>
        public event EventHandler TimeTo;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="time">循环时间</param>
        public TimeLoop(int time)
        {
            //循环为十秒
            looptime = new System.Timers.Timer(time);
            looptime.Enabled = true;
            looptime.Elapsed += new System.Timers.ElapsedEventHandler(looptime_Elapsed);
        }
        /// <summary>
        /// 计时开始
        /// </summary>
        public void TimeStrat()
        {
            looptime.Start();
        }
        /// <summary>
        /// 停止计时
        /// </summary>
        public void TimeStop()
        {
            looptime.Stop(); 
        }
        /// <summary>
        /// 释放控件
        /// </summary>
        public void TimeClose()
        {
            looptime.Dispose();
        }
        /// <summary>
        /// 时间到达触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void looptime_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (TimeTo != null)
            {
                TimeTo(this, new EventArgs());
            }
        }
    }
}
