<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFXX.aspx.cs" Inherits="WeiXinServiceManage.KFXX" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        要说的话<br />
        <br />
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        接受方<br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
    </div>
    </form>
</body>
</html>
