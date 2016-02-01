Imports PageMessage
Imports UserLibrary

Imports ErrorReportLibrary

Partial Class Register
    Inherits System.Web.UI.Page
    Dim pmMsgClass As PageMessage = New PageMessage
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary

    Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Dim intState As Integer
        Try
            If plGoLogin.Visible Then
                ScriptManager.RegisterClientScriptBlock(uplError, uplError.GetType, "1", "alert('已经注册了，请点击最上方登陆。')", True)
                form1.Focus()

            ElseIf (txtUserName.Text.Length < 3 Or txtUserName.Text.Length > 20) Then
                plError.Visible = True
                lblErrorMessage.Text = "用户名必须在3到20个字符之间"
                'Response.Write("<script language = javascript > location.href='#divAlertNo';</script>")
                form1.Focus()

            ElseIf (txtInputPassword.Text.Length < 3 Or txtInputPassword.Text.Length > 20) Then
                plError.Visible = True
                lblErrorMessage.Text = "密码必须在3到20个字符之间"
                form1.Focus()
            ElseIf (txtRepeatInputPassword.Text <> txtInputPassword.Text) Then
                plError.Visible = True
                lblErrorMessage.Text = "两次密码的输入不正确哦"

                form1.Focus()
            ElseIf (txtPhoneNumber.Text.Length < 3 Or txtPhoneNumber.Text.Length > 20) Then
                plError.Visible = True
                lblErrorMessage.Text = "电话号码必须在3到20个字符之间"

                form1.Focus()
            ElseIf (txtChineseName.Text.Length < 2 Or txtChineseName.Text.Length > 6) Then
                plError.Visible = True
                lblErrorMessage.Text = "中文名必须在2个字符到6个字符之间"
                form1.Focus()
            Else

                intState = ucUserManage.AddUser(txtUserName.Text, txtInputPassword.Text, txtPhoneNumber.Text, txtChineseName.Text, txtCompany.Text, txtEMailAddress.Text, txtQQNumber.Text)

                If intState = 88 Then

                    plError.Visible = False
                    plGoLogin.Visible = True
                ElseIf intState = -44 Then
                    plError.Visible = True
                    lblErrorMessage.Text = "注册失败，用户名已注册"
                Else
                    plError.Visible = True
                    lblErrorMessage.Text = "注册失败，为数据库原因"
                End If
                form1.Focus()

            End If

        Catch ex As Exception
            erlErrorReport.ReportServerError(1, Session("SanShiUserName"), ex.Message, Now)
            Response.Redirect("/ReportErrorLog.aspx?ep=2&eu=" & Session("SanShiUserName"))

        End Try

    End Sub

    Public Sub New()

    End Sub

    Private Sub Register_PreLoad(sender As Object, e As EventArgs) Handles Me.PreLoad
        Dim bolIsGoInside As Boolean
        If Not IsPostBack Then

            bolIsGoInside = ucUserManage.GetInOrGetOut(Session, Request.Cookies.Get("SanShiUserInfo"), Request.UserAgent.ToString, Application)
            If bolIsGoInside Then
                Response.Redirect("/Main.aspx")
            End If
        End If

    End Sub
End Class
