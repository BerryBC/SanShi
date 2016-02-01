<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="JustBuilding.aspx.vb" Inherits="JustBuilding" %>


<%----第7号页面--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <asp:Image ID="imgError" runat="server" ImageUrl="./IMG/building.jpg" Style="padding-top: 20px; padding-bottom: 20px;" />

    <div class="wow bounceInLeft" data-wow-delay="0.1s">
        <h2>抱歉，正在努力构建中，请稍等哦~</h2>
    </div>
    <div class="col-sm-12">

        <p><a class="btn btn-default  btn-danger" href="/Main.aspx" role="button" style="width: 400px;">返回主页            </a></p>
    </div>

</asp:Content>

