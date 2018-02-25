<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeatList.aspx.cs" Inherits="SchoolPocketBookOnlineV2.SelectSeat.SeatList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../Styles/jquery.mobile.min.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.mobile.min.js"></script>
    <!--Includes-->
    <link href="../Styles/mobiscroll.custom-2.4.4.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/mobiscroll.custom-2.4.4.min.js" type="text/javascript"></script>
    <!-- User-generated css -->
    <style>
        .dlClass td
        {
            height: 40px;
        }
    </style>
    <!-- User-generated js -->
    <script>
        try {

        } catch (error) {
            console.error("Your javascript has an error: " + error);
        }

        function OpenBookMessage() {
            window.showModalDialog("BookNowSeatMessage.aspx", "", "dialogWidth=300px;dialogHeight=300px;scroll:no;status=no");
        }
        function subQuery(cmd) {
            document.getElementById("subCmd").value = cmd;
            form1.submit();
        }
        function ShowBookMessage(seatNo, seatShortNo, CanBespeakSpan) {
            var No = seatNo;
            var ShortNo = seatShortNo;
            var rrid = $("#hidRrId").val();
            var bookDate = $('#txtBookDate').val();
            var timeSpan = CanBespeakSpan;
            window.location.href = "SubmitSeat.aspx?seatNo=" + No + "&seatShortNo=" + ShortNo + "&roomNo=" + rrid;
        }
        function DateOnfocus() {
            $(function () {
                var curr = new Date().getFullYear();
                var opt = {
                }
                opt.date = { preset: 'date' };
                opt.datetime = { preset: 'date', minDate: new Date(2012, 3, 10, 9, 22), maxDate: new Date(2020, 30, 15, 44), stepMinute: 5 };

                opt.time = { preset: 'time' };
                $('#hidBookDate').val($('#txtBookDate').val());
                $('#txtBookDate').val($('#hidBookDate').val()).scroller('destroy').scroller($.extend(opt['date'], { theme: 'default', mode: 'scroller', display: 'modal', dayText: '日', monthText: '月', yearText: '年', setText: '确定', cancelText: '取消', dateFormat: 'yy-mm-dd', dateOrder: 'yymmdd', startYear: 2000, endYear: 2100 }));
            });
        }
    </script>
</head>
<body>
    <!-- Home -->
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidRrId" runat="server" />
    <input type="hidden" name="subCmd" id="subCmd" />
    <div data-role="page" id="page1">
        <div data-theme="d" data-role="header">
            <a data-role="button" onclick="javascript:history.go(-1);" class="ui-btn-left">返回
            </a><a data-role="button" onclick="location.href='../MainFunctionPage.aspx'" class="ui-btn-right">
                功能界面 </a>
            <h3>
                预约座位
            </h3>
        </div>
        <div data-role="content">
            <div data-role="fieldcontain">
                <table width="100%">
                    <tr>
                        <td width="80%">
                            <select name="" data-mini="true" id="selReadingRoom" runat="server" onchange="subQuery('query')">
                            </select>
                        </td>
                        <td style="text-align: right">
                            <input data-inline="true" value="查询" data-mini="true" type="button" onclick="subQuery('query')" />
                        </td>
                    </tr>
                </table>
            </div>
            <span id="spanWarmInfo" name="spanWarmInfo" runat="server" style="color: Red"></span>
            <asp:DataList ID="DataListBookSeat" runat="server" RepeatDirection="Horizontal" RepeatColumns="4"
                class="dlClass" Width="100%">
                <ItemTemplate>
                    <div id="divItem" runat="server" data-role="fieldcontain" style="width: 25%; height: 30px;
                        margin-left: 0; margin-top: 0">
                        <div onclick='ShowBookMessage(&quot;<%# Eval("SeatNo")%>&quot;,&quot;<%# Eval("ShortSeatNo")%>&quot;)'
                            data-inline="true" data-transition="none" data-role="button" style="width: 69px;
                            text-align: center; margin-left: 0; margin-top: 0">
                            <%# Eval("ShortSeatNo")%>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
        <div data-theme="d" data-role="footer" data-position="fixed">
            <h3>
                智佰闻欣图书馆座位在线
            </h3>
            <%--<a data-role="button" class="ui-btn-right" onclick="subQuery('LoginOut')">注销</a>--%>
        </div>
    </div>
    </form>
</body>
</html>
