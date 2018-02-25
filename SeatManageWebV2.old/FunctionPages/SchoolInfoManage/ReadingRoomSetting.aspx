<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReadingRoomSetting.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.SchoolInfoManage.ReadingRoomSetting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .borderStyle
        {
            border-collapse: collapse;
            border: 0px dotted #DFE8F6;
            border-width: 5px;
        }
    </style>
    <script type="text/javascript" language="javascript" src="../../Scripts/RoomSettingVerification.js">
    </script>
    <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.4.1.js">
    </script>
    <script type="text/javascript" language="javascript">
        function CBcheck(id) {
            if ($("#" + id + "")[0].checked == true) {
                document.getElementById(id + "Table").style.display = "block";
            }
            else {
                document.getElementById(id + "Table").style.display = "none";
            }
        }
        function CBcheckALL(id) {
            if ($("#" + id + "")[0].checked == true) {
                for (var i = 0; i < 100; i++) {
                    $("#SameRoomSet_" + i).attr("checked", true);
                }
            }
            else {
                for (var i = 0; i < 100; i++) {
                    $("#SameRoomSet_" + i).attr("checked", false);
                }
            }
        }
        function BLCBcheck(id) {
            if ($("#" + id + "")[0].checked == true) {
                document.getElementById(id + "Table").style.display = "block";
                $("#IsRecordViolate").attr("checked", true);
                $("#UseBlacklist").attr("checked", true);
            }
            else {
                document.getElementById(id + "Table").style.display = "none";
            }
        }
        function showToolTip(e, tex) {
            var obj = (e.sroElement) ? e.sroElement : (e.target) ? e.target : null;
            if (obj != null && obj.tagName == 'LABEL') {
                obj.title = tex.toString();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1"
        HideScrollbar="true" />
    <ext:Panel ID="PanelSetting" ClientIDMode="Static" runat="server" EnableBackgroundColor="false"
        BodyPadding="1px" Height="500px" AutoScroll="true" ShowBorder="false" ShowHeader="true"
        Title="阅览室设置">
        <Items>
            <ext:ContentPanel BoxConfigAlign="Center" runat="server" BodyPadding="3px" ShowHeader="true"
                Title="选座方式设置" EnableBackgroundColor="true" ClientIDMode="Static" ID="FormReadingRoomSet">
                <table width="90%">
                    <tr>
                        <td width="150" class="borderStyle">
                            <span>选座方式设置：</span>
                        </td>
                        <td width="200">
                            <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ClientIDMode="Static"
                                ID="SeatSelectDefaultMode">
                                <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                <asp:ListItem Text="自选选座" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="left" width="80px">
                            启用高级设置
                        </td>
                        <td align="left">
                            <input type="checkbox" id="SeatSelectAdMode" runat="server" onclick="CBcheck('SeatSelectAdMode')" />
                        </td>
                    </tr>
                    <tr>
                        <td width="150px" class="borderStyle">
                            <span>选座次数限制：</span>
                        </td>
                        <td align="left" width="200px">
                            <input type="checkbox" id="SeatSelectPos" runat="server" />启用&nbsp;&nbsp;
                            <asp:TextBox Height="13px" ID="SelectSeatPosTimes" runat="server" Width="25" ClientIDMode="Static"></asp:TextBox>分钟内<asp:TextBox
                                Height="13px" ID="SelectSeatPosCount" runat="server" Width="25" ClientIDMode="Static"></asp:TextBox>次
                            <span id="SelectSeatPos_error" title="" style="color: Red;"></span>
                        </td>
                        <td align="left" width="80px">
                        </td>
                        <td align="left">
                        </td>
                    </tr>
                </table>
                <table width="90%" id="SeatSelectAdModeTable" runat="server" style="display: none">
                    <tr>
                        <td width="100">
                        </td>
                        <td colspan="2">
                            <table width="600" cellspacing="0" cellpadding="5" border="1" class="borderStyle">
                                <tr>
                                    <td class="borderStyle" style="font-weight: bold;" width="75px">
                                        计划日期
                                    </td>
                                    <td class="borderStyle" width="250px" style="font-weight: bold;">
                                        时间段
                                    </td>
                                    <td class="borderStyle" width="200px" style="font-weight: bold;">
                                        选座方式
                                    </td>
                                    <td class="borderStyle" width="80px" style="font-weight: bold;">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                        <asp:CheckBox ClientIDMode="Static" ID="SeatSelectAdModeDay1" runat="server" Text="周一" />
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段1：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay1_Time1_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay1_Time1_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay1_Time1_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay1_Time1_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay1_Time1_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay1_Time1_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段2：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay1_Time2_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay1_Time2_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay1_Time2_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay1_Time2_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay1_Time2_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay1_Time2_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段3：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay1_Time3_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay1_Time3_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay1_Time3_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay1_Time3_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay1_Time3_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay1_Time3_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                        <asp:CheckBox ClientIDMode="Static" ID="SeatSelectAdModeDay2" runat="server" Text="周二" />
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段1：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay2_Time1_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay2_Time1_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay2_Time1_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay2_Time1_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay2_Time1_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay2_Time1_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段2：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay2_Time2_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay2_Time2_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay2_Time2_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay2_Time2_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay2_Time2_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay2_Time2_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段3：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay2_Time3_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay2_Time3_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay2_Time3_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay2_Time3_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay2_Time3_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay2_Time3_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                        <asp:CheckBox ClientIDMode="Static" ID="SeatSelectAdModeDay3" runat="server" Text="周三" />
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段1：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay3_Time1_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay3_Time1_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay3_Time1_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay3_Time1_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay3_Time1_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay3_Time1_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段2：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay3_Time2_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay3_Time2_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay3_Time2_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay3_Time2_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay3_Time2_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay3_Time2_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段3：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay3_Time3_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay3_Time3_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay3_Time3_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay3_Time3_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay3_Time3_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay3_Time3_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                        <asp:CheckBox ClientIDMode="Static" ID="SeatSelectAdModeDay4" runat="server" Text="周四" />
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段1：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay4_Time1_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay4_Time1_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay4_Time1_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay4_Time1_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay4_Time1_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay4_Time1_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段2：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay4_Time2_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay4_Time2_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay4_Time2_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay4_Time2_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay4_Time2_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay4_Time2_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段3：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay4_Time3_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay4_Time3_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay4_Time3_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay4_Time3_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay4_Time3_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay4_Time3_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                        <asp:CheckBox ClientIDMode="Static" ID="SeatSelectAdModeDay5" runat="server" Text="周五" />
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段1：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay5_Time1_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay5_Time1_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay5_Time1_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay5_Time1_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay5_Time1_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay5_Time1_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段2：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay5_Time2_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay5_Time2_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay5_Time2_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay5_Time2_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay5_Time2_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay5_Time2_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段3：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay5_Time3_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay5_Time3_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay5_Time3_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay5_Time3_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay5_Time3_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay5_Time3_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                        <asp:CheckBox ClientIDMode="Static" ID="SeatSelectAdModeDay6" runat="server" Text="周六" />
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段1：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay6_Time1_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay6_Time1_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay6_Time1_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay6_Time1_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay6_Time1_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay6_Time1_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段2：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay6_Time2_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay6_Time2_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay6_Time2_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay6_Time2_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay6_Time2_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay6_Time2_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段3：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay6_Time3_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay6_Time3_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay6_Time3_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay6_Time3_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay6_Time3_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay6_Time3_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                        <asp:CheckBox ClientIDMode="Static" ID="SeatSelectAdModeDay0" runat="server" Text="周日" />
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段1：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay0_Time1_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay0_Time1_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay0_Time1_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay0_Time1_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay0_Time1_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay0_Time1_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段2：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay0_Time2_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay0_Time2_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay0_Time2_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay0_Time2_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay0_Time2_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay0_Time2_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="middle">
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        时间段3：
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay0_Time3_StartH" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay0_Time3_StartM" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay0_Time3_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatSelectAdModeDay0_Time3_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <asp:RadioButtonList Width="" ID="SeatSelectAdModeDay0_Time3_SelectMode" runat="server"
                                            RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Text="手动选座" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="自动选座" Value="0"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="自选选座" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle" valign="middle">
                                        <span id="SeatSelectAdModeDay0_Time3_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ext:ContentPanel>
            <ext:ContentPanel ID="FormShortLeave" BoxConfigAlign="Center" runat="server" BodyPadding="3px"
                ShowHeader="true" Title="临时离开设置" EnableBackgroundColor="true" ClientIDMode="Static">
                <table width="90%">
                    <tr>
                        <td class="borderStyle" width="150">
                            <span>座位保留时长：</span>
                        </td>
                        <td class="borderStyle">
                            <asp:TextBox Height="13px" ID="ShortLeaveDufaultTime" runat="server" Width="30px"
                                ClientIDMode="Static"></asp:TextBox>
                            分钟&nbsp;&nbsp;&nbsp;启用高级设置
                            <input type="checkbox" runat="server" id="ShortLeaveAdMode" onclick="CBcheck('ShortLeaveAdMode')" />
                            <span id="ShortLeaveDufaultTime_Error" title="" style="color: Red;"></span>
                        </td>
                    </tr>
                </table>
                <table id="ShortLeaveAdModeTable" runat="server" width="90%" style="display: none">
                    <tr>
                        <td width="100">
                        </td>
                        <td>
                            <table width="520" cellspacing="0" cellpadding="5" border="1" class="borderStyle">
                                <tr>
                                    <td class="borderStyle" style="font-weight: bold;">
                                        计划
                                    </td>
                                    <td class="borderStyle" width="200px" style="font-weight: bold;">
                                        时间段
                                    </td>
                                    <td class="borderStyle" width="120px" style="font-weight: bold;">
                                        保留时长
                                    </td>
                                    <td class="borderStyle" width="80px" style="font-weight: bold;">
                                    </td>
                                </tr>
                                <tr height="">
                                    <td class="borderStyle" valign="top">
                                        <asp:CheckBox ClientIDMode="Static" ID="ShortLeaveAdMode_Time1" runat="server" Text="时间段1" />
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <asp:TextBox Height="13px" ID="ShortLeaveAdMode_Time1_StartH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="ShortLeaveAdMode_Time1_StartM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="ShortLeaveAdMode_Time1_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="ShortLeaveAdMode_Time1_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <asp:TextBox Height="13px" ID="ShortLeaveAdMode_Time1_LeaveTime" runat="server" Width="30px"
                                            ClientIDMode="Static"></asp:TextBox>
                                        分钟
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <span id="ShortLeaveAdMode_Time1_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr height="">
                                    <td class="borderStyle" valign="top">
                                        <asp:CheckBox ClientIDMode="Static" ID="ShortLeaveAdMode_Time2" runat="server" Text="时间段2" />
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <asp:TextBox Height="13px" ID="ShortLeaveAdMode_Time2_StartH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="ShortLeaveAdMode_Time2_StartM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="ShortLeaveAdMode_Time2_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="ShortLeaveAdMode_Time2_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <asp:TextBox Height="13px" ID="ShortLeaveAdMode_Time2_LeaveTime" runat="server" Width="30px"
                                            ClientIDMode="Static"></asp:TextBox>
                                        分钟
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <span id="ShortLeaveAdMode_Time2_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="90%">
                    <tr>
                        <td class="borderStyle" width="150">
                            <span>管理员暂离设置：</span>
                        </td>
                        <td class="borderStyle">
                            <asp:CheckBox ClientIDMode="Static" ID="ShortLeaveByAdmin" runat="server" Text="启用" />
                            座位保留时长
                            <asp:TextBox Height="13px" ID="ShortLeaveByAdmin_LeaveTime" runat="server" Width="30px"
                                ClientIDMode="Static"></asp:TextBox>
                            分钟 <span id="ShortLeaveByAdmin_Error" title="" style="color: Red;"></span>
                        </td>
                    </tr>
                </table>
            </ext:ContentPanel>
            <ext:ContentPanel ID="FormReadingRoomOC" BoxConfigAlign="Center" runat="server" BodyPadding="3px"
                ShowHeader="true" Title="开闭馆设置" EnableBackgroundColor="true" ClientIDMode="Static">
                <table width="90%">
                    <tr>
                        <td width="150" valign="top" class="borderStyle">
                            <span>开闭馆时间设置：</span>
                        </td>
                        <td>
                            <table>
                                <tr height="30" valign="middle">
                                    <td class="borderStyle" valign="top">
                                        开馆时间：
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <asp:TextBox Height="13px" ID="ReadingRoomDufaultOpenTimeH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="ReadingRoomDufaultOpenTimeM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        开馆预处理时长:
                                        <asp:TextBox Height="13px" ID="ReadingRoomBeforeOpenTime" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>分钟
                                    </td>
                                    <td>
                                        <span id="ReadingRoomDufaultOpenTime_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr height="30" valign="middle">
                                    <td class="borderStyle" valign="top">
                                        闭馆时间：
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <asp:TextBox Height="13px" ID="ReadingRoomDufaultCloseTimeH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="ReadingRoomDufaultCloseTimeM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        闭馆预处理时长:
                                        <asp:TextBox Height="13px" ID="ReadingRoomBeforeCloseTime" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>分钟
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <span id="ReadingRoomDufaultCloseTime_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        启用高级设置
                                        <input type="checkbox" id="ReadingRoomOpenCloseAdMode" runat="server" onclick="CBcheck('ReadingRoomOpenCloseAdMode')" />
                                    </td>
                                    <td colspan="2">
                                        启用24小时不间断模式
                                        <input type="checkbox" id="ReadingRoomOpen24H" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <table id="ReadingRoomOpenCloseAdModeTable" runat="server" width="90%" style="display: none">
                                <tr>
                                    <td class="borderStyle" valign="top" width="100">
                                    </td>
                                    <td class="borderStyle" valign="top" colspan="2">
                                        <table>
                                            <tr>
                                                <td class="borderStyle" style="font-weight: bold;">
                                                    计划日期
                                                </td>
                                                <td class="borderStyle" style="font-weight: bold;">
                                                    开馆时间
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="borderStyle" valign="top">
                                                    <asp:CheckBox ClientIDMode="Static" ID="ReadingRoomAdOpenTime_Day1" Text="周一" runat="server" />
                                                </td>
                                                <td class="borderStyle" valign="middle">
                                                    时段1：<br />
                                                    时段2：<br />
                                                    时段3：
                                                </td>
                                                <td class="borderStyle" valign="top">
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day1_Time1_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day1_Time1_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day1_Time1_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day1_Time1_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox><br />
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day1_Time2_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day1_Time2_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day1_Time2_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day1_Time2_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox><br />
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day1_Time3_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day1_Time3_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day1_Time3_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day1_Time3_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                </td>
                                                <td class="borderStyle" valign="top">
                                                    <span id="ReadingRoomAdOpenTime_Day1_Time1_Error" title="" style="color: Red;"></span>
                                                    <br />
                                                    <span id="ReadingRoomAdOpenTime_Day1_Time2_Error" title="" style="color: Red;"></span>
                                                    <br />
                                                    <span id="ReadingRoomAdOpenTime_Day1_Time3_Error" title="" style="color: Red;"></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="borderStyle" valign="top">
                                                    <asp:CheckBox ClientIDMode="Static" ID="ReadingRoomAdOpenTime_Day2" Text="周二" runat="server" />
                                                </td>
                                                <td class="borderStyle" valign="middle">
                                                    时段1：<br />
                                                    时段2：<br />
                                                    时段3：
                                                </td>
                                                <td class="borderStyle" valign="top">
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day2_Time1_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day2_Time1_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day2_Time1_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day2_Time1_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox><br />
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day2_Time2_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day2_Time2_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day2_Time2_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day2_Time2_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox><br />
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day2_Time3_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day2_Time3_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day2_Time3_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day2_Time3_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                </td>
                                                <td class="borderStyle" valign="top">
                                                    <span id="ReadingRoomAdOpenTime_Day2_Time1_Error" title="" style="color: Red;"></span>
                                                    <br />
                                                    <span id="ReadingRoomAdOpenTime_Day2_Time2_Error" title="" style="color: Red;"></span>
                                                    <br />
                                                    <span id="ReadingRoomAdOpenTime_Day2_Time3_Error" title="" style="color: Red;"></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="borderStyle" valign="top">
                                                    <asp:CheckBox ClientIDMode="Static" ID="ReadingRoomAdOpenTime_Day3" Text="周三" runat="server" />
                                                </td>
                                                <td class="borderStyle" valign="middle">
                                                    时段1：<br />
                                                    时段2：<br />
                                                    时段3：
                                                </td>
                                                <td class="borderStyle" valign="top">
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day3_Time1_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day3_Time1_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day3_Time1_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day3_Time1_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox><br />
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day3_Time2_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day3_Time2_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day3_Time2_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day3_Time2_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox><br />
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day3_Time3_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day3_Time3_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day3_Time3_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day3_Time3_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                </td>
                                                <td class="borderStyle" valign="top">
                                                    <span id="ReadingRoomAdOpenTime_Day3_Time1_Error" title="" style="color: Red;"></span>
                                                    <br />
                                                    <span id="ReadingRoomAdOpenTime_Day3_Time2_Error" title="" style="color: Red;"></span>
                                                    <br />
                                                    <span id="ReadingRoomAdOpenTime_Day3_Time3_Error" title="" style="color: Red;"></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="borderStyle" valign="top">
                                                    <asp:CheckBox ClientIDMode="Static" ID="ReadingRoomAdOpenTime_Day4" Text="周四" runat="server" />
                                                </td>
                                                <td class="borderStyle" valign="middle">
                                                    时段1：<br />
                                                    时段2：<br />
                                                    时段3：
                                                </td>
                                                <td class="borderStyle" valign="top">
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day4_Time1_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day4_Time1_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day4_Time1_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day4_Time1_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox><br />
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day4_Time2_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day4_Time2_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day4_Time2_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day4_Time2_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox><br />
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day4_Time3_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day4_Time3_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day4_Time3_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day4_Time3_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                </td>
                                                <td class="borderStyle" valign="top">
                                                    <span id="ReadingRoomAdOpenTime_Day4_Time1_Error" title="" style="color: Red;"></span>
                                                    <br />
                                                    <span id="ReadingRoomAdOpenTime_Day4_Time2_Error" title="" style="color: Red;"></span>
                                                    <br />
                                                    <span id="ReadingRoomAdOpenTime_Day4_Time3_Error" title="" style="color: Red;"></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="borderStyle" valign="top">
                                                    <asp:CheckBox ClientIDMode="Static" ID="ReadingRoomAdOpenTime_Day5" Text="周五" runat="server" />
                                                </td>
                                                <td class="borderStyle" valign="middle">
                                                    时段1：<br />
                                                    时段2：<br />
                                                    时段3：
                                                </td>
                                                <td class="borderStyle" valign="top">
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day5_Time1_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day5_Time1_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day5_Time1_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day5_Time1_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox><br />
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day5_Time2_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day5_Time2_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day5_Time2_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day5_Time2_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox><br />
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day5_Time3_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day5_Time3_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day5_Time3_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day5_Time3_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                </td>
                                                <td class="borderStyle" valign="top">
                                                    <span id="ReadingRoomAdOpenTime_Day5_Time1_Error" title="" style="color: Red;"></span>
                                                    <br />
                                                    <span id="ReadingRoomAdOpenTime_Day5_Time2_Error" title="" style="color: Red;"></span>
                                                    <br />
                                                    <span id="ReadingRoomAdOpenTime_Day5_Time3_Error" title="" style="color: Red;"></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="borderStyle" valign="top">
                                                    <asp:CheckBox ClientIDMode="Static" ID="ReadingRoomAdOpenTime_Day6" Text="周六" runat="server" />
                                                </td>
                                                <td class="borderStyle" valign="middle">
                                                    时段1：<br />
                                                    时段2：<br />
                                                    时段3：
                                                </td>
                                                <td class="borderStyle" valign="top">
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day6_Time1_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day6_Time1_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day6_Time1_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day6_Time1_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox><br />
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day6_Time2_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day6_Time2_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day6_Time2_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day6_Time2_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox><br />
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day6_Time3_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day6_Time3_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day6_Time3_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day6_Time3_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                </td>
                                                <td class="borderStyle" valign="top">
                                                    <span id="ReadingRoomAdOpenTime_Day6_Time1_Error" title="" style="color: Red;"></span>
                                                    <br />
                                                    <span id="ReadingRoomAdOpenTime_Day6_Time2_Error" title="" style="color: Red;"></span>
                                                    <br />
                                                    <span id="ReadingRoomAdOpenTime_Day6_Time3_Error" title="" style="color: Red;"></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="borderStyle" valign="top">
                                                    <asp:CheckBox ClientIDMode="Static" ID="ReadingRoomAdOpenTime_Day0" Text="周日" runat="server" />
                                                </td>
                                                <td class="borderStyle" valign="middle">
                                                    时段1：<br />
                                                    时段2：<br />
                                                    时段3：
                                                </td>
                                                <td class="borderStyle" valign="top">
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day0_Time1_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day0_Time1_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day0_Time1_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day0_Time1_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox><br />
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day0_Time2_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day0_Time2_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day0_Time2_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day0_Time2_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox><br />
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day0_Time3_OpenH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day0_Time3_OpenM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                    至
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day0_Time3_CloseH" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>:
                                                    <asp:TextBox Height="13px" ID="ReadingRoomAdOpenTime_Day0_Time3_CloseM" runat="server"
                                                        Width="25" ClientIDMode="Static"></asp:TextBox>
                                                </td>
                                                <td class="borderStyle" valign="top">
                                                    <span id="ReadingRoomAdOpenTime_Day0_Time1_Error" title="" style="color: Red;"></span>
                                                    <br />
                                                    <span id="ReadingRoomAdOpenTime_Day0_Time2_Error" title="" style="color: Red;"></span>
                                                    <br />
                                                    <span id="ReadingRoomAdOpenTime_Day0_Time3_Error" title="" style="color: Red;"></span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ext:ContentPanel>
            <ext:ContentPanel ID="SeatTimeSetting" BoxConfigAlign="Center" runat="server" BodyPadding="3px"
                ShowHeader="true" Title="在座限时模式设置" EnableBackgroundColor="true" ClientIDMode="Static">
                <table width="90%">
                    <tr>
                        <td valign="top" class="borderStyle" width="150">
                            <span>在座时长设置：</span>
                        </td>
                        <td>
                            <asp:CheckBox ClientIDMode="Static" ID="SeatTime" runat="server" Text="启用" />
                            <table width="420" cellspacing="0" cellpadding="5" border="1" class="borderStyle">
                                <tr>
                                    <td class="borderStyle" width="100px">
                                        限制时长模式：
                                    </td>
                                    <td class="borderStyle" colspan="2">
                                        <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ClientIDMode="Static"
                                            ID="SeatTime_Mode">
                                            <asp:ListItem Text="计算在座时长" Value="Free" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="指定固定时间" Value="Fixed"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="borderStyle">
                                        <span id="SeatTime_Mode_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" width="100px">
                                        座位使用时长：
                                    </td>
                                    <td class="borderStyle" colspan="2">
                                        <asp:TextBox Height="13px" ID="SeatTime_SeatTime" runat="server" Width="25" ClientIDMode="Static"></asp:TextBox>分钟
                                    </td>
                                    <td class="borderStyle">
                                        <span id="SeatTime_SeatTime_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" width="100px">
                                        固定时段：
                                    </td>
                                    <td>
                                        时段1：<asp:TextBox Height="13px" ID="SeatTime_TimeH_0" runat="server" Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatTime_TimeM_0" runat="server" Width="25" ClientIDMode="Static"></asp:TextBox><br />
                                        时段2：<asp:TextBox Height="13px" ID="SeatTime_TimeH_1" runat="server" Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatTime_TimeM_1" runat="server" Width="25" ClientIDMode="Static"></asp:TextBox><br />
                                        时段3：<asp:TextBox Height="13px" ID="SeatTime_TimeH_2" runat="server" Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatTime_TimeM_2" runat="server" Width="25" ClientIDMode="Static"></asp:TextBox><br />
                                        时段4：<asp:TextBox Height="13px" ID="SeatTime_TimeH_3" runat="server" Width="25" ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatTime_TimeM_3" runat="server" Width="25" ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle">
                                        <span id="SeatTime_TimeSpan_0_Error" title="" style="color: Red;"></span><span id="SeatTime_TimeSpan_1_Error"
                                            title="" style="color: Red;"></span><span id="SeatTime_TimeSpan_2_Error" title=""
                                                style="color: Red;"></span><span id="SeatTime_TimeSpan_3_Error" title="" style="color: Red;">
                                                </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle">
                                        超时处理方式：
                                    </td>
                                    <td class="borderStyle" width="120">
                                        <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ClientIDMode="Static"
                                            ID="SeatTime_OverTime_Mode">
                                            <asp:ListItem Text="暂离" Value="8" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="释放座位" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle">
                                        续时设置：
                                    </td>
                                    <td class="borderStyle" width="120">
                                        <asp:CheckBox ClientIDMode="Static" ID="SeatTime_ContinueTime" runat="server" Text="允许续时"
                                            Checked="true" />
                                    </td>
                                    <td class="borderStyle">
                                        <asp:CheckBox ClientIDMode="Static" ID="SeatTime_ContinueTime_WithBookSpan" runat="server"
                                            Text="有时段预约禁止续时" Checked="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle">
                                        续时时长：
                                    </td>
                                    <td colspan="2" class="borderStyle" valign="top">
                                        <asp:TextBox Height="13px" ID="SeatTime_ContinueTime_Time" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>分钟
                                    </td>
                                    <td class="borderStyle">
                                        <span id="SeatTime_ContinueTime_Time_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td title="0次为不限制续时次数" class="borderStyle" valign="top">
                                        续时次数：
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <asp:TextBox Height="13px" ID="SeatTime_ContinueTime_ContinueCount" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>次
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        提前时间：<asp:TextBox Height="13px" ID="SeatTime_ContinueTime_BeforeTime" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>分钟
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <span id="SeatTime_ContinueTime_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ext:ContentPanel>
            <ext:ContentPanel ID="cp1" BoxConfigAlign="Center" runat="server" BodyPadding="3px"
                ShowHeader="true" Title="预约网站设置" EnableBackgroundColor="true" ClientIDMode="Static">
                <table width="90%">
                    <tr>
                        <td class="borderStyle" valign="top" width="150px" title="读者通过预约网站可以进行的离开操作">
                            允许座位操作：
                        </td>
                        <td>
                            <asp:CheckBox ClientIDMode="Static" ID="ckbShortLeave" runat="server" Text="允许暂离此阅览室的座位" />
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150px">
                            &nbsp;
                        </td>
                        <td>
                            <asp:CheckBox ClientIDMode="Static" ID="ckbDelayTime" runat="server" Text="允许续时此阅览室的座位" />
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150px">
                            &nbsp;
                        </td>
                        <td>
                            <asp:CheckBox ClientIDMode="Static" ID="ckbLeave" runat="server" Text="允许释放座位此阅览室的座位" />
                        </td>
                    </tr>
                </table>
            </ext:ContentPanel>
            <ext:ContentPanel ID="ContentPanel4" BoxConfigAlign="Center" runat="server" BodyPadding="3px"
                ShowHeader="true" Title="预约功能设置" EnableBackgroundColor="true" ClientIDMode="Static">
                <table width="90%">
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                            <span>预约模式：</span>
                        </td>
                        <td class="borderStyle">
                            <asp:CheckBox ClientIDMode="Static" ID="SeatBook" runat="server" Text="启用座位预约（开馆预约）" />
                            <asp:CheckBox ClientIDMode="Static" ID="cbNowDayBook" runat="server" Text="启用当天及时预约" />
                            <asp:CheckBox ClientIDMode="Static" ID="cbSpecifiedBook" runat="server" Text="启用指定时间预约" />
                            <asp:CheckBox ClientIDMode="Static" ID="cbMuiteSpan" runat="server" Text="启用多时段预约" />
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                            <span>隔天预约设置：</span>
                        </td>
                        <td class="borderStyle">
                            <table width="500px" cellspacing="0" cellpadding="5" border="1" class="borderStyle">
                                <tr>
                                    <td class="borderStyle" valign="top">
                                        预约提前：
                                    </td>
                                    <td class="borderStyle" align="left">
                                        <asp:TextBox Height="13px" ID="SeatBook_BeforeBookDay" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>天 <span id="SeatBook_BeforeBookDay_Error" title=""
                                                style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" valign="top">
                                        开放预约时间：
                                    </td>
                                    <td class="borderStyle" valign="top" align="left">
                                        <asp:TextBox Height="13px" ID="SeatBook_BookTime_StartH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatBook_BookTime_StartM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                        至
                                        <asp:TextBox Height="13px" ID="SeatBook_BookTime_EndH" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>:
                                        <asp:TextBox Height="13px" ID="SeatBook_BookTime_EndM" runat="server" Width="25"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <span id="SeatBook_BookTime_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" title="设置不允许预约的日期，起始和结束日期用“~”分割，多个日期段间“;”分隔，请不要输入任何中文字符，">
                                        不开放预约的日期：
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <ext:TextBox ID="SeatBook_CanNotSeatBookDate" Width="300px" runat="server" ShowLabel="false"
                                            ClientIDMode="Static" Height="20" EmptyText="示例:01-01~02-02;03-03~04-04">
                                        </ext:TextBox>
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <span id="SeatBook_CanNotSeatBookDate_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                            <span>预约时间段设置：</span>
                        </td>
                        <td class="borderStyle">
                            <table width="500px" cellspacing="0" cellpadding="5" border="1" class="borderStyle">
                                <tr>
                                    <td>
                                        <asp:CheckBox ClientIDMode="Static" ID="SeatBook_SpecifiedTime" runat="server" Text="启用指定时间段" />
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <span id="SeatBook_SpecifiedTime_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderStyle" title="设置指定预约的时段，多个时段间“;”分隔，请不要输入任何中文字符，">
                                        指定预约的时段：
                                    </td>
                                    <td class="borderStyle" valign="top">
                                        <ext:TextBox ID="SeatBook_SpecifiedTimeList" Width="300px" runat="server" ShowLabel="false"
                                            ClientIDMode="Static" Height="20" EmptyText="示例:8:00;10:00;12:00">
                                        </ext:TextBox>
                                        <span id="SeatBook_SpecifiedTimeList_Error" title="" style="color: Red;"></span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                            <span>预约范围设置：</span>
                        </td>
                        <td class="borderStyle" valign="top">
                            <table>
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="SeatBook_SeatBookRadioSetted" GroupName="SeatBespeak" runat="server"
                                            Text="指定可预约的座位" ClientIDMode="Static" />
                                    </td>
                                    <td>
                                        <ext:Button ID="btnSetBespeakSeat" runat="server" Text="指定预约座位" ClientIDMode="Static" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="SeatBook_SeatBookRadioPercent" GroupName="SeatBespeak" runat="server"
                                            Text="按照百分比预约座位" ClientIDMode="Static" />
                                        <asp:TextBox Height="13px" ID="SeatBook_SeatBookRadioPercent_Percent" runat="server"
                                            Width="25" ClientIDMode="Static"></asp:TextBox>%
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="borderStyle" valign="top">
                            <span id="SeatBook_SeatBookRadioPercent_Percent_Error" title="" style="color: Red;">
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                            <span>当天预约设置：</span>
                        </td>
                        <td class="borderStyle">
                            每天可预约座位<asp:TextBox Height="13px" ID="SeatBook_BespeakSeatCount" runat="server" Width="25"
                                ClientIDMode="Static"></asp:TextBox>次 <span id="SeatBook_BespeakSeatCount_Error"
                                    title="" style="color: Red;"></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                            &nbsp;
                        </td>
                        <td class="borderStyle">
                            <asp:CheckBox ClientIDMode="Static" ID="SeatBook_SelectBespeakSeat" runat="server"
                                Text="可以临时使用被预约的座位" />
                            <asp:CheckBox ClientIDMode="Static" ID="SeatBook_BespeakSeatOnSeat" runat="server"
                                Text="允许在座状态下预约座位" />
                            <span id="SeatBook_SelectBespeakSeat_Error" title="" style="color: Red;"></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" width="150">
                            预约签到设置：
                        </td>
                        <td class="borderStyle" valign="top">
                            可以提前
                            <asp:TextBox Height="13px" ID="SeatBook_SubmitBeforeTime" runat="server" Width="25"
                                ClientIDMode="Static"></asp:TextBox>分种， 延迟<asp:TextBox Height="13px" ID="SeatBook_SubmitLateTime"
                                    runat="server" Width="25" ClientIDMode="Static"></asp:TextBox>分钟签到（针对开馆预约和指定时间预约）
                        </td>
                        <td class="borderStyle" valign="top" width="80px">
                            <span id="SeatBook_SubmitTime_Error" title="" style="color: Red;"></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                            &nbsp
                        <td class="borderStyle">
                            及时预约座位保留时间：
                            <asp:TextBox Height="13px" ID="NowDayBookTime" runat="server" Width="25" ClientIDMode="Static"></asp:TextBox>&nbsp;分钟(预约完成后请在此时间内刷卡确认座位)
                            <span id="NowDayBookTime_Error" title="" style="color: Red;">
                        </td>
                    </tr>
                </table>
            </ext:ContentPanel>
            <ext:ContentPanel ID="ContentPanel2" BoxConfigAlign="Center" runat="server" BodyPadding="3px"
                ShowHeader="true" Title="黑名单设置" EnableBackgroundColor="true" ClientIDMode="Static">
                <table>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                            <span>是否记录本阅览室的违规：</span>
                        </td>
                        <td class="borderStyle" valign="top">
                            <asp:CheckBox ClientIDMode="Static" ID="IsRecordViolate" runat="server" Text="启用（在单独阅览室黑名单模式下强制选中）" />
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                            <span>是否限制黑名单读者进入：</span>
                        </td>
                        <td class="borderStyle" valign="top">
                            <asp:CheckBox ClientIDMode="Static" ID="UseBlacklist" runat="server" Text="启用（在单独阅览室黑名单模式下强制选中）" />
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                            <span>启用单独阅览室黑名单：</span>
                        </td>
                        <td class="borderStyle" valign="top">
                            <asp:CheckBox ClientIDMode="Static" ID="UseBlacklistSetting" runat="server" Text="启用"
                                onclick="BLCBcheck('UseBlacklistSetting')" />
                        </td>
                    </tr>
                </table>
                <table width="90%" id="UseBlacklistSettingTable" runat="server" style="display: none">
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                        </td>
                        <td class="borderStyle" valign="top" width="180">
                            进入黑名单的违规次数（次）：
                        </td>
                        <td class="borderStyle" valign="top">
                            <ext:TextBox ID="RecordViolateCount" Width="30px" runat="server" ShowLabel="false"
                                ClientIDMode="Static" Height="20">
                            </ext:TextBox>
                        </td>
                        <td>
                            <span id="RecordViolateCount_Error" title="" style="color: Red;"></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                        </td>
                        <td class="borderStyle" valign="top" width="180">
                            离开黑名单方式
                        </td>
                        <td class="borderStyle" valign="top">
                            <asp:RadioButton ID="AutoLeave" GroupName="leaveblacklist" runat="server" Text="自动离开"
                                ClientIDMode="Static" />
                            <asp:RadioButton ID="HardLeave" GroupName="leaveblacklist" runat="server" Text="手动离开"
                                ClientIDMode="Static" />
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                        </td>
                        <td class="borderStyle" valign="top" width="180">
                            离开黑名单天数（天）：
                        </td>
                        <td class="borderStyle" valign="top">
                            <ext:TextBox ID="LeaveBlackDays" Width="30px" runat="server" ShowLabel="false" ClientIDMode="Static"
                                Height="20">
                            </ext:TextBox>
                        </td>
                        <td>
                            <span id="LeaveBlackDays_Error" title="" style="color: Red;"></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                        </td>
                        <td class="borderStyle" valign="top" width="180">
                            违规记录失效天数（天）：
                        </td>
                        <td class="borderStyle" valign="top">
                            <ext:TextBox ID="LeaveRecordViolateDays" Width="30px" runat="server" ShowLabel="false"
                                ClientIDMode="Static" Height="20">
                            </ext:TextBox>
                        </td>
                        <td>
                            <span id="LeaveRecordViolateDays_Error" title="" style="color: Red;"></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                        </td>
                        <td class="borderStyle" valign="top" width="180">
                            启用违规类型：
                        </td>
                        <td class="borderStyle" valign="top">
                            <asp:CheckBox ClientIDMode="Static" ID="RecordViolate_LeaveByAdmin" runat="server"
                                Checked="true" Text="被管理员释放座位" />
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                        </td>
                        <td class="borderStyle" valign="top" width="180">
                        </td>
                        <td class="borderStyle" valign="top">
                            <asp:CheckBox ClientIDMode="Static" ID="RecordViolate_ShortLeaveByAdmin" runat="server"
                                Checked="true" Text="被管理员设置暂离，暂离超时" />
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                        </td>
                        <td class="borderStyle" valign="top" width="180">
                        </td>
                        <td class="borderStyle" valign="top">
                            <asp:CheckBox ClientIDMode="Static" ID="RecordViolate_SeatOverTime" runat="server"
                                Checked="true" Text="在座超时" />
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                        </td>
                        <td class="borderStyle" valign="top" width="180">
                        </td>
                        <td class="borderStyle" valign="top">
                            <asp:CheckBox ClientIDMode="Static" ID="RecordViolate_ShortLeaveOverTime" runat="server"
                                Checked="true" Text="暂离超时" />
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                        </td>
                        <td class="borderStyle" valign="top" width="180">
                        </td>
                        <td class="borderStyle" valign="top">
                            <asp:CheckBox ClientIDMode="Static" ID="RecordViolate_ShortLeaveByReader" runat="server"
                                Checked="true" Text="被读者设置暂离，暂离超时" />
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                        </td>
                        <td class="borderStyle" valign="top" width="180">
                        </td>
                        <td class="borderStyle" valign="top">
                            <asp:CheckBox ClientIDMode="Static" ID="RecordViolate_BookOverTime" runat="server"
                                Checked="true" Text="预约超时" />
                        </td>
                    </tr>
                </table>
            </ext:ContentPanel>
            <ext:ContentPanel ID="ContentPanel5" BoxConfigAlign="Center" runat="server" BodyPadding="3px"
                ShowHeader="true" Title="其他功能设置" EnableBackgroundColor="true" ClientIDMode="Static">
                <table>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                            <span>座位号显示长度：</span>
                        </td>
                        <td class="borderStyle" valign="top">
                            <ext:TextBox ID="ShowSeatNumberCount" Width="30px" runat="server" ShowLabel="false"
                                ClientIDMode="Static" Height="20">
                            </ext:TextBox>
                        </td>
                        <td class="borderStyle" valign="top">
                            <span id="ShowSeatNumberCount_Error" title="" style="color: Red;"></span>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                            <span>无人值守模式：</span>
                        </td>
                        <td class="borderStyle" valign="top">
                            <asp:CheckBox ClientIDMode="Static" ID="NoManMode" runat="server" Checked="true"
                                Text="启用" />
                            &nbsp;&nbsp; 无人值守模式操作间隔：
                            <asp:TextBox Height="13px" ID="NoManMode_WaitTime" runat="server" ClientIDMode="Static"
                                Width="30px"></asp:TextBox>分钟
                        </td>
                        <td>
                            <span id="NoManMode_WaitTime_Error" title="" style="color: Red;"></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                            <span>限制读者进入：</span>
                        </td>
                        <td class="borderStyle" valign="top">
                            <asp:CheckBox ClientIDMode="Static" ID="ReaderLimit" runat="server" Checked="true"
                                Text="启用" />
                            <asp:RadioButton ID="ReaderLimit_LimitMode_Writelist" GroupName="limitReader" runat="server"
                                Text="允许进入" />
                            <asp:RadioButton ID="ReaderLimit_LimitMode_Blacklist" GroupName="limitReader" runat="server"
                                Text="禁止进入" />
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                        </td>
                        <td class="borderStyle" valign="top">
                            读者类型：
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                        </td>
                        <td>
                            <asp:CheckBoxList ID="ReaderLimit_ReaderMode" ClientIDMode="Static" runat="server"
                                RepeatDirection="Horizontal" RepeatColumns="5">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </ext:ContentPanel>
            <ext:ContentPanel ID="ContentPanel1" BoxConfigAlign="Center" runat="server" BodyPadding="3px"
                ShowHeader="true" Title="应用到其它阅览室" EnableBackgroundColor="true" ClientIDMode="Static">
                <table>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                            <span>所要应用的阅览室：</span>
                        </td>
                        <td class="borderStyle" valign="top">
                            <asp:CheckBox ClientIDMode="Static" runat="server" ID="SelectAllRR" Text="选中全部阅览室"
                                onclick="CBcheckALL('SelectAllRR')" />
                        </td>
                    </tr>
                    <tr>
                        <td class="borderStyle" valign="top" width="150">
                        </td>
                        <td class="borderStyle" valign="top">
                            <asp:CheckBoxList ID="SameRoomSet" ClientIDMode="Static" runat="server" RepeatDirection="Horizontal"
                                RepeatColumns="4" RepeatLayout="Table">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td class="borderStyle" valign="top" align="center">
                            <asp:Button ID="Button1" runat="server" OnClick="Submit_OnClick" Text="保存设置" OnClientClick=" return SettingVerification()">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </ext:ContentPanel>
        </Items>
        <Items>
            <ext:Window ID="WindowEdit" Title="指定预约座位" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
                EnableMaximize="true" Target="Top" EnableResize="false" runat="server" OnClose="WindowEdit_Close"
                CloseAction="HidePostBack" EnableClose="true" IsModal="true" Width="1280px" EnableConfirmOnClose="true"
                Height="740px">
            </ext:Window>
        </Items>
    </ext:Panel>
    </form>
</body>
</html>
