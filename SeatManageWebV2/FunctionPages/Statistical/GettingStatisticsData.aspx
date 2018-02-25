<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GettingStatisticsData.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.Statistical.GettingStatisticsData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1" />
    <ext:Form EnableBackgroundColor="true" BodyPadding="5px" ID="Form2" runat="server"
        Title="查询条件" LabelWidth="80px">
        <Rows>
            <ext:FormRow>
                <Items>
                    <ext:Label ID="lbmessage" runat="server" Text="正在准备获取数据……">
                    </ext:Label>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    </form>
</body>
</html>
