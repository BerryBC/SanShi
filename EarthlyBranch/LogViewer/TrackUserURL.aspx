<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="TrackUserURL.aspx.vb" Inherits="EarthlyBranch_LogViewer_TrackUserURL" %>

<%----第29号页面--%>
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
                            <h2 style="border-bottom-style: groove;">跟踪用户曾经浏览过的网页地址 ( Track User URL History )</h2>
                        </div>
                        <div style="text-align: right">
                            请输入需查询的用户名（多个用户名请以逗号隔开）：<asp:TextBox ID="txtSearchWhat" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" class="btn btn-success" CommandName="search" Text="GO" />
                        </div>
                        <br />

                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="text-align: left; border-style: ridge; padding-bottom: 0px;">
                            <div id="divOutPut" runat="server" class="table-responsive" style="padding-bottom: 0px; margin-bottom: 0px;">
                                <table id="tbOutPut" runat="server" class="table table-striped" style="padding-bottom: 0px; margin-bottom: 0px;">
                                    <thead>
                                        <tr>
                                            <th>
                                                出Log时间</th>
                                            <th>
                                                用户</th>
                                            <th>
                                                路径</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>

                        <div style="text-align: center; align-items: center;" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <br />
                            <div style="text-align: center; align-items: center;" class="col-lg-3 col-md-3 col-sm-3 col-xs-3"></div>
                            <div style="text-align: center; align-items: center;" class="col-lg-5 col-md-5 col-sm-5 col-xs-5"></div>
                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2" style="margin-top: 6px;">
                                请选择每页显示的行数：
                            </div>

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
                    </div>
            </section>
        </ContentTemplate>


    </asp:UpdatePanel>






</asp:Content>

