<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FunctionPagesManage.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.SystemSet.FunctionPagesManage" %>

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
                    <ext:Button ID="btnAddMenu" runat="server" Text="新增功能页" CssClass="inline" EnablePostBack="false">
                    </ext:Button>
                </Items>
            </ext:Panel>
            <ext:Panel ID="Panel1" EnableBackgroundColor="false" BoxFlex="1" runat="server" BodyPadding="5px"
                ShowBorder="false" ShowHeader="false" AutoScroll="true">
                <Items>
                    <ext:Grid ID="GridFunctionPages" Title="系统功能页" ShowBorder="true" ShowHeader="true"
                        AutoHeight="true" runat="server" EnableCheckBoxSelect="false" DataKeyNames="ModSeq"
                        Width="950px" EnableRowNumber="True" AllowSorting="true" SortColumnIndex="3"
                        SortDirection="ASC" OnSort="GridFunctionPages_Sort" OnRowCommand="GridFunctionPages_RowCommand"
                       >
                        <Columns>
                            <ext:BoundField Width="100px" SortField="ModSeq" DataField="ModSeq" DataFormatString="{0}"
                                HeaderText="模块编号" />
                            <ext:BoundField Width="150px" SortField="MCaption" DataField="MCaption" DataFormatString="{0}"
                                HeaderText="模块名称" />
                            <ext:BoundField Width="450px" SortField="MenuLink" DataField="MenuLink" DataFormatString="{0}"
                                HeaderText="模块链接" />
                            <ext:BoundField Width="150px" SortField="OrderSeq" DataField="OrderSeq" HeaderText="模块分类" />
                            <ext:LinkButtonField Width="35px" CommandName="ActionDelete" Icon="Delete" ConfirmText="你确定要删除该功能页吗？会同时删除使用此页面的功能菜单以及对应的角色权限"
                                ConfirmTarget="Top" ColumnID="ColDel" ToolTip="删除" HeaderText="删除" />
                            <ext:WindowField Width="35px" WindowID="WindowEdit" Icon="Pencil" ToolTip="编辑" DataIFrameUrlFields="ModSeq"
                                DataIFrameUrlFormatString="FunctionPageEdit.aspx?ModSeq={0}&flag=edit" Title="编辑"
                                HeaderText="编辑" IFrameUrl="FunctionPageEdit.aspx" />
                        </Columns>
                    </ext:Grid>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>
    <ext:Window ID="WindowEdit" Title="弹出窗体" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="false" runat="server" OnClose="WindowEdit_Close"
        IsModal="true" Width="330px" EnableConfirmOnClose="true" Height="190px">
    </ext:Window>
    </form>
</body>
</html>
