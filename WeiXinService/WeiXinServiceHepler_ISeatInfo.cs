using System;
using System.Collections.Generic;
using SeatManage.AppJsonModel;
using SeatManage.MobileAppDataObtainProxy;
using SeatManage.SeatManageComm;

namespace WeiXinService
{
    public partial class WeiXinServiceHepler : IWeiXinService
    {
        /// <summary>
        /// 获取常用座位
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <param name="seatCount">获取的座位数目</param>
        /// <param name="dayCount">统计的天数</param>
        /// <returns></returns>
        public List<AJM_Seat> GetOftenSeat(string studentNo, int seatCount, int dayCount, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService=new MobileAppDataObtainProxy(schoolNo);
                string result = appService.GetOftenSeat(studentNo, seatCount, dayCount);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("获取常坐座位失败！");
                }
                if (!ajmResult.Result)
                {
                    throw new Exception(ajmResult.Msg);
                }
                List<AJM_Seat> ajmSeats = JSONSerializer.Deserialize<List<AJM_Seat>>(ajmResult.Msg);
                return ajmSeats;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取常坐座位失败：{0}",ex.Message));
                return null;
            }
        }
        /// <summary>
        /// 获取随机座位
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <returns></returns>
        public AJM_Seat GetRandomSeat(string roomNo,string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService=new MobileAppDataObtainProxy(schoolNo);
                string result = appService.GetRandomSeat(roomNo);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("获取随机座位失败！");
                }
                if (!ajmResult.Result)
                {
                    throw new Exception(ajmResult.Msg);
                }
                AJM_Seat ajmSeat = JSONSerializer.Deserialize<AJM_Seat>(ajmResult.Msg);
                return ajmSeat;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取随机座位失败：{0}",ex.Message));
                return null;
            }
        }

        /// <summary>
        /// 获取座位预约的信息
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="bespeakTime"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public AJM_SeatBespeakInfo GetSeatBespeakInfo(string seatNo, string roomNo, string bespeakTime, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.GetRandomSeat(roomNo);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("获取预约座位信息失败！");
                }
                if (!ajmResult.Result)
                {
                    throw new Exception(ajmResult.Msg);
                }
                AJM_SeatBespeakInfo ajmSeat = JSONSerializer.Deserialize<AJM_SeatBespeakInfo>(ajmResult.Msg);
                return ajmSeat;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取预约座位信息失败：{0}", ex.Message));
                return null;
            }
        }
    }
}
