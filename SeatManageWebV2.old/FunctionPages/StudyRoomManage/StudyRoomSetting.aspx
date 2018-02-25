<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudyRoomSetting.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.StudyRoomManage.StudyRoomSetting" %>

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
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1"
        HideScrollbar="true" />
    <ext:Panel ID="PanelSetting" ClientIDMode="Static" runat="server" EnableBackgroundColor="false"
        BodyPadding="1px" Height="320px" AutoScroll="true" ShowBorder="false" ShowHeader="false"
        Title="研习间设置">
        <Items>
            <ext:ContentPanel BoxConfigAlign="Center" runat="server" BodyPadding="3px" ShowHeader="false"
                Title="开放时间设置" EnableBackgroundColor="true" ClientIDMode="Static" ID="FormReadingRoomSet">
                <table width="100%">
                    <tr>
                        <td width="150" class="borderStyle">
                            <span>是否启用研习间：</span>
                        </td>
                        <td width="300">
                            <asp:CheckBox ClientIDMode="Static" ID="chkUseStudyRoom" runat="server" Text="启用"
                                onclick="" />
                        </td>
                    </tr>
                    <tr>
                        <td width="150" class="borderStyle">
                            <span>开放时间设置：</span>
                        </td>
                        <td width="300">
                            <asp:TextBox Height="13px" ID="txtOpenTime_H" runat="server" Width="25" ClientIDMode="Static"></asp:TextBox>:
                            <asp:TextBox Height="13px" ID="txtOpenTime_M" runat="server" Width="25" ClientIDMode="Static"></asp:TextBox>
                            至
                            <asp:TextBox Height="13px" ID="txtEndTime_H" runat="server" Width="25" ClientIDMode="Static"></asp:TextBox>:
                            <asp:TextBox Height="13px" ID="txtEndTime_M" runat="server" Width="25" ClientIDMode="Static"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="150px" class="borderStyle">
                            <span>最大使用时长：</span>
                        </td>
                        <td align="left" width="300px">
                            <asp:TextBox Height="13px" ID="txtMaxTime" runat="server" Width="35" ClientIDMode="Static"></asp:TextBox>分钟
                        </td>
                    </tr>
                    <tr>
                        <td width="150px" class="borderStyle">
                            <span>可使用设备</span>
                        </td>
                        <td align="left" width="300px">
                            <asp:TextBox Height="13px" ID="txtCanUse" runat="server" Width="500" ClientIDMode="Static"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="150px" valign="top" class="borderStyle">
                            <span>设置与设备描述：</span>
                        </td>
                        <td align="left" width="300px">
                            <asp:TextBox Height="48px" ID="txtFacilities" runat="server" Width="500" ClientIDMode="Static"
                                Wrap="true" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="150px" valign="top" class="borderStyle">
                            <span>注意事项：</span>
                        </td>
                        <td align="left" width="300px">
                            <asp:TextBox Height="48px" ID="txtPrecautions" runat="server" Width="500" ClientIDMode="Static"
                                Wrap="true" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="150px" valign="top" class="borderStyle">
                            <span>申请说明：</span>
                        </td>
                        <td align="left" width="300px">
                            <asp:TextBox Height="48px" ID="txtApplicationInfo" runat="server" Width="500" ClientIDMode="Static"
                                Wrap="true" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                    <tr>
                        <td colspan="2" class="borderStyle" valign="top" align="center">
                            <asp:Button ID="submit" runat="server" Text="保存设置" OnClick="Submit_OnClick"></asp:Button>
                        </td>
                    </tr>
                </table>
            </ext:ContentPanel>
        </Items>
    </ext:Panel>
    </form>
</body>
</html>
