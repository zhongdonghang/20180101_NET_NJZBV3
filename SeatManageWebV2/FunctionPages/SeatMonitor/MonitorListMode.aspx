<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonitorListMode.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.SeatMonitor.MonitorListMode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title> 
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1"
        HideScrollbar="true" />
    <ext:Grid ID="gridReaderList" Title="在座读者列表" AllowSorting="true" SortColumnIndex="0"
        EnableRowNumber="true" SortDirection="ASC" PageSize="30" ShowBorder="true" ShowHeader="true" Height="530px"
        AutoHeight="true" AllowPaging="true" runat="server" EnableCheckBoxSelect="False" AutoScroll="true"  
        DataKeyNames="LibraryNo" OnPageIndexChange="gridReaderList_PageIndexChange" OnSort="gridReaderList_Sort"
        OnPreRowDataBound="gridReaderList_OnPreRowDataBound" >
        <Columns> 
            <ext:BoundField Width="87px" DataField="CardNo" SortField="CardNo" DataFormatString="{0}"
                TextAlign="Center" HeaderText="学号" />
            <ext:BoundField Width="100px" DataField="ReaderName" SortField="ReaderName" DataFormatString="{0}"
                TextAlign="Center" HeaderText="姓名" />
            <ext:BoundField Width="150px" DataField="ReadingRoomName" SortField="ReadingRoomName" DataFormatString="{0}"
                TextAlign="Center" HeaderText="阅览室名称" />
            <ext:BoundField Width="60px" DataField="SeatShortNum" SortField="SeatShortNum" DataFormatString="{0}"
                TextAlign="Center" HeaderText="座位号" />
                <ext:BoundField Width="50px" DataField="Status" SortField="Status"  DataFormatString="{0}" HeaderText="状态" />
            <ext:BoundField Width="118px" DataField="EnterOutTime" SortField="EnterOutTime"
                TextAlign="Center"  DataFormatString="{0:yy/MM/dd HH:mm:ss}" HeaderText="时间" />
            <ext:BoundField Width="340px" DataField="Remark" SortField="Remark"
                TextAlign="Left" DataFormatString="{0}" HeaderText="备注" /> 
            <ext:LinkButtonField Width="35px" Icon="BulletWrench" TextAlign="Center" ToolTip="操作"
                ColumnID="seatReaderList" HeaderText="操作" />
        </Columns>
    </ext:Grid>
    <ext:Window ID="WindowEdit" Title="在座读者" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Self" EnableResize="false" runat="server" OnClose="WindowEdit_Close" CloseAction="HidePostBack"
        IsModal="true" Width="420px" EnableConfirmOnClose="true" Height="385px">
    </ext:Window>
    </form>
</body>
</html>
