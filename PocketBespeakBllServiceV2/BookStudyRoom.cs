using System;
using System.Collections.Generic;
using SeatManage.ClassModel;
using SeatManage.EnumType;

namespace SeatManage.PocketBespeakBllServiceV2
{
    public partial class PocketBespeakBllService : IPocketBespeakBllServiceV2.IPocketBespeakBllService
    {
        /// <summary>
        /// 获取研习间列表
        /// </summary>
        /// <returns></returns>
        public List<StudyRoomInfo> GetStudyRoomList()
        {
            try
            {
                return seatManage.GetStudyRoonInfoList(null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取单个研习间信息
        /// </summary>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        public StudyRoomInfo GetStudyRoomInfo(string roomNo)
        {
            try
            {
                return seatManage.GetSingleStudyRoonInfo(roomNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 提交申请记录
        /// </summary>
        /// <param name="logModel"></param>
        /// <returns></returns>
        public string SubmitStudyLog(StudyBookingLog logModel)
        {
            try
            {
                if (logModel == null)
                {
                    return "提交申请失败";
                }
                string errorMessage = seatManage.CheckBookTime(logModel.BespeakTime, logModel.UseTime, logModel.StudyRoomNo,logModel.StudyID);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    return errorMessage;
                }
                errorMessage = CheckApplication(logModel.Application);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    return errorMessage;
                }
                logModel.CheckState = CheckStatus.Checking;
                if (logModel.StudyID < 1)
                {
                    if (seatManage.AddStudyBookingLog(logModel))
                    {
                        return "";
                    }
                    else
                    {
                        return "提交申请失败";
                    }
                }
                else
                {
                    if (seatManage.UpdateStudyBookingLog(logModel))
                    {
                        return "";
                    }
                    else
                    {
                        return "提交申请失败";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取记录列表
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="spanDay"></param>
        /// <returns></returns>
        public List<StudyBookingLog> GetStudyLogList(string cardNo, int spanDay)
        {
            try
            {
                return seatManage.GetStudyBookingLogList(cardNo, null, DateTime.Now.AddDays(-spanDay), DateTime.Now, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取单个记录
        /// </summary>
        /// <param name="logID"></param>
        /// <returns></returns>
        public StudyBookingLog GetStudyLog(int logID)
        {
            try
            {
                return seatManage.GetStudyBookingLogByID(logID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 更新记录状态
        /// </summary>
        /// <param name="logModel"></param>
        /// <returns></returns>
        public string CancelStudyLog(StudyBookingLog logModel)
        {
            try
            {
                if (logModel == null)
                {
                    return "修改申请失败";
                }
                logModel.CheckState = CheckStatus.Cancel;
                if (seatManage.UpdateStudyBookingLog(logModel))
                {
                    return "";
                }
                else
                {
                    return "提交申请失败";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 验证申请
        /// </summary>
        /// <param name="appTable"></param>
        /// <returns></returns>
        private string CheckApplication(ApplicationTable appTable)
        {
            if (string.IsNullOrEmpty(appTable.ApplicantCardNo))
            {
                return "申请人证件号不能为空";
            }
            if (string.IsNullOrEmpty(appTable.ApplicantDept))
            {
                return "申请人单位不能为空";
            }
            if (string.IsNullOrEmpty(appTable.ApplicantName))
            {
                return "申请人姓名不能为空";
            }
            if (string.IsNullOrEmpty(appTable.ApplicantPhoneNum))
            {
                return "申请人电话不能为空";
            }
            if (string.IsNullOrEmpty(appTable.ApplicantType))
            {
                return "申请人类别不能为空";
            }
            if (string.IsNullOrEmpty(appTable.HeadPerson))
            {
                return "负责人姓名不能为空";
            }
            if (string.IsNullOrEmpty(appTable.HeadPersonPhoneNum))
            {
                return "负责人电话不能为空";
            }
            if (string.IsNullOrEmpty(appTable.HeadPersonType))
            {
                return "负责人类别不能为空";
            }
            if (string.IsNullOrEmpty(appTable.MeetingName))
            {
                return "会议名称不能为空";
            }
            if (appTable.MembersCount < 1)
            {
                return "参与人数要大于0";
            }
            return "";
        }
    }
}
