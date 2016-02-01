<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Register.aspx.vb" Inherits="Register" %>
<!--第2号页面-->

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/animate.css" rel="stylesheet" type="text/css" />
    <link href="css/signin.css" rel="stylesheet" />


    <script src="JS/jquery.min.js"></script>
    <script src="JS/bootstrap.min.js"></script>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
        <meta name="renderer" content="webkit"/>


    <title>Register</title>
</head>
<body class="body-register">
    <div>
        <form id="form1" class="form-signin" runat="server">
            <asp:ScriptManager runat="server"></asp:ScriptManager>



            <h1 class="form-signin-heading">Register</h1>
            <asp:UpdatePanel ID="uplError" runat="server">
                <ContentTemplate>

                    <asp:Panel ID="plError" runat="server" Visible="False">
                        <div id="divAlertNo" class="alert alert-danger" role="alert" style="margin-top: 5px;">
                            <asp:Label ID="lblErrorMessage" runat="server" Text="Label"></asp:Label>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnRegister" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="uplOK" runat="server">
                <ContentTemplate>

                    <asp:Panel ID="plGoLogin" runat="server" Visible="False">
                        <div id="divAlertOK" class="alert alert-success" role="alert" style="margin-top: 5px;" onclick="javascript:location.href='./login.aspx';return false;">
                            <strong>
                                <asp:Label ID="lblGoToLogin" runat="server" Text="点击此处返回登录页"></asp:Label></strong>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnRegister" EventName="Click" />
                </Triggers>


            </asp:UpdatePanel>
            <h4 class="form-signin-heading">必填信息</h4>
            <asp:TextBox runat="server" type="text" ID="txtUserName" class="form-control" placeholder="User Name" required="" autofocus="" />

            <asp:TextBox runat="server" type="password" ID="txtInputPassword" class="form-control" placeholder="Password" required="" />
            <asp:TextBox runat="server" type="password" ID="txtRepeatInputPassword" class="form-control" placeholder="Repeat Password" required="" />
            <asp:TextBox runat="server" type="text" ID="txtPhoneNumber" class="form-control" placeholder="Phone Number" required="" autofocus="" />
            <asp:TextBox runat="server" type="text" ID="txtChineseName" class="form-control" placeholder="Real Chinese Name" required="" autofocus="" />
            <h4 class="form-signin-heading">选填信息</h4>

            <asp:TextBox runat="server" type="text" ID="txtCompany" class="form-control" placeholder="Company Name" />
            <asp:TextBox runat="server" type="text" ID="txtEMailAddress" class="form-control" placeholder="E-Mail" />
            <asp:TextBox runat="server" type="text" ID="txtQQNumber" class="form-control" placeholder="QQ" />

            <asp:Button ID="btnRegister" runat="server" class="btn btn-lg btn-primary btn-block animated" Text="Register"  />

            <button class="btn btn-lg btn-primary btn-danger btn-block " onclick="javascript:location.href='./login.aspx';return false;">Back</button>

        </form>
    </div>
</body>
</html>
