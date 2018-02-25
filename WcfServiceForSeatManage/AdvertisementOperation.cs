using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using System.Data;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        SeatManage.DAL.AMS_Advertisement advertisement_dal = new SeatManage.DAL.AMS_Advertisement();
        /// <summary>
        /// 获取单个广告
        /// </summary>
        /// <param name="adNum"></param>
        /// <param name="adType"></param>
        /// <returns></returns>
        public SeatManage.ClassModel.AMS_Advertisement GetAdModel(string adNum, SeatManage.EnumType.AdType adType)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                strWhere.Append(" Num='" + adNum + "' and [Type]='" + (int)adType + "'");
                DataSet ds = advertisement_dal.GetList(strWhere.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToAMS_Advertisement(ds.Tables[0].Rows[0]);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取有效或无效的广告
        /// </summary>
        /// <param name="isOverTime">是否超时，null为</param>
        /// <param name="adType"></param>
        /// <returns></returns>
        public List<SeatManage.ClassModel.AMS_Advertisement> GetAdList(bool? isOverTime, SeatManage.EnumType.AdType adType)
        {
            try
            {
                List<SeatManage.ClassModel.AMS_Advertisement> modelList = new List<SeatManage.ClassModel.AMS_Advertisement>();
                StringBuilder strWhere = new StringBuilder();
                if (adType != SeatManage.EnumType.AdType.None)
                {
                    strWhere.Append(" [Type]='" + (int)adType + "'");
                }
                if (isOverTime != null)
                {
                    if (strWhere.ToString() != "")
                    {
                        strWhere.Append(" and ");
                    }
                    if (isOverTime.Value)
                    {
                        strWhere.Append(" EndDate<'" + DateTime.Now.ToShortDateString() + "'");
                    }
                    else
                    {
                        strWhere.Append(" EndDate>='" + DateTime.Now.ToShortDateString() + "' and EffectDate <='" + DateTime.Now.ToShortDateString() + "' Order by EndDate desc");
                    }
                }
                DataSet ds = advertisement_dal.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(DataRowToAMS_Advertisement(ds.Tables[0].Rows[i]));
                }
                return modelList;
            }
            catch
            {
                throw;
            }
        }

        public string AddAdModel(SeatManage.ClassModel.AMS_Advertisement adInfo)
        {
            try
            {
                if (advertisement_dal.Add(adInfo))
                {
                    return "";
                }
                else
                {
                    return "添加失败";
                }
            }
            catch
            {
                throw;
            }
        }

        public string UpdateAdModel(SeatManage.ClassModel.AMS_Advertisement adInfo)
        {
            try
            {
                if (advertisement_dal.Update(adInfo))
                {
                    return "";
                }
                else
                {
                    return "更新失败";
                }
            }
            catch
            {
                throw;
            }
        }

        public string DeleteAdModel(SeatManage.ClassModel.AMS_Advertisement adInfo)
        {
            try
            {
                if (advertisement_dal.Delete(adInfo))
                {
                    return "";
                }
                else
                {
                    return "删除失败";
                }
            }
            catch
            {
                throw;
            }
        }


        private SeatManage.ClassModel.AMS_Advertisement DataRowToAMS_Advertisement(DataRow dr)
        {
            // [ID],[Num],[EffectDate],[EndDate],[Type],[AdContent]
            SeatManage.ClassModel.AMS_Advertisement model = new SeatManage.ClassModel.AMS_Advertisement();
            model.ID = int.Parse(dr["ID"].ToString());
            model.Name = dr["Name"].ToString();
            model.Num = dr["Num"].ToString();
            model.EffectDate = DateTime.Parse(dr["EffectDate"].ToString());
            model.EndDate = DateTime.Parse(dr["EndDate"].ToString());
            model.Type = (SeatManage.EnumType.AdType)int.Parse(dr["Type"].ToString());
            model.AdContent = dr["AdContent"].ToString();
            return model;
        }

        SeatManage.DAL.AMS_AdvertUsage advertusage_dal = new SeatManage.DAL.AMS_AdvertUsage();
        SeatManage.DAL.ViewAdvertUsage viewadvertusage_dal = new SeatManage.DAL.ViewAdvertUsage();

        /// <summary>
        /// 更新使用情况
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string UpdateAdvertUsage(SeatManage.ClassModel.AMS_AdvertUsage model)
        {
            try
            {
                SeatManage.ClassModel.AMS_AdvertUsage oldModel = GetAdvertUsage(model.AdvertNum, model.AdvertType);
                if (oldModel != null)
                {
                    oldModel.LastUpdateTime = DateTime.Now;
                    oldModel.PlayCount += model.PlayCount;
                    oldModel.PrintCount += model.PrintCount;
                    oldModel.WatchCount += model.WatchCount;
                    foreach (KeyValuePair<string, SeatManage.ClassModel.AdvertisementUsage> item in model.ItemUsage)
                    {
                        if (oldModel.ItemUsage.ContainsKey(item.Key))
                        {
                            oldModel.ItemUsage[item.Key].PlayCount += item.Value.PlayCount;
                            oldModel.ItemUsage[item.Key].PrintCount += item.Value.PrintCount;
                            oldModel.ItemUsage[item.Key].WatchCount += item.Value.WatchCount;
                        }
                        else
                        {
                            oldModel.ItemUsage.Add(item.Key, item.Value);
                        }
                    }
                    if (advertusage_dal.Update(oldModel))
                    {
                        return "";
                    }
                    else
                    {
                        return "更新失败";
                    }
                }
                else
                {
                    model.LastUpdateTime = DateTime.Now;
                    if (advertusage_dal.Add(model))
                    {
                        return "";
                    }
                    else
                    {
                        return "添加失败";
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取单个记录
        /// </summary>
        /// <param name="AdNum"></param>
        /// <param name="adType"></param>
        /// <returns></returns>
        public SeatManage.ClassModel.AMS_AdvertUsage GetAdvertUsage(string AdNum, SeatManage.EnumType.AdType adType)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                strWhere.Append(" AdvertNum='" + AdNum + "' and AdvertType='" + (int)adType + "'");
                DataSet ds = viewadvertusage_dal.GetList(strWhere.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToAMS_AdvertUsage(ds.Tables[0].Rows[0]);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取全部状态
        /// </summary>
        /// <returns></returns>
        public List<SeatManage.ClassModel.AMS_AdvertUsage> GetAdvertUsageList()
        {
            try
            {
                List<SeatManage.ClassModel.AMS_AdvertUsage> modelList = new List<SeatManage.ClassModel.AMS_AdvertUsage>();
                DataSet ds = viewadvertusage_dal.GetList("");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(DataRowToAMS_AdvertUsage(ds.Tables[0].Rows[i]));
                }
                return modelList;
            }
            catch
            {
                throw;
            }
        }

        private SeatManage.ClassModel.AMS_AdvertUsage DataRowToAMS_AdvertUsage(DataRow dr)
        {
            //ID,AdvertID,AdvertUsage,LastUpdateTime,AdvertType,AdvertNum
            SeatManage.ClassModel.AMS_AdvertUsage model = new SeatManage.ClassModel.AMS_AdvertUsage();
            model = SeatManage.ClassModel.AMS_AdvertUsage.ToModel(dr["AdvertUsage"].ToString());
            model.ID = int.Parse(dr["ID"].ToString());
            model.AdvertID = int.Parse(dr["AdvertID"].ToString());
            model.AdvertNum = dr["AdvertNum"].ToString();
            model.AdvertType = (SeatManage.EnumType.AdType)int.Parse(dr["AdvertType"].ToString());
            model.AdvertUsage = dr["AdvertUsage"].ToString();
            model.LastUpdateTime = DateTime.Parse(dr["LastUpdateTime"].ToString());
            
            return model;
        }

         
    }
}
