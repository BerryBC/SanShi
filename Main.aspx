﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Main.aspx.vb" Inherits="Main" %>

<%----第4号页面--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        $('#SearchKW').bind('submit', function () {
            return false;
        });
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <section style="text-align: left; padding-bottom: 20px;">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="wow bounceInLeft" data-wow-delay="0.1s">
                        <h2 style="border-bottom-style: groove;">前世因 ( Cause of Preexistence )</h2>
                    </div>
                    <div class="col-sm-12">
                        <div class="col-md-4">
                            <h4>GSM网格小区指标批量导出</h4>
                            <p>批量导出GSM网格小区列表信息。</p>

                            <p><a class="btn btn-default" href="Preexistence/LoadIndexOfCell/LoadIndexOfCell.aspx" role="button">Karma Gain »</a></p>
                        </div>
                        <div class="col-md-4">
                            <h4>GSM六忙时原始数据导出</h4>
                            <p>指标、业务量原始数据导出（汇总但未计算指标）。</p>
                            <p><a class="btn btn-default" href="Preexistence/LoadIndexOfCell/LoadIndicatorsOfSixBusyHourOfCell.aspx" role="button">Karma Gain »</a></p>
                        </div>
                        <div class="col-md-4">
                            <h4>GSM 24小时干扰指标</h4>
                            <p>24小时干扰指标及业务量导出（暂只支持白云花都）。</p>
                            <p><a class="btn btn-default" href="Preexistence/LoadIndexOfCell/LoadInterfereOfCell24Hour.aspx" role="button">Karma Gain »</a></p>
                        </div>
                        <div class="col-md-4">
                            <h4>批量导出GSM分区级天汇总业务量</h4>
                            <p>全网所有分区级全天业务量表格导出。</p>
                            <p><a class="btn btn-default" href="Preexistence/LoadIndexOfCell/LoadDailyRegionTraffic.aspx" role="button">Karma Gain »</a></p>
                        </div>

                        <div class="col-md-4">
                            <h4>批量导出GSM小区级天汇总业务量</h4>
                            <p>小区级全天业务量表格导出（暂只支持番禺）。</p>
                            <p><a class="btn btn-default" href="Preexistence/LoadIndexOfCell/LoadDailyCellTraffic.aspx" role="button">Karma Gain »</a></p>
                        </div>
                        <div class="col-md-4">
                            <h4>批量导出GSM小区级小时级业务量</h4>
                            <p>导出小区小时级业务量及部分指标。</p>
                            <p><a class="btn btn-default" href="Preexistence/LoadIndexOfCell/Load24HourDataOfCell.aspx" role="button">Karma Gain »</a></p>
                        </div>
                    </div>

                </div>



                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="wow bounceInLeft" data-wow-delay="0.1s">
                        <h2 style="border-bottom-style: groove;">今世果 ( Effect of This Life )</h2>
                    </div>
                    <div class="col-sm-12">
                        <div class="col-md-4">
                            <h4>BSC级容量查询</h4>
                            <p>现网BSC级容量查询，为某一License或功能的容量对比。</p>
                            <p><a class="btn btn-default" href="/ThisLife/BSCPara/BSCParaDiagram.aspx" role="button">Karma Gain »</a></p>
                        </div>

                        <div class="col-md-4">
                            <h4>两点之间距离值计算</h4>
                            <p>批量计算两份表中两点间距离的计算。（按一定粒度来计算）</p>
                            <p style="color: red;">（仍在建设中） </p>
                            <p><a class="btn btn-default" href="/ThisLife/DistanceOfTwoSpot/DistanceOfTwoSpot.aspx" role="button">Karma Gain »</a></p>
                        </div>
                        <div class="col-md-4">
                            <h4>多元线性回归计算</h4>
                            <p>根据数据查找出多元线性回归的系数，用来计算两个或以上变量时的线性系数。</p>
                            <p style="color: red;">（仍在建设中） </p>
                            <p><a class="btn btn-default" href="/ThisLife/MultipleLinearRegression/CountMultipleLinearRegression.aspx" role="button">Karma Gain »</a></p>
                        </div>
                        <div class="col-md-4">
                            <h4>更新保障小区列表</h4>
                            <p>根据站点存储的基站信息更新保障小区列表。</p>
                            <p><a class="btn btn-default" href="/ThisLife/RenewTheGuaranteeCellList/RenewGCellListGuide.aspx" role="button">Karma Gain »</a></p>
                        </div>
                        <div class="col-md-4">
                            <h4>基站信息表格下载</h4>
                            <p>下载全网各类基站的信息</p>
                            <p><a class="btn btn-default" href="/ThisLife/DownBSDetailTable/BSDetailTableDownload.aspx" role="button">Karma Gain »</a></p>
                        </div>
                        <div class="col-md-4">
                            <h4>GSM载调进度查询</h4>
                            <p>关联NAMS系统、本地生产支撑系统及现网载波数综合查询GSM载调进度。</p>
                            <p><a class="btn btn-default" href="/ThisLife/DownloadCarrierAdjustmentBill/DownloadCarrierAdjustmentBill.aspx" role="button">Karma Gain »</a></p>
                        </div>
                        <div class="col-md-4">
                            <h4>保障小区列表下载</h4>
                            <p>下载曾经保障过的保障小区列表。</p>
                            <p><a class="btn btn-default" href="/ThisLife/DownHotSpotCellList/HotSpotDownloadGuide.aspx" role="button">Karma Gain »</a></p>
                        </div>
                        <div class="col-md-4">
                            <h4>GSM小区信息展现</h4>
                            <p>展现GSM小区信息，包含参数、修改历史、地图等信息。</p>
                            <p><a class="btn btn-default" href="/ThisLife/CellInfo/GSMCellInfo.aspx" role="button">Karma Gain »</a></p>
                        </div>
                        <div class="col-md-4">
                            <h4>服务器文件下载</h4>
                            <p>下载服务器上固定目录的文件，能快速下载，能访问NQI等信息。</p>
                            <p><a class="btn btn-default" href="/ThisLife/DownBSDetailTable/DownloadServerFiles.aspx" role="button">Karma Gain »</a></p>
                        </div>
                        <div class="col-md-4">
                            <h4>三世云盘</h4>
                            <p>云盘文件上传下载。</p>
                            <p><a class="btn btn-default" href="/ThisLife/CloudDisk/CDUploadPage.aspx" role="button">Karma Gain »</a></p>
                        </div>
                        <div class="col-md-4">
                            <h4>经纬度转换</h4>
                            <p>对百度地图、GSP经纬度进行转换。</p>
                            <p><a class="btn btn-default" href="/ThisLife/CoordTransform/CoordTransform.aspx" role="button">Karma Gain »</a></p>
                        </div>
                        <div class="col-md-4">
                            <h4>从地图上选择小区</h4>
                            <p>基于百度地图，直接从地图上画多边形，给出多边形内小区。</p>
                            <p><a class="btn btn-default" href="/ThisLife/MapGood/SelectCellByMap.aspx" role="button">Karma Gain »</a></p>
                        </div>
                    </div>
                </div>



                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="wow bounceInLeft" data-wow-delay="0.1s">
                        <h2 style="border-bottom-style: groove;">尘缘未了来生报 ( Bonds Outstanding ,  Serve AfterLife )</h2>
                    </div>
                    <div class="col-sm-12">
                        <div class="col-md-4">
                            <h4>全网业务量预测页</h4>
                            <p>根据神经网络的全网业务量预测值。</p>
                            <p style="color: red;">（仍在建设中） </p>
                            <p><a class="btn btn-default" href="/AfterLife/NeuralNetworks/NNShowAllNetwork.aspx" role="button">Karma Gain »</a></p>
                        </div>
                        <div class="col-md-4">
                            <h4>全网业务量训练页</h4>
                            <p>进行神经网络训练。</p>
                            <p style="color: red;">（仍在建设中） </p>
                            <p><a class="btn btn-default" href="/AfterLife/NeuralNetworks/NNCal.aspx" role="button">Karma Gain »</a></p>
                        </div>
                        <div class="col-md-4">
                            <h4>全网业务量验算页</h4>
                            <p>进行全网业务量验算。</p>
                            <p style="color: red;">（仍在建设中） </p>
                            <p><a class="btn btn-default" href="/AfterLife/NeuralNetworks/NNCheck.aspx" role="button">Karma Gain »</a></p>
                        </div>
                    </div>
                </div>

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="wow bounceInLeft" data-wow-delay="0.1s">
                        <h2 style="border-bottom-style: groove;">知天命 ( Know The Fate )</h2>
                    </div>
                    <div class="col-sm-12">
                        <div class="col-md-4">
                            <h4>LTE小测试</h4>
                            <p>测试对LTE知识点的弱项及强项。</p>
                            <p><a class="btn btn-default" href="/KnowTheFate/LTELittleTest/LTELittleTest.aspx" role="button">Karma Gain »</a></p>
                        </div>
                    </div>
                </div>




                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="wow bounceInLeft" data-wow-delay="0.1s">
                        <h2 style="border-bottom-style: groove;">英格瑞斯 ( Ingress )</h2>
                    </div>
                    <div class="col-sm-12">
                        <div class="col-md-4">
                            <h4>工作地预测</h4>
                            <p>预测用户工作的地方。</p>
                            <p style="color: red;">（仍在建设中） </p>
                            <p><a class="btn btn-default" href="/Ingress/WorkPlace/GuessWorkPlace.aspx" role="button">Karma Gain »</a></p>
                        </div>
                    </div>
                </div>


                <asp:Panel ID="plMana" runat="server" Visible="false">


                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="wow bounceInLeft" data-wow-delay="0.1s">
                            <h2 style="border-bottom-style: groove;">天干 ( Heavenly Stem )</h2>
                        </div>
                        <div class="col-sm-12">
                            <div class="col-md-4">
                                <h4>用户管理</h4>
                                <p>管理三世书中的用户。</p>
                                <p><a class="btn btn-default" href="/HeavenlyStem/UserManagement/SuManagement.aspx" role="button">Karma Gain »</a></p>
                            </div>
                        </div>
                    </div>




                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="wow bounceInLeft" data-wow-delay="0.1s">
                            <h2 style="border-bottom-style: groove;">地支 ( Earthly Branch )</h2>
                        </div>
                        <div class="col-sm-12">
                            <div class="col-md-4">
                                <h4>基站基础信息管理</h4>
                                <p>管理三世书中所使用的基础信息表格以及更新来源。</p>
                                <p><a class="btn btn-default" href="/EarthlyBranch/BSDetails/BaseSationDetails.aspx" role="button">Karma Gain »</a></p>
                            </div>
                            <div class="col-md-4">
                                <h4>GSM参数信息管理</h4>
                                <p>管理三世书中所使用的GSM参数信息表格以及更新来源。</p>
                                <p><a class="btn btn-default" href="/EarthlyBranch/BSDetails/GSMBSCData.aspx" role="button">Karma Gain »</a></p>
                            </div>
                            <div class="col-md-4">
                                <h4>列队插入数据库的数据</h4>
                                <p>管理基站基础信息以及GSM参数信息管理的列队入数</p>
                                <p><a class="btn btn-default" href="/EarthlyBranch/BSDetails/InsertQueueOfBSCPara.aspx" role="button">Karma Gain »</a></p>
                            </div>
                            <div class="col-md-4">
                                <h4>导入2G在NQI上的数据</h4>
                                <p>把2G在NQI上的网格小区每日指标导入系统</p>
                                <p><a class="btn btn-default" href="/EarthlyBranch/TrafficStatistics/GSMIndexOfCell.aspx" role="button">Karma Gain »</a></p>
                            </div>
                            <div class="col-md-4">
                                <h4>基站信息Log信息</h4>
                                <p>查看基站信息的Log信息</p>
                                <p><a class="btn btn-default" href="/EarthlyBranch/LogViewer/BaseSationDetailsLogViewer.aspx" role="button">Karma Gain »</a></p>
                            </div>

                            <div class="col-md-4">
                                <h4>GSM的BSC及小区记录参数信息</h4>
                                <p>修改及查看每天例行P查什么数</p>
                                <p><a class="btn btn-default" href="/EarthlyBranch/BSDetails/GSMParaConfigPage.aspx" role="button">Karma Gain »</a></p>
                            </div>
                            <div class="col-md-4">
                                <h4>用户浏览过哪些网页信息</h4>
                                <p>查看用户浏览过的URL信息</p>
                                <p><a class="btn btn-default" href="/EarthlyBranch/LogViewer/TrackUserURL.aspx" role="button">Karma Gain »</a></p>
                            </div>

                        </div>
                    </div>

                </asp:Panel>
            </div>
        </div>



    </section>

</asp:Content>

