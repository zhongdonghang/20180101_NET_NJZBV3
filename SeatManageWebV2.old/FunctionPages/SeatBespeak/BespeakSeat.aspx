<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BespeakSeat.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.SeatBespeak.BespeakSeat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1" />
    <ext:Form LabelWidth="100px" EnableBackgroundColor="true" BodyPadding="5px" ID="Form2"
        runat="server" Title="查询条件">
        <Rows>
            <ext:FormRow ColumnWidths="30% 30% 40%">
                <Items>
                    <ext:DropDownList ID="ddlLibrary" Label="选择图书馆" runat="server">
                    </ext:DropDownList>
                    <ext:DatePicker runat="server" Required="true" Width="160" Label="预约日期" EmptyText="请选择预约的日期"
                        EnableEdit="false" Readonly="false" ID="dpStartDate" ShowRedStar="True" AutoPostBack="true"
                        EnableAjax="true">
                    </ext:DatePicker>
                    <ext:Button ID="btnnewdate" runat="server" CssClass="inline" OnClick="btnnewdate_OnClick"
                        Text="刷新列表">
                    </ext:Button>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:Grid ID="gridRoomList" Title="阅览室列表" AllowSorting="true" SortColumnIndex="0"
        EnableRowNumber="true" SortDirection="ASC" PageSize="20" ShowBorder="true" ShowHeader="true"
        AutoHeight="true" AllowPaging="true" runat="server" EnableCheckBoxSelect="False"
        DataKeyNames="LibraryNo" OnPageIndexChange="gridRoomList_PageIndexChange" OnSort="gridRoomList_Sort"
        OnPreRowDataBound="gridRoomList_OnPreRowDataBound">
        <Columns>
            <ext:BoundField Width="87px" DataField="roomNum" SortField="roomNum" DataFormatString="{0}"
                TextAlign="Center" HeaderText="阅览室编号" />
            <ext:BoundField Width="200px" DataField="roomName" SortField="roomName" DataFormatString="{0}"
                TextAlign="Left" HeaderText="阅览室名称" />
            <ext:BoundField Width="100px" DataField="libraryName" SortField="libraryName" DataFormatString="{0}"
                TextAlign="Center" HeaderText="所属图书馆" />
            <ext:BoundField Width="100px" DataField="CanBespeakAmcount" SortField="CanBespeakAmcount"
                DataFormatString="{0}" TextAlign="Center" HeaderText="可预约座位总数" />
            <ext:BoundField Width="75px" DataField="SurplusBespeskAmcount" SortField="SurplusBespeskAmcount"
                TextAlign="Center" DataFormatString="{0}" HeaderText="剩余座位数" />
            <ext:LinkButtonField Width="60px" CommandName="ActionViewSeatLayout" Icon="Zoom"
                TextAlign="Center" ToolTip="预约座位" ColumnID="seatUsedView" HeaderText="预约" />
        </Columns>
    </ext:Grid>
    <ext:Window ID="WindowEdit" Title="预约座位" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="false" runat="server" CloseAction="HidePostBack"
        EnableClose="true" IsModal="true" Width="1280px" EnableConfirmOnClose="true"
        OnClose="WindowEdit_Close" Height="740px">
    </ext:Window>
    </form>
</body>
</html>
