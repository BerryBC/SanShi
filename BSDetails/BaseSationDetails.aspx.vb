Imports System.Data.SqlClient
Imports System.Data
Imports BaseSationDetailsLibrary
Imports System.ComponentModel
Imports System.Threading
Imports System
Imports System.Web
Imports System.Web.UI

Partial Class BSDetails_BaseSationDetails
    Inherits System.Web.UI.Page
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary
    Dim bsdlCommonLibrary As BaseSationDetailsLibrary = New BaseSationDetailsLibrary


    Private Sub BSDetails_BaseSationDetails_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dliItem As DataListItem
        Dim btnDatalistInside As Button
        Dim bolIsPowerEnough As Boolean = False

        btnWantModify.CssClass = "btn btn-info"
        btnConfirmModify.CssClass = "btn btn-success"
        txtLogMessage.CssClass = "form-control"
        btnGoInsert.CssClass = "btn btn-warning"
        btnCheckHowCount.CssClass = "btn btn-info"

        Try

            bolIsPowerEnough = ucUserManage.CheckPower(Session, 9, Response)
            If bolIsPowerEnough Then

                If Application("bwBSInsert") IsNot Nothing Then
                    plYeahGo.Visible = True
                    timerLoading.Enabled = True
                    btnWantModify.Enabled = False
                    btnCheckHowCount.Enabled = False
                    btnConfirmModify.Enabled = False
                    btnGoInsert.Enabled = False
                    btnAddOneConfig.Enabled = False
                    btnAddOneConfig.CssClass = "form-control btn-success"

                    For Each dliItem In dlBaseSationDetails.Items
                        btnDatalistInside = CType(dliItem.FindControl("btnGoModifyDataTableSet"), Button)
                        btnDatalistInside.Enabled = False
                        btnDatalistInside.CssClass = "form-control btn-warning"
                    Next

                End If



                If Not IsPostBack Then
                    BindConfigData()

                End If
            End If

        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(10, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=10&eu=" & "")
            Else
                erlErrorReport.ReportServerError(10, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=10&eu=" & Session("SanShiUserName"))

            End If

        End Try
    End Sub

    Private Sub BindConfigData()

        Dim dtBaseSationDetailsMana As DataTable

        Try

            dtBaseSationDetailsMana = bsdlCommonLibrary.ReturnBaseSationDetailsMan()
            dlBaseSationDetails.DataSource = dtBaseSationDetailsMana
            dlBaseSationDetails.DataBind()
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(10, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=10&eu=" & "")
            Else
                erlErrorReport.ReportServerError(10, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=10&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub

    Public Sub dlBaseSationDetails_ItemCommand(source As Object, e As DataListCommandEventArgs) Handles dlBaseSationDetails.ItemCommand
        Dim dtOneBaseSationDetailsConfig As DataTable

        If e.CommandName = "ModifyDataTableMana" Then
            dtOneBaseSationDetailsConfig = bsdlCommonLibrary.ReturnOneBaseSationDetailsMan(Convert.ToInt32(e.CommandArgument))
            txtDataTableName.Text = dtOneBaseSationDetailsConfig.Rows(0).Item("DataTableName").ToString
            txtFileSuffix.Text = dtOneBaseSationDetailsConfig.Rows(0).Item("FileSuffix").ToString
            txtIFExcelThenSheetName.Text = dtOneBaseSationDetailsConfig.Rows(0).Item("IFExcelThenSheetName").ToString
            txtLastUpdateTime.Text = dtOneBaseSationDetailsConfig.Rows(0).Item("LastUpdateTime").ToString
            txtMultiFile.Text = dtOneBaseSationDetailsConfig.Rows(0).Item("MultiFile").ToString
            txtUpDatePath.Text = dtOneBaseSationDetailsConfig.Rows(0).Item("UpDatePath").ToString
            txtConfigName.Text = dtOneBaseSationDetailsConfig.Rows(0).Item("ConfigName").ToString
            txtUpdateSource.Text = dtOneBaseSationDetailsConfig.Rows(0).Item("UpDateSource").ToString
            txtNumberOfConfig.Text = e.CommandArgument
            btnWantModify.Enabled = True
            btnCheckHowCount.Enabled = True
            btnGoInsert.Enabled = True
        End If
    End Sub

    Private Sub btnWantModify_Click(sender As Object, e As EventArgs) Handles btnWantModify.Click
        btnWantModify.Enabled = False
        btnCheckHowCount.Enabled = False
        btnConfirmModify.Enabled = True
        txtDataTableName.Enabled = True
        txtFileSuffix.Enabled = True
        txtIFExcelThenSheetName.Enabled = True
        txtMultiFile.Enabled = True
        txtUpDatePath.Enabled = True
        txtConfigName.Enabled = True
        txtUpdateSource.Enabled = True
        btnGoInsert.Enabled = False
    End Sub

    Private Sub btnConfirmModify_Click(sender As Object, e As EventArgs) Handles btnConfirmModify.Click
        Dim intResult As Integer
        Dim intMulti As Integer

        Try


            If Regex.IsMatch(txtMultiFile.Text, "^[1-9]\d*$") Then
                intMulti = Convert.ToInt32(txtMultiFile.Text)
            Else
                intMulti = 0
            End If
            btnWantModify.Enabled = True
            btnCheckHowCount.Enabled = True
            btnGoInsert.Enabled = False
            btnConfirmModify.Enabled = False
            txtDataTableName.Enabled = False
            txtFileSuffix.Enabled = False
            txtIFExcelThenSheetName.Enabled = False
            txtMultiFile.Enabled = False
            txtUpDatePath.Enabled = False
            txtConfigName.Enabled = False
            txtUpdateSource.Enabled = False
            btnGoInsert.Enabled = True
            txtMultiFile.Text = intMulti.ToString
            intResult = bsdlCommonLibrary.ModifyConfig(Convert.ToInt32(txtNumberOfConfig.Text), txtConfigName.Text, txtDataTableName.Text, txtUpDatePath.Text, txtFileSuffix.Text, txtIFExcelThenSheetName.Text, intMulti, txtUpdateSource.Text)
            BindConfigData()
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(10, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=10&eu=" & "")
            Else
                erlErrorReport.ReportServerError(10, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=10&eu=" & Session("SanShiUserName"))

            End If

        End Try
    End Sub

    Private Sub btnGoInsert_Click(sender As Object, e As EventArgs) Handles btnGoInsert.Click
        Dim bwGetEnterWorker As BackgroundWorker
        Dim dliItem As DataListItem
        Dim btnDatalistInside As Button



        plYeahGo.Visible = True
        timerLoading.Enabled = True
        btnWantModify.Enabled = False
        btnCheckHowCount.Enabled = False
        btnConfirmModify.Enabled = False
        btnGoInsert.Enabled = False
        btnAddOneConfig.Enabled = False
        btnAddOneConfig.CssClass = "form-control btn-success"

        For Each dliItem In dlBaseSationDetails.Items
            btnDatalistInside = CType(dliItem.FindControl("btnGoModifyDataTableSet"), Button)
            btnDatalistInside.Enabled = False
            btnDatalistInside.CssClass = "form-control btn-warning"
        Next


        Try


            If Application("bwBSInsert") IsNot Nothing Then
                bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("    已经有入数过程在运行了，请稍等", "", txtLogMessage)


            Else
                bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("    开始对配置：" & txtConfigName.Text & " 进行入数", "", txtLogMessage)

                bwGetEnterWorker = New BackgroundWorker
                AddHandler bwGetEnterWorker.DoWork, AddressOf bwGetEnterWorker_DoWork
                bwGetEnterWorker.RunWorker(Nothing)
                Application("bwBSInsert") = bwGetEnterWorker

            End If

        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(10, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=10&eu=" & "")
            Else
                erlErrorReport.ReportServerError(10, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=10&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub

    Private Sub timerLoading_Tick(sender As Object, e As EventArgs) Handles timerLoading.Tick
        Dim globalWorker As BackgroundWorker
        Dim intReuslt As Integer
        Try


            globalWorker = Nothing
            If Application("bwBSInsert") IsNot Nothing Then
                globalWorker = DirectCast(Application("bwBSInsert"), BackgroundWorker)
            Else
                Dim dliItem As DataListItem
                Dim btnDatalistInside As Button
                plYeahGo.Visible = False
                timerLoading.Enabled = False
                If txtNumberOfConfig.Text <> "" Then
                    btnWantModify.Enabled = True
                    btnCheckHowCount.Enabled = True
                    btnGoInsert.Enabled = True
                End If
                globalWorker = Nothing
                For Each dliItem In dlBaseSationDetails.Items
                    btnDatalistInside = CType(dliItem.FindControl("btnGoModifyDataTableSet"), Button)
                    btnDatalistInside.Enabled = True
                    btnDatalistInside.CssClass = "form-control btn-info"
                Next
                btnAddOneConfig.Enabled = True
                btnAddOneConfig.CssClass = "form-control btn-success"

                Exit Sub
            End If


            If lblLoading.Text.Length > 40 Then
                lblLoading.Text = lblLoading.Text.Substring(0, 10)
            End If
            lblLoading.Text = lblLoading.Text & "."
            If globalWorker.Result IsNot Nothing Then
                Dim dliItem As DataListItem
                Dim btnDatalistInside As Button

                If IsNumeric(globalWorker.Result) Then
                    intReuslt = Convert.ToInt32(globalWorker.Result)
                    If intReuslt = 0 Then
                        bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("    在配置：" & txtConfigName.Text & " 中找不到文件", "", txtLogMessage)

                    ElseIf intReuslt = -44 Then
                        bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("    在配置：" & txtConfigName.Text & " 中入数失败", "", txtLogMessage)
                    ElseIf intReuslt = 88 Then
                        bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("    配置：" & txtConfigName.Text & " 入数成功", "", txtLogMessage)
                    End If

                Else
                    bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("    配置：" & txtConfigName.Text & " 的入数过程出错，错误为：" & globalWorker.Result, "", txtLogMessage)

                End If
                bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("    完成配置：" & txtConfigName.Text & " 的进行入数过程运行", "", txtLogMessage)
                bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ".", txtLogMessage)

                plYeahGo.Visible = False
                timerLoading.Enabled = False
                btnWantModify.Enabled = True
                btnCheckHowCount.Enabled = True
                btnGoInsert.Enabled = True
                globalWorker = Nothing
                Application("bwBSInsert") = Nothing
                For Each dliItem In dlBaseSationDetails.Items
                    btnDatalistInside = CType(dliItem.FindControl("btnGoModifyDataTableSet"), Button)
                    btnDatalistInside.Enabled = True
                    btnDatalistInside.CssClass = "form-control btn-info"
                Next
                btnAddOneConfig.Enabled = True
                btnAddOneConfig.CssClass = "form-control btn-success"

            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(10, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=10&eu=" & "")
            Else
                erlErrorReport.ReportServerError(10, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=10&eu=" & Session("SanShiUserName"))

            End If

        End Try


    End Sub



    ''' <summary>
    ''' This operation will work without the end.
    ''' </summary>
    Private Sub bwGetEnterWorker_DoWork(ByRef progress As Integer, ByRef _result As Object, ByRef OutputTheTmp As String, ByVal ParamArray arguments As Object())
        Try

            _result = bsdlCommonLibrary.BulkCopyToSQLServer(txtDataTableName.Text, txtUpDatePath.Text, txtFileSuffix.Text, txtIFExcelThenSheetName.Text, Convert.ToInt32(txtMultiFile.Text), txtUpdateSource.Text, Convert.ToInt32(txtNumberOfConfig.Text))
        Catch ex As Exception
            _result = ex.Message
        End Try

    End Sub

    Private Sub btnAddOneConfig_Click(sender As Object, e As EventArgs) Handles btnAddOneConfig.Click

        Try

            bsdlCommonLibrary.AddConfig()
            BindConfigData()
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(10, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=10&eu=" & "")
            Else
                erlErrorReport.ReportServerError(10, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=10&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub

    Private Sub btnCheckHowCount_Click(sender As Object, e As EventArgs) Handles btnCheckHowCount.Click
        Dim intResultOfHowManyDataTableRows As Integer
        Try
            intResultOfHowManyDataTableRows = bsdlCommonLibrary.HowManyRowsOfDataTable(txtDataTableName.Text)

            bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("    配置：" & txtConfigName.Text & " 的目标表有数据 " & intResultOfHowManyDataTableRows.ToString & " 个 ", "", txtLogMessage)
            bsdlCommonLibrary.LogOnTextBoxAndDataBaseForBaseSation("", ".", txtLogMessage)


        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(10, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=10&eu=" & "")
            Else
                erlErrorReport.ReportServerError(10, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=10&eu=" & Session("SanShiUserName"))

            End If


        End Try
    End Sub
End Class
