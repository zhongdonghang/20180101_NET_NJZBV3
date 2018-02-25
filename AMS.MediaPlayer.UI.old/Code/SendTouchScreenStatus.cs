using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using AMS.MediaPlayer.Code;

namespace AMS.MediaPlayer.UI.Code
{
    /// <summary>
    /// 获取触目屏终端运行状态
    /// </summary>
    class SendTouchScreenStatus
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
        public SendTouchScreenStatus()
        {
            //初始化对象
            DirectoryInfo d = new DirectoryInfo(PlayerSetting.ImagelocadPath);
            if (!d.Exists)
            {
                d.Create();
            }
        }
        #endregion

        //#region 事件
        ///// <summary>
        ///// 消息发送
        ///// </summary>
        //public event MessageEventHandler SendMSG;
        //#endregion

        //#endregion

        #region 方法
        /// <summary>
        /// 开始运行
        /// </summary>
        /// <returns>返回转换好的运行状态</returns>
        public void Run()
        {
            //使用定时器
            //t = new System.Timers.Timer(20 * 60000);
            t = new System.Timers.Timer(5 * 60 * 1000);
            t.Enabled = true;
            t.AutoReset = true;
            t.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);
            t.Start();
        }
        /// <summary>
        /// 时间到达时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            t.Stop();

            //编辑文件名
            string imagefilepath = PlayerSetting.ImagelocadPath;
            if (imagefilepath.Length - 1 != imagefilepath.LastIndexOf("\\"))
            {
                imagefilepath += "\\" + PlayerSetting.SchoolNo + PlayerSetting.DeviceNo + ".jpg";
            }
            else
            {
                imagefilepath += PlayerSetting.SchoolNo + PlayerSetting.DeviceNo + ".jpg";
            }
            try
            {
                //获取截图
                AMS.Mediaplayer.Caputre.Caputre.Capture(imagefilepath);
                SeatManage.Bll.FileOperate fileOperate = new SeatManage.Bll.FileOperate();
                fileOperate.UpdateFile(imagefilepath, string.Format("{0}{1}.jpg", PlayerSetting.SchoolNo, PlayerSetting.DeviceNo), SeatManage.EnumType.SeatManageSubsystem.Caputre);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("设备截图失败：" + ex.Message);
            }
            try
            {
                //更新数据库状态
                SeatManage.ClassModel.TerminalInfoV2 terminal = SeatManage.Bll.TerminalOperatorService.GetTeminalSetting(PlayerSetting.DeviceNo); //AMS.AdvertisementManage_DAL.AMS_Device_DAL.UpdataDeviceStatus(_DeviceNo, "ScreenCaputre/" + _DeviceNo + ".jpg");
                terminal.ScreenshotPath = string.Format("{0}{1}.jpg", PlayerSetting.SchoolNo, PlayerSetting.DeviceNo);
                terminal.StatusUpdateTime = SeatManage.Bll.ServiceDateTime.Now;
                SeatManage.Bll.TerminalOperatorService.UpdateTeminalSetting(terminal);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("上传设备运行状态失败：" + ex.Message);
            }
            finally
            {
                t.Start();
            }
        }
        #endregion
    }
}
