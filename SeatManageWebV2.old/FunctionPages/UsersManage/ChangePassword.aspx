<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.UsersManage.ChangePassword" %>

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
        ShowBorder="false" ShowHeader="false" Width="340px">
        <Items>
            <ext:TextBox ID="txtPassword_old" Required="true" Label="原密码" runat="server" TextMode="Password">
            </ext:TextBox>
            <ext:TextBox ID="txtPassword" Required="true" Label="新密码" runat="server" TextMode="Password">
            </ext:TextBox>
            <ext:TextBox ID="txtPassword_d" Required="true" Label="验证密码" runat="server" TextMode="Password">
            </ext:TextBox>
        </Items>
    </ext:SimpleForm>
    <ext:Form runat="server" EnableBackgroundColor="True" Width="340px" ShowBorder="false" 
        ShowHeader="false">
        <Rows>
            <ext:FormRow ColumnWidths="40% 20% 40%">
                <Items>
                    <ext:Label runat="server">
                    </ext:Label>
                    <ext:Button ID="btnSubmit" ValidateForms="SimpleForm1" CssClass="inline" Text="提交"
                        runat="server" OnClick="btnSubmit_Click">
                    </ext:Button>
                    <ext:Label ID="Label1" runat="server">
                    </ext:Label>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    </form>
</body>
</html>
