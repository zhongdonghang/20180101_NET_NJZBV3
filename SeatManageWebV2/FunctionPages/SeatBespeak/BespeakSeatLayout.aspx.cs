using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManageWebV2.Code;

namespace SeatManageWebV2.FunctionPages.SeatBespeak
{
    public partial class BespeakSeatLayout : BasePage
    {
        protected string bespeakDate = "";
        protected string roomNum = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!OpVerifiction())
                {
                    Response.Write("请使用正常方式访问网站！");
                    Response.End();
                    return;
                }
            }
            if (string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                Response.Write("你的操作不合法，请使用正确途径预约座位");
                Response.End();
                return;
            }
            BespeakSubmitWindowParamModel par = new BespeakSubmitWindowParamModel(Request.QueryString["Param"]);
            roomNum = par.RoomNo;
            try
            {
                bespeakDate = DateTime.FromBinary(long.Parse(par.BespeakDate)).ToString();
            }
            catch
            {
                FineUI.Alert.Show("预约日期不正确");
            }




            //roomNum = Request.QueryString["roomId"];
            //try
            //{
            //    bespeakDate = DateTime.FromBinary(long.Parse(Request.QueryString["date"])).ToString();
            //}
            //catch
            //{
            //    FineUI.Alert.Show("预约日期不正确");
            //}
        }
    }
}