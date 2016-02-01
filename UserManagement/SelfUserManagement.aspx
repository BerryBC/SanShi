<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="SelfUserManagement.aspx.vb" Inherits="UserManagement_SelfUserManagement" %>

<%----第9号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section>
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
        </script>
        <div class="container">

            <div class="col-sm-2"></div>
            <div class="col-sm-8" style="text-align: left;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">用户信息 ( User Infomation )</h2>
                        </div>
                        用户名 :<br />
                        <asp:TextBox ID="txtUserName" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        用户电话号码 :<br />
                        <asp:TextBox ID="txtPhoneNumber" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        用户中文名 :<br />
                        <asp:TextBox ID="txtRealChineseName" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        公司名 :<br />
                        <asp:TextBox ID="txtCompanyName" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        E-Mail地址 :<br />
                        <asp:TextBox ID="txtEMail" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        QQ号码 :<br />
                        <asp:TextBox ID="txtQQ" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />

                        <div style="text-align: center;" id="ModifyButtonDIV">

                            <br />
                            <asp:Button ID="btnGoModifyInformation" runat="server" class="btn btn-info" Text="开始修改用户信息" Style="margin: 10px;" />
                            <asp:Button ID="btnConfirmInformation" runat="server" class="btn btn-success" Text="确定修改用户信息" Style="margin: 10px;" Enabled="false" /><br />
                        </div>
                        <asp:Panel ID="plError" runat="server" Visible="False">
                            <div id="divAlertNo" class="alert alert-danger" role="alert" style="margin-top: 5px;">
                                <asp:Label ID="lblErrorMessage" runat="server" Text="Label"></asp:Label>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="plGoLogin" runat="server" Visible="False">
                            <div id="divAlertOK" class="alert alert-success" role="alert" style="margin-top: 5px;">
                                <strong>
                                    <asp:Label ID="lblGoToLogin" runat="server" Text="已经修改信息了"></asp:Label></strong>
                            </div>
                        </asp:Panel>

                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">修改密码 ( Change Password )</h2>
                        </div>

                        旧密码 :<br />
                        <asp:TextBox ID="txtOldPassword" type="password" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        新密码 :<br />
                        <asp:TextBox ID="txtNewPassword" type="password" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        请重新输入新密码 :<br />
                        <asp:TextBox ID="txtNewPasswordAgain" type="password" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        <div style="text-align: center;">

                            <br />
                            <asp:Button ID="btnWantChangePassword" runat="server" class="btn btn-info" Text="开始修改用户密码" Style="margin: 10px;" />
                            <asp:Button ID="btnConfirmChangePassword" runat="server" class="btn btn-success" Text="确定修改用户密码" Style="margin: 10px;" Enabled="false" /><br />
                        </div>
                        <asp:Panel ID="plErrorChangePassword" runat="server" Visible="False">
                            <div class="alert alert-danger" role="alert" style="margin-top: 5px;">
                                <asp:Label ID="lblErrorMessageChangePassword" runat="server" Text="Label"></asp:Label>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="plPassChangePassword" runat="server" Visible="False">
                            <div class="alert alert-success" role="alert" style="margin-top: 5px;">
                                <strong>
                                    <asp:Label ID="lblChangedPassword" runat="server" Text="已经修改密码"></asp:Label></strong>
                            </div>
                        </asp:Panel>



                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">业力 ( Karma )</h2>
                        </div>

                        第一次注册的时间 :<br />
                        <asp:TextBox ID="txtFirstRegister" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        登陆的次数 :<br />
                        <asp:TextBox ID="txtLogTime" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        最后登录的时间 :<br />
                        <asp:TextBox ID="txtLastLogin" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        权限等级 :<br />
                        <asp:TextBox ID="txtPowerLevel" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-2"></div>

        </div>
    </section>
</asp:Content>

