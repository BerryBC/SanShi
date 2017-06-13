//定义神经网络变量
var layer_defs, net, trainer;
var avloss = 0.0;

//中间存储变量
var arrInput = [];
var arrOutput = [];
var adjectInput = [];
var adjectOutput = [];
var arrFromBeginToEnd = [];
var arrFromCSVOutput = [];
var arrTmpInput = [];
var arrTmpOutput = [];
var funCB;


//初始化设置参数
var intCountTime = 0;
var intMaxRepeatTime = 180;
var floLearnRate = 0.008;
var floMomentum = 0.9;
var intBatchSize = 7;
var floL2Decay = 0.0;
var floFirstError = 100;
var intInArrayCount = 0;
var floGoSpeed = 1;
var intLittleTime = 0;




//初始网络情况
function InitNetwork() {
    //初始化周边参数
    intInputCount = goTranJson.InputCompressPara.length;
    //预设网络
    layer_defs = [];
    layer_defs.push({ type: 'input', out_sx: 1, out_sy: 1, out_depth: intInputCount });
    layer_defs.push({ type: 'fc', num_neurons: intInputCount * 10 + 3, activation: 'relu' });
    layer_defs.push({ type: 'fc', num_neurons: Math.round(intInputCount / 2 * 10 + 3), activation: 'relu' });
    layer_defs.push({ type: 'fc', num_neurons: Math.round(intInputCount / 3 * 10 + 3), activation: 'relu' });
    layer_defs.push({ type: 'regression', num_neurons: 1 });

    //若在全局没定义，则
    if (typeof (floLearnRate) == "undefined") { floLearnRate = 0.008 };
    if (typeof (floMomentum) == "undefined") { floMomentum = 0.9 };
    if (typeof (intBatchSize) == "undefined") { intBatchSize = 7 };
    if (typeof (floL2Decay) == "undefined") { floL2Decay = 0.0 };

    net = new convnetjs.Net();

    if (Object.keys(goTranJson.NetWork).length == 0) {
        net.makeLayers(layer_defs);
        console.log('使用新网络');
    } else {
        var jsonNet = goTranJson.NetWork;
        net.fromJSON(jsonNet);
        console.log('使用已经过训练的网络');
    };
    trainer = new convnetjs.SGDTrainer(net, { learning_rate: floLearnRate, momentum: floMomentum, batch_size: intBatchSize, l2_decay: floL2Decay });
};

function InitDataTime() {
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
        return true;
    } else {
        console.log('Wrong Date');
        return false;
    };
};

function HandleTrainData() {
    var intNowInputMathchOutput = 0;
    goTranJson.TrueData.forEach(function (data) {
        if (intNowInputMathchOutput < arrInput.length) {
            var dateThisData = new Date(data[0]);
            var dateForInput = new Date(arrInput[intNowInputMathchOutput][0] + '-' + arrInput[intNowInputMathchOutput][1] + '-' + arrInput[intNowInputMathchOutput][2]);
            if (dateForInput <= dateThisData) {
                while (dateForInput < dateThisData) {
                    intNowInputMathchOutput++;
                    if (intNowInputMathchOutput >= arrInput.length) {
                        break;
                    };
                    dateForInput = new Date(arrInput[intNowInputMathchOutput][0] + '-' + arrInput[intNowInputMathchOutput][1] + '-' + arrInput[intNowInputMathchOutput][2]);
                };
                arrOutput[intNowInputMathchOutput] = data[1];
            };
        };
    }, this);

    for (var tmpI = 0; tmpI < arrInput.length; tmpI++) {
        if (arrOutput[tmpI]) {
            var arrTmpOneInput = [];
            for (var tmpJ = 0; tmpJ < goTranJson.InputCompressPara.length; tmpJ++) {
                arrTmpOneInput.push((arrInput[tmpI][tmpJ] - goTranJson.InputCompressPara[tmpJ][1]) / goTranJson.InputCompressPara[tmpJ][0]);
            };
            arrTmpInput.push(arrTmpOneInput);
            arrTmpOutput.push((arrOutput[tmpI] - goTranJson.OutputCompressPara[0][1]) / goTranJson.OutputCompressPara[0][0]);
        };
    };
};


//大循环
function BigLoop(CBFun) {
    if (typeof (CBFun) == 'function') {
        funCB = CBFun;
    } ;

    if (intCountTime > intMaxRepeatTime) {
        intMaxRepeatTime *=2;
        var jsonNet = net.toJSON();
        var strJsonNet = JSON.stringify(jsonNet);

        $.ajax({
            type: 'post',
            url: "http://" + window.location.host + strEnginePath,
            async: true,
            //问题来了
            data: { 'WhatReason': 2, 'NumberOfNetwork': goTranJson.NumberOfNetwork, 'TheNet': strJsonNet },
            success: function (result) {
                var gotJson = eval('(' + result + ')');
                if (gotJson.GotError == 0) {
                    console.log('成功上传');

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


    } ;
        if (intCountTime > 0) {


            avloss /= arrTmpInput.length;
            if (intCountTime == 1) {
                floFirstError = avloss;
            };
            trainer.learning_rate = floLearnRate * avloss / floFirstError / (1 + intCountTime / (intMaxRepeatTime ));


                funCB(intCountTime, avloss, trainer.learning_rate)

            //console.log('第' + intCountTime + '次训练');
            //console.log('此次数据数据，差值为:' + avloss);


        };
        intCountTime++;
        intLittleTime = 0;


        floLastError = avloss;
        //console.log('学习率' + floLearnRate);
        //console.log('现在的学习率为:' + trainer.learning_rate);
        GoTrain();


};


//每一次训练
function GoTrain() {
    var strTmpRow = '';
    var x = new convnetjs.Vol(1, 1, intInputCount);
    for (var tmpT = 0; tmpT <60; tmpT++) {
        if (intLittleTime >= arrTmpInput.length) { break; };
        x.w = arrTmpInput[intLittleTime];
        var stats = trainer.train(x, [arrTmpOutput[intLittleTime]]);
        avloss += (stats.loss);
        intLittleTime++;
    };

    if (intLittleTime < arrTmpInput.length) {
        setTimeout(GoTrain, 1000 / floGoSpeed);
    } else {
        BigLoop();
    };

};


function ChangeSpeed(floSpeed) {
    floGoSpeed = floSpeed;
};

function ChangeTrainer() {

    if (typeof (floLearnRate) == "undefined") { floLearnRate = 0.008 };
    if (typeof (floMomentum) == "undefined") { floMomentum = 0.9 };
    if (typeof (intBatchSize) == "undefined") { intBatchSize = 7 };
    if (typeof (floL2Decay) == "undefined") { floL2Decay = 0.0 };
    floLearnRate = document.getElementById("textLearnRate").value;
    floMomentum = document.getElementById("textMomentum").value;
    intBatchSize = document.getElementById("textBatchSize").value;
    floL2Decay = document.getElementById("textL2Decay").value;

    trainer = new convnetjs.SGDTrainer(net, { learning_rate: floLearnRate, momentum: floMomentum, batch_size: intBatchSize, l2_decay: floL2Decay });

};