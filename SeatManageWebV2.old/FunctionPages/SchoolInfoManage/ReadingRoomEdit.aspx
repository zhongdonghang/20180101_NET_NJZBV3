<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReadingRoomEdit.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.SchoolInfoManage.ReadingRoomEdit" %>

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
            <ext:DropDownList ID="ddlLibrary" Label="所属图书馆" runat="server" OnSelectedIndexChanged="ddlLibrary_OnSelectedIndexChanged" AutoPostBack="true">
            </ext:DropDownList>
            <ext:DropDownList ID="ddlArea" Label="所属区域" runat="server">
            </ext:DropDownList>
            <ext:TextBox ID="txtReadRoomNo" Required="true" Label="阅览室编号" runat="server">
            </ext:TextBox>
            <ext:TextBox ID="txtReadRoomName" Required="true" Label="阅览室名称" runat="server">
            </ext:TextBox>
            <ext:Label ID="Label1" runat="server" Text="">
            </ext:Label>
        </Items>
    </ext:SimpleForm>
    <ext:Form runat="server" EnableBackgroundColor=true ShowHeader=false Width="300px" ShowBorder="false">
        <Rows>
            <ext:FormRow ColumnWidths="41% 18% 41%">
                <Items>
                <ext:Label runat=server></ext:Label>
                    <ext:Button ID="btnSubmit" ValidateForms="SimpleForm1" CssClass="inline" Text="提交"
                        runat="server" OnClick="btnSubmit_OnClick">
                    </ext:Button>
                    <ext:Label ID="Label2" runat=server></ext:Label>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    </form>
</body>
</html>
