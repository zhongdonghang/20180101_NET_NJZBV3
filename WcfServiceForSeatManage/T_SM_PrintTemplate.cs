using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.ClassModel;
using System.Data.SqlClient;
using System.Data;
using SeatManage.DAL;
using SeatManage.EnumType;
using System.Xml;
namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        T_SM_PrintTemplate t_sm_PrintTemplate = new T_SM_PrintTemplate();
        public string GetPrintTemplate(string CardNo)
        {
            string defaultTemplate = GetDefaultPrintTemplate();
            List<SeatManage.ClassModel.AMS_Advertisement> print = GetAdList(false, AdType.PrintReceiptAd);
            string advertTemlate = "";
            //StringBuilder strWhere = new StringBuilder();
            //StringBuilder strOrder = new StringBuilder();
            //strWhere.Append("(IsUsed=1 and UsedTimeStart<getdate() and UsedTimeEnd>getDate()) ");//IsUsed为1 是临时使用的模板，为2是默认的模板
            //strOrder.Append(" newid()");
            //DataSet ds = t_sm_PrintTemplate.GetList(1, strWhere.ToString(), strOrder.ToString(), null);
            if (print.Count > 0)
            {
                advertTemlate = print[0].AdContent;
            }
            if (string.IsNullOrEmpty(defaultTemplate))
            {
                StringBuilder template = new StringBuilder();
                template.Append("<?xml version='1.0' encoding='utf-8'?>");
                template.Append("<Print>");
                template.Append("<Content font='黑体' size='14' bold='Y' italic='N'>    #SeatNo#</Content>");
                template.Append("<Content font='宋体' size='8' bold='N' italic='N'>阅览室名称：#ReadingRoomName#</Content>");
                template.Append("<Content font='宋体' size='8' bold='N' italic='N'>学号：#CardNo#</Content>");
                template.Append("<Content font='宋体' size='8' bold='N' italic='N'>姓名：#StuName#</Content>");
                template.Append("<Content font='宋体' size='8' bold='N' italic='N'>原读者：#SecCardNo#</Content>");
                template.Append("<Content font='宋体' size='7' bold='N' italic='N'>日期:#DateTime#</Content>");
                template.Append("<Content font='宋体' size='7' bold='N' italic='N'>等待结束:#EndDateTime#</Content>");
                template.Append("<Content font='宋体' size='7' bold='Y' italic='N'>离开请刷卡</Content>");
                template.Append("</Print>");
                defaultTemplate = template.ToString();
            }
            if (string.IsNullOrEmpty(advertTemlate))
            {
                return defaultTemplate;
            }
            else
            {
                XmlDocument defaultdoc = new XmlDocument();//原模板
                defaultdoc.LoadXml(defaultTemplate);
                XmlNodeList defaultnodes = defaultdoc.SelectSingleNode("//Print").ChildNodes;

                XmlDocument advertdoc = new XmlDocument();//广告模板
                advertdoc.LoadXml(advertTemlate);
                XmlNodeList advertnodes = advertdoc.SelectSingleNode("//Root/PrintTemplate").ChildNodes;

                StringBuilder newTemplate = new StringBuilder();
                newTemplate.Append("<?xml version='1.0' encoding='utf-8'?>");
                newTemplate.Append("<Print>");
                foreach (XmlNode item in defaultnodes)
                {
                    newTemplate.Append(item.OuterXml);
                }
                foreach (XmlNode item in advertnodes)
                {
                    newTemplate.Append(item.OuterXml);
                }
                newTemplate.Append("</Print>");
                return newTemplate.ToString();
            }
        }

        /// <summary>
        /// 添加打印模板
        /// </summary>
        /// <returns></returns>
        public HandleResult AddPrintTemplate(SeatManage.ClassModel.AMS_PrintTemplateModel model)
        {
            try
            {
                if (t_sm_PrintTemplate.Add(model))
                {
                    return HandleResult.Successed;
                }
                else
                {
                    return HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取过期的打印模板
        /// </summary>
        /// <returns></returns>
        public List<SeatManage.ClassModel.AMS_PrintTemplateModel> GetPrintTemplateOverTime()
        {
            try
            {
                List<SeatManage.ClassModel.AMS_PrintTemplateModel> list = new List<AMS_PrintTemplateModel>();
                StringBuilder strWhere = new StringBuilder();
                strWhere.Append(" datediff(day,UsedTimeEnd,@NowDate)>0");
                SqlParameter[] parameters = {
                                        new SqlParameter("@NowDate",SqlDbType.DateTime)
                                       };
                parameters[0].Value = GetServerDateTime();
                DataSet ds = t_sm_PrintTemplate.GetList(strWhere.ToString(), parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        list.Add(DataRowsToPrintTemplateModel(ds.Tables[0].Rows[i]));
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 删除打印模板
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult DeletePrintTemplate(SeatManage.ClassModel.AMS_PrintTemplateModel model)
        {
            try
            {
                if (t_sm_PrintTemplate.Delete(model.Id.ToString()))
                {
                    return HandleResult.Successed;
                }
                else
                {
                    return HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static SeatManage.ClassModel.AMS_PrintTemplateModel DataRowsToPrintTemplateModel(DataRow dr)
        {
            //id,Template,UsedTimeStart,UsedTimeEnd,IsUsed,Describe
            SeatManage.ClassModel.AMS_PrintTemplateModel model = new AMS_PrintTemplateModel();
            model.Id = int.Parse(dr["id"].ToString());
            model.Describe = dr["Describe"].ToString();
            model.EffectDate = DateTime.Parse(dr["UsedTimeStart"].ToString());
            model.EndDate = DateTime.Parse(dr["UsedTimeEnd"].ToString());
            model.Template = dr["Template"].ToString();
            model.Num = dr["Num"].ToString();
            return model;
        }


        public AMS_PrintTemplateModel GetPrintTemplateByNum(string Num)
        {
            try
            {
                SeatManage.ClassModel.AMS_PrintTemplateModel list = new AMS_PrintTemplateModel();
                StringBuilder strWhere = new StringBuilder();
                strWhere.Append("Num=@Num");
                SqlParameter[] parameters = {
                                        new SqlParameter("@Num",SqlDbType.NVarChar)
                                       };
                parameters[0].Value = Num;
                DataSet ds = t_sm_PrintTemplate.GetList(strWhere.ToString(), parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    list = DataRowsToPrintTemplateModel(ds.Tables[0].Rows[0]);
                }
                else
                {
                    return null;
                }
                return list;
            }
            catch
            {
                throw;
            }
        }

        public HandleResult UpdatePrintTemplate(AMS_PrintTemplateModel model)
        {
            try
            {
                if (t_sm_PrintTemplate.Update(model))
                {
                    return HandleResult.Successed;
                }
                else
                {
                    return HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
