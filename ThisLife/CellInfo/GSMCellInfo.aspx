<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="GSMCellInfo.aspx.vb" Inherits="ThisLife_CellInfo_GSMCellInfo" %>

<%----第28号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=C4msdAZymuN9mjstHB5H6nYT"></script>
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="upUpdatepanel">
        <ContentTemplate>
            <asp:Timer ID="timerGetMap" runat="server" Interval ="100" Enabled ="false" ></asp:Timer>
            <asp:Panel ID="plFindCell" runat="server">
                <section style="padding-bottom: 20px; height: 600px; padding-top: 13%;" id="Banner" class="banner">

                    <div class="container">
                        <div class="banner-info">
                            <h1>请输入需要找查的小区英文名（完全匹配）</h1>
                            <h3>(查找时间可能有点长，烦请耐心等候)</h3>
                            <h3>
                                <asp:Label ID="lblError" runat="server" Text="（找不到该小区，请输入现网小区英文名）" ForeColor="Red" Visible="false"></asp:Label></h3>
                            <asp:TextBox ID="txtCellName" runat="server"></asp:TextBox>
                            &nbsp; &nbsp; 
                            <asp:Button ID="btnSearchCell" runat="server" Text="查找" CssClass="btn btn-success" />
                        </div>





                    </div>



                </section>

            </asp:Panel>

            <asp:Panel ID="plCell" runat="server" Visible="false">
                <section style="padding-bottom: 20px;" id="Restart">
                    <div class="container">
                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">重新找查小区 ( Restart )</h2>
                        </div>
                        <div class="col-sm-12">
                            <asp:Button ID="btnRestart" runat="server" Text="查找" CssClass="btn btn-lg btn-primary btn-danger btn-block" OnClientClick=" $(document).scrollTop(0)" />

                        </div>
                        </div>
                </section>


                <section style="padding-bottom: 20px;" id="BaseInformation">
                    <div class="container">
                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">GSM小区基础信息 ( Base Information Of GSM Cell )</h2>
                        </div>
                        <div class="col-sm-12" style="overflow: auto;">
                            <table id="ContentPlaceHolder1_tbOutPut" class="table table-striped" style="padding-bottom: 0px; margin-bottom: 0px; text-align: left; align-items: center;">
                                <tr>
                                    <td>&nbsp&nbsp 小区英文名&nbsp:&nbsp 
                                <asp:Label ID="lblEnglishName" runat="server" Text=""></asp:Label>


                                    </td>
                                    <td>&nbsp&nbsp 小区ID&nbsp:&nbsp &nbsp&nbsp
                                <asp:Label ID="lblID" runat="server" Text=""></asp:Label>
                                </tr>
                                <tr>
                                    <td>&nbsp&nbsp 基站名&nbsp:&nbsp &nbsp&nbsp
                                <asp:Label ID="lblBaseSation" runat="server" Text=""></asp:Label>
                                        <td>&nbsp&nbsp 所属BSC&nbsp:&nbsp &nbsp&nbsp
                                <asp:Label ID="lblBSC" runat="server" Text=""></asp:Label>
                                </tr>
                                <tr>
                                    <td>&nbsp&nbsp 所属网格&nbsp:&nbsp &nbsp&nbsp
                                <asp:Label ID="lblGrid" runat="server" Text=""></asp:Label>
                                        <td>&nbsp&nbsp 所属区域&nbsp:&nbsp &nbsp&nbsp
                            <asp:Label ID="lblPartition" runat="server" Text=""></asp:Label>
                                </tr>

                            </table>

                        </div>




                    </div>
                </section>


                <section style="padding-bottom: 20px;" id="IndexOfCell">
                    <div class="container">
                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">最近十天GSM小区话务信息 ( Last Ten Days Indicators Of GSM Cell )</h2>
                        </div>
                        <div class="col-sm-12" style="overflow: auto;">
                            <asp:Table ID="tbOutPutIndicators" runat="server" class="table table-striped" Style="padding-bottom: 0px; margin-bottom: 0px;" ClientIDMode="Inherit" HorizontalAlign="Center">
                            </asp:Table>

                        </div>




                    </div>
                </section>

                <section style="padding-bottom: 20px;" id="CellDetails">
                    <div class="container">
                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">GSM小区参数配置 ( Details Of GSM Cell )</h2>
                        </div>
                        <div class="col-sm-4" style="overflow: auto;">
                            <asp:Table ID="tbOutPutParameter" runat="server" class="table table-striped" Style="padding-bottom: 0px; margin-bottom: 0px;" ClientIDMode="Inherit" HorizontalAlign="Center">
                            </asp:Table>

                        </div>
                        <div class="col-sm-8" style="overflow: auto;">
                            <asp:Table ID="tbOutputHistory" runat="server" class="table table-striped" Style="padding-bottom: 0px; margin-bottom: 0px;" ClientIDMode="Inherit" HorizontalAlign="Center">
                            </asp:Table>

                        </div>




                    </div>
                </section>


                <section style="padding-bottom: 20px;" id="CellInfoInMap">
                    <div class="container">
                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">GSM位置信息 ( Location Information Of GSM Cell )</h2>
                        </div>



                       
                        <div class="col-sm-12" style="overflow: auto; height: 600px;" id="CellMap">  </div>
                        
                            <div class="col-sm-12" style="overflow: auto;height: 400px;margin-top:30px;">
                                <asp:Table ID="tbNeighborCell" runat="server" class="table table-striped" Style="padding-bottom: 0px; margin-bottom: 0px;" ClientIDMode="Inherit">
                                </asp:Table>



                            </div>
                        </div>
                </section>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

