<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormSYS.aspx.cs" Inherits="SeatManageWebV2.Florms.FormSYS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=Title%></title>
    <link href="../Styles/default.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Clock.js" type="text/javascript" />
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
            <ext:Region runat="server" ID="Region_Top" Layout="Fit" Position="Top" Height="78px"
                ShowHeader="false" ShowBorder="false" Margins="0 0 0 0">
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
                                    <asp:Label ID="lblUserName" runat="server" ForeColor="White"> 当前用户：，欢迎您！</asp:Label>
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
                                <td align="right" nowrap="nowrap" align="right" background="../Images/top_middle.jpg">
                                </td>
                                <td width="10" background="../Images/top_right.jpg">
                                    &nbsp;
                                </td>
                                <td width="5" class="bg_color">
                                </td>
                            </tr>
                        </table>
                    </ext:ContentPanel>
                </Items>
                <Toolbars>
                    <ext:Toolbar ID="toolbar1" runat="server" Position="Bottom" Height="30px">
                        <Items>
                            <ext:ToolbarText ID="ToolbarText1" Text="&nbsp;" runat="server">
                            </ext:ToolbarText>
                            <ext:Button ID="btnExpandAll" IconUrl="../Images/icon/expand-all.gif" Text="展开全部"
                                EnablePostBack="false" runat="server">
                            </ext:Button>
                            <ext:Button ID="btnCollapseAll" IconUrl="../Images/icon/collapse-all.gif" Text="折叠全部"
                                EnablePostBack="false" runat="server">
                            </ext:Button>
                            <ext:ToolbarFill ID="ToolbarFill1" runat="server">
                            </ext:ToolbarFill>
                            <ext:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                            </ext:ToolbarSeparator>
                            <ext:Button ID="btnPassword" runat="server" Text="密码修改" EnablePostBack="false" Icon="Cog">
                            </ext:Button>
                            <ext:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                            </ext:ToolbarSeparator>
                            <ext:Button ID="btnExit" runat="server" Text="退出" Icon="Exclamation" ConfirmText="确定需要退出系统！"
                                ConfirmIcon="Question" OnClick="btnExit_Click">
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </Toolbars>
            </ext:Region>
            <ext:Region ID="Region_Left" Split="true" EnableSplitTip="true" CollapseMode="Mini"
                Width="200px" Margins="0 0 0 0" ShowHeader="true" Icon="Outline" EnableCollapse="true"
                Layout="Fit" Position="Left" runat="server" Title="功&nbsp;能&nbsp;菜&nbsp;单">
                <Items>
                    <ext:ContentPanel ID="ContentPanel2" ShowBorder="false" ShowHeader="false" runat="server"
                        AutoScroll="true">
                        <table style="height: 100%" border="0" cellpadding="0" cellspacing="0" valign="top">
                            <tr>
                                <td id="outLookBarShow" valign="top" align="left" style="width: 200px">
                                    <ext:Tree ID="TreeMenu" Width="158px" EnableArrows="true" ShowHeader="false" ShowBorder="false"
                                        Title="" runat="server">
                                    </ext:Tree>
                                </td>
                            </tr>
                        </table>
                    </ext:ContentPanel>
                </Items>
            </ext:Region>
            <ext:Region ID="MainRegion" ShowHeader="false" Layout="Fit" Margins="0 0 0 0" Position="Center"
                runat="server" EnableBackgroundColor="false">
                <Items>
                    <ext:TabStrip runat="server" ID="mainTabStrip" ShowBorder="false" EnableTabCloseMenu="true"
                        AutoScroll="true">
                        <Tabs>
                            <ext:Tab Title="主&nbsp;页" Icon="House" runat="server" ID="houseTab" Layout="Fit"
                                AutoScroll="true" EnableBackgroundColor="false" EnableIFrame="true" IFrameUrl="../FunctionPages/Statistical/LibraryUsedStatistical.aspx">
                            </ext:Tab>
                        </Tabs>
                    </ext:TabStrip>
                </Items>
            </ext:Region>
        </Regions>
    </ext:RegionPanel>
    <ext:Window ID="Window_Edit_Password" Popup="false" EnableIFrame="true" IFrameUrl="#"
        runat="server" Title="修改密码" Height="160px" Width="370px">
    </ext:Window>
    </form>
    <script type="text/javascript">
        var IDS = {
            treeMenu: '<%= TreeMenu.ClientID %>',
            toolbar: '<%= toolbar1.ClientID %>',
            btnExpandAll: '<%= btnExpandAll.ClientID %>',
            btnCollapseAll: '<%= btnCollapseAll.ClientID %>',
            mainTabStrip: '<%= mainTabStrip.ClientID %>'

        }; 
    </script>
    <script type="text/javascript" src="../Scripts/Default.js"></script>
</body>
</html>
