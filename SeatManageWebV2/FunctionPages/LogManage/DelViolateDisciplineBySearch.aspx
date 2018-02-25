<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DelViolateDisciplineBySearch.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.LogManage.DelViolateDisciplineBySearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager1" runat="server" />
    <ext:SimpleForm ID="SimpleForm1" BodyPadding="5px" runat="server" EnableBackgroundColor="True"
        ShowBorder="false" Width="350px" Title="根据条件删除违规记录" ShowHeader="false">
        <Items>
            <ext:DropDownList ID="ddlReadingRoom" runat="server" Width="150" Label="阅览室" ShowLabel="True">
            </ext:DropDownList>
            <ext:DatePicker runat="server" Width="150" Label="开始日期" EmptyText="请选择开始日期" ID="dpStartDate">
            </ext:DatePicker>
            <ext:DatePicker ID="dpEndDate" Width="150" EmptyText="请选择结束日期" Label="结束日期" CompareControl="dpStartDate"
                CompareOperator="GreaterThanEqual" CompareMessage="结束日期应该大于开始日期" runat="server">
            </ext:DatePicker>
        </Items>
    </ext:SimpleForm>
    <ext:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowHeader="false"
        Width="350px" ShowBorder="false">
        <Rows>
            <ext:FormRow ColumnWidths="41% 18% 41%">
                <Items>
                    <ext:Label ID="Label1" runat="server">
                    </ext:Label>
                    <ext:Button ID="btnDel" ValidateForms="SimpleForm1" CssClass="inline" Text="删除" runat="server"
                        OnClick="btnDel_Click">
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
