<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgPostSetPage.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.SystemSet.MsgPostSetPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <ext:PageManager ID="PageManager1" runat="server" />
        <ext:Form ID="Form3" runat="server" EnableBackgroundColor="false" ShowHeader="false"
        Width="300px" ShowBorder="false">
        <Rows>
            <ext:FormRow>
                <Items>
                    <ext:Button ID="btnAddItem" Type="Button" Text="添加推送路径" runat="server" CssClass="inline">
                    </ext:Button>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
        <ext:Panel ID="Panel1" EnableBackgroundColor="false" BoxFlex="1" runat="server" BodyPadding="5px"
            ShowBorder="false" ShowHeader="false" AutoScroll="true">
            <Items>
                <ext:Grid ID="GridMsgPostItems" Title="消息推送" ShowBorder="true" ShowHeader="true"
                    AutoHeight="true" runat="server" EnableCheckBoxSelect="false" DataKeyNames="UserName"
                    Width="950px" EnableRowNumber="True" AllowSorting="true" SortColumnIndex="0"
                    SortDirection="ASC" OnSort="Grid_Sort" OnRowCommand="Grid_RowCommand">
                    <Columns>
                        <ext:BoundField Width="100px" SortField="UserName" DataField="UserName" DataFormatString="{0}"
                            HeaderText="用户名" />
                        <ext:BoundField Width="150px" SortField="AppID" DataField="AppID" DataFormatString="{0}"
                            HeaderText="AppID" />
                        <ext:BoundField Width="450px" SortField="PostUrl" DataField="PostUrl" DataFormatString="{0}"
                            HeaderText="请求地址" /> 
                        <ext:LinkButtonField Width="35px" CommandName="ActionDelete" Icon="Delete" ConfirmText="确定要删除？"
                            ConfirmTarget="Top" ColumnID="ColDel" ToolTip="删除" HeaderText="删除" />
                        <ext:WindowField Width="35px" WindowID="MsgPost" Icon="Pencil" ToolTip="编辑" DataIFrameUrlFields="UserName"
                             DataIFrameUrlFormatString="MsgPostItemEdit.aspx?flag=edit&userName={0}"    Title="编辑"
                            HeaderText="编辑" IFrameUrl="MsgPostItemEdit.aspx" />
                    </Columns>
                </ext:Grid>
                <ext:Window ID="MsgPost" Title="编辑" Popup="false" EnableIFrame="true"
        IFrameUrl="about:blank" EnableMaximize="true" Target="Top" runat="server" OnClose="WindowEdit_Close"
        IsModal="true" Width="350px" EnableConfirmOnClose="true" Height="200px" EnableResize="false">
    </ext:Window>
            </Items>
        </ext:Panel>
    </form>
</body>
</html>
