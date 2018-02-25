using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SeatManage.IWeChatWcfService
{
    public partial interface IWeChatService : IExceptionService
    {
        /// <summary>
        /// 扫描选座终端二维码操作
        /// </summary>
        /// <param name="codeStr">扫描后的二维码</param>
        /// <returns></returns>
         [OperationContract]
        string QRcodeOperation(string codeStr, string studentNo);
        /// <summary>
        /// 扫描座位二维码
        /// </summary>
        /// <param name="codeStr">扫描后的二维码</param>
        /// <returns></returns>
         [OperationContract]
        string QRcodeSeatInfo(string codeStr);
    }
}
