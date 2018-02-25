<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BespeakSeatSettingWindow.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.SchoolInfoManage.BespeakSeatSettingWindow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1"
        HideScrollbar="true" />
    <ext:Form AutoScroll="true" EnableBackgroundColor="true" BodyPadding="5px" LabelWidth="85px"
        ID="Form2" runat="server" Title="座位状态" ShowBorder="false" ShowHeader="false">
        <Rows>
            <ext:FormRow>
                <Items>
                    <%--<ext:Label ID="lblRoomName" runat="server" Label="所在阅览室" Text="">
                    </ext:Label>--%>
                     <ext:Label ID="Label1" runat="server" Label="" LabelSeparator="false" Text="请稍等...">
                    </ext:Label>
                </Items>
            </ext:FormRow>
<%--            <ext:FormRow>
                <Items>
                    <ext:Label ID="lblSeatNo" runat="server" Label="座位号" Text="">
                    </ext:Label>
                </Items>
            </ext:FormRow>
            <ext:FormRow>
                <Items>
                    <ext:RadioButtonList ID="IsBespeakSeatSelect" Label="是否提供预约" runat="server" AutoPostBack="true" OnSelectedIndexChanged="IsBespeakSeatSelect_SelectedIndexChanged"
>
                        <ext:RadioItem Text="提供" Value="1" />
                        <ext:RadioItem Text="不提供" Value="0" />
                    </ext:RadioButtonList>
                </Items>
            </ext:FormRow>--%>
        </Rows>
    </ext:Form>
    <%--    <ext:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" Title="操作"
        ShowBorder="false">
        <table class="divOperate" width="100%" border="0" style="background: #DFE8F6;">
            <tr>
                <td align="right" style="width: 50%;">
                    <ext:Button ID="btnBespeak" runat="server" Text="保存设定" OnClick="btnCanBespeak_Click" />
                </td>
                <td align="left" style="width: 50%;">
                    <ext:Button ID="btnClose" runat="server" Text="重新选择" OnClick="btnClose_Click" />
                </td>
            </tr>
        </table>
    </ext:ContentPanel>--%>
    </form>
</body>
</html>
