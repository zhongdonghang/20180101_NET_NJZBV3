<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnterOutLog.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.LogManage.EnterOutLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1" />
    <ext:Form LabelWidth="95px" EnableBackgroundColor="true" BodyPadding="5px" ID="Form2"
        runat="server" Title="查询条件">
        <Rows>
            <ext:FormRow ColumnWidths="25% 25% 25% 25%">
                <Items>
                    <ext:DropDownList ID="ddlQueryMethod" runat="server" Width="150" Label="查询方式" ShowLabel="True"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlQueryMethod_SelectedIndexChanged">
                        <ext:ListItem Text="按学号查询" Value="cardNo" />
                        <ext:ListItem Text="按座位号查询" Value="seatNum" />
                    </ext:DropDownList>
                    <ext:TextBox runat="server" Width="150" ID="txtNum" EmptyText="请输入学号或座位号" Label="学号或座位号"
                        Required="true" RequiredMessage="请输入学号或者座位号" ShowRedStar="true">
                    </ext:TextBox>
                    <ext:DropDownList ID="ddlReadingRoom" runat="server" Width="150" Label="阅览室" ShowLabel="True">
                    </ext:DropDownList>
                    <ext:Label runat="server" Height="24">
                    </ext:Label>
                </Items>
            </ext:FormRow>
            <ext:FormRow ColumnWidths="25% 25% 8% 37%">
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
                    <ext:CheckBox ID="chkSearchMH" ShowLabel="false" Readonly="true" runat="server" Text="启用模糊查询">
                    </ext:CheckBox>
                    <%--                    <ext:Label runat="server">
                    </ext:Label>--%>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:Grid ID="enterOutLogList" Title="进出记录查询" AllowSorting="true" SortColumnIndex="5"
        AutoScroll="true" SortDirection="ASC" PageSize="20" ShowBorder="true" ShowHeader="true"
        AutoHeight="true" EnableRowNumber="true" AllowPaging="true" runat="server" EnableCheckBoxSelect="false"
        DataKeyNames="CardNo" OnPageIndexChange="enterOutLogList_PageIndexChange" OnRowDataBound="enterOutLogList_RowDataBound"
        OnSort="enterOutLogList_Sort">
        <Columns>
            <ext:BoundField Width="100px" DataField="CardNo" SortField="CardNo" DataFormatString="{0}"
                HeaderText="学号" />
            <ext:BoundField Width="50px" DataField="ReaderName" SortField="ReaderName" DataFormatString="{0}"
                HeaderText="姓名" />
            <ext:BoundField Width="150px" DataField="ReadingRoomName" SortField="ReadingRoomName"
                DataFormatString="{0}" HeaderText="所在阅览室" />
            <ext:BoundField Width="65px" DataField="SeatShortNum" SortField="SeatShortNum" DataFormatString="{0}"
                HeaderText="座位号" />
            <ext:BoundField Width="50px" DataField="Status" SortField="Status" DataFormatString="{0}"
                HeaderText="状态" />
            <ext:BoundField Width="120px" DataField="EnterOutTime" SortField="EnterOutTime" DataFormatString="{0:yy/MM/dd HH:mm:ss}"
                HeaderText="进出时间" />
            <ext:BoundField Width="500" DataField="Remark" DataFormatString="{0}" HeaderText="详细信息" />
        </Columns>
    </ext:Grid>
    </form>
</body>
</html>
