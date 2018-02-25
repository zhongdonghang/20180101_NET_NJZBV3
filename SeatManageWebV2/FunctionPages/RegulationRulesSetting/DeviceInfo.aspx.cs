using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManage.ClassModel;

namespace SeatManageWebV2.FunctionPages.RegulationRulesSetting
{
    public partial class DeviceInfo : BasePage
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
                DataBind();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void DataBind()
        {
            //List<SeatManage.ClassModel.TerminalInfoV2> clientlist = SeatManage.Bll.TerminalOperatorService.GetAllTeminalInfo();
            List<SeatManage.ClassModel.TerminalInfoV2> clientlist = SeatManage.Bll.TerminalOperatorService.GetAllTeminalInfo();
            foreach (SeatManage.ClassModel.TerminalInfoV2 teminal in clientlist)
            {
                FineUI.TreeNode node = new FineUI.TreeNode();
                //if (!teminal.DeviceSetting.IsAnyPaper)
                //{
                //    node.Icon = FineUI.Icon.PrinterCancel;
                //    if (string.IsNullOrEmpty(teminal.Describe))
                //    {
                //        node.Text = teminal.ClientNo + "(缺纸)";
                //    }
                //    else
                //    {
                //        node.Text = teminal.Describe + "(缺纸)";
                //    }
                //}
                //else
                //{
                node.Icon = FineUI.Icon.Computer;
                if (string.IsNullOrEmpty(teminal.Describe))
                {
                    node.Text = teminal.ClientNo;
                }
                else
                {
                    node.Text = teminal.Describe;
                }
                //}
                node.ToolTip = teminal.ClientNo;
                node.NodeID = teminal.ClientNo;
                node.EnablePostBack = true;
                treeMenu.Nodes.Add(node);
            }
            List<SeatManage.ClassModel.ReadingRoomInfo> roomlist = SeatManage.Bll.ClientConfigOperate.GetReadingRooms(null);
            foreach (SeatManage.ClassModel.ReadingRoomInfo room in roomlist)
            {
                FineUI.CheckItem ci = new FineUI.CheckItem();
                ci.Text = (room.Name + "(" + room.No + ")");
                ci.Value = room.No;
                clbroom.Items.Add(ci);
            }
            //clbroom.DataTextField = "Name";
            //clbroom.DataValueField = "No";
            //clbroom.DataSource = roomlist;
            //clbroom.DataBind();

            //cblTerm.DataTextField = "ClientNo";
            //cblTerm.DataValueField = "ClientNo";
            //cblTerm.DataSource = clientlist;
            //cblTerm.DataBind();
            if (clientlist.Count > 0)
            {
                SelectChange(clientlist[0].ClientNo);
            }
        }
        /// <summary>
        /// 点击设备左边显示设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Tree_NodeCommand(object sender, FineUI.TreeCommandEventArgs e)
        {
            SelectChange(e.Node.NodeID);
        }
        /// <summary>
        /// 赋值数据
        /// </summary>
        /// <param name="ClientNo"></param>
        private void SelectChange(string ClientNo)
        {
            TerminalInfoV2 term = SeatManage.Bll.TerminalOperatorService.GetTeminalSetting(ClientNo);
            lbno.Text = term.ClientNo;
            SelesctALLtem.Checked = false;
            cbselectallrr.Checked = false;

            txtRemark.Text = term.Describe;
            //if (!term.DeviceSetting.IsAnyPaper)
            //{
            //    lblPapersCount.Text = "打印机缺纸，请及时更换";
            //}
            //else
            //{
            //    if (term.DeviceSetting.LastPrintTimes == 0)
            //    {
            //        lblPapersCount.Text = term.DeviceSetting.PrintedTimes.ToString();
            //    }
            //    else
            //    {
            //        lblPapersCount.Text = term.DeviceSetting.PrintedTimes.ToString() + "/" + term.DeviceSetting.LastPrintTimes.ToString();
            //    }
            //}
            foreach (FineUI.RadioItem item in rblSelectSeatMode.Items)
            {
                item.Selected = false;
                if (item.Value == ((int)term.DeviceSetting.SelectMethod).ToString())
                {
                    item.Selected = true;
                }
            }
            cbSelectSeatCount.Checked = term.DeviceSetting.PosTimes.IsUsed;
            numSelectSeatTime.Text = term.DeviceSetting.PosTimes.Minutes.ToString();
            numSelectSeatCont.Text = term.DeviceSetting.PosTimes.Times.ToString();
            cbOftenSeat.Checked = term.DeviceSetting.UsingOftenUsedSeat.Used;
            if (cbOftenSeat.Checked)
            {
                nbostime.Hidden = false;
                nboscont.Hidden = false;
            }
            else
            {
                nbostime.Hidden = true;
                nboscont.Hidden = true;
            }
            nbostime.Text = term.DeviceSetting.UsingOftenUsedSeat.LengthDays.ToString();
            nboscont.Text = term.DeviceSetting.UsingOftenUsedSeat.SeatCount.ToString();
            rbprint.SelectedValue = ((int)term.DeviceSetting.UsingPrintSlip).ToString();
            cbBespeak.Checked = term.DeviceSetting.UsingActiveBespeakSeat;
            cbipos.Checked = term.DeviceSetting.IsShowInitPOS;
            cbNumSelect.Checked = term.DeviceSetting.UsingEnterNoForSeat;
            bool isSelect = false;
            foreach (FineUI.RadioItem item in rblfbl.Items)
            {
                item.Selected = false;
                if (term.DeviceSetting.SystemResoultion.WindowSize.Size.X == int.Parse(item.Value))
                {
                    item.Selected = true;
                    isSelect = true;
                }
            }
            if(!isSelect)
            {
                txtReDiy.Text = term.DeviceSetting.SystemResoultion.WindowSize.Size.X + "x" + term.DeviceSetting.SystemResoultion.WindowSize.Size.Y;
                rblfbl.SelectedValue = "0";
            }
            foreach (FineUI.CheckItem item in clbroom.Items)
            {
                item.Selected = false;
                foreach (string room in term.DeviceSetting.Rooms)
                {
                    if (item.Value == room)
                    {
                        item.Selected = true;
                    }
                }
            }
            cblTerm.Items.Clear();
            List<SeatManage.ClassModel.TerminalInfoV2> clientlist = SeatManage.Bll.TerminalOperatorService.GetAllTeminalInfo();
            foreach (SeatManage.ClassModel.TerminalInfoV2 teminal in clientlist)
            {
                if (teminal.ClientNo != term.ClientNo)
                {
                    FineUI.CheckItem ci = new FineUI.CheckItem();
                    ci.Text = teminal.ClientNo + "(" + teminal.Describe + ")";
                    ci.Value = teminal.ClientNo;
                    cblTerm.Items.Add(ci);
                }
            }
            //foreach (FineUI.CheckItem item in cblTerm.Items)
            //{
            //    item.Selected = false;
            //    if (item.Value == term.ClientNo)
            //    {
            //        item.Selected = true;
            //    }
            //}
        }
        /// <summary>
        /// 选中显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnCheckedChanged(object sender, EventArgs e)
        {
            if (cbOftenSeat.Checked)
            {
                nbostime.Hidden = false;
                nboscont.Hidden = false;
            }
            else
            {
                nbostime.Hidden = true;
                nboscont.Hidden = true;
            }
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            TerminalInfoV2 newterm = NewSetting(SeatManage.Bll.TerminalOperatorService.GetTeminalSetting(lbno.Text));
            if (newterm != null)
            {
                newterm.Describe = txtRemark.Text;
                if (SeatManage.Bll.TerminalOperatorService.UpdateTeminalSetting(newterm) == "")
                {
                    foreach (FineUI.CheckItem item in cblTerm.Items)
                    {
                        if (item.Selected)
                        {
                            if (SeatManage.Bll.TerminalOperatorService.UpdateTeminalSetting(NewSetting(SeatManage.Bll.TerminalOperatorService.GetTeminalSetting(item.Value))) != "")
                            {
                                FineUI.Alert.Show("更新失败！");
                                return;
                            }
                        }
                    }
                    FineUI.Alert.Show("更新成功！");

                }
                else
                {
                    FineUI.Alert.Show("更新失败！");
                }
            }
            else
            {
                FineUI.Alert.Show("设置错误！");
            }
        }
        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="term"></param>
        private TerminalInfoV2 NewSetting(TerminalInfoV2 term)
        {
            TerminalInfoV2 newterm = term;
            newterm.DeviceSetting.IsShowInitPOS = cbipos.Checked;
            newterm.DeviceSetting.UsingPrintSlip = (SeatManage.EnumType.PrintSlipMode)int.Parse(rbprint.SelectedValue);
            newterm.DeviceSetting.UsingEnterNoForSeat = cbNumSelect.Checked;
            newterm.DeviceSetting.SelectMethod = (SeatManage.EnumType.SelectSeatMode)int.Parse(rblSelectSeatMode.SelectedValue);
            newterm.DeviceSetting.UsingActiveBespeakSeat = cbBespeak.Checked;
            newterm.DeviceSetting.UsingOftenUsedSeat.Used = cbOftenSeat.Checked;
            newterm.DeviceSetting.UsingOftenUsedSeat.LengthDays = int.Parse(nbostime.Text);
            newterm.DeviceSetting.UsingOftenUsedSeat.SeatCount = int.Parse(nboscont.Text);
            newterm.DeviceSetting.PosTimes.Minutes = int.Parse(numSelectSeatTime.Text);
            newterm.DeviceSetting.PosTimes.Times = int.Parse(numSelectSeatCont.Text);
            newterm.DeviceSetting.PosTimes.IsUsed = cbSelectSeatCount.Checked;
            if (rblfbl.SelectedValue == "0")
            {
                newterm.DeviceSetting.SystemResoultion = new ResolutionV2();
                string[] xy = txtReDiy.Text.Split('x');
                if (xy.Length > 1)
                {
                    int w = 0;
                    int h = 0;
                    if (int.TryParse(xy[0], out w) && int.TryParse(xy[1], out h))
                    {
                        newterm.DeviceSetting.SystemResoultion.WindowSize.Size.X = w;
                        newterm.DeviceSetting.SystemResoultion.WindowSize.Size.Y = h;
                        newterm.DeviceSetting.SystemResoultion.WindowSize.Location.X = 0;
                        newterm.DeviceSetting.SystemResoultion.WindowSize.Location.Y = 0;
                        newterm.DeviceSetting.SystemResoultion.TooltipSize.Location.X = 0;
                        newterm.DeviceSetting.SystemResoultion.TooltipSize.Location.Y = 0;
                        newterm.DeviceSetting.SystemResoultion.TooltipSize.Size.X = 0;
                        newterm.DeviceSetting.SystemResoultion.TooltipSize.Size.Y = 0;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                newterm.DeviceSetting.SystemResoultion = new ResolutionV2(rblfbl.SelectedValue);
            }
            newterm.DeviceSetting.Rooms.Clear();
            foreach (FineUI.CheckItem item in clbroom.Items)
            {
                if (item.Selected)
                {
                    newterm.DeviceSetting.Rooms.Add(item.Value);
                }
            }
            return newterm;
        }
        /// <summary>
        /// 重置数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            SelectChange(lbno.Text);
        }
        protected void SelesctALLtem_CheckedChanged(object sender, EventArgs e)
        {
            if (SelesctALLtem.Checked)
            {
                foreach (FineUI.CheckItem item in cblTerm.Items)
                {
                    item.Selected = true;
                }
            }
            else
            {
                foreach (FineUI.CheckItem item in cblTerm.Items)
                {
                    item.Selected = false;
                }
            }
        }
        protected void cbselectallrr_CheckedChanged(object sender, EventArgs e)
        {
            if (cbselectallrr.Checked)
            {
                foreach (FineUI.CheckItem item in clbroom.Items)
                {
                    item.Selected = true;
                }
            }
            else
            {
                foreach (FineUI.CheckItem item in clbroom.Items)
                {
                    item.Selected = false;
                }
            }
        }
    }
}