using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using SeatManage.EnumType;

namespace AMS.DataTransfer.Code
{
    public class UploadReaderInfo
    {
        public static bool Upload()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                AMS.ServiceProxy.FileOperate fileOperate = new ServiceProxy.FileOperate();
                SeatManage.DAL.T_SM_Reader t_sm_Reader = new SeatManage.DAL.T_SM_Reader();
                DataSet ds = t_sm_Reader.GetList(null, null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sb.Append(ds.Tables[0].Rows[i]["cardId"].ToString());
                    sb.Append(ds.Tables[0].Rows[i]["cardNo"].ToString());
                    sb.Append(ds.Tables[0].Rows[i]["ReaderName"].ToString());
                    sb.Append(ds.Tables[0].Rows[i]["ReaderTypeName"].ToString());
                    sb.Append(ds.Tables[0].Rows[i]["Sex"].ToString());
                    sb.Append(ds.Tables[0].Rows[i]["ReaderDeptName"].ToString());
                    sb.Append(ds.Tables[0].Rows[i]["Flag"].ToString());
                    sb.AppendLine("");
                }
                SaveLog("RI", sb.ToString());
                string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory + "RITmp\\";
                SeatManage.SeatManageComm.ZipUtil.ZipFile(fileDircetoryPath + "RI", fileDircetoryPath + "ZRI");
                //上传文件
                fileOperate.UpdateFile(fileDircetoryPath + "ZRI", ServiceSet.SchoolNums + "_ZRI", SeatManageSubsystem.ReaderInfo);
                //删除缓存文件
                Directory.Delete(fileDircetoryPath, true);
                    
                return true;
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("获取信息失败：{0}", ex.Message));
                return false;
            }
        }

        private static void SaveLog(string txtName, string txt)
        {
            try
            {
                string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory + "RITmp\\";
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
    }
}
