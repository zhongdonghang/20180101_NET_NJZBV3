<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleManage.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.UsersManage.RoleManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel2" />
    <ext:Panel ID="Panel2" runat="server" Height="250px" ShowBorder="True" Layout="VBox"
         BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
        BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
        <Items>
            <ext:Panel ID="Panel3" EnableBackgroundColor="false" BoxFlex="1" runat="server" BodyPadding="5px"
                ShowBorder="false" ShowHeader="false" Height="30">
                <Items>
                    <ext:Button ID="btnAddRole" runat="server" Text="新增角色" CssClass="inline" EnablePostBack="false">
                    </ext:Button>
                </Items>
            </ext:Panel>
            <ext:Panel ID="Panel1" EnableBackgroundColor="false" BoxFlex="1" runat="server" BodyPadding="5px"
                ShowBorder="false" ShowHeader="false" AutoScroll="true">
                <Items>
                    <ext:Grid ID="GridRole" Title="系统角色" ShowBorder="true" ShowHeader="true" AutoHeight="true"
                        runat="server" EnableCheckBoxSelect="false" DataKeyNames="ROLEID" Width="500px"
                        OnPreRowDataBound="GridRole_PreRowDataBound" EnableRowNumber="True" OnRowCommand="GridRole_RowCommand">
                        <Columns>
                            <ext:BoundField DataField="ROLENAME" DataFormatString="{0}" HeaderText="角色名称" ExpandUnusedSpace="True"
                                Width="100px" />
                            <ext:LinkButtonField Width="35px" CommandName="ActionDelete" Icon="Delete" ConfirmText="你确定要该角色吗？"
                                ConfirmTarget="Top" ColumnID="ColDel" ToolTip="删除角色" HeaderText="删除" />
                            <ext:LinkButtonField ColumnID="lnkbtnEdit" HeaderText="编辑" Width="35px" Icon="Pencil"
                                runat="server" ToolTip="编辑" />
                        </Columns>
                    </ext:Grid>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>
    <ext:Window ID="WindowEdit" Title="弹出窗体" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="true" runat="server" OnClose="WindowEdit_Close"
        IsModal="true" Width="600px" EnableConfirmOnClose="true" Height="400px">
    </ext:Window>
    </form>
</body>
</html>
