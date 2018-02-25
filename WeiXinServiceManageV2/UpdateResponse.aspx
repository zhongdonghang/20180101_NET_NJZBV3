<%@ Page Title="" Language="C#" MasterPageFile="~/AdminLogin.Master" AutoEventWireup="true"
    CodeBehind="UpdateResponse.aspx.cs" Inherits="WeiXinServiceManage.UpdateResponse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btnconfig
        {
            border: 2px solid #FFFFFF;
            width: 90px;
            height: 25px;
            background: #3462C5;
            color: #FFFFFF;
        }
    </style>
    <script type="text/javascript">
        function jsFunction() {
            var type1 = $("#ddlType").val();
            var isbit = false;

            $.ajax({
                type: "post",
                url: "AJAX/AjaxTextType.ashx",
                async: false,
                data: { types: type1 },
                dataType: "text",
                success: function (msg) {
                    if (msg != "") {
                        isbit = shows();
                    } else {
                        isbit = true;
                    }
                }
            });
            return isbit;
        }
        function shows() {
            if (confirm("该类型已经存在是否覆盖？")) {
                return true;
            } else {
                return false;
            }
        }
        function ReSetWarnInfo() {
            $("#lbltxt").hide();
            $("#lbltxt").html("");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input id="Hf" type="hidden" />
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                回复内容：
            </td>
            <td align="center">
                <asp:TextBox ID="txttext" runat="server" onfocus="ReSetWarnInfo()"  TextMode="MultiLine" Height="108px" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center">
                回复类型：
            </td>
            <td align="center">
                <asp:DropDownList ID="ddlType" runat="server" ClientIDMode="Static" onchange="ReSetWarnInfo()">
                    <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                    <asp:ListItem Value="0">关注回复</asp:ListItem>
                    <asp:ListItem Value="1">文本回复</asp:ListItem>
                    <asp:ListItem Value="2">图片回复</asp:ListItem>
                    <asp:ListItem Value="3">语音回复</asp:ListItem>
                    <asp:ListItem Value="4">视频回复</asp:ListItem>
                    <asp:ListItem Value="5">地理位置</asp:ListItem>
                    <asp:ListItem Value="6">连接回复</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnBIn" runat="server" Text="保存" CssClass="btnconfig" OnClientClick="return jsFunction();"
                    OnClick="btnBIn_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lbltxt" runat="server" Text="" ForeColor="Red" ClientIDMode="Static"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
