<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="LTELittleTest.aspx.vb" Inherits="KnowTheFate_LTELittleTest_LTELittleTest" %>

<%----第43号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        var claAns = function () {
            this.AnsButton;
        };
        claAns.prototype = {
            arrAns: [0, 0, 0, 0, 0, 0],
            funReturn: function () {
                var strRAns = '';
                for (var tmpI = 0; tmpI < 6; tmpI++) {
                    if (this.arrAns[tmpI] > 0) {
                        strRAns += String.fromCharCode(65 + tmpI);
                    }
                }
                return strRAns;
            },
            funClearn: function () {
                this.arrAns = [0, 0, 0, 0, 0, 0];

                for (var tmpI = 0; tmpI < this.AnsButton.length;tmpI++){
                    this.AnsButton[tmpI].className = 'btn btn-info';
                };


            },
            funGetButton: function (btnOption) {
                this.AnsButton = btnOption;
            }
        };

        var claAllCont = function () { };
        claAllCont.prototype = {
            funBegin: function () {
                this.FGoodPro = $('#divFG')[0];
                this.SGoodPro = $('#divSG')[0];
                this.TGoodPro = $('#divTG')[0];
                this.FBadPro = $('#divFB')[0];
                this.SBadPro = $('#divSB')[0];
                this.TBadPro = $('#divTB')[0];
                this.FGoodSpan = $('#FirstGood')[0];
                this.SGoodSpan = $('#SecondGood')[0];
                this.TGoodSpan = $('#ThirdGood')[0];
                this.FBadSpan = $('#FirstBad')[0];
                this.SBadSpan = $('#SecondBad')[0];
                this.TBadSpan = $('#ThirdBad')[0];
                this.FirstPhase = $('#divWait')[0];
                this.SecondPhase = $('#divTell')[0];
                this.ThirdPhase = $('#divTest')[0];
                this.FourthPhase = $('#divResult')[0];
                this.TestContent = $('#divTestContent')[0];
                this.CountDownPro = $('#proCountDown')[0];
            }
        };

        var claCountDown = function () {
            this.intMinValue = 0;
            this.intMaxValue = 10;
            this.intNowValue = 0;
            this.intStep = 1;
            this.timerGo;
        };
        claCountDown.prototype = {
            funGetTheProID: function (proCD) {
                this.proProgess = proCD;
            },
            funGetCallBack: function (funCB) {
                this.funCallBack = funCB;
            },
            funClean: function () {
                this.intNowValue = 0;
                this.funSetPo();
                clearTimeout(this.timerGo);
            },
            funSetPo: function () {
                this.proProgess.style.width = (this.intNowValue / this.intMaxValue) * 100 + '%';
            },
            funGo: function (objThat) {


                if ((objThat.intNowValue + objThat.intStep) <= objThat.intMaxValue) {
                    objThat.intNowValue += objThat.intStep;
                    objThat.funSetPo();
                    this.timerGo=setTimeout('objCountDown.funGo(objCountDown)', 1000);

                } else {
                    this.funCallBack();
                };
                objThat.funSetPo();
            }

        };

        var claTest = function () {
            this.intNow = -1;
            this.intMax = 0;
            this.strNowA = '';
            this.strNowT = '';
            this.divTest;
            this.arrAClass = [];
            this.dictAClass = [];
            this.arrNameClass = [];
            this.arrAS = [];

        };
        claTest.prototype = {
            funSetMax: function (intSetMax) {
                this.intMax = intSetMax;
            },
            funGetDOM: function (divTC) {
                this.divTest = divTC;
            },
            funLoadAnother: function () {
                this.intNow++;
                objAllExam[this.intNow].ExamQ=objAllExam[this.intNow].ExamQ.replace(/\n/g, "<br/>");
                this.divTest.innerHTML = objAllExam[this.intNow].ExamQ;
                this.strNowA = objAllExam[this.intNow].ExamA;
                this.strNowT = objAllExam[this.intNow].ExamTypes;
                this.funClassCheck(this.strNowT);

            },
            funClassCheck: function (strBigC) {
                if (this.dictAClass[strBigC]!=undefined) {
                    this.arrAClass[this.dictAClass[strBigC]] += 1;
                } else {
                    this.arrAClass.push(1);
                    this.arrAS.push(0);
                    this.arrNameClass.push(strBigC);
                    this.dictAClass[strBigC] = this.arrAClass.length - 1;
                };
            }            
        };



        var objAns = new claAns();
        var objAllCount = new claAllCont();
        var objCountDown = new claCountDown();
        var objTest = new claTest();
        var objAllExam = {};
        $(function () {
            console.log('开始初始化后台');
            objAllCount.funBegin();
            objCountDown.funGetTheProID(objAllCount.CountDownPro);
            objCountDown.funGetCallBack(null);
            objAns.funGetButton($('.btnOption'));
            objCountDown.funGetCallBack(WhenAns);
            $.ajax({
                url: "/KnowTheFate/LTELittleTest/LTELittleTestAPI.ashx",
                type: "GET",
                contentType: false,
                processData: false,
                success: function (result) {
                    var gotJson = eval('(' + result + ')');
                    if (gotJson.GotError == 0) {
                        objAllCount.FirstPhase.style.display = 'none';
                        objAllCount.SecondPhase.style.display = 'block';
                        objAllExam = gotJson.GoThrow;
                        objTest.funSetMax(objAllExam.length);
                        objTest.funGetDOM(objAllCount.TestContent);
                    } else {
                        document.getElementById("divWait").innerHTML = '错误了，错误代码为：' + gotJson.GotError + '，错误原因为：' + gotJson.ErrorDsp;
                    };
                },
                error: function () {
                    document.getElementById("divWait").innerHTML = "文件传输错误，或文件超出大小，或各种各样的问题，唉。";
                }
            });


            //setTimeout("objAllCount.FirstPhase.style.display = 'none'; objAllCount.SecondPhase.style.display = 'block'", 1000);
        });
        function ChangeAnsValue(intValue, btnThis) {
            objAns.funClearn();
            if (btnThis.className == 'btn btn-info') {
                btnThis.className = 'btn btn-danger';
                objAns.arrAns[intValue] = 1;
            } else {
                btnThis.className = 'btn btn-info';
                objAns.arrAns[intValue] = 0;
            };
        };
        function GoThirdPhase() {
            objAllCount.SecondPhase.style.display = 'none';
            objAllCount.ThirdPhase.style.display = 'block';
            WhenAns();
        };
        function WhenAns() {
            if ((objTest.intNow > -1) && ((objTest.intMax - 1) >= objTest.intNow)) {
                if (objAns.funReturn() == objTest.strNowA) {
                    objTest.arrAS[objTest.dictAClass[objTest.strNowT]] += 0.6;
                    objTest.arrAS[objTest.dictAClass[objTest.strNowT]] += (Math.random() * 0.1 * (1 - (objCountDown.intNowValue / objCountDown.intMaxValue)));
                } else {
                    objTest.arrAS[objTest.dictAClass[objTest.strNowT]] += (Math.random() * 0.1 *  (objCountDown.intNowValue / objCountDown.intMaxValue));
                };
                objTest.arrAS[objTest.dictAClass[objTest.strNowT]] += (Math.random() * 0.1);
                objTest.arrAS[objTest.dictAClass[objTest.strNowT]] += ( 0.2);
            };
            if ((objTest.intMax-2) >= objTest.intNow) {
                objAns.funClearn();
                objCountDown.funClean();
                objTest.funLoadAnother();
                objCountDown.funGo(objCountDown);

            } else {
                objAns.funClearn();
                objCountDown.funClean();

                var arrScro = [];
                objTest.arrNameClass.forEach(function (eleStrCN) {
                    var objTmpSCR = {};
                    objTmpSCR.ClassName = eleStrCN;
                    objTmpSCR.ScrValue = objTest.arrAS[objTest.dictAClass[eleStrCN]] / objTest.arrAClass[objTest.dictAClass[eleStrCN]];
                    arrScro.push(objTmpSCR);
                });
                arrScro.sort(function (a, b) {
                    return b.ScrValue - a.ScrValue;
                });



                objAllCount.FGoodPro.style.width = arrScro[0].ScrValue*100 +'%';
                objAllCount.SGoodPro.style.width = arrScro[1].ScrValue * 100 + '%';
                objAllCount.TGoodPro.style.width = arrScro[2].ScrValue * 100 + '%';
                objAllCount.FBadPro.style.width = arrScro[arrScro.length - 1].ScrValue * 100 + '%';
                objAllCount.SBadPro.style.width = arrScro[arrScro.length - 2].ScrValue * 100 + '%';
                objAllCount.TBadPro.style.width = arrScro[arrScro.length - 3].ScrValue * 100 + '%';
                objAllCount.FGoodSpan.innerHTML = arrScro[0].ClassName;
                objAllCount.SGoodSpan.innerHTML = arrScro[1].ClassName;
                objAllCount.TGoodSpan.innerHTML = arrScro[2].ClassName;
                objAllCount.FBadSpan.innerHTML = arrScro[arrScro.length - 1].ClassName;
                objAllCount.SBadSpan.innerHTML = arrScro[arrScro.length - 2].ClassName;
                objAllCount.TBadSpan.innerHTML = arrScro[arrScro.length - 3].ClassName;


                objAllCount.ThirdPhase.style.display = 'none';
                objAllCount.FourthPhase.style.display = 'block';

                //计算最后分数以及展示结果页
            };
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                <h2 style="border-bottom-style: groove;">LTE小测试 ( LTE Little Test )</h2>
            </div>
        </div>
        <div id="divWait" class="col-sm-12 alert alert-info" style="text-align: center; padding-left: 10px">
            <br />
            <strong>请稍等<br />
                <br />
                系统正在初始化<br />
                <br />

            </strong>
            <img src="/IMG/Loading.gif" style="width: 10%" id="LoadingIMG" />
            <br />
            <br />
        </div>
        <div id="divTell" class="col-sm-12 alert alert-warning" style="text-align: center; padding-left: 10px; display: none">
            <br />
            <strong>本系统只做各知识点强弱项评测<br />
                <br />
                测出对于被测人员各知识点相对掌握的好还是不好<br />
                <br />
                并不代表整体水平<br />
                <br />
                P.S.  (里面全是单选题哟亲~~)<br />
                <br />
                P.S.. (每题只有10秒钟时间哟~~)<br />
                <br />
                P.S... (整个评估过程大概需要10分钟~~)<br />
                <br />            </strong>
            <button type="button" class="btn btn-primary" onclick="GoThirdPhase()">开始测试</button>
            <br />
            <br />
        </div>


        <div id="divTest" class="col-sm-12 alert alert-success" style="text-align: center; padding-left: 10px; display: none">
            <br />
            <div id="divTestContent" class="col-sm-12" style="text-align: left; padding-left: 10px">
                题目
            </div>
            <br />
            <br />

                        <div class="col-sm-12" style="text-align: center;">
            <br />
            <br />
            <button type="button" class="btn btn-info btnOption" value="0" onclick="ChangeAnsValue(this.value,this);">A</button>
            <button type="button" class="btn btn-info btnOption" value="1" onclick="ChangeAnsValue(this.value,this);">B</button>
            <button type="button" class="btn btn-info btnOption" value="2" onclick="ChangeAnsValue(this.value,this);">C</button>
            <button type="button" class="btn btn-info btnOption" value="3" onclick="ChangeAnsValue(this.value,this);">D</button>
            <button type="button" class="btn btn-info btnOption" value="4" onclick="ChangeAnsValue(this.value,this);">E</button>
            <button type="button" class="btn btn-info btnOption" value="5" onclick="ChangeAnsValue(this.value,this);">F</button>
            <br />
            <br />
</div>
            <br />
            <br />
            <button type="button" class="btn btn-primary" onclick ="WhenAns()">下一题</button>
            <br />
            <br />
            <div class="col-sm-2"></div>

            <div class="col-sm-8 alert alert-success" style="text-align: center;">

                <div class="progress">
                    <div id="proCountDown" class="progress-bar progress-bar-striped" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%"><span class="sr-only"></span></div>
                </div>
            </div>
            <div class="col-sm-2"></div>

            <br />
            <br />
        </div>


        <div id="divResult" class="col-sm-12 alert alert-warning" style="text-align: center; padding-left: 10px; font-size: larger; display: none">
            <br />
            <div class="col-sm-12" style="text-align: center; padding-left: 10px">
                <strong style ="font-size:x-large">结果</strong><br />
                <br />
                以下为强弱项情况：<br />
                <br />
                绿色为强项<br />
                <br />
                红色为弱项<br />
                <br />

            </div>

            <div class="col-sm-2"></div>

            <div class="col-sm-8" style="text-align: center;">
                ----------------------<br />
                <br />
                强项：<br />
                <br />
                <div class="progress">
                    <div id="divFG" class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100" style="width: 100%"><span id="FirstGood">LTE关键技术</span></div>
                </div>
                <br />
                <div class="progress">
                    <div id="divSG" class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%"><span id="SecondGood">吹水</span></div>
                </div>
                <br />
                <div class="progress">
                    <div id="divTG" class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: 40%"><span id="ThirdGood">偷懒</span></div>
                </div>
                <br />
                ----------------------<br />
                <br />
                弱项：<br />
                <br />
                <div class="progress">
                    <div id="divTB" class="progress-bar progress-bar-danger" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: 40%"><span id="ThirdBad">秘密</span></div>
                </div>
                <br />
                <div class="progress">
                    <div id="divSB" class="progress-bar progress-bar-danger" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: 40%"><span id="SecondBad">没有太多休息时间</span></div>
                </div>
                <br />
                <div class="progress">
                    <div id="divFB" class="progress-bar progress-bar-danger" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: 40%"><span id="FirstBad">太用心工作了</span></div>
                </div>
                <br />
            </div>
            <div class="col-sm-2"></div>
            <br />
            <br />
                 <div class="col-sm-12" style="text-align: center; padding-left: 10px">
                <br />
                <br />
                请根据强弱项情况<br />
                <br />
                适当调整练习方向<br />
                <br />
                以成为更好的人<br />
                <br />

            </div>
        </div>

               
    </div>
</asp:Content>

