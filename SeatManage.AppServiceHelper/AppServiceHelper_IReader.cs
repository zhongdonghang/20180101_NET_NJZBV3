using System;
using SeatManage.AppJsonModel;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;

namespace SeatManage.AppServiceHelper
{
    public partial class AppServiceHelper : IAppServiceHelper
    {
        /// <summary>
        /// 获取用户的基本信息
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <returns></returns>
        public string GetUserInfo(string studentNo)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                ReaderInfo reader = SeatManageDateService.GetReader(studentNo, false);
                if (reader == null)
                {
                    result.Result = false;
                    result.Msg = "对不起，不存在该读者信息！";
                    return JSONSerializer.Serialize(result);
                }
                AJM_Reader ajmReader = new AJM_Reader();
                ajmReader.CardId = reader.CardID;
                ajmReader.StudentNo = reader.CardNo;
                ajmReader.Name = reader.Name;
                ajmReader.Sex = reader.Sex;
                ajmReader.Department = reader.Pro;
                ajmReader.ReaderType = reader.ReaderType;
                result.Result = true;
                result.Msg = JSONSerializer.Serialize(ajmReader);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("根据学号获取读者信息遇到异常：{0}", ex.Message));
                result.Result = false;
                result.Msg = "获取读者信息执行遇到异常！";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取用户当前的在座状态
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <returns></returns>
        public string GetUserNowState(string studentNo)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                if (string.IsNullOrEmpty(studentNo))
                {
                    result.Result = false;
                    result.Msg = "学号不能为空";
                    return JSONSerializer.Serialize(result);
                }
                ReaderInfo readerInfo = SeatManageDateService.GetReader(studentNo, true);

                if (readerInfo == null)
                {
                    result.Result = false;
                    result.Msg = "未查询到该读者的当前状态";
                    return JSONSerializer.Serialize(result);
                }
                AJM_ReaderStatus ajmReaderStatus = new AJM_ReaderStatus();

