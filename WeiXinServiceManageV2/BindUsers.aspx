<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BindUsers.aspx.cs" Inherits="WeiXinServiceManage.BindUsers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <title>绑定微信帐号</title>
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
            else if (selValue == "-请选择学校-") {
                $("#spanWarmInfo").html("请选择您所在的学校");
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
                setTimeout(subQuery(newcmd), 3000);
            }
        }
        //点击修改绑定按钮，隐藏我的资料，显示绑定界面
        function modifyBindbtnOnClick() {
            $("#divstuInfo").hide();
            $("#divSuccess").hide();
            $("#divcontent").show();
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
        .fieldset
        {
             height: 26px;
        }
    </style>
</head>
<body>
    <!-- Home -->
    <form id="form1" runat="server">
    <input type="hidden" name="subCmd" id="subCmd" />
    <div data-role="page" id="page1">
        <div style="text-align: center; height: 65px">
            <img src="Images/LogoTop.png" alt="title" />
        </div>
        <!--读者信息--------------------------------------------------------------------------->
        <div style="padding-left:50px; " data-role="content"  id="divstuInfo" runat="server" >
            <div data-role="fieldcontain" style="height: 120px; border-width: 0px;" >
                <fieldset data-role="controlgroup" style="height:34px;">
                    <label for="studentInfo" style="font-size: 14pt">
                        <b runat="server" id="ReaderInfo">我的资料</b>
                    </label>
                </fieldset>
                <fieldset data-role="controlgroup" class="fieldset">
                    <label for="studentInfo" style="font-size: 12pt">
                        &nbsp;&nbsp;学校：
                    </label>
                    <label for="studentInfo" runat="server" style="font-size: 12pt" id="lblmySchool">东南大学
                    </label>
                    <b></b>
                </fieldset>
                <fieldset data-role="controlgroup" class="fieldset">
                    <label for="studentInfo" style="font-size: 12pt">
                        &nbsp;&nbsp;学号：
                    </label>
                    <label runat="server" for="studentInfo" style="font-size: 12pt" id="lblMyStuCode">2032934
                    </label>
                </fieldset>
                <fieldset data-role="controlgroup" class="fieldset">
                    <label for="studentInfo" style="font-size: 12pt">
                        &nbsp;&nbsp;姓名：
                    </label>
                    <label for="studentInfo" style="font-size: 12pt" id="lblMyName" runat="server">陈晓腾
                    </label>
                </fieldset>
            </div>
             <input data-inline="true" value="修改绑定" data-mini="false" type="button" onclick="modifyBindbtnOnClick()" />
        </div>

        <!--修改成功提醒------------------------------------------------------------------------------>
        <div style="padding-left:50px; display:none;" data-role="content"  id="divSuccess" runat="server" >
            <fieldset data-role="controlgroup" style="height:34px;">
                    <label for="studentInfo" style="font-size: 14pt; color:Green;">
                        <b runat="server" id="B1" >操作成功!</b>
                    </label>
                </fieldset>
                <input data-inline="true" value="关闭" data-mini="false" type="button" onclick="window.close();" />
        </div>
        <!--绑定部分---------------------------------------------------------------------------------->
        <div data-role="content" id="divcontent" style=" display:none;" runat="server">
            <span id="spanWarmInfo" name="spanWarmInfo" runat="server" style="color: Red"></span>
            <div data-role="fieldcontain" style="height: 48px; border-width: 0px">
                <fieldset data-role="controlgroup">
                    <label for="textinput1" style="font-size: 11pt">
                        <b>学号：</b>
                    </label>
                    <input name="txt_LoginID" id="txt_LoginID" placeholder="" value="" type="text" runat="server"
                        onfocus="ReSetWarnInfo()" />
                </fieldset>
            </div>
            <div data-role="fieldcontain" style="height: 63px; border-width: 0px">
                <fieldset data-role="controlgroup">
                    <label for="textinput2" style="font-size: 11pt" id="input2">
                        <b>密码：</b>
                    </label>
                    <input name="txt_Password" id="txt_Password" placeholder="" value="" type="password"
                        runat="server" onfocus="ReSetWarnInfo()" />
                </fieldset>
            </div>
            <div data-role="fieldcontain" style="height: 40px; border-width: 0px">
                <label for="selectmenu2">
                </label>
                <select id="selSchool" name="" data-mini="true" runat="server" onchange="SelOnChange()">
                </select>
            </div>
            <table width="100%">
                <tr>
                    <td width="80px">
                        <div style="text-align: right; vertical-align: bottom">
                            <input data-inline="true" value="提交" data-mini="false" type="button" onclick="Operate('Login')" />
                            <%--<input data-inline="true" value="重置" data-mini="false" type="button" onclick="ReSet()" />--%>
                        </div>
                    </td>
                    <td align="left">
                        <div id="checkboxes1" data-role="fieldcontain" style="border-width: 0px">
                            <fieldset data-role="controlgroup" data-type="vertical" style="width: 140px; border-width: 0px"
                                data-mini="true">
                            </fieldset>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div data-theme="d" data-role="footer" data-position="fixed">
            <h5>
              <%--  南京智佰闻欣图书馆座位绑定系统--%></h5>
        </div>
    </div>
    </form>
</body>
</html>
