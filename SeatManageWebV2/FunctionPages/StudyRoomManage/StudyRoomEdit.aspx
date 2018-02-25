<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudyRoomEdit.aspx.cs"
    Inherits="SeatManageWebV2.FunctionPages.StudyRoomManage.StudyRoomEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager1" runat="server" />
    <ext:SimpleForm ID="SimpleForm1" BodyPadding="5px" runat="server" EnableBackgroundColor="true"
        ShowBorder="false" ShowHeader="false" Width="300px">
        <Items>
            <ext:TextBox ID="txtStudyRoomNo" Required="true" Label="研习间编号" runat="server">
            </ext:TextBox>
            <ext:TextBox ID="txtStudyRoomName" Required="true" Label="研习间名称" runat="server">
            </ext:TextBox>
            <ext:TextArea ID="txtRemark" Required="false" Label="备注信息" runat="server" Height="48px">
            </ext:TextArea>
            <ext:Image ID="imgRoomImage" Label="研习间图片" runat="server" ImageWidth="180px" ImageHeight="120px" >
            </ext:Image>
            <ext:FileUpload ID="uploadImage" Label="上传图片" runat="server" OnFileSelected="uploadImage_OnFileSelected"
                AutoPostBack="true">
            </ext:FileUpload>
        </Items>
    </ext:SimpleForm>
    <ext:Form ID="Form2" runat="server" EnableBackgroundColor="true" ShowHeader="false"
        Width="300px" ShowBorder="false">
        <Rows>
            <ext:FormRow ColumnWidths="41% 18% 41%">
                <Items>
                    <ext:Label ID="Label1" runat="server">
                    </ext:Label>
                    <ext:Button ID="btnSubmit" ValidateForms="SimpleForm1" CssClass="inline" Text="提交"
                        runat="server" OnClick="btnSubmit_OnClick">
                    </ext:Button>
                    <ext:Label ID="Label2" runat="server">
                    </ext:Label>
                </Items>
            </ext:FormRow>
        </Rows>
    </ext:Form>
    </form>
</body>
</html>
