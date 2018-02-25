<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBlacklistInfo.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.LogManage.AddBlacklistInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager1" runat="server" />
    <ext:SimpleForm ID="SimpleForm1" BodyPadding="5px" runat="server" EnableBackgroundColor="True"
        ShowBorder="false" ShowHeader="false" Width="350px" Title="编辑内容">
        <Items>
            <ext:TextBox ID="txtCardno" Required="true" Label="读者学号" runat="server" Width="200px"
                ShowRedStar="true">
            </ext:TextBox>
            <ext:CheckBox ID="cbIsAllRR" Label="限制全部阅览室" Text="启用" runat="server" Checked="false"
                OnCheckedChanged="cbIsAllRR_OnCheckedChanged" AutoPostBack="true">
            </ext:CheckBox>
            <ext:DropDownList runat="server" ID="ddlroom" Label="阅览室选择" Width="200px">
            </ext:DropDownList>
            <ext:DropDownList runat="server" ID="ddlleaveMode" Label="离开方式" Width="200px" OnSelectedIndexChanged="ddlleaveMode_OnSelectedIndexChanged"
                AutoPostBack="true">
                <ext:ListItem Text="自动离开" Value="0" Selected="true" />
                <ext:ListItem Text="手动释放" Value="1" />
            </ext:DropDownList>
            <ext:DatePicker ID="dpEndDate" Width="150" Readonly="false" DateFormatString="yyyy-MM-dd"
                EmptyText="请选择结束日期" Label="离开黑名单日期" runat="server">
            </ext:DatePicker>
            <ext:TextBox ID="txtRemark" ShowRedStar="true" Label="备注信息" runat="server" Width="200px"
                Required="true" RegexMessage="请输入备注信息" EmptyText="请输入备注信息">
            </ext:TextBox>
        </Items>
    </ext:SimpleForm>
    <ext:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowHeader="false"
        Width="350px" ShowBorder="false" Height="90px">
        <Rows>
            <ext:FormRow ColumnWidths="41% 18% 41%">
                <Items>
                    <ext:Label ID="Label1" runat="server">
                    </ext:Label>
                    <ext:Button ID="btnSubmit" ValidateForms="SimpleForm1" CssClass="inline" Text="提交"
                        runat="server" OnClick="btnSubmit_Click">
                    </ext:Button>
                    <ext:Label ID="Label2" runat="server">
                    </ext:Label>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    </form>
</body>
</html>
