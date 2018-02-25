<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectEnterOutLog.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.ReaderLog.SelectEnterOutLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .mright
        {
            margin-right: 5px;
        }
        .datecontainer .x-form-field-trigger-wrap
        {
            margin-right: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1" />
    <ext:Form LabelWidth="100" EnableBackgroundColor="true" BodyPadding="5px" ID="Form2"
        runat="server" Title="查询条件">
        <Rows>
            <ext:FormRow ColumnWidths="25% 75%">
                <Items>
                    <ext:DatePicker runat="server" Required="true" Width="150" Label="开始日期" EmptyText="请选择开始日期"
                        Readonly="false" ID="dpStartDate" ShowRedStar="True">
                    </ext:DatePicker>
                    <ext:DatePicker ID="dpEndDate" Required="true" Width="150" Readonly="false" CompareControl="dpStartDate"
                        DateFormatString="yyyy-MM-dd" EmptyText="请选择结束日期" CompareOperator="GreaterThanEqual"
                        CompareMessage="结束日期应该大于开始日期" Label="结束日期" runat="server" ShowRedStar="True">
                    </ext:DatePicker>
                </Items>
            </ext:FormRow>
            <ext:FormRow ColumnWidths="25% 75%">
                <Items>
                    <ext:DropDownList ID="ddlReadingRoom" runat="server" Width="150" Label="阅览室" ShowLabel="True">
                    </ext:DropDownList>
                    <ext:Button runat="server" ID="btnSubmit" ValidateTarget="Top" CssClass="inline"
                        Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" AjaxLoadingType="Mask" ValidateForms="Form2"
                        OnClick="btnSubmit_Click">
                    </ext:Button>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:Grid ID="EnterOutGrid" Title="进出记录列表" AllowSorting="true" SortColumnIndex="0"
        SortDirection="ASC" PageSize="20" ShowBorder="true" ShowHeader="true" AutoHeight="true"
        AllowPaging="true" EnableRowNumber="true" runat="server" DataKeyNames="ID" OnPageIndexChange="EnterOutGrid_PageIndexChange"
        OnSort="EnterOutGrid_Sort" OnPreRowDataBound="EnterOutGrid_OnPreRowDataBound">
        <Columns>
            <ext:BoundField Width="200px" DataField="ReadingRoomName" SortField="ReadingRoomName"
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
