using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManage.ClassModel;
using SeatManage.Bll;
using SeatManage.EnumType;
using SeatManage.ISystemTerminal.IStuLibSync;
using SeatManage.InterfaceFactory;

namespace SeatManageWebV2.FunctionPages.RegulationRulesSetting
{
    public partial class SyncSet : BasePage
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
                btnUpdate.OnClientClick = WindowEdit.GetShowReference("SyncReader.aspx", "同步读者信息");
                DataBind();
            }
        }
        /// <summary>
        /// 设置绑定
        /// </summary>
        private void DataBind()
        {
            SeatManage.ClassModel.StuLibSyncSetting stulibsync = SeatManage.Bll.T_SM_SystemSet.GetStuLibSync();
            txtsip.Text = stulibsync.SouIP;
            txtsDB.Text = stulibsync.SouDBName;
            txtsID.Text = stulibsync.SouUserName;
            txtsPW.Text = stulibsync.SouPW;
            ddlmode.SelectedValue = ((int)stulibsync.SyncMode).ToString();
            time.Text = stulibsync.SyncTime;
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SeatManage.ClassModel.StuLibSyncSetting stulibsync = SeatManage.Bll.T_SM_SystemSet.GetStuLibSync();
            stulibsync.SouIP = txtsip.Text;
            stulibsync.SouDBName = txtsDB.Text;
            stulibsync.SouUserName = txtsID.Text;
            stulibsync.SouPW = txtsPW.Text;
            //stulibsync.TarIP = txttip.Text;
            //stulibsync.TarDBName = txttDB.Text;
            //stulibsync.TarUserName = txttID.Text;
            //stulibsync.TarPW = txttPW.Text;
            stulibsync.SyncMode = (SeatManage.EnumType.StudentSyncMode)int.Parse(ddlmode.SelectedValue);
            DateTime dt = new DateTime();
            if (DateTime.TryParse(time.Text, out dt))
            {
                stulibsync.SyncTime = time.Text;
            }
            else
            {
                FineUI.Alert.Show("日期格式错误！");
                return;
            }
            if (SeatManage.Bll.T_SM_SystemSet.UpdateStuLibSync(stulibsync))
            {
                FineUI.Alert.Show("设置成功！");
            }
            else
            {
                FineUI.Alert.Show("设置失败！");
            }
        }
        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            DataBind();
        }
        /// <summary>
        /// 连接测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void testsconn_Click(object sender, EventArgs e)
        {
            StuLibSyncSetting SyncSet = T_SM_SystemSet.GetStuLibSync();
            if (SyncSet != null)
            {
                IStuLibSync StuLibSync = AssemblyFactory.CreateAssembly("IStuLibSync") as IStuLibSync;// SystemTerminalFactory.CreateStuLibSync();
                StuLibSync.StuLibSyncSet = SyncSet;
                if (StuLibSync.LinkDataSourceTest())
                {
                    FineUI.Alert.Show("测试成功！");
                }
                else
                {
                    FineUI.Alert.Show("测试失败！");
                }
            }
            else
            {
                FineUI.Alert.Show("获取设置失败！");
            }
        }

        /// <summary>
        /// 编辑窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WindowEdit_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            DataBind();
        }
    }
}