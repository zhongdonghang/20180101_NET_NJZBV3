<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudyBookingLog.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.BespeakStudyRoom.StudyBookingLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1" />
    <ext:Form LabelWidth="100px" EnableBackgroundColor="true" BodyPadding="5px" ID="Form2"
        runat="server" Title="查询条件">
        <Rows>
            <ext:FormRow ColumnWidths="25% 75%">
                <Items>
                    <ext:DatePicker runat="server" Required="true" Width="150px" Label="开始日期" EmptyText="请选择开始日期"
                        Readonly="false" ID="dpStartDate" ShowRedStar="True">
                    </ext:DatePicker>
                    <ext:DatePicker ID="dpEndDate" Required="true" Width="150px" Readonly="false" CompareControl="dpStartDate"
                        DateFormatString="yyyy-MM-dd" EmptyText="请选择结束日期" CompareOperator="GreaterThanEqual"
                        CompareMessage="结束日期应该大于开始日期" Label="结束日期" runat="server" ShowRedStar="True">
                    </ext:DatePicker>
                </Items>
            </ext:FormRow>
            <ext:FormRow ColumnWidths="25% 75%">
                <Items>
                    <ext:DropDownList ID="ddlStudyState" runat="server" Width="150px" Label="审核状态">
                        <ext:ListItem Text="查询全部" Value="-1" Selected="true" />
                        <ext:ListItem Text="审核中" Value="0" />
                        <ext:ListItem Text="通过审核" Value="1" />
                        <ext:ListItem Text="等待审核" Value="2" />
                        <ext:ListItem Text="取消审核" Value="3" />
                    </ext:DropDownList>
                    <ext:Button runat="server" ID="btnSubmit" CssClass="inline" Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;"
                        AjaxLoadingType="Mask" ValidateForms="Form2" OnClick="btnSubmit_OnClick">
                    </ext:Button>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:Grid ID="gridStudyLog" Title="预约记录查询" AllowSorting="true" SortColumnIndex="4"
        AutoScroll="true" SortDirection="DESC" PageSize="20" ShowBorder="true" ShowHeader="true"
        AutoHeight="true" AllowPaging="true" runat="server" EnableRowNumber="true" DataKeyNames="StudyID"
        OnPageIndexChange="gridStudyLog_PageIndexChange" OnPreRowDataBound="gridStudyLog_OnPreRowDataBound"
        OnRowCommand="gridStudyLog_RowCommand" OnSort="gridStudyLog_Sort">
        <Columns>
            <ext:BoundField Width="0px" DataField="StudyID" SortField="StudyID" DataFormatString="{0}"
                Hidden="true" />
            <ext:BoundField Width="150px" DataField="StudyRoomName" SortField="StudyRoomName"
                DataFormatString="{0}" HeaderText="申请的研习间" />
            <ext:BoundField Width="100px" DataField="StudyRoomNo" SortField="StudyRoomNo" DataFormatString="{0}"
                HeaderText="研习间编号" />
            <ext:BoundField Width="150px" DataField="MeetingName" SortField="MeetingName" DataFormatString="{0}"
                HeaderText="会议名称" />
            <ext:BoundField Width="120px" DataField="SubmitTime" SortField="SubmitTime" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                HeaderText="提交时间" />
            <ext:BoundField Width="120px" DataField="BespeakTime" SortField="BespeakTime" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                HeaderText="预约时间" />
            <ext:BoundField Width="0px" DataField="CheckTime" SortField="CheckTime" DataFormatString="{0:yy/MM/dd HH:mm}"
                HeaderText="操作时间" />
            <ext:BoundField Width="65px" DataField="CheckStatus" SortField="CheckStatus" DataFormatString="{0}"
                HeaderText="记录状态" />
            <ext:BoundField Width="100px" DataField="Remark" DataFormatString="{0}" HeaderText="操作信息" />
            <ext:LinkButtonField Width="40px" CommandName="ActionUpdataStudyLog" Icon="Pencil"
                TextAlign="Center" ToolTip="修改申请" ColumnID="StudyUpdate" HeaderText="修改" />

            <ext:LinkButtonField Width="40px" CommandName="ActionDeleteStudyLog" Icon="Delete"
                TextAlign="Center" ToolTip="取消申请" ColumnID="StudyDelete"
                HeaderText="取消" />
        </Columns>
    </ext:Grid>
    <ext:Window ID="WindowEdit" Title="申请" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="false" runat="server" CloseAction="HidePostBack"
        EnableClose="true" IsModal="true" Width="750px" EnableConfirmOnClose="true"
        OnClose="WindowEdit_Close" Height="450px">
    </ext:Window>
        <ext:Window ID="WindowCancel" Title="取消申请" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="false" runat="server" CloseAction="HidePostBack"
        EnableClose="true" IsModal="true" Width="400px" EnableConfirmOnClose="true"
        OnClose="WindowEdit_Close" Height="170px">
    </ext:Window>
    </form>
</body>
</html>
