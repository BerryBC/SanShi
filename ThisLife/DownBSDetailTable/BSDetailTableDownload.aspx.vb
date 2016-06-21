Imports System.Data

Partial Class ThisLife_DownBSDetailTable_BSDetailTableDownload
    Inherits System.Web.UI.Page
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim bsdlCommonLibrary As BaseSationDetailsLibrary = New BaseSationDetailsLibrary
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary

    Private Sub ThisLife_DownBSDetailTable_BSDetailTableDownload_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim bolIsPowerEnough As Boolean

        Try


            btnGo.CssClass = "btn btn-warning"
            If Not IsPostBack Then
                bolIsPowerEnough = ucUserManage.CheckPower(Session, 2, Response)
                BindConfigData()

            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(24, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=24&eu=" & "")
            Else
                erlErrorReport.ReportServerError(24, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=24&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub
    Private Sub BindConfigData()
        Dim dtBaseSationDetailsMana As DataTable
        Dim drTmp As DataRow

        Try

            dtBaseSationDetailsMana = bsdlCommonLibrary.ReturnBaseSationDetailsMan()
            For Each drTmp In dtBaseSationDetailsMana.Rows
                lbListOfDetails.Items.Add(drTmp.Item(1))
            Next
            If lbListOfDetails.Items.Count > 0 Then
                lbListOfDetails.Items(0).Selected = True
            End If


        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(24, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=24&eu=" & "")
            Else
                erlErrorReport.ReportServerError(24, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=24&eu=" & Session("SanShiUserName"))

            End If

        End Try


    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim dtBaseSationDetailsMana As DataTable
        Dim strtmpListDir As New List(Of String)
        Dim drTmp As DataRow
        Dim intK As Integer
        Dim intI As Integer
        Dim intJ As Integer
        Dim strHeadOfSource As String
        Dim intWhereYear As Integer
        Dim intWhereMonth As Integer
        Dim intWhereDay As Integer
        Dim strDir As New List(Of String)
        Dim strFileName As String
        Try


            If lbListOfDetails.SelectedIndex >= 0 Then
                dtBaseSationDetailsMana = bsdlCommonLibrary.ReturnBaseSationDetailsMan()
                For Each drTmp In dtBaseSationDetailsMana.Rows
                    If drTmp.Item(1) = lbListOfDetails.SelectedItem.Text Then
                        intJ = drTmp.Item("UpDateSource").ToString.IndexOf("*")
                        intK = drTmp.Item("UpDateSource").ToString.IndexOf("%")
                        If intJ <> -1 And intK <> -1 Then
                            intI = CommonLibrary.GetMinNumber(intJ, intK)
                            strHeadOfSource = drTmp.Item("UpDateSource").ToString.Substring(0, intI)
                        ElseIf intJ = -1 And intK = -1 Then
                            strHeadOfSource = ""
                        ElseIf intJ = -1 Then
                            strHeadOfSource = drTmp.Item("UpDateSource").ToString.Substring(0, intK)
                        Else
                            strHeadOfSource = drTmp.Item("UpDateSource").ToString.Substring(0, intJ)
                        End If




                        If System.IO.Directory.Exists(drTmp.Item("UpDatePath")) Then
                            strtmpListDir = (From T In IO.Directory.GetFiles(drTmp.Item("UpDatePath"), strHeadOfSource & "*." & drTmp.Item("FileSuffix").ToString, IO.SearchOption.AllDirectories)).ToList
                        End If
                        intWhereYear = drTmp.Item("UpDateSource").ToString.IndexOf("%yyyy")
                        intWhereMonth = drTmp.Item("UpDateSource").ToString.IndexOf("%mm") - 1
                        intWhereDay = drTmp.Item("UpDateSource").ToString.IndexOf("%dd") - 2
                        strDir = CommonLibrary.GetMaxDateFile(strtmpListDir, intWhereYear, intWhereMonth, intWhereDay)

                        strtmpListDir.Clear()
                        strtmpListDir.Add(strDir.Item(0))
                        strDir.Clear()
                        strDir = strtmpListDir

                        strFileName = Now.ToString("yyyyMMddHHmmss") & "-基站信息表下载-" & Session("SanShiUserName") & "-" & System.IO.Path.GetFileName(strDir(0))

                        IO.File.Copy(strDir(0), Server.MapPath("/TmpFiles/") & strFileName)


                        hlDownloadLink.Text = Request.Url.Host & "/TmpFiles/" & strFileName
                        hlDownloadLink.NavigateUrl = "/TmpFiles/" & strFileName
                        plDownload.Visible = True
                        btnGo.Enabled = False

                    End If
                Next
            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(24, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=24&eu=" & "")
            Else
                erlErrorReport.ReportServerError(24, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=24&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub

    Private Sub lbListOfDetails_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbListOfDetails.SelectedIndexChanged
        plDownload.Visible = False
        btnGo.Enabled = True

    End Sub

End Class
