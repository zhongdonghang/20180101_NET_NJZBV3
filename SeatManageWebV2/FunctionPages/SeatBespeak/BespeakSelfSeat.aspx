<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BespeakSelfSeat.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.SeatBespeak.BespeakSelfSeat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SeatGraph.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/SeatGraphHandle.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1" />
   


    <ext:ContentPanel ID="SeatTimeSetting" BoxConfigAlign="Center" runat="server" BodyPadding="3px"
            ShowHeader="true" Title="我的座位" EnableBackgroundColor="true" ClientIDMode="Static">
         <table class="borderStyle" >
            <tr>
                <td  width="80">座位</td>
                <td><asp:Label ID="lblSeatInfo" runat="server" Text="无"></asp:Label></td> 
            </tr> 
             <tr>
                <td>状态</td>
                <td><asp:Label ID="lblState" runat="server" Text=""></asp:Label></td> 
                 
            </tr>
             <tr>
                <td title="最后一次刷卡时间">刷卡时间</td>
                <td> <asp:Label ID="lblHoldTime" runat="server" Text=""></asp:Label></td> 
            </tr>
             
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnDelayTime" runat="server" Text="续时"   OnClick="btnDelayTime_OnClick" />
                    <asp:Button ID="btnShortLeave" runat="server" Text="暂时离开" OnClick="btnShortLeave_OnClick" />
                    <asp:Button ID="btnLeave" runat="server" Text="释放座位" OnClick="btnLeave_OnClick" />
                </td> 
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                    
                </td> 
            </tr>
         </table>
    </ext:ContentPanel>
     
    <div>
    </div>
    </form>
</body>
</html>
