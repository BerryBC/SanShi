<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="NNShowAllNetwork.aspx.vb" Inherits="AfterLife_NeuralNetworks_NNShowAllNetwork" %>

<%----第36号页面--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="<%=ResolveClientUrl("~/JS/echarts.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/JS/convnetJS/convnet.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            window.addEventListener('resize', ResizeTheWindows, false);
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
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section style="padding-bottom: 20px;">
        <div class="container">
            <div id="HeadDiv" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                    <h2 style="border-bottom-style: groove;">LTE全网业务量预测值 ( LTE Network Traffic Forecast )</h2>
                </div>
            </div>
            <div id="ShowResult" class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width: 100%; height: 500px">
                <script type="text/javascript">
                    // 基于准备好的dom，初始化echarts实例
                    var myChart = echarts.init(document.getElementById('ShowResult'));
                    var strEnginePath;
                    var date = [];
                    var data1 = [];
                    var data2 = [];
                    var goTranJson = {};
                    var intInputCount;


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
                            text: '2G忙时话务量以及预测',
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
                                //itemStyle: {
                                //    normal: {
                                //        color: 'rgba(120, 36, 50, 0.8)'
                                //    }
                                //}
                            },
                            {
                                name: '预测数据',
                                type: 'line',
                                smooth: true,
                                symbol: 'none',
                                data: data2,
                                //itemStyle: {
                                //    normal: {
                                //        color: 'rgba(168, 100, 188, 1)'
                                //    }
                                //}
                            }
                        ]
                    };

                    //初始网络情况
                    function InitNetwork() {
                        //初始化周边参数
                        intInputCount = goTranJson.InputCompressPara.length;

                        net = new convnetjs.Net();

                        var jsonNet = goTranJson.NetWork;
                        net.fromJSON(jsonNet);

                    };

                    function AfterImportCSV() {
                        var arrEndOutput = [];
                        var arrInput = [];
                        var arrOutput = [];
                        var arrTmpCalOutput = [];

                        var dateBegin = new Date(goTranJson.DateRange.strBegin);
                        var dateEnd = new Date(goTranJson.DateRange.strEnd);
                        //根据开始结束时间做一个Array表
                        if (dateBegin < dateEnd) {
                            var dateInsertNow = new Date(dateBegin);
                            var intFestivalDay = 0;
                            while (dateInsertNow <= dateEnd) {
                                var tmpArrayToInput = [];
                                for (var tmpI = 0; tmpI < intInputCount; tmpI++) {
                                    tmpArrayToInput.push(0);
                                };
                                tmpArrayToInput[0] = dateInsertNow.getFullYear();
                                tmpArrayToInput[1] = dateInsertNow.getMonth() + 1;
                                tmpArrayToInput[2] = dateInsertNow.getDate();
                                tmpArrayToInput[3] = dateInsertNow.getDay();
                                while (intFestivalDay < goTranJson.Festival.length && dateInsertNow >= new Date(goTranJson.Festival[intFestivalDay][0])) {
                                    if ((dateInsertNow - new Date(goTranJson.Festival[intFestivalDay][0])) == 0) {
                                        tmpArrayToInput[goTranJson.Festival[intFestivalDay][1]] = goTranJson.Festival[intFestivalDay][2];
                                    };
                                    intFestivalDay++;
                                    if (intFestivalDay >= goTranJson.Festival.length) { break; };

                                };
                                arrInput.push(tmpArrayToInput);
                                arrOutput.push(null);
                                dateInsertNow.setDate(dateInsertNow.getDate() + 1);
                            };
                        } else {
                            console.log('Wrong Date');
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
                            data1.push(parseFloat( arrOutput[tmpI]).toFixed(2));
                            data2.push(parseFloat(arrTmpCalOutput[tmpI]).toFixed(2));
                        };

                        myChart.setOption(option);


                    };




                    strEnginePath = "/AfterLife/NeuralNetworks/NNBack.ashx"

                    $.ajax({
                        type: 'get',
                        url: "http://" + window.location.host + strEnginePath,
                        async: true,
                        //data: shit,
                        success: function (result) {
                            var gotJson = eval('(' + result + ')');
                            if (gotJson.GotError == 0) {
                                goTranJson = gotJson;
                                InitNetwork();
                                AfterImportCSV();
                                //gotJson.TrueData.forEach(function (element) {
                                //    date.push(element[0]);
                                //    data.push(element[1]);
                                //}, this);
                                //myChart.setOption(option);




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

                </script>

            </div>

        </div>
    </section>

</asp:Content>

