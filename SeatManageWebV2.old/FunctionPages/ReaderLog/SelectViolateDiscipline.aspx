<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectViolateDiscipline.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.ReaderLog.SelectViolateDiscipline" %>

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
    <ext:Form EnableBackgroundColor="true" BodyPadding="0px" ID="Form2" runat="server"
        Title="查询条件" LabelWidth="100">
        <Rows>
            <ext:FormRow ColumnWidths="25% 25% 50%">
                <Items>
                    <ext:DropDownList ID="ddlReadingRoom" runat="server" Width="150" Label="阅览室" ShowLabel="True">
                    </ext:DropDownList>
                    <ext:DropDownList ID="ddlblacklist" runat="server" Width="150" Label="是否进入黑名单" ShowLabel="True">
                        <ext:ListItem Text="查询全部" Value="-1" />
                        <ext:ListItem Text="被记录黑名单" Value="1" />
                        <ext:ListItem Text="没有被记录黑名单" Value="0" />
                    </ext:DropDownList>
                    <ext:DropDownList ID="ddllogstatus" runat="server" Width="150" Label="记录状态" ShowLabel="True">
                        <ext:ListItem Text="查询全部" Value="-1" />
                        <ext:ListItem Text="有效记录" Value="1" />
                        <ext:ListItem Text="失效记录" Value="0" />
                    </ext:DropDownList>
                </Items>
            </ext:FormRow>
            <ext:FormRow ColumnWidths="25% 25% 50%">
                <Items>
                    <ext:DatePicker runat="server" Required="true" Width="150" Label="开始日期" EmptyText="请选择开始日期"
                        Readonly="false" ID="dpStartDate" ShowRedStar="True">
                    </ext:DatePicker>
                    <ext:DatePicker ID="dpEndDate" Required="true" Width="150" Readonly="false" CompareControl="dpStartDate"
                        DateFormatString="yyyy-MM-dd" EmptyText="请选择结束日期" CompareOperator="GreaterThanEqual"
                        CompareMessage="结束日期应该大于开始日期" Label="结束日期" runat="server" ShowRedStar="True">
                    </ext:DatePicker>
                    <ext:Button runat="server" ID="btnSubmit" ValidateTarget="Top" CssClass="inline"
                        Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" AjaxLoadingType="Mask" ValidateForms="Form2"
                        OnClick="btnSubmit_Click">
                    </ext:Button>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:Grid ID="VRGrid" Title="违规记录列表" AllowSorting="true" SortColumnIndex="0" SortDirection="ASC"
        PageSize="20" ShowBorder="true" ShowHeader="true" AutoHeight="true" AllowPaging="true"
        EnableRowNumber="true" runat="server" DataKeyNames="ID" OnPageIndexChange="VRGrid_PageIndexChange"
        OnSort="VRGrid_Sort" OnPreRowDataBound="VRGrid_OnPreRowDataBound" OnRowCommand="VRGrid_RowCommand">
        <Columns>
            <ext:BoundField Width="0px" DataField="ID" SortField="ID" DataFormatString="{0}"
                Hidden="true" />
            <ext:BoundField Width="150px" DataField="AddTime" SortField="AddTime" DataFormatString="{0}"
                HeaderText="违规时间" />
            <ext:BoundField Width="200px" DataField="ReadingRoom" SortField="ReadingRoom" DataFormatString="{0}"
                HeaderText="违规阅览室" />
            <ext:BoundField Width="100px" DataField="Seat" SortField="Seat" DataFormatString="{0}"
                HeaderText="座位编号" />
            <ext:BoundField Width="80px" DataField="LogStatus" SortField="LogStatus" DataFormatString="{0}"
                HeaderText="记录状态" />
            <ext:BoundField Width="100px" DataField="BlacklistStatus" SortField="BlacklistStatus"
                DataFormatString="{0}" HeaderText="黑名单状态" />
            <ext:BoundField Width="250px" DataField="Remark" SortField="Remark" DataFormatString="{0}"
                HeaderText="备注" />
        </Columns>
    </ext:Grid>
    </form>
</body>
</html>
