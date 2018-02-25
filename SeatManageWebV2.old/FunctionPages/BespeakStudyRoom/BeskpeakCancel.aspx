<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BeskpeakCancel.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.BespeakStudyRoom.BeskpeakCancel" %>

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
        Title="取消申请">
        <Items>
            <ext:ContentPanel BoxConfigAlign="Center" runat="server" BodyPadding="3px" ShowHeader="false"
                Title="取消申请" EnableBackgroundColor="true" ClientIDMode="Static" ID="FormReadingRoomSet">
                <table width="100%">
                    <tr>
                        <td width="150px" valign="top" class="borderStyle">
                            <span>取消原因：</span>
                        </td>
                        <td align="left" width="300px">
                            <asp:TextBox Height="60px" ID="txtRemark" runat="server" Width="200" ClientIDMode="Static"
                                Wrap="true" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="borderStyle" valign="top" align="center">
                            <asp:Button ID="submit" runat="server" Text="提交" OnClick="Submit_OnClick"></asp:Button>
                        </td>
                    </tr>
                </table>
            </ext:ContentPanel>
        </Items>
    </ext:Panel>
    </form>
</body>
</html>
