using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManage.SeatManageComm;

namespace PocketBookOnline.BookSeat
{
    public partial class ScanCode : BasePage
    {
        string cardNo;
        string password;
        string schoolId;
        string connection;
        protected void Page_Load(object sender, EventArgs e)
        {
            string strparam = Request.QueryString["param"];
            if (string.IsNullOrEmpty(strparam))
            {
                spanWarmInfo.InnerText = "非正常的访问！";
                divHanderPanel.Style.Add("display", "none");
                return;
            }
            Code.ScanCodeParamModel param = new Code.ScanCodeParamModel(strparam);
            if (Request.Cookies["userInfo"] != null)//存在记录的cookies信息
            {
                cardNo = CookiesManager.GetCookiesValue(CookiesManager.LoginID);
                password = CookiesManager.GetCookiesValue(CookiesManager.Password);
                schoolId = CookiesManager.GetCookiesValue(CookiesManager.SchoolId);
                connection = CookiesManager.GetCookiesValue(CookiesManager.ConnectionString);
                this.UserSchoolInfo = new AMS.Model.AMS_School() { Id = int.Parse(schoolId), ConnectionString = connection };
                if (this.BespeakHandler == null)
                {
                    this.BespeakHandler = new TcpClient_BespeakSeat.TcpClient_BespeakSeatAllMethod(UserSchoolInfo);
                }
            }
            else
            {
                Response.Redirect(string.Format("/Login.aspx?url=/BookSeat/ScanCode.aspx?param={0}", strparam));
            }
            if (!IsPostBack)
            {
                DataBind(cardNo, param.SeatNum, param.ReadingRoomNum);
            }
            else
            {
                string cmd = Request.Form["subCmd"];
                string result;
                switch (cmd)
                {
                    case "changeSeat":
                        result = this.BespeakHandler.ChangeSeat(this.UserSchoolInfo, cardNo, param.SeatNum, param.ReadingRoomNum);
                        if (!string.IsNullOrEmpty(result))
                        {
                            this.spanWarmInfo.InnerText = result;
                        }
                        else
                        {
                            this.spanWarmInfo.InnerText = "更换座位成功";
                            DataBind(cardNo, param.SeatNum, param.ReadingRoomNum);
                            //this.divHanderPanel.Style.Add("display", "none"); 
                        }
                        break;
                    case "bespeak":
                        SeatManage.ClassModel.BespeakLogInfo bespeakModel = new SeatManage.ClassModel.BespeakLogInfo();
                        bespeakModel.BsepeakState = SeatManage.EnumType.BookingStatus.Waiting;
                        DateTime bespeatDate = DateTime.Parse(string.Format("{0} {1}", DateTime.Now.AddDays(1).ToShortDateString(), lblBookTime.InnerText));
                        bespeakModel.BsepeakTime = bespeatDate;
                        bespeakModel.CardNo = cardNo;
                        bespeakModel.ReadingRoomNo = param.ReadingRoomNum;
                        bespeakModel.Remark = string.Format("读者通过扫码预约座位");
                        bespeakModel.SeatNo = param.SeatNum;
                        bespeakModel.SubmitTime = DateTime.Now;
                        try
                        {
                            result = this.BespeakHandler.SubmitBespeakInfo(this.UserSchoolInfo, bespeakModel); 
                            lblBeapeakMsg.InnerText = result;
                        }
                        catch
                            (Exception ex)
                        {
                            lblBeapeakMsg.InnerText = ex.Message;
                        }
                        break;
                }
            }
        }

        void DataBind(string cardNo, string seatNum, string readingRoomNum)
        {
            SeatManage.ClassModel.BespeakSeatModel.ScanCodeViewModel scmodel = this.BespeakHandler.GetScanCodeSeatInfo(UserSchoolInfo, cardNo, seatNum, readingRoomNum);
            if (scmodel != null &&
                scmodel.SeatInfo != null)
            {
                this.lblRoomName.InnerText = scmodel.SeatInfo.ReadingRoom.Name;
                this.lblSeatNum.InnerText = scmodel.SeatInfo.ShortSeatNo;
                canBespeak(scmodel);
                switch (scmodel.SeatInfo.SeatUsedState)
                {
                    case SeatManage.EnumType.EnterOutLogType.ComeBack:
                    case SeatManage.EnumType.EnterOutLogType.ContinuedTime:
                    case SeatManage.EnumType.EnterOutLogType.ReselectSeat:
                    case SeatManage.EnumType.EnterOutLogType.ShortLeave:
                    case SeatManage.EnumType.EnterOutLogType.WaitingSuccess:
                    case SeatManage.EnumType.EnterOutLogType.SelectSeat:
                    case SeatManage.EnumType.EnterOutLogType.BookingConfirmation:
                    case SeatManage.EnumType.EnterOutLogType.BespeakWaiting:
                        this.lblSeatState.InnerText = "使用中";
                        lblSeatState.Style.Add("color", "Red");
                        break;
                    default:
                        this.lblSeatState.InnerText = "空闲";
                        lblSeatState.Style.Add("color", "Green");
                        break;
                }
                SeatBusy(scmodel, cardNo, seatNum, readingRoomNum);
            }
        }

