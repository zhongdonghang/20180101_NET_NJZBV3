<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectBespeakLog.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.ReaderLog.SelectBespeakLog" %>

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
                    <ext:DropDownList ID="ddlBespeakState" runat="server" Width="150px" Label="预约状态">
                    </ext:DropDownList>
                    <ext:Button runat="server" ID="btnSubmit" CssClass="inline" Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;"
                        AjaxLoadingType="Mask" ValidateForms="Form2" OnClick="btnSubmit_OnClick">
                    </ext:Button>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:Grid ID="gridBespeakLog" Title="预约记录查询" AllowSorting="true" SortColumnIndex="0"
        AutoScroll="true" SortDirection="ASC" PageSize="20" ShowBorder="true" ShowHeader="true"
        AutoHeight="true" AllowPaging="true" runat="server" EnableRowNumber="true" DataKeyNames="BespeakID"
        OnPageIndexChange="gridBespeakLog_PageIndexChange" OnPreRowDataBound="gridBespeakLog_OnPreRowDataBound"
        OnRowCommand="gridBespeakLog_RowCommand" OnSort="gridBespeakLog_Sort">
        <Columns>
            <ext:BoundField Width="0px" DataField="BespeakID" SortField="BespeakID" DataFormatString="{0}"
                Hidden="true" />
<%--            <ext:BoundField Width="100px" DataField="CardNo" SortField="CardNo" DataFormatString="{0}"
                HeaderText="学号" />
            <ext:BoundField Width="50px" DataField="ReaderName" SortField="ReaderName" DataFormatString="{0}"
                HeaderText="姓名" />--%>
            <ext:BoundField Width="200px" DataField="ReadingRoomName" SortField="ReadingRoomName"
                DataFormatString="{0}" HeaderText="所在阅览室" />
            <ext:BoundField Width="55px" DataField="SeatNum" SortField="SeatNum" DataFormatString="{0}"
                HeaderText="座位号" />
            <ext:BoundField Width="65px" DataField="BsepeakState" SortField="BsepeakState" DataFormatString="{0}"
                HeaderText="记录状态" />
            <ext:BoundField Width="80px" DataField="SubmitTime" SortField="SubmitTime" DataFormatString="{0:MM/dd HH:mm}"
                HeaderText="提交时间" />
            <ext:BoundField Width="80px" DataField="BespeakTime" SortField="BespeakTime" DataFormatString="{0:MM/dd HH:mm}"
                HeaderText="预约时间" />
            <ext:BoundField Width="80px" DataField="CancelTime" SortField="CancelTime" DataFormatString="{0:MM/dd HH:mm}"
                HeaderText="操作时间" />
            <ext:BoundField Width="150px" DataField="Remark" DataFormatString="{0}" HeaderText="操作信息" />
            <ext:LinkButtonField Width="40px" CommandName="ActionDeleteBespeakLog" Icon="Delete"
                TextAlign="Center" ConfirmText="确定要取消该预约记录吗？" ToolTip="取消预约" ColumnID="bespeakOpreate"
                HeaderText="取消" />
        </Columns>
    </ext:Grid>
    </form>
</body>
</html>
