using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using System.Data;
using SeatManage;
using SeatManage.ClassModel;
using System.Data.SqlClient;
using SeatManage.DAL;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="teminalNo"></param>
        /// <returns></returns>
        public TerminalInfoV2 GetTeminalInfo(string teminalNo)
        {
            string strWhere = "DeviceNum = @ClientNo";
            SqlParameter[] parameters = {
                                                new SqlParameter("@ClientNo",teminalNo)
                                            };
            try
            {

                DataSet ds = ams_DeviceStatus.GetList(strWhere, parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    TerminalInfoV2 terminal = DataRowToTerminalInfoNew(ds.Tables[0].Rows[0]);
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
        /// 
        /// </summary>
        /// <param name="teminalNo"></param>
        /// <returns></returns>
        public string AddPrintCount(string teminalNo)
        {
            TerminalInfoV2 model = GetTeminalInfo(teminalNo);
            model.PrintedTimes++;
            try
            {
                return UpdateTeminalInfo(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UpPrintStatus(string teminalNo, SeatManage.EnumType.Printer printerStatus)
        {
            throw new NotImplementedException();
        }



        public string UpdateTeminalInfo(TerminalInfoV2 teminal)
        {
            AMS_DeviceStatus dal = new AMS_DeviceStatus();
            try
            {
                if (dal.Update(teminal) > 0)
                {
                    return "";
                }
                else
                {
                    return "修改失败";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int LastSeatCount(List<string> list)
        {
            int count = 0;
            Dictionary<string, ReadingRoomSeatUsedState> roomStatus = GetRoomSeatUsedState(list);
            foreach (KeyValuePair<string, ReadingRoomSeatUsedState> item in roomStatus)
            {
                count += item.Value.SeatAmountAll - item.Value.SeatAmountUsed - item.Value.SeatBookingCount + item.Value.SeatTemUseCount;
            }
            return count;
        }

        private TerminalInfoV2 DataRowToTerminalInfoNew(DataRow dr)
        {
            TerminalInfoV2 terminal = new TerminalInfoV2();
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
                terminal.DeviceSetting = ClientConfigV2.Convert(tempFile);
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
            return terminal;
        }

        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <returns></returns>
        public List<TerminalInfoV2> GetAllTeminalInfo()
        {
            List<TerminalInfoV2> modelList = new List<TerminalInfoV2>();
            try
            {

                DataSet ds = ams_DeviceStatus.GetList(null, null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(DataRowToTerminalInfoNew(ds.Tables[0].Rows[i]));
                }
                return modelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="teminal"></param>
        /// <returns></returns>
        public string AddTeminalInfo(TerminalInfoV2 teminal)
        {
            return ams_DeviceStatus.Add(teminal) > 0 ? "" : "添加失败";
        }

        /// <summary>
        /// 获取剩余的座位
        /// </summary>
        /// <param name="roomList"></param>
        /// <returns></returns>
        public Dictionary<string, ReadingRoomSeatUsedState_Ex> GetTeminaRoomStatus(List<string> roomNums)
        {
            List<ReadingRoomInfo> rooms = GetReadingRoomInfo(roomNums);
            List<EnterOutLogInfo> roomEnterOutLoglist = GetRoomUsingSeatEnterOutLogInfo(roomNums);
            List<BespeakLogInfo> roomBespeakLogs = GetBespeakLogInfoByRoomsNum(roomNums, GetServerDateTime());
            Dictionary<string, ReadingRoomSeatUsedState_Ex> list = new Dictionary<string, ReadingRoomSeatUsedState_Ex>();
            for (int i = 0; i < rooms.Count; i++)
            {
                ReadingRoomSeatUsedState_Ex usedState = new ReadingRoomSeatUsedState_Ex();
                usedState.ReadingRoom = rooms[i];
                usedState.SeatAmountAll = rooms[i].SeatList.Seats.Count(u => u.Value.IsSuspended != true);
                List<EnterOutLogInfo> enterOutLoglist = roomEnterOutLoglist.FindAll(u => u.ReadingRoomNo == rooms[i].No);
                List<BespeakLogInfo> bespeakLogs = roomBespeakLogs.FindAll(u => u.ReadingRoomNo == rooms[i].No);
                usedState.SeatAmountShortLeave = enterOutLoglist.Count(u => u.EnterOutState == SeatManage.EnumType.EnterOutLogType.ShortLeave);
                for (int j = 0; j < enterOutLoglist.Count; j++)
                {
                    if (bespeakLogs.Find(u => u.SeatNo == enterOutLoglist[j].SeatNo) != null)
                    {
                        usedState.SeatTemUseCount += 1;
                        break;
                    }
                }
                usedState.SeatBookingCount = bespeakLogs.Count;
                usedState.SeatAmountUsed = enterOutLoglist.Count;
                list.Add(rooms[i].No, usedState);
            }
            return list;
        }
    }
}
