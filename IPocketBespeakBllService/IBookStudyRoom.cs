using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;
namespace SeatManage.IPocketBespeakBllService
{
    public partial interface IPocketBespeakBllService
    {
        /// <summary>
        /// 获取研习间列表
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        [OperationContract]
        List<SeatManage.ClassModel.StudyRoomInfo> GetStudyRoomList();
        /// <summary>
        /// 获取研习间信息
        /// </summary>
        /// <param name="school"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.ClassModel.StudyRoomInfo GetStudyRoomInfo(string roomNo);
        /// <summary>
        /// 提交研习间记录
        /// </summary>
        /// <param name="school"></param>
        /// <param name="logModel"></param>
        /// <returns></returns>
        [OperationContract]
        string SubmitStudyLog(SeatManage.ClassModel.StudyBookingLog logModel);
        /// <summary>
        /// 获取记录列表
        /// </summary>
        /// <param name="school"></param>
        /// <param name="cardNo"></param>
        /// <param name="spanDay"></param>
        /// <returns></returns>
        [OperationContract]
        List<SeatManage.ClassModel.StudyBookingLog> GetStudyLogList(string cardNo, int spanDay);
        /// <summary>
        /// 获取单条记录信息
        /// </summary>
        /// <param name="school"></param>
        /// <param name="logID"></param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.ClassModel.StudyBookingLog GetStudyLog(int logID);
        /// <summary>
        /// 更新记录信息
        /// </summary>
        /// <param name="school"></param>
        /// <param name="logModel"></param>
        /// <returns></returns>
        [OperationContract]
        string CancelStudyLog(SeatManage.ClassModel.StudyBookingLog logModel);
    }
}
