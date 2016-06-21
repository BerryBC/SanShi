<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportErrorLog.aspx.vb" Inherits="ReportErrorLog" %>
<%----第5号页面--%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Bootstrap Core CSS -->

    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/animate.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script src="JS/jquery.min.js"></script>
    <script src="JS/bootstrap.min.js"></script>





    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="renderer" content="webkit"/>
    <title>Error</title>
</head>
<body>


    <form id="form2" runat="server">
            <script type="text/javascript">
//<![CDATA[
var theForm = document.forms['form1'];
if (!theForm) {
    theForm = document.form1;
}
function __doPostBack(eventTarget, eventArgument) {
    if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
        theForm.__EVENTTARGET.value = eventTarget;
        theForm.__EVENTARGUMENT.value = eventArgument;
        theForm.submit();
    }
}
//]]>
</script>
        <header id="header-banner">
            <nav class="navbar navbar-default navbar-fixed-top fadeIn" role="navigation">
                <div class="container">

                    <!-- Brand and toggle get grouped for better mobile display -->
                    <div class="navbar-header">

                        <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#dropdown-box-1">
                        </button>

                        <div class="navbar-brand">
                            <a href="/main.aspx">三世书</a>
                        </div>
                    </div>

                    <!-- Collect the nav links and other content for toggling -->

                </div>
                <!-- /.container -->
            </nav>
            <!-- /.nav -->
        </header>


        <div style="text-align: center">
            <table>


                <tr style="height: 50px;">
                </tr>
            </table>





            <%--这列写 --%>
            <section style="text-align: left">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="wow bounceInLeft" data-wow-delay="0.1s">
                                <h2>请协助填写出错情况 ( Please help us to know the error )</h2>
                            </div>
                            <div class="col-sm-12">
                                <p>
                                    &nbsp&nbsp&nbsp&nbsp	烦请填写出错前进行的操作、状态，以方便我们改进，若能把出错发生时周边发生的事情、周边人说过的话、你所爱的人的名字、你的出生年月日一并填写是最好不过的，感谢！
                                </p>
                                <br />
                                <asp:TextBox runat="server" type="text" ID="txtErrorReport" class="form-control" placeholder="错误的详细情况：" TextMode="MultiLine" Height="200px" />

                                <asp:Button runat="server" ID="btnSubmit" class="btn-lg btn-primary btn-success btn-block " Text="提交" style="margin-top: 5px;" />
                                <asp:Button runat="server" type="" ID="btnGoLogin" class="btn-lg btn-primary btn-block" Text="回到主页" />
                                

                                <br />
                                <br />
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </section>











        </div>



        <!-- footer -->
        <footer id="section-footer" class="fadeIn">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 col-lg-12" style="text-align: center">

                        <div class="col-sm-7">
                            <div>
                                <address>
                                    三世书系统<br />
                                    <abbr title="Phone">Mobile: </abbr>
                                    13430337730<br />
                                    <abbr title="E-Mail">E-Mail: </abbr>
                                    <a href="mailto:13430337730@139.com">13430337730@139.com</a><br />
                                    <abbr title="E-Mail">E-Mail: </abbr>
                                    <a href="mailto:BerryBCLove@Gmail.com">BerryBCLove@Gmail.com</a><br />
                                    <abbr title="E-Mail">E-Mail: </abbr>
                                    <a href="mailto:Berry_BC_@163.com">Berry_BC_@163.com</a><br />

                                    <br />
                                    <a href="\TOS.aspx">服务条款 ( Terms Of Service )</a><br />
                                    Copyright © 2015.Berry Cui All rights reserved.<br />
                                </address>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div>
                                <address>
                                    请我喝杯咖啡呗~<br />
                                    请扫支付宝二维码~<br />
                                    <img src="\img\zhifu.jpg" height="200" width="200" />
                                </address>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </footer>
        <!-- /.footer -->



    </form>








</body>
</html>
