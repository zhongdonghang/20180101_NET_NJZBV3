<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeatGraph.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.SeatMonitor.SeatGraph" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SeatGraph.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/SeatGraphHandle.js?a=1"></script>
    <script type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript">
        $(document).ready(
            function () {
                loadSeatLayout();
                intervalRun();
            })

        function intervalRun() {
            var interval;
            interval = setInterval(loadSeatLayout, '5000');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1"
        HideScrollbar="true" />
    <ext:Window ID="seatHandleWindow" Title="座位操作" Popup="false" EnableIFrame="true"
        IFrameUrl="about:blank" EnableMaximize="true" Target="Self" EnableResize="false"
        AutoHeight="true" runat="server" IsModal="true" Width="420px" EnableConfirmOnClose="true" 
        Height="385px">
    </ext:Window>
    <input type="hidden" id="hiddenRoomNum" value="<%=roomId %> " />
    <div id="bub_box" onclick="tipHidden()" style="position: absolute; z-index: 2147483647;
        width: 200px; left: 63px; display: none; top: 140px;">
        <div class="ns_bub_box-arrow" style="border-top: transparent 15px dashed; border-left: #e6e6e6 15px solid;
            position: absolute; left: 15px;">
        </div>
        <div id="bub_Content" class="ns_bub_wrapper" style="position: absolute; top: 10px;
            box-shadow: 3px 3px 3px #ccc; padding: 4px; background: #e6e6e6; border-radius: 5px;">
        </div>
    </div>
    <div class="mainDiv">
        <div id="divSeatGraphMain" class="SeatGraphMain">
             正在载入……
        </div>
    </div>
    </form>
</body>
</html>
