Imports System.Data

Partial Class BSDetails_InsertQueueOfBSCPara
    Inherits System.Web.UI.Page
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim bsdlCommonLibrary As BaseSationDetailsLibrary = New BaseSationDetailsLibrary
    Dim bscpCommonLibrary As BSCPara = New BSCPara
    Dim gsmcCommonLibrary As GSMCellPara = New GSMCellPara
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary

    Private Sub BSDetails_InsertQueueOfBSCPara_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim bolIsPowerEnough As Boolean = False
        Try
            txtLogMessage.CssClass = "form-control  form-control"
            btnGoInsert.CssClass = "btn btn-warning"
            If Application("bwQueueOfBSCInsert") IsNot Nothing Then
                timerLoading.Enabled = True
                plYeahGo.Visible = True
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
                erlErrorReport.ReportServerError(13, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=13&eu=" & "")
            Else
                erlErrorReport.ReportServerError(13, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=13&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub

    Private Sub BindConfigData()

        Dim dtBaseSationDetailsMana As DataTable
        Dim drEveryBaseSationDetailsConfig As DataRow

        Try

            dtBaseSationDetailsMana = bsdlCommonLibrary.ReturnBaseSationDetailsMan()

            For Each drEveryBaseSationDetailsConfig In dtBaseSationDetailsMana.Rows
                cblWhichBaseSationDetailsToInsert.Items.Add(drEveryBaseSationDetailsConfig.Item("ConfigName"))
            Next

            cblWhichBaseSationDetailsToInsert.DataBind()
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(13, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=13&eu=" & "")
            Else
                erlErrorReport.ReportServerError(13, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=13&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub


    Private Sub btnGoInsert_Click(sender As Object, e As EventArgs) Handles btnGoInsert.Click
        Dim cblitemCheckChecked As ListItem
        Dim listbsdipBSInsertPara As List(Of BaseSationDetailsInsertParaClass)
        listbsdipBSInsertPara = New List(Of BaseSationDetailsInsertParaClass)
        Dim bsdipOneOfPara As BaseSationDetailsInsertParaClass
        Dim dtBaseSationDetailsMana As DataTable
        Dim drEveryBaseSationDetailsConfig As DataRow
        Dim dtCellParaDetailsMana As DataTable
        Dim strBSCParaUpDatePath As String
        Dim strBSCParaUpdateSource As String
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
        Dim strtmpFileName As String
        Dim dateWhatNow As Date
        Dim bwGetEnterWorker As BackgroundWorker
        Dim arrobjParaOfBGWorker As Object()
        Dim arrobjParaOfBSCPara As Object()
        Dim bolDataBaseSation As Boolean
        Dim bolBSCPara As Boolean


        Try


            bwGetEnterWorker = New BackgroundWorker

            dtBaseSationDetailsMana = bsdlCommonLibrary.ReturnBaseSationDetailsMan()
            bolDataBaseSation = False
            bolBSCPara = False
            btnGoInsert.Enabled = False




            For Each cblitemCheckChecked In cblWhichBaseSationDetailsToInsert.Items
                If cblitemCheckChecked.Selected Then

                    txtLogMessage.Text += "已选 " & cblitemCheckChecked.Text & " 入数" & vbCrLf
                    For Each drEveryBaseSationDetailsConfig In dtBaseSationDetailsMana.Rows
                        If cblitemCheckChecked.Text = drEveryBaseSationDetailsConfig.Item("ConfigName") Then
                            bsdipOneOfPara = New BaseSationDetailsInsertParaClass
                            bsdipOneOfPara.strConfigName = drEveryBaseSationDetailsConfig.Item("ConfigName")
                            bsdipOneOfPara.strDataTableName = drEveryBaseSationDetailsConfig.Item("DataTableName")
                            bsdipOneOfPara.strUpDatePath = drEveryBaseSationDetailsConfig.Item("UpDatePath")
                            bsdipOneOfPara.strUpDateSource = drEveryBaseSationDetailsConfig.Item("UpDateSource")
                            bsdipOneOfPara.strFileSuffix = drEveryBaseSationDetailsConfig.Item("FileSuffix")
                            bsdipOneOfPara.strIFExcelThenSheetName = drEveryBaseSationDetailsConfig.Item("IFExcelThenSheetName")
                            bsdipOneOfPara.intMultiFile = drEveryBaseSationDetailsConfig.Item("MultiFile")
                            bsdipOneOfPara.strDataTableID = drEveryBaseSationDetailsConfig.Item("DataTableID")
                            listbsdipBSInsertPara.Add(bsdipOneOfPara)
                            bolDataBaseSation = True
                            Exit For
                        End If
                    Next

                End If

            Next
            bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ",", txtLogMessage)
            bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("列队插入数据库--基站信息表的选择完毕", "", txtLogMessage)
            bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ",", txtLogMessage)



            If cblWhichBSCParaToInsert.Items(0).Selected Or cblWhichBSCParaToInsert.Items(1).Selected Then
                bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("列队插入数据库--有选择将例行P文件入到数据库", "", txtLogMessage)
                bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ",", txtLogMessage)
                dtCellParaDetailsMana = bsdlCommonLibrary.GetParameterConfig("GSM Daily Para")
                strBSCParaUpDatePath = dtCellParaDetailsMana.Rows(0).Item("UpDatePath").ToString
                strBSCParaUpdateSource = dtCellParaDetailsMana.Rows(0).Item("UpdateSourceName").ToString

                intJ = strBSCParaUpdateSource.IndexOf("*")
                intK = strBSCParaUpdateSource.IndexOf("%")
                If intJ <> -1 And intK <> -1 Then
                    intI = CommonLibrary.GetMinNumber(intJ, intK)
                    strHeadOfSource = strBSCParaUpdateSource.Substring(0, intI)
                ElseIf intJ = -1 And intK = -1 Then
                    strHeadOfSource = ""
                ElseIf intJ = -1 Then
                    strHeadOfSource = strBSCParaUpdateSource.Substring(0, intK)
                Else
                    strHeadOfSource = strBSCParaUpdateSource.Substring(0, intJ)
                End If
                If System.IO.Directory.Exists(strBSCParaUpDatePath) Then
                    strtmpListDir = (From T In IO.Directory.GetFiles(strBSCParaUpDatePath, strHeadOfSource & "*.mdb", IO.SearchOption.AllDirectories)).ToList
                End If
                bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("列队插入数据库--查找完文件了", "", txtLogMessage)
                bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ",", txtLogMessage)
                intWhereYear = strHeadOfSource.IndexOf("%yyyy")
                intWhereMonth = strHeadOfSource.IndexOf("%mm") - 1
                intWhereDay = strHeadOfSource.IndexOf("%dd") - 2
                intWhereHour = strHeadOfSource.IndexOf("%hh") - 3
                intWhereMin = strHeadOfSource.IndexOf("%MM") - 4
                intWhereSec = strHeadOfSource.IndexOf("%ss") - 5
                strDir = CommonLibrary.GetMaxDateFile(strtmpListDir, intWhereYear, intWhereMonth, intWhereDay, intWhereHour, intWhereMin, intWhereSec)

                If strDir.Count > 0 Then
                    strtmpFileName = IO.Path.GetFileName(strDir(0))
                    dateWhatNow = New Date(strtmpFileName.Substring(intWhereYear, 4), strtmpFileName.Substring(intWhereMonth, 2), strtmpFileName.Substring(intWhereDay, 2), strtmpFileName.Substring(intWhereHour, 2), strtmpFileName.Substring(intWhereMin, 2), strtmpFileName.Substring(intWhereSec, 2))
                    arrobjParaOfBSCPara = {strDir(0), dateWhatNow, Server.MapPath("/EarthlyBranch/BSDetails/Config/BSCParaConfig.json"), Server.MapPath("/EarthlyBranch/BSDetails/Config/GSMCellParaConfig.json"), cblWhichBSCParaToInsert.Items(0).Selected, cblWhichBSCParaToInsert.Items(1).Selected}
                    bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("列队插入数据库--找到例行P文件了，最新的文件日期是: " & dateWhatNow.ToString, "", txtLogMessage)
                    bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ",", txtLogMessage)
                    '-----------------要先判断是否有入数过程在进行
                    bolBSCPara = True

                Else
                    bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("列队插入数据库--找不到例行P数文件哟: ", "", txtLogMessage)
                    bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ",", txtLogMessage)
                End If
            End If
            bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ",", txtLogMessage)
            bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("列队插入数据库--例行P的入数选择完毕", "", txtLogMessage)
            bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ",", txtLogMessage)




            If bolDataBaseSation And Not bolBSCPara Then
                arrobjParaOfBGWorker = {listbsdipBSInsertPara, Nothing}
            ElseIf Not bolDataBaseSation And bolBSCPara Then
                arrobjParaOfBGWorker = {Nothing, arrobjParaOfBSCPara}
            Else
                arrobjParaOfBGWorker = {listbsdipBSInsertPara, arrobjParaOfBSCPara}
            End If

            AddHandler bwGetEnterWorker.DoWork, AddressOf bwGetEnterWorker_DoWork
            bwGetEnterWorker.RunWorker(arrobjParaOfBGWorker)
            Application("bwQueueOfBSCInsert") = bwGetEnterWorker
            Application("intQueueOfBSCInsertProgress") = 0
            timerLoading.Enabled = True
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(13, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=13&eu=" & "")
            Else
                erlErrorReport.ReportServerError(13, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=13&eu=" & Session("SanShiUserName"))

            End If

        End Try
    End Sub



    ''' <summary>
    ''' This operation will work without the end.
    ''' </summary>
    Private Sub bwGetEnterWorker_DoWork(ByRef progress As Integer, ByRef _result As Object, ByRef OutputTheTmp As String, ByVal ParamArray arguments As Object())
        Dim listbsdipBSInsertPara As List(Of BaseSationDetailsInsertParaClass)
        Dim bsdipOneOfPara As BaseSationDetailsInsertParaClass
        Dim arrobjParaOfBSCPara As Object()
        _result = New Object
        Try
            If arguments(0) IsNot Nothing Then
                listbsdipBSInsertPara = arguments(0)
                For Each bsdipOneOfPara In listbsdipBSInsertPara
                    _result = bsdlCommonLibrary.BulkCopyToSQLServer(bsdipOneOfPara.strDataTableName, bsdipOneOfPara.strUpDatePath, bsdipOneOfPara.strFileSuffix, bsdipOneOfPara.strIFExcelThenSheetName, Convert.ToInt32(bsdipOneOfPara.intMultiFile), bsdipOneOfPara.strUpDateSource, Convert.ToInt32(bsdipOneOfPara.strDataTableID))
                    If _result = 0 Then
                        OutputTheTmp += bsdipOneOfPara.strConfigName & " 找不到数据" & vbCrLf
                    ElseIf _result = 88 Then
                        OutputTheTmp += bsdipOneOfPara.strConfigName & " 已经完成入数了" & vbCrLf
                    ElseIf _result = -44 Then
                        OutputTheTmp += bsdipOneOfPara.strConfigName & " 入数有问题！！" & vbCrLf
                    Else
                        OutputTheTmp += "入数有问题！！问题是: " & _result & vbCrLf
                    End If
                    progress += 1
                    If progress >= 99 Then
                        progress = 0
                    End If
                Next
            End If
            If arguments(1) IsNot Nothing Then
                arrobjParaOfBSCPara = arguments(1)
                If arrobjParaOfBSCPara(4) Then
                    _result = bscpCommonLibrary.HandelDailyAccessBSCPara(arrobjParaOfBSCPara(0), "dt_GSMP_BSC_Daily", arrobjParaOfBSCPara(1), arrobjParaOfBSCPara(2))
                    If _result = 88 Then
                        OutputTheTmp += "例行P BSC级数据 已经完成入数了" & vbCrLf
                    ElseIf _result = -44 Then
                        OutputTheTmp += "例行P BSC级数据 入数有问题！！" & vbCrLf
                    Else
                        OutputTheTmp += "入数有问题！！问题是: " & _result.ToString & vbCrLf
                    End If

                    progress += 1
                    If progress >= 99 Then
                        progress = 0
                    End If
                End If
                If arrobjParaOfBSCPara(5) Then
                    _result = gsmcCommonLibrary.HandelDailyAccessGSMCellPara(arrobjParaOfBSCPara(0), "dt_GSMP_Cell_Daily", arrobjParaOfBSCPara(1), arrobjParaOfBSCPara(3), "SELECT [CELL],[ID] FROM [SanShi_BaseSationDetails].[dbo].[dt_GSM_ID]")

                    If _result = 88 Then
                        OutputTheTmp += "例行P Cell级数据 已经完成入数了" & vbCrLf
                    ElseIf _result = -44 Then
                        OutputTheTmp += "例行P Cell级数据 入数有问题！！" & vbCrLf
                    Else
                        OutputTheTmp += "入数有问题！！问题是: " & _result.ToString & vbCrLf
                    End If


                    progress += 1
                    If progress >= 99 Then
                        progress = 0
                    End If

                End If
            End If
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

        Try


            If Application("bwQueueOfBSCInsert") IsNot Nothing Then
                globalWorker = DirectCast(Application("bwQueueOfBSCInsert"), BackgroundWorker)
            Else
                plYeahGo.Visible = False
                timerLoading.Enabled = False

            End If
            If lblLoading.Text.Length > 40 Then
                lblLoading.Text = lblLoading.Text.Substring(0, 10)
            End If
            lblLoading.Text = lblLoading.Text & "."
            If Application("intQueueOfBSCInsertProgress") IsNot Nothing Then
                intglobalProgress = DirectCast(Application("intQueueOfBSCInsertProgress"), Integer)

            End If
            If globalWorker.Result IsNot Nothing Then
                If globalWorker.Progress < 100 Then
                    If intglobalProgress <> globalWorker.Progress Then
                        bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation(globalWorker.OutPut, "", txtLogMessage)

                        globalWorker.OutPut = ""
                        Application("intQueueOfBSCInsertProgress") = globalWorker.Progress
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
                        bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("在例行P数据处理时出错，错误是：" & globalWorker.Result, "", txtLogMessage)
                        bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ".", txtLogMessage)

                    End If

                    bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("已经对所有入数的过程过了一遍了~", "", txtLogMessage)
                    bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ".", txtLogMessage)



                    plYeahGo.Visible = False
                    timerLoading.Enabled = False
                    btnGoInsert.Enabled = True

                    globalWorker = Nothing
                    Application("bwQueueOfBSCInsert") = Nothing
                    Application("intQueueOfBSCInsertProgress") = Nothing

                End If
            End If


        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(13, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=13&eu=" & "")
            Else
                erlErrorReport.ReportServerError(13, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=13&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub
End Class
