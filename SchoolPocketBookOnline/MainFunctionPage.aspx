<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainFunctionPage.aspx.cs" Inherits="SchoolPocketBookOnline.MainFunctionPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <title></title>
    <link rel="stylesheet" href="Styles/jquery.mobile.min.css" />
    <script src="Scripts/jquery-1.7.2.min.js">
    </script>
    <script src="Scripts/jquery.mobile.min.js">
    </script>
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

    </script>
</head>
<body>
    <!-- Home -->
    <form id="form1" runat="server">
    <input type="hidden" name="subCmd" id="subCmd" />
    <div data-role="page" id="page1">
        <div data-theme="d" data-role="header">
            <div style="">
                <img style="width: 220px; height: 50px" src="Images/LogoTop.png" />
            </div>
        </div>
        <div data-theme="c" data-role="header">
            <div class="ui-grid-a">
                <span id="SpanNowState" runat="server" data-inline="true"></span>
                <asp:LinkButton ID="btnRefresh" data-mini="true" data-inline="true" 
                    runat="server" onclick="btnRefresh_Click">刷新</asp:LinkButton>
            </div>
        </div>
        <div data-role="content">
            <div class="ui-grid-b" style="margin-top: 0; margin-left: 0">
                <div class="ui-block-a">
                    <a class="btn_book" data-role="button" rel="external" href="BookSeat/BookSeatListForm.aspx">
                    </a>
                </div>
                <div class="ui-block-b">
                    <a class="btn_UsedState" data-role="button" rel="external" href="ReadingRoomInfos/ReadingRoomState.aspx">
                    </a>
                </div>
                <div class="ui-block-c">
                    <a class="btn_Query" data-role="button" rel="external" href="UserInfos/QueryLogs.aspx">
                    </a>
                </div>
                <div class="ui-block-a">
                    <a id="btnShortLeave" runat="server" class="btn_ShortLeave" onclick="subQuery('shortLeave')"
                        data-role="button" href="#page1"></a>
                </div>
                <div class="ui-block-b">
                    <a id="btnLeave" runat="server" class="btn_Leave" onclick="subQuery('leave')" data-role="button"
                        href="#page1"></a>
                </div>
                <div class="ui-block-c">
                    <a class="btn_BlackList" data-role="button" rel="external" href="UserInfos/BlackList.aspx">
                    </a>
                </div>
                <div class="ui-block-a">
                    <a id="btn_ComeBack" runat="server"
                     class="btn_ComeBack" onclick="subQuery('ComeBack')" data-role="button" href="#page1"></a>
                </div>
                 <div class="ui-block-b">
                    <a id="btn_WaitSeat" runat="server"
                     class="btn_WaitSeat"  rel="external" data-role="button" href="WaitSeat/WaitSeatListForm.aspx"></a>
                </div>
                <div class="ui-block-c">
                    <a id="btn_ContinuedWhen" runat="server"
                     class="btn_ContinuedWhen" onclick="subQuery('ContinuedWhen')" data-role="button" href="#page1"></a>
                </div>
               
            </div>
            <span id="spanWarmInfo" name="spanWarmInfo" runat="server" style="color: Red"></span>
        </div>
        <div data-theme="d" data-role="footer" data-position="fixed">
            <a data-role="button" class="ui-btn-left" rel="external" href="AboutUs/Feedback.aspx">
                意见反馈</a>
            <h5>
            </h5>
            <a data-role="button" class="ui-btn-right" onclick="subQuery('LoginOut')">注销</a>
        </div>
    </div>
    </form>
</body>
</html>
