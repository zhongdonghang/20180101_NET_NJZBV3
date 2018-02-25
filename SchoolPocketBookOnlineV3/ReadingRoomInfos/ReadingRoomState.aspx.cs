using System;
using System.Collections.Generic;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.IPocketBespeakBllServiceV2;
using SeatManage.PocketBespeakBllServiceV2;
using SeatManage.SeatManageComm;

namespace SchoolPocketBookWeb.ReadingRoomInfos
{
    public partial class ReadingRoomState : BasePage
    {
        /// <summary>
        /// 
        /// </summary>
        public string roomUsedMessage = "";
        private IPocketBespeakBllService handler = new PocketBespeakBllService();
        //SeatManage.IPocketBespeak.IMainFunctionPageBll handler = new SeatManage.PocketBespeak.PocketBespeak_MainFunctionPageBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginUserInfo == null)
            {
                Response.Redirect(LoginUrl());
            }
            //List<SeatManage.ClassModel.ReadingRoomInfo> roomList = new List<SeatManage.ClassModel.ReadingRoomInfo>();
            //string endPonitAddress = Session["SchoolConnectionString"].ToString();
            //IReadingRoomStateBll bll = new ReadingRoomStateBll();
            //roomList = bll.GetReadingRoomList(endPonitAddress);
            BindDataList();

            string cmd = Request.Form["subCmd"];
            if (IsPostBack)
            {
                switch (cmd)
                {
                    case "LoginOut":
                        Session.Clear();
                        Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
                        CookiesManager.RefreshNum = 0;
                        Response.Redirect(LogoutUrl());
                        break;
                }
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomList"></param>
        private void BindDataList()
        {
            StringBuilder strRoomMessage = new StringBuilder();
            //IReadingRoomStateBll bll = new ReadingRoomStateBll();
            //List<string> roomNums = new List<string>();
            //for (int i = 0; i < roomList.Count; i++)
            //{
            //    roomNums.Add(roomList[i].No);
            //}
            strRoomMessage.Append("<ul data-role='listview' data-divider-theme='d' data-inset='true'><li data-role='list-divider' role='heading'>阅览室座位使用情况</li>");
            Dictionary<string, ReadingRoomSeatUsedState_Ex> dic = handler.GetAllRoomSeatUsedState();// bll.GetReadingRoomSeatUsingState(roomNums, Session["SchoolConnectionString"].ToString());
            foreach (ReadingRoomSeatUsedState_Ex seatUsedState in dic.Values)
            {
                strRoomMessage.Append(string.Format("<li data-theme='c'>{0}:剩余{1}个座位  <ul><li>总座位：{2}</li><li>在座：{3}</li><li>暂离：{4}</li><li>空闲：{5}</li><li><div data-role='button' style='width:90px; height=50px' data-mini='true' class='ui-btn-left' onclick='javascript:history.go(-1);'>返回 </div></li></ul></li>", seatUsedState.ReadingRoom.Name, seatUsedState.SeatAmountFree, seatUsedState.SeatAmountAll, seatUsedState.SeatAmountUsed - seatUsedState.SeatAmountShortLeave, seatUsedState.SeatAmountShortLeave, seatUsedState.SeatAmountFree));
            }

            //int freeSeatCount =roomList[i].SeatCount_All - roomList[i].SeatCount_Leave - roomList[i].SeatCount_Used;
            //strRoomMessage.Append(string.Format("<li data-theme='c'>{0}:剩余{1}个座位  <ul><li>总座位：{2}</li><li>在座：{3}</li><li>暂离：{4}</li><li>空闲：{5}</li><li><div data-role='button' style='width:90px; height=50px' data-mini='true' class='ui-btn-left' onclick='javascript:history.go(-1);'>返回 </div></li></ul></li>", roomList[i].Name, freeSeatCount, roomList[i].SeatCount_All, roomList[i].SeatCount_Used, roomList[i].SeatCount_Leave, freeSeatCount));

            strRoomMessage.Append("</ul>");
            roomUsedMessage = strRoomMessage.ToString();
        }
    }
}