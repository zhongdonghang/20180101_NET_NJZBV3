<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddViolateDiscipline.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.LogManage.AddViolateDiscipline" %>

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
            <ext:DropDownList runat="server" ID="ddlroom" Label="阅览室选择" Width="200px">
            </ext:DropDownList>
            <ext:TextBox ID="txtseatno" Label="座位号" runat="server" Width="200px">
            </ext:TextBox>
            <ext:DropDownList runat="server" ID="ddltype" Label="违规类型选择" Width="200px">
                <ext:ListItem Text="预约超时" Value="0" Selected="true" />
                <ext:ListItem Text="管理员释放座位" Value="1" Selected="true" />
                <ext:ListItem Text="在座超时" Value="2" Selected="true" />
                <ext:ListItem Text="暂离超时" Value="3" Selected="true" />
                <ext:ListItem Text="被管理员设置暂离，暂离超时" Value="4" Selected="true" />
                <ext:ListItem Text="被读者设置暂离，暂离超时" Value="5" Selected="true" />
                <ext:ListItem Text="离开未刷卡" Value="8" />
            </ext:DropDownList>
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
