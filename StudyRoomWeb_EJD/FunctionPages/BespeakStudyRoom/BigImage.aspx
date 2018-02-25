<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BigImage.aspx.cs" Inherits="StudyRoomWeb.FunctionPages.BespeakStudyRoom.BigImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .borderStyle
        {
            border-collapse: collapse;
            border: 0px dotted #DFE8F6;
            border-width: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ext:PageManager ID="PageManager" runat="server" AutoSizePanelID="RegionPanel_1"
        HideScrollbar="true" />
    <ext:Panel ID="PanelSetting" ClientIDMode="Static" runat="server" EnableBackgroundColor="false"
        BodyPadding="1px" Height="500px" AutoScroll="true" ShowBorder="false" ShowHeader="false"
        Title="研习间图片">
        <Items>
            <ext:ContentPanel BoxConfigAlign="Center" runat="server" BodyPadding="3px" ShowHeader="false"
                Title="研习间图片" EnableBackgroundColor="true" ClientIDMode="Static" ID="FormReadingRoomSet">
                <ext:Image ID="imgRoomImage" ImageHeight="480px" runat="server" ImageWidth="640px"
                    ShowLabel="false">
                </ext:Image>
            </ext:ContentPanel>
        </Items>
    </ext:Panel>
    </form>
</body>
</html>
