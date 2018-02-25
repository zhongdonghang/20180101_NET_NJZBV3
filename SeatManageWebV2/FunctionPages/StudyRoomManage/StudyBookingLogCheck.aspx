<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudyBookingLogCheck.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.StudyRoomManage.StudyBookingLogCheck" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager1" runat="server" />
    <ext:Form runat="server" ID="extform" ShowHeader="false" ShowBorder="false" Width="720px"
        EnableBackgroundColor="true">
        <Rows>
            <ext:FormRow ColumnWidths="50% 50%">
                <Items>
                    <ext:SimpleForm ID="SimpleForm2" BodyPadding="5px" runat="server" EnableBackgroundColor="true"
                        ShowBorder="false" LabelWidth="150px" LabelAlign="Right" ShowHeader="false" Width="350px">
                        <Items>
                            <ext:Label ID="Label1" runat="server" Label="申请人信息">
                            </ext:Label>
                            <ext:Label ID="txtApplicantName" Label="申请人姓名" runat="server" Width="100px">
                            </ext:Label>
                            <ext:Label ID="txtApplicantType" Label="申请人类别/职称" runat="server" Width="100px">
                            </ext:Label>
                            <ext:Label ID="txtApplicantCardNo" Label="有效证件号码" runat="server">
                            </ext:Label>
                            <ext:Label ID="txtApplicantDept" Label="申请人单位" runat="server" Width="100px">
                            </ext:Label>
                            <ext:Label ID="txtApplicantPhoneNum" Label="联系电话" runat="server" Width="150px">
                            </ext:Label>
                        </Items>
                    </ext:SimpleForm>
                    <ext:SimpleForm ID="SimpleForm1" BodyPadding="5px" runat="server" EnableBackgroundColor="true"
                        ShowBorder="false" LabelWidth="150px" LabelAlign="Right" ShowHeader="false" Width="350px">
                        <Items>
                            <ext:Label ID="Label2" runat="server" Label="负责人信息">
                            </ext:Label>
                            <ext:Label ID="txtHeadPerson" Label="负责人姓名" runat="server" Width="100px">
                            </ext:Label>
                            <ext:Label ID="txtHeadPersonType" Label="负责人类别/职称" runat="server" Width="100px">
                            </ext:Label>
                            <ext:Label ID="txtHeadPersonPhoneNum" Label="联系电话" runat="server" Width="150px">
                            </ext:Label>
                        </Items>
                    </ext:SimpleForm>
                </Items>
            </ext:FormRow>
            <ext:FormRow>
                <Items>
                    <ext:SimpleForm ID="SimpleForm3" BodyPadding="5px" runat="server" EnableBackgroundColor="true"
                        ShowBorder="false" LabelWidth="150px" LabelAlign="Right" ShowHeader="false" Width="350px">
                        <Items>
                            <ext:Label ID="Label3" runat="server" Label="申请内容">
                            </ext:Label>
                            <ext:Label ID="txtMeetingName" Label="学术会议名称" runat="server" >
                            </ext:Label>
                            <ext:Label ID="txtMembersCount" Label="参加人数" runat="server" Width="50px">
                            </ext:Label>
                            <ext:Label runat="server" ID="cbIsUseProjector" Label="设备需求">
                            </ext:Label>
                        </Items>
                    </ext:SimpleForm>
                    <ext:SimpleForm ID="SimpleForm4" BodyPadding="5px" runat="server" EnableBackgroundColor="true"
                        ShowBorder="false" LabelWidth="150px" LabelAlign="Right" ShowHeader="false" Width="350px">
                        <Items>
                            <ext:Label runat="server" ID="dpBookingDate" Label="预约日期">
                            </ext:Label>
                            <ext:Label runat="server" ID="tpBookingTime" Label="预约时间">
                            </ext:Label>
                            <ext:Label runat="server" ID="nbUseTime" Label="使用时长（分钟）">
                            </ext:Label>
                            <ext:TextArea runat="server" ID="txtRemark" Label="批注" Height="48px">
                            </ext:TextArea>
                            <ext:Label runat="server" ID="lbRemark" Label="批注">
                            </ext:Label>
                        </Items>
                    </ext:SimpleForm>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    <ext:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowHeader="false"
        Width="720px" ShowBorder="false">
        <Rows>
            <ext:FormRow ColumnWidths="30% 20% 20% 30%">
                <Items>
                    <ext:Label ID="Label4" runat="server">
                    </ext:Label>
                    <ext:Button ID="btnSubmit_OK" ValidateForms="SimpleForm1" CssClass="inline" Text="通过审核"
                        runat="server" OnClick="btnSubmit_OK_OnClick">
                    </ext:Button>
                    <ext:Button ID="btnSubmit_NO" ValidateForms="SimpleForm1" CssClass="inline" Text="退回申请"
                        runat="server" OnClick="btnSubmit_NO_OnClick">
                    </ext:Button>
                    <ext:Label ID="Label5" runat="server">
                    </ext:Label>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    </form>
</body>
</html>
