<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SeatManageWebV2.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <title>图书馆座位管理移动管理平台</title>
    <link rel="stylesheet" href="Styles/jquery.mobile.min.css" />
    <script src="Scripts/jquery-1.7.2.min.js"></script>
    <script src="Scripts/jquery.mobile.min.js"></script>
    <script>
        try {

            $(function () {

            });

        } catch (error) {
            console.error("Your javascript has an error: " + error);
        }
        //验证输入信息是否完整
        function CheckLoginInfo(cmd) {
            var loginId = $("#txt_LoginID").val();
            var password = $("#txt_Password").val();
            var selValue = jQuery("#selSchool option:selected").text();
            if (loginId == "" || password == "") {
                $("#spanWarmInfo").html("您输入的信息不完整");
                $("#spanWarmInfo").show();
                cmd = "NoLogin";
            }
            return cmd;
        }
        //重置按钮 清空信息
        function ReSet() {
            $("#txt_LoginID").val("");
            $("#txt_Password").val("");
            $("#spanWarmInfo").hide();
            $("#spanWarmInfo").html("");
        }
        //清空警告提示信息
        function ReSetWarnInfo() {
            $("#spanWarmInfo").hide();
            $("#spanWarmInfo").html("");
        }

        //点击登录按钮操作
        function Operate(cmd) {
            var newcmd = CheckLoginInfo(cmd);
            if (newcmd == "Login") {
                subQuery(newcmd);
            }
        }

        //提交登录信息
        function subQuery(cmd) {
            document.getElementById("subCmd").value = cmd;
            form1.submit();
        }

        function SelOnChange() {
            $("#spanWarmInfo").hide();
            $("#spanWarmInfo").html("");
        }
    </script>
    <style>
        .styled
        {
            width: 29px;
            height: 35px;
            padding: 0 5px 0 0;
            background: url(Images/checkbox.png) no-repeat;
            display: block;
            clear: left;
            float: left;
        }
    </style>
</head>
<body>
    <!-- Home -->
    <form id="form1" runat="server">
    <input type="hidden" name="subCmd" id="subCmd" />
    <div data-role="page" id="page1">
        <div style="text-align: center; height: 205px">
            <img style="width: 390px; height: 61px; margin-top: 75px" src="Images/Pad/LogoTop.png" />
        </div>
        <div data-role="content">
            <div data-role="fieldcontain" style="text-align: center">
                <span id="spanWarmInfo" name="spanWarmInfo" runat="server" style="color: Red;"></span>
            </div>
            <div data-role="fieldcontain" style="height: 58px; border-width: 0px; text-align: center">
                <fieldset data-role="controlgroup">
                    <label for="textinput1" style="font-size: 11pt">
                        <b>用户名：</b>
                    </label>
                    <input name="txt_LoginID" id="txt_LoginID" placeholder="" value="" type="text" runat="server"
                        onfocus="ReSetWarnInfo()" />
                </fieldset>
            </div>
            <div data-role="fieldcontain" style="height: 25px; border-width: 0px; text-align: center">
                <fieldset data-role="controlgroup">
                    <label for="textinput2" style="font-size: 11pt">
                        <b>密&nbsp;&nbsp;&nbsp;&nbsp;码：</b>
                    </label>
                    <input name="txt_Password" id="txt_Password" placeholder="" value="" type="password"
                        runat="server" onfocus="ReSetWarnInfo()" />
                </fieldset>
            </div>
            <div data-role="fieldcontain" style="height: 40px; border-width: 0px">
            </div>
            <table width="100%">
                <tr>
                    <td width="50%">
                        <div style="text-align: right; vertical-align: bottom;">
                            <input data-inline="true" value="登录" data-mini="false" type="button" onclick="Operate('Login')" />
                        </div>
                    </td>
                    <td align="left">
                        <div id="checkboxes1" data-role="fieldcontain" style="border-width: 0px">
                            <fieldset data-role="controlgroup" data-type="vertical" style="width: 140px; border-width: 0px"
                                data-mini="true">
                                <legend></legend>
                                <input id="chk_RemPasspword" name="chk_RemPasspword" type="checkbox" runat="server" />
                                <label for="chk_RemPasspword" style="border-width: 0px; border-color: transparent;
                                    margin-left: 7px; background-image: none; width: 100px">
                                    记住密码
                                </label>
                            </fieldset>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div data-theme="d" data-role="footer" data-position="fixed">
            <h5>
                南京智佰闻欣图书馆座位移动管理平台</h5>
        </div>
    </div>
    </form>
</body>
</html>
