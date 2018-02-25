using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeatManageWebV2.FunctionPages.SchoolInfoManage
{
    public partial class BespeakSeatSetting : BasePage
    {
        protected string roomNum = "";
        public string cmd = "";
        public string seatNo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!OpVerifiction())
                {
                    Response.Write("请使用正常方式访问网站！");
                    Response.End();
                }
            }
            roomNum = Request.QueryString["roomId"];
            cmd = Request.Form["subCmd"];
            seatNo = Request.Form["seatNo"];
            if (cmd != null)
            {
                SeatManage.ClassModel.SeatLayout _SeatLayout = SeatManage.Bll.T_SM_SeatBespeak.GetBeseakSeatSettingLayout(roomNum);
                switch (cmd)
                {
                    case "setBook"://设置座位为可预约
                        try
                        {
                            foreach (SeatManage.ClassModel.Seat seat in _SeatLayout.Seats.Values)
                            {
                                if (seat.SeatNo == seatNo)
                                {
                                    seat.CanBeBespeak = true;
                                    _SeatLayout.RoomNo = roomNum;
                                    if (SeatManage.Bll.T_SM_ReadingRoom.UpdateSeatLayout(_SeatLayout) == SeatManage.EnumType.HandleResult.Failed)
                                    {
                                        FineUI.Alert.ShowInTop("设置失败");
                                    }
                                    else
                                    {

                                    }
                                }
                            }
                        }
                        catch
                        {

                        }
                        break;
                    case "setNoBook"://设置座位为不可预约
                        try
                        {
                            foreach (SeatManage.ClassModel.Seat seat in _SeatLayout.Seats.Values)
                            {
                                if (seat.SeatNo == seatNo)
                                {
                                    seat.CanBeBespeak = false;
                                    _SeatLayout.RoomNo = roomNum;
                                    if (SeatManage.Bll.T_SM_ReadingRoom.UpdateSeatLayout(_SeatLayout) == SeatManage.EnumType.HandleResult.Failed)
                                    {
                                        FineUI.Alert.ShowInTop("设置失败");
                                    }
                                    else
                                    {

                                    }
                                }
                            }
                        }
                        catch
                        {

                        }
                        break;
                }
            }
        }
    }
}