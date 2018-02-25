using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvertManage.DataTransfer
{
    public class TitleAd
    {
        /// <summary>
        /// 更新冠名广告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool GetTitleAd(int id)
        {
            Model.AMS_TitleAdModel advertTitle = BLL.AMS_TitleAdBLL.GetTitleAdById(id);
            if (advertTitle != null)
            {
                SeatManage.ClassModel.TitleAdvertInfo seatTitle = new SeatManage.ClassModel.TitleAdvertInfo();
                seatTitle.EffectDate = advertTitle.EffectDate;
                seatTitle.EndDate = advertTitle.EndDate;
                seatTitle.TitleAdvert = advertTitle.AdContent;
                if (SeatManage.Bll.AMS_TitleAd.AddTitleAdvert(seatTitle) == SeatManage.EnumType.HandleResult.Successed)
                {
                    return
                        true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
}
