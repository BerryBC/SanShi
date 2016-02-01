<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<!--第1号页面-->
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



    <title>Login</title>
</head>
<body>
    <div>
        <form id="form1" runat="server" class="form-signin">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <h1 class="form-signin-heading" style="padding-top: 130px">The Book of SanShi System <br/>&lt;Please login&gt;</h1>
            <asp:UpdatePanel ID="uplError" runat="server">
                <ContentTemplate>

                    <asp:Panel ID="plError" runat="server" Visible="False">
                        <div id="divAlertNo" class="alert alert-danger" role="alert" style="margin-top: 5px;">
                            <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnLogIn" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>

            <label for="inputEmail" class="sr-only">User Name</label>
            <asp:TextBox runat="server"  type="text" id="txtUserName" class="form-control" placeholder="User Name"  autofocus="" />
            <label for="inputPassword" class="sr-only">Password</label>
            <asp:TextBox runat="server"  type="password" id="txtPassword" class="form-control" placeholder="Password"  />
            <div class="checkbox">
                <label>
                    <asp:CheckBox ID="cbRemember" runat="server"  value="remember-me"  />


                    Remember me
                </label>
            </div>
            <asp:Button runat="server" ID="btnLogIn"  class="btn btn-lg btn-primary btn-success btn-block" Text="Login" />
            <asp:Button runat="server" ID="btnGoRegister" class="btn btn-lg btn-primary btn-block" Text ="Register"/>
            <p style="color :whitesmoke ;text-align :center; padding-top :10px;">(If the textbox is blank,just switch another browser)</p>
            <%-- <p style="color :whitesmoke ;text-align :center; ">(如果文本框为空,请更换更为聪明的浏览器)</p>--%>
        </form>
    </div>
</body>
</html>
