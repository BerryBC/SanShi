<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="HotSpotDownloadByHotspot.aspx.vb" Inherits="ThisLife_DownHotSpotCellList_HotSpotDownloadByHotspot" %>

<%----第27号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="upUpdatepanel">
        <ContentTemplate>
            <section style="padding-bottom: 20px;">
                <div class="container">
                    <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                        <h2 style="border-bottom-style: groove;">以热点名称下载热点小区列表 ( Download Hotspot Cell List By Hotspot )</h2>
                    </div>
                    <div class="col-sm-12 alert alert-info" style="text-align: left; padding-bottom: 20px; margin-bottom: 20px; text-indent: 10px; padding-left: 20px">
                        </br><strong>
                            1、请于下面的文本框入数要找的热点名字的关键字，然后点击搜索；</br></br>
                            2、在左边的列表框中点选所需要下载的热点名后（可多选），点击中间绿色按钮；</br></br>
                            3、右边列表框为已选定热点，点选已选定热点后点击中间红色按钮可剔除该已选热点；</br></br>
                            4、选择好后，点击以下的下载按钮。
                        </strong></br></br>
                           <asp:Button ID="btnGo" runat="server" Text="下载" class="btn btn-warning" Enabled="false" />
                    </div>
                    <div class="col-sm-12">
                        <asp:Panel ID="plUpdating" runat="server" Visible="false" Style="margin-bottom: 20px; margin-top: 20px;">
                            <div class="alert alert-warning" style="text-align: center;">
                                </br>
                            <asp:Label ID="lblLoading" runat="server" Text="正在处理，请稍后..."></asp:Label>
                                </br>
                           </br>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="plDownload" runat="server" Visible="false" Style="margin-bottom: 20px; margin-top: 20px;">
                            <div class="alert alert-success" style="text-align: center;">
                                </br>
                            下载地址为：<asp:HyperLink ID="hlDownloadLink" runat="server"></asp:HyperLink></br>
                           </br>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-sm-5" style="text-align: center;">
                        <asp:TextBox ID="txtHotspot" runat="server" CssClass="form-control "></asp:TextBox></br>
                        <asp:Button ID="btnSearchHotSpot" runat="server" Text="搜索" CssClass="form-control btn btn-info" /></br></br>
                        <asp:ListBox ID="lbListOfHotspot" CssClass="form-control " Style="height: 290px; overflow-x: scroll" runat="server" AutoPostBack="true" SelectionMode="Multiple"></asp:ListBox>
                    </div>
                    <div class="col-sm-2" style="text-align: center;">
                        </br></br></br></br>
                        <asp:Button ID="btnAdd" runat="server" Text="-->>" CssClass="form-control  btn btn-success" /></br></br>
                        <asp:Button ID="btnDelete" runat="server" Text="<<--" CssClass="form-control  btn btn-danger" /></br></br>
                    </div>
                    <div class="col-sm-5" style="text-align: center;">
                        <asp:ListBox ID="lbHotspot" CssClass="form-control " Style="height: 400px; overflow-x: scroll" runat="server" AutoPostBack="true" SelectionMode="Multiple"></asp:ListBox>
                    </div>
                </div>
            </section>
            <asp:Timer ID="timerDelay" runat="server" Enabled="false" Interval="1000"></asp:Timer>
        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>

