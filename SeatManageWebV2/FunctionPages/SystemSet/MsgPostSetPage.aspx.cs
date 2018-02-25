using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeatManageWebV2.FunctionPages.SystemSet
{
    public partial class MsgPostSetPage : BasePage
    {
        SeatManage.ClassModel.MsgPostSet msgpostSet = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!OpVerifiction())
                {
                    Response.Write("请使用正常方式访问网站！");
                    Response.End();
                }
                BindData();
                btnAddItem.OnClientClick = MsgPost.GetShowReference("MsgPostItemEdit.aspx?flag=add", "添加消息推送路径");//为btnAddReadRoom新增点击事件
            }
        }

        void BindData()
        {
            msgpostSet = SeatManage.Bll.T_SM_SystemSet.GetMsgPostSet();
            if (msgpostSet != null)
            {
                DataTable dt = new DataTable();
                DataColumn UserName = new DataColumn("UserName", typeof(string));
                DataColumn AppID = new DataColumn("AppID", typeof(string));
                DataColumn PostUrl = new DataColumn("PostUrl", typeof(string));
                dt.Columns.Add(UserName);
                dt.Columns.Add(AppID);
                dt.Columns.Add(PostUrl);
                foreach (SeatManage.ClassModel.MsgPostItem item in msgpostSet.PostItems)
                {
                    DataRow row = dt.NewRow();
                    if (item.UserName == "juneberry")
                    {
                        continue;
                    }
                    row["UserName"] = item.UserName; 
                    row["AppID"]=item.AppID;
                    row["PostUrl"]=item.PostUrl;
                 
                    dt.Rows.Add(row);
                }
                string sortField = GridMsgPostItems.Columns[GridMsgPostItems.SortColumnIndex].SortField;
                string sortDirection = GridMsgPostItems.SortDirection;
                DataView TableView = dt.DefaultView;
                TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
                GridMsgPostItems.DataSource = TableView;
                GridMsgPostItems.DataBind();
            }
        }
        protected void Grid_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            if (e.CommandName == "ActionDelete")
            { //执行删除操作
                string userName = GridMsgPostItems.Rows[e.RowIndex].DataKeys[0].ToString();
                foreach (SeatManage.ClassModel.MsgPostItem item in msgpostSet.PostItems)
                {
                    if (item.UserName == userName)
                    {
                        msgpostSet.PostItems.Remove(item);
                        break;
                    }
                }

                try
                {
                    SeatManage.Bll.T_SM_SystemSet.SaveMsgPostSet(msgpostSet);
                    BindData();
                }
                catch (Exception ex)
                {
                    FineUI.Alert.ShowInTop("执行出现问题！");
                }
 
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            GridMsgPostItems.SortDirection = e.SortDirection;
            GridMsgPostItems.SortColumnIndex = e.ColumnIndex;
            BindData();
        }

        /// <summary>
        /// 编辑窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WindowEdit_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            BindData();
        }
    }
}