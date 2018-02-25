using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.EnumType;

namespace AMS.DataTransfer.Code
{
    public class UpLoadLog
    {
        /// <summary>
        /// 开始计算
        /// </summary>
        public static bool Upload()
        {
            try
            {
                DateTime sdt;
                string lastdate = GetLastDate();
                if (!string.IsNullOrEmpty(lastdate))
                {
                    sdt = DateTime.Parse(lastdate);
                }
                else
                {
                    sdt = SeatManage.Bll.T_SM_EnterOutLog_bak.GetFristLogDate();
                }
                if (sdt <= DateTime.Parse("2000-1-1"))
                {
                    return true;
                }
                AMS.ServiceProxy.FileOperate fileOperate = new ServiceProxy.FileOperate();
                sdt = sdt.AddDays(1);
                while (true)
                {
                    //获取进出记录
                    List<EnterOutLogInfo> enterOutLogList = T_SM_EnterOutLog_bak.GetStatisticsLogsByDate(sdt);
                    List<BespeakLogInfo> bespeakLogList = T_SM_SeatBespeak.GetBespeakList(null, null, sdt, 0, null);
                    List<ViolationRecordsLogInfo> violationLogList = T_SM_ViolateDiscipline.GetViolationRecords(null, null, sdt.ToShortDateString(), sdt.Date.AddDays(1).AddSeconds(-1).ToString(), LogStatus.None, LogStatus.None);
                    List<BlackListInfo> blacklistList = T_SM_Blacklist.GetAllBlackListInfo(null, LogStatus.None, sdt.ToShortDateString(), sdt.Date.AddDays(1).AddSeconds(-1).ToString());
                    if (enterOutLogList.Count <= 0 && bespeakLogList.Count <= 0 && violationLogList.Count <= 0 && sdt >= ServiceDateTime.Now.Date.AddDays(-1))
                    {
                        break;
                    }
                    StringBuilder eolsb = new StringBuilder();
                    StringBuilder blisb = new StringBuilder();
                    StringBuilder vrisb = new StringBuilder();
                    StringBuilder bllsb = new StringBuilder();
                    foreach (EnterOutLogInfo eol in enterOutLogList)
                    {
                        //记录内容
                        eolsb.Append(eol.EnterOutLogID + ",");
                        eolsb.Append(eol.EnterOutLogNo + ",");
                        eolsb.Append(eol.EnterOutState + ",");
                        eolsb.Append(eol.EnterOutTime + ",");
                        //座位信息
                        eolsb.Append(eol.TerminalNum + ",");
                        eolsb.Append(eol.SeatNo + ",");
                        eolsb.Append(eol.ReadingRoomNo + ",");
                        eolsb.Append(eol.ReadingRoomName + ",");
                        //备注
                        eolsb.Append(eol.Flag + ",");
                        eolsb.Append(eol.Remark + ",");
                        //读者信息
                        eolsb.Append(eol.CardNo + ",");
                        eolsb.Append(eol.ReaderName + ",");
                        eolsb.Append(eol.Sex + ",");
                        vrisb.Append(eol.DeptName + ",");
                        vrisb.Append(eol.TypeName + ";");
                        eolsb.AppendLine();
                    }
                    foreach (BespeakLogInfo bli in bespeakLogList)
                    {
                        blisb.Append(bli.BsepeaklogID + ",");
                        blisb.Append(bli.BsepeakTime + ",");
                        blisb.Append(bli.BsepeakState + ",");
                        blisb.Append(bli.SubmitTime + ",");
                        blisb.Append(bli.CancelPerson + ",");
                        blisb.Append(bli.CancelTime + ",");

                        blisb.Append(bli.SeatNo + ",");
                        blisb.Append(bli.ReadingRoomNo + ",");
                        blisb.Append(bli.ReadingRoomName + ",");

                        blisb.Append(bli.Remark + ",");

                        blisb.Append(bli.CardNo + ",");
                        blisb.Append(bli.ReaderName + ",");
                        blisb.Append(bli.Sex + ",");
                        vrisb.Append(bli.DeptName + ",");
                        vrisb.Append(bli.TypeName + ";");
                        blisb.AppendLine();
                    }
                    foreach (ViolationRecordsLogInfo vri in violationLogList)
                    {
                        vrisb.Append(vri.ID + ",");
                        vrisb.Append(vri.BlacklistID + ",");
                        vrisb.Append(vri.WarningState + ",");
                        vrisb.Append(vri.EnterFlag + ",");
                        vrisb.Append(vri.EnterOutTime + ",");
                        vrisb.Append(vri.Flag + ",");

                        vrisb.Append(vri.SeatID + ",");
                        vrisb.Append(vri.ReadingRoomID + ",");
                        vrisb.Append(vri.ReadingRoomName + ",");

                        vrisb.Append(vri.Remark + ",");

                        vrisb.Append(vri.CardNo + ",");
                        vrisb.Append(vri.ReaderName + ",");
                        vrisb.Append(vri.Sex + ",");
                        vrisb.Append(vri.DeptName + ",");
                        vrisb.Append(vri.TypeName + ";");
                        vrisb.AppendLine();
                    }
                    foreach (BlackListInfo bli in blacklistList)
                    {
                        bllsb.Append(bli.ID + ",");
                        bllsb.Append(bli.AddTime + ",");
                        bllsb.Append(bli.BlacklistState + ",");
                        bllsb.Append(bli.OutBlacklistMode + ",");
                        bllsb.Append(bli.OutTime + ",");

                        bllsb.Append(bli.ReadingRoomID + ",");
                        bllsb.Append(bli.ReadingRoomName + ",");

                        bllsb.Append(bli.ReMark + ",");

                        bllsb.Append(bli.CardNo + ",");
                        bllsb.Append(bli.ReaderName + ",");
                        bllsb.Append(bli.Sex + ",");
                        bllsb.Append(bli.DeptName + ",");
                        bllsb.Append(bli.TypeName + ";");
                        bllsb.AppendLine();
                    }
                    try
                    {
                        string filedateName = sdt.Year + "-" + sdt.Month + "-" + sdt.Day;
                        SaveLog("EOL_" + filedateName, eolsb.ToString());
                        SaveLog("BSL_" + filedateName, blisb.ToString());
                        SaveLog("VRL_" + filedateName, vrisb.ToString());
                        SaveLog("BLL_" + filedateName, bllsb.ToString());
                        //压缩文件
                        string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory + "StatisticsTmp\\";
                        SeatManage.SeatManageComm.ZipUtil.ZipFile(fileDircetoryPath + "EOL_" + filedateName, fileDircetoryPath + "ZEOL_" + filedateName);
                        SeatManage.SeatManageComm.ZipUtil.ZipFile(fileDircetoryPath + "BSL_" + filedateName, fileDircetoryPath + "ZBSL_" + filedateName);
                        SeatManage.SeatManageComm.ZipUtil.ZipFile(fileDircetoryPath + "VRL_" + filedateName, fileDircetoryPath + "ZVRL_" + filedateName);
                        SeatManage.SeatManageComm.ZipUtil.ZipFile(fileDircetoryPath + "BLL_" + filedateName, fileDircetoryPath + "ZBLL_" + filedateName);
                        //上传文件
                        fileOperate.UpdateFile(fileDircetoryPath + "ZEOL_" + filedateName, ServiceSet.SchoolNums + "_ZEOL_" + filedateName, SeatManageSubsystem.EnterOutLog);
                        fileOperate.UpdateFile(fileDircetoryPath + "ZBSL_" + filedateName, ServiceSet.SchoolNums + "_ZBSL_" + filedateName, SeatManageSubsystem.BespeakLog);
                        fileOperate.UpdateFile(fileDircetoryPath + "ZVRL_" + filedateName, ServiceSet.SchoolNums + "_ZVRL_" + filedateName, SeatManageSubsystem.ViolateDiscipline);
                        fileOperate.UpdateFile(fileDircetoryPath + "ZBLL_" + filedateName, ServiceSet.SchoolNums + "_ZBLL_" + filedateName, SeatManageSubsystem.Blistlist);
                        //删除缓存文件
                        Directory.Delete(fileDircetoryPath, true);
                        SetLastDate(sdt.ToShortDateString());
                        sdt = sdt.AddDays(1);
                        if (sdt >= ServiceDateTime.Now.Date)
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        SeatManage.SeatManageComm.WriteLog.Write(string.Format("统计{0}记录失败：{1}", sdt.ToShortDateString(), ex.Message));
                        return false;
                    }
                }
                return true;

            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("统计记录失败：{0}", ex.Message));
                return false;
            }
        }

