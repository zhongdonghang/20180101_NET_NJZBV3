using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.DataTransfer.Code
{
    public class UpLoadAdvertUsage
    {
        public static bool SendUsage()
        {
            try
            {
                //获取使用情况
                List<SeatManage.ClassModel.AMS_AdvertUsage> usageList = SeatManage.Bll.AdvertisementOperation.GetAdvertUsageList();
                //创建使用记录
                List<AMS.Model.AMS_AdvertUsage> modelList = new List<Model.AMS_AdvertUsage>();
                foreach (SeatManage.ClassModel.AMS_AdvertUsage item in usageList)
                {
                    AMS.Model.AMS_AdvertUsage model = new Model.AMS_AdvertUsage();
                    model = AMS.Model.AMS_AdvertUsage.ToModel(item.AdvertUsage);
                    model.AdvertNum = item.AdvertNum;
                    model.AdvertType = (AMS.Model.Enum.AdType)System.Enum.Parse(typeof(AMS.Model.Enum.AdType), item.AdvertType.ToString());
                    model.AdvertUsage = item.AdvertUsage;
                    model.LastUpdateTime = item.LastUpdateTime;
                    model.SchoolNum = ServiceSet.SchoolNums;
                    if (!AMS.ServiceProxy.AdvertisementOperationService.UpLoadGetAdvertUsage(model))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
