<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BespeakSubmitWindow.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.SeatBespeak.BespeakSubmitWindow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="roomOpenTime" runat="server" />
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1"
        HideScrollbar="true" />
    <ext:Form AutoScroll="true" EnableBackgroundColor="true" BodyPadding="5px" LabelWidth="75px"
        ID="Form2" runat="server" Title="座位状态" ShowBorder="false" ShowHeader="false">
        <Rows>
            <ext:FormRow>
                <Items>
                    <ext:Label ID="lblRoomName" runat="server" Label="所在阅览室" Text="">
                    </ext:Label>
                </Items>
            </ext:FormRow>
            <ext:FormRow>
                <Items>
                    <ext:Label ID="lblSeatNo" runat="server" Label="座位号" Text="">
                    </ext:Label>
                </Items>
            </ext:FormRow>
            <ext:FormRow>
                <Items>
                    <ext:Label ID="lblbeginDate" runat="server" Label="预约日期" Text="">
                    </ext:Label>
                </Items>
            </ext:FormRow>
            <ext:FormRow>
                <Items>
                    <ext:RadioButtonList ID="rblModel" runat="server" AutoPostBack="true" ColumnNumber="2"
                        OnSelectedIndexChanged="rblModel_OnSelectedIndexChanged" Label="预约模式">
                        <ext:RadioItem Text="开馆预约" Value="0" Selected="true" />
                        <ext:RadioItem Text="指定时间" Value="1" />
                    </ext:RadioButtonList>
                </Items>
            </ext:FormRow>
            <ext:FormRow>
                <Items>
                    <ext:DropDownList ID="DropDownList_FreeTime" runat="server" AutoPostBack="true" Label="时间选择"
                        OnSelectedIndexChanged="DropDownList_FreeTime_OnSelectedIndexChanged" Width="70">
                    </ext:DropDownList>
                </Items>
            </ext:FormRow>
            <ext:FormRow>
                <Items>
                    <ext:DropDownList ID="DropDownList_Time" runat="server" AutoPostBack="true" Label="时段选择"
                        OnSelectedIndexChanged="DropDownList_Time_OnSelectedIndexChanged" Width="70">
                    </ext:DropDownList>
                </Items>
            </ext:FormRow>
            <ext:FormRow>
                <Items>
                    <ext:Label ID="lblEndDate" runat="server" Label="确认时间" Text="">
                    </ext:Label>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" Title="操作"
        ShowBorder="false">
        <table class="divOperate" width="100%" border="0" style="background: #DFE8F6;">
            <tr>
                <td align="right" style="width: 50%;">
                    <ext:Button ID="btnBespeak" runat="server" Text="确认预约" OnClick="btnBespeak_Click" />
                </td>
                <td align="left" style="width: 50%;">
                    <ext:Button ID="btnClose" runat="server" Text="重新选择" OnClick="btnClose_Click" />
                </td>
            </tr>
        </table>
    </ext:ContentPanel>
    </form>
</body>
</html>
