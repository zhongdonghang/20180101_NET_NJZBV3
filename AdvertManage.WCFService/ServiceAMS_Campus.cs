using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvertManage.IWCFService;
using System.Data;

namespace AdvertManage.WCFService
{
    public partial class AdvertManageService : IAdvertManageService
    {
        DAL.AMS_Campus campus = new DAL.AMS_Campus();
        /// <summary>
        /// 根据校区编号获取校区信息
        /// </summary>
        /// <param name="campusNum"></param>
        /// <returns></returns>
        public AdvertManage.Model.AMS_CampusModel GetCampusInfoByNum(string campusNum)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" Number='{0}'", campusNum);

            DataSet ds = campus.GetList(strWhere.ToString(), null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToAMS_CampusModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据Id获取校区信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.AMS_CampusModel GetCampusInfoByID(int id)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" Id='{0}'", id);

            DataSet ds = campus.GetList(strWhere.ToString(), null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToAMS_CampusModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        public List<Model.AMS_CampusModel> GetCampusInfoListBySchoolNum(string number)
        {
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(number))
            {
                strWhere.AppendFormat(" SchoolNum='{0}'", number);
            }
            List<Model.AMS_CampusModel> list = new List<Model.AMS_CampusModel>();
            DataSet ds = campus.GetList(strWhere.ToString(), null);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(DataRowToAMS_CampusModel(ds.Tables[0].Rows[i]));

            }
            return list;
        }
        /// <summary>
        /// 根据学校Id获取校区列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Model.AMS_CampusModel> GetCampusInfoListBySchoolId(int id)
        {
            StringBuilder strWhere = new StringBuilder();
            if (id > 0)
            {
                strWhere.AppendFormat(" SchoolId='{0}'", id);
            }

            List<Model.AMS_CampusModel> list = new List<Model.AMS_CampusModel>();
            DataSet ds = campus.GetList(strWhere.ToString(), null);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(DataRowToAMS_CampusModel(ds.Tables[0].Rows[i]));

            }
            return list;
        }
        /// <summary>
        /// 新增校区信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Model.Enum.HandleResult AddCampus(Model.AMS_CampusModel model)
        {
            if (campus.Add(model))
            {
                return Model.Enum.HandleResult.Successed;
            }
            else
            {
                return Model.Enum.HandleResult.Failed;
            }
        }
        /// <summary>
        /// 删除校区
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.Enum.HandleResult DeleteCampus(int id)
        {
            if (campus.Delete(id))
            {
                return Model.Enum.HandleResult.Successed;
            }
            else
            {
                return Model.Enum.HandleResult.Failed;
            }
        }
        /// <summary>
        /// 更新校区
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Model.Enum.HandleResult UpdateCampus(Model.AMS_CampusModel model)
        {
            if (campus.Update(model))
            {
                return Model.Enum.HandleResult.Successed;
            }
            else
            {
                return Model.Enum.HandleResult.Failed;
            }
        }
        #region 私有方法
        AdvertManage.Model.AMS_CampusModel DataRowToAMS_CampusModel(DataRow dr)
        {
            AdvertManage.Model.AMS_CampusModel model = new AdvertManage.Model.AMS_CampusModel();

            if (dr["Id"] != null && dr["Id"].ToString() != "")
            {
                model.Id = int.Parse(dr["Id"].ToString());
            }
            if (dr["Number"] != null && dr["Number"].ToString() != "")
            {
                model.Number = dr["Number"].ToString();
            }
            if (dr["SchoolId"] != null && dr["SchoolId"].ToString() != "")
            {
                model.SchoolId = int.Parse(dr["SchoolId"].ToString());
            }
            if (dr["Name"] != null && dr["Name"].ToString() != "")
            {
                model.Name = dr["Name"].ToString();
            }
            if (dr["Describe"] != null && dr["Describe"].ToString() != "")
            {
                model.Describe = dr["Describe"].ToString();
            }
            if (dr["SchoolNum"] != null && dr["SchoolNum"].ToString() != "")
            {
                model.SchoolNum = dr["SchoolNum"].ToString();
            }
            if (dr["SchoolName"] != null && dr["SchoolName"].ToString() != "")
            {
                model.SchoolName = dr["SchoolName"].ToString();
            }
            if (dr["SchoolDTUIp"] != null && dr["SchoolDTUIp"].ToString() != "")
            {
                model.SchoolDTUIp = dr["SchoolDTUIp"].ToString();
            }
            if (dr["SchoolDescribe"] != null && dr["SchoolDescribe"].ToString() != "")
            {
                model.SchoolDescribe = dr["SchoolDescribe"].ToString();
            }
            if (dr["SchoolConnectionString"] != null && dr["SchoolConnectionString"].ToString() != "")
            {
                model.SchoolConnectionString = dr["SchoolConnectionString"].ToString();
            }
            return model;

        }
        #endregion



    }
}
