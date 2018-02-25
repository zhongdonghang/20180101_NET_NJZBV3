<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoomTripsOutInfo.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.Statistical.RoomTripsOutInfo" %>

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
            font-weight: bold;
            font-family: 黑体;
        }
    </style>
</head>
<body bgcolor="#DFE8F6">
    <form id="form1" runat="server">
    <table width="800" style="background: #DFE8F6;">
        <tr align="center">
            <td>
                <span class="style2">阅览室进出人次统计（天-每小时）</span>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <span class="style1">查询注意事项：</span><br />
                <span class="style1">1、在未勾选阅览室的情况下查出的数据是整个图书馆的数据</span><br />
                <span class="style1">2、自动排除没有记录的日期，不加入统计。如果只有2个月的数据，时间范围是1年，计算时按照2个月的范围计算</span><br />
                <span class="style1">3、统计平均每天的每小时进出人次，查看每小时选座或离开的人流量</span><br />
                <span class="style1">4、勾选多个阅览室可以对多个阅览室进行单独计算并对数据进行对比</span><br />
                <span class="style1">5、勾选统计“统计入座人次”或“统计离开人次”分别查看或进行数据对比</span>
            </td>
        </tr>
        <tr align="center">
            <td colspan="2">
                <span id="Span3" class="style3" runat="server">统计条件设置</span>
            </td>
        </tr>
        <tr>
            <td valign="middle">
                <span class="style1">统计日期 从</span><input type="text" id="startdate" onfocus="WdatePicker({startDate:'1900-01-01'})"
                    runat="server" />
                <span class="style1">到</span><input type="text" id="enddate" onfocus="WdatePicker({startDate:'2100-01-01'})"
                    runat="server" />
                <span class="style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 选择图书馆:<asp:DropDownList ID="ddlLibrary"
                    runat="server" Width="150" AutoPostBack="true" OnSelectedIndexChanged="ddlLibrary_SelectedIndexChanged">
                </asp:DropDownList>
                </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                    ID="btn1" runat="server" Text="开始统计" OnClick="btn1_Click" />
                <asp:Button
                    ID="btn2" runat="server" Text="导出Excel" OnClick="btn2_Click" />
            </td>
        </tr>
        <tr>
            <td align="right">
            <asp:CheckBox ID="cbonseat" Text="统计在座人数" runat="server" Checked="true" Style="font-size: small" />
                <asp:CheckBox ID="cbselect" Text="统计入座人次" runat="server" Checked="false" Style="font-size: small" /><asp:CheckBox
                    ID="cbleave" Text="统计离开人次" runat="server" Style="font-size: small" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:CheckBoxList ID="cblreadingroom" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                    Style="font-size: small">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Chart ID="librarySeatUsedInfo" runat="server" Height="420px" Width="850px" BackColor="223, 232, 246"
                    Palette="None" AlternateText="对不起此图书馆暂无数据！">
                    <Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1" IsSameFontSizeForAllAxes="True" BackColor="#DFE8F6">
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
