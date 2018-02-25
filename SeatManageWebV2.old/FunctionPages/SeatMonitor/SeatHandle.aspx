<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeatHandle.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.SeatMonitor.SeatHandle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/SeatGraph.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/SeatGraphHandle.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1"
        HideScrollbar="true" />
    <ext:Form EnableBackgroundColor="true" BodyPadding="5px" LabelWidth="50px" ID="Form2"
        runat="server" Title="座位信息" LabelAlign="Right" ShowBorder="false">
        <Rows>
            <ext:FormRow ColumnWidths="40% 30% 30%">
                <Items>
                    <ext:Label ID="lblCardNo" runat="server" Label="学号" Text="">
                    </ext:Label>
                    <ext:Label ID="lblName" runat="server" Label="姓名" Text="">
                    </ext:Label>
                    <ext:Label ID="lblSeatNo" runat="server" Label="座位号" Text="">
                    </ext:Label>
                </Items>
            </ext:FormRow>
            <ext:FormRow ColumnWidths="30% 69% 1%">
                <Items>
                    <ext:Label ID="lblSeatStatus" runat="server" Label="状态" Text="">
                    </ext:Label>
                    <ext:Label ID="lblTimeLength" runat="server" Label="时间" Text="">
                    </ext:Label>
                    <ext:Label runat="server">
                    </ext:Label>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:ContentPanel runat="server" ShowHeader="true" Title="操作" ShowBorder="false">
        <table class="divOperate" width="100%" border="0">
            <tr>
                <td align="right" style="width: 120px;">
                    <ext:Button ID="btnShortLeave" runat="server" Text="暂离" OnClick="btn_shortLeave"
                        ConfirmText="是否确定把该读者设置为暂离？" />
                </td>
                <td align="center" style="width: 50px;">
                    <ext:Button ID="btnLeave" runat="server" Text="离开" ConfirmText="是否确定把此座位的读者设置离开？"
                        OnClick="btn_btnLeave" />
                </td>
                <td align="left" style="width: 70px;">
                    <ext:Button ID="btnAddBlackList" runat="server" OnClientClick=" return showDiv('divBlack','divAllotSeat')"
                        Text="加入黑名单" />
                </td>
                <td align="left" style="width: 140px;">
                    <ext:Button ID="btnAllotSeat" runat="server" OnClientClick=" return showDiv('divAllotSeat','divBlack')"
                        Text="分配座位" />
                </td>
            </tr>
        </table>
        <!--加入黑名单操作-->
        <div id="divBlack" style="width: 420px; display: none;">
            <ext:SimpleForm ID="SimpleForm1" BodyPadding="5px" Width="420px" EnableBackgroundColor="true"
                Title="黑名单信息" runat="server" LabelWidth="120px" ShowBorder="false">
                <Items>
                    <ext:TextBox ID="txtCardNo" Label="学号" Readonly="true" runat="server" Enabled="false">
                    </ext:TextBox>
                    <ext:TextBox ID="txtReaderName" Label="姓名" Readonly="true" runat="server" Enabled="false">
                    </ext:TextBox>
                    <ext:TextArea ID="txtRemark" Label="备注" ShowRedStar="true" MinLength="5"
                        Required="true" RequiredMessage="请输入备注信息" EmptyText="请输入备注信息以便以后查看，输入备注不得少于5个字"
                        MinLengthMessage="请详细输入备注信息,以方便你以后查询,备注内容必须超过5个字" runat="server" Width="150px">
                    </ext:TextArea>
                </Items>
            </ext:SimpleForm>
            <table width="420px" style="background: #DFE8F6; margin-left: auto; margin-right: auto;">
                <tr>
                    <td align="right">
                        <ext:Button Text="确定" ValidateForms="SimpleForm1" runat="server" ID="btnSureAddBlacklist"
                            OnClick="btn_SureAddBlacklist">
                        </ext:Button>
                    </td>
                    <td>
                        <ext:Button Text="取消" runat="server" OnClientClick="return hiddenAll('divAllotSeat','divBlack')"
                            ID="btnCancel">
                        </ext:Button>
                    </td>
                </tr>
            </table>
        </div>
        <!--分配座位-->
        <div id="divAllotSeat" style="display: none;">
            <ext:SimpleForm ID="SimpleForm2" BodyPadding="5px" Width="420px" EnableBackgroundColor="true"
                Title="分配座位信息" runat="server" LabelWidth="120px" ShowBorder="false">
                <Items>
                    <ext:TextBox ID="txtSeat" Label="座位号"  Readonly="true"
                        runat="server" Enabled="false">
                    </ext:TextBox>
                    <ext:TextBox ID="txtCardNo1" Label="学号" ShowRedStar="true" Required="true"
                        RequiredMessage="请输入读者学号" MinLength="3" MinLengthMessage="请准确输入读者学号" EmptyText="请输入读者学号或者证号"
                        runat="server">
                    </ext:TextBox>
                </Items>
            </ext:SimpleForm>
            <table width="420px" style="background: #DFE8F6; margin-left: auto; margin-right: auto;">
                <tr>
                    <td align="right">
                        <ext:Button Text="确定" ValidateForms="SimpleForm2" runat="server" ID="btnSureAllotSeat"
                            OnClick="btnSureAllotSeat_Click">
                        </ext:Button>
                    </td>
                    <td>
                        <ext:Button Text="取消" runat="server" OnClientClick="return hiddenAll('divAllotSeat','divBlack')"
                            ID="Button2">
                        </ext:Button>
                    </td>
                </tr>
            </table>
        </div>
    </ext:ContentPanel>
    </form>
</body>
</html>
