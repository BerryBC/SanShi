<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="NNCal.aspx.vb" Inherits="AfterLife_NeuralNetworks_NNCal" %>

<%----第35号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="<%=ResolveClientUrl("~/JS/echarts.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/JS/convnetJS/convnet.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/JS/AfterLife/NeuralNetworks/NeuralCalculation.js")%>" type="text/javascript"></script>

    <script type="text/javascript">
        var myChart;
        var strEnginePath;
        var data1 = [];
        var goTranJson = {};
        var intInputCount;
        var intNowCal = 0;
        var option;
        var arrHistoryTrainInfo = [];

        $(function () {
            window.addEventListener('resize', ResizeTheWindows, false);
            GoCAL();
        });
        function ResizeTheWindows() {
            var divShowResult = document.getElementById('ShowResult');
            divShowResult.style.height = '500px;'
            myChart.setOption(option);

            myChart.resize();
            TopSearchTextBoxHide();
        };

        function setOutputContainer(text) {
            document.getElementById("ShowResult").innerHTML += ('<br/>' + text);
        };

        function GetRateAndCount(intCountTime, avloss, learning_rate) {
            var strShow = '';
            data1.push({ name: intCountTime, value: [intCountTime, avloss] });
            if (data1.length > 100) {
                data1.shift();
            };
            myChart.setOption(option);
            //setOutputContainer('第 ' + intCountTime + ' 次，现在的误差为： ' + avloss + ' ，学习率为： ' + learning_rate);
            //console.log('第 ' + intCountTime + ' 次，现在的误差为： ' + avloss + ' ，学习率为： ' + learning_rate);
            if (arrHistoryTrainInfo.length >= 10) {
                arrHistoryTrainInfo.shift();
            };
            arrHistoryTrainInfo.push('<strong>第 ' + intCountTime + ' 次，现在的误差为： ' + avloss + ' ，学习率为： ' + learning_rate + '</strong><br/>');
            arrHistoryTrainInfo.forEach(function (data) {
                strShow += data;
            }, this);


            document.getElementById("ShowNowCalInfo").innerHTML = (strShow);

        };

        function GoCAL() {
            // 基于准备好的dom，初始化echarts实例
            myChart = echarts.init(document.getElementById('ShowResult'));


            option = {
                title: {
                    text: '误差率',
                    left: 'center',
                },
                tooltip: {
                    trigger: 'axis',
                    formatter: function (params) {
                        params = params[0];
                        return '第 ' + params.value[0] + ' 次误差率为 : ' + params.value[1];
                    },
                    axisPointer: {
                        animation: false
                    }
                },
                xAxis: {
                    type: 'value',
                    boundaryGap: ['0%', '0%'],
                    splitLine: {
                        show: false
                    },
                    min: 'dataMin',
                    max: 'dataMax'

                },
                yAxis: {
                    type: 'value',
                    boundaryGap: ['0%', '100%'],
                    splitLine: {
                        show: false
                    }
                },
                series: [{
                    name: '误差率',
                    type: 'line',
                    showSymbol: false,
                    hoverAnimation: false,
                    data: data1
                }]
            };
            myChart.setOption(option);


            strEnginePath = "/AfterLife/NeuralNetworks/NNBack.ashx"
            $.ajax({
                type: 'get',
                url: "http://" + window.location.host + strEnginePath,
                async: true,
                data: { 'WhatReason': 1 },
                success: function (result) {
                    var gotJson = eval('(' + result + ')');
                    if (gotJson.GotError == 0) {
                        goTranJson = gotJson;
                        InitNetwork();
                        InitDataTime();
                        HandleTrainData();
                        BigLoop(GetRateAndCount);


                    } else {
                        setOutputContainer(result);
                        setOutputContainer('错误了，错误代码为：' + gotJson.GotError + '错误原因为：' + gotJson.ErrorDsp);
                        setOutputContainer('------------------');
                    };
                },
                error: function () {
                    setOutputContainer("Error");
                }
            });
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section style="padding-bottom: 20px;">
        <div class="container">
            <div id="HeadDiv" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                    <h2 style="border-bottom-style: groove;">准备计算 ( Prepare Calculation )</h2>
                </div>
            </div>
            <div class="col-sm-12" style="text-align: center; margin-top: 20px;">
                <table id="ContentPlaceHolder1_tbOutPut" class="table table-striped" style="padding-bottom: 0px; margin-bottom: 0px; text-align: center; align-items: center;">
                    <tr>
                        <td>学习率为 :<input type="text" id="textLearnRate" value="0.008" /></td>
                        <td>束大小 :<input type="text" id="textBatchSize" value="7" /></td>

                    </tr>
                    <tr>
                        <td>动量大小 :<input type="text" id="textMomentum" value="0.9" /></td>
                        <td>L2衰退 :<input type="text" id="textL2Decay" value="0" /></td>

                    </tr>
                    <tr>
                        <td>
                            <select id="selSpeed" onchange="ChangeSpeed(this.value)">
                                <option value="500">超快</option>
                                <option value="100">比较快</option>
                                <option value="10">小快</option>
                                <option value="1" selected="selected">一般</option>
                                <option value="0.2">慢</option>
                            </select>
                        </td>
                        <td>
                            <input type="button" id="btnGo" value="更改训练方式" class="btn-warning btn" onclick="ChangeTrainer()" /></td>


                    </tr>

                </table>

            </div>
            <div class="col-sm-12">
                <div id="ShowNowCalInfo" class="alert alert-info" role="alert" style="margin-top: 5px;">
                    <strong>正在进行计算~耐心~耐心哟~</strong>
                </div>
                <div id="ShowResult" class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width: 100%; height: 500px">
                </div>

            </div>
    </section>

</asp:Content>

