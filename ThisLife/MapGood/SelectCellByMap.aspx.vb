Imports SQLServerLibrary
Partial Class ThisLife_MapGood_SelectCellByMap
    Inherits System.Web.UI.Page
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim clCommon As CommonLibrary = New CommonLibrary

    Private Sub ThisLife_MapGood_SelectCellByMap_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim hcToken As HttpCookie
        Try

            If Not IsPostBack Then
                ucUserManage.CheckPower(Session, 2, Response)

                hcToken = New HttpCookie("TokenForNN")
                hcToken.Values.Add("Token", clCommon.StringTranscodingToMD5(Date.Now.ToShortDateString))
                hcToken.Expires = DateTime.MaxValue
                Response.AppendCookie(hcToken)
                Response.Write("")

            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(46, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=46&eu=" & "")
            Else
                erlErrorReport.ReportServerError(46, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=46&eu=" & Session("SanShiUserName"))

            End If

        End Try
    End Sub
End Class
