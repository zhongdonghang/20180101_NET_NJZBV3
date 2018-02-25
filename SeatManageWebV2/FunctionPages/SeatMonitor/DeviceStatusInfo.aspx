<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeviceStatusInfo.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.RegulationRulesSetting.DeviceStatusInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager1" runat="server" />
         <ext:Form ID="Form2" runat="server" EnableBackgroundColor="false" ShowHeader="false"
        Width="300px" ShowBorder="false">
        <Rows>
            <ext:FormRow>
                <Items>
                    <ext:Button ID="btnbinding" Type="Button" Text="刷新列表" runat="server" CssClass="inline" OnClick="btnbinding_OnClick">
                    </ext:Button>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:Grid ID="GridDevice" ShowBorder="true" ShowHeader="true" AutoHeight="true" AllowPaging="true"
        runat="server" Title="设备状态列表" AutoScroll="true" EnableBackgroundColor="true" PageSize="10"
        EnableRowNumber="true">
        <Columns>
            <ext:BoundField Width="100px" DataField="DeviceNum" DataFormatString="{0}" HeaderText="设备编号"
                SortField="DeviceNum" />
            <ext:BoundField Width="100px" DataField="Describe" DataFormatString="{0}" HeaderText="设备备注"
                SortField="Describe" />
            <ext:BoundField Width="200px" DataField="PrintedTimes" DataFormatString="{0}" HeaderText="已打印次数"
                SortField="PrintedTimes" />
            <ext:BoundField Width="150px" DataField="LastPrintTimes" DataFormatString="{0}" HeaderText="上一卷打印总数"
                SortField="LastPrintTimes" />
            <ext:BoundField Width="150px" DataField="Date" DataFormatString="{0}" HeaderText="客户端连接服务器时间"
                SortField="Date" />
            <ext:BoundField Width="150px" DataField="PrinterStatus" DataFormatString="{0}" HeaderText="打印机状态"
                SortField="PrinterStatus" />
        </Columns>
    </ext:Grid>
    </form>
</body>
</html>
