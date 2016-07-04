Imports System.Data
Imports System

Partial Class ThisLife_RenewTheGuaranteeCellList_RenewGCellList
    Inherits System.Web.UI.Page
    Dim rgclUpdateCell As RenewGCellListLibrary
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary
    Dim ucUserManage As UserLibrary = New UserLibrary

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim hfcFileCollection As HttpFileCollection
        Dim bwGetEnterWorker As BackgroundWorker
        Dim arrobjParaOfBGWorker As Object()
        Dim hpfEveryFile As HttpPostedFile
        Dim strFilePath As String

        Try


            If Application(Session("SanShiUserName") & "bwRenewGCellList") Is Nothing Then
                btnGo.OnClientClick = ""


                hfcFileCollection = Request.Files

                If fuFileA.HasFile Then

                    txtLogMessage.Text += "正在上传，请耐心等待处理" & vbCrLf
                    btnGo.OnClientClick = "javascript:{return false;}"
                    plUpdating.Visible = True
                    plDownload.Visible = False

                    timerDelay.Enabled = True



                    hpfEveryFile = hfcFileCollection(0)
                    strFilePath = Server.MapPath("/TmpFiles/") & Now.ToString("yyyyMMddHHmmss") & "-保障小区列表上传-" & Session("SanShiUserName") & "-" & System.IO.Path.GetFileName(hpfEveryFile.FileName)
                    hpfEveryFile.SaveAs(strFilePath)

                    arrobjParaOfBGWorker = {strFilePath, System.IO.Path.GetFileName(hpfEveryFile.FileName)}

                    bwGetEnterWorker = New BackgroundWorker
                    AddHandler bwGetEnterWorker.DoWork, AddressOf bwGetEnterWorker_DoWork
                    bwGetEnterWorker.RunWorker(arrobjParaOfBGWorker)
                    Application(Session("SanShiUserName") & "bwRenewGCellList") = bwGetEnterWorker

                Else
                    txtLogMessage.Text += "你还没有上传文件哦~" & vbCrLf
                    txtLogMessage.Text += "---------------------------------------" & vbCrLf
                End If

            End If

        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(20, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=20&eu=" & "")
            Else
                erlErrorReport.ReportServerError(20, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=20&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub

    Private Sub ThisLife_RenewTheGuaranteeCellList_RenewGCellList_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtLogMessage.CssClass = "form-control"
        btnGo.CssClass = "form-control btn-warning"
        Try

            If Not IsPostBack Then
                ucUserManage.CheckPower(Session, 2, Response)

                txtLogMessage.Text = ""
                plDownload.Visible = False
                plUpdating.Visible = False
                btnGo.OnClientClick = "javascript:{var stamp = document.getElementById('.*btnGo.*');stamp.disabled=true;}"
                ucUserManage.CheckPower(Session, 1, Response)

            End If

            If Application(Session("SanShiUserName") & "bwRenewGCellList") Is Nothing Then
                btnGo.OnClientClick = ""
            Else
                timerDelay.Enabled = True
                plUpdating.Visible = True
            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(20, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=20&eu=" & "")
            Else
                erlErrorReport.ReportServerError(20, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=20&eu=" & Session("SanShiUserName"))

            End If

        End Try


    End Sub

    Private Sub timerDelay_Tick(sender As Object, e As EventArgs) Handles timerDelay.Tick
        Dim globalWorker As BackgroundWorker
        Dim strFilePath As String

        Try



            globalWorker = Nothing
            If Application(Session("SanShiUserName") & "bwRenewGCellList") IsNot Nothing Then
                globalWorker = DirectCast(Application(Session("SanShiUserName") & "bwRenewGCellList"), BackgroundWorker)
            Else
                btnGo.OnClientClick = ""
                plUpdating.Visible = False
                timerDelay.Enabled = False
                btnGo.OnClientClick = ""
                Application(Session("SanShiUserName") & "bwRenewGCellList") = Nothing

                Exit Sub
            End If
            If lblLoading.Text.Length > 40 Then
                lblLoading.Text = lblLoading.Text.Substring(0, 20)
            End If
            lblLoading.Text = lblLoading.Text & "."


            If globalWorker.Result IsNot Nothing Then
                If globalWorker.Result = "No!" Then
                    txtLogMessage.Text += "你还没有上传文件哦~" & vbCrLf
                    txtLogMessage.Text += "---------------------------------------" & vbCrLf
                ElseIf globalWorker.Result.ToString.Substring(0, 4) = "Yes!" Then
                    strFilePath = globalWorker.Result.ToString.Substring(4)

                    txtLogMessage.Text += "已经完成了喵~" & vbCrLf
                    txtLogMessage.Text += "---------------------------------------" & vbCrLf
                    plDownload.Visible = True
                    hlDownloadLink.Text = Request.Url.Host & "/TmpFiles/" & strFilePath
                    hlDownloadLink.NavigateUrl = "/TmpFiles/" & strFilePath


                Else

                    txtLogMessage.Text += "出错了喵~，错误为" & vbCrLf
                    txtLogMessage.Text += globalWorker.Result & vbCrLf
                    txtLogMessage.Text += "---------------------------------------" & vbCrLf

                End If
                plUpdating.Visible = False
                timerDelay.Enabled = False
                btnGo.OnClientClick = ""
                Application(Session("SanShiUserName") & "bwRenewGCellList") = Nothing
            End If

        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(20, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=20&eu=" & "")
            Else
                erlErrorReport.ReportServerError(20, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=20&eu=" & Session("SanShiUserName"))

            End If

        End Try


    End Sub



    ''' <summary>
    ''' This operation will work without the end.
    ''' </summary>
    Private Sub bwGetEnterWorker_DoWork(ByRef progress As Integer, ByRef _result As Object, ByRef OutputTheTmp As String, ByVal ParamArray arguments As Object())
        Dim strFilePath As String
        Dim dsAllCellList As DataSet
        Dim bolIsUpdateSuccess As Boolean
        Dim strGuaranteeName As String
        Dim strFileName As String
        Dim strSaveFileName As String

        strSaveFileName = ""

        Try

            strFilePath = arguments(0)
            strFileName = arguments(1)

            If fuFileA.HasFile Then
                dsAllCellList = RenewGCellListLibrary.GetCellDataSet(strFilePath)
                strGuaranteeName = RenewGCellListLibrary.LoadTheGuaranteeName(strFilePath)
                bolIsUpdateSuccess = RenewGCellListLibrary.ImportGCellToSQLServer(dsAllCellList, Session("SanShiUserName"), strGuaranteeName)
                txtLogMessage.Text += strGuaranteeName & vbCrLf


                strSaveFileName = Now.ToString("yyyyMMddHHmmss") & "-保障小区列表更新后-" & Session("SanShiUserName") & "-" & strFileName

                strFilePath = Server.MapPath("/TmpFiles/") & strSaveFileName
                dsAllCellList = RenewGCellListLibrary.UpdateCellsRows(dsAllCellList, Session("SanShiUserName"), strGuaranteeName)
                IO.File.Copy(Server.MapPath("/ThisLife/RenewTheGuaranteeCellList/Files/TemplateFiles/" & "Export.xlsx"), strFilePath)
                bolIsUpdateSuccess = RenewGCellListLibrary.SaveDataSetToExcel(dsAllCellList, strFilePath)

            Else
                strFilePath = "No!"

            End If


            _result = "Yes!" & strSaveFileName
        Catch ex As Exception
            _result = ex.Message

        End Try

    End Sub




End Class
