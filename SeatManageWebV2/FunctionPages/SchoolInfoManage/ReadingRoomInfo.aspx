<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReadingRoomInfo.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.SchoolInfoManage.ReadingRoomInfo" %>

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
    <ext:Form ID="Form2" runat="server" EnableBackgroundColor="false" ShowHeader="false"
        Width="300px" ShowBorder="false">
        <Rows>
            <ext:FormRow>
                <Items>
                    <ext:Button ID="btnAddReadRoom" Type="Button" Text="添加阅览室" runat="server" CssClass="inline">
                    </ext:Button>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:Grid ID="GridReadRoom" AllowSorting="true" SortColumnIndex="0" SortDirection="ASC"
        PageSize="20" ShowBorder="true" ShowHeader="true" AutoHeight="true" AllowPaging="true"
        runat="server" DataKeyNames="ReadingRoomNo,ReadingRoomName" Title="阅览室列表" AutoScroll="true"
        EnableBackgroundColor="true" EnableRowNumber="true" OnRowCommand="GridReadRoom_RowCommand"
        OnSort="GridReadRoom_Sort" OnPageIndexChange="GridReadRoom_PageIndexChange"  OnPreRowDataBound="GridReadRoom_OnPreRowDataBound">
        <Columns>
            <ext:BoundField Width="100px" DataField="ReadingRoomNo" DataFormatString="{0}" HeaderText="阅览室编号"
                SortField="ReadingRoomNo" />
            <ext:BoundField Width="200px" DataField="ReadingRoomName" DataFormatString="{0}"
                HeaderText="阅览室名称" SortField="ReadingRoomName" />
            <ext:BoundField Width="150px" DataField="LibraryName" DataFormatString="{0}" HeaderText="所属图书馆"
                SortField="LibraryName" />
            <ext:BoundField Width="150px" DataField="SchoolName" DataFormatString="{0}" HeaderText="所属校区"
                SortField="SchoolName" />
            <ext:WindowField Width="35px" WindowID="WindowEdit" Icon="Pencil" ToolTip="阅览室编辑"
                DataIFrameUrlFields="ReadingRoomNo,ReadingRoomName" DataIFrameUrlFormatString="ReadingRoomEdit.aspx?rrid={0}&flag=edit"
                Title="编辑" IFrameUrl="ReadingRoomEdit.aspx" HeaderText="编辑" TextAlign="Center" />
            <ext:WindowField Width="35px" WindowID="WindowSetting" Icon="Cog" ToolTip="阅览室设置"
                DataIFrameUrlFields="ReadingRoomNo,ReadingRoomName" DataIFrameUrlFormatString="ReadingRoomSetting.aspx?id={0}"
                Title="设置" IFrameUrl="ReadingRoomSetting.aspx" HeaderText="设置" TextAlign="Center" />
            <ext:LinkButtonField Width="35px" CommandName="ActionDelete" Icon="Delete"
                ConfirmTarget="Top" ColumnID="ReadingRoomdelete" ToolTip="删除阅览室" HeaderText="删除" />
        </Columns>
    </ext:Grid>
    <ext:Window ID="WindowEdit" Title="阅览室信息设置" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" runat="server" OnClose="WindowEdit_Close"
        IsModal="true" Width="330px" EnableConfirmOnClose="true" Height="210px" EnableResize="false">
    </ext:Window>
    <ext:Window ID="WindowSetting" Title="阅览室管理设置" Popup="false" EnableIFrame="true"
        IFrameUrl="about:blank" EnableMaximize="true" Target="Top" runat="server" OnClose="WindowSetting_Close"
        IsModal="true" Width="950px" EnableConfirmOnClose="true" Height="550px" EnableResize="false">
    </ext:Window>
    <ext:Window ID="WindowDelete" Title="阅览室删除" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="false" runat="server" OnClose="WindowEdit_Close"
        IsModal="true" Width="400" EnableConfirmOnClose="true" Height="180px">
    </ext:Window>
    </form>
</body>
</html>
