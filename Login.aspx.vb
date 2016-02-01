Imports PageMessage
Imports UserLibrary
Imports System.Data
Imports CommonLibrary
Imports ErrorReportLibrary
Partial Class Login
    Inherits System.Web.UI.Page


    Dim pmMsgClass As PageMessage = New PageMessage
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim clCommon As CommonLibrary = New CommonLibrary
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary




    Protected Sub btnGoRegister_Click(sender As Object, e As EventArgs) Handles btnGoRegister.Click
        Response.Redirect("/Register.aspx")
    End Sub




    Protected Sub btnLogIn_Click(sender As Object, e As EventArgs) Handles btnLogIn.Click

        Dim dtUserDetail As DataTable
        Dim hcCookie As HttpCookie


        If (txtUserName.Text.Trim <> "" And txtPassword.Text.Trim <> "") Then
            Try
                dtUserDetail = ucUserManage.UserLoginCheck(txtUserName.Text, txtPassword.Text, Request.UserAgent.ToString)
            Catch ex As Exception
                If Session("SanShiUserName") Is Nothing Then
                    erlErrorReport.ReportServerError(1, "", ex.Message, Now)
                    Response.Redirect("/ReportErrorLog.aspx?ep=1&eu=" & "")
                    dtUserDetail = Nothing
                Else
                    erlErrorReport.ReportServerError(1, Session("SanShiUserName"), ex.Message, Now)
                    Response.Redirect("/ReportErrorLog.aspx?ep=1&eu=" & Session("SanShiUserName"))
                    dtUserDetail = Nothing

                End If

            End Try

            If (dtUserDetail.Rows.Count > 0) Then
                Session("SanShiUserName") = dtUserDetail.Rows(0)("UserName").ToString
                Session("PowerLevel") = dtUserDetail.Rows(0)("PowerLevel").ToString
                Application("NowUser:" & dtUserDetail.Rows(0)("UserName").ToString) = Request.UserAgent.ToString


                If cbRemember.Checked = True Then

                    hcCookie = New HttpCookie("SanShiUserInfo")
                    hcCookie.Values.Add("SanShiUserName", dtUserDetail.Rows(0)("UserName").ToString)
                    hcCookie.Values.Add("MachineName", clCommon.StringTranscodingToMD5(Request.UserAgent.ToString).ToString)
                    hcCookie.Expires = DateTime.MaxValue
                    Response.AppendCookie(hcCookie)
                End If
                Response.Redirect("/Main.aspx")
            Else
                plError.Visible = True
                lblErrorMessage.Text = "密码错！"
                form1.Focus()

            End If

        Else
            plError.Visible = True
            lblErrorMessage.Text = "用户名或密码为空诶！"
            form1.Focus()
        End If
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim bolIsGoInside As Boolean
        If Not IsPostBack Then

            Try


                bolIsGoInside = ucUserManage.GetInOrGetOut(Session, Request.Cookies.Get("SanShiUserInfo"), Request.UserAgent.ToString, Application)
            Catch ex As Exception
                If Session("SanShiUserName") Is Nothing Then
                    erlErrorReport.ReportServerError(1, "", ex.Message, Now)
                    Response.Redirect("/ReportErrorLog.aspx?ep=1&eu=" & "")
                Else
                    erlErrorReport.ReportServerError(1, Session("SanShiUserName"), ex.Message, Now)
                    Response.Redirect("/ReportErrorLog.aspx?ep=1&eu=" & Session("SanShiUserName"))

                End If

            End Try
            If bolIsGoInside Then
                Response.Redirect("/Main.aspx")

            End If
        End If
    End Sub
End Class
