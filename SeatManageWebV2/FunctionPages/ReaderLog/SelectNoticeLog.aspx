<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectNoticeLog.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.ReaderLog.SelectNoticeLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1" />
    <ext:Grid ID="NoticeGrid" Title="消息列表" AllowSorting="true" SortColumnIndex="0" SortDirection="DESC"
        PageSize="20" ShowBorder="true" ShowHeader="true" AutoHeight="true" AllowPaging="true"
        EnableRowNumber="true" runat="server" DataKeyNames="NoticeId" OnPageIndexChange="NoticeGrid_PageIndexChange"
        OnSort="NoticeGrid_Sort" OnPreRowDataBound="NoticeGrid_OnPreRowDataBound">
        <Columns>
            <ext:BoundField Width="0px" DataField="NoticeId" SortField="NoticeId" DataFormatString="{0}"
                Hidden="true" />
            <ext:BoundField Width="130px" DataField="AddTime" SortField="AddTime" DataFormatString="{0}"
                HeaderText="时间" />
            <ext:BoundField Width="650px" DataField="NoticeContent" SortField="NoticeContent"
                DataFormatString="{0}" HeaderText="内容" />
            <%--  <ext:LinkButtonField Width="75px" CommandName="ActionInfo" Icon="Zoom" ToolTip="详细"
                ColumnID="noticeContent" HeaderText="详细" TextAlign="Center" /> --%>
        </Columns>
    </ext:Grid>
    <ext:Window ID="WindowEdit" Title="消息内容" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="false" runat="server" IsModal="true"
        Width="670" EnableConfirmOnClose="true" Height="175">
    </ext:Window>
    </form>
</body>
</html>
