<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.UsersManage.UserInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager2" runat="server" />
    <ext:Form ID="Form2" runat="server" EnableBackgroundColor="false" Width="560px" ShowBorder="false"
        ShowHeader="false" LabelWidth="100px">
        <Rows>
            <ext:FormRow ColumnWidths="20% 35% 35% 10%">
                <Items>
                    <ext:Button ID="btnAddUser" runat="server" Text="添加用户" CssClass="inline" EnablePostBack="false">
                    </ext:Button>
                    <ext:DropDownList runat="server" ID="ddlselectType" Label="按用户类型分类" OnSelectedIndexChanged="selectType_OnSelectedIndexChanged"
                        AutoPostBack="true">
                        <ext:ListItem Text="全部" Value="-1" />
                        <ext:ListItem Text="管理员" Value="0" Selected="true" />
                        <ext:ListItem Text="读者" Value="1" />
                    </ext:DropDownList>
                    <ext:DropDownList runat="server" ID="ddlselectIsUsed" Label="按是否启用分类" OnSelectedIndexChanged="selectIsUsed_OnSelectedIndexChanged"
                        AutoPostBack="true">
                        <ext:ListItem Text="全部" Value="-1" Selected="true" />
                        <ext:ListItem Text="未启用" Value="0" />
                        <ext:ListItem Text="启用" Value="1" />
                    </ext:DropDownList>
                    <ext:Label runat="server">
                    </ext:Label>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:PageManager ID="PageManager1" runat="server" />
    <ext:Grid ID="UsersGrid" Title="用户列表" AllowSorting="true" SortColumnIndex="0" SortDirection="ASC"
        PageSize="20" ShowBorder="true" ShowHeader="true" AutoHeight="true" AllowPaging="true"
        EnableRowNumber="true" runat="server" EnableCheckBoxSelect="True" DataKeyNames="用户登录ID"
        OnPageIndexChange="UsersGrid_PageIndexChange" OnSort="UsersGrid_Sort" OnPreRowDataBound="UsersGrid_OnPreRowDataBound"
        OnRowCommand="UsersGrid_RowCommand">
        <Columns>
            <ext:BoundField Width="100px" DataField="用户姓名" SortField="用户姓名" DataFormatString="{0}"
                HeaderText="用户姓名" />
            <ext:BoundField Width="100px" DataField="用户登录ID" SortField="用户登录ID" DataFormatString="{0}"
                HeaderText="用户登录ID" />
            <ext:BoundField Width="100px" DataField="用户类型" SortField="用户类型" DataFormatString="{0}"
                HeaderText="用户类型" />
            <ext:CheckBoxField Width="60px" RenderAsStaticField="true" DataField="用户状态" SortField="用户状态"
                HeaderText="是否启用" />
            <ext:BoundField Width="200px" DataField="备注" SortField="备注" DataFormatString="{0}"
                HeaderText="备注" />
            <ext:LinkButtonField Width="35px" CommandName="ActionDelete" Icon="Delete" ConfirmText="你确定要这个用户吗？用户的设置和信息会一起删除"
                ConfirmTarget="Top" ColumnID="userdelete" ToolTip="删除用户" HeaderText="删除" />
            <ext:LinkButtonField Width="35px" CommandName="ActionEdit" Icon="Pencil" ToolTip="编辑用户"
                ColumnID="useredit" HeaderText="编辑" />
        </Columns>
    </ext:Grid>
    <ext:Window ID="WindowEdit" Title="用户编辑" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="true" runat="server" OnClose="WindowEdit_Close"
        IsModal="true" Width="520" EnableConfirmOnClose="true" Height="400px">
    </ext:Window>
    </form>
</body>
</html>
