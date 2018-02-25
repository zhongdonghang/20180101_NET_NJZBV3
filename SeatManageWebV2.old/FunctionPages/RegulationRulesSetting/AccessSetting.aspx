<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccessSetting.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.RegulationRulesSetting.AccessSetting" %>

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
        ShowBorder="false" ShowHeader="True" Width="400px" Title="通道机接口设置" LabelAlign="Right"
        LabelWidth="150">
        <Items>
            <ext:CheckBox ID="IsASUserd" Label="通道机接口设置" Text="启用设置" runat="server" OnCheckedChanged="IsASUserd_CheckedChanged"
                AutoPostBack="true">
            </ext:CheckBox>
            <ext:Label ID="label1" runat="server">
            </ext:Label>
            <ext:CheckBox ID="IsELUserd" Label="入馆设置" Text="启用设置" runat="server" OnCheckedChanged="IsELUserd_CheckedChanged"
                AutoPostBack="true">
            </ext:CheckBox>
            <ext:CheckBox ID="IsShortLeave" Text="处理暂离读者（设置为暂离回来）" runat="server">
            </ext:CheckBox>
            <ext:CheckBox ID="IsOnSeat" Text="处理离开不刷卡读者（释放座位）" runat="server">
            </ext:CheckBox>
            <ext:CheckBox ID="IsBooking" Text="处理预约读者（预约确认）" runat="server">
            </ext:CheckBox>
            <ext:CheckBox ID="IsAddrv" Text="记录违规（离开不刷卡）" runat="server">
            </ext:CheckBox>
            <ext:NumberBox ID="LeaveTime" runat="server" Label="入馆处理状态间隔(分钟)" CompareType="Int" CompareOperator="GreaterThan"
                CompareValue="-1" Width="80">
            </ext:NumberBox>
            <ext:Label ID="label2" runat="server">
            </ext:Label>
            <ext:CheckBox ID="IsOLUserd" Label="离馆设置" Text="启用设置" runat="server" OnCheckedChanged="IsOLUserd_CheckedChanged"
                AutoPostBack="true">
            </ext:CheckBox>
            <ext:DropDownList ID="ddlleavemode" Label="离开读者处理" runat="server" Width="100">
                <ext:ListItem Text="设为暂离" Value="8" Selected="true" />
                <ext:ListItem Text="释放座位" Value="0" />
            </ext:DropDownList>
            <ext:Label ID="label3" runat="server">
            </ext:Label>
            <ext:CheckBox ID="cbBLIsUsed" Label="限制黑名单" Text="限制黑名单读者进入" runat="server">
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