        private static void SaveLog(string txtName, string txt)
        {
            try
            {
                string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory + "StatisticsTmp\\";
                if (!Directory.Exists(fileDircetoryPath))
                {
                    Directory.CreateDirectory(fileDircetoryPath);
                }
                else
                {
                    SeatManage.SeatManageComm.FileReadOnly.RemovingReadOnly(fileDircetoryPath);
                }
                string filePath = string.Format("{0}{1}", fileDircetoryPath, txtName);
                string strLogFilePath = filePath;
                File.WriteAllText(strLogFilePath, txt);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("保存日志失败：{0}", ex.Message));
            }
        }



        private static void SetLastDate(string lastDate)
        {
            try
            {
                string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = string.Format("{0}StatictiscLastDate.dll", fileDircetoryPath);
                string strLogFilePath = filePath;
                File.WriteAllText(strLogFilePath, lastDate.ToString());
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("保存最大ID失败：{0}", ex.Message));
            }
        }

        private static string GetLastDate()
        {
            try
            {
                string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = string.Format("{0}StatictiscLastDate.dll", fileDircetoryPath);
                string strLogFilePath = filePath;
                if (File.Exists(strLogFilePath))
                {
                    string password = File.ReadAllText(strLogFilePath);
                    return password;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("获取最大ID失败：{0}", ex.Message));
                throw ex;
            }
        }
    }
}
