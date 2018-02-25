<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FunctionPageEdit.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.SystemSet.FunctionPageEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager1" runat="server" />
    <ext:SimpleForm ID="SimpleForm1" BodyPadding="5px" runat="server" EnableBackgroundColor="true"
        ShowBorder="false" ShowHeader="false" Width="300px">
        <Items>
            <ext:TextBox ID="txtModSeq" Label="菜单编号" runat="server" Required="true">
            </ext:TextBox>
            <ext:TextBox ID="txtMCaption" Required="true" Label="菜单名称" runat="server">
            </ext:TextBox>
            <ext:TextBox ID="txtMenuLink" Required="true" Label="功能页地址" runat="server">
            </ext:TextBox>
            <ext:TextBox ID="txtOrderSeq" Required="true" Label="功能分类" runat="server">
            </ext:TextBox>
        </Items>
    </ext:SimpleForm>
    <ext:Form ID="Form2" runat="server" EnableBackgroundColor="True" Width="300px" ShowBorder="false"
        ShowHeader="false">
        <Rows>
            <ext:FormRow ColumnWidths="40% 20% 40%">
                <Items>
                    <ext:Label ID="Label1" runat="server">
                    </ext:Label>
                    <ext:Button ID="btnSubmit" ValidateForms="SimpleForm1" CssClass="inline" Text="提交"
                        runat="server" OnClick="btnSubmit_OnClick">
                    </ext:Button>
                    <ext:Label ID="Label2" runat="server">
                    </ext:Label>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    </form>
</body>
</html>
