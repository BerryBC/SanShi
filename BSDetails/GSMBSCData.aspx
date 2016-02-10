<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="GSMBSCData.aspx.vb" Inherits="BSDetails_GSMBSCData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <section>
                <div class="container">
                    <div class="col-sm-12" style="text-align: left;" aria-multiline="True">

                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">GSM参数信息管理 ( Parameter of GSM Management )</h2>
                        </div>

                        <br />
                        最后更新的时间 :<br />
                        <asp:TextBox ID="txtLastUpdateTime" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        更新路径 :<br />
                        <asp:TextBox ID="txtUpDatePath" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        更新源文件名 :<br />
                        <asp:TextBox ID="txtUpdateSource" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        <div style="text-align: center;" id="ModifyButtonDIV">

                            <br />
                            <asp:Button ID="btnWantModify" runat="server" class="btn btn-info" Text="开始表格更新信息" Style="margin: 10px;" />
                            <asp:Button ID="btnConfirmModify" runat="server" class="btn btn-success" Text="确定修改表格更新信息" Style="margin: 10px;" Enabled="false" />
                            <asp:Button ID="btnGoInsert" runat="server" Text="开始入数" class="btn btn-warning" />
                            <asp:Timer ID="timerLoading" runat="server" Enabled="false" Interval="500"></asp:Timer>
                            <asp:Panel ID="plYeahGo" runat="server" Visible="False" Style="text-align: center;">
                                <div class="alert alert-success" role="alert" style="margin-top: 5px;">
                                    <asp:Label ID="lblLoading" runat="server" Text="Inserting"></asp:Label>
                                </div>
                            </asp:Panel>                        
                        
                        </div>
                    </div>
                </div>
            </section>
            <section>
                <div class="container">
                    <div class="col-sm-12" style="text-align: left;" aria-multiline="True">

                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">当前管理操作记录 ( Log The Managerment Operation )</h2>
                        </div>

                        <div class="col-sm-12" style="text-align: center; align-self: center;">

                            <asp:TextBox ID="txtLogMessage" runat="server" class="form-control" TextMode="MultiLine" Style="overflow-y: visible;" Wrap="true" Height="300px" Enabled="false"></asp:TextBox>

                        </div>


                    </div>
                </div>
            </section>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

