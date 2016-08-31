addEventListener("load", initAll, false);
function initAll() {
    if (window.addEventListener) {
        window.addEventListener('resize', TopSearchTextBoxHide, false);
        window.addEventListener('scroll', TopScrollDown, false);
    } else if (window.attachEvent) {
        window.attachEvent('onresize', TopSearchTextBoxHide, false);
        window.attachEvent('onscroll', TopScrollDown, false);
    };

};
function TopSearchTextBoxHide() {
    if (getComputedStyle(document.getElementsByClassName("navbar-toggle collapsed")[0], null)["display"] == "none") {
        document.getElementById("SearchKW").style.display = "block";
    } else {
        document.getElementById("SearchKW").style.display = "none";
    };
};
function TopScrollDown() {
    var intHowHight = document.documentElement.scrollTop || document.body.scrollTop;
    var doubleOpac=0;
    if((1 - intHowHight / 2000)>0.5){
        doubleOpac=1 - intHowHight / 2000;
    }else{
        doubleOpac=0.5;
    };
    $(".navbar").css("background-color","rgba(130,20,112,"+doubleOpac+")");

}

function IsPressEnter(evt) {
    if (evt) {
        var thisKeyCode = evt.keyCode;
        var thisElement = evt.target;
    }
    else {
        var thisKeyCode = window.event.keyCode;
        var thisElement = window.event.srcElement;
    };
    if (thisKeyCode == 13 || evt.type == "click") {
        var objSEKeyword = document.getElementsByClassName("WantKW");
        var strSEKeyword = thisElement.value;
        if (evt.type == "click") {
            strSEKeyword = objSEKeyword[0].value;
        };

        window.location = "/AfterLife/SearchEngine/SanShiSearch.aspx" + "?kw=" + encodeURI(strSEKeyword);

        if (evt) {
            evt.preventDefault();
        }
        else {
            window.event.returnValue = false;
        };

    };
};

