using System;
using SeatManage.AppJsonModel;
using SeatManage.MobileAppDataObtainProxy;
using SeatManage.SeatManageComm;

namespace WeiXinService
{
    public partial class WeiXinServiceHepler:IWeiXinService
    {
        /// <summary>
        /// 扫描选座终端二维码操作
        /// </summary>
        /// <param name="codeStr">扫描后的二维码</param>
        /// <param name="studentNo">学号</param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public string QRcodeOperation(string codeStr, string studentNo,string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService = new MobileAppDataObtainProxy(schoolNo);
                string result = appService.QRcodeOperation(codeStr, studentNo);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("扫描终端二维码操作失败！");
                }
                return ajmResult.Msg;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("扫描终端二维码操作失败：{0}",ex.Message));
                return null;
            }
        }
        // <summary>
        /// 扫描座位二维码
        /// </summary>
        /// <param name="codeStr">扫描后的二维码</param>
        /// <returns></returns>
        public AJM_SeatStatus QRcodeSeatInfo(string codeStr,string schoolNo)
        {
            try
            {
                IMobileAppDataObtianProxy appService=new MobileAppDataObtainProxy(schoolNo);
                string result = appService.QRcodeSeatInfo(codeStr);
                AJM_HandleResult ajmResult = JSONSerializer.Deserialize<AJM_HandleResult>(result);
                if (ajmResult == null)
                {
                    throw new Exception("扫描座位二维码失败！");
                }
                if (!ajmResult.Result)
                {
                    throw new Exception(ajmResult.Msg);
                }
                AJM_SeatStatus ajmSeatStatus = JSONSerializer.Deserialize<AJM_SeatStatus>(ajmResult.Msg);
                return ajmSeatStatus;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("扫描座位二维码失败：{0}", ex.Message));
                return null;
            }
        }

    }
}
