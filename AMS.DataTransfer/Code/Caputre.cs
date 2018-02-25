using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AMS.DataTransfer.Code
{
    public class Caputre
    {
        /// <summary>
        /// 上传设备截图
        /// </summary>
        /// <returns></returns>
        public static bool UpdateCaputre()
        {
            try
            {
                List<SeatManage.ClassModel.TerminalInfoV2> terminalList = SeatManage.Bll.TerminalOperatorService.GetAllTeminalInfo();
                SeatManage.Bll.FileOperate seatfileTransport = new SeatManage.Bll.FileOperate();
                AMS.ServiceProxy.FileOperate advertTransport = new AMS.ServiceProxy.FileOperate();
                foreach (SeatManage.ClassModel.TerminalInfoV2 terminal in terminalList)
                {

                    if (!string.IsNullOrEmpty(terminal.ScreenshotPath))
                    {

                        AMS.Model.AMS_Device model = AMS.ServiceProxy.IDevice.GetDevicebyNo(terminal.ClientNo);
                        if (model == null)
                        {
                            return true;//如果媒体服务器上获取不到信息，直接返回true，以便结束命令。
                        }

                        string fileFullName = string.Format("{0}{1}", ServiceSet.TempFilePath, terminal.ScreenshotPath);
                        if (seatfileTransport.FileDownLoad(fileFullName, terminal.ScreenshotPath, SeatManage.EnumType.SeatManageSubsystem.Caputre))
                        {
                            if (advertTransport.UpdateFile(fileFullName, terminal.ScreenshotPath, SeatManage.EnumType.SeatManageSubsystem.Caputre)=="")
                            {
                                File.Delete(fileFullName);
                                //上传完成，更新一下截图路径。
                                model.CaputrePath = terminal.ScreenshotPath;
                                AMS.ServiceProxy.IDevice.UpdateDeviceModel(model);
                            }
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
