Imports System.Data

Partial Class ThisLife_DownBSDetailTable_DownloadServerFiles
    Inherits System.Web.UI.Page
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary
    Dim dsfServerFilesLibrary As DownloadServerFilesLibrary = New DownloadServerFilesLibrary

    Private Sub ThisLife_DownBSDetailTable_DownloadServerFiles_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim bolIsPowerEnough As Boolean

        Try


            btnGo.CssClass = "btn btn-warning"
            If Not IsPostBack Then
                bolIsPowerEnough = ucUserManage.CheckPower(Session, 4, Response)
                BindConfigData()

            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(30, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=30&eu=" & "")
            Else
                erlErrorReport.ReportServerError(30, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=30&eu=" & Session("SanShiUserName"))

            End If



        End Try

    End Sub



    Private Sub BindConfigData()
        Dim dtServerFilePath As DataTable
        Dim drTmp As DataRow
        Dim litmpItem As ListItem

        Try

            dtServerFilePath = dsfServerFilesLibrary.ReturnServerFilePath()
            For Each drTmp In dtServerFilePath.Rows
                litmpItem = New ListItem
                litmpItem.Text = drTmp.Item(0)
                litmpItem.Value = drTmp.Item(1)
                lbListOfDetails.Items.Add(litmpItem)
            Next
            lbListOfDetails.ClearSelection()
            plDownload.Visible = False
            btnGo.Enabled = False


        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(30, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=30&eu=" & "")
            Else
                erlErrorReport.ReportServerError(30, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=30&eu=" & Session("SanShiUserName"))

            End If

        End Try


    End Sub

    Private Sub lbListOfDetails_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbListOfDetails.SelectedIndexChanged
        Dim strtmpListDir As New List(Of String)
        Dim strtmpFiles As String
        Dim litmpItem As ListItem

        Try

            plDownload.Visible = False
            btnGo.Enabled = False
            lbListOfFiles.Items.Clear()

            strtmpListDir = (From T In IO.Directory.GetFiles(lbListOfDetails.SelectedValue, "*.*", IO.SearchOption.TopDirectoryOnly)).ToList
            For Each strtmpFiles In strtmpListDir
                litmpItem = New ListItem
                litmpItem.Text = System.IO.Path.GetFileName(strtmpFiles)
                litmpItem.Value = strtmpFiles
                lbListOfFiles.Items.Add(litmpItem)
            Next
            lbListOfFiles.ClearSelection()
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(30, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=30&eu=" & "")
            Else
                erlErrorReport.ReportServerError(30, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=30&eu=" & Session("SanShiUserName"))

            End If


        End Try

    End Sub

    Private Sub lbListOfFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbListOfFiles.SelectedIndexChanged
        plDownload.Visible = False
        btnGo.Enabled = True
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim strFileName As String
        Try

            strFileName = Now.ToString("yyyyMMddHHmmss") & "-服务器文件下载-" & Session("SanShiUserName") & "-" & System.IO.Path.GetFileName(lbListOfFiles.SelectedValue)
            IO.File.Copy(lbListOfFiles.SelectedValue, Server.MapPath("/TmpFiles/") & strFileName)
            hlDownloadLink.Text = Request.Url.Host & "/TmpFiles/" & strFileName
            hlDownloadLink.NavigateUrl = "/TmpFiles/" & strFileName
            plDownload.Visible = True
            btnGo.Enabled = False
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(30, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=30&eu=" & "")
            Else
                erlErrorReport.ReportServerError(30, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=30&eu=" & Session("SanShiUserName"))

            End If


        End Try


    End Sub
End Class
