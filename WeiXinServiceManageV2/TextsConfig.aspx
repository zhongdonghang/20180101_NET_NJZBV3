<%@ Page Title="" Language="C#" MasterPageFile="~/AdminLogin.Master" AutoEventWireup="true" CodeBehind="TextsConfig.aspx.cs" Inherits="WeiXinServiceManage.TextsConfig" %>
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
    td,th
    {
        height:25px;
        text-align:center;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
    <asp:Button ID="btnADD" runat="server" Text="添加" CssClass="btnconfig" PostBackUrl="UpdateResponse.aspx" /></div>
<div>    
    <asp:Repeater ID="rptResponse" runat="server">
     <HeaderTemplate>
    <table border="1" cellpadding="10" cellspacing="0" width="100%">
     <tr>
    <th>文本</th>
    <th>类型</th>
    <th>操作人</th>
    <th>操作时间</th>
    <th>编辑</th>
<%--    <th>删除</th>--%>
<%--    <th>设置</th>--%>
    </tr>
    </HeaderTemplate>
     <ItemTemplate>
     <tr>
     <td><%#Eval("Text") %></td>
     <td><%#convert(int.Parse(( Eval("Type").ToString()))) %></td>
     <td><%#Eval("LoginID.UserName")%></td>
     <td><%# Convert.ToDateTime(Eval("AddDateTime")).ToString("yyyy年MM月dd日 hh:mm") %></td>

     <td>
         <asp:Button ID="btnUpda" runat="server" Text="修改" PostBackUrl='<%#  "UpdateResponse.aspx?id="+ Eval("ID").ToString() %>'  CssClass="btnconfig"/>
<%--     <td>
         <asp:Button ID="btnDel" runat="server" Text="删除"  PostBackUrl='TextsConfig.aspx?id=<%#Eval("ID").ToString() %>&Type=Del'  CssClass="btnconfig"/></td>--%>
<%--     <td>
         <asp:Button ID="btnBit" runat="server" Text='<%# Eval("IsUsed").ToString()=="1"?"设为无效":"设为有效" %>'  PostBackUrl='TextsConfig.aspx?id=<%#Eval("ID").ToString() %>&Type=Bit' CssClass="btnconfig"/></td>--%>
         <asp:Button ID="btnDel" runat="server" Text="删除" OnCommand="delete" OnClientClick="return confirm('是否确定删除？')" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' CssClass="btnconfig"/>
         </td>
     </tr>
     </ItemTemplate>
     <FooterTemplate>
        </table>
    </FooterTemplate>
    </asp:Repeater>

</div>
</asp:Content>
