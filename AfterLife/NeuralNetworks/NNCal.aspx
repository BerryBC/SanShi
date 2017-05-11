<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="NNCal.aspx.vb" Inherits="AfterLife_NeuralNetworks_NNCal" %>
<%----第35号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(function () {
            //setTimeout("alert('Shit')", 3000);
        });
        function onClickGo() {
            //alert('Shit');
        };

        function setOutputContainer(text) {
            document.getElementById("ShowResult").innerHTML += ('<br/>' + text);
        };


        function GetTheData() {
            var strEnginePath;

            strEnginePath = "/AfterLife/NeuralNetworks/NNBack.ashx"

            $.ajax({
                type: 'get',
                url: "http://" + window.location.host + strEnginePath ,
                async: true,
                //data: shit,
                success: function (result) {
                    var gotJson = eval('(' + result + ')');
                    if (gotJson.GotError == 0) {
                        setOutputContainer(gotJson.ID);
                        setOutputContainer(gotJson.Name);
                        setOutputContainer('------------------');
                    } else {
                        setOutputContainer(result);
                        setOutputContainer('错误了，错误代码为：' + gotJson.GotError + '错误原因为：' + gotJson.ErrorDsp);
                        setOutputContainer('------------------');

                    };




                    //var stu = eval('(' + result + ')');
                    //setContainer(stu.ID);
                    //setContainer(stu.Name);
                    //setContainer(result);
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
                    <input type="button" id="btnGetStart" class="form-control btn-info" style="color: white; font-weight: bold;" onmouseover="this.style.backgroundColor='#BEC9F6';this.style.color='buttontext';this.style.cursor='default';" onmouseout="this.style.backgroundColor='';this.style.color='';this.style.color='White'" onclick="GetTheData()" value="开始" />
                </div>
            </div>
            <div id="ShowResult" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            </div>

        </div>
    </section>

</asp:Content>

