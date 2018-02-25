/********************************************
 * 作者：王昊天
 * 创建时间：2013-6-4
 * 说明：读者同步
 * 修改人：
 * 修改时间：
 * *****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SeatManage.ClassModel;
using SeatManage.EnumType;
namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 同步读者信息库
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string ReaderInfoSycn();
    }
}
