using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;

namespace SeatManage.Bll
{
    public class T_SM_ReaderNotice
    {
        /// <summary>
        /// 添加一条读者消息
        /// </summary>
        /// <param name="model">读者消息的model</param>
        /// <returns></returns>
        public static int AddReaderNotice(ReaderNoticeInfo model)
        {
        IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
        bool error = false;
            try
            {
                return seatService.AddReaderNotice(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加读者消息失败：" + ex.Message);
                return -1;
            }
           
        }
        /// <summary>
        /// 添加一条读者消息
        /// </summary>
        /// <param name="model">读者消息的model</param>
        /// <returns></returns>
        public static bool SendPushMsg(PushMsgInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.SendMsg(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("发送读者消息失败：" + ex.Message);
                return false;
            }
          
        }
        /// <summary>
        /// 更新读者通知状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EnumType.HandleResult UpdateReaderNotice(ReaderNoticeInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            { 
                return seatService.UpdateReaderNotice(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("更新读者状态失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
          
        }
        /// <summary>
        /// 获取通知记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<ReaderNoticeInfo> GetReaderNoticeByCardNoStatus(string cardNo, EnumType.LogStatus logReadStatus)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetReaderNoticeByCardNoStatus(cardNo, logReadStatus);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取通知记录失败：" + ex.Message);
                return new List<ReaderNoticeInfo>() ;
            }
        
        }
        /// <summary>
        /// 设置已读
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public static string SetReaderNoteRead(List<ReaderNoticeInfo> modelList)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.SetReaderNoteRead(modelList);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取通知记录失败：" + ex.Message);
                return "设置失败";
            }
            
        }
    }
}
