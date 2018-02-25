<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WaitSeatMessage.aspx.cs" Inherits="SchoolPocketBookOnline.WaitSeat.WaitSeatMessage" %>

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

        function GoBookSeatListForm() {
            window.location.href = "WaitSeatListForm.aspx";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!-- Home -->
    <input type="hidden" name="subCmd" id="subCmd" />
    <div data-role="page" id="page1" runat="server">
        <div data-theme="d" data-role="header">
            <a data-role="button" class="ui-btn-right" onclick="javascript:history.go(-1);">返回
            </a>
            <h3>
                等待座位
            </h3>
        </div>
        <div data-role="content">
            <h4>
                座位详细信息
            </h4>
            <div class="ui-grid-a">
                <div class="ui-block-a" style="width: 50%; height: 40px; text-align: right">
                    <span>
                        <h5>
                            阅览室名称：</h5>
                    </span>
                </div>
                <div class="ui-block-b" style="height: 40px">
                    <span>
                        <h5 id="lblReadingRoomName" runat="server">
                        </h5>
                    </span>
                </div>
                <div class="ui-block-a" style="width: 50%; height: 40px; text-align: right">
                    <span>
                        <h5>
                            座位号：</h5>
                    </span>
                </div>
                <div class="ui-block-b" style="height: 40px">
                    <span>
                        <h5 id="lblSeatNo" runat="server">
                        </h5>
                    </span>
                </div>
                <div class="ui-block-a" style="width: 50%; height: 40px; text-align: right">
                    <span>
                        <h5>
                            等待时长：</h5>
                    </span>
                </div>
                <div class="ui-block-b" style="height: 40px">
                    <span>
                        <h5 id="lblWaitTime" runat="server">
                        </h5>
                    </span>
                </div>
                <div class="ui-block-a" style="width: 50%; height: 40px; text-align: right">
                    <span>
                        <h5>
                            入座时间：</h5>
                    </span>
                </div>
                <div class="ui-block-b" style="height: 40px">
                    <span>
                        <h5 id="lblGetSeatTime" runat="server">
                        </h5>
                    </span>
                </div>
                <div class="ui-block-a" style="width: 50%; text-align: center">
                    <input id="btnSubmit" data-inline="true" value="等待" data-mini="true" type="button" onclick="subQuery('query')" runat="server" />
                </div>
                <div class="ui-block-b" style="width: 50%; text-align: center">
                    <input data-inline="true" value="返回" data-mini="true" type="button" onclick="javascript:history.go(-1);" />
                </div>
                <span id="spanWarmInfo" name="spanWarmInfo" runat="server" style="color: Red"></span>
            </div>
        </div>
    </div>
    <div data-role="page" id="page2" runat="server" style="display: none">
        <div data-role="content">
            <h4>
                等待详情
            </h4>
            <div class="ui-grid-a">
                <div class="ui-block-a" style="width: 50%; height: 40px; text-align: right">
                    <span>
                        <h5>
                            阅览室名称：</h5>
                    </span>
                </div>
                <div class="ui-block-b" style="height: 40px">
                    <span>
                        <h5 id="lblRoomName_Booked" runat="server">
                        </h5>
                    </span>
                </div>
                <div class="ui-block-a" style="width: 50%; height: 40px; text-align: right">
                    <span>
                        <h5>
                            座位号：</h5>
                    </span>
                </div>
                <div class="ui-block-b" style="height: 40px">
                    <span>
                        <h5 id="lblSeatNo_Booked" runat="server">
                        </h5>
                    </span>
                </div>
                <div class="ui-block-a" style="width: 100%; height: 40px; text-align: center">
                    <span>
                        <h5 id="lblConfirmMessage" runat="server">
                        </h5>
                    </span>
                </div>
                <div class="ui-block-a" style="width: 100%; text-align: center">
                    <input data-inline="true" value="关闭" data-mini="true" type="button" onclick="GoBookSeatListForm()" />
                </div>
            </div>
        </div>
    </div>
        <div data-role="page" id="page3" runat="server" style="display: none">
        <div data-role="content">
            <h4>
                消息提醒
            </h4>
            <div class="ui-grid-a">
                <div class="ui-block-a" style="width: 100%; height: 40px; text-align: center">
                    <span>
                        <h5 id="MessageTip" runat="server">
                        </h5>
                    </span>
                </div>
                <div class="ui-block-a" style="width: 100%; text-align: center">
                    <input data-inline="true" value="关闭" data-mini="true" type="button" onclick="GoBookSeatListForm()" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
