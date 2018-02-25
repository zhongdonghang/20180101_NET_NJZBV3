<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoomTripsOutUseInfo.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.Statistical.RoomTripsOutUseInfo" %>

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
            font-family: 黑体;
            font-weight: bold;
            font-size: medium;
        }
    </style>
</head>
<body bgcolor="#DFE8F6">
    <form id="form1" runat="server">
    <table width="800" style="background: #DFE8F6;">
        <tr align="center">
            <td colspan="2">
                <span class="style2" runat="server">阅览室进出人次统计</span>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <span class="style1">查询注意事项：</span><br />
                <span class="style1">1、在未勾选阅览室的情况下查出的数据是整个图书馆的数据</span><br />
                <span class="style1">2、自动排除没有记录的日期，不加入统计。如果只有2个月的数据，时间范围是1年，计算时按照2个月的范围计算</span><br />
                <span class="style1">3、阅览室进出人次统计：</span><br />
                <span class="style1">&nbsp;&nbsp;“周-每天”是按照周为单位统计7天人次波动情况&nbsp;&nbsp;&nbsp;&nbsp;“月-每天”是按照月为单位统计31天人次波动情况</span><br />
                <span class="style1">&nbsp;&nbsp;“年-每天”是按照年为单位统计366天人次波动情况&nbsp;&nbsp;“年-每月”是按照年为单位统计12个月人次波动情况</span><br />
                <span class="style1">&nbsp;&nbsp;“年-每周”是按照年为单位统计53周人次波动情况</span><br />
                <span class="style1">4、勾选多个阅览室可以对多个阅览室进行单独计算并对数据进行对比</span><br />
                <span class="style1">5、阅览室进出人次统计（总数）是统计选定时间范围内的全部进出人次，和统计方式无关</span><br />
                <span class="style1">6、阅览室上座率统计是统计平均每天每个阅览室座位被使用的几率，计算公式为“总进出人次数÷实际统计天数÷统计范围的座位数×100”</span>
            </td>
        </tr>
        <tr align="center">
            <td colspan="2">
                <span id="Span3" class="style4" runat="server">统计条件设置</span>
            </td>
        </tr>
        <tr>
            <td valign="middle" colspan="2">
                <span class="style1">统计日期 从</span><input type="text" id="startdate" onfocus="WdatePicker({startDate:'1900-01-01'})"
                    runat="server" />
                <span class="style1">到</span><input type="text" id="enddate" onfocus="WdatePicker({startDate:'2100-01-01'})"
                    runat="server" />
                <span class="style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 选择图书馆:<asp:DropDownList ID="ddlLibrary"
                    runat="server" Width="150" AutoPostBack="true" OnSelectedIndexChanged="ddlLibrary_SelectedIndexChanged">
                </asp:DropDownList>
                </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btn1" runat="server" Text="开始统计"
                    OnClick="btn1_Click" />
                  <asp:Button
                    ID="btn2" runat="server" Text="导出Excel" OnClick="btn2_Click" />
            </td>
        </tr>
        <tr>
            <td align="right" width="450">
                <span class="style1">统计方式:</span>
            </td>
            <td align="right">
                <asp:RadioButtonList ID="rbltype" runat="server" RepeatDirection="Horizontal" Width="400"
                    Style="font-size: small">
                    <asp:ListItem Value="7" Selected="True">周-每天</asp:ListItem>
                    <asp:ListItem Value="31">月-每天</asp:ListItem>
                    <asp:ListItem Value="366">年-每天</asp:ListItem>
                    <asp:ListItem Value="12">年-每月</asp:ListItem>
                    <asp:ListItem Value="53">年-每周</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td align="right" width="450">
                <span class="style1">统计类型:</span>
            </td>
            <td align="left">
                <asp:RadioButtonList ID="rbleatype" runat="server" RepeatDirection="Horizontal" Width="150"
                    Style="font-size: small">
                    <asp:ListItem Value="EnterOutCount" Selected="True">进出人次</asp:ListItem>
                    <asp:ListItem Value="Attendance">上座率</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:CheckBoxList ID="cblreadingroom" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                    Style="font-size: small">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr align="center">
            <td colspan="2">
                <span class="style3" id="tl" runat="server">阅览室进出人次统计（年-每月）</span>
            </td>
        </tr>
        <tr>
            <td colspan="2">
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
        <tr align="center">
            <td colspan="2">
                <span class="style3" id="Span2" runat="server">阅览室进出人次统计（总数）</span>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Chart ID="libraryEnterOutInfo" runat="server" Height="420px" Width="850px" BackColor="223, 232, 246"
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
        <tr align="center">
            <td colspan="2">
                <span class="style3" id="Span1" runat="server">阅览室上座率统计（平均）</span>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Chart ID="libraryAttendanceInfo" runat="server" Height="420px" Width="850px"
                    BackColor="223, 232, 246" Palette="None" AlternateText="对不起此图书馆暂无数据！">
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
