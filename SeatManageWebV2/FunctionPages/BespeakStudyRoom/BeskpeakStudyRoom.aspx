<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BeskpeakStudyRoom.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.BespeakStudyRoom.BeskpeakStudyRoom" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager1" runat="server" />
    <ext:Form runat="server" ID="remarkform" ShowHeader="false" ShowBorder="false" Width="720px"
        EnableBackgroundColor="true">
        <Rows>
            <ext:FormRow ColumnWidths="200px 500px">
                <Items>
                    <ext:SimpleForm ID="SimpleForm6" BodyPadding="5px" runat="server" EnableBackgroundColor="true"
                        ShowBorder="false" LabelWidth="0px" LabelAlign="Right" ShowHeader="false" Width="200px">
                        <Items>
                            <ext:Label ID="Label7" runat="server" Text="研习间图片" ShowLabel="false">
                            </ext:Label>
                            <ext:Image ID="imgRoomImage" ImageHeight="120px" runat="server" ImageWidth="180px"
                                ShowLabel="false">
                            </ext:Image>
                            <ext:Button ID="btnImage" CssClass="inline" Text="&nbsp;&nbsp;&nbsp;点击看大图&nbsp;&nbsp;" runat="server">
                            </ext:Button>
                            <ext:Button ID="btnApp" CssClass="inline" Text="&nbsp;&nbsp;&nbsp;研习间申请&nbsp;&nbsp;" runat="server" OnClick="btnApp_OnClick">
                            </ext:Button>
                        </Items>
                    </ext:SimpleForm>
                    <ext:SimpleForm ID="SimpleForm5" BodyPadding="5px" runat="server" EnableBackgroundColor="true"
                        ShowBorder="false" LabelWidth="75px" LabelAlign="Right" ShowHeader="false" Width="500px">
                        <Items>
                            <ext:ContentPanel BoxConfigAlign="Center" runat="server" BodyPadding="3px" ShowHeader="false"
                                ShowBorder="false" EnableBackgroundColor="true" ClientIDMode="Static" ID="ContentPanel3">
                                <table>
                                    <tr>
                                        <td width="70px" valign="Top">
                                            设施描述:
                                        </td>
                                        <td width="430px" id="txtFacilitiesRenmark" runat="server">
                                        </td>
                                    </tr>
                                </table>
                            </ext:ContentPanel>
                            <ext:ContentPanel BoxConfigAlign="Center" runat="server" BodyPadding="3px" ShowHeader="false"
                                ShowBorder="false" EnableBackgroundColor="true" ClientIDMode="Static" ID="ContentPanel1">
                                <table>
                                    <tr>
                                        <td width="70px" valign="Top">
                                            注意事项:
                                        </td>
                                        <td width="430px" id="txtPrecautions" runat="server">
                                        </td>
                                    </tr>
                                </table>
                            </ext:ContentPanel>
                            <ext:ContentPanel BoxConfigAlign="Center" runat="server" BodyPadding="3px" ShowHeader="false"
                                ShowBorder="false" EnableBackgroundColor="true" ClientIDMode="Static" ID="ContentPanel2">
                                <table>
                                    <tr>
                                        <td width="70px" valign="Top">
                                            申请说明:
                                        </td>
                                        <td width="430px" id="txtApplicationInfo" runat="server">
                                        </td>
                                    </tr>
                                </table>
                            </ext:ContentPanel>
                        </Items>
                    </ext:SimpleForm>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:Form runat="server" ID="extform" ShowHeader="false" ShowBorder="false" Width="720px"
        EnableBackgroundColor="true">
        <Rows>
            <ext:FormRow ColumnWidths="50% 50%">
                <Items>
                    <ext:SimpleForm ID="SimpleForm2" BodyPadding="5px" runat="server" EnableBackgroundColor="true"
                        ShowBorder="false" LabelWidth="150px" LabelAlign="Right" ShowHeader="false" Width="350px">
                        <Items>
                            <ext:Label ID="Label4" runat="server" Label="申请人信息">
                            </ext:Label>
                            <ext:TextBox ID="txtApplicantName" Required="true" Label="申请人姓名" runat="server">
                            </ext:TextBox>
                            <ext:TextBox ID="txtApplicantType" Required="true" Label="申请人类别/职称" runat="server"
                                Width="100px">
                            </ext:TextBox>
                            <ext:TextBox ID="txtApplicantCardNo" Required="true" Label="有效证件号码" runat="server">
                            </ext:TextBox>
                            <ext:TextBox ID="txtApplicantDept" Required="true" Label="申请人单位" runat="server">
                            </ext:TextBox>
                            <ext:TextBox ID="txtApplicantPhoneNum" Required="true" Label="联系电话" runat="server">
                            </ext:TextBox>
                        </Items>
                    </ext:SimpleForm>
                    <ext:SimpleForm ID="SimpleForm1" BodyPadding="5px" runat="server" EnableBackgroundColor="true"
                        ShowBorder="false" LabelWidth="150px" LabelAlign="Right" ShowHeader="false" Width="350px">
                        <Items>
                            <ext:Label ID="Label3" runat="server" Label="负责人信息">
                            </ext:Label>
                            <ext:TextBox ID="txtHeadPerson" Required="true" Label="负责人姓名" runat="server">
                            </ext:TextBox>
                            <ext:TextBox ID="txtHeadPersonType" Required="true" Label="负责人类别/职称" runat="server">
                            </ext:TextBox>
                            <ext:TextBox ID="txtHeadPersonPhoneNum" Required="true" Label="联系电话" runat="server">
                            </ext:TextBox>
                        </Items>
                    </ext:SimpleForm>
                </Items>
            </ext:FormRow>
            <ext:FormRow>
                <Items>
                    <ext:SimpleForm ID="SimpleForm3" BodyPadding="5px" runat="server" EnableBackgroundColor="true"
                        ShowBorder="false" LabelWidth="150px" LabelAlign="Right" ShowHeader="false" Width="350px">
                        <Items>
                            <ext:Label ID="Label5" runat="server" Label="申请内容">
                            </ext:Label>
                            <ext:TextBox ID="txtMeetingName" Required="true" Label="学术会议名称" runat="server">
                            </ext:TextBox>
                            <ext:NumberBox ID="txtMembersCount" Required="true" Label="参加人数" runat="server" Width="50px">
                            </ext:NumberBox>
                            <ext:CheckBoxList ID="cbUse" runat="server" AutoPostBack="true" ColumnNumber="3"
                                Label="需求设备">
                            </ext:CheckBoxList>
                        </Items>
                    </ext:SimpleForm>
                    <ext:SimpleForm ID="SimpleForm4" BodyPadding="5px" runat="server" EnableBackgroundColor="true"
                        ShowBorder="false" LabelWidth="150px" LabelAlign="Right" ShowHeader="false" Width="350px">
                        <Items>
                            <ext:Label ID="Label6" runat="server" Label="使用时间">
                            </ext:Label>
                            <ext:DatePicker runat="server" ID="dpBookingDate" Label="预约日期">
                            </ext:DatePicker>
                            <ext:TimePicker runat="server" ID="tpBookingTime" Label="预约时间" Increment="10">
                            </ext:TimePicker>
                            <ext:NumberBox runat="server" ID="nbUseTime" Label="使用时长（分钟）" Text="60">
                            </ext:NumberBox>
                            <ext:TextBox runat="server" ID="txtemail" Label="消息接收邮箱" EmptyText="用于发送审核消息">
                            </ext:TextBox>
                        </Items>
                    </ext:SimpleForm>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowHeader="false"
        Width="720px" ShowBorder="false">
        <Rows>
            <ext:FormRow ColumnWidths="41% 18% 41%">
                <Items>
                    <ext:Label ID="Label1" runat="server">
                    </ext:Label>
                    <ext:Button ID="btnSubmit" ValidateForms="extform" CssClass="inline" Text="提&nbsp;&nbsp;交"
                        runat="server" OnClick="btnSubmit_OnClick">
                    </ext:Button>
                    <ext:Label ID="Label2" runat="server">
                    </ext:Label>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:Window ID="WindowImage" Title="查看大图" Popup="false" EnableIFrame="true" IFrameUrl="about:blank"
        EnableMaximize="true" Target="Top" EnableResize="false" runat="server" CloseAction="HidePostBack"
        EnableClose="true" IsModal="true" Width="660px" EnableConfirmOnClose="true" Height="520px">
    </ext:Window>
    </form>
</body>
</html>
