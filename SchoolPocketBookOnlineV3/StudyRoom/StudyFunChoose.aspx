<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudyFunChoose.aspx.cs" Inherits="SchoolPocketBookWeb.StudyRoom.StudyFunChoose" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <link rel="stylesheet" type="text/css" href="../Styles/jquery.mobile.min.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.mobile.min.js"></script>
    <!--Includes-->
    <link href="../Styles/mobiscroll.custom-2.4.4.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/mobiscroll.custom-2.4.4.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div data-role="page" id="page1">
        <div data-theme="d" data-role="header">
            <a data-role="button" onclick="javascript:history.go(-1);" class="ui-btn-left">返回
            </a><a data-role="button" onclick="location.href='/MainFunctionPage.aspx'" class="ui-btn-right">
                功能界面 </a>
            <h3>
                研习间
            </h3>
        </div>
    <div>
     <div data-role="content">
            <div class="ui-grid-b" style="margin-top: 0; margin-left: 0">
                <div class="ui-block-a">
                    <a class="btn_BookStudyRoom" data-role="button" rel="external" href="/StudyRoom/StudyRoomList.aspx">
                    </a>
                </div>
                <div class="ui-block-b">
                   &nbsp;
                </div>
                <div class="ui-block-c">
                    <a class="btn_StudyLog" data-role="button" rel="external" href="/StudyRoom/StudyLogList.aspx">
                    </a>
                </div>
            </div>
            <span id="spanWarmInfo" name="spanWarmInfo" runat="server" style="color: Red"></span>
        </div>
        <div data-theme="d" data-role="footer" data-position="fixed">
            <h5>智佰闻欣图书馆座位在线
            </h5>
            <%--<a data-role="button" class="ui-btn-right" onclick="subQuery('LoginOut')">注销</a>--%>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
