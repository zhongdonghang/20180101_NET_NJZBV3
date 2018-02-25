using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.DAL;
using System.Data;
using SeatManage.ClassModel;
using SeatManage.EnumType;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        SeatManage.DAL.T_SM_School school_dal = new T_SM_School();
        /// <summary>
        /// 新增校区
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddSchoolInfo(SeatManage.ClassModel.School model)
        {
            try
            {
                return school_dal.Add(model);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 修改校区
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateSchool(SeatManage.ClassModel.School model)
        {
            try
            {
                return school_dal.Update(model);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 删除校区
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteSchool(SeatManage.ClassModel.School model)
        {
            try
            {
                return school_dal.Delete(model);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取校区列表
        /// </summary>
        /// <returns></returns>
        public List<SeatManage.ClassModel.School> GetSchoolList(string no, string name)
        {
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(no))
            {
                strWhere.Append(" SchoolNo='" + no + "'");
            }
            if (!string.IsNullOrEmpty(name))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(" SchoolName='" + name + "'");
                }
                else
                {
                    strWhere.Append(" and SchoolName='" + name + "'");
                }
            }
            try
            {
                DataSet ds = school_dal.GetList(strWhere.ToString(), null);
                List<School> list = new List<School>();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(DataRowToSchoolModel(dr));
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        private School DataRowToSchoolModel(DataRow dr)
        {
            School school = new School();
            school.No = dr["SchoolNo"].ToString();
            school.Name = dr["SchoolName"].ToString();
            return school;
        }
    }
}
