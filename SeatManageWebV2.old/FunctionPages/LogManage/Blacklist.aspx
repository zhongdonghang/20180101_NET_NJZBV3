<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Blacklist.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.LogManage.Blacklist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1" />
    <ext:Form EnableBackgroundColor="true" BodyPadding="5px" ID="Form2" runat="server"
        Title="查询条件" LabelWidth="100px">
        <Rows>
            <ext:FormRow ColumnWidths="25% 25% 25% 25%">
                <Items>
                    <ext:TextBox runat="server" Width="150" ID="txtNum" Label="学号" EmptyText="不输入学号默认查询全部">
                    </ext:TextBox>
                    <ext:DropDownList ID="ddllogstatus" runat="server" Width="150" Label="记录状态" ShowLabel="True">
                        <ext:ListItem Text="查询全部" Value="-1" />
                        <ext:ListItem Text="处罚中黑名单" Value="1" Selected="true" />
                        <ext:ListItem Text="过期黑名单" Value="0" />
                    </ext:DropDownList>
                    <ext:CheckBox ID="chkSearchMH" ShowLabel="false" Height="22px" Readonly="true" runat="server"
                        Text="启用模糊查询">
                    </ext:CheckBox>
                    <ext:Label ID="Label1" runat="server" Height="22px">
                    </ext:Label>
                    <%--                    <ext:Label ID="Label1" runat="server">
                    </ext:Label>--%>
                </Items>
            </ext:FormRow>
            <ext:FormRow ColumnWidths="25% 25% 25% 25%">
                <Items>
                    <ext:DatePicker runat="server" Width="150" Label="开始日期" EmptyText="请选择开始日期" Readonly="false"
                        ID="dpStartDate">
                    </ext:DatePicker>
                    <ext:DatePicker ID="dpEndDate" Width="150" Readonly="false" DateFormatString="yyyy-MM-dd"
                        CompareControl="dpStartDate" CompareOperator="GreaterThanEqual" CompareMessage="结束日期应该大于开始日期"
                        EmptyText="请选择结束日期" Label="结束日期" runat="server">
                    </ext:DatePicker>
                    <ext:Button runat="server" ID="btnSubmit" ValidateTarget="Top" CssClass="inline"
                        Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" AjaxLoadingType="Mask" ValidateForms="Form2"
                        OnClick="btnSubmit_Click">
                    </ext:Button>
                    <ext:Label ID="Label2" runat="server">
                    </ext:Label>
                </Items>
            </ext:FormRow>
            <ext:FormRow ColumnWidths="15% 15% 15% 55%">
                <Items>
                    <ext:Button ID="btnAddBl" runat="server" Text="添加黑名单记录" CssClass="inline">
                    </ext:Button>
                    <ext:Button ID="btnSelectDelete" runat="server" Text="批量清除黑名单" CssClass="inline"
                        ConfirmText="你确定要把读者从黑名单中移除吗？" OnClick="btnSelectDelete_Click">
                    </ext:Button>
                    <ext:Button ID="btnDeleteBlackList" runat="server" Text="按照日期清除黑名单" CssClass="inline">
                    </ext:Button>
                    <ext:Label ID="Label3" runat="server">
                    </ext:Label>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:Grid ID="BlacklistGrid" Title="黑名单记录列表" AllowSorting="true" SortColumnIndex="0"
        SortDirection="ASC" PageSize="20" ShowBorder="true" ShowHeader="true" AutoHeight="true"
        AllowPaging="true" EnableRowNumber="true" runat="server" EnableCheckBoxSelect="True"
        DataKeyNames="ID" OnPageIndexChange="BlacklistGrid_PageIndexChange" OnSort="BlacklistGrid_Sort"
        OnPreRowDataBound="BlacklistGrid_OnPreRowDataBound" OnRowCommand="BlacklistGrid_RowCommand">
        <Columns>
            <ext:BoundField Width="0px" DataField="ID" SortField="ID" DataFormatString="{0}"
                Hidden="true" />
            <ext:BoundField Width="100px" DataField="CardNo" SortField="CardNo" DataFormatString="{0}"
                HeaderText="读者学号" />
            <ext:BoundField Width="75px" DataField="ReaderName" SortField="ReaderName" DataFormatString="{0}"
                HeaderText="读者姓名" />
            <ext:BoundField Width="120px" DataField="AddTime" SortField="AddTime" DataFormatString="{0:yy/MM/dd HH:mm:ss}"
                HeaderText="加入黑名单时间" />
            <ext:BoundField Width="120px" DataField="LeaveTime" SortField="LeaveTime" DataFormatString="{0:yy/MM/dd HH:mm:ss}"
                HeaderText="离开黑名单时间" />
            <ext:BoundField Width="60px" DataField="LeaveMode" SortField="LeaveMode" DataFormatString="{0}"
                HeaderText="离开方式" />
            <ext:BoundField Width="60px" DataField="LogStatus" SortField="LogStatus" DataFormatString="{0}"
                HeaderText="记录状态" />
            <ext:BoundField Width="400px" DataField="Remark" SortField="Remark" DataFormatString="{0}"
                HeaderText="备注" />
            <ext:LinkButtonField Width="35px" CommandName="ActionInfo" Icon="Zoom" ToolTip="黑名单详情"
                ColumnID="BlacklistInfo" HeaderText="详情" TextAlign="Center" />
            <ext:LinkButtonField Width="35px" CommandName="ActionDelete" Icon="Delete" ConfirmText="你确定要把读者从黑名单中移除吗？"
                ConfirmTarget="Top" ColumnID="Blacklistdelete" ToolTip="清除黑名单" HeaderText="删除"
                TextAlign="Center" />
        </Columns>
    </ext:Grid>
    <ext:Window ID="WindowEdit" Title="黑名单详情" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="false" runat="server" IsModal="true"
        Width="1070" EnableConfirmOnClose="true" Height="375">
    </ext:Window>
    <ext:Window ID="WindowBLdelete" Title="批量删除黑名单" Popup="false" EnableIFrame="true"
        IFrameUrl="about:blank" EnableMaximize="true" Target="Top" EnableResize="false"
        runat="server" IsModal="true" Width="350" EnableConfirmOnClose="true" Height="200"
        OnClose="Window_Close">
    </ext:Window>
    <ext:Window ID="WindowBLAdd" Title="添加黑名单" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="false" runat="server" IsModal="true"
        Width="400" EnableConfirmOnClose="true" Height="300" OnClose="Window_Close">
    </ext:Window>
    </form>
</body>
</html>
