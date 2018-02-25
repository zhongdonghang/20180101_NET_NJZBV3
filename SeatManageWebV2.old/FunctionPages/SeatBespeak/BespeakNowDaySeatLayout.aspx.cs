using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeatManageWebV2.FunctionPages.SeatBespeak
{
    public partial class BespeakNowDaySeatLayout : BasePage
    {
        protected string roomNum = "";
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
        }
    }
}