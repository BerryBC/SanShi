<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RenewGCellList.aspx.vb" Inherits="ThisLife_RenewTheGuaranteeCellList_RenewGCellList" %>

<%----第20号页面--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="upUpdatepanel">
        <ContentTemplate>

            <section style="padding-bottom: 20px;">
                <div class="container">
                    <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                        <h2 style="border-bottom-style: groove;">更新保障小区列表 ( Update the Guarant Cell List )</h2>
                    </div>
                    <div class="col-sm-12 alert alert-info" style="text-align: center; text-align: left;">
                        </br><strong>
                            1、请下载模板，并根据模板格式上传保障小区列表：<a href="./Files/TemplateFiles/TemplateFromID.xlsx">模板下载</a>，并保证表头标紫列不为空。</br></br>
                            2、请注意：上传的小区内必须填写“保障类型”列，并且一个表格只能更新一个保障类型（别把两个不同的保障列表放在一起）。
                        </strong></br>
                           </br>
                    </div>
                    <div class="col-sm-12" style="text-align: left;">
                        </br>
                            上传需要更新的保障小区列表 ：
                        <asp:FileUpload ID="fuFileA" runat="server" class="btn btn-info" Style="display: inline" /></br>
                           </br>
                    </div>
                    <div class="col-sm-12" style="text-align: center; text-align: left;">
                        </br>
                          <asp:Button ID="btnGo" runat="server" Text="更新小区列表" class="form-control btn-warning" OnClientClick="javascript:{var stamp = document.getElementById('.*btnGo.*');stamp.disabled=true;}" />

                        </br>
                           </br>
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

            <section>
                <div class="container">
                    <div class="col-sm-12" style="text-align: left;" aria-multiline="True">

                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">当前操作记录 ( Log The Operation )</h2>
                        </div>

                        <div class="col-sm-12" style="text-align: center; align-self: center;">

                            <asp:TextBox ID="txtLogMessage" runat="server" class="form-control" TextMode="MultiLine" Style="overflow-y: visible;" Wrap="true" Height="300px" Enabled="false"></asp:TextBox>

                        </div>


                    </div>
                </div>
            </section>


            <asp:Timer ID="timerDelay" runat="server" Enabled="false" Interval="1000"></asp:Timer>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnGo" />

        </Triggers>

    </asp:UpdatePanel>

</asp:Content>

