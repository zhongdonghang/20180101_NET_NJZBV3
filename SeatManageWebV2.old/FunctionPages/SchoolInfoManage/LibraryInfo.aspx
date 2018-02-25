<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LibraryInfo.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.SchoolInfoManage.LibraryInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager1" runat="server" />
    <ext:Form ID="Form2" runat="server" EnableBackgroundColor="false" ShowHeader="false"
        Width="300px" ShowBorder="false">
        <Rows>
            <ext:FormRow ColumnWidths="41% 18% 41%">
                <Items>
                    <ext:Button ID="btnAddLibrary" runat="server" Text="添加图书馆" CssClass="inline" EnablePostBack="false">
                    </ext:Button>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:Grid ID="LibraryGrid" Title="图书馆列表" AllowSorting="true" SortColumnIndex="0"
        SortDirection="ASC" PageSize="20" ShowBorder="true" ShowHeader="true" AutoHeight="true"
        AllowPaging="true" runat="server" EnableCheckBoxSelect="false" DataKeyNames="LibraryNo"
        EnableRowNumber="true" OnPageIndexChange="LibraryGrid_PageIndexChange" OnSort="LibraryGrid_Sort"
        OnPreRowDataBound="LibraryGrid_OnPreRowDataBound" OnRowCommand="LibraryGrid_RowCommand">
        <Columns>
            <ext:BoundField Width="100px" DataField="LibraryNo" SortField="LibraryNo" DataFormatString="{0}"
                HeaderText="图书馆编号" />
            <ext:BoundField Width="150px" DataField="LibraryName" SortField="LibraryName" DataFormatString="{0}"
                HeaderText="图书馆名称" />
<%--            <ext:BoundField Width="100px" DataField="SchoolNo" SortField="SchoolNo" DataFormatString="{0}"
                HeaderText="所属校区编号" />--%>
            <ext:BoundField Width="150px" DataField="SchoolName" SortField="SchoolName" DataFormatString="{0}"
                HeaderText="所属校区" />
            <ext:LinkButtonField Width="35px" CommandName="ActionDelete" Icon="Delete"
                ConfirmTarget="Top" ColumnID="Librarydelete" ToolTip="删除图书馆" HeaderText="删除" />
            <ext:LinkButtonField Width="35px" CommandName="ActionEdit" Icon="Pencil" ToolTip="编辑图书馆"
                ColumnID="Libraryedit" HeaderText="编辑" />
        </Columns>
    </ext:Grid>
    <ext:Window ID="WindowEdit" Title="图书馆编辑" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="false" runat="server" OnClose="WindowEdit_Close"
        IsModal="true" Width="330" EnableConfirmOnClose="true" Height="240px">
    </ext:Window>
    <ext:Window ID="WindowDelete" Title="删除验证" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="false" runat="server" OnClose="WindowEdit_Close"
        IsModal="true" Width="400" EnableConfirmOnClose="true" Height="180px">
    </ext:Window>
    </form>
</body>
</html>
