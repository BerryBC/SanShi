Imports System.Data
Imports SQLServerLibrary
Imports System.Data.SqlClient

Partial Class ThisLife_DownHotSpotCellList_HotSpotDownloadByEvent
    Inherits System.Web.UI.Page
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary

    Private Sub ThisLife_DownHotSpotCellList_HotSpotDownloadByEvent_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim bolIsPowerEnough As Boolean

        btnGo.CssClass = "btn btn-warning"
        lbListOfEvent.CssClass = "form-control"
        lbListOfUser.CssClass = "form-control"
        btnGo.Enabled = False

        Try



            If Not IsPostBack Then
                bolIsPowerEnough = ucUserManage.CheckPower(Session, 1, Response)
                lbListOfEvent.Items.Clear()
                lbListOfUser.Items.Clear()
                BindConfigDataTheEvent()
                btnGo.Enabled = False
                If Application(Session("SanShiUserName") & "bwDownGCellList") IsNot Nothing Then
                    timerDelay.Enabled = True
                    plUpdating.Visible = True
                    lbListOfUser.Enabled = False
                    lbListOfEvent.Enabled = False

                End If

            End If

        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(26, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=26&eu=" & "")
            Else
                erlErrorReport.ReportServerError(26, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=26&eu=" & Session("SanShiUserName"))

            End If

        End Try


    End Sub





    Private Sub BindConfigDataTheEvent()
        Dim dtGuaranteeMana As DataTable
        Dim drTmp As DataRow
        Dim scmdCommand As SqlCommand
        Dim strSQLS As String

        Try


            strSQLS = "SELECT * FROM (SELECT [保障类型] ,[更新时间], ROW_NUMBER() OVER (PARTITION BY [保障类型]  ORDER BY [更新时间] DESC) AS intDateRank FROM [SanShi_Guarantee].[dbo].[dt_GuaranteeCellListManagement]) dtTmp  WHERE dttmp.intDateRank=1 ORDER BY dtTmp.更新时间 DESC"
            scmdCommand = sqllSSLibrary.GetCommandStr(strSQLS, CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            dtGuaranteeMana = sqllSSLibrary.GetSQLServerDataTable(scmdCommand)


            For Each drTmp In dtGuaranteeMana.Rows
                lbListOfEvent.Items.Add(drTmp.Item(0))
            Next
            If lbListOfEvent.Items.Count > 0 Then
                lbListOfEvent.Items(0).Selected = True
            End If


        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(26, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=26&eu=" & "")
            Else
                erlErrorReport.ReportServerError(26, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=26&eu=" & Session("SanShiUserName"))

            End If

        End Try


    End Sub

    Private Sub lbListOfEvent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbListOfEvent.SelectedIndexChanged
        Dim dtGuaranteeMana As DataTable
        Dim drTmp As DataRow
        Dim scmdCommand As SqlCommand
        Dim strSQLS As String
        Dim liTmp As ListItem


        Try
            lbListOfUser.Items.Clear()

            strSQLS = "SELECT [导入用户] ,[保障类型] ,[更新时间] FROM [SanShi_Guarantee].[dbo].[dt_GuaranteeCellListManagement] WHERE 保障类型='" & lbListOfEvent.SelectedValue & "'"
            scmdCommand = sqllSSLibrary.GetCommandStr(strSQLS, CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            dtGuaranteeMana = sqllSSLibrary.GetSQLServerDataTable(scmdCommand)


            For Each drTmp In dtGuaranteeMana.Rows
                If drTmp.Item(0) = "Berry" Then
                    liTmp = New ListItem
                    liTmp.Text = "官方列表 - 更新时间： " & drTmp.Item(2).ToString
                    liTmp.Value = drTmp.Item(0).ToString
                    lbListOfUser.Items.Add(liTmp)

                Else
                    liTmp = New ListItem
                    liTmp.Text = drTmp.Item(0) & " - 更新时间： " & drTmp.Item(2).ToString
                    liTmp.Value = drTmp.Item(0).ToString
                    lbListOfUser.Items.Add(liTmp)

                End If
            Next
            If lbListOfUser.Items.Count > 0 Then
                lbListOfUser.ClearSelection()
            End If


        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(26, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=26&eu=" & "")
            Else
                erlErrorReport.ReportServerError(26, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=26&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub

    Private Sub lbListOfUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbListOfUser.SelectedIndexChanged
        If Application(Session("SanShiUserName") & "bwDownGCellList") Is Nothing Then
            btnGo.Enabled = True
        End If

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim bwGetEnterWorker As BackgroundWorker

        Try


            If Application(Session("SanShiUserName") & "bwDownGCellList") Is Nothing Then
                btnGo.Enabled = False



                plUpdating.Visible = True
                plDownload.Visible = False
                lbListOfUser.Enabled = False
                lbListOfEvent.Enabled = False
                timerDelay.Enabled = True
                lblLoading.Text = "正在处理，请稍后..."
                bwGetEnterWorker = New BackgroundWorker
                AddHandler bwGetEnterWorker.DoWork, AddressOf bwGetEnterWorker_DoWork
                bwGetEnterWorker.RunWorker(Nothing)
                Application(Session("SanShiUserName") & "bwDownGCellList") = bwGetEnterWorker

            End If


        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(26, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=26&eu=" & "")
            Else
                erlErrorReport.ReportServerError(26, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=26&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub


    ''' <summary>
    ''' This operation will work without the end.
    ''' </summary>
    Private Sub bwGetEnterWorker_DoWork(ByRef progress As Integer, ByRef _result As Object, ByRef OutputTheTmp As String, ByVal ParamArray arguments As Object())
        Dim strFilePath As String
        Dim dsAllCellList As DataSet
        Dim bolIsUpdateSuccess As Boolean
        Dim strGuaranteeName As String
        Dim strUserName As String
        Dim strSaveFileName As String

        strSaveFileName = ""

        Try
            dsAllCellList = New DataSet
            dsAllCellList.Tables.Add("2G")
            dsAllCellList.Tables.Add("TD")
            dsAllCellList.Tables.Add("LTE")

            strGuaranteeName = lbListOfEvent.SelectedValue.ToString
            strUserName = lbListOfUser.SelectedValue.ToString

            strSaveFileName = Now.ToString("yyyyMMddHHmmss") & "-按保障类型下载小区列表-" & Session("SanShiUserName") & "-" & strGuaranteeName & ".xlsx"

            strFilePath = Server.MapPath("/TmpFiles/") & strSaveFileName
            dsAllCellList = RenewGCellListLibrary.DownloadCells（dsAllCellList, strUserName, strGuaranteeName)
            IO.File.Copy(Server.MapPath("/ThisLife/RenewTheGuaranteeCellList/Files/TemplateFiles/" & "Export.xlsx"), strFilePath)
            bolIsUpdateSuccess = RenewGCellListLibrary.SaveDataSetToExcel(dsAllCellList, strFilePath)



            _result = "Yes!" & strSaveFileName
        Catch ex As Exception
            _result = ex.Message

        End Try

    End Sub

    Private Sub timerDelay_Tick(sender As Object, e As EventArgs) Handles timerDelay.Tick
        Dim globalWorker As BackgroundWorker
        Dim strFilePath As String

        Try



            globalWorker = Nothing
            If Application(Session("SanShiUserName") & "bwDownGCellList") IsNot Nothing Then
                globalWorker = DirectCast(Application(Session("SanShiUserName") & "bwDownGCellList"), BackgroundWorker)
            Else
                btnGo.Enabled = True
                plUpdating.Visible = False
                timerDelay.Enabled = False
                Exit Sub
            End If
            If lblLoading.Text.Length > 40 Then
                lblLoading.Text = lblLoading.Text.Substring(0, 20)
            End If
            lblLoading.Text = lblLoading.Text & "."


            If globalWorker.Result IsNot Nothing Then
                If globalWorker.Result.ToString.Substring(0, 4) = "Yes!" Then
                    strFilePath = globalWorker.Result.ToString.Substring(4)

                    plDownload.Visible = True
                    hlDownloadLink.Text = Request.Url.Host & "/TmpFiles/" & strFilePath
                    hlDownloadLink.NavigateUrl = "/TmpFiles/" & strFilePath
                    plUpdating.Visible = False

                Else
                    lblLoading.Text = "出问题了，问题是：" & globalWorker.Result.ToString

                End If
                timerDelay.Enabled = False
                btnGo.Enabled = True
                Application(Session("SanShiUserName") & "bwDownGCellList") = Nothing
                lbListOfUser.Enabled = True
                lbListOfEvent.Enabled = True

            End If

        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(26, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=26&eu=" & "")
            Else
                erlErrorReport.ReportServerError(26, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=26&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub
End Class
