<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="BaseSationDetailsLogViewer.aspx.vb" Inherits="EarthlyBranch_LogViewer_BaseSationDetailsLogViewer" %>

<%----第16号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <section>
                <div class="container">


                    <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                        <h2 style="border-bottom-style: groove;">当前管理操作记录 ( Log The Managerment Operation )</h2>
                    </div>
                    <div style="text-align: right;" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                     <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
</div>
                        <div style="text-align: center; align-items: center;" class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                            <asp:DropDownList ID="ddlHowManyRow" runat="server" AutoPostBack="True" class="form-control">
                                <asp:ListItem Text="一天" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="三天" Value="3"></asp:ListItem>
                                <asp:ListItem Text="十天" Value="10"></asp:ListItem>
                                <asp:ListItem Text="一个月" Value="30"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>


                    <div class="col-sm-12" style="text-align: center; align-self: center;">
                        </br>
                        <asp:TextBox ID="txtLogMessage" runat="server" class="form-control" TextMode="MultiLine" Style="overflow-y: visible;" Wrap="true" Height="300px" Enabled="false"></asp:TextBox>

                    </div>

                </div>
            </section>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

