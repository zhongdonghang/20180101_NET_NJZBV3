using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using SeatManageWebV2.Code;

namespace SeatManageWebV2.FunctionPages.SeatBespeak
{
    /// <summary>
    /// SeatLayoutHandle 的摘要说明
    /// </summary>
    public class SeatLayoutHandle : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");



            if (!IsCanBespeak(context.Request.Params["roomNum"], context.Request.Params["date"]))
            {
                BasePage.WriteLogs("阅览室布局页面");
                context.Response.Write("你的操作不合法，请使用正确途径预约座位");
            }
            else
            {
                context.Response.Write(drowSeatLayoutHtml(context.Request.Params["roomNum"], context.Request.Params["date"], context.Request.Params["divTransparentTop"], context.Request.Params["divTransparentLeft"]));
            }
        }

        /// <summary>
        /// 绘制Html的座位布局
        /// </summary>
        /// <param name="roomNum">房间编号</param>
        /// <param name="divTransparentTop">透明层距离顶部的位置</param>
        /// <param name="divTransparentLeft">透明层距离左边的位置</param>
        /// <returns></returns>
        private string drowSeatLayoutHtml(string roomNum, string strDate, string divTransparentTop, string divTransparentLeft)
        {
            DateTime date;
            try
            {
                date = DateTime.Parse(strDate);
            }
            catch
            {
                return "日期格式不正确";
            }
            //if (string.IsNullOrEmpty(divTransparentTop) || string.IsNullOrEmpty(divTransparentLeft))
            //{
            //    divTransparentTop = "0";
            //    divTransparentLeft = "0";
            //}
            try
            {
                SeatManage.ClassModel.SeatLayout _SeatLayout = SeatManage.Bll.T_SM_SeatBespeak.GetBeseakSeatLayout(roomNum, date);
                if (_SeatLayout == null)
                {
                    return "获取阅览室座位布局出错了";
                }
                StringBuilder seatLayoutHtml = new StringBuilder();
                //布局实际图
                //绘制实际图窗体大小
                //实际图的长宽
                double layoutWidth = (double)(_SeatLayout.SeatCol * 18);
                double layoutHeight = (double)(_SeatLayout.SeatRow * 18);
                //实际图和缩略图的比例
                double scaleX = (double)layoutWidth / 300;
                double scaleY = (double)layoutHeight / 300;
                double transparentScaleX = layoutWidth / 1245;
                double transparentScaleY = layoutHeight / 685;
                double moveX = 0;
                double moveY = 0;
                if (layoutWidth >= layoutHeight)
                {
                    scaleY = scaleX;
                    moveY = (layoutWidth - layoutHeight) / 2 / scaleY;
                }
                else
                {
                    scaleX = scaleY;
                    moveX = (layoutHeight - layoutWidth) / 2 / scaleX;
                }
                //实际图当前的坐标位置
                if (string.IsNullOrEmpty(divTransparentTop) || string.IsNullOrEmpty(divTransparentLeft))
                {
                    divTransparentTop = moveY.ToString();
                    divTransparentLeft = moveX.ToString();
                }
                double layoutTop = (moveY - double.Parse(divTransparentTop)) * scaleY;
                double layoutLeft = (moveX - double.Parse(divTransparentLeft)) * scaleX;
                seatLayoutHtml.Append("<div id='divSeatLayoutFrom' class='SeatLayoutFrom'>");
                seatLayoutHtml.AppendFormat("<div id='divSeatLayout' class='SeatLayout' style='height:{0}px; width:{1}px;top:{2}px;left:{3}px'>", layoutHeight, layoutWidth, layoutTop, layoutLeft);
                //布局座位
                foreach (SeatManage.ClassModel.Seat seat in _SeatLayout.Seats.Values)
                {
                    string shortleaveimg = "../../Images/Node/note_blank.png";
                    string powerimh = "../../Images/Node/note_blank.png";
                    string readerimg = "../../Images/Node/note_blank.png";
                    //string seatStyle = "";//座位样式
                    string seatTop = (18 * seat.PositionY).ToString();//座位顶部位置
                    string seatLeft = (18 * seat.PositionX).ToString();//座位左边位置
                    string urlParameters = "";
                    string tipContent = "";

                    if (seat.HavePower)
                    {
                        powerimh = "../../Images/SeatImage/ImgPower.png";
                    }
                    if (seat.IsSuspended || !seat.CanBeBespeak)
                    {
                        readerimg = "../../Images/SeatImage/ImgStopUse.png";
                        tipContent = "不可预约";
                        urlParameters = "";
                    }
                    else if (seat.CanBeBespeak)
                    {
                        tipContent = "可预约";
                        urlParameters = string.Format("seatNo={0}&seatShortNo={1}&date={2}&roomNo={3}", seat.SeatNo, seat.ShortSeatNo, date.ToBinary(), roomNum);
                    }

                    if (seat.SeatUsedState == SeatManage.EnumType.EnterOutLogType.BookingConfirmation)
                    {
                        readerimg = "../../Images/SeatImage/Imgbook.png";
                        tipContent = string.Format("已经被预约<br />学号：{0}", seat.UserCardNo);
                        urlParameters = "";
                    }

                    if (!string.IsNullOrEmpty(urlParameters))
                    {
                        urlParameters = SeatManage.SeatManageComm.AESAlgorithm.DESEncode(urlParameters);
                    }
                    string seatdiv = "<div id='{0}' class='SeatBackground' style='left: {1}px; top: {2}px;width: 36px; height: 36px;transform: rotate({3}deg); -o-transform: rotate({3}deg); -webkit-transform: rotate({3}deg);-moz-transform: rotate({3}deg);'  onclick='BespeakSeatClick(\"{4}\")' onmouseover='tipShow(this,\"{5}\")' onmouseout='tipHidden(this)'>" +
                                     "<img src='../../Images/SeatImage/ImgSeat.png' height='34px' width='34px' />" +
                                    "<div style='margin-top: -34px; margin-left: 24px; height: 12px; width: 12px'>" +
                                     "<img src='{6}' height='12px' width='12px' />" +
                                     "</div>" +
                                     "<div style='margin-top: 12px; margin-left: 0px; height: 12px; width: 12px'>" +
                                     "<img src='{7}' height='12px' width='12px' />" +
                                     "</div>" +
                                     "<div style='margin-top: -28px; margin-left: 6px; height: 24px; width: 24px'>" +
                                     "<img src='{8}' height='24px' width='24px' />" +
                                     "</div>" +
                                     "<div style='margin-top: -34px; margin-left: 0px;color:white; transform: rotate(-{3}deg); -o-transform: rotate(-{3}deg);" +
                                     "-webkit-transform: rotate(-{3}deg); -moz-transform: rotate(-{3}deg);'>{9}</div></div>";
                    seatLayoutHtml.AppendFormat(seatdiv, seat.SeatNo, seatLeft, seatTop, seat.RotationAngle, urlParameters, tipContent, shortleaveimg, powerimh, readerimg, seat.ShortSeatNo);

                    //seatLayoutHtml.AppendFormat("<div id='{0}'   class='{1}' style='left: {2}px; top: {3}px;width: {4}px;height: {5}px;transform: rotate({9}deg);-o-transform: rotate({9}deg);-webkit-transform: rotate({9}deg);-moz-transform: rotate({9}deg)' onclick='BespeakSeatClick(\"{6}\")' onmouseover='tipShow(this,\"{7}\")' onmouseout='tipHidden(this)'><div style='transform: rotate(-{9}deg);-o-transform: rotate(-{9}deg);-webkit-transform: rotate(-{9}deg);-moz-transform: rotate(-{9}deg)'>{8}</div></div> ", seat.SeatNo, seatStyle, seatLeft, seatTop, 42, 42, urlParameters, tipContent, seat.ShortSeatNo, seat.RotationAngle);
                }
                //备注
                int countNode = 0;
                foreach (SeatManage.ClassModel.Note note in _SeatLayout.Notes)
                {
                    countNode++;
                    string seatTop = (18 * note.PositionY).ToString();//座位顶部位置
                    string seatLeft = (18 * note.PositionX).ToString();//座位左边位置 
                    string noteImage = "note_blank";
                    switch (note.Type)
                    {
                        case SeatManage.EnumType.OrnamentType.AirConditioning:
                            noteImage = "note_AirConditioning";
                            break;
                        case SeatManage.EnumType.OrnamentType.Bookshelf:
                            noteImage = "note_Bookshelf";
                            break;
                        case SeatManage.EnumType.OrnamentType.Door:
                            noteImage = "note_Door";
                            break;
                        case SeatManage.EnumType.OrnamentType.PCTable:
                            noteImage = "note_PCTable";
                            break;
                        case SeatManage.EnumType.OrnamentType.Pillar:
                            noteImage = "note_Pillar";
                            break;
                        case SeatManage.EnumType.OrnamentType.Plant:
                            noteImage = "note_Plant";
                            break;
                        case SeatManage.EnumType.OrnamentType.Roundtable:
                            noteImage = "note_Roundtable";
                            break;
                        case SeatManage.EnumType.OrnamentType.Steps:
                            noteImage = "note_Steps";
                            break;
                        case SeatManage.EnumType.OrnamentType.Table:
                            noteImage = "note_Table";
                            break;
                        case SeatManage.EnumType.OrnamentType.Wall:
                            noteImage = "note_Wall";
                            break;
                        case SeatManage.EnumType.OrnamentType.Window:
                            noteImage = "note_Window";
                            break;
                        case SeatManage.EnumType.OrnamentType.Elevator:
                            noteImage = "note_Elevator";
                            break;
                        case SeatManage.EnumType.OrnamentType.Stairway:
                            noteImage = "note_Stairway";
                            break;
                    }
                    seatLayoutHtml.AppendFormat("<div id='{0}' class='note_blank' style='left: {1}px; top: {2}px;width: {3}px;height: {4}px;transform: rotate({6}deg);-o-transform: rotate({6}deg);-webkit-transform: rotate({6}deg);-moz-transform: rotate({6}deg); '><img  src='../../Images/Node/{7}.png' style='width: {3}px;height: {4}px;'/><div style='margin-top: {8}px;transform: rotate(-0deg); -o-transform: rotate(-0deg);-webkit-transform: rotate(-0deg); -moz-transform: rotate(-0deg);'>{5}</div></div>", countNode, seatLeft, seatTop, note.BaseWidth * 18, note.BaseHeight * 18, note.Remark, note.RotationAngle, noteImage, -note.BaseHeight * 9 - 18);
                }
                seatLayoutHtml.Append("</div>");
                seatLayoutHtml.Append("</div>");

                //缩略图
                seatLayoutHtml.AppendFormat("<div id='divThumbnail' class='Thumbnail' onclick='ThumbnailClick(this,event,{0},{1},{2},{3})' >", scaleX, scaleY, moveX, moveY);
                foreach (SeatManage.ClassModel.Seat seat in _SeatLayout.Seats.Values)
                {
                    double width = 36 / scaleX;
                    double height = 36 / scaleY;
                    string style = "";
                    switch (seat.SeatUsedState)
                    {
                        case SeatManage.EnumType.EnterOutLogType.Leave:
                            style = "ThumbnailSeatFree";
                            break;
                        case SeatManage.EnumType.EnterOutLogType.SelectSeat:
                        case SeatManage.EnumType.EnterOutLogType.ReselectSeat:
                        case SeatManage.EnumType.EnterOutLogType.ComeBack:
                        case SeatManage.EnumType.EnterOutLogType.ContinuedTime:
                        case SeatManage.EnumType.EnterOutLogType.ShortLeave:
                        case SeatManage.EnumType.EnterOutLogType.BookingConfirmation:

                            style = "ThumbnailSeatUsing";
                            break;
                    }
                    seatLayoutHtml.AppendFormat("<div id='t{0}' class='{1}' style='left: {2}px; top: {3}px; width: {4}px;height: {5}px;transform: rotate({6}deg);-o-transform: rotate({6}deg);-webkit-transform: rotate({6}deg);-moz-transform: rotate({6}deg)'></div>", seat.SeatNo, style, (18 * seat.PositionX + 3) / scaleX + moveX, (18 * seat.PositionY + 3) / scaleY + moveY, width, height, seat.RotationAngle);
                }
                foreach (SeatManage.ClassModel.Note note in _SeatLayout.Notes)
                {
                    double width = 18 * note.BaseWidth / scaleX;
                    double height = 18 * note.BaseHeight / scaleY;
                    string style = "";
                    switch (note.Type)
                    {
                        case SeatManage.EnumType.OrnamentType.Plant:
                        case SeatManage.EnumType.OrnamentType.Roundtable:
                            seatLayoutHtml.AppendFormat("<div id='note' class='ThumbnailNote' style='left: {1}px; top: {2}px; width: {3}px;height: {4}px; border:1px solid #F5BF36; -webkit-border-radius:{5}px; -moz-border-radius:{5}px; -o-border-radius:{5}px; border-radius:{5}px;'></div>", style, (double)(18 / scaleX * note.PositionX) + (double)moveX, (double)(18 / scaleY * note.PositionY) + (double)moveY, (double)width - 1, (double)height - 1, (double)height);
                            continue;
                        case SeatManage.EnumType.OrnamentType.AirConditioning:
                        case SeatManage.EnumType.OrnamentType.Bookshelf:
                        case SeatManage.EnumType.OrnamentType.PCTable:
                        case SeatManage.EnumType.OrnamentType.Pillar:
                        case SeatManage.EnumType.OrnamentType.Steps:
                        case SeatManage.EnumType.OrnamentType.Table:
                        case SeatManage.EnumType.OrnamentType.Wall:
                        case SeatManage.EnumType.OrnamentType.Elevator:
                            style = "ThumbnailNote";
                            break;
                    }

                    seatLayoutHtml.AppendFormat("<div id='note' class='{0}' style='left: {1}px; top: {2}px; width: {3}px;height: {4}px;transform: rotate({5}deg);-o-transform: rotate({5}deg);-webkit-transform: rotate({5}deg);-moz-transform: rotate({5}deg); '></div>", style, (double)(18 / scaleX * note.PositionX) + (double)moveX, (double)(18 / scaleY * note.PositionY) + (double)moveY, (double)width, (double)height, note.RotationAngle);
                }
                seatLayoutHtml.AppendFormat("<div id='divTransparent' class='Transparent' style='left: {0}px; top: {1}px; width: {2}px; height: {3}px;'>", divTransparentLeft, divTransparentTop, (300 - 2 * moveX) / transparentScaleX, (300 - 2 * moveY) / transparentScaleY);
                seatLayoutHtml.Append("</div>");
                seatLayoutHtml.Append("</div>");
                return seatLayoutHtml.ToString();
            }
            catch (Exception ex)
            {
                return string.Format("布局座位分布遇到异常：{0}", ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 判断座位是否符合预约条件
        /// </summary>
        /// <returns></returns>
        protected bool IsCanBespeak(string roomNo, string selDate)
        {
            try
            {
                bool result = true;
                DateTime nowDate = SeatManage.Bll.ServiceDateTime.Now;
                SeatManage.ClassModel.ReadingRoomSetting set = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(roomNo).Setting;
                if (!set.SeatBespeak.Used)
                {
                    FineUI.Alert.ShowInTop("阅览室没有开放预约");
                    result = false;
                }
                if (!dateBespeak(set.SeatBespeak, nowDate, selDate))
                {
                    FineUI.Alert.ShowInTop("该日期不能预约");
                    result = false;
                }
                if (!timeCanBespeak(set.SeatBespeak, nowDate))
                {
                    FineUI.Alert.ShowInTop(string.Format("预约时间为：{0}到{1}", set.SeatBespeak.CanBespeatTimeSpace.BeginTime, set.SeatBespeak.CanBespeatTimeSpace.EndTime));
                    result = false;
                }
                return result;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 判断选择的日期是否可以预约，false为不可预约
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        private bool dateBespeak(SeatManage.ClassModel.SeatBespeakSet set, DateTime nowDate, string seldate)
        {
            DateTime selectedDate = DateTime.Parse(seldate);
            for (int i = 0; i < set.NoBespeakDates.Count; i++)
            {
                try
                {
                    DateTime beginDate = DateTime.Parse(set.NoBespeakDates[i].BeginTime);
                    DateTime endDate = DateTime.Parse(set.NoBespeakDates[i].EndTime);
                    if (SeatManage.SeatManageComm.DateTimeOperate.DateAccord(beginDate, endDate, selectedDate))
                    {//如果当前时间符合某个不可预约的规则，则直接返回false，不可预约
                        return false;
                    }
                }
                catch
                {//日期转换遇到异常，则忽略 
                }
            }
            //判断当天是否大于选择的日期
            TimeSpan span = selectedDate.Date - nowDate.Date;
            if (span.Days > set.BespeakBeforeDays)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 判断当前时间是否可以预约
        /// </summary>
        /// <param name="set"></param>
        /// <param name="nowDate"></param>
        /// <returns></returns>
        private bool timeCanBespeak(SeatManage.ClassModel.SeatBespeakSet set, DateTime nowDate)
        {
            try
            {
                DateTime beginTime = DateTime.Parse(string.Format("{0} {1}", nowDate.ToShortDateString(), set.CanBespeatTimeSpace.BeginTime));
                DateTime endTime = DateTime.Parse(string.Format("{0} {1}", nowDate.ToShortDateString(), set.CanBespeatTimeSpace.EndTime));
                if (SeatManage.SeatManageComm.DateTimeOperate.DateAccord(beginTime, endTime, nowDate))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return true;
            }
        }
    }
}