Imports System.IO
Imports GSMIndexOfCellLibrary
Imports ExcelLibrary
Imports System.Data
Imports System.Data.SqlClient
Imports SQLServerLibrary


Partial Class TrafficStatistics_GSMIndexOfCell
    Inherits System.Web.UI.Page
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim gsmioclLibrary As GSMIndexOfCellLibrary = New GSMIndexOfCellLibrary
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary
    Dim bsdlCommonLibrary As BaseSationDetailsLibrary = New BaseSationDetailsLibrary

    Private Sub TrafficStatistics_GSMIndexOfCell_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim bolIsPowerEnough As Boolean = False

        Try


            btnWantModify.CssClass = "btn btn-info"
            btnConfirmModify.CssClass = "btn btn-success"
            txtLogMessage.CssClass = "form-control"
            btnGoInsert.CssClass = "btn btn-warning"


            If Application("bwGSMIndexOfCell") IsNot Nothing Then
                timerLoading.Enabled = True
                plYeahGo.Visible = True
                btnWantModify.Enabled = False
                btnGoInsert.Enabled = False
            Else
                plYeahGo.Visible = False
                timerLoading.Enabled = False

            End If



            If Not IsPostBack Then
                bolIsPowerEnough = ucUserManage.CheckPower(Session, 9, Response)
                BindConfigData()
            End If

        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(14, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=14&eu=" & "")
            Else
                erlErrorReport.ReportServerError(14, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=14&eu=" & Session("SanShiUserName"))

            End If

        End Try


    End Sub


    Private Sub BindConfigData()
        Dim strJsonLoad As String
        Dim gsmiccInsertToDataBase As GSMIndexOfCellConfig


        Try



            If (System.IO.File.Exists(Server.MapPath("/EarthlyBranch/TrafficStatistics/Config/GSMIndexOfCell.json"))) Then
                strJsonLoad = File.ReadAllText(Server.MapPath("/EarthlyBranch/TrafficStatistics/Config/GSMIndexOfCell.json"))
                gsmiccInsertToDataBase = SimpleJson.SimpleJson.DeserializeObject(Of GSMIndexOfCellConfig)(strJsonLoad)
                txtUpDatePath.Text = gsmiccInsertToDataBase.strUpdatePath
                txtUpdateSource.Text = gsmiccInsertToDataBase.strFileName
                txtLastUpdateTime.Text = gsmioclLibrary.GetGSMIndexMaxDate.ToString
            Else
                txtUpDatePath.Text = ""
                txtUpdateSource.Text = ""

            End If

        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(14, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=14&eu=" & "")
            Else
                erlErrorReport.ReportServerError(14, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=14&eu=" & Session("SanShiUserName"))

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

    Private Sub btnConfirmModify_Click(sender As Object, e As EventArgs) Handles btnConfirmModify.Click
        Dim strPathConfig As String
        Dim swSaveStream As StreamWriter
        Dim strSaveJson As String
        Dim gsmiccInsertToDataBase As GSMIndexOfCellConfig
        gsmiccInsertToDataBase = New GSMIndexOfCellConfig

        Try

            If Not System.IO.Directory.Exists(Server.MapPath("/EarthlyBranch/TrafficStatistics/Config/")) Then
                My.Computer.FileSystem.CreateDirectory(Server.MapPath("/EarthlyBranch/TrafficStatistics/Config/"))
            End If


            gsmiccInsertToDataBase.strFileName = txtUpdateSource.Text
            gsmiccInsertToDataBase.strUpdatePath = txtUpDatePath.Text

            strPathConfig = Server.MapPath("/EarthlyBranch/TrafficStatistics/Config/GSMIndexOfCell.json")
            swSaveStream = New StreamWriter(File.Open(strPathConfig, FileMode.Create))
            strSaveJson = SimpleJson.SimpleJson.SerializeObject(gsmiccInsertToDataBase)
            swSaveStream.Write(strSaveJson)
            swSaveStream.Flush()
            swSaveStream.Close()
            swSaveStream = Nothing

            btnWantModify.Enabled = True
            btnConfirmModify.Enabled = False
            btnGoInsert.Enabled = True
            txtUpDatePath.Enabled = False
            txtUpdateSource.Enabled = False

        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(14, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=14&eu=" & "")
            Else
                erlErrorReport.ReportServerError(14, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=14&eu=" & Session("SanShiUserName"))

            End If

        End Try


    End Sub

    Private Sub btnGoInsert_Click(sender As Object, e As EventArgs) Handles btnGoInsert.Click
        Dim bwGetEnterWorker As BackgroundWorker
        Dim intI As Integer
        Dim intJ As Integer
        Dim intK As Integer
        Dim strHeadOfSource As String
        Dim strtmpListDir As New List(Of String)
        Dim intWhereYear As Integer
        Dim intWhereMonth As Integer
        Dim intWhereDay As Integer
        Dim strDir As New List(Of String)
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
                strtmpListDir = (From T In IO.Directory.GetFiles(txtUpDatePath.Text, strHeadOfSource & "*.xls", IO.SearchOption.AllDirectories)).ToList
            End If
            bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("GSM网格指标每日小区--查找完文件了", "", txtLogMessage)
            bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ",", txtLogMessage)



            intWhereYear = txtUpdateSource.Text.IndexOf("%yyyy")
            intWhereMonth = txtUpdateSource.Text.IndexOf("%mm") - 1
            intWhereDay = txtUpdateSource.Text.IndexOf("%dd") - 2


            strDir = CommonLibrary.GetAfterOneDateFile(strtmpListDir, intWhereYear, intWhereMonth, intWhereDay, gsmioclLibrary.GetGSMIndexMaxDate)





            If strDir.Count > 0 Then
                arrobjParaOfBGWorker = {strDir}
                bwGetEnterWorker = New BackgroundWorker

                AddHandler bwGetEnterWorker.DoWork, AddressOf bwGetEnterWorker_DoWork
                bwGetEnterWorker.RunWorker(arrobjParaOfBGWorker)
                Application("bwGSMIndexOfCell") = bwGetEnterWorker
                timerLoading.Enabled = True
                plYeahGo.Visible = True
                bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("GSM网格指标每日小区--开始入数了哦~", "", txtLogMessage)
                bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ",", txtLogMessage)


            Else
                bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("GSM网格指标每日小区--找不到网格小区指标的文件哟", "", txtLogMessage)
                bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ".", txtLogMessage)

                plYeahGo.Visible = False
                timerLoading.Enabled = False
                btnGoInsert.Enabled = True
                btnWantModify.Enabled = True




            End If


        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(14, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=14&eu=" & "")
            Else
                erlErrorReport.ReportServerError(14, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=14&eu=" & Session("SanShiUserName"))

            End If

        End Try


    End Sub



    ''' <summary>
    ''' This operation will work without the end.
    ''' </summary>
    Private Sub bwGetEnterWorker_DoWork(ByRef progress As Integer, ByRef _result As Object, ByRef OutputTheTmp As String, ByVal ParamArray arguments As Object())
        Dim strDir As New List(Of String)
        Dim strtmpDir As String
        Dim dtExl As DataTable
        Dim exlExl As LoadExcel
        Dim strIFExcelThenSheetName As String
        Dim dtData As DataTable
        Dim dtFormat As DataTable

        Try



            dtFormat = sqllSSLibrary.ReturnFormat("dt_GSM_Daily_Grib_Traffic", CommonLibrary.GetSQLServerConnect("ConnectionTrafficDB"))

            strDir = arguments(0)
            For Each strtmpDir In strDir


                exlExl = New LoadExcel(strtmpDir)
                exlExl.GetInformation()
                strIFExcelThenSheetName = exlExl.strSheets(0)

                dtExl = exlExl.GetData(strIFExcelThenSheetName)
                dtData = CommonLibrary.ReturnNewNormalDT(dtExl, dtFormat)
                sqllSSLibrary.BlukInsert("dt_GSM_Daily_Grib_Traffic", dtData, CommonLibrary.GetSQLServerConnect("ConnectionTrafficDB"))

                If exlExl.strSheets.Count > 0 Then
                    dtData.Dispose()
                    dtData = Nothing
                End If
                exlExl.Dispose()
                exlExl = Nothing

                progress += 1
                If progress >= 99 Then
                    progress = 0
                End If
                OutputTheTmp += "已完成文件 : " & IO.Path.GetFileName(strtmpDir) & "的入数" & vbCrLf

            Next
            _result = 88
        Catch ex As Exception
            _result = ex.Message
        End Try

    End Sub





    Private Sub timerLoading_Tick(sender As Object, e As EventArgs) Handles timerLoading.Tick
        Dim globalWorker As BackgroundWorker
        Dim intglobalProgress As Integer
        Dim intReuslt As Integer
        globalWorker = Nothing
        intglobalProgress = 0
        Try


            If Application("bwGSMIndexOfCell") IsNot Nothing Then
                globalWorker = DirectCast(Application("bwGSMIndexOfCell"), BackgroundWorker)
            Else
                plYeahGo.Visible = False
                timerLoading.Enabled = False
                Exit Sub
            End If
            If lblLoading.Text.Length > 40 Then
                lblLoading.Text = lblLoading.Text.Substring(0, 10)
            End If
            lblLoading.Text = lblLoading.Text & "."
            If Application("intGSMIndexOfCellProgress") IsNot Nothing Then
                intglobalProgress = DirectCast(Application("intGSMIndexOfCellProgress"), Integer)

            End If
            If globalWorker.Result IsNot Nothing Then
                If globalWorker.Progress < 100 Then
                    If intglobalProgress <> globalWorker.Progress Then
                        bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation(globalWorker.OutPut, "", txtLogMessage)
                        globalWorker.OutPut = ""
                        Application("intGSMIndexOfCellProgress") = globalWorker.Progress
                    End If

                Else
                    bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation(globalWorker.OutPut, "", txtLogMessage)
                    globalWorker.OutPut = ""

                    If IsNumeric(globalWorker.Result) Then
                        intReuslt = Convert.ToInt32(globalWorker.Result)
                        If intReuslt = -44 Then
                            bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("入数失败了哟~", "", txtLogMessage)
                            bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ".", txtLogMessage)
                        ElseIf intReuslt = 88 Then
                            bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("完成了", "", txtLogMessage)
                            bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ".", txtLogMessage)
                        End If

                    Else
                        bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("在网格每日指标数据处理时出错，错误是：" & globalWorker.Result, "", txtLogMessage)
                        bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ".", txtLogMessage)
                    End If

                    bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("已经对所有入数的过程过了一遍了~", "", txtLogMessage)
                    bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ".", txtLogMessage)



                    plYeahGo.Visible = False
                    timerLoading.Enabled = False
                    btnGoInsert.Enabled = True
                    btnWantModify.Enabled = True

                    globalWorker = Nothing
                    Application("bwGSMIndexOfCell") = Nothing
                    Application("intGSMIndexOfCellProgress") = Nothing

                End If
            End If


        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(14, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=14&eu=" & "")
            Else
                erlErrorReport.ReportServerError(14, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=14&eu=" & Session("SanShiUserName"))

            End If

        End Try


    End Sub





End Class
