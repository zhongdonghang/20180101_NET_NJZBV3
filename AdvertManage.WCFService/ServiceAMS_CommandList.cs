using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvertManage.IWCFService;
using System.Data;
using System.Data.SqlClient;

namespace AdvertManage.WCFService
{
    public partial class AdvertManageService : IAdvertManageService
    {
        AdvertManage.DAL.AMS_CommandListDal commandDal = new DAL.AMS_CommandListDal();
        /// <summary>
        /// 根据学校编号获取有效的命令列表
        /// </summary>
        /// <returns></returns> 
        public List<AdvertManage.Model.AMS_CommandListModel> GetCommandListBySchoolNum(string schoolNum)
        {
            StringBuilder strWhere = new StringBuilder();
            List<AdvertManage.Model.AMS_CommandListModel> list = new List<Model.AMS_CommandListModel>();
            //添加条件
            if (!string.IsNullOrEmpty(schoolNum))
            {
                strWhere.AppendFormat("SchoolNum=@SchoolNum and (FinishFlag={0} or FinishFlag={1} or FinishFlag={2}) ", (int)Model.Enum.CommandHandleResult.Wait, (int)Model.Enum.CommandHandleResult.Getting, (int)Model.Enum.CommandHandleResult.Failed);
                SqlParameter[] parameters = {
                                            new SqlParameter("@SchoolNum",SqlDbType.NVarChar)
                                           };
                parameters[0].Value = schoolNum;
                try
                {
                    DataSet ds = commandDal.GetList(strWhere.ToString(), parameters);
                    //遍历查询结果，转换为Model，添加到List里面
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        AdvertManage.Model.AMS_CommandListModel model = DataRowToAMS_CammandListModel(ds.Tables[0].Rows[i]);
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
        /// <summary>
        /// 数据中转服务一条命令处理完成后，更新这条命令的状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        public AdvertManage.Model.Enum.HandleResult UpdateFinishFlag(AdvertManage.Model.AMS_CommandListModel model)
        {
            StringBuilder strWhere = new StringBuilder();

            if (model != null)
            {
                model.FinishTime = GetServerDateTime();
                if (commandDal.Update(model))
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
        /// <summary>
        /// 下发的时候添加一条命令
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public AdvertManage.Model.Enum.HandleResult AddAMS_CommandList(AdvertManage.Model.AMS_CommandListModel model)
        {
            try
            {
                if (commandDal.Add(model) > 0)
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
        /// 根据条件获取命令
        /// </summary>
        /// <param name="schoolId"></param>
        /// <param name="commandType"></param>
        /// <param name="handleResult"></param>
        /// <returns></returns>
        public List<AdvertManage.Model.AMS_CommandListModel> GetCommandListByCondition(int schoolId, Model.Enum.CommandType commandType, Model.Enum.CommandHandleResult handleResult)
        {
            StringBuilder strWhere = new StringBuilder();
            if (schoolId != -1)
            {
                strWhere.Append(" SchoolId=@SchoolId ");
            }
            if (commandType != Model.Enum.CommandType.None)
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(" Command=@Command");
                }
                else
                {
                    strWhere.Append(" and Command=@Command");
                }
            }
            if (handleResult != Model.Enum.CommandHandleResult.None)
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(" FinishFlag=@FinishFlag");
                }
                else
                {
                    strWhere.Append(" and FinishFlag=@FinishFlag");
                }
            }

            SqlParameter[] parameters = {
                                          new SqlParameter("@SchoolId",schoolId),
                                          new SqlParameter("@Command",commandType),
                                          new SqlParameter("@FinishFlag",handleResult),
                                          };
            List<AdvertManage.Model.AMS_CommandListModel> listModel = new List<Model.AMS_CommandListModel>();
            DataSet ds = commandDal.GetList(strWhere.ToString(), parameters);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listModel.Add(DataRowToAMS_CammandListModel(ds.Tables[0].Rows[i]));
            }
            return listModel;
        }
        #region 私有方法
        private AdvertManage.Model.AMS_CommandListModel DataRowToAMS_CammandListModel(DataRow dr)
        {
            AdvertManage.Model.AMS_CommandListModel model = new AdvertManage.Model.AMS_CommandListModel();

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
                model.FinishFlag = (Model.Enum.CommandHandleResult)int.Parse(dr["FinishFlag"].ToString());

            }
            if (dr["SchoolNum"] != null && dr["SchoolNum"].ToString() != "")
            {
                model.SchoolNum = dr["SchoolNum"].ToString();
            }
            if (dr["SchoolName"] != null && dr["SchoolName"].ToString() != "")
            {
                model.SchoolName = dr["SchoolName"].ToString();
            }
            if (dr["SchoolConnectionString"] != null && dr["SchoolConnectionString"].ToString() != "")
            {
                model.SchoolConnectionString = dr["SchoolConnectionString"].ToString();
            }
            if (dr["SchoolDescribe"] != null && dr["SchoolDescribe"].ToString() != "")
            {
                model.SchoolDescribe = dr["SchoolDescribe"].ToString();
            }
            if (dr["SchoolDTUip"] != null && dr["SchoolDTUip"].ToString() != "")
            {
                model.SchoolDTUip = dr["SchoolDTUip"].ToString();
            }
            return model;



        }
        #endregion
    }
}
