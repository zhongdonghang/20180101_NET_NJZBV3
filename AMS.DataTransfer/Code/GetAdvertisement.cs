using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AMS.DataTransfer.Code
{
    public class GetAdvertisement
    {
        public static bool Get(int AdID)
        {
            try
            {
                AMS.Model.AMS_AdvertisementSchoolCopy adModel = AMS.ServiceProxy.AdvertisementOperationService.GetSchoolAdvert(AdID);
                if (adModel != null)
                {
                    //上传下载类
                    AMS.ServiceProxy.FileOperate advertfileOperate = new AMS.ServiceProxy.FileOperate();
                    SeatManage.Bll.FileOperate seatmanagefileOperate = new SeatManage.Bll.FileOperate();
                    seatmanagefileOperate.Downloaded += new SeatManage.Bll.EventHandleFileTransport(seatmanagefileOperate_Downloaded);
                    advertfileOperate.DownloadError += new AMS.ServiceProxy.EventHandleFileOperateError(fileOperate_DownloadError);
                    //创建model
                    SeatManage.ClassModel.AMS_Advertisement model = new SeatManage.ClassModel.AMS_Advertisement();
                    model.AdContent = adModel.AdContent;
                    model.EffectDate = adModel.EffectDate;
                    model.EndDate = adModel.EndDate;
                    model.ImageFilePath = SeatManage.ClassModel.AMS_Advertisement.GetDownloadFile(adModel.AdContent);
                    model.Name = adModel.Name;
                    model.Num = adModel.Num;
                    model.Type = (SeatManage.EnumType.AdType)System.Enum.Parse(typeof(SeatManage.EnumType.AdType), adModel.Type.ToString());
                    //图片下载
                    foreach (string file in model.ImageFilePath)
                    {
                        string fileFullName = string.Format(@"{0}{1}", ServiceSet.TempFilePath, file);
                        //开始下载文件
                        if (!File.Exists(fileFullName))
                        {
                            bool downloadResult = false;
                            string error = advertfileOperate.FileDownLoad(fileFullName, file, (SeatManage.EnumType.SeatManageSubsystem)System.Enum.Parse(typeof(SeatManage.EnumType.SeatManageSubsystem), model.Type.ToString()));
                            if (error == "")
                            {
                                downloadResult = true;
                            }
                            else
                            {
                                downloadResult = false;
                            }
                            if (!downloadResult)
                            {
                                //下载失败，直接返回false
                                return false;
                            }
                        }
                    }
                    //图片上传
                    foreach (string file in model.ImageFilePath)
                    {
                        string fileFullName = string.Format(@"{0}{1}", ServiceSet.TempFilePath, file);
                        //文件下载完成，执行上传操作 
                        bool uploadResult = seatmanagefileOperate.UpdateFile(fileFullName, file, (SeatManage.EnumType.SeatManageSubsystem)System.Enum.Parse(typeof(SeatManage.EnumType.SeatManageSubsystem), model.Type.ToString()));
                        if (!uploadResult)
                        {
                            //上传失败，直接返回false，不再尝试其他操作
                            return false;
                        }
                    }
                    SeatManage.ClassModel.AMS_Advertisement sameModel = SeatManage.Bll.AdvertisementOperation.GetAdModel(model.Num, model.Type);
                    string errorMessage = "";
                    if (sameModel == null)
                    {
                        errorMessage = SeatManage.Bll.AdvertisementOperation.AddAdModel(model);
                    }
                    else
                    {
                        model.ID = sameModel.ID;
                        errorMessage = SeatManage.Bll.AdvertisementOperation.UpdateAdModel(model);
                    }
                    if (errorMessage != "")
                    {
                        SeatManage.SeatManageComm.WriteLog.Write(string.Format("更新广告遇到错误：{0}", errorMessage));
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void seatmanagefileOperate_Downloaded(int message)
        {
            SeatManage.SeatManageComm.WriteLog.Write(string.Format("上传播放列表中的文件遇到错误：{0}", message));
        }

        static void fileOperate_DownloadError(string message)
        {
            SeatManage.SeatManageComm.WriteLog.Write(string.Format("下载播放列表中的文件遇到错误：{0}", message));
        }
    }
}
