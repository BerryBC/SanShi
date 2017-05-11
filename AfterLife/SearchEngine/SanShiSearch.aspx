<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="SanShiSearch.aspx.vb" Inherits="AfterLife_SearchEngine_SanShiSearch" %>
<%----第32号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <base target='_blank' />
    <!--<script src="<%=ResolveClientUrl("~/JS/jquery.scrollTo.js")%>" type="text/javascript"></script>-->
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section style="padding-bottom: 20px;">
        <div class="container">
            <div id="KeywordDiv" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                    <h2 style="border-bottom-style: groove;">紧张的搜索引擎 ( Tense Search Engine )</h2>
                </div>
                <div style="text-align: left" class="InputDIV">
                    请输入关键字：
                    <input type="text" id="Text1" runat="server" class="WantKW" onkeyup="IsPressEnter(event)" />
                    <input type="button" id="Button1" runat="server" class="btn btn-info" value="Go" onclick="IsPressEnter(event)" /></br>
                    <!--<input type="text" id="Text12" runat="server" class="WantKW"  />-->

                </div>
            </div>
            <div id="ShowResult" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">


            </div>
                        <div class="CannotFind col-lg-12 col-md-12 col-sm-12 col-xs-12" id="CannotFind">
                <h2>正在为你找查结果哟~<br />
                    <img src="/IMG/Loading.gif" width="10%" /></h2>
            </div>
            <div style="text-align: right" class="ShowResultCount col-lg-12 col-md-12 col-sm-12 col-xs-12">
                哎哟~为你找到 XX 条记录了！
            </div>
            <div class="NOPage col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <input type="button" value="<前一页" id="PreviousPage" class="btn" style="display:none;" />
                
                <span id="ContentPage" style="position:static">

                </span>
                <input type="button" value="后一页>" id="NextPage" class="btn" style="display:none;"  />

            </div>



        </div>
    </section>
</asp:Content>

