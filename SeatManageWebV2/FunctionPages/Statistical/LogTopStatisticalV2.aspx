<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogTopStatisticalV2.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.Statistical.LogTopStatisticalV2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1" />
        <ext:Form EnableBackgroundColor="true" BodyPadding="5px" ID="Form2" runat="server"
            Title="查询条件" LabelWidth="80px">
            <Rows>
                <ext:FormRow ColumnWidths="25% 25% 25% 25%">
                    <Items>
                        <ext:DropDownList ID="ddllogtype" runat="server" Width="150" Label="排行榜类型" ShowLabel="True">
                            <ext:ListItem Text="选座次数排行" Value="0" />
                            <ext:ListItem Text="在座时长排行" Value="1" />
                            <ext:ListItem Text="违规记录排行" Value="2" />
                            <ext:ListItem Text="黑名单排行" Value="3" />
                        </ext:DropDownList>
                        <ext:DropDownList ID="ddlreadertype" runat="server" Width="150" Label="统计分类" ShowLabel="True">
                            <ext:ListItem Text="按读者统计" Value="0" />
                            <ext:ListItem Text="按读者类型统计" Value="1" />
                            <ext:ListItem Text="按院系统计" Value="2" />
                        </ext:DropDownList>
                        <ext:DropDownList ID="ddltopnum" runat="server" Width="150" Label="统计前" ShowLabel="True">
                            <ext:ListItem Text="100名" Value="100" />
                            <ext:ListItem Text="200名" Value="200" />
                            <ext:ListItem Text="500名" Value="500" />
                            <ext:ListItem Text="1000名" Value="1000" />
                        </ext:DropDownList>
                        <ext:Label ID="l1" runat="server">
                        </ext:Label>
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
                            Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" ValidateForms="Form2" OnClick="btnSubmit_Click">
                        </ext:Button>
                        <ext:Button ID="btn2" runat="server" Text="导出Excel" OnClick="btn2_Click"></ext:Button>
                        <ext:Label ID="Label3" runat="server">
                        </ext:Label>
                    </Items>
                </ext:FormRow>
                <ext:FormRow ColumnWidths="15% 60% 25%">
                    <Items>
                        <ext:Label ID="Label4" runat="server">
                        </ext:Label>
                        <ext:Label ID="lbmessage" ClientIDMode="Static" runat="server" Text="请选择查询条件，点击查询按钮进行查询，查询速度较慢请耐心等待">
                        </ext:Label>
                        <ext:Label ID="Label1" runat="server">
                        </ext:Label>
                    </Items>
                </ext:FormRow>
            </Rows>
        </ext:Form>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <ext:Grid ID="TopListGrid" Title="记录排行榜" AllowSorting="true" SortColumnIndex="0"
            SortDirection="ASC" PageSize="20" ShowBorder="true" ShowHeader="true"
            AutoHeight="true" AllowPaging="true" EnableRowNumber="true" runat="server" EnableCheckBoxSelect="false"
            DataKeyNames="ID" OnPageIndexChange="TopListGrid_PageIndexChange" OnSort="TopListGrid_Sort">
            <Columns>
                <ext:BoundField Width="50px" DataField="TopNum" SortField="TopNum" DataFormatString="{0}"
                    HeaderText="排名" />
                <ext:BoundField Width="100px" DataField="CardNo" SortField="CardNo" DataFormatString="{0}"
                    HeaderText="读者学号" />
                <ext:BoundField Width="75px" DataField="ReaderName" SortField="ReaderName" DataFormatString="{0}"
                    HeaderText="读者姓名" />
                <ext:BoundField Width="80px" DataField="TypeName" SortField="TypeName" DataFormatString="{0}"
                    HeaderText="读者类型" />
                <ext:BoundField Width="150px" DataField="DeptName" SortField="DeptName" DataFormatString="{0}"
                    HeaderText="读者院系" />
                <ext:BoundField Width="180px" DataField="LogCount" SortField="LogCount" DataFormatString="{0}"
                    HeaderText="记录数目" />
                <ext:BoundField Width="500px" DataField="Remark" SortField="Remark" DataFormatString="{0}"
                    HeaderText="备注" />
            </Columns>
        </ext:Grid>
    </form>
</body>
</html>
