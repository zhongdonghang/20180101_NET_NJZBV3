using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudyRoomWeb.FunctionPages.BespeakStudyRoom
{
    public partial class BigImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["imageurl"] != null)
            {
                imgRoomImage.ImageUrl = Server.UrlDecode(Request.QueryString["imageurl"]);
            }
        }
    }
}