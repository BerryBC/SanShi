<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RenewGCellListGuide.aspx.vb" Inherits="ThisLife_RenewTheGuaranteeCellList_RenewGCellListGuide" %>
<%----第22号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section style="padding-bottom: 20px;">
        <div class="container">
            <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                <h2 style="border-bottom-style: groove;">请选择哪种方式更新小区列表 ( Choose The Way To Update Guarantee Cell List )</h2>
            </div>
            <div class="col-sm-6 alert alert-info" style="text-align: center; text-align: left;">
                </br><strong>
                            1、根据小区Cell ID等标识更新：</br></br>
                            ① 2G：使用小区英文名更新；</br></br>
                            ② TD：使用RNCID以及CellID更新；</br></br>
                            ③ LTE：使用eNodeBID以及CellID更新。</br></br>
                </strong></br>
                           </br>
                <p><a class="btn btn-default" href="/ThisLife/RenewTheGuaranteeCellList/RenewGCellList.aspx" role="button">以ID标识更新小区</a></p>
            </div>
            <div class="col-sm-6 alert alert-warning" style="text-align: center; text-align: left;">
                </br><strong>
                            2、根据小区中文名等标识更新：</br></br>
                            ① 2G：使用小区英文名更新；</br></br>
                            ② TD：使用CellName（中文名）更新；</br></br>
                            ③ LTE：使用CellName（中文名）更新。</br></br>
                </strong></br>
                           </br>
                <p><a class="btn btn-default" href="/ThisLife/RenewTheGuaranteeCellList/RenewGCellListWithChineseName.aspx" role="button">以中文名标识更新小区</a></p>
            </div>




        </div>
    </section>
</asp:Content>

