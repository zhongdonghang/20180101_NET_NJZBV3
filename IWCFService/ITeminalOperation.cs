using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 获取终端配置
        /// </summary>
        /// <param name="teminalNo"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.ClassModel.TerminalInfoV2 GetTeminalInfo(string teminalNo);
        /// <summary>
        /// 获取所有终端
        /// </summary>
        /// <param name="teminalNo"></param>
        /// <returns></returns>
        [OperationContract]
        List<SeatManage.ClassModel.TerminalInfoV2> GetAllTeminalInfo();
        /// <summary>
        /// 更新终端配置
        /// </summary>
        /// <param name="teminalNo"></param>
        /// <returns></returns>
        [OperationContract]
        string UpdateTeminalInfo(SeatManage.ClassModel.TerminalInfoV2 teminal);
        /// <summary>
        /// 添加打印次数
        /// </summary>
        /// <param name="teminalNo"></param>
        /// <returns></returns>
        [OperationContract]
        string AddPrintCount(string teminalNo);

        /// <summary>
        /// 更新打印机状态
        /// </summary>
        /// <param name="teminalNo"></param>
        /// <param name="printerStatus"></param>
        /// <returns></returns>
        [OperationContract]
        string UpPrintStatus(string teminalNo, SeatManage.EnumType.Printer printerStatus);

        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns></returns>
        //[OperationContract]
        //DateTime GetServerDateTime();

        [OperationContract]
        int LastSeatCount(List<string> list);
        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="teminal"></param>
        /// <returns></returns>
        [OperationContract]
        string AddTeminalInfo(SeatManage.ClassModel.TerminalInfoV2 teminal);
        /// <summary>
        /// 获取终端阅览室的剩余座位
        /// </summary>
        /// <param name="roomList"></param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<string, ReadingRoomSeatUsedState_Ex> GetTeminaRoomStatus(List<string> roomList);
    }
}