                if (readerInfo.EnterOutLog != null)
                {
                    AJM_EnterOutLog ajmEnterOutLog = new AJM_EnterOutLog
                    {
                        EnterOutTime = readerInfo.EnterOutLog.EnterOutTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        EnterOutState = readerInfo.EnterOutLog.EnterOutState.ToString(),
                        Id = readerInfo.EnterOutLog.EnterOutLogID,
                        Operator = readerInfo.EnterOutLog.Flag.ToString(),
                        Remark = readerInfo.EnterOutLog.Remark,
                        RoomName = readerInfo.EnterOutLog.ReadingRoomName,
                        RoomNo = readerInfo.EnterOutLog.ReadingRoomNo,
                        SeatNo = readerInfo.EnterOutLog.SeatNo,
                        SeatShortNo = readerInfo.EnterOutLog.ShortSeatNo

                    };
                    switch (readerInfo.EnterOutLog.EnterOutState)
                    {
                        case EnterOutLogType.None:
                        case EnterOutLogType.Leave:
                        case EnterOutLogType.BookingCancel:
                        case EnterOutLogType.WaitingCancel:
                        case EnterOutLogType.BespeakWaiting:
                            ajmReaderStatus.Status = ReaderStatus.Leave.ToString();
                            break;
                        case EnterOutLogType.BookingConfirmation:
                        case EnterOutLogType.CancelTime:
                        case EnterOutLogType.ComeBack:
                        case EnterOutLogType.ContinuedTime:
                        case EnterOutLogType.ReselectSeat:
                        case EnterOutLogType.SelectSeat:
                        case EnterOutLogType.Timing:
                        case EnterOutLogType.WaitingSuccess:
                            ajmReaderStatus.Status = ReaderStatus.Seating.ToString();
                            break;
                        case EnterOutLogType.ShortLeave:
                            ajmReaderStatus.Status = ReaderStatus.ShortLeave.ToString();
                            break;
                        case EnterOutLogType.Waiting:
                            ajmReaderStatus.Status = ReaderStatus.Waiting.ToString();
                            break;
                    }
                    ajmReaderStatus.AjmEnterOutLog = ajmEnterOutLog;
                }
                if (readerInfo.WaitSeatLog != null)
                {
                    AJM_WaitSeatLog ajmWaitSeatLog = new AJM_WaitSeatLog
                    {
                        RoomName = readerInfo.WaitSeatLog.EnterOutLog.ReadingRoomName,
                        RoomNo = readerInfo.WaitSeatLog.ReadingRoomNo,
                        SeatNo = readerInfo.WaitSeatLog.SeatNo,
                        SeatShortNo = readerInfo.WaitSeatLog.EnterOutLog.ShortSeatNo,
                        SeatWaitId = readerInfo.WaitSeatLog.SeatWaitingID,
                        SeatWaitTime = readerInfo.WaitSeatLog.SeatWaitTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        StudentNo_A = readerInfo.WaitSeatLog.CardNo,
                        StudentNo_B = readerInfo.WaitSeatLog.CardNoB
                    };
                    ajmReaderStatus.AjmWaitSeatLogs = ajmWaitSeatLog;
                    ajmReaderStatus.Status = ReaderStatus.Waiting.ToString();

                }
                if (readerInfo.BespeakLog.Count > 0)
                {
                    foreach (BespeakLogInfo model in readerInfo.BespeakLog)
                    {
                        if (model.BsepeakTime.Date == DateTime.Now.Date)
                        {
                            ajmReaderStatus.Status = ReaderStatus.Booking.ToString();
                        }
                        AJM_BespeakLog ajmBespeakLog = new AJM_BespeakLog
                        {
                            Id = model.BsepeaklogID,
                            BookTime = model.BsepeakTime.ToString("yyyy-MM-dd HH:mm:ss"),
                            IsValid = true,
                            Remark = model.Remark,
                            RoomName = model.ReadingRoomName,
                            RoomNo = model.ReadingRoomNo,
                            SeatNo = model.SeatNo,
                            SeatShortNo = model.ShortSeatNum,
                            SubmitDateTime = model.SubmitTime.ToString("yyyy-MM-dd HH:mm:ss")
                        };
                        ajmReaderStatus.AjmBespeakLogs.Add(ajmBespeakLog);
                    }
                }
                result.Result = true;
                result.Msg = JSONSerializer.Serialize(ajmReaderStatus);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取读者当前状态发生异常：{0}", ex.Message));
                result.Result = false;
                result.Msg = "获取用户状态执行异常！";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginId">登录账号</param>
        /// <param name="password">登录密码</param>
        /// <returns></returns>
        public string CheckUser(string loginId, string password)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                if (string.IsNullOrEmpty(loginId.Trim()) || string.IsNullOrEmpty(password.Trim()))
                {
                    result.Result = false;
                    result.Msg = "用户名或密码不能为空！";
                    return JSONSerializer.Serialize(result);
                }
                UserInfo userInfo = SeatManageDateService.GetUserInfo(loginId);
                if (userInfo == null)
                {
                    result.Result = false;
                    result.Msg = "用户名或密码错误！";
                    return JSONSerializer.Serialize(result);
                }
                string md5Password = MD5Algorithm.GetMD5Str32(password);
                if (!md5Password.Equals(userInfo.Password))
                {
                    result.Result = false;
                    result.Msg = "用户名或密码错误！";
                    return JSONSerializer.Serialize(result);
                }
                AJM_Reader ajmReader = new AJM_Reader
                {
                    StudentNo = userInfo.LoginId,
                    Name = userInfo.UserName
                };
                result.Result = true;
                result.Msg = JSONSerializer.Serialize(ajmReader);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("登录出现异常：{0}", ex.Message));
                result.Result = false;
                result.Msg = "执行登录操作遇到异常";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取登录读者详细信息
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public string GetUserInfo_WeiXin(string studentNo)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                if (string.IsNullOrEmpty(studentNo))
                {
                    result.Result = false;
                    result.Msg = "学号不能为空！";
                    return JSONSerializer.Serialize(result);
                }
                ReaderInfo readerInfo = SeatManageDateService.GetReader(studentNo, true);
                if (readerInfo == null)
                {
                    result.Result = false;
                    result.Msg = "未查询到该读者的当前状态";
                    return JSONSerializer.Serialize(result);
                }
                AJM_WeiXinUserInfo ajmWeiXinUserInfo = new AJM_WeiXinUserInfo();
                ajmWeiXinUserInfo.StudentNo = readerInfo.CardNo;
                ajmWeiXinUserInfo.Name = readerInfo.Name;
                ajmWeiXinUserInfo.ReaderType = readerInfo.ReaderType;
                //预约网站设置
                ajmWeiXinUserInfo.AjmPecketBookSetting.UseBookComfirm = readerInfo.PecketWebSetting.UseBookComfirm;
                ajmWeiXinUserInfo.AjmPecketBookSetting.UseBookNextDaySeat = readerInfo.PecketWebSetting.UseBookNextDaySeat;
                ajmWeiXinUserInfo.AjmPecketBookSetting.UseBookNowDaySeat = readerInfo.PecketWebSetting.UseBookNowDaySeat;
                ajmWeiXinUserInfo.AjmPecketBookSetting.UseBookSeat = readerInfo.PecketWebSetting.UseBookSeat;
                ajmWeiXinUserInfo.AjmPecketBookSetting.UseCanLeave = readerInfo.PecketWebSetting.UseCanLeave;
                ajmWeiXinUserInfo.AjmPecketBookSetting.UseCancelBook = readerInfo.PecketWebSetting.UseCancelBook;
                ajmWeiXinUserInfo.AjmPecketBookSetting.UseCancelWait = readerInfo.PecketWebSetting.UseCancelWait;
                ajmWeiXinUserInfo.AjmPecketBookSetting.UseChangeSeat = readerInfo.PecketWebSetting.UseChangeSeat;
                ajmWeiXinUserInfo.AjmPecketBookSetting.UseComeBack = readerInfo.PecketWebSetting.UseComeBack;
                ajmWeiXinUserInfo.AjmPecketBookSetting.UseContinue = readerInfo.PecketWebSetting.UseContinue;
                ajmWeiXinUserInfo.AjmPecketBookSetting.UseSelectSeat = readerInfo.PecketWebSetting.UseSelectSeat;
                ajmWeiXinUserInfo.AjmPecketBookSetting.UseShortLeave = readerInfo.PecketWebSetting.UseShortLeave;
                ajmWeiXinUserInfo.AjmPecketBookSetting.UseWaitSeat = readerInfo.PecketWebSetting.UseWaitSeat;

                AJM_ReaderStatus ajmReaderStatus = new AJM_ReaderStatus();

                if (readerInfo.EnterOutLog != null)
                {
                    AJM_EnterOutLog ajmEnterOutLog = new AJM_EnterOutLog();
                    ajmEnterOutLog.EnterOutTime = readerInfo.EnterOutLog.EnterOutTime.ToString("yyyy-MM-dd HH:mm:ss");
                    ajmEnterOutLog.EnterOutState = readerInfo.EnterOutLog.EnterOutState.ToString();
                    ajmEnterOutLog.Id = readerInfo.EnterOutLog.EnterOutLogID;
                    ajmEnterOutLog.Operator = readerInfo.EnterOutLog.Flag.ToString();
                    ajmEnterOutLog.Remark = readerInfo.EnterOutLog.Remark;
                    ajmEnterOutLog.RoomName = readerInfo.EnterOutLog.ReadingRoomName;
                    ajmEnterOutLog.RoomNo = readerInfo.EnterOutLog.ReadingRoomNo;
                    ajmEnterOutLog.SeatNo = readerInfo.EnterOutLog.SeatNo;
                    ajmEnterOutLog.SeatShortNo = readerInfo.EnterOutLog.ShortSeatNo;
                    ajmWeiXinUserInfo.AjmReadingRoomState = GetSingleRoomOpenState(ajmEnterOutLog.RoomNo);

                    switch (readerInfo.EnterOutLog.EnterOutState)
                    {
                        case EnumType.EnterOutLogType.None:
                        case EnumType.EnterOutLogType.Leave:
                        case EnumType.EnterOutLogType.BookingCancel:
                        case EnumType.EnterOutLogType.WaitingCancel:
                        case EnumType.EnterOutLogType.BespeakWaiting:
                            ajmReaderStatus.Status = EnumType.ReaderStatus.Leave.ToString();
                            break;
                        case EnumType.EnterOutLogType.BookingConfirmation:
                        case EnumType.EnterOutLogType.CancelTime:
                        case EnumType.EnterOutLogType.ComeBack:
                        case EnumType.EnterOutLogType.ContinuedTime:
                        case EnumType.EnterOutLogType.ReselectSeat:
                        case EnumType.EnterOutLogType.SelectSeat:
                        case EnumType.EnterOutLogType.Timing:
                        case EnumType.EnterOutLogType.WaitingSuccess:
                            ajmReaderStatus.Status = EnumType.ReaderStatus.Seating.ToString();
                            break;
                        case EnumType.EnterOutLogType.ShortLeave:
                            ajmReaderStatus.Status = EnumType.ReaderStatus.ShortLeave.ToString();
                            break;
                        case EnumType.EnterOutLogType.Waiting:
                            ajmReaderStatus.Status = EnumType.ReaderStatus.Waiting.ToString();
                            break;
                    }
                    ajmReaderStatus.AjmEnterOutLog = ajmEnterOutLog;
                }
                if (readerInfo.WaitSeatLog != null)
                {
                    AJM_WaitSeatLog ajmWaitSeatLog = new AJM_WaitSeatLog();
                    ajmWaitSeatLog.RoomName = readerInfo.WaitSeatLog.EnterOutLog.ReadingRoomName;
                    ajmWaitSeatLog.RoomNo = readerInfo.WaitSeatLog.ReadingRoomNo;
                    ajmWaitSeatLog.SeatNo = readerInfo.WaitSeatLog.SeatNo;
                    ajmWaitSeatLog.SeatShortNo = readerInfo.WaitSeatLog.EnterOutLog.ShortSeatNo;
                    ajmWaitSeatLog.SeatWaitId = readerInfo.WaitSeatLog.SeatWaitingID;
                    ajmWaitSeatLog.SeatWaitTime = readerInfo.WaitSeatLog.SeatWaitTime.ToString("yyyy-MM-dd HH:mm:ss");
                    ajmWaitSeatLog.StudentNo_A = readerInfo.WaitSeatLog.CardNo;
                    ajmWaitSeatLog.StudentNo_B = readerInfo.WaitSeatLog.CardNoB;
                    ajmWeiXinUserInfo.AjmReadingRoomState = GetSingleRoomOpenState(ajmWaitSeatLog.RoomNo);

                    ajmReaderStatus.AjmWaitSeatLogs = ajmWaitSeatLog;
                    ajmReaderStatus.Status = EnumType.ReaderStatus.Waiting.ToString();

                }
                if (readerInfo.BespeakLog.Count > 0)
                {
                    foreach (BespeakLogInfo model in readerInfo.BespeakLog)
                    {
                        if (model.BsepeakTime.Date == DateTime.Now.Date)
                        {
                            ajmReaderStatus.Status = EnumType.ReaderStatus.Booking.ToString();
                        }
                        AJM_BespeakLog ajmBespeakLog = new AJM_BespeakLog
                        {
                            Id = model.BsepeaklogID,
                            BookTime = model.BsepeakTime.ToString("yyyy-MM-dd HH:mm:ss"),
                            IsValid = true,
                            Remark = model.Remark,
                            RoomName = model.ReadingRoomName,
                            RoomNo = model.ReadingRoomNo,
                            SeatNo = model.SeatNo,
                            SeatShortNo = model.ShortSeatNum,
                            SubmitDateTime = model.SubmitTime.ToString("yyyy-MM-dd HH:mm:ss")
                        };
                        ajmReaderStatus.AjmBespeakLogs.Add(ajmBespeakLog);
                    }
                }
                ajmWeiXinUserInfo.AjmReaderStatus = ajmReaderStatus;

                result.Result = true;
                result.Msg = JSONSerializer.Serialize(ajmWeiXinUserInfo);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取登录读者详细信息遇到异常：{0}", ex.Message));
                result.Result = false;
                result.Msg = "获取登录读者详细信息执行遇到异常！";
                return JSONSerializer.Serialize(result);
            }
        }


        public string GetUserNowStateV2(string studentNo, bool isCheckCode)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                if (string.IsNullOrEmpty(studentNo))
                {
                    result.Result = false;
                    result.Msg = "学号不能为空";
                    return JSONSerializer.Serialize(result);
                }
                ReaderInfo readerInfo = SeatManageDateService.GetReader(studentNo, true);

                if (readerInfo == null)
                {
                    result.Result = false;
                    result.Msg = "未查询到该读者的当前状态";
                    return JSONSerializer.Serialize(result);
                }
                AJM_UserNowStatus ajmReaderStatus = new AJM_UserNowStatus();
                ajmReaderStatus.StudentNum = readerInfo.CardNo;
                ajmReaderStatus.Name = readerInfo.Name;
                ajmReaderStatus.Status = ajmReaderStatus.Status = ReaderStatus.Leave.ToString();
                if (readerInfo.EnterOutLog != null && readerInfo.EnterOutLog.EnterOutState != EnterOutLogType.Leave)
                {
                    switch (readerInfo.EnterOutLog.EnterOutState)
                    {
                        case EnterOutLogType.ComeBack:
                        case EnterOutLogType.ContinuedTime:
                        case EnterOutLogType.ReselectSeat:
                        case EnterOutLogType.SelectSeat:
                        case EnterOutLogType.WaitingSuccess:
                        case EnterOutLogType.BookingConfirmation:
                            ajmReaderStatus.Status = ReaderStatus.Seating.ToString();
                            ajmReaderStatus.CanOperation = "Leave;ShortLeave" + (isCheckCode && readerInfo.AtReadingRoom.Setting.SeatUsedTimeLimit.IsCanContinuedTime && readerInfo.AtReadingRoom.Setting.SeatUsedTimeLimit.Used ? ";ContiuneTime" : "");
                            break;
                        case EnterOutLogType.ShortLeave:
                            ajmReaderStatus.Status = ReaderStatus.ShortLeave.ToString();
                            ajmReaderStatus.CanOperation = "Leave;" + (isCheckCode ? ";ComeBack" : "");
                            break;
                    }
                    ajmReaderStatus.InRoom = readerInfo.AtReadingRoom.Name;
                    ajmReaderStatus.SeatNum = readerInfo.EnterOutLog.ShortSeatNo;
                    ajmReaderStatus.NowStatusRemark = readerInfo.EnterOutLog.Remark;
                    ajmReaderStatus.Time = readerInfo.EnterOutLog.EnterOutTime.ToString();
                }
                if (readerInfo.WaitSeatLog != null)
                {
                    ajmReaderStatus.Status = ReaderStatus.Waiting.ToString();
                    ajmReaderStatus.InRoom = readerInfo.AtReadingRoom.Name;
                    ajmReaderStatus.SeatNum = readerInfo.WaitSeatLog.EnterOutLog.ShortSeatNo;
                    ajmReaderStatus.CanOperation = "CancelWait";
                    ajmReaderStatus.NowStatusRemark = string.Format("您正在等待，{0} {1}号座位。", ajmReaderStatus.InRoom, ajmReaderStatus.SeatNum);
                    ajmReaderStatus.Time = readerInfo.WaitSeatLog.EnterOutLog.ToString();
                }
                if (readerInfo.BespeakLog.Count > 0 && readerInfo.BespeakLog[0].BsepeakTime.Date == DateTime.Now.Date)
                {
                    ajmReaderStatus.Status = ReaderStatus.Booking.ToString();
                    ajmReaderStatus.InRoom = readerInfo.BespeakLog[0].ReadingRoomName;
                    ajmReaderStatus.SeatNum = readerInfo.BespeakLog[0].ShortSeatNum;
                    ajmReaderStatus.CanOperation = "CancelBook" + (isCheckCode ? ";CheckBook" : ""); ;
                    ajmReaderStatus.NowStatusRemark = readerInfo.BespeakLog[0].Remark;
                    ajmReaderStatus.Time = readerInfo.BespeakLog[0].SubmitTime.ToString();
                }
                result.Result = true;
                result.Msg = JSONSerializer.Serialize(ajmReaderStatus);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取读者当前状态发生异常：{0}", ex.Message));
                result.Result = false;
                result.Msg = "获取用户状态执行异常！";
                return JSONSerializer.Serialize(result);
            }
        }
    }
}
