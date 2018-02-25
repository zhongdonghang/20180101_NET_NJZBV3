<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="SchoolPocketBookOnlineV2.UserInfos.UserInfo" %>

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
    <script defer="defer">
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
        function initdatebox() {
            $("#txt_BookDate").attr("value", document.getElementById("chooseDate").value);
            //alert(document.getElementById("chooseDate").value);
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
<body>
    <form id="form1" runat="server">
    <input type="hidden" name="subCmd" runat="server" id="subCmd" />
    <input type="hidden" name="subBookNo" id="subBookNo" />
    <div data-role="page" id="page1">
        <div data-theme="d" data-role="header">
            <a data-role="button" onclick="javascript:history.go(-1);" class="ui-btn-left">返回
            </a><a data-role="button" onclick="location.href='../MainFunctionPage.aspx'" class="ui-btn-right">
                功能界面 </a>
            <h3>
                我的座位
            </h3>
        </div>
        <div>
            <div data-role="content">
                <h4>
                    座位详细信息
                </h4>
                <div class="ui-grid-a">
                    <div class="ui-block-a" style="width: 30%; height: 40px; text-align: right; line-height: 20px">
                        <span>
                            <h5>
                                阅览室：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="height: 40px; line-height: 20px; width: 70%;">
                        <span>
                            <h5 id="lblReadingRoomName" runat="server">
                            </h5>
                        </span>
                    </div>
                    <div class="ui-block-a" style="width: 30%; height: 40px; text-align: right; line-height: 20px">
                        <span>
                            <h5>
                                座位号：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="height: 40px; line-height: 20px; width: 70%;">
                        <span>
                            <h5 id="lblSeatNo" runat="server">
                            </h5>
                        </span>
                    </div>
                    <div class="ui-block-a" style="width: 30%; height: 40px; text-align: right; line-height: 20px">
                        <span>
                            <h5>
                                在座状态：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="height: 40px; line-height: 20px; width: 70%;">
                        <span>
                            <h5 id="lblSeatStatus" runat="server">
                            </h5>
                        </span>
                    </div>
                    <div class="ui-block-a" style="width: 30%; height: 40px; text-align: right; line-height: 20px">
                        <span>
                            <h5>
                                操作时间：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="height: 40px; line-height: 20px; width: 70%;">
                        <span>
                            <h5 id="lblenterOutTime" runat="server">
                            </h5>
                        </span>
                    </div>
                    <div class="ui-block-a" style="width: 30%; height: 40px; text-align: right; line-height: 20px">
                        <span>
                            <h5>
                                备注：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="height: 100px; line-height: 20px; width: 70%;">
                        <span>
                            <h5 id="lblRemark" runat="server">
                            </h5>
                        </span>
                    </div>
                </div>
                <h4>
                    座位操作
                </h4>
                <div class="ui-grid-b" style="margin-top: 0; margin-left: 0">
                    <div class="ui-block-a">
                        <a id="btnShortLeave" runat="server" class="btn_ShortLeave" onclick="subQuery('shortLeave')"
                            data-role="button" href="#page1"></a><a id="btn_ComeBack" runat="server" class="btn_ComeBack"
                                onclick="subQuery('ComeBack')" data-role="button" href="#page1" visible="false">
                            </a><a id="btn_CancelBook" runat="server" class="btn_CancelBook" onclick="subQuery('CancelBook')"
                                data-role="button" href="#page1" visible="false"></a><a id="btn_CancelWait" runat="server"
                                    class="btn_CancelWait" onclick="subQuery('CancelWait')" data-role="button" href="#page1"
                                    visible="false"></a>
                    </div>
                    <div class="ui-block-b">
                        <a id="btnLeave" runat="server" class="btn_Leave" onclick="subQuery('leave')" data-role="button"
                            href="#page1"></a><a id="btn_BookConfirm" runat="server" class="btn_BookConfirm"
                                onclick="subQuery('BookConfirm')" data-role="button" href="#page1"></a>
                    </div>
                    <div class="ui-block-c">
                        <a class="btn_ContinuedWhen" id="btn_ContinuedWhen" data-role="button" rel="external"
                            href="#page1" runat="server" onclick="subQuery('ContinuedWhen')"></a>
                    </div>
                    <div class="ui-block-a">
                        &nbsp;
                    </div>
                    <div class="ui-block-b">
                        &nbsp;
                    </div>
                    <div class="ui-block-c">
                        &nbsp;
                    </div>
                </div>
                <ul data-role="listview" data-divider-theme="b" data-inset="true" inset="true">
                    <%=listMessage%>
                </ul>
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
