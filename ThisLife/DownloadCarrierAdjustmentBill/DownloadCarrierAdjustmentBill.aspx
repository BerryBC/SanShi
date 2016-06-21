<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="DownloadCarrierAdjustmentBill.aspx.vb" Inherits="ThisLife_DownloadCarrierAdjustmentBill_DownloadCarrierAdjustmentBill" %>
<%----第25号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section>
                <div class="container">
                    <div class="col-sm-12" style="text-align: left;" aria-multiline="True">
                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">下载GSM载调进度表 ( Dowmload GSM Carrier Adjustmen Progress )</h2>
                        </div>
                    </div>
                    <table id="ContentPlaceHolder1_tbOutPut" class="table table-striped" style="padding-bottom: 0px; margin-bottom: 0px; text-align: center; align-items: center;">
                        <tr>
                            <td>&nbsp&nbsp 本地生产任务名&nbsp:&nbsp &nbsp&nbsp
                            <asp:TextBox ID="txtNumberOfBill" runat="server"></asp:TextBox></td>
                            <td>&nbsp&nbsp 小区&nbsp:&nbsp &nbsp&nbsp
                            <asp:TextBox ID="txtCell" runat="server"></asp:TextBox></td>
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
                            <asp:HyperLink ID="hlDownloadLink" runat="server">HyperLink</asp:HyperLink></br></br>
                                                                其中A-C列为现网载波数以及频点数，D-AF列为本地生产支撑系统内容，AG-BB列为NAMS系统维护反馈情况。

                            </div>

                        </asp:Panel>
                        <asp:Panel ID="plGoClickQuery" runat="server">
                            <div class="alert alert-info" role="alert" style="margin-top: 5px;"  >
                            <asp:Label ID="lblGoClickQuery" runat="server" Text="请点击查询按钮,本地生产支撑系统的任务名可以输入关键字，按小区名查询必须输入准确小区名，多个条件用逗号隔开"></asp:Label></br></br>
                                <strong> 本系统每天中午12点钟更新。</strong>
                                
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

