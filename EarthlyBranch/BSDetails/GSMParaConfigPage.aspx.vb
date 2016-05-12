Imports System.Data
Imports System.IO
Imports System.Web

Partial Class EarthlyBranch_BSDetails_GSMParaConfig_GSMParaConfigPage
    Inherits System.Web.UI.Page
    Dim dtBaseSationDetailsMana As DataTable
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary
    Dim ucUserManage As UserLibrary = New UserLibrary


    Private Sub EarthlyBranch_BSDetails_GSMParaConfig_GSMParaConfigPage_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                ucUserManage.CheckPower(Session, 9, Response)

                If Session.Item("ParaAndSQL") IsNot Nothing Then
                    dtBaseSationDetailsMana = CType(Session.Item("ParaAndSQL"), DataTable)
                Else
                    LoadJsonToDataTable()

                End If
                BindToDataList()

            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(18, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=18&eu=" & "")
            Else
                erlErrorReport.ReportServerError(18, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=18&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub


    Private Sub LoadJsonToDataTable()
        Dim strJsonLoad As String
        Dim strBSCConfigAddress As String
        Dim strCellConfigAddress As String
        Dim strConfigFile As String
        Dim sabConfigSQLSandBSCList As SQLSandBSCList
        Dim pastmpParaAndSQLS As ParameterAndSQL
        Dim tbltmpBscList As TableBSCList
        Dim drTmp As DataRow
        Dim i As Integer

        i = 0

        Try

            strBSCConfigAddress = Server.MapPath("/EarthlyBranch/BSDetails/Config/BSCParaConfig.json")
            strCellConfigAddress = Server.MapPath("/EarthlyBranch/BSDetails/Config/GSMCellParaConfig.json")

            If ddlBSCorCell.SelectedValue = "BSC" Then
                strConfigFile = strBSCConfigAddress
            Else
                strConfigFile = strCellConfigAddress
            End If


            strJsonLoad = File.ReadAllText(strConfigFile)
            sabConfigSQLSandBSCList = SimpleJson.SimpleJson.DeserializeObject(Of SQLSandBSCList)(strJsonLoad)

            dtBaseSationDetailsMana = New DataTable
            dtBaseSationDetailsMana.Columns.Add("1st", GetType(String))
            dtBaseSationDetailsMana.Columns.Add("2nd", GetType(String))
            dtBaseSationDetailsMana.Columns.Add("Number", GetType(Integer))

            If ddlNeOrPara.SelectedValue = "NE" Then
                For Each tbltmpBscList In sabConfigSQLSandBSCList.listtblBscList
                    drTmp = dtBaseSationDetailsMana.NewRow
                    drTmp.Item(0) = tbltmpBscList.strBSCName
                    drTmp.Item(1) = tbltmpBscList.strTableName
                    drTmp.Item(2) = i
                    i += 1
                    dtBaseSationDetailsMana.Rows.Add(drTmp)
                Next

            Else
                For Each pastmpParaAndSQLS In sabConfigSQLSandBSCList.listpasParaAndSQLS
                    drTmp = dtBaseSationDetailsMana.NewRow
                    drTmp.Item(0) = pastmpParaAndSQLS.strColName
                    drTmp.Item(1) = pastmpParaAndSQLS.strSQLStatements
                    drTmp.Item(2) = i
                    i += 1
                    dtBaseSationDetailsMana.Rows.Add(drTmp)
                Next

            End If
            Session.Item("ParaAndSQL") = dtBaseSationDetailsMana

        Catch ex As Exception
            Throw New Exception(ex.Message, ex)


        End Try

    End Sub
    Private Sub BindToDataList()
        Try

            dlParaConfigOfBSCorCell.DataSource = dtBaseSationDetailsMana
            dlParaConfigOfBSCorCell.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)

        End Try
    End Sub

    Private Sub RewriteToDataTable()
        Dim txtGetValueFirst As TextBox
        Dim txtGetValueSecond As TextBox
        Dim intGetNumber As Integer
        Dim dliItemOfEveryRow As DataListItem
        Dim drTmp As DataRow

        Try


            dtBaseSationDetailsMana = New DataTable
            dtBaseSationDetailsMana.Columns.Add("1st", GetType(String))
            dtBaseSationDetailsMana.Columns.Add("2nd", GetType(String))
            dtBaseSationDetailsMana.Columns.Add("Number", GetType(Integer))


            For Each dliItemOfEveryRow In dlParaConfigOfBSCorCell.Controls
                If dliItemOfEveryRow.ItemType = ListItemType.AlternatingItem Or dliItemOfEveryRow.ItemType = ListItemType.Item Then
                    drTmp = dtBaseSationDetailsMana.NewRow

                    txtGetValueFirst = dliItemOfEveryRow.FindControl("txtFirst")
                    txtGetValueSecond = dliItemOfEveryRow.FindControl("txtSecond")
                    intGetNumber = CType(CType(dliItemOfEveryRow.FindControl("lblNumber"), Label).Text, Integer)

                    drTmp.Item(0) = txtGetValueFirst.Text
                    drTmp.Item(1) = txtGetValueSecond.Text
                    drTmp.Item(2) = intGetNumber

                    dtBaseSationDetailsMana.Rows.Add(drTmp)

                End If
            Next
            Session.Item("ParaAndSQL") = dtBaseSationDetailsMana
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)

        End Try

    End Sub

    Private Sub btnAddOne_Click(sender As Object, e As EventArgs) Handles btnAddOne.Click
        Dim drTmp As DataRow
        Dim drTmpSecond As DataRow
        Dim i As Integer
        Try
            i = 0
            RewriteToDataTable()
            drTmp = dtBaseSationDetailsMana.NewRow
            drTmp.Item(0) = ""
            drTmp.Item(1) = ""
            For Each drTmpSecond In dtBaseSationDetailsMana.Rows
                If CType(drTmpSecond.Item(2), Integer) > i Then
                    i = CType(drTmpSecond.Item(2), Integer)
                End If
            Next

            drTmp.Item(2) = i + 1
            dtBaseSationDetailsMana.Rows.Add(drTmp)
            Session.Item("ParaAndSQL") = dtBaseSationDetailsMana
            BindToDataList()
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(18, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=18&eu=" & "")
            Else
                erlErrorReport.ReportServerError(18, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=18&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub

    Private Sub ddlBSCorCell_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBSCorCell.SelectedIndexChanged
        Try
            LoadJsonToDataTable()
            BindToDataList()
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(18, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=18&eu=" & "")
            Else
                erlErrorReport.ReportServerError(18, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=18&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub

    Private Sub ddlNeOrPara_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNeOrPara.SelectedIndexChanged
        Try
            LoadJsonToDataTable()
            BindToDataList()
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(18, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=18&eu=" & "")
            Else
                erlErrorReport.ReportServerError(18, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=18&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub

    Public Sub dlParaConfigOfBSCorCell_ItemCommand(source As Object, e As DataListCommandEventArgs) Handles dlParaConfigOfBSCorCell.ItemCommand
        Dim i As Integer
        Dim drTmp As DataRow

        Try

            If Session.Item("ParaAndSQL") IsNot Nothing Then
                dtBaseSationDetailsMana = CType(Session.Item("ParaAndSQL"), DataTable)
            Else
                RewriteToDataTable()

            End If

            If e.CommandName = "Delete" Then
                i = (Convert.ToInt32(e.CommandArgument))
                For Each drTmp In dtBaseSationDetailsMana.Rows
                    If CType(drTmp.Item(2), Integer) = i Then
                        dtBaseSationDetailsMana.Rows.Remove(drTmp)
                        Exit For
                    End If
                Next
                Session.Item("ParaAndSQL") = dtBaseSationDetailsMana

                BindToDataList()
            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(18, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=18&eu=" & "")
            Else
                erlErrorReport.ReportServerError(18, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=18&eu=" & Session("SanShiUserName"))

            End If

        End Try


    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim swSaveStream As StreamWriter
        Dim strBSCConfigAddress As String
        Dim strCellConfigAddress As String
        Dim strConfigFile As String
        Dim strJsonLoad As String
        Dim sabConfigSQLSandBSCList As SQLSandBSCList
        Dim drTmp As DataRow


        Try


            strBSCConfigAddress = Server.MapPath("/EarthlyBranch/BSDetails/Config/BSCParaConfig.json")
            strCellConfigAddress = Server.MapPath("/EarthlyBranch/BSDetails/Config/GSMCellParaConfig.json")

            If ddlBSCorCell.SelectedValue = "BSC" Then
                strConfigFile = strBSCConfigAddress
            Else
                strConfigFile = strCellConfigAddress
            End If

            If File.Exists(strConfigFile) Then
                strJsonLoad = File.ReadAllText(strConfigFile)
                sabConfigSQLSandBSCList = SimpleJson.SimpleJson.DeserializeObject(Of SQLSandBSCList)(strJsonLoad)
            Else
                sabConfigSQLSandBSCList = New SQLSandBSCList
            End If


            RewriteToDataTable()

            If ddlNeOrPara.SelectedValue = "NE" Then
                sabConfigSQLSandBSCList.listtblBscList = New List(Of TableBSCList)
                For Each drTmp In dtBaseSationDetailsMana.Rows

                    sabConfigSQLSandBSCList.listtblBscList.Add(New TableBSCList With {.strBSCName = drTmp.Item(0), .strTableName = drTmp.Item(1)})

                Next

            Else
                sabConfigSQLSandBSCList.listpasParaAndSQLS = New List(Of ParameterAndSQL)
                For Each drTmp In dtBaseSationDetailsMana.Rows

                    sabConfigSQLSandBSCList.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = drTmp.Item(0), .strSQLStatements = drTmp.Item(1)})

                Next

            End If

            If Not System.IO.Directory.Exists(Server.MapPath("/EarthlyBranch/BSDetails/Config/")) Then
                My.Computer.FileSystem.CreateDirectory(Server.MapPath("/EarthlyBranch/BSDetails/Config/"))
            End If

            swSaveStream = New StreamWriter(File.Open(strConfigFile, FileMode.Create))
            strJsonLoad = SimpleJson.SimpleJson.SerializeObject(sabConfigSQLSandBSCList)
            swSaveStream.Write(strJsonLoad)
            swSaveStream.Flush()
            swSaveStream.Close()
            swSaveStream = Nothing

        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(18, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=18&eu=" & "")
            Else
                erlErrorReport.ReportServerError(18, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=18&eu=" & Session("SanShiUserName"))

            End If

        End Try


    End Sub
End Class
