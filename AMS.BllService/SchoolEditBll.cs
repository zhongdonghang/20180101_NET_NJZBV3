using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.IBllService;
using System.Data;
using AMS.Model;

namespace AMS.BllService
{
    public partial class AdvertManageBllService : IAdvertManageBllService
    {
        public List<AMS.Model.AMS_School> GetSchoolInfoList()
        {
            try
            {
                List<AMS.Model.AMS_School> modellist = new List<Model.AMS_School>();
                DataSet schoolDs = dal_School.GetList(null);
                for (int i = 0; i < schoolDs.Tables[0].Rows.Count; i++)
                {
                    modellist.Add(DataRowToSchoolModel(schoolDs.Tables[0].Rows[i]));
                }
                return modellist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public AMS_School GetSchoolInfoById(int SchoolID)
        {
            try
            {
                AMS.Model.AMS_School modellist = new Model.AMS_School();
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("ID={0}",SchoolID);
                DataSet schoolDs = dal_School.GetList(sql.ToString());
                for (int i = 0; i < schoolDs.Tables[0].Rows.Count; i++)
                {
                    modellist=DataRowToSchoolModel(schoolDs.Tables[0].Rows[i]);
                }
                return modellist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         

        public AMS_School GetSchoolInfoByNum(string schoolNum)
        {
            AMS.Model.AMS_School modellist = new Model.AMS_School();
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("Number='{0}'", schoolNum);
            DataSet schoolDs = dal_School.GetList(sql.ToString());
            for (int i = 0; i < schoolDs.Tables[0].Rows.Count; i++)
            {
                modellist = DataRowToSchoolModel(schoolDs.Tables[0].Rows[i]);
            }
            return modellist;
        }
    }
}
