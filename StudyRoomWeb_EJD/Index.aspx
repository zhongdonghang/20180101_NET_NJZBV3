<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="StudyRoomWeb.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>研习间预约</title>
    <link href="/Styles/default.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/Clock.js" type="text/javascript" />
    <style type="text/css">
        .bg
        {
            border: 0px #000 solid;
            overflow: hidden;
            text-align: center;
        }
    </style>
    <script language="javascript">
       
    </script>
</head>
<body oncontextmenu="return false;">
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1"
        HideScrollbar="true" />
    <ext:RegionPanel ID="RegionPanel_1" runat="server" ShowBorder="false">
        <Regions>
            <ext:Region runat="server" ID="Region_Top" Layout="Fit" Position="Top" Height="48px"
                ShowHeader="false" ShowBorder="false" Margins="0 150 0 150">
                <Items>
                    <ext:ContentPanel ID="ContentPanel1" ShowBorder="false" ShowHeader="false" runat="server">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="630" height="50" background="../Images/FormTop.png">
                                    &nbsp;
                                </td>
                                <td align="center" valign="center" class="bg_color">
                                    &nbsp;
                                </td>
                                <td width="130" align="right" valign="center" class="bg_color">
                                    <asp:Label ID="lblUserName" runat="server" ForeColor="White"> 欢迎使用</asp:Label>
                                </td>
                                <td width="280" align="right" valign="center" class="bg_color">
                                    <div id="clock" align="center" class="bg_color" style="color: #FFFFFF">
                                        1900-1-1 00:00:00</div>
                                    <script type="text/javascript">
                                        var clock = new Clock();
                                        clock.display(document.getElementById("clock"));  
                                    </script>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="5" height="28" class="bg_color">
                                </td>
                                <td width="10" background="../Images/top_left.jpg">
                                    &nbsp;
                                </td>
                                <td align="left" background="../Images/top_middle.jpg">
                                </td>
                                <td align="right" nowrap="nowrap" align="right" background="/Images/top_middle.jpg">
                                </td>
                                <td width="10" background="/Images/top_right.jpg">
                                    &nbsp;
                                </td>
                                <td width="5" class="bg_color">
                                </td>
                            </tr>
                        </table>
                    </ext:ContentPanel>
                </Items>
            </ext:Region>
            
            <ext:Region ID="MainRegion" ShowHeader="false" Layout="Fit" Margins="0 150 0 150" Position="Center"
                runat="server" EnableBackgroundColor="false">
                <Items>
                    <ext:TabStrip runat="server" ID="mainTabStrip" ShowBorder="false" EnableTabCloseMenu="true"
                        AutoScroll="true">
                        <Tabs>
                            <ext:Tab  Title="预约研习间" Icon="House" runat="server" ID="houseTab" Layout="Fit"
                                AutoScroll="true" EnableBackgroundColor="false" EnableIFrame="true" IFrameUrl="/FunctionPages/BespeakStudyRoom/StudyRoomList.aspx">
                            </ext:Tab>
                            <ext:Tab  Title="预约记录查询" Icon="House" runat="server" ID="Tab1" Layout="Fit"
                                AutoScroll="true" EnableBackgroundColor="false" EnableIFrame="true" IFrameUrl="/FunctionPages/BespeakStudyRoom/StudyBookingLog.aspx">
                            </ext:Tab>
                        </Tabs>
                    </ext:TabStrip>
                </Items>
            </ext:Region>
        </Regions>
    </ext:RegionPanel>
    </form>

    <script type="text/javascript" src="../Scripts/Default.js"></script>
</body>
</html>
