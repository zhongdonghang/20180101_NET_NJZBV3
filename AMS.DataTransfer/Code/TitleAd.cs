using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.DataTransfer.Code
{
    public class TitleAd
    {
        /// <summary>
        /// 更新冠名广告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool GetTitleAd(int Num)
        {
            try
            {
                Model.AMS_TitleAd advertTitle = AMS.ServiceProxy.ITitleAdService.GetTitleAdByNum(Num);
                if (advertTitle != null)
                {
                    SeatManage.ClassModel.TitleAdvertInfo model = SeatManage.Bll.AMS_TitleAd.GetTitleModel(advertTitle.Num);
                    SeatManage.ClassModel.TitleAdvertInfo seatTitle = new SeatManage.ClassModel.TitleAdvertInfo();
                    seatTitle.EffectDate = Convert.ToDateTime(advertTitle.EffectDate);
                    seatTitle.EndDate = Convert.ToDateTime(advertTitle.EndDate);
                    seatTitle.TitleAdvert = advertTitle.AdContent;
                    seatTitle.TitleAdvertNo = advertTitle.Num;
                    if (model == null)
                    {
                        if (SeatManage.Bll.AMS_TitleAd.AddTitleAdvert(seatTitle) == SeatManage.EnumType.HandleResult.Successed)
                        {
                            return
                                true;
                        }
                        return false;
                    }
                    else
                    {
                        if (SeatManage.Bll.AMS_TitleAd.UpdateTitleAdvert(seatTitle) == SeatManage.EnumType.HandleResult.Successed)
                        {
                            return
                                true;
                        }
                        return false;
                    }

                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
