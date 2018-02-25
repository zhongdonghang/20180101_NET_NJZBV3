using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;

namespace SeatService.StatisticsService.Code
{
    public class AdvertUsageStatistics
    {
        /// <summary>
        /// 删除过期广告
        /// </summary>
        public void AdvertOverTime()
        {
            try
            {
                string error = "";
                List<AMS_Advertisement> modelList = SeatManage.Bll.AdvertisementOperation.GetAdList(true, AdType.None);
                foreach (AMS_Advertisement model in modelList.Where(model => model.Type != AdType.SchoolNotice))
                {
                    model.ImageFilePath = AMS_Advertisement.GetDownloadFile(model.AdContent);
                    foreach (string file in model.ImageFilePath.Where(file => !FileOperate.FileDelete(file, (SeatManage.EnumType.SeatManageSubsystem)System.Enum.Parse(typeof(SeatManage.EnumType.SeatManageSubsystem), model.Type.ToString()))))
                    {
                        WriteLog.Write(string.Format("删除过期广告处理遇到异常：文件{0}删除失败", file));
                    }
                    error = SeatManage.Bll.AdvertisementOperation.DeleteAdModel(model);
                    if (!string.IsNullOrEmpty(SeatManage.Bll.AdvertisementOperation.DeleteAdModel(model)))
                    {
                        WriteLog.Write(string.Format("删除过期广告处理遇到异常：{0}", error));
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog.Write(string.Format("删除过期广告处理遇到异常：{0}", e.Message));
            }
        }
    }
}
