<%@ Page Title="" Language="C#" MasterPageFile="~/AdminLogin.Master" AutoEventWireup="true" CodeBehind="UpAdvertis.aspx.cs" Inherits="WeiXinServiceManage.UpAdvertis" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btnconfig
    {
        border:2px solid #FFFFFF;
        width:90px;
        height:25px;
        background:#3462C5;
        color:#FFFFFF;
    }
    </style>
    <script type="text/javascript">
        function BtnShow() {
            var title = $("#ContentPlaceHolder1_txtTitle").val();
            var url = $("#ContentPlaceHolder1_txtURl").val();
            if (url == null || url == "") {
                $("#lbltxt").html("连接地址不允许为空");
                return false;
            }
            if (title == null || title == "") {
                $("#lbltxt").html("标题不能为空");
                return false;
            }
        }
        function openBrowse() {
            $("#lbltxt").hide();
            $("#lbltxt").html("");
            var ie = navigator.appName == "Microsoft Internet Explorer" ? true : false;
            if (ie) {
                document.getElementById("ContentPlaceHolder1_FileUpload1").click();
            } else {
                var a = document.createEvent("MouseEvents"); //FF的处理 
                a.initEvent("click", true, true);
                document.getElementById("ContentPlaceHolder1_FileUpload1").dispatchEvent(a);
            }

        }

        function dox(obj) {
            alert("heh");
            var docObj = document.getElementById("ContentPlaceHolder1_FileUpload1");
            var imgObjPreview = document.getElementById("ContentPlaceHolder1_preview");
            if (docObj.files && docObj.files[0]) {
                //火狐下，直接设img属性
                imgObjPreview.style.display = 'block';
                imgObjPreview.style.width = '90px';
                imgObjPreview.style.height = '90px';
                //imgObjPreview.src = docObj.files[0].getAsDataURL();
                alert("123");
                //火狐7以上版本不能用上面的getAsDataURL()方式获取，需要一下方式  
                imgObjPreview.src = window.URL.createObjectURL(docObj.files[0]);

            } else {
                alert("456");
                //IE下，使用滤镜
                docObj.select();
                var imgSrc = obj;
                var localImagId = document.getElementById("localImag");
                //必须设置初始大小
                localImagId.style.width = "90px";
                localImagId.style.height = "90px";
                //图片异常的捕捉，防止用户修改后缀来伪造图片
                try {
                    localImagId.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale)";
                    localImagId.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = imgSrc;
                } catch (e) {
                    $("#lbltxt").val("您上传的图片格式不正确，请重新选择!");
                    return false;
                }
                imgObjPreview.style.display = 'none';
                document.selection.empty();
            }
            return true;
        }
        function ReSetWarnInfo() {
            $("#lbltxt").hide();
            $("#lbltxt").html("");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="10" cellspacing="0" width="100%">
        
        <tr>
            <td align="center">标题：
            </td>
            <td align="center">
                <asp:TextBox ID="txtTitle" onfocus="ReSetWarnInfo()" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr  style=" height:120px;">
            <td align="center">图片上传：
            </td>
            <td align="center">
            <input id="Button3" type="button" value="浏览..." class="btnconfig" onclick="openBrowse()" />
            <asp:FileUpload ID="FileUpload1" runat="server" style="display:none" onchange="dox(this.value)"/>
            <asp:LinkButton ID="lbUploadPhoto" runat="server" onclick="lbUploadPhoto_Click"></asp:LinkButton>
            <div id="localImag" style="width:90px; height:90px;">
                <img id="preview" width="-1" height="-1" runat="server" style="display:none" alt=""/>
             </div>
            
            </td>
        </tr>
        <tr>
            <td align="center">连接地址：
            </td>
            <td align="center">
                <asp:TextBox ID="txtURl" onfocus="ReSetWarnInfo()" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="Button1" runat="server" Text="提交" CssClass="btnconfig" OnClientClick="return BtnShow()" onclick="Button1_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lbltxt" runat="server" ClientIDMode="Static" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
