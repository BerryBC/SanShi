<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="NNCheck.aspx.vb" Inherits="AfterLife_NeuralNetworks_NNCheck" %>

<%----第40号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="<%=ResolveClientUrl("~/JS/echarts.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/JS/convnetJS/convnet.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/JS/AfterLife/NeuralNetworks/NeuralCalculation.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        var strEnginePath;
        var date = [];
        var data1 = [];
        var data2 = [];
        var goTranJson = {};
        var intInputCount;
        var objWhatNetwork = { arrWhichNN: [], arrAllNN: [] };
        var intHandleCount = 0;
        var intMaxHandleCount = 0;

        $(function () {
            GetAllPendingDetectionNetwork();
        });

        function GetAllPendingDetectionNetwork() {
            strEnginePath = "/AfterLife/NeuralNetworks/NNBack.ashx"

            $.ajax({
                type: 'GET',
                url: "http://" + window.location.host + strEnginePath,
                async: true,
                data: { 'WhatReason': 4 },
                success: function (result) {
                    var gotJson = eval('(' + result + ')');
                    if (gotJson.GotError == 0) {
                        gotJson.PendingDetectionNetwork.forEach(function (eleArrName) {
                            //----------
                            var arrNN = [];

                            arrNN.push(eleArrName[1]);
                            arrNN.push(eleArrName[0]);
                            objWhatNetwork.arrAllNN.push(arrNN);
                        }, this);
                        CalAllPDNetwork();
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
        function setOutputContainer(text) {
            document.getElementById("ShowResult").innerHTML += ('<br/><strong>' + text + '</strong>');
        };
        function CalAllPDNetwork() {
            if (objWhatNetwork.arrAllNN.length > 0) {
                intMaxHandleCount = objWhatNetwork.arrAllNN.length;
                objWhatNetwork.arrAllNN.forEach(function (eleArrName) {
                    setOutputContainer("有 " + eleArrName[0] + " 的 " + eleArrName[1]);

                    $.ajax({
                        type: 'get',
                        url: "http://" + window.location.host + strEnginePath,
                        async: true,
                        data: { 'WhatReason': 5, 'NumberOfNet': eleArrName[0], 'NumberOfTmpNet': eleArrName[1] },
                        success: function (result) {
                            var gotJson = eval('(' + result + ')');
                            if (gotJson.GotError == 0) {
                                var floError = 0.0;

                                goTranJson = gotJson;
                                InitNetwork();
                                AfterImportCSV();
                                floError = CalTheError();
                                setOutputContainer(eleArrName[0] + " 中 " + eleArrName[1] + " 的误差是 " + floError);

                                if (typeof (objWhatNetwork.arrWhichNN[eleArrName[0]]) == 'undefined') {
                                    objWhatNetwork.arrWhichNN[eleArrName[0]] = { floNowError: 0.0, intNowBestTmpID: 0, strTemplate: '' };
                                    objWhatNetwork.arrWhichNN[eleArrName[0]].floNowError = floError;
                                    objWhatNetwork.arrWhichNN[eleArrName[0]].intNowBestTmpID = eleArrName[1];
                                    objWhatNetwork.arrWhichNN[eleArrName[0]].strTemplate = eleArrName[0];
                                } else {
                                    if (floError < objWhatNetwork.arrWhichNN[eleArrName[0]].floNowError) {
                                        objWhatNetwork.arrWhichNN[eleArrName[0]].floNowError = floError;
                                        objWhatNetwork.arrWhichNN[eleArrName[0]].intNowBestTmpID = eleArrName[1];
                                        objWhatNetwork.arrWhichNN[eleArrName[0]].strTemplate = eleArrName[0];
                                    };
                                };
                            } else {
                                setOutputContainer(result);
                                setOutputContainer('错误了，错误代码为：' + gotJson.GotError + '错误原因为：' + gotJson.ErrorDsp);
                                setOutputContainer('------------------');
                            };
                            intHandleCount++;
                            LastOutput();
                        },
                        error: function () {
                            setOutputContainer("Error");
                        }
                    });
                }, this);
            } else {
                setOutputContainer("没有待检测网络哟喵~");
            };

        };
        function LastOutput() {
            if (intHandleCount >= intMaxHandleCount) {
                objWhatNetwork.arrWhichNN.forEach(function (eleNN) {
                    setOutputContainer(eleNN.strTemplate + " 的最佳模板是: " + eleNN.intNowBestTmpID.toString());

                    $.ajax({
                        type: 'get',
                        url: "http://" + window.location.host + strEnginePath,
                        async: true,
                        data: { 'WhatReason': 0, 'NumberOfNet': eleNN.strTemplate },
                        success: function (result) {
                            var gotJson = eval('(' + result + ')');
                            if (gotJson.GotError == 0) {
                                var floError = 0.0;
                                var intB = 0;
                                goTranJson = gotJson;
                                InitNetwork();
                                AfterImportCSV();
                                floError = CalTheError();

                                setOutputContainer(eleNN.strTemplate + " 原本的误差是: " + floError.toString());
                                if (floError > eleNN.floNowError) {
                                    setOutputContainer(eleNN.strTemplate + " 原本的网络不好，用新的去修改他。 ");
                                    intB = 1;
                                } else {
                                    setOutputContainer(eleNN.strTemplate + " 原本的网络很好，不用改了。 ");
                                    intB = 0;
                                };
                                    $.ajax({
                                        type: 'get',
                                        url: "http://" + window.location.host + strEnginePath,
                                        async: true,
                                        data: { 'WhatReason': 6, 'NumberOfNet': eleNN.strTemplate, 'NumberOfTmpNet': eleNN.intNowBestTmpID,'IsChange':intB },
                                        success: function (result) {
                                            var gotJson = eval('(' + result + ')');
                                            if (gotJson.GotError == 0) {
                                                setOutputContainer(eleNN.strTemplate + " 修改网络结果: " + gotJson.FeedBack);

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


                }, this);
            };
        };
        function AfterImportCSV() {

            var arrTmpCalOutput = [];
            data1 = [];
            data2 = [];
            date = [];
            arrOutput = [];
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
                data1.push(parseFloat(arrOutput[tmpI]));
                data2.push(parseFloat(arrTmpCalOutput[tmpI]));
            };
        };
        function CalTheError() {
            var floAvLoss = 0.0;
            var intHowManyTime = 0;
            for (var tmpI = 0; tmpI < data1.length; tmpI++) {
                if (data1[tmpI] && data2[tmpI]) {
                    floAvLoss += 2 * (data1[tmpI] - data2[tmpI]) * (data1[tmpI] - data2[tmpI]);
                    intHowManyTime++;
                };
            };
            floAvLoss /= intHowManyTime;
            return floAvLoss;
        };


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section style="padding-bottom: 20px;">
        <div class="container">
            <div id="HeadDiv" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                    <h2 style="border-bottom-style: groove;">全网业务量预测神经网络验算 ( Whole Network Traffic Forecast Neural Network Check)</h2>
                </div>
            </div>


            <div id="ShowResult" class="col-lg-12 col-md-12 col-sm-12 col-xs-12 alert alert-info" style="width: 100%;margin: 20px;padding-bottom:20px;" >
         
            </div>

        </div>
    </section>

</asp:Content>

