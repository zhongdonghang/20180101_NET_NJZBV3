/**********************************************
 * 作者：王昊天
 * 创建日期：2013-5-23
 * 说明：开闭馆记录操作
 * 修改人：
 * 修改时间：
 * ********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using System.ServiceModel;

namespace SeatManage.Bll
{
    [Serializable]
    public class T_SM_RROpenCloseLog
    {
        /// <summary>
        /// 获取阅览室开闭馆计划，条件为null获取全部
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <param name="logstatus">记录状态</param>
        /// <param name="beginDate">记录起始时间</param>
        /// <param name="endDate">记录结束时间</param>
        /// <returns></returns>
        public static List<ReadingRoomOpenCloseLogInfo> GetReadingRoomOClog(string roomNum, LogStatus logstatus, string beginDate, string endDate)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetReadingRoomOCLog(roomNum, logstatus, beginDate, endDate);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取开闭馆计划失败：" + ex.Message);
                return new List<ReadingRoomOpenCloseLogInfo>();
            }
           
        }
        /// <summary>
        /// 增加开闭馆记录
        /// </summary>
        /// <param name="model">开闭馆记录</param>
        /// <param name="new_id">返回的新的记录id</param>
        /// <returns></returns>
        public static int AddNewReadingRoomOClog(ReadingRoomOpenCloseLogInfo model,ref int new_id)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddReadingRoomOClog(model, ref new_id);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加开闭馆计划失败：" + ex.Message);
                return -1;
            }
           
        }
    }
}
