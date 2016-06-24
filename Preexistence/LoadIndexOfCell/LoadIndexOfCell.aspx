<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="LoadIndexOfCell.aspx.vb" Inherits="Preexistence_LoadIndexOfCell" %>

<%----第19号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section>
                <div class="container">
                    <div class="col-sm-12" style="text-align: left;" aria-multiline="True">
                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">批量导出GSM网格小区指标 ( Batch Export GSM Cell Index )</h2>
                        </div>
                    </div>
                    <table id="ContentPlaceHolder1_tbOutPut" class="table table-striped" style="padding-bottom: 0px; margin-bottom: 0px; text-align: center; align-items: center;">
                        <tr>
                            <td>分区区域&nbsp:&nbsp 
                            <asp:TextBox ID="txtPartition" runat="server"></asp:TextBox></td>
                            <td>&nbsp&nbsp 网格&nbsp:&nbsp &nbsp&nbsp
                            <asp:TextBox ID="txtGrid" runat="server"></asp:TextBox></td>

                        </tr>
                        <tr>
                            <td>&nbsp&nbsp BSC&nbsp:&nbsp &nbsp&nbsp
                            <asp:TextBox ID="txtBSC" runat="server"></asp:TextBox></td>
                            <td>&nbsp&nbsp小区&nbsp:&nbsp &nbsp&nbsp
                            <asp:TextBox ID="txtCell" runat="server"></asp:TextBox></td>
                        </tr>
                                                <tr>
                            <td>&nbsp&nbsp 基站中文名&nbsp:&nbsp
                            <asp:TextBox ID="txtBaseName" runat="server"></asp:TextBox></td>
                            <td>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbsp &nbsp&nbsp
                            </td>
                        </tr>

                        <tr style="text-align: center; align-items: center;">
                            <td style="text-align: center; align-items: center;">
                                <div style="text-align: center; align-items: center;">
                                    选择日期&nbsp:&nbsp
                            <asp:DropDownList ID="ddlWhatTime" runat="server" AutoPostBack="True">
                                <asp:ListItem Text="最近1天" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="最近3天" Value="3"></asp:ListItem>
                                <asp:ListItem Text="最近7天" Value="7"></asp:ListItem>

                            </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                <asp:Panel ID="plFromDateToDate" runat="server" Visible="false">
                                    &nbsp&nbsp 开始日期&nbsp:&nbsp 
                                    <asp:TextBox ID="txtBeginDate" runat="server"></asp:TextBox>
                                    </br>
                                    </br>
                                    &nbsp&nbsp 结束日期&nbsp:&nbsp 
                                        <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                                </asp:Panel>
                            </td>
                        </tr>

                    </table>
                    <div class="col-sm-12">
                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <asp:Button ID="btnRunQuery" runat="server" Text="查找" class="form-control btn-warning" />
                        </div>
                    </div>
                </div>
            </section>
            <section>
                <div class="container">
                    <div class="col-sm-12" style="text-align: left;" aria-multiline="True">
                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">结果 ( Top 10 Results )</h2>
                        </div>
                    </div>
                    <div class="col-sm-12" style="overflow: auto;">
                        <asp:table id="tbOutPut" runat="server" class="table table-striped" style="padding-bottom: 0px; margin-bottom: 0px;" ClientIDMode="Inherit" HorizontalAlign="Center" >

                        </asp:table>

                    </div>
                    </br>
                    <div class="col-sm-12">
                        <asp:Panel ID="plForShowMessage" runat="server" Visible="false">
                            <div class="alert alert-danger" style="margin-top: 5px;">
                                <asp:Label ID="lblLoading" runat="server" Text="df"></asp:Label>
                            </div>

                        </asp:Panel>
                        <asp:Panel ID="plDownLoadAddress" runat="server" Visible="false">
                            <div class="alert alert-success" role="alert" style="margin-top: 5px;">
                                下载地址:
                            <asp:HyperLink ID="hlDownloadLink" runat="server">HyperLink</asp:HyperLink>
                            </div>

                        </asp:Panel>
                        <asp:Panel ID="plGoClickQuery" runat="server">
                            <div class="alert alert-info" role="alert" style="margin-top: 5px;"  >
                            <asp:Label ID="lblGoClickQuery" runat="server" Text="请点击查询按钮,多个条件用逗号隔开"></asp:Label>
                                
                            </div>

                        </asp:Panel>
                        <asp:Panel ID="plError" runat="server"  Visible="false">
                            <div  class="alert alert-danger" role="alert" style="margin-top: 5px;" >
                                <asp:Label ID="lblWrongDate" runat="server" Text=""></asp:Label>
                            </div>

                        </asp:Panel>
                    </div>
                </div>
                        <asp:Timer ID="timerGo" runat="server" Interval="300" Enabled="False"></asp:Timer>
            </section>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

