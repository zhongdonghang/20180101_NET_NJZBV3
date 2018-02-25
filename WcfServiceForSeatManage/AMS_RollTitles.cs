using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.DAL;
using System.Data;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        SeatManage.DAL.AMS_RollTitles ams_RollTitles = new SeatManage.DAL.AMS_RollTitles();
        public SeatManage.EnumType.HandleResult AddRollTitles(SeatManage.ClassModel.RollTitlesInfo model)
        {
            try
            {
                if (ams_RollTitles.AddRollTitles(model))
                {
                    return SeatManage.EnumType.HandleResult.Successed;
                }
                else
                {
                    return SeatManage.EnumType.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string GetModelStr()
        {
            try
            {
                string Type = "";
                string sql = string.Format("  datediff(day,EffectDate,'{0}')>=0 and datediff(day,EndDate,'{0}')<=0 order by EffectDate desc", DateTime.Now);
                DataSet ds = ams_RollTitles.GetList(sql);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Type += "   " + ds.Tables[0].Rows[i]["Type"];
                }
                return Type;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public SeatManage.ClassModel.RollTitlesInfo GetModelByNum(string Num)
        {
            try
            {
                string sql = string.Format(" Num ='{0}'", Num);
                DataSet ds = ams_RollTitles.GetList(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToAMS_RollTitles(ds.Tables[0].Rows[0]);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public SeatManage.EnumType.HandleResult UpdateRollTitles(SeatManage.ClassModel.RollTitlesInfo model)
        {
            try
            {
                if (ams_RollTitles.UpdateRollTitles(model))
                {
                    return SeatManage.EnumType.HandleResult.Successed;
                }
                else
                {
                    return SeatManage.EnumType.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private SeatManage.ClassModel.RollTitlesInfo DataRowToAMS_RollTitles(DataRow dr)
        {
            SeatManage.ClassModel.RollTitlesInfo model = new SeatManage.ClassModel.RollTitlesInfo
            {
                EffectDate = Convert.ToDateTime(dr["EffectDate"].ToString()),
                EndDate = Convert.ToDateTime(dr["EndDate"].ToString()),
                Type = dr["Type"].ToString(),
                Num = dr["Num"].ToString()
            };
            return model;
        }


        public List<SeatManage.ClassModel.RollTitlesInfo> GetOverTimeRollTitleList()
        {
            try
            {
                List<SeatManage.ClassModel.RollTitlesInfo> list = new List<SeatManage.ClassModel.RollTitlesInfo>();
                string sql = string.Format("  datediff(day,EndDate,'{0}')>0 order by EffectDate desc", DateTime.Now);
                DataSet ds = ams_RollTitles.GetList(sql);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    list.Add(DataRowToAMS_RollTitles(ds.Tables[0].Rows[i]));
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SeatManage.EnumType.HandleResult DeleteRollTitle(string Num)
        {
            try
            {
                if (ams_RollTitles.DeleteRollTitles(Num))
                {
                    return SeatManage.EnumType.HandleResult.Successed;
                }
                else
                {
                    return SeatManage.EnumType.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
