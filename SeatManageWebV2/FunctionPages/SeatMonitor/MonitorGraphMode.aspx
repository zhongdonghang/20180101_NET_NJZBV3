<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonitorGraphMode.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.SeatMonitor.MonitorGraphMode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SeatGraph.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/SeatGraphHandle.js">
         
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1"
        HideScrollbar="true" />
        <ext:Form ID="Form2" runat="server" EnableBackgroundColor="false" ShowHeader="false"
        Width="300px" ShowBorder="false">
        <Rows>
            <ext:FormRow>
                <Items>
                    <ext:Button ID="btnbinding" Type="Button" Text="刷新列表" runat="server" CssClass="inline" OnClick="btnbinding_OnClick">
                    </ext:Button>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:Grid ID="gridRoomList" Title="阅览室列表" AllowSorting="true" SortColumnIndex="0" EnableRowNumber="true"
        SortDirection="ASC" PageSize="20" ShowBorder="true" ShowHeader="true" AutoHeight="true" AutoScroll="true"
        AllowPaging="true" runat="server" EnableCheckBoxSelect="False" DataKeyNames="LibraryNo" Height="430px"
        OnPageIndexChange="gridRoomList_PageIndexChange" OnSort="gridRoomList_Sort" OnPreRowDataBound="gridRoomList_OnPreRowDataBound"
        OnRowCommand="gridRoomList_RowCommand">
        <Columns> 
            <ext:BoundField Width="87px" DataField="roomNum" SortField="roomNum" DataFormatString="{0}" TextAlign="Center"
                HeaderText="阅览室编号" />
            <ext:BoundField Width="150px" DataField="roomName" SortField="roomName" DataFormatString="{0}"  TextAlign="Center"
                HeaderText="阅览室名称" />
            <ext:BoundField Width="100px" DataField="libraryName" SortField="libraryName" DataFormatString="{0}"  TextAlign="Center"
                HeaderText="所属图书馆" />
            <ext:BoundField Width="60px" DataField="seatCountAll" SortField="seatCountAll" DataFormatString="{0}"  TextAlign="Center"
                HeaderText="座位总数" />
            <ext:BoundField Width="75px" DataField="seatCountUsed" SortField="seatCountUsed"  TextAlign="Center"
                DataFormatString="{0}" HeaderText="座位使用数" />
            <ext:BoundField Width="60px" DataField="seatCountShortLeave" SortField="seatCountShortLeave"  TextAlign="Center"
                DataFormatString="{0}" HeaderText="暂离人数" />
            <ext:LinkButtonField Width="60px" CommandName="ActionViewSeatLayout" Icon="Zoom"  TextAlign="Center"
                ToolTip="管理座位" ColumnID="seatUsedView" HeaderText="查看视图" />
                <ext:LinkButtonField Width="60px"   Icon="ApplicationViewList"  TextAlign="Center"
                ToolTip="在座读者" ColumnID="seatReaderList" HeaderText="在座读者" />
        </Columns>
    </ext:Grid>
    <ext:Window ID="WindowEdit" Title="座位视图" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="false" runat="server" OnClose="WindowEdit_Close" CloseAction="HidePostBack"  EnableClose="true"   
        IsModal="true" Width="1280px" EnableConfirmOnClose="true" Height="735px">
    </ext:Window>
    <ext:Window ID="windowSeatUsedList" Title="在座读者" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="false" runat="server" OnClose="WindowEdit_Close" CloseAction="HidePostBack"  EnableClose="true"   AutoScroll="true"
        IsModal="true" Width="1020px" EnableConfirmOnClose="true" Height="585px">
    </ext:Window>
    </form>
</body>
</html>
