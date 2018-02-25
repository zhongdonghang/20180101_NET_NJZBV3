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
    public partial class UpAdvertis : System.Web.UI.Page
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
            if (Request.QueryString["id"] != null)
            {
                WeiXinAdvertse model = WeiXinProxy.GetAdvertseById(Convert.ToInt32(Request.QueryString["id"]));
                txtTitle.Text = model.Title;
                txtURl.Text = model.Url;
                preview.Src = model.Image;
                preview.Style.Add("display", "block");
                preview.Width = 90;
                preview.Height = 90;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            AMS_UserInfo userinfo = Session["Login"] as AMS_UserInfo;
            WeiXinAdvertse model = new WeiXinAdvertse
            {
                Title = txtTitle.Text,
                Url = txtURl.Text,
                LoginID = new AMS_UserInfo { ID = userinfo.ID },
                DateTime = DateTime.Now
            };
            bool fileOK = false;
            string path = "~/Temp/";
            if (FileUpload1.HasFile)
            {
                string a = FileUpload1.FileName;
                String fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                if (fileExtension == ".gif")
                {
                    fileOK = true;
                }
            }
            if (FileUpload1.FileName == "" && Request.QueryString["id"] == null)
            {
                this.lbltxt.Text = "请上传图片！";
               //this.RegisterStartupScript("错误提示", "<script>alert('图片上传不能等于空');</script>");
                return;
            }
            if (fileOK)
            {
                model.Image = path + FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath(path) + FileUpload1.FileName);
                model.ContentXML = AdvertiseToJson(model);
            }
            else
            {
                if (Request.QueryString["id"] != null)
                {
                    WeiXinAdvertse josns = WeiXinProxy.GetAdvertseById(Convert.ToInt32(Request.QueryString["id"]));
                    josns.Title = model.Title;
                    josns.Url = model.Url;
                    model.ContentXML = AdvertiseToJson(josns);
                }
            }
            if (Request.QueryString["id"] != null)
            {
                model.ID = Convert.ToInt32(Request.QueryString["id"]);
                WeiXinProxy.UpdateAdvertise(model);
                Response.Write("<script>window.location.href ='AdvertiseFrom.aspx'</script>");
            }
            else
            {
                if (WeiXinProxy.SaveAdvertise(model))
                {
                    Response.Write("<script>window.location.href ='AdvertiseFrom.aspx'</script>");
                }
                else
                {
                    this.lbltxt.Text = "添加失败";
                    //this.RegisterStartupScript("添加提示", "<script>alert('添加失败');</script>");
                }
            }
        }

        /// <summary>
        /// 广告JOSN
        /// </summary>
        /// <param name="advert"></param>
        /// <returns></returns>
        public string AdvertiseToJson(WeiXinAdvertse advert)
        {
            string json = "{ 'title':'" + advert.Title + "','description':'','url':'" + advert.Url + "','picurl':'" + advert.Image + "'}";
            //            string json = @"{
            //                        'thumb_media_id':'zbwxAdvertise',
            //                        'author':'" + Session["LoginID"].ToString() + "',";
            //            json += "'title':'" + advert.Title + "',";
            //            json += "'content_source_url':'" + advert.Url + "',";
            //            json += "'content':'content',";
            //            json += "'digest':'digest'}";
            json = json.Replace('\'', '\"');
            return json;
        }

        protected void lbUploadPhoto_Click(object sender, EventArgs e)
        {
            bool fileOK = false;
            string path = "~/Temp/";
            if (FileUpload1.HasFile)
            {
                string a = FileUpload1.FileName;
                String fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                if (fileExtension == ".gif")
                {
                    fileOK = true;
                }
            }
            if (fileOK)
            {
                this.lbltxt.Text = FileUpload1.FileName;
                //this.RegisterStartupScript("错误提示", "<script>alert('" + FileUpload1.FileName + "');</script>");
                //FileUpload1.SaveAs(Server.MapPath(path) + FileUpload1.FileName);
            }
        }

    }
}