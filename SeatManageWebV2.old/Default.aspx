﻿<%@ Page Title="图书馆座位管理系统" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
    Inherits="SeatManageWebV2._Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=Title%>
    </title>
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        window.moveTo(0, 0);
        window.resizeTo(window.screen.availWidth, window.screen.availHeight);
        function SubmitExchange() {
            document.all.cmdOK.click();
        }
        function formReset() {
            From1.txtUserName.value = "";
            From1.txtPassword.value = "";

        }
        if (jQuery.browser.msie) {
            alert("为了保证功能的正常使用，建议使用FireFox或Chrome游览器，如果您使用的是360或搜狗游览器请打开急速模式！");
            //window.close();
        }
    </script>
    <style type="text/css">
        .bg
        {
            background: url(Images/Login/bg.jpg) repeat-x;
        }
        bdinput
        {
            border-top: 1px #414141 solid;
            border-left: 1px #5c5c5c solid;
            width: 150px;
            height: 22px;
        }
    </style>
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" scroll="no">
    <form id="From1" method="post" runat="server">
    <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" align="center"
        class="bg">
        <tr>
            <td valign="top">
                <table id="__01" width="1024" height="768" border="0" cellpadding="0" cellspacing="0"
                    align="center">
                    <tr>
                        <td colspan="3">
                            <img src="Images/Login/login_01.jpg" width="1024" height="317" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2">
                            <img src="Images/Login/login_02.jpg" width="445" height="451" alt="" />
                        </td>
                        <td background="Images/Login/login_03.jpg" width="336" height="193" valign="top">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-left: 60px;
                                margin-top: 25px;">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtUserName" CssClass="bdinput" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="40px">
                                        <asp:TextBox ID="txtPassword" CssClass="bdinput" Width="150px" runat="server" TextMode="password"
                                            onKeyUp="if (event.keyCode==13) cmdOK.onclick()"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="45px">
                                        <asp:ImageButton ID="cmdOK" runat="server" OnClick="cmdOK_Click" ImageUrl="Images/Login/img_dl.jpg" />
                                        <img src="Images/Login/img_cz.jpg" width="69" height="29" hspace="6" onclick="formReset()"
                                            style="cursor: pointer;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td rowspan="2">
                            <img src="Images/Login/login_04.jpg" width="243" height="451" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img src="Images/Login/login_05.jpg" width="336" height="258" alt="" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">
        window.status = "<%=Title%>";
        if (document.all.txtUserName.value != "") {
            document.all.txtPassword.focus();
        }
        else {
            document.all.txtUserName.focus();
        }
    </script>
    </form>
</body>
</html>
