<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Main.aspx.vb" Inherits="Main" %>

<%----第4号页面--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                            <p><a class="btn btn-default" href="Preexistence/LoadIndexOfCell.aspx" role="button">Karma Gain »</a></p>
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
                            <h4>保障期间通报发布</h4>
                            <p>根据保障期间实时话务自动生成通报以及小区指标情况。</p>
                            <p style="color: red;">（仍在建设中） </p>
                            <p><a class="btn btn-default" href="/JustBuilding.aspx" role="button">Karma Gain »</a></p>
                        </div>
                                                <div class="col-md-4">
                            <h4>LTE小区级参数查询</h4>
                            <p>查出LTE小区级参数、修改历史。（以基站信息表信息为准）</p>
                            <p style="color: red;">（仍在建设中） </p>
                            <p><a class="btn btn-default" href="/JustBuilding.aspx" role="button">Karma Gain »</a></p>
                        </div>

                    </div>
                </div>



                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="wow bounceInLeft" data-wow-delay="0.1s">
                        <h2 style="border-bottom-style: groove;">尘缘未了来生报 ( Bonds Outstanding ,  Serve AfterLife )</h2>
                    </div>
                    <div class="col-sm-12">
                        <div class="col-md-4">
                            <h4>三滚规划业务预测</h4>
                            <p>根据以前的全网业务量对三滚规划的预测。</p>
                            <p style="color: red;">（仍在建设中） </p>
                            <p><a class="btn btn-default" href="/JustBuilding.aspx" role="button">Karma Gain »</a></p>
                        </div>
                    </div>
                </div>



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

                    </div>
                </div>


            </div>
        </div>



    </section>

</asp:Content>

