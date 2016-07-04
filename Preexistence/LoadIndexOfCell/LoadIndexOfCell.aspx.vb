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
    Dim gsmioclLibrary As GSMIndexOfCellLibrary = New GSMIndexOfCellLibrary

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

            scmdCommand = sqllSSLibrary.GetCommandStr(strSQLS, CommonLibrary.GetSQLServerConnect("ConnectionTrafficDB"))
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


            strSaveFileName = Now.ToString("yyyyMMddHHmmss") & "-网格小区列表导出-" & Session("SanShiUserName") & ".csv"

            csvCSV = New LoadCSV(Server.MapPath("/TmpFiles/") & strSaveFileName)
            bolSaveSuccess = csvCSV.SaveASNewOne(dtIndexOfGSMCell)
            'If bolSaveSuccess = True Then csvCSV.CompressFile(Server.MapPath("/TmpFiles/") & strSaveFileName, Server.MapPath("/TmpFiles/") & strSaveFileName.Substring(0, Len(strSaveFileName) - 4) & ".zip")

            'hlDownloadLink.Text = Request.Url.Host & "/TmpFiles/" & strSaveFileName.Substring(0, Len(strSaveFileName) - 4 & ".zip")
            'hlDownloadLink.NavigateUrl = "/TmpFiles/" & strSaveFileName.Substring(0, Len(strSaveFileName) - 4 & ".zip")
            'If bolSaveSuccess = True Then csvCSV.CompressFile(Server.MapPath("/TmpFiles/") & strSaveFileName, Server.MapPath("/TmpFiles/") & strSaveFileName & ".zip")

            'hlDownloadLink.Text = Request.Url.Host & "/TmpFiles/" & strSaveFileName & ".zip"
            'hlDownloadLink.NavigateUrl = "/TmpFiles/" & strSaveFileName & ".zip"
            hlDownloadLink.Text = Request.Url.Host & "/TmpFiles/" & strSaveFileName
            hlDownloadLink.NavigateUrl = "/TmpFiles/" & strSaveFileName

            plGoClickQuery.Visible = False
            plDownLoadAddress.Visible = True
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
                If (Not IsDateString(txtBeginDate.Text) And Not IsDateString(txtBeginDate.Text)) Then
                    lblWrongDate.Text = "日期格式错误，应为 ""2016-05-30""且范围是2015年至今的日期"
                    plError.Visible = True
                    plGoClickQuery.Visible = False
                    plDownLoadAddress.Visible = False
                    plForShowMessage.Visible = False
                    btnRunQuery.Enabled = True

                    Exit Sub
                End If


            End If
            lblGoClickQuery.Text = "正在处理，请耐心等候"

            plDownLoadAddress.Visible = False
            plGoClickQuery.Visible = True
            plError.Visible = False
            plForShowMessage.Visible = False
            btnRunQuery.Enabled = False
            btnRunQuery.CssClass = "form-control btn-warning"

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

    Private Sub ddlWhatTime_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlWhatTime.SelectedIndexChanged
        If ddlWhatTime.Items(ddlWhatTime.SelectedIndex).Value = 100 Then
            plFromDateToDate.Visible = True
        Else
            plFromDateToDate.Visible = False
        End If

    End Sub

    Private Sub Preexistence_LoadIndexOfCell_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim liAllData As ListItem
        Try

            If Not IsPostBack Then
                ucUserManage.CheckPower(Session, 1, Response)
                If CType(Session("PowerLevel"), Integer) >= 3 Then
                    liAllData = New ListItem
                    liAllData.Text = "自定义"
                    liAllData.Value = 100
                    ddlWhatTime.Items.Add(liAllData)
                    txtBeginDate.Text = "1988-12-21"
                    txtEndDate.Text = "2016-05-03"
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


        Try


            timerGo.Enabled = False




            bolIsHaveWord = False

            btnRunQuery.Enabled = False
            btnRunQuery.CssClass = "form-control btn-warning"



            strSQLSHandel = "SELECT  * FROM [SanShi_Traffic].[dbo].[dt_GSM_Daily_Grib_Traffic] Where ( "

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
                strSQLSHandel += " ([Day]>='" & gsmioclLibrary.GetGSMIndexMaxDate().AddDays(-ddlWhatTime.SelectedValue + 1).ToShortDateString & "' and [Day]<='" & gsmioclLibrary.GetGSMIndexMaxDate().ToShortDateString & "')"
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
                If (Not IsDateString(txtBeginDate.Text) And Not IsDateString(txtBeginDate.Text)) Then
                    lblWrongDate.Text = "日期格式错误，应为 ""2016-05-30""且范围是2015年至今的日期"
                    plError.Visible = True
                    plGoClickQuery.Visible = False
                    plDownLoadAddress.Visible = False
                    plForShowMessage.Visible = False
                    btnRunQuery.Enabled = True

                    Exit Sub
                End If

                strSQLSHandel += " ([Day]>='" & txtBeginDate.Text & "' and [Day]<='" & txtEndDate.Text & "')"

            End If

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
End Class
