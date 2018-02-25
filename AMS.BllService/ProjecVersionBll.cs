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
        AMS.DAL.ProgramUpgrade dal_ProgramUpgrade = new DAL.ProgramUpgrade();
        public List<Model.ProgramUpgrade> GetProgramUpgradeList()
        {
            try
            {
                List<Model.ProgramUpgrade> modellist = new List<Model.ProgramUpgrade>();
                DataSet ds = dal_ProgramUpgrade.GetList(null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modellist.Add(DataRowToAMS_ProgramUpgradeListModel(ds.Tables[0].Rows[i]));
                }
                return modellist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Model.ProgramUpgrade GetProgramInfoByProgramType(Model.Enum.SeatManageSubsystem programType)
        {
            throw new NotImplementedException();
        }

        public string AddNewProgramUpgrade(Model.ProgramUpgrade model)
        {
            try
            {
                AMS.Model.ProgramUpgrade sameModel = dal_ProgramUpgrade.GetModel(model.Version, model.Application);
                if (sameModel != null)
                {
                    return "该程序已存在相同的版本！";
                }
                if (dal_ProgramUpgrade.Add(model) == 0)
                {
                    return "添加失败！";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string UpdateProgramUpgrade(Model.ProgramUpgrade model)
        {
            throw new NotImplementedException();
        }
        public string DeleteProgramUpgrade(Model.ProgramUpgrade model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 转换ProgramUpgradeList的model
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private AMS.Model.ProgramUpgrade DataRowToAMS_ProgramUpgradeListModel(DataRow dr)
        {
            AMS.Model.ProgramUpgrade model = new Model.ProgramUpgrade();
            model.Id = int.Parse(dr["Id"].ToString());
            model.Version = dr["Version"].ToString();
            model.ReleaseDate = DateTime.Parse(dr["ReleaseDate"].ToString());
            model.UpdateLog = dr["UpdateLog"].ToString();
            model.Remark = dr["Remark"].ToString();
            model.Application = int.Parse(dr["Application"].ToString());
            model.AutoUpdaterXml = dr["AutoUpdaterXml"].ToString();
            return model;
        }


        public Model.ProgramUpgrade GetProgramUpgradeByID(int id)
        {
            try
            {
                Model.ProgramUpgrade modellist = new Model.ProgramUpgrade();
                modellist = dal_ProgramUpgrade.GetModelByID(id);
                return modellist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
