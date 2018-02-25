<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViolateDiscipline.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.LogManage.ViolateDiscipline" %>

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
        Title="查询条件" LabelWidth="80px">
        <Rows>
            <ext:FormRow ColumnWidths="22% 22% 22% 22% 12%">
                <Items>
                    <ext:TextBox runat="server" Width="150" ID="txtNum" Label="学号" EmptyText="不输入学号默认查询全部">
                    </ext:TextBox>
                    <ext:DropDownList ID="ddlReadingRoom" runat="server" Width="150" Label="阅览室" ShowLabel="True">
                    </ext:DropDownList>
                    <ext:DropDownList ID="ddlblacklist" runat="server" Width="150" Label="黑名单情况" ShowLabel="True">
                        <ext:ListItem Text="查询全部" Value="-1" />
                        <ext:ListItem Text="被记录黑名单" Value="1" />
                        <ext:ListItem Text="没有被记录黑名单" Value="0" Selected="true" />
                    </ext:DropDownList>
                    <ext:DropDownList ID="ddlVrType" runat="server" Width="150" Label="违规类型" ShowLabel="True">
                        <ext:ListItem Text="查询全部" Value="-1" />
                        <ext:ListItem Text="预约超时" Value="0" />
                        <ext:ListItem Text="在座超时" Value="2" />
                        <ext:ListItem Text="暂离超时" Value="3" />
                        <ext:ListItem Text="被管理员设为暂离，暂离超时" Value="4" />
                        <ext:ListItem Text="被读者设为暂离，暂离超时" Value="5" />
                        <ext:ListItem Text="被管理员设置离开" Value="1" />
                        <ext:ListItem Text="离开未刷卡" Value="8" />
                    </ext:DropDownList>
                    <ext:Label ID="Label2" runat="server">
                    </ext:Label>
                </Items>
            </ext:FormRow>
            <ext:FormRow ColumnWidths="22% 22% 22% 8% 26%">
                <Items>
                    <ext:DatePicker runat="server" Width="150" Label="开始日期" EmptyText="请选择开始日期" ID="dpStartDate">
                    </ext:DatePicker>
                    <ext:DatePicker ID="dpEndDate" Width="150" EmptyText="请选择结束日期" Label="结束日期" CompareControl="dpStartDate"
                        CompareOperator="GreaterThanEqual" CompareMessage="结束日期应该大于开始日期" runat="server">
                    </ext:DatePicker>
                    <ext:DropDownList ID="ddllogstatus" runat="server" Width="150" Label="记录状态" ShowLabel="True">
                        <ext:ListItem Text="查询全部" Value="-1" />
                        <ext:ListItem Text="有效记录" Value="1" />
                        <ext:ListItem Text="失效记录" Value="0" />
                    </ext:DropDownList>
                    <ext:Button runat="server" ID="btnSubmit" ValidateTarget="Top" CssClass="inline"
                        Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" AjaxLoadingType="Mask" ValidateForms="Form2"
                        OnClick="btnSubmit_Click">
                    </ext:Button>
                    <ext:CheckBox ID="chkSearchMH" ShowLabel="false" Readonly="true" runat="server" Text="启用模糊查询">
                    </ext:CheckBox>
                    <%--<ext:Label ID="Label3" runat="server">
                    </ext:Label>--%>
                </Items>
            </ext:FormRow>
            <ext:FormRow ColumnWidths="15% 15% 15% 55%">
                <Items>
                    <ext:Button ID="btnAdd" runat="server" Text="添加违规记录" CssClass="inline" EnablePostBack="false">
                    </ext:Button>
                    <ext:Button ID="btnSelectDelete" runat="server" Text="批量删除违规记录" CssClass="inline"
                        ConfirmText="确认删除选中的违规记录吗？" OnClick="btnSelectDelete_Click">
                    </ext:Button>
                    <ext:Button ID="btnDeleteBySearch" runat="server" Text="按条件删除违规记录" CssClass="inline">
                    </ext:Button>
                    <ext:Label ID="Label1" runat="server">
                    </ext:Label>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:Grid ID="VRGrid" Title="违规记录列表" AllowSorting="true" SortColumnIndex="0" SortDirection="ASC"
        PageSize="20" ShowBorder="true" ShowHeader="true" AutoHeight="true" AllowPaging="true"
        EnableRowNumber="true" runat="server" EnableCheckBoxSelect="True" DataKeyNames="ID"
        OnPageIndexChange="VRGrid_PageIndexChange" OnSort="VRGrid_Sort" OnPreRowDataBound="VRGrid_OnPreRowDataBound"
        OnRowCommand="VRGrid_RowCommand">
        <Columns>
            <ext:BoundField Width="0px" DataField="ID" SortField="ID" DataFormatString="{0}"
                Hidden="true" />
            <ext:BoundField Width="100px" DataField="CardNo" SortField="CardNo" DataFormatString="{0}"
                HeaderText="读者学号" />
            <ext:BoundField Width="75px" DataField="ReaderName" SortField="ReaderName" DataFormatString="{0}"
                HeaderText="读者姓名" />
            <ext:BoundField Width="120px" DataField="AddTime" SortField="AddTime" DataFormatString="{0:yy/MM/dd HH:mm:ss}"
                HeaderText="违规时间" />
            <ext:BoundField Width="120px" DataField="ReadingRoom" SortField="ReadingRoom" DataFormatString="{0}"
                HeaderText="违规阅览室" />
            <ext:BoundField Width="75px" DataField="Seat" SortField="Seat" DataFormatString="{0}"
                HeaderText="座位编号" />
            <ext:BoundField Width="75px" DataField="LogStatus" SortField="LogStatus" DataFormatString="{0}"
                HeaderText="记录状态" />
            <ext:BoundField Width="85px" DataField="BlacklistStatus" SortField="BlacklistStatus"
                DataFormatString="{0}" HeaderText="黑名单状态" />
            <ext:BoundField Width="400px" DataField="Remark" SortField="Remark" DataFormatString="{0}"
                HeaderText="备注" />
            <ext:LinkButtonField Width="35px" CommandName="ActionDelete" Icon="Delete" ConfirmText="你确定要删除此条记录吗？（并不会真正删除，只是使此条记录无效）"
                ConfirmTarget="Top" ColumnID="VRdelete" ToolTip="删除记录" HeaderText="删除" />
        </Columns>
    </ext:Grid>
    <ext:Window ID="WindowEdit" Title="添加违规记录" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Self" EnableResize="false" runat="server" OnClose="WindowEdit_Close"
        IsModal="true" Width="370" EnableConfirmOnClose="true" Height="270px" AutoHeight="true">
    </ext:Window>
    <ext:Window ID="WindowDelBySearch" Title="按条件删除违规记录" Popup="false" EnableIFrame="true"
        IFrameUrl="about:blank" EnableMaximize="true" Target="Self" EnableResize="false"
        runat="server" OnClose="WindowDelBySearch_Close" IsModal="true" Width="370" EnableConfirmOnClose="true"
        Height="200px" AutoHeight="true">
    </ext:Window>
    </form>
</body>
</html>
