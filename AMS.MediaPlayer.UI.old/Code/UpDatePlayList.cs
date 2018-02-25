using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.MediaPlayer.Code;

namespace AMS.MediaPlayer.UI.Code
{
    public delegate void MessageEventHandler(object sender, string message);
    class UpDatePlayList
    {
        #region 私有成员

        /// <summary>
        /// 时间循环
        /// </summary>
        System.Timers.Timer t;
        #endregion


        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="deviceno">设备编号</param>
        /// <param name="imagepath">图片上传地址</param>
        /// <param name="time">运行间隔时间</param>
        /// <param name="imagelocadpath">本地截图保存地址</param>
        public UpDatePlayList()
        {

        }
        #endregion

        #region 事件
        /// <summary>
        /// 更新播放列表
        /// </summary>
        public event MessageEventHandler UpdatePlaylist;
        /// <summary>
        /// 错误
        /// </summary>
        public event MessageEventHandler Error;
        #endregion

        //#endregion

        #region 方法
        /// <summary>
        /// 开始运行
        /// </summary>
        /// <returns>返回转换好的运行状态</returns>
        public void Run()
        {
            //使用定时器
            t = new System.Timers.Timer(int.Parse(PlayerSetting.UpdateTime));
            t.Enabled = true;
            t.AutoReset = true;
            t.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);
        }
        /// <summary>
        /// 时间到达时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            t.Stop();
            try
            {
                SeatManage.ClassModel.TerminalInfoV2 terminal = SeatManage.Bll.TerminalOperatorService.GetTeminalSetting(PlayerSetting.DeviceNo);
                if (terminal.IsUpdatePlayList)
                {
                    if (UpdatePlaylist != null)
                    {
                        UpdatePlaylist(this, "");
                    }
                }
            }
            catch (Exception ex)
            {
                if (Error != null)
                {
                    Error(this, ex.Message);
                }
            }
            finally
            {
                t.Start();
            }
        }
        #endregion
    }
}
