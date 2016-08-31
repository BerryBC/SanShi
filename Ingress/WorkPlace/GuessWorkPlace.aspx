<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="GuessWorkPlace.aspx.vb" Inherits="Ingress_WorkPlace_GuessWorkPlace" %>
<%----第31号页面--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=C4msdAZymuN9mjstHB5H6nYT"></script>
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="upUpdatepanel">
        <ContentTemplate>
            <section style="padding-bottom: 20px;  " id="Banner" class="banner">
                <div class="container">
                    <div class="col-sm-12" style="text-align:left;margin-bottom:50px;">
                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">查找用户 ( Search User )</h2>
                        </div>

                        查询的用户 ：<asp:TextBox ID="txtSearchWhat" runat="server"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" class="btn btn-success" CommandName="search" Text="GO" />

                    </div>

                    <div class="col-sm-12" style="overflow: auto; height: 600px;display:block;" id="CellMap">  </div>




                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

