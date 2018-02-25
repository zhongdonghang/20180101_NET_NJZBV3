<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlackListDelete.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.LogManage.BlackListDelete" %>

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
        ShowBorder="false" ShowHeader="false" Width="315px" Title="编辑内容" Height="100px">
        <Items>
        <ext:Label ID="Label3" runat="server">
                    </ext:Label>
            <ext:DatePicker runat="server" Width="150" Label="删除起始日期" EmptyText="请选择开始日期" Readonly="false"
                ID="dpStartDate">
            </ext:DatePicker>
            <ext:Label ID="Label4" runat="server">
                    </ext:Label>
            <ext:DatePicker ID="dpEndDate" Width="150" Readonly="false" DateFormatString="yyyy-MM-dd"
                CompareControl="dpStartDate" CompareOperator="GreaterThanEqual" CompareMessage="结束日期应该大于开始日期"
                EmptyText="请选择结束日期" Label="删除结束日期" runat="server">
            </ext:DatePicker>
        </Items>
    </ext:SimpleForm>
    <ext:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowHeader="false"
        Width="315px" ShowBorder="false" Height="50px">
        <Rows>
            <ext:FormRow ColumnWidths="41% 18% 41%">
                <Items>
                    <ext:Label ID="Label1" runat="server">
                    </ext:Label>
                    <ext:Button ID="btnSubmit" ValidateForms="SimpleForm1" CssClass="inline" Text="确认"
                        runat="server" OnClick="btnSubmit_Click" ConfirmText="你确定要清除这个时间段的黑名单？">
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
