<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeletePassword.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.SystemSet.DeletePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager1" runat="server" />
    <ext:Form ID="Form3" runat="server" EnableBackgroundColor="true" ShowHeader="false"
        Width="380px" ShowBorder="false" LabelWidth="0px">
        <Rows>
            <ext:FormRow>
                <Items>
                    <ext:Label Text="此操作会造成系统信息丢失，请慎重考虑" runat="server">
                    </ext:Label>
                </Items>
            </ext:FormRow>
            <ext:FormRow>
                <Items>
                    <ext:Label Text="为了避免误删除请输入删除密码" runat="server">
                    </ext:Label>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:SimpleForm ID="SimpleForm1" BodyPadding="5px" runat="server" EnableBackgroundColor="True"
        ShowBorder="false" Width="380px" Title="删除密码确认" ShowHeader="false">
        <Items>
            <ext:TextBox ID="txtpw1" Required="true" Label="输入密码" runat="server" TextMode="Password"
                Width="200">
            </ext:TextBox>
            <ext:TextBox ID="txtpw2" Required="true" Label="再次输入密码" runat="server" TextMode="Password"
                Width="200">
            </ext:TextBox>
        </Items>
    </ext:SimpleForm>
    <ext:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowHeader="false"
        Width="380px" ShowBorder="false">
        <Rows>
            <ext:FormRow ColumnWidths="41% 18% 41%">
                <Items>
                    <ext:Label ID="Label1" runat="server">
                    </ext:Label>
                    <ext:Button ID="Button1" ValidateForms="SimpleForm1" CssClass="inline" Text="确认"
                        runat="server" EnablePostBack="true" OnClick="btnSubmit_Click" ConfirmText="确认要进行删除操作码？删除后数据不可恢复"
                        ConfirmTarget="Top" >
                    </ext:Button>
                    <ext:Label ID="Label2" runat="server">
                    </ext:Label>
                </Items>
            </ext:FormRow>
            <ext:FormRow>
                <Items>
                    <ext:Label ID="Label3" runat="server">
                    </ext:Label>
                </Items>
            </ext:FormRow>
            <ext:FormRow>
                <Items>
                    <ext:Label ID="Label4" runat="server">
                    </ext:Label>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    </form>
</body>
</html>