        private void canBespeak(SeatManage.ClassModel.BespeakSeatModel.ScanCodeViewModel scmodel)
        {
            try
            {
                if (scmodel.BespeakLog != null)
                {
                    lblBeapeakMsg.InnerText = string.Format("您已经预约了{0} {1}座位,到时可直接刷卡入座。", scmodel.BespeakLog.ReadingRoomName, scmodel.BespeakLog.ShortSeatNum);
                    btnBespeak.Style.Add("display", "none");
                    return;
                }
                if (scmodel.SeatInfo.CanBeBespeak)
                {
                    DateTime dtTomorrow = DateTime.Now.AddDays(1);
                    DateTime opDT = DateTime.Parse(string.Format("{0} {1}", dtTomorrow.ToShortDateString(), scmodel.SeatInfo.ReadingRoom.Setting.RoomOpenSet.DefaultOpenTime.BeginTime));
                    DateTime openTime = scmodel.SeatInfo.ReadingRoom.Setting.RoomOpenSet.DateOpenTime(dtTomorrow);
                    string beforeTime = openTime.AddMinutes(-int.Parse(scmodel.SeatInfo.ReadingRoom.Setting.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString();
                    string endTime = openTime.AddMinutes(int.Parse(scmodel.SeatInfo.ReadingRoom.Setting.SeatBespeak.ConfirmTime.EndTime)).ToShortTimeString();
                    lblBeapeakMsg.InnerText = string.Format("该座位可预约，预约时间为：{0}，需要在{1}到{2}刷卡确认。", openTime.ToShortTimeString(), beforeTime, endTime);
                    lblBookTime.InnerText = scmodel.SeatInfo.ReadingRoom.Setting.RoomOpenSet.DateOpenTime(DateTime.Now.AddDays(1)).ToShortTimeString();
                }
                else
                {
                    lblBeapeakMsg.InnerText = "该座位已被预约，或者不可预约";
                    btnBespeak.Style.Add("display", "none");
                }
            }
            catch (Exception ex)
            {
                lblBeapeakMsg.InnerText = "判断座位是否可预约出错。";
                btnBespeak.Style.Add("display", "none");
            }
        }

        void SeatBusy(SeatManage.ClassModel.BespeakSeatModel.ScanCodeViewModel model, string cardNo, string seatNum, string readingRoomNum)
        {

            if (model.ReaderInfo != null)
            {
                ReaderInfo.InnerText = string.Format("{0}您好", string.IsNullOrEmpty(model.ReaderInfo.Name) ? model.ReaderInfo.CardNo : model.ReaderInfo.Name);
                //读者有预约的处理
                if (model.ReaderInfo.BespeakLog.Count > 0)
                {
                    if (model.ReaderInfo.BespeakLog[0].SeatNo == seatNum && model.ReaderInfo.BespeakLog[0].ReadingRoomNo == readingRoomNum)
                    {
                        string result = this.BespeakHandler.ChangeSeat(this.UserSchoolInfo, cardNo, seatNum, readingRoomNum);
                        if (!string.IsNullOrEmpty(result))
                        {
                            spanWarmInfo.InnerText = "预约入座确认成功！";
                            DataBind(cardNo, seatNum, readingRoomNum);
                           // divHanderPanel.Style.Add("display", "none");
                            return;
                        }
                        else
                        {
                            spanWarmInfo.InnerText = result;
                            divHanderPanel.Style.Add("display", "none");
                        }
                    }
                    else
                    {
                        lblReaderMsg.InnerText = string.Format("您预约了{0} {1}座位，请直接扫描该座位上的二维码确认入座。", model.ReaderInfo.BespeakLog[0].ReadingRoomName, model.ReaderInfo.BespeakLog[0].ShortSeatNum);
                        btnChangeSeat.Style.Add("display", "none");
                        return;
                    }
                }
                //读者有等待的处理
                else if (model.ReaderInfo.WaitSeatLog != null)
                {
                    lblReaderMsg.InnerText = string.Format("您在等待其他座位，请在触摸屏上刷卡取消等待后再进行操作。");
                    btnChangeSeat.Style.Add("display", "none");
                    return;
                }
                //判断读者是否有座位
                else if (model.ReaderInfo.EnterOutLog!= null && model.ReaderInfo.EnterOutLog.EnterOutState != SeatManage.EnumType.EnterOutLogType.Leave)
                {
                    if (model.ReaderInfo.EnterOutLog.SeatNo == seatNum)
                    {
                        lblReaderMsg.InnerText = string.Format("您当前正在使用该座位");
                        btnChangeSeat.Style.Add("display", "none");

                    }
                    else
                    {
                        switch (model.SeatInfo.SeatUsedState)
                        {
                            case SeatManage.EnumType.EnterOutLogType.ComeBack:
                            case SeatManage.EnumType.EnterOutLogType.ContinuedTime:
                            case SeatManage.EnumType.EnterOutLogType.ReselectSeat:
                            case SeatManage.EnumType.EnterOutLogType.ShortLeave:
                            case SeatManage.EnumType.EnterOutLogType.WaitingSuccess:
                            case SeatManage.EnumType.EnterOutLogType.SelectSeat:
                            case SeatManage.EnumType.EnterOutLogType.BookingConfirmation:
                            case SeatManage.EnumType.EnterOutLogType.BespeakWaiting:
                                lblReaderMsg.InnerText = string.Format("该座位正在被其他读者使用。您当前在{0} {1}座位。", model.ReaderInfo.EnterOutLog.ReadingRoomName, model.ReaderInfo.EnterOutLog.ShortSeatNo);
                                btnChangeSeat.Style.Add("display", "none");
                                break;
                            default:
                                lblReaderMsg.InnerText = string.Format("您当前在{0} {1}座位，点击下方更换按钮，更换到该座位", model.ReaderInfo.EnterOutLog.ReadingRoomName, model.ReaderInfo.EnterOutLog.ShortSeatNo);

                                break;

                        }
                    }
                }
                else
                {
                    lblReaderMsg.InnerText = "您当前没有座位，你在终端上选座之后再执行该操作。";
                    btnChangeSeat.Style.Add("display", "none");
                }
            }
        }
    }
}