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
        AMS.DAL.View_CommandList dal_CommandListView = new DAL.View_CommandList();
        AMS.DAL.AMS_CommandList dal_CommandList = new DAL.AMS_CommandList();
        AMS.DAL.SlipReleaseCampus dal_SlipReleaseCampus = new DAL.SlipReleaseCampus();
        public List<Model.View_CommandList> GetAllCommandList()
        {
            try
            {
                List<Model.View_CommandList> modellist = new List<Model.View_CommandList>();
                DataSet ds = dal_CommandListView.GetList("");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modellist.Add(DataRowToAMS_CommandListModel(ds.Tables[0].Rows[i]));
                }
                return modellist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Model.View_CommandList> GetCommandList(string schoolId, AMS.Model.Enum.CommandType commandType, AMS.Model.Enum.CommandHandleResult handleResult)
        {
            try
            {
                List<Model.View_CommandList> modellist = new List<Model.View_CommandList>();
                StringBuilder str = new StringBuilder();
                //拼接学校编号。如果学校编号为-1，则标识查询所有的。
                if (schoolId != "-1")
                {
                    str.Append(" SchoolNum='" + schoolId + "'");
                }
                //拼接命令类型，如果条件为空，则不加and
                if (string.IsNullOrEmpty(str.ToString()))
                {
                    if (commandType != Model.Enum.CommandType.None)
                    {
                        str.AppendFormat(" Command={0}  ", (int)commandType);
                    }
                }
                else
                {
                    if (commandType != Model.Enum.CommandType.None)
                    {
                        str.AppendFormat(" and Command={0}", (int)commandType);
                    }
                }

                //拼接“处理结果” 参数
                if (string.IsNullOrEmpty(str.ToString()))
                {
                    if (handleResult != Model.Enum.CommandHandleResult.None)//不为None时，查询指定类型。
                    {
                        str.AppendFormat(" FinishFlag={0}", (int)handleResult);
                    }
                    else
                    {//参数值为None时，去掉已经成功的
                        str.AppendFormat(" FinishFlag<>{0}", (int)Model.Enum.CommandHandleResult.Success);
                    }
                }
                else
                {
                    if (handleResult != Model.Enum.CommandHandleResult.None)//不为None时，查询指定类型。
                    {
                        str.AppendFormat(" and FinishFlag={0}", (int)handleResult);
                    }
                    else
                    {//参数值为None时，去掉已经成功的
                        str.AppendFormat(" and FinishFlag<>{0}", (int)Model.Enum.CommandHandleResult.Success);
                    }
                }

                DataSet ds = dal_CommandListView.GetList(str.ToString());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modellist.Add(DataRowToAMS_CommandListModel(ds.Tables[0].Rows[i]));
                }
                return modellist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取命令明细列表
        /// </summary>
        /// <param name="schoolId"></param>
        /// <param name="commandType"></param>
        /// <param name="handleResult"></param>
        /// <returns></returns>
        public List<Model.AMS_CommandDetail> GetCommandDetailList(string schoolId, AMS.Model.Enum.CommandType commandType, AMS.Model.Enum.CommandHandleResult handleResult)
        {
            StringBuilder str = new StringBuilder();
            List<AMS.Model.AMS_CommandDetail> commandDetailList = new List<Model.AMS_CommandDetail>();
            List<Model.View_CommandList> commandList = GetCommandList(schoolId, commandType, handleResult);
            foreach (AMS.Model.View_CommandList cmdModel in commandList)
            {
                AMS.Model.AMS_CommandDetail cmd = new Model.AMS_CommandDetail();
                switch (cmdModel.Command)
                {
                    case Model.Enum.CommandType.Caputre:
                        cmd.ContentDescribe = "获取截图";
                        cmd.ContentName = "获取截图";
                        break;
                    case Model.Enum.CommandType.HardAd:
                        Model.AMS_HardAd hardAd = Dal_HardAd.GetModelByNum(cmdModel.CommandId.Value);
                        cmd.ContentName = hardAd.Name;
                        cmd.ContentDescribe = hardAd.Describe;
                        cmd.ContentNumber = hardAd.Number;
                        cmd.ContentID = hardAd.ID;
                        break;
                    case Model.Enum.CommandType.Playlist:
                        if (dal_Playlist.GetList(string.Format(" id={0}", cmdModel.CommandId.Value)).Tables[0].Rows.Count > 0)
                        {
                            Model.AMS_PlayList playList = DataRowToAMS_PlayListModel(dal_PlaylistView.GetList(string.Format(" id={0}", cmdModel.CommandId.Value)).Tables[0].Rows[0]);
                            cmd.ContentName = playList.PlayListName;
                            cmd.ContentDescribe = playList.Describe;
                            cmd.ContentNumber = playList.Number;
                            cmd.ContentID = playList.Id;
                        }
                        break;
                    case Model.Enum.CommandType.PrintTemplate:
                        if (dal_PrintTemplateView.GetList(string.Format(" id={0}", cmdModel.CommandId.Value)).Tables[0].Rows.Count > 0)
                        {

                            Model.AMS_PrintTemplate printTemplate = DataRowToAMS_PrintTemplateModel(dal_PrintTemplateView.GetList(string.Format(" id={0}", cmdModel.CommandId.Value)).Tables[0].Rows[0]);
                            cmd.ContentName = printTemplate.Name;
                            cmd.ContentNumber = printTemplate.Number;
                            cmd.ContentDescribe = printTemplate.Describe;
                            cmd.ContentID = printTemplate.Id;
                        }
                        break;
                    case Model.Enum.CommandType.ProgramUpgrade:
                        if (dal_ProgramUpgrade.GetList(string.Format(" id={0}", cmdModel.CommandId.Value)).Tables[0].Rows.Count > 0)
                        {
                            Model.ProgramUpgrade program = DataRowToAMS_ProgramUpgradeListModel(dal_ProgramUpgrade.GetList(string.Format(" id={0}", cmdModel.CommandId.Value)).Tables[0].Rows[0]);
                            cmd.ContentName = ((AMS.Model.Enum.SeatManageSubsystem)program.Application).ToString();
                            cmd.ContentDescribe = program.Remark;
                            cmd.ContentID = program.Id;
                        }
                        break;
                    case Model.Enum.CommandType.SlipCustomer:
                        if (dal_SlipCustomer.GetList(string.Format(" id={0}", cmdModel.CommandId.Value)).Tables[0].Rows.Count > 0)
                        {
                            Model.AMS_SlipCustomer slipcustomer = DataRowToAMS_SlipCustomerModel(dal_SlipCustomerView.GetList(string.Format(" id={0}", cmdModel.CommandId.Value)).Tables[0].Rows[0]);
                            cmd.ContentName = slipcustomer.SlipName;
                            cmd.ContentNumber = slipcustomer.Number;
                            cmd.ContentDescribe = slipcustomer.Describe;
                            cmd.ContentID = slipcustomer.Id;
                        }
                        break;
                    case Model.Enum.CommandType.TitleAd:
                        if (Dal_TitleAd.GetList(string.Format(" id={0}", cmdModel.CommandId.Value)).Tables[0].Rows.Count > 0)
                        {
                            Model.AMS_TitleAd titleAd = DataRowToAMS_TitleAd(Dal_TitleAd.GetList(string.Format(" id={0}", cmdModel.CommandId.Value)).Tables[0].Rows[0]);
                            cmd.ContentName = titleAd.Name;
                            cmd.ContentNumber = titleAd.Num;
                            cmd.ContentID = titleAd.Id;
                        }
                        break;
                    case Model.Enum.CommandType.RollTitles:
                        if (dal.GetModel(string.Format(" id={0}", cmdModel.CommandId.Value)).Tables[0].Rows.Count > 0)
                        {
                            Model.AMS_RollTitles titleAd = DataRowToAMS_RollTitles(dal.GetModel(string.Format(" id={0}", cmdModel.CommandId.Value)).Tables[0].Rows[0]);
                            cmd.ContentName = titleAd.Name;
                            cmd.ContentNumber = titleAd.Num;
                            cmd.ContentID = titleAd.ID;
                        }
                        break;
                }

                cmd.Command = cmdModel.Command;
                cmd.FinishFlag = cmdModel.FinishFlag;
                cmd.FinishTime = cmdModel.FinishTime;
                cmd.ID = cmdModel.ID;
                cmd.OperatorBranchName = cmdModel.OperatorBranchName;
                cmd.OperatorLoginId = cmdModel.OperatorLoginId;
                cmd.OperatorPwd = cmdModel.OperatorPwd;
                cmd.OperatorRemark = cmdModel.OperatorRemark;
                cmd.OperatorName = cmdModel.OperatorName;
                cmd.ReleaseTime = cmdModel.ReleaseTime;
                cmd.SchoolAddress = cmdModel.SchoolAddress;
                cmd.SchoolCardInfo = cmdModel.SchoolCardInfo;
                cmd.SchoolConnectionString = cmdModel.SchoolConnectionString;
                cmd.SchoolDescribe = cmdModel.SchoolDTUip;
                cmd.SchoolInterfaceInfo = cmdModel.SchoolInterfaceInfo;
                cmd.SchoolLinkMan = cmdModel.SchoolLinkMan;
                cmd.SchoolName = cmdModel.SchoolName;
                cmd.SchoolNum = cmdModel.SchoolNum;
                cmd.SchoolProvince = cmdModel.SchoolProvince;
                commandDetailList.Add(cmd);
            }

            return commandDetailList;

        }

        /// <summary>
        /// 添加命令列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddNewCommandList(Model.AMS_CommandList model)
        {
            try
            {
                dal_CommandList.Add(model);
                return "";
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("下发命令出错：{0}", ex.Message));
            }
        }
        /// <summary>
        /// 下发优惠券的命令
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddNewCampusCommandList(Model.SlipReleaseCampus model)
        {
            try
            {

                return dal_SlipReleaseCampus.Add(model);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("下发命令出错：{0}", ex.Message));
            }
        }

        /// <summary>
        /// 转换commandlist的model
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private AMS.Model.View_CommandList DataRowToAMS_CommandListModel(DataRow dr)
        {
            AMS.Model.View_CommandList model = new Model.View_CommandList();

            string strTemp = "";
            model.ID = int.Parse(dr["ID"].ToString());
            model.CommandId = int.Parse(dr["CommandId"].ToString());
            model.Command = (AMS.Model.Enum.CommandType)int.Parse(dr["Command"].ToString());
            model.FinishFlag = int.Parse(dr["FinishFlag"].ToString());
            strTemp = dr["FinishTime"].ToString();
            if (!string.IsNullOrEmpty(strTemp))
            {
                model.FinishTime = DateTime.Parse(strTemp);
            }
            model.OperatorBranchName = dr["OperatorBranchName"].ToString();
            model.OperatorLoginId = dr["OperatorLoginId"].ToString();
            model.OperatorName = dr["OperatorName"].ToString();
            model.OperatorRemark = dr["OperatorRemark"].ToString();
            strTemp = dr["ReleaseTime"].ToString();
            if (!string.IsNullOrEmpty(strTemp))
            {
                model.ReleaseTime = DateTime.Parse(strTemp);
            }
            model.SchoolName = dr["SchoolName"].ToString();
            model.SchoolNum = dr["SchoolNum"].ToString();
            return model;
        }


        public bool DelCommandDetailInfo(int id)
        {
            return ComDal.Delete(id);
        }
    }
}
