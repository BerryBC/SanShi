<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="SelectCellByMap.aspx.vb" Inherits="ThisLife_MapGood_SelectCellByMap" %>

<%----第46号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=C4msdAZymuN9mjstHB5H6nYT"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/library/DrawingManager/1.4/src/DrawingManager_min.js"></script>
    <link rel="stylesheet" href="http://api.map.baidu.com/library/DrawingManager/1.4/src/DrawingManager_min.css" />
    <script src="<%=ResolveClientUrl("~/JS/coordtransform/index.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        var bdMap;
        var drawingManager;
        var lastOverLays;
        var strEnginePath;
        var intAddDot;
        var arrPolP = [];

        $(function () {
            strEnginePath = "/ThisLife/MapGood/LoadCell.ashx"
            bdMap = new BMap.Map("CellMap", { enableMapClick: false });
            var nowPoint = new BMap.Point(113.30764968, 23.1200491);
            bdMap.centerAndZoom(nowPoint, 13);

            var locNow = new BMap.Geolocation();
            locNow.getCurrentPosition(function (r) {
                if (this.getStatus() == BMAP_STATUS_SUCCESS) {

                    bdMap.panTo(r.point);

                }
                else {
                    bdMap.panTo(nowPoint);
                }
            }, { enableHighAccuracy: true });
            bdMap.setCurrentCity("广州");
            bdMap.enableScrollWheelZoom(true);


            var styleOptions = {
                strokeColor: "red",    //边线颜色。
                fillColor: "purple",      //填充颜色。当参数为空时，圆形将没有填充效果。
                strokeWeight: 3,       //边线的宽度，以像素为单位。
                strokeOpacity: 0.8,	   //边线透明度，取值范围0 - 1。
                fillOpacity: 0.6,      //填充的透明度，取值范围0 - 1。
                strokeStyle: 'dashed' //边线的样式，solid或dashed。
            }
            drawingManager = new BMapLib.DrawingManager(bdMap, {
                isOpen: false, //是否开启绘制模式
                enableDrawingTool: false, //是否显示工具栏
                drawingToolOptions: {
                    anchor: BMAP_ANCHOR_TOP_RIGHT, //位置
                    offset: new BMap.Size(5, 5), //偏离值
                },
                circleOptions: styleOptions, //圆的样式
                polylineOptions: styleOptions, //线的样式
                polygonOptions: styleOptions, //多边形的样式
                rectangleOptions: styleOptions //矩形的样式
            });
            drawingManager.setDrawingMode(BMAP_DRAWING_POLYGON);
            drawingManager.addEventListener('overlaycomplete', drawComplete);
        });
        function funDraw() {
            var vbtnGo = document.getElementById("btnGo");
            if (vbtnGo.value == '开始画图') {
                vbtnGo.value = '双击停止画图';
                vbtnGo.className = 'btn btn-danger';
                drawingManager.open();
            } else {
                //vbtnGo.value = '开始画图';
                //vbtnGo.className = 'btn btn-success';
                //drawingManager.close();
            };
        };

        function funSatpic() {
            var vbtnSatpic = document.getElementById("btnSatpic");
            if (vbtnSatpic.value == '展示卫星图') {
                vbtnSatpic.value = '展示平面图';
                vbtnSatpic.className = 'btn btn-warning';
                bdMap.setMapType(BMAP_HYBRID_MAP);

            } else {
                vbtnSatpic.value = '展示卫星图';
                vbtnSatpic.className = 'btn btn-primary';
                bdMap.setMapType(BMAP_NORMAL_MAP);
            };
        };



        function drawComplete(e) {
            var vbtnGo = document.getElementById("btnGo");
            var vtbCoord = document.getElementById("taCroord");
            var vdivTips = document.getElementById("divAlert");
            var doubleMaxLng;
            var doubleMaxLat;
            var doubleMinLng;
            var doubleMaxLat;
            var arrPath;

            vbtnGo.value = '开始画图';
            vbtnGo.className = 'btn btn-success';
            drawingManager.close();
            if (lastOverLays) {
                bdMap.clearOverlays();
                bdMap.addOverlay(e.overlay);
            };

            lastOverLays = e.overlay;
            //lastOverLays.push(e.overlay);
            vtbCoord.value = "";


            arrPath = lastOverLays.getPath();

            var arrEle = tranCoordFB2W(arrPath[0].lng, arrPath[0].lat);

            doubleMaxLng = arrEle[0];
            doubleMinLng = arrEle[0];
            doubleMaxLat = arrEle[1];
            doubleMinLat = arrEle[1];

            arrPolP = [];

            arrPath.forEach(elePoint => {
                arrEle = tranCoordFB2W(elePoint.lng, elePoint.lat);
                vtbCoord.value += arrEle[0] + ',' + arrEle[1] + '\n';
                arrPolP.push([arrEle[0], arrEle[1]]);
                if (arrEle[0] > doubleMaxLng) {
                    doubleMaxLng = arrEle[0];
                };
                if (arrEle[0] < doubleMinLng) {
                    doubleMinLng = arrEle[0];
                };
                if (arrEle[1] > doubleMaxLat) {
                    doubleMaxLat = arrEle[1];
                };
                if (arrEle[1] < doubleMinLat) {
                    doubleMinLat = arrEle[1];
                };
            });

            vdivTips.innerHTML = '<strong>系统正在导出小区信息..</strong> ';
            vdivTips.className = 'alert alert-danger';
            intAddDot = setInterval(addDot, 1000);

            $.ajax({
                type: 'GET',
                url: "http://" + window.location.host + strEnginePath,
                async: true,
                data: { 'doubleMaxLng': doubleMaxLng, 'doubleMinLng': doubleMinLng, 'doubleMaxLat': doubleMaxLat, 'doubleMinLat': doubleMinLat, },
                success: function (result) {
                    var arrAllCoord = [];
                    var intCoordCount = 0;
                    var dictAllCoord = [];

                    var gotJson = eval('(' + result + ')');

                    var vbtnCheck;
                    vbtnCheck = document.getElementById("btnCheckCell");
                    vbtnCheck.disabled = "";

                    clearInterval(intAddDot);
                    if (gotJson.GotError == 0) {
                        var intCountCell;
                        console.log(gotJson);
                        vdivTips.innerHTML = '<strong>已导出信息，页面下方有小区列表。或者按左方按钮重新画范围。</strong> ';
                        vdivTips.className = 'alert alert-success';
                        var myIconG = new BMap.Icon("/IMG/red-circle.png", new BMap.Size(23, 25), { offset: new BMap.Size(10, 25), imageOffset: new BMap.Size(0, 0) });
                        var myIconT = new BMap.Icon("/IMG/grn-circle.png", new BMap.Size(23, 25), { offset: new BMap.Size(10, 25), imageOffset: new BMap.Size(0, 0) });
                        var myIconL = new BMap.Icon("/IMG/purple-circle.png", new BMap.Size(23, 25), { offset: new BMap.Size(10, 25), imageOffset: new BMap.Size(0, 0) });
                        intCountCell = gotJson.GSMCell.length + gotJson.TDSCell.length + gotJson.LTECell.length;
                        if (intCountCell < 1000) {

                            funFillCellTA([], "G", false);
                            funFillCellTA([], "T", false);
                            funFillCellTA([], "L", false);


                            gotJson.GSMCell.forEach(elePoint => {
                                if (dictAllCoord[elePoint[2] + "-" + elePoint[3]]) {
                                    arrAllCoord[dictAllCoord[elePoint[2] + "-" + elePoint[3]]][2] += elePoint[1] + "-" + elePoint[0] + "<br/>";
                                } else {
                                    arrAllCoord.push([elePoint[2], elePoint[3], elePoint[1] + "-" + elePoint[0] + "<br/>", myIconG]);
                                    dictAllCoord[elePoint[2] + "-" + elePoint[3]] = intCoordCount;
                                    intCoordCount++;
                                };
                                if (wn_PnPoly([elePoint[2], elePoint[3]], arrPolP) != 0) {
                                    funFillCellTA(elePoint, "G", true);
                                };
                            });

                            gotJson.TDSCell.forEach(elePoint => {
                                if (dictAllCoord[elePoint[3] + "-" + elePoint[4]]) {
                                    arrAllCoord[dictAllCoord[elePoint[3] + "-" + elePoint[4]]][2] += elePoint[0] + "<br/>";
                                } else {
                                    arrAllCoord.push([elePoint[3], elePoint[4], elePoint[0] + "<br/>", myIconT]);
                                    dictAllCoord[elePoint[3] + "-" + elePoint[4]] = intCoordCount;
                                    intCoordCount++;
                                };
                                if (wn_PnPoly([elePoint[3], elePoint[4]], arrPolP) != 0) {
                                    funFillCellTA(elePoint, "T", true);
                                };
                            });


                            gotJson.LTECell.forEach(elePoint => {
                                if (dictAllCoord[elePoint[4] + "-" + elePoint[5]]) {
                                    arrAllCoord[dictAllCoord[elePoint[4] + "-" + elePoint[5]]][2] += elePoint[0] + "<br/>";
                                } else {
                                    arrAllCoord.push([elePoint[4], elePoint[5], elePoint[0] + "<br/>", myIconL]);
                                    dictAllCoord[elePoint[4] + "-" + elePoint[5]] = intCoordCount;
                                    intCoordCount++;
                                };
                                if (wn_PnPoly([elePoint[4], elePoint[5]], arrPolP) != 0) {
                                    funFillCellTA(elePoint, "L", true);
                                };
                            });

                            var optsOfMaker = {
                                //width: 250,     // 信息窗口宽度
                                //height: 80,     // 信息窗口高度
                                title: "小区信息", // 信息窗口标题
                                enableMessage: false,//设置允许信息窗发送短息
                                offset: { height: -20, width: 0 }
                            };

                            arrAllCoord.forEach(eleCoord => {
                                var arrEle = tranCoordFW2B(eleCoord[0], eleCoord[1]);
                                var markerBeforPoint = new BMap.Marker(new BMap.Point(arrEle[0], arrEle[1]), { icon: eleCoord[3] });

                                bdMap.addOverlay(markerBeforPoint);

                                markerBeforPoint.addEventListener("mouseover", function (e) {
                                    var p = e.target;
                                    var point = new BMap.Point(p.getPosition().lng, p.getPosition().lat);
                                    var infoWindow = new BMap.InfoWindow(eleCoord[2], optsOfMaker);
                                    bdMap.openInfoWindow(infoWindow, point);

                                });
                                markerBeforPoint.addEventListener("mouseout", function (e) {
                                    bdMap.closeInfoWindow();
                                });

                            });
                        } else {
                            if (document.createElement('canvas').getContext) {  // 判断当前浏览器是否支持绘制海量点
                                var arrEle = [];
                                var ptCell = [];  // 添加海量点数据
                                funFillCellTA([], "G", false);
                                funFillCellTA([], "T", false);
                                funFillCellTA([], "L", false);

                                for (var intI = 0; intI < gotJson.GSMCell.length; intI++) {
                                    arrEle = tranCoordFW2B(gotJson.GSMCell[intI][2], gotJson.GSMCell[intI][3]);
                                    ptCell.push(new BMap.Point(arrEle[0], arrEle[1]));
                                    if (wn_PnPoly([gotJson.GSMCell[intI][2], gotJson.GSMCell[intI][3]], arrPolP) != 0) {
                                        funFillCellTA(gotJson.GSMCell[intI], "G", true);
                                    };
                                }
                                var optionsPC = {
                                    size: BMAP_POINT_SIZE_SMALL,
                                    shape: BMAP_POINT_SHAPE_CIRCLE,
                                    color: '#FC6255'
                                }
                                var pointCollection = new BMap.PointCollection(ptCell, optionsPC);  // 初始化PointCollection

                                bdMap.addOverlay(pointCollection);  // 添加Overlay


                                ptCell = [];  // 添加海量点数据
                                for (var intI = 0; intI < gotJson.TDSCell.length; intI++) {
                                    arrEle = tranCoordFW2B(gotJson.TDSCell[intI][3], gotJson.TDSCell[intI][4]);
                                    ptCell.push(new BMap.Point(arrEle[0], arrEle[1]));
                                    if (wn_PnPoly([gotJson.TDSCell[intI][3], gotJson.TDSCell[intI][4]], arrPolP) != 0) {
                                        funFillCellTA(gotJson.TDSCell[intI], "T", true);
                                    };
                                }
                                optionsPC = {
                                    size: BMAP_POINT_SIZE_SMALL,
                                    shape: BMAP_POINT_SHAPE_CIRCLE,
                                    color: '#56FF7B'
                                }
                                pointCollection = new BMap.PointCollection(ptCell, optionsPC);  // 初始化PointCollection

                                bdMap.addOverlay(pointCollection);  // 添加Overlay



                                ptCell = [];  // 添加海量点数据
                                for (var intI = 0; intI < gotJson.LTECell.length; intI++) {
                                    arrEle = tranCoordFW2B(gotJson.LTECell[intI][4], gotJson.LTECell[intI][5]);
                                    ptCell.push(new BMap.Point(arrEle[0], arrEle[1]));
                                    if (wn_PnPoly([gotJson.LTECell[intI][4], gotJson.LTECell[intI][5]], arrPolP) != 0) {
                                        funFillCellTA(gotJson.LTECell[intI], "L", true);
                                    };
                                }
                                optionsPC = {
                                    size: BMAP_POINT_SIZE_SMALL,
                                    shape: BMAP_POINT_SHAPE_CIRCLE,
                                    color: '#8154FF'
                                }
                                pointCollection = new BMap.PointCollection(ptCell, optionsPC);  // 初始化PointCollection

                                bdMap.addOverlay(pointCollection);  // 添加Overlay

                            } else {
                                alert('请在chrome、safari、IE8+以上浏览器查看本示例');
                            }

                        };


                    } else {
                        vdivTips.innerHTML = '<strong>系统导出信息有错误</strong> ';
                        vdivTips.className = 'alert alert-info';

                    };
                },
                error: function () {
                    vdivTips.innerHTML = '<strong>链接网络出错</strong> ';
                    vdivTips.className = 'alert alert-info';
                }
            });
        };

        function addDot() {
            var vdivTips = document.getElementById("divAlert");
            if (vdivTips.innerText.length >= 20) {
                vdivTips.innerText = '系统正在导出小区信息..';
            } else {
                vdivTips.innerText += '.';
            };
        };
        function tranCoordFB2W(doubleInLng, doubleInLat) {
            var arrEle = [];
            arrEle = coordtransform.bd09togcj02(doubleInLng, doubleInLat);
            arrEle = coordtransform.gcj02towgs84(arrEle[0], arrEle[1]);
            return arrEle;
        };
        function tranCoordFW2B(doubleInLng, doubleInLat) {
            var arrEle = [];
            arrEle = coordtransform.wgs84togcj02(doubleInLng, doubleInLat);
            arrEle = coordtransform.gcj02tobd09(arrEle[0], arrEle[1]);
            return arrEle;
        };

        function funIsLeft(pP0, pP1, pP2) {
            return ((pP1[0] - pP0[0]) * (pP2[1] - pP0[1]) - (pP2[0] - pP0[0]) * (pP1[1] - pP0[1]));
        };

        function wn_PnPoly(pP, arrpV) {
            var intWN = 0;    // the  winding number counter
            var intN = arrpV.length - 1;
            var intI;
            arrpV.push(arrpV[0]);
            // loop through all edges of the polygon
            for (intI = 0; intI < intN; intI++) {   // edge from V[i] to  V[i+1]
                if (arrpV[intI][1] <= pP[1]) {          // start y <= P.y
                    if (arrpV[intI + 1][1] > pP[1]) {    // an upward crossing
                        if (funIsLeft(arrpV[intI], arrpV[intI + 1], pP) > 0)  // P left of  edge
                            intWN++;            // have  a valid up intersect
                    }
                } else {                        // start y > P.y (no test needed)
                    if (arrpV[intI + 1][1] <= pP[1]) {   // a downward crossing
                        if (funIsLeft(arrpV[intI], arrpV[intI + 1], pP) < 0) {  // P right of  edge
                            intWN--;            // have  a valid down intersect}
                        };
                    };
                };
            };
            return intWN;
        };

        function funFillCellTA(arrInEle, strGTL, bolFillOrClean) {
            var vtaCell;
            if (strGTL == "G") {
                vtaCell = document.getElementById("taGSMCell");
            } else if (strGTL == "T") {
                vtaCell = document.getElementById("taTDSCell");
            } else if (strGTL == "L") {
                vtaCell = document.getElementById("taLTECell");
            };
            if (!bolFillOrClean) {
                vtaCell.value = "";
                if (strGTL == "G") {
                    vtaCell.value = "小区英文名,基站名,经度,纬度,\n";
                } else if (strGTL == "T") {
                    vtaCell.value = "小区中文名,CellID,RNCID,经度,纬度,\n";
                } else if (strGTL == "L") {
                    vtaCell.value = "小区中文名,CellID,SectorID,eNodeBID,经度,纬度,\n";
                };
            } else {
                arrInEle.forEach(elePoint => {
                    vtaCell.value += elePoint + ",";
                });
                vtaCell.value += "\n";
                ;
            };
        };

        function funFindPlace() {
            var vtextAddress = document.getElementById("textAddress");
            var myGeo = new BMap.Geocoder();
            // 将地址解析结果显示在地图上,并调整地图视野
            myGeo.getPoint(vtextAddress.value, function (pP) {
                if (pP) {
                    bdMap.centerAndZoom(pP, 16);

                    var polP = new BMap.Polygon([
                        new BMap.Point(pP.lng + 0.001, pP.lat + 0.001),
                        new BMap.Point(pP.lng - 0.001, pP.lat + 0.001),
                        new BMap.Point(pP.lng - 0.001, pP.lat - 0.001),
                        new BMap.Point(pP.lng + 0.001, pP.lat - 0.001)
                    ], {
                        strokeColor: "red",    //边线颜色。
                        fillColor: "purple",      //填充颜色。当参数为空时，圆形将没有填充效果。
                        strokeWeight: 3,       //边线的宽度，以像素为单位。
                        strokeOpacity: 0.8,	   //边线透明度，取值范围0 - 1。
                        fillOpacity: 0.6,      //填充的透明度，取值范围0 - 1。
                        strokeStyle: 'dashed' //边线的样式，solid或dashed。

                    });
                    bdMap.addOverlay(polP);
                    drawComplete({ overlay: polP });
                } else {
                    alert("您选择地址没有解析到结果!");
                }
            }, "广州市");

        };



        function IsSearchEnter(evt) {
            if (evt) {
                var thisKeyCode = evt.keyCode;
                var thisElement = evt.target;
                var strSEKeyword = thisElement.value;
            }
            else {
                var thisKeyCode = window.event.keyCode;
                var thisElement = window.event.srcElement;
                var strSEKeyword = thisElement.value;

            };
            if (evt) {
                evt.preventDefault();
            }
            else {
                window.event.returnValue = false;
            };
            if (thisKeyCode == 13 || evt.type == "click") {
                funFindPlace();

            };
        };



    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section style="padding-bottom: 20px;">
        <div class="container">
            <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                <h2 style="border-bottom-style: groove;">从地图上选择小区 ( Select Cell By Map )</h2>
            </div>
            <div class="col-sm-6" style="margin-top: -6px;">
                <div class="col-sm-4" style="overflow: auto;" id="divControl">
                    <input type="button" id="btnGo" class="btn btn-success" value="开始画图" onclick="funDraw()" />
                    <br />
                </div>
                <div class="col-sm-4" style="overflow: auto;">
                    <input type="button" id="btnSatpic" class="btn btn-primary" value="展示卫星图" onclick="funSatpic()" />
                    <br />
                </div>
                <div class="col-sm-4" style="overflow: auto;" id="divCheckCell">
                    <input type="button" id="btnCheckCell" disabled="disabled" class="btn btn-info" value="查看小区" onclick="var target = $('#divShow').offset().top - 38; $('html,body').animate({ scrollTop: target }, 'slow');" />
                    <br />

                </div>

                <div class="col-sm-12" style="margin-top: 5px;">
                    可以在这输入地址直接到达：       
                    <input type="text" id="textAddress"  onkeyup="IsSearchEnter(event)" />
                    <input type="button" value="查找地址" class="btn btn-info" id="btnSearchAdd" onclick="funFindPlace();" />

                </div>
            </div>

            <div class="col-sm-6" style="">
                <div class="alert alert-success" style="" id="divAlert">
                    <strong><---请先点击“开始画图”然后大概框出目标形状</strong>
                </div>
            </div>


            <div class="col-sm-12" style="overflow: auto; height: 500px; width: 100%;" id="CellMap"></div>

            <div class="col-sm-12" style="text-align: center;" id="divShow">
                <br />
                <strong>多边形坐标(WGS84坐标系)：</strong><br />
                <textarea id="taCroord" class="tbBeford" style="height: 300px; width: 100%; resize: none;"></textarea>
                <br />
            </div>
            <div class="col-sm-12" style="text-align: center;">
                <br />
                <strong>多边形内GSM小区：</strong><br />
                <textarea id="taGSMCell" class="tbBeford" style="height: 300px; width: 100%; resize: none;"></textarea>
                <br />
            </div>
            <div class="col-sm-12" style="text-align: center;">
                <br />
                <strong>多边形内TDS小区：</strong><br />
                <textarea id="taTDSCell" class="tbBeford" style="height: 300px; width: 100%; resize: none;"></textarea>
                <br />
            </div>
            <div class="col-sm-12" style="text-align: center;">
                <br />
                <strong>多边形内LTE小区：</strong><br />
                <textarea id="taLTECell" class="tbBeford" style="height: 300px; width: 100%; resize: none;"></textarea>
                <br />
            </div>
        </div>
    </section>
</asp:Content>

