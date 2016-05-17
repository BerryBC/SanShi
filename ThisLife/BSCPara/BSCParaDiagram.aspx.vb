Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports SQLServerLibrary

Partial Class ThisLife_BSCPara_BSCParaDiagram
    Inherits System.Web.UI.Page
    Dim bscpCommonLibrary As BSCPara = New BSCPara
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer
    Shared listBSCParaConfig As List(Of BSCParaSingleDiagram)
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary
    Dim ucUserManage As UserLibrary = New UserLibrary



    Private Sub ThisLife_BSCPara_BSCParaDiagram_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim strJsonLoad As String
            Dim intI As Integer
            Dim ddlitemConfig As ListItem
            Dim bsdlCommonLibrary As BaseSationDetailsLibrary = New BaseSationDetailsLibrary
            Dim dtCellParaDetailsMana As DataTable

            btnGoLastPage.CssClass = "btn btn-danger"
            btnGoNextPage.CssClass = "btn btn-danger"
            btnGoFirstPage.CssClass = "btn btn-danger"
            btnGoFrontPage.CssClass = "btn btn-danger"
            Try
                ucUserManage.CheckPower(Session, 1, Response)

                dtCellParaDetailsMana = bsdlCommonLibrary.GetParameterConfig("GSM Daily Para")
                lblLastUpdateTime.Text = dtCellParaDetailsMana.Rows(0).Item("LastUpdateTime").ToString


                strJsonLoad = File.ReadAllText(Server.MapPath("/ThisLife/BSCPara/Config/GSMBSCParaSingleConfig.json"))
                listBSCParaConfig = SimpleJson.SimpleJson.DeserializeObject(Of List(Of BSCParaSingleDiagram))(strJsonLoad)

                If listBSCParaConfig.Count = 0 Then
                    plFaild.Visible = True
                Else
                    For intI = 0 To listBSCParaConfig.Count - 1
                        ddlitemConfig = New ListItem
                        ddlitemConfig.Text = listBSCParaConfig(intI).strTitle
                        ddlitemConfig.Value = intI.ToString
                        ddlWhichPara.Items.Add(ddlitemConfig)
                    Next
                    BindDataOfBSCPara(0)

                End If
            Catch ex As Exception
                If Session("SanShiUserName") Is Nothing Then
                    erlErrorReport.ReportServerError(11, "", ex.Message, Now)
                    Response.Redirect("/ReportErrorLog.aspx?ep=11&eu=" & "")
                Else
                    erlErrorReport.ReportServerError(11, Session("SanShiUserName"), ex.Message, Now)
                    Response.Redirect("/ReportErrorLog.aspx?ep=11&eu=" & Session("SanShiUserName"))

                End If

            End Try

        End If

    End Sub

    Public Function HowMuchNowUseOfPercent(intWhatNow As Object, intWhatDefine As Object) As Double
        If ((IsNumeric(intWhatNow)) And (IsNumeric(intWhatDefine))) Then
            If CType(intWhatDefine, Integer) > 0 Then

                Return CType(intWhatNow, Integer) / CType(intWhatDefine, Integer)
            Else
                Return 0
            End If
        Else
            Return 0
        End If
    End Function


    Public Function ReturnGateForUp(intWhatNow As Object, intWhatDefine As Object, intYellow As Double, intRed As Double) As String
        Dim dblHowMuchNow As Double
        If ((IsNumeric(intWhatNow)) And (IsNumeric(intWhatDefine))) Then
            If CType(intWhatDefine, Integer) > 0 Then


                dblHowMuchNow = CType(intWhatNow, Integer) / CType(intWhatDefine, Integer)
                If dblHowMuchNow > intRed Then
                    Return "danger"
                ElseIf dblHowMuchNow > intYellow Then
                    Return "warning"
                Else
                    Return "success"
                End If
            Else
                Return "success"
            End If
        Else
            Return "success"
        End If
    End Function

    Private Sub BindDataOfBSCPara(intGoPage As Integer)
        Dim scmdCMD As SqlCommand
        Dim dtBaseSationDetailsMana As DataTable
        Dim intI As Integer
        Dim longJ As Long
        Dim listOnRoad As List(Of Integer)
        Dim drDataInDataTable As DataRow
        Dim tableHtmlTable As HtmlTable
        Dim trHtmlRows As HtmlTableRow
        Dim tdHtmlCells As HtmlTableCell
        Dim intHowManyPageTotal As Integer
        Dim intWhatNowPage As Integer
        Dim strFilterSQL As String
        Dim liststrFilterEle As List(Of String)
        Dim strtmpFilterSQL As String
        Dim dtSortData As DataTable
        Dim drSortDataRow As DataRow
        Dim intOnePageNumber As Integer

        Try

            dtSortData = New DataTable


            intOnePageNumber = CType(ddlHowManyRow.SelectedItem.Value, Integer)

            listOnRoad = listBSCParaConfig(CType(ddlWhichPara.SelectedItem.Value, Integer)).listOnRoad


            '加上对逗号的区分
            strFilterSQL = listBSCParaConfig(CType(ddlWhichPara.SelectedItem.Value, Integer)).strSQLS
            liststrFilterEle = txtSearchWhat.Text.Replace("，", ",").Split(",").ToList
            If liststrFilterEle.Count > 0 Then strFilterSQL += " where "

            For Each strtmpFilterSQL In liststrFilterEle
                strFilterSQL += "[BSC] like '%" & strtmpFilterSQL & "%' or "
            Next

            strFilterSQL = strFilterSQL.Substring(0, strFilterSQL.Length - 3)



            scmdCMD = sqllSSLibrary.GetCommandStr(strFilterSQL, CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
            dtBaseSationDetailsMana = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)


            dtSortData.Columns.Add(New System.Data.DataColumn("BSC", dtBaseSationDetailsMana.Columns(0).DataType))
            dtSortData.Columns.Add(New System.Data.DataColumn(dtBaseSationDetailsMana.Columns(listOnRoad(1)).ColumnName, dtBaseSationDetailsMana.Columns(listOnRoad(1)).DataType))
            dtSortData.Columns.Add(New System.Data.DataColumn(dtBaseSationDetailsMana.Columns(listOnRoad(2)).ColumnName, dtBaseSationDetailsMana.Columns(listOnRoad(2)).DataType))
            dtSortData.Columns.Add(New System.Data.DataColumn("Redundancy", GetType(Integer)))
            dtSortData.Columns.Add(New System.Data.DataColumn("Percent", GetType(Double)))

            For Each drDataInDataTable In dtBaseSationDetailsMana.Rows
                drSortDataRow = dtSortData.NewRow
                drSortDataRow.Item(0) = drDataInDataTable(0)
                drSortDataRow.Item(1) = drDataInDataTable(listOnRoad(1))
                drSortDataRow.Item(2) = drDataInDataTable(listOnRoad(2))
                drSortDataRow.Item(3) = drDataInDataTable(listOnRoad(1)) - drDataInDataTable(listOnRoad(2))
                drSortDataRow.Item(4) = HowMuchNowUseOfPercent(drDataInDataTable(listOnRoad(2)), drDataInDataTable(listOnRoad(1)))
                dtSortData.Rows.Add(drSortDataRow)
            Next

            intWhatNowPage = intGoPage

            intHowManyPageTotal = Int((dtSortData.Rows.Count - 1) / intOnePageNumber)

            If intWhatNowPage > intHowManyPageTotal Then intWhatNowPage = intHowManyPageTotal
            If intWhatNowPage < 0 Then intWhatNowPage = 0

            lblTotal.Text = (intHowManyPageTotal + 1).ToString
            lblNow.Text = (intWhatNowPage + 1).ToString

            tableHtmlTable = tbOutPut
            linkbtnNumerator.Text = dtSortData.Columns(1).Caption
            linkbtnDenominator.Text = dtSortData.Columns(2).Caption

            If lblBSCDown.Visible Then
                dtSortData.DefaultView.Sort = dtSortData.Columns(0).Caption & " DESC"
            ElseIf lblBSCUp.Visible Then
                dtSortData.DefaultView.Sort = dtSortData.Columns(0).Caption & " ASC"
            ElseIf lblNumeratorDown.Visible Then
                dtSortData.DefaultView.Sort = dtSortData.Columns(1).Caption & " DESC"
            ElseIf lblNumeratorUp.Visible Then
                dtSortData.DefaultView.Sort = dtSortData.Columns(1).Caption & " ASC"
            ElseIf lblDenominatorDown.Visible Then
                dtSortData.DefaultView.Sort = dtSortData.Columns(2).Caption & " DESC"
            ElseIf lblDenominatorUp.Visible Then
                dtSortData.DefaultView.Sort = dtSortData.Columns(2).Caption & " ASC"
            ElseIf lblRedundancyDown.Visible Then
                dtSortData.DefaultView.Sort = dtSortData.Columns(3).Caption & " DESC"
            ElseIf lblRedundancyUp.Visible Then
                dtSortData.DefaultView.Sort = dtSortData.Columns(3).Caption & " ASC"
            ElseIf lblPercentDown.Visible Then
                dtSortData.DefaultView.Sort = dtSortData.Columns(4).Caption & " DESC"
            ElseIf lblPercentUp.Visible Then
                dtSortData.DefaultView.Sort = dtSortData.Columns(4).Caption & " ASC"

            End If

            dtSortData = dtSortData.DefaultView.ToTable

            For longJ = intOnePageNumber * intWhatNowPage To CommonLibrary.GetMinNumber(intOnePageNumber * (intWhatNowPage + 1), dtSortData.Rows.Count) - 1
                drDataInDataTable = dtSortData.Rows.Item(longJ)
                trHtmlRows = New HtmlTableRow
                For intI = 0 To 3
                    tdHtmlCells = New HtmlTableCell
                    tdHtmlCells.InnerHtml = drDataInDataTable(intI).ToString
                    trHtmlRows.Cells.Add(tdHtmlCells)
                Next


                tdHtmlCells = New HtmlTableCell
                tdHtmlCells.InnerHtml = "<div class=""progress""> <div id=""proPC"" class=""progress-bar progress-bar-" & ReturnGateForUp(drDataInDataTable(2), drDataInDataTable(1), 0.8, 0.9) & """ role=""progressbar"" aria-valuenow="" " & drDataInDataTable(4) * 100 & """ aria-valuemin=""0"" aria-valuemax=""100"" style=""width: " & drDataInDataTable(4) * 100 & "%""><span >" & CType(drDataInDataTable(4) * 10000, Int16) / 100 & "%</span></div></div>"
                trHtmlRows.Cells.Add(tdHtmlCells)

                tableHtmlTable.Rows.Add(trHtmlRows)

            Next

            If intWhatNowPage >= intHowManyPageTotal Then
                btnGoLastPage.Enabled = False
                btnGoNextPage.Enabled = False
                btnGoLastPage.CssClass = "btn btn-danger"
                btnGoNextPage.CssClass = "btn btn-danger"
            Else
                btnGoLastPage.Enabled = True
                btnGoNextPage.Enabled = True
                btnGoLastPage.CssClass = "btn btn-success"
                btnGoNextPage.CssClass = "btn btn-success"


            End If
            If intWhatNowPage <= 0 Then
                btnGoFirstPage.Enabled = False
                btnGoFrontPage.Enabled = False
                btnGoFirstPage.CssClass = "btn btn-danger"
                btnGoFrontPage.CssClass = "btn btn-danger"
            Else
                btnGoFirstPage.Enabled = True
                btnGoFrontPage.Enabled = True
                btnGoFirstPage.CssClass = "btn btn-success"
                btnGoFrontPage.CssClass = "btn btn-success"

            End If


            scmdCMD.Dispose()
            scmdCMD = Nothing
            dtBaseSationDetailsMana.Dispose()
            dtBaseSationDetailsMana = Nothing
            dtSortData.Dispose()
            dtSortData = Nothing


            'listOnRoad.Clear()
            listOnRoad = Nothing
            drDataInDataTable = Nothing
            tableHtmlTable = Nothing
            trHtmlRows = Nothing
            tdHtmlCells = Nothing
            'liststrFilterEle.Clear()
            liststrFilterEle = Nothing


        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(11, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=11&eu=" & "")
            Else
                erlErrorReport.ReportServerError(11, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=11&eu=" & Session("SanShiUserName"))

            End If

        End Try






    End Sub

    Private Sub btnGoFirstPage_Click(sender As Object, e As EventArgs) Handles btnGoFirstPage.Click
        BindDataOfBSCPara(0)

    End Sub

    Private Sub btnGoNextPage_Click(sender As Object, e As EventArgs) Handles btnGoNextPage.Click
        BindDataOfBSCPara(CType(lblNow.Text, Integer))

    End Sub

    Private Sub btnGoFrontPage_Click(sender As Object, e As EventArgs) Handles btnGoFrontPage.Click
        BindDataOfBSCPara(CType(lblNow.Text, Integer) - 2)

    End Sub

    Private Sub btnGoLastPage_Click(sender As Object, e As EventArgs) Handles btnGoLastPage.Click
        BindDataOfBSCPara(CType(lblTotal.Text, Integer) - 1)

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindDataOfBSCPara(0)

    End Sub

    Private Sub btnGoto_Click(sender As Object, e As EventArgs) Handles btnGoto.Click
        If IsNumeric(txtPage.Text) Then
            BindDataOfBSCPara(CType(txtPage.Text, Integer) - 1)

        End If

    End Sub

    Private Sub linktbnBSC_Click(sender As Object, e As EventArgs) Handles linktbnBSC.Click
        If lblBSCDown.Visible Then
            lblBSCDown.Visible = False
            lblBSCUp.Visible = True
        ElseIf lblBSCUp.Visible Then
            lblBSCDown.Visible = False
            lblBSCUp.Visible = False
        Else
            lblBSCDown.Visible = True
        End If
        'lblBSCDown.Visible = False
        'lblBSCUp.Visible = False
        lblDenominatorDown.Visible = False
        lblDenominatorUp.Visible = False
        lblNumeratorDown.Visible = False
        lblNumeratorUp.Visible = False
        lblPercentDown.Visible = False
        lblPercentUp.Visible = False
        lblRedundancyDown.Visible = False
        lblRedundancyUp.Visible = False
        BindDataOfBSCPara(0)

    End Sub

    Private Sub linkbtnNumerator_Click(sender As Object, e As EventArgs) Handles linkbtnNumerator.Click
        If lblNumeratorDown.Visible Then
            lblNumeratorDown.Visible = False
            lblNumeratorUp.Visible = True
        ElseIf lblNumeratorUp.Visible Then
            lblNumeratorDown.Visible = False
            lblNumeratorUp.Visible = False
        Else
            lblNumeratorDown.Visible = True
        End If
        lblBSCDown.Visible = False
        lblBSCUp.Visible = False
        lblDenominatorDown.Visible = False
        lblDenominatorUp.Visible = False
        'lblNumeratorDown.Visible = False
        'lblNumeratorUp.Visible = False
        lblPercentDown.Visible = False
        lblPercentUp.Visible = False
        lblRedundancyDown.Visible = False
        lblRedundancyUp.Visible = False
        BindDataOfBSCPara(0)

    End Sub

    Private Sub linkbtnDenominator_Click(sender As Object, e As EventArgs) Handles linkbtnDenominator.Click
        If lblDenominatorDown.Visible Then
            lblDenominatorDown.Visible = False
            lblDenominatorUp.Visible = True
        ElseIf lblDenominatorUp.Visible Then
            lblDenominatorDown.Visible = False
            lblDenominatorUp.Visible = False
        Else
            lblDenominatorDown.Visible = True
        End If
        lblBSCDown.Visible = False
        lblBSCUp.Visible = False
        'lblDenominatorDown.Visible = False
        'lblDenominatorUp.Visible = False
        lblNumeratorDown.Visible = False
        lblNumeratorUp.Visible = False
        lblPercentDown.Visible = False
        lblPercentUp.Visible = False
        lblRedundancyDown.Visible = False
        lblRedundancyUp.Visible = False
        BindDataOfBSCPara(0)

    End Sub

    Private Sub linkbtnPercent_Click(sender As Object, e As EventArgs) Handles linkbtnPercent.Click
        If lblPercentDown.Visible Then
            lblPercentDown.Visible = False
            lblPercentUp.Visible = True
        ElseIf lblPercentUp.Visible Then
            lblPercentDown.Visible = False
            lblPercentUp.Visible = False
        Else
            lblPercentDown.Visible = True
        End If
        lblBSCDown.Visible = False
        lblBSCUp.Visible = False
        lblDenominatorDown.Visible = False
        lblDenominatorUp.Visible = False
        lblNumeratorDown.Visible = False
        lblNumeratorUp.Visible = False
        'lblPercentDown.Visible = False
        'lblPercentUp.Visible = False
        lblRedundancyDown.Visible = False
        lblRedundancyUp.Visible = False
        BindDataOfBSCPara(0)

    End Sub

    Private Sub linkbtnRedundancy_Click(sender As Object, e As EventArgs) Handles linkbtnRedundancy.Click
        If lblRedundancyDown.Visible Then
            lblRedundancyDown.Visible = False
            lblRedundancyUp.Visible = True
        ElseIf lblRedundancyUp.Visible Then
            lblRedundancyDown.Visible = False
            lblRedundancyUp.Visible = False
        Else
            lblRedundancyDown.Visible = True
        End If
        lblBSCDown.Visible = False
        lblBSCUp.Visible = False
        lblDenominatorDown.Visible = False
        lblDenominatorUp.Visible = False
        lblNumeratorDown.Visible = False
        lblNumeratorUp.Visible = False
        lblPercentDown.Visible = False
        lblPercentUp.Visible = False
        'lblRedundancyDown.Visible = False
        'lblRedundancyUp.Visible = False
        BindDataOfBSCPara(0)

    End Sub

    Private Sub ddlWhichPara_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlWhichPara.SelectedIndexChanged
        BindDataOfBSCPara(0)

    End Sub

    Private Sub ddlHowManyRow_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHowManyRow.SelectedIndexChanged
        BindDataOfBSCPara(0)

    End Sub
End Class
