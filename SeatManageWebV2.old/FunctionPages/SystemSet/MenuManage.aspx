<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuManage.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.SystemSet.MenuManage" %>

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
        BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5" BoxConfigChildMargin="0 5 0 0"
        ShowHeader="false">
        <Items>
            <ext:Panel ID="Panel3" EnableBackgroundColor="false" BoxFlex="1" runat="server" BodyPadding="5px"
                ShowBorder="false" ShowHeader="false" Height="30">
                <Items>
                    <ext:Button ID="btnAddMenu" runat="server" Text="新增菜单" CssClass="inline" EnablePostBack="false">
                    </ext:Button>
                </Items>
            </ext:Panel>
            <ext:Panel ID="Panel1" EnableBackgroundColor="false" BoxFlex="1" runat="server" BodyPadding="5px"
                ShowBorder="false" ShowHeader="false" AutoScroll="true">
                <Items>
                    <ext:Grid ID="GridSysMenu" Title="系统菜单" ShowBorder="true" ShowHeader="true" AutoHeight="true"
                        runat="server" EnableCheckBoxSelect="false" DataKeyNames="MenuID,ModSeq" Width="430px"
                        EnableRowNumber="false" OnRowCommand="GridSysMenu_RowCommand">
                        <Columns>
                            <ext:BoundField DataField="ModSeq" DataSimulateTreeLevelField="MenuLv" DataFormatString="{0}"
                                HeaderText="菜单编号" ExpandUnusedSpace="True" Width="150px" />
                            <ext:BoundField DataField="Mcaption" DataFormatString="{0}" HeaderText="菜单名称" ExpandUnusedSpace="True"
                                Width="200px" />
                            <ext:LinkButtonField Width="35px" CommandName="ActionDelete" Icon="Delete" ConfirmText="你确定要该菜单吗？所对应的角色权限也会删除"
                                ConfirmTarget="Top" ColumnID="ColDel" ToolTip="删除" HeaderText="删除" />
                            <ext:WindowField Width="35px" WindowID="WindowEdit" Icon="Pencil" ToolTip="编辑" DataIFrameUrlFields="MenuID,ModSeq"
                                DataIFrameUrlFormatString="MenuEdit.aspx?MenuID={0}&flag=edit" Title="编辑"  HeaderText="编辑" IFrameUrl="MenuEdit.aspx" />
                        </Columns>
                    </ext:Grid>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>
    <ext:Window ID="WindowEdit" Title="弹出窗体" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="true" runat="server" OnClose="WindowEdit_Close"
        IsModal="true" Width="330px" EnableConfirmOnClose="true" Height="220px">
    </ext:Window>
    </form>
</body>
</html>
