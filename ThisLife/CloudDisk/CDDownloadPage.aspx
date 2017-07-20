<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="CDDownloadPage.aspx.vb" Inherits="ThisLife_CloudDisk_CDDownloadPage" %>

<%----第42号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ShowDelay() {

            $('.divWait').css('display', 'block');
            setTimeout(function () { $('.divOutTable').css('display', 'block'); $('.divWait').css('display', 'none'); }, 888);
        };
        $(function () {
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>


            <div class="container">
                <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                    <h2 style="border-bottom-style: groove;">云盘下载页 ( Could Disk Download Page )</h2>
                </div>
                <div class="col-sm-12 alert alert-info" style="text-align: center; text-align: center; padding-left: 10px">
                    <br />
                    <asp:Label ID="lblTipsCode" runat="server" Text="请输入提取码：" Font-Size="X-Large"></asp:Label>

                    <asp:TextBox ID="txtCode" runat="server" Font-Size="X-Large"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" class="btn btn-success" CommandName="search" Text="查找文件" Font-Size="X-Large" Style="margin-top: -8px" />
                    <br />
                    <br />
                </div>

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <br />
                    <br />
                </div>


                <div id="divAllOutput" runat="server" class="col-lg-12 col-md-12 col-sm-12 col-xs-12 divOutTable" style="text-align: left; border-style: ridge; padding-bottom: 0px; display: none">

                    <div id="divOutPut" runat="server" class="table-responsive" style="padding-bottom: 0px; margin-bottom: 0px;">
                        <table id="tbOutPut" runat="server" class="table table-striped" style="padding-bottom: 0px; margin-bottom: 0px;">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:Label ID="lblTime" runat="server" Text="上传时间"></asp:Label></th>

                                    <th>
                                        <asp:Label ID="lblFileName" runat="server" Text="文件名"></asp:Label></th>
                                    <th>
                                        <asp:Label ID="lblDownLink" runat="server" Text="下载链接"></asp:Label></th>
                                </tr>
                            </thead>




                        </table>
                    </div>
                    <div id="divWrong" runat="server" class="col-sm-12 alert alert-warning" style="text-align: center; padding-left: 10px;">

                        <strong id="Wrong" style="color: rgba(200,0,500,.8);">
                            <br />
                            该提取码错误或者文件已过期。</strong>
                        <br />
                        <br />
                    </div>



                </div>

                <div id="divWait" runat="server" class="col-sm-12 alert alert-warning divWait" style="text-align: center; padding-left: 10px; display: none">
                    <br />
                    <img src="/IMG/Loading.gif" id="LoadIMG" style="width: 10%" />
                    <strong id="LoadStrong" style="color: rgba(200,0,500,.8);">
                        <br />
                        请稍等唷~</strong>
                    <br />
                    <br />
                </div>

                <div class="col-sm-12" style="text-align: center; text-align: left;">
                    <br />
                    <br />
                    <div class="col-sm-6" style="text-align: center; text-align: left;">

                        <a role="button" id="aJumpToDownload" runat="server" href="/ThisLife/CloudDisk/CDUploadPage.aspx" class="form-control btn-info " style="text-align: center">上传文件页</a>

                    </div>
                    <div class="col-sm-6" style="text-align: center; text-align: left;">

                        <input type="button" id="btnJumpToFileList" runat="server" value="本用户在云端的文件" class="form-control btn-success " />

                    </div>
                </div>

            </div>

        </ContentTemplate>


    </asp:UpdatePanel>
</asp:Content>

