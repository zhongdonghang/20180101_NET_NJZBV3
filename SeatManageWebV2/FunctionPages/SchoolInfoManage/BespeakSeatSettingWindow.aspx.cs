using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace SeatManageWebV2.FunctionPages.SchoolInfoManage
{
    public partial class BespeakSeatSettingWindow : BasePage
    {
        string seatNo = "";
        string canBook = "";
        string roomNo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            seatNo = Request.QueryString["seatNo"];
            canBook = Request.QueryString["canBook"];
            roomNo = Request.QueryString["roomNo"];
            //if (!IsPostBack)
            //{
            //    BindUIElement(seatNo, canBook);
            //}
            SetBook();
        }

        private void SetBook()
        {
            SeatManage.ClassModel.SeatLayout _SeatLayout = SeatManage.Bll.T_SM_SeatBespeak.GetBeseakSeatSettingLayout(roomNo);
            if (canBook == "nobook")
            {

                foreach (SeatManage.ClassModel.Seat seat in _SeatLayout.Seats.Values)
                {
                    if (seat.SeatNo == seatNo)
                    {
                        seat.CanBeBespeak = false;
                        _SeatLayout.RoomNo = roomNo;
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
            else
            {
                foreach (SeatManage.ClassModel.Seat seat in _SeatLayout.Seats.Values)
                {
                    if (seat.SeatNo == seatNo)
                    {
                        seat.CanBeBespeak = true;
                        _SeatLayout.RoomNo = roomNo;
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
            PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
        }
        //protected void btnCanBespeak_Click(object sender, EventArgs e)
        //{
            //SeatManage.ClassModel.SeatLayout _SeatLayout = SeatManage.Bll.T_SM_SeatBespeak.GetBeseakSeatSettingLayout(roomNo);
            //if (IsBespeakSeatSelect.SelectedValue == "0")
            //{

            //    foreach (SeatManage.ClassModel.Seat seat in _SeatLayout.Seats.Values)
            //    {
            //        if (seat.SeatNo == seatNo)
            //        {
            //            seat.CanBeBespeak = false;
            //            _SeatLayout.RoomNo = roomNo;
            //            if (SeatManage.Bll.T_SM_ReadingRoom.UpdateSeatLayout(_SeatLayout) == SeatManage.EnumType.HandleResult.Failed)
            //            {
            //                FineUI.Alert.ShowInTop("设置失败");
            //            }
            //            else
            //            {
            //                FineUI.Alert.ShowInTop("设置成功");
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    foreach (SeatManage.ClassModel.Seat seat in _SeatLayout.Seats.Values)
            //    {
            //        if (seat.SeatNo == seatNo)
            //        {
            //            seat.CanBeBespeak = true;
            //            _SeatLayout.RoomNo = roomNo;
            //            if (SeatManage.Bll.T_SM_ReadingRoom.UpdateSeatLayout(_SeatLayout) == SeatManage.EnumType.HandleResult.Failed)
            //            {
            //                FineUI.Alert.ShowInTop("设置失败");
            //            }
            //            else
            //            {
            //                FineUI.Alert.ShowInTop("设置成功");
            //            }
            //        }
            //    }
            //}
            //PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
        //}
        //protected void btnClose_Click(object sender, EventArgs e)
        //{
        //    PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
        //}

        protected void IsBespeakSeatSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeatManage.ClassModel.SeatLayout _SeatLayout = SeatManage.Bll.T_SM_SeatBespeak.GetBeseakSeatSettingLayout(roomNo);
            if (IsBespeakSeatSelect.SelectedValue == "0")
            {

                foreach (SeatManage.ClassModel.Seat seat in _SeatLayout.Seats.Values)
                {
                    if (seat.SeatNo == seatNo)
                    {
                        seat.CanBeBespeak = false;
                        _SeatLayout.RoomNo = roomNo;
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
            else
            {
                foreach (SeatManage.ClassModel.Seat seat in _SeatLayout.Seats.Values)
                {
                    if (seat.SeatNo == seatNo)
                    {
                        seat.CanBeBespeak = true;
                        _SeatLayout.RoomNo = roomNo;
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
            PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 绑定UI元素
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="seatShortNo"></param>GetBespeakLogInfoBySeatNo
        void BindUIElement(string seatNo, string seatShortNo)
        {
            SeatManage.ClassModel.ReadingRoomInfo room = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(roomNo);
            this.lblSeatNo.Text = seatShortNo;
            this.lblRoomName.Text = room.Name;
            SeatManage.ClassModel.SeatLayout _SeatLayout = SeatManage.Bll.T_SM_SeatBespeak.GetBeseakSeatSettingLayout(roomNo);
            foreach (SeatManage.ClassModel.Seat seat in _SeatLayout.Seats.Values)
            {
                if (seat.SeatNo == seatNo)
                {
                    //判断点击座位是否提供预约
                    if (seat.CanBeBespeak == true)
                    {
                        IsBespeakSeatSelect.SelectedValue = "1";
                    }
                    else
                    {
                        IsBespeakSeatSelect.SelectedValue = "0";
                    }
                }
            }
        }
    }
}