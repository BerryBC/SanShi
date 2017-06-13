<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="NNShowAllNetwork.aspx.vb" Inherits="AfterLife_NeuralNetworks_NNShowAllNetwork" %>

<%----第36号页面--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="<%=ResolveClientUrl("~/JS/echarts.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/JS/convnetJS/convnet.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/JS/AfterLife/NeuralNetworks/NeuralCalculation.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        var myChart;
        var strEnginePath;
        var date = [];
        var data1 = [];
        var data2 = [];
        var goTranJson = {};
        var intInputCount;

        $(function () {
            window.addEventListener('resize', ResizeTheWindows, false);
            //GoShow();
            GetAllName();
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
        function GetAllName() {
            strEnginePath = "/AfterLife/NeuralNetworks/NNBack.ashx"

            $.ajax({
                type: 'GET',
                url: "http://" + window.location.host + strEnginePath,
                async: true,
                data: { 'WhatReason': 3 },
                success: function (result) {
                    var gotJson = eval('(' + result + ')');
                    console.log(result);
                    if (gotJson.GotError == 0) {
                        gotJson.SeriesOfNetwork.forEach(function (eleArrName) {
                            //----------
                            var domeleOneOption = document.createElement('option');
                            domeleOneOption.text = eleArrName[1];
                            domeleOneOption.value = eleArrName[0];

                            document.getElementById("selTemplate").add(domeleOneOption);
                        }, this);



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


        function GoShow() {
            // 基于准备好的dom，初始化echarts实例
            date = [];
            data1 = [];
            data2 = [];
            arrInput = [];
            arrOutput = [];
            myChart = echarts.init(document.getElementById('ShowResult'));
            function AfterImportCSV() {

                var arrTmpCalOutput = [];

                if (!InitDataTime()) {
                    setOutputContainer('Wrong Date');
                };


                var intNowInputMathchOutput = 0;
                goTranJson.TrueData.forEach(function (data) {
                    var dateThisData = new Date(data[0]);
                    var dateForInput = new Date(arrInput[intNowInputMathchOutput][0] + '-' + arrInput[intNowInputMathchOutput][1] + '-' + arrInput[intNowInputMathchOutput][2]);

                    if (dateForInput <= dateThisData) {
                        while (dateForInput < dateThisData) {
                            intNowInputMathchOutput++;
                            dateForInput = new Date(arrInput[intNowInputMathchOutput][0] + '-' + arrInput[intNowInputMathchOutput][1] + '-' + arrInput[intNowInputMathchOutput][2]);
                        };
                        arrOutput[intNowInputMathchOutput] = data[1];
                    };
                }, this);

                for (var tmpI = 0; tmpI < arrInput.length; tmpI++) {
                    var arrTmpOneEndOutput = [];
                    var arrTmpOneInput = [];
                    var arrNowOutput = [];
                    for (var tmpJ = 0; tmpJ < goTranJson.InputCompressPara.length; tmpJ++) {
                        arrTmpOneInput.push((arrInput[tmpI][tmpJ] - goTranJson.InputCompressPara[tmpJ][1]) / goTranJson.InputCompressPara[tmpJ][0]);
                    };
                    arrNowOutput.push((arrOutput[tmpI] - goTranJson.OutputCompressPara[0][1]) / goTranJson.OutputCompressPara[0][0]);

                    var netx = new convnetjs.Vol(1, 1, intInputCount);
                    netx.w = arrTmpOneInput;
                    var a = net.forward(netx);
                    arrTmpCalOutput[tmpI] = ((a.w[0] * goTranJson.OutputCompressPara[0][0]) + goTranJson.OutputCompressPara[0][1]);
                    date.push(arrInput[tmpI][0] + '-' + arrInput[tmpI][1] + '-' + arrInput[tmpI][2]);
                    data1.push(parseFloat(arrOutput[tmpI]).toFixed(2));
                    data2.push(parseFloat(arrTmpCalOutput[tmpI]).toFixed(2));
                };
                myChart.setOption(option);
            };
            strEnginePath = "/AfterLife/NeuralNetworks/NNBack.ashx"
            $.ajax({
                type: 'get',
                url: "http://" + window.location.host + strEnginePath,
                async: true,
                data: { 'WhatReason': 0, 'NumberOfNet': document.getElementById("selTemplate").value },
                success: function (result) {
                    var gotJson = eval('(' + result + ')');
                    if (gotJson.GotError == 0) {
                        goTranJson = gotJson;
                        option = {
                            tooltip: {
                                trigger: 'axis',
                                position: function (pt) {
                                    return [pt[0], pt[1]];
                                }
                            },
                            legend: {
                                width: 'auto',
                                height: 'auto',
                                x: 'left',
                                data: ["真实数据", "预测数据"]
                            },
                            title: {
                                left: 'center',
                                text: goTranJson.TitleForNetwork,
                            },
                            toolbox: {
                                feature: {
                                    dataZoom: {
                                        yAxisIndex: 'none'
                                    },
                                    restore: {},
                                    saveAsImage: {}
                                }
                            },
                            xAxis: {
                                type: 'category',
                                boundaryGap: true,
                                data: date
                            },
                            yAxis: {
                                type: 'value',
                                boundaryGap: ['0%', '100%']
                            },
                            dataZoom: [{
                                type: 'inside',
                                start: 0,
                                end: 10
                            }, {
                                start: 0,
                                end: 10
                            }],
                            series: [
                                {
                                    name: '真实数据',
                                    type: 'line',
                                    smooth: true,
                                    symbol: 'none',
                                    data: data1,

                                },
                                {
                                    name: '预测数据',
                                    type: 'line',
                                    smooth: true,
                                    symbol: 'none',
                                    data: data2,

                                }
                            ]
                        };
                        InitNetwork();
                        AfterImportCSV();


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
                    <h2 style="border-bottom-style: groove;">全网业务量预测值 ( Whole Network Traffic Forecast )</h2>
                </div>
            </div>
                        <div class="col-sm-12" style="text-align: center; margin-top: 20px;">
                <table id="ContentPlaceHolder1_tbOutPut" class="table table-striped" style="padding-bottom: 0px; margin-bottom: 0px; text-align: center; align-items: center;">
                    <tr>
                        <td>
                            <select id="selTemplate" onchange="" style="padding:5px;">


                            </select>
                        </td>
                        <td>
                            <input type="button" id="btnGo" value="查看" class="btn-warning btn" onclick="GoShow()" /></td>


                    </tr>

                </table>

            </div>

            <div id="ShowResult" class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width: 100%; height: 500px">
            </div>

        </div>
    </section>

</asp:Content>

