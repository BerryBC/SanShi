<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="CDUploadPage.aspx.vb" Inherits="ThisLife_CloudDisk_CDUploadPage" %>

<%----第41号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function JSCodeShow() {
            var divDown = document.getElementById('divShowWait');
            var newImg = document.createElement('img');
            newImg.src = '/IMG/Loading.gif';
            newImg.style.width = '10%';
            newImg.id = "LoadIMG";
            var newStrong = document.createElement('strong');
            newStrong.style = "color:rgba(200,0,500,.8);";
            newStrong.innerHTML = "<br/>请稍等唷~";
            newStrong.id = "LoadStrong";
            divDown.appendChild(newImg);
            divDown.appendChild(newStrong);
            setTimeout(function () { $('.btn-warning').attr("disabled", true); }, 10);
        };
        function setOutputContainer(text) {
            document.getElementById("divShowWait").innerHTML += ('<br/><strong style="font-size:xx-large">' + text + '</strong><br/><br/>');
        };
        $(function () {
            console.log('Aiyo');

        });
        function OnReUpload() {
            setTimeout(function () { $('.btn-warning').attr("disabled", false); }, 10);
            $('#divShowWait').css('display', 'none');
            $('.btnRU').css('display', 'none');

        };

        function OnBtnGoClick() {
            document.getElementById("divShowWait").innerHTML = '';

            var formData = new FormData();
            var fuFiles = document.getElementsByClassName("uploadInput")[0].files;

            if (fuFiles.length > 0) {


                for (var tmpI = 0; tmpI < fuFiles.length; tmpI++) {
                    formData.append(fuFiles[tmpI].name, fuFiles[tmpI]);
                }
                $.ajax({
                    url: "/ThisLife/CloudDisk/UploadHandler.ashx",
                    type: "POST",
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (result) {
                        document.getElementById("divShowWait").innerHTML = '';
                        var gotJson = eval('(' + result + ')');
                        if (gotJson.GotError == 0) {
                            var strLink="http://" + window.location.host + "/ThisLife/CloudDisk/CDDownloadPage.aspx?GetCode=" + gotJson.PassCode
                            setOutputContainer("请牢记，提取码为：" + gotJson.PassCode + "<br/>另外也可以直接把链接发给其他人共享文件，链接如下：<br/> <a href=" + strLink + " >" + strLink + "</a>");


                        } else {
                            setOutputContainer('错误了，错误代码为：' + gotJson.GotError + '，错误原因为：' + gotJson.ErrorDsp);
                        };
                    },
                    error: function () {
                        document.getElementById("divShowWait").innerHTML = '';
                        setOutputContainer("文件传输错误，或文件超出大小，或各种各样的问题，唉。");
                    }
                });
                JSCodeShow();
                $('#divShowWait').css('display', 'block');
                $('.btnRU').css('display', 'block');


            } else {
                console.log('没文件');
                $('#divShowWait').css('display', 'none');
                $('.btnRU').css('display', 'none');

            };

        };
    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
            <h2 style="border-bottom-style: groove;">云盘上传页 ( Could Disk Upload Page )</h2>
        </div>

        <div class="col-sm-12 alert alert-info" style="text-align: left; padding-left: 10px">
            <br />
            <strong>1、请选择上传文件并点击上传。<br />
                <br />
                2、请注意：上传后提示的提取码必须牢记。<br />
                <br />
                3、再注意：提取码只有2天有效。
            </strong>
            <br />
            <br />
        </div>
        <div class="col-sm-12" style="text-align: left;">
            <br />
            上传文件 ：
                        <input type="file" id="fuFileA" runat="server" class="btn btn-info uploadInput" style="display: inline" multiple="multiple" />
            <br />
            <br />
        </div>
        <div class="col-sm-12" style="text-align: center; text-align: left;">
            <br />
            <input type="button" id="btnGo" runat="server" value="点击上传" class="form-control btn-warning" onclick="OnBtnGoClick();" />
            <br />
            <input type="button" id="btnReUpload" runat="server" value="重新上传" class="form-control btn-danger btnRU" style="display: none" onclick="OnReUpload();" />

            <br />
            <br />
        </div>

        <div id="divShowWait" class="col-sm-12 alert alert-warning" style="display: none; padding: 10px;">
        </div>

                <div class="col-sm-12" style="text-align: center; text-align: left;">
                            <br/>
        <br/>
                <div class="col-sm-6" style="text-align: center; text-align: left;">

            <a role="button" id="aJumpToDownload" runat="server" href="/ThisLife/CloudDisk/CDDownloadPage.aspx"  class="form-control btn-primary "  style="text-align:center " >提取文件页</a>

        </div>
                        <div class="col-sm-6" style="text-align: center; text-align: left;">

            <input type="button" id="btnJumpToFileList" runat="server" value="本用户在云端的文件" class="form-control btn-success "  />

        </div>
                    </div>
    </div>
</asp:Content>

