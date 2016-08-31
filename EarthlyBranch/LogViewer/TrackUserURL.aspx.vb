Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports SQLServerLibrary
Imports CSVLibrary

Partial Class EarthlyBranch_LogViewer_TrackUserURL
    Inherits System.Web.UI.Page
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer

    Private Sub EarthlyBranch_LogViewer_TrackUserURL_Load(sender As Object, e As EventArgs) Handles Me.Load


        If Not IsPostBack Then
            Try
                ucUserManage.CheckPower(Session, 9, Response)

                BindDataOfUserURLLog()

            Catch ex As Exception
                If Session("SanShiUserName") Is Nothing Then
                    erlErrorReport.ReportServerError(29, "", ex.Message, Now)
                    Response.Redirect("/ReportErrorLog.aspx?ep=29&eu=" & "")
                Else
                    erlErrorReport.ReportServerError(29, Session("SanShiUserName"), ex.Message, Now)
                    Response.Redirect("/ReportErrorLog.aspx?ep=29&eu=" & Session("SanShiUserName"))

                End If

            End Try

        End If


    End Sub






    Private Sub BindDataOfUserURLLog()
        Dim scmdCMD As SqlCommand
        Dim dtUserURLLog As DataTable
        Dim intI As Integer
        Dim longJ As Long
        Dim drDataInDataTable As DataRow
        Dim tableHtmlTable As HtmlTable
        Dim trHtmlRows As HtmlTableRow
        Dim tdHtmlCells As HtmlTableCell
        Dim strFilterSQL As String
        Dim liststrFilterEle As List(Of String)
        Dim strtmpFilterSQL As String
        Dim intOnePageNumber As Integer

        Try



            intOnePageNumber = CType(ddlHowManyRow.SelectedItem.Value, Integer)


            strFilterSQL = "SELECT *  FROM [SanShi_Log].[dbo].[dt_UserLogURL] "

            '加上对逗号的区分
            liststrFilterEle = txtSearchWhat.Text.Replace("，", ",").Split(",").ToList
            If liststrFilterEle.Count > 0 Then strFilterSQL += " where [User_ID] not like 'Berry' and"

            For Each strtmpFilterSQL In liststrFilterEle
                strFilterSQL += " [User_ID] like '%" & strtmpFilterSQL & "%' or "
            Next

            strFilterSQL = strFilterSQL.Substring(0, strFilterSQL.Length - 3)

            strFilterSQL += " ORDER BY LogTime DESC"

            scmdCMD = sqllSSLibrary.GetCommandStr(strFilterSQL, CommonLibrary.GetSQLServerConnect("ConnectionLogDB"))
            dtUserURLLog = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)



            tableHtmlTable = tbOutPut


            For longJ = 0 To CommonLibrary.GetMinNumber(intOnePageNumber, dtUserURLLog.Rows.Count) - 1
                drDataInDataTable = dtUserURLLog.Rows.Item(longJ)
                trHtmlRows = New HtmlTableRow
                For intI = 0 To 2
                    tdHtmlCells = New HtmlTableCell
                    tdHtmlCells.InnerHtml = drDataInDataTable(intI).ToString
                    trHtmlRows.Cells.Add(tdHtmlCells)
                Next


                tableHtmlTable.Rows.Add(trHtmlRows)

            Next

            scmdCMD.Dispose()
            scmdCMD = Nothing
            dtUserURLLog.Dispose()
            dtUserURLLog = Nothing


            'listOnRoad.Clear()
            drDataInDataTable = Nothing
            tableHtmlTable = Nothing
            trHtmlRows = Nothing
            tdHtmlCells = Nothing
            'liststrFilterEle.Clear()
            liststrFilterEle = Nothing


        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(29, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=29&eu=" & "")
            Else
                erlErrorReport.ReportServerError(29, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=29&eu=" & Session("SanShiUserName"))

            End If

        End Try






    End Sub

    Private Sub ddlHowManyRow_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHowManyRow.SelectedIndexChanged
        BindDataOfUserURLLog()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindDataOfUserURLLog()

    End Sub
End Class
