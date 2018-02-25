<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoomSeatUseInfo.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.Statistical.RoomSeatUseInfo" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../../Scripts/My97DatePicker/WdatePicker.js"></script>
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
        .style3
        {
            font-family: 黑体;
            font-weight: bold;
            font-size: large;
        }
        .style4
        {
            font-weight: bold;
            font-family: 黑体;
        }
    </style>
</head>
<body bgcolor="#DFE8F6">
    <form id="form1" runat="server">
    <table width="800" style="background: #DFE8F6;">
        <tr align="center">
            <td colspan="3">
                <span class="style2">座位使用情况统计</span>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <span class="style1">查询注意事项：</span><br />
                <span class="style1">1、在未勾选阅览室的情况下查出的数据是整个图书馆的数据</span><br />
                <span class="style1">2、勾选多个阅览室可以对多个阅览室进行计算</span><br />
                <span class="style1">3、选座方式统计：统计时间段内读者用不同的方式选座的次数以及比例</span><br />
                <span class="style1">3、离开方式统计：统计时间段内读者用不同的方式释放座位的次数以及比例</span><br />
                <span class="style1">3、在座时长统计：统计时间段内读者在座时长以及比例</span><br />
            </td>
        </tr>
        <tr align="center">
            <td colspan="3">
                <span id="Span3" class="style4" runat="server">统计条件设置</span>
            </td>
        </tr>
        <tr>
            <td valign="middle" colspan="3">
                <span class="style1">统计日期 从</span><input type="text" id="startdate" onfocus="WdatePicker({startDate:'1900-01-01'})"
                    runat="server" />
                <span class="style1">到</span><input type="text" id="enddate" onfocus="WdatePicker({startDate:'2100-01-01'})"
                    runat="server" />
                <span class="style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 选择图书馆:<asp:DropDownList ID="ddlLibrary"
                    runat="server" Width="150" AutoPostBack="true" OnSelectedIndexChanged="ddlLibrary_SelectedIndexChanged">
                </asp:DropDownList>
                </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btn1" runat="server" Text="开始统计"
                    OnClick="btn1_Click" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                <asp:CheckBoxList ID="cblreadingroom" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                    Style="font-size: small">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td align="center" class="style3">
                选座方式统计
            </td>
            <td align="center" class="style3">
                离开方式统计
            </td>
            <td align="center" class="style3">
                在座时长统计
            </td>
        </tr>
        <tr>
            <td>
                <asp:Chart ID="Chart1" runat="server" Height="350px" Width="320px" BackColor="223, 232, 246"
                    Palette="None" AlternateText="对不起此图书馆暂无数据！">
                    <Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1" IsSameFontSizeForAllAxes="True" BackColor="223, 232, 246">
                            <AxisY>
                            </AxisY>
                            <AxisX TextOrientation="Rotated90">
                            </AxisX>
                            <AxisX2 TextOrientation="Rotated90">
                            </AxisX2>
                        </asp:ChartArea>
                    </ChartAreas>
                    <Legends>
                        <asp:Legend Name="Legend1" Title="图例" Docking="Top" IsDockedInsideChartArea="False"
                            BackColor="223, 232, 246">
                        </asp:Legend>
                    </Legends>
                </asp:Chart>
            </td>
            <td>
                <asp:Chart ID="Chart2" runat="server" Height="350px" Width="320px" BackColor="223, 232, 246"
                    Palette="None" AlternateText="对不起此图书馆暂无数据！">
                    <Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1" IsSameFontSizeForAllAxes="True" BackColor="223, 232, 246">
                            <AxisY>
                            </AxisY>
                            <AxisX TextOrientation="Rotated90">
                            </AxisX>
                            <AxisX2 TextOrientation="Rotated90">
                            </AxisX2>
                        </asp:ChartArea>
                    </ChartAreas>
                    <Legends>
                        <asp:Legend Name="Legend1" Title="图例" Docking="Top" IsDockedInsideChartArea="False"
                            BackColor="223, 232, 246">
                        </asp:Legend>
                    </Legends>
                </asp:Chart>
            </td>
            <td colspan="2">
                <asp:Chart ID="Chart3" runat="server" Height="350px" Width="320px" BackColor="223, 232, 246"
                    Palette="None" AlternateText="对不起此图书馆暂无数据！">
                    <Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1" IsSameFontSizeForAllAxes="True" BackColor="223, 232, 246">
                            <AxisY>
                            </AxisY>
                            <AxisX TextOrientation="Rotated90">
                            </AxisX>
                            <AxisX2 TextOrientation="Rotated90">
                            </AxisX2>
                        </asp:ChartArea>
                    </ChartAreas>
                    <Legends>
                        <asp:Legend Name="Legend1" Title="图例" Docking="Top" IsDockedInsideChartArea="False"
                            BackColor="223, 232, 246">
                        </asp:Legend>
                    </Legends>
                </asp:Chart>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
