using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvertManage.IWCFService;
using System.ServiceModel;
using System.Data;

namespace AdvertManage.WCFService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public partial class AdvertManageService : IAdvertManageService
    {
        DAL.ProgramUpgradeDal programUpgradeDal = new DAL.ProgramUpgradeDal();
        /// <summary>
        /// 根据Id获取程序版本信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AdvertManage.Model.ProgramUpgradeModel GetProgramInfoById(int id)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" Id={0}", id);
            DataSet ds = programUpgradeDal.GetList(strWhere.ToString(), null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToProgramUpgradeModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据程序类型获取对应的程序信息

        /// </summary>
        /// <param name="programType"></param>
        /// <returns></returns>
        public AdvertManage.Model.ProgramUpgradeModel GetProgramInfoByProgramType(Model.Enum.SeatManageSubsystem programType)
        {
            StringBuilder strWhere = new StringBuilder();

            strWhere.AppendFormat(" Application={0}", (int)programType);

            DataSet ds = programUpgradeDal.GetList(strWhere.ToString(), null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToProgramUpgradeModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取所有发布的程序信息
        /// </summary>
        /// <returns></returns>
        public List<AdvertManage.Model.ProgramUpgradeModel> GetAllProgramInfo()
        {
            List<AdvertManage.Model.ProgramUpgradeModel> list= new List<Model.ProgramUpgradeModel>();
            DataSet ds = programUpgradeDal.GetList("", null);
            for (int i=0;i<ds.Tables[0].Rows.Count;i++)
            { 
                list.Add( DataRowToProgramUpgradeModel(ds.Tables[0].Rows[0])); 
            }
            return list;
        }

        /// <summary>
        /// 发布新版本程序
        /// </summary>
        /// <param name="programModel"></param>
        /// <param name="programType"></param>
        /// <returns></returns>
        public Model.Enum.HandleResult ReleaseProgram(AdvertManage.Model.ProgramUpgradeModel programModel)
        {
            try
            {
                AdvertManage.Model.ProgramUpgradeModel oldProgram = GetProgramInfoByProgramType(programModel.Application);

                if (oldProgram == null)
                {

                    programUpgradeDal.Add(programModel);

                }
                else
                {

                    if (oldProgram.Version == programModel.Version)
                    {
                        return Model.Enum.HandleResult.Failed;
                    }
                    else
                    {
                        programModel.Id = oldProgram.Id;
                        programUpgradeDal.Update(programModel);
                    }
                }
                return Model.Enum.HandleResult.Successed;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 私有方法
        AdvertManage.Model.ProgramUpgradeModel DataRowToProgramUpgradeModel(DataRow dr)
        {
            AdvertManage.Model.ProgramUpgradeModel model = new AdvertManage.Model.ProgramUpgradeModel();
            if (dr["Id"] != null && dr["Id"].ToString() != "")
            {
                model.Id = int.Parse(dr["Id"].ToString());
            }
            if (dr["Application"] != null && dr["Application"].ToString() != "")
            {
                model.Application = (Model.Enum.SeatManageSubsystem)int.Parse(dr["Application"].ToString());
            }
            if (dr["AutoUpdaterXml"] != null && dr["AutoUpdaterXml"].ToString() != "")
            {
                model.AutoUpdaterXml = dr["AutoUpdaterXml"].ToString();
            }
            if (dr["UpdateLog"] != null && dr["UpdateLog"].ToString() != "")
            {
                model.UpdateLog = dr["UpdateLog"].ToString();
            }
            if (dr["ReleaseDate"] != null && dr["ReleaseDate"].ToString() != "")
            {
                model.ReleaseDate = DateTime.Parse(dr["ReleaseDate"].ToString());
            }
            if (dr["Version"] != null && dr["Version"].ToString() != "")
            {
                model.Version = dr["Version"].ToString();
            }
            return model;
        }
        #endregion
    }
}
