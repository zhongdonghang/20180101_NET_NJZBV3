using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AMS.Model;
using AMS.ServiceProxy;
using WeiXinJK;

namespace WeiXinServiceManage
{
    public partial class AdvertiseFrom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }
        }

        public override void DataBind()
        {
            List<WeiXinAdvertse> list = WeiXinProxy.GetAllAdvertise();
            GVAdvertise.DataSource = list;
            GVAdvertise.DataBind();
        }

        protected void btnAdv_Click(object sender, EventArgs e)
        {
            int y = 0;
            string id = "";
            for (int i = 0; i < this.GVAdvertise.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)this.GVAdvertise.Rows[i].FindControl("cbox");
                if (chk.Checked == true)
                {
                    id += chk.ToolTip + ",";
                    y++;
                }
            }
            if (y <= 0)
            {
                
                this.Label5.Text = "请勾选后在进行发布";
                return;
            }
            if (y > 10)
            {
                this.Label5.Text = "勾选发布广告不允许超过10条";
                return;
            }
            string ids = id.Substring(0, id.Length - 1);
            List<string> list = WeiXinProxy.GetAdvertseList(ids);
            IWeiXinAdvertService iwx = new WeiXinAdvertService();
            string str = iwx.SendArticleToAll(list);
        }

    }
}