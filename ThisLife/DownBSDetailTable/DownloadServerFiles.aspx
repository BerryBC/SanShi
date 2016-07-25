<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="DownloadServerFiles.aspx.vb" Inherits="ThisLife_DownBSDetailTable_DownloadServerFiles" %>

<%----第30号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="upUpdatepanel">
        <ContentTemplate>

            <section style="padding-bottom: 20px;">
                <div class="container">
                    <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                        <h2 style="border-bottom-style: groove;">下载服务器文件 ( Download Files From Server )</h2>
                    </div>
                    <div class="col-sm-4" style="text-align: left; padding:inherit;">

                        <asp:ListBox ID="lbListOfDetails" CssClass="form-control " Style="height: 300px;" runat="server" AutoPostBack="true"></asp:ListBox>

                    </div>


                    <div class="col-sm-8" style="text-align: center;padding:inherit">

                        <asp:ListBox ID="lbListOfFiles" CssClass="form-control " Style="height: 300px;" runat="server" AutoPostBack="true"></asp:ListBox>

                    </div>

                    <div class="col-sm-12 alert alert-info" style="text-align: center; margin-top: 20px;">
                        </br><strong>
                            如果你真的要下载，请点击下面的按钮，祝福你。</br></br>
                        </strong></br>
                           </br>
                           <asp:Button ID="btnGo" runat="server" Text="下载" class="btn btn-warning" Enabled="false" />
                        </br>
                           </br>


                    </div>
                    <div class="col-sm-12" style="text-align: center; margin-top: 20px">

                        <asp:Panel ID="plDownload" runat="server" Visible="false">
                            <div style="text-align: center;" class="alert alert-success">
                                </br>
                            下载地址为：<asp:HyperLink ID="hlDownloadLink" runat="server"></asp:HyperLink></br>
                           </br>
                            </div>



                        </asp:Panel>
                    </div>






                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

