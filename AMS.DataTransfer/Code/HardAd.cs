using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.DataTransfer.Code
{
    /// <summary>
    /// 硬广操作
    /// </summary>
    public class HardAd
    {
        /// <summary>
        /// 获取硬广,并且执行添加到学校数据库操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool GetHardAd(int id)
        {
            try
            {
                Model.AMS_HardAd hardAdModel = AMS.ServiceProxy.IHardAdService.GetHardAdModelByNum(id);
                if (hardAdModel != null)
                {
                    SeatManage.ClassModel.HardAdvertInfo seathardAdvert = SeatManage.Bll.AMS_HardAd.GetHardAdvertByNum(hardAdModel.Number);
                    if (seathardAdvert != null)
                    {
                        seathardAdvert.AdvertImage = hardAdModel.AdImage;
                        seathardAdvert.EffectDate = Convert.ToDateTime(hardAdModel.EffectDate);
                        seathardAdvert.EndDate = Convert.ToDateTime(hardAdModel.EndDate);
                        if (SeatManage.Bll.AMS_HardAd.UpdateHardAdvert(seathardAdvert) == SeatManage.EnumType.HandleResult.Successed)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        seathardAdvert = new SeatManage.ClassModel.HardAdvertInfo();
                        seathardAdvert.AdvertImage = hardAdModel.AdImage;
                        seathardAdvert.EffectDate = Convert.ToDateTime(hardAdModel.EffectDate);
                        seathardAdvert.EndDate = Convert.ToDateTime(hardAdModel.EndDate);
                        seathardAdvert.HardAdvertNo = hardAdModel.Number;
                        if (SeatManage.Bll.AMS_HardAd.AddHardAd(seathardAdvert) == SeatManage.EnumType.HandleResult.Successed)
                        {
                            return true;
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
    }
}
