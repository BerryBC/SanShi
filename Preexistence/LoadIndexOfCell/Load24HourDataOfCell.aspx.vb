Imports SQLServerLibrary
Imports System.Data.SqlClient
Imports System.Data
Imports ExcelLibrary
Imports CSVLibrary
Partial Class Preexistence_LoadIndexOfCell_Load24HourDataOfCell
    Inherits System.Web.UI.Page
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary



    Private Sub Preexistence_LoadIndexOfCell_Load24HourDataOfCell_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim liAllData As ListItem
        Try
            If Not IsPostBack Then
                ucUserManage.CheckPower(Session, 2, Response)

                If CType(Session("PowerLevel"), Integer) >= 3 Then
                    liAllData = New ListItem
                    liAllData.Text = "自定义"
                    liAllData.Value = 100
                    ddlWhatTime.Items.Add(liAllData)
                    txtBeginDate.Text = Date.Now.Year & "-" & Format((Date.Now.Month), "00") & "-" & Format(Date.Now.Day, "00")
                    txtEndDate.Text = Date.Now.Year & "-" & Format((Date.Now.Month), "00") & "-" & Format(Date.Now.Day, "00")
                End If
            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(45, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=45&eu=" & "")
            Else
                erlErrorReport.ReportServerError(45, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=45&eu=" & Session("SanShiUserName"))

            End If
        End Try

    End Sub

    Private Sub ddlWhatTime_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlWhatTime.SelectedIndexChanged
        Dim strJumpJS As String

        If ddlWhatTime.Items(ddlWhatTime.SelectedIndex).Value = 100 Then
            plFromDateToDate.Visible = True
            strJumpJS = " $("".txtDateFrom , .txtDateTo"").datepicker({dateFormat: ""yy-mm-dd"",onSelect: function (selectedDate) { $(""#datepicked"").empty().append(selectedDate); } });"
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, Me.GetType, "ShowLoading", strJumpJS, True)

        Else
            plFromDateToDate.Visible = False
        End If

    End Sub

    Private Sub ddlWhatTimeBegin_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlWhatTimeBegin.SelectedIndexChanged
        Dim strJumpJS As String

        If ddlWhatTime.Items(ddlWhatTime.SelectedIndex).Value = 100 Then
            plFromDateToDate.Visible = True
            strJumpJS = " $("".txtDateFrom , .txtDateTo"").datepicker({dateFormat: ""yy-mm-dd"",onSelect: function (selectedDate) { $(""#datepicked"").empty().append(selectedDate); } });"
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, Me.GetType, "ShowLoading", strJumpJS, True)

        Else
            plFromDateToDate.Visible = False
        End If
    End Sub

    Private Sub ddlWhatTimeEnd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlWhatTimeEnd.SelectedIndexChanged
        Dim strJumpJS As String

        If ddlWhatTime.Items(ddlWhatTime.SelectedIndex).Value = 100 Then
            plFromDateToDate.Visible = True
            strJumpJS = " $("".txtDateFrom , .txtDateTo"").datepicker({dateFormat: ""yy-mm-dd"",onSelect: function (selectedDate) { $(""#datepicked"").empty().append(selectedDate); } });"
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, Me.GetType, "ShowLoading", strJumpJS, True)

        Else
            plFromDateToDate.Visible = False
        End If
    End Sub





    Private Sub SaveIndexFile(strSQLS As String)
        Dim scmdCommand As SqlCommand
        Dim dtIndexOfGSMCell As DataTable
        Dim tableHtmlTable As Table
        Dim thrHead As TableHeaderRow
        Dim thcHead As TableHeaderCell
        Dim dcColoumnForHead As DataColumn
        Dim drLoadData As DataRow
        Dim i As Integer
        Dim intMaxDataTableCols As Integer
        Dim tbrContent As TableRow
        Dim tbcContent As TableCell
        Dim intBeford As Integer
        Dim bolSaveSuccess As Boolean
        Dim csvCSV As LoadCSV
        Dim strSaveFileName As String


        Try


            intBeford = 0

            scmdCommand = sqllSSLibrary.GetCommandStr(strSQLS, CommonLibrary.GetSQLServerConnect("ConnectionGanZhi"))
            scmdCommand.CommandTimeout = 500
            dtIndexOfGSMCell = sqllSSLibrary.GetSQLServerDataTable(scmdCommand)

            tableHtmlTable = tbOutPut
            thrHead = New TableHeaderRow

            For Each dcColoumnForHead In dtIndexOfGSMCell.Columns
                thcHead = New TableHeaderCell
                thcHead.Text = dcColoumnForHead.Caption
                thcHead.BorderStyle = BorderStyle.Groove
                thcHead.VerticalAlign = VerticalAlign.Middle
                thcHead.HorizontalAlign = HorizontalAlign.Center
                thcHead.Wrap = False

                thrHead.Cells.Add(thcHead)
            Next

            tableHtmlTable.Rows.AddAt(0, thrHead)

            intMaxDataTableCols = dtIndexOfGSMCell.Columns.Count

            For Each drLoadData In dtIndexOfGSMCell.Rows
                If intBeford >= 10 Then
                    Exit For
                Else

                    tbrContent = New TableRow
                    For i = 0 To intMaxDataTableCols - 1
                        tbcContent = New TableCell
                        tbcContent.Text = drLoadData.Item(i).ToString
                        tbcContent.BorderStyle = BorderStyle.Groove
                        tbcContent.VerticalAlign = VerticalAlign.Middle
                        tbcContent.HorizontalAlign = HorizontalAlign.Center
                        tbcContent.Wrap = False
                        tbrContent.Cells.Add(tbcContent)
                    Next
                    tableHtmlTable.Rows.Add(tbrContent)
                    intBeford += 1
                End If
            Next

            If dtIndexOfGSMCell.Rows.Count > 10 Then
                lblLoading.Text = "还剩余 " & dtIndexOfGSMCell.Rows.Count - 10 & " 行没有显示，请下载完整数据"
                plForShowMessage.Visible = True
            End If


            strSaveFileName = Now.ToString("yyyyMMddHHmmss") & "-GSM小区小时级数据导出-" & Session("SanShiUserName") & ".csv"

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
                erlErrorReport.ReportServerError(45, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=45&eu=" & "")
            Else
                erlErrorReport.ReportServerError(45, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=45&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub


    Private Sub btnRunQuery_Click(sender As Object, e As EventArgs) Handles btnRunQuery.Click
        Dim bolIsHaveWord As Boolean
        Dim strJumpJS As String

        Try
            bolIsHaveWord = False
            btnRunQuery.Enabled = False
            btnRunQuery.CssClass = "form-control btn-warning"

            If txtPartition.Text.Length > 0 Then
                bolIsHaveWord = True
            End If
            If txtGrid.Text.Length > 0 Then
                bolIsHaveWord = True
            End If
            If txtBSC.Text.Length > 0 Then
                bolIsHaveWord = True
            End If
            If txtCell.Text.Length > 0 Then
                bolIsHaveWord = True
            End If
            If txtBaseName.Text.Length > 0 Then
                bolIsHaveWord = True
            End If

            If Not bolIsHaveWord Then
                lblWrongDate.Text = "不能不设置条件"
                plError.Visible = True
                plGoClickQuery.Visible = False
                plDownLoadAddress.Visible = False
                plForShowMessage.Visible = False
                btnRunQuery.Enabled = True

                Exit Sub

            End If

            If ddlWhatTime.SelectedValue >= 100 Then
                If (txtBeginDate.Text.Length <> 10 And txtEndDate.Text.Length <> 10) Then
                    lblWrongDate.Text = "日期格式错误，应为 ""2016-05-30""且范围是2015年至今的日期"
                    plError.Visible = True
                    plGoClickQuery.Visible = False
                    plDownLoadAddress.Visible = False
                    plForShowMessage.Visible = False
                    btnRunQuery.Enabled = True

                    Exit Sub
                End If
                If (Not IsDateString(txtBeginDate.Text) And Not IsDateString(txtEndDate.Text)) Then
                    lblWrongDate.Text = "日期格式错误，应为 ""2016-05-30""且范围是2015年至今的日期"
                    plError.Visible = True
                    plGoClickQuery.Visible = False
                    plDownLoadAddress.Visible = False
                    plForShowMessage.Visible = False
                    btnRunQuery.Enabled = True

                    Exit Sub
                End If
            End If
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
                erlErrorReport.ReportServerError(45, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=45&eu=" & "")
            Else
                erlErrorReport.ReportServerError(45, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=45&eu=" & Session("SanShiUserName"))

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
        Dim strPartition() As String
        Dim strGrib() As String
        Dim strBSC() As String
        Dim strCell() As String
        Dim strBaseName() As String
        Dim strSQLSHandel As String
        Dim strSingleWord As String
        Dim bolIsHaveWord As Boolean

        '------------这里改SQL
        Try


            timerGo.Enabled = False




            bolIsHaveWord = False

            btnRunQuery.Enabled = False
            btnRunQuery.CssClass = "form-control btn-warning"



            strSQLSHandel = "SELECT  dbo.Data.[Bsc(GSM_CELL)],dbo.Data.[Moid(GSM_CELL)],dbo.[_ID表].ID,dbo.[_小区网格分区].网格九分区, [data].[Datetime Id(GSM_CELL)], (SUM(IIF(([data].[Randomacc Cnrocnt] + [data].[Randomacc Raaccfa]) >= 0,   [data].[Celtchfp Tfcongpgsm], 0)) / (NULLIF (SUM(IIF([data].[Celtchfp Tfcongpgsm] >= 0,   [data].[Randomacc Cnrocnt] + [data].[Randomacc Raaccfa], 0)), 0) + 0.00)) * (SUM([data].[Cltch Tcassall])   / (NULLIF (SUM([data].[Cltch Tassall]), 0) + 0.00)) AS 无线接入性, 1 - SUM([data].[Cellgprs Faildltbfest])   / (NULLIF (SUM([data].[Cellgprs Dltbfest]), 0) + 0.00) AS 下行TBF建立成功率, SUM([data].[掉话数])   / (NULLIF (SUM([data].[TCH话务量]), 0) + 0.00) AS 每Erl掉话数, SUM([data].[TCH话务量]) + 0.00 AS TCH话务量,   SUM([data].[半速率话务量]) / (NULLIF (SUM([data].[TCH话务量]), 0) + 0.00) AS 半速率比例,   SUM([data].[Randomacc Cnrocnt]) / (NULLIF (SUM([data].[Randomacc Cnrocnt] + [data].[Randomacc Raaccfa]), 0) + 0.00)   AS 随机接入成功率, SUM(IIF([data].[Cltch Tassall] >= 0, [data].[Celtchfp Tfestpgsmsub], 0))   / (NULLIF (SUM(IIF([data].[Celtchfp Tfestpgsmsub] >= 0, [data].[Cltch Tassall], 0)), 0) + 0.00) AS TCH拥塞率_考核算法,   SUM(IIF([data].[Celtchfp Tfcongpgsmsub] >= 0, [data].[TCH话务量], 0)) / (NULLIF (SUM(IIF([data].[TCH话务量] >= 0,   [data].[Celtchfp Tfcongpgsmsub], 0)), 0) + 0.00) * 60 AS 话务掉话比,   SUM([data].[Idleutchf Itfusib4] + [data].[Idleutchf Itfusib5])   / (NULLIF (SUM([data].[Idleutchf Itfusib1] + [data].[Idleutchf Itfusib2] + [data].[Idleutchf Itfusib3] + [data].[Idleutchf Itfusib4] +   [data].[Idleutchf Itfusib5]), 0) + 0.00) AS 上行底噪, SUM([data].[Cellqoseg DlthpegthrTotal])   / (NULLIF (SUM([data].[Cellqoseg DlthpegdataTotal]), 0) + 0.00) AS EDGE下行速率, SUM([data].[Cellqosg DlthpgthrTotal])   / (NULLIF (SUM([data].[Cellqosg DlthpgdataTotal]), 0) + 0.00) AS GPRS下行速率, SUM(IIF([data].[Cltch Tmsestb] >= 0,   [data].[Celtchfp Tfcongpgsmsub], 0)) / (NULLIF (SUM(IIF([data].[Celtchfp Tfcongpgsmsub] >= 0, [data].[Cltch Tmsestb], 0)),   0) + 0.00) AS 掉话率_最差小区考核算法, SUM([data].[掉话数]) / (NULLIF (SUM([data].[Cltch Tcassall]), 0) + 0.00)   AS TCH掉话率_不含切换,   SUM([data].[Idleutchf Itfusib1] + [data].[Idleutchf Itfusib2] * 2 + [data].[Idleutchf Itfusib3] * 3 + [data].[Idleutchf Itfusib4] * 4 + [data].[Idleutchf Itfusib5]   * 5)   / (NULLIF (SUM([data].[Idleutchf Itfusib1] + [data].[Idleutchf Itfusib2] + [data].[Idleutchf Itfusib3] + [data].[Idleutchf Itfusib4] +   [data].[Idleutchf Itfusib5]), 0) + 0.00) AS 干扰系数,   (SUM([data].[EG IP流量(DL KB)] + [data].[EG IP流量(UL KB)] + [data].[GPRS IP流量(DL KB)] + [data].[GPRS IP流量(UL KB)])   + 0.00) / 1024 AS 总流量_MB, SUM([data].[PDCH使用数]) AS PDCH使用数, SUM(NULLIF ([data].[TCH可用信道数], 0))   / (SUM(NULLIF ([data].[TCH定义信道数], 0)) + 0.00) AS TCH信道完好率, SUM([data].[TCH定义信道数]) AS TCH定义信道数,   SUM([data].[TCH可用信道数]) AS TCH可用信道数, SUM(NULLIF ([data].[Celtchfp Tfcongpgsm], 0))   / (SUM(NULLIF ([data].[Randomacc Cnrocnt] + [data].[Randomacc Raaccfa], 0)) + 0.00) AS SDCCH指配成功率,   SUM([data].[Clsdcch Ccongs]) / (SUM(NULLIF ([data].[Clsdcch Ccalls], 0)) + 0.00) AS SDCCH拥塞率,   SUM([data].[Clsdcch Cndrop]) / (SUM(NULLIF ([data].[Clsdcch Cmsestab], 0)) + 0.00) AS SDCCH掉话率,   SUM([data].[Cltch Tcassall]) / (SUM(NULLIF ([data].[Cltch Tassall], 0)) + 0.00) AS TCH指配成功率,   SUM([data].[Trafdlgprs Dltbfpbpdch] + [data].[Trafdlgprs Dltbfpepdch])   / (SUM(NULLIF (([data].[Trafdlgprs Dlbpdch] + [data].[Trafdlgprs Dlepdch]), 0)) + 0.00) AS PDCH下行复用度_旧,   SUM([data].[Trafdlgprs Dlacttbfpbpdch] + [data].[Trafdlgprs Dlacttbfpepdch])   / (SUM(NULLIF ([data].[Trafdlgprs Dlactbpdch] + [data].[Trafdlgprs Dlactepdch], 0)) + 0.00) AS PDCH下行复用度_新,   SUM([data].[Cellpag Pagetooold] + [data].[Cellpag Pagpchcong]) AS 寻呼拥塞总数,   SUM([data].[TCH话务量] + [data].[PDCH使用数]) / (SUM(NULLIF ([data].[TCH定义信道数], 0)) + 0.00) / 0.75 AS 信道利用率,   SUM([data].[Cellgprs2 Ldistfi] + [data].[Cellgprs2 Ldisrr] + [data].[Cellgprs2 Ldisoth])   / (NULLIF (SUM([data].[Cellgprs Dltbfest] - [data].[Cellgprs Faildltbfest]), 0) + 0.00) AS TBF掉线率,   SUM(IIF((NULLIF (([data].[Idleutchf Itfusib4] + [data].[Idleutchf Itfusib5]), 0)   / (NULLIF (([data].[Idleutchf Itfusib1] + [data].[Idleutchf Itfusib2] + [data].[Idleutchf Itfusib3] + [data].[Idleutchf Itfusib4] + [data].[Idleutchf Itfusib5]),   0) + 0.00)) > 0.3, 1, 0)) AS 高干扰小区数_45级干扰大于30, COUNT([data].[TCH话务量]) AS 小区数 FROM      (dbo.Data  LEFT JOIN dbo.[_ID表] ON CELL=[Moid(GSM_CELL)]) LEFT JOIN dbo.[_小区网格分区] ON [_小区网格分区].ID = [_ID表].ID  Where ( "

            strPartition = txtPartition.Text.Split(",")
            strGrib = txtGrid.Text.Split(",")
            strBSC = txtBSC.Text.Split(",")
            strCell = txtCell.Text.Split(",")
            strBaseName = txtBaseName.Text.Split(",")

            If txtPartition.Text.Length > 0 Then
                For Each strSingleWord In strPartition
                    strSingleWord = strSingleWord.Replace(" ", "")
                    strSingleWord = strSingleWord.Replace("'", "")
                    strSQLSHandel += "[网格九分区]='" & strSingleWord & "' or "
                Next
                bolIsHaveWord = True
            End If
            If txtGrid.Text.Length > 0 Then
                For Each strSingleWord In strGrib
                    strSingleWord = strSingleWord.Replace(" ", "")
                    strSingleWord = strSingleWord.Replace("'", "")
                    strSQLSHandel += "[网格]='" & strSingleWord & "' or "
                Next
                bolIsHaveWord = True
            End If
            If txtBSC.Text.Length > 0 Then
                For Each strSingleWord In strBSC
                    strSingleWord = strSingleWord.Replace(" ", "")
                    strSingleWord = strSingleWord.Replace("'", "")
                    strSQLSHandel += "[Bsc(GSM_CELL)]='" & strSingleWord & "' or "
                Next
                bolIsHaveWord = True
            End If
            If txtCell.Text.Length > 0 Then
                For Each strSingleWord In strCell
                    strSingleWord = strSingleWord.Replace(" ", "")
                    strSingleWord = strSingleWord.Replace("'", "")
                    strSQLSHandel += "[Moid(GSM_CELL)]='" & strSingleWord & "' or "
                Next
                bolIsHaveWord = True
            End If
            If txtBaseName.Text.Length > 0 Then
                For Each strSingleWord In strBaseName
                    strSingleWord = strSingleWord.Replace(" ", "")
                    strSingleWord = strSingleWord.Replace("'", "")
                    strSQLSHandel += "[基站名]='" & strSingleWord & "' or "
                Next
                bolIsHaveWord = True
            End If

            If bolIsHaveWord Then
                strSQLSHandel = Left(strSQLSHandel, Len(strSQLSHandel) - 3) & ") and "
            Else
                lblWrongDate.Text = "不能不设置条件"
                plError.Visible = True
                plGoClickQuery.Visible = False
                plDownLoadAddress.Visible = False
                plForShowMessage.Visible = False
                btnRunQuery.Enabled = True

                Exit Sub

            End If

            If ddlWhatTime.SelectedValue < 100 Then
                strSQLSHandel += " ([Datetime Id(GSM_CELL)]>='" & DateTime.Now.AddDays(-ddlWhatTime.SelectedValue).ToShortDateString & "' and [Datetime Id(GSM_CELL)]<='" & DateTime.Now.ToShortDateString & "')   GROUP BY dbo.Data.[Bsc(GSM_CELL)],dbo.Data.[Moid(GSM_CELL)],dbo.[_ID表].ID,dbo.[_小区网格分区].网格九分区, [data].[Datetime Id(GSM_CELL)]"
            Else
                If (txtBeginDate.Text.Length <> 10 And txtEndDate.Text.Length <> 10) Then
                    lblWrongDate.Text = "日期格式错误，应为 ""2016-05-30""且范围是2015年至今的日期"
                    plError.Visible = True
                    plGoClickQuery.Visible = False
                    plDownLoadAddress.Visible = False
                    plForShowMessage.Visible = False
                    btnRunQuery.Enabled = True
                    Exit Sub
                End If
                If (Not IsDateString(txtBeginDate.Text) And Not IsDateString(txtEndDate.Text)) Then
                    lblWrongDate.Text = "日期格式错误，应为 ""2016-05-30""且范围是2015年至今的日期"
                    plError.Visible = True
                    plGoClickQuery.Visible = False
                    plDownLoadAddress.Visible = False
                    plForShowMessage.Visible = False
                    btnRunQuery.Enabled = True

                    Exit Sub
                End If

                strSQLSHandel += " ([Datetime Id(GSM_CELL)]>='" & txtBeginDate.Text & " " & ddlWhatTimeBegin.SelectedValue & ":00' and [Datetime Id(GSM_CELL)]<='" & txtEndDate.Text & " " & ddlWhatTimeEnd.SelectedValue & ":00')  GROUP BY dbo.Data.[Bsc(GSM_CELL)],dbo.Data.[Moid(GSM_CELL)],dbo.[_ID表].ID,dbo.[_小区网格分区].网格九分区, [data].[Datetime Id(GSM_CELL)]"

            End If
            'strJumpJS = "setTimeout(function () { JSCodeShow();}, 100);"
            'ScriptManager.RegisterClientScriptBlock(UpdatePanel1, Me.GetType, "ShowLoading", strJumpJS, True)

            SaveIndexFile(strSQLSHandel)
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(45, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=45&eu=" & "")
            Else
                erlErrorReport.ReportServerError(45, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=45&eu=" & Session("SanShiUserName"))

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
        tbOutPut.Rows.Clear()
        lblGoClickQuery.Text = "请点击查询按钮,多个条件用逗号隔开"

        strJumpJS = " $("".txtDateFrom , .txtDateTo"").datepicker({dateFormat: ""yy-mm-dd"",onSelect: function (selectedDate) { $(""#datepicked"").empty().append(selectedDate); } });"
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, Me.GetType, "ShowLoading", strJumpJS, True)

    End Sub
End Class
