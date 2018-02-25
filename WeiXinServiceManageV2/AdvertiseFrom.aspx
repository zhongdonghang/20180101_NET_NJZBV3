<%@ Page Title="" Language="C#" MasterPageFile="~/AdminLogin.Master" AutoEventWireup="true"
    CodeBehind="AdvertiseFrom.aspx.cs" Inherits="WeiXinServiceManage.AdvertiseFrom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        td
        {
            text-align: center;
        }
        .btn
        {
            background: #3462C5;
            border: 1px solid #FFFFFF;
            color: #FFFFFF;
            width: 200px;
            height: 30px;
        }
    </style>
    <script type="text/javascript">
        function AddShow() {
            location.href = "UpAdvertis.aspx";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input id="btnadd" type="button" value="添加" class="btn" onclick="AddShow()" />
    <asp:Button ID="btnAdv" runat="server" Text="发布" CssClass="btn" 
        onclick="btnAdv_Click" />
    <asp:Label ID="Label5" runat="server" Text="" ForeColor="#FF3300"></asp:Label>
    <asp:GridView ID="GVAdvertise" runat="server" AutoGenerateColumns="False" CellPadding="4"
        ForeColor="#333333" GridLines="None" Width="100%">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="选择">
                <ItemTemplate>
                    <asp:CheckBox ID="cbox" runat="server" ToolTip='<%# Eval("ID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="标题">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="图片">
                <ItemTemplate>
                    <asp:Image ID="Image1" runat="server" Height="50px" ImageUrl='<%# Eval("Image") %>'
                        Width="50px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="链接">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Url") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作人">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("LoginID.Username") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作时间">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Convert.ToDateTime(Eval("DateTime")).ToString("yyyy年MM月dd日 hh：mm") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>  
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton2" runat="server" Text="修改" PostBackUrl='<%# "UpAdvertis.aspx?id="+Eval("ID").ToString() %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
</asp:Content>
