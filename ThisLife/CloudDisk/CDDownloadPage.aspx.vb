Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports SQLServerLibrary
Imports CSVLibrary
Partial Class ThisLife_CloudDisk_CDDownloadPage
    Inherits System.Web.UI.Page
    Dim bscpCommonLibrary As BSCPara = New BSCPara
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary
    Dim ucUserManage As UserLibrary = New UserLibrary

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        GoFindFiles(txtCode.Text)
    End Sub
    Private Sub GoFindFiles(strCodeInput As String)
        Dim scmdCMD As SqlCommand
        Dim dtBaseSationDetailsMana As DataTable
        Dim longJ As Long
        Dim drDataInDataTable As DataRow
        Dim tableHtmlTable As HtmlTable
        Dim trHtmlRows As HtmlTableRow
        Dim tdHtmlCells As HtmlTableCell
        Dim strCode As String

        Try

            strCode = strCodeInput.Replace("%", "").Replace("[", "").Replace("]", "").Replace("_", "")

            tableHtmlTable = tbOutPut


            scmdCMD = sqllSSLibrary.GetCommandStr("SELECT * FROM [dbo].[dt_UploadFileInfo]  WHERE SaveTime>GETDATE()-2 AND Password LIKE '" & strCode & "'", CommonLibrary.GetSQLServerConnect("ConnectionLogDB"))
            dtBaseSationDetailsMana = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)

            If dtBaseSationDetailsMana.Rows.Count > 0 Then


                For longJ = 0 To dtBaseSationDetailsMana.Rows.Count - 1
                    drDataInDataTable = dtBaseSationDetailsMana.Rows.Item(longJ)
                    trHtmlRows = New HtmlTableRow

                    tdHtmlCells = New HtmlTableCell
                    tdHtmlCells.InnerHtml = drDataInDataTable(0).ToString
                    trHtmlRows.Cells.Add(tdHtmlCells)
                    tdHtmlCells = New HtmlTableCell
                    tdHtmlCells.InnerHtml = drDataInDataTable(2).ToString
                    trHtmlRows.Cells.Add(tdHtmlCells)
                    tdHtmlCells = New HtmlTableCell
                    tdHtmlCells.InnerHtml = "<a href=""http://" & Request.Url.Host & "/TmpFiles/" & drDataInDataTable(3).ToString & """>请点击这里下载</a>"
                    trHtmlRows.Cells.Add(tdHtmlCells)


                    tableHtmlTable.Rows.Add(trHtmlRows)

                Next
                divOutPut.Style.Item("display") = "block"
                divWrong.Style.Item("display") = "none"
            Else
                divOutPut.Style.Item("display") = "none"
                divWrong.Style.Item("display") = "block"
            End If
            'divAllOutput.Style.Item("display") = "block"
            ScriptManager.RegisterClientScriptBlock(divAllOutput, Me.GetType, "ShowDelay", "ShowDelay();", True)

        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(42, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=42&eu=" & "")
            Else
                erlErrorReport.ReportServerError(42, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=42&eu=" & Session("SanShiUserName"))

            End If
        End Try

    End Sub

    Private Sub ThisLife_CloudDisk_CDDownloadPage_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                divAllOutput.Style.Item("display") = "none"
                If (Request.Params("GetCode") IsNot Nothing) Then
                    GoFindFiles(Request.Params("GetCode"))
                    divWait.Style.Item("display") = "block"
                End If


            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(42, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=42&eu=" & "")
            Else
                erlErrorReport.ReportServerError(42, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=42&eu=" & Session("SanShiUserName"))

            End If

        End Try
    End Sub
End Class
