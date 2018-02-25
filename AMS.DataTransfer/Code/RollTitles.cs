using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.DataTransfer.Code
{
    public class RollTitles
    {
        /// <summary>
        /// 更新滚动广告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool GetTitleAd(int Num)
        {
            try
            {
                Model.AMS_RollTitles advertTitle = AMS.ServiceProxy.IRollTitlesService.GetModelNum(Num);
                if (advertTitle != null)
                {
                    SeatManage.ClassModel.RollTitlesInfo models = SeatManage.Bll.AMS_RollTitles.GetModelByNum(advertTitle.Num);
                    SeatManage.ClassModel.RollTitlesInfo seatTitle = new SeatManage.ClassModel.RollTitlesInfo();
                    seatTitle.EffectDate = Convert.ToDateTime(advertTitle.EffectDate);
                    seatTitle.EndDate = Convert.ToDateTime(advertTitle.EndDate);
                    seatTitle.Type = advertTitle.Type;
                    seatTitle.Num = advertTitle.Num;
                    if (models == null)
                    {
                        if (SeatManage.Bll.AMS_RollTitles.AddRollTitles(seatTitle) == SeatManage.EnumType.HandleResult.Successed)
                        {
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        if (SeatManage.Bll.AMS_RollTitles.UpdateRollTitles(seatTitle) == SeatManage.EnumType.HandleResult.Successed)
                        {
                            return true;
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
