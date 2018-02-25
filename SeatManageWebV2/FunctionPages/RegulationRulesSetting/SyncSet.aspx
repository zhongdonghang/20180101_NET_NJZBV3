<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SyncSet.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.RegulationRulesSetting.SyncSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager1" runat="server" />
    <ext:SimpleForm ID="SimpleForm1" BodyPadding="5px" runat="server" EnableBackgroundColor="True"
        ShowBorder="false" ShowHeader="True" Width="1100px" Title="读者信息同步设置" LabelAlign="Right"
        LabelWidth="150">
        <Items>
            <ext:Label   ID="label1" Label="读者信息数据库配置" runat="server">
            </ext:Label>
            <ext:TextBox ID="txtsip" Label="数据库IP" runat="server" Width="200">
            </ext:TextBox>
            <ext:TextBox ID="txtsDB" Label="数据库名" runat="server" Width="200">
            </ext:TextBox>
            <ext:TextBox ID="txtsID" Label="登录名" runat="server" Width="200">
            </ext:TextBox>
            <ext:TextBox ID="txtsPW" Label="密码" runat="server" TextMode="Password" Width="200">
            </ext:TextBox>
            <ext:Button ID="testsconn" ValidateForms="SimpleForm1" CssClass="inline" Text="连接测试"
                runat="server" OnClick="testsconn_Click">
            </ext:Button>
            <%--<ext:Label ID="label2" Label="座位管理信息数据库配置" runat="server">
            </ext:Label>
            <ext:TextBox ID="txttip" Label="数据库IP" runat="server" Width="200">
            </ext:TextBox>
            <ext:TextBox ID="txttDB" Label="数据库名" runat="server" Width="200">
            </ext:TextBox>
            <ext:TextBox ID="txttID" Label="登录名" runat="server" Width="200">
            </ext:TextBox>
            <ext:TextBox ID="txttPW" Label="密码" runat="server" TextMode="Password" Width="200">
            </ext:TextBox>--%>
            <ext:DropDownList ID="ddlmode" Label="同步方式" runat="server" Width="100">
                <ext:ListItem Text="自动同步" Value="0" Selected="true" />
                <ext:ListItem Text="手动同步" Value="1" />
            </ext:DropDownList>
            <ext:TextBox ID="time" Label="自动同步时间" runat="server" Width="200">
            </ext:TextBox>
            <ext:Button ID="btnSubmit" ValidateForms="SimpleForm1" CssClass="inline" Text="提交"
                runat="server" OnClick="btnSubmit_Click">
            </ext:Button>
            <ext:Button ID="btnReset" Text="重置" ValidateForms="SimpleForm1" runat="server" 
                CssClass="inline" OnClick="btnReset_Click">
            </ext:Button>
            <ext:Button ID="btnUpdate" ValidateForms="SimpleForm1" CssClass="inline" Text="立即同步"
                runat="server">
            </ext:Button>
        </Items>
    </ext:SimpleForm>
    <ext:Window ID="WindowEdit" Title="用户编辑" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="true" runat="server" OnClose="WindowEdit_Close"
        IsModal="true" Width="365px" EnableConfirmOnClose="true" Height="165px">
    </ext:Window>
    </form>
</body>
</html>
