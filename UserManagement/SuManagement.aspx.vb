Imports System.Data

Partial Class UserManagement_SuManagement
    Inherits System.Web.UI.Page
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary
    Shared psDataSource As PagedDataSource = New PagedDataSource

    Protected Sub dlSuUserDo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dlSuUserDo.SelectedIndexChanged
    End Sub

    Private Sub UserManagement_SuManagement_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim bolIsPowerEnough As Boolean = False
        If Not IsPostBack Then
            Try

                bolIsPowerEnough = ucUserManage.CheckPower(Session, 9, Response)
                If bolIsPowerEnough Then BindDataDataList(0)
            Catch ex As Exception
                If Session("SanShiUserName") Is Nothing Then
                    erlErrorReport.ReportServerError(8, "", ex.Message, Now)
                    Response.Redirect("/ReportErrorLog.aspx?ep=8&eu=" & "")
                Else
                    erlErrorReport.ReportServerError(8, Session("SanShiUserName"), ex.Message, Now)
                    Response.Redirect("/ReportErrorLog.aspx?ep=8&eu=" & Session("SanShiUserName"))

                End If

            End Try
        End If
    End Sub

    Private Sub BindDataDataList(intCurrentPage As Integer)
        Dim dtUserInfo As DataTable
        If ((intCurrentPage < 0) Or (intCurrentPage >= psDataSource.PageCount)) Then
            intCurrentPage = 0
        End If


        Try


            dtUserInfo = ucUserManage.ReturnAllUserInfo(txtSearchWhat.Text)
            psDataSource.DataSource = dtUserInfo.DefaultView
            psDataSource.AllowPaging = 10
            psDataSource.CurrentPageIndex = intCurrentPage
            dlSuUserDo.DataSource = psDataSource
            dlSuUserDo.DataBind()
            If psDataSource.IsLastPage Then
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
            If psDataSource.IsFirstPage Then
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
            lblNow.Text = (psDataSource.CurrentPageIndex + 1).ToString
            lblTotal.Text = (psDataSource.PageCount).ToString
            plChangeLevel.Visible = False
            plOK.Visible = False
            plFaild.Visible = False
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(8, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=8&eu=" & "")
            Else
                erlErrorReport.ReportServerError(8, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=8&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub

    Private Sub btnGoFirstPage_Click(sender As Object, e As EventArgs) Handles btnGoFirstPage.Click

        psDataSource.CurrentPageIndex = 0
        BindDataDataList(psDataSource.CurrentPageIndex)
    End Sub

    Private Sub btnGoFrontPage_Click(sender As Object, e As EventArgs) Handles btnGoFrontPage.Click
        psDataSource.CurrentPageIndex = psDataSource.CurrentPageIndex - 1
        BindDataDataList(psDataSource.CurrentPageIndex)

    End Sub

    Private Sub btnGoLastPage_Click(sender As Object, e As EventArgs) Handles btnGoLastPage.Click
        psDataSource.CurrentPageIndex = psDataSource.PageCount - 1
        BindDataDataList(psDataSource.CurrentPageIndex)

    End Sub

    Private Sub btnGoNextPage_Click(sender As Object, e As EventArgs) Handles btnGoNextPage.Click
        psDataSource.CurrentPageIndex = psDataSource.CurrentPageIndex + 1
        BindDataDataList(psDataSource.CurrentPageIndex)


    End Sub

    Private Sub btnGoto_Click(sender As Object, e As EventArgs) Handles btnGoto.Click
        Dim intGoToPage As Integer
        If txtPage.Text <> "" Then
            intGoToPage = Convert.ToInt32(txtPage.Text)
            If ((intGoToPage >= 1) Or (intGoToPage < psDataSource.PageCount)) Then
                BindDataDataList(intGoToPage - 1)
            Else
                BindDataDataList(0)

            End If

        End If

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindDataDataList(0)

    End Sub

    Private Sub dlSuUserDo_ItemCommand(source As Object, e As DataListCommandEventArgs) Handles dlSuUserDo.ItemCommand
        plChangeLevel.Visible = False
        plFixPassword.Visible = False
        plOK.Visible = False
        plFaild.Visible = False

        If e.CommandName = "Modify Power" Then
            plChangeLevel.Visible = True
            lblChangeLevelUserName.Text = CType(e.Item.FindControl("lblUserName"), Label).Text
            lblNowLevel.Text = CType(e.Item.FindControl("lblPowerLevel"), Label).Text
        End If


        If e.CommandName = "Fix Password" Then
            plFixPassword.Visible = True
            lblFixPassword.Text = CType(e.Item.FindControl("lblUserName"), Label).Text
        End If

    End Sub

    Private Sub btnChangeLevel_Click(sender As Object, e As EventArgs) Handles btnChangeLevel.Click
        Try

            Dim bolIsDoThingSuccess As Boolean
            bolIsDoThingSuccess = ucUserManage.ChangeUserPowerLevel(lblChangeLevelUserName.Text, CType(txtChangeLevel.Text, Integer))
            If bolIsDoThingSuccess Then
                plOK.Visible = True
            Else
                plFaild.Visible = False
            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(8, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=8&eu=" & "")
            Else
                erlErrorReport.ReportServerError(8, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=8&eu=" & Session("SanShiUserName"))

            End If


        End Try
    End Sub

    Private Sub btnBackPL_Click(sender As Object, e As EventArgs) Handles btnBackPL.Click
        plChangeLevel.Visible = False
        txtChangeLevel.Text = ""
        plOK.Visible = False
        plFaild.Visible = False
    End Sub

    Private Sub btnBackFPW_Click(sender As Object, e As EventArgs) Handles btnBackFPW.Click
        plFixPassword.Visible = False
        plOK.Visible = False
        plFaild.Visible = False
    End Sub

    Private Sub btnGoFixPW_Click(sender As Object, e As EventArgs) Handles btnGoFixPW.Click
        Try

            Dim bolIsDoThingSuccess As Boolean
            bolIsDoThingSuccess = ucUserManage.ChangeUserPassword(lblFixPassword.Text, "123456")
            If bolIsDoThingSuccess Then
                plOK.Visible = True
            Else
                plFaild.Visible = False
            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(8, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=8&eu=" & "")
            Else
                erlErrorReport.ReportServerError(8, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=8&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub
End Class
