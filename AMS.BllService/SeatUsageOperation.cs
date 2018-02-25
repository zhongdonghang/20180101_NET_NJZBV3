using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.IBllService;
using System.Data;

namespace AMS.BllService
{
    public partial class AdvertManageBllService : IAdvertManageBllService
    {
        DAL.SMS_SeatUsage seatUsage_dal = new DAL.SMS_SeatUsage();
        DAL.View_SeatUsage seatUsageView_dal = new DAL.View_SeatUsage();
        /// <summary>
        /// 添加使用记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddNewSeatUsage(Model.SMS_SeatUsage model)
        {
            try
            {
                AMS.Model.AMS_School school = GetSchoolInfoByNum(model.SchoolNum);
                if (school != null)
                {
                    model.SchoolID = school.Id;
                    return seatUsage_dal.Add(model) > 0 ? true : false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取最后的日期
        /// </summary>
        /// <param name="SchoolNum"></param>
        /// <returns></returns>
        public DateTime LastSeatUsageUploadDate(string SchoolNum)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                strWhere.Append(" SchoolNum='" + SchoolNum + "' order by UploadDate desc");
                DataSet ds = seatUsageView_dal.GetList(strWhere.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DateTime.Parse(ds.Tables[0].Rows[0]["UploadDate"].ToString());
                }
                else
                {
                    return DateTime.Parse("2000-1-1");
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取使用记录
        /// </summary>
        /// <param name="SchoolNum"></param>
        /// <param name="stratDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<Model.SMS_SeatUsage> GetSeatUsageList(string SchoolNum, DateTime stratDate, DateTime endDate)
        {
            try
            {
                List<Model.SMS_SeatUsage> modelList = new List<Model.SMS_SeatUsage>();
                StringBuilder strWhere = new StringBuilder();
                strWhere.Append(" SchoolNum='" + SchoolNum + "' and UploadDate>='" + stratDate.ToShortDateString() + "' and UploadDate<='" + endDate.ToShortDateString() + "'");
                DataSet ds = seatUsageView_dal.GetList(strWhere.ToString());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(DataRowToSMS_SeatUsage(ds.Tables[0].Rows[i]));
                }
                return modelList;
            }
            catch
            {
                throw;
            }
        }

        private AMS.Model.SMS_SeatUsage DataRowToSMS_SeatUsage(DataRow dr)
        {
            // [ID],[SchoolID],[UploadDate],[SeatUsageXml],[SchoolNum],[SchoolName]
            AMS.Model.SMS_SeatUsage model = new Model.SMS_SeatUsage();
            model = AMS.Model.SMS_SeatUsage.ToModel(dr["SeatUsageXml"].ToString());
            model.SeatUsageXml = dr["SeatUsageXml"].ToString();
            model.ID = int.Parse(dr["ID"].ToString());
            model.SchoolID = int.Parse(dr["SchoolID"].ToString());
            model.SchoolName = dr["SchoolName"].ToString();
            model.SchoolNum = dr["SchoolNum"].ToString();
            return model;
        }
    }
}
