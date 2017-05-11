<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="LoadInterfereOfCell24Hour.aspx.vb" Inherits="Preexistence_LoadIndexOfCell_LoadInterfereOfCell24Hour" %>

<%----第34号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function JSCodeShow() {
            var divDown = document.getElementById('divDisplayTheTip');
            var newImg = document.createElement('img');
            newImg.src = '/IMG/Loading.gif';
            newImg.style.width = '10%';
            newImg.id = "LoadIMG";
            var newStrong = document.createElement('strong');
            newStrong.style = "color:rgba(200,0,500,.8);";
            newStrong.innerHTML = "<br/>请稍等唷~";
            newStrong.id = "LoadStrong";
            divDown.appendChild(newImg);
            divDown.appendChild(newStrong);
            setTimeout(function () { $('.btn-warning').attr("disabled", true); }, 10);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section>
                <div class="container">
                    <div class="col-sm-12" style="text-align: left;" aria-multiline="True">
                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">批量导出GSM 24小时干扰指标 ( Batch Export GSM 24 Hour Interfere Data )</h2>
                        </div>
                    </div>
                    <table id="ContentPlaceHolder1_tbOutPut" class="table table-striped" style="padding-bottom: 0px; margin-bottom: 0px; text-align: center; align-items: center;">

                        <tr style="text-align: center; align-items: center;">

                            <td>
                                <asp:Panel ID="plFromDateToDate" runat="server">
                                    &nbsp&nbsp 选择一天&nbsp:&nbsp 
                                    <asp:TextBox ID="txtBeginDate" runat="server" class="txtDateFrom"></asp:TextBox>
                                </asp:Panel>
                            </td>
                        </tr>

                    </table>
                    <div class="col-sm-12">
                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <asp:Button ID="btnRunQuery" runat="server" Text="查找" class="form-control btn-warning" OnClientClick="JSCodeShow()" />
                            <br />
                            <asp:Button ID="btnReQuer" runat="server" Text="重新查找" class="form-control btn-danger" Visible="false" />
                        </div>
                    </div>
                </div>
            </section>
            <section>
                <div class="container">
                    <div class="col-sm-12" style="text-align: left;" aria-multiline="True">
                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">结果 ( Results to Download )</h2>
                        </div>
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
                            <div class="alert alert-info" role="alert" style="margin-top: 5px;" id="divDisplayTheTip">
                                <asp:Label ID="lblGoClickQuery" runat="server" Text="请点击查询按钮,多个条件用逗号隔开"></asp:Label>

                            </div>

                        </asp:Panel>
                        <asp:Panel ID="plError" runat="server" Visible="false">
                            <div class="alert alert-danger" role="alert" style="margin-top: 5px;">
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

