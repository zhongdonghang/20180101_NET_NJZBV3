<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgPostItemEdit.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.UsersManage.MsgPostItemEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager1" runat="server" />

    <ext:SimpleForm ID="SimpleForm1" BodyPadding="5px" runat="server" EnableBackgroundColor="True"
        ShowBorder="false"  Width="300px" Title="编辑内容" ShowHeader="false">
        <Items>
            <ext:TextBox ID="txtUserName" Required="true" Label="授权用户名" runat="server"   Enabled="false">
            </ext:TextBox>
            <ext:TextBox ID="txtPwd" Required="true" Label="密码" runat="server">
            </ext:TextBox>
            <ext:TextBox ID="txtAppId" Required="true" Label="AppId"  runat="server">
            </ext:TextBox>
            <ext:TextBox ID="txtUrl" Required="true" Label="url"  runat="server">
            </ext:TextBox>
        </Items>
    </ext:SimpleForm>
    <ext:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowHeader="false"
        Width="300px" ShowBorder="false">
        <Rows>
            <ext:FormRow ColumnWidths="41% 18% 41%">
                <Items>
                    <ext:Label ID="Label1" runat="server">
                    </ext:Label>
                    <ext:Button ID="Button1" ValidateForms="SimpleForm1" CssClass="inline" Text="提交"
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
