<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="HotSpotDownloadByEvent.aspx.vb" Inherits="ThisLife_DownHotSpotCellList_HotSpotDownloadByEvent" %>
<%----第26号页面--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="upUpdatepanel">
        <ContentTemplate>

            <section style="padding-bottom: 20px;">
                <div class="container">
                    <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                        <h2 style="border-bottom-style: groove;">以保障类型下载热点小区列表 ( Download Hotspot Cell List By Event )</h2>
                    </div>
                    <div class="col-sm-3" style="text-align: center;">

                        <asp:ListBox ID="lbListOfEvent" CssClass="form-control " Style="height: 300px;overflow-x:scroll" runat="server" AutoPostBack ="true" ></asp:ListBox>

                    </div>
                    <div class="col-sm-3" style="text-align: center;">

                        <asp:ListBox ID="lbListOfUser" CssClass="form-control " Style="height: 300px;overflow-x:scroll" runat="server" AutoPostBack ="true" ></asp:ListBox>

                    </div>
                    <div class="col-sm-6 alert alert-info" style="text-align: center; height: 300px;">
                        </br><strong>
                            如果你真的要下载，请点击下面的按钮，祝福你。</br></br>
                        </strong></br>
                           </br>
                           <asp:Button ID="btnGo" runat="server" Text="下载" class="btn btn-warning" Enabled ="false" />



                    </div>
                    <div class="col-sm-12">
                        <asp:Panel ID="plUpdating" runat="server" Visible="false">

                            <div class="alert alert-warning" style="text-align: center;">
                                </br>
                            <asp:Label ID="lblLoading" runat="server" Text="正在处理，请稍后..."></asp:Label>
                                </br>
                           </br>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="plDownload" runat="server" Visible="false">

                            <div class="alert alert-success" style="text-align: center;">
                                </br>
                            下载地址为：<asp:HyperLink ID="hlDownloadLink" runat="server"></asp:HyperLink></br>
                           </br>
                            </div>
                        </asp:Panel>

                    </div>



                </div>
            </section>
            <asp:Timer ID="timerDelay" runat="server" Enabled="false" Interval="1000"></asp:Timer>
        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>

