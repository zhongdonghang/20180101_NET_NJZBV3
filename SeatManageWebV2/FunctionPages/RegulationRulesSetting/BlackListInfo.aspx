<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlackListInfo.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.RegulationRulesSetting.BlackListInfo" %>

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
        ShowBorder="false" ShowHeader="True" Width="400px" Title="黑名单设置" LabelAlign="Right"
        LabelWidth="150">
        <Items>
            <ext:CheckBox ID="IsBlUserd" Label="黑名单设置" Text="启用黑名单管理" runat="server" OnCheckedChanged="IsBlUserd_CheckedChanged"
                AutoPostBack="true">
            </ext:CheckBox>
            <ext:Label ID="label1" runat="server">
            </ext:Label>
            <ext:NumberBox ID="nbvrcont" Label="违规进入黑名单次数(次)" MinValue="1" runat="server" Width="150">
            </ext:NumberBox>
            <ext:DropDownList ID="ddlleavemode" Label="离开黑名单方式" runat="server" Width="100">
                <ext:ListItem Text="自动离开" Value="0" Selected="true" />
                <ext:ListItem Text="手动离开" Value="1" />
            </ext:DropDownList>
            <ext:NumberBox ID="nbleavetime" Label="离开黑名单时间(天)" MinValue="1" runat="server" Width="150">
            </ext:NumberBox>
            <ext:NumberBox ID="nbvrovertime" Label="违规失效时间(天)" MinValue="0" runat="server" Width="150">
            </ext:NumberBox>
            <ext:Label ID="label3" LabelSeparator="" runat="server">
            </ext:Label>
            <ext:Label ID="label2" Label="启用违规类型" runat="server">
            </ext:Label>
            <ext:CheckBox ID="cbLeaveByAdmin" Text="被管理员释放座位" runat="server">
            </ext:CheckBox>
            <ext:CheckBox ID="cbShortLeaveByAdmin" Text="被管理员设置暂离，暂离超时" runat="server">
            </ext:CheckBox>
            <ext:CheckBox ID="cbSeatOverTime" Text="在座超时" runat="server">
            </ext:CheckBox>
            <ext:CheckBox ID="cbShortLeaveOverTime" Text="暂离超时" runat="server">
            </ext:CheckBox>
            <ext:CheckBox ID="cbShortLeaveByReader" Text="被读者设置暂离，暂离超时" runat="server">
            </ext:CheckBox>
            <ext:CheckBox ID="cbBookOverTime" Text="预约超时" runat="server">
            </ext:CheckBox>
        </Items>
    </ext:SimpleForm>
    <ext:Form ID="Form2" runat="server" EnableBackgroundColor="True" Width="400px" ShowBorder="false"
        ShowHeader="false">
        <Rows>
            <ext:FormRow ColumnWidths="40% 20% 40%">
                <Items>
                    <ext:Label runat="server">
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
