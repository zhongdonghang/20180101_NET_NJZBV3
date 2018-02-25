<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeatPad.aspx.cs" Inherits="SchoolPocketBookWeb.Pad.SeatPad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="/Styles/jquery.mobile.min.css" />
    <link rel="stylesheet" type="text/css" href="/Styles/Pad.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.mobile.min.js"></script>
    <script type="text/javascript" src="/Scripts/SeatGraphHandle.js"></script>
    <!--Includes-->
    <style>
        .dlClass td
        {
            height: 40px;
        }
    </style>
    <script>
        $.mobile.fixedtoolbar.prototype.options.tapToggle = false; //去除点击空白部分标头栏小时
        try {

        } catch (error) {
            console.error("Your javascript has an error: " + error);
        }
        function checkSeat(id) {
            if ($("#" + id).attr("checked")) {
                $("#" + id).removeAttr("checked");
            }
            else {
                $("#" + id).attr("checked", 'true');
            }
        }
        function SetOperateFlag(flag) {
            document.getElementById("subCmd").value = flag;
            form1.submit();
        }
        function GetCheckBox() {
            var ckb = $('input[name="ckbSeat"]');
            var noArr = "";
            for (var i = 0; i < ckb.length; i++) {
                if (ckb[i].checked) {
                    noArr += ckb[i].value + ",";
                }
            }
            $("#hidSeatNo").val(noArr.substring(0, noArr.length - 1));
        }
        $(document).ready(function () {
            $("#Panel1").height($(window).height() - 185);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" name="subCmd" id="subCmd" />
    <input type="hidden" name="seatNoArr" id="hidSeatNo" runat="server" />
    <!-- Home -->
       <div id="bub_box" onclick="tipHidden()" style="position: absolute; z-index: 2147483647;
        width: 200px; left: 63px; display: none; top: 140px;">
        <div id="bub_JanTou" class="ns_bub_box-arrow" style="border-top: transparent 15px dashed; border-left: #e6e6e6 15px solid;
            position: absolute; left: 15px;">
        </div>
        <div id="bub_Content" class="ns_bub_wrapper" style="position: absolute; top: 10px;
            box-shadow: 3px 3px 3px #ccc; padding: 4px; background: #e6e6e6; border-radius: 5px;">
        </div>
    </div>
    <%-- <div data-role="page" id="page1">
        <div data-role="content">
            <div data-theme="c" data-position="fixed" style="height: 140px; top: 0px">
                <div class="ui-grid-a" style="height: 50px">
                    <div class="ui-block-a" style="text-align: left">
                        <div data-role="fieldcontain" style="width: 100%">
                            <select id="selectReadingRomm" name="" data-mini="true" runat="server" onchange="SetOperateFlag('search')">
                            </select>
                        </div>
                    </div>
                    <div class="ui-block-b" data-mini="true" style="text-align: left">
                        <div data-role="fieldcontain">
                            <select id="selectSeatState" name="" data-theme="c" data-mini="true" runat="server"
                                onchange="SetOperateFlag('search')">
                                <option value="allSeat">全部 </option>
                                <option value="seated" selected="selected">在座 </option>
                                <option value="shortLeave">暂离 </option>
                                <option value="onTime">正在计时 </option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="ui-grid-b" style="height: 40px; text-align: left">
                    <a data-role="button" href="#page1" data-inline="true" onclick="GetCheckBox();SetOperateFlag('ShortLeave')"
                        data-mini="true" style="width: 80px">设置暂离 </a><a data-role="button" href="#page1"
                            data-inline="true" data-mini="true" style="width: 80px" onclick="GetCheckBox();SetOperateFlag('onTime')">
                            开始计时 </a><a data-role="button" data-inline="true" data-mini="true" style="width: 100px"
                                onclick="GetCheckBox();SetOperateFlag('AddBlacklist')">加入黑名单</a>
                </div>
                <div class="ui-grid-b" style="height: 40px; text-align: left">
                    <a data-role="button" href="#page1" data-inline="true" onclick="GetCheckBox();SetOperateFlag('ReleaseShortLeave')"
                        data-mini="true" style="width: 80px">取消暂离 </a><a data-role="button" href="#page1"
                            data-inline="true" data-mini="true" style="width: 80px" onclick="GetCheckBox();SetOperateFlag('offTime')">
                            停止计时 </a><a data-role="button" href="#page1" data-inline="true" data-mini="true"
                                style="width: 80px" onclick="GetCheckBox();SetOperateFlag('Release')">释放座位
                    </a><a data-role="button" data-inline="true" data-mini="true" style="width: 80px"
                        onclick="SetOperateFlag('search')">查询</a>
                </div>
            </div>
            <div style="margin-top: 140px" runat="server">
                <div id="seatContent" runat="server">
                </div>
            </div>
        </div>
        <div data-theme="c" data-role="footer" data-position="fixed">
            <h3>
                智佰闻欣图书馆座位管理移动平台
            </h3>
            <a data-role="button" class="ui-btn-right" onclick="SetOperateFlag('LoginOut')">注销</a>
        </div>
    </div>--%>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%">
                <div data-role="fieldcontain" style="width: 100%">
                    <select id="selectReadingRomm" name="" data-mini="true" runat="server" onchange="SetOperateFlag('search')">
                    </select>
                </div>
            </td>
            <td width="50%">
                <div data-role="fieldcontain">
                    <select width="50%" id="selectSeatState" name="" data-theme="c" data-mini="true"
                        runat="server" onchange="SetOperateFlag('search')">
                        <option value="allSeat">全部 </option>
                        <option value="seated" selected="selected">在座 </option>
                        <option value="shortLeave">暂离 </option>
                        <option value="onTime">正在计时 </option>
                    </select>
                </div>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <a data-role="button" href="#page1" data-inline="true" onclick="GetCheckBox();SetOperateFlag('ShortLeave')"
                    data-mini="true" style="width: 80px">设置暂离 </a><a data-role="button" href="#page1"
                        data-inline="true" data-mini="true" style="width: 80px" onclick="GetCheckBox();SetOperateFlag('onTime')">
                        开始计时 </a><a data-role="button" data-inline="true" data-mini="true" style="width: 100px"
                            onclick="GetCheckBox();SetOperateFlag('AddBlacklist')">加入黑名单</a>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <a data-role="button" href="#page1" data-inline="true" onclick="GetCheckBox();SetOperateFlag('ReleaseShortLeave')"
                    data-mini="true" style="width: 80px">取消暂离 </a><a data-role="button" href="#page1"
                        data-inline="true" data-mini="true" style="width: 80px" onclick="GetCheckBox();SetOperateFlag('offTime')">
                        停止计时 </a><a data-role="button" href="#page1" data-inline="true" data-mini="true"
                            style="width: 80px" onclick="GetCheckBox();SetOperateFlag('Release')">释放座位
                </a><a data-role="button" data-inline="true" data-mini="true" style="width: 80px"
                    onclick="SetOperateFlag('search')">刷新</a>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <%--<asp:Panel ID="Panel1" ScrollBars="Vertical" Height="200px" ClientIDMode="Static"
                    runat="server">--%>
                <div id="Panel1" style="width: 100%; overflow: hidden; overflow-x: hide; overflow-y:scroll; ">
                    <div id="seatContent"
                        runat="server">
                    </div>
                </div>
                <%--</asp:Panel>--%>
            </td>
        </tr>
    </table>
    <div data-theme="c" data-role="footer" data-position="fixed">
        <h3>
            智佰闻欣图书馆座位管理移动平台
        </h3>
        <a data-role="button" class="ui-btn-right" onclick="SetOperateFlag('LoginOut')">注销</a>
    </div>
    </form>
</body>
</html>
