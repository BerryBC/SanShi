Imports System.Data

Partial Class BSDetails_GSMBSCData
    Inherits System.Web.UI.Page
    Dim bsdlCommonLibrary As BaseSationDetailsLibrary = New BaseSationDetailsLibrary
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim bscpCommonLibrary As BSCPara = New BSCPara
    Dim gsmcCommonLibrary As GSMCellPara = New GSMCellPara
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary

    Private Sub BSDetails_GSMBSCData_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim bolIsPowerEnough As Boolean = False

        Try
            btnWantModify.CssClass = "btn btn-info"
            btnConfirmModify.CssClass = "btn btn-success"
            txtLogMessage.CssClass = "form-control"
            btnGoInsert.CssClass = "btn btn-warning"
            bolIsPowerEnough = ucUserManage.CheckPower(Session, 9, Response)

            If Not IsPostBack Then
                If bolIsPowerEnough Then BindConfigData()
                If Application("bwBSParaInsert") IsNot Nothing Then
                    timerLoading.Enabled = True
                    plYeahGo.Visible = True
                    btnWantModify.Enabled = False
                    btnConfirmModify.Enabled = False
                    btnGoInsert.Enabled = False
                    bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("例行P--已经有入数过程在运行了，请稍等。", "", txtLogMessage)
                    bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ".", txtLogMessage)
                End If
            End If

        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(12, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=12&eu=" & "")
            Else
                erlErrorReport.ReportServerError(12, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=12&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub

    Private Sub BindConfigData()

        Dim dtCellParaDetailsMana As DataTable

        Try

            dtCellParaDetailsMana = bsdlCommonLibrary.GetParameterConfig("GSM Daily Para")
            txtLastUpdateTime.Text = dtCellParaDetailsMana.Rows(0).Item("LastUpdateTime").ToString
            txtUpDatePath.Text = dtCellParaDetailsMana.Rows(0).Item("UpDatePath").ToString
            txtUpdateSource.Text = dtCellParaDetailsMana.Rows(0).Item("UpdateSourceName").ToString
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(12, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=12&eu=" & "")
            Else
                erlErrorReport.ReportServerError(12, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=12&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub

    Private Sub btnConfirmModify_Click(sender As Object, e As EventArgs) Handles btnConfirmModify.Click
        Dim intResult As Integer
        btnWantModify.Enabled = True
        btnConfirmModify.Enabled = False
        btnGoInsert.Enabled = True
        txtUpDatePath.Enabled = False
        txtUpdateSource.Enabled = False

        Try

            intResult = bsdlCommonLibrary.ModifyParaConfig("GSM Daily Para", txtUpDatePath.Text, txtUpdateSource.Text)

            If intResult = 88 Then
                txtLogMessage.Text += "修改配置成功！" & vbCrLf

            ElseIf intResult = -44 Then
                txtLogMessage.Text += "修改配置失败....T_T。" & vbCrLf
            End If

            BindConfigData()
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(12, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=12&eu=" & "")
            Else
                erlErrorReport.ReportServerError(12, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=12&eu=" & Session("SanShiUserName"))

            End If

        End Try
    End Sub

    Private Sub btnGoInsert_Click(sender As Object, e As EventArgs) Handles btnGoInsert.Click
        Dim strtmpListDir As New List(Of String)
        Dim intI As Integer
        Dim intJ As Integer
        Dim intK As Integer
        Dim strHeadOfSource As String
        Dim intWhereYear As Integer
        Dim intWhereMonth As Integer
        Dim intWhereDay As Integer
        Dim intWhereHour As Integer
        Dim intWhereMin As Integer
        Dim intWhereSec As Integer
        Dim strDir As New List(Of String)
        Dim dateWhatNow As Date
        Dim strtmpFileName As String
        Dim bwGetEnterWorker As BackgroundWorker
        Dim arrobjParaOfBGWorker As Object()

        Try

            intJ = txtUpdateSource.Text.IndexOf("*")
            intK = txtUpdateSource.Text.IndexOf("%")
            If intJ <> -1 And intK <> -1 Then
                intI = CommonLibrary.GetMinNumber(intJ, intK)
                strHeadOfSource = txtUpdateSource.Text.Substring(0, intI)
            ElseIf intJ = -1 And intK = -1 Then
                strHeadOfSource = ""
            ElseIf intJ = -1 Then
                strHeadOfSource = txtUpdateSource.Text.Substring(0, intK)
            Else
                strHeadOfSource = txtUpdateSource.Text.Substring(0, intJ)
            End If



            btnWantModify.Enabled = False
            btnConfirmModify.Enabled = False
            btnGoInsert.Enabled = False

            If System.IO.Directory.Exists(txtUpDatePath.Text) Then
                strtmpListDir = (From T In IO.Directory.GetFiles(txtUpDatePath.Text, strHeadOfSource & "*.mdb", IO.SearchOption.AllDirectories)).ToList
            End If
            bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("例行P--查找完文件了。", "", txtLogMessage)
            bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ",", txtLogMessage)

            intWhereYear = txtUpdateSource.Text.IndexOf("%yyyy")
            intWhereMonth = txtUpdateSource.Text.IndexOf("%mm") - 1
            intWhereDay = txtUpdateSource.Text.IndexOf("%dd") - 2
            intWhereHour = txtUpdateSource.Text.IndexOf("%hh") - 3
            intWhereMin = txtUpdateSource.Text.IndexOf("%MM") - 4
            intWhereSec = txtUpdateSource.Text.IndexOf("%ss") - 5
            strDir = CommonLibrary.GetMaxDateFile(strtmpListDir, intWhereYear, intWhereMonth, intWhereDay, intWhereHour, intWhereMin, intWhereSec)
            If strDir.Count > 0 Then
                strtmpFileName = IO.Path.GetFileName(strDir(0))
                dateWhatNow = New Date(strtmpFileName.Substring(intWhereYear, 4), strtmpFileName.Substring(intWhereMonth, 2), strtmpFileName.Substring(intWhereDay, 2), strtmpFileName.Substring(intWhereHour, 2), strtmpFileName.Substring(intWhereMin, 2), strtmpFileName.Substring(intWhereSec, 2))
                bwGetEnterWorker = New BackgroundWorker
                AddHandler bwGetEnterWorker.DoWork, AddressOf bwGetEnterWorker_DoWork
                arrobjParaOfBGWorker = {strDir(0), dateWhatNow, Server.MapPath("/EarthlyBranch/BSDetails/Config/BSCParaConfig.json"), Server.MapPath("/EarthlyBranch/BSDetails/Config/GSMCellParaConfig.json")}
                bwGetEnterWorker.RunWorker(arrobjParaOfBGWorker)
                Application("bwBSParaInsert") = bwGetEnterWorker

                timerLoading.Enabled = True
                plYeahGo.Visible = True
                bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("例行P--开始入数了。", "", txtLogMessage)
                bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ",", txtLogMessage)

                '-----------------要先判断是否有入数过程在进行


            Else
                bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("例行P--找不到例行P数文件哟。", "", txtLogMessage)
                bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ".", txtLogMessage)
                btnWantModify.Enabled = True
                btnConfirmModify.Enabled = False
                btnGoInsert.Enabled = True



            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(12, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=12&eu=" & "")
            Else
                erlErrorReport.ReportServerError(12, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=12&eu=" & Session("SanShiUserName"))

            End If

        End Try
    End Sub

    Private Sub btnWantModify_Click(sender As Object, e As EventArgs) Handles btnWantModify.Click
        btnWantModify.Enabled = False
        btnConfirmModify.Enabled = True
        btnGoInsert.Enabled = False
        txtUpDatePath.Enabled = True
        txtUpdateSource.Enabled = True

    End Sub


    ''' <summary>
    ''' This operation will work without the end.
    ''' </summary>
    Private Sub bwGetEnterWorker_DoWork(ByRef progress As Integer, ByRef _result As Object, ByRef OutputTheTmp As String, ByVal ParamArray arguments As Object())
        Dim intBSCResult As Integer
        Dim intCellResult As Integer
        Try

            intBSCResult = bscpCommonLibrary.HandelDailyAccessBSCPara(arguments(0), "dt_GSMP_BSC_Daily", arguments(1), arguments(2))
            intCellResult = gsmcCommonLibrary.HandelDailyAccessGSMCellPara(arguments(0), "dt_GSMP_Cell_Daily", arguments(1), arguments(3), "SELECT [CELL],[ID] FROM [SanShi_BaseSationDetails].[dbo].[dt_GSM_ID]")
            bsdlCommonLibrary.UpdateParaDate("GSM Daily Para", arguments(1))
            If intBSCResult < 0 Or intCellResult < 0 Then
                _result = -44
            Else
                _result = 88
            End If
        Catch ex As Exception
            _result = ex.Message
        End Try

    End Sub

    Private Sub timerLoading_Tick(sender As Object, e As EventArgs) Handles timerLoading.Tick
        Dim globalWorker As BackgroundWorker
        Dim intReuslt As Integer
        Try


            globalWorker = Nothing
            If Application("bwBSParaInsert") IsNot Nothing Then
                globalWorker = DirectCast(Application("bwBSParaInsert"), BackgroundWorker)
            Else


                plYeahGo.Visible = False
                timerLoading.Enabled = False

            End If


            If lblLoading.Text.Length > 40 Then
                lblLoading.Text = lblLoading.Text.Substring(0, 10)
            End If
            lblLoading.Text = lblLoading.Text & "."
            If globalWorker.Result IsNot Nothing Then

                If IsNumeric(globalWorker.Result) Then
                    intReuslt = Convert.ToInt32(globalWorker.Result)
                    If intReuslt = 0 Then
                        bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("例行P--找不到例行P数文件哟。", "", txtLogMessage)
                        bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ".", txtLogMessage)

                    ElseIf intReuslt = -44 Then
                        bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("例行P--入数失败了哟~", "", txtLogMessage)
                        bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ".", txtLogMessage)
                    ElseIf intReuslt = 88 Then
                        bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("例行P--成功了哟~", "", txtLogMessage)
                        bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ".", txtLogMessage)
                    End If

                Else
                    bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("在例行P数据处理时出错，错误是：" & globalWorker.Result, "", txtLogMessage)
                    bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ".", txtLogMessage)
                End If

                txtLogMessage.Text += "已经完成了例行P的处理了哦~" & vbCrLf
                bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("已经完成了例行P的处理了哦~", "", txtLogMessage)
                bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ".", txtLogMessage)


                plYeahGo.Visible = False
                timerLoading.Enabled = False
                btnGoInsert.Enabled = True
                btnWantModify.Enabled = True
                globalWorker = Nothing
                Application("bwBSParaInsert") = Nothing

            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(12, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=12&eu=" & "")
            Else
                erlErrorReport.ReportServerError(12, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=12&eu=" & Session("SanShiUserName"))

            End If

        End Try


    End Sub






End Class
