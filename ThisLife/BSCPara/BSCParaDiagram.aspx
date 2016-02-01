<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="BSCParaDiagram.aspx.vb" Inherits="ThisLife_BSCPara_BSCParaDiagram" %>
<%----第11号页面--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>




            <section style="padding-bottom: 20px; margin-left: -30px;">

                <div class="container">
                    <div class="table table-responsive col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">广州爱立信BSC参数 ( GZ Ericsson's BSC Parameter )</h2>
                        </div>
                        <div style="text-align: right">
                            请输入需查询的BSC（多个BSC请以逗号隔开）：<asp:TextBox ID="txtSearchWhat" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" class="btn btn-success" CommandName="search" Text="GO" />
                        </div>
                        <br />
                        <div style="text-align: right">

                            <asp:DropDownList ID="ddlWhichPara" runat="server" class="form-control" AutoPostBack="True"></asp:DropDownList>
                        </div>
                        <br />

                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="text-align: left; border-style: ridge; padding-bottom: 0px;">
                            <div id="divOutPut" runat="server" class="table-responsive" style="padding-bottom: 0px; margin-bottom: 0px;">
                                <table id="tbOutPut" runat="server" class="table table-striped" style="padding-bottom: 0px; margin-bottom: 0px;">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:LinkButton ID="linktbnBSC" runat="server">BSC</asp:LinkButton>
                                                <asp:Label ID="lblBSCUp" runat="server" Text="↑" Visible="false"></asp:Label>
                                                <asp:Label ID="lblBSCDown" runat="server" Text="↓" Visible="false"></asp:Label></th>
                                            <th>
                                                <asp:LinkButton ID="linkbtnNumerator" runat="server"></asp:LinkButton>
                                                <asp:Label ID="lblNumeratorUp" runat="server" Text="↑" Visible="false"></asp:Label>
                                                <asp:Label ID="lblNumeratorDown" runat="server" Text="↓" Visible="false"></asp:Label></th>
                                            <th>
                                                <asp:LinkButton ID="linkbtnDenominator" runat="server"></asp:LinkButton>
                                                <asp:Label ID="lblDenominatorUp" runat="server" Text="↑" Visible="false"></asp:Label>
                                                <asp:Label ID="lblDenominatorDown" runat="server" Text="↓" Visible="false"></asp:Label></th>
                                            <th>
                                                <asp:LinkButton ID="linkbtnRedundancy" runat="server">剩余</asp:LinkButton>
                                                <asp:Label ID="lblRedundancyUp" runat="server" Text="↑" Visible="false"></asp:Label>
                                                <asp:Label ID="lblRedundancyDown" runat="server" Text="↓" Visible="false"></asp:Label></th>
                                            <th>
                                                <asp:LinkButton ID="linkbtnPercent" runat="server">占用百分比</asp:LinkButton>
                                                <asp:Label ID="lblPercentUp" runat="server" Text="↑" Visible="false"></asp:Label>
                                                <asp:Label ID="lblPercentDown" runat="server" Text="↓" Visible="false"></asp:Label></th>
                                        </tr>
                                    </thead>




                                </table>

                            </div>
                        </div>
                        <div style="align-self: center; align-items: center;" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <asp:Button ID="btnGoFirstPage" runat="server" Text="First Page" class="btn btn-danger" Enabled="false" Style="margin: 20px;" />
                            |
            <asp:Button ID="btnGoFrontPage" runat="server" Text="Front Page" class="btn btn-danger" Enabled="false" Style="margin: 20px;" />
                            |&nbsp
            Now:<asp:Label ID="lblNow" runat="server" Text=""></asp:Label>&nbsp&nbsp in &nbsp&nbsp Total:<asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                            &nbsp |
            <asp:Button ID="btnGoNextPage" runat="server" Text="Next Page" class="btn btn-danger" Enabled="false" Style="margin: 20px;" />
                            |
            <asp:Button ID="btnGoLastPage" runat="server" Text="Last Page" class="btn btn-danger" Enabled="false" Style="margin: 20px;" />
                            <br />
                        </div>

                        <div style="text-align: center; align-items: center;" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div style="text-align: center; align-items: center;" class="col-lg-3 col-md-3 col-sm-3 col-xs-3"></div>
                            <div style="text-align: center; align-items: center;" class="col-lg-5 col-md-5 col-sm-5 col-xs-5">

                                <span style="margin-left: 180px" />Jump to :
                    <asp:TextBox ID="txtPage" runat="server" Width="30"></asp:TextBox>
                                <asp:Button ID="btnGoto" runat="server" class="btn btn-success" Text="GO" />
                                <asp:RegularExpressionValidator ID="revForPage" runat="server"
                                    ControlToValidate="txtPage" ErrorMessage="请输入数字" ForeColor="red"
                                    ValidationExpression="[1-9]+(\d)*"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2" style ="margin-top :6px;"> 
                               请选择每页显示的行数： </div>
                           
                            <div style="text-align: center; align-items: center;" class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                <asp:DropDownList ID="ddlHowManyRow" runat="server" AutoPostBack="True" class="form-control">
                                    <asp:ListItem Text="10" Value="10" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                    <asp:ListItem Text="200" Value="200"></asp:ListItem>
                                </asp:DropDownList>
                            </div>




                        </div>

                        <div style="text-align: center; align-items: center;" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                            <asp:Panel ID="plFaild" runat="server" Visible="false">
                                <div id="divFaild" class="alert alert-danger" role="alert" style="margin-top: 5px;">
                                    <strong>数据为空或没有配置文件</strong>
                                </div>
                            </asp:Panel>



                        </div>


                    </div>
            </section>
        </ContentTemplate>


    </asp:UpdatePanel>


</asp:Content>

