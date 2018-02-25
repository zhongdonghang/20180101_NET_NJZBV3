<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchoolInfo.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.SchoolInfoManage.SchoolInfo" %>

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
            <ext:FormRow>
                <Items>
                    <ext:Button ID="btnAddSchool" runat="server" Text="添加校区" CssClass="inline" EnablePostBack="false">
                    </ext:Button>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:Grid ID="SchoolGrid" Title="校区列表" AllowSorting="true" SortColumnIndex="0" SortDirection="ASC"
        PageSize="20" ShowBorder="true" ShowHeader="true" AutoHeight="true" AllowPaging="true"
        runat="server" EnableCheckBoxSelect="false" DataKeyNames="SchoolNo" OnPageIndexChange="SchoolGrid_PageIndexChange"
        OnSort="SchoolGrid_Sort" OnPreRowDataBound="SchoolGrid_OnPreRowDataBound" OnRowCommand="SchoolGrid_RowCommand"
        EnableRowNumber="true">
        <Columns>
            <ext:BoundField Width="70px" DataField="SchoolNo" SortField="SchoolNo" DataFormatString="{0}"
                HeaderText="校区编号" />
            <ext:BoundField Width="150px" DataField="SchoolName" SortField="SchoolName" DataFormatString="{0}"
                HeaderText="校区名称" />
            <ext:LinkButtonField Width="35px" CommandName="ActionDelete" Icon="Delete"
                ConfirmTarget="Parent" ColumnID="Schooldelete" ToolTip="删除校区" HeaderText="删除" />
            <ext:LinkButtonField Width="35px" CommandName="ActionEdit" Icon="Pencil" ToolTip="编辑校区"
                ColumnID="Schooledit" HeaderText="编辑" />
        </Columns>
    </ext:Grid>
    <ext:Window ID="WindowEdit" Title="校区编辑" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="false" runat="server" OnClose="WindowEdit_Close"
        IsModal="true" Width="330" EnableConfirmOnClose="true" Height="140px">
    </ext:Window>
    <ext:Window ID="WindowDelete" Title="删除验证" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="false" runat="server" OnClose="WindowEdit_Close"
        IsModal="true" Width="400" EnableConfirmOnClose="true" Height="180px">
    </ext:Window>
    </form>
</body>
</html>
