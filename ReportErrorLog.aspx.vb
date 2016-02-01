Imports ErrorReportLibrary

Partial Class ReportErrorLog
    Inherits System.Web.UI.Page
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary

    Public Sub New()
    End Sub

    Protected Sub btnGoLogin_Click(sender As Object, e As EventArgs) Handles btnGoLogin.Click
        Response.Redirect("/Login.aspx")
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim intErrorPage As Integer
        Dim strErrorUser As String

        If (Request.Params("EP") Is Nothing) Then
            intErrorPage = -1
        Else
            intErrorPage = Request.Params("EP")
        End If
        If (Request("EU") Is Nothing) Then
            strErrorUser = ""
        Else
            strErrorUser = Request("EU")
        End If


        erlErrorReport.ReportUserError(intErrorPage, strErrorUser, txtErrorReport.Text, Now)
        Response.Redirect("/Login.aspx")


    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
    End Sub
End Class
