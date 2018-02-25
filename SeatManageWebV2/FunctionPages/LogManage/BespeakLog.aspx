<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BespeakLog.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.LogManage.BespeakLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1" />
    <ext:Form LabelWidth="100" EnableBackgroundColor="true" BodyPadding="5px" ID="Form2"
        runat="server" Title="查询条件">
        <Rows>
            <ext:FormRow ColumnWidths="25% 25% 25% 25%">
                <Items>
                    <ext:TextBox runat="server" Width="150" ID="txtCardNo" EmptyText="为空则查询全部" Label="学号">
                    </ext:TextBox>
                    <ext:DropDownList ID="ddlReadingRoom" runat="server" Width="150" Required="true"
                        RequiredMessage="请选择阅览室" Label="阅览室" ShowLabel="True">
                    </ext:DropDownList>
                    <ext:DropDownList ID="ddlBespeakState" runat="server" Width="150" Label="预约状态">
                    </ext:DropDownList>
                    <ext:Label runat="server">
                    </ext:Label>
                </Items>
            </ext:FormRow>
            <ext:FormRow ColumnWidths="25% 25% 8% 42%">
                <Items>
                    <ext:DatePicker runat="server" Required="true" Width="150" Label="开始日期" EmptyText="请选择开始日期"
                        Readonly="false" ID="dpStartDate" ShowRedStar="True">
                    </ext:DatePicker>
                    <ext:DatePicker ID="dpEndDate" Required="true" Width="150" Readonly="false" CompareControl="dpStartDate"
                        DateFormatString="yyyy-MM-dd" EmptyText="请选择结束日期" CompareOperator="GreaterThanEqual"
                        CompareMessage="结束日期应该大于开始日期" Label="结束日期" runat="server" ShowRedStar="True">
                    </ext:DatePicker>
                    <ext:Button runat="server" ID="btnSubmit" CssClass="inline" Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;"
                        AjaxLoadingType="Mask" ValidateForms="Form2" OnClick="btnSubmit_OnClick">
                    </ext:Button>
                    <ext:CheckBox ID="chkSearchMH" ShowLabel="false" Readonly="true" runat="server" Text="启用模糊查询">
                    </ext:CheckBox>
                    <%-- <ext:Label ID="Label1" runat="server">
                    </ext:Label>--%>
                </Items>
            </ext:FormRow>
            <%--  <ext:FormRow>
                        <Items>
                            <ext:Label  v  Text="&nbsp;&nbsp;&nbsp;&nbsp;阅览室提供60个可预约座位，从xx年xx月xx日至xx年xx月xx日已经被预约2405人次，最高每天预约座位数为34人次。 当前有34条有效预约记录。" runat ="server"></ext:Label>
                        </Items>
                    </ext:FormRow>--%>
        </Rows>
    </ext:Form>
    <ext:Grid ID="gridBespeakLog" Title="预约记录查询" AllowSorting="true" SortColumnIndex="6"
        AutoScroll="true" SortDirection="ASC" PageSize="20" ShowBorder="true" ShowHeader="true"
        AutoHeight="true" AllowPaging="true" runat="server" EnableCheckBoxSelect="false"
        EnableRowNumber="true" DataKeyNames="BespeakID" OnPageIndexChange="gridBespeakLog_PageIndexChange"
        OnPreRowDataBound="gridBespeakLog_RowDataBound" OnRowCommand="gridBespeakLog_RowCommand"
        OnSort="gridBespeakLog_Sort">
        <Columns>
            <ext:BoundField Width="0px" DataField="BespeakID" SortField="BespeakID" DataFormatString="{0}"
                Hidden="true" />
            <ext:BoundField Width="100px" DataField="CardNo" SortField="CardNo" DataFormatString="{0}"
                HeaderText="学号" />
            <ext:BoundField Width="75px" DataField="ReaderName" SortField="ReaderName" DataFormatString="{0}"
                HeaderText="姓名" />
            <ext:BoundField Width="150px" DataField="ReadingRoomName" SortField="ReadingRoomName"
                DataFormatString="{0}" HeaderText="所在阅览室" />
            <ext:BoundField Width="65px" DataField="SeatNum" SortField="SeatNum" DataFormatString="{0}"
                HeaderText="座位号" />
            <ext:BoundField Width="75px" DataField="BsepeakState" SortField="BsepeakState" DataFormatString="{0}"
                HeaderText="记录状态" />
            <ext:BoundField Width="120px" DataField="SubmitTime" SortField="SubmitTime" DataFormatString="{0:yy/MM/dd HH:mm:ss}"
                HeaderText="提交时间" />
            <ext:BoundField Width="120px" DataField="BespeakTime" SortField="BespeakTime" DataFormatString="{0:yy/MM/dd HH:mm:ss}"
                HeaderText="预约时间" />
            <ext:BoundField Width="120px" DataField="CancelTime" SortField="CancelTime" DataFormatString="{0:yy/MM/dd HH:mm:ss}"
                HeaderText="操作时间" />
            <ext:BoundField Width="300" DataField="Remark" DataFormatString="{0}" HeaderText="操作信息" />
            <ext:LinkButtonField Width="60px" CommandName="ActionDeleteBespeakLog" Icon="Delete"
                TextAlign="Center" ConfirmText="确定要取消该预约记录吗？" ToolTip="取消预约" ColumnID="bespeakOpreate"
                HeaderText="取消预约" />
        </Columns>
    </ext:Grid>
    </form>
</body>
</html>
