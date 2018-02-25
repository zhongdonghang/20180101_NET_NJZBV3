<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogTopStatistics.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.Statistical.LogTopStatistics" Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript">
        var interval;
        function intervalRun(cmd) {

            interval = setInterval(GetSession, '500');
        }
        function GetSession(value) {
            jQuery.ajax({
                type: "POST",
                dataType: "text",
                url: "LogTopStatistics.aspx",
                async: true, //是否ajax同步 
                data: { "param": "GetSession" },
                success: function (msg) { 
                        $("#lbmessage").text(msg);
                    if (msg == "(4/4)查询完成，感谢您的耐心等待！") {
                        clearInterval(interval);
                        location.reload();
                    }

                }
            });
        }
        function end() {
            jQuery.post({
                type: "POST",
                dataType: "text",
                url: "LogTopStatistics.aspx",
                async: false, //是否ajax同步 
                data: { "param": "GetSession" },
                success: function (msg) {
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1" />
    <ext:Form EnableBackgroundColor="true" BodyPadding="5px" ID="Form2" runat="server"
        Title="查询条件" LabelWidth="80px">
        <Rows>
            <ext:FormRow ColumnWidths="25% 25% 50%">
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
                    <ext:Label ID="Label2" runat="server">
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
                        Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" ValidateForms="Form2" OnClick="btnSubmit_Click"
                        OnClientClick="intervalRun()">
                    </ext:Button>
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
    <%--<asp:UpdatePanel ID="MessagePanel" runat="server" ChildrenAsTriggers="True" UpdateMode="Always">
        <ContentTemplate>
            <asp:Label ID="lbmessage" runat="server" Text="数据加载中亲稍等……"></asp:Label>
            <asp:Button runat="server" ID="btnSubmit1" ValidateTarget="Top" CssClass="inline"
                Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" ValidateForms="Form2"
                OnClick="btnSubmit_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>--%>
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
