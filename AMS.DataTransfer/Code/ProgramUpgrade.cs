using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.ServiceProxy;

namespace AMS.DataTransfer.Code
{
    public class ProgramUpgrade
    {
        /// <summary>
        /// 获取更新程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool GetUpgrade(int id)
        {
            Model.ProgramUpgrade programModel = AMS.ServiceProxy.ProjecVersionWindow.GetProgramUpgradeByID(id);
            if (programModel != null)
            {
                FileOperate advertfileOperate = new FileOperate();
                SeatManage.EnumType.SeatManageSubsystem systemType = (SeatManage.EnumType.SeatManageSubsystem)(int)programModel.Application;

                SeatManage.Bll.FileOperate seatmanagefileOperate = new SeatManage.Bll.FileOperate();
                SeatManage.ClassModel.FileUpdateInfo programInfo = SeatManage.ClassModel.FileUpdateInfo.Convert(programModel.AutoUpdaterXml);
                List<string> filePath = programInfo.BuildUpdateFilePaths();
                foreach (string path in filePath)
                {
                    string fullPath = string.Format("{0}{1}", ServiceSet.TempFilePath, path);
                    //文件下载
                    if (advertfileOperate.FileDownLoad(fullPath, path, systemType)!="")
                    {
                        return false;
                    }
                    if (!seatmanagefileOperate.UpdateFile(fullPath, path, systemType))
                    {
                        return false;
                    }
                }
                //执行完成，不需要更新数据库。
                //上传完毕删除本地的文件以及文件夹。
                SeatManage.ClassModel.FileUpdateInfo.DelDirectorys(ServiceSet.TempFilePath);
                return true;
            }
            return true;
        }
    }
}
