<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlacklistInfo.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.LogManage.BlacklistInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager1" runat="server" />
    <ext:Grid ID="VRGrid" Title="违规记录列表" AllowSorting="true" SortColumnIndex="0" SortDirection="ASC"
        PageSize="5" ShowBorder="true" ShowHeader="true" AutoHeight="true" AllowPaging="true" Height="340" AutoScroll="true"
        OnPageIndexChange="VRGrid_PageIndexChange" OnSort="VRGrid_Sort" runat="server">
        <Columns>
            <ext:BoundField Width="80px" DataField="CardNo" SortField="CardNo" DataFormatString="{0}"
                HeaderText="读者学号" />
            <ext:BoundField Width="65px" DataField="ReaderName" SortField="ReaderName" DataFormatString="{0}"
                HeaderText="读者姓名" />
            <ext:BoundField Width="120px" DataField="AddTime" SortField="AddTime"  DataFormatString="{0:yy/MM/dd HH:mm:ss}"
                HeaderText="违规时间" />
            <ext:BoundField Width="150px" DataField="ReadingRoom" SortField="ReadingRoom" DataFormatString="{0}"
                HeaderText="违规阅览室" />
            <ext:BoundField Width="65px" DataField="Seat" SortField="Seat" DataFormatString="{0}"
                HeaderText="座位编号" />
            <ext:BoundField Width="600px" DataField="Remark" SortField="Remark" DataFormatString="{0}"
                HeaderText="备注" />
        </Columns>
    </ext:Grid>
    </form>
</body>
</html>
