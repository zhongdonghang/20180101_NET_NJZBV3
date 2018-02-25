using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManageWebV2.Code;
namespace SeatManageWebV2.FunctionPages.RegulationRulesSetting
{
    public partial class SyncReader : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string result = "";
            //if (Session["work"] == null)
            //{
            //    try
            //    {
            //        w = new WorkSync();
            //        Session["work"] = w;
            //    }
            //    catch (Exception ex)
            //    {
            //        result = string.Format("同步出错：构造同步程序时出现异常：{0}", ex.Message);
            //        Response.Write(result);
            //        return;
            //    }
            //}

            string parameter = Request.Params["param"];
            if (parameter != null)
            {
                switch (parameter)
                {
                    case "Sync":
                        result = SyncDoWork();
                        break;
                    case "Stop":
                        Abort();
                        break;
                }
                Response.Write(result);
                Response.End();
            }
        }


        protected WorkSync w;
        private string SyncDoWork()
        {
            if (Session["work"] == null)
            {
                w = new WorkSync();
                Session["work"] = w;
            }
            else
            {
                w = (WorkSync)Session["work"];
            }
            try
            {
                switch (w.State)
                {
                    case SeatManage.ISystemTerminal.IStuLibSync.SyncState.None:
                        w.runwork();
                        return "1:开始同步";
                    case SeatManage.ISystemTerminal.IStuLibSync.SyncState.Syncing:
                        return "1:已完成：" + w.Percent + " ％，已耗时：" + ((TimeSpan)(DateTime.Now - w.StartTime)).TotalSeconds.ToString("0") + "秒";
                    case SeatManage.ISystemTerminal.IStuLibSync.SyncState.Success:
                        string r = string.Format("2:任务结束,新增{0}条，更新{1}条，错误{2}条，用时{3}秒", w.AddAmount, w.UpdateAmount, w.ErrorAmount, (int)(((TimeSpan)(w.FinishTime - w.StartTime)).TotalSeconds));
                        //  Abort();
                        return r;

                }
                return "3:执行错误";
            }
            catch (Exception ex)
            {
                return string.Format("3:执行出错：{0}", ex.Message);
            }
        }

        public void Abort()
        {
            w = (WorkSync)Session["work"];
            w.Dispose();
            Session["work"] = null;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}