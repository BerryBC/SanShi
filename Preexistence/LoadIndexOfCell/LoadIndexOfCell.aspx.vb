Imports SQLServerLibrary
Imports System.Data.SqlClient
Imports System.Data
Imports ExcelLibrary
Imports CSVLibrary


Partial Class Preexistence_LoadIndexOfCell
    Inherits System.Web.UI.Page
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary

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

            scmdCommand = sqllSSLibrary.GetCommandStr(strSQLS, CommonLibrary.GetSQLServerConnect("ConnectionJunHeng"))
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


            strSaveFileName = Now.ToString("yyyyMMddHHmmss") & "-小区网格六忙时指标-" & Session("SanShiUserName") & ".csv"

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
                erlErrorReport.ReportServerError(19, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=19&eu=" & "")
            Else
                erlErrorReport.ReportServerError(19, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=19&eu=" & Session("SanShiUserName"))

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
                erlErrorReport.ReportServerError(19, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=19&eu=" & "")
            Else
                erlErrorReport.ReportServerError(19, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=19&eu=" & Session("SanShiUserName"))

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


    Private Sub timerGo_Tick(sender As Object, e As EventArgs) Handles timerGo.Tick
        Dim strPartition() As String
        Dim strGrib() As String
        Dim strBSC() As String
        Dim strCell() As String
        Dim strBaseName() As String
        Dim strSQLSHandel As String
        Dim strSingleWord As String
        Dim bolIsHaveWord As Boolean


        Try


            timerGo.Enabled = False




            bolIsHaveWord = False

            btnRunQuery.Enabled = False
            btnRunQuery.CssClass = "form-control btn-warning"



            strSQLSHandel = "SELECT   小区名,dbo.[_ID表].ID,网格九分区,网格,[Moid(GSM_CELL)], [Bsc(GSM_CELL)], JunHeng.[Datetime Id(GSM_CELL)] AS Day, SUM([JunHeng].[TCH话务量]) AS TCH话务量, SUM([JunHeng].[总流量(MB)]) AS [总流量(MB)], SUM([JunHeng].[PDCH使用数]) AS PDCH使用数, SUM([JunHeng].[TCH话务量] + [JunHeng].[PDCH使用数]) / NULLIF (SUM([JunHeng].[TCH定义信道数]) + 0.00, 0) / 0.75 / 6 AS 信道利用率, SUM([JunHeng].[半速率话务量]) / NULLIF (SUM([JunHeng].[TCH话务量]), 0) AS 半速率比例, (SUM(IIF(([JunHeng].[Randomacc Cnrocnt] + [JunHeng].[Randomacc Raaccfa]) IS NOT NULL, [JunHeng].[Celtchfp Tfcongpgsm] + 0.00, 0)) / NULLIF (SUM(IIF([JunHeng].[Celtchfp Tfcongpgsm] IS NOT NULL, [JunHeng].[Randomacc Cnrocnt] + [JunHeng].[Randomacc Raaccfa], 0)), 0)) * (SUM([JunHeng].[Cltch Tcassall]) / NULLIF (SUM([JunHeng].[Cltch Tassall]) + 0.00, 0)) AS 无线接入性, SUM([JunHeng].[Cltch Tcassall]) / NULLIF (SUM([JunHeng].[Cltch Tassall]) + 0.00, 0) AS TCH指配成功率, SUM([JunHeng].[Randomacc Cnrocnt]) / NULLIF (SUM([JunHeng].[Randomacc Cnrocnt] + [JunHeng].[Randomacc Raaccfa]) + 0.00, 0) AS 随机接入成功率, SUM(IIF(([JunHeng].[Cltch Tassall]) >= 0, [JunHeng].[Celtchfp Tfestpgsmsub], 0)) / NULLIF (SUM([JunHeng].[Cltch Tassall]) + 0.00, 0) AS [TCH拥塞率(考核算法)], SUM(IIF(([JunHeng].[Randomacc Cnrocnt] + [JunHeng].[Randomacc Raaccfa]) > 0, [JunHeng].[Celtchfp Tfcongpgsm], 0)) / NULLIF (SUM(IIF([JunHeng].[Celtchfp Tfcongpgsm] >= 0, [JunHeng].[Randomacc Cnrocnt] + [JunHeng].[Randomacc Raaccfa], 0)) + 0.00, 0) AS SDCCH指配成功率, SUM([JunHeng].[Clsdcch Ccongs]) / NULLIF (SUM([JunHeng].[Clsdcch Ccalls]) + 0.00, 0) AS SDCCH拥塞率, SUM([JunHeng].[Clsdcch Cndrop]) / NULLIF (SUM([JunHeng].[Clsdcch Cmsestab]) + 0.00, 0) AS SDCCH掉话率, SUM([JunHeng].[TCH可用信道数]) / NULLIF (SUM([JunHeng].[TCH定义信道数]) + 0.00, 0) AS TCH信道完好率, SUM([JunHeng].[Cellpag Pagetooold] + [JunHeng].[Cellpag Pagpchcong]) AS 寻呼拥塞总数, SUM([JunHeng].[掉话数]) / NULLIF (SUM([JunHeng].[TCH话务量]), 0) AS 每Erl掉话数, SUM(IIF([Celtchfp Tfcongpgsmsub] >= 0, [TCH话务量], 0)) / (NULLIF (SUM(IIF([TCH话务量] >= 0, [Celtchfp Tfcongpgsmsub], 0)), 0) + 0.00) * 60 AS 话务掉话比, SUM([JunHeng].[Celtchfp Tfcongpgsmsub]) / NULLIF (SUM([JunHeng].[Cltch Tmsestb]) + 0.00, 0) AS [掉话率(最差小区考核算法)], SUM([JunHeng].[掉话数]) / NULLIF (SUM([JunHeng].[Cltch Tcassall]) + 0.00, 0) AS [TCH掉话率(不含切换)], SUM([JunHeng].[Idleutchf Itfusib4] + [JunHeng].[Idleutchf Itfusib5]) / NULLIF (SUM([JunHeng].[Idleutchf Itfusib1] + [JunHeng].[Idleutchf Itfusib2] + [JunHeng].[Idleutchf Itfusib3] + [JunHeng].[Idleutchf Itfusib4] + [JunHeng].[Idleutchf Itfusib5]) + 0.00, 0) AS 上行底噪, SUM([JunHeng].[Idleutchf Itfusib1] + [JunHeng].[Idleutchf Itfusib2] * 2 + [JunHeng].[Idleutchf Itfusib3] * 3 + [JunHeng].[Idleutchf Itfusib4] * 4 + [JunHeng].[Idleutchf Itfusib5] * 5) / NULLIF (SUM([JunHeng].[Idleutchf Itfusib1] + [JunHeng].[Idleutchf Itfusib2] + [JunHeng].[Idleutchf Itfusib3] + [JunHeng].[Idleutchf Itfusib4] + [JunHeng].[Idleutchf Itfusib5]) + 0.00, 0) AS 干扰系数, SUM([JunHeng].[切出分子]) / NULLIF (SUM([JunHeng].[切出分母]) + 0.00, 0) AS 切出成功率, SUM([JunHeng].[切入分子]) / NULLIF (SUM([JunHeng].[切入分母]) + 0.00, 0) AS 切入成功率, 1 - SUM([JunHeng].[Cellgprs Faildltbfest]) / NULLIF (SUM([JunHeng].[Cellgprs Dltbfest]) + 0.00, 0) AS 下行TBF建立成功率, SUM([JunHeng].[Trafdlgprs Dltbfpbpdch] + [JunHeng].[Trafdlgprs Dltbfpepdch]) / NULLIF (SUM([JunHeng].[Trafdlgprs Dlbpdch] + [JunHeng].[Trafdlgprs Dlepdch]) + 0.00, 0) AS [PDCH下行复用度(旧)], SUM([JunHeng].[Trafdlgprs Dlacttbfpbpdch] + [JunHeng].[Trafdlgprs Dlacttbfpepdch]) / NULLIF (SUM([JunHeng].[Trafdlgprs Dlactbpdch] + [JunHeng].[Trafdlgprs Dlactepdch]) + 0.00, 0) AS [PDCH下行复用度(新)], SUM([JunHeng].[Cellqoseg DlthpegthrTotal]) / NULLIF (SUM([JunHeng].[Cellqoseg DlthpegdataTotal]) + 0.00, 0) AS EDGE下行速率, SUM([JunHeng].[Cellqosg DlthpgthrTotal]) / NULLIF (SUM([JunHeng].[Cellqosg DlthpgdataTotal]) + 0.00, 0) AS GPRS下行速率, SUM([JunHeng].[Cellgprs2 Ldistfi] + [JunHeng].[Cellgprs2 Ldisrr] + [JunHeng].[Cellgprs2 Ldisoth]) / NULLIF (SUM([JunHeng].[Cellgprs Dltbfest]  - [JunHeng].[Cellgprs Faildltbfest]) + 0.00, 0) AS TBF掉线率, SUM([JunHeng].[上行质差6-7级分子]) / NULLIF (SUM([JunHeng].[上行质差分母]) + 0.00, 0) AS [上行6-7级质差话务比例], SUM([JunHeng].[下行质差6-7级分子]) / NULLIF (SUM([JunHeng].[下行质差分母]) + 0.00, 0) AS [下行6-7级质差话务比例], 1 - SUM([JunHeng].[上行不达标(0-9)]) / NULLIF (SUM([JunHeng].[上行汇总]) + 0.00, 0) AS 上行覆盖率, 1 - SUM([JunHeng].[下行不达标(0-15)]) / NULLIF (SUM([JunHeng].[下行汇总]) + 0.00, 0) AS 下行覆盖率, SUM([JunHeng].[上行路损分子]) / NULLIF (SUM([JunHeng].[上行路损分母]) + 0.00, 0) * 2 + 30 AS 上行路损, SUM([JunHeng].[下行路损分子]) / NULLIF (SUM([JunHeng].[下行路损分母]) + 0.00, 0) * 2 + 30 AS 下行路损, SUM([JunHeng].[TA分子]) / NULLIF (SUM([JunHeng].[TA分母]) + 0.00, 0) AS 平均TA, SUM(IIF(dbo.getMN([Moid(GSM_CELL)]) = '室内', [上行质差6-7级分子], 0)) / NULLIF (SUM(IIF(dbo.getMN([Moid(GSM_CELL)]) = '室内', 上行质差分母, 0)) + 0.00, 0) AS '室内上行质差比例', SUM(IIF(dbo.getMN([Moid(GSM_CELL)]) = '室内', [下行质差6-7级分子], 0)) / NULLIF (SUM(IIF(dbo.getMN([Moid(GSM_CELL)]) = '室内', 下行质差分母, 0)) + 0.00, 0) AS '室内下行质差比例', SUM(IIF(dbo.getMN([Moid(GSM_CELL)]) = '室外', [上行质差6-7级分子], 0)) / NULLIF (SUM(IIF(dbo.getMN([Moid(GSM_CELL)]) = '室外', 上行质差分母, 0)) + 0.00, 0) AS '室外上行质差比例', SUM(IIF(dbo.getMN([Moid(GSM_CELL)]) = '室外', [下行质差6-7级分子], 0)) / NULLIF (SUM(IIF(dbo.getMN([Moid(GSM_CELL)]) = '室外', 下行质差分母, 0)) + 0.00, 0) AS '室外下行质差比例', SUM(IIF(([JunHeng].[Idleutchf Itfusib4] + [JunHeng].[Idleutchf Itfusib5]) / NULLIF ([JunHeng].[Idleutchf Itfusib1] + [JunHeng].[Idleutchf Itfusib2] + [JunHeng].[Idleutchf Itfusib3] + [JunHeng].[Idleutchf Itfusib4] + [JunHeng].[Idleutchf Itfusib5] + 0.00, 0) > 0.3, 1, 0)) AS [高干扰小区数(4-5级干扰>30%)], SUM(IIF([JunHeng].[上行质差6-7级分子] / NULLIF ([JunHeng].[上行质差分母] + 0.00, 0) > 0.05, 1, 0)) AS [上行高质差小区数(6-7级质差>5%)], SUM(IIF([JunHeng].[下行质差6-7级分子] / NULLIF ([JunHeng].[下行质差分母] + 0.00, 0) > 0.05, 1, 0)) AS [下行高质差小区数(6-7级质差>5%)], COUNT([JunHeng].[TCH话务量]) AS 小区数, SUM([JunHeng].[TCH定义信道数]) AS TCH定义信道数, SUM([JunHeng].[TCH可用信道数]) AS TCH可用信道数, SUM([Randomacc Cnrocnt]) AS [Randomacc Cnrocnt], SUM([Randomacc Raaccfa]) AS [Randomacc Raaccfa], SUM([Ccchload Psimmass]) AS [Ccchload Psimmass], SUM([Ccchload Csimmass]) AS [Ccchload Csimmass], SUM([Cellpag Pagetooold]) AS [Cellpag Pagetooold], SUM([Cellpag Pagpchcong]) AS [Cellpag Pagpchcong], SUM([Clsdcch Ccalls]) AS [Clsdcch Ccalls], SUM([Clsdcch Ccongs]) AS [Clsdcch Ccongs], SUM([Clsdcch Cmsestab]) AS [Clsdcch Cmsestab], SUM([Clsdcch Cndrop]) AS [Clsdcch Cndrop], SUM([Cellcchho Cchhosuc]) AS [Cellcchho Cchhosuc], SUM([TCH申请数]) AS [TCH申请数], SUM([TCH拥塞数]) AS [TCH拥塞数], SUM([Cltch Tmsestb]) AS [Cltch Tmsestb], SUM([Cltch Tcassall]) AS [Cltch Tcassall], SUM([Cltch Tassall]) AS [Cltch Tassall], SUM([掉话数]) AS [掉话数], SUM([上行弱信号掉话]) AS [上行弱信号掉话], SUM([下行弱信号掉话]) AS [下行弱信号掉话], SUM([双向弱信号掉话]) AS [双向弱信号掉话], SUM([上行质差掉话]) AS [上行质差掉话], SUM([下行质差掉话]) AS [下行质差掉话], SUM([双向质差掉话]) AS [双向质差掉话], SUM([TA掉话]) AS [TA掉话], SUM([突然掉话]) AS [突然掉话], SUM([Cellsqi GOOD]) AS [Cellsqi GOOD], SUM([Cellsqi ACCPT]) AS [Cellsqi ACCPT], SUM([Cellsqi BAD]) AS [Cellsqi BAD], SUM([Idleutchf Itfusib1]) AS [Idleutchf Itfusib1], SUM([Idleutchf Itfusib2]) AS [Idleutchf Itfusib2], SUM([Idleutchf Itfusib3]) AS [Idleutchf Itfusib3], SUM([Idleutchf Itfusib4]) AS [Idleutchf Itfusib4], SUM([Idleutchf Itfusib5]) AS [Idleutchf Itfusib5], SUM([Celtchfp Tfcongpgsm]) AS [Celtchfp Tfcongpgsm], SUM([Celtchfp Tfcongpgsmsub]) AS [Celtchfp Tfcongpgsmsub], SUM([Celtchfp Tfestpgsmsub]) AS [Celtchfp Tfestpgsmsub], SUM([Trafdlgprs Dlbpdch]) AS [Trafdlgprs Dlbpdch], SUM([Trafdlgprs Dlepdch]) AS [Trafdlgprs Dlepdch], SUM([Trafdlgprs Dltbfpbpdch]) AS [Trafdlgprs Dltbfpbpdch], SUM([Trafdlgprs Dltbfpepdch]) AS [Trafdlgprs Dltbfpepdch], SUM([Cellgprs Dltbfest]) AS [Cellgprs Dltbfest], SUM([Cellgprs Faildltbfest]) AS [Cellgprs Faildltbfest], SUM([Cellqoseg DlthpegdataTotal]) AS [Cellqoseg DlthpegdataTotal], SUM([Cellqoseg DlthpegthrTotal]) AS [Cellqoseg DlthpegthrTotal], SUM([Cellqosg DlthpgdataTotal]) AS [Cellqosg DlthpgdataTotal], SUM([Cellqosg DlthpgthrTotal]) AS [Cellqosg DlthpgthrTotal], SUM([Trafdlgprs Dlactbpdch]) AS [Trafdlgprs Dlactbpdch], SUM([Trafdlgprs Dlactepdch]) AS [Trafdlgprs Dlactepdch], SUM([Trafdlgprs Dlacttbfpbpdch]) AS [Trafdlgprs Dlacttbfpbpdch], SUM([Trafdlgprs Dlacttbfpepdch]) AS [Trafdlgprs Dlacttbfpepdch], SUM([Cellgprs2 Ldistfi]) AS [Cellgprs2 Ldistfi], SUM([Cellgprs2 Ldisrr]) AS [Cellgprs2 Ldisrr], SUM([Cellgprs2 Ldisoth]) AS [Cellgprs2 Ldisoth], SUM([Cellgprs Dltbfest-3]) AS [Cellgprs Dltbfest-3], SUM([Cellgprs Faildltbfest-3]) AS [Cellgprs Faildltbfest-3], SUM([Cellgprs2 Ldistfi-3]) AS [Cellgprs2 Ldistfi-3], SUM([Cellgprs2 Ldisrr-3]) AS [Cellgprs2 Ldisrr-3], SUM([Cellgprs2 Ldisoth-3]) AS [Cellgprs2 Ldisoth-3], SUM([上行质差5-7级分子]) AS [上行质差5-7级分子], SUM([下行质差5-7级分子]) AS [下行质差5-7级分子], SUM([上行质差6-7级分子]) AS [上行质差6-7级分子], SUM([下行质差6-7级分子]) AS [下行质差6-7级分子], SUM([上行质差分母]) AS [上行质差分母], SUM([下行质差分母]) AS [下行质差分母], SUM([上行不达标(0-9)]) AS [上行不达标(0-9)], SUM([上行汇总]) AS [上行汇总], SUM([下行不达标(0-15)]) AS [下行不达标(0-15)], SUM([下行汇总]) AS [下行汇总], SUM([TA分子]) AS [TA分子], SUM([TA分母]) AS [TA分母], SUM([上行路损分子]) AS [上行路损分子], SUM([上行路损分母]) AS [上行路损分母], SUM([下行路损分子]) AS [下行路损分子], SUM([下行路损分母]) AS [下行路损分母], SUM([切出分子]) AS [切出分子], SUM([切出分母]) AS [切出分母], SUM([切入分子]) AS [切入分子], SUM([切入分母]) AS [切入分母] FROM JunHeng INNER JOIN dbo.[_ID表] ON CELL=[Moid(GSM_CELL)] INNER JOIN dbo.[_小区网格分区] ON [_小区网格分区].ID = [_ID表].ID where ( "

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
                    strSQLSHandel += "[小区名]='" & strSingleWord & "' or "
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
                strSQLSHandel += " ([Datetime Id(GSM_CELL)]>='" & DateTime.Now.AddDays(-ddlWhatTime.SelectedValue).ToShortDateString & "' and [Datetime Id(GSM_CELL)]<='" & DateTime.Now.ToShortDateString & "')"
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

                strSQLSHandel += " ([Datetime Id(GSM_CELL)]>='" & txtBeginDate.Text & "' and [Datetime Id(GSM_CELL)]<='" & txtEndDate.Text & "')"

            End If
            'strJumpJS = "setTimeout(function () { JSCodeShow();}, 100);"
            'ScriptManager.RegisterClientScriptBlock(UpdatePanel1, Me.GetType, "ShowLoading", strJumpJS, True)

            strSQLSHandel += "GROUP BY 小区名,dbo.[_ID表].ID,网格九分区,网格,[Moid(GSM_CELL)], [Bsc(GSM_CELL)], JunHeng.[Datetime Id(GSM_CELL)];"

            SaveIndexFile(strSQLSHandel)
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(19, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=19&eu=" & "")
            Else
                erlErrorReport.ReportServerError(19, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=19&eu=" & Session("SanShiUserName"))

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

    Private Sub Preexistence_LoadIndexOfCell_Load(sender As Object, e As EventArgs) Handles Me.Load
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
                erlErrorReport.ReportServerError(19, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=19&eu=" & "")
            Else
                erlErrorReport.ReportServerError(19, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=19&eu=" & Session("SanShiUserName"))

            End If

        End Try
    End Sub
End Class
