using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.ClassModel;
using System.Data;
using SeatManage.DAL;
using System.Data.SqlClient;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        AMS_HardAd ams_HardAd = new AMS_HardAd();
        /// <summary>
        /// 获取有效的硬广
        /// </summary>
        /// <returns></returns>
        public HardAdvertInfo GetHardAdvert()
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(string.Format(" datediff(day,EffectDate,@NowDate)>=0 and datediff(day,EndDate,@NowDate)<=0 order by EffectDate desc"));
            SqlParameter[] parameters = {
                                        new SqlParameter("@NowDate",SqlDbType.DateTime)
                                       };
            parameters[0].Value = GetServerDateTime();
            DataSet ds = ams_HardAd.GetList(strWhere.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return dataRowToHardAdvertInfo(ds.Tables[0].Rows[0]);
            }
            else
            {
                return
                    null;
            }
        }
        /// <summary>
        /// 添加硬广
        /// </summary>
        /// <param name="hardAdvert"></param>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult AddHardAd(HardAdvertInfo hardAdvert)
        {
            if (ams_HardAd.Add(hardAdvert))
            {
                return SeatManage.EnumType.HandleResult.Successed;
            }
            else
            {
                return SeatManage.EnumType.HandleResult.Failed;
            }
        }

        /// <summary>
        /// 根据编号获取硬广信息
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public HardAdvertInfo GetHardAdvertByNum(string num)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                strWhere.AppendFormat(" AdNo=@AdNo ");
                SqlParameter[] parameters ={
                                        new SqlParameter("@AdNo",num)
                                       };
                DataSet ds = ams_HardAd.GetList(strWhere.ToString(), parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return dataRowToHardAdvertInfo(ds.Tables[0].Rows[0]);
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
        /// 更新硬广内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult UpdateHardAdvert(HardAdvertInfo model)
        {
            if (ams_HardAd.Update(model))
            {
                return SeatManage.EnumType.HandleResult.Successed;
            }
            else
            {
                return SeatManage.EnumType.HandleResult.Failed;
            }
        }
        /// <summary>
        /// 删除硬广
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult DeleteHardAdvert(HardAdvertInfo model)
        {
            if (ams_HardAd.Delete(model.HardAdvertNo))
            {
                return SeatManage.EnumType.HandleResult.Successed;
            }
            else
            {
                return SeatManage.EnumType.HandleResult.Failed;
            }
        }
        /// <summary>
        /// 获取过期的硬广
        /// </summary>
        /// <returns></returns>
        public List<HardAdvertInfo> GetHardAdvertOverTime()
        {
            List<HardAdvertInfo> list = new List<HardAdvertInfo>();
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat("  datediff(day,EndDate,'{0}')>0", GetServerDateTime());
            try
            {
                DataSet ds = ams_HardAd.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    list.Add(dataRowToHardAdvertInfo(ds.Tables[0].Rows[i]));
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private HardAdvertInfo dataRowToHardAdvertInfo(DataRow dr)
        {
            HardAdvertInfo info = new HardAdvertInfo();
            info.AdvertImage = (byte[])dr["AdImage"];
            info.EffectDate = DateTime.Parse(dr["EffectDate"].ToString());
            info.EndDate = DateTime.Parse(dr["EndDate"].ToString());
            info.HardAdvertNo = dr["AdNo"].ToString();
            return info;
        }
    }
}
