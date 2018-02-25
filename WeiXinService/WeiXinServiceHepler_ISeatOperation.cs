using System;
using SeatManage.AppJsonModel;
using SeatManage.MobileAppDataObtainProxy;
using SeatManage.SeatManageComm;

namespace WeiXinService
{
    public partial class WeiXinServiceHepler : IWeiXinService
    {
        /// <summary>
        /// 预约座位
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="cardNo"></param>
        /// <param name="besapeakTime"></param>
        /// <param name="isNowBesapeak"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public string SubmitBesapeskSeat(string seatNo, string roomNo, string cardNo, string besapeakTime, bool isNowBesapeak, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.SubmitBesapeskSeat(seatNo, roomNo, cardNo, besapeakTime, isNowBesapeak);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("预约座位失败！");
                }
                return ajmResult.Msg;
            }
            catch (Exception ex)
            {
                WriteLog.Write("预约座位失败：" + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 根据预约记录的ID取消预约
        /// </summary>
        /// <param name="bespeakId"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public string CancelBesapeakById(int bespeakId, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.CancelBesapeak(bespeakId);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("取消预约失败！");
                }
                return ajmResult.Msg;
            }
            catch (Exception ex)
            {
                WriteLog.Write("取消预约失败：" + ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 根据读者学号取消当前的预约
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="bespeakDate">预约日期</param>
        /// <param name="schoolNo">学校编号</param>
        /// <returns></returns>
        public string CancelBesapeakByCardNo(string cardNo, string bespeakDate, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.CancelBesapeakByCardNo(cardNo, bespeakDate);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("根据学号取消当前预约失败！");
                }
                return ajmResult.Msg;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("根据学号取消当前预约失败：{0}", ex.Message));
                throw ex;
            }
        }
        /// <summary>
        /// 暂离
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public string ShortLeave(string cardNo, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.ShortLeave(cardNo);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("设置座位暂离失败！");
                }
                return ajmResult.Msg;
            }
            catch (Exception ex)
            {
                WriteLog.Write("设置座位暂离失败：" + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 释放座位
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public string ReleaseSeat(string cardNo, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.ReleaseSeat(cardNo);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("释放座位失败！");
                }
                return ajmResult.Msg;
            }
            catch (Exception ex)
            {
                WriteLog.Write("释放座位失败：" + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 取消等待
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public string CancelWait(string cardNo, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.CancelWait(cardNo);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("释放座位失败！");
                }
                return ajmResult.Msg;
            }
            catch (Exception ex)
            {
                WriteLog.Write("释放座位失败：" + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 更换座位
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="cardNo"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public string ChangeSeat(string seatNo, string roomNo, string cardNo, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.ChangeSeat(seatNo, roomNo, cardNo);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("更换座位失败！");
                }
                return ajmResult.Msg;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("更换座位失败：{0}", ex.Message));
                throw ex;
            }
        }

        /// <summary>
        /// 暂离回来
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public string ComeBack(string studentNo,string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.ComeBack(studentNo);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("暂离回来失败！");
                }
                return ajmResult.Msg;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("暂离回来失败：{0}", ex.Message));
                throw ex;
            }
        }

        /// <summary>
        /// 选择座位
        /// </summary>
        /// <param name="studentNo"></param>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        public string SelectSeat(string studentNo, string seatNo, string roomNo, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.SelectSeat(studentNo, seatNo, roomNo);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("选择座位失败！");
                }
                return ajmResult.Msg;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("选择座位失败：{0}", ex.Message));
                throw ex;
            }
        }

        /// <summary>
        /// 座位续时
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public string DelayTime(string studentNo, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.DelayTime(studentNo);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("座位续时失败！");
                }
                return ajmResult.Msg;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("座位续时失败：{0}", ex.Message));
                throw ex;
            }
        }

        /// <summary>
        /// 等待座位
        /// </summary>
        /// <param name="studentNo_A"></param>
        /// <param name="studentNo_B"></param>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        public string WaitSeat(string studentNo_A, string studentNo_B, string seatNo, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.WaitSeat(studentNo_A, studentNo_B, seatNo);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("等待座位失败！");
                }
                return ajmResult.Msg;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("等待座位失败：{0}", ex.Message));
                throw ex;
            }
        }

        /// <summary>
        /// 预约签到
        /// </summary>
        /// <param name="besapeakNo"></param>
        /// <returns></returns>
        public string ConfirmSeat(string besapeakNo, string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.ConfirmSeat(besapeakNo);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("预约签到失败！");
                }
                return ajmResult.Msg;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("预约签到失败：{0}", ex.Message));
                throw ex;
            }
        }
    }
}
