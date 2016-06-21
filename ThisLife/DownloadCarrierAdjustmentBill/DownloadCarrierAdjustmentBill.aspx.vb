Imports SQLServerLibrary
Imports System.Data.SqlClient
Imports System.Data
Imports ExcelLibrary
Imports CSVLibrary

Partial Class ThisLife_DownloadCarrierAdjustmentBill_DownloadCarrierAdjustmentBill
    Inherits System.Web.UI.Page
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary

    Private Sub ThisLife_DownloadCarrierAdjustmentBill_DownloadCarrierAdjustmentBill_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                ucUserManage.CheckPower(Session, 1, Response)
            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(25, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=25&eu=" & "")
            Else
                erlErrorReport.ReportServerError(25, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=25&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub

    Private Sub btnRunQuery_Click(sender As Object, e As EventArgs) Handles btnRunQuery.Click
        Dim bolIsHaveWord As Boolean

        Try


            bolIsHaveWord = False

            btnRunQuery.Enabled = False
            btnRunQuery.CssClass = "form-control btn-warning"

            If txtNumberOfBill.Text.Length > 0 Then
                bolIsHaveWord = True
            End If
            If txtCell.Text.Length > 0 Then
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
                erlErrorReport.ReportServerError(25, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=25&eu=" & "")
            Else
                erlErrorReport.ReportServerError(25, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=25&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub

    Private Sub timerGo_Tick(sender As Object, e As EventArgs) Handles timerGo.Tick
        Dim strNumberOfBill() As String
        Dim strCell() As String
        Dim strSQLSHandel As String
        Dim strSingleWord As String
        Dim bolIsHaveWord As Boolean

        Try

            timerGo.Enabled = False

            bolIsHaveWord = False

            btnRunQuery.Enabled = False
            btnRunQuery.CssClass = "form-control btn-warning"


            strSQLSHandel = "select dtGCD.[Cell]  as 小区名 ,dtGCD.[TRX]   as 现网载波数  ,dtGCD.[NumberOfFN]  as 现网频点数 ,dtCB.[编号] ,dtCB.[NAMS编码] ,dtCB.[提单单号] ,dtCB.[CELL_ID] ,dtCB.[基站名] ,dtCB.[BSC] ,dtCB.[频段] ,dtCB.[设备类型]  ,dtCB.[合路器类型]  ,dtCB.[原网载波数]  ,dtCB.[调整载波数] ,dtCB.[调整后载波数] ,dtCB.[调整合路器数（原始）] ,dtCB.[调整合路器数/DUG数] ,dtCB.[调整机架数/子框数] ,dtCB.[提单日期] ,dtCB.[备注] ,dtCB.[提单时调整类型] ,dtCB.[载波调整细类] ,dtCB.[6000的v1/v2版本]  ,dtCB.[调整类型备注(6000系列软调/硬调、换型类型）] ,dtCB.[所属区域] ,dtCB.[涉及减EDGE信道时可将EDGE信道数减容到] ,dtCB.[是否需考虑室分接入情况（是/否）] ,dtCB.[是否是(是/否/部分是)] ,dtCB.[操作内容] ,dtCB.[实际调整类型（载波调整/加向/6601远端机扩容/换型）] ,dtCB.[操作人] ,dtCB.[实施时间] ,dtNAMS.[工单号] ,dtNAMS.[工单主题] ,dtNAMS.[工单类型]  ,dtNAMS.[工单分类] ,dtNAMS.[工单状态] ,dtNAMS.[操作类型] ,dtNAMS.[区域] ,dtNAMS.[代维公司]  ,dtNAMS.[实施人] ,dtNAMS.[实施人电话] ,dtNAMS.[基站名]  ,dtNAMS.[小区名] ,dtNAMS.[派单时间] ,dtNAMS.[实施结果] ,dtNAMS.[实施完成时间] ,dtNAMS.[工单闭环时间] ,dtNAMS.[当前待办步骤]  ,dtNAMS.[当前待办人] ,dtNAMS.[无法实施归属] ,dtNAMS.[无法实施原因] ,dtNAMS.[物资来源] ,dtNAMS.[现网调整工单号] FROM ([SanShi_BaseSationDetails].[dbo].[dt_GSM_CarrierBill] dtCB left join   [SanShi_BaseSationDetails].[dbo].[dt_NAMS] dtNAMS on  dtCB.[NAMS编码] = dtNAMS.[工单号] ) left join    [SanShi_BaseSationDetails].[dbo].[dt_GSMP_Cell_Daily] dtGCD  on  dtCB.[CELL_ID] = dtGCD.[Cell]       Where ( "

            strNumberOfBill = txtNumberOfBill.Text.Split(",")
            strCell = txtCell.Text.Split(",")

            If txtNumberOfBill.Text.Length > 0 Then
                For Each strSingleWord In strNumberOfBill
                    strSingleWord = strSingleWord.Replace(" ", "")
                    strSingleWord = strSingleWord.Replace("'", "")
                    strSQLSHandel += "dtCB.[提单单号] like '%" & strSingleWord & "%' or "
                Next
                bolIsHaveWord = True
            End If
            If txtCell.Text.Length > 0 Then
                For Each strSingleWord In strCell
                    strSingleWord = strSingleWord.Replace(" ", "")
                    strSingleWord = strSingleWord.Replace("'", "")
                    strSQLSHandel += "dtCB.[CELL_ID] like '" & strSingleWord & "' or "
                Next
                bolIsHaveWord = True
            End If
            If bolIsHaveWord Then
                strSQLSHandel = Left(strSQLSHandel, Len(strSQLSHandel) - 3) & ")  "
            Else
                lblWrongDate.Text = "不能不设置条件"
                plError.Visible = True
                plGoClickQuery.Visible = False
                plDownLoadAddress.Visible = False
                plForShowMessage.Visible = False
                btnRunQuery.Enabled = True

                Exit Sub

            End If


            SaveIndexFile(strSQLSHandel)
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(25, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=25&eu=" & "")
            Else
                erlErrorReport.ReportServerError(25, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=25&eu=" & Session("SanShiUserName"))

            End If

        End Try

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
        Dim intHowManyRowDisPlay As Integer


        Try

            intHowManyRowDisPlay = 10
            intBeford = 0

            scmdCommand = sqllSSLibrary.GetCommandStr(strSQLS, CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
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
                If intBeford >= intHowManyRowDisPlay Then
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

            If dtIndexOfGSMCell.Rows.Count > intHowManyRowDisPlay Then
                lblLoading.Text = "还剩余 " & dtIndexOfGSMCell.Rows.Count - intHowManyRowDisPlay & " 行没有显示，请下载完整数据"
                plForShowMessage.Visible = True
            End If


            strSaveFileName = Now.ToString("yyyyMMddHHmmss") & "-GSM载调进度表-" & Session("SanShiUserName") & ".csv"

            csvCSV = New LoadCSV(Server.MapPath("/TmpFiles/") & strSaveFileName)
            bolSaveSuccess = csvCSV.SaveASNewOne(dtIndexOfGSMCell)
            hlDownloadLink.Text = Request.Url.Host & "/TmpFiles/" & strSaveFileName
            hlDownloadLink.NavigateUrl = "/TmpFiles/" & strSaveFileName

            plGoClickQuery.Visible = False
            plDownLoadAddress.Visible = True
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(25, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=25&eu=" & "")
            Else
                erlErrorReport.ReportServerError(25, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=25&eu=" & Session("SanShiUserName"))

            End If

        End Try


    End Sub




End Class
