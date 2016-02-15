<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="InsertQueueOfBSCPara.aspx.vb" Inherits="BSDetails_InsertQueueOfBSCPara" %>

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
                            <h2 style="border-bottom-style: groove;">列队插入数据库的数据 ( Which To Queue To Insert The Database )</h2>
                        </div>
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">选择需要插入的过程</h3>
                            </div>
                            <div class="panel-body">
                                <div class="page-header" style="margin-top: -15px">
                                    <h3>基站信息入数</h3>
                                </div>


                                <asp:CheckBoxList ID="cblWhichBaseSationDetailsToInsert" runat="server">
                                </asp:CheckBoxList>
                                <div class="page-header">
                                    <h3>例行P入数</h3>
                                </div>
                                <asp:CheckBoxList ID="cblWhichBSCParaToInsert" runat="server">
                                </asp:CheckBoxList>


                            </div>

                        </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

