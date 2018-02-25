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
        DAL.AMS_IssureList dal_issurelist = new DAL.AMS_IssureList();
        DAL.View_IssureList dal_issureView = new DAL.View_IssureList();
        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="schoolIDList"></param>
        /// <returns></returns>
        public bool AddCommand(AMS.Model.AMS_IssureList command, List<int> schoolIDList)
        {
            try
            {
                command.SubmitTime = DateTime.Now;
                if (command.CommandType == Model.Enum.IsureCommandType.Advertisement)
                {
                    AMS.Model.AMS_Advertisement adModel = GetSingleAdvertisement(command.CommandID);
                    {
                        if (adModel != null)
                        {
                            foreach (int school in schoolIDList)
                            {
                                AMS.Model.AMS_AdvertisementSchoolCopy copy = new Model.AMS_AdvertisementSchoolCopy();
                                copy.AdContent = adModel.AdContent;
                                copy.CustomerID = adModel.CustomerID;
                                copy.EffectDate = adModel.EffectDate;
                                copy.EndDate = adModel.EndDate;
                                copy.Name = adModel.Name;
                                copy.Num = adModel.Num;
                                copy.OperatorID = adModel.OperatorID;
                                copy.OriginalID = adModel.ID;
                                copy.SchoolID = school;
                                copy.Type = adModel.Type;
                                AMS.Model.AMS_AdvertisementSchoolCopy sameCopy = GetSameSchoolAdvert(school, adModel.ID);
                                int copyID = 0;
                                if (sameCopy != null)
                                {
                                    copy.ID = sameCopy.ID;
                                    copyID = sameCopy.ID;
                                    if (!dal_advertisementcopy.Update(copy))
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    copyID = dal_advertisementcopy.Add(copy);
                                }
                                if (copyID < 1)
                                {
                                    return false;
                                }
                                else
                                {
                                    command.SchoolID = school;
                                    command.CommandID = copyID;
                                    if (dal_issurelist.Add(command) < 1)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取命令状态
        /// </summary>
        /// <param name="command"></param>
        /// <param name="schoolID"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public List<Model.AMS_IssureList> GetCommandState(Model.Enum.IsureCommandType command, int schoolID, Model.Enum.CommandHandleResult state)
        {
            try
            {
                List<Model.AMS_IssureList> modelList = new List<Model.AMS_IssureList>();
                StringBuilder strWhere = new StringBuilder();
                if (command != Model.Enum.IsureCommandType.None)
                {
                    strWhere.Append(" CommandType=" + (int)command + " ");
                }
                if (schoolID > 0)
                {
                    if (!string.IsNullOrEmpty(strWhere.ToString()))
                    {
                        strWhere.Append(" and ");
                    }
                    strWhere.Append("SchoolID=" + schoolID + " ");
                }
                if (state != Model.Enum.CommandHandleResult.None)
                {
                    if (!string.IsNullOrEmpty(strWhere.ToString()))
                    {
                        strWhere.Append(" and ");
                    }
                    strWhere.Append(" Flag=" + (int)state + " ");
                }
                DataSet ds = dal_issureView.GetList(strWhere.ToString());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(DataRowToIssureList(ds.Tables[0].Rows[i]));
                }
                return modelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取学校命令
        /// </summary>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public List<Model.AMS_IssureList> GetSchoolCommand(string schoolNo)
        {
            try
            {
                List<Model.AMS_IssureList> modelList = new List<Model.AMS_IssureList>();
                StringBuilder strWhere = new StringBuilder();
                strWhere.Append(" Number='" + schoolNo + "' and Flag!=" + (int)AMS.Model.Enum.CommandHandleResult.Success);
                DataSet ds = dal_issureView.GetList(strWhere.ToString());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(DataRowToIssureList(ds.Tables[0].Rows[i]));
                }
                return modelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 删除命令
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteCommand(Model.AMS_IssureList model)
        {
            try
            {
                return dal_issurelist.Delete(model.ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 修改命令
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateCommand(Model.AMS_IssureList model)
        {
            try
            {
                switch (model.Flag)
                {
                    case 1:
                    case 3:
                        model.GetTime = DateTime.Now;
                        break;
                    case 2:
                        model.CompleteTime = DateTime.Now;
                        break;
                }
                return dal_issurelist.Update(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private Model.AMS_IssureList DataRowToIssureList(DataRow dr)
        {
            //[SchoolID],[OperatorID],[UserName],[Name],[Number],[ID],[CommandType],[CommandID],[SubmitTime],[GetTime],[CompleteTime],[Flag],[Num],[AdName],[Type]
            Model.AMS_IssureList model = new Model.AMS_IssureList();
            model.AdvertType = (AMS.Model.Enum.AdType)int.Parse(dr["Type"].ToString());
            model.CommandID = int.Parse(dr["CommandID"].ToString());
            model.CommandType = (AMS.Model.Enum.IsureCommandType)int.Parse(dr["CommandType"].ToString());
            model.CompleteTime = DateTime.Parse(dr["CompleteTime"].ToString());
            model.Flag = int.Parse(dr["Flag"].ToString());
            model.GetTime = DateTime.Parse(dr["GetTime"].ToString());
            model.ID = int.Parse(dr["ID"].ToString());
            model.OperatorID = int.Parse(dr["OperatorID"].ToString());
            model.SchoolID = int.Parse(dr["SchoolID"].ToString());
            model.SchoolName = dr["Name"].ToString();
            model.SubmitTime = DateTime.Parse(dr["SubmitTime"].ToString());
            model.AdInfo = dr["Num"].ToString() + " " + dr["AdName"].ToString();
            model.OperatorName = dr["UserName"].ToString();
            return model;
        }



    }
}
