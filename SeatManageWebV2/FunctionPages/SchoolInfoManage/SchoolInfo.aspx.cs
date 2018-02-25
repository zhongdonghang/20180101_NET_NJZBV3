using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FineUI;

namespace SeatManageWebV2.FunctionPages.SchoolInfoManage
{
    public partial class SchoolInfo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!OpVerifiction())
                {
                    Response.Write("请使用正常方式访问网站！");
                    Response.End();
                }
                btnAddSchool.OnClientClick = WindowEdit.GetShowReference("SchoolEdit.aspx?flag=add", "添加学校");
                BindGrid();
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string sortField = SchoolGrid.Columns[SchoolGrid.SortColumnIndex].SortField;
            string sortDirection = SchoolGrid.SortDirection;
            DataTable table = GetSchoolInfoDateTable();
            DataView TableView = table.DefaultView;
            TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
            SchoolGrid.DataSource = TableView;
            SchoolGrid.DataBind();
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        private DataTable GetSchoolInfoDateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SchoolNo", typeof(string));
            dt.Columns.Add("SchoolName", typeof(string));
            List<SeatManage.ClassModel.School> schoollist = SeatManage.Bll.T_SM_School.GetSchoolInfoList(null, null);
            foreach (SeatManage.ClassModel.School school in schoollist)
            {
                DataRow dr = dt.NewRow();
                dr["SchoolNo"] = school.No;
                dr["SchoolName"] = school.Name;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 行命令时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SchoolGrid_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            //if (e.CommandName == "ActionDelete")
            //{
            //    //FineUI.Label lbok = new FineUI.Label();
            //    //WindowDelete.GetSaveStateReference(lbok.Text);
            //    //WindowDelete.GetShowReference("../SystemSet/DeletePassword.aspx", "授权认证");
            //    //WindowDelete.
            //    //if (lbok.Text == "True") 
            //    //{
            //        SeatManage.ClassModel.School school = new SeatManage.ClassModel.School();
            //        school.No = SchoolGrid.Rows[e.RowIndex].DataKeys[0].ToString();
            //        if (!SeatManage.Bll.T_SM_School.DeleteSchool(school))
            //        {
            //            FineUI.Alert.ShowInTop("删除失败！");
            //        }
            //        else
            //        {
            //            FineUI.Alert.ShowInTop("删除完成！");
            //            BindGrid();
            //        }
            //    //}
            //}
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SchoolGrid_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            SchoolGrid.SortDirection = e.SortDirection;
            SchoolGrid.SortColumnIndex = e.ColumnIndex;
            BindGrid();
        }
        /// <summary>
        /// 页面切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SchoolGrid_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            SchoolGrid.PageIndex = e.NewPageIndex;
        }
        /// <summary>
        /// 行绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SchoolGrid_OnPreRowDataBound(object sender, FineUI.GridPreRowEventArgs e)
        {
            LinkButtonField lbf = SchoolGrid.FindColumn("Schooledit") as LinkButtonField;
            DataRowView row = e.DataItem as DataRowView;
            string schoolno = row[0].ToString();
            lbf.OnClientClick = WindowEdit.GetShowReference("SchoolEdit.aspx?flag=edit&id=" + schoolno + "", "校区编辑");

            LinkButtonField lbfx = SchoolGrid.FindColumn("Schooldelete") as LinkButtonField;
            DataRowView rowx = e.DataItem as DataRowView;
            string schoolnox = rowx[0].ToString();
            lbfx.OnClientClick = WindowDelete.GetShowReference("../SystemSet/DeletePassword.aspx?Type=School&id=" + schoolnox + "", "校区删除");
        }
        /// <summary>
        /// 编辑窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WindowEdit_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            BindGrid();
        }
    }
}