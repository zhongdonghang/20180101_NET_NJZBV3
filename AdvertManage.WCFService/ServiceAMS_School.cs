using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvertManage.IWCFService;
using System.Data;
using AdvertManage.DAL;

namespace AdvertManage.WCFService
{
    public partial class AdvertManageService : IAdvertManageService
    {
        AMS_SchoolDal schoolDal = new AMS_SchoolDal();
        /// <summary>
        /// 根据学校编号获取学校信息
        /// </summary>
        /// <param name="schoolNum"></param>
        /// <returns></returns>
        public Model.AMS_SchoolModel GetSchoolInfoByNum(string schoolNum)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" Number='{0}'", schoolNum);
            DataSet ds = schoolDal.GetList(strWhere.ToString(), null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToAms_SchoolModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        public Model.AMS_SchoolModel GetSchoolInfoByID(int id)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" Id='{0}'", id);
            DataSet ds = schoolDal.GetList(strWhere.ToString(), null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToAms_SchoolModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }



        public List<Model.AMS_SchoolModel> GetAllSchoolInfo()
        {
            List<Model.AMS_SchoolModel> modelList = new List<Model.AMS_SchoolModel>();
            DataSet ds = schoolDal.GetList(null, null);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                modelList.Add(DataRowToAms_SchoolModel(ds.Tables[0].Rows[i]));
            }
            return modelList;
        }
        /// <summary>
        /// 根据Id删除学校信息
        /// </summary>
        /// <returns></returns> 
        public Model.Enum.HandleResult DeleteSchoolInfo(int Id)
        {
            try
            {
                if (schoolDal.Delete(Id))
                {
                    return Model.Enum.HandleResult.Successed;
                }
                else
                {
                    return Model.Enum.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加学校信息
        /// </summary>
        /// <param name="model"> 学校信息</param>
        /// <returns></returns> 
        public Model.Enum.HandleResult AddSchoolInfo(Model.AMS_SchoolModel model)
        {
            try
            {
                if (schoolDal.Add(model)>0)
                {
                    return Model.Enum.HandleResult.Successed;
                }
                else
                {
                    return Model.Enum.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更新学校信息
        /// </summary>
        /// <param name="model">学校 </param>
        /// <returns></returns> 
        public Model.Enum.HandleResult UpdateSchoolInfo(Model.AMS_SchoolModel model)
        {
            try
            {
                if (schoolDal.Update(model))
                {
                    return Model.Enum.HandleResult.Successed;
                }
                else
                {
                    return Model.Enum.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 私有方法
        AdvertManage.Model.AMS_SchoolModel DataRowToAms_SchoolModel(DataRow dr)
        {
            AdvertManage.Model.AMS_SchoolModel model = new AdvertManage.Model.AMS_SchoolModel();

            if (dr["Id"] != null && dr["Id"].ToString() != "")
            {
                model.Id = int.Parse(dr["Id"].ToString());
            }
            if (dr["Number"] != null && dr["Number"].ToString() != "")
            {
                model.Number = dr["Number"].ToString();
            }
            if (dr["Name"] != null && dr["Name"].ToString() != "")
            {
                model.Name = dr["Name"].ToString();
            }
            if (dr["DTUip"] != null && dr["DTUip"].ToString() != "")
            {
                model.DTUip = dr["DTUip"].ToString();
            }
            if (dr["Describe"] != null && dr["Describe"].ToString() != "")
            {
                model.Describe = dr["Describe"].ToString();
            }
            if (dr["ConnectionString"] != null && dr["ConnectionString"].ToString() != "")
            {
                model.ConnectionString = dr["ConnectionString"].ToString();
            }
            if (dr["Flag"] != null && dr["Flag"].ToString() != "")
            {
                model.Flag = int.Parse(dr["Flag"].ToString());
            }
            return model;

        }
        #endregion



    }
}
