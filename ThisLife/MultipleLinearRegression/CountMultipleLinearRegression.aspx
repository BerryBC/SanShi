<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="CountMultipleLinearRegression.aspx.vb" Inherits="ThisLife_MultipleLinearRegression_CountMultipleLinearRegression" %>

<%----第17号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <section>
                <div class="container">


                    <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                        <h2 style="border-bottom-style: groove;">基础数据 ( Basic Data )</h2>
                    </div>
                    <div style="text-align: right;" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1"></div>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                            <asp:TextBox ID="txtBasicData" runat="server" class="form-control" TextMode="MultiLine" Style="overflow-y: visible;" Wrap="true" Height="300px"></asp:TextBox>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1"></div>

                        <div style="text-align: right;" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            </br>
                            <asp:Button ID="btnGoCount" runat="server" Text="开始计算" class="btn btn-warning form-control" />
                        </div>
                    </div>
                </div>
                <section>
                    <div class="container">

                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">结果输出 ( Results Output )</h2>
                        </div>
                        <div style="text-align: right;" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1"></div>

                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                                <asp:TextBox ID="txtResults" runat="server" class="form-control" TextMode="MultiLine" Style="overflow-y: visible;" Wrap="true" Height="600px"></asp:TextBox>


                            </div>
                            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1"></div>


                        </div>



                    </div>
                </section>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

