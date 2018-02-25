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
        AMS.DAL.AMS_CallBackErrorInfo Dal_CallBackErrorInfo = new DAL.AMS_CallBackErrorInfo();
        AMS.DAL.View_CallBackErrorInfo Dal_CallBackErrorInfoView = new DAL.View_CallBackErrorInfo();
        /// <summary>
        /// 获取反馈信息
        /// </summary>
        /// <returns></returns>
        public List<Model.AMS_CallBackErrorInfo> GetCallBackInfo()
        {
            try
            {
                List<AMS.Model.AMS_CallBackErrorInfo> modellist = new List<Model.AMS_CallBackErrorInfo>();
                DataSet ds = Dal_CallBackErrorInfoView.GetList(null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modellist.Add(DataRowToCallBackErrorInfo(ds.Tables[0].Rows[i]));
                }
                return modellist;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 根据问题解决状态获得反馈信息
        /// </summary>
        /// <param name="SolveStatic"></param>
        /// <returns></returns>
        public List<Model.AMS_CallBackErrorInfo> GetCallBackInfoBySolveStatic(string SolveStatic)
        {
            try
            {
                List<AMS.Model.AMS_CallBackErrorInfo> modellist = new List<Model.AMS_CallBackErrorInfo>();
                DataSet ds = Dal_CallBackErrorInfo.GetList(null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (SolveStatic.Equals("未解决"))
                    {
                        modellist.Add(DataRowToCallBackErrorInfo(ds.Tables[0].Rows[i]));
                    }
                }
                return modellist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddNewCallBackInfo(Model.AMS_CallBackErrorInfo model)
        {
            try
            {
                if (Dal_CallBackErrorInfo.Add(model) == 0)
                {
                    return "添加反馈信息失败!";
                }
                return null;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string UpdateCallBackInfo(Model.AMS_CallBackErrorInfo model)
        {
            try
            {
                if (!Dal_CallBackErrorInfo.Update(model))
                {
                    return "修改反馈信息失败！";
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string DeleteCallBackInfo(Model.AMS_CallBackErrorInfo model)
        {
            try
            {
                if (!Dal_CallBackErrorInfo.Delete(model.ID))
                {
                    return "删除反馈记录失败！";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private AMS.Model.AMS_CallBackErrorInfo DataRowToCallBackErrorInfo(DataRow dr)
        {
            //SchoolNum,SchoolName,SchoolDTUip,SchoolDescribe,SchoolCon,SchoolLinkMan
            //SchoolLinkAddress,SchoolCardInfo,SchoolInterfaceInfo,FbPerson,FbTime,SolveTime
            //SolveWay,ProblemType,FbDescribe,ID,SolveLoginId,SolveUserPwd,SolveRemark,SolveUserName
            //sf    SolveBranchName,MarkBrandName,MarkUserPwd,MarkLoginId,MarkRemark,MarkUserName,Status
            //SolveManID,MarkManID 
            AMS.Model.AMS_CallBackErrorInfo model = new Model.AMS_CallBackErrorInfo();
            model.ID = int.Parse(dr["ID"].ToString());
            model.FbPerson = dr["FbPerson"].ToString();
            model.FbDescribe = dr["FbDescribe"].ToString();
            if (!string.IsNullOrEmpty(dr["FbTime"].ToString()))
            {
                model.FbTime = Convert.ToDateTime(dr["FbTime"]);
            }
            if (!string.IsNullOrEmpty(dr["SolveTime"].ToString()))
            {
                model.SolveTime = Convert.ToDateTime(dr["SolveTime"]);
            }
            model.Markman = dr["MarkUserName"].ToString();
            model.Solveman = dr["SolveUserName"].ToString();
            model.ProblemType = int.Parse(dr["ProblemType"].ToString());
            model.Schoolname = dr["Schoolname"].ToString();
            model.Solvestatic = int.Parse(dr["Status"].ToString());
            model.SolveWay = dr["SolveWay"].ToString();
            if (!string.IsNullOrEmpty(dr["SolveManID"].ToString()))
            {
                model.SolveManID = int.Parse(dr["SolveManID"].ToString());
            }
            if (!string.IsNullOrEmpty(dr["MarkManID"].ToString()))
            {
                model.MarkManID = int.Parse(dr["MarkManID"].ToString());
            }
            model.SchoolId = int.Parse(dr["SchoolId"].ToString());
            return model;
        }
    }
}
