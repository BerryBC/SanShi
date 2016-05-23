<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="CanNotAccess.aspx.vb" Inherits="CanNotAccess" %>

<%----第6号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
                    <div class="container">

    <asp:Image ID="imgError" runat="server" ImageUrl="./IMG/cannotaccess.jpg" Style="padding-top: 20px; padding-bottom: 20px;" />

    <div class="wow bounceInLeft" data-wow-delay="0.1s">
        <h2>
            <asp:Label ID="lblError" runat="server" Text="您的权限较低，无法使用该功能，请联系站主。"></asp:Label></h2>
    </div>
    <div class="col-sm-12">

        <p><a class="btn btn-default  btn-danger" href="/Main.aspx" role="button" style="width: 400px;">返回主页            </a></p>
    </div>
        <div class="col-sm-12">
            <asp:Panel ID="plGoLogIn" runat="server" Visible ="false" >
        <p><a class="btn btn-default  btn-info" href="/Login.aspx" role="button" style="width: 400px;">登陆            </a></p>
    </asp:Panel>

        </div>
</div> 

</asp:Content>

