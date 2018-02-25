<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectBlacklist.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.ReaderLog.SelectBlacklist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1" />
    <ext:Form EnableBackgroundColor="true" BodyPadding="5px" ID="Form2" runat="server"
        Title="查询条件" LabelAlign="Right" LabelWidth="100">
        <Rows>
            <ext:FormRow ColumnWidths="25% 75%">
                <Items>
                    <ext:DatePicker runat="server" Width="150" Label="开始日期" EmptyText="请选择开始日期" Readonly="false"
                        ID="dpStartDate">
                    </ext:DatePicker>
                    <ext:DatePicker ID="dpEndDate" Width="150" Readonly="false" DateFormatString="yyyy-MM-dd"
                        EmptyText="请选择结束日期" Label="结束日期" runat="server">
                    </ext:DatePicker>
                </Items>
            </ext:FormRow>
            <ext:FormRow ColumnWidths="25% 75%">
                <Items>
                    <ext:DropDownList ID="ddllogstatus" runat="server" Width="150" Label="记录状态" ShowLabel="True">
                        <ext:ListItem Text="查询全部" Value="-1" />
                        <ext:ListItem Text="处罚中黑名单" Value="1" />
                        <ext:ListItem Text="过期黑名单" Value="0" />
                    </ext:DropDownList>
                    <ext:Button runat="server" ID="btnSubmit" ValidateTarget="Top" CssClass="inline"
                        Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" AjaxLoadingType="Mask" ValidateForms="Form2"
                        OnClick="btnSubmit_Click">
                    </ext:Button>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:Grid ID="BlacklistGrid" Title="黑名单记录列表" AllowSorting="true" SortColumnIndex="0"
        SortDirection="ASC" PageSize="20" ShowBorder="true" ShowHeader="true" AutoHeight="true"
        AllowPaging="true" EnableRowNumber="true" runat="server" DataKeyNames="ID" OnPageIndexChange="BlacklistGrid_PageIndexChange"
        OnSort="BlacklistGrid_Sort" OnPreRowDataBound="BlacklistGrid_OnPreRowDataBound">
        <Columns>
            <ext:BoundField Width="0px" DataField="ID" SortField="ID" DataFormatString="{0}"
                Hidden="true" />
            <ext:BoundField Width="150px" DataField="AddTime" SortField="AddTime" DataFormatString="{0}"
                HeaderText="加入黑名单时间" TextAlign="Center" />
            <ext:BoundField Width="150px" DataField="LeaveTime" SortField="AddTime" DataFormatString="{0}"
                HeaderText="离开黑名单时间" TextAlign="Center" />
            <ext:BoundField Width="60px" DataField="LogStatus" SortField="LogStatus" DataFormatString="{0}"
                HeaderText="记录状态" TextAlign="Center" />
            <ext:BoundField Width="300px" DataField="Remark" SortField="Remark" DataFormatString="{0}"
                HeaderText="备注" />
            <ext:LinkButtonField Width="75px" CommandName="ActionInfo" Icon="Zoom" ToolTip="黑名单详情"
                ColumnID="BlacklistInfo" HeaderText="黑名单详情" TextAlign="Center" />
        </Columns>
    </ext:Grid>
    <ext:Window ID="WindowEdit" Title="黑名单详情" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="false" runat="server" IsModal="true"
        Width="670" EnableConfirmOnClose="true" Height="175">
    </ext:Window>
    </form>
</body>
</html>
