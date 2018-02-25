using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinJK;

namespace WeiXinServiceManage
{
    public partial class KFXX : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            IWeiXinAdvertService service = new WeiXinAdvertService();
            service.SendTxtMessage(TextBox2.Text, TextBox1.Text);
        }
    }
}