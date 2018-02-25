using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using WeiXinJK.Model;
using WeiXinJK;

namespace WeiXinServiceManage
{
    public partial class TheEventGenerator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("/Scripts/json.txt");
            string txt = File.ReadAllText(path, Encoding.Default);
            IWeiXinAdvertService ads = new WeiXinAdvertService();
            this.TextBox1.Text = ads.BuildUpMenu(txt);
        }
    }
}