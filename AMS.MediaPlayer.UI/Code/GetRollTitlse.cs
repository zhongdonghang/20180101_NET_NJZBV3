using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace AMS.MediaPlayer.Code
{
    public class GetRollTitlse
    {
        /// <summary>
        /// 广告内容
        /// </summary>
        public string RollText
        {
            get;
            set;
        }
        int looptime;
        Timer t;
        Timer t1;
        /// <summary>
        /// 开始事件
        /// </summary>
        public event EventHandler RollStart;

        
        /// <summary>
        /// 获取广告
        /// </summary>

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <returns></returns>
        public void Get()
        {
            t1 = new Timer(1000);
            t1.Elapsed += new ElapsedEventHandler(t1_Elapsed);
            looptime = 0;
            t1.Start();
            t = new Timer(5000);
            t.Elapsed += new ElapsedEventHandler(t_Elapsed);
            t.Start();

        }

        void t1_Elapsed(object sender, ElapsedEventArgs e)
        {
            looptime += 1;
        }

        void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            t.Stop();
            try
            {
                RollText = SeatManage.Bll.AMS_RollTitles.GetModelStr();
                if (RollStart != null && !string.IsNullOrEmpty(RollText))
                {
                    RollStart(this, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                if (looptime > 300)
                {
                    t1.Stop();
                }
                else
                {
                    t.Start();
                }
            }
        }
    }
}
