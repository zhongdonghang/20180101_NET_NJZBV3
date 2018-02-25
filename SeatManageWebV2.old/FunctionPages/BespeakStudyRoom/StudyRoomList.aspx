<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudyRoomList.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.BespeakStudyRoom.StudyRoomList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1" />
    <ext:Grid ID="gridRoomList" Title="研习间列表" AllowSorting="true" SortColumnIndex="0"
        EnableRowNumber="true" SortDirection="ASC" PageSize="20" ShowBorder="true" ShowHeader="true"
        AutoHeight="true" AllowPaging="true" runat="server" EnableCheckBoxSelect="False"
        DataKeyNames="LibraryNo" OnPageIndexChange="gridRoomList_PageIndexChange" OnSort="gridRoomList_Sort"
        OnPreRowDataBound="gridRoomList_OnPreRowDataBound">
        <Columns>
            <ext:BoundField Width="87px" DataField="StudyRoomNo" SortField="StudyRoomNo" DataFormatString="{0}"
                TextAlign="Center" HeaderText="研习间编号" />
            <ext:BoundField Width="200px" DataField="StudyRoomName" SortField="StudyRoomName"
                DataFormatString="{0}" TextAlign="Left" HeaderText="研习间名称" />
            <ext:ImageField Width="170px" DataImageUrlField="StudyImage" HeaderText="研习间图片" SortField="StudyImage"
                ImageHeight="120px" ImageWidth="160px" />
            <ext:BoundField Hidden="true" Width="200px" DataField="Remark" SortField="Remark"
                DataFormatString="{0}" TextAlign="Left" HeaderText="备注" />
            <ext:TemplateField Width="500px" TextAlign="Left" HeaderText="备注">
                <ItemTemplate>
                    <div runat="server" id="divremark">
                    </div>
                </ItemTemplate>
            </ext:TemplateField>
            <ext:LinkButtonField ID="btnSubmitApply" Width="35px" CommandName="AppTable" IconUrl="/Images/Hand.png"
                TextAlign="Center" ToolTip="提交申请" ColumnID="appTable" HeaderText="申请"/>
        </Columns>
    </ext:Grid>
    <ext:Window ID="WindowEdit" Title="申请" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="false" runat="server" CloseAction="HidePostBack"
        EnableClose="true" IsModal="true" Width="750px" EnableConfirmOnClose="true" OnClose="WindowEdit_Close"
        Height="400px">
    </ext:Window>
    </form>
</body>
</html>
