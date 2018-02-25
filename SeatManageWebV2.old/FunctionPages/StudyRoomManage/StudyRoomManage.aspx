<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudyRoomManage.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.StudyRoomManage.StudyRoomManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                    <ext:Button ID="btnAddStudyRoom" Type="Button" Text="添加研习间" runat="server" CssClass="inline">
                    </ext:Button>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:Grid ID="GridStudyRoom" AllowSorting="true" SortColumnIndex="0" SortDirection="ASC"
        PageSize="20" ShowBorder="true" ShowHeader="true" AutoHeight="true" AllowPaging="true"
        runat="server" DataKeyNames="StudyRoomNo,StudyRoomName" Title="研习间列表" AutoScroll="true"
        EnableBackgroundColor="true" EnableRowNumber="true" OnRowCommand="GridStudyRoom_RowCommand"
        OnSort="GridStudyRoom_Sort" OnPageIndexChange="GridStudyRoom_PageIndexChange"
        OnPreRowDataBound="GridStudyRoom_OnPreRowDataBound">
        <Columns>
            <ext:BoundField Width="100px" DataField="StudyRoomNo" DataFormatString="{0}" HeaderText="研习间编号"
                SortField="StudyRoomNo" />
            <ext:BoundField Width="200px" DataField="StudyRoomName" DataFormatString="{0}" HeaderText="研习间名称"
                SortField="StudyRoomName" />
            <ext:BoundField Width="200px" DataField="Remark" DataFormatString="{0}" HeaderText="备注"
                SortField="Remark" />
            <ext:WindowField Width="35px" WindowID="WindowEdit" Icon="Pencil" ToolTip="研习间编辑"
                DataIFrameUrlFields="StudyRoomNo,StudyRoomName" DataIFrameUrlFormatString="StudyRoomEdit.aspx?srid={0}&flag=edit"
                Title="研习间编辑" IFrameUrl="ReadingRoomEdit.aspx" HeaderText="编辑" TextAlign="Center" />
            <ext:WindowField Width="35px" WindowID="WindowSetting" Icon="Cog" ToolTip="研习间设置"
                DataIFrameUrlFields="StudyRoomNo,StudyRoomName" DataIFrameUrlFormatString="StudyRoomSetting.aspx?no={0}"
                Title="研习间设置" IFrameUrl="StudyRoomSetting.aspx" HeaderText="设置" TextAlign="Center" />
            <ext:LinkButtonField Width="35px" CommandName="ActionDelete" Icon="Delete" ConfirmTarget="Top"
                ColumnID="ReadingRoomdelete" ToolTip="删除研习间" HeaderText="删除" ConfirmText="确认删除次研习间吗？" />
        </Columns>
    </ext:Grid>
    <ext:Window ID="WindowEdit" Title="研习间信息设置" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" runat="server" OnClose="WindowEdit_Close"
        IsModal="true" Width="330px" EnableConfirmOnClose="true" Height="400px" EnableResize="false">
    </ext:Window>
    <ext:Window ID="WindowSetting" Title="研习间管理设置" Popup="false" EnableIFrame="true"
        IFrameUrl="about:blank" EnableMaximize="true" Target="Top" runat="server" OnClose="WindowSetting_Close"
        IsModal="true" Width="750px" EnableConfirmOnClose="true" Height="350px" EnableResize="false">
    </ext:Window>
    </form>
</body>
</html>
