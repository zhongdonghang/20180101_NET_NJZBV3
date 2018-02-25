using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SeatManageWebV2.FunctionPages.RegulationRulesSetting
{
    public partial class DeviceStatusInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void DataBind()
        {
            List<SeatManage.ClassModel.TerminalInfoV2> clientlist = SeatManage.Bll.TerminalOperatorService.GetAllTeminalInfo();
            DataTable dt = new DataTable();
            dt.Columns.Add("DeviceNum", typeof(string));
            dt.Columns.Add("Describe", typeof(string));
            dt.Columns.Add("LastPrintTimes", typeof(string));
            dt.Columns.Add("PrintedTimes", typeof(string));
            dt.Columns.Add("PrinterStatus", typeof(string));
            dt.Columns.Add("Date", typeof(string));
            foreach (SeatManage.ClassModel.TerminalInfoV2 client in clientlist)
            {
                DataRow dr = dt.NewRow();
                dr["DeviceNum"] = client.ClientNo;
                dr["Describe"] = client.Describe;
                if (client.LastPrintTimes == 0)
                {
                    dr["LastPrintTimes"] = "暂无打印数据";
                }
                else
                {
                    dr["LastPrintTimes"] = client.LastPrintTimes.ToString();
                }
                dr["PrintedTimes"] = client.PrintedTimes.ToString();
                if (client.PrinterStatus)
                {
                    dr["PrinterStatus"] = "打印机有纸";
                }
                else
                {
                    dr["PrinterStatus"] = "打印机缺纸";
                }
                if (client.StatusUpdateTime == null)
                {
                    dr["Date"] = "暂无记录";
                }
                else
                {
                    dr["Date"] = client.StatusUpdateTime.ToString();
                }

                dt.Rows.Add(dr);
            }
            DataView TableView = dt.DefaultView;
            GridDevice.DataSource = TableView;
            GridDevice.DataBind();
        }
    }
}