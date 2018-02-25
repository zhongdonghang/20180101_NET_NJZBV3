<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeviceInfo.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.RegulationRulesSetting.DeviceInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.4.1.js">
    </script>
    <script type="text/javascript" language="javascript">
        function CBcheckALL(id) {
            if ($("#" + id + "")[0].checked == true) {
                for (var i = 0; i < 100; i++) {
                    $("#cblTerm_" + i).attr("checked", true);
                }
            }
            else {
                for (var i = 0; i < 100; i++) {
                    $("#cblTerm_" + i).attr("checked", false);
                }
            }
        }
    </script>
    <style>
        .lblRed {
            color: Red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1"
            HideScrollbar="true" />
        <ext:RegionPanel ID="RegionPanel_1" runat="server" ShowBorder="false" EnableBackgroundColor="true">
            <Regions>
                <ext:Region ID="Region_Left" Split="true" EnableSplitTip="true" CollapseMode="Mini"
                    Width="200px" Margins="0 0 0 0" ShowHeader="true" Icon="Outline" EnableCollapse="true"
                    Layout="Fit" Position="Left" runat="server" Title="终&nbsp;端&nbsp;设&nbsp;备&nbsp;列&nbsp;表">
                    <Items>
                        <ext:SimpleForm ID="SimpleForm2" BodyPadding="5px" runat="server" EnableBackgroundColor="true"
                            ShowBorder="false" ShowHeader="false" Width="300px">
                            <Items>
                                <ext:Tree ID="treeMenu" runat="server" ShowBorder="false" ShowHeader="false" EnableArrows="true"
                                    EnableBackgroundColor="true" AutoScroll="true" CssStyle="font-size:18px" Collapsed="false"
                                    OnNodeCommand="Tree_NodeCommand">
                                </ext:Tree>
                            </Items>
                        </ext:SimpleForm>
                    </Items>
                </ext:Region>
                <ext:Region ID="MainRegion" ShowHeader="false" Layout="Fit" Margins="0 0 0 0" Position="Center"
                    runat="server" EnableBackgroundColor="true" AutoScroll="true">
                    <Items>
                        <ext:Form Width="700px" runat="server" LabelWidth="160px" EnableBackgroundColor="true"
                            AutoScroll="true" BodyPadding="5px" ShowBorder="True" ShowHeader="false" LabelAlign="Right">
                            <Rows>
                                <ext:FormRow>
                                    <Items>
                                        <ext:Label ID="lbno" Label="设备编号" runat="server">
                                        </ext:Label>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:TextBox ID="txtRemark" Label="备注" runat="server" CssClass="lable" Width="300">
                                        </ext:TextBox>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:RadioButtonList ID="rblSelectSeatMode" Label="选座方式设置" runat="server" Width="300">
                                            <ext:RadioItem Text="默认" Value="3" />
                                            <ext:RadioItem Text="自选选座" Value="2" />
                                            <ext:RadioItem Text="手动选座" Value="1" />
                                            <ext:RadioItem Text="自动选座" Value="0" />
                                        </ext:RadioButtonList>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:CheckBox ID="cbSelectSeatCount" runat="server" Text="启用" Checked="true" Label="启用选座次数限制" AutoPostBack="true">
                                        </ext:CheckBox>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:NumberBox ID="numSelectSeatTime" Label="范围时间(分钟)" runat="server" Width="100">
                                        </ext:NumberBox>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:NumberBox ID="numSelectSeatCont" Label="最多次数(次)" runat="server" Width="100">
                                        </ext:NumberBox>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:CheckBox ID="cbOftenSeat" runat="server" Checked="true" Text="启用" Label="常坐座位"
                                            OnCheckedChanged="OnCheckedChanged" AutoPostBack="true">
                                        </ext:CheckBox>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:NumberBox ID="nbostime" Label="范围时间(天)" runat="server" Width="100">
                                        </ext:NumberBox>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:NumberBox ID="nboscont" Label="统计个数(个)" runat="server" Width="100">
                                        </ext:NumberBox>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:RadioButtonList ID="rbprint" Label="打印凭条设置" runat="server" Width="300">
                                            <ext:RadioItem Text="用户自选" Value="2" />
                                            <ext:RadioItem Text="自动打印" Value="1" />
                                            <ext:RadioItem Text="不打印" Value="0" />
                                        </ext:RadioButtonList>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:CheckBox ID="cbBespeak" runat="server" Text="启用" Checked="true" Label="预约激活">
                                        </ext:CheckBox>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:CheckBox ID="cbNumSelect" runat="server" Text="启用" Checked="true" Label="输号选座">
                                        </ext:CheckBox>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:CheckBox ID="cbipos" runat="server" Text="启用" Checked="true" Label="初始化读卡器">
                                        </ext:CheckBox>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:RadioButtonList ID="rblfbl" Label="分辨率选择" runat="server" Width="550">
                                            <ext:RadioItem Text="1080x1000" Value="1080" />
                                            <ext:RadioItem Text="1024x768" Value="1024" />
                                            <ext:RadioItem Text="1280x800" Value="1280" />
                                            <ext:RadioItem Text="1440x900" Value="1280" />
                                            <ext:RadioItem Text="1920x1080" Value="1920" />
                                            <ext:RadioItem Text="自定义" Value="0" />
                                        </ext:RadioButtonList>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:TextBox ID="txtReDiy" Label="自定义分辨率" runat="server" CssClass="lable" Width="100">
                                        </ext:TextBox>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:Label ID="Label2" runat="server">
                                        </ext:Label>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:CheckBox Label="阅览室管理" Text="选中全部阅览室" runat="server" ID="cbselectallrr" OnCheckedChanged="cbselectallrr_CheckedChanged"
                                            AutoPostBack="true" Checked="false">
                                        </ext:CheckBox>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:CheckBoxList ID="clbroom" runat="server" ColumnNumber="3" AutoPostBack="false"
                                            AjaxLoadingType="Mask" Width="750" ColumnWidth="250px">
                                        </ext:CheckBoxList>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:Label ID="Label4" runat="server">
                                        </ext:Label>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:CheckBox Label="应用到其他设备" Text="选中全部设备" runat="server" ID="SelesctALLtem" OnCheckedChanged="SelesctALLtem_CheckedChanged"
                                            AutoPostBack="true" Checked="false">
                                        </ext:CheckBox>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:CheckBoxList ID="cblTerm" runat="server" ColumnNumber="3" AjaxLoadingType="Mask"
                                            Width="750">
                                        </ext:CheckBoxList>
                                    </Items>
                                </ext:FormRow>
                                <ext:FormRow>
                                    <Items>
                                        <ext:Label runat="server">
                                        </ext:Label>
                                        <ext:Button ID="btnSubmit" ValidateForms="SimpleForm1" CssClass="inline" Text="&nbsp;&nbsp;提&nbsp;交&nbsp;&nbsp;"
                                            runat="server" OnClick="btnSubmit_Click">
                                        </ext:Button>
                                        <ext:Label ID="Label3" runat="server">
                                        </ext:Label>
                                    </Items>
                                </ext:FormRow>
                            </Rows>
                        </ext:Form>
                    </Items>
                </ext:Region>
            </Regions>
        </ext:RegionPanel>
    </form>
</body>
</html>
