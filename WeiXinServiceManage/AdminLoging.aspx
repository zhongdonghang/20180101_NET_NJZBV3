<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLoging.aspx.cs" Inherits="WeiXinServiceManage.AdminLoging" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <title></title>
    <style type="text/css">
        body,div
        {
            margin:0px auto;
            padding:0px auto;
        }
        body
        {
            width:100%;
            background:#334667;
            background-image:url("Images/backimg.jpg");
            background-repeat:no-repeat;
        }
    #dv
    {
    text-align:center;
    border:0px solid RED;
    width:100%;
    height:100%;
    float:left;
    /*border-radius:25px;*/
    }
    #dvL
    {
      border:1px solid RED;
      background:#F4FEFE;
      margin:-430px 435px ;
      border-radius:25px;
      width:490px;
      height:280px;
      float:left;
      box-shadow: 10px 10px 5px #888888;
    }
    #dvLogin
    {

      border:3px solid #0B93E7;
      margin-top:132px;
      margin-left:435px;
      border-radius:25px;
      width:490px;
      height:300px;
      float:left;
      box-shadow: 10px 10px 5px #888888;
    }
    .txtUs
    {
        border-radius:5px;
        width:250px;
        border:1px solid #FFFFFF;
        height:30px;
        background:#055FB5;
        background-image:url("Images/user-icon.png");
        background-repeat:no-repeat;
        background-position:10px center;
        padding-left:30px;
        color:#FFFFFF;
    }
    .btn
    {
        background:#05458F;
        width:250px;    
        height:35px;
        border-radius:5px;
        border:1px solid #FFFFFF;
        color:#FFFFFF;
        
    }
    .txtPwd
    {
        border-radius:5px;
        width:250px;
        border:1px solid #FFFFFF;
        height:30px;
        background:#055FB5;
        background-image:url("Images/pass-icon.png");
        background-repeat:no-repeat;
        background-position:10px center;
        padding-left:30px;
        color:#FFFFFF;
    }
    #tb
    {
         margin:30px auto;
         border-radius:25px;
    }
    </style>
    <script type="text/javascript">
        function LoginShow() {
            var no = $("#txtName").val();
            var pwd = $("#txtPwd").val();
            if (no == "") {
                $("#Label2").html("账号不能为空");
                return;
            }
            if (pwd == "") {
                $("#Label2").html("密码不能为空");
                return;
            }
        }
        function ReSetWarnInfo() {
            $("#Label2").hide();
            $("#Label2").html("");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="dv">
    
        <div id="dvLogin">  
        <table id="tb" border="0" cellpadding="13" cellspacing="0" width="80%">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="登录" BorderColor="Blue" 
                            Font-Bold="True" Font-Names="华文新魏" Font-Size="XX-Large" ForeColor="White"></asp:Label>
                    </td>
                </tr>
                <tr>
                <td style=" height:10px;">
                    <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtName" runat="server" CssClass="txtUs" 
                            onfocus="ReSetWarnInfo()" AutoCompleteType="Disabled"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtPwd" runat="server" CssClass="txtPwd" 
                            onfocus="ReSetWarnInfo()" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnLogin" runat="server" Text="登录"  CssClass="btn" 
                            onclick="btnLogin_Click" OnClientClick="LoginShow()"/>
                    </td>
                </tr>
            </table>       
        </div>

        
    </div>
    </form>
</body>
</html>
