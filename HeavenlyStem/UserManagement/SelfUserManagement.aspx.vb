Imports System.Data

Partial Class UserManagement_SelfUserManagement
    Inherits System.Web.UI.Page
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary

    Private Sub btnGoModifyInformation_Click(sender As Object, e As EventArgs) Handles btnGoModifyInformation.Click
        btnConfirmInformation.Enabled = True
        btnGoModifyInformation.Enabled = False
        btnGoModifyInformation.CssClass = "btn btn-info"
        btnWantChangePassword.Enabled = True
        btnConfirmChangePassword.Enabled = False
        btnConfirmChangePassword.CssClass = "btn btn-success"

        txtCompanyName.Enabled = True
        txtEMail.Enabled = True
        txtPhoneNumber.Enabled = True
        txtQQ.Enabled = True
        txtRealChineseName.Enabled = True

        txtOldPassword.Enabled = False
        txtNewPassword.Enabled = False
        txtNewPasswordAgain.Enabled = False
        txtOldPassword.Text = ""
        txtNewPassword.Text = ""
        txtNewPasswordAgain.Text = ""

        plGoLogin.Visible = False
        plPassChangePassword.Visible = False
        plErrorChangePassword.Visible = False

    End Sub

    Private Sub UserManagement_SelfUserManagement_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim bolIsPowerEnough As Boolean = False


        btnConfirmInformation.CssClass = "btn btn-success"
        btnConfirmChangePassword.CssClass = "btn btn-success"
        Try
            bolIsPowerEnough = ucUserManage.CheckPower(Session, 1, Response)
            If Not IsPostBack Then
                If bolIsPowerEnough Then BindUserData()
            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(9, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=9&eu=" & "")
            Else
                erlErrorReport.ReportServerError(9, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=9&eu=" & Session("SanShiUserName"))

            End If


        End Try
    End Sub

    Private Sub BindUserData()
        Dim dtUserInfo As DataTable
        Try

            dtUserInfo = ucUserManage.ReturnOneUserInfoByUserName(Session("SanShiUserName"))
            txtUserName.Text = dtUserInfo.Rows(0).Item("UserName").ToString
            txtPhoneNumber.Text = dtUserInfo.Rows(0).Item("PhoneNumber").ToString
            txtRealChineseName.Text = dtUserInfo.Rows(0).Item("RealChineseName").ToString
            txtCompanyName.Text = dtUserInfo.Rows(0).Item("CompanyName").ToString
            txtEMail.Text = dtUserInfo.Rows(0).Item("EMailAddress").ToString
            txtQQ.Text = dtUserInfo.Rows(0).Item("QQ").ToString
            txtFirstRegister.Text = dtUserInfo.Rows(0).Item("FirstRegister").ToString
            txtLogTime.Text = dtUserInfo.Rows(0).Item("OnLineTime").ToString
            txtLastLogin.Text = dtUserInfo.Rows(0).Item("LastLogIn").ToString
            txtPowerLevel.Text = dtUserInfo.Rows(0).Item("PowerLevel").ToString
            plError.Visible = False
            plErrorChangePassword.Visible = False
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(9, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=9&eu=" & "")
            Else
                erlErrorReport.ReportServerError(9, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=9&eu=" & Session("SanShiUserName"))

            End If


        End Try
    End Sub

    Private Sub btnConfirmInformation_Click(sender As Object, e As EventArgs) Handles btnConfirmInformation.Click
        If ((txtPhoneNumber.Text.Length < 3) Or (txtPhoneNumber.Text.Length > 20)) Then
            plError.Visible = True
            lblErrorMessage.Text = "电话号码必须在3到20个字符之间"
        ElseIf ((txtRealChineseName.Text.Length < 2) Or (txtRealChineseName.Text.Length > 6)) Then
            plError.Visible = True
            lblErrorMessage.Text = "中文名必须在2个字符到6个字符之间"


        Else
            Try

                ucUserManage.ModifyUserInformation(Session("SanShiUserName"), txtPhoneNumber.Text, txtRealChineseName.Text, txtCompanyName.Text, txtEMail.Text, txtQQ.Text)
                txtCompanyName.Enabled = False
                txtEMail.Enabled = False
                txtPhoneNumber.Enabled = False
                txtQQ.Enabled = False
                txtRealChineseName.Enabled = False
                btnConfirmInformation.Enabled = False
                btnGoModifyInformation.Enabled = True
                btnConfirmInformation.CssClass = "btn btn-success"
                plError.Visible = False
                plGoLogin.Visible = True

                BindUserData()
            Catch ex As Exception
                If Session("SanShiUserName") Is Nothing Then
                    erlErrorReport.ReportServerError(9, "", ex.Message, Now)
                    Response.Redirect("/ReportErrorLog.aspx?ep=9&eu=" & "")
                Else
                    erlErrorReport.ReportServerError(9, Session("SanShiUserName"), ex.Message, Now)
                    Response.Redirect("/ReportErrorLog.aspx?ep=9&eu=" & Session("SanShiUserName"))

                End If


            End Try
        End If

    End Sub

    Private Sub btnWantChangePassword_Click(sender As Object, e As EventArgs) Handles btnWantChangePassword.Click
        btnConfirmInformation.Enabled = False
        btnGoModifyInformation.Enabled = True
        btnWantChangePassword.Enabled = False
        btnConfirmChangePassword.Enabled = True
        btnWantChangePassword.CssClass = "btn btn-info"


        txtCompanyName.Enabled = False
        txtEMail.Enabled = False
        txtPhoneNumber.Enabled = False
        txtQQ.Enabled = False
        txtRealChineseName.Enabled = False

        txtOldPassword.Enabled = True
        txtNewPassword.Enabled = True
        txtNewPasswordAgain.Enabled = True
        txtOldPassword.Text = ""
        txtNewPassword.Text = ""
        txtNewPasswordAgain.Text = ""

        plGoLogin.Visible = False
        plError.Visible = False
        plPassChangePassword.Visible = False

    End Sub

    Private Sub btnConfirmChangePassword_Click(sender As Object, e As EventArgs) Handles btnConfirmChangePassword.Click
        If txtNewPassword.Text <> txtNewPasswordAgain.Text Then
            plErrorChangePassword.Visible = True
            lblErrorMessageChangePassword.Text = "两次新密码输入错误了"

        Else
            Try

                Dim dtCheckOldPassword As DataTable
                dtCheckOldPassword = ucUserManage.UserLoginCheck(Session("SanShiUserName"), txtOldPassword.Text, Request.UserAgent.ToString)
                If dtCheckOldPassword.Rows.Count > 0 Then
                    ucUserManage.ChangeUserPassword(Session("SanShiUserName"), txtNewPassword.Text)
                    txtOldPassword.Enabled = False
                    txtNewPassword.Enabled = False
                    txtNewPasswordAgain.Enabled = False
                    txtOldPassword.Text = ""
                    txtNewPassword.Text = ""
                    txtNewPasswordAgain.Text = ""
                    btnConfirmChangePassword.Enabled = False
                    plErrorChangePassword.Visible = False
                    btnWantChangePassword.Enabled = True
                    plPassChangePassword.Visible = True

                Else
                    plErrorChangePassword.Visible = True
                    lblErrorMessageChangePassword.Text = "密码错啊！"

                End If
            Catch ex As Exception
                If Session("SanShiUserName") Is Nothing Then
                    erlErrorReport.ReportServerError(9, "", ex.Message, Now)
                    Response.Redirect("/ReportErrorLog.aspx?ep=9&eu=" & "")
                Else
                    erlErrorReport.ReportServerError(9, Session("SanShiUserName"), ex.Message, Now)
                    Response.Redirect("/ReportErrorLog.aspx?ep=9&eu=" & Session("SanShiUserName"))

                End If


            End Try


        End If
    End Sub
End Class
