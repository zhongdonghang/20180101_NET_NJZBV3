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
        T_SM_StudyRoom t_sm_studyRoom_DAL = new T_SM_StudyRoom();
        /// <summary>
        /// 添加研习间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddNewStudyRoom(StudyRoomInfo model)
        {
            try
            {
                return t_sm_studyRoom_DAL.Add(model);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 更新研习间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateStudyRoom(StudyRoomInfo model)
        {

            try
            {
                return t_sm_studyRoom_DAL.Update(model);
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// 删除研习间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteStudyRoom(StudyRoomInfo model)
        {
            try
            {
                return t_sm_studyRoom_DAL.Delete(model.StudyRoomNo);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取单个研习间信息
        /// </summary>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        public StudyRoomInfo GetSingleStudyRoonInfo(string roomNo)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                strWhere.AppendFormat(" StudyRoomNo='{0}'", roomNo);
                DataSet ds = t_sm_studyRoom_DAL.GetList(strWhere.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToStudyRoomInfo(ds.Tables[0].Rows[0]);
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
        /// <summary>
        /// 获取研习间列表
        /// </summary>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        public List<StudyRoomInfo> GetStudyRoonInfoList(List<string> roomNo)
        {
            try
            {
                List<StudyRoomInfo> modelList = new List<StudyRoomInfo>();
                StringBuilder strWhere = new StringBuilder();
                if (roomNo != null)
                {
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
                DataSet ds = t_sm_studyRoom_DAL.GetList(strWhere.ToString());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(DataRowToStudyRoomInfo(ds.Tables[0].Rows[i]));
                }
                return modelList;
            }
            catch
            {
                throw;
            }
        }


        private StudyRoomInfo DataRowToStudyRoomInfo(DataRow dr)
        {
            StudyRoomInfo model = new StudyRoomInfo();
            if (dr["StudyRoomNo"] != null && dr["StudyRoomNo"].ToString() != "")
            {
                model.StudyRoomNo = dr["StudyRoomNo"].ToString();
            }
            if (dr["StudyRoomName"] != null && dr["StudyRoomName"].ToString() != "")
            {
                model.StudyRoomName = dr["StudyRoomName"].ToString();
            }
            if (dr["StudyRoomSetting"] != null && dr["StudyRoomSetting"].ToString() != "")
            {
                model.Setting = model.Setting = new StudyRoomSetting(dr["StudyRoomSetting"].ToString());
            }
            if (dr["Remark"] != null && dr["Remark"].ToString() != "")
            {
                model.Remark = dr["Remark"].ToString();
            }
            if (dr["RoomImage"] != null && dr["RoomImage"].ToString() != "")
            {
                model.RoomImage = dr["RoomImage"].ToString();
            }
            return model;
        }
    }
}
