using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;
using SeatManage.EnumType;
using SeatManage.JsonModel;

/**
 * 该接口可实现Pad管理端的一系列操作：
 *    
 * **/

namespace SeatManage.ServiceHelper
{
    public partial class ServiceHelper : IPadManage
    {
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>登录成功返回用户信息，登录失败返回错误信息</returns>
        public string AdminLogin(string loginId, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(loginId.Trim()) || string.IsNullOrEmpty(password.Trim()))
                {
                    JM_HandleResultObject result = new JM_HandleResultObject();
                    result.Result = false;
                    result.Msg = "用户名或密码不能为空!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                JM_User user = null;
                SeatManage.ClassModel.UserInfo userInfo = seatDataService.GetUserInfo(loginId);
                if (userInfo != null)
                {
                    string strPwd = SeatManageComm.MD5Algorithm.GetMD5Str32(password);
                    if (strPwd.Equals(userInfo.Password))
                    {
                        user = new JM_User();
                        user.LoginId = userInfo.LoginId;
                        user.UserName = userInfo.UserName;
                        if (userInfo.UserType == SeatManage.EnumType.UserType.Admin)
                        {
                            JM_HandleResultObject result = new JM_HandleResultObject();
                            result.Result = true;
                            result.Msg = user;
                            return SeatManageComm.JSONSerializer.Serialize(result);
                        }
                        else
                        {
                            JM_HandleResultObject result = new JM_HandleResultObject();
                            result.Result = false;
                            result.Msg = "您不具备管理员权限！请使用管理员账号登录";
                            return SeatManageComm.JSONSerializer.Serialize(result);
                        }

                    }
                    else
                    {
                        JM_HandleResultObject result = new JM_HandleResultObject();
                        result.Result = false;
                        result.Msg = "用户名或密码错误!";
                        return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                    }
                }
                else
                {
                    JM_HandleResultObject result = new JM_HandleResultObject();
                    result.Result = false;
                    result.Msg = "用户名或密码错误!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("登录遇到异常：" + ex.Message);
                JM_HandleResultObject result = new JM_HandleResultObject();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }

        /// <summary>
        /// 获取管理员管理的阅览室
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <returns>返回阅览室列表</returns>
        public string GetManagerPotencyReadingRoom(string loginId)
        {
            try
            {
                ManagerPotency modelManagerPotency = seatDataService.GetManagerPotencyByLoginID(loginId);

                List<ReadingRoomInfo> rightRoomList = modelManagerPotency.RightRoomList;
                List<string> roomNoList = new List<string>();
                foreach (ReadingRoomInfo room in rightRoomList)
                {
                    string no = "";
                    no = room.No;
                    roomNoList.Add(no);
                }

                List<JM_ReadingRoom> rooms = new List<JM_ReadingRoom>();
                if (roomNoList.Count > 0)
                {
                    List<SeatManage.ClassModel.ReadingRoomInfo> roomListModel = seatDataService.GetReadingRoomInfo(roomNoList);
                    for (int i = 0; i < roomListModel.Count; i++)
                    {
                        JM_ReadingRoom roomInfo = new JM_ReadingRoom();
                        roomInfo.RoomNum = roomListModel[i].No;
                        roomInfo.RoomName = roomListModel[i].Name;

                        //TODO:添加阅览室设置属性 

                        //if (!String.IsNullOrEmpty(dr["ReadingSetting"].ToString()))
                        //{
                        //    roomInfo.Setting = new ReadingRoomSetting(dr["ReadingSetting"].ToString());
                        //}
                        //else
                        //{
                        //    roomInfo.Setting = new ReadingRoomSetting();
                        //}

                        rooms.Add(roomInfo);
                    }
                    JM_HandleResultObject result = new JM_HandleResultObject();
                    result.Result = true;
                    result.Msg = rooms;
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                else
                {
                    JM_HandleResultObject result = new JM_HandleResultObject();
                    result.Result = false;
                    result.Msg = "当前没有可管理的阅览室";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("获取管理的阅览室列表异常：" + ex.Message);
                JM_HandleResultObject result = new JM_HandleResultObject();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }

        /// <summary>
        /// 根据阅览室编号、座位状态获取对应的座位
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <param name="seatState">座位状态</param>
        /// <returns></returns>
        public string GetRoomSeats(string roomNum, string seatState)
        {
            try
            {
                List<JM_Seat> jm_list = new List<JM_Seat>();
                SeatLayout seatList = seatDataService.GetRoomSeatLayOut(roomNum);

                //获取阅览室座位
                foreach (Seat seat in seatList.Seats.Values)
                {
                    JM_Seat jmSeat = new JM_Seat();
                    jmSeat.SeatNo = seat.SeatNo;
                    jmSeat.ShortSeatNo = seat.ShortSeatNo;
                    jmSeat.SeatState = seat.SeatUsedState.ToString();
                    jmSeat.ReadingRoomNum = seat.ReadingRoomNum;
                    jmSeat.UserName = seat.UserName;
                    jmSeat.UserCardNo = seat.UserCardNo;
                    jmSeat.MarkTime = seat.MarkTime.ToString();
                    jmSeat.BeginUsedTime = seat.BeginUsedTime.ToString();
                    if (seat.SeatUsedState.ToString() == seatState)
                    {
                        jm_list.Add(jmSeat);
                    }
                }
                if (jm_list.Count > 0)
                {
                    JM_HandleResultObject result = new JM_HandleResultObject();
                    result.Result = true;
                    result.Msg = jm_list;
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                else
                {
                    JM_HandleResultObject result = new JM_HandleResultObject();
                    result.Result = false;
                    result.Msg = string.Format("没有查询到{0}状态下的座位", seatState);
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("根据阅览室编号获座位者信息遇到异常：" + ex.Message);
                JM_HandleResultObject result = new JM_HandleResultObject();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }

        /// <summary>
        /// 管理员对座位进行管理操作
        /// </summary>
        /// <param name="seatNoList">多选座位列表</param>
        /// <param name="operateType">操作类型</param>
        /// <param name="loginId">登录名</param>
        /// <returns></returns>
        public string SeatOperation(string seatNoList, string operateType, string loginId)
        {
            try
            {
                JM_HandleResultObject result = new JM_HandleResultObject();
                result.Result = false;
                List<JM_Seat> list = SeatManage.SeatManageComm.JSONSerializer.Deserialize<List<JM_Seat>>(seatNoList);
                int successResult = 0;
                int failResult = 0;
                List<string> noList = new List<string>();
                string no = list[0].ReadingRoomNum;
                noList.Add(no);
                List<ReadingRoomInfo> room = seatDataService.GetReadingRoomInfo(noList);

                switch (operateType)
                {
                    #region 设置暂离
                    case "shortLeave":
                        try
                        {
                            foreach (JM_Seat seat in list)
                            {
                                EnterOutLogInfo model = seatDataService.GetEnterOutLogInfoBySeatNum(seat.SeatNo);
                                if (model != null && model.EnterOutState != SeatManage.EnumType.EnterOutLogType.ShortLeave)
                                {
                                    model.EnterOutState = SeatManage.EnumType.EnterOutLogType.ShortLeave;
                                    model.Flag = SeatManage.EnumType.Operation.Admin;
                                    model.Remark = "在" + model.ReadingRoomName + "，" + model.SeatNo + "号座位，被管理员" + loginId + "，通过手持设备设置为暂离";
                                    int newId = -1;
                                    SeatManage.EnumType.HandleResult rs = seatDataService.AddEnterOutLogInfo(model, ref newId);
                                    if (rs == SeatManage.EnumType.HandleResult.Successed)
                                    {
                                        successResult++;
                                        result.Result = true;
                                    }
                                    else
                                    {
                                        failResult++;
                                    }
                                }
                            }
                            result.Msg = "设置读者暂离";
                        }
                        catch (Exception ex)
                        {
                            SeatManage.SeatManageComm.WriteLog.Write("设置读者暂离遇到异常：" + ex.Message);
                            result.Result = false;
                            result.Msg = "执行遇到异常!";
                        }
                        break;
                    #endregion

                    #region 取消暂离
                    case "comeBack":
                        try
                        {
                            foreach (JM_Seat seat in list)
                            {
                                EnterOutLogInfo model = seatDataService.GetEnterOutLogInfoBySeatNum(seat.SeatNo);
                                if (model != null && model.EnterOutState == EnterOutLogType.ShortLeave)
                                {
                                    model.EnterOutState = EnterOutLogType.ComeBack;
                                    model.Flag = Operation.Admin;
                                    model.Remark = "在" + model.ReadingRoomName + "，" + model.SeatNo + "号座位，被管理员" + loginId + "，通过手持设备取消暂离，恢复为在座";
                                    int newId = -1;
                                    SeatManage.EnumType.HandleResult rs = seatDataService.AddEnterOutLogInfo(model, ref newId);
                                    if (rs == SeatManage.EnumType.HandleResult.Successed)
                                    {
                                        List<SeatManage.ClassModel.WaitSeatLogInfo> logs = seatDataService.GetWaitLogList("", model.EnterOutLogID, null, null, null);
                                        WaitSeatLogInfo log = null;
                                        if (logs.Count > 0)
                                        {
                                            log = logs[0];
                                            log.NowState = LogStatus.Fail;
                                            log.OperateType = Operation.OtherReader;
                                            log.WaitingState = EnterOutLogType.WaitingCancel;
                                            if (seatDataService.UpdateWaitLog(log))
                                            {
                                                //result.Result = true;
                                                //result.Msg = "取消读者暂离成功";
                                                successResult++;
                                                result.Result = true;
                                            }
                                            else
                                            {
                                                //result.Result = true;
                                                //result.Msg = "取消读者暂离成功，释放读者等待失败";
                                                successResult++;
                                                result.Result = true;
                                            }
                                        }
                                        else
                                        {
                                            //result.Result = true;
                                            //result.Msg = "取消读者暂离成功";
                                            successResult++;
                                            result.Result = true;
                                        }
                                    }
                                    else
                                    {
                                        //result.Result = false;
                                        //result.Msg = "取消读者暂离失败";
                                        failResult++;
                                    }
                                }
                            }
                            result.Msg = "取消读者暂离";
                        }
                        catch (Exception ex)
                        {
                            SeatManage.SeatManageComm.WriteLog.Write("取消读者暂离遇到异常：" + ex.Message);
                            result.Result = false;
                            result.Msg = "执行遇到异常!";
                        }
                        break;
                    #endregion

                    #region 释放座位
                    case "leave":
                        try
                        {
                            foreach (JM_Seat seat in list)
                            {
                                EnterOutLogInfo model = seatDataService.GetEnterOutLogInfoBySeatNum(seat.SeatNo);
                                if (model != null && model.EnterOutState != EnterOutLogType.Leave)
                                {
                                    model.EnterOutState = EnterOutLogType.Leave;
                                    model.Flag = Operation.Admin;
                                    model.Remark = "在" + model.ReadingRoomName + "，" + model.SeatNo + "号座位，被管理员" + loginId + "，通过手持设备设置离开";
                                    int newId = -1;
                                    HandleResult rs = seatDataService.AddEnterOutLogInfo(model, ref newId);
                                    if (rs == HandleResult.Successed)
                                    {
                                        SeatManage.ClassModel.RegulationRulesSetting rules = seatDataService.GetRegulationRulesSetting();
                                        if (room[0].Setting.IsRecordViolate)
                                        {
                                            if (room[0].Setting.BlackListSetting.Used)
                                            {
                                                if (room[0].Setting.BlackListSetting.ViolateRoule[ViolationRecordsType.LeaveByAdmin])
                                                {
                                                    ViolationRecordsLogInfo logs = new ViolationRecordsLogInfo();
                                                    logs.CardNo = model.CardNo;
                                                    logs.SeatID = model.SeatNo.Substring(model.SeatNo.Length - room[0].Setting.SeatNumAmount, room[0].Setting.SeatNumAmount);
                                                    logs.ReadingRoomID = model.ReadingRoomNo;
                                                    logs.EnterOutTime = DateTime.Now.ToString();
                                                    logs.EnterFlag = ViolationRecordsType.LeaveByAdmin;
                                                    logs.Remark = string.Format("在{0}，{1}号座位，被管理员{2}，通过手持设备设置离开", room[0].Name, model.ShortSeatNo, loginId);
                                                    logs.BlacklistID = "-1";
                                                    seatDataService.AddViolationRecordsLog(logs);
                                                }
                                            }
                                            else if (rules.BlacklistSet.Used && rules.BlacklistSet.ViolateRoule[ViolationRecordsType.LeaveByAdmin])
                                            {
                                                ViolationRecordsLogInfo logs = new ViolationRecordsLogInfo();
                                                logs.CardNo = model.CardNo;
                                                logs.SeatID = model.SeatNo.Substring(model.SeatNo.Length - room[0].Setting.SeatNumAmount, room[0].Setting.SeatNumAmount);
                                                logs.ReadingRoomID = model.ReadingRoomNo;
                                                logs.EnterOutTime = DateTime.Now.ToString();
                                                logs.EnterFlag = ViolationRecordsType.LeaveByAdmin;
                                                logs.Remark = string.Format("在{0}，{1}号座位，被管理员{2}，通过手持设备设置离开", room[0].Name, model.ShortSeatNo, loginId);
                                                logs.BlacklistID = "-1";
                                                seatDataService.AddViolationRecordsLog(logs);
                                            }
                                        }
                                        result.Result = true;
                                        //result.Msg = "成功释放读者座位";
                                        successResult++;
                                    }
                                    else
                                    {
                                        //result.Result = false;
                                        //result.Msg = "释放读者座位失败";
                                        failResult++;
                                    }
                                }
                            }
                            result.Msg = "释放读者座位";
                        }
                        catch (Exception ex)
                        {
                            SeatManage.SeatManageComm.WriteLog.Write("释放读者座位遇到异常：" + ex.Message);
                            result.Result = false;
                            result.Msg = "执行遇到异常!";
                        }
                        break;
                    #endregion

                    #region 加入计时
                    case "timing":
                        try
                        {
                            foreach (JM_Seat seat in list)
                            {
                                EnterOutLogInfo model = seatDataService.GetEnterOutLogInfoBySeatNum(seat.SeatNo);
                                if (model != null && model.EnterOutState != EnterOutLogType.ShortLeave)
                                {
                                    DateTime markTime = DateTime.Now;
                                    if (seatDataService.UpdateMarkTime(model.EnterOutLogID, markTime))
                                    {
                                        //result.Result = true;
                                        //result.Msg = "加入计时成功";
                                        successResult++;
                                        result.Result = true;
                                    }
                                    else
                                    {
                                        //result.Result = false;
                                        //result.Msg = "加入计时失败";
                                        failResult++;
                                    }
                                }
                            }
                            result.Msg = "加入计时";
                        }
                        catch (Exception ex)
                        {
                            SeatManage.SeatManageComm.WriteLog.Write("加入计时遇到异常：" + ex.Message);
                            result.Result = false;
                            result.Msg = "执行遇到异常!";
                        }
                        break;
                    #endregion

                    #region 取消计时
                    case "cancelTiming":
                        try
                        {
                            foreach (JM_Seat seat in list)
                            {
                                EnterOutLogInfo model = seatDataService.GetEnterOutLogInfoBySeatNum(seat.SeatNo);
                                if (model != null && !string.IsNullOrEmpty(model.MarkTime.ToString()) && model.MarkTime.CompareTo(DateTime.Parse("1900/1/1")) != 0)
                                {
                                    DateTime markTime = DateTime.Parse("1900/1/1");
                                    if (seatDataService.UpdateMarkTime(model.EnterOutLogID, markTime))
                                    {
                                        //result.Result = true;
                                        //result.Msg = "取消计时成功";
                                        successResult++;
                                        result.Result = true;
                                    }
                                    else
                                    {
                                        //result.Result = false;
                                        //result.Msg = "取消计时失败";
                                        failResult++;
                                    }
                                }
                            }
                            result.Msg = "取消计时";
                        }
                        catch (Exception ex)
                        {
                            SeatManage.SeatManageComm.WriteLog.Write("取消计时遇到异常：" + ex.Message);
                            result.Result = false;
                            result.Msg = "执行遇到异常!";
                        }
                        break;
                    #endregion

                    #region 加入黑名单
                    case "addBlackList":
                        try
                        {
                            int newId = -1;
                            SeatManage.ClassModel.RegulationRulesSetting rules = seatDataService.GetRegulationRulesSetting();
                            if (!room[0].Setting.BlackListSetting.Used && !rules.BlacklistSet.Used)
                            {
                                result.Result = false;
                                result.Msg = "阅览室未开启记录黑名单功能";
                                break;
                            }
                            foreach (JM_Seat seat in list)
                            {
                                EnterOutLogInfo model = seatDataService.GetEnterOutLogInfoBySeatNum(seat.SeatNo);
                                if (model != null && model.EnterOutState != EnterOutLogType.Leave)
                                {
                                    if (room[0] != null && room[0].Setting.BlackListSetting.Used)
                                    {
                                        BlackListInfo info = new BlackListInfo();
                                        info.AddTime = DateTime.Now;
                                        info.BlacklistState = LogStatus.Valid;
                                        info.CardNo = model.CardNo;
                                        info.ReadingRoomID = model.ReadingRoomNo;
                                        info.OutBlacklistMode = rules.BlacklistSet.LeaveBlacklist;
                                        if (info.OutBlacklistMode == LeaveBlacklistMode.AutomaticMode)
                                        {
                                            info.ReMark = string.Format("管理员{0}通过手持设备{0}把读者加入黑名单，记录黑名单{1}天", loginId, room[0].Setting.BlackListSetting.LimitDays);
                                            info.OutTime = info.AddTime.AddDays(room[0].Setting.BlackListSetting.LimitDays);
                                        }
                                        else
                                        {
                                            info.ReMark = string.Format("管理员{0}通过手持设备把读者加入黑名单，手动离开黑名单", loginId);
                                        }
                                        newId = seatDataService.AddBlacklist(info);
                                    }
                                    else if (rules.BlacklistSet.Used)
                                    {
                                        BlackListInfo info = new BlackListInfo();
                                        info.AddTime = DateTime.Now;
                                        info.OutTime = info.AddTime.AddDays(rules.BlacklistSet.LimitDays);
                                        info.BlacklistState = LogStatus.Valid;
                                        info.CardNo = model.CardNo;
                                        info.OutBlacklistMode = rules.BlacklistSet.LeaveBlacklist;
                                        if (info.OutBlacklistMode == LeaveBlacklistMode.AutomaticMode)
                                        {
                                            info.ReMark = string.Format("管理员{0}通过手持设备把读者加入黑名单，记录黑名单{1}天", loginId, rules.BlacklistSet.LimitDays);
                                            info.OutTime = info.AddTime.AddDays(rules.BlacklistSet.LimitDays);
                                        }
                                        else
                                        {
                                            info.ReMark = string.Format("管理员{0}通过手持设备把读者加入黑名单，手动离开黑名单", loginId);
                                        }
                                        newId = seatDataService.AddBlacklist(info);
                                    }

                                    if (newId > 0)
                                    {
                                        model.EnterOutState = EnterOutLogType.Leave;
                                        model.Flag = Operation.Admin;
                                        model.Remark = string.Format("在{0}，{1}号座位，被管理员{2}，通过手持设备设置离开", room[0].Name, model.ShortSeatNo, loginId);

                                        HandleResult rs = seatDataService.AddEnterOutLogInfo(model, ref newId);
                                        if (rs == HandleResult.Successed)
                                        {
                                            //result.Result = true;
                                            //result.Msg = "成功将读者加入黑名单！";
                                            successResult++;
                                            result.Result = true;
                                        }
                                        else
                                        {
                                            //result.Result = false;
                                            //result.Msg = "将读者加入黑名单失败！";
                                            failResult++;
                                        }
                                    }
                                    else
                                    {
                                        //result.Result = false;
                                        //result.Msg = "将读者加入黑名单失败！";
                                        failResult++;
                                    }
                                }
                            }
                            result.Msg = "加入黑名单";
                        }
                        catch (Exception ex)
                        {
                            SeatManage.SeatManageComm.WriteLog.Write("加入黑名单遇到异常：" + ex.Message);
                            result.Result = false;
                            result.Msg = "执行遇到异常!";
                        }
                        break;
                    #endregion
                }
                StringBuilder str = new StringBuilder();
                if (result.Result)
                {
                    str.Append("成功");
                }
                else
                {
                    str.Append("失败");
                }
                if (successResult > 0)
                {
                    str.Append(string.Format("，成功{0}条", successResult));
                }
                if (failResult > 0)
                {
                    str.Append(string.Format("，失败{0}条", failResult));
                }
                result.Msg = result.Msg + str.ToString();
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("对座位进行操作遇到异常：" + ex.Message);
                JM_HandleResultObject result = new JM_HandleResultObject();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }

    }
}
