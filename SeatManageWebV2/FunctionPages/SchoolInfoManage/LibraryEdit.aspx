<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LibraryEdit.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.SchoolInfoManage.LibraryEdit" %>

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
        ShowBorder="false" ShowHeader="false" Width="300px" Title="编辑内容">
        <Items>
            <ext:DropDownList runat="server" ID="ddlschool" Label="校区选择">
            </ext:DropDownList>
            <ext:TextBox ID="txtLibraryNo" Required="true" Label="图书馆编号" runat="server" MaxLength="2">
            </ext:TextBox>
            <ext:TextBox ID="txtLibraryName" Required="true" Label="图书馆名称" runat="server">
            </ext:TextBox>
            <ext:TextArea ID="txtArea" Label="图书馆区域" runat="server" EmptyText="区域之间请用‘;’号分割">
            </ext:TextArea>
        </Items>
    </ext:SimpleForm>
    <ext:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowHeader="false"
        Width="300px" ShowBorder="false">
        <Rows>
            <ext:FormRow ColumnWidths="41% 18% 41%">
                <Items>
                    <ext:Label ID="Label1" runat="server">
                    </ext:Label>
                    <ext:Button ID="btnSubmit" ValidateForms="SimpleForm1" CssClass="inline" Text="提交"
                        runat="server" OnClick="btnSubmit_Click">
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
