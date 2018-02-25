using System;
using System.Web.UI.WebControls;
using SeatManage.AppJsonModel;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using WeiXinPocketBookOnline.Code;

namespace WeiXinPocketBookOnline.BookSeat
{
    public partial class BookSeatMessage : BasePage
    {

        private string seatNo = "";
        private string seatShortNo = "";
        private string date = "";
        private string roomNo = "";
        private readonly WeiXinService.IWeiXinService weiXinService = new WeiXinService.WeiXinServiceHepler();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginUserInfo == null || UserSchoolInfo == null)
            {
                Response.Redirect(LoginUrl());
            }
            seatNo = Request.QueryString["seatNo"];
            date = Request.QueryString["bookDate"];
            roomNo = Request.QueryString["roomNo"];
            if (!IsPostBack)
            {
                BindUIElement(seatNo, roomNo, date);
            }
            string cmd = Request.Form["subCmd"];
            switch (cmd)
            {
                case "select":
                    BindUIElement(seatNo, seatShortNo, date);
                    break;
                case "query":
                    BespeakLogInfo bespeakModel = new BespeakLogInfo();

                    DateTime bespeakTime = DateTime.Parse(string.Format("{0} {1}", DateTime.Parse(date).ToShortDateString(), lblBookTime.InnerText));
                    bool isNowDay = bespeakTime.Date == DateTime.Now.Date;
                    try
                    {
                        string resultValue = weiXinService.SubmitBesapeskSeat(seatNo, roomNo, LoginUserInfo.StudentNo, bespeakTime.ToString("yyyy-MM-dd HH:mm:ss"), isNowDay, UserSchoolInfo.SchoolNo);
                        page1.Style.Add("display", "none");
                        page2.Style.Add("display", "none");
                        page3.Style.Add("display", "block");
                        MessageTip.InnerText = resultValue;
                    }
                    catch (Exception ex)
                    {
                        page1.Style.Add("display", "none");
                        page2.Style.Add("display", "none");
                        page3.Style.Add("display", "block");
                        MessageTip.InnerText = ex.Message;
                    }
                    break;
            }
        }

        /// <summary>
        /// 绑定座位预约显示数据
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="date"></param>
        private void BindUIElement(string seatNo, string roomNo, string date)
        {
            #region 暂时注释

            if (!IsPostBack)
            {
                try
                {
                    BespeakSeatInfo = weiXinService.GetSeatBespeakInfo(seatNo, roomNo, date, UserSchoolInfo.SchoolNo);
                    //添加时间选择段
                    foreach (var dt in BespeakSeatInfo.TimeList)
                    {
                        timeSelect.Items.Add(new ListItem(dt, dt));
                    }
                    if (timeSelect.Items.Count > 0)
                    {
                        timeSelect.SelectedIndex = 0;
                    }
                    //添加预约方式选择
                    if (DateTime.Parse(date).Date == DateTime.Now.Date)
                    {
                        bookMode.Items.Add(new ListItem("立即预约", "now"));
                        if (BespeakSeatInfo.IsCanSelectTime)
                        {
                            bookMode.Items.Add(new ListItem("选择时间", "select"));
                        }
                        else
                        {
                            timeSelect.Visible = false;
                            timeSelect_sp.Visible = false;
                        }
                        bookMode.SelectedIndex = 0;
                    }
                    else
                    {
                        bookMode.Visible = false;
                        bookMode_sp.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    page1.Style.Add("display", "none");
                    page2.Style.Add("display", "none");
                    page3.Style.Add("display", "block");
                    MessageTip.InnerText = ex.Message;
                }


            }
            DateTime bespeakTime;
            DateTime bespeakBeginTime;
            DateTime bespeakEndTime;
            //选择时间判断
            if (bookMode.SelectedIndex == 1)
            {
                timeSelect.Visible = true;
                timeSelect_sp.Visible = true;
                bespeakTime = Convert.ToDateTime(timeSelect.Items[timeSelect.SelectedIndex].Value);
                bespeakBeginTime = bespeakTime.AddMinutes(-BespeakSeatInfo.CheckBeforeTime);
                bespeakEndTime = bespeakTime.AddMinutes(BespeakSeatInfo.CheckLastTime);
            }
            else
            {
                timeSelect.Visible = false;
                timeSelect_sp.Visible = false;
                bespeakTime = DateTime.Now;
                bespeakBeginTime = bespeakTime;
                bespeakEndTime = bespeakTime.AddMinutes(BespeakSeatInfo.CheckKeepTime);
            }
            lblBookDate.InnerText = BespeakSeatInfo.BespeakDate;
            lblSeatNo.InnerText = BespeakSeatInfo.SeatShortNo;
            lblReadingRoomName.InnerText = BespeakSeatInfo.RoomName;
            lblSeatNo_Booked.InnerText = BespeakSeatInfo.SeatShortNo;
            lbbookspan.InnerText = string.Format("{0}至{1}", bespeakBeginTime.ToShortTimeString(), bespeakEndTime.ToShortTimeString());
            lbbookspan.InnerText = string.Format(bespeakTime.ToShortTimeString());

            #endregion
        }

    }


}