
Partial Class CanNotAccess
    Inherits System.Web.UI.Page
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary

    Private Sub CanNotAccess_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If CType(Session("PowerLevel"), Integer) = 0 Then
                lblError.Text = "您未登陆，无法使用该功能，请登陆或者注册。"
                plGoLogIn.Visible = True
            End If

        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(6, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=6&eu=" & "")
            Else
                erlErrorReport.ReportServerError(6, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=6&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub
End Class
