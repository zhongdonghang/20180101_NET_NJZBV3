<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleEdit.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.UsersManage.RoleEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager1" runat="server" />
    <ext:SimpleForm ID="SimpleForm1" BodyPadding="5px" runat="server" EnableBackgroundColor="true"
        ShowBorder="false" ShowHeader="false" Width="560px" LabelWidth="50px">
        <Items>
            <ext:TextBox ID="txtRoleName" Required="true" Label="名称" runat="server" Width="280px">
            </ext:TextBox>
            <ext:Tree ID="treeMenu" runat="server" OnNodeCheck="TreeMenu_NodeCheck" Title="菜单权限"
                EnableArrows="true" ShowBorder="true" Width="560px" Height="280px" AutoScroll="true">
            </ext:Tree>
            <ext:Label runat="server" Text="">
            </ext:Label>
        </Items>
    </ext:SimpleForm>
    <ext:Form ID="Form2" runat="server" EnableBackgroundColor="True" Width="560px" ShowBorder="false"
        ShowHeader="false">
        <Rows>
            <ext:FormRow ColumnWidths="45% 10% 45%">
                <Items>
                    <ext:Label ID="Label1" runat="server">
                    </ext:Label>
                    <ext:Button ID="btnSubmit" ValidateForms="SimpleForm1" CssClass="inline" Text="提交"
                        runat="server" OnClick="btnSubmit_OnClick">
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
