using System.Collections.Generic;
using System.ServiceModel;
using SeatManage.ClassModel;

namespace SeatManage.IPocketBespeakBllServiceV2
{

    public partial interface IPocketBespeakBllService
    {
        /// <summary>
        /// 设置暂离，返回提示信息。
        /// </summary>
        /// <param name="school">学校</param>
        /// <param name="reader">读者</param>
        /// <returns></returns>
        [OperationContract]
        string SetShortLeave(string cardNo);

        /// <summary>
        /// 释放座位，返回操作结果
        /// </summary>
        /// <param name="school"></param>
        /// <param name="reader"></param>
        /// <returns>返回提示信息</returns>
        [OperationContract]
        string FreeSeat(string cardNo);

        /// <summary>
        /// 获取读者当前状态
        /// </summary>
        /// <param name="school"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        [OperationContract]
        ReaderInfo GetReaderInfo(string cardNo);
        /// <summary>
        /// 获取所有阅览室的座位使用状态
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<string, ReadingRoomSeatUsedState_Ex> GetAllRoomSeatUsedState();
        /// <summary>
        /// 续时
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        [OperationContract]
        string DelaySeatUsedTime(ReaderInfo reader);
        /// <summary>
        /// 暂离回来
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        [OperationContract]
        string ReaderComeBack(ReaderInfo reader);
        /// <summary>
        /// 选择座位
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="seatNum"></param>
        /// <param name="readingRoomNum"></param>
        /// <returns></returns>
        [OperationContract]
        string SelectSeat(string cardNo, string seatNum, string readingRoomNum);
    }
}
