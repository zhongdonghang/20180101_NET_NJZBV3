using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.ClassModel;
using SeatManage.DAL;
using System.Data;
using System.Data.SqlClient;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        AMS_TitleAd ams_TitleAd = new AMS_TitleAd();
        /// <summary>
        /// 获取冠名信息
        /// </summary>
        /// <returns></returns>
        public TitleAdvertInfo GetTitleAdvertInfo() 
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(string.Format(" datediff(day,EffectDate,@NowDate)>=0 and datediff(day,EndDate,@NowDate)<=0 order by EffectDate desc" ));
            SqlParameter[] parameters = {
                                        new SqlParameter("@NowDate",SqlDbType.DateTime)
                                       };
            parameters[0].Value = GetServerDateTime();
            DataSet ds = ams_TitleAd.GetList(strWhere.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                TitleAdvertInfo info = dataRowToTitleAdvertInfo(ds.Tables[0].Rows[0]);
                return info;
            }
            else 
            {
                return
                    null;
            }
        }
        /// <summary>
        /// 添加冠名广告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult AddTitleAdvert(SeatManage.ClassModel.TitleAdvertInfo model)
        {
            try
            {
                if (ams_TitleAd.Add(model))
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
        /// <summary>
        /// 删除冠名广告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult DeleteTitleAdvert(SeatManage.ClassModel.TitleAdvertInfo model)
        {
            try
            {
                if (ams_TitleAd.Delete(model.ID))
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
        /// <summary>
        /// 获取过期的冠名广告
        /// </summary>
        /// <returns></returns>
        public List<SeatManage.ClassModel.TitleAdvertInfo> GetTitleAdOverTime()
        {
            List<SeatManage.ClassModel.TitleAdvertInfo> list = new List<TitleAdvertInfo>();
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(string.Format("  datediff(day,EndDate,@NowDate)>0"));
            SqlParameter[] parameters = {
                                        new SqlParameter("@NowDate",SqlDbType.DateTime)
                                       };
            parameters[0].Value = GetServerDateTime();
            DataSet ds = ams_TitleAd.GetList(strWhere.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    list.Add(dataRowToTitleAdvertInfo(ds.Tables[0].Rows[i]));
                }
            }
            return list;
        }
        /// <summary>
        /// 冠名广告信息行转换成列
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private TitleAdvertInfo dataRowToTitleAdvertInfo(DataRow dr)
        {
            TitleAdvertInfo info = new TitleAdvertInfo();
            info.EffectDate = DateTime.Parse(dr["EffectDate"].ToString());
            info.EndDate = DateTime.Parse(dr["EndDate"].ToString());
            info.TitleAdvert = dr["AdContent"].ToString();
            info.TitleAdvertNo = dr["Num"].ToString();
            info.ID = int.Parse(dr["AdNo"].ToString());
            return info;
        }


        public TitleAdvertInfo GetTitleModel(string Num)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(string.Format("Num=@Num"));
            SqlParameter[] parameters = {
                                        new SqlParameter("@Num",SqlDbType.NVarChar)
                                       };
            parameters[0].Value = Num;
            DataSet ds = ams_TitleAd.GetList(strWhere.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                TitleAdvertInfo info = dataRowToTitleAdvertInfo(ds.Tables[0].Rows[0]);
                return info;
            }
            else
            {
                return
                    null;
            }
        }

        public SeatManage.EnumType.HandleResult UpdateTitleAdvert(TitleAdvertInfo model)
        {
            try
            {
                if (ams_TitleAd.Update(model))
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
