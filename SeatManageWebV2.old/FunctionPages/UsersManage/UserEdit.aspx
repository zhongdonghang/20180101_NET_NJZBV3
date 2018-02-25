<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.UsersManage.UserEdit" %>

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
        ShowBorder="false" ShowHeader="True" Width="500px" Title="编辑内容">
        <Items>
            <ext:TextBox ID="txtLoginID" Required="true" Label="用户ID" runat="server" Width="300px">
            </ext:TextBox>
            <ext:TextBox ID="txtUserName" Required="true" Label="姓名" runat="server" Width="300px">
            </ext:TextBox>
            <ext:TextBox ID="txtPassword" Required="true" Label="登录密码" runat="server" TextMode="Password"
                Width="300px">
            </ext:TextBox>
            <ext:TextBox ID="txtPassword_d" Required="true" Label="验证密码" runat="server" TextMode="Password"
                Width="300px">
            </ext:TextBox>
            <ext:TextBox ID="txtRemark" Label="备注信息" runat="server" Width="300px">
            </ext:TextBox>
            <ext:TextBox ID="txtIPAdd" Label="IP绑定" runat="server" Width="300px">
            </ext:TextBox>
            <ext:CheckBox ID="clbused" Label="是否启用" runat="server" Checked="true">
            </ext:CheckBox>
            <ext:CheckBoxList ID="clbRole" Label="角色选择" runat="server" ColumnNumber="2" ColumnWidth="200px" AutoPostBack="false"
                AjaxLoadingType="Mask">
            </ext:CheckBoxList>
            <ext:CheckBoxList ID="clbroom" Label="权限选择" runat="server" ColumnNumber="2" ColumnWidth="200px" AutoPostBack="false"
                AjaxLoadingType="Mask">
            </ext:CheckBoxList>
        </Items>
    </ext:SimpleForm>
    <ext:Form ID="Form2" runat="server" EnableBackgroundColor="True" Width="500px" ShowBorder="false"
        ShowHeader="false">
        <Rows>
            <ext:FormRow ColumnWidths="45% 10% 45%">
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
