using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.IPocketBespeak
{
    public interface IBookStudyRoom
    {
        /// <summary>
        /// 获取研习间列表
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        List<SeatManage.ClassModel.StudyRoomInfo> GetStudyRoomList(AMS.Model.AMS_School school);
        /// <summary>
        /// 获取研习间信息
        /// </summary>
        /// <param name="school"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        SeatManage.ClassModel.StudyRoomInfo GetStudyRoomInfo(AMS.Model.AMS_School school, string roomNo);
        /// <summary>
        /// 提交研习间记录
        /// </summary>
        /// <param name="school"></param>
        /// <param name="logModel"></param>
        /// <returns></returns>
        string SubmitStudyLog(AMS.Model.AMS_School school, SeatManage.ClassModel.StudyBookingLog logModel);
        /// <summary>
        /// 获取记录列表
        /// </summary>
        /// <param name="school"></param>
        /// <param name="cardNo"></param>
        /// <param name="spanDay"></param>
        /// <returns></returns>
        List<SeatManage.ClassModel.StudyBookingLog> GetStudyLogList(AMS.Model.AMS_School school, string cardNo, int spanDay);
        /// <summary>
        /// 获取单条记录信息
        /// </summary>
        /// <param name="school"></param>
        /// <param name="logID"></param>
        /// <returns></returns>
        SeatManage.ClassModel.StudyBookingLog GetStudyLog(AMS.Model.AMS_School school, int logID);
        /// <summary>
        /// 更新记录信息
        /// </summary>
        /// <param name="school"></param>
        /// <param name="logModel"></param>
        /// <returns></returns>
        string CancelStudyLog(AMS.Model.AMS_School school, SeatManage.ClassModel.StudyBookingLog logModel);
    }
}
