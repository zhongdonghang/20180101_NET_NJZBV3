using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
namespace AdvertManage.DataTransfer
{
    public class DataTransfer
    {
        #region 方法
        /// <summary>
        /// 命令处理
        /// </summary>
        public void CommandHandle()
        {
            try
            {
                List<Model.AMS_CommandListModel> commandList = AdvertManage.BLL.AMS_CommandBLL.GetCommandListBySchoolNum(ServiceSet.SchoolNums);

                foreach (Model.AMS_CommandListModel model in commandList)
                {

                    bool handleResult = false;
                    model.FinishFlag = Model.Enum.CommandHandleResult.Getting;
                    BLL.AMS_CommandBLL.UpdateFinishFlag(model);
                    switch (model.Command)
                    {
                        case Model.Enum.CommandType.HardAd:
                            try
                            {
                                handleResult = HardAd.GetHardAd(model.CommandId.Value);
                            }
                            catch (Exception ex)
                            {
                                SeatManage.SeatManageComm.WriteLog.Write(string.Format("执行获取硬广命令遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
                                handleResult = false;
                            }
                            break;
                        case Model.Enum.CommandType.Playlist:
                            try
                            {
                                handleResult = Playlist.GetPlaylist(model.CommandId.Value);
                            }
                            catch (Exception ex)
                            {
                                SeatManage.SeatManageComm.WriteLog.Write(string.Format("执行获取播放列表命令遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
                                handleResult = false;
                            }
                            break;
                        case Model.Enum.CommandType.PrintTemplate:
                            try
                            {
                                handleResult = PrintTemplate.GetPrintTemplate(model.CommandId.Value);
                            }
                            catch (Exception ex)
                            {
                                SeatManage.SeatManageComm.WriteLog.Write(string.Format("执行获取打印模版命令遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
                                handleResult = false;
                            }
                            break;
                        case Model.Enum.CommandType.ProgramUpgrade:
                            try
                            {
                                handleResult = ProgramUpgrade.GetUpgrade(model.CommandId.Value);
                            }
                            catch (Exception ex)
                            {
                                SeatManage.SeatManageComm.WriteLog.Write(string.Format("执行程序更新命令遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
                                handleResult = false;
                            }
                            break;
                        case Model.Enum.CommandType.SlipCustomer:
                            try
                            {
                                handleResult = SlipCustomer.GetSlipCustomer(model.CommandId.Value);
                            }
                            catch (Exception ex)
                            {
                                SeatManage.SeatManageComm.WriteLog.Write(string.Format("执行获取优惠券命令遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
                                handleResult = false;
                            }
                            break;
                        case Model.Enum.CommandType.TitleAd:
                            try
                            {
                                handleResult = TitleAd.GetTitleAd(model.CommandId.Value);
                            }
                            catch (Exception ex)
                            {
                                SeatManage.SeatManageComm.WriteLog.Write(string.Format("执行更新冠名命令遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
                                handleResult = false;
                            }
                            break;
                        case Model.Enum.CommandType.Caputre:
                            try
                            {
                                handleResult = Caputre.UpdateCaputre();
                            }
                            catch (Exception ex)
                            {
                                SeatManage.SeatManageComm.WriteLog.Write(string.Format("上传设备截图遇到异常,异常来自{0}，异常信息：", ex.Source, ex.Message));
                                handleResult = false;
                            }
                            break;
                    }
                    try
                    {
                        //操作正确完成，把该条命令更新为完成
                        if (handleResult)
                        {
                            model.FinishFlag = Model.Enum.CommandHandleResult.Success;
                            BLL.AMS_CommandBLL.UpdateFinishFlag(model);
                        }
                        else
                        {
                            model.FinishFlag = Model.Enum.CommandHandleResult.Failed;
                            BLL.AMS_CommandBLL.UpdateFinishFlag(model);
                        }
                    }
                    catch (Exception ex)
                    {
                        SeatManage.SeatManageComm.WriteLog.Write(string.Format("更新命令完成结果遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
                        model.FinishFlag = Model.Enum.CommandHandleResult.Failed;
                        BLL.AMS_CommandBLL.UpdateFinishFlag(model);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }

            

        }
        /// <summary>
        /// 获取该学校的终端信息
        /// </summary>
        public void GetDevice()
        {
            try
            {
                List<Model.AMS_DeviceModel> modelList = BLL.AMS_DeviceBLL.GeDeviceModelBySchoolNum(ServiceSet.SchoolNums, true);
                foreach (Model.AMS_DeviceModel model in modelList)
                {
                    if (model.IsDel.HasValue && model.IsDel.Value)
                    {
                        SeatManage.Bll.AMS_Terminal.DeleteTerminal(model.Number);
                    }
                    else
                    {
                        SeatManage.ClassModel.TerminalInfo terminal = SeatManage.Bll.ClientConfigOperate.GetClientConfig(model.Number);
                        if (terminal != null)
                        {
                            terminal.Describe = model.Describe;
                            terminal.EmpowerLoseEfficacyTime = SeatManage.Bll.ServiceDateTime.Now.AddDays(7);
                            SeatManage.Bll.ClientConfigOperate.UpdateTerminal(terminal);
                        }
                        else
                        {
                            terminal = new SeatManage.ClassModel.TerminalInfo();
                            terminal.Describe = model.Describe;
                            terminal.EmpowerLoseEfficacyTime = SeatManage.Bll.ServiceDateTime.Now.AddDays(7);
                            terminal.ClientNo = model.Number;
                            terminal.IsUpdatePlayList = false;
                            SeatManage.Bll.AMS_Terminal.AddClientSetting(terminal);
                        }
                    }
                    model.Flag = false;
                    AdvertManage.BLL.AMS_DeviceBLL.UpdateDeviceModel(model);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("获取学校终端信息失败：{0}", ex.Message));
            }
        }

        /// <summary>
        /// 授权，如果设置为服务器授权，则不管是否在线都要联网获取授权
        /// </summary>
        public void Empower()
        {
            try
            {
                //如果设置为服务器授权，则不管是否在线都要联网获取授权。
                if (ServiceSet.Empower == EmpowerMode.Server)
                {
                    try
                    {
                        //从Advert服务器上获取时间，以判断 是否能够连接到公司服务器上
                        DateTime? dt = AdvertManage.BLL.ServerDateTime.Now;
                        //如果有值说明能够连接到服务器上，进行授权。
                        if (dt.HasValue)
                        {
                            List<SeatManage.ClassModel.TerminalInfo> terminalList = SeatManage.Bll.ClientConfigOperate.GetTerminalsInfo();
                            foreach (SeatManage.ClassModel.TerminalInfo terminal in terminalList)
                            {
                                terminal.EmpowerLoseEfficacyTime = SeatManage.Bll.ServiceDateTime.Now.AddDays(7);
                                SeatManage.Bll.ClientConfigOperate.UpdateTerminal(terminal);
                            }
                        }
                    }
                    catch { }
                }
                else if (ServiceSet.Empower == EmpowerMode.Local)
                {
                    List<SeatManage.ClassModel.TerminalInfo> terminalList = SeatManage.Bll.ClientConfigOperate.GetTerminalsInfo();
                    foreach (SeatManage.ClassModel.TerminalInfo terminal in terminalList)
                    {
                        terminal.EmpowerLoseEfficacyTime = SeatManage.Bll.ServiceDateTime.Now.AddDays(7);
                        SeatManage.Bll.ClientConfigOperate.UpdateTerminal(terminal);
                    }
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("终端使用授权失败：{0}", ex.Message));
            }
        }

        /// <summary>
        /// 上传设备运行状态
        /// </summary>
        public void UpdateDeviceState()
        {
            try
            { 
                List<SeatManage.ClassModel.TerminalInfo> terminalList = SeatManage.Bll.ClientConfigOperate.GetTerminalsInfo();
                foreach (SeatManage.ClassModel.TerminalInfo terminal in terminalList)
                { 
                    //TODO:
                    AdvertManage.BLL.AMS_DeviceBLL.UpdateDeviceStatus(terminal.ClientNo,terminal.StatusUpdateTime); 
                }
            }
            catch (Exception ex)
            {
              //  SeatManage.SeatManageComm.WriteLog.Write(string.Format("上传设备运行状态失败：{0}", ex.Message));
            }
        }


        /// <summary>
        /// 记录上传表示
        /// </summary>
        bool isUpload = true;
        /// <summary>
        /// 上传记录
        /// </summary>
        /// <returns></returns>
        public void LogUpload()
        {
            //try
            //{
            //    DateTime uploadTime = DateTime.Parse(string.Format("{0} {1}", DateTime.Now.ToShortDateString(), ServiceSet.LogUploadTime));
            //    //同步时间小于当前时间，并大于
            //    if (uploadTime.CompareTo(DateTime.Now) < 0 && isUpload)
            //    {
            //        //进出记录上传
            //        EnterOutLogUpload.Upload();
            //        SlipCustomer.UploadSlipPrintInfo();
            //        isUpload = false;
            //    }
            //    else if (uploadTime.CompareTo(DateTime.Now) > 0)
            //    {
            //        isUpload = true;
            //    }
                
            //}
            //catch (Exception ex)
            //{
            //    SeatManage.SeatManageComm.WriteLog.Write(string.Format("上传刷卡记录失败：{0}", ex.Message));
            //    //执行完成删除一个月以前的日志文件
            //    SeatManage.SeatManageComm.WriteLog.DeleteLog(DateTime.Now.AddMonths(-2));
            //}
        }
        #endregion

    }
}
