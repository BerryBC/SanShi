Imports System.Data
Imports System.Data.SqlClient
Imports SQLServerLibrary

Partial Class ThisLife_DownHotSpotCellList_HotSpotDownloadByHotspot
    Inherits System.Web.UI.Page
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary

    Private Sub ThisLife_DownHotSpotCellList_HotSpotDownloadByHotspot_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim bolIsPowerEnough As Boolean

        Try




            btnGo.CssClass = "btn btn-warning"

            If Not IsPostBack Then
                bolIsPowerEnough = ucUserManage.CheckPower(Session, 1, Response)
                lbHotspot.Items.Clear()
                lbListOfHotspot.Items.Clear()
                btnGo.Enabled = False
                If Application(Session("SanShiUserName") & "bwDownGCellListByHS") IsNot Nothing Then
                    timerDelay.Enabled = True
                    plUpdating.Visible = True

                End If

            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(27, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=27&eu=" & "")
            Else
                erlErrorReport.ReportServerError(27, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=27&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub

    Private Sub btnSearchHotSpot_Click(sender As Object, e As EventArgs) Handles btnSearchHotSpot.Click
        Dim dtGuaranteeMana As DataTable
        Dim scmdCommand As SqlCommand
        Dim strSQLS As String
        Dim drTmp As DataRow
        Try

            lbListOfHotspot.Items.Clear()
            strSQLS = "SELECT [区域名称] FROM [SanShi_Guarantee].[dbo].[dt_GSM_Guarantee_Cell_List] WHERE [区域名称] like '%" & txtHotspot.Text & "%' group by [区域名称]"
            scmdCommand = sqllSSLibrary.GetCommandStr(strSQLS, CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            dtGuaranteeMana = sqllSSLibrary.GetSQLServerDataTable(scmdCommand)

            For Each drTmp In dtGuaranteeMana.Rows
                lbListOfHotspot.Items.Add(drTmp.Item(0))
            Next

        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(27, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=27&eu=" & "")
            Else
                erlErrorReport.ReportServerError(27, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=27&eu=" & Session("SanShiUserName"))

            End If

        End Try


    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim arrintSelected As Integer()
        Dim intI As Integer
        Dim tmpliItem As ListItem

        Try

            arrintSelected = lbListOfHotspot.GetSelectedIndices()

            If arrintSelected.Count > 0 Then
                For Each intI In arrintSelected
                    tmpliItem = lbHotspot.Items.FindByText(lbListOfHotspot.Items(intI).Text)
                    If tmpliItem Is Nothing Then
                        lbHotspot.Items.Add(lbListOfHotspot.Items(intI))
                    End If
                Next
            End If
            If lbHotspot.Items.Count > 0 Then
                btnGo.Enabled = True
            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(27, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=27&eu=" & "")
            Else
                erlErrorReport.ReportServerError(27, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=27&eu=" & Session("SanShiUserName"))

            End If

        End Try


    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim arrintSelected As Integer()
        Dim intI As Integer
        Dim liststrName As List(Of String)
        Dim strTmp As String

        Try

            liststrName = New List(Of String)
            arrintSelected = lbHotspot.GetSelectedIndices()

            If arrintSelected.Count > 0 Then
                For Each intI In arrintSelected
                    liststrName.Add(lbHotspot.Items(intI).Text)
                Next
            End If


            If arrintSelected.Count > 0 Then
                For Each strTmp In liststrName
                    lbHotspot.Items.Remove(strTmp)
                Next
            End If
            If lbHotspot.Items.Count = 0 Then
                btnGo.Enabled = False
            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(27, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=27&eu=" & "")
            Else
                erlErrorReport.ReportServerError(27, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=27&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim bwGetEnterWorker As BackgroundWorker


        Try

            If Application(Session("SanShiUserName") & "bwDownGCellListByHS") Is Nothing Then
                If lbHotspot.GetSelectedIndices.Count > 0 Then

                    btnGo.Enabled = False



                    plUpdating.Visible = True
                    plDownload.Visible = False
                    timerDelay.Enabled = True
                    lblLoading.Text = "正在处理，请稍后..."
                    bwGetEnterWorker = New BackgroundWorker
                    AddHandler bwGetEnterWorker.DoWork, AddressOf bwGetEnterWorker_DoWork
                    bwGetEnterWorker.RunWorker(Nothing)
                    Application(Session("SanShiUserName") & "bwDownGCellListByHS") = bwGetEnterWorker
                End If

            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(27, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=27&eu=" & "")
            Else
                erlErrorReport.ReportServerError(27, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=27&eu=" & Session("SanShiUserName"))

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
        Dim strSaveFileName As String
        Dim arrintSelected As Integer()
        Dim liststrName As List(Of String)
        Dim intI As Integer
        Dim strtmpGetHotSpot As String


        strSaveFileName = ""

        Try
            arrintSelected = lbHotspot.GetSelectedIndices()

            liststrName = New List(Of String)
            strtmpGetHotSpot = ""
            If arrintSelected.Count > 0 Then
                For Each intI In arrintSelected
                    If strtmpGetHotSpot = "" Then
                        strtmpGetHotSpot = " 区域名称 LIKE '" & lbHotspot.Items(intI).Text & "' "
                    Else
                        strtmpGetHotSpot += " or 区域名称 LIKE '" & lbHotspot.Items(intI).Text & "' "
                    End If
                Next
            End If


            strSaveFileName = Now.ToString("yyyyMMddHHmmss") & "-按热点名字下载保障小区列表-" & Session("SanShiUserName") & ".xlsx"

            strFilePath = Server.MapPath("/TmpFiles/") & strSaveFileName
            dsAllCellList = RenewGCellListLibrary.DownloadCellsByHotspotName(strtmpGetHotSpot)
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
            If Application(Session("SanShiUserName") & "bwDownGCellListByHS") IsNot Nothing Then
                globalWorker = DirectCast(Application(Session("SanShiUserName") & "bwDownGCellListByHS"), BackgroundWorker)
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
                Application(Session("SanShiUserName") & "bwDownGCellListByHS") = Nothing

            End If

        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(27, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=27&eu=" & "")
            Else
                erlErrorReport.ReportServerError(27, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=27&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub




End Class
