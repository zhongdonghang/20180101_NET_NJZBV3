using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AMS.Model;
using AMS.ServiceProxy;

namespace WeiXinServiceManage
{
    public partial class TextsConfig : System.Web.UI.Page
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
            List<WeiXinResponse> list = WeiXinProxy.GetAllResponse();

            rptResponse.DataSource = list;
            rptResponse.DataBind();
        }

        /// <summary>
        /// 转换回复
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string convert(int type)
        {
            string str = "";

            switch (type)
            {
                case 0:
                    str = "关注";
                    break;
                case 1:
                    str = "文本";
                    break;
                case 2:
                    str = "图片";
                    break;

                case 3:
                    str = "语音";
                    break;
                case 4:
                    str = "视频";
                    break;
                case 5:
                    str = "地理";
                    break;
                case 6:
                    str = "链接";
                    break;

                default:
                    str = "未定义";
                    break;

            }
            return str;
        }

        protected void delete(object sender, CommandEventArgs e)
        {
            string ID = e.CommandArgument.ToString();
            WeiXinProxy.DeleResponse(int.Parse(ID));
            DataBind();
            //删除动作
        }
      
    }
}