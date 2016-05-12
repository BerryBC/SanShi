Imports System.Data.SqlClient
Imports SQLServerLibrary
Imports System.Data


Partial Class EarthlyBranch_LogViewer_BaseSationDetailsLogViewer
    Inherits System.Web.UI.Page

    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer

    Private Sub ddlHowManyRow_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHowManyRow.SelectedIndexChanged
        txtLogMessage.Text = ""
        LogDataBind(CType(ddlHowManyRow.SelectedItem.Value, Integer))

    End Sub

    Private Sub EarthlyBranch_LogViewer_BaseSationDetailsLogViewer_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            If Not IsPostBack Then
                ucUserManage.CheckPower(Session, 9, Response)

                txtLogMessage.CssClass = "form-control"
                LogDataBind(3)
            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(16, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=16&eu=" & "")
            Else
                erlErrorReport.ReportServerError(16, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=16&eu=" & Session("SanShiUserName"))

            End If

        End Try
    End Sub
    Private Sub LogDataBind(intLogPage As Integer)
        Dim dtLogData As DataTable
        Dim intHowManyDay As Integer
        Dim strFilterSQL As String
        Dim scmdCMD As SqlCommand
        Dim drtmpLogOut As DataRow

        Try

            dtLogData = New DataTable
            intHowManyDay = intLogPage

            strFilterSQL = "SELECT *   FROM [SanShi_Log].[dbo].[dt_BaseSationConfigLog]   where ([DoConfigTime] >= getdate() -" & intHowManyDay & " )     order by  [DoConfigTime] desc"



            scmdCMD = sqllSSLibrary.GetCommandStr(strFilterSQL, CommonLibrary.GetSQLServerConnect("ConnectionLogDB"))
            dtLogData = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
            For Each drtmpLogOut In dtLogData.Rows
                txtLogMessage.Text = txtLogMessage.Text & drtmpLogOut.Item(1).ToString & "  " & drtmpLogOut.Item(2).ToString & vbCrLf


            Next

        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(16, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=16&eu=" & "")
            Else
                erlErrorReport.ReportServerError(16, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=16&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub
End Class
