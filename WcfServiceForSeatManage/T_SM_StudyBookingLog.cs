using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.ClassModel;
using SeatManage.DAL;
using SeatManage.EnumType;
using System.Data;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {

        T_SM_StudyBookingLog t_sm_studybookinglog_DAL = new T_SM_StudyBookingLog();
        SeatManage.DAL.ViewStudyBookingLog studybookinglogView = new SeatManage.DAL.ViewStudyBookingLog();

        public bool AddStudyBookingLog(StudyBookingLog model)
        {
            try
            {
                return t_sm_studybookinglog_DAL.Add(model) > 0 ? true : false;
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateStudyBookingLog(StudyBookingLog model)
        {
            try
            {
                if (t_sm_studybookinglog_DAL.Update(model))
                {
                    if (!string.IsNullOrEmpty(model.Application.EmailAddress))
                    {
                        if (model.CheckState == CheckStatus.Adopt)
                        {
                            SeatManage.SeatManageComm.SendEmail.Send(model.Application.EmailAddress, "研习间审核结果通知", "恭喜您，您申请的" + model.StudyRoomName + "已通过审核，申请时间为" + model.BespeakTime + "，使用时长为" + model.UseTime + "分钟，请按时到场并与管理员联系，电话为870304。", false);
                        }
                        if (model.CheckState == CheckStatus.Failure)
                        {
                            SeatManage.SeatManageComm.SendEmail.Send(model.Application.EmailAddress, "研习间审核结果通知", "对不起，您申请的" + model.StudyRoomName + "尚未通过审核，原因为" + model.Remark + "，请重新申请或联系管理员，电话为870304。", false);
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteStudyBookingLog(StudyBookingLog model)
        {
            try
            {
                return t_sm_studybookinglog_DAL.Delete(model.StudyID);
            }
            catch
            {
                throw;
            }
        }

        public List<StudyBookingLog> GetStudyBookingLogList(string CardNo, List<string> roomNo, DateTime startDate, DateTime endDate, List<CheckStatus> status)
        {
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(CardNo))
            {
                strWhere.AppendFormat(" CardNo='{0}'", CardNo);
            }
            if (roomNo != null)
            {
                if (!string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(" and");
                }
                for (int i = 0; i < roomNo.Count; i++)
                {
                    if (i == 0)
                    {
                        strWhere.Append(string.Format(" StudyRoomNo in ('{0}'", roomNo[i]));
                    }
                    else if (i != roomNo.Count - 1)
                    {
                        strWhere.Append(string.Format(",'{0}'", roomNo[i]));
                    }
                    if (i == roomNo.Count - 1)
                    {
                        strWhere.Append(string.Format(",'{0}' ) ", roomNo[i]));
                    }
                }
            }
            if (startDate != null)
            {
                if (!string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(" and");
                }
                strWhere.AppendFormat(" SubmitTime>='{0}'", startDate.ToShortDateString());
            }
            if (endDate != null)
            {
                if (!string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(" and");
                }
                strWhere.AppendFormat(" SubmitTime<='{0}'", endDate.AddDays(1).Date.AddSeconds(-1).ToString());
            }
            if (status != null && status.Count > 0 && status[0] != CheckStatus.None)
            {
                if (!string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(" and");
                }
                for (int i = 0; i < status.Count; i++)
                {
                    if (i == 0)
                    {
                        strWhere.Append(string.Format(" CheckState in ('{0}'", (int)status[i]));
                    }
                    else if (i != status.Count - 1)
                    {
                        strWhere.Append(string.Format(",'{0}'", (int)status[i]));
                    }
                    if (i == status.Count - 1)
                    {
                        strWhere.Append(string.Format(",'{0}' ) ", (int)status[i]));
                    }
                }
            }
            strWhere.Append(" order by BespeakTime desc");
            List<StudyBookingLog> modelList = new List<StudyBookingLog>();
            try
            {
                DataSet ds = studybookinglogView.GetList(strWhere.ToString());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(DataRowToStudyBookingLog(ds.Tables[0].Rows[i]));
                }
                return modelList;
            }
            catch
            {
                throw;
            }

        }

        public StudyBookingLog GetStudyBookingLogByID(int id)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                strWhere.AppendFormat(" StudyID={0}", id);
                DataSet ds = studybookinglogView.GetList(strWhere.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToStudyBookingLog(ds.Tables[0].Rows[0]);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        public string CheckBookTime(DateTime bookTime, int useTime, string roomNo, int logID)
        {
            List<string> rooms = new List<string>();
            rooms.Add(roomNo);
            List<CheckStatus> status = new List<CheckStatus>();
            status.Add(CheckStatus.Adopt);
            status.Add(CheckStatus.Checking);
            try
            {
                List<StudyBookingLog> list = GetStudyBookingLogList(null, rooms, bookTime.Date, bookTime.AddDays(1).Date.AddSeconds(-1), status);
                if (list.Count > 0)
                {
                    foreach (StudyBookingLog item in list)
                    {
                        if ((item.BespeakTime <= bookTime && item.BespeakTime.AddMinutes(item.UseTime) >= bookTime || bookTime <= item.BespeakTime && bookTime.AddMinutes(useTime) >= item.BespeakTime) && logID != item.StudyID)
                        {
                            return item.BespeakTime.ToShortTimeString() + "-" + item.BespeakTime.AddMinutes(item.UseTime).ToShortTimeString() + "已被其他用户预约";
                        }
                    }
                }
                if (bookTime < DateTime.Now)
                {
                    return "预约时间不能小于当前时间";
                }
                StudyRoomInfo room = GetSingleStudyRoonInfo(roomNo);
                if (room != null)
                {
                    if (useTime > room.Setting.MaxBookTime)
                    {
                        return "使用时间不能超过" + room.Setting.MaxBookTime + "分钟";
                    }
                    if (DateTime.Parse(bookTime.ToShortTimeString()) < room.Setting.OpenTime)
                    {
                        return "预约时间不能小于研习间开放时间" + room.Setting.OpenTime.ToShortTimeString();
                    }
                    if (bookTime.AddMinutes(useTime) > DateTime.Parse(bookTime.Date.ToShortDateString() + " " + room.Setting.CloseTime.ToShortTimeString()))
                    {
                        return "使用时间不能超过研习间关闭时间" + room.Setting.CloseTime.ToShortTimeString();
                    }
                }
                return "";
            }
            catch
            {
                throw;
            }
        }
        private StudyBookingLog DataRowToStudyBookingLog(DataRow dr)
        {
            //[CardNo],[StudyID],[SubmitTime],[CheckTime],[BespeakTime],[UseTime],[CheckState],[ChecklPerson],[Remark],[StudyRoomNo],[StudyRoomName],[StudyRoomSetting],[ReaderName],[ApplicationTable]
            StudyBookingLog model = new StudyBookingLog();
            model.Application = new ApplicationTable(dr["ApplicationTable"].ToString());
            model.BespeakTime = DateTime.Parse(dr["BespeakTime"].ToString());
            model.CardNo = dr["CardNo"].ToString();
            model.ChecklPerson = dr["ChecklPerson"].ToString();
            model.CheckState = (SeatManage.EnumType.CheckStatus)int.Parse(dr["CheckState"].ToString());
            if (!string.IsNullOrEmpty(dr["CheckTime"].ToString()))
            {
                model.CheckTime = DateTime.Parse(dr["CheckTime"].ToString());
            }
            model.Remark = dr["Remark"].ToString();
            model.StudyID = int.Parse(dr["StudyID"].ToString());
            model.StudyRoomNo = dr["StudyRoomNo"].ToString();
            model.SubmitTime = DateTime.Parse(dr["SubmitTime"].ToString());
            model.UseTime = int.Parse(dr["UseTime"].ToString());
            model.ReaderName = dr["ReaderName"].ToString();
            model.StudyRoomName = dr["StudyRoomName"].ToString();
            model.RoomSetting = new StudyRoomSetting(dr["StudyRoomSetting"].ToString());
            return model;
        }





    }
}
