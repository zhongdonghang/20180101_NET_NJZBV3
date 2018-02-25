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
        DAL.AMS_Campus campus = new DAL.AMS_Campus();

        public List<Model.AMS_Campus> GetCampusInfo(string schoolNo, bool fullInfo)
        {
            throw new NotImplementedException();
        }

        public Model.AMS_Campus GetSingleCampusInfo(string campusNo, bool fullInfo)
        {
            if (fullInfo)
            {
                return null;
            }
            else
            {
                StringBuilder strWhere = new StringBuilder();
                strWhere.AppendFormat(" Number='{0}'", campusNo);

                DataSet ds = campus.GetList(strWhere.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToAMS_CampusModel(ds.Tables[0].Rows[0]);
                }
                else
                {
                    return null;
                }
            }
        }

        public Model.Enum.HandleResult AddNewCampus(Model.AMS_Campus model)
        {
            throw new NotImplementedException();
        }

        public Model.Enum.HandleResult UpdateCampus(Model.AMS_Campus model)
        {
            throw new NotImplementedException();
        }

        public Model.Enum.HandleResult DeleteCampus(Model.AMS_Campus model)
        {
            throw new NotImplementedException();
        }

        #region 私有方法
        Model.AMS_Campus DataRowToAMS_CampusModel(DataRow dr)
        {
            Model.AMS_Campus model = new Model.AMS_Campus();

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
            return model;

        }
        #endregion
    }
}
