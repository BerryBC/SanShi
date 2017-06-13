addEventListener("load", initAll, false);
var objEveryPage = { "intMaxPage": 0, "PageResult": [[]] };
var objEveryResult = [];
var regWrongHeadAndContent = new RegExp("(strong>|b>)", "gm");
var regWrongShowURL = new RegExp("(strong>|em>)", "gm");
var regSameHead = new RegExp("(\||\s|\-|_|<em>|</em>|\n|\!)*", "gm");
var arrSearchEngine = []
var intOnePageNum = 10;
var intCurrentPage = 1;
var intExplored = 0;
var bolFirstLoad = true;
var strEnginePath = ":1089/";
var strQueryKW;
var objShowCount;
var eleCF;
var objTopest;
var objPage;
var objPreviousPage;
var objNextPage;



function initAll() {

    var objSEKeyword = document.getElementsByClassName("WantKW")[0];
    var objTopKeyword = document.getElementById("SearchKW");
    strQueryKW = GetQueryString(window.location, "kw");
    objShowCount = document.getElementsByClassName("ShowResultCount")[0];
    eleCF = document.getElementsByClassName("CannotFind")[0];
    objTopest = document.getElementById("ShowResult");
    objPage = document.getElementsByClassName("NOPage")[0];
    objPreviousPage = document.getElementById("PreviousPage");
    objNextPage = document.getElementById("NextPage");


    if (objPreviousPage) { objPreviousPage.onclick = GoPreviousPage; };
    if (objNextPage) { objNextPage.onclick = GoNextPage; };


    if (strQueryKW == null || strQueryKW.toString().length == 0) {
        if (eleCF) {
            eleCF.innerHTML = "<h2>欢迎查询~请输入关键字然后~GoGoGo嘛~么么哒~</h2>";
            eleCF.style.display = "block";
        };
    }    else {
        objSEKeyword.value = decodeURI(strQueryKW);
        objTopKeyword.value = decodeURI(strQueryKW);
        GetTheUnique()
    };


};
function PageChange(evt) {
    intCurrentPage = parseInt(this.value);
    AfterPageChange(true);
};
function GoPreviousPage(evt) {
    var objNextPage = document.getElementById("NextPage");
    intCurrentPage--;
    AfterPageChange(true);
    objNextPage.style.display = "inline";
};
function GoNextPage(evt) {
    var objPreviousPage = document.getElementById("PreviousPage");
    intCurrentPage++;
    AfterPageChange(true);
    objPreviousPage.style.display = "inline";
};
function AfterPageChange(bolIsRefresh) {
    var objPreviousPage = document.getElementById("PreviousPage");
    var objNextPage = document.getElementById("NextPage");
    var objMiddlePageDiv = document.getElementById("ContentPage");

    var nowMaxPage = objEveryPage.PageResult.length;
    var intBeginPage = 1;
    var intEndPage = nowMaxPage;

    if (bolIsRefresh) { ShowUpOnWeb(); };


    objMiddlePageDiv.innerHTML = "";
    if (intCurrentPage >= nowMaxPage) { intCurrentPage = nowMaxPage; };
    if (intCurrentPage == 1) {
        objPreviousPage.style.display = "none";
    } else {
        objPreviousPage.style.display = "inline";
    };
    if (intCurrentPage >= nowMaxPage) {
        objNextPage.style.display = "none";
    } else {
        objNextPage.style.display = "inline";
    };
    intBeginPage = intCurrentPage - 4;
    intEndPage = intCurrentPage + 3;
    if (intCurrentPage < 5) {
        intBeginPage = 1;
        if (nowMaxPage < 8) {
            intEndPage = nowMaxPage;
        } else {
            intEndPage = 8;
        };
    };
    if ((nowMaxPage - intCurrentPage) <= 3) {
        intEndPage = nowMaxPage;
        if (nowMaxPage >= 8) {
            intBeginPage = nowMaxPage - 7;
        } else {
            intBeginPage = 1;
        };
    };
    for (var intTmpI = 1; intTmpI <= (intEndPage - intBeginPage + 1); intTmpI++) {
        var tmpPage = document.createElement("input");
        tmpPage.type = "button";
        tmpPage.className = "btn"
        tmpPage.value = (intTmpI + intBeginPage - 1);
        tmpPage.id = "Page" + intTmpI;
        tmpPage.onclick = PageChange;
        if ((intTmpI + intBeginPage - 1) > intExplored) { tmpPage.disabled = true; };
        objMiddlePageDiv.appendChild(tmpPage);
    };
    document.getElementById("Page" + (intCurrentPage - intBeginPage + 1)).className += " Chosen";
    if (intExplored < intCurrentPage) {
        intExplored = intCurrentPage;
        PostKWToServer(strQueryKW, intExplored - 1);
    };
    // window.location.href = "#header-banner";
    if (bolIsRefresh) { $('html,body').animate({ scrollTop: '0px' }, "slow"); };


};
function GetQueryString(strFullURL, strQueryKey) {
    var reg = new RegExp("(^|&|/?)" + strQueryKey + "=([^&]*)(&|$)");
    if (typeof (strFullURL) == "string") {
        var r = strFullURL.substr(1).match(reg);
    } else {
        var r = strFullURL.search.substr(1).match(reg);
    };
    if (r != null) return r[2]; return null;
};
function ShowUpOnWeb() {
    var objTopest = document.getElementById("ShowResult");
    objTopest.innerHTML = "";

    var intNowID = 0
    objEveryPage.PageResult[intCurrentPage - 1].forEach(function (element) {

        var tmpResultTopDiv = document.createElement("div");
        tmpResultTopDiv.className = "ShowEveryResultInfo";
        tmpResultTopDiv.id = "SERI" + intNowID;
        var tmpResultHead = document.createElement("h3");
        tmpResultHead.className = "ResultHead";
        var tmpResultHeadA = document.createElement("a");
        tmpResultHeadA.href = element.GoURL;
        tmpResultHeadA.innerHTML = element.HeadHTML;
        tmpResultHead.appendChild(tmpResultHeadA);
        tmpResultTopDiv.appendChild(tmpResultHead);
        var tmpResultCententDiv = document.createElement("div");
        tmpResultCententDiv.className = "ResultContent";
        tmpResultCententDiv.innerHTML = element.Content;
        tmpResultTopDiv.appendChild(tmpResultCententDiv);
        var tmpResultShowURLDiv = document.createElement("div");
        tmpResultShowURLDiv.className = "ResultLink";
        tmpResultShowURLDiv.innerHTML = element.ShowURL;
        var tmpResultQuoteFromWhere = document.createElement("span");
        tmpResultQuoteFromWhere.className = "QuoteFrom";
        tmpResultQuoteFromWhere.innerHTML = "&nbsp&nbsp&nbsp 结果来自 " + arrSearchEngine[element.FromEngine].SEName;
        tmpResultShowURLDiv.appendChild(tmpResultQuoteFromWhere);
        tmpResultTopDiv.appendChild(tmpResultShowURLDiv);
        objTopest.appendChild(tmpResultTopDiv);
        intNowID++;
    }, this);
};
function GetDataFromAjax(jsonData) {

    if (!jsonData.IsError) {
        if (jsonData.MaxResult > objEveryPage.intMaxPage) {
            objEveryPage.intMaxPage = jsonData.MaxResult;
            objShowCount.innerHTML = "哎哟~为你找到 " + objEveryPage.intMaxPage + " 条记录了！";
        };
        if (objEveryPage.intMaxPage == 0) {
            eleCF.innerHTML = "<h2>找不到搜索结果哟~请更换关键字吧~么么哒~</h2>";
            eleCF.style.display = "block";
            objTopest.style.display = "none";
            location.href = "#CannotFind";

        } else {
            eleCF.style.display = "none";

            jsonData.EveryResult.forEach(function (eleReturnResult) {
                HandleTheTag(eleReturnResult);
                if (CheckRepeat(eleReturnResult)) {
                    objEveryResult.push(eleReturnResult);
                    PushToPage(eleReturnResult);

                };
            }, this);

            if (bolFirstLoad) {
                objShowCount.style.display = "block";
                objPage.style.display = "block";
                objNextPage.style.display = "inline";
                objTopest.style.display = "block";

                bolFirstLoad = false;
                AfterPageChange(true);
            } else {
                AfterPageChange(false);
            };

        };
    } else if (objEveryPage.intMaxPage == 0) {
        eleCF.innerHTML = "<h2>出错了~么么哒~问题是 :</h2><br /> " + jsonData.ErrorCode;
        eleCF.style.display = "block";
        location.href = "#CannotFind";


    };
    function HandleTheTag(eleCRResult) {
        eleCRResult.HeadHTML = eleCRResult.HeadHTML.replace(regWrongHeadAndContent, "em>");
        eleCRResult.Content = eleCRResult.Content.replace(regWrongHeadAndContent, "em>");
        eleCRResult.ShowURL = eleCRResult.ShowURL.replace(regWrongShowURL, "b>");
        eleCRResult.GoURL = GetTheRealURL(eleCRResult.GoURL, arrSearchEngine[eleCRResult.FromEngine].RealURLPara);
    };
    function CheckRepeat(eleCRResult) {
        var bolIsPass = true;
        objEveryResult.forEach(function (eleAllOfBeforeResult) {
            if (eleAllOfBeforeResult.GoURL == eleCRResult.GoURL || eleAllOfBeforeResult.HeadHTML.replace(regSameHead, "") == eleCRResult.HeadHTML.replace(regSameHead, "")) {
                if (eleAllOfBeforeResult.FromEngine < eleCRResult.FromEngine) {
                    eleAllOfBeforeResult.HeadHTML = eleCRResult.HeadHTML;
                    eleAllOfBeforeResult.GoURL = eleCRResult.GoURL;
                    eleAllOfBeforeResult.ShowURL = eleCRResult.ShowURL;
                    eleAllOfBeforeResult.FromEngine = eleCRResult.FromEngine;
                };
                bolIsPass = false;

            };
        }, this);
        if (bolIsPass) {
            return true;
        } else {
            return false;
        };
    };
    function PushToPage(eleCRResult) {
        var nowMaxPage = objEveryPage.PageResult.length;
        if (objEveryPage.PageResult[nowMaxPage - 1].length >= intOnePageNum || nowMaxPage <= intExplored) {
            objEveryPage.PageResult.push([]);
            nowMaxPage = objEveryPage.PageResult.length;
        };
        objEveryPage.PageResult[nowMaxPage - 1].push(eleCRResult);
    };
};
function PostKWToServer(intGoKW, intGoPage) {
    arrSearchEngine.forEach(function (eleEngineConfig) {
        $.ajax({
            type: "GET",
            url: "http://" + window.location.host + strEnginePath + "search?kw=" + strQueryKW + "&pg=" + intGoPage + "&se=" + eleEngineConfig.SEName,
            dataType: "json",
            success: GetDataFromAjax,
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //处理错误的地方
                if (objEveryResult.length <= 0) {
                    location.href = "#CannotFind";
                    eleCF.innerHTML = "<h2>本次操作出错了~么么哒~问题是 :<br /><br />服务器连接出错！</h2><br /> ";
                    eleCF.style.display = "block";
                };
            }
        });

    }, this);
};
function GetTheUnique() {
    $.ajax({
        type: "GET",
        url: "http://" + window.location.host + strEnginePath + "loadconfig",
        dataType: "json",
        success: ReceiveConfig,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //处理错误的地方
            location.href = "#CannotFind";
            eleCF.innerHTML = "<h2>初始化错误欸~么么哒~</h2><br /> ";
            eleCF.style.display = "block";
        }
    });

    function ReceiveConfig(jsonData) {
        for (var intForEngineConfig = 0; intForEngineConfig < jsonData.arrSearchEngine.length; intForEngineConfig++) {
            arrSearchEngine.push(jsonData.arrSearchEngine[intForEngineConfig]);
        };
        PostKWToServer(strQueryKW, 0);

    };
};



