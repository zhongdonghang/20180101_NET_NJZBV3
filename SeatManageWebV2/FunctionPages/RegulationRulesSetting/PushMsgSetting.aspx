<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PushMsgSetting.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.RegulationRulesSetting.PushMsgSetting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:PageManager ID="PageManager1" runat="server" />
        <ext:SimpleForm ID="SimpleForm1" BodyPadding="5px" runat="server" EnableBackgroundColor="True"
            ShowBorder="false" ShowHeader="True" Width="400px" Title="短消息设置" LabelAlign="Right"
            LabelWidth="200">
            <Items>
                 <ext:CheckBox ID="cb_UserOperation" Label="记录读者自己操作消息" Text="启用" runat="server">
                </ext:CheckBox>
                <ext:CheckBox ID="cb_AdminOperation" Label="记录被管理员操作的消息" Text="启用" runat="server">
                </ext:CheckBox>
                <ext:CheckBox ID="cb_OtherUser" Label="记录被其他读者操作的消息" Text="启用" runat="server">
                </ext:CheckBox>
                <ext:CheckBox ID="cb_EnterVr" Label="记录违规消息" Text="启用" runat="server">
                </ext:CheckBox>
                   <ext:CheckBox ID="cb_EnterBlack" Label="记录黑名单消息" Text="启用" runat="server">
                </ext:CheckBox>
                <ext:CheckBox ID="cb_LeaveVrBlack" Label="记录消除违规和离开黑名单的消息" Text="启用" runat="server">
                </ext:CheckBox>
                <ext:CheckBox ID="cb_TimeOut" Label="记录超时的消息" Text="启用" runat="server">
                </ext:CheckBox>
            </Items>
        </ext:SimpleForm>
        <ext:Form ID="Form2" runat="server" EnableBackgroundColor="True" Width="400px" ShowBorder="false"
            ShowHeader="false">
            <Rows>
                <ext:FormRow ColumnWidths="40% 20% 40%">
                    <Items>
                        <ext:Label ID="Label5" runat="server">
                        </ext:Label>
                        <ext:Button ID="btnSubmit" ValidateForms="SimpleForm1" CssClass="inline" Text="&nbsp;&nbsp;提&nbsp;交&nbsp;&nbsp;"
                            runat="server" OnClick="btnSubmit_Click">
                        </ext:Button>
                        <ext:Label ID="Label4" runat="server">
                        </ext:Label>
                    </Items>
                </ext:FormRow>
            </Rows>
        </ext:Form>
    </form>
</body>
</html>
