<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IntherfaceAuthorization.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.InterfaceAuthorization.IntherfaceAuthorization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>授权文件管理</title>
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
        function ClientFunBtn() {
            document.getElementById("upFunFileTR").style.visibility = "visible";
            document.getElementById("rupFunFileTR").style.visibility = "hidden";
        }
        function ClientAccessBtn() {
            document.getElementById("upAccessFileTR").style.visibility = "visible";
            document.getElementById("rupAccessFileTR").style.visibility = "hidden";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1"
            HideScrollbar="true" />
        <ext:Panel ID="PanelSetting" ClientIDMode="Static" runat="server" EnableBackgroundColor="false"
            BodyPadding="1px" Height="500px" AutoScroll="true" ShowBorder="false" ShowHeader="true"
            Title="授权文件配置">
            <Items>
                <ext:ContentPanel BoxConfigAlign="Center" runat="server" BodyPadding="3px" ShowHeader="false"
                    Title="移动终端配置" EnableBackgroundColor="true" ClientIDMode="Static" ID="FormReadingRoomSet">
                    <table width="650px" border="0">
                        <tr id="upFunFileTR" runat="server">
                            <td width="150px" class="borderStyle">
                                <span>功能授权配置：</span>
                            </td>
                            <td width="500px" class="borderStyle">功能授权文件选择&nbsp<asp:FileUpload ID="funFile" runat="server" />
                                <asp:Button ID="btn_funFileSave" runat="server" OnClick="btn_funFileSave_Click" Text="保存授权文件"></asp:Button>
                            </td>
                        </tr>
                        <tr id="rupFunFileTR" runat="server">
                            <td width="150px" class="borderStyle">
                                <span>功能授权配置：</span>
                            </td>
                            <td width="500px" class="borderStyle">
                                <input onclick="ClientFunBtn()" type="button" value="更改授权文件" />
                            </td>
                        </tr>
                        <tr>
                            <td width="150px" class="borderStyle" valign="top">
                                <span>已授权的功能：</span>
                            </td>
                            <td width="500px" class="borderStyle" id="funFileMsg" runat="server">暂无授权
                            </td>
                        </tr>
                    </table>
                    <table width="650px" border="0">
                        <tr id="upAccessFileTR" runat="server">
                            <td width="150px" class="borderStyle">
                                <span>接口授权配置：</span>
                            </td>
                            <td width="500px" class="borderStyle">接口授权文件选择&nbsp<asp:FileUpload ID="accessFile" runat="server" />
                                <asp:Button ID="btn_accessFileSave" runat="server" OnClick="btn_accessFileSave_Click" Text="保存授权文件"></asp:Button>
                            </td>
                        </tr>
                        <tr id="rupAccessFileTR" runat="server">
                            <td width="150px" class="borderStyle">
                                <span>功能授权配置：</span>
                            </td>
                            <td width="500px" class="borderStyle">
                                <input onclick="ClientAccessBtn()" type="button" value="更改授权文件" />
                            </td>
                        </tr>
                        <tr>
                            <td width="150px" class="borderStyle" valign="top">
                                <span>已授权的功能：</span>
                            </td>
                            <td width="500px" class="borderStyle" id="accessFileMsg" runat="server">暂无授权
                            </td>
                        </tr>
                    </table>
                </ext:ContentPanel>
            </Items>
        </ext:Panel>
    </form>
</body>
</html>
