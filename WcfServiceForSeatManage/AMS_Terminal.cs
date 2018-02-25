using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.ClassModel;
using System.Data;
using System.Data.SqlClient;
using SeatManage.DAL;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        #region 终端相关操作
        private AMS_DeviceStatus ams_DeviceStatus = new AMS_DeviceStatus();
        public List<TerminalInfo> GetAllTerminals()
        {
            List<TerminalInfo> clients = new List<TerminalInfo>();
            using (DataSet ds = ams_DeviceStatus.GetList(null, null))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    TerminalInfo terminal = DataRowToTerminalInfo(ds.Tables[0].Rows[i]);
                    clients.Add(terminal);
                }
            }
            return clients;
        }
        public TerminalInfo GetTerminalInfo(string clientNo)
        {
            string strWhere = "DeviceNum = @ClientNo";
            SqlParameter[] parameters = {
                                                new SqlParameter("@ClientNo",clientNo)
                                            };
            try
            {

                DataSet ds = ams_DeviceStatus.GetList(strWhere, parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    TerminalInfo terminal = DataRowToTerminalInfo(ds.Tables[0].Rows[0]);
                    return terminal;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加终端
        /// </summary>
        /// <param name="clientConfig"></param>
        /// <returns></returns>
        public int AddClientSetting(TerminalInfo clientConfig)
        {
            return ams_DeviceStatus.Add(clientConfig);

        }
        /// <summary>
        /// 更新终端设置
        /// </summary>
        /// <param name="clientConfig"></param>
        /// <returns></returns>
        int ISeatManageService.UpdateClient(TerminalInfo clientConfig)
        {
            //clientConfig.StatusUpdateTime = GetServerDateTime();
            return ams_DeviceStatus.Update(clientConfig);
        }
        /// <summary>
        /// 根据编号删除终端
        /// </summary>
        /// <param name="clintNum"></param>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult DeleteTerminal(string clintNum)
        {
            if (ams_DeviceStatus.Delete(clintNum) > 0)
            {
                return SeatManage.EnumType.HandleResult.Successed;
            }
            else
            {
                return SeatManage.EnumType.HandleResult.Failed;
            }
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 把行转换为对应的实体。
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private TerminalInfo DataRowToTerminalInfo(DataRow dr)
        {
            TerminalInfo terminal = new TerminalInfo();
            terminal.ClientNo = dr["DeviceNum"].ToString();
            terminal.TerminalMacAddress = dr["TerminalMacAddress"].ToString();
            terminal.ScreenshotPath = dr["ScreenshotPath"].ToString();
            string tempFile = dr["Date"].ToString();
            if (!string.IsNullOrEmpty(tempFile))
            {
                terminal.StatusUpdateTime = DateTime.Parse(tempFile);
            }
            terminal.IsUpdatePlayList = (bool)dr["IsUpdatePlayList"];
            terminal.Describe = dr["Describe"].ToString();
            tempFile = dr["DeviceSetting"].ToString();
            if (!string.IsNullOrEmpty(tempFile))
            {
                terminal.DeviceSetting = ClientConfig.Convert(tempFile);
            }

            tempFile = dr["EmpowerLoseEfficacyTime"].ToString();
            if (!string.IsNullOrEmpty(tempFile))
            {
                terminal.EmpowerLoseEfficacyTime = DateTime.Parse(tempFile);
            }
            if (dr["LastPrintTimes"] == DBNull.Value)
            {
                terminal.LastPrintTimes = 0;
            }
            else
            {
                terminal.LastPrintTimes = int.Parse(dr["LastPrintTimes"].ToString());
            }
            if (dr["PrintedTimes"] == DBNull.Value)
            {
                terminal.PrintedTimes = 0;
            }
            else
            {
                terminal.PrintedTimes = int.Parse(dr["PrintedTimes"].ToString());
            }
            if (dr["PrinterStatus"] == DBNull.Value)
            {
                terminal.PrinterStatus = false;
            }
            else
            {
                terminal.PrinterStatus = Convert.ToBoolean(dr["PrinterStatus"].ToString());
            }
            //HardAdvertInfo hardAdvert = GetHardAdvert();//获取有效的硬广
            //if (hardAdvert != null)
            //{
            //    terminal.HardAdImage = hardAdvert.AdvertImage;
            //}
            TitleAdvertInfo titleAdvert = GetTitleAdvertInfo();//获取冠名广告
            if (titleAdvert != null)
            {
                terminal.TitleAd = titleAdvert.TitleAdvert;
            }
            return terminal;
        }
        #endregion

    }
}
