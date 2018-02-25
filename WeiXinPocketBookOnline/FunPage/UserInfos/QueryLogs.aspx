<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QueryLogs.aspx.cs" Inherits="WeiXinPocketBookOnline.UserInfos.QueryLogs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../Styles/jquery.mobile.min.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.mobile.min.js"></script>
    <!-- User-generated css -->
    <style>
        
    </style>
    <!-- User-generated js -->
    <script defer="defer">
        try {

            $(function () {

            });

        } catch (error) {
            console.error("Your javascript has an error: " + error);
        }

        function initdatebox() {
            $("#txt_BookDate").attr("value", document.getElementById("chooseDate").value);
            //alert(document.getElementById("chooseDate").value);
        }
        function subQuery() {
            form1.submit();
        }
        function setPageValue(cmd) {
            $("#subCmd").val(cmd);
        }
        function subCancel(cmd) {
            $("#subCmd").val('cancel');
            $("#subBookNo").val(cmd);
            form1.submit();
        }
        function subLoginOut(cmd) {
            document.getElementById("subCmd").value = cmd;
            form1.submit();
        }
    </script>
</head>
<body onload="initdatebox()">
    <!-- Home -->
    <form id="form1" name="form1" runat="server">
        <input type="hidden" name="subCmd" id="subCmd" runat="server" />
        <input type="hidden" name="subBookNo" id="subBookNo" />
        <input type="hidden" name="chooseDate" id="chooseDate" runat="server" value="选择日期" />
        <div data-role="page" id="querylogs">
            <div data-theme="d" data-role="header">
                <a data-role="button" onclick="javascript:history.go(-1);" class="ui-btn-left">返回
                </a><a data-role="button" onclick="location.href='../MainFunctionPage.aspx'" class="ui-btn-right">功能界面 </a>
                <h3>记录查询
                </h3>
            </div>
            <div data-role="navbar" data-theme="d">
                <ul>
                    <li><a href="#" class="ui-btn-active" onclick="setPageValue('BookLogs')" id="bLi"
                        runat="server">预约记录</a></li>
                    <li><a href="#" onclick="setPageValue('EnterOutLog')" id="eLi" runat="server">进出记录</a></li>
                </ul>
            </div>
            <div data-role="content">
                <div data-role="fieldcontain">
                    <fieldset data-role="controlgroup" data-mini="true">
                        <label for="txt_BookDate">
                        </label>
                        <select id="ddlDate" name="" data-mini="true" runat="server">
                            <option value="7">一周内 </option>
                            <option value="21">三周内 </option>
                            <option value="30">一个月内 </option>
                            <option value="60">三个月内 </option>
                            <option value="180">六个月内 </option>
                        </select>
                    </fieldset>
                </div>
                <div data-role="fieldcontain">
                    <table width="100%">
                        <tr>
                            <td width="80%">
                                <select id="ddlRoom" name="" data-mini="true" runat="server">
                                    <option value="-1">全部阅览室 </option>
                                </select>
                            </td>
                            <td align="right">
                                <input data-inline="true" data-mini="true" value="查询" type="button" onclick="subQuery()" />
                            </td>
                        </tr>
                    </table>
                </div>
                <span id="spanWarmInfo" name="spanWarmInfo" runat="server" style="color: Red"></span>
                <ul data-role="listview" data-divider-theme="b" data-inset="true" inset="true">
                    <%=listMessage%>
                </ul>
                <div data-theme="d" data-role="footer" data-position="fixed">
                    <h3>智佰闻欣图书馆座位在线
                    </h3>
                    <%--<a data-role="button" class="ui-btn-right" onclick="subLoginOut('LoginOut')">注销</a>--%>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
