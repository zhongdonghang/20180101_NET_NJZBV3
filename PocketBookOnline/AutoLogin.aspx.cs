using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManage.PocketBespeak;
using WeiXinJK.Model;

namespace PocketBookOnline
{
    public partial class AutoLogin : BasePage
    {
        //参数：?parameters=
        //cardNo=20313304&schoolId=2&operateKey=bespeak
        Code.AutoLoginParamModel paramObj = null; 
        protected void Page_Load(object sender, EventArgs e)
        {
            string parameters = Request.QueryString["parameters"];
            paramObj = Code.AutoLoginParamModel.Parse(parameters);
            this.UserSchoolInfo = new TcpClient_BespeakSeat.TcpClient_Login().GetSingleSchoolInfo (paramObj.SchoolId);
            this.BespeakHandler = new TcpClient_BespeakSeat.TcpClient_BespeakSeatAllMethod(this.UserSchoolInfo);
            this.LoginUserInfo = BespeakHandler.GetReaderInfoByCardNo(paramObj.CardNo, this.UserSchoolInfo);
            pageTransfer();
            Response.End();
           // Server.Transfer("");
        }
        /// <summary>
        /// 修改此处
        /// </summary>
        private void pageTransfer()
        {
            switch (paramObj.OperateKey)
            { 
                case EnumMenuKey.ReserveSeat://预约座位
                    Server.Transfer("BookSeat/BookSeatListForm.aspx");
                    break;
                case EnumMenuKey.BlackList://黑名单
                    Server.Transfer("UserInfos/BlackList.aspx");
                    break;
                case EnumMenuKey.GetBespeakLog://预约记录
                    Server.Transfer("UserInfos/QueryLogs.aspx");
                    break;
            }
        }
    }
}