Imports SQLServerLibrary
Imports System.Data.SqlClient
Imports System.Data
Imports ExcelLibrary
Imports CSVLibrary
Partial Class Preexistence_LoadIndexOfCell_LoadInterfereOfCell24Hour
    Inherits System.Web.UI.Page
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary

    Private Sub Preexistence_LoadIndexOfCell_LoadInterfereOfCell24Hour_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim strJumpJS As String

        Try

            If Not IsPostBack Then
                ucUserManage.CheckPower(Session, 3, Response)

                strJumpJS = "setTimeout(function (){ $("".txtDateFrom "").datepicker({dateFormat: ""yy-mm-dd"",onSelect: function (selectedDate) { $(""#datepicked"").empty().append(selectedDate); } });},100)"
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, Me.GetType, "ShowLoading", strJumpJS, True)

            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(34, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=34&eu=" & "")
            Else
                erlErrorReport.ReportServerError(34, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=34&eu=" & Session("SanShiUserName"))

            End If

        End Try
    End Sub


    Private Sub SaveIndexFile(strSQLS As String)
        Dim scmdCommand As SqlCommand
        Dim dtIndexOfGSMCell As DataTable
        Dim intBeford As Integer
        Dim bolSaveSuccess As Boolean
        Dim csvCSV As LoadCSV
        Dim strSaveFileName As String


        Try


            intBeford = 0

            scmdCommand = sqllSSLibrary.GetCommandStr(strSQLS, CommonLibrary.GetSQLServerConnect("ConnectionGanZhi"))
            scmdCommand.CommandTimeout = 500
            dtIndexOfGSMCell = sqllSSLibrary.GetSQLServerDataTable(scmdCommand)

            strSaveFileName = Now.ToString("yyyyMMddHHmmss") & "-全网干扰24小时数据-" & Session("SanShiUserName") & ".csv"

            csvCSV = New LoadCSV(Server.MapPath("/TmpFiles/") & strSaveFileName)
            bolSaveSuccess = csvCSV.SaveASNewOne(dtIndexOfGSMCell)
            hlDownloadLink.Text = Request.Url.Host & "/TmpFiles/" & strSaveFileName
            hlDownloadLink.NavigateUrl = "/TmpFiles/" & strSaveFileName

            'strJumpJS = "setTimeout(function () { document.location.href = '#divDisplayTheTip'}, 50);"
            'ScriptManager.RegisterClientScriptBlock(UpdatePanel1, Me.GetType, "ShowLoading", strJumpJS, True)


            plGoClickQuery.Visible = False
            plDownLoadAddress.Visible = True
            btnReQuer.Visible = True

            'btnRunQuery.Enabled = True

        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(34, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=34&eu=" & "")
            Else
                erlErrorReport.ReportServerError(34, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=34&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub

    Private Sub btnRunQuery_Click(sender As Object, e As EventArgs) Handles btnRunQuery.Click
        Dim strJumpJS As String

        Try


            If (Not IsDateString(txtBeginDate.Text) And Not IsDateString(txtBeginDate.Text)) Then
                lblWrongDate.Text = "日期格式错误，应为 ""2016-05-30""且范围是2015年至今的日期"
                plError.Visible = True
                plGoClickQuery.Visible = False
                plDownLoadAddress.Visible = False
                plForShowMessage.Visible = False
                btnRunQuery.Enabled = True
                strJumpJS = "setTimeout(function (){ $("".txtDateFrom "").datepicker({dateFormat: ""yy-mm-dd"",onSelect: function (selectedDate) { $(""#datepicked"").empty().append(selectedDate); } });},100)"
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, Me.GetType, "ShowLoading", strJumpJS, True)

                Exit Sub
            End If




            btnRunQuery.Enabled = False
            btnRunQuery.CssClass = "form-control btn-warning"


            lblGoClickQuery.Text = "正在处理，请耐心等候</br>"

            plDownLoadAddress.Visible = False
            plGoClickQuery.Visible = True
            plError.Visible = False
            plForShowMessage.Visible = False
            btnRunQuery.Enabled = False
            btnRunQuery.CssClass = "form-control btn-warning"
            strJumpJS = "setTimeout(function () { JSCodeShow();}, 100);"
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, Me.GetType, "ShowLoading", strJumpJS, True)

            timerGo.Enabled = True
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(34, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=34&eu=" & "")
            Else
                erlErrorReport.ReportServerError(34, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=34&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub


    Private Function IsDateString(strInString As String) As Boolean
        Dim strTestDateString As String

        strTestDateString = strInString.Substring(0, 4)
        If Not IsNumeric(strTestDateString) Then
            Return False
        End If
        If CType(strTestDateString, Integer) < 2000 Or CType(strTestDateString, Integer) > 2046 Then
            Return False
        End If
        strTestDateString = strInString.Substring(4, 1)
        If strTestDateString <> "-" And strTestDateString <> "/" Then
            Return False
        End If
        strTestDateString = strInString.Substring(5, 2)
        If Not IsNumeric(strTestDateString) Then
            Return False
        End If
        If CType(strTestDateString, Integer) < 1 Or CType(strTestDateString, Integer) > 12 Then
            Return False
        End If
        strTestDateString = strInString.Substring(7, 1)
        If strTestDateString <> "-" And strTestDateString <> "/" Then
            Return False
        End If
        strTestDateString = strInString.Substring(8, 2)
        If Not IsNumeric(strTestDateString) Then
            Return False
        End If
        If CType(strTestDateString, Integer) < 1 Or CType(strTestDateString, Integer) > 31 Then
            Return False
        End If
        Return True

    End Function

    Private Sub timerGo_Tick(sender As Object, e As EventArgs) Handles timerGo.Tick
        Dim strSQLSHandel As String
        Dim bolIsHaveWord As Boolean
        Dim strJumpJS As String


        Try


            timerGo.Enabled = False




            bolIsHaveWord = False

            btnRunQuery.Enabled = False
            btnRunQuery.CssClass = "form-control btn-warning"



            strSQLSHandel = "SELECT   _小区网格分区.网格九分区, _ID表.ID, _小区网格分区.基站名,  Data.[Moid(GSM_CELL)], Data.[Bsc(GSM_CELL)],  Data.[Datetime Id(GSM_CELL)], Data.TCH话务量, Data.半速率话务量,Data.[Idleutchf Itfusib1], Data.[Idleutchf Itfusib2],  Data.[Idleutchf Itfusib3], Data.[Idleutchf Itfusib4],  Data.[Idleutchf Itfusib5] FROM      Data INNER JOIN _ID表 ON _ID表.CELL = Data.[Moid(GSM_CELL)] INNER JOIN  _小区网格分区 ON _小区网格分区.ID = _ID表.ID WHERE   (_小区网格分区.网格九分区 LIKE '白云' OR  _小区网格分区.网格九分区 LIKE '花都') AND  "





            If (Not IsDateString(txtBeginDate.Text)) Then
                lblWrongDate.Text = "日期格式错误，应为 ""2016-05-30""且范围是2015年至今的日期"
                plError.Visible = True
                plGoClickQuery.Visible = False
                plDownLoadAddress.Visible = False
                plForShowMessage.Visible = False
                btnRunQuery.Enabled = True
                strJumpJS = "setTimeout(function (){ $("".txtDateFrom "").datepicker({dateFormat: ""yy-mm-dd"",onSelect: function (selectedDate) { $(""#datepicked"").empty().append(selectedDate); } });},100)"
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, Me.GetType, "ShowLoading", strJumpJS, True)

                Exit Sub
            End If

            strSQLSHandel += " ([Datetime Id(GSM_CELL)]>='" & txtBeginDate.Text & "' and [Datetime Id(GSM_CELL)]<dateadd(dd,1,'" & txtBeginDate.Text & "' ))"


            'strJumpJS = "setTimeout(function () { JSCodeShow();}, 100);"
            'ScriptManager.RegisterClientScriptBlock(UpdatePanel1, Me.GetType, "ShowLoading", strJumpJS, True)

            SaveIndexFile(strSQLSHandel)
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(34, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=34&eu=" & "")
            Else
                erlErrorReport.ReportServerError(34, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=34&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub

    Private Sub btnReQuer_Click(sender As Object, e As EventArgs) Handles btnReQuer.Click
        Dim strJumpJS As String

        btnRunQuery.Enabled = True
        plError.Visible = False
        plGoClickQuery.Visible = True
        plDownLoadAddress.Visible = False
        plForShowMessage.Visible = False
        btnReQuer.Visible = False
        lblGoClickQuery.Text = "请点击查询按钮,多个条件用逗号隔开"

        strJumpJS = " $("".txtDateFrom "").datepicker({dateFormat: ""yy-mm-dd"",onSelect: function (selectedDate) { $(""#datepicked"").empty().append(selectedDate); } });"
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, Me.GetType, "ShowLoading", strJumpJS, True)

    End Sub
End Class
