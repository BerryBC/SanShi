<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="HotSpotDownloadGuide.aspx.vb" Inherits="ThisLife_DownHotSpotCellList_HotSpotDownloadGuide" %>
<%----第23号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <section style="padding-bottom: 20px;">
        <div class="container">
            <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                <h2 style="border-bottom-style: groove;">下载热点小区列表的方式 ( Choose The Way To Download Guarantee Cell List )</h2>
            </div>
            <div class="col-sm-6 alert alert-info" style="text-align: center; text-align: left;">
                </br><strong>
                            1、针对某次保障的整份保障列表下载，包含整个保障期间的所有热点。</br></br>
                </strong></br>
                           </br>
                <p><a class="btn btn-default" href="/ThisLife/RenewTheGuaranteeCellList/RenewGCellList.aspx" role="button">全量保障小区</a></p>
            </div>
            <div class="col-sm-6 alert alert-warning" style="text-align: center; text-align: left;">
                </br><strong>
                            2、挑选热点下载，下载某些热点的保障小区列表。</br></br>
                </strong></br>
                           </br>
                <p><a class="btn btn-default" href="/ThisLife/RenewTheGuaranteeCellList/RenewGCellListWithChineseName.aspx" role="button">热点保障小区</a></p>
            </div>




        </div>
    </section>





</asp:Content>

