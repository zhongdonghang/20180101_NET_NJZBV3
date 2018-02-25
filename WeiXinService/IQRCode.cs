using SeatManage.AppJsonModel;

namespace WeiXinService
{
    public partial interface IWeiXinService
    {
        /// <summary>
        /// 扫描选座终端二维码操作
        /// </summary>
        /// <param name="codeStr">扫描后的二维码</param>
        /// <param name="studentNo">学号</param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        string QRcodeOperation(string codeStr, string studentNo, string schoolNo);

        /// <summary>
        /// 扫描座位二维码
        /// </summary>
        /// <param name="codeStr">扫描后的二维码</param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        AJM_SeatStatus QRcodeSeatInfo(string codeStr, string schoolNo);
    }
}
