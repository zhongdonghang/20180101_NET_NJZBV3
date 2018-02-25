using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
namespace SeatClientLeave.Code
{
   /// <summary>
   /// 窗体状态
   /// </summary>
    public enum FormWindowState
    {
        /// <summary>
        /// 最小化
        /// </summary>
        Minimized=0,
        /// <summary>
        /// 最大化
        /// </summary>
        Maximized=1,
        /// <summary>
        /// 原始大小
        /// </summary>
        Normal=2
    }

    /// <summary>
    /// 离开终端设置
    /// </summary>
    public class LeaveClientSetting
    {
         
        /// <summary>
        /// 读者刷卡时候的离开状态（None为弹出离开选择窗口）
        /// </summary>
        public static LeaveState LeaveState
        {
            get
            {
                string set = ConfigurationManager.AppSettings["LeaveState"];
                if (!string.IsNullOrEmpty(set))
                {
                    try
                    {
                        return (LeaveState)int.Parse(set);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("离开状态设置错误：" + ex.Message);
                    }
                }
                else
                {
                    throw new Exception("没有设置离开状态节点： <add key='LeaveState' value='0'/>");
                }
            }
        } 
        /// <summary>
        /// 窗体大小
        /// </summary>
        public static FormWindowState WindowState
        {
            get {
                string strSet = ConfigurationManager.AppSettings["windowState"];
                if (!string.IsNullOrEmpty(strSet))
                {
                    try
                    {
                        return (FormWindowState)int.Parse(strSet);
                    }   
                    catch (Exception ex)
                    {
                        throw new Exception("窗体大小设置错误：" + ex.Message);
                    }
                }
                else
                {
                    throw new Exception("没有设置窗体大小节点： <add key='windowState' value='0'/>");
                }
               } 
        }


    }
}
