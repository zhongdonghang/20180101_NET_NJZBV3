using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.IWCFService;
using SeatManage.WcfAccessProxy;
using System.ServiceModel;
using SeatManage.SeatManageComm;

namespace SeatManage.Bll
{
    public class ClientConfigOperate
    {
        /// <summary>
        /// 获取终端设置
        /// </summary>
        /// <param name="ClientNo"></param>
        /// <returns></returns>
        public static ClassModel.TerminalInfo GetClientConfig(string ClientNo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetTerminalInfo(ClientNo);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取终端设置失败：" + ex.Message);
                throw new Exception("获取终端设置失败：" + ex.Message);
               // return new TerminalInfo();
            }
            

        }
        /// <summary>
        /// 根据链接字符串，和设备编号
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="ClientNo"></param>
        /// <returns></returns>
        public static ClassModel.TerminalInfo GetClientConfig(string connectionString, string ClientNo)
        {
            IWCFService.ISeatManageService seatService = WcfAccessProxy.ServiceProxy.CreateChannelSeatManageService(connectionString);
            bool error = false;
            try
            {
                return seatService.GetTerminalInfo(ClientNo);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取终端设置失败：" + ex.Message);
                return new TerminalInfo();
            }
           
        }
        /// <summary>
        /// 获取所有的终端设置
        /// </summary>
        /// <returns></returns>
        public static List<ClassModel.TerminalInfo> GetTerminalsInfo()
        {
            IWCFService.ISeatManageService SeatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                List<ClassModel.TerminalInfo> terminals = SeatService.GetAllTerminals();
                return terminals;
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取终端设置失败：" + ex.Message);
                return new List<TerminalInfo>();
            }
           
        }
        /// <summary>
        /// 更新终端设置
        /// </summary>
        /// <param name="terminal"></param>
        /// <returns></returns>
        public static bool UpdateTerminal(ClassModel.TerminalInfo terminal)
        {
            IWCFService.ISeatManageService SeatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                int i = SeatService.UpdateClient(terminal);
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write(string.Format("更新终端设置失败：{0}",ex.Message));
                throw ex;
            }
            
        }
        /// <summary>
        /// 更新终端设置
        /// </summary>
        /// <param name="connectionString">链接字符串</param>
        /// <param name="terminal">设备设置</param>
        /// <returns></returns>
        public static bool UpdateTerminal(string connectionString,ClassModel.TerminalInfo terminal)
        {
            IWCFService.ISeatManageService SeatService = WcfAccessProxy.ServiceProxy.CreateChannelSeatManageService(connectionString);
            bool error = false;
            try
            {
                int i = SeatService.UpdateClient(terminal);
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write(ex.Message);
                return false;
            }
           
        
        }
         
        /// <summary>
        /// 根据阅览室编号列表获取阅览室列表
        /// </summary>
        /// <param name="roomNums">编号列表</param>
        /// <returns></returns>
        public static List<ReadingRoomInfo> GetReadingRooms(List<string> roomNums)
        {
            IWCFService.ISeatManageService SeatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                List<ReadingRoomInfo> rooms = SeatService.GetReadingRoomInfo(roomNums);
                return rooms;
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write(ex.Message);
                return new List<ReadingRoomInfo>();
            }
            
        }
        //public ClassModel.ReadingRoomInfo GetRoomInfo(string roomNo)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
