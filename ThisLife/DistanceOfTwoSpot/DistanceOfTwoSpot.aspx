<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="DistanceOfTwoSpot.aspx.vb" Inherits="ThisLife_DistanceOfTwoSpot_DistanceOfTwoSpot" %>

<%----第15号页面--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <section>
                <div class="container">
                    <div class="col-sm-12" style="text-align: left;" aria-multiline="True">

                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">批量找出两点之间的距离 ( Batch Find the Distance Between Two Spot )</h2>
                        </div>
                        <div class="col-sm-12 alert alert-info" style="text-align: center; text-align: left;">
                            </br>
                            请下载模板，并把两份表的点标识及点经纬度按照模板格式保存好，并分别上传到A表以及B表：<a href="./Files/TemplateFiles/Template.xlsx">模板下载</a></br>
                           </br>
                        </div>
                        <div class="col-sm-6" style="text-align: center; text-align: left;">
                            </br>
                            表A：<asp:FileUpload ID="fuFileA" runat="server" class="btn btn-info" Style="display :inline" /></br>
                           </br>
                        </div>
                        <div class="col-sm-6" style="text-align: center; text-align: left;">
                            </br>
                            表B：<asp:FileUpload ID="fuFileB" runat="server" class="btn btn-info" Style="display :inline" /></br>
                           </br>
                        </div>








                    </div>
                </div>
            </section>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

