<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScanCode.aspx.cs" Inherits="PocketBookOnline.BookSeat.ScanCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <title>座位二维码扫描</title>
    <link rel="stylesheet" href="/Styles/jquery.mobile.min.css" />
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.mobile.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        //提交登录信息
        function subQuery(cmd) {
            document.getElementById("subCmd").value = cmd;
            form1.submit();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" name="subCmd" id="subCmd" />
    <div data-role="page" id="page1">
        <div style="text-align: center; height: 65px">
            <img src="/Images/LogoTop.png" alt="title" />
        </div>
        <!--座位信息--------------------------------------------------------------------------->
        <div style="padding-left: 20px;" data-role="content" id="divstuInfo" runat="server">
            <span id="spanWarmInfo" name="spanWarmInfo" runat="server" style="color: Red"></span>
            <div data-role="fieldcontain" style="border-width: 0px;" id="divHanderPanel" runat="server">
                <fieldset data-role="controlgroup" style="height: 40px;">
                    <label for="studentInfo" style="font-size: 14pt">
                        <b runat="server" id="ReaderInfo"></b>
                    </label> <br />
                     &nbsp;&nbsp;<label>您扫描的座位信息如下：</label> 
                </fieldset>
                <div id="seatInfo" runat="server">
                    <fieldset data-role="controlgroup" class="fieldset">
                        <label for="studentInfo" style="font-size: 12pt">
                            &nbsp;&nbsp;座位号：
                        </label>
                        <label for="studentInfo" runat="server" style="font-size: 12pt" id="lblSeatNum">
                            201
                        </label> 
                    </fieldset>
                    <fieldset data-role="controlgroup" class="fieldset">
                        <label for="studentInfo" style="font-size: 12pt">
                            &nbsp;&nbsp;阅览室：
                        </label>
                        <label runat="server" for="studentInfo" id="lblRoomName" style="font-size: 12pt">
                        </label>
                    </fieldset>
                    <fieldset data-role="controlgroup" class="fieldset" style="height: 25px;">
                        <label for="studentInfo" style="font-size: 12pt">
                            &nbsp;&nbsp;座位状态：
                        </label>
                        <label for="studentInfo" style="font-size: 12pt" runat="server" id="lblSeatState">
                        </label>
                    </fieldset>
                </div>
                <fieldset data-role="controlgroup" class="fieldset" style="border: 1px solid black;
                    padding: 10px; margin: 5px;">
                    <legend><b>座位信息</b></legend>
                    <label for="studentInfo" style="font-size: 12pt" id="lblReaderMsg" runat="server">
                        您当前在{0}座位上，可以更换到这个座位，或者预约该座位。 您还没有选座，您可以在触摸屏上直接选择该座位
                    </label>
                    <br />
                    <input data-inline="true" value="更换" data-mini="false"    type="button" onclick="subQuery('changeSeat')"
                        id="btnChangeSeat" runat="server" />
                </fieldset>
                <fieldset data-role="controlgroup" class="fieldset" id="fsBespeakInfo" runat="server"
                    style="border: 1px solid black; padding: 10px; margin: 5px;">
                    <legend><b runat="server" id="bespeakTitle">明天的预约信息</b></legend> 
                        <label for="studentInfo" style="font-size: 12pt" id="lblBeapeakMsg" runat="server">
                            该座位尚未被预约，您可以预约该座位
                        </label> 
                    <br />
                    <input data-inline="true" value="预约" data-mini="false" type="button" onclick="subQuery('bespeak')"
                        id="btnBespeak" runat="server" />
                    <label for="studentInfo" style="font-size: 12pt" id="lblBookTime" runat="server"
                        hidden="hidden">
                    </label>
                </fieldset>
                <%--<input data-inline="true" value="确认入座" data-mini="false" type="button" onclick="modifyBindbtnOnClick()"
                    id="btnBookingConfirmation" />
                <input data-inline="true" value="重新登录" data-mini="false" type="button" onclick="modifyBindbtnOnClick()"
                    id="btnRelogin" />--%>
            </div>
        </div>
        <div data-theme="d" data-role="footer" data-position="fixed">
            <%--<h5>
                南京智佰闻欣图书馆座位绑定系统</h5>--%>
        </div>
    </div>
    </form>
</body>
</html>
