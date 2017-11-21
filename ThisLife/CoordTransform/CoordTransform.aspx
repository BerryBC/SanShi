<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="CoordTransform.aspx.vb" Inherits="ThisLife_CoordTransform_CoordTransform" %>

<%----第44号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="<%=ResolveClientUrl("~/JS/coordtransform/index.js")%>" type="text/javascript"></script>
        <script type="text/javascript">
            var tbB;
            var tbA;
            $(function () {
                tbB = document.getElementsByClassName("tbBeford")[0];
                tbA = document.getElementsByClassName("tbAfter")[0];
                tbB.value = "113.49314,23.0869\n113.40278,22.93806\n113.31245,23.25806\n";
            });
            function funTranAll(bolBDorWG) {
                var strResult = "";
                var arrBeford = new Array();
                arrBeford = tbB.value.split("\n");
                arrBeford.forEach(function (eleBe) {
                    var arrEle = eleBe.split(",");
                    if (arrEle.length!=2) {return };
                    if (bolBDorWG) {
                        arrEle = coordtransform.bd09togcj02(arrEle[0], arrEle[1]);
                        arrEle = coordtransform.gcj02towgs84(arrEle[0], arrEle[1]);
                    } else {
                        arrEle = coordtransform.wgs84togcj02(arrEle[0], arrEle[1]);
                        arrEle = coordtransform.gcj02tobd09(arrEle[0], arrEle[1]);
                    };
                    strResult += arrEle[0] + "," + arrEle[1]+"\n";
                });
                tbA.value = strResult;
            };
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section style="padding-bottom: 20px; margin-left: -30px;">

        <div class="container">
            <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                <h2 style="border-bottom-style: groove;">百度、火星、GPS经纬度转换 ( BD-09、GCJ-02、WGS84 Coord Transform )</h2>
            </div>
            <div class="col-sm-6" style="text-align: center;">
                </br>
                            <strong>输入：</strong></br>
                     <textarea id="tbBeford" class="tbBeford" runat="server" multiline="true" style="height: 300px; width: 100%; resize: none;"></textarea>
                </br>
            </div>
            <div class="col-sm-6" style="text-align: center; height: 200px;">
                </br>
                            <strong>结果：</strong></br>
                            <textarea id="tbAfter" runat="server" class="tbAfter" multiline="true" style="height: 300px; width: 100%; resize: none;"></textarea>
                </br>
            </div>
           
                                <div class="col-sm-12 alert alert-info" id="divGoDoThat" style="text-align: center; ">
                        </br><strong>
                            此工具来自wandergis，项目链接如下：</br></br>
                            <a href="https://github.com/wandergis/coordtransform" title="链接哟~">https://github.com/wandergis/coordtransform</a></br></br>
                            点下方按钮进行转换
                        </strong></br>
                           </br>
                           <input type="button" id="btnBD2WG" runat="server"  value="从百度坐标转为GPS坐标" class="btn btn-warning" onclick="funTranAll(true);"  /></br></br>
                           <input type="button" id="btnWG2BD" runat="server"  value="从GPS坐标转为百度坐标" class="btn btn-danger" onclick="funTranAll(false);"  /></br></br>
                        </br>
                           </br>


                    </div>

        </div>
    </section>
</asp:Content>

