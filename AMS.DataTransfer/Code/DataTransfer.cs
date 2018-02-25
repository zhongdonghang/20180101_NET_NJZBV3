using System;
using System.Collections.Generic;
using System.Linq;
using AMS.Model;
using AMS.Model.Enum;
using AMS.ServiceProxy;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;

namespace AMS.DataTransfer.Code
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
                List<AMS_CommandList> commandList = ICommandListService.GetCommandListBySchoolNum(ServiceSet.SchoolNums);

                foreach (AMS_CommandList model in commandList)
                {

                    bool handleResult = false;
                    model.FinishFlag = Convert.ToInt32(CommandHandleResult.Getting);
                    ICommandListService.UpdateFinishFlag(model);
                    switch (model.Command)
                    {
                        case CommandType.HardAd:
                            try
                            {
                                handleResult = HardAd.GetHardAd(model.CommandId.Value);
                            }
                            catch (Exception ex)
                            {
                                WriteLog.Write(string.Format("执行获取硬广命令遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
                                handleResult = false;
                            }
                            break;
                        case CommandType.Playlist:
                            try
                            {
                                handleResult = Playlist.GetPlaylist(model.CommandId.Value);
                            }
                            catch (Exception ex)
                            {
                                WriteLog.Write(string.Format("执行获取播放列表命令遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
                                handleResult = false;
                            }
                            break;
                        case CommandType.PrintTemplate:
                            try
                            {
                                handleResult = PrintTemplate.GetPrintTemplate(model.CommandId.Value);
                            }
                            catch (Exception ex)
                            {
                                WriteLog.Write(string.Format("执行获取打印模版命令遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
                                handleResult = false;
                            }
                            break;
                        case CommandType.ProgramUpgrade:
                            try
                            {
                                handleResult = ProgramUpgrade.GetUpgrade(model.CommandId.Value);
                            }
                            catch (Exception ex)
                            {
                                WriteLog.Write(string.Format("执行程序更新命令遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
                                handleResult = false;
                            }
                            break;
                        case CommandType.SlipCustomer:
                            try
                            {
                                handleResult = SlipCustomer.GetSlipCustomer(model.CommandId.Value);
                            }
                            catch (Exception ex)
                            {
                                WriteLog.Write(string.Format("执行获取优惠券命令遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
                                handleResult = false;
                            }
                            break;
                        case CommandType.TitleAd:
                            try
                            {
                                handleResult = TitleAd.GetTitleAd(model.CommandId.Value);
                            }
                            catch (Exception ex)
                            {
                                WriteLog.Write(string.Format("执行更新冠名命令遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
                                handleResult = false;
                            }
                            break;
                        case CommandType.Caputre:
                            try
                            {
                                handleResult = Caputre.UpdateCaputre();
                            }
                            catch (Exception ex)
                            {
                                WriteLog.Write(string.Format("上传设备截图遇到异常,异常来自{0}，异常信息：", ex.Source, ex.Message));
                                handleResult = false;
                            }
                            break;
                        case CommandType.RollTitles:
                            try
                            {
                                handleResult = RollTitles.GetTitleAd(model.CommandId.Value);
                            }
                            catch (Exception ex)
                            {
                                WriteLog.Write(string.Format("执行更新冠名命令遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
                                handleResult = false;
                            }
                            break;
                    }
                    try
                    {
                        //操作正确完成，把该条命令更新为完成
                        if (handleResult)
                        {
                            model.FinishFlag = Convert.ToInt32(CommandHandleResult.Success);
                            ICommandListService.UpdateFinishFlag(model);
                        }
                        else
                        {
                            model.FinishFlag = Convert.ToInt32(CommandHandleResult.Failed);
                            ICommandListService.UpdateFinishFlag(model);
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteLog.Write(string.Format("更新命令完成结果遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
                        model.FinishFlag = Convert.ToInt32(CommandHandleResult.Failed);
                        ICommandListService.UpdateFinishFlag(model);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取命令遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
            }



        }
        /// <summary>
        /// 获取该学校的终端信息
        /// </summary>
        public void GetDevice()
        {
            try
            {
                if (ServiceSet.IsOnline)
                {
                    //媒体服务器的设备列表
                    List<AMS_Device> modelList = IDevice.GeDeviceModelBySchoolNum(ServiceSet.SchoolNums, false);
                    Dictionary<string, AMS_Device> ModelDic = new Dictionary<string, AMS_Device>();
                    foreach (AMS_Device item in modelList)
                    {
                        ModelDic.Add(item.Number, item);
                    }
                    //学校的设备列表
                    List<TerminalInfoV2> oldModelList = TerminalOperatorService.GetAllTeminalInfo();
                    Dictionary<string, TerminalInfoV2> oldModelDic = new Dictionary<string, TerminalInfoV2>();
                    foreach (TerminalInfoV2 item in oldModelList)
                    {
                        oldModelDic.Add(item.ClientNo, item);
                    }
                    //判断是否存在num
                    foreach (KeyValuePair<string, AMS_Device> item in ModelDic)
                    {
                        if (oldModelDic.Keys.Contains(item.Key))
                        {
                            if (!item.Value.IsDel.Value)
                            {
                                TerminalInfoV2 terminal = TerminalOperatorService.GetTeminalSetting(item.Value.Number);
                                //terminal.Describe = item.Value.Describe;
                                terminal.EmpowerLoseEfficacyTime = ServiceDateTime.Now.AddDays(7);
                                TerminalOperatorService.UpdateTeminalSetting(terminal);
                            }
                            else
                            {
                                AMS_Terminal.DeleteTerminal(item.Key);
                            }
                        }
                        else
                        {
                            if (!item.Value.IsDel.Value)
                            {
                                TerminalInfoV2 terminal = new TerminalInfoV2();
                                //terminal.Describe = item.Value.Describe;
                                terminal.EmpowerLoseEfficacyTime = ServiceDateTime.Now.AddDays(7);
                                terminal.ClientNo = item.Value.Number;
                                terminal.IsUpdatePlayList = false;
                                TerminalOperatorService.AddTeminalInfo(terminal);
                            }
                        }
                        if (item.Value.Flag.Value)
                        {
                            item.Value.Flag = false;
                            IDevice.UpdateDeviceModel(item.Value);
                        }
                    }
                    //判断是否存在非法No
                    foreach (KeyValuePair<string, TerminalInfoV2> item in oldModelDic)
                    {
                        if (!ModelDic.Keys.Contains(item.Key))
                        {
                            AMS_Terminal.DeleteTerminal(item.Key);
                        }
                    }
                }
                else
                {
                    SystemAuthorization saModel = SeatManage.Bll.SystemAuthorizationOperation.GetSystemAuthorization();
                    if (saModel == null || saModel.SchoolNum != SeatManage.Bll.Registry.GetSchoolNum())
                    {
                        WriteLog.Write("获取授权文件失败！");
                        return;
                    }
                    List<TerminalInfoV2> oldModelList = TerminalOperatorService.GetAllTeminalInfo();
                    Dictionary<string, TerminalInfoV2> oldModelDic = oldModelList.ToDictionary(item => item.ClientNo);
                    foreach (var item in from item in oldModelDic from v in saModel.SeatClientList.Where(v => item.Key == v.Key) select item)
                    {
                        item.Value.EmpowerLoseEfficacyTime = ServiceDateTime.Now.AddDays(7);
                        TerminalOperatorService.UpdateTeminalSetting(item.Value);
                    }
                    foreach (var item in from item in oldModelDic let isExist = saModel.SeatClientList.Any(v => item.Key == v.Key) where !isExist select item)
                    {
                        AMS_Terminal.DeleteTerminal(item.Key);
                    }
                    foreach (var item in from item in saModel.SeatClientList let isExist = oldModelDic.Any(v => item.Key == v.Key) where !isExist select item)
                    {
                        TerminalInfoV2 terminal = new TerminalInfoV2();
                        terminal.EmpowerLoseEfficacyTime = ServiceDateTime.Now.AddDays(7);
                        terminal.ClientNo = item.Key;
                        terminal.IsUpdatePlayList = false;
                        TerminalOperatorService.AddTeminalInfo(terminal);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取学校终端信息失败：{0}", ex.Message));
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
                        DateTime? dt = DateTime.Now;
                        //如果有值说明能够连接到服务器上，进行授权。
                        if (dt.HasValue)
                        {
                            List<TerminalInfoV2> terminalList = TerminalOperatorService.GetAllTeminalInfo();
                            foreach (TerminalInfoV2 terminal in terminalList)
                            {
                                terminal.EmpowerLoseEfficacyTime = ServiceDateTime.Now.AddDays(7);
                                TerminalOperatorService.UpdateTeminalSetting(terminal);
                            }
                        }
                    }
                    catch { }
                }
                else if (ServiceSet.Empower == EmpowerMode.Local)
                {
                    List<TerminalInfoV2> terminalList = TerminalOperatorService.GetAllTeminalInfo();
                    foreach (TerminalInfoV2 terminal in terminalList)
                    {
                        terminal.EmpowerLoseEfficacyTime = ServiceDateTime.Now.AddDays(7);
                        TerminalOperatorService.UpdateTeminalSetting(terminal);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("终端使用授权失败：{0}", ex.Message));
            }
        }

        /// <summary>
        /// 上传设备运行状态
        /// </summary>
        public void UpdateDeviceState()
        {
            try
            {
                List<TerminalInfoV2> terminalList = TerminalOperatorService.GetAllTeminalInfo();
                foreach (TerminalInfoV2 terminal in terminalList)
                {
                    //TODO:
                    IDevice.UpdateDeviceStatus(terminal.ClientNo, terminal.StatusUpdateTime);
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
            try
            {
                DateTime uploadTime = DateTime.Parse(string.Format("{0} {1}", DateTime.Now.ToShortDateString(), ServiceSet.LogUploadTime));
                //同步时间小于当前时间，并大于
                if (uploadTime.CompareTo(DateTime.Now) < 0 && isUpload)
                {
                    //进出记录上传
                    //EnterOutLogUpload.Upload();
                    //SlipCustomer.UploadSlipPrintInfo();
                    if (!UpLoadLog.Upload())
                    {
                        WriteLog.Write("添加记录遇到错误！");
                    }
                    if (!UpLoadAdvertUsage.SendUsage())
                    {
                        WriteLog.Write("添加媒体使用记录遇到错误！");
                    }
                    if (!UploadSeatUsage.GetUsage())
                    {
                        WriteLog.Write("添加使用记录遇到错误！");
                    }
                    isUpload = false;
                }
                else if (uploadTime.CompareTo(DateTime.Now) > 0)
                {
                    isUpload = true;
                }

            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("上传刷卡记录失败：{0}", ex.Message));
                //执行完成删除一个月以前的日志文件
                WriteLog.DeleteLog(DateTime.Now.AddMonths(-2));
            }
        }
        /// <summary>
        /// 新的命令获取
        /// </summary>
        public void GetCommand()
        {
            try
            {
                //UploadReaderInfo.Upload();
                bool handleResult = false;
                List<AMS_IssureList> modelList = new List<AMS_IssureList>();
                modelList = IssuredCommandService.GetSchoolCommand(ServiceSet.SchoolNums);
                foreach (AMS_IssureList command in modelList)
                {
                    command.Flag = (int)CommandHandleResult.Getting;
                    IssuredCommandService.UpdateCommand(command);
                    switch (command.CommandType)
                    {
                        case IsureCommandType.Advertisement:
                            try
                            {
                                //获取广告操作
                                handleResult = GetAdvertisement.Get(command.CommandID);
                            }
                            catch (Exception ex)
                            {
                                WriteLog.Write(string.Format("执行获取广告遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
                                handleResult = false;
                            }
                            break;
                        case IsureCommandType.AdvertUsage:
                            try
                            {
                                handleResult = UpLoadAdvertUsage.SendUsage();
                            }
                            catch (Exception ex)
                            {
                                WriteLog.Write(string.Format("执行获取广告情况遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
                                handleResult = false;
                            }
                            break;
                        case IsureCommandType.EnterOutLog:
                            try
                            {
                                handleResult = UploadSeatUsage.GetUsage();
                            }
                            catch (Exception ex)
                            {
                                WriteLog.Write(string.Format("执行获取使用记录状态遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
                                handleResult = false;
                            }
                            break;
                        case IsureCommandType.State:
                            try
                            {
                            }
                            catch (Exception ex)
                            {
                                WriteLog.Write(string.Format("执行获取使用状态遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
                                handleResult = false;
                            }
                            break;
                        case IsureCommandType.ReaderInfo:
                            try
                            {
                                handleResult = UploadReaderInfo.Upload();
                            }
                            catch (Exception ex)
                            {
                                WriteLog.Write(string.Format("执行获取信息遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
                                handleResult = false;
                            }
                            break;
                    }
                    if (handleResult)
                    {
                        command.Flag = (int)CommandHandleResult.Success;
                        IssuredCommandService.UpdateCommand(command);
                    }
                    else
                    {
                        command.Flag = (int)CommandHandleResult.Failed;
                        IssuredCommandService.UpdateCommand(command);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取命令遇到错误,异常来自{0}，异常信息：", ex.Source, ex.Message));
            }
        }
        #endregion

    }
}
