using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;
using SeatManage.EnumType;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddStudyBookingLog(StudyBookingLog model);
        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateStudyBookingLog(StudyBookingLog model);
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteStudyBookingLog(StudyBookingLog model);
        /// <summary>
        /// 获取记录列表
        /// </summary>
        /// <param name="CardNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="staetDate"></param>
        /// <param name="endDate"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [OperationContract]
        List<StudyBookingLog> GetStudyBookingLogList(string CardNo, List<string> roomNo, DateTime startDate, DateTime endDate, List<CheckStatus> status);
        /// <summary>
        /// 根据ID获取记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        StudyBookingLog GetStudyBookingLogByID(int id);
        /// <summary>
        /// 判断是否此时间段已被预约
        /// </summary>
        /// <param name="bookTime"></param>
        /// <param name="useTime"></param>
        /// <returns></returns>
        [OperationContract]
        string CheckBookTime(DateTime bookTime, int useTime, string roomNo,int logID);
    }
}
