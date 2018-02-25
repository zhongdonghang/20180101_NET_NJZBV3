<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoveWebAppSetting.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.ClientManage.MoveWebAppSetting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>移动客户端设置</title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .borderStyle {
            border-collapse: collapse;
            border: 0px dotted #DFE8F6;
            border-width: 5px;
        }
    </style>
    <script type="text/javascript" language="javascript" src="../../Scripts/RoomSettingVerification.js">
    </script>
    <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.4.1.js">
    </script>
    <script>
        function CBCheck(id) {
            if ($("#" + id + "")[0].checked == true) {
                document.getElementById(id + "_Area").style.display = "inline";
            }
            else {
                document.getElementById(id + "_Area").style.display = "none";
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
            Title="移动终端配置">
            <Items>
                <ext:ContentPanel BoxConfigAlign="Center" runat="server" BodyPadding="3px" ShowHeader="false"
                    Title="移动终端配置" EnableBackgroundColor="true" ClientIDMode="Static" ID="FormReadingRoomSet">
                    <table width="450px" border="0">
                        <tr>
                            <td width="150px" class="borderStyle">
                                <span>预约功能设置：</span>
                            </td>
                            <td width="300px" class="borderStyle">
                                <input type="checkbox" id="BespeakSeat" runat="server" onclick="CBCheck('BespeakSeat')" />启用预约功能
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table id="BespeakSeat_Area" runat="server">
                                    <tr>
                                        <td width="150px" class="borderStyle">&nbsp;
                                        </td>
                                        <td width="100px" class="borderStyle">
                                            <input type="checkbox" id="BespeakNextDay" runat="server" />启用隔天预约
                                        </td>
                                        <td width="100px" class="borderStyle" id="nowDayTD" runat="server">
                                            <input type="checkbox" id="BespeakNowDay" runat="server" />启用当天预约
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="450px" border="0">
                        <tr>
                            <td width="150px" class="borderStyle">
                                <span>客户端操作配置：</span>
                            </td>
                            <td width="300px" class="borderStyle">
                                <input type="checkbox" id="ShortLeave" runat="server" />允许用户暂离&nbsp;
                                <input type="checkbox" id="Leave" runat="server" />允许预约释放座位
                            </td>
                        </tr>
                    </table>
                    <table width="450px" border="0" id="dcodeT" runat="server">
                        <tr>
                            <td width="150px" class="borderStyle">
                                <span>二维码功能：</span>
                            </td>
                            <td width="300px" class="borderStyle">
                                <input type="checkbox" id="Dcode" runat="server" onclick="CBCheck('Dcode')" />启用二维码功能
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table id="Dcode_Area" runat="server">
                                    <tr>
                                        <td width="150px" class="borderStyle">&nbsp;
                                        </td>
                                        <td width="100px" class="borderStyle">
                                            <input type="checkbox" id="DcodeShortLeave" runat="server" />允许座位暂离
                                        </td>
                                        <td width="100px" class="borderStyle">
                                            <input type="checkbox" id="DcodeLeave" runat="server" />允许释放座位
                                        </td>
                                    </tr>
                                    <tr id="selectSeatTR" runat="server">
                                        <td width="150px" class="borderStyle">&nbsp;
                                        </td>
                                        <td width="100px" class="borderStyle">
                                            <input type="checkbox" id="DcodeSelectSeat" runat="server" />允许选择座位
                                        </td>
                                        <td width="100px" class="borderStyle">
                                            <input type="checkbox" id="DcodeReselectSeat" runat="server" />允许更换座位
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="150px" class="borderStyle">&nbsp;
                                        </td>
                                        <td width="100px" class="borderStyle">
                                            <input type="checkbox" id="DcodeComeBack" runat="server" />允许暂离回来
                                        </td>
                                        <td width="100px" class="borderStyle">
                                            <input type="checkbox" id="DcodeCheck" runat="server" />允许预约签到
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="150px" class="borderStyle">&nbsp;
                                        </td>
                                        <td width="100px" class="borderStyle">
                                            <input type="checkbox" id="DcodeContnueTime" runat="server" />允许座位续时
                                        </td>
                                        <td width="100px" class="borderStyle" id="waitSeatTD" runat="server">
                                            <input type="checkbox" id="DcodeWaitSeat" runat="server" />允许等待座位
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="borderStyle" valign="top" align="center" width="450px">
                                <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" Text="保存设置"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </ext:ContentPanel>
            </Items>
        </ext:Panel>
    </form>
</body>
</html>
