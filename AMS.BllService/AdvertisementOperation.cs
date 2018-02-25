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
        DAL.View_Advertisement dal_advertisementView = new DAL.View_Advertisement();
        DAL.View_AdvertisementCopy dal_advertisementcopyView = new DAL.View_AdvertisementCopy();
        DAL.AMS_Advertisement dal_advertisement = new DAL.AMS_Advertisement();
        DAL.AMS_AdvertisementSchoolCopy dal_advertisementcopy = new DAL.AMS_AdvertisementSchoolCopy();
        DAL.AMS_AdvertUsage dal_advertUsage = new DAL.AMS_AdvertUsage();
        DAL.View_AdvertUsage dal_advertUageView = new DAL.View_AdvertUsage();
        /// <summary>
        /// 获取广告列表
        /// </summary>
        /// <param name="adType"></param>
        /// <returns></returns>
        public List<Model.AMS_Advertisement> GetAdvertList(Model.Enum.AdType adType)
        {
            try
            {
                List<Model.AMS_Advertisement> modelList = new List<Model.AMS_Advertisement>();
                StringBuilder strWhere = new StringBuilder();
                strWhere.Append(" [Type]='" + (int)adType + "'");
                DataSet ds = dal_advertisementView.GetList(strWhere.ToString());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(DataRowToAMS_Advertisement(ds.Tables[0].Rows[i]));
                }
                return modelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取学校广告
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <param name="AdID"></param>
        /// <returns></returns>
        public Model.AMS_AdvertisementSchoolCopy GetSchoolAdvert(int AdID)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                strWhere.Append(" ID='" + AdID + "'");
                DataSet ds = dal_advertisementcopyView.GetList(strWhere.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToAMS_AdvertisementCopy(ds.Tables[0].Rows[0]);
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
        /// <summary>
        /// 获取学校的广告
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <returns></returns>
        public List<Model.AMS_AdvertisementSchoolCopy> GetSchoolNowAdvert(int SchoolID)
        {
            try
            {
                List<Model.AMS_AdvertisementSchoolCopy> modelList = new List<Model.AMS_AdvertisementSchoolCopy>();
                StringBuilder strWhere = new StringBuilder();
                strWhere.Append(" [SchoolID]='" + SchoolID.ToString() + "'");
                DataSet ds = dal_advertisementcopyView.GetList(strWhere.ToString());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(DataRowToAMS_AdvertisementCopy(ds.Tables[0].Rows[i]));
                }
                return modelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Model.AMS_Advertisement GetSingleAdvertisement(int ID)
        {
            try
            {
                Model.AMS_Advertisement model = new Model.AMS_Advertisement();
                StringBuilder strWhere = new StringBuilder();
                strWhere.Append(" ID='" + ID + "'");
                DataSet ds = dal_advertisementView.GetList(strWhere.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToAMS_Advertisement(ds.Tables[0].Rows[0]);
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



        public Model.AMS_AdvertisementSchoolCopy GetSameSchoolAdvert(int SchoolID, int OriginalID)
        {
            try
            {
                Model.AMS_AdvertisementSchoolCopy model = new Model.AMS_AdvertisementSchoolCopy();
                StringBuilder strWhere = new StringBuilder();
                strWhere.Append(" SchoolID='" + SchoolID + "' and OriginalID='" + OriginalID + "'");
                DataSet ds = dal_advertisementcopyView.GetList(strWhere.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToAMS_AdvertisementCopy(ds.Tables[0].Rows[0]);
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
        public bool AddAdvertisement(Model.AMS_Advertisement model)
        {
            try
            {
                model.ReleaseDate = DateTime.Now;
                return dal_advertisement.Add(model) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateAdvertisement(Model.AMS_Advertisement model)
        {
            try
            {
                return dal_advertisement.Update(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteAdvertisement(Model.AMS_Advertisement model)
        {
            try
            {
                return dal_advertisement.Delete(model.ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddAdvertisementCopy(Model.AMS_AdvertisementSchoolCopy model)
        {
            try
            {
                return dal_advertisementcopy.Add(model) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateAdvertisementCopy(Model.AMS_AdvertisementSchoolCopy model)
        {
            try
            {
                return dal_advertisementcopy.Update(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteAdvertisementCopy(Model.AMS_AdvertisementSchoolCopy model)
        {
            try
            {
                return dal_advertisementcopy.Delete(model.ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private AMS.Model.AMS_Advertisement DataRowToAMS_Advertisement(DataRow dr)
        {
            //[ID],[Num],[Name],[EffectDate],[EndDate],[Type],[AdContent],[CustomerID],[OperatorName] ,[CompanyName],[UserName]
            AMS.Model.AMS_Advertisement model = new Model.AMS_Advertisement();
            model.AdContent = dr["AdContent"].ToString();
            model.CustomerID = int.Parse(dr["CustomerID"].ToString());
            model.EffectDate = DateTime.Parse(dr["EffectDate"].ToString());
            model.EndDate = DateTime.Parse(dr["EndDate"].ToString());
            model.ID = int.Parse(dr["ID"].ToString());
            model.Name = dr["Name"].ToString();
            model.Num = dr["Num"].ToString();
            model.OperatorID = int.Parse(dr["OperatorName"].ToString());
            model.OperatorName = dr["UserName"].ToString();
            model.ReleaseDate = DateTime.Parse(dr["ReleaseDate"].ToString());
            model.Type = (AMS.Model.Enum.AdType)int.Parse(dr["Type"].ToString());
            return model;
        }
        private AMS.Model.AMS_AdvertisementSchoolCopy DataRowToAMS_AdvertisementCopy(DataRow dr)
        {
            //[[SchoolID],[CustomerID],[OriginalID],[UserName],[IsNew],[Type],[AdContent],[OperatorName],[EndDate],[EffectDate],[Name],[Num],[ID],[SchoolName],[Number],[CompanyName],[CustomerNo]
            AMS.Model.AMS_AdvertisementSchoolCopy model = new Model.AMS_AdvertisementSchoolCopy();
            model.AdContent = dr["AdContent"].ToString();
            model.CustomerID = int.Parse(dr["CustomerID"].ToString());
            model.EffectDate = DateTime.Parse(dr["EffectDate"].ToString());
            model.EndDate = DateTime.Parse(dr["EndDate"].ToString());
            model.ID = int.Parse(dr["ID"].ToString());
            model.Name = dr["Name"].ToString();
            model.Num = dr["Num"].ToString();
            model.OperatorID = int.Parse(dr["OperatorName"].ToString());
            model.OperatorName = dr["UserName"].ToString();
            model.Type = (AMS.Model.Enum.AdType)int.Parse(dr["Type"].ToString());
            model.IsNew = bool.Parse(dr["IsNew"].ToString());
            model.OriginalID = int.Parse(dr["OriginalID"].ToString());
            model.SchoolID = int.Parse(dr["SchoolID"].ToString());
            model.SchoolName = dr["SchoolName"].ToString();
            return model;
        }








        //存在相同的广告
        public bool ExistSameAdvert(string Num, string Name, AMS.Model.Enum.AdType adType)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                strWhere.Append(" (Num='" + Num + "' or Name='" + Name + "') and [Type]='" + (int)adType + "'");
                DataSet ds = dal_advertisementView.GetList(strWhere.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Model.AMS_AdvertUsage> GetAdvertUsage(int SchoolID, int AdID)
        {
            try
            {
                List<Model.AMS_AdvertUsage> modelList = new List<Model.AMS_AdvertUsage>();
                StringBuilder strWhere = new StringBuilder();
                if (SchoolID > 0)
                {
                    strWhere.Append(" SchoolID='" + SchoolID + "'");
                }
                if (AdID > 0)
                {
                    if (!string.IsNullOrEmpty(strWhere.ToString()))
                    {
                        strWhere.Append(" and ");
                    }
                    strWhere.Append(" OriginalID='" + AdID + "'");
                }
                DataSet ds = dal_advertUageView.GetList(strWhere.ToString());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(DataRowToAMS_AdvertUsage(ds.Tables[0].Rows[i]));
                }
                return modelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpLoadGetAdvertUsage(Model.AMS_AdvertUsage model)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                strWhere.Append(" [Type]='" + (int)model.AdvertType + "' and Number='" + model.SchoolNum + "' and Num='" + model.AdvertNum + "'");
                DataSet ds = dal_advertUageView.GetList(strWhere.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Model.AMS_AdvertUsage sameModel = DataRowToAMS_AdvertUsage(ds.Tables[0].Rows[0]);
                    sameModel.AdvertUsage = Model.AMS_AdvertUsage.ToXml(model);
                    sameModel.LastUpdateTime = model.LastUpdateTime;
                    return dal_advertUsage.Update(sameModel);
                }
                else
                {
                    Model.AMS_AdvertisementSchoolCopy advert = GetSchholAdvertByNum(model.AdvertNum, model.SchoolNum, model.AdvertType);
                    if (advert == null)
                    {
                        return true;
                    }
                    model.SchoolID = advert.SchoolID;
                    model.AdvertID = advert.ID;
                    return dal_advertUsage.Add(model) > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private Model.AMS_AdvertUsage DataRowToAMS_AdvertUsage(DataRow dr)
        {
            //[AdvertID],[ID],[AdvertUsage],[LastUpdateTime],[SchoolID],[CustomerID],[OriginalID],[UserName],[Type]
            //,[AdContent],[IsNew],[OperatorName],[EndDate],[EffectDate],[Name],[Num],[SchoolName],
            //[Number],[CompanyName],[CustomerNo],[OriginalNum],[OriginaName],[OriginaEffectDate],[OriginaEndDate],[OriginaContent]
            Model.AMS_AdvertUsage model = new Model.AMS_AdvertUsage();
            model = Model.AMS_AdvertUsage.ToModel(dr["AdvertUsage"].ToString());
            model.ID = int.Parse(dr["ID"].ToString());
            model.AdvertID = int.Parse(dr["AdvertID"].ToString());
            model.AdvertNum = dr["Num"].ToString();
            model.AdvertType = (AMS.Model.Enum.AdType)int.Parse(dr["Type"].ToString());
            model.AdvertUsage = dr["AdvertUsage"].ToString();
            model.LastUpdateTime = DateTime.Parse(dr["LastUpdateTime"].ToString());
            model.SchoolID = int.Parse(dr["SchoolID"].ToString());
            model.SchoolName = dr["SchoolName"].ToString();
            model.SchoolNum = dr["Number"].ToString();
            
            return model;
        }




        public Model.AMS_AdvertisementSchoolCopy GetSchholAdvertByNum(string AdNum, string schoolNum, Model.Enum.AdType adType)
        {
            try
            {
                Model.AMS_AdvertisementSchoolCopy model = new Model.AMS_AdvertisementSchoolCopy();
                StringBuilder strWhere = new StringBuilder();
                strWhere.Append(" Number='" + schoolNum + "' and Num='" + AdNum + "' and [Type]='" + (int)adType + "'");
                DataSet ds = dal_advertisementcopyView.GetList(strWhere.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToAMS_AdvertisementCopy(ds.Tables[0].Rows[0]);
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
    }
}
