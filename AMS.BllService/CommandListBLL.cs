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
        AMS.DAL.AMS_CommandList ComDal = new DAL.AMS_CommandList();
        AMS.DAL.View_CommandList VComDal = new DAL.View_CommandList();
        public List<Model.AMS_CommandList> GetCommandListBySchoolNum(string schoolNum)
        {
            StringBuilder strWhere = new StringBuilder();
            List<Model.AMS_CommandList> list = new List<Model.AMS_CommandList>();
            //添加条件
            if (!string.IsNullOrEmpty(schoolNum))
            {
                strWhere.AppendFormat("SchoolNum='{0}' and (FinishFlag={1} or FinishFlag={2} or FinishFlag={3}) ", schoolNum,(int)Model.Enum.CommandHandleResult.Wait, (int)Model.Enum.CommandHandleResult.Getting, (int)Model.Enum.CommandHandleResult.Failed);
                try
                {
                    DataSet ds = VComDal.GetList(strWhere.ToString());
                    //遍历查询结果，转换为Model，添加到List里面
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Model.AMS_CommandList model = DataRowToAMS_CammandListModel(ds.Tables[0].Rows[i]);
                        list.Add(model);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return list;
        }

        public Model.Enum.HandleResult UpdateFinishFlag(Model.AMS_CommandList model)
        {
            StringBuilder strWhere = new StringBuilder();

            if (model != null)
            {
                model.FinishTime = DateTime.Now;
                if (ComDal.Update(model))
                {
                    return Model.Enum.HandleResult.Successed;
                }
                else
                {
                    return Model.Enum.HandleResult.Failed;
                }

            }
            return Model.Enum.HandleResult.Failed;
        }

        public Model.Enum.HandleResult AddAMS_CommandList(Model.AMS_CommandList model)
        {
            try
            {
                if (ComDal.Add(model) > 0)
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

        public List<Model.AMS_CommandList> GetCommandListByCondition(int schoolId, Model.Enum.CommandType commandType, Model.Enum.CommandHandleResult handleResult)
        {
            StringBuilder strWhere = new StringBuilder();
            if (schoolId != -1)
            {
                strWhere.AppendFormat(" SchoolId={0} ", schoolId);
            }
            if (commandType != Model.Enum.CommandType.None)
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.AppendFormat(" Command={0}", (int)commandType);
                }
                else
                {
                    strWhere.AppendFormat(" and Command={0}", (int)commandType);
                }
            }
            if (handleResult != Model.Enum.CommandHandleResult.None)
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.AppendFormat(" FinishFlag={0}", (int)handleResult);
                }
                else
                {
                    strWhere.AppendFormat(" and FinishFlag={0}", (int)handleResult);
                }
            }
            List<Model.AMS_CommandList> listModel = new List<Model.AMS_CommandList>();
            DataSet ds = ComDal.GetList(strWhere.ToString());
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listModel.Add(DataRowToAMS_CammandListModel(ds.Tables[0].Rows[i]));
            }
            return listModel;
        }

        #region 私有方法
        private Model.AMS_CommandList DataRowToAMS_CammandListModel(DataRow dr)
        {
            Model.AMS_CommandList model = new Model.AMS_CommandList();

            if (dr["ID"] != null && dr["ID"].ToString() != "")
            {
                model.ID = int.Parse(dr["ID"].ToString());
            }
            if (dr["SchoolId"] != null && dr["SchoolId"].ToString() != "")
            {
                model.SchoolId = int.Parse(dr["SchoolId"].ToString());
            }
            if (dr["Command"] != null && dr["Command"].ToString() != "")
            {
                model.Command = (Model.Enum.CommandType)int.Parse(dr["Command"].ToString());
            }
            if (dr["CommandId"] != null && dr["CommandId"].ToString() != "")
            {
                model.CommandId = int.Parse(dr["CommandId"].ToString());
            }
            if (dr["ReleaseTime"] != null && dr["ReleaseTime"].ToString() != "")
            {
                model.ReleaseTime = DateTime.Parse(dr["ReleaseTime"].ToString());
            }
            if (dr["FinishTime"] != null && dr["FinishTime"].ToString() != "")
            {
                model.FinishTime = DateTime.Parse(dr["FinishTime"].ToString());
            }
            if (dr["FinishFlag"] != null && dr["FinishFlag"].ToString() != "")
            {
                model.FinishFlag = int.Parse(dr["FinishFlag"].ToString());

            }
            return model;
        }
        #endregion
    }
}
