<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LibraryUsedStatistical.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.Statistical.LibraryUsedStatistical" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            font-size: small;
        }
        .style2
        {
            font-family: 黑体;
            font-weight: bold;
            font-size: x-large;
        }
    </style>
</head>
<body bgcolor="#DFE8F6">
    <form id="form1" runat="server">
    <table width="800" style="background: #DFE8F6;">
        <tr align="center">
            <td>
                <span class="style2">欢迎使用图书馆精细化管理平台</span>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr align="right" valign="middle">
            <td>
                <span class="style1">选择图书馆:</span><asp:DropDownList ID="ddlLibrary" runat="server"
                    Width="150" AutoPostBack="true" OnSelectedIndexChanged="ddlLibrary_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Button ID="btn1" runat="server" OnClick="btn1_Click" Text="刷新" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Chart ID="librarySeatUsedInfo" runat="server" Height="420px" Width="799px" 
                    BackColor="#DFE8F6" AlternateText="对不起此图书馆暂无数据！">
                    <Series>
                        <asp:Series Name="SeatAmount" IsValueShownAsLabel="true" ChartArea="ChartArea1" LegendText="座位总数"
                            Legend="Legend1">
                        </asp:Series>
                        <asp:Series Name="SeatUsedAmount" IsValueShownAsLabel="true" ChartArea="ChartArea1"
                            LegendText="正在使用" Legend="Legend1">
                        </asp:Series>
                        <asp:Series Name="PersonTimes" IsValueShownAsLabel="true" ChartArea="ChartArea1"
                            LegendText="进出人次" Legend="Legend1">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1" IsSameFontSizeForAllAxes="True" BackColor="#DFE8F6">
                            <AxisX TextOrientation="Rotated90">
                            </AxisX>
                            <AxisX2 TextOrientation="Rotated90">
                            </AxisX2>
                        </asp:ChartArea>
                    </ChartAreas>
                    <Legends>
                        <asp:Legend Name="Legend1" Title="图例" Alignment="Near" Docking="Top" IsDockedInsideChartArea="False" BackColor="#DFE8F6">
                        </asp:Legend>
                    </Legends>
                    <Titles>
                        <asp:Title Name="titleName" Text="座位使用信息">
                        </asp:Title>
                    </Titles>
                </asp:Chart>
            </td>
        </tr>
    </table>
    <div>
    </div>
    </form>
</body>
</html>