function GetTheRealURL(strGetPureUrl, strPara) {
    if (typeof (strPara) == "null" || typeof (strPara) == "undefined") {
        return strGetPureUrl;
    } else {
        var strReturnURL = "";
        strReturnURL = GetQueryString(decodeURI(strGetPureUrl), "q");
        if (strReturnURL != null) { return strReturnURL };
        return strGetPureUrl;
    };
};

function GetBaiduSuggestion(strKeyword, e) {
    $.ajax({
        type: "GET",
        url: "http://" + window.location.host + strEnginePath+"loadbdsug?kw=" + strKeyword,
        dataType: "json",
        success: ShowBaiduSuggestion,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //处理错误的地方
            console.log("获取百度建议出错")
        }
    });
    function ShowBaiduSuggestion(jsonData) {

        $("#" + e.srcElement.id).autocomplete({
            source: function (request, response) {
                var matcher = new RegExp("[\S|\W]*", "gm");
                response($.grep(jsonData.s, function (item) {
                    return matcher.test(item);
                }));
            },


            mustMatch: false,
            matchContains: false,
            matchContains: true,
            matchSubset: true,
            matchCase: false,
            minChars: 0,
            select: function (event, ui) {

                window.location = "/AfterLife/SearchEngine/SanShiSearch.aspx" + "?kw=" + encodeURI(ui.item.value);

                return false;
            }
        });



    };
};

