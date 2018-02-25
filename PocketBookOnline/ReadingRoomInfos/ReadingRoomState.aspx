<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReadingRoomState.aspx.cs"
    Inherits="PocketBookOnline.ReadingRoomInfos.ReadingRoomState" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="/Styles/jquery.mobile.min.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.mobile.min.js"></script>
    <!-- User-generated css -->
    <style>
        
    </style>
    <!-- User-generated js -->
    <script>
        try {

            $(function () {

            });

        } catch (error) {
            console.error("Your javascript has an error: " + error);
        }
        function subQuery(cmd) {

            document.getElementById("subCmd").value = cmd;
            form1.submit();
        }
        //        $("#bithday").attr("readonly", "true").datepicker();
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!-- Home -->
    <input type="hidden" name="subCmd" id="subCmd" />
    <div data-role="page" id="page1">
        <div data-theme="d" data-role="header">
            <a data-role="button" onclick="javascript:history.go(-1);" class="ui-btn-left">返回
            </a><a data-role="button" onclick="location.href='../MainFunctionPage.aspx'" class="ui-btn-right">
                功能界面 </a>
            <h3>
                阅览室使用情况
            </h3>
        </div>
        <div data-role="content">
            <%=roomUsedMessage %>
        </div>
        <div data-theme="d" data-role="footer" data-position="fixed">
            <h3>
                智佰闻欣座位在线预约
            </h3>
            <a data-role="button" class="ui-btn-right" onclick="subQuery('LoginOut')">注销</a>
        </div>
    </div>
    </form>
</body>
</html>
