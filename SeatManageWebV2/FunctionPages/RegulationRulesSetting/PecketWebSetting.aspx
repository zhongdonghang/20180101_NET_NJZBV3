<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PecketWebSetting.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.RegulationRulesSetting.PecketWebSetting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager1" runat="server" />
    <ext:SimpleForm ID="SimpleForm1" BodyPadding="5px" runat="server" EnableBackgroundColor="True"
        ShowBorder="false" ShowHeader="True" Width="400px" Title="移动网站设置" LabelAlign="Right"
        LabelWidth="150">
        <Items>
            <ext:CheckBox ID="cb_UseBookSeat" Label="手机预约" Text="启用" runat="server">
            </ext:CheckBox>
            <ext:CheckBox ID="cb_UseBookNextDaySeat" Text="启用隔天预约" runat="server">
            </ext:CheckBox>
            <ext:CheckBox ID="cb_UseBookNowDaySeat" Text="启用当天预约" runat="server">
            </ext:CheckBox>
            <ext:CheckBox ID="cb_UseCancelBook" Text="允许取消预约" runat="server">
            </ext:CheckBox>
            <ext:Label ID="label1" runat="server">
            </ext:Label>
            <ext:Label ID="label6" runat="server" Label="座位操作">
            </ext:Label>
            <ext:CheckBox ID="cb_UseShortLeave" Text="允许暂离座位" runat="server">
            </ext:CheckBox>
            <ext:CheckBox ID="cb_UseComeBack" Text="允许暂离回来" runat="server">
            </ext:CheckBox>
            <ext:CheckBox ID="cb_UseCanLeave" Text="允许释放座位" runat="server">
            </ext:CheckBox>
            <ext:CheckBox ID="cb_UseContinue" Text="允许座位续时" runat="server">
            </ext:CheckBox>
            <ext:CheckBox ID="cb_UseBookComfirm" Text="允许座位签到" runat="server">
            </ext:CheckBox>
            <ext:CheckBox ID="cb_UseWaitSeat" Label="等待座位" Text="启用" runat="server">
            </ext:CheckBox>
            <ext:CheckBox ID="cb_UseCancelWait" Text="允许取消等待" runat="server">
            </ext:CheckBox>

            <ext:CheckBox ID="cb_ChangeSeat" Text="允许更换座位" runat="server">
            </ext:CheckBox>
            <ext:CheckBox ID="cb_SelectSeat" Text="允许选择座位" runat="server">
            </ext:CheckBox>
        </Items>
    </ext:SimpleForm>
    <ext:Form ID="Form2" runat="server" EnableBackgroundColor="True" Width="400px" ShowBorder="false"
        ShowHeader="false">
        <Rows>
            <ext:FormRow ColumnWidths="40% 20% 40%">
                <Items>
                    <ext:Label ID="Label5" runat="server">
                    </ext:Label>
                    <ext:Button ID="btnSubmit" ValidateForms="SimpleForm1" CssClass="inline" Text="&nbsp;&nbsp;提&nbsp;交&nbsp;&nbsp;"
                        runat="server" OnClick="btnSubmit_Click">
                    </ext:Button>
                    <ext:Label ID="Label4" runat="server">
                    </ext:Label>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    </form>
</body>
</html>
