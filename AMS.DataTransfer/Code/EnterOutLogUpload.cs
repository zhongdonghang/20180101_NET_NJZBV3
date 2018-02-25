using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AMS.DataTransfer.Code
{
    /// <summary>
    /// 进出记录查询
    /// </summary>
    public class EnterOutLogUpload
    {
        /// <summary>
        /// 上传进出记录
        /// </summary>
        /// <returns></returns>
        public static bool Upload()
        {
            int i = readerMaxId();
            List<SeatManage.ClassModel.EnterOutLogInfo> enterOutLogList = SeatManage.Bll.T_SM_EnterOutLog.GetEnterOutLogInfoGreaterThanId(i, false);
            List<Model.AMS_EnterOutLog> ams_enterOutLogList = new List<Model.AMS_EnterOutLog>();
            int newMaxId = 0;
            foreach (SeatManage.ClassModel.EnterOutLogInfo model in enterOutLogList)
            {
                Model.AMS_EnterOutLog amsModel = new Model.AMS_EnterOutLog();
                amsModel.CardNo = model.CardNo;
                amsModel.EnterOutNo = model.EnterOutLogNo;
                amsModel.EnterOutState = (int)model.EnterOutState;
                amsModel.EnterOutTime = model.EnterOutTime;
                amsModel.EnterOutType = (int)model.EnterOutType;
                amsModel.Operator = (int)model.Flag;
                amsModel.ReadingRoomNum = model.ReadingRoomNo;
                amsModel.Remark = model.Remark;
                amsModel.SeatNo = model.SeatNo;
                amsModel.TerminalNum = model.TerminalNum;
                ams_enterOutLogList.Add(amsModel);
                if (int.Parse(model.EnterOutLogID) > newMaxId)
                {
                    newMaxId = int.Parse(model.EnterOutLogID);
                }
            }
            if (ams_enterOutLogList.Count > 0 && AMS.ServiceProxy.IEnterOutLogService.AddEnterOutLogList(ams_enterOutLogList) == Model.Enum.HandleResult.Successed)
            {
                writeMaxId(newMaxId);
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 写入当前最大的Id
        /// </summary>
        /// <param name="maxId"></param>
        private static void writeMaxId(int maxId)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                FileStream aFile = new FileStream(string.Format("{0}MaxId.dll", path), FileMode.Create);
                StreamWriter sw = new StreamWriter(aFile);
                sw.WriteLine(maxId);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 读取最大的进出记录ID
        /// </summary>
        /// <returns></returns>
        private static int readerMaxId()
        {
            string strLine;
            int maxId = -1;
            try
            {
                string path = string.Format("{0}MaxId.dll", AppDomain.CurrentDomain.BaseDirectory);
                if (File.Exists("MaxId.dll"))
                {
                    FileStream aFile = new FileStream("MaxId.dll", FileMode.Open);
                    StreamReader sr = new StreamReader(aFile);
                    strLine = sr.ReadLine();
                    //Read data in line by line 这个兄台看的懂吧~一行一行的读取   
                    if (strLine != null)
                    {
                        maxId = int.Parse(strLine);
                    }
                    sr.Close();
                    return
                        maxId;
                }
                else
                {
                    return 0;
                }
            }
            catch (IOException ex)
            {
                return -1;
            }
        }

    }
}
