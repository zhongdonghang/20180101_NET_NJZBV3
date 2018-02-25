using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace SeatManageWebV2.FunctionPages.InterfaceAuthorization
{
    public partial class IntherfaceAuthorization : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinding();
            }
        }

        private void DataBinding()
        {
            AuthorizeVerify.FunctionAuthorizeInfo funAuthorize = SeatManage.Bll.AuthorizationOperation.GetFunctionAuthorize();
            if (funAuthorize != null)
            {
                funFileMsg.InnerHtml = "";
                foreach (string fun in funAuthorize.SystemFunction)
                {
                    string funName = "";
                    switch (fun)
                    {
                        case "Bespeak_AppointTime": funName = "指定时间预约"; break;
                        case "Bespeak_NowDay": funName = "预约当天座位"; break;
                        case "Client_SeachBespeak": funName = "触摸屏终端预约记录查询"; break;
                        case "Client_SeachBlasklist": funName = "触摸屏终端黑名单查询"; break;
                        case "Client_SeachViolation": funName = "触摸屏终端违规记录查询"; break;
                        case "Client_ShowLastSeat": funName = "触摸屏终端座位使用情况查询"; break;
                        case "Client_ShowReaderInfo": funName = "触摸屏终端读者信息显示"; break;
                        case "Client_ShowReaderMeg": funName = "触摸屏终端读者消息推送"; break;
                        case "LimitTime_SpanMode": funName = "计时功能时段模式"; break;
                        case "Media_MediaPlayer": funName = "媒体播放器视频广告"; break;
                        case "Media_PopAd": funName = "触摸屏终端弹窗广告"; break;
                        case "Media_SchoolNotice": funName = "触摸屏终端校园通知"; break;
                        case "Media_TitleAd": funName = "触摸屏终端冠名广告"; break;
                        case "MoveClient_AdminManage": funName = "移动终端管理员终端"; break;
                        case "MoveClient_ContinueTime": funName = "移动终端续时功能"; break;
                        case "MoveClient_QRcodeDecode": funName = "移动终端二维码功能"; break;
                        case "RoomOC_24HModel": funName = "24小时功能"; break;
                        case "MoveClient_SeatSelect": funName = "移动终端选座功能"; break;
                        case "MoveClient_SeatWait": funName = "移动终端座位等待功能"; break;
                        case "SendMsg": funName = "消息推送功能"; break;
                    }
                    if (funName != "")
                    {
                        funFileMsg.InnerHtml += funName + "</br>";
                    }
                }
                if (funFileMsg.InnerHtml != "")
                {
                    upFunFileTR.Style["visibility"] = "hidden";
                    rupFunFileTR.Style["visibility"] = "visible";
                }
                else
                {
                    funFileMsg.InnerHtml = "暂无授权";
                }
            }
            else
            {
                upFunFileTR.Style["visibility"] = "visible";
                rupFunFileTR.Style["visibility"] = "hidden";
            }
            try
            {
                Dictionary<string, AuthorizeVerify.ServiceAuthorize> serviceAuthorizes = AuthorizeVerify.ServiceAuthorize.AnalyzeAuthorize(Server.MapPath("~/WebService/ws_authorized_keys"));
                accessFileMsg.InnerHtml = "";
                foreach (KeyValuePair<string, AuthorizeVerify.ServiceAuthorize> item in serviceAuthorizes)
                {
                    string accessName = "账户：" + item.Key + "</br>";
                    foreach (AuthorizeVerify.ServiceAuthorizeItem access in item.Value.ServiceAuthorizeItems)
                    {
                        switch (access.ServiceName)
                        {
                            case "GetReaderInfoService":
                                accessName += "</br>获取用户信息接口：" + access.ServiceName + "</br>";
                                break;
                            case "GetReadingRoomInfoService":
                                accessName += "</br>获取阅览室信息：" + access.ServiceName + "</br>";
                                break;
                            case "DelaySeatUsedTimeService":
                                accessName += "</br>座位续时接口：" + access.ServiceName + "</br>";
                                break;
                            case "ShortLeaveService":
                                accessName += "</br>座位暂离接口：" + access.ServiceName + "</br>";
                                break;
                            case "BespeakSeatService":
                                accessName += "</br>座位预约接口：" + access.ServiceName + "</br>";
                                break;
                            case "ChooseSeatService":
                                accessName += "</br>座位选择接口：" + access.ServiceName + "</br>";
                                break;
                            case "FreeSeatService":
                                accessName += "</br>释放座位接口：" + access.ServiceName + "</br>";
                                break;
                        }
                        foreach (string function in access.AllowMethodName)
                        {
                            switch (function)
                            {
                                case "GetAllReadingRoomBaseInfo":
                                    accessName += "获取全部阅览室信息方法：" + function + "</br>";
                                    break;
                                case "GetReadingRoomSetInfoByRoomNum":
                                    accessName += "获取阅览室设置方法：" + function + "</br>";
                                    break;
                                case "GetSeatsLayoutByRoomNum":
                                    accessName += "获取座位布局方法：" + function + "</br>";
                                    break;
                                case "GetSeatsUsedInfoByRoomNum":
                                    accessName += "获取座位使用情况方法：" + function + "</br>";
                                    break;
                                case "GetCanBespeakSeatsLayout":
                                    accessName += "获取可预约座位的布局方法：" + function + "</br>";
                                    break;
                                case "GetSeatsBespeakInfoByRoomNum":
                                    accessName += "获取座位预约信息方法：" + function + "</br>";
                                    break;
                                case "GetBaseReaderInfoByCardId":
                                    accessName += "获取用户信息方法：" + function + "</br>";
                                    break;
                                case "GetBaseReaderInfo":
                                    accessName += "获取用户信息方法：" + function + "</br>";
                                    break;
                                case "GetReaderActualTimeRecord":
                                    accessName += "获取用户实时记录方法：" + function + "</br>";
                                    break;
                                case "GetReaderBespeakRecord":
                                    accessName += "获取用户预约记录方法：" + function + "</br>";
                                    break;
                                case "GetReaderChooseSeatRecord":
                                    accessName += "获取用户选座记录方法：" + function + "</br>";
                                    break;
                                case "GetReaderBlacklistRecord":
                                    accessName += "获取用户黑名单记录方法：" + function + "</br>";
                                    break;
                                case "GetReaderAccount":
                                    accessName += "获取用户账号方法：" + function + "</br>";
                                    break;
                                case "GetDelaySet":
                                    accessName += "获取续时设置方法：" + function + "</br>";
                                    break;
                                case "SubmitDelayResult":
                                    accessName += "座位续时方法：" + function + "</br>";
                                    break;
                                case "ShortLeave":
                                    accessName += "座位暂离方法：" + function + "</br>";
                                    break;
                                case "GetOpenBespeakRooms":
                                    accessName += "获取开放预约的阅览室方法：" + function + "</br>";
                                    break;
                                case "SubmitBespeakInfo":
                                    accessName += "预约座位方法：" + function + "</br>";
                                    break;
                                case "GetBespeakSeatRoomSet":
                                    accessName += "获取预约的阅览室设置方法：" + function + "</br>";
                                    break;
                                case "GetReaderActualTimeBespeakRecord":
                                    accessName += "获取用户实时预约记录方法：" + function + "</br>";
                                    break;
                                case "VerifyCanDoIt":
                                    accessName += "验证用户是否能选座方法：" + function + "</br>";
                                    break;
                                case "SeatLock":
                                    accessName += "锁定座位方法：" + function + "</br>";
                                    break;
                                case "SubmitChooseResult":
                                    accessName += "更换座位方法：" + function + "</br>";
                                    break;
                                case "FreeSeat":
                                    accessName += "释放座位方法：" + function + "</br>";
                                    break;
                            }
                        }
                    }
                    if (accessName != "")
                    {
                        accessFileMsg.InnerHtml += accessName + "</br>";
                    }
                }
                if (accessFileMsg.InnerHtml != "")
                {
                    upAccessFileTR.Style["visibility"] = "hidden";
                    rupAccessFileTR.Style["visibility"] = "visible";
                }
                else
                {
                    accessFileMsg.InnerHtml = "暂无授权";
                }
            }
            catch
            {
                upAccessFileTR.Style["visibility"] = "visible";
                rupAccessFileTR.Style["visibility"] = "hidden";
            }
        }

        protected void btn_funFileSave_Click(object sender, EventArgs e)
        {
            if (funFile.HasFile)
            {
                try
                {
                    Stream stream = funFile.FileContent;
                    StreamReader streamReader = new StreamReader(stream);
                    string context = streamReader.ReadToEnd();
                    AuthorizeVerify.FunctionAuthorizeInfo authorize
                        = SeatManage.SeatManageComm.JSONSerializer.Deserialize<AuthorizeVerify.FunctionAuthorizeInfo>(SeatManage.SeatManageComm.AESAlgorithm.AESDecrypt(context));
                    if (authorize.SchoolNum != SeatManage.Bll.Registry.GetSchoolNum())
                    {
                        FineUI.Alert.ShowInTop("请使用当前学校的授权文件！");
                        return;
                    }
                    if (SeatManage.Bll.AuthorizationOperation.SaveFunctionAuthorize(authorize))
                    {

                        FineUI.Alert.ShowInTop("保存成功！");
                        funFileMsg.InnerHtml = "";
                        foreach (string fun in authorize.SystemFunction)
                        {
                            string funName = "";
                            switch (fun)
                            {
                                case "Bespeak_AppointTime": funName = "指定时间预约"; break;
                                case "Bespeak_NowDay": funName = "预约当天座位"; break;
                                case "Client_SeachBespeak": funName = "触摸屏终端预约记录查询"; break;
                                case "Client_SeachBlasklist": funName = "触摸屏终端黑名单查询"; break;
                                case "Client_SeachViolation": funName = "触摸屏终端违规记录查询"; break;
                                case "Client_ShowLastSeat": funName = "触摸屏终端座位使用情况查询"; break;
                                case "Client_ShowReaderInfo": funName = "触摸屏终端读者信息显示"; break;
                                case "Client_ShowReaderMeg": funName = "触摸屏终端读者消息推送"; break;
                                case "LimitTime_SpanMode": funName = "计时功能时段模式"; break;
                                case "Media_MediaPlayer": funName = "媒体播放器视频广告"; break;
                                case "Media_PopAd": funName = "触摸屏终端弹窗广告"; break;
                                case "Media_SchoolNotice": funName = "触摸屏终端校园通知"; break;
                                case "Media_TitleAd": funName = "触摸屏终端冠名广告"; break;
                                case "MoveClient_AdminManage": funName = "移动终端管理员终端"; break;
                                case "MoveClient_ContinueTime": funName = "移动终端续时功能"; break;
                                case "MoveClient_QRcodeDecode": funName = "移动终端二维码功能"; break;
                                case "RoomOC_24HModel": funName = "24小时功能"; break;
                                case "MoveClient_SeatSelect": funName = "移动终端选座功能"; break;
                                case "MoveClient_SeatWait": funName = "移动终端座位等待功能"; break;
                                case "SendMsg": funName = "消息推送功能"; break;
                            }
                            if (funName != "")
                            {
                                funFileMsg.InnerHtml += funName + "</br>";
                            }
                        }
                        if (funFileMsg.InnerHtml != "")
                        {
                            upFunFileTR.Style["visibility"] = "hidden";
                            rupFunFileTR.Style["visibility"] = "visible";
                        }
                    }
                    else
                    {
                        FineUI.Alert.ShowInTop("保存失败！");
                    }
                }
                catch
                {
                    FineUI.Alert.ShowInTop("读取文件错误！请保证文件正确！");
                }
            }
        }
        protected void btn_accessFileSave_Click(object sender, EventArgs e)
        {
            if (accessFile.HasFile)
            {
                try
                {
                    Stream stream = accessFile.FileContent;
                    StreamReader streamReader = new StreamReader(stream);
                    string context = streamReader.ReadToEnd();
                    Dictionary<string, AuthorizeVerify.ServiceAuthorize> authorizes = new Dictionary<string, AuthorizeVerify.ServiceAuthorize>();
                    string[] arrAuthorizes = context.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string authorize in arrAuthorizes)
                    {
                        try
                        {
                            string authorizeUserName = authorize.Substring(0, authorize.IndexOf('='));
                            string authorizeContent = authorize.Substring(authorize.IndexOf('=') + 1);
                            AuthorizeVerify.ServiceAuthorize authorizeObj = SeatManage.SeatManageComm.JSONSerializer.Deserialize<AuthorizeVerify.ServiceAuthorize>(SeatManage.SeatManageComm.AESAlgorithm.AESDecrypt(authorizeContent));
                            authorizes.Add(authorizeUserName, authorizeObj);
                        }
                        catch (Exception ex)
                        {
                            FineUI.Alert.ShowInTop("读取文件错误！请保证文件正确！");
                            return;
                        }
                    }
                    accessFile.SaveAs(Server.MapPath("~/WebService/ws_authorized_keys"));
                    FineUI.Alert.ShowInTop("保存成功！");
                    accessFileMsg.InnerHtml = "";
                    foreach (KeyValuePair<string, AuthorizeVerify.ServiceAuthorize> item in authorizes)
                    {
                        string accessName = "账户：" + item.Key + "</br>";
                        foreach (AuthorizeVerify.ServiceAuthorizeItem access in item.Value.ServiceAuthorizeItems)
                        {
                            switch (access.ServiceName)
                            {
                                case "GetReaderInfoService":
                                    accessName += "</br>获取用户信息接口：" + access.ServiceName + "</br>";
                                    break;
                                case "GetReadingRoomInfoService":
                                    accessName += "</br>获取阅览室信息：" + access.ServiceName + "</br>";
                                    break;
                                case "DelaySeatUsedTimeService":
                                    accessName += "</br>座位续时接口：" + access.ServiceName + "</br>";
                                    break;
                                case "ShortLeaveService":
                                    accessName += "</br>座位暂离接口：" + access.ServiceName + "</br>";
                                    break;
                                case "BespeakSeatService":
                                    accessName += "</br>座位预约接口：" + access.ServiceName + "</br>";
                                    break;
                                case "ChooseSeatService":
                                    accessName += "</br>座位选择接口：" + access.ServiceName + "</br>";
                                    break;
                                case "FreeSeatService":
                                    accessName += "</br>释放座位接口：" + access.ServiceName + "</br>";
                                    break;
                            }
                            foreach (string function in access.AllowMethodName)
                            {
                                switch (function)
                                {
                                    case "GetAllReadingRoomBaseInfo":
                                        accessName += "获取全部阅览室信息方法：" + function + "</br>";
                                        break;
                                    case "GetReadingRoomSetInfoByRoomNum":
                                        accessName += "获取阅览室设置方法：" + function + "</br>";
                                        break;
                                    case "GetSeatsLayoutByRoomNum":
                                        accessName += "获取座位布局方法：" + function + "</br>";
                                        break;
                                    case "GetSeatsUsedInfoByRoomNum":
                                        accessName += "获取座位使用情况方法：" + function + "</br>";
                                        break;
                                    case "GetCanBespeakSeatsLayout":
                                        accessName += "获取可预约座位的布局方法：" + function + "</br>";
                                        break;
                                    case "GetSeatsBespeakInfoByRoomNum":
                                        accessName += "获取座位预约信息方法：" + function + "</br>";
                                        break;
                                    case "GetBaseReaderInfoByCardId":
                                        accessName += "获取用户信息方法：" + function + "</br>";
                                        break;
                                    case "GetBaseReaderInfo":
                                        accessName += "获取用户信息方法：" + function + "</br>";
                                        break;
                                    case "GetReaderActualTimeRecord":
                                        accessName += "获取用户实时记录方法：" + function + "</br>";
                                        break;
                                    case "GetReaderBespeakRecord":
                                        accessName += "获取用户预约记录方法：" + function + "</br>";
                                        break;
                                    case "GetReaderChooseSeatRecord":
                                        accessName += "获取用户选座记录方法：" + function + "</br>";
                                        break;
                                    case "GetReaderBlacklistRecord":
                                        accessName += "获取用户黑名单记录方法：" + function + "</br>";
                                        break;
                                    case "GetReaderAccount":
                                        accessName += "获取用户账号方法：" + function + "</br>";
                                        break;
                                    case "GetDelaySet":
                                        accessName += "获取续时设置方法：" + function + "</br>";
                                        break;
                                    case "SubmitDelayResult":
                                        accessName += "座位续时方法：" + function + "</br>";
                                        break;
                                    case "ShortLeave":
                                        accessName += "座位暂离方法：" + function + "</br>";
                                        break;
                                    case "GetOpenBespeakRooms":
                                        accessName += "获取开放预约的阅览室方法：" + function + "</br>";
                                        break;
                                    case "SubmitBespeakInfo":
                                        accessName += "预约座位方法：" + function + "</br>";
                                        break;
                                    case "GetBespeakSeatRoomSet":
                                        accessName += "获取预约的阅览室设置方法：" + function + "</br>";
                                        break;
                                    case "GetReaderActualTimeBespeakRecord":
                                        accessName += "获取用户实时预约记录方法：" + function + "</br>";
                                        break;
                                    case "VerifyCanDoIt":
                                        accessName += "验证用户是否能选座方法：" + function + "</br>";
                                        break;
                                    case "SeatLock":
                                        accessName += "锁定座位方法：" + function + "</br>";
                                        break;
                                    case "SubmitChooseResult":
                                        accessName += "更换座位方法：" + function + "</br>";
                                        break;
                                    case "FreeSeat":
                                        accessName += "释放座位方法：" + function + "</br>";
                                        break;
                                }
                            }
                        }
                        if (accessName != "")
                        {
                            accessFileMsg.InnerHtml += accessName + "</br>";
                        }
                    }
                    if (accessFileMsg.InnerHtml != "")
                    {
                        upAccessFileTR.Style["visibility"] = "hidden";
                        rupAccessFileTR.Style["visibility"] = "visible";
                    }
                    else
                    {
                        accessFileMsg.InnerHtml = "暂无授权";
                    }
                }
                catch
                {
                    FineUI.Alert.ShowInTop("读取文件错误！请保证文件正确！");
                }
            }
        }
    }
}