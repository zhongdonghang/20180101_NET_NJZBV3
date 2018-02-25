using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolPocketBookOnlineV2.QRcodeDecode
{
    public partial class SeatInfoManage : BasePage
    {
        public string cmd;
        public string state;
        public string bookNo;
        public string cardNo;
        public string seatNo;
        public string readingRoomNo;
        public string listMessage = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //string strparam = Request.QueryString["param"];
            //if (string.IsNullOrEmpty(strparam))
            //{
            //    spanWarmInfo.InnerText = "非正常的访问！";
            //    divHanderPanel.Style.Add("display", "none");
            //    return;
            //}
            //Code.ScanCodeParamModel param = new Code.ScanCodeParamModel(strparam);
            //seatNo = param.SeatNum;
            //readingRoomNo = param.ReadingRoomNum;
            //if()

        }
    }
}